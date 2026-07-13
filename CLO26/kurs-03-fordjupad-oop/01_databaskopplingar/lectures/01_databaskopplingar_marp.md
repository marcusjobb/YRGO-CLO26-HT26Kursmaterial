---
marp: true
theme: nion-dark
paginate: true
---

# Databaskopplingar i C#

**Kurs:** Fördjupad OOP
**Modul:** 01 — Databaskopplingar

Marcus Ackre Medina · YRGO · CLO26

---

## Vad ska vi lära oss idag?

- **Tre typer av databaser** — fildatabas, minnesdatabas, databasserver
- **ADO.NET** — det manuella sättet att prata med en databas
- **SQL injection** — varför parametrar inte är valfria
- **EF Core** — ORM som sköter mappningen åt dig
- **Migrationer** — versionshantering för ditt databasschema
- **Repository pattern** — ett lager mellan logik och databas
- **ADO.NET vs EF Core** — när väljer du vilket?

---

## Tre typer av databaser

| Typ | Exempel | Data sparas |
|-----|---------|-------------|
| Fildatabas | SQLite | I en fil på disk |
| Minnesdatabas | EF Core InMemory | I RAM — försvinner vid stopp |
| Databasserver | SQL Server, MySQL | På en separat server |

Alla tre nås från C# — men på olika sätt och med olika krav.

---

## Fildatabas — SQLite

SQLite är en enda `.db`-fil på disk.  
Ingen server. Ingen installation. Filen *är* databasen.

```
projekt/
├── Program.cs
├── Models/
└── shop.db        ← hela databasen ligger här
```

**Passar för:** lokalt test, enkla appar, mobila appar, prototyper.

**Passar inte för:** flera användare samtidigt, stora datamängder, produktion i molnet.

---

## Minnesdatabas — InMemory

Datan finns bara i RAM.  
Starta appen → data finns. Stäng appen → data borta.

```csharp
// I konfigurationen
optionsBuilder.UseInMemoryDatabase("TestDb");
```

**Passar för:** enhetstester — inga filer, ingen städning efteråt.

**Passar inte för:** produktion, eller om du behöver data kvar efter omstart.

---

## Databasserver — SQL Server och MySQL

En separat process (eller server) som hanterar datan.  
Appen kopplar upp sig via en connection string.

```
Connection string — SQL Server (lokal):
"Server=localhost;Database=PizzaDb;Trusted_Connection=True;"

Connection string — MySQL:
"Server=localhost;Database=pizzadb;User=root;Password=hemligt123;"
```

**Passar för:** produktionssystem, team-arbete, molnmiljöer.

**Passar inte för:** snabba prototyper — kräver installation och konfiguration.

---

## Vilken databas väljer du?

```
Skriver du ett test?
    → InMemory

Bygger du något litet lokalt, ensam?
    → SQLite

Bygger du något som ska köras i produktion?
    → SQL Server (Azure) eller MySQL
```

Det är inte svårare än så.  
Men du måste *veta* varför du väljer — inte bara kopiera en connection string.

---

## ADO.NET — det manuella lagret

ADO.NET är .NETs inbyggda sätt att prata med en databas.  
Ingen magi. Du styr varje steg.

```
Connection  → öppna anslutningen
Command     → formulera din SQL-fråga
Reader      → läs svaret rad för rad
```

Det är mer kod än EF Core — men du förstår exakt vad som händer.

---

## ADO.NET — flödet i kod

```csharp
// Öppna anslutningen
using var conn = new SqliteConnection("Data Source=shop.db");
conn.Open();

// Formulera frågan
using var cmd = new SqliteCommand("SELECT * FROM Products", conn);

// Läs svaret
using var reader = cmd.ExecuteReader();
while (reader.Read())
{
    // Hämta varje kolumn med namn eller index
    string name = reader.GetString(0);
    decimal price = reader.GetDecimal(1);
    Console.WriteLine($"{name}: {price} kr");
}
```

Varje rad i `while`-loopen = en rad i databasen.

---

## SQL injection — vad som händer utan parametrar

Sofie har byggt en inloggning för Kalles Pizzeria.  
Hon skriver SQL-frågan såhär:

```csharp
// ❌ Farlig — Sofie blandar in användarens input direkt i SQL-strängen
string sql = $"SELECT * FROM Users WHERE Username = '{username}'";
```

En elak besökare skriver in: `' OR '1'='1`

```sql
-- Det som faktiskt körs mot databasen:
SELECT * FROM Users WHERE Username = '' OR '1'='1'
-- Returnerar ALLA användare. Inloggat som alla. Grattis.
```

**Det här kallas SQL injection. Det är en av de vanligaste attackerna mot webbappar.**

---

## SQL injection — rätt sätt med parametrar

```csharp
// ✅ Säker — input skickas separat, tolkas aldrig som SQL
string sql = "SELECT * FROM Users WHERE Username = @username";
using var cmd = new SqliteCommand(sql, conn);
cmd.Parameters.AddWithValue("@username", username);
```

`@username` är en platshållare.  
Databasen ersätter den med värdet — men tolkar det aldrig som SQL-kod.  
`' OR '1'='1` blir en sträng att söka efter, inte en SQL-sats.

**Regeln:** Varje gång du tar emot data från en användare — använd parametrar.

---

## EF Core — vad är en ORM?

**ORM** = Object-Relational Mapping

Du arbetar med C#-objekt.  
EF Core översätter dem till SQL och tillbaka.

```
C#-objekt                     Databas
──────────────────            ──────────────────
Product p = new()             Products-tabellen
{                    ←──→     ┌────┬──────────┬───────┐
    Id = 1,                   │ Id │ Name     │ Price │
    Name = "Margherita",      ├────┼──────────┼───────┤
    Price = 89m               │  1 │ Margh... │  89   │
};                            └────┴──────────┴───────┘
```

