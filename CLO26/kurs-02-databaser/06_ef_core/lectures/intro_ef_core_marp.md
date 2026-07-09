---
marp: true
theme: default
class: invert
paginate: true
---

# Introduktion till ORM och Entity Framework Core

**Kurs:** Databashantering och -design
**Modul:** 06 — Entity Framework Core

---

## Vad ska vi lära oss idag?

- **Varför ORM?** — Från ADO.NET till automatisering
- **EF Core** — Microsofts ORM för .NET
- **Code First** — Skapa databas från C#-klasser
- **DbContext** — Hjärtat i EF Core
- **Migrationer** — Versionshantera din databas

---

## Från ADO.NET till ORM

**ADO.NET (manuell vägen):**
```csharp
string sql = "SELECT * FROM Customer";
var cmd = new SqlCommand(sql, conn);
var reader = cmd.ExecuteReader();
while(reader.Read()) {
    customers.Add(new Customer {
        Id = (int)reader["Id"],
        Name = (string)reader["Name"]
    });
}
```

**EF Core (ORM-vägen):**
```csharp
var customers = context.Customers.ToList();
```

---

## Vad är en ORM?

**ORM** = Object-Relational Mapping

Översätter mellan två världar:

```
C#-objekt                    Databastabeller
─────────────────           ─────────────────
Customer customer = new()    Customers-tabellen
{                            ┌────┬───────┐
    Id = 1,         ←──→     │ Id │ Name  │
    Name = "Anna"           ├────┼───────┤
};                           │ 1  │ Anna  │
                             └────┴───────┘
```

**ORM:en sköter mappningen** — du jobbar bara med C#-objekt.

---

## Varför EF Core?

| Fördel | Utan ORM | Med EF Core |
|--------|----------|-------------|
| Kodmängd | Mycket (SqlConnection, Command, Reader) | Minimalt |
| SQL Injection | Risk om du bygger strängar | Inbyggt skydd |
| Relationshantering | Manuella JOINs | Navigation properties |
| Ändra schema | Manuella SQL-scripts | Migrationer (automatiska) |
| Databastestning | Svårt | InMemory-provider |

---

## EF Core Arkitektur

```
┌──────────────────────────────┐
│       Din Applikation         │
├──────────────────────────────┤
│   DbContext (ditt lager)      │
├──────────────────────────────┤
│   DbSet<T> (tabellerna)       │
├──────────────────────────────┤
│   EF Core Engine              │
├──────┬───────┬───────┬───────┤
│ LINQ  │ Change│ Migra-│ SQL   │
│ → SQL │Tracker│ tioner│ Gen.  │
├──────┴───────┴───────┴───────┤
│     Database Provider         │
│  (SQLite, SQL Server, MySQL)  │
└──────────────────────────────┘
```

---

## Entity-klasser — Dina modeller

En entity = en C#-klass som mappas till en databastabell:

```csharp
public class Customer
{
    public int Id { get; set; }                 // PK (konvention: Id eller CustomerId)
    public string Name { get; set; }            // Kolumn
    public string Email { get; set; }
    public ICollection<Order> Orders { get; set; } // Navigation property
}

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Total { get; set; }
    public int CustomerId { get; set; }          // FK
    public Customer Customer { get; set; }       // Navigation
}
```

---

## DbContext — Hjärtat i EF Core

```csharp
public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=shop.db");
    }
}
```

- `DbSet<T>` = tabellen i databasen
- Du frågar via `context.Customers.Where(c => c.Name == "Anna")`
- EF Core översätter LINQ till SQL automatiskt

---

## Code First — Databas från kod

1. Skriv dina C#-klasser (entities)
2. Skapa en DbContext-klass
3. Skapa en migration
4. Uppdatera databasen

```
Du skriver:          EF Core skapar:
Customer.cs      →   CREATE TABLE Customers (
                         Id INTEGER PRIMARY KEY,
                         Name TEXT NOT NULL,
                         Email TEXT
                     );
```

---

## Migrationer — Versionshantering

```bash
# Skapa en migration (i Package Manager Console)
Add-Migration InitialCreate

# Applicera på databasen
Update-Database
```

**Vad händer?**
1. EF Core jämför dina C#-klasser med databasen
2. Skapar en C#-fil med uppgraderings-/nedgraderingskod
3. Kör SQL mot databasen

```csharp
// Genererad migration (exempel)
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.CreateTable(
        name: "Customers",
        columns: table => new {
            Id = table.Column<int>(nullable: false)
                .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(nullable: false)
        },
        constraints: table => table.PrimaryKey("PK_Customers", x => x.Id));
}
```

---

## Migrationer i team

```bash
# Hämta andras ändringar
git pull

# Applicera nya migrationer
Update-Database

# Om konflikt:
# 1. Ta bort din migration
Remove-Migration
# 2. Hämta andras
git pull
# 3. Skapa din migration på nytt
Add-Migration DinAndring
```

**Regel:** Ändra aldrig en publicerad migration — skapa en ny istället.

---

## CRUD med EF Core

```csharp
using var context = new AppDbContext();

// CREATE
context.Customers.Add(new Customer { Name = "Anna", Email = "anna@test.se" });
context.SaveChanges();

// READ
var customer = context.Customers.FirstOrDefault(c => c.Name == "Anna");

// UPDATE
customer.Email = "anna@nyadress.se";
context.SaveChanges();

// DELETE
context.Customers.Remove(customer);
context.SaveChanges();
```

`SaveChanges()` skickar allt till databasen. Flera ändringar = en transaktion.

---

## LINQ-frågor — Exempel

```csharp
// Alla kunder
var all = context.Customers.ToList();

// Filtrera
var anna = context.Customers.FirstOrDefault(c => c.Name == "Anna");

// Sortera
var byName = context.Customers.OrderBy(c => c.Name).ToList();

// Projektera
var names = context.Customers.Select(c => c.Name).ToList();

// Aggregera
var count = context.Customers.Count();
var first = context.Customers.Min(c => c.Id);
```

---

## Filtrering i C# vs SQL

**LINQ (C#):**
```csharp
var result = context.Customers
    .Where(c => c.Name.StartsWith("A") && c.Orders.Any())
    .OrderBy(c => c.Name)
    .ToList();
```

**SQL (genereras av EF Core):**
```sql
SELECT * FROM Customers c
WHERE c.Name LIKE 'A%'
  AND EXISTS (SELECT 1 FROM Orders o WHERE o.CustomerId = c.Id)
ORDER BY c.Name
```

Du skriver LINQ — EF Core genererar optimal SQL. ✅

---

## Prova själv

1. Skapa en entity `Product` med Id, Name, Price, Stock
2. Skapa en DbContext med `DbSet<Product>`
3. Skapa en migration och uppdatera databasen
4. Lägg till 3 produkter
5. Hämta alla produkter med pris över 100 kr

---

## Sammanfattning

- ✅ ORM mappar objekt ⇄ tabeller automatiskt
- ✅ EF Core är Microsofts ORM för .NET
- ✅ Code First = din kod definierar databasen
- ✅ DbContext = bryggan mellan kod och databas
- ✅ Migrationer = versionshantering för databasschema
- ✅ LINQ → SQL sker automatiskt
- ➡️ Nästa: Relationer och avancerad CRUD med EF Core

---
