---
marp: true
theme: default
class: invert
paginate: true
---

# Relationer och CRUD med EF Core

**Kurs:** Databashantering och -design
**Modul:** 06 — Entity Framework Core

---

## Vad ska vi lära oss idag?

- **Relationer** — 1:1, 1:N, N:M i EF Core
- **Navigation Properties** — navigera mellan entiteter
- **Fluent API** — konfiguration i kod
- **Data Annotations** — attribut på entiteter
- **CRUD med relationer** — skapa, läsa, uppdatera med relaterad data
- **Eager vs Lazy Loading**

---

## Relationer i EF Core

Samma som i SQL, men uttryckt i C#-kod:

| Relation | SQL | EF Core |
|----------|-----|---------|
| **1:N** | FK i många-tabellen | `ICollection<T>` + FK-property |
| **1:1** | FK + UNIQUE | Navigation property båda håll |
| **N:M** | Kopplingstabell | Två `ICollection<T>` + implicit junction |

---

## 1:N — En kund har många ordrar

```csharp
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation — en kund har MANGA ordrar
    public ICollection<Order> Orders { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Total { get; set; }

    public int CustomerId { get; set; }      // FK
    public Customer Customer { get; set; }    // Navigation tillbaka
}
```

EF Core hittar FK via konvention: `CustomerId` → `Customer.Id`.

---

## 1:1 — En användare har en profil

```csharp
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public Profile Profile { get; set; }       // Navigation
}

public class Profile
{
    public int Id { get; set; }                // Delad PK med User
    public string Bio { get; set; }
    public string AvatarUrl { get; set; }
    public int UserId { get; set; }            // FK
    public User User { get; set; }
}
```

Obs! EF Core kräver extra konfiguration för 1:1 — använd Fluent API.

---

## N:M — Studenter och kurser

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Course> Courses { get; set; }  // EF Core 5+
}

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Student> Students { get; set; }
}
```

EF Core 5+ skapar automatiskt en kopplingstabell `StudentCourse`.

Före EF Core 5 behövde du skapa kopplingsentiteten manuellt.

---

## Data Annotations — Attribut på entiteter

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Products")]                     // Tabellnamn
public class Product
{
    [Key]                               // Explicit PK
    public int ProductId { get; set; }

    [Required]                          // NOT NULL
    [MaxLength(100)]                    // VARCHAR(100)
    public string Name { get; set; }

    [Column("unit_price")]              // Kolumnnamn i databasen
    public decimal Price { get; set; }

    [Range(0, 9999)]                    // CHECK-constraint
    public int Stock { get; set; }
}
```

---

## Fluent API — Konfiguration i kod

Alternativ till attribut. Mer kraftfullt, håller entiteterna rena:

```csharp
public class AppDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tabellnamn
        modelBuilder.Entity<Product>()
            .ToTable("Products");

        // PK
        modelBuilder.Entity<Product>()
            .HasKey(p => p.ProductId);

        // Obligatorisk + maxlängd
        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Relation 1:N
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)     // Order har en Customer
            .WithMany(c => c.Orders)     // Customer har många Orders
            .HasForeignKey(o => o.CustomerId);
    }
}
```

---

## Data Annotations vs Fluent API

| Aspekt | Data Annotations | Fluent API |
|--------|-----------------|------------|
| Placering | På entitetsklassen | I DbContext.OnModelCreating |
| Läsbarhet | Lätt att se på klassen | Separerat från entiteten |
| Kraft | Grundläggande | Allt går |
| Validering | Inbyggd (`[Required]`) | Kräver extra kod |
| Relationskonfig | Begränsad | Full kontroll |

**Rekommendation:** Fluent API för relationer, Data Annotations för enkla regler på properties.

---

## CRUD med relationer — Skapa

```csharp
using var context = new AppDbContext();

// Skapa kund + order samtidigt
var customer = new Customer { Name = "Anna" };
var order = new Order { OrderDate = DateTime.Now, Total = 299 };

customer.Orders.Add(order);     // Koppla via navigation
context.Customers.Add(customer); // Lägger till både kund och order
context.SaveChanges();
```

