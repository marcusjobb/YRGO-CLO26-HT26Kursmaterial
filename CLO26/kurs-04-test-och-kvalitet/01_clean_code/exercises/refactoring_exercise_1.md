---

title: Övning 1: Code Smells & Refactoring
author: Marcus Ackre Medina
type: exercise
topic: clean-code
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/3_advanced_oop/exercises/refactoring/refactoring_exercise_1.md"
description: "Du har ärvt legacy kod från en junior utvecklare. Koden fungerar, men är full av code smells. Din uppgift: refactora!"
tags: ["clean-code", "csharp", "exercise", "refactoring", "smells", "visual-studio", "övning"]
week_fit: []
---

# Övning 1: Code Smells & Refactoring

🟢


## Scenario
Du har ärvt legacy kod från en junior utvecklare. Koden fungerar, men är full av code smells. Din uppgift: refactora!

## Uppgift

### Del 1: Identifiera Code Smells

Här är den dåliga koden:

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
public class OrderProcessor
{
    public void ProcessOrder(int orderType, decimal amount, string customerName, string email, bool isPremium)
    {
        // Magic numbers
        if (amount > 1000)
        {
            Console.WriteLine("Large order!");
        }

        // Long method
        decimal discount = 0;
        if (orderType == 1)
        {
            discount = amount * 0.1m;
        }
        else if (orderType == 2)
        {
            discount = amount * 0.15m;
        }
        else if (orderType == 3)
        {
            discount = amount * 0.2m;
        }

        if (isPremium)
        {
            discount += amount * 0.05m;
        }

        decimal total = amount - discount;

        // Duplicate code
        if (email.Contains("@") && email.Contains("."))
        {
            Console.WriteLine("Sending email to " + email);
            Console.WriteLine("Dear " + customerName);
            Console.WriteLine("Your order total: " + total);
            Console.WriteLine("Thanks for shopping!");
        }

        // More duplicate code
        if (email.Contains("@") && email.Contains("."))
        {
            Console.WriteLine("Sending receipt to " + email);
            Console.WriteLine("Dear " + customerName);
            Console.WriteLine("Receipt: " + total);
            Console.WriteLine("Thanks!");
        }
    }
}
```

**Identifiera alla code smells!**

### Del 2: Refactoring Checklist

Fixa följande:

1. **Magic Numbers** - Skapa constants
2. **Long Method** - Bryt ut till mindre metoder
3. **Duplicate Code** - Extract method
4. **Poor Naming** - Byt orderType till enum
5. **Too Many Parameters** - Skapa Order class
6. **Validation** - Extract till metod

### Del 3: Refactored Solution

Målet:

```csharp
public class Order
{
    public OrderType Type { get; set; }
    public decimal Amount { get; set; }
    public Customer Customer { get; set; }
}

public class OrderProcessor
{
    public void ProcessOrder(Order order)
    {
        // Clean, readable code
    }
}
```

## Exempel Output

**Före:**
```
Large order!
Sending email to john@example.com
Dear John Doe
Your order total: 850
Thanks for shopping!
Sending receipt to john@example.com
Dear John Doe
Receipt: 850
Thanks!
```

**Efter (samma output, bättre kod):**
```
Processing large order...
Discount applied: 150 kr
Sending order confirmation to john@example.com
Sending receipt to john@example.com
Order processed successfully!
```

## Bonus
- Implementera SOLID principles
- Lägg till unit tests
- Extract email sending till EmailService

<details>
<summary>Lösning</summary>

```csharp
// Constants
public static class OrderConstants
{
    public const decimal LargeOrderThreshold = 1000m;
    public const decimal RegularDiscount = 0.1m;
    public const decimal BulkDiscount = 0.15m;
    public const decimal WholesaleDiscount = 0.2m;
    public const decimal PremiumBonus = 0.05m;
}

// Enums instead of magic numbers
public enum OrderType
{
    Regular = 1,
    Bulk = 2,
    Wholesale = 3
}

// Value objects
public class Customer
{
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsPremium { get; set; }

    public bool HasValidEmail()
    {
        return !string.IsNullOrEmpty(Email) &&
               Email.Contains("@") &&
               Email.Contains(".");
    }
}

