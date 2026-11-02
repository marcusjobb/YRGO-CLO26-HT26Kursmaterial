# 🐾 Entity Framework 8 – Djursjukhus

🟢


En workshop där du bygger ett litet **djursjukhus-system** i C# och EF Core 8.
Du lär dig modellera relationer, köra CRUD-operationer och skapa ett program som både **lagrar** och **listar** djur och deras ägare.

**Varför just djursjukhus?** Relationen mellan ägare och djur är perfekt för att förstå One-to-Many relationer – en ägare kan ha flera djur, men varje djur har bara en ägare. Precis som i verkliga systemet!

---

## 🎯 Mål

Efter övningen ska du kunna:

- Skapa modeller för **ägare** och **djur**
- Använda EF Core 8 (Code First) för att generera databasen
- Köra CRUD (Create, Read, Update, Delete) på båda tabellerna
- Koppla ihop tabeller via relationer
- Visa resultat i konsolen
- Fråga användaren efter data och visa resultat dynamiskt

---

## 🧱 Del 1 – Projektstruktur (10 min)

### Varför struktur?

En tydlig mappstruktur gör programmet lätt att underhålla. När projektet växer vill du inte leta efter filer i kaos!

**Struktur vi ska bygga:**
```
Djursjukhus/
├── Database/          # DbContext ligger här
├── Models/           # Entiteter (Owner, Animal)
├── Controllers/      # CRUD-logik
└── Program.cs        # Menylogik
```

### Steg 1: Skapa projekt

```bash
dotnet new console -n Djursjukhus
cd Djursjukhus
```

### Steg 2: Lägg till EF Core-paket

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

**Vad är Tools-paketet?** Det innehåller `dotnet ef` kommandona för migrationer. Utan det kan du inte köra `dotnet ef migrations add`.

💡 **SQLite istället?** Byt `SqlServer` mot `Sqlite` och använd `UseSqlite()` i DbContext. Perfekt för utveckling!

### Steg 3: Skapa mappar

```bash
mkdir Database Models Controllers
```

**Verifiera strukturen:**
```bash
ls
```

Du ska se: `Database/`, `Models/`, `Controllers/`, `Program.cs`, `Djursjukhus.csproj`

---

## 🐶 Del 2 – Skapa modeller (15 min)

### Varför börja med modeller?

Modellerna är **hjärtat i EF Core** – de definierar vad databasen ska innehålla. Tänk på dem som ritningar för dina databastabeller.

### Steg 1: Skapa Owner (Ägare)

Skapa `Models/Owner.cs`:

```csharp
namespace Djursjukhus.Models;

public class Owner
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";

    // ✅ En ägare kan ha flera djur
    public List<Animal> Animals { get; set; } = new();
}
```

**Vad händer här?**
- `Id` blir primärnyckel automatiskt (EF Core konvention)
- `List<Animal>` är en **navigation property** – visar relationen till djur
- `= new()` initierar listan så du slipper null-problem

### Steg 2: Skapa Animal (Djur)

Skapa `Models/Animal.cs`:

```csharp
namespace Djursjukhus.Models;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Species { get; set; } = "";
    public int Age { get; set; }

    // ✅ Ett djur tillhör en ägare
    public int OwnerId { get; set; }
    public Owner? Owner { get; set; }
}
```

**Vad händer här?**
- `OwnerId` är **foreign key** – pekar på ägarens Id
- `Owner?` är navigation property (nullable med `?` eftersom EF fyller den vid Include)

### Relationen förklarad

```
Owner (1) -----> (N) Animal
  Id                  Id
  Name                Name
  Animals             OwnerId  ← Foreign Key
```

**One-to-Many:** En ägare kan ha många djur, men varje djur har bara en ägare. EF Core förstår detta genom:
1. `List<Animal>` i Owner
2. `OwnerId` + `Owner?` i Animal

---

## ⚙️ Del 3 – Databasklass (20 min)

### Varför DbContext?

`DbContext` är din **portal till databasen**. Den:
- Översätter C#-kod till SQL
- Håller koll på ändringar (Change Tracking)
- Hanterar connections och transaktioner

### Steg 1: Skapa HospitalContext

