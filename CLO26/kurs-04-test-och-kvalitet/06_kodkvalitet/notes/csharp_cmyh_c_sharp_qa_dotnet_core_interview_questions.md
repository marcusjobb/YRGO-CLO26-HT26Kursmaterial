---

title: .NET Core Intervjufrågor - Från Lätt till Avancerat
author: Marcus Ackre Medina
type: article
topic: testing
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/csharp_cmyh/C-Sharp/qa/dotnet-core-interview-questions.md"
description: "25 viktiga frågor för att förbereda dig inför .NET Core-intervjuer, organiserade efter svårighetsgrad."
tags: [".net", "asp.net", "avancerat", "core", "csharp", "design-patterns", "dotnet", "entity-framework", "från", "git"]
week_fit: []
---

# .NET Core Intervjufrågor - Från Lätt till Avancerat

🔴


25 viktiga frågor för att förbereda dig inför .NET Core-intervjuer, organiserade efter svårighetsgrad.

## TL;DR

En omfattande samling intervjufrågor som täcker grundläggande koncept, arkitektur och avancerade scenarion i .NET Core. Perfekt för att förbereda dig inför tekniska intervjuer eller fördjupa din .NET Core-kunskap.

## När du läst detta ska du kunna

- Förstå fundamentala .NET Core-koncept och komponenter
- Förklara middleware, dependency injection och request pipeline
- Diskutera arkitekturmönster och designbeslut
- Hantera avancerade scenarion som microservices och centraliserad loggning

---

## 🟢 Lätta Frågor

### 1. Vad är .NET Core och hur skiljer det sig från .NET Framework?

**Svar:**

.NET Core är en modern, open-source, plattformsoberoende implementation av .NET. Skillnaderna:

- **Plattformsoberoende**: .NET Core körs på Windows, Linux och macOS, medan .NET Framework bara körs på Windows
- **Open-source**: Hela .NET Core-stacken är öppen källkod
- **Modularitet**: NuGet-paket istället för monolitisk installation
- **Prestanda**: Betydligt snabbare än .NET Framework
- **Deployment**: Stöd för side-by-side versioner
- **Cloud-optimerad**: Designad för moderna cloud-applikationer

```csharp
// .NET Core projektreferens (minimal)
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```

---

### 2. Vilken roll har Startup.cs-filen i en ASP.NET Core-applikation?

**Svar:**

Startup.cs är applikationens konfigurationscentrum (sedan .NET 6 har denna funktionalitet flyttats till Program.cs):

**I äldre .NET Core-versioner:**

```csharp
public class Startup
{
    // ConfigureServices - registrera tjänster i DI-containern
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<AppDbContext>();
        services.AddScoped<IUserService, UserService>();
    }

    // Configure - bygg request pipeline med middleware
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
```

**I moderna .NET 6+:**

```csharp
// Program.cs kombinerar båda funktionerna
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // ConfigureServices-delen

var app = builder.Build();
app.UseAuthorization(); // Configure-delen
app.MapControllers();
app.Run();
```

---

### 3. Vad är middleware-komponenter och hur fungerar de i ASP.NET Core request pipeline?

**Svar:**

Middleware är komponenter som hanterar HTTP-förfrågningar och svar i en pipeline. Varje middleware:

1. Kan processa inkommande request
2. Bestämmer om den ska anropa nästa middleware
3. Kan processa utgående response

**Visualisering:**

```
Request → Middleware 1 → Middleware 2 → Middleware 3 → Endpoint
          ↓                ↓                ↓             ↓
Response ← Middleware 1 ← Middleware 2 ← Middleware 3 ← Response
```

**Exempel:**

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Custom middleware
app.Use(async (context, next) =>
{
    Console.WriteLine($"Before: {context.Request.Path}");
    await next(); // Anropa nästa middleware
    Console.WriteLine($"After: {context.Response.StatusCode}");
});

