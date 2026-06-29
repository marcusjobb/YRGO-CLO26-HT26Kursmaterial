---
title: Fritt databasprojekt – din data, din app, din lagring
author: Marcus Ackre Medina
type: exam
topic: databases
difficulty: 3
language: csharp
status: new
marcus_voice: true
description: "Ett fritt projekt där du väljer ämne, frontend och databasbackend. Bygg ett fullständigt CRUD-system med SQLite (SQL), LocalDB (Entity Framework) eller online JSON (MongoDB) – precis som i arbetslivet där du måste välja rätt verktyg för jobbet."
tags: ["databases", "crud", "sqlite", "entity-framework", "mongodb"]
---

# Fritt databasprojekt – din data, din app, din lagring

🔴

**Intro:**
Du ska bygga ett eget databasprogram från scratch. Du väljer vad det ska göra, hur gränssnittet ser ut och vilken databaslösning som passar bäst. Det här är så det fungerar i arbetslivet – du får ett problem och måste själv välja verktyg, arkitektur och design.

## Du väljer ämne

Välj **ett** ämne som intresserar dig. Här är några förslag – eller hitta på något eget.

### Förslag 1 – Familjeträd
Ett program för att registrera och visualisera släktingar. Koppla föräldrar, barn, syskon och partners. Visa trädet på skärmen.

**Tänkbar data:** Person (namn, födelsedatum, dödsdatum), Relation (förälder-barn, partner, syskon), Plats (födelseort, bosättning)

### Förslag 2 – Inköpsregister & kategorisering
Registrera alla dina inköp, kategorisera dem och få koll på ekonomin. Mata in kvitton manuellt eller via en enkel formulärapp.

**Tänkbar data:** Kategori (mat, kläder, nöjen, etc.), Inköp (datum, summa, kategori, butik), Budget (maxbelopp per kategori och månad)

### Förslag 3 – Inkomster & utgifter
En personlig plånbok som spårar alla transaktioner. Se saldo, trender och månadsrapporter.

**Tänkbar data:** Konto (namn, saldo), Transaktion (datum, belopp, typ, kategori, beskrivning), Kategori (lön, hyra, mat, sparande)

### Förslag 4 – Eget ämne
Har du en egen idé? Kör! Godkänn med läraren först så att omfattningen är rimlig.

**Exempel från tidigare studenter:**
- Samling av Pokemon-kort med prisutveckling
- Träningslogg med övningar, set och reps
- Boksamling med betyg och recensioner
- Receptdatabas med ingredienser och näringsvärden

> Poängen är inte ämnet – det är implementationen. Välj något du brinner för!

## Du väljer frontend (måste vara C#)

Du bestämmer hur användaren möter programmet. Det måste vara C#, men alla UI-typer är tillåtna:

| Frontend | När passar det? |
|----------|----------------|
| **Console** | Snabbt och enkelt. Perfekt för CRUD-appar. |
| **WPF** (.NET Framework eller .NET Core) | Windows-desktop med grafiskt gränssnitt. |
| **MAUI** | Samma kod för Windows, macOS, Android, iOS. |
| **Windows Forms** | Klassiskt Windows-gränssnitt, snabbt att bygga. |
| **Avalonia** | Cross-platform som MAUI men mer WPF-likt. |
| **Blazor Hybrid** | Webb-teknik inbäddad i desktop-app. |
| **ASP.NET MVC / Razor Pages** | Webbapp – kräver en webbserver. |

**Viktigt:** Du ska kunna förklara *varför* du valde just din frontend i din reflektion.

## Du väljer backend (en av tre)

Här är kärnan i uppgiften. Du väljer **en** databaslösning och implementerar CRUD mot den.

### Alternativ A – SQLite med rå SQL
```xml
<PackageReference Include="Microsoft.Data.Sqlite" Version="9.0.0" />
```

**Kännetecken:** Lokal filbaserad databas. Du skriver all SQL för hand. Full kontroll.

