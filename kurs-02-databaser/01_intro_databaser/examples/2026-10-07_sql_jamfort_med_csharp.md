# SQL jämfört med C# — livekodning 2026-10-07

Du kan redan C#. Det mesta vi gör i SQL har du alltså redan gjort, fast med listor, loopar och `if`. Här ställer vi dem bredvid varandra: *det här SQL-kommandot gör det här, och det är som om vi hade skrivit så här i C#.*

Exemplen bygger vidare på hjältarna i [Första SQLite-databasen](2026-10-07_forsta_sqlite_databasen.md).

## 1. Tabellen = en klass + en lista

I SQL beskriver vi hur en rad ser ut och skapar en tom tabell:

```sql
CREATE TABLE "People" (
    "Id"    INTEGER NOT NULL,
    "Name"  TEXT NOT NULL,
    "City"  TEXT,
    PRIMARY KEY("Id" AUTOINCREMENT)
);
```

I C# beskriver klassen hur *en* rad ser ut, och listan är själva tabellen:

```csharp
class People
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? City { get; set; }
}
```

```csharp
List<People> people = new List<People>();
```

| SQL | C# |
|---|---|
| Kolumn | Property |
| Rad | Objekt |
| Tabell | `List<People>` |
| `TEXT NOT NULL` | `string` |
| `TEXT` (får vara `NULL`) | `string?` |

## 2. INSERT = Add

```sql
INSERT INTO People (Name, City) VALUES
    ('Clark Kent', 'Metropolis'),
    ('Bruce Wayne', 'Gotham City');
```

är som:

```csharp
people.Add(new People() { Id = 1, Name = "Clark Kent", City = "Metropolis" });
people.Add(new People() { Id = 2, Name = "Bruce Wayne", City = "Gotham City" });
```

Lägg märke till `Id`. I SQL sköter `AUTOINCREMENT` numreringen åt oss. Listan i C# har ingen aning om att `Id` ska vara unikt, så där får vi hålla koll själva.

## 3. SELECT * = foreach

```sql
SELECT * FROM People;
```

är som att gå igenom hela listan och skriva ut varje rad:

```csharp
foreach (People p in people)
    Console.WriteLine($"{p.Id} {p.Name} {p.City}");
```

## 4. WHERE = if

```sql
SELECT Name FROM People WHERE Name LIKE 'Clark%';
```

är som en loop med ett `if` i:

```csharp
foreach (People p in people)
    if (p.Name.StartsWith("Clark"))
        Console.WriteLine(p.Name);
```

`%` betyder "vad som helst". Var `%` står avgör vilken C#-metod det motsvarar:

| SQL | C# | Hittar |
|---|---|---|
| `LIKE 'Clark%'` | `StartsWith("Clark")` | Namn som *börjar* på Clark |
| `LIKE '%Kent'` | `EndsWith("Kent")` | Namn som *slutar* på Kent |
| `LIKE '%Clark%'` | `Contains("Clark")` | Namn som *innehåller* Clark var som helst |
| `= 'Clark Kent'` | `== "Clark Kent"` | Exakt det namnet |

> **Skillnad:** `LIKE` i SQLite bryr sig inte om stora och små bokstäver, så `'clark%'` hittar också Clark. `StartsWith` i C# gör skillnad på dem.

## 5. LIMIT 1 = Find

Vill vi bara ha den *första* som matchar:

```sql
SELECT Name FROM People WHERE Name LIKE 'Clark%' LIMIT 1;
```

är som:

```csharp
string? sup = people.Find(p => p.Name.StartsWith("Clark"))?.Name;
Console.WriteLine(sup);
```

`Find` slutar leta så fort den hittar en träff, precis som `LIMIT 1`. Hittar den ingen får vi `null`, och därför står det `?.` före `Name`.

## 6. UPDATE = foreach + if + tilldelning

```sql
UPDATE People SET City = 'Metropolis' WHERE Name = 'Bruce Wayne';
```

är som:

```csharp
foreach (People p in people)
    if (p.Name == "Bruce Wayne")
        p.City = "Metropolis";
```

`WHERE` är `if`-satsen och `SET` är tilldelningen. Glömmer du `if` i C# får *alla* Metropolis som stad. Glömmer du `WHERE` i SQL händer exakt samma sak.

## 7. Så varför inte bara använda en lista?

| | `List<People>` i C# | Tabell i en databas |
|---|---|---|
| **Var bor datan?** | I minnet. Borta när programmet stängs. | I en fil. Finns kvar tills du raderar den. |
| **Unika `Id`** | Du får hålla koll själv | `AUTOINCREMENT` sköter det |
| **Regler** | Bara det du själv kodar | `NOT NULL`, `UNIQUE`, `CHECK`, främmande nycklar |
| **Hur du frågar** | Du skriver *hur* den ska leta: loop, `if`, utskrift | Du skriver *vad* du vill ha, och databasen räknar ut hur |
| **Flera användare** | Ett program åt gången | Många program och användare samtidigt |

Den viktigaste raden är *hur* kontra *vad*. I C# skriver vi loopen själva. I SQL beskriver vi bara resultatet vi vill ha.

## Hela programmet

```csharp
Console.WriteLine("Hello, SQL!");

// CREATE TABLE
List<People> people = new List<People>();

// INSERT INTO People (Name, City) VALUES (...), (...);
people.Add(new People() { Id = 1, Name = "Clark Kent", City = "Metropolis" });
people.Add(new People() { Id = 2, Name = "Bruce Wayne", City = "Gotham City" });

// SELECT * FROM People;
foreach (People p in people)
    Console.WriteLine($"{p.Id} {p.Name} {p.City}");

// SELECT Name FROM People WHERE Name LIKE 'Clark%';
foreach (People p in people)
    if (p.Name.StartsWith("Clark"))
        Console.WriteLine(p.Name);

// SELECT Name FROM People WHERE Name LIKE 'Clark%' LIMIT 1;
string? sup = people.Find(p => p.Name.StartsWith("Clark"))?.Name;
Console.WriteLine(sup);

// UPDATE People SET City = 'Metropolis' WHERE Name = 'Bruce Wayne';
foreach (People p in people)
    if (p.Name == "Bruce Wayne")
        p.City = "Metropolis";

class People
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? City { get; set; }
}
```

## TL;DR

| SQL | C# |
|---|---|
| `CREATE TABLE` | `class` + `new List<>()` |
| `INSERT INTO` | `.Add()` |
| `SELECT *` | `foreach` |
| `WHERE` | `if` |
| `LIKE 'Clark%'` / `'%Kent'` / `'%Clark%'` | `StartsWith` / `EndsWith` / `Contains` |
| `LIMIT 1` | `.Find()` |
| `UPDATE ... SET ... WHERE` | `foreach` + `if` + `=` |

En lista lever i minnet och försvinner när programmet stängs. En databas sparar datan, håller koll på reglerna och låter dig fråga efter *vad* du vill ha i stället för att skriva *hur* den ska leta.

## Summan av kardemumman

I senare artiklar ska vi använda c# och SQL databasen, denna artikeln är mer för att det ska bli lättare att tänka på det.