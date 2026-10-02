# LINQ för Databaser — Programmeringstermer

## LINQ (Language Integrated Query)
Språkintegrerad frågeteknik i C# som låter dig skriva SQL-liknande frågor direkt i C#-koden. EF Core översätter LINQ till SQL.

## IEnumerable vs IQueryable
De två huvudgränssnitten för LINQ-frågor:

| Aspekt | IEnumerable | IQueryable |
|--------|-------------|------------|
| Var körs frågan? | I minnet (client-side) | På databasen (server-side) |
| Lazy? | Ja | Ja |
| Filter (Where) | Efter all data laddats | I SQL-satsen |
| Include | Nej (kräver IQueryable) | Ja |

```csharp
// IQueryable — filter skickas till SQL
IQueryable<Product> query = context.Products.Where(p => p.Price > 100);

// ToList() kör frågan — bara billiga produkter över nätet
var result = query.ToList();
```

## Where
Filtrerar en sekvens baserat på ett villkor. SQL: `WHERE`.

```csharp
var products = context.Products.Where(p => p.Price > 100 && p.Stock > 0);
```

## Select
Projicerar varje element till en ny form. Minskar mängden data som returneras.

```csharp
var summaries = context.Products
    .Where(p => p.Price > 100)
    .Select(p => new { p.Name, p.Price })
    .ToList();
// SQL: SELECT Name, Price FROM Products WHERE Price > 100
```

## OrderBy / OrderByDescending
Sorterar en sekvens. SQL: `ORDER BY`.

```csharp
context.Products.OrderBy(p => p.Price).ThenBy(p => p.Name);
```

## FirstOrDefault / First / SingleOrDefault / Single
Hämtar enstaka element.

| Metod | Kastar om ingen match | Kastar om flera matchar |
|-------|----------------------|------------------------|
| `FirstOrDefault` | Nej (returnerar null) | Nej |
| `First` | Ja | Nej |
| `SingleOrDefault` | Nej (returnerar null) | Ja |
| `Single` | Ja | Ja |

Använd `FirstOrDefault` för det mesta. `Single` när du är säker på exakt en träff.

## Any / All / Contains
Kontrollfrågor som returnerar bool.

```csharp
bool hasExpensive = context.Products.Any(p => p.Price > 1000);
bool allInStock = context.Products.All(p => p.Stock > 0);
```

## Count / LongCount
Räknar element. Använd i EF Core för `COUNT(*)` i SQL.

```csharp
int count = context.Products.Count(p => p.CategoryId == 1);
// SQL: SELECT COUNT(*) FROM Products WHERE CategoryId = 1
```

## Sum / Average / Min / Max
Aggregeringsfunktioner. Beräknas på databasen.

```csharp
decimal total = context.Orders.Sum(o => o.Total);
decimal avg = context.Products.Average(p => p.Price);
```

## GroupBy
Grupperar element efter en nyckel. SQL: `GROUP BY`.

```csharp
var stats = context.Products
    .GroupBy(p => p.CategoryId)
    .Select(g => new {
        CategoryId = g.Key,
        Count = g.Count(),
        AvgPrice = g.Average(p => p.Price)
    })
    .ToList();
```

## Join
Slår ihop två sekvenser baserat på en gemensam nyckel.

```csharp
var data = context.Products
    .Join(context.Categories,
        p => p.CategoryId,
        c => c.Id,
        (product, category) => new {
            product.Name,
            Category = category.Name
        })
    .ToList();
```

Använd `Include()` istället för `Join()` när du har navigation properties — enklare och säkrare.

## Skip / Take
Paginering. Hoppa över N element och ta M.

```csharp
int page = 2, size = 20;
var page2 = context.Products
    .OrderBy(p => p.Name)
    .Skip((page - 1) * size)
    .Take(size)
    .ToList();
```

## Deferred Execution (Sen exekvering)
LINQ-frågor körs inte förrän du börjar iterera (`.ToList()`, `.FirstOrDefault()`, `foreach`, etc.). Du kan bygga en fråga steg för steg utan att databasen anropas.

```csharp
var query = context.Products.Where(p => p.Price > 100);  // Ingen SQL
query = query.Where(p => p.CategoryId == 1);              // Fortfarande ingen SQL
query = query.OrderBy(p => p.Name);                       // Fortfarande ingen SQL
var result = query.ToList();                              // NU körs SQL:en
```

## Compiled Queries (EF Core 6+)
Cacha översättningen av LINQ → SQL för återkommande frågor.

```csharp
private static readonly Func<AppDbContext, decimal, List<Product>> _cheapProducts =
    EF.CompileQuery((AppDbContext ctx, decimal maxPrice) =>
        ctx.Products.Where(p => p.Price < maxPrice).ToList());

// Användning — snabbare än ocompiled query
var cheap = _cheapProducts(context, 100);
```