// Terminal middleware (avslutar pipeline)
app.Run(async context =>
{
    await context.Response.WriteAsync("Hej från endpoint!");
});

app.Run();
```

**Viktig ordning:**

```csharp
app.UseExceptionHandler();  // Först - fånga alla fel
app.UseHttpsRedirection();  // Redirect till HTTPS
app.UseStaticFiles();       // Servera statiska filer
app.UseRouting();           // Aktivera routing
app.UseAuthentication();    // Identifiera användaren
app.UseAuthorization();     // Kontrollera rättigheter
app.MapControllers();       // Endpoints
```

---

### 4. Hur konfigurerar du dependency injection i .NET Core?

**Svar:**

Dependency Injection är inbyggt i .NET Core via `IServiceCollection`:

```csharp
// Registrera tjänster
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ICacheService, RedisCacheService>();
builder.Services.AddTransient<IEmailService, SmtpEmailService>();

// Använd i controller
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepo;

    // Injiceras automatiskt
    public UserController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        return Ok(user);
    }
}
```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

**Livscyklar:**
- **Transient**: Ny instans varje gång
- **Scoped**: En instans per HTTP-request
- **Singleton**: En instans för hela applikationens livstid

---

### 5. Vad är skillnaden mellan IConfiguration och IOptions i .NET Core?

**Svar:**

**IConfiguration**: Direkt åtkomst till konfiguration

```csharp
public class MyService
{
    private readonly IConfiguration _config;

    public MyService(IConfiguration config)
    {
        _config = config;
    }

    public void DoWork()
    {
        string connString = _config["ConnectionStrings:Default"];
        int timeout = _config.GetValue<int>("Timeout");
    }
}
```

**IOptions**: Strongly-typed konfiguration (rekommenderat)

```csharp
// appsettings.json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "EnableSsl": true
  }
}

// POCO-klass
public class EmailSettings
{
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
}

// Registrera
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

// Använd
public class EmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public void SendEmail()
    {
        // Använd _settings.SmtpServer, _settings.Port osv.
    }
}
```

**Fördelar med IOptions:**
- Typkontroll vid kompilering
- Validering av konfiguration
- Hot-reload med `IOptionsSnapshot`
- Separation of concerns

---

### 6. Hur fungerar appsettings.json-filen i .NET Core?

**Svar:**

appsettings.json är primär konfigurationskälla:

**Struktur:**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MyDb;User Id=sa;Password=Pass123;"
  },
  "JwtSettings": {
    "SecretKey": "SuperSecretKey123",
    "Issuer": "MyApp",
    "ExpiryMinutes": 60
  }
}
```

**Miljöspecifik konfiguration:**

```
appsettings.json                  # Bas-konfiguration
appsettings.Development.json      # Development-overrides
appsettings.Production.json       # Production-overrides
```

**Läsa konfiguration:**

```csharp
// Direkt åtkomst
string connString = builder.Configuration.GetConnectionString("DefaultConnection");

// Nested värden
string logLevel = builder.Configuration["Logging:LogLevel:Default"];

// Strongly-typed
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));
```

**Prioritetsordning (högre överskrider lägre):**
1. Command-line arguments
2. Environment variables
3. appsettings.{Environment}.json
4. appsettings.json
5. User secrets (Development)

---

### 7. Vad är betydelsen av Program.cs-filen i .NET Core-applikationer?

**Svar:**

Program.cs är applikationens startpunkt (entry point):

**Modern stil (.NET 6+):**

```csharp
// Top-level statements - ingen Main()-metod synlig
var builder = WebApplication.CreateBuilder(args);

// Konfigurera tjänster
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Bygg applikationen
var app = builder.Build();

// Konfigurera middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Starta applikationen
app.Run();
```

