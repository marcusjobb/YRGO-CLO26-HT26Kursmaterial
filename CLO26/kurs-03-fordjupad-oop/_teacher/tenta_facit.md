# Tentafacit — Fördjupad OOP
**Kurs:** Kurs 3 — Fördjupad objektorienterad programmering i C#
**Program:** CLO26 · YRGO

*Läraren beslutar om gränsfall. Facit är ett riktmärke, inte ett facit i sten.*

---

## Del A — Flervalsfrågor (16p)

| Fråga | Svar | Kommentar |
|-------|------|-----------|
| 1 | **B** | Fildatabas = fil på disk (t.ex. SQLite .db-fil). Databasserver = separat process med nätverksprotokoll. |
| 2 | **C** | InMemory passar för testning — snabb, ingen fil, nollkonfiguration. Inte för produktion (data försvinner). |
| 3 | **C** | `SqlDataReader` är rad-för-rad-läsaren. `SqlCommand` kör frågan. `SqlConnection` öppnar anslutningen. |
| 4 | **B** | Parametrar (`@name`) förhindrar SQL injection. A och C bygger strängar direkt — farligt. |
| 5 | **C** | Factory Method löser exakt detta: klienten behöver inte känna till konkret klass. |
| 6 | **B** | Observer = subject notifierar observers vid tillståndsändring. Event-mönstret i C# är Observer. |
| 7 | **C** | SRP: en klass, ett ansvar, en anledning att ändras (Robert C. Martin). |
| 8 | **B** | WIP-gräns = Work In Progress-gräns. Begränsar parallellt pågående arbete. |

---

## Del B — Kortsvarsfrågror (20p)

### Fråga 9 — ADO.NET vs EF Core (4p)

**Bedömningsguide:**

*Full poäng (4p) kräver:*
- Korrekt förklaring av båda (2p): ADO.NET = lågnivå, manuell SQL + objektmappning; EF Core = ORM, genererar SQL från C#-klasser
- Minst en fördel/nackdel för vardera (1p)
- Minst en situationsanvändning per teknik (1p)

*Exempelsvar:*
ADO.NET är Microsofts lågnivå-API för databasåtkomst. Fördelar: full kontroll, bättre prestanda för tunga queries. Nackdelar: mycket boilerplate-kod, manuell mappning av DataReader till objekt.

EF Core är ett ORM (Object-Relational Mapper) som automatiskt översätter C#-klasser till databastabeller. Fördelar: mindre kod, migrationer, tydligare LINQ-frågor. Nackdelar: mindre kontroll, prestandaöverhead vid komplexa queries.

ADO.NET passar bättre: bulk-operationer, legacy-system, komplexa SQL-queries som kräver optimering.
EF Core passar bättre: standard CRUD-applikationer, snabb prototyputveckling, när maintainability prioriteras.

---

### Fråga 10 — Code First med EF Core (4p)

**Bedömningsguide:**

*Full poäng (4p) kräver:*
- Förklaring av Code First (1p): man skriver C#-klasser → EF Core skapar databasen
- DbContext-rollen (1p): registrerar entiteter, konfigurerar anslutning, hanterar ändringar
- Migrations add + database update (1p): add skapar en migrationsklassering (snapshot), update applicerar på databasen
- Varför migrationer är användbara (1p): versionshantering av databasschema, teamarbete, reproducerbarhet

*Exempelsvar:*
Code First innebär att man definierar sin databasstruktur som C#-klasser (entiteter) — EF Core genererar SQL och skapar/uppdaterar tabellerna utifrån dessa.

`DbContext` är hjärtat: man registrerar sina entiteter som `DbSet<T>`-properties och konfigurerar anslutningssträngen. EF Core spårar alla ändringar via contexten.

`dotnet ef migrations add NamnPåMigration` snappar nuläget och skapar en C#-fil som beskriver ändringen. `dotnet ef database update` applicerar alla väntande migrationer på databasen.

Migrationer gör att man kan versionsstyra sitt databasschema precis som man versionsstyr kod — teammedlemmar kan köra `database update` och alltid ha rätt schema.

---

### Fråga 11 — Repository-mönstret (4p)

**Bedömningsguide:**

*Full poäng (4p) kräver:*
- Vad mönstret löser (1p): separerar dataåtkomst från affärslogik / döljer databasens implementation
- Interfacets roll (1p): möjliggör utbytbarhet (swap SQLite → SQL Server), testbarhet med mock
- Konkreta metodnamn (2p): minst 3-4 rimliga metodnamn

*Exempelsvar:*
Repository-mönstret separerar dataåtkomstlogiken från resten av koden. Applikationskoden kommunicerar mot ett interface och vet inte om det är SQLite, SQL Server eller en mock som finns bakom.

Interfacet gör det möjligt att byta implementation utan att ändra applikationskoden — och att ersätta databasen med en mock vid testning.

Ett `IProductRepository` kan ha metoderna: `GetById(int id)`, `GetAll()`, `Add(Product product)`, `Update(Product product)`, `Delete(int id)`.

---

### Fråga 12 — Refactoring (4p)

**Bedömningsguide:**

*Full poäng (4p) kräver:*
- Definition av refactoring (1p): ändra intern struktur utan att ändra externt beteende
- Två tekniker namngivna och förklarade (2p)
- Kodexempel för minst en teknik (1p)

*Exempelsvar:*
Refactoring innebär att man förbättrar kodens interna struktur utan att ändra vad den gör — output och beteende är identiska efter refactoring.

