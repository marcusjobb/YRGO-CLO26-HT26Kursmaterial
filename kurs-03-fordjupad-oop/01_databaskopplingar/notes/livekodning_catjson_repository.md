# Livekodning: Repository-mönstret med katter

**Lektion 4 nov 2026** · Kurs-03 Fördjupad OOP — Databaskopplingar

Vi tog det vi lärde oss om klasser och arv — och använde det för att bygga ett riktigt
lagringsmönster. Samma applikation, två helt olika databaser, noll ändringar i resten av koden.
Det är repository-mönstrets poäng.

---

## Modellen — vad är en katt?

Först behöver vi definiera vad vi lagrar.

```csharp
public class Cat
{
    public string ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

### Properties — varför `{ get; set; }` och inte bara ett vanligt fält?

Du kanske undrar varför vi inte skriver det enklare:

```csharp
public string Name; // public fält — undvik detta
```

Skillnaden: ett public fält exponerar allt direkt. Vem som helst kan läsa och skriva fritt,
och du kan inte lägga till logik senare utan att ändra alla som använder klassen.

Med `{ get; set; }` (en property) kontrollerar du tillgången:

```csharp
public string Name { get; set; }         // alla kan läsa och skriva
public string Name { get; private set; } // alla kan läsa, bara klassen kan skriva
public string Name { get; }              // bara konstruktorn kan sätta värdet
```

Praktiskt exempel: om du senare vill logga varje gång `Name` sätts, kan du göra det
i settern utan att ändra ett enda anrop utanför klassen. Med ett public fält går det inte.

Ramverk som `JsonSerializer` och Entity Framework kräver också properties — de kan inte
jobba med public fält.

---

## Problemet: vad händer när vi vill byta databas?

Om vi skriver all fillogik direkt i `Program.cs` — hur byter vi till SQLite sen?
Vi måste skriva om hela programmet. Det är dyrt och riskabelt.

Lösning: en abstrakt basklass som definierar *vad* ett repository ska kunna göra,
utan att bestämma *hur* det görs. Det kallas **Repository-mönstret**.

---

## Basklassen — kontraktet

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

Det här är bara ett löfte: "den som ärver mig MÅSTE implementera dessa metoder."
Basklassen vet ingenting om filer eller databaser — det är helt medvetet.

### Vad betyder `Cat?` med frågetecknet?

`Cat?` betyder att metoden kan returnera antingen en `Cat` eller `null` — det vill säga inget alls.
Det händer till exempel om vi söker efter en katt som inte finns.

Utan `?` lovar du att du alltid returnerar ett objekt. Lögner kraschar program.
Med `?` tvingar C# dig att kolla innan du använder returvärdet:

```csharp
Cat? hittadKatt = repository.GetCat("Pelle");
if (hittadKatt is not null)
{
    Console.WriteLine(hittadKatt.Name); // säkert — vi vet att den inte är null
}
```

**NullReferenceException** är den vanligaste kraschen i C#. `?` är ditt skyddsnät.

### När vet man att det är dags att skapa en basklass?

Code smell: du kopierar fält eller metoder mellan klasser.
Om `HeroStorage` och en framtida `CatStorage` skulle ha samma `SavePath`-logik —
då är det dags att flytta till en gemensam basklass.

Regeln: **om förändringen ska synas på två ställen, flytta den.**

**Hur man extraherar en basklass ur en befintlig klass i IDE:**

*Visual Studio:*
1. Klicka på klassnamnet (t.ex. `CatRepository`)
2. Tryck `Ctrl+.` (eller klicka på glödlampan)
3. Välj "Extract base class..."
4. Namnge basklassen, välj vilka members som ska med

*Rider (JetBrains):*
1. Högerklicka på klassnamnet
2. Refactor → Extract Superclass...
3. Alternativt: `Ctrl+Alt+Shift+T` → Extract Superclass

*VS Code (C# Dev Kit):*
1. Högerklicka på klassnamnet → Quick Fix
2. Om alternativet "Extract base class" syns — välj det
3. Fungerar inte alltid — VS eller Rider är bättre för tung refaktorering

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

### `=>` — expression-bodied members

Du ser `=>` på flera ställen. Det är inte en ny sorts metod — det är bara en kortare skrivform:

```csharp
// Lång form
public override void AddCat(Cat cat)
{
    cats.Add(cat);
}

// Kort form med =>
public override void AddCat(Cat cat) => cats.Add(cat);
```

Exakt samma sak. Använd `=>` när hela metoden är ett enda uttryck.
Det kallas *expression-bodied member* och är Clean Code — korta metoder ska se korta ut.

```csharp
// Längre exempel: returvärde
public override Cat? GetCat(string name) =>
    cats.FirstOrDefault(c => c.Name == name);

