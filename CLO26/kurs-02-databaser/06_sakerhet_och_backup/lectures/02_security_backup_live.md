---

title: Fort Knox Protocol - Live Coding Guide
author: Marcus Ackre Medina
type: lecture
topic: databaser
difficulty: 1
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/lectures/ef_core/part2_mysql_docker/lecture/02_security_backup_live.md"
description: "Förutsättningar:** Fungerande Heroes-app på MySQL från igår"
tags: ["backup", "bash", "coding", "databaser", "fort", "git", "installation", "knox", "live", "protocol"]
week_fit: []
---

# Fort Knox Protocol - Live Coding Guide

🟢


---

## Fredag Workshop - Del 2 av 2

**Tidsram:** 90 minuter
**Förutsättningar:** Fungerande Heroes-app på MySQL från igår
**Mål:** Säkra databasen och implementera backup-strategi

---

## Förberedelser (Innan lektionen)

**SÄG:** "Igår fick vi MySQL att funka. Idag ska vi låsa ner den som Fort Knox!"

**Kolla att du har:**
- MySQL container körande från igår: `docker ps`
- Heroes_ef_live projektet fungerande
- Terminal öppen

**SÄG:** "Tre delar idag: Bobby Tables (SQL injection), användarbehörigheter, och backup!"

---

## Del 1: Bobby Tables - The Legend (15 minuter)

**SÄG:** "Innan vi börjar koda, låt mig berätta om världens mest kända databasbarn..."

**ÖPPNA:** https://xkcd.com/327/ (visa bilden)

**SÄG:** "Mamman: 'We named him Robert'); DROP TABLE Students;--'"
**SÄG:** "Skolan: 'Oh god, jag hoppas ni inte använde hans namn i någon database...'"
**SÄG:** "Resultat: Hela student-tabellen borta!"

**FÖRKLARA:** "Detta kallas SQL Injection - den vanligaste webbsårbarheten!"

---

### Hur Fungerar SQL Injection?

**SÄG:** "Låt mig visa hur det funkar med RAW SQL. Vi gör INTE detta i vår app, men ni måste förstå faran!"

**SKAPA:** En ny fil: `SqlInjectionDemo.cs`

```csharp
using MySqlConnector;

Console.Write("Ange hjältenamn: ");
var heroName = Console.ReadLine();

var connectionString = "Server=localhost;Database=heroesdb;User=root;Password=SuperSecret123;";

using var connection = new MySqlConnection(connectionString);
connection.Open();

// FARLIG KOD - Gör ALDRIG detta!
var sql = $"SELECT * FROM Heroes WHERE HeroName = '{heroName}'";
Console.WriteLine($"SQL: {sql}");

var command = new MySqlCommand(sql, connection);
var reader = command.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine($"{reader["HeroName"]} - {reader["RealName"]}");
}
```

**SÄG:** "Vanlig användare skriver: 'Iron Man'"

---

### Demo 1: Normal Användning

**KÖR:** Programmet

```
Ange hjältenamn: Iron Man
SQL: SELECT * FROM Heroes WHERE HeroName = 'Iron Man'
Iron Man - Tony Stark
```

**SÄG:** "Fungerar perfekt! Men vad händer om jag är elak?"

---

### Demo 2: SQL Injection Attack

**KÖR:** Programmet igen

```
Ange hjältenamn: Thanos' OR '1'='1
SQL: SELECT * FROM Heroes WHERE HeroName = 'Thanos' OR '1'='1'
Iron Man - Tony Stark
Spider-Man - Peter Parker
Thor - Thor Odinson
... (alla hjältar!)
```

**SÄG:** "BOOM! Hackaren får ALLA hjältar utan att känna till några namn!"

**FÖRKLARA:**
- `Thanos' OR '1'='1` stänger strängen
- `OR '1'='1'` är alltid sant
- Alla rader returneras!

---

### Demo 3: Bobby Tables Attack

**SÄG:** "Men det blir värre..."

```
Ange hjältenamn: Bobby'); DROP TABLE Heroes;--
SQL: SELECT * FROM Heroes WHERE HeroName = 'Bobby'); DROP TABLE Heroes;--'
```

