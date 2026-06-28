---

title: 🚀 API:er i C# - En Snäll Guide
author: Marcus Ackre Medina
type: assignment
topic: projekt
difficulty: 1
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/4_test_and_quality_assurance/assignment/articles/api_basics_csharp.md"
description: "För er som aldrig gjort ett API förut"
tags: ["api", "api:er", "csharp", "projekt", "snäll", "visual-studio"]
week_fit: []
---

# 🚀 API:er i C# - En Snäll Guide

🟢


**För er som aldrig gjort ett API förut**

---

## 🤔 Vad är ett API?

**API = Application Programming Interface**

Tänk på ett API som en restaurangmeny:

- **Menyn** visar vad du kan beställa (endpoints)
- **Kocken** lagar maten (backend)
- **Servitören** tar emot beställning och levererar mat (API)

```
Du (Frontend) → Beställer hamburgare (GET /api/burgers/1)
                     ↓
            API tar emot request
                     ↓
            Backend hämtar från databas
                     ↓
            API skickar tillbaka burger-data (JSON)
                     ↓
Du får din hamburgare (i JSON-format)
```

**Enkelt förklarat:** Ett API låter olika program prata med varandra över internet.

### 💡 API:er fungerar som metoder i kod!

**Kommer ni ihåg när vi anropar metoder i C#?**

```csharp
// I vanlig C# kod:
Console.WriteLine("Hello");
//  ↑        ↑
// Klass   Metod
```

**Med API:er funkar det EXAKT likadant!**

```
http://localhost:5000/api/customers/GetAll
                      ↑              ↑
                   "Klass"        "Metod"
```

**Så när vi pratar med ett API:**

- `/api/customers` = "Klassen" (vad vi jobbar med)
- `/GetAll` = "Metoden" (vad vi vill göra)

I detta fall har klassen `api` ett objekt `customers` med en metod `GetAll()` som hämtar alla kunder.
Så det är inte så annorlunda från vanlig C# kod!

```csharp
// I kod:
var result = CustomerService.GetAll();

// Med API:
GET http://localhost:5000/api/customers/GetAll
```

**Det är samma tänk, bara över HTTP istället! 🎉**

**BONUS:** Med API slipper vi bygga frontend! 😂 Swagger ger oss en färdig UI att testa med. Win-win!

---

## 🎯 Varför behöver vi API:er?

### Problem utan API

```
❌ Frontend måste veta hur databasen fungerar
❌ Mobil-app, webb-app, och desktop-app behöver olika kod
❌ Svårt att skydda känslig data
❌ Ingen separation mellan front och back
```

### Lösning med API

```
✅ Frontend behöver bara veta endpoints (/api/customers)
✅ Ett API funkar för ALLA klienter (webb, mobil, IoT)
✅ API kontrollerar vem som får göra vad (säkerhet)
✅ Front och back kan utvecklas separat
```

**Real-world exempel:**

- **Spotify:** Mobil-app, webb, desktop → samma API
- **Swish:** Fungerar i alla bankers appar → samma API
- **Google Maps:** Används av tusentals appar → ett API

---

## 📦 Vad är REST API?

**REST = Representational State Transfer**

Fancy namn för "använd HTTP verb på rätt sätt":

| HTTP Verb  | Vad        | Exempel                                    |
| ---------- | ---------- | ------------------------------------------ |
| **GET**    | Hämta data | `GET /api/customers` - Lista kunder        |
| **POST**   | Skapa ny   | `POST /api/customers` - Ny kund            |
| **PUT**    | Uppdatera  | `PUT /api/customers/5` - Uppdatera kund 5  |
| **DELETE** | Ta bort    | `DELETE /api/customers/5` - Ta bort kund 5 |

**REST principer:**

1. Stateless (varje request är oberoende)
2. Använd rätt HTTP verb
3. Returnera rätt status codes
4. JSON format (vanligast)

---

## 🏗️ Hur bygger man ett API i C#?

### Steg 1: Skapa ASP.NET Core Web API

```bash
dotnet new webapi -n MyFirstApi
cd MyFirstApi
dotnet run
```

**Du får automatiskt:**

- Ett färdigt API-projekt
- Swagger UI (för att testa API:et)
- En exempel-controller (WeatherForecast)

### Steg 2: Skapa din första Controller

**En Controller = En samling endpoints för en resurs**

```csharp
using Microsoft.AspNetCore.Mvc;

namespace MyFirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        // Lista alla todos
        [HttpGet]
        public IActionResult GetAll()
        {
            var todos = new List<string>
            {
                "Handla mjölk",
                "Lära mig API:er",
                "Bli kung på C#"
            };

            return Ok(todos); // 200 OK + data
        }

        // Hämta specifik todo
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID måste vara positivt"); // 400
            }

            var todo = $"Todo nummer {id}";
            return Ok(todo); // 200
        }

        // Skapa ny todo
        [HttpPost]
        public IActionResult Create([FromBody] string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Titel saknas"); // 400
            }

            // Här skulle vi spara i databas
            var newTodo = new { Id = 1, Title = title };

            return CreatedAtAction(
                nameof(GetById),
                new { id = 1 },
                newTodo
            ); // 201 Created
        }

        // Uppdatera todo
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string title)
        {
            // Här skulle vi uppdatera i databas
            return NoContent(); // 204 No Content
        }

        // Ta bort todo
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Här skulle vi ta bort från databas
            return NoContent(); // 204
        }
    }
}
```

