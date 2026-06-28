---

title: Övning 3: DIP - Dependency Inversion Principle
author: Marcus Ackre Medina
type: exercise
topic: clean-code
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/3_advanced_oop/exercises/solid/solid_exercise_3.md"
description: "Du ska bygga en notification system som kan skicka meddelanden via olika kanaler. Systemet måste vara testbart och utbytbart."
tags: ["clean-code", "csharp", "dependency", "exercise", "git", "installation", "inversion", "principle", "solid", "övning"]
week_fit: []
---

# Övning 3: DIP - Dependency Inversion Principle

🟢


## Scenario
Du ska bygga en notification system som kan skicka meddelanden via olika kanaler. Systemet måste vara testbart och utbytbart.

## Uppgift

### Del 1: Dålig Kod (Tight Coupling)

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
public class OrderService
{
    private EmailSender _emailSender = new EmailSender();
    private SmsSender _smsSender = new SmsSender();
    private SqlOrderRepository _repository = new SqlOrderRepository();

    public void PlaceOrder(Order order)
    {
        // Validate order
        if (order.TotalAmount < 0)
        {
            throw new Exception("Invalid amount");
        }

        // Spara to database
        _repository.Save(order);

        // Send notifications
        _emailSender.Send(order.CustomerEmail, "Order confirmed!");
        _smsSender.Send(order.CustomerPhone, "Order confirmed!");
    }
}

public class EmailSender
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"Email to {to}: {message}");
    }
}

public class SmsSender
{
    public void Send(string phone, string message)
    {
        Console.WriteLine($"SMS to {phone}: {message}");
    }
}

public class SqlOrderRepository
{
    public void Save(Order order)
    {
        Console.WriteLine($"Saving order {order.Id} to SQL...");
    }
}
```

**Problem:**
- Kan inte testa `OrderService` utan att skicka riktiga emails/SMS
- Kan inte byta från SQL till MongoDB
- Hårt kopplat till konkreta implementationer

### Del 2: Refactora med DIP

Implementera Dependency Inversion genom att:

1. Skapa interface `INotificationService` med metod `Send(string recipient, string message)`
2. Skapa interface `IOrderRepository` med metod `Save(Order order)`
3. Implementera konkreta klasser:
   - `EmailNotificationService`
   - `SmsNotificationService`
   - `PushNotificationService` (BONUS ny kanal!)
   - `SqlOrderRepository`
   - `MongoOrderRepository` (BONUS alternativ!)
4. Injicera dependencies i `OrderService` via constructor
5. Skapa en `CompositeNotificationService` som skickar via flera kanaler

### Del 3: Testing

Skapa en `FakeNotificationService` och `FakeOrderRepository` för testning:

```csharp
var fakeNotifier = new FakeNotificationService();
var fakeRepo = new FakeOrderRepository();

var service = new OrderService(fakeRepo, fakeNotifier);
service.PlaceOrder(order);

// Verifiera att Save och Send anropades
Assert.True(fakeRepo.SaveWasCalled);
Assert.True(fakeNotifier.SendWasCalled);
```

## Exempel Output

```
Production:
Saving order 123 to SQL database...
Email to customer@example.com: Order confirmed!
SMS to +46701234567: Order confirmed!
Push notification to device123: Order confirmed!

Testing:
FAKE: Saved order 123
FAKE: Sent notification to customer@example.com
Test passed!
```

## Bonus
- Skapa en `LoggingNotificationService` decorator som loggar alla notifications
- Implementera `NotificationFactory` för att välja rätt notifier baserat på customer preferences
- Lägg till retry-logic i en `RetryNotificationService` wrapper

<details>
<summary>Lösning</summary>

```csharp
public class Order
{
    public int Id { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
    public decimal TotalAmount { get; set; }
}

// Abstractions (interfaces)
public interface INotificationService
{
    void Send(string recipient, string message);
}

public interface IOrderRepository
{
    void Save(Order order);
}

// Concrete implementations - Email
public class EmailNotificationService : INotificationService
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"Email to {recipient}: {message}");
        // Real SMTP logic here
    }
}

// Concrete implementations - SMS
public class SmsNotificationService : INotificationService
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"SMS to {recipient}: {message}");
        // Real SMS API here
    }
}

// NEW! Push notifications - INGET ändrat i OrderService!
public class PushNotificationService : INotificationService
{
    public void Send(string recipient, string message)
    {
        Console.WriteLine($"Push notification to {recipient}: {message}");
        // Firebase/OneSignal API here
    }
}

// SQL Repository
public class SqlOrderRepository : IOrderRepository
{
    public void Save(Order order)
    {
        Console.WriteLine($"Saving order {order.Id} to SQL database...");
        // EF Core logic here
    }
}

