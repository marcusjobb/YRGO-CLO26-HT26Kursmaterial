# Livekodning: Repository-mönstret med katter

**Lektion 4 nov 2026** · Kurs-03 Fördjupad OOP — Databaskopplingar

Vi tog det vi lärde oss om klasser och arv — och använde det för att bygga ett riktigt
lagringsmönster. Samma kod, två helt olika databaser. Det är poängen.

---

## Modellen — vad är en katt?

Först behöver vi veta vad vi lagrar.

```csharp
public class Cat
{
    public string ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```

En enkel datamodell. Inget mer, inget mindre.

---

## Problemet: vad händer när vi vill byta databas?

Om vi skriver all fillogik direkt i `Program.cs` — hur byter vi till SQLite sen?
Vi måste skriva om hela programmet.

Lösning: abstrakt basklass som definierar *vad* ett repository ska kunna göra,
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
Basklassen vet ingenting om filer eller databaser — det är medvetet.

---

## Implementation 1: JSON-fil

```csharp
public class CatRepository : CatRepositoryBase
{
    public override List<Cat> cats { get; set; } = new();
    public string FilePath { get; set; }

    public override void AddCat(Cat cat) => cats.Add(cat);

    public override Cat? GetCat(string name) =>
        cats.FirstOrDefault(c => c.Name == name);

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
            cats = new(); // filen finns inte än — tom lista
    }
}
```

Notera `else cats = new()` — utan den grenen kan `cats` vara null och krascha nästa gång.

Listan fungerar som en in-memory spegel av filen:
- `LoadCats()` läser in filen till listan vid start
- `AddCat()` / `DeleteCat()` ändrar bara listan i minnet
- `SaveCats()` skriver tillbaka listan till fil vid avslut

---

## Implementation 2: SQLite — samma kontrakt, ny motor

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
            cats.Add(new Cat { ID = reader.GetString(0), Name = reader.GetString(1), Age = reader.GetInt32(2) });
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

Notera `$id`, `$name`, `$age` — parametrar i SQLite-kommandon förhindrar SQL injection.
Aldrig konkatenera strängar direkt i SQL.

---

## Program.cs — minimalt

```csharp
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

`Program.cs` vet ingenting om JSON eller SQLite — den pratar bara med repositoryt.
Vill vi byta till SQLite är det **en rad**:

```csharp
// var cr = new CatRepository();          // JSON-fil
var cr = new CatSqliteRepository(@"c:\git\cats.db"); // SQLite
```

Resten av programmet är oförändrat. Det är poängen med repository-mönstret.

---

## Vad vi lärde oss

| Begrepp | Förklaring |
|---------|------------|
| Repository-mönstret | Dölj lagringsdetaljerna bakom ett gemensamt gränssnitt |
| Abstrakt basklass | Definierar kontraktet — vad som MÅSTE finnas |
| Konkret implementation | Bestämmer HUR det görs (JSON, SQLite, API...) |
| `$param` i SQL | Skyddar mot SQL injection — alltid parametrar, aldrig string concat |
| `SaveCats()` vid avslut | Utan den försvinner ändringar när programmet stängs |

Koden finns på GitHub:
- [CatJson](https://github.com/marcusjobb/CatJson)
