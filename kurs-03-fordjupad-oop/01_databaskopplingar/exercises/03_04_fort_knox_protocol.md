# Fort Knox Protocol: Säkra din EF Core-lösning

🟢


## NuGet-paket du behöver

- `Microsoft.Extensions.Logging.Console` (för att se vad som loggas)
- `Microsoft.EntityFrameworkCore.Relational` (ingår ofta redan)

> Inga nya paket? Perfekt – fokus ligger på konfiguration, SQL och backup i dag.

## Bakgrund

Efter två workshops har du en fungerande MySQL-baserad lösning. Nu låser vi ner allt: input-validering, behörigheter och backup.

**Varför är säkerhet viktigt?**
- **SQL Injection** är #1 säkerhetshot mot webbappar (OWASP Top 10)
- **Dataförlust** kan kosta företag miljoner kronor
- **Behörighetsfel** kan leda till dataintrång

Fort Knox Protocol = multilager-försvar mot dessa hot!

## Del 1 – Bobby Tables-skydd (45 min)

### Bakgrund: Little Bobby Tables

Känner du den klassiska [XKCD-serien](https://xkcd.com/327/)?

```
Mom: What's your son's name?
School: Robert'); DROP TABLE Students;--
```

Detta kallas **SQL Injection** - att smuggla in SQL-kod via user input.

### Vad EF Core gör automatiskt

**Bra nyheter:** EF Core skyddar automatiskt mot SQL Injection genom **parametrisering**!

**Osäker SQL (manuell string concatenation):**
```csharp
// ALDRIG GÖRA SÅ HÄR!
var name = userInput;
var sql = $"SELECT * FROM Students WHERE Name = '{name}'";  // Farligt!
```

**Säker EF Core (parametriserad):**
```csharp
// Detta är säkert!
var students = context.Students
    .Where(s => s.Name == userInput)
    .ToList();
```

EF Core genererar SQL med parametrar:
```sql
SELECT * FROM Students WHERE Name = @p0
-- Parameter: @p0 = "Robert'); DROP TABLE Students;--"
```

Input behandlas som **värde**, aldrig som **kod**!

### Din uppgift: Verifiera SQL Injection-skydd

#### Steg 1: Aktivera SQL-loggning

Vi vill SE att EF Core använder parametrar. Uppdatera `OnConfiguring`:

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    options.UseMySql(connectionString, serverVersion)
           .LogTo(Console.WriteLine, LogLevel.Information)
           .EnableSensitiveDataLogging();  // Visar parametervärden
}
```

**Viktigt:** `EnableSensitiveDataLogging()` visar riktiga värden i loggen - använd ENDAST i development!

#### Steg 2: Testa "farlig" input

```csharp
using var db = new SchoolContext();

// Bobby Tables attack!
var dangerousName = "Robert'); DROP TABLE Students;--";
var student = new Student
{
    Name = dangerousName,
    Email = "bobby@school.com"
};

db.Students.Add(student);
db.SaveChanges();

Console.WriteLine("✅ Student skapad - tabellen finns fortfarande!");
```

**Kolla loggen!** Du ska se något liknande:

```
Executed DbCommand (12ms) [Parameters=[@p0='Robert''); DROP TABLE Students;--', @p1='bobby@school.com'], CommandType='Text']
INSERT INTO `Students` (`Name`, `Email`) VALUES (@p0, @p1);
```

Se `@p0` och `@p1`? Det är parametrar - **SQL Injection är omöjlig**!

#### Steg 3: Lägg till input-validering

EF Core stoppar SQL Injection, men vi vill också **avvisa nonsens-data**.

Uppdatera `Student.cs`:

```csharp
using System.ComponentModel.DataAnnotations;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Namn krävs")]
    [MaxLength(100, ErrorMessage = "Max 100 tecken")]
    [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\s\-]+$",
        ErrorMessage = "Endast bokstäver, mellanslag och bindestreck")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Måste vara giltig e-postadress")]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