**Viktiga delar:**

```csharp
[ApiController] // Säger "detta är en API controller"
[Route("api/[controller]")] // URL blir /api/todos

[HttpGet] // GET request
[HttpPost] // POST request
[FromBody] // Data kommer i request body (JSON)

return Ok(data); // 200 status
return BadRequest("error"); // 400 status
return NotFound(); // 404 status
return CreatedAtAction(...); // 201 status
```

---

## 📡 HTTP Status Codes

**2xx - Success:**

- **200 OK** - Allt gick bra
- **201 Created** - Ny resurs skapad
- **204 No Content** - Success men ingen data tillbaka

**4xx - Client Error:**

- **400 Bad Request** - Fel i requesten
- **401 Unauthorized** - Inte inloggad
- **403 Forbidden** - Inte behörighet
- **404 Not Found** - Finns inte

**5xx - Server Error:**

- **500 Internal Server Error** - Någonting krascha

**Använd rätt status codes!**

```csharp
// ❌ DÅLIGT
[HttpGet("{id}")]
public IActionResult Get(int id)
{
    var customer = _db.Find(id);
    if (customer == null)
    {
        return Ok("Hittades inte"); // FEL! Ok = 200
    }
    return Ok(customer);
}

// ✅ BRA
[HttpGet("{id}")]
public IActionResult Get(int id)
{
    var customer = _db.Find(id);
    if (customer == null)
    {
        return NotFound(); // 404 - Korrekt!
    }
    return Ok(customer); // 200
}
```

---

## 🔧 Swagger / OpenAPI

**Swagger = Swagger UI (gamla namnet)**
**OpenAPI = Specifikationen**

**Vad är det?**
En interaktiv dokumentation där du kan:

- Se alla endpoints
- Testa API:et direkt i browsern
- Se vilken data som förväntas
- Se exempel på responses

### Hur aktiverar man Swagger?

**ASP.NET Core 9+:**

Swagger är redan aktiverat! Bara kör:

```bash
dotnet run
```

Öppna: `https://localhost:5001/swagger`

**Du ser:**

- Lista över alla endpoints
- "Try it out" knappar
- Request/Response format
- Status codes

### Swagger i kod

```csharp
// Program.cs (redan konfigurerat i nya projekt)

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Lägger till Swagger

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Genererar OpenAPI spec
    app.UseSwaggerUI(); // Visar UI
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

### Förbättra Swagger-docs

```csharp
using Microsoft.OpenApi.Models;

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Todo API",
        Version = "v1",
        Description = "Ett enkelt API för att hantera todos",
        Contact = new OpenApiContact
        {
            Name = "Ditt Namn",
            Email = "din@email.com"
        }
    });
});
```

### Dokumentera endpoints

```csharp
/// <summary>
/// Hämtar alla todos
/// </summary>
/// <returns>Lista med todos</returns>
/// <response code="200">Success</response>
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
public IActionResult GetAll()
{
    // ...
}

/// <summary>
/// Skapar en ny todo
/// </summary>
/// <param name="todo">Todo att skapa</param>
/// <returns>Den skapade todon</returns>
/// <response code="201">Todo skapad</response>
/// <response code="400">Felaktig input</response>
[HttpPost]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public IActionResult Create([FromBody] CreateTodoDto todo)
{
    // ...
}
```

**För att få XML-kommentarer i Swagger:**

I `.csproj`:

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

I `Program.cs`:

```csharp
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
```

---

## 🎨 JSON Format

**API:er pratar JSON (JavaScript Object Notation)**

**C# Object:**

```csharp
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

var customer = new Customer
{
    Id = 1,
    Name = "Anna Andersson",
    Email = "anna@test.se"
};
```

**JSON (vad API:et skickar):**

```json
{
  "id": 1,
  "name": "Anna Andersson",
  "email": "anna@test.se"
}
```

**ASP.NET Core gör detta automatiskt!**

```csharp
[HttpGet("{id}")]
public IActionResult Get(int id)
{
    var customer = new Customer { Id = id, Name = "Anna" };
    return Ok(customer); // Blir automatiskt JSON!
}
```

---

## 🧪 Testa ditt API

### 1. Swagger UI

```
https://localhost:5001/swagger
```

Klicka "Try it out" → Fyll i data → "Execute"

### 2. cURL (Terminal)

```bash
# GET
curl https://localhost:5001/api/todos

