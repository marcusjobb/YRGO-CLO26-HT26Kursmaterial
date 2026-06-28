---

title: 🗺️ AutoMapper - En Snäll Guide
author: Marcus Ackre Medina
type: assignment
topic: projekt
difficulty: 1
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/4_test_and_quality_assurance/assignment/articles/automapper_guide.md"
description: "Varför och hur man använder AutoMapper i API:er"
tags: ["automapper", "csharp", "installation", "projekt", "snäll", "ssh", "visual-studio"]
week_fit: []
---

# 🗺️ AutoMapper - En Snäll Guide

🟢


**Varför och hur man använder AutoMapper i API:er**

---

## 🤔 Problemet: Varför behöver vi AutoMapper?

### Scenario: Kund-API

```csharp
// Detta är din Entity (databas-modell)
public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } // 🔒 KÄNSLIG!
    public string SocialSecurityNumber { get; set; } // 🔒 KÄNSLIG!
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    // Navigation properties
    public List<Order> Orders { get; set; }
    public Address Address { get; set; }
}
```

**Problem 1: Du vill INTE skicka allt detta till frontend!**

```json
❌ DÅLIGT - Skickar allt från Entity
{
  "id": 1,
  "firstName": "Anna",
  "lastName": "Svensson",
  "email": "anna@test.se",
  "passwordHash": "hashed_secret_123", ⚠️ FARLIGT!
  "socialSecurityNumber": "19900101-1234", ⚠️ FARLIGT!
  "createdAt": "2025-01-01T10:00:00",
  "updatedAt": "2025-01-15T14:30:00",
  "isDeleted": false,
  "orders": [...], // Kanske massa data
  "address": {...}
}
```

**Problem 2: Manuell mappning är tråkigt**

```csharp
// ❌ DÅLIGT - Manual mapping överallt
[HttpGet]
public IActionResult GetAll()
{
    var customers = _db.Customers.ToList();

    // Manuell mappning för VARJE endpoint 😫
    var dtos = customers.Select(c => new CustomerDto
    {
        Id = c.Id,
        FullName = c.FirstName + " " + c.LastName,
        Email = c.Email,
        MemberSince = c.CreatedAt
    }).ToList();

    return Ok(dtos);
}

[HttpGet("{id}")]
public IActionResult GetById(int id)
{
    var customer = _db.Customers.Find(id);

    // Samma mappning IGEN 🤦
    var dto = new CustomerDto
    {
        Id = customer.Id,
        FullName = customer.FirstName + " " + customer.LastName,
        Email = customer.Email,
        MemberSince = customer.CreatedAt
    };

    return Ok(dto);
}
```

**Problem 3: Svårt att underhålla**

```
Vad händer om du lägger till fält i Customer?
→ Du måste uppdatera mappning i 10 olika endpoints 💀
```

---

## 💡 Lösningen: DTOs + AutoMapper

### Vad är en DTO?

**DTO = Data Transfer Object**

En DTO är en "presentation" av din data - vad API:et faktiskt skickar.

```csharp
// DTO - Vad API:et skickar
public class CustomerDto
{
    public int Id { get; set; }
    public string FullName { get; set; } // Kombinerad!
    public string Email { get; set; }
    public DateTime MemberSince { get; set; }
    // Inga känsliga fält!
    // Inga databas-specifika fält!
}
```

**Fördelar:**

- ✅ Säkrare (inget lösenord, personnummer)
- ✅ Renare (mindre data över nätverket)
- ✅ Flexibelt (kan kombinera/formatera fält)
- ✅ Oberoende (Entity kan ändras utan att API:et ändras)

### Vad är AutoMapper?

**AutoMapper** = Ett bibliotek som automatiskt mappar mellan Entity och DTO

```
Entity (Databas) → AutoMapper → DTO (API)
```

---

## 🚀 Hur använder man AutoMapper?

### Steg 1: Installera NuGet Package

```bash
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```

### Steg 2: Skapa DTOs

```csharp
// CustomerDto.cs - För GET requests
public class CustomerDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime MemberSince { get; set; }
}

// CreateCustomerDto.cs - För POST requests
public class CreateCustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

// UpdateCustomerDto.cs - För PUT requests
public class UpdateCustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
```

### Steg 3: Skapa Mapping Profile

```csharp
using AutoMapper;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        // Entity → DTO (GET)
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.MemberSince,
                opt => opt.MapFrom(src => src.CreatedAt));

        // DTO → Entity (POST)
        CreateMap<CreateCustomerDto, Customer>();

        // DTO → Entity (PUT)
        CreateMap<UpdateCustomerDto, Customer>();
    }
}
```

**Förklaring:**

```csharp
CreateMap<Source, Destination>()
```