```

**Förklaring av attribut:**
- `[Required]` - Kan inte vara null/tom
- `[MaxLength(100)]` - Max antal tecken (förhindrar DoS)
- `[RegularExpression]` - Måste matcha mönster
- `[EmailAddress]` - Måste vara valid e-post

#### Steg 4: Testa validering

```csharp
try
{
    var invalidStudent = new Student
    {
        Name = "Bobby123!!",  // Siffror och ! är ej tillåtna
        Email = "not-an-email"
    };

    db.Students.Add(invalidStudent);
    db.SaveChanges();
}
catch (DbUpdateException ex)
{
    Console.WriteLine("❌ Validering misslyckades (som förväntat):");
    Console.WriteLine(ex.InnerException?.Message);
}
```

#### Skapa migration för validering

```bash
dotnet ef migrations add AddValidationRules
dotnet ef database update
```

EF Core uppdaterar nu databasschemat för att matcha validation-reglerna!

### Säkerhetschecklista Del 1

- ✅ SQL Injection-skydd verifierat (parametriserade queries)
- ✅ Input-validering på alla user-facing fields
- ✅ SQL-loggning aktiverad för development
- ✅ Nonsens-data avvisas innan det når databasen

## Del 2 – Skapa read-only användare (45 min)

### Varför?

**Principle of Least Privilege:** Ge bara minsta nödvändiga behörighet.

**Scenario:** Din rapportfunktion ska bara LÄSA data - varför ge den INSERT/UPDATE/DELETE?

Om rapport-systemet blir hackat kan angriparen inte ändra databasen!

### Arkitektur

```
┌─────────────┐                ┌─────────────┐
│  Main App   │ root-user      │   MySQL     │
│  (CRUD)     │───────────────▶│  Database   │
└─────────────┘  full access   └─────────────┘
                                      ▲
┌─────────────┐  reporter-user       │
│  Reports    │───────────────────────┘
│  (ReadOnly) │  SELECT only
└─────────────┘
```

### Steg 1: Skapa read-only MySQL-användare

Logga in i MySQL-containern:

```bash
docker exec -it ef-mysql mysql -u root -pPassw0rd!
```

Skapa användare och ge behörigheter:

```sql
-- Skapa användare
CREATE USER 'reporter'@'%' IDENTIFIED BY 'S3cure!Report';

-- Ge endast SELECT-behörighet på school-databasen
GRANT SELECT ON school.* TO 'reporter'@'%';

-- Applicera ändringar
FLUSH PRIVILEGES;

-- Verifiera behörigheter
SHOW GRANTS FOR 'reporter'@'%';
```

**Förväntad output:**
```
+-------------------------------------------------------------+
| Grants for reporter@%                                       |
+-------------------------------------------------------------+
| GRANT USAGE ON *.* TO `reporter`@`%`                       |
| GRANT SELECT ON `school`.* TO `reporter`@`%`               |
+-------------------------------------------------------------+
```

### Steg 2: Testa begränsningar

Fortfarande i MySQL:

```sql
-- Byt till reporter-användare
exit
```

```bash
docker exec -it ef-mysql mysql -u reporter -pS3cure!Report
```

```sql
-- Detta ska fungera
USE school;
SELECT * FROM Students LIMIT 5;

-- Detta ska INTE fungera
INSERT INTO Students (Name, Email) VALUES ('Hacker', 'evil@hack.com');
```

**Förväntad output:**
```
ERROR 1142 (42000): INSERT command denied to user 'reporter'@'localhost' for table 'students'
```

Perfekt! Reporter kan inte ändra data.

### Steg 3: Uppdatera appsettings med två connections

```json
{
  "ConnectionStrings": {
    "SchoolConnection": "Server=localhost;Database=school;User=root;Password=Passw0rd!",
    "SchoolReadOnly": "Server=localhost;Database=school;User=reporter;Password=S3cure!Report"
  }
}
```

### Steg 4: Skapa ReportContext

Skapa `Data/Context/ReportContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using YourNamespace.Data.Models;

namespace YourNamespace.Data.Context;

public class ReportContext : DbContext
{
    private readonly string _connectionString;

    public ReportContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseMySql(_connectionString,
            new MySqlServerVersion(new Version(8, 0, 36)));
    }
}
```

### Steg 5: Skapa rapport-service

Skapa `Services/StudentReport.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using YourNamespace.Data.Context;

public class StudentReport
{
    public void GenerateEnrollmentReport(string readOnlyConnectionString)
    {
        using var db = new ReportContext(readOnlyConnectionString);

        var stats = db.Students
            .AsNoTracking()  // Läs-only, ingen change tracking!
            .Include(s => s.Courses)
            .GroupBy(s => s.Courses.Count)
            .Select(g => new
            {
                CourseCount = g.Key,
                Students = g.Count()
            })
            .OrderBy(x => x.CourseCount)
            .ToList();

        Console.WriteLine("\n📊 Enrollment Report:");
        Console.WriteLine("===================");
        foreach (var stat in stats)
        {
            Console.WriteLine($"{stat.CourseCount} kurser: {stat.Students} studenter");
        }
    }
}
```

**Varför `AsNoTracking()`?**

Change tracking kostar prestanda. För read-only queries sparar vi minne och snabbar upp.

### Steg 6: Testa reporter INTE kan skriva

```csharp
var readOnlyConnection = configuration.GetConnectionString("SchoolReadOnly");

