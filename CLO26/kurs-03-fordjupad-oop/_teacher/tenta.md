# Skriftlig tentamen — Fördjupad OOP
**Kurs:** Kurs 3 — Fördjupad objektorienterad programmering i C#
**Program:** CLO26 · YRGO
**Tid:** 3 timmar
**Hjälpmedel:** Inga

---

**Poängfördelning:**
- Del A: 8 flervalsfrågor à 2p = **16p**
- Del B: 5 kortsvarsfrågror à 4p = **20p**
- Del C: 1 koduppgift = **24p**
- **Totalt: 60p**

**Betygsgränser:**
- G: 36p (60%)
- VG: 48p (80%)

---

## Del A — Flervalsfrågor (16p)

*Välj ett svar per fråga. Fel svar ger 0p.*

---

**Fråga 1 (2p)**

Vilket påstående stämmer bäst om skillnaden mellan en fildatabas och en databasserver?

A. En fildatabas kräver en nätverksanslutning för att fungera, en databasserver inte.
B. En fildatabas lagrar data direkt i en fil på disk; en databasserver körs som en separat process och hanterar anslutningar från flera klienter.
C. En databasserver är alltid snabbare än en fildatabas.
D. Fildatabaser stödjer inte SQL.

---

**Fråga 2 (2p)**

Vad är en minnesdatabas (in-memory database) och när är den lämplig att använda?

A. En databas som lagrar data i en textfil och läser in den till minnet vid start.
B. En databas vars data endast existerar i RAM under körning — lämplig för produktion när man vill ha hög prestanda.
C. En databas vars data endast existerar i RAM under körning — lämplig för testning eftersom den inte kräver en riktig databasfil.
D. En databas som cacher all data från SQL Server i minnet.

---

**Fråga 3 (2p)**

I ADO.NET — vilket objekt används för att läsa flera rader från en SELECT-fråga?

A. `SqlCommand`
B. `SqlDataAdapter`
C. `SqlDataReader`
D. `SqlConnection`

---

**Fråga 4 (2p)**

Vilket kodblock är korrekt med avseende på SQL-injektionsskydd?

A.
```csharp
string sql = $"SELECT * FROM Users WHERE Name = '{name}'";
```
B.
```csharp
string sql = "SELECT * FROM Users WHERE Name = @name";
cmd.Parameters.AddWithValue("@name", name);
```
C.
```csharp
string sql = "SELECT * FROM Users WHERE Name = " + name;
```
D. A och C är likvärdiga och båda säkra.

---

**Fråga 5 (2p)**

Vilket designmönster passar bäst när man vill skapa objekt av olika typer utan att behöva ange exakt vilken klass som ska skapas i klientkoden?

A. Observer
B. Strategy
C. Factory Method
D. Repository

---

**Fråga 6 (2p)**

Vilket påstående beskriver Observer-mönstret korrekt?

A. Observer gör att ett objekt kan byta ut sin algoritm under körning.
B. Observer låter ett objekt (subject) notifiera en lista av beroende objekt (observers) automatiskt när dess tillstånd ändras.
C. Observer är ett creational pattern för att bygga komplexa objekt steg för steg.
D. Observer används för att separera dataåtkomstlogik från affärslogik.

---

**Fråga 7 (2p)**

Vad innebär principen SRP (Single Responsibility Principle)?

A. En klass ska ärva från exakt en basklass.
B. En klass ska ha exakt ett publikt interface.
C. En klass ska ha ett enda ansvar — en enda anledning att ändras.
D. En metod ska bara anropa en annan metod.

---

**Fråga 8 (2p)**

Vad beskriver begreppet WIP-gräns i Kanban?

A. Det maximala antalet user stories i product backlog.
B. Det maximala antalet uppgifter som får vara i arbete (In Progress) simultant.
C. Det minsta antal story points ett team måste leverera per sprint.
D. Antalet dagliga standups per vecka.

---

## Del B — Kortsvarsfrågror (20p)

*Svara med fullständiga meningar. Kodexempel är välkomna men inte obligatoriska.*

---

**Fråga 9 (4p)**

Förklara skillnaden mellan ADO.NET och Entity Framework Core (EF Core) för databasåtkomst i C#.

I ditt svar ska du:
- Beskriva vad varje teknik är
- Nämna minst en fördel och en nackdel med vardera
- Nämna en situation där ADO.NET passar bättre och en där EF Core passar bättre

---

**Fråga 10 (4p)**

