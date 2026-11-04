# Livekodning: Repository-mönstret med katter

**Lektion 4 nov 2026** · Kurs-03 Fördjupad OOP — Databaskopplingar

Vi tog det vi lärde oss om klasser och arv — och använde det för att bygga ett riktigt
lagringsmönster. Samma applikation, två helt olika databaser, noll ändringar i resten av koden.
Det är repository-mönstrets poäng.

---

## Modellen — vad är en katt?

Först definierar vi vad vi lagrar.

```csharp
public class Cat
{
    public string ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

### Varför `{ get; set; }` och inte bara ett vanligt fält?

Du kanske undrar varför vi inte skriver det enklare:

```csharp
public string Name; // public fält — undvik detta
```

Med ett public fält exponerar du allt direkt — vem som helst kan läsa och skriva fritt,
och du kan inte lägga till logik senare utan att ändra alla som använder klassen.

Med `{ get; set; }` (en *property*) kontrollerar du tillgången:

```csharp
public string Name { get; set; }         // alla kan läsa och skriva
public string Name { get; private set; } // alla kan läsa, bara klassen kan skriva
public string Name { get; }              // bara konstruktorn kan sätta värdet
```

Ramverk som `JsonSerializer` och Entity Framework kräver properties — de kan inte jobba med public fält.

---

## Problemet: vad händer när vi vill byta databas?

Om vi skriver all fillogik direkt i `Program.cs` — hur byter vi till SQLite sen?
Vi måste skriva om hela programmet.

Lösning: en abstrakt basklass som definierar *vad* ett repository ska kunna göra,
utan att bestämma *hur* det görs.

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

`Cat?` betyder att metoden kan returnera antingen en `Cat` eller `null` — ingenting alls.
Det händer till exempel om vi söker efter en katt som inte finns.

```csharp
Cat? hittadKatt = repository.GetCat("Pelle");
if (hittadKatt is not null)
{
    Console.WriteLine(hittadKatt.Name); // säkert — vi vet att den inte är null
}
```

**NullReferenceException** är den vanligaste kraschen i C#. `?` är ditt skyddsnät —
det tvingar dig att kolla innan du använder returvärdet.

---

## Implementation 1: JSON-fil

Vi bygger `CatRepository` del för del.

### Fälten

```csharp
public class CatRepository : CatRepositoryBase
{
    public override List<Cat> cats { get; set; } = new();
    public string FilePath { get; set; }
```

`cats` är en in-memory lista — vår "databas" medan programmet körs.
`FilePath` talar om var JSON-filen ska sparas.

### Lägga till och ta bort

```csharp
    public override void AddCat(Cat cat) => cats.Add(cat);