**Äldre stil (före .NET 6):**

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
```

**Viktiga ansvar:**
- Skapa och konfigurera host
- Registrera tjänster
- Bygga middleware pipeline
- Starta webbservern

---

## 🟡 Medelsvåra Frågor

### 8. Vad är Kestrel och varför används det i .NET Core?

**Svar:**

Kestrel är den inbyggda, cross-platform webbservern för ASP.NET Core, skriven i C#.

**Egenskaper:**
- **Plattformsoberoende**: Funkar på Windows, Linux, macOS
- **Högpresterande**: Asynkron I/O, minimal overhead
- **Lätt**: Kan användas standalone eller bakom reverse proxy
- **HTTP/2 och HTTP/3**: Modernt protokollstöd

**Deployment-scenarion:**

```csharp
// Standalone (utveckling/testning)
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options =>
{
    options.ListenLocalhost(5000); // HTTP
    options.ListenLocalhost(5001, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS
    });
});
```

**Produktion med reverse proxy:**

```
Internet → Nginx/IIS → Kestrel → ASP.NET Core App
```

**Varför reverse proxy?**
- SSL-terminering
- Load balancing
- Static file caching
- Security features

```csharp
// Konfigurera för bakom proxy
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
```

---

### 9. Förklara routing i ASP.NET Core

**Svar:**

Routing mappar inkommande requests till endpoints (controllers, Razor Pages, etc.).

**Conventional routing (MVC):**

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Matchar: /Products/Details/5
// Controller: ProductsController
// Action: Details
// Parameter: id = 5
```

**Attribute routing (Web API):**

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // GET: api/products
    [HttpGet]
    public IActionResult GetAll() { }

    // GET: api/products/5
    [HttpGet("{id}")]
    public IActionResult GetById(int id) { }

    // GET: api/products/search?query=laptop
    [HttpGet("search")]
    public IActionResult Search([FromQuery] string query) { }

    // POST: api/products
    [HttpPost]
    public IActionResult Create([FromBody] Product product) { }

    // PUT: api/products/5
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Product product) { }

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id) { }
}
```

**Minimal API routing (.NET 6+):**

```csharp
app.MapGet("/", () => "Hello World");
app.MapGet("/users/{id}", (int id) => $"User {id}");
app.MapPost("/users", (User user) => Results.Created($"/users/{user.Id}", user));
```

**Route constraints:**

```csharp
[HttpGet("products/{id:int}")] // Endast integers
[HttpGet("archive/{date:datetime}")] // DateTime-format
[HttpGet("files/{filename}.{ext}")] // Filnamn.extension
```

---

### 10. Vad är syftet med ConfigureServices och Configure i Startup-klassen?

**Svar:**

**ConfigureServices**: Registrera tjänster i DI-containern

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Framework-tjänster
    services.AddControllers();
    services.AddDbContext<AppDbContext>();

    // Custom tjänster
    services.AddScoped<IUserService, UserService>();
    services.AddSingleton<ICacheService, CacheService>();

    // Konfiguration
    services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

    // Autentisering
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => { /* ... */ });
}
```

**Configure**: Bygg HTTP request pipeline

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Middleware-ordning är KRITISK!

    // Exception handling först
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/error");
        app.UseHsts();
    }

    // Request processing
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    // Auth middleware (ordning viktig!)
    app.UseAuthentication();  // Vem är du?
    app.UseAuthorization();   // Vad får du göra?

    // Endpoints
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

**I .NET 6+**: Båda kombineras i Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);
// ConfigureServices-delen
builder.Services.AddControllers();

var app = builder.Build();
// Configure-delen
app.UseAuthorization();
app.MapControllers();
app.Run();
```

---

### 11. Hur hanterar ASP.NET Core CORS?

**Svar:**

CORS (Cross-Origin Resource Sharing) tillåter API:er att acceptera requests från andra domains.

**Konfigurera CORS:**

```csharp
var builder = WebApplication.CreateBuilder(args);

