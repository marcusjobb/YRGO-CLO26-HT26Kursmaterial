---

title: Bilagor
author: Marcus Ackre Medina
type: article
topic: databaser
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/csharp_cmyh/C-Sharp/databases/brief/bilagor.md"
description: "docker logs -f <container_name>"
tags: ["asp.net", "bilagor", "csharp", "databaser", "design-patterns", "entity-framework", "git", "oop", "sql"]
week_fit: []
---

# Bilagor

🟢


## A. Docker-kommandon

### Grundläggande Docker-kommandon

```bash
# Visa alla körande containers
docker ps

# Visa alla containers (även stoppade)
docker ps -a

# Starta en container
docker start <container_name>

# Stoppa en container
docker stop <container_name>

# Ta bort en container
docker rm <container_name>

# Visa loggar från en container
docker logs <container_name>

# Följ loggar live
docker logs -f <container_name>

# Kör kommando i körande container
docker exec -it <container_name> <command>

# Öppna bash-shell i container
docker exec -it <container_name> bash

# Kopiera fil från container till host
docker cp <container_name>:/path/in/container ./local/path

# Kopiera fil från host till container
docker cp ./local/path <container_name>:/path/in/container
```

### Docker Compose-kommandon

```bash
# Starta alla services (detached mode)
docker-compose up -d

# Stoppa alla services
docker-compose down

# Stoppa och ta bort volymer (RADERAR DATA!)
docker-compose down -v

# Visa loggar från alla services
docker-compose logs

# Följ loggar live
docker-compose logs -f

# Starta om alla services
docker-compose restart

# Visa status på services
docker-compose ps

# Bygg om images
docker-compose build
```

### Databasspecifika kommandon

**MySQL:**
```bash
# Kör MySQL-shell
docker exec -it mysql_dev mysql -u root -p

# Backup
docker exec mysql_dev mysqldump -u root -p myapp > backup.sql

# Restore
docker exec -i mysql_dev mysql -u root -p myapp < backup.sql
```

**SQL Server:**
```bash
# Kör sqlcmd
docker exec -it sqlserver_dev /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'YourPassword'

# Backup
docker exec sqlserver_dev /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'pass' -Q "BACKUP DATABASE MyApp TO DISK='/var/opt/mssql/backup/MyApp.bak'"
```

**MongoDB:**
```bash
# Kör mongosh
docker exec -it mongodb_dev mongosh -u admin -p adminpassword

# Backup
docker exec mongodb_dev mongodump --username admin --password adminpassword --db myapp --out /dump

# Restore
docker exec mongodb_dev mongorestore --username admin --password adminpassword --db myapp /dump/myapp
```

---

## B. Lista över NuGet-paket

### SQLite

```bash
# Grundläggande anslutning
dotnet add package Microsoft.Data.Sqlite

# Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### MySQL

```bash
# Grundläggande anslutning
dotnet add package MySql.Data

# Entity Framework Core
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### SQL Server / LocalDB

```bash
# Grundläggande anslutning
dotnet add package Microsoft.Data.SqlClient

# Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### MongoDB

```bash
# MongoDB Driver (INGET EF Core)
dotnet add package MongoDB.Driver
```

### EF Core Tools (Globalt)

```bash
# Installera dotnet-ef verktyg
dotnet tool install --global dotnet-ef

# Uppdatera dotnet-ef
dotnet tool update --global dotnet-ef

# Verifiera installation
dotnet ef
```

---

## C. Exempel på `appsettings.json`

### Struktur för alla databaser

```json
{
  "ConnectionStrings": {
    "SQLite": "Data Source=app.db",

    "MySQL": "Server=localhost;Port=3306;Database=myapp;User=appuser;Password=apppassword;",

    "LocalDB": "Server=(localdb)\\MSSQLLocalDB;Database=MyApp;Integrated Security=true;",

    "SQLServer": "Server=localhost,1433;Database=MyApp;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=true;",

    "MongoDB": "mongodb://admin:adminpassword@localhost:27017",

    "MongoDBAtlas": "mongodb+srv://username:password@cluster.mongodb.net/myapp"
  },

  "DatabaseSettings": {
    "Provider": "SQLite",
    "DatabaseName": "myapp"
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  }
}
```

### Använda i C#

```csharp
using Microsoft.Extensions.Configuration;

