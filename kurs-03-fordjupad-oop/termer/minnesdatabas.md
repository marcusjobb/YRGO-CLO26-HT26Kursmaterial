# Minnesdatabas — Programmeringstermer

## Minnesdatabas
En databas som lagrar all data i RAM istället för på disk. Extremt snabb — men all data försvinner när processen avslutas eller servern startas om.

Kallas även: **in-memory database**.

## Varför existerar minnesdatabaser?

Tre vanliga användningsfall:

| Användningsfall | Exempel |
|----------------|---------|
| Caching | Spara resultatet av tunga databasanrop |
| Testisolering | Testa databaskod utan riktig databas |
| Session-lagring | Håll inloggningsstatus för webbanvändare |

## EF Core InMemory Provider

Microsoft tillhandahåller en in-memory provider för EF Core som används framförallt i **tester**.

```csharp
// Installera: dotnet add package Microsoft.EntityFrameworkCore.InMemory

// I testprojektet
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseInMemoryDatabase("TestDb")
    .Options;

using var context = new AppDbContext(options);
context.Products.Add(new Product { Name = "Laptop", Price = 9999 });
context.SaveChanges();

// Testet kör mot minnet — ingen riktig databas behövs
```

> **OBS:** EF Core InMemory beter sig inte alltid som en riktig SQL-databas. Transaktioner och constraints fungerar inte på samma sätt. För mer realistiska tester — använd SQLite `:memory:` istället.

## SQLite i minnesläge

SQLite kan köras helt i minnet med connection string `Data Source=:memory:`:

```csharp
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite("Data Source=:memory:")
    .Options;
```

Beter sig som en riktig SQL-databas (constraints, transaktioner fungerar) men utan filsystemet. Bra kompromiss för tester.

## Redis

Populär fristående minnesdatabas/cache-server. Används i produktionssystem för att avlasta den primära databasen.

```
[Webb-API] ──→ Redis (cache, snabb) ──→ returnerar cachad data
              ↓ (om inte cachad)
           SQL Server (långsammare, auktoritativ)
```

```csharp
// Installera: dotnet add package StackExchange.Redis
var redis = ConnectionMultiplexer.Connect("localhost:6379");
var db = redis.GetDatabase();

db.StringSet("user:42:name", "Anna Svensson", TimeSpan.FromMinutes(10));
var name = db.StringGet("user:42:name");
```

## Datapermanens — vad händer vid omstart?

```
Minnesdatabas:    [Omstart] → All data borta ⚠️
Fildatabas:       [Omstart] → Data finns kvar ✓
Databasserver:    [Omstart] → Data finns kvar ✓
```

Redis kan konfigureras för persistens (snapshots eller append-only log), men det är undantaget, inte regeln.

## När ska du välja minnesdatabas?

- **Tester** — snabb isolering, ingen städning efteråt
- **Caching** — saker som är dyra att räkna om men OK att förlora
- **Temporär data** — sessioner, köer, rate-limiting
- **Aldrig** — för data du inte har råd att förlora
