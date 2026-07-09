# Hello Entity Framework Core

🟢


En super-enkel första övning för att komma igång med EF Core. Perfekt för absoluta nybörjare!

## Mål

- Installera EF Core
- Skapa din första entitet
- Skapa en DbContext
- Generera databas med migrations
- Lägga till och hämta data

## Steg 1 – Skapa projekt (2 min)

```bash
dotnet new console -n HelloEF
cd HelloEF
```

## Steg 2 – Installera EF Core (3 min)

```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

💡 Vi använder SQLite för enkelhetens skull – ingen server behövs!

## Steg 3 – Skapa din första entitet (5 min)

Skapa `Person.cs`:

```csharp
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}
```

**Förklaring:**

- `Id` – EF Core känner igen detta som primärnyckel automatiskt
- `string.Empty` – standardvärde för att undvika null warnings
- `int Age` – vanlig property som blir en kolumn i databasen

## Steg 4 – Skapa DbContext (5 min)

Skapa `HelloContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;

public class HelloContext : DbContext
{
    // Tabellen med personer
    public DbSet<Person> People => Set<Person>();

    // Anslutning till SQLite-databas
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=hello.db");
    }
}
```

**Förklaring:**

- `DbSet<Person>` – representerar tabellen i databasen
- `People` – tabellnamnet (pluralis av Person)
- `UseSqlite` – säger åt EF att använda SQLite
- `hello.db` – databasen sparas i en fil

## Steg 5 – Skapa databasen (3 min)

Kör i terminalen:

```bash
dotnet ef migrations add Initial
dotnet ef database update
```

✅ Nu har du en databas! Kolla i projektmappen – där finns `hello.db`

**Vad hände?**

1. `migrations add` – skapade en migration (instruktion för databasändring)
2. `database update` – körde migrationen och skapade databasen

## Steg 6 – Lägg till data (10 min)

Uppdatera `Program.cs`:

```csharp
using HelloEF;

// Skapa databas-koppling
using var db = new HelloContext();

// Skapa en person
var person = new Person
{
    Name = "Ada Lovelace",
    Age = 36
};

// Lägg till i databas
db.People.Add(person);
db.SaveChanges();

Console.WriteLine($"Sparade {person.Name} med Id: {person.Id}");

// Hämta alla personer
var allPeople = db.People.ToList();
Console.WriteLine($"\nAntal personer i databas: {allPeople.Count}");

foreach (var p in allPeople)
{
    Console.WriteLine($"- {p.Name}, {p.Age} år");
}
```

## Steg 7 – Kör programmet (2 min)

```bash
dotnet run
```

**Förväntad output:**

```
Sparade Ada Lovelace med Id: 1

Antal personer i databas: 1
- Ada Lovelace, 36 år
```

## Utmaning 1 – Lägg till fler personer

Lägg till 3 personer till:

```csharp
var people = new List<Person>
{
    new Person { Name = "Grace Hopper", Age = 85 },
    new Person { Name = "Margaret Hamilton", Age = 87 },
    new Person { Name = "Alan Turing", Age = 41 }
};

db.People.AddRange(people);
db.SaveChanges();
```

## Utmaning 2 – Sök efter person

Hitta en person med visst namn:

```csharp
var grace = db.People.FirstOrDefault(p => p.Name.Contains("Grace"));
if (grace != null)
{
    Console.WriteLine($"Hittade: {grace.Name}");
}
```

## Utmaning 3 – Uppdatera ålder

Ändra åldern på en person:

```csharp
var ada = db.People.FirstOrDefault(p => p.Name == "Ada Lovelace");
if (ada != null)
{
    ada.Age = 37;
    db.SaveChanges();
    Console.WriteLine($"{ada.Name} är nu {ada.Age} år");
}
```

## Utmaning 4 – Radera person

Ta bort en person:

```csharp
var toRemove = db.People.FirstOrDefault(p => p.Name == "Alan Turing");
if (toRemove != null)
{
    db.People.Remove(toRemove);
    db.SaveChanges();
    Console.WriteLine($"Raderade {toRemove.Name}");
}
```

## Grattis!

Du har nu:

✅ Skapat din första databas med EF Core
✅ Lagt till data
✅ Hämtat data
✅ Uppdaterat data
✅ Raderat data

Detta är grunden i all databashantering – resten bygger på detta!

## Nästa steg

- Läs om [Entiteter](link-to-entiteter.md)
- Prova [CRUD-övningen](link-to-crud.md)
- Utforska [Relationer](link-to-relations.md)

## Troubleshooting

**Problem:** `dotnet ef` fungerar inte
**Lösning:** Installera verktyget globalt:

```bash

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
dotnet tool install --global dotnet-ef
```

**Problem:** Migration skapar fel tabell
**Lösning:** Ta bort migrationen och försök igen:

```bash
dotnet ef migrations remove
dotnet ef migrations add Initial
dotnet ef database update
```

**Problem:** Databasen uppdateras inte
**Lösning:** Radera `hello.db` och kör:

```bash
dotnet ef database update
```

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