Skapa `Database/HospitalContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using Djursjukhus.Models;

namespace Djursjukhus.Database;

public class HospitalContext : DbContext
{
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Animal> Animals => Set<Animal>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Djursjukhus;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Definiera relationen explicit
        modelBuilder.Entity<Owner>()
            .HasMany(o => o.Animals)
            .WithOne(a => a.Owner)
            .HasForeignKey(a => a.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Valfria index för bättre prestanda
        modelBuilder.Entity<Owner>().HasIndex(o => o.Name);
        modelBuilder.Entity<Animal>().HasIndex(a => a.Name);
    }
}
```

**Vad händer här?**

**DbSet<T>:**
- `Owners` och `Animals` representerar tabeller
- Du kan köra LINQ-queries mot dem: `context.Owners.Where(...)`

**OnConfiguring:**
- `UseSqlServer()` väljer SQL Server LocalDB
- Connection string pekar på lokal databas "Djursjukhus"

**OnModelCreating:**
- `HasMany().WithOne()` definierar 1:N relation
- `HasForeignKey()` specificerar vilken kolumn som är FK
- `OnDelete(Cascade)` = när ägare raderas, raderas djuren också
- `HasIndex()` = snabbare sökningar på Name-kolumnen

### Steg 2: Skapa databasen med migrationer

```bash
dotnet ef migrations add InitialCreate
```

**Vad händer?**
EF Core analyserar dina modeller och skapar en migration-fil i `Migrations/` med SQL-kommandon för att skapa tabellerna.

```bash
dotnet ef database update
```

**Vad händer?**
EF Core kör SQL-kommandona och skapar databasen med två tabeller: `Owners` och `Animals`.

**Verifiera att det fungerade:**

Öppna SQL Server Object Explorer i Visual Studio (eller Azure Data Studio) och kolla att databasen `Djursjukhus` finns med två tabeller.

---

## 🧪 Del 4 – Skapa testdata (10 min)

### Varför testdata?

Det är tråkigt att testa med en tom databas! Lägg in lite startdata så du kan börja experimentera direkt.

### Steg 1: Seed data i Program.cs

```csharp
using Djursjukhus.Database;
using Djursjukhus.Models;

using var db = new HospitalContext();

// Kolla om databasen är tom
if (!db.Owners.Any())
{
    var anna = new Owner
    {
        Name = "Anna Andersson",
        Phone = "070-1234567",
        Animals = new List<Animal>
        {
            new() { Name = "Misse", Species = "Katt", Age = 3 },
            new() { Name = "Fido", Species = "Hund", Age = 5 }
        }
    };

    var björn = new Owner
    {
        Name = "Björn Bengtsson",
        Phone = "070-7654321",
        Animals = new List<Animal>
        {
            new() { Name = "Pelle", Species = "Hamster", Age = 1 }
        }
    };

    db.Owners.AddRange(anna, björn);
    db.SaveChanges();

    Console.WriteLine("✅ Testdata skapad – 2 ägare, 3 djur");
}
else
{
    Console.WriteLine("ℹ️ Databasen har redan data");
}
```

**Vad händer här?**

1. `db.Owners.Any()` kollar om det finns ägare
2. Om nej, skapas två ägare med djur
3. `AddRange()` lägger till flera objekt på en gång
4. `SaveChanges()` skriver till databasen

**Magiskt moment:** När du lägger till en Owner med Animals, skapar EF Core BÅDE ägaren OCH djuren automatiskt. Du behöver inte manuellt sätta OwnerId!

**Kör programmet:**
```bash
dotnet run
```

Du ska se: `✅ Testdata skapad – 2 ägare, 3 djur`

---

## 📋 Del 5 – Visa listor med Include() (15 min)

### Varför Include()?

**Problem:** Om du bara hämtar `Owners.ToList()`, blir `Animals`-listan tom! EF Core laddar inte relationer automatiskt.

**Lösning:** Använd `Include()` för att explicit ladda relaterade data.

### Steg 1: Lista alla ägare med djur

Lägg till i Program.cs (efter seeding):

```csharp
using Microsoft.EntityFrameworkCore;

// Lista alla ägare i bokstavsordning
Console.WriteLine("\n--- Ägare och deras djur ---\n");

foreach (var owner in db.Owners
                        .Include(o => o.Animals)
                        .OrderBy(o => o.Name))
{
    Console.WriteLine($"👤 {owner.Name} ({owner.Phone})");

    if (owner.Animals.Any())
    {
        foreach (var animal in owner.Animals)
            Console.WriteLine($"   🐾 {animal.Name} ({animal.Species}, {animal.Age} år)");
    }
    else
    {
        Console.WriteLine("   (Inga djur registrerade)");
    }
}
```

