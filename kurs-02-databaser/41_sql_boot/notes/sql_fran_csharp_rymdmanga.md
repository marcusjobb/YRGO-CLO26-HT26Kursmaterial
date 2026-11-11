# SQL från C# till databasen — en rymdmanga

Det finns två sätt att prata med en databas från C#. Antingen *skickar* du något (skapa, ändra, radera) och bryr dig inte om svaret, eller så *frågar* du efter data och vill ha den tillbaka.

Besättningen på fraktskeppet **Kometen** visar hur båda sätten fungerar.

| | | |
|---|---|---|
| ![Sora](../../01_intro_databaser/notes/manga/sora.jpg) | **Kapten Sora** | fattar besluten |
| ![Yuki](../../01_intro_databaser/notes/manga/yuki.jpg) | **Yuki** | kommunikation, öppnar och stänger kanaler |
| ![Kenta](../../01_intro_databaser/notes/manga/kenta.jpg) | **Kenta** | ingenjör, bygger kommandona |
| ![Hoshi](../../01_intro_databaser/notes/manga/hoshi.jpg) | **Doktor Hoshi** | analytiker, tar emot och kontrollerar data |
| ![Raiko](../../01_intro_databaser/notes/manga/raiko.jpg) | **Raiko** | pirat, inte någon vän |
| ![Mamo](../../01_intro_databaser/notes/manga/mamo.jpg) | **Mamo** | handlare med en kappa full av fickor |

