# Entity Framework Core — Introduktion och Grunder

## Vad är Entity Framework Core?

Entity Framework Core (EF Core) är Microsofts moderna ORM (Object-Relational Mapper) för .NET. Det låter dig arbeta med en databas genom att använda C#-objekt — utan att behöva skriva SQL-kod för de flesta operationer.

EF Core är:
- **Open source** (MIT-licens)
- **Cross-platform** (Windows, Linux, macOS)
- **Lightweight** jämfört med tidigare Entity Framework-versioner
- **Extensible** — byt databas med en rad kod

## ORM — Varför?

Innan ORM kom var databasåtkomst i C# en manuell process:

1. Öppna en anslutning (`SqlConnection`)
2. Skapa ett kommando (`SqlCommand` med SQL-sträng)
3. Kör och läs resultat (`SqlDataReader`)
4. Mappa resultatet till C#-objekt (manuell loop)
5. Stäng anslutningen

En ORM automatiserar steg 2-4. Du definierar mappningen en gång, sen jobbar du med objekt.

```csharp
// Utan ORM (ADO.NET) — ~10 rader per query
var customers = new List<Customer>();
using var conn = new SqlConnection(connectionString);
using var cmd = new SqlCommand("SELECT * FROM Customer", conn);
conn.Open();
using var reader = cmd.ExecuteReader();
while (reader.Read())
{
    customers.Add(new Customer
    {
        Id = (int)reader["Id"],
        Name = (string)reader["Name"]
    });
}

// Med EF Core — 1 rad
var customers = context.Customers.ToList();
```

## Grundläggande Koncept

### Entity

En entity är en C#-klass som mappas till en databastabell. Varje instans av klassen = en rad i tabellen.

```csharp
public class Customer
{
    public int Id { get; set; }           // Primary Key (konvention)
    public string Name { get; set; }       // Kolumn (NOT NULL)
    public string Email { get; set; }      // Kolumn
    public ICollection<Order> Orders { get; set; }  // Navigation property
}
```

**Konventioner för Primary Key:**
- Egenskap som heter `Id` eller `{ClassName}Id` (t.ex. `CustomerId`)
- `int` → autoincrement
- `string` → ingen autoincrement

### DbContext

DbContext representerar en session med databasen. Den är bryggan mellan dina C#-klasser och databastabellerna.

```csharp
public class AppDbContext : DbContext
{
    // DbSet = tabell
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API-konfiguration här
    }
}
```

**Viktigt om DbContext:**
- Den är **inte trådsäker** — skapa en ny per request/operation
- Den är **lätt** — skapa, använd, kasta
- Använd `using` för att säkerställa att den stängs

### DbSet

`DbSet<T>` representerar en tabell i databasen. Du använder den för att:
- **Fråga:** `context.Customers.Where(c => c.Name == "Anna")`
- **Lägga till:** `context.Customers.Add(new Customer())`
- **Ta bort:** `context.Customers.Remove(customer)`
- **Uppdatera:** Ändra property på en trackad entitet

### Migrationer

Migrationer är EF Cores sätt att versionshantera databasens struktur. Varje migration är en C#-klass som beskriver vad som ska ändras (och hur man ångrar det).

```bash
# Skapa migration
Add-Migration AddEmailToCustomer    # Package Manager Console
dotnet ef migrations add AddEmailToCustomer  # .NET CLI

# Applicera
Update-Database                     # PMC
dotnet ef database update           # .NET CLI

# Ta bort senaste migration (inte applicerad)
Remove-Migration                    # PMC
dotnet ef migrations remove         # .NET CLI
```

**Vanliga kommandon:**

| Kommando | Vad det gör |
|----------|-------------|
| `Add-Migration Namn` | Skapar en migration baserat på ändringar i entities |
| `Update-Database` | Applicerar migrationer på databasen |
| `Update-Database FöregåendeNamn` | Rullar tillbaka till en specifik migration |
| `Remove-Migration` | Tar bort senaste migrationen (om den inte är applicerad) |
| `Script-Migration` | Genererar SQL-skript istället för att köra direkt |
| `Get-Migration` | Listar alla migrationer |

**Migrationer i team:**
1. Glöm aldrig att köra `Update-Database` efter `git pull`
2. Ändra aldrig en migration som redan är pushad — skapa en ny istället
3. Om du får merge-konflikt: ta bort din migration, pulla, skapa på nytt

## Code First vs Database First

| Strategi | Beskrivning | När använda |
|----------|-------------|-------------|
| **Code First** | Du skriver C#-klasser → EF Core skapar databasen | Nytt projekt, full kontroll |
| **Database First** | Du har en befintlig databas → EF Core genererar klasser | Existerande databas |
| **Model First** | Du ritar ett diagram → EF Core skapar både klasser och databas | Visual Studio-användare |

I den här kursen använder vi **Code First** — det ger dig mest kontroll och är bäst lämpat för moderna .NET-applikationer.

## Livscykel för en DbContext-instans

```
Skapa DbContext → Gör operationer → Spara → Kasta
```

```csharp
// KORT livscykel (rekommenderat)
public List<Product> GetCheapProducts()
{
    using var context = new AppDbContext();
    return context.Products
        .Where(p => p.Price < 100)
        .AsNoTracking()
        .ToList();
}  // context.Dispose() här — anslutningen släpps
```

```csharp
// LÅNG livscykel (undvik om möjligt)
var context = new AppDbContext(); // Öppen i timmar...
// Kan leda till gamla data, minnesläckor, prestandaproblem
```

## Konfigurationskällor

EF Core kan konfigureras på flera sätt:

1. **OnConfiguring:** Enkel, hårdkodad (bra för labs)
2. **appsettings.json:** Connection string i konfigurationsfil
3. **Dependency Injection:** Via Startup/Program.cs (bäst för ASP.NET)

```csharp
// ASP.NET Core — rekommenderat sätt
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
```

## Sammanfattning

- EF Core = Microsofts ORM för .NET
- Entity = C#-klass → databastabell
- DbContext = bryggan mellan kod och databas
- DbSet = tabeller som du kan fråga
- Migrationer = versionshantering för databasschema
- Code First = din kod definierar databasen
- Skapa DbContext per operation, kasta efter användning