// 1. Lägg till CORS-tjänst
builder.Services.AddCors(options =>
{
    // Tillåt specifik origin
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://myapp.com", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });

    // Tillåt alla (endast utveckling!)
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// 2. Använd CORS middleware (före UseAuthorization!)
app.UseCors("AllowFrontend");

app.MapControllers();
app.Run();
```

**Per-controller/action CORS:**

```csharp
[EnableCors("AllowFrontend")]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() { }

    [DisableCors] // Inaktivera för specifik action
    [HttpGet("internal")]
    public IActionResult GetInternal() { }
}
```

**Preflight requests:**

CORS använder OPTIONS-requests för att kolla om cross-origin är tillåtet:

```
Browser → OPTIONS /api/products → Server
       ← Allow headers/methods ← Server
       → GET /api/products → Server (actual request)
```

---

### 12. Vilka fördelar har Entity Framework Core i .NET Core?

**Svar:**

**Fördelar:**

1. **Code-First approach**: Definiera modeller i C#, generera databas

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Category Category { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
```

2. **LINQ queries**: Type-safe databasoperationer

```csharp
// Istället för SQL:
// SELECT * FROM Products WHERE Price > 100 ORDER BY Name

var products = await _context.Products
    .Where(p => p.Price > 100)
    .OrderBy(p => p.Name)
    .ToListAsync();
```

3. **Migrations**: Versionshanterad databasstruktur

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. **Change tracking**: Automatisk upptäckt av ändringar

```csharp
var product = await _context.Products.FindAsync(1);
product.Price = 199.99m; // EF Core trackar ändring
await _context.SaveChangesAsync(); // UPDATE genereras automatiskt
```

5. **Relationships**: Enkelt hantera relationer

```csharp
// Lazy loading
var product = await _context.Products.FindAsync(1);
var category = product.Category; // Laddar automatiskt

// Eager loading
var products = await _context.Products
    .Include(p => p.Category)
    .ToListAsync();
```

6. **Cross-platform**: Stöd för SQL Server, PostgreSQL, MySQL, SQLite, Cosmos DB

---

### 13. Förklara IHostedService och ge exempel

**Svar:**

IHostedService låter dig köra bakgrundsprocesser i din ASP.NET Core-app.

**Interface:**

```csharp
public interface IHostedService
{
    Task StartAsync(CancellationToken cancellationToken);
    Task StopAsync(CancellationToken cancellationToken);
}
```

**Exempel - Email Queue Processor:**

```csharp
public class EmailQueueService : IHostedService
{
    private readonly ILogger<EmailQueueService> _logger;
    private Timer? _timer;

    public EmailQueueService(ILogger<EmailQueueService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email Queue Service startar...");

        // Kör varje minut
        _timer = new Timer(ProcessQueue, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private void ProcessQueue(object? state)
    {
        _logger.LogInformation("Processar email-kö...");
        // Hämta och skicka emails från kö
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email Queue Service stoppar...");
        _timer?.Change(Timeout.Infinite, 0);
        _timer?.Dispose();
        return Task.CompletedTask;
    }
}

// Registrera
builder.Services.AddHostedService<EmailQueueService>();
```

**BackgroundService (enklare):**

```csharp
public class DataSyncService : BackgroundService
{
    private readonly ILogger<DataSyncService> _logger;

    public DataSyncService(ILogger<DataSyncService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Synkar data...");

            // Synka data
            await SyncDataAsync();

            // Vänta 5 minuter
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    private async Task SyncDataAsync()
    {
        // Implementation
    }
}
```

---

### 14. Hur stödjer ASP.NET Core asynkron programmering?

**Svar:**

ASP.NET Core är byggt för async från grunden med async/await.

**Async controllers:**

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    // Async action returnerar Task<IActionResult>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var products = await _service.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetByIdAsync(int id)
    {
        var product = await _service.GetProductAsync(id);
        if (product == null)
            return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Product product)
    {
        await _service.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = product.Id }, product);
    }
}
```

**Async database operations:**

```csharp
public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task CreateProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }
}
```

**Async HTTP calls:**

```csharp
public class ExternalApiService
{
    private readonly HttpClient _httpClient;

    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        var response = await _httpClient.GetAsync($"api/weather/{city}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<WeatherData>();
    }
}
```

**Fördelar:**
- Frigör trådar under I/O-operationer
- Bättre skalbarhet
- Högre throughput
- Lägre resource usage

---

## 🔴 Avancerade Frågor

### 15. Hur fungerar ASP.NET Core request processing pipeline internt?

**Svar:**

Request pipeline är en kedja av middleware-komponenter som processas sekventiellt.

**Intern flow:**

```
1. Kestrel tar emot HTTP request
   ↓