# POST
curl -X POST https://localhost:5001/api/todos \
  -H "Content-Type: application/json" \
  -d '{"title":"Ny todo"}'

# PUT
curl -X PUT https://localhost:5001/api/todos/1 \
  -H "Content-Type: application/json" \
  -d '{"title":"Uppdaterad"}'

# DELETE
curl -X DELETE https://localhost:5001/api/todos/1
```

### 3. Postman / Thunder Client

**Postman:** Desktop app
**Thunder Client:** VSCode extension

Båda är GUI för att testa API:er.

### 4. Integration Tests (xUnit)

```csharp
public class TodosControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TodosControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/todos");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Create_ValidInput_ReturnsCreated()
    {
        // Arrange
        var todo = new { Title = "Test" };
        var content = new StringContent(
            JsonSerializer.Serialize(todo),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await _client.PostAsync("/api/todos", content);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
```

---

## 🔒 Best Practices

### 1. Routing

```csharp
// ✅ BRA - RESTful
GET    /api/customers        // Lista alla
GET    /api/customers/5      // Hämta specifik
POST   /api/customers        // Skapa ny
PUT    /api/customers/5      // Uppdatera
DELETE /api/customers/5      // Ta bort

// ❌ DÅLIGT - Inte RESTful
GET    /api/getAllCustomers
GET    /api/getCustomerById?id=5
POST   /api/createNewCustomer
POST   /api/updateCustomer
POST   /api/deleteCustomerById
```

### 2. Namngivning

```csharp
// ✅ BRA - Plural, lowercase
/api/customers
/api/orders
/api/products

// ❌ DÅLIGT
/api/Customer
/api/GetOrders
/api/product
```

### 3. Validering

```csharp
[HttpPost]
public IActionResult Create([FromBody] CreateCustomerDto dto)
{
    // Validera input
    if (string.IsNullOrEmpty(dto.Name))
    {
        return BadRequest("Name is required");
    }

    if (!IsValidEmail(dto.Email))
    {
        return BadRequest("Invalid email format");
    }

    // Fortsätt med logik...
}
```

### 4. Error Handling

```csharp
[HttpGet("{id}")]
public IActionResult Get(int id)
{
    try
    {
        var customer = _service.GetById(id);

        if (customer == null)
        {
            return NotFound(); // 404
        }

        return Ok(customer); // 200
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error getting customer {Id}", id);
        return StatusCode(500, "Internal server error"); // 500
    }
}
```

### 5. Async/Await

```csharp
// ✅ BRA - Async för databas-operationer
[HttpGet]
public async Task<IActionResult> GetAll()
{
    var customers = await _db.Customers.ToListAsync();
    return Ok(customers);
}

// ❌ DÅLIGT - Blockerar tråden
[HttpGet]
public IActionResult GetAll()
{
    var customers = _db.Customers.ToList(); // Blocking!
    return Ok(customers);
}
```

---

## 📚 Komplett Exempel

**Customer.cs:**

```csharp
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

**ICustomerService.cs:**

```csharp
public interface ICustomerService
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer> CreateAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(int id);
}
```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

**CustomersController.cs:**

```csharp
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(
        ICustomerService service,
        ILogger<CustomersController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Hämtar alla kunder
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _service.GetAllAsync();
        return Ok(customers);
    }

    /// <summary>
    /// Hämtar specifik kund
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _service.GetByIdAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    /// <summary>
    /// Skapar ny kund
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var created = await _service.CreateAsync(customer);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created
        );
    }

    /// <summary>
    /// Uppdaterar kund
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] Customer customer)
    {
        if (id != customer.Id)
        {
            return BadRequest("ID mismatch");
        }

        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(customer);
        return NoContent();
    }

    /// <summary>
    /// Tar bort kund
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        await _service.DeleteAsync(id);
        return NoContent();
    }
}
```

---

## 🎯 Sammanfattning

**För att skapa ett API i C#:**

1. **Skapa projekt:** `dotnet new webapi`
2. **Skapa Controller** med `[ApiController]`
3. **Använd rätt HTTP verb** (GET, POST, PUT, DELETE)
4. **Returnera rätt status codes** (200, 201, 404, 400, 500)
5. **Testa med Swagger** (`/swagger`)
6. **Använd async/await** för databas
7. **Validera input**
8. **Hantera errors**

**Swagger/OpenAPI:**

- Redan aktiverat i nya projekt
- Öppna `/swagger` för att se docs
- Använd XML-kommentarer för bättre docs
- Test API direkt i browsern

**Nästa steg:**

- Lägg till Entity Framework för databas
- Använd DTOs (Data Transfer Objects) - se AutoMapper-artikel
- Lägg till authentication/authorization
- Lägg till logging
- Lägg till caching

**Lycka till med ditt API! 🚀**

---

_© Campus Mölndal 2025 - Test och Kvalitetssäkring CLO25_
