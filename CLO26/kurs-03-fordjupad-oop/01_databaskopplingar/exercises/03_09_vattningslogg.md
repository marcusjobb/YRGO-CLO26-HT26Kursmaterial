# 🪴 Vattningslogg – Håll koll på dina växter

🟢


En workshop där du bygger ett system för att hålla koll på dina växter.
Du registrerar varje växt, hur ofta den ska vattnas, hur mycket vatten den behöver, och när du vattnade sist.
Sen låter du appen räkna ut vilka växter som är törstiga idag — allt med Entity Framework Core 8.

**Konceptet:** Många glömmer vattna sina växter. Den här appen löser det.
Du får lära dig hantera datum, relationer, beräkningar och LINQ — och så slipper du dina monsteras bruna blad på köpet. 🌱

---

## 🎯 Mål

Efter övningen ska du kunna:

- Hantera `DateTime` i EF Core
- Skapa 1:N relation (Plant → WateringLog)
- Använda `AsEnumerable()` för komplex datumlogik
- Implementera affärsregler (törstiga växter)
- Beräkna dagar mellan datum
- Seeda data med `HasData()`

---

## 🧱 Del 1 – Projektstruktur (5 min)

### Varför Vattningslogg?

Många glömmer vattna sina växter! En app som:
- Håller koll på varje växt
- Loggar när du vattnade
- Berättar vilka växter är törstiga IDAG

### Steg 1: Skapa projekt

```bash
dotnet new console -n Vattningslogg
cd Vattningslogg
```

### Steg 2: Lägg till EF Core

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### Steg 3: Skapa mappar

```bash
mkdir Models Database Controllers
```

---

## 🌿 Del 2 – Modeller (15 min)

### Steg 1: Skapa Plant

Skapa `Models/Plant.cs`:

```csharp
namespace Vattningslogg.Models;

public class Plant
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int WaterEveryDays { get; set; }  // Vattnas var X:e dag

    // En växt har många vattenloggar (1:N)
    public List<WateringLog> Logs { get; set; } = new();
}
```

**Vad händer här?**
- `WaterEveryDays` = hur ofta växten behöver vatten
- `Logs` = historik över alla vattningstillfällen

### Steg 2: Skapa WateringLog

Skapa `Models/WateringLog.cs`:

```csharp
namespace Vattningslogg.Models;

public class WateringLog
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int AmountMl { get; set; }  // Hur mycket vatten (ml)

    // Tillhör en växt (1:N)
    public int PlantId { get; set; }
    public Plant? Plant { get; set; }
}
```

---

## 🪴 Del 3 – Databas och seeding (20 min)

Skapa `Database/PlantContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using Vattningslogg.Models;

namespace Vattningslogg.Database;

public class PlantContext : DbContext
{
    public DbSet<Plant> Plants => Set<Plant>();
    public DbSet<WateringLog> Logs => Set<WateringLog>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=VattningsloggDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder m)
    {
        // Relation: Plant → Logs (1:N)
        m.Entity<Plant>()
            .HasMany(p => p.Logs)
            .WithOne(l => l.Plant)
            .HasForeignKey(l => l.PlantId)
            .OnDelete(DeleteBehavior.Cascade);  // Ta bort loggar om växt raderas

        // Seed växter
        m.Entity<Plant>().HasData(
            new Plant { Id = 1, Name = "Monstera", WaterEveryDays = 7 },
            new Plant { Id = 2, Name = "Kaktus", WaterEveryDays = 21 },
            new Plant { Id = 3, Name = "Orchidé", WaterEveryDays = 10 },
            new Plant { Id = 4, Name = "Fredskalla", WaterEveryDays = 5 }
        );
    }
}
```

**Skapa databasen:**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 💧 Del 4 – PlantService (35 min)

### Varför en Service-klass?

Affärslogiken (hitta törstiga växter, logga vattning) hör inte hemma i Program.cs!

### Steg 1: Skapa PlantService