**SÄG:** "Vad händer här?"
1. `Bobby'` stänger strängen
2. `);` avslutar SELECT-satsen
3. `DROP TABLE Heroes;` tar bort tabellen!
4. `--` kommenterar bort resten

**VARNA:** "KOLLA INTE! Detta raderar verkligen tabellen!"

---

### Hur EF Core Skyddar

**SÄG:** "Nu kör vi samma attack mot vår Heroes-app!"

**ÖPPNA:** Program.cs

**PEKA på:** Lista hjältar-funktionen

```csharp
static void ListHeroes(HeroContext db)
{
    var heroes = db.Heroes
        .Include(h => h.HeroAddresses)
            .ThenInclude(ha => ha.Address)
        .ToList();
    // ...
}
```

**SÄG:** "Ingen string concatenation! EF använder LINQ!"

---

### Live Test: EF vs Injection

**SÄG:** "Låt mig bevisa att EF skyddar oss!"

**MODIFIERA:** AddHero-funktionen tillfälligt

```csharp
static void AddHero(HeroContext db)
{
    Console.Write("\nCivilt namn: ");
    var realName = Console.ReadLine() ?? "";
    Console.Write("Hjältenamn: ");
    var heroName = Console.ReadLine() ?? "";
    Console.Write("Telefon: ");
    var phone = Console.ReadLine() ?? "";

    // Testa med injection-försök
    var hero = new Hero
    {
        RealName = realName,
        HeroName = heroName,
        Phone = phone
    };

    db.Heroes.Add(hero);
    db.SaveChanges();

    Console.WriteLine($"✓ {heroName} tillagd med ID: {hero.Id}");
}
```

**KÖR:** dotnet run

```
Hjältenamn: Bobby'); DROP TABLE Heroes;--
✓ Bobby'); DROP TABLE Heroes;-- tillagd med ID: 5
```

---

### Kolla Databasen

**SÄG:** "Finns Heroes-tabellen kvar?"

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb
```

```sql
SHOW TABLES;
```

**SÄG:** "Alla tabeller kvar! EF skapade en hero med namnet 'Bobby'); DROP TABLE Heroes;--' istället!"

```sql
SELECT * FROM Heroes WHERE HeroName LIKE '%Bobby%';
```

**SÄG:** "Se! Injection-strängen blev data, inte kod! Detta kallas 'parametriserade queries'!"

```sql
EXIT;
```

---

### När EF INTE Skyddar

**SÄG:** "EF skyddar bara när vi använder LINQ. Om vi använder FromSqlRaw fel..."

```csharp
var heroName = Console.ReadLine();

// FARLIGT!
var heroes = db.Heroes
    .FromSqlRaw($"SELECT * FROM Heroes WHERE HeroName = '{heroName}'")
    .ToList();

// SÄKERT!
var heroes = db.Heroes
    .FromSqlInterpolated($"SELECT * FROM Heroes WHERE HeroName = {heroName}")
    .ToList();

// BÄST!
var heroes = db.Heroes
    .Where(h => h.HeroName == heroName)
    .ToList();
```

**SÄG:** "Regel: Använd LINQ! Undvik raw SQL!"

---

## Del 2: Användarbehörigheter (25 minuter)

**SÄG:** "Nu till problem nummer två: Vi använder root-användaren!"

**FÖRKLARA:** "Root kan göra ALLT:"
- Skapa/radera databaser
- Skapa/radera användare
- Ändra behörigheter
- Stoppa servern

**SÄG:** "Om hackare kommer in via vår app med root-credentials = game over!"

---

### Principen om Minsta Behörighet

**SÄG:** "Least Privilege Principle: Ge minsta möjliga behörigheter som krävs!"

**RITA** (på whiteboard eller i kommentar):

```
┌─────────────┐
│    ROOT     │  Allt!
├─────────────┤
│   APP USER  │  SELECT, INSERT, UPDATE, DELETE
├─────────────┤
│ READ-ONLY   │  SELECT
└─────────────┘
```

**SÄG:** "Vi ska skapa båda: read-only OCH app-user!"

---

### Skapa Read-Only User

**SÄG:** "Börjar med read-only. Perfekt för rapporter och analytics!"

**LOGGA IN:**

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123
```