**Vad händer här?**

1. `Include(o => o.Animals)` laddar djuren för varje ägare
2. `OrderBy(o => o.Name)` sorterar alfabetiskt
3. `owner.Animals.Any()` kollar om ägaren har djur

**Förväntad output:**
```
--- Ägare och deras djur ---

👤 Anna Andersson (070-1234567)
   🐾 Misse (Katt, 3 år)
   🐾 Fido (Hund, 5 år)
👤 Björn Bengtsson (070-7654321)
   🐾 Pelle (Hamster, 1 år)
```

### Experiment: Ta bort Include()

Kommentera ut `.Include(o => o.Animals)` och kör igen. Vad händer?

<details>
<summary>Svar (klicka för att visa)</summary>

Animals-listan blir tom för alla ägare! Du får:
```
👤 Anna Andersson (070-1234567)
   (Inga djur registrerade)
```

**Varför?** EF Core använder **lazy loading avstängt** by default. Du måste explicit säga att du vill ha relaterade data med `Include()`.
</details>

---

## ✏️ Del 6 – CRUD via Controller (30 min)

### Varför Controller-klasser?

**Problem:** Om all CRUD-logik ligger i Program.cs blir det snabbt kaos.

**Lösning:** Separera ansvar – Controllers hanterar dataoperationer, Program.cs hanterar UI.

### Steg 1: Skapa OwnerController

Skapa `Controllers/OwnerController.cs`:

```csharp
using Djursjukhus.Database;
using Djursjukhus.Models;
using Microsoft.EntityFrameworkCore;

namespace Djursjukhus.Controllers;

public class OwnerController
{
    private readonly HospitalContext _db = new();

    // CREATE
    public void AddOwner(string name, string phone)
    {
        var owner = new Owner { Name = name, Phone = phone };
        _db.Owners.Add(owner);
        _db.SaveChanges();
        Console.WriteLine($"✅ Ägaren {name} skapades med ID {owner.Id}");
    }

    // READ
    public void ListOwners()
    {
        Console.WriteLine("\n--- Alla ägare ---");
        foreach (var o in _db.Owners.Include(o => o.Animals))
        {
            Console.WriteLine($"{o.Id}. {o.Name} ({o.Phone}) – {o.Animals.Count} djur");
        }
    }

    // UPDATE
    public void UpdateOwnerPhone(int id, string newPhone)
    {
        var owner = _db.Owners.Find(id);
        if (owner == null)
        {
            Console.WriteLine("❌ Ägare hittades inte");
            return;
        }

        owner.Phone = newPhone;
        _db.SaveChanges();
        Console.WriteLine($"✅ {owner.Name}s telefon uppdaterad till {newPhone}");
    }

    // DELETE
    public void DeleteOwner(int id)
    {
        var owner = _db.Owners.Include(o => o.Animals).FirstOrDefault(o => o.Id == id);
        if (owner == null)
        {
            Console.WriteLine("❌ Ägare hittades inte");
            return;
        }

        var animalCount = owner.Animals.Count;
        _db.Owners.Remove(owner);
        _db.SaveChanges();
        Console.WriteLine($"✅ {owner.Name} (och {animalCount} djur) raderades");
    }
}
```

**Vad händer här?**

**CREATE:**
- `Add()` markerar entiteten för insättning
- `SaveChanges()` kör INSERT i databasen
- Efter SaveChanges fylls `owner.Id` automatiskt!

**READ:**
- `Include()` hämtar djuren
- `Animals.Count` visar hur många djur ägaren har

**UPDATE:**
- `Find(id)` hittar ägaren (snabbt, använder primärnyckel)
- Ändra property direkt – ingen `Update()` behövs!
- `SaveChanges()` kör UPDATE i databasen

**DELETE:**
- `Remove()` markerar för radering
- Eftersom vi har `OnDelete(Cascade)` raderas djuren automatiskt!

### Steg 2: Testa controllern

I Program.cs (lägg till efter seeding-koden):

```csharp
using Djursjukhus.Controllers;

var ownerCtrl = new OwnerController();

// Testa CREATE
ownerCtrl.AddOwner("Cecilia Carlsson", "070-1111111");

// Testa READ
ownerCtrl.ListOwners();

// Testa UPDATE
ownerCtrl.UpdateOwnerPhone(1, "070-9999999");

// Testa DELETE (radera ID 3)
ownerCtrl.DeleteOwner(3);

// Visa resultatet
ownerCtrl.ListOwners();
```

