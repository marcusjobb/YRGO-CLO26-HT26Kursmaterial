---

title: ![bg left:40%](https://images.pexels.com/photos/2599244/pexels-photo-2599244.jpeg)
author: Marcus Ackre Medina
type: lecture
topic: clean-code
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/3_advanced_oop/lectures/refactoring/refactoring_basics_marp.md"
description: "Förbättra kod UTAN att ändra vad den gör"
tags: ["![bg", "clean-code", "csharp", "left:40%](https://images.pexels.com/photos/2599244/pexels-photo-2599244.jpeg)", "marp", "refactoring", "rider", "ssh", "verktyg", "visual-studio"]
week_fit: []
---

# ![bg left:40%](https://images.pexels.com/photos/2599244/pexels-photo-2599244.jpeg)

🟢


# **Refactoring**

### Städa er kod från vecka 1!
---

## **Vad är Refactoring?** 🧹

**Förbättra kod UTAN att ändra vad den gör**

```csharp
// Före: Fungerar, men ugly
public void DoStuff(int x, int y) { ... }

// Efter: Fungerar lika, men clean
public void CalculateTotal(int price, int quantity) { ... }
```

**Samma output, bättre kod!**

**Som att städa rummet - saker på samma ställe, men ordnat**

---

## **Code Smells** 👃

**Tecken på att kod behöver refactoring:**

🔴 **Long Method** - Metod > 20 rader
🔴 **Duplicate Code** - Samma kod på flera ställen
🔴 **Magic Numbers** - `if (age > 18)` vad betyder 18?
🔴 **Bad Names** - `DoStuff()`, `var x = ...`
🔴 **God Class** - En klass gör ALLT

**Känner ni igen er vecka 1-kod?** 😅

---

## **Extract Method** ❌

```csharp
// Ugly - gör allt i en metod
public void ProcessOrder(Order order)
{
    // Validera
    if (order.Items.Count == 0) throw new Exception();
    if (order.Total < 0) throw new Exception();

    // Beräkna
    var discount = order.Total > 1000 ? order.Total * 0.1m : 0;
    var tax = (order.Total - discount) * 0.25m;
    var finalTotal = order.Total - discount + tax;

    // Spara
    database.Save(order);
    email.Send(order.Email, $"Total: {finalTotal}");

    // ... 50+ rader mer ...
}
```

---

## **Extract Method** ✅

```csharp
// Clean - varje metod gör EN sak
public void ProcessOrder(Order order)
{
    ValidateOrder(order);
    var finalTotal = CalculateFinalTotal(order);
    SaveOrder(order);
    SendConfirmationEmail(order, finalTotal);
}

private void ValidateOrder(Order order)
{
    if (order.Items.Count == 0) throw new Exception();
    if (order.Total < 0) throw new Exception();
}

private decimal CalculateFinalTotal(Order order)
{
    var discount = CalculateDiscount(order.Total);
    var tax = CalculateTax(order.Total - discount);
    return order.Total - discount + tax;
}
```

**Läsbart som en bok!**

---

## **Rename - Bra Namn** 📝

```csharp
// ❌ DÅLIGT
public void DoStuff(int x, int y)
{
    var z = x * y;
    var a = z * 0.25m;
    return z + a;
}

// ✅ BRA
public decimal CalculateTotalWithTax(int price, int quantity)
{
    var subtotal = price * quantity;
    var tax = subtotal * 0.25m;
    return subtotal + tax;
}
```

**Kod ska läsas som engelska!**

---

## **DRY - Don't Repeat Yourself** ❌

```csharp
// Duplicate kod!
public void ProcessCreditCard(Payment p)
{
    if (p.Amount <= 0) throw new Exception("Invalid");
    if (string.IsNullOrEmpty(p.CardNumber)) throw new Exception("No card");
    _db.Payments.Add(p);
    _db.SaveChanges();
}

public void ProcessSwish(Payment p)
{
    if (p.Amount <= 0) throw new Exception("Invalid");
    if (string.IsNullOrEmpty(p.PhoneNumber)) throw new Exception("No phone");
    _db.Payments.Add(p);
    _db.SaveChanges();
}
```

**Samma validation överallt!**

---

## **DRY - Don't Repeat Yourself** ✅

```csharp
// Extract common logic
private void ValidatePayment(Payment payment)
{
    if (payment.Amount <= 0)
        throw new Exception("Invalid amount");
}

private void SavePayment(Payment payment)
{
    _db.Payments.Add(payment);
    _db.SaveChanges();
}

public void ProcessCreditCard(Payment p)
{
    ValidatePayment(p);
    if (string.IsNullOrEmpty(p.CardNumber)) throw new Exception();
    SavePayment(p);
}

public void ProcessSwish(Payment p)
{
    ValidatePayment(p);
    if (string.IsNullOrEmpty(p.PhoneNumber)) throw new Exception();
    SavePayment(p);
}
```

---

## **Magic Numbers → Constants** 🔢

```csharp
// ❌ Magic numbers
if (age >= 18) { ... }
var price = total * 1.25; // Vad är 1.25?
if (score > 100) { ... }

// ✅ Named constants
private const int ADULT_AGE = 18;
private const decimal TAX_RATE = 0.25m;
private const int MAX_SCORE = 100;

if (age >= ADULT_AGE) { ... }
var priceWithTax = total * (1 + TAX_RATE);
if (score > MAX_SCORE) { ... }
```

**Nu förstår man vad talen betyder!**

---

## **När Ska Man Refactorera?** 🤔

**Rule of Three:**
1. Första gången - skriv kod
2. Andra gången - copy-paste (får inte vara för ofta!)
3. Tredje gången - **REFACTOR!**

**Bra tillfällen:**
- Innan du lägger till ny feature
- När du hittar bugg (gör kod lättare att förstå först!)
- Code review (din eller andras kod)
- **NU!** Ni har kod från vecka 1 att städa!

---

## **Verktyg för Refactoring** 🛠️

**Visual Studio / Rider / VS Code:**
- F2 - Rename
- Ctrl+R,M - Extract Method
- Ctrl+R,E - Encapsulate Field
- Ctrl+. - Quick Actions

**SonarLint / ReSharper:**
- Automatisk detection av code smells
- Förslag på refactorings

**Testa själva - högerklick → Refactor!**

---

## **Sammanfattning** 🎯

**Vanliga refactorings:**
- ✅ **Extract Method** - Dela upp långa metoder
- ✅ **Rename** - Ge bra namn
- ✅ **DRY** - Ta bort duplicerad kod
- ✅ **Constants** - Ersätt magic numbers

**Regel:** Fungerar det? REFACTOR INTE!
**Regel 2:** Ska du ändra kod? REFACTOR FÖRST!

**Nu: Kolla er vecka 1-kod och städa!**

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
