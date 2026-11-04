# Livekodning: Repository-mönstret med katter

**Lektion 4 nov 2026** · Kurs-03 Fördjupad OOP — Databaskopplingar

Vi tog det vi lärde oss om klasser och arv och använde det för att bygga ett riktigt
lagringsmönster. Samma kod, två helt olika databaser. Det är poängen med det vi kodade idag.

Koden finns på GitHub: [CatJson](https://github.com/marcusjobb/CatJson)

---

## Modellen: vad är en katt?

Innan vi lagrar något behöver vi veta *vad* vi lagrar. Vi börjar med en enkel dataklass.

```csharp
public class Cat
{
    public string ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

Det här är en POCO — Plain Old C# Object. En enkel klass utan logik, bara data.
Den har tre properties med `{ get; set; }`.

**Varför `{ get; set; }` och inte bara `public string Name;`?**

`public string Name;` är ett publikt fält — vem som helst kan läsa och skriva till det,
och du kan aldrig lägga till kontroll i efterhand utan att bryta alla som använder klassen.

`public string Name { get; set; }` är en property. Det ser likadant ut utifrån, men ger dig:
- Möjlighet att lägga till logik (validering, beräkning) i get eller set utan att ändra
  anropskoden
- `{ get; private set; }` = alla kan läsa, bara klassen själv kan skriva
- Kompatibilitet med JSON-serialisering, databas-ramverk och UI-bindning — de flesta
  ramverk kräver properties

Tumregel: exponera aldrig data som publika fält. Använd alltid properties.

---

## Problemet: hur byter vi databas utan att skriva om allt?

Om vi skriver all fillogik direkt i `Program.cs`, vad händer när vi vill byta till SQLite?
Vi måste leta igenom hela `Program.cs` och skriva om varje del som rör filer.

Det bryter mot **SRP — Single Responsibility Principle**:
> En klass ska ha ett enda ansvar. Ett enda skäl att förändras.

`Program.cs` som hanterar UI *och* filer = två skäl att ändras = ett problem.

Och det bryter mot **SoC — Separation of Concerns**:
> Olika "bekymmer" — vad användaren ser, hur data lagras — ska hanteras av olika delar.

Lösningen: en abstrakt basklass som definierar vad ett repository ska kunna göra,
utan att bestämma hur det görs.

---

## Basklassen: kontraktet

```csharp
public abstract class CatRepositoryBase
{
    public abstract List<Cat> cats { get; set; }
    public abstract void AddCat(Cat cat);
    public abstract void DeleteCat(Cat cat);
    public abstract Cat? GetCat(string name);
    public abstract void LoadCats();
}
```

Det här är bara ett löfte. `abstract` på metoderna betyder: ingen kropp här — den som
ärver basklassen *måste* implementera dessa metoder, annars kompilerar inte koden.

`Cat?` på `GetCat` — frågetecknet berättar att metoden kan returnera null.

**Om `?` och null**

Null är frånvaron av ett värde. Den vanligaste orsaken till kraschar i C# är att försöka
använda ett objekt som är null — `NullReferenceException`.

`Cat?` = "resultatet kan vara null — kolla innan du använder det."

Det är kompilatorns sätt att tvinga dig att tänka på felfallet. Om du skriver `Cat` (utan `?`)
lovar du att du *aldrig* returnerar null.

---

## Implementation 1: JSON-fil

```csharp
using System.Text.Json;

public class CatRepository : CatRepositoryBase
{
    public override List<Cat> cats { get; set; } = new();
    public string FilePath { get; set; }

    public override void AddCat(Cat cat) => cats.Add(cat);

    public override Cat? GetCat(string name) =>
        cats.FirstOrDefault(c => c.Name == name);

    public void UpdateCat(Cat cat)
    {
        var oldCat = GetCat(cat.Name);
        oldCat.Age = cat.Age;
    }

    public override void DeleteCat(Cat cat) => cats.Remove(cat);

    public void SaveCats()
    {
        string filename = Path.Combine(FilePath, "Catlist.json");
        string json = JsonSerializer.Serialize(cats);
        File.WriteAllText(filename, json);
    }

    public override void LoadCats()
    {
        string filename = Path.Combine(FilePath, "Catlist.json");
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            cats = JsonSerializer.Deserialize<List<Cat>>(json);
        }
        else
            cats = new();
    }
}
```

**Expression-bodied members: `=>`**

Du ser `=>` på `AddCat` och `GetCat`. Det är ett nytt syntax för er — expression-bodied members.

```csharp
public override void AddCat(Cat cat) => cats.Add(cat);
```

Det är exakt samma som:

```csharp
public override void AddCat(Cat cat)
{
    cats.Add(cat);
}
```

Reglerna är enkla: använd `=>` när hela metoden är *ett enda uttryck*. Resultatet är
renare kod — korta metoder ska se korta ut. Det är Clean Code i praktiken.

**Listan som in-memory spegel**

`cats`-listan är programmets "arbetsminne". Den speglar vad som finns i filen:

1. `LoadCats()` vid start: läs filen, fyll listan
2. `AddCat()` / `DeleteCat()` under körning: ändra bara listan i minnet
3. `SaveCats()` vid avslut: skriv listan tillbaka till fil

Utan steg 3 försvinner alla ändringar när programmet stängs.

**`else cats = new()` — varför måste den finnas?**

```csharp
if (File.Exists(filename))
{
    string json = File.ReadAllText(filename);
    cats = JsonSerializer.Deserialize<List<Cat>>(json);
}
else
    cats = new(); // Filen finns inte än — skapa tom lista