- **Source:** Vad du mappar FRÅN (Customer Entity)
- **Destination:** Vad du mappar TILL (CustomerDto)

```csharp
.ForMember(dest => dest.FullName, ...)
```

- Används när destination-fältet inte matchar source
- `FullName` finns inte i Customer - måste kombineras!

```csharp
opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
```

- Hur vi skapar FullName från FirstName + LastName

### Steg 4: Registrera i Program.cs

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registrera AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();
app.Run();
```

### Steg 5: Använd i Controller

```csharp
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;
    private readonly IMapper _mapper; // AutoMapper

    public CustomersController(
        ICustomerService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/customers
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _service.GetAllAsync();

        // ✅ AutoMapper gör jobbet!
        var dtos = _mapper.Map<List<CustomerDto>>(customers);

        return Ok(dtos);
    }

    // GET: api/customers/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _service.GetByIdAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        // ✅ Mappe EN customer
        var dto = _mapper.Map<CustomerDto>(customer);

        return Ok(dto);
    }

    // POST: api/customers
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCustomerDto dto)
    {
        // DTO → Entity
        var customer = _mapper.Map<Customer>(dto);

        var created = await _service.CreateAsync(customer);

        // Entity → DTO (för response)
        var resultDto = _mapper.Map<CustomerDto>(created);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            resultDto
        );
    }

    // PUT: api/customers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateCustomerDto dto)
    {
        var existing = await _service.GetByIdAsync(id);

        if (existing == null)
        {
            return NotFound();
        }

        // Uppdatera existing med data från dto
        _mapper.Map(dto, existing);

        await _service.UpdateAsync(existing);

        return NoContent();
    }
}
```

---

## 🎯 AutoMapper Patterns

### Pattern 1: Enkla Mappningar (Automatiska)

**Om fältnamnen matchar - inget behövs!**

```csharp
public class Customer
{
    public int Id { get; set; }
    public string Email { get; set; }
}

public class CustomerDto
{
    public int Id { get; set; }
    public string Email { get; set; }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // AutoMapper mappar automatiskt Id→Id och Email→Email
        CreateMap<Customer, CustomerDto>();
    }
}
```

### Pattern 2: Kombinera Fält

```csharp
public class Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class CustomerDto
{
    public string FullName { get; set; }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src =>
                    $"{src.FirstName} {src.LastName}"));
    }
}
```

### Pattern 3: Byt Namn på Fält

```csharp
public class Customer
{
    public DateTime CreatedAt { get; set; }
}

public class CustomerDto
{
    public DateTime MemberSince { get; set; }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.MemberSince,
                opt => opt.MapFrom(src => src.CreatedAt));
    }
}
```

### Pattern 4: Nested Objects

```csharp
public class Customer
{
    public Address Address { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
}

public class CustomerDto
{
    public string Street { get; set; }
    public string City { get; set; }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.Street,
                opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.City,
                opt => opt.MapFrom(src => src.Address.City));
    }
}
```

**ELLER flatten automatiskt:**

```csharp
public class CustomerDto
{
    public string AddressStreet { get; set; } // Auto-maps från Address.Street
    public string AddressCity { get; set; }   // Auto-maps från Address.City
}

CreateMap<Customer, CustomerDto>(); // Inget ForMember behövs!
```

### Pattern 5: Ignorera Fält

```csharp
public class Customer
{
    public int Id { get; set; }
    public string PasswordHash { get; set; }
}

public class CustomerDto
{
    public int Id { get; set; }
    // Ingen PasswordHash - bra!
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.Ignore()); // Ignorera vid mappning
    }
}
```

### Pattern 6: Conditional Mapping

```csharp
CreateMap<Customer, CustomerDto>()
    .ForMember(dest => dest.Email,
        opt => opt.MapFrom(src =>
            src.IsEmailPublic ? src.Email : "***@***.***"));
```

### Pattern 7: Value Transformers

```csharp
CreateMap<Customer, CustomerDto>()
    .ForMember(dest => dest.Email,
        opt => opt.MapFrom(src => src.Email.ToLower()));
```

---

## 🧪 Testa AutoMapper Mappningar

**VIKTIGT: Testa att mappningar fungerar!**

```csharp
public class MappingTests
{
    private readonly IMapper _mapper;

