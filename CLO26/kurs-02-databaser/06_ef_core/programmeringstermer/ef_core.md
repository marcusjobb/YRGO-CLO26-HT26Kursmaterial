# EF Core — Programmeringstermer

## Entity Framework Core (EF Core)
Microsofts moderna, cross-platform ORM för .NET. Låter dig arbeta med databaser via C#-objekt istället för SQL. Open source (MIT).

## Code First
Strategi där du först skriver C#-klasser (entities) och sedan genererar databasen från dem. Mest kontroll, bäst för nya projekt.

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
// → Genererar tabell: Products (Id, Name, Price)
```

## Database First
Strategi där du har en befintlig databas och genererar C#-klasser från den. Används med äldre eller befintliga databaser.

## Data Annotations
Attribut i C# som styr hur entiteter mappas till databasen. Placeras direkt på klasser och properties.

```csharp
[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
```

## Fluent API
Konfiguration av entiteter i `DbContext.OnModelCreating()`. Kraftfullare än Data Annotations och håller entitetsklasserna rena.

## Convention over Configuration
EF Cores princip att använda standardregler (konventioner) istället för explicit konfiguration. Exempel: egenskapen `Id` blir automatiskt Primary Key.

## Include
Metod för Eager Loading av relaterad data. Orsakar JOIN i genererad SQL.

```csharp
context.Customers.Include(c => c.Orders).ToList();
```

## ThenInclude
Kedjar Include för flera nivåer av navigation properties.

```csharp
context.Customers
    .Include(c => c.Orders)
    .ThenInclude(o => o.OrderItems)
    .ToList();
```

## Concurrency Conflict
När två användare försöker uppdatera samma data samtidigt. EF Core kastar `DbUpdateConcurrencyException`.

## Shadow Property
Egenskap som EF Core skapar i databasen men inte finns i C#-klassen. Exempel: `CustomerId` i Order om du inte deklarerar den explicit.

## Global Query Filter
Filter som automatiskt appliceras på alla queries för en entity. Används för mjuk radering eller multi-tenant.

```csharp
modelBuilder.Entity<Product>()
    .HasQueryFilter(p => !p.IsDeleted);
```

## Owned Entity
Entitet som inte har egen identitet och inte kan existera utan sin ägare. Exempel: `Address` som alltid tillhör en `Customer`.

## Raw SQL
Möjlighet att köra rå SQL i EF Core. Säkert med parameterized queries, riskabelt med strängkonkatenering.

```csharp
context.Products.FromSql($"SELECT * FROM Products WHERE Price > {minPrice}");
```