**SKAPA ANVÄNDARE:**

```sql
CREATE USER 'readonly'@'%' IDENTIFIED BY 'ReadOnly123';
```

**FÖRKLARA:**
- `'readonly'` = användarnamn
- `'%'` = kan koppla från vilken host som helst (localhost, IP, etc)
- `IDENTIFIED BY 'ReadOnly123'` = lösenordet

**GE BEHÖRIGHETER:**

```sql
GRANT SELECT ON heroesdb.* TO 'readonly'@'%';
```

**FÖRKLARA:**
- `GRANT SELECT` = endast läsrättigheter
- `ON heroesdb.*` = alla tabeller i heroesdb
- `TO 'readonly'@'%'` = till användaren readonly

---

### Aktivera Behörigheter

```sql
FLUSH PRIVILEGES;
```

**SÄG:** "Flush laddar om behörighetstabellerna!"

**KOLLA ANVÄNDARE:**

```sql
SELECT User, Host FROM mysql.user;
```

**SÄG:** "Se! readonly finns!"

```sql
EXIT;
```

---

### Testa Read-Only User

**LOGGA IN SOM READONLY:**

```bash
docker exec -it mysql-heroes mysql -ureadonly -pReadOnly123 heroesdb
```

**TESTA SELECT:**

```sql
SELECT * FROM Heroes;
```

**SÄG:** "Fungerar! Vi kan läsa!"

**TESTA INSERT:**

```sql
INSERT INTO Heroes (RealName, HeroName, Phone)
VALUES ('Wade Wilson', 'Deadpool', '555-CHIMICHANGAS');
```

**SÄG:** "ERROR 1142: INSERT command denied to user 'readonly'"

**SÄG:** "Perfekt! Readonly kan bara läsa, inte skriva!"

---

### Testa Andra Operationer

```sql
UPDATE Heroes SET Phone = '555-HACK' WHERE Id = 1;
-- ERROR 1142: UPDATE command denied
```

```sql
DELETE FROM Heroes WHERE Id = 1;
-- ERROR 1142: DELETE command denied
```

```sql
DROP TABLE Heroes;
-- ERROR 1142: DROP command denied
```

**SÄG:** "Alla skrivoperationer blockerade! Exakt vad vi vill!"

```sql
EXIT;
```

---

### Skapa App User

**SÄG:** "Nu skapar vi app-user som kan göra CRUD, men inte ändra schema!"

**LOGGA IN SOM ROOT:**

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123
```

**SKAPA APP USER:**

```sql
CREATE USER 'heroapp'@'%' IDENTIFIED BY 'HeroApp456';
```

**GE CRUD-BEHÖRIGHETER:**

```sql
GRANT SELECT, INSERT, UPDATE, DELETE ON heroesdb.* TO 'heroapp'@'%';
```

**FÖRKLARA:** "SELECT, INSERT, UPDATE, DELETE - allt vi behöver för CRUD!"

**SÄG:** "Vad får heroapp INTE göra?"
- ❌ CREATE/DROP DATABASE
- ❌ CREATE/DROP TABLE
- ❌ ALTER TABLE
- ❌ Skapa användare

```sql
FLUSH PRIVILEGES;
EXIT;
```

---

### Uppdatera HeroContext.cs

**SÄG:** "Nu byter vi från root till heroapp i vår app!"

**ÖPPNA:** HeroContext.cs

**FÖRE:**

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var connectionString = "Server=localhost;Database=heroesdb;" +
                          "User=root;Password=SuperSecret123;";

    optionsBuilder.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString));
}
```