Beskriv hur Code First-arbetsflödet i EF Core fungerar.

I ditt svar ska du:
- Förklara vad Code First innebär
- Nämna rollen för `DbContext`
- Beskriva vad `migrations add` respektive `database update` gör
- Förklara varför migrationer är användbara

---

**Fråga 11 (4p)**

Förklara Repository-mönstret och varför det används.

I ditt svar ska du:
- Beskriva vad mönstret löser
- Förklara vad ett interface bidrar med i mönstret
- Ge ett konkret exempel på metodnamn ett `IProductRepository` kan ha

---

**Fråga 12 (4p)**

Vad är refactoring? Beskriv två konkreta tekniker för refactoring och ge ett exempel på varje.

I ditt svar ska du:
- Definiera refactoring
- Namnge och förklara två tekniker (t.ex. Extract Method, Rename Variable, Replace Magic Number)
- Visa ett litet kodexempel för minst en av teknikerna

---

**Fråga 13 (4p)**

Förklara vad ett UML-klassdiagram är och hur det relaterar till C#-klasser.

I ditt svar ska du:
- Förklara syftet med ett klassdiagram
- Beskriva hur relationer (association, komposition, arv) visas
- Förklara hur du går från ett klassdiagram till C#-kod

---

## Del C — Koduppgift (24p)

*Välj antingen uppgift C1 eller uppgift C2. Båda ger max 24p.*

---

### C1 — Identifiera och namnge designmönster (24p)

Nedan följer fyra kodsnuttar. För varje snuttidentifiera:
- Vilket designmönster som används (2p per korrekt namn)
- En kort förklaring på varför du känner igen det (4p per korrekt förklaring)

**Snuttarna:**

**Snuttarna 1:**
```csharp
public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

public class SwishPayment : IPaymentStrategy
{
    public void Pay(decimal amount) => Console.WriteLine($"Swish: {amount} kr");
}

public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount) => Console.WriteLine($"Kort: {amount} kr");
}

public class Checkout
{
    private IPaymentStrategy _strategy;
    public Checkout(IPaymentStrategy strategy) { _strategy = strategy; }
    public void Complete(decimal amount) => _strategy.Pay(amount);
}
```

**Snuttarna 2:**
```csharp
public interface IProductRepository
{
    Product? GetById(int id);
    List<Product> GetAll();
    void Add(Product product);
    void Delete(int id);
}

public class SqlProductRepository : IProductRepository
{
    // implementation med EF Core eller ADO.NET
}
```

**Snuttarna 3:**
```csharp
public class OrderProcessor
{
    public event Action<Order>? OrderPlaced;

    public void PlaceOrder(Order order)
    {
        // ...spara ordern...
        OrderPlaced?.Invoke(order);
    }
}

public class EmailService
{
    public void Subscribe(OrderProcessor processor)
    {
        processor.OrderPlaced += SendConfirmationEmail;
    }

    private void SendConfirmationEmail(Order order) { /* skicka mail */ }
}
```

**Snuttarna 4:**
```csharp
public abstract class AnimalFactory
{
    public abstract IAnimal CreateAnimal();
}

public class DogFactory : AnimalFactory
{
    public override IAnimal CreateAnimal() => new Dog();
}

public class CatFactory : AnimalFactory
{
    public override IAnimal CreateAnimal() => new Cat();
}
```

---

### C2 — Refaktorera ett kodblock (24p)

Nedan finns ett fungerande men dåligt skrivet C#-program. Din uppgift är att refaktorera det.

**Originalkod:**
```csharp
class P
{
    static void Main()
    {
        List<double[]> d = new List<double[]>();
        d.Add(new double[] { 1, 2, 3, 4, 5 });
        d.Add(new double[] { 10, 20, 30 });
        d.Add(new double[] { 100 });

        foreach (var x in d)
        {
            double s = 0;
            foreach (var y in x)
                s += y;
            double a = s / x.Length;
            Console.WriteLine("Genomsnitt: " + a);
        }

        Console.WriteLine("Klar");
    }
}
```

**Din uppgift:**

1. **(8p)** Lista minst fyra konkreta problem med koden (namngivning, clean code, struktur etc.)
2. **(16p)** Skriv en refaktorerad version av koden som åtgärdar dessa problem.

Krav på den refaktorerade versionen:
- Vettiga klass- och variabelnamn
- Extraherade metoder (minst en)
- Kommentarer på svenska där de tillför värde
- Koden ska producera samma output som originalet

---

*Lycka till!*