Skapa `Controllers/PlantService.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using Vattningslogg.Database;
using Vattningslogg.Models;

namespace Vattningslogg.Controllers;

public class PlantService
{
    // Hitta växter som behöver vatten IDAG
    public IEnumerable<Plant> ThirstyToday()
    {
        using var db = new PlantContext();
        var today = DateTime.Today;  // Bara datum, ingen tid

        return db.Plants
            .Include(p => p.Logs)
            .AsEnumerable()  // Viktigt! Fortsätt i minnet
            .Where(p =>
            {
                // Hitta senaste vattningen
                var lastWatering = p.Logs
                    .OrderByDescending(l => l.Date)
                    .FirstOrDefault();

                // Om aldrig vattnad → törstig!
                if (lastWatering == null)
                    return true;

                // Beräkna dagar sedan senaste vattning
                var daysSince = (today - lastWatering.Date).Days;

                // Törstig om daysSince >= WaterEveryDays
                return daysSince >= p.WaterEveryDays;
            });
    }

    // Vattna en växt
    public void Water(int plantId, int amountMl)
    {
        using var db = new PlantContext();

        var plant = db.Plants.Find(plantId);
        if (plant == null)
        {
            Console.WriteLine("❌ Växten hittades inte");
            return;
        }

        var log = new WateringLog
        {
            PlantId = plantId,
            AmountMl = amountMl,
            Date = DateTime.Now
        };

        db.Logs.Add(log);
        db.SaveChanges();

        Console.WriteLine($"✅ {plant.Name} vattnad med {amountMl} ml");
    }

    // Lista alla växter med senaste vattning
    public void ListPlants()
    {
        using var db = new PlantContext();

        var plants = db.Plants
            .Include(p => p.Logs)
            .ToList();

        Console.WriteLine("\n--- Mina växter ---");
        foreach (var p in plants)
        {
            var lastWatering = p.Logs
                .OrderByDescending(l => l.Date)
                .FirstOrDefault();

            var lastWateredText = lastWatering != null
                ? $"Senast vattnad: {lastWatering.Date:yyyy-MM-dd}"
                : "Aldrig vattnad";

            Console.WriteLine($"{p.Id}. {p.Name} (var {p.WaterEveryDays}:e dag) - {lastWateredText}");
        }
    }

    // Visa nästa vattningsdag
    public void ShowNextWatering(int plantId)
    {
        using var db = new PlantContext();

        var plant = db.Plants
            .Include(p => p.Logs)
            .FirstOrDefault(p => p.Id == plantId);

        if (plant == null)
        {
            Console.WriteLine("❌ Växten hittades inte");
            return;
        }

        var lastWatering = plant.Logs
            .OrderByDescending(l => l.Date)
            .FirstOrDefault();

        if (lastWatering == null)
        {
            Console.WriteLine($"{plant.Name} har aldrig vattnats! Vattna NU! 💦");
            return;
        }

        var nextDate = lastWatering.Date.AddDays(plant.WaterEveryDays);
        var daysUntil = (nextDate - DateTime.Today).Days;

        if (daysUntil <= 0)
        {
            Console.WriteLine($"🚨 {plant.Name} behöver vatten IDAG!");
        }
        else
        {
            Console.WriteLine($"📅 {plant.Name} behöver vatten om {daysUntil} dagar ({nextDate:yyyy-MM-dd})");
        }
    }
}
```

**Vad händer här?**

**AsEnumerable() - VIKTIGT!**
```csharp
.AsEnumerable()
```
- Hämtar data från databasen till minnet
- Fortsätter filtreringen i C# (inte SQL)
- **Varför?** `DateTime`-beräkningar i SQL är komplexa och kan ge fel

**Datumberäkning:**
```csharp
var daysSince = (today - lastWatering.Date).Days;
```
- Subtraherar två `DateTime` → ger `TimeSpan`
- `.Days` hämtar antal hela dagar

**Törstighetskontroll:**
```csharp
return daysSince >= p.WaterEveryDays;
```
- Om det gått >= 7 dagar sedan senaste vattning (för Monstera) → törstig!

---

## 🌤️ Del 5 – Program (20 min)

```csharp
using Vattningslogg.Controllers;

var service = new PlantService();
bool running = true;

Console.WriteLine("🪴 Välkommen till Vattningslogg!");

while (running)
{
    Console.WriteLine("\n========== PLANTPAL ==========");
    Console.WriteLine("1. Törstiga växter (idag)");
    Console.WriteLine("2. Lista alla växter");
    Console.WriteLine("3. Vattna en växt");
    Console.WriteLine("4. Nästa vattningsdag");
    Console.WriteLine("0. Avsluta");
    Console.WriteLine("==============================");
    Console.Write("Välj > ");

    switch (Console.ReadLine())
    {
        case "1":
            var thirsty = service.ThirstyToday().ToList();
            if (!thirsty.Any())
            {
                Console.WriteLine("✅ Alla växter är glada!");
            }
            else
            {
                Console.WriteLine("\n💦 Dessa växter behöver vatten:");
                foreach (var p in thirsty)
                    Console.WriteLine($"   - {p.Name}");
            }
            break;

        case "2":
            service.ListPlants();
            break;

        case "3":
            service.ListPlants();
            Console.Write("\nVälj växt-ID: ");
            int.TryParse(Console.ReadLine(), out int plantId);

            Console.Write("Mängd vatten (ml): ");
            int.TryParse(Console.ReadLine(), out int ml);

            service.Water(plantId, ml);
            break;

        case "4":
            service.ListPlants();
            Console.Write("\nVälj växt-ID: ");
            int.TryParse(Console.ReadLine(), out int id);
            service.ShowNextWatering(id);
            break;

        case "0":
            Console.WriteLine("Hejdå! 🌿 Glöm inte vattna!");
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

---

## 🐛 Troubleshooting

### Problem 1: DateTime.Now vs DateTime.Today

**Orsak:** `DateTime.Now` inkluderar tid (08:45:23), `DateTime.Today` är bara datum (00:00:00).

**Lösning:**
```csharp
var today = DateTime.Today;  // Använd ALLTID Today för datumjämförelser
```

### Problem 2: AsEnumerable() saknas

**Orsak:** Du försöker göra komplex datumberäkning i SQL.

**Fel:**
```csharp
var thirsty = db.Plants
    .Where(p => (DateTime.Today - p.Logs.Max(l => l.Date)).Days >= p.WaterEveryDays);