2. Request konverteras till HttpContext
   ↓
3. Middleware 1 (Exception Handler)
   - Processa request
   - Anropa next()
   ↓
4. Middleware 2 (HTTPS Redirection)
   - Kontrollera HTTPS
   - Anropa next() eller redirect
   ↓
5. Middleware 3 (Routing)
   - Matcha route
   - Sätt endpoint i context
   - Anropa next()
   ↓
6. Middleware 4 (Authentication)
   - Verifiera identity
   - Sätt User i context
   - Anropa next()
   ↓
7. Middleware 5 (Authorization)
   - Kontrollera policies
   - Anropa next() eller 403
   ↓
8. Endpoint Middleware
   - Exekvera controller action
   - Generera response
   ↓
Response bubblar tillbaka genom middleware-kedjan
   ↓
Kestrel skickar HTTP response
```

**Middleware-implementation:**

```csharp
public class CustomMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomMiddleware> _logger;

    public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Before: Process request
        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
        var stopwatch = Stopwatch.StartNew();

        // Call next middleware
        await _next(context);

        // After: Process response
        stopwatch.Stop();
        _logger.LogInformation($"Response: {context.Response.StatusCode} ({stopwatch.ElapsedMilliseconds}ms)");
    }
}

// Extension method
public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware>();
    }
}
```

**Short-circuiting:**

Middleware kan stoppa pipeline:

```csharp
app.Use(async (context, next) =>
{
    if (!context.Request.Headers.ContainsKey("API-Key"))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("API Key saknas");
        return; // Stoppa här, anropa inte next()
    }

    await next();
});
```

---

### 16. Hur designar och implementerar du microservices med ASP.NET Core?

**Svar:**

**Arkitekturprinciper:**

```
                    API Gateway (Ocelot/YARP)
                           |
         +-----------------+-----------------+
         |                 |                 |
   User Service    Product Service    Order Service
   (Port 5001)     (Port 5002)       (Port 5003)
         |                 |                 |
    SQL Server        MongoDB          PostgreSQL
```

**Service implementation:**

```csharp
// Product Service (Program.cs)
var builder = WebApplication.CreateBuilder(args);

// Service registration
builder.Services.AddControllers();
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDb")));

// Health checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ProductDbContext>();

// Service discovery (Consul)
builder.Services.AddConsulServiceDiscovery(options =>
{
    options.ServiceName = "product-service";
    options.ServicePort = 5002;
});

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
```

**API Gateway (Ocelot):**

```json
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/products/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        { "Host": "localhost", "Port": 5002 }
      ],
      "UpstreamPathTemplate": "/products/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    },
    {
      "DownstreamPathTemplate": "/api/orders/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        { "Host": "localhost", "Port": 5003 }
      ],
      "UpstreamPathTemplate": "/orders/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    }
  ]
}
```

**Service-to-service communication:**

```csharp
// Order Service kallar Product Service
public class OrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ProductService");
    }

    public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
    {
        // Hämta produktinfo från Product Service
        var product = await _httpClient.GetFromJsonAsync<Product>(
            $"api/products/{dto.ProductId}");

        if (product == null)
            throw new Exception("Produkt ej funnen");

        // Skapa order
        var order = new Order
        {
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            TotalPrice = product.Price * dto.Quantity
        };

        return order;
    }
}