**Förväntad output:**
```
✅ Ägaren Cecilia Carlsson skapades med ID 3

--- Alla ägare ---
1. Anna Andersson (070-1234567) – 2 djur
2. Björn Bengtsson (070-7654321) – 1 djur
3. Cecilia Carlsson (070-1111111) – 0 djur

✅ Anna Anderssons telefon uppdaterad till 070-9999999
✅ Cecilia Carlsson (och 0 djur) raderades

--- Alla ägare ---
1. Anna Andersson (070-9999999) – 2 djur
2. Björn Bengtsson (070-7654321) – 1 djur
```

---

## 🐕 Del 7 – CRUD för djur (20 min)

### Steg 1: Skapa AnimalController

Skapa `Controllers/AnimalController.cs`:

```csharp
using Djursjukhus.Database;
using Djursjukhus.Models;
using Microsoft.EntityFrameworkCore;

namespace Djursjukhus.Controllers;

public class AnimalController
{
    private readonly HospitalContext _db = new();

    // CREATE
    public void AddAnimal(string name, string species, int age, int ownerId)
    {
        // Kolla att ägaren finns
        if (!_db.Owners.Any(o => o.Id == ownerId))
        {
            Console.WriteLine("❌ Ägare med det ID:t finns inte");
            return;
        }

        var animal = new Animal
        {
            Name = name,
            Species = species,
            Age = age,
            OwnerId = ownerId
        };

        _db.Animals.Add(animal);
        _db.SaveChanges();
        Console.WriteLine($"✅ {name} har lagts till");
    }

    // READ
    public void ListAnimals()
    {
        Console.WriteLine("\n--- Alla djur ---");
        foreach (var a in _db.Animals.Include(a => a.Owner).OrderBy(a => a.Name))
        {
            Console.WriteLine($"{a.Id}. {a.Name} ({a.Species}, {a.Age} år) – Ägare: {a.Owner?.Name ?? "Okänd"}");
        }
    }

    // UPDATE
    public void UpdateAnimalAge(int id, int newAge)
    {
        var animal = _db.Animals.Find(id);
        if (animal == null)
        {
            Console.WriteLine("❌ Djur hittades inte");
            return;
        }

        animal.Age = newAge;
        _db.SaveChanges();
        Console.WriteLine($"✅ {animal.Name} är nu {newAge} år gammal");
    }

    // DELETE
    public void DeleteAnimal(int id)
    {
        var animal = _db.Animals.Find(id);
        if (animal == null)
        {
            Console.WriteLine("❌ Djur hittades inte");
            return;
        }

        var name = animal.Name;
        _db.Animals.Remove(animal);
        _db.SaveChanges();
        Console.WriteLine($"✅ {name} har tagits bort");
    }
}
```

**Vad händer här?**

**Viktigt i CREATE:**
- Vi kollar att ägaren finns innan vi skapar djuret
- Annars får vi foreign key constraint error från databasen!

**Viktigt i READ:**
- `a.Owner?.Name ?? "Okänd"` använder null-coalescing
- Om Owner är null (vilket inte ska hända, men säkert är säkert), visas "Okänd"

### Steg 2: Testa AnimalController

I Program.cs:

```csharp
var animalCtrl = new AnimalController();

// Testa CREATE
animalCtrl.AddAnimal("Kalle", "Fisk", 2, 1);  // Annas fisk

// Testa READ
animalCtrl.ListAnimals();

// Testa UPDATE
animalCtrl.UpdateAnimalAge(1, 4);  // Misse blir 4 år

// Testa DELETE
animalCtrl.DeleteAnimal(3);  // Radera Pelle
```

---

## 💬 Del 8 – Interaktiv meny (20 min)

### Steg 1: Bygg meny i Program.cs

Nu kombinerar vi allt till ett interaktivt program!

