---

title: ![bg left:40%](https://images.pexels.com/photos/2599244/pexels-photo-2599244.jpeg)
author: Marcus Ackre Medina
type: lecture
topic: clean-code
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/3_advanced_oop/lectures/refactoring/refactoring_marp.md"
description: "Refactoring** = Städa och omstrukturera kod **utan** att ändra funktionalitet."
tags: ["![bg", "clean-code", "csharp", "git", "left:40%](https://images.pexels.com/photos/2599244/pexels-photo-2599244.jpeg)", "marp", "refactoring", "rider", "ssh", "verktyg"]
week_fit: []
---
# ![bg left:40%](https://images.pexels.com/photos/2599244/pexels-photo-2599244.jpeg)

# **Refactoring**

### Förbättra befintlig kod utan att ändra beteende
---

## **Vad är Refactoring?**

**Refactoring** = Städa och omstrukturera kod **utan** att ändra funktionalitet.

Tänk på det som **renovering av kod**:
- Samma funktion, bättre struktur
- Lättare att läsa och underhålla
- Färre buggar i framtiden

**Inte**: Lägga till features eller fixa buggar
**Väl**: Göra kod mer läsbar och underhållbar

---

## **Varför Refactoring?**

Kod blir **legacy** över tid:

- Folk slutar, ny kod läggs till, ingen dokumentation
- "Quick fixes" staplas på varandra
- Teknisk skuld växer

**Martin Fowler**: "Refactoring är som att borsta tänderna - gör det ofta, inte en gång om året!"

**Resultat**: Lägre kostnader, snabbare utveckling, färre buggar

---

## **Code Smells - Tecken På Dålig Kod**

| Code Smell | Beskrivning |
|-----------|-------------|
| **Long Method** | Metod > 20 rader |
| **Large Class** | Klass gör för mycket |
| **Duplicate Code** | Samma kod på flera ställen |
| **Magic Numbers** | Hårdkodade värden |
| **Dead Code** | Oanvänd kod |
| **God Class** | En klass gör ALLT |

**Om du känner igen något - dags att refactorera!**

---

## **1. Extract Method**

**Problem**: Lång metod som gör många saker.

```csharp
public void ProcessOrder(Order order)
{
    // Validera
    if (order.Items.Count == 0)
        throw new Exception("No items");
    if (order.Total < 0)
        throw new Exception("Invalid total");

    // Beräkna
    var discount = order.Total > 1000 ? order.Total * 0.1m : 0;
    var tax = (order.Total - discount) * 0.25m;
    var finalTotal = order.Total - discount + tax;

    // Spara
    database.Save(order);
    email.Send(order.CustomerEmail, $"Total: {finalTotal}");
}
```

**50+ rader! Svårt att läsa!**

---

## **Extract Method - Refactored**

```csharp
public void ProcessOrder(Order order)
{
    ValidateOrder(order);
    var finalTotal = CalculateFinalTotal(order);
    SaveOrder(order);
    SendConfirmationEmail(order, finalTotal);
}

private void ValidateOrder(Order order)
{
    if (order.Items.Count == 0)
        throw new Exception("No items");
    if (order.Total < 0)
        throw new Exception("Invalid total");
}

private decimal CalculateFinalTotal(Order order)
{
    var discount = CalculateDiscount(order.Total);
    var tax = CalculateTax(order.Total - discount);
    return order.Total - discount + tax;
}
```

---

## **2. Rename Variable/Method**

**Problem**: Dåliga namn.

```csharp
public void DoStuff(int x, int y)
{
    var z = x * y;
    var a = z * 0.25m;
    var b = z + a;
    return b;
}
```

**Vad gör detta? Ingen aning!**

---

## **Rename - Refactored**

```csharp
public decimal CalculateTotalPrice(int quantity, decimal pricePerItem)
{
    var subtotal = quantity * pricePerItem;
    var tax = subtotal * 0.25m;
    var totalPrice = subtotal + tax;
    return totalPrice;
}
```

**Nu är det kristallklart!**

**Regel**: Kod läses 10x mer än den skrivs - skriv för läsbarhet!

---

## **3. Remove Duplicate Code (DRY)**

**Problem**: Samma kod på flera ställen.

```csharp
public void SendWelcomeEmail(User user)
{
    var smtp = new SmtpClient("smtp.gmail.com", 587);
    smtp.EnableSsl = true;
    smtp.Credentials = new NetworkCredential("user", "pass");
    smtp.Send(new MailMessage("from", user.Email, "Welcome!", "..."));
}

public void SendOrderEmail(User user, Order order)
{
    var smtp = new SmtpClient("smtp.gmail.com", 587);
    smtp.EnableSsl = true;
    smtp.Credentials = new NetworkCredential("user", "pass");
    smtp.Send(new MailMessage("from", user.Email, "Order!", "..."));
}
```

---

## **Remove Duplicate - Refactored**

```csharp
private SmtpClient CreateSmtpClient()
{
    var smtp = new SmtpClient("smtp.gmail.com", 587);
    smtp.EnableSsl = true;
    smtp.Credentials = new NetworkCredential("user", "pass");
    return smtp;
}

private void SendEmail(string to, string subject, string body)
{
    var smtp = CreateSmtpClient();
    smtp.Send(new MailMessage("from@email.com", to, subject, body));
}

public void SendWelcomeEmail(User user) =>
    SendEmail(user.Email, "Welcome!", "...");

public void SendOrderEmail(User user, Order order) =>
    SendEmail(user.Email, "Order!", "...");
```

---

## **4. Replace Magic Numbers**

**Problem**: Hårdkodade värden.

```csharp
public decimal CalculateDiscount(decimal total)
{
    if (total > 1000)
        return total * 0.1m;
    else if (total > 500)
        return total * 0.05m;
    return 0;
}
```

**Vad betyder 1000, 0.1, 500, 0.05?**

---

## **Replace Magic Numbers - Refactored**

```csharp
private const decimal PREMIUM_THRESHOLD = 1000m;
private const decimal STANDARD_THRESHOLD = 500m;
private const decimal PREMIUM_DISCOUNT_RATE = 0.1m;
private const decimal STANDARD_DISCOUNT_RATE = 0.05m;

public decimal CalculateDiscount(decimal total)
{
    if (total > PREMIUM_THRESHOLD)
        return total * PREMIUM_DISCOUNT_RATE;
    else if (total > STANDARD_THRESHOLD)
        return total * STANDARD_DISCOUNT_RATE;
    return 0;
}
```

**Nu förstår alla vad siffrorna betyder!**

---

## **5. Replace Conditional With Polymorphism**

**Problem**: Massa if-else eller switch.

```csharp
public decimal CalculateShipping(Order order)
{
    if (order.ShippingMethod == "Standard")
        return 50m;
    else if (order.ShippingMethod == "Express")
        return 150m;
    else if (order.ShippingMethod == "Overnight")
        return 300m;
    else
        throw new Exception("Unknown shipping");
}
```

---

## **Replace Conditional - Refactored**

```csharp
public interface IShippingStrategy
{
    decimal CalculateCost();
}

public class StandardShipping : IShippingStrategy
{
    public decimal CalculateCost() => 50m;
}

public class ExpressShipping : IShippingStrategy
{
    public decimal CalculateCost() => 150m;
}

// Användning
IShippingStrategy shipping = new ExpressShipping();
var cost = shipping.CalculateCost();
```

**Strategy Pattern! Lägg till ny shipping-metod = Ny klass!**

---

## **6. Introduce Parameter Object**

**Problem**: För många parametrar.

```csharp
public void CreateUser(string firstName, string lastName,
                       string email, string phone,
                       string address, string city,
                       string zipCode, string country)
{
    // ...
}

// Användning
CreateUser("Anna", "Andersson", "anna@email.com", "070-123",
           "Storgatan 1", "Göteborg", "41234", "Sweden");
```

**8 parametrar! Lätt att få fel ordning!**

---

## **Introduce Parameter Object - Refactored**

```csharp
public class UserInfo
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Address Address { get; set; }
}

public void CreateUser(UserInfo userInfo)
{
    // ...
}

// Användning
CreateUser(new UserInfo
{
    FirstName = "Anna",
    LastName = "Andersson",
    Email = "anna@email.com",
    Address = new Address { City = "Göteborg" }
});
```

---

## **7. Extract Class**

**Problem**: Klass gör för mycket (God Class).

```csharp
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }

    // Adress-relaterat
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }

    // Betalnings-relaterat
    public string CardNumber { get; set; }
    public string CardExpiry { get; set; }
    public string CardCvv { get; set; }
}
```

---

## **Extract Class - Refactored**

```csharp
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
    public PaymentInfo PaymentInfo { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}

public class PaymentInfo
{
    public string CardNumber { get; set; }
    public string CardExpiry { get; set; }
    public string CardCvv { get; set; }
}
```

**Single Responsibility Principle!**

---

## **8. Remove Dead Code**

**Problem**: Oanvänd kod.

```csharp
public class OrderService
{
    // Använd
    public void ProcessOrder(Order order) { }

    // ALDRIG använd!
    public void OldProcessOrder(Order order) { }
    public void DebugMethod() { }
    public void TestStuff() { }

    // Kommenterad kod
    // public void SomethingOld() { }
}
```

**Ta bort allt oanvänt! Version control sparar historiken.**

---

## **Refactoring Workflow**

1. **Skriv tester** (eller verifiera befintliga)
2. **Identifiera code smell**
3. **Refactorera i små steg**
4. **Kör tester** efter varje steg
5. **Commit ofta**

**Aldrig**: Refactorera + Lägg till features samtidigt!

**Alltid**: En sak i taget!

---

## **Refactoring Tools i IDE**

**Visual Studio / Rider shortcuts:**

- `Ctrl + R, M` - Extract Method
- `Ctrl + R, R` - Rename
- `Ctrl + R, V` - Extract Variable
- `Ctrl + R, I` - Extract Interface

**ReSharper**: Ännu mer kraftfulla refactoring-verktyg!

**Använd IDE:n - låt den göra jobbet!**

---

## **Refactoring vs Rewriting**

**Refactoring**:
- Små steg
- Alltid fungerande kod
- Låg risk

**Rewriting**:
- Kasta allt och börja om
- Högrisk
- Ofta går det fel

**Joel Spolsky**: "Never rewrite from scratch!"

**Refactorera inkrementellt istället!**

---

## **Teknisk Skuld**

Kod blir sämre över tid = **Teknisk skuld**

**Medveten skuld**: "Quick fix nu, refactorera senare"
**Omedveten skuld**: "Visste inte bättre när jag skrev detta"

**Betala av skulden regelbundet!**

Sätt av tid varje sprint för refactoring.

---

## **Boy Scout Rule**

**"Lämna koden renare än du hittade den"**

```csharp
// Innan du börjar jobba i filen
public void ProcessData(int x) { ... }

// Efter din ändring
public void ProcessData(int userId)  // Bättre namn!
{
    ValidateUserId(userId);  // Extract method!
    ...
}
```

**Små förbättringar varje gång = Stor skillnad över tid!**

---

## **Refactoring Exempel - Före**

```csharp
public class OrderProcessor
{
    public void Process(Order o)
    {
        if (o.Items.Count == 0) throw new Exception("Empty");
        var t = 0m;
        foreach (var i in o.Items)
            t += i.Price * i.Qty;
        if (t > 1000) t = t * 0.9m;
        t = t * 1.25m;
        o.Total = t;
        db.Save(o);
        email.Send(o.Customer.Email, "Order: " + t);
    }
}
```

**Code smells**: Magic numbers, dåliga namn, lång metod, no SRP

---

## **Refactoring Exempel - Efter**

```csharp
public class OrderProcessor
{
    private const decimal DISCOUNT_THRESHOLD = 1000m;
    private const decimal DISCOUNT_RATE = 0.1m;
    private const decimal TAX_RATE = 0.25m;

    public void Process(Order order)
    {
        ValidateOrder(order);
        order.Total = CalculateTotal(order);
        SaveOrder(order);
        SendConfirmation(order);
    }

    private void ValidateOrder(Order order)
    {
        if (!order.Items.Any())
            throw new InvalidOperationException("Order is empty");
    }

    private decimal CalculateTotal(Order order)
    {
        var subtotal = CalculateSubtotal(order);
        var discount = ApplyDiscount(subtotal);
        var total = ApplyTax(subtotal - discount);
        return total;
    }
}
```

---

## **SOLID Principles & Refactoring**

Refactorera mot SOLID:

- **S**ingle Responsibility - En klass, ett ansvar
- **O**pen/Closed - Öppen för utökning, stängd för ändring
- **L**iskov Substitution - Subklasser ska kunna ersätta basklasser
- **I**nterface Segregation - Små, specifika interfaces
- **D**ependency Inversion - Beroende på abstraktioner

**God kod följer SOLID!**

---

## **Praktisk Övning - Denna Vecka**

**Uppgift 1**: Ta er "smutsiga" kod från tidigare projekt
- Identifiera code smells
- Applicera Extract Method
- Byt namn på variabler/metoder

**Uppgift 2**: Refactorera till patterns
- God Class → Extract Class + Repository Pattern
- If-else → Strategy Pattern
- Magic numbers → Constants

**Uppgift 3**: Code review i par
- Granska varandras refactoring
- Ge konstruktiv feedback

---

## **Sammanfattning - Dagens Lärande**

Ni har lärt er:

- ✅ **Code smells** - Hur man känner igen dålig kod
- ✅ **Extract Method** - Bryt ner långa metoder
- ✅ **Rename** - Bättre namn = Bättre kod
- ✅ **Remove Duplication** - DRY principle
- ✅ **Replace Magic Numbers** - Constants
- ✅ **Refactoring workflow** - Små steg, testa ofta
- ✅ **Boy Scout Rule** - Lämna kod renare

**Refactorera ofta, i små steg!**

---

## **Nästa Steg**

**Tisdag (Självstudier)**:
- Läs: Martin Fowler - Refactoring
- Läs: Clean Code principles
- Identifiera: Code smells i egna projekt

**Onsdag (Campus)**:
- Live refactoring session
- Pair programming refactoring
- Code review workshop

**Torsdag (Campus)**:
- UML för refactored kod
- Git best practices för refactoring commits

---

## **Källor & Resurser**

**Böcker:**
- Martin Fowler: Refactoring
- Robert C. Martin: Clean Code
- Steve McConnell: Code Complete

**Online:**
- Refactoring Guru: https://refactoring.guru
- Clean Code cheat sheet
- ReSharper refactoring guide

**Tools:**
- ReSharper (Visual Studio)
- Rider (JetBrains)
- SonarQube (code quality)

**Koda vilt och refactorera! 🚀**
