---
marp: true
theme: nion-dark
paginate: true
---

# Mira flyttar in i en databas

**Kurs:** Fördjupad OOP i C#
**Modul:** 01 Spara och ladda data (EF Core)

Marcus Ackre Medina · YRGO · CLO26

---

## Förra veckan: Mira i en fil

```json
{
  "Name": "Mira",
  "Health": 100,
  "Level": 3
}
```

Det fungerade. Mira överlevde att programmet stängdes.

---

## Men tänk dig tusen hjältar

- Du vill ändra **en** hjälte. Hela filen måste läsas och skrivas om.
- Du vill hitta alla med mer än 50 i hälsa. Du läser **alla** först.
- Två program skriver samtidigt. Den ena skriver över den andra.

En databas löser det. Den kan söka, ändra en rad och hålla ordning.

---

## EF Core är tolken

```text
  C#-objekt                 EF Core              Databas (tabell Heroes)
  ─────────                 ───────              ─────────────────────────
  new Hero { "Mira" }   →   översätter   →       | Id | Name | Health | Level |
                                                 |  1 | Mira |  100   |   1   |
```

Du skriver C#. EF Core skriver SQL åt dig.

---

## Tre delar

| Del | Vad den gör |
|-----|-------------|
| `Hero` | Klassen. Blir en tabell. |
| `GameContext` | Kontakten med databasen. |
| `game.db` | Själva databasen, en fil (SQLite). |

---

## Först: verktygslådan (NuGet-paket)

**NuGet** = färdiga kodpaket som andra skrivit. Som appar i en appbutik.

```bash
dotnet new console -n HeroDb
cd HeroDb
dotnet add package Microsoft.EntityFrameworkCore.Sqlite   # EF Core som pratar med SQLite
dotnet add package Microsoft.EntityFrameworkCore.Design   # behövs för att skapa migrationer
dotnet tool install --global dotnet-ef                    # kommandot dotnet ef, en gång per dator
```

Kolla i `HeroDb.csproj`: två rader `<PackageReference>` har dykt upp.

---

## Klassen och kontexten

```csharp
class Hero
{
    public int Id { get; set; }               // Id blir primärnyckel automatiskt
    public string Name { get; set; } = "";
    public int Health { get; set; }
    public int Level { get; set; }
}

class GameContext : DbContext
{
    public DbSet<Hero> Heroes => Set<Hero>();   // tabellen Heroes

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=game.db");
}
```

Klasserna ligger **under** koden i `Program.cs`.

---

## Receptet (samma varje gång)

Paketen är på plats. Nu ritningen:

```bash
dotnet ef migrations add Init              # ritningen av databasen
dotnet run
```

En **migration** är en ritning: "så här ska tabellerna se ut".

---

## Öppna databasen

```csharp
using Microsoft.EntityFrameworkCore;

// Öppna databasen. Migrate() skapar filen game.db första gången
// och lägger till nya tabeller när du har gjort en ny migration.
using var db = new GameContext();
db.Database.Migrate();
```

`Migrate()` bygger databasen efter ritningen. Du behöver inte köra något mer kommando.

---

## Spara

```csharp
// SPARA: lägg till ett objekt och spara
var mira = new Hero { Name = "Mira", Health = 100, Level = 1 };
db.Heroes.Add(mira);
db.SaveChanges();
Console.WriteLine($"Sparade {mira.Name} med Id {mira.Id}");
```

```text
Sparade Mira med Id 1
```

Lägg märke till att du aldrig satte `Id`. Databasen gav Mira nummer 1.

---

## Titta in i databasen

**DB Browser for SQLite** (gratis, sqlitebrowser.org, Windows/Mac/Linux). Öppna `game.db`:

- `dotnet run` i projektmappen: filen ligger i `HeroDb/`
- Starta-knappen i Visual Studio: i `bin/Debug/net10.0/`

1. **Database Structure:** tabellen `Heroes` med kolumnerna. Och `__EFMigrationsHistory`, där EF noterar vilka migrationer som körts.
2. **Browse Data:** tryck uppdatera. Syns Mira?

Stäng databasen (**Close Database**) innan du kör programmet efter en ny migration. Annars kan filen vara låst.

---

## Läsa

```csharp
// LÄSA: hämta alla hjältar
foreach (Hero hero in db.Heroes.ToList())
{
    Console.WriteLine($"{hero.Id}: {hero.Name}, {hero.Health} hälsa, nivå {hero.Level}");
}
```

```text
1: Mira, 100 hälsa, nivå 1
```

---

## Ändra

```csharp
// HITTA OCH ÄNDRA: ändra objektet och spara igen
Hero? found = db.Heroes.Find(mira.Id);
if (found != null)
{
    found.Level = 2;
    db.SaveChanges();
    Console.WriteLine($"{found.Name} är nu nivå {found.Level}");
}
```

Ändra objektet som vanligt. `SaveChanges()` skickar ändringen.

---

## LINQ blir SQL

```csharp
// FILTRERA: LINQ blir SQL
var strong = db.Heroes.Where(h => h.Health >= 50);
Console.WriteLine(strong.ToQueryString());
```

```text
SELECT "h"."Id", "h"."Health", "h"."Level", "h"."Name"
FROM "Heroes" AS "h"
WHERE "h"."Health" >= 50
```

Samma SQL som i databaskursen. Den här gången skrev EF Core den.

---

## Ta bort

```csharp
// TA BORT
db.Heroes.Remove(mira);
db.SaveChanges();
Console.WriteLine($"Hjältar kvar: {db.Heroes.Count()}");
```

---

## Det här tar du med dig

1. En klass blir en tabell. `Id` blir nyckeln.
2. `DbContext` är kontakten. `DbSet` är tabellen.
3. Ny eller ändrad klass: `dotnet ef migrations add <Namn>`.
4. `Add`, `Find`, `Remove`, och sedan alltid `SaveChanges()`.

**Nu:** Övning, *Hjältarna i databasen*.