// MongoDB Repository - UTBYTBART!
public class MongoOrderRepository : IOrderRepository
{
    public void Save(Order order)
    {
        Console.WriteLine($"Saving order {order.Id} to MongoDB...");
        // MongoDB driver logic here
    }
}

// Composite pattern för flera notifiers
public class CompositeNotificationService : INotificationService
{
    private readonly List<INotificationService> _services;

    public CompositeNotificationService(params INotificationService[] services)
    {
        _services = services.ToList();
    }

    public void Send(string recipient, string message)
    {
        foreach (var service in _services)
        {
            service.Send(recipient, message);
        }
    }
}

// OrderService - BEROENDE PÅ ABSTRACTIONS!
public class OrderService
{
    private readonly IOrderRepository _repository;
    private readonly INotificationService _notificationService;

    // Dependency Injection via constructor
    public OrderService(
        IOrderRepository repository,
        INotificationService notificationService)
    {
        _repository = repository;
        _notificationService = notificationService;
    }

    public void PlaceOrder(Order order)
    {
        // Validate
        if (order.TotalAmount < 0)
            throw new Exception("Invalid amount");

        // Spara
        _repository.Save(order);

        // Notify
        _notificationService.Send(order.CustomerEmail, "Order confirmed!");
    }
}

// PRODUCTION USAGE
var order = new Order
{
    Id = 123,
    CustomerEmail = "customer@example.com",
    CustomerPhone = "+46701234567",
    TotalAmount = 499
};

// Setup med alla kanaler
var notifier = new CompositeNotificationService(
    new EmailNotificationService(),
    new SmsNotificationService(),
    new PushNotificationService()
);

var repository = new SqlOrderRepository();
var service = new OrderService(repository, notifier);

service.PlaceOrder(order);

// TESTING - Fake implementations
public class FakeNotificationService : INotificationService
{
    public bool SendWasCalled { get; private set; }
    public string LastRecipient { get; private set; }

    public void Send(string recipient, string message)
    {
        SendWasCalled = true;
        LastRecipient = recipient;
        Console.WriteLine($"FAKE: Sent notification to {recipient}");
    }
}

public class FakeOrderRepository : IOrderRepository
{
    public bool SaveWasCalled { get; private set; }
    public Order? LastSavedOrder { get; private set; }

    public void Save(Order order)
    {
        SaveWasCalled = true;
        LastSavedOrder = order;
        Console.WriteLine($"FAKE: Saved order {order.Id}");
    }
}

// Test
var fakeNotifier = new FakeNotificationService();
var fakeRepo = new FakeOrderRepository();
var testService = new OrderService(fakeRepo, fakeNotifier);

testService.PlaceOrder(order);

Console.WriteLine($"\nTest Results:");
Console.WriteLine($"Save called: {fakeRepo.SaveWasCalled}");
Console.WriteLine($"Notification sent: {fakeNotifier.SendWasCalled}");
Console.WriteLine($"Last recipient: {fakeNotifier.LastRecipient}");
```

**Bonus - Logging Decorator:**

```csharp
public class LoggingNotificationService : INotificationService
{
    private readonly INotificationService _inner;

    public LoggingNotificationService(INotificationService inner)
    {
        _inner = inner;
    }

    public void Send(string recipient, string message)
    {
        Console.WriteLine($"[LOG] Sending notification to {recipient}...");
        _inner.Send(recipient, message);
        Console.WriteLine($"[LOG] Notification sent successfully");
    }
}

// Wrap any notifier with logging
var emailWithLogging = new LoggingNotificationService(
    new EmailNotificationService()
);
```

**Bonus - Retry Logic:**

```csharp
public class RetryNotificationService : INotificationService
{
    private readonly INotificationService _inner;
    private readonly int _maxRetries;

    public RetryNotificationService(INotificationService inner, int maxRetries = 3)
    {
        _inner = inner;
        _maxRetries = maxRetries;
    }

    public void Send(string recipient, string message)
    {
        for (int i = 0; i < _maxRetries; i++)
        {
            try
            {
                _inner.Send(recipient, message);
                return; // Success!
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RETRY] Attempt {i + 1} failed: {ex.Message}");
                if (i == _maxRetries - 1) throw;
            }
        }
    }
}
```

</details>

## Diskussion

**Frågor:**
1. Hur lätt är det att byta från SQL till MongoDB nu?
2. Kan du testa `OrderService` utan att skicka riktiga notifications?
3. Vad är fördelen med `CompositeNotificationService`?
4. Hur skulle du implementera en "Slack notification" nu?

**Pair Programming:**
- Implementera `SlackNotificationService`
- Lägg till utan att ändra `OrderService`
- Testa med fake implementation

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
