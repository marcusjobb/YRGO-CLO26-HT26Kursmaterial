# ADO.NET Quicksheet — C# och databaser

En snabbreferens. Spara den, hitta tillbaka till den. Den berättar inte allt — men den berättar det du behöver när du sitter och kodar.

---

## appsettings.json och appsettings.example.json

Du jobbar alltid med **två filer** sida vid sida.

| Fil | Innehåll | Git |
|---|---|---|
| `appsettings.json` | Riktiga lösenord och credentials | **Gitignorerad** |
| `appsettings.example.json` | Mallvärden med platshållare | **Committas** |

**appsettings.example.json** — den här commitar du:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=DITT-SERVER;Database=DIN-DATABAS;User=DIN-USER;Password=DITT-LÖSENORD;"
  }
}
```

**appsettings.json** — den här gitignorerar du:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=heroapp;User=root;Password=hemligt123;"
  }
}
```

**.gitignore** — lägg till detta om det saknas:

```
appsettings.json
```

> Tumregel: om du inte vill se det på GitHub — lägg det i appsettings.json, inte example.

---

## Läsa connection string i C#

```csharp
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("DefaultConnection")!;
```

---

## NuGet + connection string per databastyp

### SQLite

```bash
dotnet add package Microsoft.Data.Sqlite
```

```json
"DefaultConnection": "Data Source=app.db"
```

```csharp
using Microsoft.Data.Sqlite;
using var connection = new SqliteConnection(connectionString);
```

---

### MySQL (och TiDB Cloud)

```bash
dotnet add package MySql.Data
```

```json
"DefaultConnection": "Server=localhost;Port=3306;Database=myapp;User=root;Password=LÖSENORD;"
```

TiDB Cloud — lägg till `SslMode=Required` och port `4000`:

```json
"DefaultConnection": "Server=gateway01.eu-central-1.prod.aws.tidbcloud.com;Port=4000;Database=test;User=DIN-USER;Password=DITT-LÖSENORD;SslMode=Required;"
```

```csharp
using MySql.Data.MySqlClient;
using var connection = new MySqlConnection(connectionString);
```

---

### MariaDB

Exakt samma paket och syntax som MySQL — MariaDB är kompatibelt:

```bash
dotnet add package MySql.Data
```

```json
"DefaultConnection": "Server=localhost;Port=3306;Database=myapp;User=root;Password=LÖSENORD;"
```

---

### PostgreSQL

```bash
dotnet add package Npgsql
```

```json
"DefaultConnection": "Host=localhost;Port=5432;Database=myapp;Username=postgres;Password=LÖSENORD;"
```

```csharp
using Npgsql;
using var connection = new NpgsqlConnection(connectionString);
```

---

### SQL Server / LocalDB

```bash
dotnet add package Microsoft.Data.SqlClient
```

```json
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=MyApp;Integrated Security=true;"
```

Med lösenord (Docker eller server):

```json
"DefaultConnection": "Server=localhost,1433;Database=MyApp;User Id=sa;Password=LÖSENORD;TrustServerCertificate=true;"
```

```csharp
using Microsoft.Data.SqlClient;
using var connection = new SqlConnection(connectionString);
```

---

### MongoDB

```bash
dotnet add package MongoDB.Driver
```

```json
"DefaultConnection": "mongodb://localhost:27017"
```

Med autentisering:

```json
"DefaultConnection": "mongodb://USER:LÖSENORD@localhost:27017"
```

```csharp
using MongoDB.Driver;
var client = new MongoClient(connectionString);
var db = client.GetDatabase("myapp");
```

---

## Anslut och verifiera — samma mönster för alla

```csharp
using var connection = new MySqlConnection(connectionString); // byt klass per DB
connection.Open();
Console.WriteLine("Ansluten till: " + connection.Database);
```

`using var` stänger anslutningen automatiskt när blocket är klart. Glöm inte det.

---

## Snabbreferens

| Databas | NuGet-paket | Connection-klass |
|---|---|---|
| SQLite | `Microsoft.Data.Sqlite` | `SqliteConnection` |
| MySQL / TiDB / MariaDB | `MySql.Data` | `MySqlConnection` |
| PostgreSQL | `Npgsql` | `NpgsqlConnection` |
| SQL Server / LocalDB | `Microsoft.Data.SqlClient` | `SqlConnection` |
| MongoDB | `MongoDB.Driver` | `MongoClient` |

> EF Core-paket och connection strings för Entity Framework finns i ett eget quicksheet.