**EFTER:**

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var connectionString = "Server=localhost;Database=heroesdb;" +
                          "User=heroapp;Password=HeroApp456;";

    optionsBuilder.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString));
}
```

---

### Testa Appen med Heroapp

**KÖR:**

```bash
dotnet run
```

**TESTA:**
1. Lägg till hjälte → ✓ Fungerar!
2. Lista hjältar → ✓ Fungerar!
3. Uppdatera data → ✓ Fungerar!
4. Ta bort hjälte → ✓ Fungerar!

**SÄG:** "Perfekt! All CRUD-funktionalitet fungerar!"

**AVSLUTA APPEN**

---

### Testa Migration med Heroapp

**SÄG:** "Men vad händer om vi kör migrations?"

```bash
dotnet ef migrations add TestSecurity
```

**SÄG:** "Genererar migration - fungerar! Bara kod!"

```bash
dotnet ef database update
```

**SÄG:** "ERROR! heroapp kan inte ALTER TABLE!"

**FÖRKLARA:** "Detta är BRA! Migrations ska köras separat med root-credentials!"

**TA BORT TEST-MIGRATION:**

```bash
rm -rf Migrations/
```

---

### Migrations-strategi

**SÄG:** "I produktion:"

1. **Development:** Använd root lokalt för migrations
2. **Production:**
   - Kör migrations via CI/CD med root
   - App använder heroapp-användare

**VISA LÖSNING:**

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    #if DEBUG
        // Development - kan köra migrations
        var connectionString = "Server=localhost;Database=heroesdb;" +
                              "User=root;Password=SuperSecret123;";
    #else
        // Production - endast CRUD
        var connectionString = "Server=localhost;Database=heroesdb;" +
                              "User=heroapp;Password=HeroApp456;";
    #endif

    optionsBuilder.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString));
}
```

**SÄG:** "Eller använd environment variables - mer om det senare!"

---

## Del 3: Backup & Restore (25 minuter)

**SÄG:** "Nu till livsviktigt: Backup! Har ni någonsin råkat radera något viktigt?"

**SCENARION:**
- 💥 Hårdvarukrasch
- 🐛 Bug som raderar data
- 🧑‍💻 Hackare med ransomware
- 👤 "Jag testade DROP TABLE i produktion..."

**SÄG:** "3-2-1 regeln:"
- **3** kopior av data
- **2** olika media
- **1** offsite (geografiskt separerad)

---

### mysqldump - MySQL's Backup Tool

**SÄG:** "MySQL har inbyggt backup-verktyg: mysqldump!"

**SYNTAX:**

```bash
mysqldump -u[user] -p[password] [database] > backup.sql
```

**MED DOCKER:**

```bash
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  > heroes_backup.sql
```

**SÄG:** "Skapar SQL-fil med alla CREATE och INSERT statements!"

---

### Skapa Backup

**KÖR:**

```bash
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  > heroes_backup_$(date +%Y%m%d_%H%M%S).sql
```

**SÄG:** "Timestamp i filnamnet - bra för versionshantering!"

**KOLLA FILEN:**

```bash
ls -lh heroes_backup_*.sql
```

**VISA INNEHÅLL:**

```bash
head -n 50 heroes_backup_*.sql
```

**FÖRKLARA vad du ser:**

```sql
-- MySQL dump 10.13  Distrib 8.0.40, for Linux (x86_64)
--
-- Host: localhost    Database: heroesdb
-- ------------------------------------------------------

DROP TABLE IF EXISTS `Heroes`;
CREATE TABLE `Heroes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RealName` longtext NOT NULL,
  `HeroName` longtext NOT NULL,
  `Phone` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

LOCK TABLES `Heroes` WRITE;
INSERT INTO `Heroes` VALUES
  (1,'Tony Stark','Iron Man','555-STARK'),
  (2,'Peter Parker','Spider-Man','555-WEB');
UNLOCK TABLES;
```

**SÄG:** "Komplett återställningsbar snapshot!"

---

### Disaster Simulation

**SÄG:** "Nu simulerar vi en katastrof och återställning! Spännande!"

**STEG 1: Kolla nuvarande data**

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT COUNT(*) as 'Antal hjältar' FROM Heroes;"
```

**ANTECKNA:** "Antal hjältar: X"

**STEG 2: Skapa backup**

```bash
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  > before_disaster.sql
```

**SÄG:** "✓ Backup klar! Nu har vi fallskärm!"

---

### KATASTROF!

**SÄG:** "Nu händer det! Simulerar DELETE utan WHERE!"

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "DELETE FROM Heroes;"
```

**SÄG:** "💥 BOOM! Alla hjältar borta!"

