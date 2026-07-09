---
marp: true
theme: default
class: invert
paginate: true
---

# EF Core i Produktion — MySQL, Prestanda och Säkerhet

**Kurs:** Databashantering och -design
**Modul:** 06 — Entity Framework Core

---

## Vad ska vi lära oss idag?

- **Från SQLite till MySQL** — byta databasprovider
- **Docker för MySQL** — lokal utvecklingsdatabas
- **Prestandaoptimering** — undvik vanliga fallgropar
- **AsNoTracking** — Read-only queries
- **SQL Injection-skydd** — EF Core gör det automatiskt
- **Connection Strings** — säker hantering

---

## SQLite vs MySQL — När byta?

| Aspekt | SQLite | MySQL |
|--------|--------|-------|
| Användning | Utveckling, prototyper | Produktion, team |
| Prestanda | Bra för en användare | Flertrådad + samtidig |
| Storage | En fil | Server (port 3306) |
| Migration | Fungerar bra | Fungerar bra |
| Setup | Ingen | Kräver Docker/server |

**Vår väg:** SQLite under utveckling → MySQL i produktion

---

## Byt databasprovider — så enkelt är det

Steg 1: Installera NuGet-paket
```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

Steg 2: Ändra OnConfiguring
```csharp
// SQLite (utveckling)
optionsBuilder.UseSqlite("Data Source=shop.db");

// MySQL (produktion)
optionsBuilder.UseMySql(
    "Server=localhost;Database=webshop;User=root;Password=hemligt;",
    new MariaDbServerVersion(new Version(10, 5))
);
```

Det är ALLT. Samma entities, samma LINQ-queries, samma migrationer. ✅

---

## Docker för MySQL

Starta MySQL på en minut:

```bash
docker run --name mysql-dev \
    -e MYSQL_ROOT_PASSWORD=hemligt \
    -e MYSQL_DATABASE=webshop \
    -p 3306:3306 \
    -d mysql:8
```

| Flagga | Betydelse |
|--------|-----------|
| `--name` | Namn på containern |
| `-e` | Environment variables |
| `-p 3306:3306` | Port mapping (host:container) |
| `-d` | Detached (kör i bakgrunden) |

---

## Koppla EF Core till Docker-MySQL

```csharp
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=webshop;User=root;Password=hemligt;"
  }
}

// Program.cs
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MariaDbServerVersion(new Version(10, 5))
    ));
```

```bash
# Skapa migration för MySQL
Add-Migration InitialCreate
Update-Database
```

---

## Anslutningssträngar — ALDRIG i kod

**Dåligt:** Lösenord hårdkodat i C#:
```csharp
options.UseMySql("Server=...;Password=hemligt;", ...);
```

**Bra:** User Secrets i utveckling:
```bash
dotnet user-secrets set "ConnectionStrings:Default" "Server=...;Password=hemligt;"
```

**Bra:** Environment Variables i produktion:
```bash
export DB_CONNECTION="Server=...;Password=hemligt;"
```

```csharp
var connStr = Environment.GetEnvironmentVariable("DB_CONNECTION");
```

---

## Prestanda — AsNoTracking

När du bara läser data (inget kommer ändras):

```csharp
// Långsam — EF Core spårar ALLA entiteter i minnet
var customers = context.Customers.ToList();

// Snabb — ingen tracking, mindre minne
var customers = context.Customers
    .AsNoTracking()
    .ToList();

// Globalt (alla queries)
optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
```

Skillnad: Tracking använder ChangeTracker för att veta vad som ändrats. AsNoTracking hoppar det — 30-50% snabbare!

---

## Prestanda — Split Queries

Vid `Include()` med stora datamängder:

```csharp
// Standard: En jätte-JOIN
var customers = context.Customers
    .Include(c => c.Orders)
    .ToList();
// SQL: SELECT c.*, o.* FROM Customers c
//      LEFT JOIN Orders o ON c.Id = o.CustomerId
//      → mycket duplicerad data!

