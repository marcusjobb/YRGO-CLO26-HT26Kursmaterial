---
marp: true
theme: nion-dark
paginate: true
---

# Refactoring och Clean Code

**Kurs:** Fördjupad OOP
**Modul:** 03 — Refactoring

Marcus Ackre Medina · YRGO · CLO26

---

## Vad är refactoring?

> Förändra kodens struktur **utan att förändra dess beteende**

Det handlar inte om att lägga till features.
Det handlar inte om att fixa buggar.

Det handlar om att **göra koden bättre att leva med**.

---

## Varför refactorar man?

Kod ruttnar om den inte sköts om.

- "Quick fixes" staplas på varandra
- Ingen törs röra gammalt som fungerar
- Ny person förstår ingenting
- Varje ny feature tar dubbelt så lång tid

Det kallas **teknisk skuld** — och den drabbar hela teamet.

---

## Teknisk skuld — ett konkret exempel

Emma har byggt en feature på en dag.
Men koden är rörig. Tobias ska vidareutveckla den tre månader senare.

Det tar honom en vecka att förstå vad Emma menade.

**Skulden betalas alltid — förr eller senare.**
Refactoring är att betala av den i förväg.

---

## Code smell: Lång metod

En metod som gör för mycket är svår att läsa, testa och förstå.

```csharp
// ❌ Allt på ett ställe — vad gör den egentligen?
public void HandleOrder(Order order)
{
    // Validera
    if (order.Items.Count == 0) throw new Exception("Inga varor");
    if (order.CustomerId <= 0) throw new Exception("Ogiltigt kundid");

    // Beräkna totalt
    double total = 0;
    foreach (var item in order.Items)
        total += item.Price * item.Quantity;

    // Spara i databas
    _db.Orders.Add(order);
    _db.SaveChanges();

    // Skicka bekräftelse
    _emailService.Send(order.CustomerEmail, $"Tack! Din order: {total} kr");
}
```

---

## Code smell: God Class

En klass som vet allt och gör allt.

```csharp
// ❌ En klass som ansvarar för ALLT
public class ApplicationManager
{
    public void Login(string user, string pass) { ... }
    public void SaveOrder(Order o) { ... }
    public void SendEmail(string to, string body) { ... }
    public void GenerateReport() { ... }
    public void LogError(string msg) { ... }
    public void UpdateInventory(int id, int qty) { ... }
    public void ProcessPayment(decimal amount) { ... }
}
```

Sju ansvarsområden. Sju anledningar att ändra klassen. Sju sätt att introducera buggar.

---

## Code smell: Magic numbers

Vad betyder `0.25`? Vad är `7`? Ingen aning utan kontext.

```csharp
// ❌ Vad är 0.25? Vad är 7?
public decimal CalculatePrice(decimal basePrice, int days)
{
    if (days > 7)
        return basePrice * 0.25m;

    return basePrice;
}
```

Om samma siffra dyker upp på fem ställen och du behöver ändra den — lycka till.

---

## Code smell: Duplicerad kod

Samma logik på tre ställen. Ni fixar en bugg. Men bara på ett av dem.

```csharp
// ❌ Samma beräkning i tre metoder
public decimal GetMemberPrice(decimal price)
{
    return price - (price * 0.1m);
}

public decimal GetStudentPrice(decimal price)
{
    return price - (price * 0.1m);
}

public decimal GetSeniorPrice(decimal price)
{
    return price - (price * 0.1m);
}
```

---

## Clean Code — namngivning: variabler

Namnet ska berätta vad det är. Inte hur det ser ut.

```csharp
// ❌ Vad är x? Vad är d?
int x = 86400;
bool d = false;

// ✅ Tydligt direkt
int secondsPerDay = 86400;
bool isDelivered = false;
```

En variabel med bra namn behöver ingen kommentar.

---

## Clean Code — namngivning: metoder

En metod ska heta vad den **gör**, inte vad den **är**.

```csharp
// ❌ Vad händer när man kallar DoStuff?
public void DoStuff(Order o) { ... }

// ❌ "Process" är meningslöst — allt processas
public void ProcessData(List<Product> products) { ... }

// ✅ Tydlig avsikt
public void PlaceOrder(Order order) { ... }
public void ApplyDiscount(List<Product> products) { ... }
```

---

## Clean Code — namngivning: klasser

Klassen ska heta **vad den representerar** — ett substantiv, inte ett verb.

```csharp
// ❌ Vad är en "Manager"? Vad hanterar "Handler"?
public class OrderManager { ... }
public class DataHandler { ... }

// ✅ Konkret och specifikt
public class OrderRepository { ... }
public class InvoiceService { ... }
public class CustomerValidator { ... }
```

---

## SRP — Single Responsibility Principle

En klass ska ha **ett** ansvarsområde. En anledning att ändras.

```csharp
// ❌ En klass, tre ansvarsområden
public class UserService
{
    public User GetUser(int id) { ... }         // Datahämtning
    public void SendWelcomeEmail(User u) { ... } // E-post
    public string FormatUserProfile(User u) { } // Presentation
}
```

Om e-posttjänsten byts ut — varför måste `UserService` ändras?

---

## SRP — uppdelat