// Registrera HttpClient
builder.Services.AddHttpClient("ProductService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5002");
});
```

**Event-driven communication (RabbitMQ/MassTransit):**

```csharp
// Publisher (Order Service)
public class OrderCreatedEvent
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public async Task CreateOrderAsync(Order order)
    {
        // Spara order
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        // Publicera event
        await _publishEndpoint.Publish(new OrderCreatedEvent
        {
            OrderId = order.Id,
            ProductId = order.ProductId,
            Quantity = order.Quantity
        });
    }
}

// Consumer (Inventory Service)
public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IInventoryService _inventoryService;

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        // Uppdatera lagersaldo
        await _inventoryService.DecreaseStockAsync(
            context.Message.ProductId,
            context.Message.Quantity);
    }
}
```

---

### 17. Vad är skillnaden mellan IApplicationBuilder och IServiceCollection?

**Svar:**

**IServiceCollection**: Registrera tjänster (DI container)

```csharp
// Används i ConfigureServices/Program.cs före Build()
public void ConfigureServices(IServiceCollection services)
{
    // Framework services
    services.AddControllers();
    services.AddDbContext<AppDbContext>();
    services.AddAuthentication();

    // Custom services
    services.AddScoped<IUserService, UserService>();
    services.AddSingleton<ICacheService, CacheService>();

    // Configuration
    services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

    // Third-party
    services.AddAutoMapper(typeof(Startup));
}

// Modern .NET 6+
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // IServiceCollection
```

**IApplicationBuilder**: Bygg middleware pipeline

```csharp
// Används i Configure/Program.cs efter Build()
public void Configure(IApplicationBuilder app)
{
    // Middleware components (ordning viktig!)
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints => endpoints.MapControllers());
}

// Modern .NET 6+
var app = builder.Build(); // WebApplication implements IApplicationBuilder
app.UseAuthorization(); // IApplicationBuilder methods
```

**Sammanfattning:**

| Aspekt | IServiceCollection | IApplicationBuilder |
|--------|-------------------|---------------------|
| **När** | Före Build() | Efter Build() |
| **Syfte** | Registrera tjänster | Konfigurera pipeline |
| **Exempel** | AddScoped, AddDbContext | UseRouting, UseAuthorization |
| **Livscykel** | DI container setup | Request processing setup |

---

### 18. Hur implementerar du centraliserad loggning i .NET Core?

**Svar:**

**Strukturerad loggning med Serilog:**

```csharp
// Installation: Serilog.AspNetCore, Serilog.Sinks.Seq, Serilog.Sinks.File

// Program.cs
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Konfigurera Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentName()
    .WriteTo.Console()
    .WriteTo.File("logs/app-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5341") // Centraliserad loggserver
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    Log.Information("Applikationen startar...");

    var app = builder.Build();

    // Request logging
    app.UseSerilogRequestLogging(options =>
    {
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"]);
            diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress);
        };
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Applikationen kraschade vid start");
}
finally
{
    Log.CloseAndFlush();
}
```

**appsettings.json:**

```json
{
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.File", "Serilog.Sinks.Seq" ],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.AspNetCore": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      { "Name": "Console" },
      {
        "Name": "File",
        "Args": {
          "path": "logs/app-.txt",
          "rollingInterval": "Day",
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      },
      {
        "Name": "Seq",
        "Args": {
          "serverUrl": "http://seq-server:5341",
          "apiKey": "your-api-key"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ]
  }
}
```

**Strukturerad loggning i kod:**

```csharp
public class ProductService
{
    private readonly ILogger<ProductService> _logger;

    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger;
    }

    public async Task<Product> GetProductAsync(int productId)
    {
        _logger.LogInformation("Hämtar produkt {ProductId}", productId);

        try
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                _logger.LogWarning("Produkt {ProductId} hittades inte", productId);
                return null;
            }

            _logger.LogInformation("Produkt {ProductId} hämtad: {ProductName}",
                productId, product.Name);

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fel vid hämtning av produkt {ProductId}", productId);
            throw;
        }
    }
}
```

**Monitoring dashboard (Seq/ELK/Application Insights):**

```csharp
// Application Insights
builder.Services.AddApplicationInsightsTelemetry();