```

`JsonSerializer.Deserialize` kan returnera null. Om vi inte hanterar det fallet och
sedan försöker anropa `cats.Add(...)` — kraschar programmet med `NullReferenceException`.
`else cats = new()` garanterar att listan alltid är initialiserad, oavsett om filen finns.

**`FirstOrDefault` och null**

```csharp
public override Cat? GetCat(string name) =>
    cats.FirstOrDefault(c => c.Name == name);
```

`FirstOrDefault` returnerar den första katten vars namn matchar, eller null om ingen hittas.
Returtypen är `Cat?` — vi lovar ingenting, kolla alltid resultatet.

---

## Implementation 2: SQLite — samma kontrakt, ny motor

Nu byter vi lagring från JSON-fil till SQLite-databas. Notera att vi inte rör basklassen
eller `Program.cs` — vi skapar bara en ny implementation.

```csharp
using Microsoft.Data.Sqlite;

public class CatSqliteRepository : CatRepositoryBase
{
    public override List<Cat> cats { get; set; } = new();

    private readonly string _connectionString;

    public CatSqliteRepository(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";
        CreateTableIfNotExists();
    }

    private void CreateTableIfNotExists()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var cmd = connection.CreateCommand();
        cmd.CommandText = """
            CREATE TABLE IF NOT EXISTS Cats (
                ID   TEXT PRIMARY KEY,
                Name TEXT NOT NULL,
                Age  INTEGER NOT NULL
            )
            """;
        cmd.ExecuteNonQuery();
    }

    public override void LoadCats()
    {
        cats = new();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT ID, Name, Age FROM Cats";
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            cats.Add(new Cat
            {
                ID   = reader.GetString(0),
                Name = reader.GetString(1),
                Age  = reader.GetInt32(2)
            });
    }

    public override void AddCat(Cat cat)
    {
        cat.ID = Guid.NewGuid().ToString();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "INSERT INTO Cats (ID, Name, Age) VALUES ($id, $name, $age)";
        cmd.Parameters.AddWithValue("$id",   cat.ID);
        cmd.Parameters.AddWithValue("$name", cat.Name);
        cmd.Parameters.AddWithValue("$age",  cat.Age);
        cmd.ExecuteNonQuery();
        cats.Add(cat);
    }

    public override Cat? GetCat(string name) =>
        cats.FirstOrDefault(c => c.Name == name);

    public override void DeleteCat(Cat cat)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "DELETE FROM Cats WHERE ID = $id";
        cmd.Parameters.AddWithValue("$id", cat.ID);
        cmd.ExecuteNonQuery();
        cats.Remove(cat);
    }
}
```

**Parametrar i SQL: `$id`, `$name`, `$age`**

Skriv aldrig detta:

```csharp
cmd.CommandText = "INSERT INTO Cats (Name) VALUES ('" + cat.Name + "')";
```

Om `cat.Name` innehåller `'` eller SQL-kommandon kan en angripare manipulera din databas —
det kallas **SQL injection** och är ett av de vanligaste säkerhetshålen.

