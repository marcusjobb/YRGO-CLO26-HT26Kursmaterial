# SQLite i C# — livekodning 2026-10-07

**Verktyg:** .NET 10, NuGet-paketet `Microsoft.Data.Sqlite`

Hittills har vi pratat med databasen via DB Browser. Nu låter vi ett C#-program göra jobbet: skapa en databas, fylla den med data och ställa frågor.

Koden bygger på Microsofts eget exempel ([dokumentationen](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=net-cli) och [HelloWorldSample på GitHub](https://github.com/dotnet/docs/tree/main/samples/snippets/standard/data/sqlite/HelloWorldSample)). Deras version är ganska kompakt, så här tar vi den bit för bit.

> **Nästa steg:** i [SQLite-handler](2026-10-07_sqlite_handler.md) flyttar vi samma kod in i en egen klass, så att `Program.cs` blir mycket kortare.

## 1. Skapa projektet

Vi döper alla databasprojekt till `SQLite_...`. Det här heter `SQLite_Live`. Det är en vanlig konsolapplikation, och du kan skapa den på tre sätt.

**I terminalen:**

```bash
dotnet new console -n SQLite_Live
cd SQLite_Live
```

**I Visual Studio:**
*Create a new project* → välj **Console App** (C#) → döp projektet till `SQLite_Live` → välj **.NET 10** → *Create*.

**I Rider:**
*New Solution* → välj **Console Application** under .NET → döp projektet till `SQLite_Live` → välj **net10.0** → *Create*.

## 2. Lägg till NuGet-paketet

.NET kan inte prata med SQLite direkt. Vi behöver ett **NuGet-paket**, alltså ett färdigt kodbibliotek som någon annan har skrivit och publicerat på [nuget.org](https://www.nuget.org/packages/Microsoft.Data.Sqlite). Paketet vi behöver heter `Microsoft.Data.Sqlite` och kommer från Microsoft.

**I terminalen** (stå i projektmappen):

```bash
dotnet add package Microsoft.Data.Sqlite
```

**I Visual Studio:**
Högerklicka på projektet → *Manage NuGet Packages...* → fliken **Browse** → sök på `Microsoft.Data.Sqlite` → *Install*.

**I Rider:**
Högerklicka på projektet → *Manage NuGet Packages* → sök på `Microsoft.Data.Sqlite` → klicka på **+** vid paketet.

Alla tre sätten gör samma sak: de skriver en rad i projektfilen. Den tittar vi på härnäst.

## 3. Projektfilen (`.csproj`)

Öppna `SQLite_Live.csproj`. I Visual Studio dubbelklickar du på projektnamnet, och i Rider högerklickar du och väljer *Edit* → *Edit 'SQLite_Live.csproj'*.

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

Projektfilen är en XML-fil som talar om för .NET hur projektet ska byggas. Den har två delar.

**`<PropertyGroup>`** innehåller inställningar för projektet:

| Inställning | Betyder |
|---|---|
| `OutputType` = `Exe` | Det blir ett program du kan köra (inte ett bibliotek). |
| `TargetFramework` = `net10.0` | Vi bygger för .NET 10. |
| `ImplicitUsings` = `enable` | Vanliga `using` (som `System` och `System.IO`) läggs till automatiskt. |
| `Nullable` = `enable` | Kompilatorn varnar om något kan vara `null` när du inte har räknat med det. |

**`<ItemGroup>`** är en lista över saker projektet *innehåller* eller *behöver*. Här finns en enda sak, nämligen en `PackageReference`:

```xml
<PackageReference Include="Microsoft.Data.Sqlite" Version="10.0.12" />
```

- `PackageReference` betyder att projektet behöver ett NuGet-paket.
- `Include` anger vilket paket det gäller.
- `Version` anger vilken version. Du kan få ett högre nummer än 10.0.12 om det har kommit en nyare version, och det gör inget.

Nästa gång du bygger hämtar .NET paketet från nuget.org (det kallas *restore*). Du kan alltså skriva raden för hand i stället för att använda menyerna. Resultatet blir detsamma.

### Paketen som följer med

`Microsoft.Data.Sqlite` drar automatiskt med sig några paket till. De kallas **transitiva beroenden**. Du kan se dem med:

```bash
dotnet list package --include-transitive
```

```
   Top-level Package            Requested   Resolved
   > Microsoft.Data.Sqlite      10.0.12     10.0.12

   Transitive Package                     Resolved
   > Microsoft.Data.Sqlite.Core           10.0.12
   > SQLitePCLRaw.bundle_e_sqlite3        2.1.12
   > SQLitePCLRaw.lib.e_sqlite3           2.1.12
   > SQLitePCLRaw.provider.e_sqlite3      2.1.12
```

- **`Microsoft.Data.Sqlite.Core`** innehåller C#-klasserna vi använder: `SqliteConnection`, `SqliteCommand` och `SqliteDataReader`.
- **`SQLitePCLRaw.*`** är bryggan ner till själva SQLite-motorn, som är skriven i C. `lib.e_sqlite3` innehåller den kompilerade SQLite-motorn för Windows, Mac och Linux, så du behöver inte installera SQLite separat.

> **Tips:** under lektionen lade vi även till `SQLitePCLRaw.core` för hand. Det behövs inte, eftersom paketet redan följer med. Har du den raden i din `.csproj` kan du ta bort den.

## 4. Huvudprogrammet

Nu till `Program.cs`. Microsofts exempel använder en klassisk `Main`-metod och delar upp programmet i två delar:

```csharp
using Microsoft.Data.Sqlite;

namespace HelloWorldSample
{
    class Program
    {
        static void Main()
        {
            CreateAndSeed(); // Skapar databas och fyller den med data
            Query();         // Ställer frågor

            // Städa upp
            SqliteConnection.ClearAllPools(); // Släpp alla öppna kopplingar
            File.Delete("hello.db");          // Ta bort databasfilen
        }
```

`using Microsoft.Data.Sqlite;` ger oss tillgång till klasserna från NuGet-paketet.

`Main` gör tre saker: skapar och fyller databasen, ställer en fråga och städar upp efter sig.

Städningen behövs för att exemplet ska gå att köra igen och igen. .NET håller kopplingar till databasen öppna i en *pool* för att det ska gå snabbare nästa gång. `ClearAllPools()` stänger dem, och annars är filen låst och `File.Delete` misslyckas. Kommentera bort `File.Delete` om du vill öppna `hello.db` i DB Browser efteråt. Filen ligger i `bin/Debug/net10.0/`.

## 5. Öppna en koppling

```csharp
        static void CreateAndSeed()
        {
            using var connection = new SqliteConnection("Data Source=hello.db");
            connection.Open();
```

En **connection** (koppling) är vår linje till databasen. Strängen `"Data Source=hello.db"` kallas *connection string* och anger vilken fil vi vill använda. Finns filen inte så skapar SQLite den.

`using var` betyder att kopplingen stängs automatiskt när metoden är klar. Det slipper vi alltså komma ihåg själva.

## 6. Skapa tabellen och lägg in data

```csharp
            var command = connection.CreateCommand();
            command.CommandText = """
                CREATE TABLE user (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL
                );

                INSERT INTO user
                VALUES (1, 'Brice'),
                       (2, 'Alexander'),
                       (3, 'Nate');
            """;
            int svar = command.ExecuteNonQuery(); // Detta är ingen fråga
            Console.WriteLine(svar); // 3 = 3 nya rader i tabellen
```

En **command** är en SQL-sats som vi vill köra. Vi får den från kopplingen och lägger SQL-koden i `CommandText`.

SQL-koden är samma som du har skrivit i DB Browser. De tre citattecknen `"""` är en *raw string literal*, och med den kan vi skriva SQL på flera rader utan krångel.

`ExecuteNonQuery()` används när vi *gör* något (CREATE, INSERT, UPDATE, DELETE) och inte *frågar* efter något. Metoden svarar med hur många rader som påverkades, i det här fallet 3.

## 7. Lägg till en rad från användaren, med parameter

```csharp
            Console.Write("Name: ");
            var name = Console.ReadLine();

            command.CommandText = "INSERT INTO user (name) VALUES ($name)";
            command.Parameters.AddWithValue("$name", name);
            command.ExecuteNonQuery();
```

Nu ska användaren skriva in ett namn. Lägg märke till att vi **inte** klistrar in namnet direkt i SQL-strängen. Vi skriver `$name` som en platshållare och skickar värdet separat med `Parameters.AddWithValue`.

Varför? Om vi hade byggt strängen själva, till exempel `"... VALUES ('" + name + "')"`, så kunde användaren skriva in egen SQL-kod. Det kallas **SQL injection**. Med parametrar behandlas värdet alltid som data och aldrig som kod.

Vi skickar inte med något `id`. Kolumnen har `AUTOINCREMENT`, så databasen väljer nästa lediga nummer.

## 8. Vilket id fick vi?

```csharp
            command.CommandText = "SELECT last_insert_rowid()";
            var newId = (long)command.ExecuteScalar()!;

            Console.WriteLine($"Your new user ID is {newId}.");
        }
```

`last_insert_rowid()` är en SQLite-funktion som returnerar id:t för raden vi nyss lade in.

`ExecuteScalar()` används när svaret är **ett enda värde**. Det kommer tillbaka som `object?`, så vi gör om det till `long` (SQLite lagrar heltal som 64 bitar). Utropstecknet `!` säger till kompilatorn att vi vet att svaret inte är `null`.

## 9. Ställ en fråga och läs svaret

```csharp
        static void Query()
        {
            Console.Write("User ID: ");
            var line = Console.ReadLine();
            if (line is null)
            {
                return;
            }

            var id = int.Parse(line);
```

Först frågar vi användaren efter ett id. Om inmatningen tar slut (`null`) avbryter vi.

```csharp
            using var connection = new SqliteConnection("Data Source=hello.db");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = """
                SELECT name
                FROM user
                WHERE id = $id
            """;
            command.Parameters.AddWithValue("$id", id);
```

Vi öppnar en ny koppling till samma fil och skriver en `SELECT` med en parameter, på samma sätt som i steg 7.

```csharp
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var name = reader.GetString(0);

                Console.WriteLine($"Hello, {name}!");
            }
        }
    }
}
```

`ExecuteReader()` används när vi förväntar oss **rader** tillbaka. Vi får en *reader* som vi går igenom rad för rad:

- `reader.Read()` flyttar fram till nästa rad och svarar `false` när raderna är slut.
- `reader.GetString(0)` hämtar kolumn nummer 0 (den första i vår `SELECT`) som en sträng.

## Sammanfattning: tre sätt att köra SQL

| Metod | När | Svarar med |
|---|---|---|
| `ExecuteNonQuery()` | CREATE, INSERT, UPDATE, DELETE | antal påverkade rader |
| `ExecuteScalar()` | ett enda värde, t.ex. `last_insert_rowid()` eller `COUNT(*)` | `object?` |
| `ExecuteReader()` | SELECT som ger rader | en reader att loopa igenom |

## Kör programmet

```
3
Name: Ada
Your new user ID is 4.
User ID: 4
Hello, Ada!
```

Allt ligger i `Program.cs` än så länge, och det blir rörigt när programmet växer. I nästa exempel, [SQLite-handler](2026-10-07_sqlite_handler.md), samlar vi databaskoden i en egen klass.