**Du måste visa att du kan:**
```csharp
using var connection = new SqliteConnection("Data Source=app.db");
connection.Open();

using var command = connection.CreateCommand();
command.CommandText = "SELECT * FROM Products WHERE Price > @minPrice";
command.Parameters.AddWithValue("@minPrice", 100);

using var reader = command.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine($"{reader["Name"]}: {reader["Price"]} kr");
}
```

**Krav:** Parameteriserade queries, främmande nycklar, minst 3 tabeller i 3NF, transaktioner.

### Alternativ B – LocalDB med Entity Framework Core
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.0" />
```

**Kännetecken:** Microsoft SQL Server LocalDB (installeras med Visual Studio). Du använder EF Core med LINQ istället för rå SQL.

**Du måste visa att du kan:**
```csharp
public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MyApp;");
}

// LINQ instead of SQL
var expensive = context.Products
    .Where(p => p.Price > 100)
    .OrderBy(p => p.Name)
    .ToList();
```

**Krav:** Minst 3 DbSet, LINQ-queries (inte `.FromSqlRaw`), relationshantering (`.Include`), migrationer (eller `EnsureCreated`).

### Alternativ C – Online JSON via MongoDB
```xml
<PackageReference Include="MongoDB.Driver" Version="3.0.0" />
```

**Kännetecken:** En NoSQL-databas online (MongoDB Atlas free tier). Data lagras som BSON-dokument (JSON-liknande). Ingen SQL, inga joins – allt är dokument.

**Du måste visa att du kan:**
```csharp
var client = new MongoClient("mongodb+srv://user:pass@cluster.xxxxx.mongodb.net/");
var database = client.GetDatabase("MyApp");
var collection = database.GetCollection<Product>("Products");

var expensive = collection
    .Find(p => p.Price > 100)
    .SortBy(p => p.Name)
    .ToList();
```

**Krav:** Anslutning till MongoDB Atlas (free tier), dokumentdesign (embedded vs referenced), indexering, CRUD med `Find`, `InsertOne`, `ReplaceOne`, `DeleteOne`.

### Valfritt – kombinera
Om du känner dig modig: bygg ett lager (interface + implementation) så att appen kan byta backend utan att UI-koden ändras. Använd dependency injection.

```csharp
public interface IProductRepository
{
    List<Product> GetAll();
    Product GetById(int id);
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
}