try
{
    using var db = new ReportContext(readOnlyConnection);
    var student = new Student { Name = "Hacker", Email = "evil@hack.com" };
    db.Students.Add(student);
    db.SaveChanges();  // Kommer krascha!
}
catch (MySql.Data.MySqlClient.MySqlException ex)
{
    Console.WriteLine($"✅ Reporter blockerad från INSERT: {ex.Message}");
}
```

### Säkerhetschecklista Del 2

- ✅ Read-only MySQL-användare skapad
- ✅ Begränsningar testade och verifierade
- ✅ Separat ReportContext med read-only connection
- ✅ `AsNoTracking()` för prestanda på rapporter

## Del 3 – Backup och återställning (45 min)

### Varför är backup kritiskt?

**Horror stories:**
- GitLab.com raderade 300GB produktionsdata 2017 (6 av 5 backups misslyckades!)
- Ransomware kan kryptera hela databasen
- Utvecklarfel kan radera alla rader med `DELETE FROM Students;` (glömde WHERE)

**Backup är din enda försäkring mot dataförlust!**

### Backup-strategi: 3-2-1 regeln

- **3** kopior av data
- **2** olika medier (disk + molnet)
- **1** offsite backup (annan fysisk plats)

### Steg 1: Manuell backup

```bash
# Full backup med struktur och data
docker exec ef-mysql mysqldump -u root -pPassw0rd! school > backup_$(date +%Y%m%d_%H%M%S).sql

# Verifiera backup skapades
ls -lh backup_*.sql
```

**Vad innehåller backup-filen?**

Öppna `backup_XXXXXX.sql` i editor. Du ser:
- `CREATE TABLE`-statements (struktur)
- `INSERT INTO`-statements (data)
- `CREATE INDEX`-statements (index)

### Steg 2: Testa återställning (VARNING: Destruktiv!)

**VIKTIGT:** Detta raderar databasen! Gör bara i dev-miljö!

```bash
# Radera databasen
docker exec -it ef-mysql mysql -u root -pPassw0rd! -e "DROP DATABASE school; CREATE DATABASE school;"

# Verifiera att den är tom
docker exec -it ef-mysql mysql -u root -pPassw0rd! -e "USE school; SHOW TABLES;"
# Output: Empty set

# Återställ från backup
docker exec -i ef-mysql mysql -u root -pPassw0rd! school < backup_XXXXXXXX_XXXXXX.sql

# Verifiera data är tillbaka
docker exec -it ef-mysql mysql -u root -pPassw0rd! -e "SELECT COUNT(*) AS StudentCount FROM school.Students;"
```

**Resultat:** Alla studenter är tillbaka!

### Steg 3: Automatiserad backup-script

Skapa `backup.sh` (Linux/Mac):

```bash
#!/bin/bash
# Automatisk backup-script för MySQL

# Konfiguration
DATE=$(date +%Y%m%d_%H%M%S)
BACKUP_DIR="./backups"
BACKUP_FILE="$BACKUP_DIR/school_backup_$DATE.sql"
CONTAINER_NAME="ef-mysql"
MYSQL_USER="root"
MYSQL_PASSWORD="Passw0rd!"
DATABASE="school"

# Skapa backup-mapp om den inte finns
mkdir -p $BACKUP_DIR

echo "🔄 Skapar backup av $DATABASE..."

# Ta backup
docker exec $CONTAINER_NAME mysqldump -u $MYSQL_USER -p$MYSQL_PASSWORD $DATABASE > $BACKUP_FILE

# Kolla om backup lyckades
if [ $? -eq 0 ]; then
    echo "✅ Backup klar: $BACKUP_FILE"

    # Komprimera för att spara diskutrymme
    gzip $BACKUP_FILE
    echo "📦 Komprimerad: $BACKUP_FILE.gz"

    # Ta bort gamla backups (behåll senaste 7 dagarna)
    find $BACKUP_DIR -name "*.sql.gz" -mtime +7 -delete
    echo "🧹 Städat gamla backups (äldre än 7 dagar)"
else
    echo "❌ Backup misslyckades!"
    exit 1