Du slipper skriva SQL. EF Core sköter det.

---

## Entity-klassen — din modell

En entity-klass = en C#-klass som mappas till en tabell.  
Konvention: `Id` eller `ProductId` → primärnyckel automatiskt.

```csharp
public class Product
{
    // Primärnyckel — EF Core känner igen Id-konventionen
    public int Id { get; set; }

    // Kolumner — varje property blir en kolumn
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
```

Inga attribut krävs för ett enkelt scenario.  
EF Core läser klassens struktur och skapar tabellen åt dig.

---

## DbContext — bryggan mellan kod och databas

```csharp
public class ShopContext : DbContext
{
    // Varje DbSet<T> = en tabell i databasen
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Byt ut connection string beroende på miljö
        options.UseSqlite("Data Source=shop.db");
    }
}
```

Använd det såhär:

```csharp
using var db = new ShopContext();

// LINQ → EF Core → SQL → tillbaka som C#-objekt
var billigheter = db.Products.Where(p => p.Price < 100).ToList();
```

---

## Code First — från klass till databas

Du skriver koden. EF Core skapar databasen.

```
Steg 1: Skriv dina entity-klasser  (Product, Order, ...)
Steg 2: Skapa DbContext
Steg 3: Skapa en migration
Steg 4: Kör migrationen mot databasen
```

```
Du skriver:               EF Core skapar:
Product.cs          →     CREATE TABLE Products (
                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                              Name TEXT NOT NULL,
                              Price REAL NOT NULL,
                              Stock INTEGER NOT NULL
                          );
```

Du behöver aldrig skriva den SQL:en manuellt.

---

## Migrationer — varför finns de?

Du ändrar en klass. Databasen vet inte om det.  
Migrationer löser det — de är versionshantering för ditt schema.

```
Vecka 1: Product har Id, Name, Price
Vecka 2: du lägger till Stock
Vecka 3: du byter namn på Price till BasePrice
```

Utan migrationer: du ändrar koden och databasen är ur synk.  
Med migrationer: varje ändring dokumenteras och kan köras mot vilken miljö som helst.

---

## Migrationer — hur du kör dem

Du behöver paketet `Microsoft.EntityFrameworkCore.Tools` installerat.

```bash
# Skapa en ny migration (ge den ett beskrivande namn)
dotnet ef migrations add LaggTillStock

# Kör migrationen mot databasen
dotnet ef database update
```

EF Core skapar en fil i `Migrations/`-mappen:

```csharp
// Genereras automatiskt — rör den inte om du inte vet vad du gör
protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.AddColumn<int>(
        name: "Stock",
        table: "Products",
        nullable: false,
        defaultValue: 0);
}
```

---

## Repository pattern — varför?

Just nu är din affärslogik blandad med databaskod.

```csharp
// ❌ Direkt databasanrop spritt i hela applikationen
using var db = new ShopContext();
var products = db.Products.Where(p => p.Stock > 0).ToList();
```

Det gör det svårt att testa och svårt att byta databas.

**Repository pattern:** lägg allt databasprat bakom ett interface.  
Resten av applikationen bryr sig inte om hur datan hämtas — bara att den hämtas.

---

## Repository pattern — IRepository

```csharp
// Interfacet beskriver VAD som kan göras — inte HUR
public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    void Add(Product product);
    void Delete(int id);
    void Save();
}
```

Fördelen: din affärslogik pratar mot interfacet.  
Du kan byta implementation (EF Core → InMemory → mock) utan att ändra logiken.

---

## Repository pattern — konkret implementation

```csharp
// Den faktiska implementationen med EF Core
public class ProductRepository : IProductRepository
{
    private readonly ShopContext _db;

    public ProductRepository(ShopContext db)
    {
        _db = db;
    }

    public IEnumerable<Product> GetAll()
        => _db.Products.ToList();

    public Product? GetById(int id)
        => _db.Products.FirstOrDefault(p => p.Id == id);

    public void Add(Product product)
        => _db.Products.Add(product);

    public void Delete(int id)
    {
        var p = GetById(id);
        if (p is not null) _db.Products.Remove(p);
    }

    public void Save()
        => _db.SaveChanges();
}
```

---

## ADO.NET vs EF Core — när väljer du vad?

| | ADO.NET | EF Core |
|---|---|---|
| Kodmängd | Mycket | Lite |
| Kontroll | Full | Begränsad (men räcker) |
| SQL-kunskaper krävs | Ja | Nej (men rekommenderas) |
| SQL injection-risk | Ja, om du slarvar | Inbyggt skydd |
| Lämplig för | Prestandakritiska frågor, komplexa SQL | Vardaglig CRUD |
| Migrationer | Manuellt | Inbyggt |

**Tumregel:**  
Bygg med EF Core. Lär dig ADO.NET för att förstå vad som händer under huven.

---

## Sammanfattning

- ✅ **Fildatabas** (SQLite) — fil på disk, enkel, för lokalt bruk
- ✅ **Minnesdatabas** (InMemory) — finns i RAM, perfekt för tester
- ✅ **Databasserver** (SQL Server/MySQL) — för produktion och team
- ✅ **ADO.NET** — manuellt flöde: Connection → Command → Reader
- ✅ **Parametrar** — skydd mot SQL injection, alltid obligatoriskt
- ✅ **EF Core** — ORM som mappar objekt till tabeller automatiskt
- ✅ **Code First** — du skriver klasser, EF Core skapar schema
- ✅ **Migrationer** — `dotnet ef migrations add` + `dotnet ef database update`
- ✅ **Repository pattern** — interface separerar logik från databaskod

**Nästa gång:** Designmönster — Factory, Singleton och Strategy i praktiken