// Samma som:
public override Cat? GetCat(string name)
{
    return cats.FirstOrDefault(c => c.Name == name);
}
```

### Varför `else cats = new()`?

Det här är ett klassiskt buggtillfälle:

```csharp
public override void LoadCats()
{
    string filename = Path.Combine(FilePath, "Catlist.json");
    if (File.Exists(filename))
    {
        string json = File.ReadAllText(filename);
        cats = JsonSerializer.Deserialize<List<Cat>>(json);
    }
    else
        cats = new(); // <-- DEN HÄR RADEN ÄR KRITISK
}
```

Utan `else`-grenen: om filen inte finns, förblir `cats` vad den var — eventuellt `null`.
Nästa gång du anropar `cats.Add(...)` kraschar programmet med NullReferenceException.

Med `else cats = new()`: ingen fil? Tomt lager. Programmet kan fortsätta utan att krascha.

### Listan som spegel

Listan `cats` är en in-memory spegel av det som finns i filen:
- `LoadCats()` läser filen till listan vid start
- `AddCat()` / `DeleteCat()` ändrar bara listan i minnet
- `SaveCats()` skriver tillbaka listan till fil vid avslut

Glömmer du `SaveCats()` i slutet försvinner alla ändringar när programmet stängs.

---

## SRP — Single Responsibility Principle

`CatRepository` har ett ansvar: hantera katter i en JSON-fil.
Den ändras bara om filformatet ändras.

`Program.cs` har ett ansvar: hantera användarens val.
Den ändras bara om menyn eller flödet förändras.

Hade vi lagt fillogiken i `Program.cs` hade den haft *två* skäl att förändras —
det är ett brott mot SRP. En klass, ett ansvar.

## SoC — Separation of Concerns

SoC och SRP hänger ihop men handlar om olika saker:
- **SRP**: en klass har ett ansvar
- **SoC**: olika *typer* av bekymmer sköts av olika delar av koden

Bekymren i den här applikationen:
- **UI-bekymret**: vad ska skrivas ut, vad vill användaren? → `Program.cs`
- **Lagringsbekymret**: hur sparas katter, var, i vilket format? → `CatRepository`
- **Datamodells-bekymret**: vad är en katt? → `Cat`

Varje fil har sitt bekymmer. Blandar du dem blir koden svårare att testa, byta ut och förstå.

---

## Implementation 2: SQLite — samma kontrakt, ny motor

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

### `using var connection` — varför?

En databasanslutning är en begränsad resurs. Om du inte stänger den finns det risk för
anslutningsläckor — programmet håller kvar resurser i onödan.

`using var` garanterar att `connection.Dispose()` anropas automatiskt när kodblocket är slut,
även om ett undantag kastas. Du behöver aldrig tänka på att stänga manuellt.

```csharp
using var connection = new SqliteConnection(_connectionString);
// connection.Dispose() anropas automatiskt när vi lämnar detta block
```

### `Guid.NewGuid()` — varför inte bara 1, 2, 3?

```csharp
cat.ID = Guid.NewGuid().ToString();
// Exempel: "a3f5b2c1-4d8e-4f7a-9b1c-2e3d4f5a6b7c"
```

Räknare (1, 2, 3...) fungerar bra i ett system. Men vad händer om du har två system
som skapar katter oberoende av varandra och sen ska slå ihop sina databaser?
Båda har en katt med ID 1 — krock.

`Guid` är ett globalt unikt ID — matematiskt garanterat unikt oavsett var och när det genereras.
Används överallt i distribuerade system och API:er.

### SQL injection — varför `$param` och aldrig strängkonkatenering

**Fel sätt** (gör aldrig detta):
```csharp
cmd.CommandText = "DELETE FROM Cats WHERE Name = '" + name + "'";
```

Om `name` är `'; DROP TABLE Cats; --` raderas hela tabellen.
Det kallas SQL injection och är ett av de vanligaste säkerhetshålen (OWASP Top 10).

**Rätt sätt** — parametrar:
```csharp
cmd.CommandText = "DELETE FROM Cats WHERE ID = $id";
cmd.Parameters.AddWithValue("$id", cat.ID);
```

Parametrar skickas separat från SQL-texten. Databasen behandlar dem alltid som data,
aldrig som kod. Ingen injection möjlig.

---

## Program.cs — minimalt

```csharp
Console.WriteLine("Hello, CatJson!");

// Byt kommentar för att byta databas — inget annat i koden behöver ändras
// var cr = new CatSqliteRepository(@"c:\git\cats.db");
var cr = new CatRepository();
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

Vill vi byta från JSON-fil till SQLite är det **en rad**:

```csharp
// var cr = new CatRepository();                          // JSON
var cr = new CatSqliteRepository(@"c:\git\cats.db");   // SQLite
```

Resten av programmet är oförändrat. Det är repository-mönstrets hela poäng.

---

## Hela resan i korthet

| Begrepp | Förklaring |
|---------|------------|
| Properties `{ get; set; }` | Kontrollerad tillgång till data — inte bara ett fält |
| `Cat?` | Nullable — metoden kan returnera null, kolla innan du använder |
| `=>` | Expression-bodied — kort skrivform när metoden är ett uttryck |
| `else cats = new()` | Skyddsnät — utan den kan listan vara null och krascha |
| `using var` | Stänger resurser automatiskt — aldrig glömma att stänga |
| `Guid.NewGuid()` | Globalt unikt ID — säkrare än räknare i distribuerade system |
| `$param` i SQL | Skyddar mot SQL injection — alltid parametrar, aldrig string concat |
| Repository-mönstret | Dölj lagringsdetaljerna bakom ett gemensamt gränssnitt |
| SRP | En klass, ett ansvar, ett skäl att förändras |
| SoC | UI, lagring och datamodell i separata delar av koden |

Koden finns på GitHub:
- [CatJson](https://github.com/marcusjobb/CatJson)