**Extract Method:** Man bryter ut ett kodblock till en egen namngiven metod.

❌ Före:
```csharp
// 15 rader beräkningslogik direkt i Main
double total = 0;
foreach (var p in products) total += p.Price * p.Quantity;
double tax = total * 0.25;
Console.WriteLine($"Summa: {total + tax}");
```

✅ Efter:
```csharp
double total = CalculateTotal(products);
double tax = CalculateTax(total);
Console.WriteLine($"Summa: {total + tax}");
```

**Rename Variable:** Byta ut kryptiska namn mot beskrivande.

❌ `int x = 7;`
✅ `int daysUntilDeadline = 7;`

---

### Fråga 13 — UML-klassdiagram (4p)

**Bedömningsguide:**

*Full poäng (4p) kräver:*
- Syftet med klassdiagram (1p): visuell planering av klasser och relationer
- Relationstyper förklarade (2p): minst association/komposition + arv
- Kodmappning förklarad (1p)

*Exempelsvar:*
Ett UML-klassdiagram visualiserar klassers attribut, metoder och relationer innan man skriver kod. Det fungerar som en ritning — precis som man ritar ett hus innan man börjar bygga det.

Relationer:
- **Association (pil):** ett objekt känner till ett annat — `Order` har en `Customer`
- **Komposition (fylld romb):** ägarskap — om `Order` raderas försvinner dess `OrderLines`
- **Arv (öppen pilspets):** `Dog` ärver från `Animal`

Från klassdiagram till C#: varje ruta = en klass, varje linje = en property/navigationspropertry eller arv. `Order → Customer` (association) = `public Customer Customer { get; set; }` i Order-klassen.

---

## Del C — Koduppgift (24p)

### C1 — Identifiera designmönster

**Snuttarna 1 — Strategy Pattern (6p)**
- Namn: Strategy (2p)
- Förklaring (4p): `IPaymentStrategy` definierar kontraktet. `SwishPayment` och `CreditCardPayment` är konkreta strategier. `Checkout` tar emot en strategi via konstruktorn (dependency injection) och delegerar anropet. Algoritmen (betalningsmetod) kan bytas ut runtime utan att ändra `Checkout`.

**Snuttarna 2 — Repository Pattern (6p)**
- Namn: Repository (2p)
- Förklaring (4p): `IProductRepository` definierar dataåtkomstgränssnittet. `SqlProductRepository` är konkret implementation. Mönstret separerar dataåtkomst från affärslogik och gör koden testbar via mock-implementation.

**Snuttarna 3 — Observer Pattern (6p)**
- Namn: Observer / Event-mönster (2p)
- Förklaring (4p): `OrderProcessor` är subject med ett event `OrderPlaced`. `EmailService` prenumererar på eventet (subscribe). När `PlaceOrder` anropas notifieras alla subscribers automatiskt. C#:s `event`-nyckelord är den inbyggda Observer-implementationen.

**Snuttarna 4 — Factory Method Pattern (6p)**
- Namn: Factory Method (2p)
- Förklaring (4p): `AnimalFactory` är abstrakt fabrik med abstrakt metod `CreateAnimal`. `DogFactory` och `CatFactory` är konkreta fabriker som bestämmer vilken klass som skapas. Klientkoden kan jobba med `AnimalFactory`-abstraktionen utan att känna till konkreta typer.

---

### C2 — Refaktorera (24p)

**Del 1 — Identifiera problem (8p, 2p per problem, minst 4)**

Godkända svar inkluderar:
1. Klassnamn `P` — icke-beskrivande, klass borde heta t.ex. `AverageCalculator` eller `Program`
2. Variabelnamn `d` — icke-beskrivande; bör heta t.ex. `dataSets`
3. Variabelnamn `x` och `y` — icke-beskrivande; bör heta t.ex. `dataSet` och `value`
4. Variabelnamn `s` och `a` — icke-beskrivande; bör heta `sum` och `average`
5. Ingen Extract Method — beräkningslogiken (summa och genomsnitt) bör brytas ut i en metod
6. Magic number — inget magiskt tal här, men om studerande pekar på något rimligt godkänns det
7. Saknar kommentarer — vid behov
8. `double[]` som samling av datamängder — bör heta något beskrivande

**Del 2 — Refaktorerad version (16p)**

*Bedömning:*
- Vettiga namn på klass, variabler, metod (4p)
- Minst en Extract Method används (4p)
- Kommentarer på svenska där de tillför värde (4p)
- Samma output som originalet (4p)

*Exempelsvar (lärarkod):*
```csharp
/// <summary>
/// Beräknar och skriver ut genomsnitt för ett antal datamängder.
/// </summary>
class AverageCalculator
{
    static void Main()
    {
        // Förbered datamängder
        List<double[]> dataSets = new List<double[]>
        {
            new double[] { 1, 2, 3, 4, 5 },
            new double[] { 10, 20, 30 },
            new double[] { 100 }
        };

        foreach (var dataSet in dataSets)
        {
            double average = CalculateAverage(dataSet);
            Console.WriteLine($"Genomsnitt: {average}");
        }

        Console.WriteLine("Klar");
    }

    /// <summary>
    /// Beräknar genomsnittet för en array av double-värden.
    /// </summary>
    static double CalculateAverage(double[] values)
    {
        double sum = 0;
        foreach (var value in values)
            sum += value;

        return sum / values.Length;
    }
}
```

---

*Facit fastställt av Marcus Ackre Medina, YRGO/NionIT.*
