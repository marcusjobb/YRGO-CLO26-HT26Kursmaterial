# SQLite-handler — livekodning 2026-10-07

**Verktyg:** .NET 10, NuGet-paketet `Microsoft.Data.Sqlite`

I [SQLite i C#](2026-10-07_sqlite_i_csharp.md) låg all databaskod i `Program.cs`. Det fungerar för ett litet exempel men blir snabbt rörigt. Nu flyttar vi databaskoden till en egen klass, `SQLiteHandler`. Då behöver `Program.cs` bara säga *vad* som ska hända, medan klassen vet *hur* det görs.

> Läs gärna [SQLite i C#](2026-10-07_sqlite_i_csharp.md) först. Där förklaras `SqliteConnection`, `ExecuteNonQuery`, `ExecuteScalar`, `ExecuteReader` och parametrar steg för steg. Här använder vi dem utan att förklara allt igen.

## 1. Skapa projektet

Precis som förra gången skapar vi en konsolapplikation, den här gången med namnet `SQLite_Handler`.

**I terminalen:**

```bash
dotnet new console -n SQLite_Handler
cd SQLite_Handler
dotnet add package Microsoft.Data.Sqlite
```

**I Visual Studio:**
*Create a new project* → **Console App** (C#) → döp projektet till `SQLite_Handler` → **.NET 10** → *Create*. Högerklicka sedan på projektet → *Manage NuGet Packages...* → **Browse** → `Microsoft.Data.Sqlite` → *Install*.

**I Rider:**
*New Solution* → **Console Application** → döp projektet till `SQLite_Handler` → **net10.0** → *Create*. Högerklicka sedan på projektet → *Manage NuGet Packages* → `Microsoft.Data.Sqlite` → **+**.

> **Varför `SQLite_Handler` och inte `SQLiteHandler`?** Under lektionen döpte vi projektet till samma sak som klassen. Projektnamnet blir också namespace, så klassen hette `SQLiteHandler.SQLiteHandler` och vi var tvungna att skriva det dubbla namnet i `Program.cs`. Ge projektet och klassen olika namn så slipper du det.

## 2. Projektfilen

`SQLite_Handler.csproj` ser ut precis som i förra exemplet:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Data.Sqlite" Version="10.0.12" />
  </ItemGroup>

</Project>
```

`<PropertyGroup>` innehåller projektets inställningar, och `<ItemGroup>` listar NuGet-paketen projektet behöver. Vad varje rad betyder och vilka paket som följer med automatiskt står i [SQLite i C#, avsnitt 3](2026-10-07_sqlite_i_csharp.md#3-projektfilen-csproj).

## 3. Klassen och kopplingen

Skapa en ny fil, `SQLiteHandler.cs`. Högerklicka på projektet → *Add* → *Class*.

```csharp
using Microsoft.Data.Sqlite;

namespace SQLite_Handler;

internal class SQLiteHandler
{
    public string DatabaseFile { get; set; } = "hello.db";

    private SqliteConnection? connection;

    public void OpenConnection()
    {
        connection = new SqliteConnection($"Data Source={DatabaseFile}");
        connection.Open();
    }
```

Lägg märke till skillnaden mot förra exemplet. Där öppnade varje metod en egen koppling. Här öppnas kopplingen **en gång** och sparas i fältet `connection`, så att alla metoder i klassen kan använda den.

- `DatabaseFile` är en property med standardvärdet `"hello.db"`. Vill du använda en annan fil kan du ändra den innan du anropar `OpenConnection()`.
- `SqliteConnection?` har ett frågetecken eftersom fältet är `null` tills `OpenConnection()` har körts.
- `namespace SQLite_Handler;` (med semikolon) är ett *file-scoped namespace*. Det betyder samma sak som `namespace SQLite_Handler { ... }`, men vi slipper en nivå med måsvingar.

## 4. Skapa en tabell

```csharp
    public void CreateTable(string sql)
    {
        using var command = connection!.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
```

Metoden tar emot en `CREATE TABLE`-sats och kör den. Det här är samma `ExecuteNonQuery()` som förut, men nu inpackad i en metod.

`connection!` betyder "jag lovar att kopplingen är öppen". Glömmer du att anropa `OpenConnection()` först så kraschar programmet här.

## 5. Lägg till ett namn

```csharp
    public long AddName(string name)
    {
        using var command = connection!.CreateCommand();
        command.CommandText = "INSERT INTO user (name) VALUES ($name)";
        command.Parameters.AddWithValue("$name", name);
        command.ExecuteNonQuery(); // Kör inmatning

        command.CommandText = "SELECT last_insert_rowid()";
        return (long)command.ExecuteScalar()!;
    }
```

Det här är steg 7 och 8 från förra exemplet, ihopslagna till en metod. Vi lägger in namnet med en parameter (skydd mot SQL injection) och **returnerar** sedan det nya id:t i stället för att skriva ut det. Det är upp till den som anropar metoden att bestämma vad som ska hända med id:t.

## 6. Hitta en användare med id

```csharp
    public string FindUserById(int id)
    {
        using var command = connection!.CreateCommand();
        command.CommandText = """
            SELECT name
            FROM user
            WHERE id = $id
        """;
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();

        string name = "";
        while (reader.Read())
        {
            name = reader.GetString(0);
        }
        return name;
    }
```

Det här är `Query()` från förra exemplet. Skillnaden är att metoden **returnerar** namnet i stället för att skriva ut det med `Console.WriteLine`.

Det är en viktig princip: klassen pratar med databasen och `Program.cs` pratar med användaren. Om ingen användare hittas returneras en tom sträng.

## 7. Hitta en användare med namn

```csharp
    public string FindUserByName(string name)
    {
        using var command = connection!.CreateCommand();
        command.CommandText = """
            SELECT id
            FROM user
            WHERE name = $name
        """;
        command.Parameters.AddWithValue("$name", name);

        using var reader = command.ExecuteReader();

        long id = 0;
        while (reader.Read())
        {
            id = reader.GetInt64(0);
        }

        return $"{id} {name}";
    }
}
```

Det här är samma mönster, fast åt andra hållet: vi söker på namn och får tillbaka id.

Eftersom `id` är ett heltal i databasen läser vi det med `GetInt64` (64-bitars heltal, alltså `long`). Under lektionen skrev vi `long.Parse(reader.GetString(0))`. Det fungerar också, men det tar en omväg via en sträng.

Det kan finnas flera användare med samma namn. Loopen skriver över `id` för varje rad, så det är den **sista** träffen som returneras.

## 8. Program.cs

Nu blir huvudprogrammet kort och lättläst:

```csharp
using SQLite_Handler;

SQLiteHandler sqlite = new();

sqlite.OpenConnection();
sqlite.CreateTable("""
    CREATE TABLE IF NOT EXISTS user (
        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
        name TEXT NOT NULL
    );
    """);

sqlite.AddName("Brice");
sqlite.AddName("Alexander");
sqlite.AddName("Nate");

Console.WriteLine(sqlite.FindUserById(2));
Console.WriteLine(sqlite.FindUserByName("Nate"));
```

`using SQLite_Handler;` gör att vi kan skriva bara `SQLiteHandler` i stället för hela namnet `SQLite_Handler.SQLiteHandler`.

`IF NOT EXISTS` gör att `CREATE TABLE` inte kraschar om tabellen redan finns. Det behövs, eftersom vi den här gången **inte** tar bort databasfilen när programmet är klart.

## Kör programmet

Första gången:

```
Alexander
3 Nate
```

Kör programmet igen:

```
Alexander
6 Nate
```

Varför blev det 6? Databasfilen finns kvar sedan förra körningen, så de tre namnen läggs in en gång till och får id 4, 5 och 6. Nu finns det två "Nate", och `FindUserByName` returnerar den sista. Öppna `bin/Debug/net10.0/hello.db` i DB Browser så ser du alla rader.

## Före och efter

| | [SQLite i C#](2026-10-07_sqlite_i_csharp.md) | SQLite-handler |
|---|---|---|
| Var finns SQL-koden? | i `Program.cs` | i klassen `SQLiteHandler` |
| Koppling | en ny i varje metod | en, sparad i ett fält |
| Utskrift | databaskoden skriver själv till konsolen | metoderna returnerar värden, och `Program.cs` skriver ut |
| Databasfilen | tas bort när programmet är klart | finns kvar mellan körningarna |
