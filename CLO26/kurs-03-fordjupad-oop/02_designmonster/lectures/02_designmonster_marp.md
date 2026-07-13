---
marp: true
theme: nion-dark
paginate: true
---

# Designmönster i C#

**Kurs:** Fördjupad OOP
**Modul:** 02 — Designmönster

Marcus Ackre Medina · YRGO · CLO26

---

## Vad ska vi lära oss idag?

- Vad är ett designmönster — och varför bry sig?
- Gang of Four och de tre kategorierna
- Factory Method, Observer, Repository, Strategy, Builder
- Hur man väljer rätt mönster
- Vilka mönster du redan använt utan att veta om det

---

## En designer behöver inte uppfinna stolen varje dag

Köksstolen uppfanns en gång.  
Sedan kopieras *konceptet* — inte ritningen.

Designmönster är samma sak för kod.  
Lösningar på problem som vi sett så många gånger att de förtjänar ett namn.

```
Problem + Kontext + Beprövad lösning = Designmönster
```

---

## Gang of Four — historien på 30 sekunder

1994. Fyra killar skriver boken *Design Patterns: Elements of Reusable Object-Oriented Software*.

De heter Gamma, Helm, Johnson och Vlissides.  
Alla kallar dem bara **GoF** (Gang of Four).

Boken listar **23 mönster** i tre kategorier.  
Det är dessa vi pratar om än idag — 30 år senare.

> Sofie på din första jobb-intervju: *"Kan du berätta om ett designmönster?"*  
> Du: *"Självklart."*

---

## De tre kategorierna

| Kategori | Handlar om | Exempel |
|----------|------------|---------|
| **Creational** | Hur objekt skapas | Factory, Builder, Singleton |
| **Structural** | Hur objekt kopplas ihop | Repository, Adapter, Facade |
| **Behavioral** | Hur objekt kommunicerar | Observer, Strategy, Command |

Idag täcker vi ett mönster ur varje kategori — plus ett extra.

---

## Factory Method — problemet

Patrik jobbar på Kalles Pizzeria AB.  
Systemet hanterar betalningar: kort, Swish och faktura.

```csharp
// ❌ new() spritt över hela kodbasen
if (betalning == "kort")
{
    var betSvc = new KortbetalningsTjänst(); // här
}
if (betalning == "swish")
{
    var betSvc = new SwishTjänst(); // och här
}
// ... och 14 ställen till
```

När Klarna ska läggas till ändrar Patrik på 16 ställen.  
Han missar tre. Systemet kraschar på fredag kväll.

---

## Factory Method — lösningen

```csharp
// ✅ Skapa objekt på ett ställe
public static IBetalningsTjänst Skapa(string typ) => typ switch
{
    "kort"    => new KortbetalningsTjänst(),
    "swish"   => new SwishTjänst(),
    "faktura" => new FakturaTjänst(),
    _         => throw new ArgumentException($"Okänd betaltyp: {typ}")
};
```

Nu lägger Patrik till Klarna på **ett enda ställe**.  
Resten av koden rör han inte.

> Regeln: kod som skapar objekt ska bo på ett ställe.

---

## Factory Method — hela strukturen

```csharp
// Gemensamt kontrakt
public interface IBetalningsTjänst
{
    Task<bool> BetalaAsync(decimal belopp);
}

// Konkreta implementationer
public class KortbetalningsTjänst : IBetalningsTjänst
{
    public async Task<bool> BetalaAsync(decimal belopp)
    {
        // Anropar kortterminalen
        return await KortTerminal.ChargeAsync(belopp);
    }
}

// Fabriksmetoden — enda stället med new()
public class BetalningsFabrik
{
    public static IBetalningsTjänst Skapa(string typ) => typ switch
    {
        "kort"    => new KortbetalningsTjänst(),
        "swish"   => new SwishTjänst(),
        "faktura" => new FakturaTjänst(),
        _         => throw new ArgumentException($"Okänd betaltyp: {typ}")
    };
}
```

---

## Observer — problemet

Sofie bygger ett ordersystem.  
När en order läggs ska tre saker hända:

1. Skicka bekräftelsemail
2. Uppdatera lagersaldot
3. Notifiera plocklistan i lagret