// Custom metrics
public class OrderService
{
    private readonly TelemetryClient _telemetry;

    public async Task CreateOrderAsync(Order order)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Track success
            _telemetry.TrackEvent("OrderCreated", new Dictionary<string, string>
            {
                { "OrderId", order.Id.ToString() },
                { "CustomerId", order.CustomerId.ToString() }
            });
        }
        finally
        {
            stopwatch.Stop();
            _telemetry.TrackMetric("OrderCreationTime", stopwatch.ElapsedMilliseconds);
        }
    }
}
```

---

### 19. Förklara transient, scoped och singleton i dependency injection

**Svar:**

**Livscyklar:**

```csharp
// Transient - Ny instans varje gång
builder.Services.AddTransient<IEmailService, EmailService>();

// Scoped - En instans per HTTP request
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Singleton - En instans för hela applikationen
builder.Services.AddSingleton<ICacheService, CacheService>();
```

**Visualisering:**

```
Request 1:                          Request 2:
├─ Transient A (instans 1)         ├─ Transient A (instans 3)
├─ Transient A (instans 2)         ├─ Transient A (instans 4)
├─ Scoped B (instans 1)            ├─ Scoped B (instans 2)
├─ Scoped B (instans 1) [samma]    ├─ Scoped B (instans 2) [samma]
├─ Singleton C (instans 1)         ├─ Singleton C (instans 1) [samma]
└─ Singleton C (instans 1) [samma] └─ Singleton C (instans 1) [samma]
```

**Transient - Använd för:**
- Lightweight, stateless services
- Services utan shared state

```csharp
public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Ingen state, säkert att skapa ny varje gång
    }
}

builder.Services.AddTransient<IEmailService, EmailService>();
```

**Scoped - Använd för:**
- DbContext (en per request)
- Services med request-specific state
- Unit of Work pattern

```csharp
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context; // Scoped

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
}

builder.Services.AddDbContext<AppDbContext>(); // Scoped by default
builder.Services.AddScoped<IUserRepository, UserRepository>();
```

**Singleton - Använd för:**
- Caching services
- Configuration
- Thread-safe shared resources

```csharp
public class CacheService : ICacheService
{
    private readonly ConcurrentDictionary<string, object> _cache = new();

    public void Set(string key, object value)
    {
        _cache[key] = value;
    }

    public object? Get(string key)
    {
        _cache.TryGetValue(key, out var value);
        return value;
    }
}

builder.Services.AddSingleton<ICacheService, CacheService>();
```

**VARNING - Captive Dependencies:**

```csharp
// FARLIGT! Singleton innehåller Scoped dependency
public class SingletonService
{
    private readonly AppDbContext _context; // BAD! DbContext är Scoped

    public SingletonService(AppDbContext context)
    {
        _context = context; // Kommer leva längre än intended scope
    }
}

// RÄTT - Använd IServiceProvider för att resolve scoped services
public class SingletonService
{
    private readonly IServiceProvider _serviceProvider;

    public SingletonService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DoWorkAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Använd context...
    }
}
```

---

### 20. Hur implementerar du health checks i ASP.NET Core?

**Svar:**

Health checks övervakar applikationens hälsa (databas, externa API:er, disk space, etc.).

**Basic health checks:**

```csharp
var builder = WebApplication.CreateBuilder(args);

// Lägg till health checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>("database")
    .AddUrlGroup(new Uri("https://api.example.com/health"), "external-api")
    .AddDiskStorageHealthCheck(s => s.AddDrive("C:\\", 1024), "disk-space") // Min 1GB free
    .AddCheck<CustomHealthCheck>("custom-check");

var app = builder.Build();

// Map health check endpoints
app.MapHealthChecks("/health"); // Simple endpoint
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false // Bara kontrollera att appen svarar
});

