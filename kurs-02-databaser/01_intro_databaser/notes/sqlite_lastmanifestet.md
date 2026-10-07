# Lastmanifestet — klasser till databasen

**Verktyg:** .NET 10, NuGet-paketet `Microsoft.Data.Sqlite`

Besättningen på **Kometen** är tillbaka. Den här gången får de lära sig varför man skickar **objekt** till databasen i stället för en lång rad lösa värden.

> Det här bygger på [SQLite-handler](../examples/2026-10-07_sqlite_handler.md), där vi samlade databaskoden i en egen klass. Figurerna känner du igen från [SQL från C# — en rymdmanga](sql_fran_csharp_rymdmanga.md).

| | | |
|---|---|---|
| ![Tetsu](manga/tetsu.jpg) | **Inspektör Tetsu** | tullinspektör, har aldrig blivit imponerad |
| ![Sora](manga/sora.jpg) | **Kapten Sora** | fattar besluten |
| ![Kenta](manga/kenta.jpg) | **Kenta** | ingenjör, skriver koden |
| ![Hoshi](manga/hoshi.jpg) | **Doktor Hoshi** | analytiker, ser felen först |
| ![Mamo](manga/mamo.jpg) | **Mamo** | handlare, äger förvånansvärt mycket av lasten |

## Kapitel 1 — Tullen

![Tetsu](manga/tetsu.jpg)

**Tetsu:** Fraktskeppet Kometen. Ert lastmanifest, tack. Varje låda: namn, vikt, ägare och pris.

![Sora](manga/sora.jpg)

**Sora:** Kenta! Vi behöver ett lastmanifest. I en databas. Nu!

## Kapitel 2 — Projektet

![Kenta](manga/kenta.jpg)

**Kenta:** Jag startar ett nytt projekt!

Skapa en konsolapplikation som heter `SQLite_Lastmanifest`.

**I terminalen:**

```bash
dotnet new console -n SQLite_Lastmanifest
cd SQLite_Lastmanifest
dotnet add package Microsoft.Data.Sqlite
```

**I Visual Studio:**
*Create a new project* → **Console App** (C#) → döp projektet till `SQLite_Lastmanifest` → **.NET 10** → *Create*. Högerklicka sedan på projektet → *Manage NuGet Packages...* → **Browse** → `Microsoft.Data.Sqlite` → *Install*.

**I Rider:**
*New Solution* → **Console Application** → döp projektet till `SQLite_Lastmanifest` → **net10.0** → *Create*. Högerklicka sedan på projektet → *Manage NuGet Packages* → `Microsoft.Data.Sqlite` → **+**.

Projektfilen får samma `<ItemGroup>` med `Microsoft.Data.Sqlite` som förut. Vad raderna betyder står i [SQLite i C#, avsnitt 3](../examples/2026-10-07_sqlite_i_csharp.md#3-projektfilen-csproj).

## Kapitel 3 — Lösa värden

![Kenta](manga/kenta.jpg)

**Kenta:** Lätt! Jag gör som i SQLite-handlern. En metod som tar namn, vikt, ägare och pris:

```csharp
public long AddCargo(string name, int weight, string owner, int price)
```

```csharp
cargoHandler.AddCargo("Hyperdriftskristall", 12, "Mamo", 500);
cargoHandler.AddCargo("Snabbnudlar (12-pack)", 8, "Mamo", 180);
```

![Tetsu](manga/tetsu.jpg)

**Tetsu:** *höjer ett ögonbryn* Snabbnudlar som väger 8 kilo och kostar 180 credits?

![Hoshi](manga/hoshi.jpg)

**Hoshi:** *justerar glasögonen* Kenta. Du har bytt plats på vikt och pris.

Både `weight` och `price` är `int`, så kompilatorn kan inte se att de har bytt plats. Programmet kör, databasen sparar och ingen säger något, men datan är fel.

Ju fler parametrar en metod har, desto lättare är det att blanda ihop dem. Det blir också svårt att läsa anropet: vad betyder `8` och vad betyder `180`?

![Hoshi](manga/hoshi.jpg)

**Hoshi:** Varje låda är en sak med egenskaper. Ge den en klass.

## Kapitel 4 — Klassen `Cargo`

Lägg till en ny klass i projektet, `Cargo.cs`. Högerklicka på projektet → *Add* → *Class*.

```csharp
namespace SQLite_Lastmanifest;

internal class Cargo
{
    public long Id { get; set; }
    public string Name { get; set; } = "";
    public int Weight { get; set; } // kg
    public string Owner { get; set; } = "";
    public int Price { get; set; }  // credits
}
```

Klassen har en property för varje kolumn i databastabellen. Det är ingen slump: **en rad i tabellen motsvarar ett objekt i C#**.

- `Id` är `long`, eftersom SQLite lagrar heltal som 64 bitar. Den sätts av databasen, inte av oss.
- `= ""` ger strängarna ett startvärde, så att de inte är `null`.
- Kommentarerna `// kg` och `// credits` talar om vilken enhet talen har. Det är exakt den informationen som saknades när Kenta skickade in lösa tal.

## Kapitel 5 — `CargoHandler`

Nu bygger vi handlern. Lägg till en ny klass, `CargoHandler.cs`.

```csharp
using Microsoft.Data.Sqlite;

namespace SQLite_Lastmanifest;

internal class CargoHandler
{
    public string DatabaseFile { get; set; } = "kometen.db";

    private SqliteConnection? connection;

    public void OpenConnection()
    {
        connection = new SqliteConnection($"Data Source={DatabaseFile}");
        connection.Open();
    }
```

Början är densamma som i [SQLite-handler](../examples/2026-10-07_sqlite_handler.md#3-klassen-och-kopplingen): en property för filnamnet och en koppling som öppnas en gång och sparas i ett fält.

### Tabellen

```csharp
    public void CreateTable()
    {
        using var command = connection!.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS cargo (
                id     INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                name   TEXT    NOT NULL,
                weight INTEGER NOT NULL,
                owner  TEXT    NOT NULL,
                price  INTEGER NOT NULL
            );
            """;
        command.ExecuteNonQuery();
    }
```

Förra gången skickade `Program.cs` in SQL-koden till `CreateTable`. Nu vet `CargoHandler` själv hur tabellen ska se ut. Det är *handlerns* jobb att känna till databasen, inte `Program.cs`.

Jämför kolumnerna med klassen `Cargo`. Det är samma fem, i samma ordning.

### Spara ett objekt

![Kenta](manga/kenta.jpg)

**Kenta:** Okej, okej. Metoden tar en hel låda i stället.

```csharp
    public long AddCargo(Cargo cargo)
    {
        using var command = connection!.CreateCommand();
        command.CommandText = """
            INSERT INTO cargo (name, weight, owner, price)
            VALUES ($name, $weight, $owner, $price)
            """;
        command.Parameters.AddWithValue("$name", cargo.Name);
        command.Parameters.AddWithValue("$weight", cargo.Weight);
        command.Parameters.AddWithValue("$owner", cargo.Owner);
        command.Parameters.AddWithValue("$price", cargo.Price);
        command.ExecuteNonQuery();

        command.CommandText = "SELECT last_insert_rowid()";
        cargo.Id = (long)command.ExecuteScalar()!;
        return cargo.Id;
    }
```

Metoden har nu **en** parameter i stället för fyra. Varje värde hämtas från objektet med namn: `cargo.Weight` är vikten och `cargo.Price` är priset. Här går det inte att blanda ihop dem.

Precis som förut skickar vi värdena som parametrar (`$name`, `$weight` och så vidare), aldrig inklistrade direkt i SQL-strängen.

Det id databasen ger lådan sparar vi direkt i objektet med `cargo.Id = ...`. Efter anropet vet alltså objektet själv vilket id det har.

### Läsa tillbaka objekt

```csharp
    public List<Cargo> GetAllCargo()
    {
        using var command = connection!.CreateCommand();
        command.CommandText = "SELECT id, name, weight, owner, price FROM cargo";

        using var reader = command.ExecuteReader();

        var manifest = new List<Cargo>();
        while (reader.Read())
        {
            var cargo = new Cargo
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1),
                Weight = reader.GetInt32(2),
                Owner = reader.GetString(3),
                Price = reader.GetInt32(4)
            };
            manifest.Add(cargo);
        }
        return manifest;
    }
}
```

Det här är vägen tillbaka: **varje rad blir ett nytt `Cargo`-objekt**, och alla objekt samlas i en `List<Cargo>`.

- Siffran i `GetInt64(0)`, `GetString(1)` och så vidare är kolumnens plats i `SELECT`. Därför skriver vi ut kolumnerna i `SELECT` i stället för `*`. Då vet vi exakt i vilken ordning de kommer.
- Läsmetoden måste matcha typen: `GetInt64` för `long`, `GetInt32` för `int` och `GetString` för `string`.
- Metoden returnerar listan. `CargoHandler` skriver aldrig ut något själv.

## Kapitel 6 — Det nya manifestet

![Kenta](manga/kenta.jpg)

**Kenta:** Andra försöket!

```csharp
using SQLite_Lastmanifest;

CargoHandler cargoHandler = new();
cargoHandler.OpenConnection();
cargoHandler.CreateTable();

Cargo crystal = new()
{
    Name = "Hyperdriftskristall",
    Weight = 12,
    Owner = "Mamo",
    Price = 500
};
cargoHandler.AddCargo(crystal);
```

Objektet skapas med en *object initializer*, alltså klamrarna efter `new()`. Varje värde står bredvid sitt namn, så du ser direkt att 12 är vikten och 500 är priset.

```csharp
Cargo noodles = new()
{
    Name = "Snabbnudlar (12-pack)",
    Weight = 180,
    Owner = "Mamo",
    Price = 8
};
cargoHandler.AddCargo(noodles);

Console.WriteLine($"Nudlarna fick id {noodles.Id}");
```

Efter `AddCargo` har `noodles.Id` fått ett värde. Det satte handlern dit.

```csharp
foreach (Cargo cargo in cargoHandler.GetAllCargo())
{
    Console.WriteLine($"{cargo.Id}: {cargo.Name}, {cargo.Weight} kg, {cargo.Owner}, {cargo.Price} cr");
}
```

`GetAllCargo()` ger en lista med objekt, och vi loopar igenom den som vilken lista som helst. Det är `Program.cs` som bestämmer hur utskriften ska se ut.

## Kör programmet

```
Nudlarna fick id 2
1: Hyperdriftskristall, 12 kg, Mamo, 500 cr
2: Snabbnudlar (12-pack), 180 kg, Mamo, 8 cr
```

![Tetsu](manga/tetsu.jpg)

**Tetsu:** *läser* Rätt fält, rätt enheter. Manifestet är godkänt.

![Tetsu](manga/tetsu.jpg)

**Tetsu:** ...men varför väger tolv paket snabbnudlar 180 kilo?

![Mamo](manga/mamo.jpg)

**Mamo:** *redan på väg ut genom luftslussen* Fufufu...

> Kör du programmet igen läggs lådorna in en gång till med id 3 och 4, eftersom `kometen.db` finns kvar mellan körningarna. Det känner du igen från [SQLite-handler](../examples/2026-10-07_sqlite_handler.md#kör-programmet).

## Före och efter

| | Lösa värden | Med klassen `Cargo` |
|---|---|---|
| Anrop | `AddCargo("Snabbnudlar", 8, "Mamo", 180)` | `AddCargo(noodles)` |
| Byter du plats på vikt och pris | märks det inte | går det inte, eftersom varje värde har ett namn |
| Läsa tillbaka | lösa värden ur en reader | en `List<Cargo>` |
| En rad i tabellen | blir ingenting särskilt i C# | blir ett objekt |