```csharp
using Djursjukhus.Controllers;

var ownerCtrl = new OwnerController();
var animalCtrl = new AnimalController();

bool running = true;
while (running)
{
    Console.WriteLine("\n========== DJURSJUKHUS ==========");
    Console.WriteLine("1. Lista ägare");
    Console.WriteLine("2. Lista djur");
    Console.WriteLine("3. Lägg till ägare");
    Console.WriteLine("4. Lägg till djur");
    Console.WriteLine("5. Uppdatera telefon");
    Console.WriteLine("6. Ta bort ägare");
    Console.WriteLine("0. Avsluta");
    Console.WriteLine("=================================");
    Console.Write("Välj > ");

    switch (Console.ReadLine())
    {
        case "1":
            ownerCtrl.ListOwners();
            break;

        case "2":
            animalCtrl.ListAnimals();
            break;

        case "3":
            Console.Write("Namn: ");
            var name = Console.ReadLine() ?? "";
            Console.Write("Telefon: ");
            var phone = Console.ReadLine() ?? "";
            ownerCtrl.AddOwner(name, phone);
            break;

        case "4":
            Console.Write("Djur-namn: ");
            var animalName = Console.ReadLine() ?? "";
            Console.Write("Art (t.ex. Hund, Katt): ");
            var species = Console.ReadLine() ?? "";
            Console.Write("Ålder: ");
            int.TryParse(Console.ReadLine(), out int age);

            ownerCtrl.ListOwners();  // Visa tillgängliga ägare
            Console.Write("Välj ägar-ID: ");
            int.TryParse(Console.ReadLine(), out int ownerId);

            animalCtrl.AddAnimal(animalName, species, age, ownerId);
            break;

        case "5":
            ownerCtrl.ListOwners();
            Console.Write("Välj ägar-ID: ");
            int.TryParse(Console.ReadLine(), out int updateId);
            Console.Write("Ny telefon: ");
            var newPhone = Console.ReadLine() ?? "";
            ownerCtrl.UpdateOwnerPhone(updateId, newPhone);
            break;

        case "6":
            ownerCtrl.ListOwners();
            Console.Write("Välj ägar-ID att radera: ");
            int.TryParse(Console.ReadLine(), out int deleteId);
            ownerCtrl.DeleteOwner(deleteId);
            break;

        case "0":
            Console.WriteLine("Hejdå! 👋");
            running = false;
            break;

        default:
            Console.WriteLine("❌ Ogiltigt val");
            break;
    }
}
```

**Kör programmet:**
```bash
dotnet run
```

Nu har du ett fullt fungerande CRUD-system!

---

## 🔍 Del 9 – Sökning med LINQ (15 min)

### Varför sökning?

När databasen växer vill du inte lista ALLT – du vill hitta specifika ägare eller djur snabbt.

### Steg 1: Lägg till sökmetod i OwnerController

```csharp
public void SearchOwner(string searchTerm)
{
    var results = _db.Owners
        .Include(o => o.Animals)
        .Where(o => o.Name.Contains(searchTerm))
        .ToList();

    if (!results.Any())
    {
        Console.WriteLine($"❌ Ingen ägare matchar '{searchTerm}'");
        return;
    }

    Console.WriteLine($"\n--- Sökresultat för '{searchTerm}' ---");
    foreach (var owner in results)
    {
        Console.WriteLine($"👤 {owner.Name} ({owner.Phone})");
        foreach (var animal in owner.Animals)
            Console.WriteLine($"   🐾 {animal.Name} ({animal.Species})");
    }
}
```

**Vad händer här?**

- `Contains(searchTerm)` motsvarar SQL `LIKE '%term%'`
- Sökningen är case-sensitive (använd `.ToLower()` för case-insensitive)

### Steg 2: Lägg till i menyn

```csharp
case "7":
    Console.Write("Sök ägare: ");
    var search = Console.ReadLine() ?? "";
    ownerCtrl.SearchOwner(search);
    break;
```

**Testa:**
- Sök "Anna" → hittar Anna Andersson
- Sök "Berg" → hittar Björn Bengtsson (om han heter Bengtsson-Berg!)
- Sök "xyz" → hittar ingenting

---

## 🐛 Troubleshooting (Vanliga problem)

### Problem 1: "A connection was successfully established, but then an error occurred"

**Orsak:** SQL Server LocalDB kör inte eller är inte installerat.

**Lösning:**
1. Kolla att LocalDB är installerat: `sqllocaldb info`
2. Starta LocalDB: `sqllocaldb start MSSQLLocalDB`
3. Eller byt till SQLite:
   ```csharp
   dotnet add package Microsoft.EntityFrameworkCore.Sqlite

   // I OnConfiguring:
   options.UseSqlite("Data Source=hospital.db");
   ```