```csharp
// ✅ Varje klass har ett tydligt ansvar
public class UserRepository
{
    public User GetUser(int id) { ... }
}

public class EmailService
{
    public void SendWelcomeEmail(User u) { ... }
}

public class UserProfileFormatter
{
    public string Format(User u) { ... }
}
```

Nu kan varje del ändras utan att påverka de andra.

---

## DRY — Don't Repeat Yourself

```csharp
// ❌ Samma rabattlogik på tre ställen
public decimal GetMemberPrice(decimal price) => price - (price * 0.1m);
public decimal GetStudentPrice(decimal price) => price - (price * 0.1m);
public decimal GetSeniorPrice(decimal price) => price - (price * 0.1m);

// ✅ Bryt ut till en metod — ändra på ett ställe
private decimal ApplyDiscount(decimal price, decimal rate) => price - (price * rate);

public decimal GetMemberPrice(decimal price) => ApplyDiscount(price, 0.1m);
public decimal GetStudentPrice(decimal price) => ApplyDiscount(price, 0.1m);
public decimal GetSeniorPrice(decimal price) => ApplyDiscount(price, 0.1m);
```

---

## KISS — Keep It Simple, Stupid

Komplex kod är inte smart kod. Det är ett problem.

```csharp
// ❌ Onödig komplexitet
public bool IsEligible(int age)
{
    bool result = false;
    if (age != null)
    {
        if (age >= 18)
        {
            if (age <= 65)
            {
                result = true;
            }
        }
    }
    return result;
}

// ✅ Enkelt och direkt
public bool IsEligible(int age) => age >= 18 && age <= 65;
```

---

## Magic numbers — fixa med konstanter

Ge siffran ett namn. Det kostar ingenting och ger allt.

```csharp
// ❌ Vad är 0.25? Vad är 7?
if (days > 7)
    return basePrice * 0.25m;

// ✅ Nu förstår alla
private const int LongStayThreshold = 7;
private const decimal LongStayDiscount = 0.25m;

if (days > LongStayThreshold)
    return basePrice * LongStayDiscount;
```

---

## Refactoringteknik: Extract Method

Ta ut ett kodblock och ge det ett eget namn.

```csharp
// ❌ Före — allt i HandleOrder
public void HandleOrder(Order order)
{
    if (order.Items.Count == 0) throw new Exception("Inga varor");
    if (order.CustomerId <= 0) throw new Exception("Ogiltigt kundid");

    double total = 0;
    foreach (var item in order.Items)
        total += item.Price * item.Quantity;

    _db.Orders.Add(order);
    _db.SaveChanges();

    _emailService.Send(order.CustomerEmail, $"Tack! Din order: {total} kr");
}
```

---

## Extract Method — efter

```csharp
// ✅ Efter — varje steg har ett eget namn
public void HandleOrder(Order order)
{
    ValidateOrder(order);
    double total = CalculateTotal(order);
    SaveOrder(order);
    SendConfirmation(order, total);
}

private void ValidateOrder(Order order)
{
    if (order.Items.Count == 0) throw new Exception("Inga varor");
    if (order.CustomerId <= 0) throw new Exception("Ogiltigt kundid");
}

private double CalculateTotal(Order order)
{
    double total = 0;
    foreach (var item in order.Items)
        total += item.Price * item.Quantity;
    return total;
}
```

Metoden berättar nu vad den gör — utan att man behöver läsa varje rad.

---

## Refactoringteknik: Rename Variable/Method

Det enklaste du kan göra. Det underskattas konstant.

```csharp
// ❌ Tobias öppnar Emmas fil tre månader senare
int v = GetV();
bool f = CheckF(v);
if (f) DoX(v);

// ✅ Tobias vet direkt vad som händer
int vacationDays = GetRemainingVacationDays();
bool isEligibleForBonus = CheckBonusEligibility(vacationDays);
if (isEligibleForBonus) ApplyYearEndBonus(vacationDays);
```

Rename är gratis. Namnlösa variabler är dyra.

---

## Code review — checklista

Innan ni lämnar in (eller godkänner) kod:

- **Namn** — förstår man vad det är/gör utan att läsa implementationen?
- **Metodlängd** — ryms metoden på skärmen utan scroll?
- **Kommentarer** — förklarar de *varför*, inte *vad*?
- **Duplication** — finns samma logik på mer än ett ställe?
- **Klasser** — har varje klass ett tydligt, enskilt ansvar?
- **Magic numbers** — finns hårdkodade siffror som borde vara konstanter?

Sätt upp den här listan bredvid er IDE. Gå igenom den innan ni pushar.

---

## Sammanfattning

✅ Refactoring = förändra struktur utan att förändra beteende
✅ Teknisk skuld är verklig — och drabbar hela teamet
✅ Code smells: lång metod, God class, magic numbers, duplicerad kod
✅ Namngivning är inte kosmetika — det är arkitektur
✅ SRP: en klass, ett ansvar
✅ DRY: en implementering, inte tre
✅ KISS: enkel kod slår alltid komplex kod
✅ Extract Method och Rename är era viktigaste verktyg
✅ Code review-checklistan är ett proffsverktyg — använd den

---

**Nästa gång: UML och planering**
