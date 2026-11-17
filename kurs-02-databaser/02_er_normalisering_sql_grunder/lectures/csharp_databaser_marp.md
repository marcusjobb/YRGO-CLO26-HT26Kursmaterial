---
marp: true
theme: nion_dark
paginate: true
title: C# och databaser — från SQL till kod
description: Koppla SQLite till C# med ADO.NET — connection, parametrar och Execute-metoderna
---

<!-- _class: lead -->
<!-- _paginate: false -->

# C# och databaser

Från SQL till kod — och tillbaka igen

---

## Tabell och klass — samma sak, olika språk

| I databasen | I C# |
|---|---|
| tabell | klass |
| kolumn | property |
| rad | objekt |
| alla rader | `List<Game>` |
| `NOT NULL` | `required` / icke-nullable typ |
| `NULL` tillåtet | `?` efter typen |
| `DEFAULT 0` | `= 0` i klassen |

> En `SELECT` hämtar rader. Du omvandlar dem till objekt.

📖 [Från tabell till klass](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql/tabeller-och-klasser.md)

---

## Databasen som klass

```sql
CREATE TABLE Games (
    GameId  INTEGER PRIMARY KEY,
    Title   TEXT    NOT NULL,
    Genre   TEXT,
    Price   REAL    NOT NULL
);
```

```csharp
public class Game
{
    public int GameId { get; set; }
    public required string Title { get; set; }
    public string? Genre { get; set; }
    public decimal Price { get; set; }
}
```

> Varje kolumn → en property. `NOT NULL` → ingen `?`. `NULL` tillåtet → `?`.

---

## Steg 1 — NuGet-paket

```bash
dotnet add package Microsoft.Data.Sqlite
```

**appsettings.json** — gitignoreras:

```json
{ "ConnectionStrings": { "DefaultConnection": "Data Source=spelbutik.db" } }
```

**appsettings.example.json** — committas:

```json
{ "ConnectionStrings": { "DefaultConnection": "Data Source=DIN-DATABAS.db" } }
```

**.gitignore** — lägg till:

```
appsettings.json
```

📖 [NuGet-paket och connection strings](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/ado_quicksheet.md)

---

## Steg 2 — anslut

```csharp
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("DefaultConnection")!;

using var connection = new SqliteConnection(connectionString);
connection.Open();

Console.WriteLine("Ansluten till: " + connection.Database);
```

> `using var` stänger anslutningen automatiskt — även om något går fel.

---

## ⚠️ Parametrar — inte strängkonkatenering

```csharp
// ❌ SQL injection — gör aldrig såhär
string sql = "SELECT * FROM Games WHERE Genre = '" + genre + "'";

// ✅ Parametrar separerar data från SQL-kod
string sql = "SELECT * FROM Games WHERE Genre = @genre";
cmd.Parameters.AddWithValue("@genre", genre);
```

En parameter skickas **separat** från SQL-texten.
Databasen behandlar den alltid som data — aldrig som kod.

---

## INSERT, UPDATE, DELETE → ExecuteNonQuery

Allt som **inte returnerar rader**:

```csharp
string sql = "INSERT INTO Games (Title, Genre, Price) VALUES (@title, @genre, @price)";
using var cmd = new SqliteCommand(sql, connection);
cmd.Parameters.AddWithValue("@title", "Hollow Knight");
cmd.Parameters.AddWithValue("@genre", "Action");
cmd.Parameters.AddWithValue("@price", 69.00m);

int rows = cmd.ExecuteNonQuery();
Console.WriteLine($"{rows} rad(er) lades till.");
```

> `0` rader = inget ändrades — inte ett fel, men värt att notera.

---

## SELECT ett värde → ExecuteScalar

Perfekt för `COUNT`, `MAX`, `SUM`:

```csharp
string sql = "SELECT COUNT(*) FROM Games WHERE Genre = @genre";
using var cmd = new SqliteCommand(sql, connection);
cmd.Parameters.AddWithValue("@genre", "RPG");

int count = Convert.ToInt32(cmd.ExecuteScalar());
Console.WriteLine($"Antal RPG-spel: {count}");
```

> Resultatet är `object?` — casta alltid med `Convert.ToInt32`, `ToString()` o.s.v.

---

## SELECT många rader → ExecuteReader

```csharp
string sql = "SELECT GameId, Title, Price FROM Games WHERE Price < @max";
using var cmd = new SqliteCommand(sql, connection);
cmd.Parameters.AddWithValue("@max", 200m);
using var reader = cmd.ExecuteReader();

while (reader.Read())
{
    int id       = reader.GetInt32("GameId");
    string title = reader.GetString("Title");
    decimal pris = reader.GetDecimal("Price");
    Console.WriteLine($"{id}: {title} — {pris} kr");
}
```

> `reader.Read()` returnerar `true` för varje rad. `false` = klart.

---

## Från reader till lista

```csharp
var games = new List<Game>();

while (reader.Read())
{
    games.Add(new Game
    {
        GameId = reader.GetInt32("GameId"),
        Title  = reader.GetString("Title"),
        Genre  = reader.IsDBNull("Genre") ? null : reader.GetString("Genre"),
        Price  = reader.GetDecimal("Price")
    });
}
```

> Kolumner som kan vara `NULL` kollas med `reader.IsDBNull("kolumn")` innan du läser.

---

## Flödet i ett svep

```
appsettings.json
      ↓
SqliteConnection.Open()
      ↓
INSERT / UPDATE / DELETE   → ExecuteNonQuery  → int   (antal rader)
SELECT COUNT(*)            → ExecuteScalar    → object? → Convert
SELECT * FROM ...          → ExecuteReader    → rad för rad → List<Game>
```

---

<!-- _class: lead -->
<!-- _paginate: false -->

# Nu kör ni

Koppla spelbutiksdatabasen till C#.
SELECT alla spel, INSERT ett nytt, visa en enkel meny.

---

## Resurser

| Ämne | Fil |
|---|---|
| NuGet-paket och connection strings | [ado_quicksheet.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/ado_quicksheet.md) |
| ADO.NET-objekt — Command, Reader, Transaction | [ado_objects_quicksheet.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/ado_objects_quicksheet.md) |
| Tabell till klass — SQL och C# sida vid sida | [tabeller-och-klasser.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql/tabeller-och-klasser.md) |
| SQL från C# — en rymdmanga | [sql_fran_csharp_rymdmanga.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql_fran_csharp_rymdmanga.md) |