**VERIFIERA:**

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT COUNT(*) FROM Heroes;"
```

**SÄG:** "COUNT(*) = 0. Tomt. Ingen Iron Man. Ingen Spider-Man. Panik! 😱"

---

### ÅTERSTÄLLNING!

**SÄG:** "Men vi har backup! Lugn!"

```bash
docker exec -i mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  < before_disaster.sql
```

**SÄG:** "Vänta... Vänta... 🤞"

**KOLLA:**

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT COUNT(*) FROM Heroes;"
```

**SÄG:** "YESS! Alla hjältar tillbaka! 🎉"

**VISA DETALJER:**

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT * FROM Heroes;"
```

**SÄG:** "Iron Man, Spider-Man - alla här! Backup räddade oss!"

---

### Automatiska Backups

**SÄG:** "I produktion kör vi backups automatiskt!"

**LINUX/MAC - CRONTAB:**

```bash
# Redigera crontab
crontab -e

# Lägg till (varje dag kl 02:00)
0 2 * * * docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  > /backups/heroes_$(date +\%Y\%m\%d).sql
```

**WINDOWS - TASK SCHEDULER:**

Skapa `backup.bat`:

```batch

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
@echo off
set BACKUP_DIR=C:\backups
set TIMESTAMP=%date:~0,4%%date:~5,2%%date:~8,2%

docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb ^
  > %BACKUP_DIR%\heroes_%TIMESTAMP%.sql
```

**SCHEMALÄGG:** Task Scheduler → kör backup.bat varje natt

---

### Retention Policy

**SÄG:** "Vi kan inte spara backups för evigt! Retention policy!"

**EXEMPEL:**

```bash
#!/bin/bash
# Behåll dagliga backups 7 dagar
find /backups -name "heroes_*.sql" -mtime +7 -delete

# Behåll veckovisa (söndag) 4 veckor
# Behåll månatliga (första) 12 månader
```

**SÄG:** "Balans mellan diskutrymme och datasäkerhet!"

---

## Del 4: Secrets Management (15 minuter)

**SÄG:** "Sista problemet: Vi har hårdkodat lösenord i koden!"

**ÖPPNA:** HeroContext.cs

**PEKA:**

```csharp
var connectionString = "Server=localhost;Database=heroesdb;" +
                      "User=heroapp;Password=HeroApp456;";
```

**SÄG:** "Vad händer när vi committar till Git?"
- Lösenordet synligt för alla med repo-access
- GitHub scanner efter secrets
- Hackare söker på 'Password=' i public repos
- Kan inte ha olika lösenord för dev/prod

---

### Lösning 1: Environment Variables

**SÄG:** "Enklaste lösningen: Environment variables!"

**UPPDATERA:** HeroContext.cs

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var connectionString = Environment.GetEnvironmentVariable("HEROES_DB_CONNECTION")
        ?? throw new InvalidOperationException("HEROES_DB_CONNECTION saknas!");

    optionsBuilder.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString));
}
```

**SÄTT ENVIRONMENT VARIABLE:**

**Linux/Mac:**

```bash
export HEROES_DB_CONNECTION="Server=localhost;Database=heroesdb;User=heroapp;Password=HeroApp456;"
```

**Windows PowerShell:**

```powershell
$env:HEROES_DB_CONNECTION="Server=localhost;Database=heroesdb;User=heroapp;Password=HeroApp456;"
```

---

### Testa med Environment Variable

**KÖR:**

```bash
dotnet run
```

**SÄG:** "Fungerar! Och ingen connection string i koden!"

**VISA .gitignore:**

```bash
cat .gitignore
```

**SÄG:** "Ingen .env fil borde committas!"

**SKAPA:** .env (för dev)

```
HEROES_DB_CONNECTION=Server=localhost;Database=heroesdb;User=heroapp;Password=HeroApp456;
```

**LÄGG TILL:** .gitignore

```
.env
```

---

### Lösning 2: .NET User Secrets

**SÄG:** "För development finns User Secrets!"

**INITIERA:**

```bash
dotnet user-secrets init
```

**SÄG:** "Lägger till UserSecretsId i .csproj!"

**LÄGG TILL SECRET:**