Skriv alltid parametrar:

```csharp
cmd.CommandText = "INSERT INTO Cats (Name) VALUES ($name)";
cmd.Parameters.AddWithValue("$name", cat.Name);
```

Databasen behandlar då `$name` som data, aldrig som SQL-kod. Oavsett vad användaren skriver in.

**`using var connection`**

`using` ser till att databasanslutningen stängs och resurser frigörs automatiskt när
blocket är klart — även om ett undantag kastas. Utan `using` kan du läcka anslutningar
och låsa databasen.

**`Guid.NewGuid().ToString()`**

Varje katt behöver ett unikt ID. `Guid` (Globally Unique Identifier) genererar ett
slumpmässigt ID som är unikt med astronomisk sannolikhet — används globalt utan koordinering.
Exempel: `"3f2504e0-4f89-11d3-9a0c-0305e82c3301"`.

---

## Program.cs: minimal och databas-agnostisk

```csharp
Console.WriteLine("Hello, CatJson!");

// var cr = new CatSqliteRepository(@"c:\git\cats.db"); // <-- SQLite
var cr = new CatRepository();                            // <-- JSON-fil
cr.FilePath = @"c:\git";
cr.LoadCats();

Console.WriteLine("1 - lista alla katter");
Console.WriteLine("2 - Lägg till en katt");

var input = Console.ReadLine();

switch (input)
{
    case "1":
        foreach (Cat cat in cr.cats)
            Console.WriteLine(cat.Name);
        break;
    case "2":
        var name = Console.ReadLine();
        cr.AddCat(new Cat { Name = name, Age = 1 });
        break;
}

cr.SaveCats();
```

`Program.cs` vet ingenting om JSON eller SQLite. Den pratar bara med repositoryt.

Vill du byta till SQLite är det **en rad**:

```csharp
// Kommentera ut JSON:
// var cr = new CatRepository();

// Lägg till SQLite:
var cr = new CatSqliteRepository(@"c:\git\cats.db");
```

Resten av programmet är oförändrat. Det är exakt vad SRP och SoC ger dig: möjligheten
att byta ut en del utan att röra resten.

---

## Sammanfattning: hur man tänker

**När skapar man en abstrakt basklass?**

När du märker att du är på väg att kopiera metoder eller properties mellan klasser.
"Jag ska skriva en `JsonRepository` och en `SqliteRepository` — de har exakt samma metoder
men olika implementationer." Det är tecknet. Skapa basklassen först, implementera sedan.

**SRP i ett stycke**

Fråga dig: hur många skäl har den här klassen att ändras? Om svaret är mer än ett —
dela upp klassen. `CatRepository` ändras om filformatet ändras. `Program.cs` ändras om
UI-logiken ändras. De delar inte skäl, och ska inte dela klass.

**SoC i ett stycke**

Håll isär vad koden *gör* (UI, affärslogik) från *hur* den gör det (filer, databaser, nätverk).
Det gör koden lättare att testa: du kan testa `CatRepository` utan att starta ett UI.
Det gör koden lättare att underhålla: du vet var du ska leta när något går fel.

---

## Begrepp på ett ställe

| Begrepp | Förklaring |
|---------|------------|
| POCO | Plain Old C# Object — enkel dataklass utan logik |
| `{ get; set; }` | Property — kontrollerad tillgång till data |
| Abstract basklass | Definierar kontraktet, implementerar ingenting |
| Konkret implementation | Bestämmer HUR det görs (JSON, SQLite, API...) |
| `=>` | Expression-bodied member — kort metod på en rad |
| `Cat?` | Returtypen kan vara null — kolla innan du använder |
| `$param` i SQL | Parametriserade queries — skyddar mot SQL injection |
| `using var connection` | Stänger resurser automatiskt |
| `Guid.NewGuid()` | Globalt unikt ID utan koordinering |
| SRP | En klass, ett ansvar, ett skäl att ändras |
| SoC | Separata bekymmer hanteras av separata delar |

Koden: [CatJson](https://github.com/marcusjobb/CatJson)