public class Order
{
    public OrderType Type { get; set; }
    public decimal Amount { get; set; }
    public Customer Customer { get; set; }

    public bool IsLargeOrder() => Amount > OrderConstants.LargeOrderThreshold;
}

// Single Responsibility - Email sending
public class EmailService
{
    public void SendOrderConfirmation(Customer customer, decimal total)
    {
        if (!customer.HasValidEmail())
        {
            Console.WriteLine("Cannot send email: Invalid email address");
            return;
        }

        Console.WriteLine($"Sending order confirmation to {customer.Email}");
        Console.WriteLine($"Dear {customer.Name}");
        Console.WriteLine($"Your order total: {total:C}");
        Console.WriteLine("Thanks for shopping!");
    }

    public void SendReceipt(Customer customer, decimal total)
    {
        if (!customer.HasValidEmail())
        {
            Console.WriteLine("Cannot send receipt: Invalid email address");
            return;
        }

        Console.WriteLine($"Sending receipt to {customer.Email}");
        Console.WriteLine($"Dear {customer.Name}");
        Console.WriteLine($"Receipt: {total:C}");
        Console.WriteLine("Thanks!");
    }
}

// Clean processor
public class OrderProcessor
{
    private readonly EmailService _emailService;

    public OrderProcessor(EmailService emailService)
    {
        _emailService = emailService;
    }

    public void ProcessOrder(Order order)
    {
        if (order.IsLargeOrder())
        {
            Console.WriteLine("Processing large order...");
        }

        var discount = CalculateDiscount(order);
        var total = order.Amount - discount;

        Console.WriteLine($"Discount applied: {discount:C}");

        _emailService.SendOrderConfirmation(order.Customer, total);
        _emailService.SendReceipt(order.Customer, total);

        Console.WriteLine("Order processed successfully!");
    }

    private decimal CalculateDiscount(Order order)
    {
        var discount = CalculateTypeDiscount(order);

        if (order.Customer.IsPremium)
        {
            discount += CalculatePremiumBonus(order.Amount);
        }

        return discount;
    }

    private decimal CalculateTypeDiscount(Order order)
    {
        return order.Type switch
        {
            OrderType.Regular => order.Amount * OrderConstants.RegularDiscount,
            OrderType.Bulk => order.Amount * OrderConstants.BulkDiscount,
            OrderType.Wholesale => order.Amount * OrderConstants.WholesaleDiscount,
            _ => 0
        };
    }

    private decimal CalculatePremiumBonus(decimal amount)
    {
        return amount * OrderConstants.PremiumBonus;
    }
}

// Användning
var emailService = new EmailService();
var processor = new OrderProcessor(emailService);

var order = new Order
{
    Type = OrderType.Bulk,
    Amount = 1500m,
    Customer = new Customer
    {
        Name = "John Doe",
        Email = "john@example.com",
        IsPremium = true
    }
};

processor.ProcessOrder(order);
```

**Före vs Efter Jämförelse:**

| Code Smell | Före | Efter |
|------------|------|-------|
| Magic Numbers | `if (amount > 1000)` | `order.IsLargeOrder()` |
| Type Code | `orderType == 1` | `OrderType.Regular` |
| Long Method | 40+ lines | Multiple 5-line methods |
| Duplicate Code | Email logic x2 | EmailService class |
| Many Parameters | 5 parameters | 1 Order object |
| Poor Naming | `orderType: int` | `OrderType: enum` |

**Refactoring Techniques Used:**
1. **Extract Method** - CalculateDiscount, SendEmail
2. **Replace Magic Number with Constant**
3. **Replace Type Code with Enum**
4. **Introduce Parameter Object** - Order class
5. **Extract Class** - EmailService
6. **Replace Conditional with Polymorphism** - Switch expression

</details>

## Diskussion

**Code Review Questions:**
1. Vilka code smells hittade du?
2. Vilka SOLID principles bröts i original?
3. Hur mycket lättare är den nya koden att testa?
4. Vilka ytterligare förbättringar kan göras?

**Pair Exercise:**
- Byt kod med en klasskamrat
- Identifiera code smells i varandras kod
- Refactora tillsammans

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