// Bättre: Delade queries (EF Core 5+)
var customers = context.Customers
    .Include(c => c.Orders)
    .AsSplitQuery()
    .ToList();
// SQL: SELECT * FROM Customers
//      SELECT * FROM Orders WHERE CustomerId IN (...)
```

---

## Prestanda — Batch Operations

```csharp
// DÅLIGT: N+1 queries, N transaktioner
foreach(var price in priceList)
{
    var product = context.Products.Find(price.ProductId);
    product.Price = price.NewPrice;
    context.SaveChanges();
}

// BRA: En batch, en transaktion
foreach(var price in priceList)
{
    var product = context.Products.Find(price.ProductId);
    product.Price = price.NewPrice;
}
context.SaveChanges(); // Allt på en gång!
```

**ExecuteUpdate / ExecuteDelete** (EF Core 7+):
```csharp
// Utan att ladda in entiteter
context.Products
    .Where(p => p.Stock == 0)
    .ExecuteDelete();

context.Products
    .Where(p => p.Category == "Old")
    .ExecuteUpdate(p => p.SetProperty(x => x.Price, x => x.Price * 0.8));
```

---

## SQL Injection — EF Core skyddar dig

**Sårbart (ADO.NET/raw SQL):**
```csharp
string input = "' OR '1'='1";
var sql = $"SELECT * FROM Users WHERE Name = '{input}'";
```

**Säkert (EF Core):**
```csharp
string input = "' OR '1'='1";
var user = context.Users.FirstOrDefault(u => u.Name == input);
// EF Core genererar: SELECT * FROM Users WHERE Name = @p0
// @p0 = "' OR '1'='1" (behandlas som text, inte SQL)
```

**Raw SQL med EF Core (ändå säkert):**
```csharp
var users = context.Users
    .FromSql($"SELECT * FROM Users WHERE Name = {input}")
    .ToList();
```

---

## Prestanda — Ladda bara det du behöver

```csharp
// DÅLIGT: Laddar ALLA kolumner för ALLA rader
var allData = context.Products.ToList();

// BRA: Bara det du behöver
var cheapProducts = context.Products
    .Where(p => p.Price < 100)
    .OrderBy(p => p.Name)
    .Select(p => new { p.Name, p.Price })
    .ToList();

// ELLER: Paginering
var page = context.Products
    .OrderBy(p => p.Name)
    .Skip(20)    // Hoppa över första 20
    .Take(10)    // Ta 10
    .ToList();
```

---

## Connection Pooling

MySQL connection pooling = återanvänd anslutningar istället för att öppna/stänga:

```csharp
// BRA: using = stäng efter användning, pooling gör resten
using var context = new AppDbContext();
var products = context.Products.ToList();
```

```csharp
// DÅLIGT: Långlivad context
var context = new AppDbContext(); // Öppen länge, minnesläcka risk
```

DbContext är designad för kortlivade instanser — skapa, använd, släng.

---

## Checklista för produktion

- [ ] SQLite → MySQL (eller SQL Server)
- [ ] Anslutningssträng i User Secrets / Environment
- [ ] `AsNoTracking()` på read-only queries
- [ ] `AsSplitQuery()` för komplexa Includes
- [ ] Batcha SaveChanges — inte en per rad
- [ ] Paginera listor med Skip/Take
- [ ] Index på kolumner du söker/filtrerar på
- [ ] Inga råa SQL-strängar — använd LINQ
- [ ] Connection Pooling påslaget (default i MySQL)

---

## Sammanfattning

- ✅ SQLite → MySQL = byt provider, allt annat samma
- ✅ Docker startar MySQL på 1 minut
- ✅ Anslutningssträngar i User Secrets / miljövariabler
- ✅ AsNoTracking = 30-50% snabbare läsning
- ✅ ExecuteDelete / ExecuteUpdate för bulk (EF 7+)
- ✅ EF Core skyddar mot SQL Injection
- ✅ Paginering med Skip/Take

---
