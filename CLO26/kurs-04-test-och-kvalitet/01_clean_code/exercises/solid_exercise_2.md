# Övning 2: OCP - Open/Closed Principle

🟢


## Scenario
En webshop har olika discount-regler som ständigt ändras. Din uppgift är att göra systemet utökningsbart utan att ändra befintlig kod.

## Uppgift

### Del 1: Dålig Kod

Här är den nuvarande implementationen:

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
public class Order
{
    public decimal TotalAmount { get; set; }
    public string CustomerType { get; set; }
    public bool IsBlackFriday { get; set; }
    public int ItemCount { get; set; }

    public decimal CalculateDiscount()
    {
        decimal discount = 0;

        if (CustomerType == "Regular")
        {
            discount = TotalAmount * 0.05m;
        }
        else if (CustomerType == "Premium")
        {
            discount = TotalAmount * 0.10m;
        }
        else if (CustomerType == "VIP")
        {
            discount = TotalAmount * 0.20m;
        }

        if (IsBlackFriday)
        {
            discount += TotalAmount * 0.15m;
        }

        if (ItemCount > 10)
        {
            discount += 50;
        }

        return discount;
    }
}
```

**Problem:** Varje ny discount-regel kräver ändring i `CalculateDiscount()` metoden!

### Del 2: Refactora med Strategy Pattern

Implementera OCP genom att:

1. Skapa interface `IDiscountStrategy` med metod `decimal Calculate(Order order)`
2. Skapa konkreta strategier:
   - `RegularCustomerDiscount` (5%)
   - `PremiumCustomerDiscount` (10%)
   - `VIPCustomerDiscount` (20%)
   - `BlackFridayDiscount` (15%)
   - `BulkPurchaseDiscount` (50 kr om >10 items)
3. Skapa `DiscountCalculator` som tar lista av strategier
4. Lägg till ny strategi `ChristmasDiscount` (25%) **UTAN** att ändra befintlig kod

### Del 3: Composite Discounts

Skapa en `CompositeDiscountStrategy` som kan kombinera flera discounts:

```csharp
var discounts = new CompositeDiscountStrategy(
    new VIPCustomerDiscount(),
    new BlackFridayDiscount(),
    new BulkPurchaseDiscount()
);

var total = discounts.Calculate(order);
```

## Exempel Output

```
Order Total: 1000 kr

Applying discounts:
- VIP Customer: -200 kr
- Black Friday: -150 kr
- Bulk Purchase: -50 kr

Total discount: 400 kr
Final price: 600 kr
```

## Bonus
- Implementera `MaxDiscountStrategy` som tar max av flera strategier
- Skapa `ConditionalDiscountStrategy` som tillämpar discount endast om villkor uppfylls
- Lägg till logging i varje strategi

<details>
<summary>Lösning</summary>

```csharp
public class Order
{
    public decimal TotalAmount { get; set; }
    public string CustomerType { get; set; }
    public bool IsBlackFriday { get; set; }
    public int ItemCount { get; set; }
}

// Strategy interface
public interface IDiscountStrategy
{
    decimal Calculate(Order order);
    string Description { get; }
}

// Concrete strategies
public class RegularCustomerDiscount : IDiscountStrategy
{
    public string Description => "Regular Customer";

    public decimal Calculate(Order order)
    {
        return order.CustomerType == "Regular"
            ? order.TotalAmount * 0.05m
            : 0;
    }
}

public class PremiumCustomerDiscount : IDiscountStrategy
{
    public string Description => "Premium Customer";

    public decimal Calculate(Order order)
    {
        return order.CustomerType == "Premium"
            ? order.TotalAmount * 0.10m
            : 0;
    }
}

public class VIPCustomerDiscount : IDiscountStrategy
{
    public string Description => "VIP Customer";

    public decimal Calculate(Order order)
    {
        return order.CustomerType == "VIP"
            ? order.TotalAmount * 0.20m
            : 0;
    }
}

public class BlackFridayDiscount : IDiscountStrategy
{
    public string Description => "Black Friday";

    public decimal Calculate(Order order)
    {
        return order.IsBlackFriday
            ? order.TotalAmount * 0.15m
            : 0;
    }
}

public class BulkPurchaseDiscount : IDiscountStrategy
{
    public string Description => "Bulk Purchase";

    public decimal Calculate(Order order)
    {
        return order.ItemCount > 10 ? 50 : 0;
    }
}

// NY STRATEGI - Ingen ändring i befintlig kod!
public class ChristmasDiscount : IDiscountStrategy
{
    public string Description => "Christmas Special";

    public decimal Calculate(Order order)
    {
        var now = DateTime.Now;
        return now.Month == 12 && now.Day >= 20
            ? order.TotalAmount * 0.25m
            : 0;
    }
}

// Composite pattern
public class CompositeDiscountStrategy : IDiscountStrategy
{
    private readonly List<IDiscountStrategy> _strategies;
    public string Description => "Combined Discounts";

    public CompositeDiscountStrategy(params IDiscountStrategy[] strategies)
    {
        _strategies = strategies.ToList();
    }

    public decimal Calculate(Order order)
    {
        decimal total = 0;

        Console.WriteLine("\nApplying discounts:");
        foreach (var strategy in _strategies)
        {
            var discount = strategy.Calculate(order);
            if (discount > 0)
            {
                Console.WriteLine($"- {strategy.Description}: -{discount} kr");
                total += discount;
            }
        }

        return total;
    }
}

// Calculator
public class DiscountCalculator
{
    public decimal CalculateFinalPrice(Order order, IDiscountStrategy strategy)
    {
        Console.WriteLine($"Order Total: {order.TotalAmount} kr");

        var discount = strategy.Calculate(order);

        Console.WriteLine($"\nTotal discount: {discount} kr");
        Console.WriteLine($"Final price: {order.TotalAmount - discount} kr");

        return order.TotalAmount - discount;
    }
}

// Användning
var order = new Order
{
    TotalAmount = 1000,
    CustomerType = "VIP",
    IsBlackFriday = true,
    ItemCount = 15
};

var discounts = new CompositeDiscountStrategy(
    new VIPCustomerDiscount(),
    new BlackFridayDiscount(),
    new BulkPurchaseDiscount()
);

var calculator = new DiscountCalculator();
calculator.CalculateFinalPrice(order, discounts);

// Lägg till Christmas discount senare - ÖPPEN för utökning!
var winterDiscounts = new CompositeDiscountStrategy(
    new VIPCustomerDiscount(),
    new ChristmasDiscount() // NY! Ingen ändring i befintlig kod
);
```

**Bonus - MaxDiscountStrategy:**

```csharp
public class MaxDiscountStrategy : IDiscountStrategy
{
    private readonly List<IDiscountStrategy> _strategies;
    public string Description => "Best Available Discount";

    public MaxDiscountStrategy(params IDiscountStrategy[] strategies)
    {
        _strategies = strategies.ToList();
    }

    public decimal Calculate(Order order)
    {
        return _strategies.Max(s => s.Calculate(order));
    }
}

// Användning: Ge kunden BÄSTA discount
var bestDeal = new MaxDiscountStrategy(
    new VIPCustomerDiscount(),    // 200 kr
    new BlackFridayDiscount(),    // 150 kr
    new ChristmasDiscount()       // 250 kr
);
// Result: 250 kr (Christmas är bäst!)
```

</details>

## Diskussion

**Frågor:**
1. Hur lätt är det att lägga till en "CyberMonday" discount nu?
2. Kan du testa varje discount strategy isolerat?
3. Vad händer om affärsreglerna ändras för VIP discount?

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