public class ProductRepositorySqlite : IProductRepository { /* SQLite */ }
public class ProductRepositoryEF : IProductRepository { /* EF Core */ }
public class ProductRepositoryMongo : IProductRepository { /* MongoDB */ }
```

> Detta är VG-nivå. Men det är också precis så riktiga system byggs.

---

## ✅ Grundkrav (G)

### 1. Databas
- Databasen har minst **3 tabeller/collections** (eller motsvarande)
- Det finns en logisk relation mellan tabellerna
- Data är normaliserad (3NF för SQL, genomtänkt dokumentdesign för MongoDB)
- Du kan skapa, läsa, uppdatera och radera data (CRUD)

### 2. Användargränssnitt
- Användaren kan se all data i en överskådlig lista/tabell
- Användaren kan lägga till nya poster via ett formulär
- Användaren kan uppdatera befintliga poster
- Användaren kan radera poster (med bekräftelse)
- Navigeringen är tydlig och användarvänlig

### 3. Kodkvalitet
- Tydlig klassindelning med SRP (en klass = ett ansvar)
- Meningfulla namn på klasser, metoder och variabler
- Konsistent namngivning (svenska eller engelska – inte både och)
- Felhantering med `try-catch` för användarinput och databasfel
- Inga kompileringsvarningar

### 4. README.md
- Projektbeskrivning
- Screenshots av programmet i aktion (minst 2)
- Installationsinstruktioner (klona, bygg, kör)
- Lista över funktioner
- Kända buggar eller begränsningar

---

## 🌟 VG-krav (välj minst 2)

### 1. Repository-mönster med interface
Skapa ett interface per databasentitet och implementera det. Detta gör att du enkelt kan byta databastyp i framtiden.

```csharp
public interface IRepository<T>
{
    List<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(int id);
}
```

### 2. Avancerad sökning/filtrering
Låt användaren söka på flera fält samtidigt med dynamiska filter.

```
Sök på namn: Kalle
Filtrera på kategori (eller lämna tomt): Mat
Fråån datum (eller lämna tomt): 2025-01-01
Sortera på: Datum (stigande)
```

### 3. Rapporter och statistik
Minst 2 sammansatta rapporter som kombinerar data från flera tabeller. Visa i konsol eller GUI.

**Exempel:**
- "Månadskostnad per kategori" (inköpsregistret)
- "Mest utgifter – topp 5 kategorier" (inkomster/utgifter)
- "Fullständigt släktträd för vald person" (familjeträd)
- "Budget vs verklighet" (inköp + budget)

### 4. Export/Import
Exportera data till CSV eller JSON. Importera från CSV.

```csv
Datum,Kategori,Belopp,Butik
2025-03-01,Mat,450:50,Ica
2025-03-02,Nöjen,299:00,Bio
```

### 5. Databas-seeding
Skapa en klass som fyller databasen med realistisk testdata vid första körning.

```csharp
public class DatabaseSeeder
{
    public void Seed(AppDbContext context)
    {
        if (context.Products.Any()) return; // Redan seedad

        context.Products.AddRange(
            new Product { Name = "Mjölk", Price = 15.50m, Category = "Mat" },
            new Product { Name = "Bröd", Price = 25.00m, Category = "Mat" }
        );
        context.SaveChanges();
    }
}
```

### 6. Dependency Injection
Använd DI för att injicera repositories och tjänster istället för att skapa dem med `new`.

```csharp
// Program.cs
var builder = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IDatabaseConnection, SqliteConnection>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<App>();
    })
    .Build();