```csharp
// ❌ OrderService vet om ALLT
public void LäggOrder(Order order)
{
    _lagret.MinskaLager(order);
    _mejl.SkickaKvitto(order);
    _plock.LäggTillPlocklista(order);
    // Nästa krav: _sms.SkickaAvi(order)?
}
```

`OrderService` är ihopkopplad med tre andra tjänster.  
Varje nytt krav kräver en ändring i fel klass.

---

## Observer — lösningen med event och delegate

```csharp
public class OrderService
{
    // ✅ Publicerar vad som hänt — vet inte vem som lyssnar
    public event Action<Order>? OrderLagd;

    public void LäggOrder(Order order)
    {
        // ... spara ordern i databasen ...
        OrderLagd?.Invoke(order); // "Hallå, en order lades!"
    }
}

// Prenumeranter kopplar på utifrån
var svc = new OrderService();
svc.OrderLagd += order => _lagret.MinskaLager(order);
svc.OrderLagd += order => _mejl.SkickaKvitto(order);
svc.OrderLagd += order => _plock.LäggTillPlocklista(order);

// Nästa krav: en rad till — utan att röra OrderService
svc.OrderLagd += order => _sms.SkickaAvi(order);
```

---

## Observer i verkligheten

`event` och `delegate` i C# **är** Observer-mönstret.  
Du har redan använt det varje gång du skrivit:

```csharp
knapp.Click += (s, e) => Console.WriteLine("Klickad!");
```

`Click` är ett event. Din lambda är observatören.  
Windows Forms, WPF och Blazor bygger på detta.

> Observer är Behavioral — det handlar om hur objekt *kommunicerar*.  
> OrderService vet inte om mejl, lager eller SMS. De vet om den.

---

## Repository Pattern — vad och varför

Sofie vill byta databas från SQL Server till PostgreSQL.  
Med direkt databaskod spridd i hela applikationen tar det tre veckor.

Med Repository tar det en dag.

```
Controller → IOrderRepository → SqlOrderRepository
                             ↘ PostgresOrderRepository  (ny!)
```

Resten av koden ser bara interfacet.  
Den bryr sig inte om vad som ligger bakom.

> Repository är Structural — det handlar om hur lager *kopplas ihop*.

---

## Repository Pattern — IRepository-interface

```csharp
// Kontraktet — alla repositories måste uppfylla detta
public interface IRepository<T> where T : class
{
    Task<T?> HämtaAsync(int id);
    Task<IEnumerable<T>> HämtaAllaAsync();
    Task LäggTillAsync(T entitet);
    Task UppdateraAsync(T entitet);
    Task TaBortAsync(int id);
}
```

```csharp
// Konkret implementation för SQL Server
public class SqlOrderRepository : IRepository<Order>
{
    private readonly AppDbContext _db;

    public SqlOrderRepository(AppDbContext db) => _db = db;

    public async Task<Order?> HämtaAsync(int id)
        => await _db.Orders.FindAsync(id);

    // ... resten av metoderna
}
```

---

## Strategy Pattern — problemet

Patrik ska bygga sortering för Kalles Pizzeria-meny.  
Varje ny sorteringstyp ger fler grenar i samma metod.

```csharp
// ❌ If/else-träd som bara växer
public List<Rätt> SorteraMeny(List<Rätt> meny, string sortering)
{
    if (sortering == "pris")
        return meny.OrderBy(r => r.Pris).ToList();
    else if (sortering == "namn")
        return meny.OrderBy(r => r.Namn).ToList();
    else if (sortering == "popularitet")
        return meny.OrderByDescending(r => r.Beställningar).ToList();
    else if (sortering == "kalorier") // nytt krav varje vecka
        return meny.OrderBy(r => r.Kalorier).ToList();
    // ...
}
```

Varje nytt krav kräver en ändring i en befintlig metod.  
Det är ett tecken på att ansvaret är fel fördelat.

---

## Strategy Pattern — lösningen