> Kapitlen nedan använder SQLite som standardexempel. Vill du byta databas? Se [Välj din databas](#välj-din-databas) längst ner — API:et är detsamma, bara anslutningssträngen och klassnamnen skiljer sig.

## Kapitel 1 — Piratmetoden: kommunikation utan förväntat svar

![Sora](../../01_intro_databaser/notes/manga/sora.jpg)

**Sora:** Ett skepp närmar sig! Yuki, anropa det!

```csharp
using var conn = new SqliteConnection("Data Source=Space.db");
```

![Yuki](../../01_intro_databaser/notes/manga/yuki.jpg)

**Yuki:** Öppnar kommunikationskanalen!

```csharp
conn.Open();
```

![Raiko](../../01_intro_databaser/notes/manga/raiko.jpg)

**Raiko:** Hahaha! Lämna över lasten, annars blir ni rymdskrot!

```csharp
// (Det här var ingen förhandling)
```

![Sora](../../01_intro_databaser/notes/manga/sora.jpg)

**Sora:** Kenta, förbered för strid!

```csharp
string sql = "DELETE FROM SpaceShips WHERE Ship = @ship";
```

![Kenta](../../01_intro_databaser/notes/manga/kenta.jpg)

**Kenta:** Skölden uppe! Laddar kanonerna!

```csharp
using var cmd = new SqliteCommand(sql, conn);
```

![Sora](../../01_intro_databaser/notes/manga/sora.jpg)

**Sora:** Sikta på piratskeppet!

```csharp
cmd.Parameters.AddWithValue("@ship", "PirateShip");
```

![Sora](../../01_intro_databaser/notes/manga/sora.jpg)

**Sora:** ELD!

```csharp
int rows = cmd.ExecuteNonQuery();
```

![Hoshi](../../01_intro_databaser/notes/manga/hoshi.jpg)

**Hoshi:** *justerar glasögonen* Kontrollerar att piratskeppet är borta.

```csharp
if (rows == 1)
{
    Console.WriteLine("Bekräftat. Ett piratskepp mindre.");
}
```

`ExecuteNonQuery()` returnerar **antalet rader som påverkades**. Om Hoshi får `0` har vi missat, för då fanns det inget skepp med det namnet.

## Kapitel 2 — Handlarmetoden: kommunikation som returnerar en DataTable

![Hoshi](../../01_intro_databaser/notes/manga/hoshi.jpg)

**Hoshi:** Ett skepp närmar sig. Jag förbereder minnesplats för informationsutbyte.

```csharp
DataTable dt = new DataTable(); // kräver using System.Data;
```

![Sora](../../01_intro_databaser/notes/manga/sora.jpg)

**Sora:** Yuki, anropa skeppet!

```csharp
using var conn = new SqliteConnection("Data Source=Space.db");
```

![Yuki](../../01_intro_databaser/notes/manga/yuki.jpg)

**Yuki:** Öppnar kommunikationskanalen!

```csharp
conn.Open();
```

![Mamo](../../01_intro_databaser/notes/manga/mamo.jpg)

**Mamo:** Fufufu... jag har precis det ni söker, kapten.

```csharp
// (Han har det alltid. Frågan är bara vad det kostar.)
```

![Sora](../../01_intro_databaser/notes/manga/sora.jpg)

**Sora:** Visa vad du har.

```csharp
string sql = "SELECT * FROM MerchantItems WHERE Merchant = @merchant";
```

![Yuki](../../01_intro_databaser/notes/manga/yuki.jpg)

**Yuki:** Förbereder sökning av varor.

```csharp
using var cmd = new SqliteCommand(sql, conn);
```

![Yuki](../../01_intro_databaser/notes/manga/yuki.jpg)

**Yuki:** Skickar över sökparametrar.

```csharp
cmd.Parameters.AddWithValue("@merchant", "Mamo");
```

![Hoshi](../../01_intro_databaser/notes/manga/hoshi.jpg)

**Hoshi:** Förbereder överföring av hans data till oss.

```csharp
using var reader = cmd.ExecuteReader();
```

![Hoshi](../../01_intro_databaser/notes/manga/hoshi.jpg)

**Hoshi:** Tar emot information.

```csharp
dt.Load(reader);
```

![Yuki](../../01_intro_databaser/notes/manga/yuki.jpg)

**Yuki:** All data är mottagen. Stänger kanalen.

```csharp
return dt;
```

![Kenta](../../01_intro_databaser/notes/manga/kenta.jpg)

**Kenta:** Eh... varför står det en låda med "GRATISPROV" i lastrummet?

![Mamo](../../01_intro_databaser/notes/manga/mamo.jpg)

**Mamo:** *redan långt borta* Fufufu...

`ExecuteReader()` öppnar strömmen av rader, och `dt.Load(reader)` lägger dem i en `DataTable` som du sedan kan loopa igenom:

```csharp
foreach (DataRow row in dt.Rows)
{
    Console.WriteLine($"{row["Item"]} {row["Price"]}");
}
```

## Kapitel 3 — Parametern som hette fel

![Kenta](../../01_intro_databaser/notes/manga/kenta.jpg)

**Kenta:** Kapten! Programmet kraschade!

```csharp
string sql = "SELECT * FROM MerchantItems WHERE Merchant = @name";
// ...
cmd.Parameters.AddWithValue("@merchant", "Mamo");
```

```
System.InvalidOperationException: Must add values for the following parameters: @name
```

![Hoshi](../../01_intro_databaser/notes/manga/hoshi.jpg)

**Hoshi:** Logiskt. SQL-satsen frågar efter `@name`, men vi skickade `@merchant`. Namnen måste vara **exakt samma** på båda ställena.

Felmeddelandet talar om vilken parameter i SQL-satsen som saknar värde. Läs det, så vet du var du ska leta.

> I SQLite i C# skriver vi parametrarna som `$name`. `Microsoft.Data.Sqlite` förstår både `@`, `$` och `:`, så välj ett av dem och håll fast vid det.

## Sammanfattning

**Piratmetoden** använder du när du ska *manipulera* data (skapa, ändra eller radera) och inte behöver ta emot några rader:

- Du kör `ExecuteNonQuery()`, och den returnerar antalet påverkade rader.
- Vill du ha id:t på en rad du nyss har lagt in (`INSERT`) frågar du efter det med `SELECT last_insert_rowid()` och `ExecuteScalar()`. Det visas i SQLite i C#, avsnitt 8.

**Handlarmetoden** använder du när du *söker* data och vill ha den tillbaka:

- Du kör `ExecuteReader()` och lägger resultatet i en `DataTable` med `dt.Load(reader)`.
- En `DataTable` kan innehålla flera rader (`dt.Rows`). Varje `DataRow` är **en rad** i resultatet.
- Om `dt.Rows.Count == 0` hittade databasen inget som matchade din sökning.

**Använd alltid parametrar**, oavsett om värdet är text eller en siffra. Det skyddar mot SQL injection, och du slipper tänka på citattecken och decimaltecken.

## Välj din databas

SQL-koden är **identisk** oavsett databas. Det som ändras är tre saker:

| | SQLite | MySQL | PostgreSQL | SQL Server |
|---|---|---|---|---|
| **NuGet-paket** | `Microsoft.Data.Sqlite` | `MySqlConnector` | `Npgsql` | `Microsoft.Data.SqlClient` |
| **Connection-klass** | `SqliteConnection` | `MySqlConnection` | `NpgsqlConnection` | `SqlConnection` |
| **Command-klass** | `SqliteCommand` | `MySqlCommand` | `NpgsqlCommand` | `SqlCommand` |
| **Server krävs** | Nej | Ja | Ja | Ja |

---

### SQLite — filbaserad, kräver ingen server

```bash
dotnet add package Microsoft.Data.Sqlite
```

```xml
<PackageReference Include="Microsoft.Data.Sqlite" Version="9.0.0" />
```

```csharp
using Microsoft.Data.Sqlite;

const string conn = "Data Source=Space.db";
```

---

### MySQL — server eller Docker

```bash
dotnet add package MySqlConnector
```

```xml
<PackageReference Include="MySqlConnector" Version="2.3.7" />
```

Starta en MySQL-server med Docker:

```bash
docker run -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=hemligt --name mysql-space mysql
```

```csharp
using MySqlConnector;

const string conn = "Server=localhost;Database=space;User ID=root;Password=hemligt;";
```

---

### PostgreSQL — server eller Docker

```bash
dotnet add package Npgsql
```

```xml
<PackageReference Include="Npgsql" Version="8.0.3" />
```

Starta en PostgreSQL-server med Docker:

```bash
docker run -d -p 5432:5432 -e POSTGRES_PASSWORD=hemligt --name postgres-space postgres
```

```csharp
using Npgsql;

const string conn = "Host=localhost;Database=space;Username=postgres;Password=hemligt;";
```

---

### SQL Server — LocalDB (Windows) eller Docker

```bash
dotnet add package Microsoft.Data.SqlClient
```

```xml
<PackageReference Include="Microsoft.Data.SqlClient" Version="5.2.2" />
```

**LocalDB** (Windows, kräver Visual Studio-installation):

```csharp
using Microsoft.Data.SqlClient;

const string conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=space;Integrated Security=true;";
```

**Docker** (Linux, Mac, Windows):

```bash
docker run -d -p 1433:1433 \
  -e SA_PASSWORD=HemligtL0senord! \
  -e ACCEPT_EULA=Y \
  --name sqlserver-space \
  mcr.microsoft.com/mssql/server
```

```csharp
using Microsoft.Data.SqlClient;

const string conn = "Data Source=localhost;Initial Catalog=space;User ID=sa;Password=HemligtL0senord!;TrustServerCertificate=true;";
```

> **SQL Server:** kolumntypen `TEXT` heter `NVARCHAR(MAX)` i SQL Server. Setup-koden längre ner visar rätt syntax.

---

## Testa själv

Vill du köra exemplen behöver databasen tabellerna och lite data. Kör setup-koden en gång innan — välj rätt block för din databas.

### SQLite

```csharp
using Microsoft.Data.Sqlite;

using var setup = new SqliteConnection("Data Source=Space.db");
setup.Open();
using var c = setup.CreateCommand();

c.CommandText = "CREATE TABLE IF NOT EXISTS SpaceShips (Ship TEXT NOT NULL)";
c.ExecuteNonQuery();

c.CommandText = "INSERT INTO SpaceShips VALUES ('Kometen'), ('PirateShip')";
c.ExecuteNonQuery();

c.CommandText = """
    CREATE TABLE IF NOT EXISTS MerchantItems (
        Merchant TEXT NOT NULL,
        Item     TEXT NOT NULL,
        Price    INTEGER
    )
    """;
c.ExecuteNonQuery();

c.CommandText = """
    INSERT INTO MerchantItems VALUES
        ('Mamo', 'Hyperdriftskristall', 500),
        ('Mamo', 'Snabbnudlar (12-pack)', 8),
        ('Mamo', 'Gratisprov', 0),
        ('Okänd handlare', 'Begagnad robotarm', 120)
    """;
c.ExecuteNonQuery();
```

### MySQL

```csharp
using MySqlConnector;

using var setup = new MySqlConnection("Server=localhost;Database=space;User ID=root;Password=hemligt;");
setup.Open();
using var c = setup.CreateCommand();

c.CommandText = "CREATE TABLE IF NOT EXISTS SpaceShips (Ship TEXT NOT NULL)";
c.ExecuteNonQuery();

c.CommandText = "INSERT INTO SpaceShips VALUES ('Kometen'), ('PirateShip')";
c.ExecuteNonQuery();

c.CommandText = """
    CREATE TABLE IF NOT EXISTS MerchantItems (
        Merchant TEXT NOT NULL,
        Item     TEXT NOT NULL,
        Price    INT
    )
    """;
c.ExecuteNonQuery();

c.CommandText = """
    INSERT INTO MerchantItems VALUES
        ('Mamo', 'Hyperdriftskristall', 500),
        ('Mamo', 'Snabbnudlar (12-pack)', 8),
        ('Mamo', 'Gratisprov', 0),
        ('Okänd handlare', 'Begagnad robotarm', 120)
    """;
c.ExecuteNonQuery();
```

### PostgreSQL

```csharp
using Npgsql;

using var setup = new NpgsqlConnection("Host=localhost;Database=space;Username=postgres;Password=hemligt;");
setup.Open();
using var c = setup.CreateCommand();

c.CommandText = "CREATE TABLE IF NOT EXISTS SpaceShips (Ship TEXT NOT NULL)";
c.ExecuteNonQuery();

c.CommandText = "INSERT INTO SpaceShips VALUES ('Kometen'), ('PirateShip')";
c.ExecuteNonQuery();

c.CommandText = """
    CREATE TABLE IF NOT EXISTS MerchantItems (
        Merchant TEXT NOT NULL,
        Item     TEXT NOT NULL,
        Price    INTEGER
    )
    """;
c.ExecuteNonQuery();

c.CommandText = """
    INSERT INTO MerchantItems VALUES
        ('Mamo', 'Hyperdriftskristall', 500),
        ('Mamo', 'Snabbnudlar (12-pack)', 8),
        ('Mamo', 'Gratisprov', 0),
        ('Okänd handlare', 'Begagnad robotarm', 120)
    """;
c.ExecuteNonQuery();
```

### SQL Server

```csharp
using Microsoft.Data.SqlClient;

// Skapa databasen om den inte finns (behövs bara en gång)
using (var master = new SqlConnection("Data Source=localhost;Initial Catalog=master;User ID=sa;Password=HemligtL0senord!;TrustServerCertificate=true;"))
{
    master.Open();
    using var mc = master.CreateCommand();
    mc.CommandText = "IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'space') CREATE DATABASE space";
    mc.ExecuteNonQuery();
}

using var setup = new SqlConnection("Data Source=localhost;Initial Catalog=space;User ID=sa;Password=HemligtL0senord!;TrustServerCertificate=true;");
setup.Open();
using var c = setup.CreateCommand();

c.CommandText = """
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SpaceShips')
    CREATE TABLE SpaceShips (Ship NVARCHAR(MAX) NOT NULL)
    """;
c.ExecuteNonQuery();

c.CommandText = "INSERT INTO SpaceShips VALUES ('Kometen'), ('PirateShip')";
c.ExecuteNonQuery();

c.CommandText = """
    IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'MerchantItems')
    CREATE TABLE MerchantItems (
        Merchant NVARCHAR(MAX) NOT NULL,
        Item     NVARCHAR(MAX) NOT NULL,
        Price    INT
    )
    """;
c.ExecuteNonQuery();

c.CommandText = """
    INSERT INTO MerchantItems VALUES
        ('Mamo', 'Hyperdriftskristall', 500),
        ('Mamo', 'Snabbnudlar (12-pack)', 8),
        ('Mamo', 'Gratisprov', 0),
        ('Okänd handlare', 'Begagnad robotarm', 120)
    """;
c.ExecuteNonQuery();
```

Handlarmetoden ger tre rader. Den sista är förstås ett gratisprov.

---

📖 **Läs mer i C#-boken**  
→ [SQL från C# — rymdmangan](https://marcusjobb.github.io/programmering-csharp/sql/sql-fran-csharp-rymdmanga/)
