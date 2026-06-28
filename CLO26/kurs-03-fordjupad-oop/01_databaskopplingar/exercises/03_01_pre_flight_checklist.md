---

title: Pre-Flight Checklist: Förbered din EF Core-miljö
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 1
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/exercises/ef/03_01_pre_flight_checklist.md"
description: "- `Microsoft.EntityFrameworkCore`"
tags: ["checklist", "checklist:", "core-miljö", "csharp", "databaser", "exercise", "flight", "förbered", "installation", "pre"]
week_fit: []
---

# Pre-Flight Checklist: Förbered din EF Core-miljö

🟢


## NuGet-paket att installera först

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.Tools`

> Tips: Kör `dotnet restore` efter varje paket för att dubbelkolla att allt löser sig.

## Database Provider (välj en!)

### SQLite (Rekommenderas för veckan)

**Fördelar:** Noll konfiguration, fungerar direkt, perfekt för lärande och utveckling.

- `Microsoft.EntityFrameworkCore.Sqlite`

### LocalDB (Windows SQL Server)

**Fördelar:** Riktig SQL Server-upplevelse, bra om du vill testa T-SQL features.

LocalDB är en lättviktsversion av SQL Server. Ladda ner från [Microsofts webbplats](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) om du inte har det.

- `Microsoft.EntityFrameworkCore.SqlServer`

> **Vårt val denna vecka:** SQLite - vi vill fokusera på EF Core, inte databaskonfiguration!

## Bakgrund

Innan onsdagens workshop vill vi ha en stabil grund: utvecklingsmiljö, Docker och en färdig modellstruktur. Dagens mål är att du ska kunna trycka på "play" och låta EF Core göra jobbet åt dig resten av veckan.

## Del 1 – Installera och verifiera Docker (45 min)

Docker behöver vi för torsdagens MySQL-workshop. Låt oss få det klart nu så vi kan fokusera på kod senare.

### Steg 1: Installera Docker Desktop

1. Ladda ner från [Docker Hub](https://www.docker.com/products/docker-desktop)
2. Följ installationsguiden för ditt OS
3. Starta om datorn om installationen kräver det
4. **Du behöver inte skapa konto** - skippa det steget!

### Steg 2: Aktivera WSL 2 (endast Windows)

Docker på Windows kräver WSL 2 (Windows Subsystem for Linux).

1. Öppna PowerShell **som administratör**
2. Kör: `wsl --set-default-version 2`
3. Om WSL inte är installerat, följ guiden i Docker-installationen
4. Starta om datorn efter WSL-installation

### Steg 3: Verifiera Docker fungerar

Öppna terminal och testa:

```bash
docker version
```

**Förväntat resultat:** Du ska se både "Client" och "Server" information. Om du bara ser Client betyder det att Docker daemon inte kör - starta Docker Desktop!

### Steg 4: Städa upp gamla containers (valfritt)

Om du har använt Docker tidigare:

```bash
docker container prune
```

Detta tar bort gamla stoppade containers. Bra att börja rent!

## Del 2 – Projektet från vecka 1 (30 min)

### Steg 1: Öppna ditt projekt

Ta fram projektet du byggde under vecka 1 - konsol- eller webbapp spelar ingen roll. EF Core fungerar i båda!

### Steg 2: Verifiera det kompilerar

```bash
dotnet clean
dotnet build
```

Båda ska köra utan fel. Om det är något problem, fixa det innan vi går vidare!

### Steg 3: Organisera mappstruktur

Skapa mappar för att hålla EF Core-koden organiserad:

```bash
mkdir -p Data/Models
mkdir -p Data/Context
```

**Varför?** När projektet växer vill du inte ha 50 filer i rotkatalogen. Bra struktur från dag 1 betalar sig!

## Del 3 – Skapa modellklasser (60 min)

Nu bygger vi entiteter (entities) - C#-klasser som blir databastabeller.

### Vad är en entitet?

En **entitet** är en klass som representerar en rad i databasen. Varje property blir en kolumn.

```csharp
// Detta blir en tabell med tre kolumner
public class Student
{
    public int Id { get; set; }        // Kolumn: Id (primärnyckel)
    public string Name { get; set; }   // Kolumn: Name
    public string Email { get; set; }  // Kolumn: Email
}
```

### Steg 1: Skapa Student-modellen

Skapa `Data/Models/Student.cs`:

```csharp
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Data.Models;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Namn krävs")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    // Navigation property - relationer till kurser
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
```

**Vad betyder attributen?**

- `[Required]` - Kan inte vara null eller tom
- `[MaxLength(100)]` - Max 100 tecken (blir VARCHAR(100) i databasen)
- `[EmailAddress]` - Måste vara giltig e-postadress

**Varför `= string.Empty`?** C# 11+ kräver att non-nullable strings initieras. Annars får du compiler warnings.

**Varför `ICollection<T>`?** Detta är en **navigation property** - visar relationer till andra tabeller. Vi kommer till det snart!

### Steg 2: Skapa Course-modellen

Skapa `Data/Models/Course.cs`:

```csharp
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Data.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 10, ErrorMessage = "Högskolepoäng måste vara 1-10")]
    public int Credits { get; set; }

    // Navigation property - relationer till studenter
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
```

### Vad är relationen här?

Student har `ICollection<Course>` och Course har `ICollection<Student>` - detta är en **många-till-många relation**:

- En student kan läsa flera kurser
- En kurs kan ha flera studenter

EF Core skapar automatiskt en kopplingstabellell (junction table) åt oss!

### Varför inte bara `List<T>`?

`ICollection<T>` är ett interface som ger flexibilitet. EF Core kan använda olika collection-typer internt för optimering. Du kan fortfarande använda det som en lista:

```csharp
student.Courses.Add(course);  // Fungerar!
var count = student.Courses.Count;  // Fungerar!
```

## Del 4 – Sätt upp DbContext (60 min)

### Vad är DbContext?

`DbContext` är din **gateway till databasen**. Den:

- Representerar en databassession
- Håller koll på ändringar (change tracking)
- Skapar SQL-queries från dina LINQ-uttryck
- Hanterar connections och transaktioner

### Steg 1: Skapa SchoolContext

Skapa `Data/Context/SchoolContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using YourNamespace.Data.Models;