```csharp
// ✅ Interface för varje algoritm
public interface ISorteringsStrategi
{
    List<Rätt> Sortera(List<Rätt> meny);
}

// Konkreta strategier
public class SorteraPåPris : ISorteringsStrategi
{
    public List<Rätt> Sortera(List<Rätt> meny)
        => meny.OrderBy(r => r.Pris).ToList();
}

public class SorteraPåPopularitet : ISorteringsStrategi
{
    public List<Rätt> Sortera(List<Rätt> meny)
        => meny.OrderByDescending(r => r.Beställningar).ToList();
}

// Menyn använder vilken strategi som helst
public class Meny
{
    private ISorteringsStrategi _strategi;

    public Meny(ISorteringsStrategi strategi) => _strategi = strategi;

    public void BytStrategi(ISorteringsStrategi ny) => _strategi = ny;

    public List<Rätt> HämtaSorterad(List<Rätt> rätter)
        => _strategi.Sortera(rätter);
}
```

---

## Strategy i praktiken

```csharp
var meny = new Meny(new SorteraPåPris());
var billigast = meny.HämtaSorterad(rätter);

// Byt algoritm runtime — utan att ändra Meny-klassen
meny.BytStrategi(new SorteraPåPopularitet());
var populärast = meny.HämtaSorterad(rätter);
```

Nytt krav? Lägg till en ny klass. Rör ingenting annat.

> Strategy är Behavioral — det handlar om utbytbara algoritmer.  
> `OrderBy()` i LINQ är Strategy. Du väljer sorteringslogik runtime.

---

## Builder Pattern — problemet

```csharp
// ❌ Konstruktorer som ser ut så här är ett problem
var order = new Order(
    "Sofie Lindgren",
    "sofie@example.com",
    "Storgatan 1",
    "41104",
    "Göteborg",
    "Sverige",
    true,
    false,
    null,
    DateTime.Now,
    42
);
```

Vad är `true`? Vad är `false`? Vad är `42`?  
Ingen vet. Inte ens Sofie som skrev det.

---

## Builder Pattern — lösningen

```csharp
// ✅ Steg-för-steg, läsbart
var order = new OrderBuilder()
    .FörKund("Sofie Lindgren", "sofie@example.com")
    .MedLeveransadress("Storgatan 1", "41104", "Göteborg")
    .MedExpressLevrans()
    .Bygg();
```

```csharp
public class OrderBuilder
{
    private readonly Order _order = new();

    public OrderBuilder FörKund(string namn, string mejl)
    {
        _order.Namn = namn;
        _order.Mejl = mejl;
        return this; // returnerar sig själv — möjliggör method chaining
    }

    public OrderBuilder MedExpressLevrans()
    {
        _order.Express = true;
        return this;
    }

    public Order Bygg() => _order;
}
```

> Builder är Creational — det handlar om hur komplexa objekt *konstrueras*.

---

## Hur väljer man rätt mönster?

```
Skapar du objekt på många ställen?
    → Factory Method

Har du ett if/else-träd som väljer algoritm?
    → Strategy

Ska ett objekt notifiera flera utan att känna dem?
    → Observer / Event

Vill du dölja hur du hämtar data?
    → Repository

Bygger du objekt med många valfria delar?
    → Builder
```

Börja enkelt. Extrahera mönstret när problemet dyker upp — inte innan.

---

## Designmönster du redan sett — utan att veta det

| Vad du använt | Vilket mönster |
|---------------|---------------|
| `knapp.Click += handler` | Observer |
| `meny.OrderBy(r => r.Pris)` | Strategy |
| `foreach` över `IEnumerable<T>` | Iterator |
| `IRepository<T>` från förra modulen | Repository |
| `StringBuilder` | Builder |
| `HttpClientFactory.Create()` | Factory Method |
| Dependency Injection-containern | Factory + Singleton |

Du programmerar redan med designmönster.  
Nu vet du vad de heter.

---

## Sammanfattning

- ✅ Designmönster är beprövade lösningar med namn — inte syntaxregler
- ✅ Gang of Four: Creational / Structural / Behavioral
- ✅ Factory Method samlar `new()` på ett ställe
- ✅ Observer kopplar loss sändare från mottagare via `event`
- ✅ Repository döljer datakällan bakom ett interface
- ✅ Strategy byter algoritm runtime utan att ändra befintlig kod
- ✅ Builder konstruerar komplexa objekt läsbart och steg för steg
- ✅ Du har redan använt de flesta utan att känna till namnen

**Nästa gång: Refactoring och Clean Code**
