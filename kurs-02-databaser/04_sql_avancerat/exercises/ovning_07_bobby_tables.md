# Övning 7 — Bobby Tables 🔴

> *"Her daughter is named Help I'm trapped in a driver's license factory."*
> — xkcd #327

Den här övningen visar hur SQL injection fungerar — och varför parameteriserade queries är det enda rätta.

Du bygger ett litet program i tre steg. Kör varje steg och läs vad som händer innan du går vidare.

---

## Steg 1 — Skapa tabellen och testa ett felaktigt anrop

Skapa ett nytt konsolprojekt (`SQLite_Bobby`) och lägg till `Microsoft.Data.Sqlite`.

Börja med att skapa tabellen `Studenter`. Men innan du gör något INSERT — testa ett medvetet felaktigt SQL-anrop så att du ser hur felmeddelanden ser ut.

```csharp
using Microsoft.Data.Sqlite;

var connection = new SqliteConnection("Data Source=skolan.db");
connection.Open();

// Steg 1a: Skapa tabellen
var create = connection.CreateCommand();
create.CommandText = """
    CREATE TABLE IF NOT EXISTS Studenter (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Namn TEXT NOT NULL
    )
    """;
create.ExecuteNonQuery();
Console.WriteLine("Tabellen Studenter skapad.");

// Steg 1b: Testa ett felaktigt anrop mot en tabell som INTE finns
try
{
    var felaktig = connection.CreateCommand();
    felaktig.CommandText = "SELECT * FROM AnvändareFinnsInte";
    felaktig.ExecuteReader();
}
catch (SqliteException ex)
{
    Console.WriteLine($"Databasfel: {ex.Message}");
}

Console.WriteLine("\nTrycks Enter för att gå vidare till inserts...");
Console.ReadLine();
```

### Förväntad output

```
Tabellen Studenter skapad.
Databasfel: SQLite Error 1: 'no such table: AnvändareFinnsInte'.

Trycks Enter för att gå vidare till inserts...
```

Bra. Nu vet du hur ett databasfel ser ut. `try-catch` fångar det utan att krascha programmet.

---

## Steg 2 — Input-loopen med strängkonkatenering (det farliga)

Ta bort första testet (Steg 1b), då vi nu vet att den funkar.

Nu bygger du loopen. För varje namn användaren matar in: INSERT → `Console.Clear()` → visa hela listan.

Här bygger vi SQL:en med strängkonkatenering — precis som man **inte** ska göra.

```csharp
// Steg 2: Input-loop MED strängkonkatenering — SÅRBAR VERSION
while (true)
{
    Console.Write("Ange namn (eller 'q' för att avsluta): ");
    string input = Console.ReadLine() ?? "";

    if (input.ToLower() == "q") break;
    if (string.IsNullOrWhiteSpace(input)) continue;

    // ⚠️ SÅRBAR — input klistras rakt in i SQL-strängen
    var insert = connection.CreateCommand();
    insert.CommandText = "INSERT INTO Studenter (Namn) VALUES ('" + input + "')";

    try
    {
        insert.ExecuteNonQuery();
    }
    catch (SqliteException ex)
    {
        Console.WriteLine($"Databasfel: {ex.Message}");
        continue;
    }

    // Rensa och visa uppdaterad lista
    Console.Clear();
    Console.WriteLine("=== Lista på studenter ===\n");

    var select = connection.CreateCommand();
    select.CommandText = "SELECT Id, Namn FROM Studenter ORDER BY Id";
    using var reader = select.ExecuteReader();

    while (reader.Read())
    {
        Console.WriteLine($"  {reader["Id"],3}. {reader["Namn"]}");
    }

    Console.WriteLine();
}
```

Mata in tre vanliga namn — Anna, Bo, Kalle — och se att listan uppdateras för varje.

---

## Steg 3 — Bobby Tables

Nu är det dags. Mata in det här som ett namn:

```
Robert'); DELETE FROM Studenter; --
```

### Vad tror du händer?

Fundera en sekund innan du trycker Enter.

...

### Vad som faktiskt händer

Listan är tom. Alla studenter borta.

```
=== Lista på studenter ===

```

**Varför?** SQL:en som körs mot databasen ser ut så här:

```sql
INSERT INTO Studenter (Namn) VALUES ('Robert'); DELETE FROM Studenter; --')
```

Det är tre separata SQL-satser i ett enda anrop:

1. `INSERT INTO Studenter (Namn) VALUES ('Robert')` — lägger till Robert
2. `DELETE FROM Studenter` — raderar hela tabellen
3. `--')` — kommenterar bort resten

Ingen backup. Trevlig måndag.

---

## Steg 4 — Fixa det med parametriserade queries

Ersätt INSERT-raden med en parametriserad query:

```csharp
// Steg 4: Samma loop — nu med parametriserad query
var insert = connection.CreateCommand();
insert.CommandText = "INSERT INTO Studenter (Namn) VALUES (@namn)";
insert.Parameters.AddWithValue("@namn", input);
insert.ExecuteNonQuery();
```

Skapa en ny databas (byt namn i connection string till `skolan2.db`), kör loopen igen med Anna, Bo, Kalle — och testa sedan Bobby Tables-inputen en gång till.

### Vad händer nu?

Listan uppdateras och du ser:

```
=== Lista på studenter ===

    1. Anna
    2. Bo
    3. Kalle
    4. Robert'); DELETE FROM Studenter; --
```

Tabellen lever. Bobby Tables är bara ett namn i databasen.

**Varför?** Parametern `@namn` skickas som *data* — aldrig som SQL-kod. Databasen läser `'); DELETE FROM Studenter; --` som en textsträng och inget annat.

---

## Reflektionsfrågor

- 💡 Varför fungerar attacken bara med strängkonkatenering och inte med parametrar?
- 💡 Vad är `--` i SQL?
- 💡 Förutom DELETE — vad annat kan en angripare göra med SQL injection?
- 💡 Varför räcker det inte att "kontrollera att inputen ser okej ut" som skydd?