    public MappingTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerProfile>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void CustomerProfile_IsValid()
    {
        // Arrange
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerProfile>();
        });

        // Act & Assert
        config.AssertConfigurationIsValid(); // Kastar exception om fel!
    }

    [Fact]
    public void Map_Customer_To_CustomerDto()
    {
        // Arrange
        var customer = new Customer
        {
            Id = 1,
            FirstName = "Anna",
            LastName = "Svensson",
            Email = "anna@test.se",
            CreatedAt = new DateTime(2025, 1, 1)
        };

        // Act
        var dto = _mapper.Map<CustomerDto>(customer);

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal("Anna Svensson", dto.FullName);
        Assert.Equal("anna@test.se", dto.Email);
        Assert.Equal(new DateTime(2025, 1, 1), dto.MemberSince);
    }

    [Fact]
    public void Map_CreateCustomerDto_To_Customer()
    {
        // Arrange
        var dto = new CreateCustomerDto
        {
            FirstName = "Bob",
            LastName = "Builder",
            Email = "bob@test.se"
        };

        // Act
        var customer = _mapper.Map<Customer>(dto);

        // Assert
        Assert.Equal("Bob", customer.FirstName);
        Assert.Equal("Builder", customer.LastName);
        Assert.Equal("bob@test.se", customer.Email);
    }
}
```

---

## 📦 Komplett Exempel

**Entities:**

```csharp
public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }

    public List<Order> Orders { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }
}
```

**DTOs:**

```csharp
public class CustomerDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime MemberSince { get; set; }
    public int TotalOrders { get; set; }
}

public class CreateCustomerDto
{
    [Required]
    [MinLength(2)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(2)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}
```

**Mapping Profile:**

```csharp
public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        // Entity → DTO
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.MemberSince,
                opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.TotalOrders,
                opt => opt.MapFrom(src => src.Orders.Count));

        // CreateDTO → Entity
        CreateMap<CreateCustomerDto, Customer>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => HashPassword(src.Password)))
            .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive,
                opt => opt.MapFrom(src => true));
    }

    private string HashPassword(string password)
    {
        // Använd BCrypt eller liknande i verkligheten
        return $"hashed_{password}";
    }
}
```

**Controller:**

```csharp
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public CustomersController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _db.Customers
            .Include(c => c.Orders)
            .ToListAsync();

        var dtos = _mapper.Map<List<CustomerDto>>(customers);

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _db.Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null)
        {
            return NotFound();
        }

        var dto = _mapper.Map<CustomerDto>(customer);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCustomerDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var customer = _mapper.Map<Customer>(dto);

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();

        var resultDto = _mapper.Map<CustomerDto>(customer);

        return CreatedAtAction(
            nameof(GetById),
            new { id = customer.Id },
            resultDto
        );
    }
}
```

---

## 🎯 Best Practices

### 1. En Profile per Entity

```
CustomerProfile.cs
OrderProfile.cs
ProductProfile.cs
```

### 2. Testa Mappningar

```csharp
config.AssertConfigurationIsValid();
```

### 3. Olika DTOs för olika operationer

```
CustomerDto         - GET
CreateCustomerDto   - POST
UpdateCustomerDto   - PUT
```

### 4. Använd Validation Attributes på DTOs

```csharp
public class CreateCustomerDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
```

### 5. Projection i EF Core

```csharp
// ✅ BRA - Mappar i databasen
var dtos = await _db.Customers
    .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
    .ToListAsync();

// ❌ DÅLIGT - Hämtar allt först
var customers = await _db.Customers.ToListAsync();
var dtos = _mapper.Map<List<CustomerDto>>(customers);
```

---

## 🆚 Före och Efter AutoMapper

**FÖRE (Manuellt):**

```csharp
var dtos = customers.Select(c => new CustomerDto
{
    Id = c.Id,
    FullName = c.FirstName + " " + c.LastName,
    Email = c.Email,
    MemberSince = c.CreatedAt,
    TotalOrders = c.Orders.Count
}).ToList();
```

**EFTER (AutoMapper):**

```csharp
var dtos = _mapper.Map<List<CustomerDto>>(customers);
```

**Fördelar:**

- ✅ Mindre kod
- ✅ Lättare att underhålla
- ✅ Konsekvent mapping överallt
- ✅ Testbart
- ✅ Mindre risk för buggar

---

## 🎉 Sammanfattning

**AutoMapper = Automatisk mappning mellan Entity och DTO**

**Varför?**

- Säkerhet (inga känsliga fält i API)
- Flexibilitet (olika format för API vs databas)
- Mindre kod (ingen manuell mappning)
- Lättare underhåll (en plats att ändra mappning)

**Hur?**

1. Installera NuGet: `AutoMapper.Extensions.Microsoft.DependencyInjection`
2. Skapa DTOs
3. Skapa Mapping Profile med `CreateMap<Source, Dest>()`
4. Registrera i `Program.cs`: `AddAutoMapper(typeof(Program))`
5. Inject `IMapper` i controller
6. Använd: `_mapper.Map<Destination>(source)`

**Testa:**

```csharp
config.AssertConfigurationIsValid();
```

**Happy Mapping! 🗺️**

---

_© Campus Mölndal 2025 - Test och Kvalitetssäkring CLO25_