// ❌ Detta fungerar INTE i SQL!
```

**Rätt:**
```csharp
var thirsty = db.Plants
    .Include(p => p.Logs)
    .AsEnumerable()  // Fortsätt i minnet!
    .Where(p => ...);
```

### Problem 3: Växter som aldrig vattnats crashar

**Orsak:** `p.Logs` är tom → `FirstOrDefault()` returnerar null.

**Lösning:**
```csharp
var lastWatering = p.Logs.OrderByDescending(l => l.Date).FirstOrDefault();

if (lastWatering == null)
    return true;  // Aldrig vattnad → törstig!
```

---

## 🧠 Sammanfattning

| Koncept           | Betydelse                                   |
| ----------------- | ------------------------------------------- |
| `DateTime.Today`  | Dagens datum utan tid (00:00:00)            |
| `DateTime.Now`    | Dagens datum OCH tid                        |
| `AsEnumerable()`  | Fortsätt query i minnet (inte SQL)          |
| `TimeSpan.Days`   | Antal hela dagar mellan två datum           |
| `AddDays()`       | Lägg till dagar till ett datum              |

---

## 🤔 Reflektionsfrågor

### Fråga 1: Växter som aldrig vattnats
Hur hittar du de växter som aldrig har vattnats?

<details>
<summary>Svar</summary>

```csharp
public IEnumerable<Plant> NeverWatered()
{
    using var db = new PlantContext();

    return db.Plants
        .Include(p => p.Logs)
        .Where(p => !p.Logs.Any());
}
```

**Förklaring:** `!p.Logs.Any()` kollar om Logs-listan är tom.
</details>

### Fråga 2: DateTime.Now vs .Date
Vad händer om du jämför `DateTime.Now` istället för `.Date`?

<details>
<summary>Svar</summary>

**Problem:**
```csharp
var daysSince = (DateTime.Now - lastWatering.Date).Days;
```

Om du vattnade igår kl 16:00 och det nu är 14:00:
- `DateTime.Now - yesterday` = ca 22 timmar
- `.Days` = 0 (mindre än 24h)
- Växten verkar INTE törstig, fast det gått 1 dag!

**Rätt:**
```csharp
var daysSince = (DateTime.Today - lastWatering.Date.Date).Days;
```

Nu jämförs bara datum → 1 dag rätt!
</details>

### Fråga 3: Nästa vattningsdag
Hur skulle du lägga till en funktion som säger "nästa vattningsdag"?

<details>
<summary>Svar</summary>

**Implementerad i ShowNextWatering():**
```csharp
var nextDate = lastWatering.Date.AddDays(plant.WaterEveryDays);
var daysUntil = (nextDate - DateTime.Today).Days;
```

**Alternativ - Enkel property:**
```csharp
public DateTime? NextWateringDate
{
    get
    {
        var lastLog = Logs.OrderByDescending(l => l.Date).FirstOrDefault();
        return lastLog?.Date.AddDays(WaterEveryDays);
    }
}
```

Lägg till i Plant-klassen för enkel access!
</details>

---

## 🚀 Utmaningar

### Utmaning 1: Påminnelser
Implementera "påminn mig 1 dag innan" – visa växter som behöver vatten IMORGON.

### Utmaning 2: Växttyper
Lägg till `PlantType`-tabell (Succulent, Flowering, Fern) med olika vatteningskrav.

### Utmaning 3: Säsongsvariationer
Justera `WaterEveryDays` baserat på årstid (mindre vatten på vintern).

---

_"Plantor är som kod – de växer bäst med konsistent vård och rätt timing."_ 🌱

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