EF Core följer navigation properties och lägger till allt som behövs.

---

## CRUD med relationer — Läsa

```csharp
// Utan Include — bara Customer, Orders är null
var customers = context.Customers.ToList();

// Med Include — laddar Orders också (EAGER loading)
var customers = context.Customers
    .Include(c => c.Orders)
    .ToList();

// Flera nivåer (ThenInclude)
var customers = context.Customers
    .Include(c => c.Orders)
        .ThenInclude(o => o.OrderItems)
    .ToList();
```

Utan `Include()` = `null` på navigation properties (om inte Lazy Loading är på).

---

## CRUD med relationer — Uppdatera

```csharp
// Lägg till order till befintlig kund
var customer = context.Customers.FirstOrDefault(c => c.Id == 1);
customer.Orders.Add(new Order { OrderDate = DateTime.Now, Total = 499 });
context.SaveChanges();

// Ändra FK
var order = context.Orders.FirstOrDefault(o => o.Id == 3);
order.CustomerId = 2;  // Flytta ordern till annan kund
context.SaveChanges();
```

---

## CRUD med relationer — Ta bort

```csharp
// Ta bort en kund — vad händer med ordrarna?
var customer = context.Customers
    .Include(c => c.Orders)
    .FirstOrDefault(c => c.Id == 1);

context.Customers.Remove(customer);
context.SaveChanges();

// Beror på Cascade Delete (Cascade = standard för 1:N)
// Antingen: Ordrarna försvinner också
// Eller:    SQL-fel om ordrar finns
```

Konfigurera i Fluent API:
```csharp
modelBuilder.Entity<Order>()
    .HasOne(o => o.Customer)
    .WithMany(c => c.Orders)
    .OnDelete(DeleteBehavior.Restrict); // Förhindra borttagning
```

---

## Eager vs Lazy Loading

| Strategi | När laddas data? | Fördel | Nackdel |
|----------|-----------------|--------|---------|
| **Eager** (Include) | Direkt i samma query | En databasroundtrip | Laddar alltid, även om du inte behöver |
| **Lazy** (Proxies) | När du accessar .Orders | Laddar bara det du behöver | N+1-problemet! |

**N+1-problemet (Lazy):**
```csharp
foreach(var c in context.Customers)  // 1 query
    Console.WriteLine(c.Orders.Count); // N queries (en per kund)
```

**Lösning:** Eager loading för listor, Lazy loading för enstaka.

---

## Filtrera med Include

```csharp
// Hämta kunder som har ordrar över 1000 kr (EF Core 5+)
var customers = context.Customers
    .Include(c => c.Orders.Where(o => o.Total > 1000))
    .ToList();

// Hämta kunder + räkna ordrar
var customers = context.Customers
    .Select(c => new {
        c.Name,
        OrderCount = c.Orders.Count,
        TotalSpent = c.Orders.Sum(o => o.Total)
    })
    .ToList();
```

---

## Projektion med Select

Istället för att ladda hela entiteter, välj bara det du behöver:

```csharp
// DTO (Data Transfer Object) — bara data du behöver
var result = context.Customers
    .Where(c => c.Orders.Any())
    .Select(c => new CustomerSummary {
        Name = c.Name,
        Email = c.Email,
        OrderCount = c.Orders.Count,
        TotalSpent = c.Orders.Sum(o => o.Total)
    })
    .ToList();
```

✅ Mindre data över nätverket
✅ Enklare JSON-serialisering
✅ Ingen risk för Lazy Loading-exception

---

## Sammanfattning

- ✅ 1:N = FK + `ICollection<T>`
- ✅ 1:1 = FK + UNIQUE (kräver Fluent API)
- ✅ N:M = implicit junction (EF Core 5+)
- ✅ Data Annotations = enkla regler på klassen
- ✅ Fluent API = full kontroll i OnModelCreating
- ✅ Eager loading = `Include()` / `ThenInclude()`
- ✅ Projektion med `Select()` = prestanda
- ➡️ Nästa: MySQL, prestanda och produktion

---