namespace YourNamespace.Data.Context;

public class SchoolContext : DbContext
{
    // DbSets = tabeller i databasen
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();

    // Konfigurera vilken databas vi ska använda
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=school.db");
    }

    // Konfigurera relationer och regler
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfigurera många-till-många explicit
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Courses)
            .WithMany(c => c.Students)
            .UsingEntity(j => j.ToTable("StudentCourses"));
    }
}
```

### Förklaring: DbSet<T>

```csharp
public DbSet<Student> Students => Set<Student>();
```

Detta skapar en "tabell" du kan köra queries mot:

```csharp
var students = context.Students.ToList();  // SELECT * FROM Students
```

**Modern syntax:** `=> Set<Student>()` är kortare än att ha en private field och property. Båda fungerar!

### Förklaring: OnConfiguring

Här väljer vi vilken databas som ska användas:

```csharp
options.UseSqlite("Data Source=school.db");
```

Detta skapar en SQLite-fil `school.db` i projektmappen. Enkel och portabel!

**Alternativ för andra databaser:**

```csharp
// LocalDB (SQL Server)
options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=School");

// MySQL (kommer på torsdag!)
options.UseMySql(connectionString, serverVersion);
```

### Förklaring: OnModelCreating

Här konfigurerar vi hur EF Core ska hantera relationer:

```csharp
modelBuilder.Entity<Student>()
    .HasMany(s => s.Courses)      // Student har många Courses
    .WithMany(c => c.Students)    // Course har många Students
    .UsingEntity(j => j.ToTable("StudentCourses"));  // Kopplingstabellens namn
```

**Utan denna kod:** EF Core hade skapat kopplingstabellen automatiskt, men med genererat namn typ `CourseStudent` eller `StudentCourse`. Nu bestämmer vi namnet själva!

### Testa att det kompilerar

```bash
dotnet build
```

Ska kompilera utan fel. Om du får fel, kolla att:
- Using-statements är korrekta
- Namespace matchar din projektstruktur
- NuGet-paketen är installerade

## Del 5 – EF Core Tools och första migrationen (45 min)

### Vad är migrationer?

**Migrationer** är kod som beskriver databasändringar:

- "Skapa tabell Students"
- "Lägg till kolumn Email"
- "Radera tabell OldStuff"

EF Core genererar migrations-kod automatiskt från dina modeller!

### Steg 1: Installera EF Core CLI Tools

Kolla om det redan är installerat:

```bash
dotnet ef --version
```

Om du får fel, installera globalt:

```bash
dotnet tool install --global dotnet-ef
```

### Steg 2: Skapa första migrationen

```bash
dotnet ef migrations add InitialCreate
```

**Vad händer?**

EF Core:
1. Läser dina modeller (Student, Course)
2. Läser din DbContext-konfiguration
3. Genererar C#-kod i `Migrations/`-mappen
4. Kod som skapar tabeller när du kör nästa kommando

**Kolla migrations-filen!** Öppna `Migrations/XXXXXXX_InitialCreate.cs` och se hur EF Core planerar att skapa tabellerna.

### Steg 3: Kör migrationen

```bash
dotnet ef database update
```

**Vad händer?**

1. EF Core kopplar till databasen (eller skapar den om den inte finns)
2. Kör SQL från migrationen
3. Skapar tabeller: `Students`, `Courses`, `StudentCourses`
4. Skapar en `__EFMigrationsHistory`-tabell för att hålla reda på vilka migrationer som körts

### Steg 4: Verifiera databasen skapades

Du ska nu ha en `school.db`-fil i projektmappen!

Öppna den med SQLite-verktyg eller använd VS Code-extension "SQLite" för att inspektera tabellerna.

## Vanliga problem och lösningar

### Problem 1: "No database provider has been configured"

**Lösning:** Du glömde anropa `UseSqlite()` (eller annan provider) i `OnConfiguring`.

### Problem 2: "Build failed"

**Lösning:** Migrationer kräver att projektet kompilerar. Fixa alla kompileringsfel först!

### Problem 3: Migration har redan körts men jag ändrade modellerna

**Lösning:** Skapa en NY migration för ändringarna:

```bash
dotnet ef migrations add AddNewField
dotnet ef database update
```

### Problem 4: Vill börja om från scratch

**Lösning:**

```bash
# Ta bort databasen
rm school.db

# Ta bort alla migrationer
rm -rf Migrations/

# Börja om
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Checklist: Är du redo?

- ✅ Docker Desktop är installerat och kör
- ✅ Projektet från vecka 1 kompilerar
- ✅ Student och Course-modeller är skapade med validation
- ✅ SchoolContext är konfigurerad med DbSets
- ✅ Första migrationen är skapad och körd
- ✅ `school.db` finns i projektmappen

**Om alla är checkade:** Du är redo för onsdagens Code First-workshop! 🚀

**Om något saknas:** Gå tillbaka till den delen och dubbelkolla stegen.

## Sneak peek: Vad händer nästa gång?

I morgon ska vi:
- Skriva LINQ-queries för att hämta data
- Skapa en service-klass för CRUD-operationer
- Seeda databasen med testdata
- Se hur Include() laddar relationer

Ta en fika – du har byggt grunden! ☕

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
