# Relationer, Prestanda och Produktion med EF Core

## Relationer i EF Core

EF Core stödjer samma relationstyper som relationsdatabaser, men uttrycker dem med C#-navigation properties.

### 1:N (En-till-många)

Den vanligaste relationen. En parent-entitet har en samling child-entiteter.

```csharp
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }  // Många
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }          // FK
    public Category Category { get; set; }        // Navigation tillbaka
}
```

EF Core hittar relationen via konvention: `CategoryId` matchar `Category.Id`.

### 1:1 (En-till-en)

Mindre vanlig. Används när en entitet har exakt en av en annan.

```csharp
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public Profile Profile { get; set; }
}

public class Profile
{
    public int Id { get; set; }
    public string Bio { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
```

Kräver Fluent API för att säkerställa att UserId har UNIQUE-constraint:
```csharp
modelBuilder.Entity<Profile>()
    .HasIndex(p => p.UserId)
    .IsUnique();
```

### N:M (Många-till-många)

I EF Core 5+ kan du skriva detta direkt:

```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Course> Courses { get; set; }
}

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Student> Students { get; set; }
}
```

EF Core skapar automatiskt kopplingstabellen `StudentCourse`. Du kan även definiera den explicit om du vill lägga till extra data (t.ex. `EnrolledDate`).

## Data Annotations vs Fluent API

### Data Annotations — Attribut på entiteten

```csharp
[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Column("price", TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
}
```

**Fördelar:**
- Allt syns direkt på entitetsklassen
- Enkelt att förstå för den som läser koden
- Validering fungerar i ASP.NET Core (Model Binding)

### Fluent API — Konfiguration i DbContext

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Product>(entity =>
    {
        entity.ToTable("Products");
        entity.HasKey(p => p.Id);
        entity.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);
        entity.Property(p => p.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(10,2)");
        entity.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
    });
}
```

**Fördelar:**
- Entitetsklasserna förblir rena (inga attribut)
- Mer kraftfullt — all konfiguration är möjlig
- All databaslogik samlad på ett ställe

**Rekommendation:** Använd båda. Data Annotations för enkla regler på properties, Fluent API för relationer och komplex konfiguration.

## Laddningsstrategier

### Eager Loading (Ladda direkt)

Laddar relaterad data i samma query med `Include()`:

```csharp
// En query med JOIN
var orders = context.Orders
    .Include(o => o.Customer)
    .ThenInclude(c => c.Profile)
    .ToList();
```

**När:** Du vet att du behöver den relaterade datan direkt.

### Explicit Loading (Ladda på begäran)

Laddar relaterad data efter att huvud-entiteten är hämtad:

```csharp
var customer = context.Customers.First();
context.Entry(customer).Collection(c => c.Orders).Load();
```

**När:** Du behöver relaterad data ibland, men inte alltid.

### Lazy Loading (Ladda automatiskt vid access)

Kräver installation av `Microsoft.EntityFrameworkCore.Proxies`:

```csharp
optionsBuilder.UseLazyLoadingProxies();

public class Customer
{
    public int Id { get; set; }
    public virtual ICollection<Order> Orders { get; set; }  // virtual!
}
```

**När:** Du vill att navigering ska fungera utan `Include()`.

**Farligt:** N+1-problemet! En loop över Customers → en query per Customer.undvik för listor.

## Prestandaoptimering

### AsNoTracking

EF Core spårar alla entiteter den hämtar (för att kunna upptäcka ändringar). Det kostar minne och CPU.

```csharp
// Read-only queries — inget kommer ändras
var products = context.Products
    .AsNoTracking()
    .Where(p => p.Price > 100)
    .ToList();

// Globalt — alla queries blir NoTracking
optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
```

**Vinst:** 30-50% snabbare för read-only queries.

### AsSplitQuery

Vid komplexa Includes kan standard-JOIN ge mycket duplicerad data:

```csharp
var customers = context.Customers
    .Include(c => c.Orders)
    .ThenInclude(o => o.OrderItems)
    .AsSplitQuery()  // Flera queries istället för en stor JOIN
    .ToList();
```

Använd alltid `AsSplitQuery()` när du har flera nivåer av `Include()`.

### ExecuteDelete / ExecuteUpdate (EF Core 7+)

Ta bort eller uppdatera utan att ladda in data:

```csharp
// GAMMALT: Ladda + ta bort
var oldProducts = context.Products.Where(p => p.Stock == 0).ToList();
context.Products.RemoveRange(oldProducts);
context.SaveChanges();

// NYTT (EF 7+): Direkt SQL, en query
context.Products.Where(p => p.Stock == 0).ExecuteDelete();

// Uppdatera direkt
context.Products
    .Where(p => p.Price > 1000)
    .ExecuteUpdate(p => p.SetProperty(x => x.Price, x => x.Price * 0.9));
```

### Paginering

Använd alltid `Skip`/`Take` för listor:

```csharp
int page = 1;
int pageSize = 20;

var products = context.Products
    .OrderBy(p => p.Name)
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToList();
```

### Index

Lägg till index på kolumner du ofta filtrerar eller sorterar på:

```csharp
// Data Annotation
[Index(nameof(Email), IsUnique = true)]
public class Customer { ... }

// Fluent API
modelBuilder.Entity<Customer>()
    .HasIndex(c => c.Email)
    .IsUnique();
```

## SQL Injection — Varför EF Core är säkert

När du använder LINQ-frågor genererar EF Core **parameterized SQL** — användarinmatning separeras från SQL-koden:

```csharp
// Detta är ALLTID säkert i EF Core:
var user = context.Users
    .FirstOrDefault(u => u.Username == userInput);

// Genererad SQL:
// SELECT * FROM Users WHERE Username = @p0
// @p0 = 'injected_value'  — behandlas som TEXT, inte SQL
```

**Aldrig någonsin bygg LINQ-filter med strängkonkatenering.**

## Anslutning till MySQL

### Med Docker

```bash
docker run --name mysql \
    -e MYSQL_ROOT_PASSWORD=hemligt \
    -e MYSQL_DATABASE=minDb \
    -p 3306:3306 \
    -d mysql:8
```

### I ASP.NET Core (appsettings.json)

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=minDb;User=root;Password=hemligt;"
  }
}
```

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Default"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))
    ));
```

### Anslutningssträng-säkerhet

| Miljö | Spara anslutningssträng |
|-------|------------------------|
| Lokal utv. | `dotnet user-secrets` eller appsettings.Development.json |
| Repo | ALDRIG med lösenord |
| CI/CD | Environment Variables / GitHub Secrets |
| Produktion | Environment Variables / Key Vault |

## Summering av prestandaregler

1. **AsNoTracking** för alla read-only queries
2. **AsSplitQuery** för komplexa Includes
3. **Batcha SaveChanges** — inte en per entitet
4. **Paginera** listor med Skip/Take
5. **Index** på kolumner du söker på
6. **ExecuteDelete/ExecuteUpdate** för bulk-operationer
7. **Projicera** med Select istället för att ladda hela entiteter
8. **Använd using** för DbContext — kort livscykel