builder.Services.GetRequiredService<App>().Run();
```

### 7. Grafisk visualisering
Om du valt WPF/MAUI/WinForms: visa data i diagram, trädstruktur eller färgkodad tabell.

**Exempel:**
- Cirkeldiagram över utgifter per kategori
- Släktträd som faktiskt ritas ut (med linjer mellan noder)
- Budget-stapeldiagram som visar grönt/rött

---

## Arkitekturförslag

Oavsett vilken frontend och backend du väljer, här är en grundstruktur som fungerar:

```
DittProjekt/
├── Models/              # Datamodeller (samma oavsett backend)
│   ├── Product.cs
│   ├── Category.cs
│   └── Purchase.cs
├── Data/                # Databas-logik (byt ut beroende på backend)
│   ├── AppDbContext.cs     # (EF Core)
│   ├── DatabaseConnection.cs  # (SQLite)
│   ├── MongoContext.cs    # (MongoDB)
│   └── Repositories/     # CRUD-operationer
│       ├── IProductRepository.cs
│       ├── ProductRepository.cs
│       ├── ICategoryRepository.cs
│       └── CategoryRepository.cs
├── Services/            # Facade, seeders, export/import
│   ├── AppFacade.cs
│   └── DatabaseSeeder.cs
├── UI/                  # Användargränssnitt
│   └── MainWindow.xaml / ConsoleUI.cs / Pages/
├── Program.cs / App.xaml
└── app.db / appsettings.json
```

---

## Tips och råd

### Innan du börjar koda

1. **Välj ämne** som intresserar dig – du kommer spendera många timmar
2. **Välj backend** baserat på vad du vill lära dig:
   - SQLite = du lär dig SQL ordentligt
   - EF Core = du lär dig ORM och LINQ
   - MongoDB = du lär dig NoSQL och dokumentdatabaser
3. **Skissa dina tabeller/dokument** på papper först
4. **Skapa ett GitHub-repo** och commit:a ofta

### Under utvecklingen

1. Börja med enkel CRUD för en tabell – få det att fungera
2. Lägg till fler tabeller en i taget
3. Skapa relationerna när alla tabeller fungerar
4. Fixa UI-navigeringen (menyer eller GUI-kontroller)
5. Lägg till felhantering
6. Först när grunden är stabil – börja med VG-funktioner

### Fallgropar att undvika

- ❌ All kod i Program.cs eller MainWindow.xaml.cs
- ❌ Hårdkodade databasvägar (`C:\Users\...`)
- ❌ SQL-injektion (använd alltid parametrar)
- ❌ Glömda `using`-satser (anslutningar som inte stängs)
- ❌ En enda commit dagen innan deadline
- ❌ Screenshots som inte fungerar (visa inte tomma fönster)

---

## Inlämning

### Deadline
Se kursplanen för deadline.

**Vid försening:** Säg till senast onsdag (för fredag-deadline). Att säga till på fredag är för sent.

### GitHub-repo
```bash
git clone https://github.com/YRGO-CLO26/assignment-database-[ditt-användarnamn].git
```

Din README.md ska innehålla:
- [ ] Projektbeskrivning och ditt val av ämne
- [ ] Minst 2 screenshots
- [ ] Installations- och körinstruktioner
- [ ] Lista över funktioner (grund + VG)
- [ ] Kända buggar och begränsningar
- [ ] Länk till repositoryt

### Checklista före inlämning

**G:**
- [ ] Databas med ≥3 tabeller och logiska relationer
- [ ] Full CRUD (skapa, läsa, uppdatera, radera)
- [ ] Tydligt användargränssnitt
- [ ] README med screenshots
- [ ] Felhantering

**VG:**
- [ ] Minst 2 VG-funktioner implementerade
- [ ] Repository-mönster med interface (rekommenderas)
- [ ] Väl strukturerad kod
- [ ] Djup reflektion i reflection.md

---

## Reflektionsfrågor

Dessa besvarar du i din `reflection.md`:

### G-frågor
1. Vilket ämne valde du och varför?
2. Vilken databasbackend valde du och varför? Vad var avgörande för ditt val?
3. Vilken frontend valde du och varför passade den ditt projekt?
4. Vad var svårast med att implementera CRUD?
5. Hur hanterar din app felaktig inmatning från användaren?

### VG-frågor
1. Vilka VG-funktioner implementerade du och hur fungerar de?
2. Hur skulle du göra om du ville byta databasbackend (t.ex. från SQLite till EF Core)?
3. Vad skulle du göra annorlunda om du byggde om projektet från scratch?
4. Hur tänker du kring skalbarhet – vad händer när databasen växer till 10 000+ poster?
5. Vilka design patterns använde du och varför?

---

## Resurser

### SQLite
- [Microsoft.Data.Sqlite dokumentation](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/)
- [SQLite dokumentation](https://www.sqlite.org/docs.html)
- [DB Browser for SQLite](https://sqlitebrowser.org/) – grafiskt verktyg

### LocalDB/EF Core
- [Getting started med EF Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli)
- [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- [LINQ Query Examples](https://learn.microsoft.com/en-us/dotnet/csharp/linq/)

### MongoDB
- [MongoDB Atlas (free tier)](https://www.mongodb.com/cloud/atlas/register)
- [MongoDB .NET Driver](https://www.mongodb.com/docs/drivers/csharp/current/)
- [MongoDB CRUD operations](https://www.mongodb.com/docs/manual/crud/)
- [MongoDB dokumentdesign](https://www.mongodb.com/docs/manual/core/data-modeling-introduction/)

### Allmänt
- [Repository Pattern](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)
- [SOLID Principles (SRP)](https://en.wikipedia.org/wiki/Single-responsibility_principle)
- [Facade Pattern](https://refactoring.guru/design-patterns/facade)
- [goblin.tools/ToDo](https://goblin.tools/ToDo) – bryt ner user stories i delsteg

---

*Valet är ditt. Verktygen finns. Nu bygger du.*
