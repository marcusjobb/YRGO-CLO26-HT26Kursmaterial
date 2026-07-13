---
marp: true
theme: nion-dark
paginate: true
---

# Clean Code & SOLID

**Kurs:** Test och kvalitetssäkring
**Modul:** 01 — Clean Code
Marcus Ackre Medina · YRGO · CLO26

---

## Vad är Clean Code?

Riktlinjer för hur kod ska skrivas.

Inte regler skrivna i sten — det är råd från erfarna programmerare.

I ett projekt bestämmer man i förväg vilken standard som gäller. Ofta bygger den på Clean Code.

---

## Varför spelar det roll?

Du skriver kod som **andra ska läsa**.

Inklusivt du själv om sex månader.

Läsbar kod är snabbare att underhålla, enklare att testa och tryggare att ändra.

---

## Namngivning — grunden

❌
```csharp
int v1, v2, v3;
string s;
bool f;
```

✅
```csharp
int year, month, day;
string firstName;
bool isLoggedIn;
```

**Ge variabler namn som förklarar vad de håller.**

---

## Konventioner i C#

| Vad | Stil |
|-----|------|
| Klasser, metoder, properties | `PascalCase` |
| Lokalvariabler, argument | `camelCase` |
| Konstanter | `PascalCase` |
| Interfaces | `IPrefix` (IPerson, IRepository) |

---

## Metoder — ett ansvar

❌
```csharp
public void SaveAndSendEmail(User user)
{
    // Sparar till databas
    // Skickar email
    // Genererar PDF
    // Loggar händelse
}
```

✅
```csharp
public void Save(User user) { }
public void SendWelcomeEmail(User user) { }
```

**En metod gör en sak.**

---

## Metodlängd

Om en metod inte ryms på halva skärmen — den är för lång.

Bryt ut delar till privata hjälpmetoder.

```csharp
// ❌ 80 rader i en metod
public void ProcessOrder(Order order) { ... }

// ✅ Tydlig orkestrering
public void ProcessOrder(Order order)
{
    ValidateOrder(order);
    CalculateTotal(order);
    SaveToDatabase(order);
    SendConfirmation(order);
}
```

---

## Inga magiska tal

❌
```csharp
if (age > 18)
    discount = price * 0.15;
```

✅
```csharp
const int LegalAdultAge = 18;
const double SeniorDiscountRate = 0.15;

if (age > LegalAdultAge)
    discount = price * SeniorDiscountRate;
```

**Siffror utan kontext berättar ingenting.**

---

## Kommentarer — sparsamt och träffsäkert

❌
```csharp
// Loopar igenom listan
foreach (var item in items)
{
    // Lägger till i total
    total += item.Price;
}
```

✅
```csharp
// Moms inkluderas inte — faktureras separat (se avtal 2024-03)
foreach (var item in items)
    total += item.Price;
```

**Kommentera varför, inte vad.**

---

## SOLID — fem principer

| Bokstav | Princip |
|---------|---------|
| **S** | Single Responsibility |
| **O** | Open/Closed |
| **L** | Liskov Substitution |
| **I** | Interface Segregation |
| **D** | Dependency Inversion |

**Kod som följer SOLID är testbar, utbytbar och enklare att förstå.**

---

## S — Single Responsibility

❌
```csharp
public class User
{
    public string Name { get; set; }
    public void SaveToDatabase() { }
    public void SendEmail() { }
    public void GenerateReport() { }
}
```

✅
```csharp
public class User { public string Name { get; set; } }
public class UserRepository { public void Save(User user) { } }
public class EmailService { public void Send(User user) { } }
```

---

## O — Open/Closed

❌
```csharp
public decimal Calculate(decimal price, string type)
{
    if (type == "Regular") return price * 0.95m;
    if (type == "Premium") return price * 0.90m;
    // Ny typ? Ändra denna metod!
}
```

✅
```csharp
public interface IDiscountStrategy
{
    decimal Apply(decimal price);
}
// Lägg till ny strategi utan att ändra befintlig kod
```

---

## L — Liskov Substitution

En härledd klass ska fungera överallt där basklassen används.

```csharp
public abstract class Shape
{
    public abstract double GetArea();
}

public class Rectangle : Shape
{
    public override double GetArea() => Width * Height;
}

public class Circle : Shape
{
    public override double GetArea() => Math.PI * Radius * Radius;
}

// Båda fungerar som Shape — ingen överraskning
```

---

## I — Interface Segregation

❌
```csharp
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}
// Robot måste implementera Eat och Sleep!
```

✅
```csharp
public interface IWorkable { void Work(); }
public interface IFeedable { void Eat(); }

public class Human : IWorkable, IFeedable { }
public class Robot : IWorkable { }
```

---

## D — Dependency Inversion

❌
```csharp
public class OrderService
{
    private SqlRepository _repo = new SqlRepository();
}
// Kan inte byta databas, kan inte testa!
```

✅
```csharp
public class OrderService
{
    private readonly IRepository _repo;

    public OrderService(IRepository repo)
    {
        _repo = repo;
    }
}
```

---

## Vanliga code smells att undvika

- **Duplicerad kod** — bryt ut till metod
- **Långa parameterlistor** — skapa ett objekt
- **Kommenterad-bort kod** — ta bort den
- **Oanvända using-satser** — ta bort dem
- **Magic strings** — använd konstanter

---

## Klassstruktur — rätt ordning

```csharp
public class Product
{
    // 1. Konstruktor
    // 2. Fält (fields)
    // 3. Properties
    // 4. Publika metoder
    // 5. Privata metoder
    // 6. Statiska metoder
}
```

Konsekvent ordning gör koden förutsägbar att navigera.

---

## Sammanfattning

- ✅ Vettiga namn — koden ska läsas av människor
- ✅ En metod, ett ansvar
- ✅ Inga magiska tal — använd konstanter
- ✅ SOLID gör koden testbar och utbytbar
- ✅ Kommentera varför, inte vad

**Nästa: Unit-testning med xUnit**