// Läs configuration
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Hämta connection string
string connectionString = config.GetConnectionString("SQLite")!;

// Hämta specifik setting
string provider = config["DatabaseSettings:Provider"]!;
```

### Med Dependency Injection (ASP.NET Core)

```csharp
// Program.cs eller Startup.cs
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(Configuration.GetConnectionString("SQLite")));
```

---

## D. CRUD-kodexempel

### Entity Framework Core (SQL-databaser)

```csharp
// CREATE
using (var context = new AppDbContext())
{
    var entity = new Customer { Name = "Test", Email = "test@example.com" };
    context.Customers.Add(entity);
    context.SaveChanges();
}

// READ - Alla
using (var context = new AppDbContext())
{
    var customers = context.Customers.ToList();
}

// READ - Filtrerad
using (var context = new AppDbContext())
{
    var customers = context.Customers
        .Where(c => c.Email.EndsWith("@example.com"))
        .OrderBy(c => c.Name)
        .ToList();
}

// READ - Med relationer
using (var context = new AppDbContext())
{
    var customers = context.Customers
        .Include(c => c.Orders)
        .ThenInclude(o => o.OrderItems)
        .ToList();
}

// UPDATE
using (var context = new AppDbContext())
{
    var customer = context.Customers.Find(1);
    if (customer != null)
    {
        customer.Email = "newemail@example.com";
        context.SaveChanges();
    }
}

// DELETE
using (var context = new AppDbContext())
{
    var customer = context.Customers.Find(1);
    if (customer != null)
    {
        context.Customers.Remove(customer);
        context.SaveChanges();
    }
}
```

### MongoDB.Driver

```csharp
// Setup
var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("myapp");
var collection = database.GetCollection<Customer>("customers");

// CREATE
var customer = new Customer { Name = "Test", Email = "test@example.com" };
await collection.InsertOneAsync(customer);

// READ - Alla
var customers = await collection.Find(_ => true).ToListAsync();

// READ - Filtrerad
var customers = await collection
    .Find(c => c.Email.EndsWith("@example.com"))
    .SortBy(c => c.Name)
    .ToListAsync();

// READ - Med Filter Builder
var filter = Builders<Customer>.Filter.Eq(c => c.Email, "test@example.com");
var customer = await collection.Find(filter).FirstOrDefaultAsync();

// UPDATE
var filter = Builders<Customer>.Filter.Eq(c => c.Id, customerId);
var update = Builders<Customer>.Update.Set(c => c.Email, "newemail@example.com");
await collection.UpdateOneAsync(filter, update);

// UPDATE - Lägg till i array
var addUpdate = Builders<Customer>.Update.Push(c => c.Tags, "newtag");
await collection.UpdateOneAsync(filter, addUpdate);