### Problem 2: "Foreign key constraint failed"

**Orsak:** Du försöker skapa ett djur med `OwnerId` som inte finns.

**Lösning:**
- Kolla alltid att ägaren finns innan du skapar djur
- Se `AddAnimal()`-metoden som validerar ägaren först

### Problem 3: Animals-listan är tom

**Orsak:** Du glömde `Include(o => o.Animals)`.

**Lösning:**
- Lägg ALLTID till `.Include()` när du behöver relaterade data
- Utan Include får du bara parent-entiteten (Owner), inte children (Animals)

### Problem 4: "The instance of entity type cannot be tracked"

**Orsak:** Du försöker lägga till samma entitet flera gånger.

**Lösning:**
- Skapa en ny `HospitalContext` för varje operation
- Eller använd Dependency Injection (kommer i senare kurser)

### Problem 5: Migration går inte köra

**Orsak:** Projektet kompilerar inte.

**Lösning:**
```bash
dotnet build  # Fixa alla kompileringsfel först!
dotnet ef migrations add FixName
dotnet ef database update
```

---

## 🧠 Sammanfattning

| Koncept                  | Syfte                                       |
| ------------------------ | ------------------------------------------- |
| DbSet<T>                 | Representerar en tabell i databasen         |
| Include()                | Laddar relaterade entiteter (eager loading) |
| HasMany / WithOne        | Definierar 1:N relation med foreign key    |
| OnDelete(Cascade)        | Radera children automatiskt vid parent-radering |
| CRUD                     | Create, Read, Update, Delete operationer    |
| Migration                | Versionshantering av databasschema          |
| Find(id)                 | Snabb sökning på primärnyckel               |
| Contains()               | SQL LIKE-sökning för textfält               |

---

## 🤔 Reflektionsfrågor

### Fråga 1: Include() vs ToList()
Hur skulle du beskriva skillnaden mellan `Include()` och att bara hämta `Owners.ToList()`?

<details>
<summary>Svar (klicka för att visa)</summary>

**Utan Include():**
```csharp
var owners = db.Owners.ToList();
// Alla owners.Animals listor är tomma!
```

**Med Include():**
```csharp
var owners = db.Owners.Include(o => o.Animals).ToList();
// Alla owners.Animals listor innehåller djuren!
```

**Varför?** EF Core använder **lazy loading avstängt** by default. Navigation properties laddas inte automatiskt för att undvika N+1 query-problem. Du måste explicit begära relaterade data med `Include()`.
</details>

### Fråga 2: Cascade Delete
Varför är det bra att använda `OnDelete(DeleteBehavior.Cascade)` i denna modell?

<details>
<summary>Svar (klicka för att visa)</summary>

**Utan Cascade:**
Om du raderar en ägare får du foreign key constraint error – djuren har fortfarande `OwnerId` som pekar på en ägare som inte finns!

**Med Cascade:**
När du raderar en ägare raderas alla tillhörande djur automatiskt. Det är logiskt – ett djur utan ägare ska inte finnas kvar i systemet.

**Alternativ:**
- `SetNull` – Sätter `OwnerId` till null (kräver nullable FK)
- `Restrict` – Tillåter inte radering om djur finns (måste radera djur först)
</details>

### Fråga 3: Utöka modellen
Hur hade du lagt till veterinärbesök eller journaler i databasen?

<details>
<summary>Svar (klicka för att visa)</summary>

**Ny modell: Visit**
```csharp
public class Visit
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Reason { get; set; } = "";
    public decimal Cost { get; set; }

    // Ett besök tillhör ett djur
    public int AnimalId { get; set; }
    public Animal? Animal { get; set; }
}
```

**Uppdatera Animal:**
```csharp
public class Animal
{
    // ... befintliga properties ...

    // Ett djur kan ha många besök
    public List<Visit> Visits { get; set; } = new();
}
```

**Relation:**
```
Animal (1) -----> (N) Visit
```

Ett djur kan ha många besök, men varje besök tillhör ett djur.
</details>

### Fråga 4: Flera telefonnummer
Vad skulle ändras om en ägare kan ha flera telefonnummer?

<details>
<summary>Svar (klicka för att visa)</summary>

**Alternativ 1: Enkel lista (JSON)**
```csharp
public class Owner
{
    // ... andra properties ...
    public List<string> Phones { get; set; } = new();
}
```
Kräver: `options.UseSqlServer(o => o.UseJsonProperty())`

