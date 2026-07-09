# ![bg left:40%](https://images.pexels.com/photos/1181472/pexels-photo-1181472.jpeg)

🟢


# **SOLID Principles**

### 5 principer för bättre OOP-kod
---

## **SOLID** 🧱

- **S** - Single Responsibility
- **O** - Open/Closed
- **L** - Liskov Substitution
- **I** - Interface Segregation
- **D** - Dependency Inversion

**= Testbar, underhållbar, flexibel kod**

---

## **S - Single Responsibility** ❌

```csharp
// EN klass, för MÅNGA ansvar
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }

    public void SaveToDatabase() { /* SQL */ }
    public void SendEmail() { /* SMTP */ }
    public void ValidateUser() { /* Validation */ }
    public void GenerateReport() { /* PDF */ }
}
```

**Problem:** Ändring i databas påverkar email-kod!

---

## **S - Single Responsibility** ✅

```csharp
// En klass, ett ansvar
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UserRepository
{
    public void Save(User user) { }
}

public class EmailService
{
    public void Send(User user, string message) { }
}

public class UserValidator
{
    public bool Validate(User user) { }
}
```

---

## **O - Open/Closed** ❌

```csharp
// Stängd för utökning
public class DiscountCalculator
{
    public decimal Calculate(decimal price, string type)
    {
        if (type == "Regular") return price * 0.95m;
        if (type == "Premium") return price * 0.9m;
        if (type == "VIP") return price * 0.8m;
        // Ny typ? Ändra denna metod! BAD!
        return price;
    }
}
```

---

## **O - Open/Closed** ✅

```csharp
// Öppen för utökning, stängd för modifiering
public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal price);
}

public class RegularDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price * 0.95m;
}

public class PremiumDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price * 0.9m;
}

// Lägg till ny utan att ändra existerande kod!
public class StudentDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price) => price * 0.85m;
}
```

---

## **L - Liskov Substitution**

**Derived class kan ersätta base class utan problem**

```csharp
// Base
public abstract class Shape
{
    public abstract int GetArea();
}

// Derived - fungerar som Shape!
public class Rectangle : Shape
{
    public int Width { get; set; }
    public int Height { get; set; }
    public override int GetArea() => Width * Height;
}

public class Square : Shape
{
    public int Side { get; set; }
    public override int GetArea() => Side * Side;
}

// Båda fungerar som Shape!
Shape shape1 = new Rectangle();
Shape shape2 = new Square();
```

---

## **I - Interface Segregation**

**Små specifika interfaces, inte feta**

```csharp
// ❌ Stor interface
public interface IWorker { void Work(); void Eat(); void Sleep(); }

// Robot måste implementera Eat/Sleep även om den inte gör det!

// ✅ Små interfaces
public interface IWorkable { void Work(); }
public interface IFeedable { void Eat(); }
public interface ISleepable { void Sleep(); }

public class Human : IWorkable, IFeedable, ISleepable { }
public class Robot : IWorkable { } // Bara Work!
```

---

## **D - Dependency Inversion** ❌

```csharp
// Hårdkopplad till konkret klass
public class UserService
{
    private SqlUserRepository _repo = new SqlUserRepository();

    public void AddUser(User user)
    {
        _repo.Save(user);
    }
}
```

**Problem:** Kan inte byta databas, kan inte testa!

---

## **D - Dependency Inversion** ✅

```csharp
// Beroende på interface
public interface IUserRepository
{
    void Save(User user);
}

public class SqlUserRepository : IUserRepository
{
    public void Save(User user) { /* SQL */ }
}

public class UserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo) // Inject!
    {
        _repo = repo;
    }

    public void AddUser(User user) => _repo.Save(user);
}

// Utbytbart! Testbart!
var service = new UserService(new SqlUserRepository());
var service2 = new UserService(new MongoUserRepository());
```

---

## **Sammanfattning** 🎯

| Princip | Betyder |
|---------|---------|
| **S** | En klass, ett ansvar |
| **O** | Utöka med nya klasser, ändra inte gamla |
| **L** | Derived fungerar som base |
| **I** | Små interfaces |
| **D** | Beroende på interface, inte class |

**Ni kommer använda SRP, OCP och DIP mest!**

**Nu: Entity Framework!**

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