    public override void DeleteCat(Cat cat) => cats.Remove(cat);
```

### `=>` — expression-bodied members

Du ser `=>` här för första gången. Det är inte en ny sorts metod — bara en kortare skrivform:

```csharp
// Lång form
public override void AddCat(Cat cat)
{
    cats.Add(cat);
}

// Kort form med =>
public override void AddCat(Cat cat) => cats.Add(cat);
```

Exakt samma sak. Använd `=>` när hela metoden är ett enda uttryck — metoden ska se
lika kort ut som den faktiskt är. Det är Clean Code.

### Hämta en specifik katt

```csharp
    public override Cat? GetCat(string name) =>
        cats.FirstOrDefault(c => c.Name == name);
```

`FirstOrDefault` returnerar den första katten vars `Name` matchar — eller `null` om ingen hittas.
Därför är returtypen `Cat?` (kan vara null).

### Uppdatera en katt

```csharp
    public void UpdateCat(Cat cat)
    {
        var oldCat = GetCat(cat.Name);
        oldCat.Age = cat.Age;
    }
```

Vi hämtar en referens till rätt katt-objekt i listan och ändrar direkt på den.
Eftersom `oldCat` pekar på samma objekt som listan, uppdateras listan automatiskt.

### Spara till fil

```csharp
    public void SaveCats()
    {
        string filename = Path.Combine(FilePath, "Catlist.json");
        string json = JsonSerializer.Serialize(cats);
        File.WriteAllText(filename, json);
    }
```

`JsonSerializer.Serialize(cats)` omvandlar hela listan till en JSON-sträng.
`File.WriteAllText` skriver strängen till disk. Glömmer du anropa `SaveCats()` i slutet
försvinner alla ändringar när programmet stängs.

### Ladda från fil

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
            cats = new();
    }
}
```

`else cats = new()` är kritisk. Utan den: om filen inte finns förblir `cats` null,
och nästa `cats.Add(...)` kraschar med NullReferenceException.
Med `else`: ingen fil? Tomt lager. Programmet fortsätter utan krasch.

---

## SRP och SoC

`CatRepository` har ett ansvar: hantera katter i en JSON-fil. Den ändras bara om filformatet ändras.

`Program.cs` har ett ansvar: hantera användarens val. Den ändras bara om menyn förändras.

Det kallas **SRP** — Single Responsibility Principle. En klass, ett skäl att förändras.

**SoC** — Separation of Concerns — handlar om samma sak men på en högre nivå:
olika typer av bekymmer hör hemma i olika delar av koden.

| Bekymmer | Vem äger det |
|----------|-------------|
| Vad vill användaren? | `Program.cs` |
| Hur sparas katter? | `CatRepository` |
| Vad är en katt? | `Cat` |

Blandar du dessa i samma klass blir koden svårare att testa, byta ut och förstå.

---

## Implementation 2: SQLite — samma kontrakt, ny motor

Nu skapar vi `CatSqliteRepository` som implementerar exakt samma kontrakt — men mot en databas.

### Konstruktorn — koppla upp och skapa tabell

```csharp
public class CatSqliteRepository : CatRepositoryBase
{
    public override List<Cat> cats { get; set; } = new();
    private readonly string _connectionString;

    public CatSqliteRepository(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";
        CreateTableIfNotExists();
    }
```

Konstruktorn tar emot sökvägen till databasfilen och anropar direkt `CreateTableIfNotExists()`.
Det garanterar att tabellen alltid finns när vi börjar jobba mot den.

### Skapa tabellen

```csharp
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
```

`IF NOT EXISTS` betyder att SQL-satsen inte kraschar om tabellen redan finns.
Kör vi metoden igen händer ingenting — idempotent.

### `using var connection` — varför?

En databasanslutning är en begränsad resurs. Om du inte stänger den riskerar du
anslutningsläckor — programmet håller kvar resurser i onödan.

`using var` garanterar att `connection.Dispose()` anropas automatiskt när kodblocket är slut,
även om ett undantag kastas. Du behöver aldrig tänka på att stänga manuellt.

### Ladda alla katter

```csharp
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
```

`reader.Read()` stegar igenom ett resultat i taget — en rad per iteration.
`reader.GetString(0)` hämtar kolumn 0 (ID), `GetString(1)` kolumn 1 (Name), och så vidare.

### Lägga till en katt

```csharp
    public override void AddCat(Cat cat)
    {
        cat.ID = Guid.NewGuid().ToString();
```

#### `Guid.NewGuid()` — varför inte bara 1, 2, 3?

Räknare fungerar bra i ett system. Men i distribuerade system — två appar som skapar
katter oberoende av varandra och sen slår ihop sina databaser — krockar de.
Båda har en katt med ID 1.

`Guid` är ett globalt unikt ID, matematiskt garanterat unikt var och när det genereras.
Exempel: `a3f5b2c1-4d8e-4f7a-9b1c-2e3d4f5a6b7c`.

```csharp
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
```

#### SQL injection — varför `$param` och aldrig strängkonkatenering

**Fel sätt** (gör aldrig detta):
```csharp
cmd.CommandText = "INSERT INTO Cats (Name) VALUES ('" + cat.Name + "')";
```

Om `cat.Name` är `'; DROP TABLE Cats; --` raderas hela tabellen.
Det kallas SQL injection och är ett av de vanligaste säkerhetshålen (OWASP Top 10).

**Rätt sätt** — parametrar:
```csharp
cmd.CommandText = "INSERT INTO Cats (Name) VALUES ($name)";
cmd.Parameters.AddWithValue("$name", cat.Name);
```

Parametrar skickas separat från SQL-texten. Databasen behandlar dem alltid som data,
aldrig som kod. Ingen injection möjlig.

### Hämta och ta bort

```csharp
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

`GetCat` söker i minneslistan — vi har redan laddat allt via `LoadCats()`.
`DeleteCat` tar bort från databasen OCH från minneslistan, annars visas katten fortfarande i listan.

---

## Program.cs — minimalt

```csharp
// Byt kommentar för att byta databas — inget annat i koden behöver ändras
// var cr = new CatSqliteRepository(@"c:\git\cats.db");
var cr = new CatRepository();
cr.FilePath = @"c:\git";
cr.LoadCats();
```

Notera: för att byta från JSON till SQLite ändrar vi **en rad**.
Resten av programmet vet inte om det — det pratar bara med `CatRepositoryBase`.

```csharp
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

cr.SaveCats(); // sparar alltid tillbaka till fil innan programmet avslutas
```

`cr.SaveCats()` måste alltid anropas sist. Utan den försvinner alla ändringar.

---

## Hela resan i korthet

| Begrepp | Förklaring |
|---------|------------|
| Properties `{ get; set; }` | Kontrollerad tillgång till data |
| `Cat?` | Nullable — metoden kan returnera null, kolla innan du använder |
| `=>` | Expression-bodied — kort skrivform när metoden är ett uttryck |
| `else cats = new()` | Skyddsnät mot NullReferenceException |
| `using var` | Stänger resurser automatiskt |
| `Guid.NewGuid()` | Globalt unikt ID — säkrare än räknare |
| `$param` i SQL | Skyddar mot SQL injection — alltid parametrar |
| Repository-mönstret | Dölj lagringsdetaljerna bakom ett gemensamt gränssnitt |
| SRP | En klass, ett ansvar, ett skäl att förändras |
| SoC | UI, lagring och datamodell i separata delar av koden |

Koden finns på GitHub:
- [CatJson](https://github.com/marcusjobb/CatJson)