fi
```

**Gör körbar:**

```bash
chmod +x backup.sh
./backup.sh
```

### Steg 4: PowerShell-version (Windows)

Skapa `backup.ps1`:

```powershell
# Automatisk backup-script för MySQL (PowerShell)

$date = Get-Date -Format "yyyyMMdd_HHmmss"
$backupDir = ".\backups"
$backupFile = "$backupDir\school_backup_$date.sql"

# Skapa backup-mapp
New-Item -ItemType Directory -Force -Path $backupDir | Out-Null

Write-Host "🔄 Skapar backup..." -ForegroundColor Cyan

# Ta backup
docker exec ef-mysql mysqldump -u root -pPassw0rd! school | Out-File -FilePath $backupFile -Encoding UTF8

if ($?) {
    Write-Host "✅ Backup klar: $backupFile" -ForegroundColor Green

    # Komprimera
    Compress-Archive -Path $backupFile -DestinationPath "$backupFile.zip" -Force
    Remove-Item $backupFile
    Write-Host "📦 Komprimerad: $backupFile.zip" -ForegroundColor Green

    # Ta bort gamla backups
    Get-ChildItem -Path $backupDir -Filter *.zip |
        Where-Object { $_.LastWriteTime -lt (Get-Date).AddDays(-7) } |
        Remove-Item
    Write-Host "🧹 Städat gamla backups" -ForegroundColor Green
} else {
    Write-Host "❌ Backup misslyckades!" -ForegroundColor Red
    exit 1
}
```

**Kör:**

```powershell
.\backup.ps1
```

### Steg 5: Schemalagd backup (Cron/Task Scheduler)

**Linux/Mac (crontab):**

```bash
# Öppna crontab
crontab -e

# Lägg till: Backup varje natt kl 02:00
0 2 * * * /path/to/backup.sh >> /path/to/backup.log 2>&1
```

**Windows (Task Scheduler):**

1. Öppna Task Scheduler
2. Create Basic Task
3. Trigger: Daily at 2:00 AM
4. Action: Start a program: `powershell.exe`
5. Arguments: `-File "C:\path\to\backup.ps1"`

### Backup best practices

| Vad | Frekvens | Retention |
|-----|----------|-----------|
| **Daglig backup** | 02:00 | 7 dagar |
| **Veckobackup** | Söndagar | 4 veckor |
| **Månadsbackup** | 1:a varje månad | 12 månader |
| **Pre-migration backup** | Före varje EF migration | Tills migration verifierad |

**Offsite backup:** Synka till:
- AWS S3 / Azure Blob Storage
- Google Drive / Dropbox
- Annan server på annan plats

### Säkerhetschecklista Del 3

- ✅ Manuell backup testad och verifierad
- ✅ Återställning testad (data kom tillbaka!)
- ✅ Automatiserat backup-script fungerar
- ✅ Plan för regelbunden backup (cron/scheduler)
- ✅ Offsite backup-strategi definierad

## Del 4 – Reflektion (20 min)

Skriv kort i din loggbok eller `README.md`:

### 1. Vilka skydd ger EF Core gratis?

**Ditt svar:**
```
- SQL Injection-skydd genom parametrisering
- ...
```

### 2. Vilka säkerhetsdelar måste du själv konfigurera?

**Ditt svar:**
```
- Input-validering (Data Annotations)
- ...
```

### 3. Vad är planen för backup i skarp miljö?

**Ditt svar:**
```
- Dagliga automatiska backups kl 02:00
- ...
```

## Fort Knox Protokoll: Komplett!

### Vad du har åstadkommit:

- ✅ **Layer 1 - SQL Injection:** Verifierat EF Core's parametrisering
- ✅ **Layer 2 - Input Validation:** Data Annotations på alla modeller
- ✅ **Layer 3 - Least Privilege:** Read-only användare för rapporter
- ✅ **Layer 4 - Disaster Recovery:** Backup och återställning testad

### Säkerhetspoäng: 🔐🔐🔐🔐🔐

Din EF Core-lösning är nu säkrad med multilager-försvar! Du kan sova gott om natten.

### Nästa steg i produktion:

1. **Secrets Management:** Använd Azure Key Vault / AWS Secrets Manager istället för appsettings
2. **HTTPS Only:** Kryptera all trafik
3. **Audit Logging:** Logga alla database-ändringar
4. **Monitoring:** Alert vid ovanliga query-patterns
5. **Regular Security Audits:** Granska behörigheter månadsvis

**Grattis - du är nu en EF Core Security Champion!** 🏆🔐

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