**Alternativ 2: Separat tabell (bättre)**
```csharp
public class PhoneNumber
{
    public int Id { get; set; }
    public string Number { get; set; } = "";
    public string Type { get; set; } = ""; // "Mobil", "Hem", "Arbete"

    public int OwnerId { get; set; }
    public Owner? Owner { get; set; }
}

public class Owner
{
    // ... andra properties ...
    public List<PhoneNumber> PhoneNumbers { get; set; } = new();
}
```

**Varför separat tabell?** Du kan lägga till metadata (typ, primär/sekundär) och söka effektivt.
</details>

### Fråga 5: SQLite istället
Om du byter till SQLite – vad behöver ändras i koden?

<details>
<summary>Svar (klicka för att visa)</summary>

**Två ändringar:**

1. **Paket:**
```bash
dotnet remove package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

2. **OnConfiguring:**
```csharp
protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite("Data Source=hospital.db");
```

**Allt annat:** Modeller, Controllers, LINQ – INGET ändras! Det är kraften med EF Core – samma kod fungerar mot olika databaser.

**Återskapa databasen:**
```bash
rm -rf Migrations/
dotnet ef migrations add InitialCreate
dotnet ef database update
```
</details>

### Fråga 6: Testa Update och Delete
Hur kan du testa att `Update` och `Delete` fungerar utan att krascha programmet?

<details>
<summary>Svar (klicka för att visa)</summary>

**1. Lista innan och efter:**
```csharp
ownerCtrl.ListOwners();  // Visa före
ownerCtrl.DeleteOwner(2);
ownerCtrl.ListOwners();  // Visa efter
```

**2. Kolla med Find() först:**
```csharp
var owner = db.Owners.Find(id);
if (owner != null)
{
    db.Owners.Remove(owner);
    db.SaveChanges();
}
else
{
    Console.WriteLine("Finns inte!");
}
```

**3. Använd SQL-verktyg:**
- SQL Server Object Explorer (Visual Studio)
- Azure Data Studio
- DB Browser for SQLite

Kör operationer och verifiera i verktyget att data ändrats.
</details>

### Fråga 7: Windows-app eller Web API
Vilka förbättringar krävs för att göra detta till en enkel Windows-app eller Web API?

<details>
<summary>Svar (klicka för att visa)</summary>

**Windows Forms/WPF:**
- Byt konsol-meny mot UI-komponenter (knappar, listor)
- Använd DataGridView för att visa data
- Bind Controllers till UI-events
- DbContext blir samma!

**Web API (ASP.NET Core):**
```csharp
[ApiController]
[Route("api/[controller]")]
public class OwnerApiController : ControllerBase
{
    private readonly HospitalContext _db;

    public OwnerApiController(HospitalContext db)
    {
        _db = db;  // Dependency Injection
    }

    [HttpGet]
    public IEnumerable<Owner> GetOwners()
        => _db.Owners.Include(o => o.Animals).ToList();

    [HttpPost]
    public IActionResult AddOwner([FromBody] Owner owner)
    {
        _db.Owners.Add(owner);
        _db.SaveChanges();
        return CreatedAtAction(nameof(GetOwners), new { id = owner.Id }, owner);
    }
}
```

**Viktiga ändringar:**
- Dependency Injection för DbContext
- Return types blir IActionResult/JsonResult
- Felhantering med HTTP status codes
- Autentisering och auktorisering
</details>

---

## 🚀 Utmaningar

### Utmaning 1: Raser
Lägg till en `Breed`-tabell (ras). Ett djur kan ha en ras (Labrador, Perser, etc.).

**Tips:**
- 1:N relation (en ras, många djur)
- Nullable FK (alla djur kanske inte har känd ras)

### Utmaning 2: Veterinärer
Lägg till `Veterinarian` och `Appointment` tabeller. En veterinär kan ha många bokningar, och varje bokning tillhör ett djur.

**Tips:**
- Veterinarian (1) → (N) Appointment
- Animal (1) → (N) Appointment

### Utmaning 3: Vaccination
Skapa `Vaccination` tabell med datum och typ. Ett djur kan ha många vaccinationer.

**Bonus:** Lägg till logik som varnar om vaccinet snart går ut (Date < DateTime.Now.AddMonths(-12)).

---

_"Den som har kontroll på sin databas har kontroll på kaoset."_ 🐾

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