```bash
dotnet user-secrets set "ConnectionStrings:HeroesDb" \
  "Server=localhost;Database=heroesdb;User=heroapp;Password=HeroApp456;"
```

**KOLLA SECRETS:**

```bash
dotnet user-secrets list
```

**SÄG:** "Lagras i: %APPDATA%\Microsoft\UserSecrets\ - INTE i repo!"

---

### Använd User Secrets i Kod

**INSTALLERA PAKET:**

```bash
dotnet add package Microsoft.Extensions.Configuration.UserSecrets
```

**UPPDATERA:** HeroContext.cs

```csharp
using Microsoft.Extensions.Configuration;

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var configuration = new ConfigurationBuilder()
        .AddUserSecrets<HeroContext>()
        .Build();

    var connectionString = configuration["ConnectionStrings:HeroesDb"]
        ?? throw new InvalidOperationException("Connection string saknas!");

    optionsBuilder.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString));
}
```

**TESTA:**

```bash
dotnet run
```

**SÄG:** "Fungerar! Och connection string är hemlig! ✅"

---

### Production Secrets

**SÄG:** "I produktion använder vi:"

**Azure:**
- Azure Key Vault
- App Service Configuration

**AWS:**
- AWS Secrets Manager
- Parameter Store

**Kubernetes:**
- Sealed Secrets
- External Secrets Operator

**Docker:**

```yaml
# docker-compose.yml
services:
  app:
    environment:
      - HEROES_DB_CONNECTION=${DB_CONNECTION}
    secrets:
      - db_password

secrets:
  db_password:
    file: ./secrets/db_password.txt
```

---

## Sammanfattning (5 minuter)

**SÄG:** "Vad har vi lärt oss idag?"

**GÅ IGENOM FORT KNOX CHECKLIST:**

✅ **SQL Injection:**
- EF Core parametriserar automatiskt
- Använd LINQ, inte FromSqlRaw
- Bobby Tables kan inte radera våra tabeller!

✅ **Användarbehörigheter:**
- Root endast för setup/migrations
- heroapp för CRUD operations
- readonly för rapporter
- Minsta behörighet = maximal säkerhet

✅ **Backup & Restore:**
- mysqldump skapar snapshots
- Testa återställning regelbundet
- Automatiska nattliga backups
- 3-2-1 regel

✅ **Secrets:**
- Aldrig hårdkoda lösenord
- Environment variables (enklast)
- User Secrets (development)
- Key Vault (production)

---

## Workshop-tid! 🛠️

**SÄG:** "Nu är det er tur! 5 övningar:"

1. **Docker MySQL Setup** - Från scratch till körande container
2. **Migrera Heroes till MySQL** - Step-by-step guide
3. **Bobby Tables** - Testa SQL injection säkert
4. **Read-Only User** - Skapa och testa begränsad user
5. **Backup & Restore** - Full disaster recovery

**SÄG:** "Alla övningar finns i exercises-mappen!"

**SÄG:** "Hjälp varandra! Fråga om ni kör fast!"

**SÄG:** "När ni är klara - ni har en produktionsklar, säker MySQL-setup! 🔐"

---

## Lärarnotes

**Timing:**
- Del 1 (Bobby Tables): 15 min
- Del 2 (Users): 25 min
- Del 3 (Backup): 25 min
- Del 4 (Secrets): 15 min
- Sammanfattning: 5 min
- Workshop: 60+ min

**Common Issues:**
- Glömmer % i CREATE USER → kan inte koppla från app
- Glömmer FLUSH PRIVILEGES → behörigheter uppdateras inte
- Använder -i flaggan fel vid restore → inget händer
- mysqldump utan -i vid restore → utskrift till terminal

**Tips:**
- Ha xkcd Bobby Tables-bilden synlig hela lektionen
- Gör disaster recovery demo LÅNGSAMT - det är coolt!
- Uppmuntra elever att testa DROP TABLE i sin sandbox
- Påminn: Använd ALDRIG dessa lösenord i produktion
- Visa docker logs mysql-heroes om något krånglar

**VIKTIGT:**
- Återskapa migrations efter användarbytet (från root → heroapp)
- Ha backup av fungerande databas innan katastrofsimulation
- Testa alla kommandon innan lektionen!

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