// DELETE
var filter = Builders<Customer>.Filter.Eq(c => c.Id, customerId);
await collection.DeleteOneAsync(filter);
```

---

## E. Ordförklaringar (Glossary)

### Allmänna databastermer

| Term | Förklaring |
|------|------------|
| **ACID** | Atomicity, Consistency, Isolation, Durability – garantier för transaktioner |
| **Connection String** | Text som innehåller information för att ansluta till databas |
| **CRUD** | Create, Read, Update, Delete – grundläggande databasoperationer |
| **Index** | Datastruktur som gör queries snabbare |
| **Migration** | Fil som beskriver ändringar i databasschema |
| **ORM** | Object-Relational Mapping – mappar objekt till databastabeller |
| **Query** | Förfrågan till databas för att hämta eller ändra data |
| **Schema** | Struktur som definierar tabeller, kolumner, datatyper |
| **Transaction** | Grupp av operationer som utförs som en enhet |

### SQL-specifika termer

| Term | Förklaring |
|------|------------|
| **Foreign Key** | Kolumn som refererar till primärnyckel i annan tabell |
| **JOIN** | Kombinerar rader från flera tabeller |
| **Primary Key** | Unik identifierare för rad i tabell |
| **Stored Procedure** | Fördefinierad SQL-kod som kan återanvändas |
| **Trigger** | Automatisk åtgärd vid INSERT/UPDATE/DELETE |
| **View** | Virtuell tabell baserad på query |

### NoSQL-specifika termer (MongoDB)

| Term | Förklaring |
|------|------------|
| **BSON** | Binary JSON – Mongos lagringsformat |
| **Collection** | Motsvarar tabell i SQL |
| **Document** | Motsvarar rad i SQL (JSON-objekt) |
| **Embedded Document** | Dokument inuti dokument (nested) |
| **ObjectId** | Unikt ID som MongoDB genererar automatiskt |
| **Aggregation Pipeline** | Serie av operationer för komplex databehandling |
| **Sharding** | Horisontell uppdelning av data över flera servrar |

### Entity Framework Core-termer

| Term | Förklaring |
|------|------------|
| **DbContext** | Klass som representerar databas-session |
| **DbSet** | Samling av entiteter (motsvarar tabell) |
| **Migration** | Kod som uppdaterar databasschema |
| **Navigation Property** | Property som representerar relation |
| **Tracking** | EF håller koll på ändringar i objekt |
| **Include()** | Eager loading av relaterade entiteter |
| **SaveChanges()** | Sparar alla ändringar till databasen |

### Docker-termer

| Term | Förklaring |
|------|------------|
| **Container** | Isolerad körande instans av en image |
| **Image** | Mall för att skapa containers |
| **Volume** | Persistent lagring för containers |
| **docker-compose** | Verktyg för att definiera multi-container-appar |
| **Port Mapping** | Exponera container-port till host |

---

## F. Format-jämförelse (JSON, XML, CSV, BSON)

### Samma data i olika format

**JSON:**
```json
{
  "id": 1,
  "name": "Anna Andersson",
  "email": "anna@example.com",
  "address": {
    "street": "Storgatan 1",
    "city": "Stockholm",
    "zip": "11122"
  },
  "tags": ["vip", "premium"]
}
```

**XML:**
```xml
<?xml version="1.0" encoding="UTF-8"?>
<customer>
  <id>1</id>
  <name>Anna Andersson</name>
  <email>anna@example.com</email>
  <address>
    <street>Storgatan 1</street>
    <city>Stockholm</city>
    <zip>11122</zip>
  </address>
  <tags>
    <tag>vip</tag>
    <tag>premium</tag>
  </tags>
</customer>
```

**CSV:**
```csv
id,name,email,address_street,address_city,address_zip,tags
1,"Anna Andersson","anna@example.com","Storgatan 1","Stockholm","11122","vip;premium"
```

**BSON (konceptuellt, binärt format):**
```
\x16\x00\x00\x00               // Document size
\x10id\x00\x01\x00\x00\x00    // int32 id = 1
\x02name\x00\x10\x00\x00\x00Anna Andersson\x00
...
```

### Format-jämförelse tabell

| Funktion | JSON | XML | CSV | BSON |
|----------|------|-----|-----|------|
| **Läsbarhet** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐ (binärt) |
| **Kompakthet** | ⭐⭐⭐⭐ | ⭐⭐ | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ |
| **Nested data** | ✅ | ✅ | ❌ | ✅ |
| **Datatyper** | Begränsade | String-baserade | Inga | Rika (Date, ObjectId, etc.) |
| **Parsing-hastighet** | Snabb | Långsam | Mycket snabb | Mycket snabb |
| **Användning** | API:er, config | SOAP, äldre system | Excel, data-export | MongoDB |

---

**Detta var sista bilagan – du har nu alla referensmaterial du behöver!** 📚

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