app.Run();
```

**Custom health check:**

```csharp
public class CustomHealthCheck : IHealthCheck
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CustomHealthCheck(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(
                "https://external-service.com/health",
                cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy("External service is healthy");
            }

            return HealthCheckResult.Degraded(
                $"External service returned {response.StatusCode}");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                "External service is unavailable",
                ex);
        }
    }
}
```

**JSON response format:**

```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.1234567",
  "entries": {
    "database": {
      "status": "Healthy",
      "duration": "00:00:00.0567890",
      "description": "Database connection is healthy"
    },
    "external-api": {
      "status": "Healthy",
      "duration": "00:00:00.0456789"
    },
    "disk-space": {
      "status": "Healthy",
      "data": {
        "FreeSpace": "50 GB"
      }
    }
  }
}
```

**Health Checks UI (dashboard):**

```csharp
// Installation: AspNetCore.HealthChecks.UI

builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(30); // Kontrollera var 30:e sekund
    options.AddHealthCheckEndpoint("API", "/health");
}).AddInMemoryStorage();

var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(options => options.UIPath = "/health-ui");
```

**Kubernetes integration:**

```yaml
# deployment.yaml
apiVersion: apps/v1
kind: Deployment
spec:
  template:
    spec:
      containers:
      - name: api
        image: myapi:latest
        livenessProbe:
          httpGet:
            path: /health/live
            port: 80
          initialDelaySeconds: 10
          periodSeconds: 30
        readinessProbe:
          httpGet:
            path: /health/ready
            port: 80
          initialDelaySeconds: 5
          periodSeconds: 10
```

---

### 21-25. Snabbsvar på Resterande Frågor

**21. BackgroundService vs IHostedService**

BackgroundService är en förenklad base class för IHostedService:

```csharp
// BackgroundService
public class MyBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Gör arbete
            await Task.Delay(5000, stoppingToken);
        }
    }
}

// IHostedService (mer kontroll)
public class MyHostedService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Start logic
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Cleanup logic
    }
}
```

---

**22. Generic Host vs Web Host**

**Generic Host** (.NET Core 3.0+): Generell container för alla appar

```csharp
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<MyBackgroundService>();
    })
    .Build();

await host.RunAsync();
```

**Web Host** (äldre): Specifik för webappar

```csharp
var host = WebHost.CreateDefaultBuilder(args)
    .UseStartup<Startup>()
    .Build();
```

Generic Host är modernare och rekommenderas.

---

**23. Global Exception Handling**

```csharp
// Middleware approach
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = new { error = ex.Message, statusCode = 500 };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        return context.Response.WriteAsJsonAsync(response);
    }
}

// Registrera
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

---

**24. DataProtection**

Används för att kryptera känslig data (cookies, tokens):

```csharp
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\keys"))
    .SetApplicationName("MyApp");

public class SecureService
{
    private readonly IDataProtector _protector;

    public SecureService(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("MyPurpose");
    }

    public string Encrypt(string plaintext)
    {
        return _protector.Protect(plaintext);
    }

    public string Decrypt(string ciphertext)
    {
        return _protector.Unprotect(ciphertext);
    }
}
```

---

**25. Multiple Environments**

```csharp
// Environment detection
var env = app.Environment;

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else if (env.IsStaging())
{
    app.UseExceptionHandler("/error-staging");
}
else if (env.IsProduction())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

// Environment-specific config
appsettings.json
appsettings.Development.json
appsettings.Staging.json
appsettings.Production.json

// Sätt environment via:
// - ASPNETCORE_ENVIRONMENT environment variable
// - launchSettings.json
// - Command line: --environment Production
```

---

## Obligatorisk Dad-joke

Varför är .NET Core-utvecklare alltid så optimistiska?

För att de alltid använder `async/await` - de vet att allt kommer lösa sig... eventually!

(Och om det inte gör det, ja då har de väl `try/catch` som backup!) 🥁

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
