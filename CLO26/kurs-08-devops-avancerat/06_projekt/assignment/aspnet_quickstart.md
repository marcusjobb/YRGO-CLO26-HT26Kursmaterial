---

title: 🚀 ASP.NET Core API Quickstart - Med Swagger
author: Marcus Ackre Medina
type: assignment
topic: projekt
difficulty: 1
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/4_test_and_quality_assurance/assignment/cheatsheets/aspnet_quickstart.md"
description: "För TDD Grupprojekt - REST API Development"
tags: ["asp.net", "aspnet", "core", "csharp", "installation", "projekt", "quickstart", "swagger", "verktyg", "visual-studio"]
week_fit: []
---

# 🚀 ASP.NET Core API Quickstart - Med Swagger

🟢


**För TDD Grupprojekt - REST API Development**

> **Detta är perfekt för er REST API!** Ni får Swagger (API-dokumentation) automatiskt + allt ni behöver för att komma igång snabbt! 🎯

---

## ⚡ Snabbstart (15 minuter)

### Steg 1: Skapa Solution & Projekt (5 min)

```bash
# Skapa solution
dotnet new sln -n MyProject

# Skapa ASP.NET Core API med Swagger
dotnet new webapi -n MyProject.Api

# Skapa Core-bibliotek för business logic
dotnet new classlib -n MyProject.Core

# Skapa test-projekt
dotnet new xunit -n MyProject.Tests

# Lägg till alla i solution
dotnet sln add MyProject.Api
dotnet sln add MyProject.Core
dotnet sln add MyProject.Tests
```

**Vad får vi?**
- ✅ ASP.NET Core API projekt med Swagger REDAN installerat!
- ✅ Program.cs med minimal API setup
- ✅ Swagger UI på `/swagger`
- ✅ WeatherForecast exempel-controller (kan tas bort)

---

### Steg 2: Lägg till Project References (2 min)

```bash
# API behöver Core
cd MyProject.Api
dotnet add reference ../MyProject.Core

# Tests behöver både API och Core
cd ../MyProject.Tests
dotnet add reference ../MyProject.Api
dotnet add reference ../MyProject.Core

cd ..
```

---

### Steg 3: Installera NuGet Packages (3 min)

```bash
# I MyProject.Core - för databas
cd MyProject.Core
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite

# I MyProject.Api - för EF migrations
cd ../MyProject.Api
dotnet add package Microsoft.EntityFrameworkCore.Design

# I MyProject.Tests - för mocking och assertions
cd ../MyProject.Tests
dotnet add package Moq
dotnet add package FluentAssertions

cd ..
```

---

### Steg 4: Konfigurera Swagger (REDAN KLART! 🎉)

**Program.cs** (kommer redan ha något liknande):

```csharp
var builder = WebApplication.CreateBuilder(args);

// Lägg till services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // 👈 Swagger redan här!

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       // 👈 Swagger endpoints
    app.UseSwaggerUI();     // 👈 Swagger UI
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

**Det är ALLT!** Swagger funkar direkt! 🚀

---

### Steg 5: Testa att Det Fungerar (2 min)

```bash
# Kör projektet
cd MyProject.Api
dotnet run

# Öppna i webbläsare:
# https://localhost:7XXX/swagger
```

**Du borde se:**
- 🟢 Swagger UI med snygg dokumentation
- 📋 WeatherForecast endpoint (exempel)
- 🎨 Interaktiv API-testning

---

## 📝 Skapa Din Första Controller

### Exempel: TeamsController

**MyProject.Api/Controllers/TeamsController.cs:**

```csharp
using Microsoft.AspNetCore.Mvc;
using MyProject.Core.Entities;

namespace MyProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    // GET: api/teams
    [HttpGet]
    public ActionResult<List<Team>> GetAll()
    {
        // TODO: Implement with service
        return Ok(new List<Team>());
    }

    // GET: api/teams/5
    [HttpGet("{id}")]
    public ActionResult<Team> GetById(int id)
    {
        // TODO: Implement with service
        return Ok(new Team { Id = id, Name = "Example Team" });
    }

    // POST: api/teams
    [HttpPost]
    public ActionResult<Team> Create([FromBody] Team team)
    {
        // TODO: Implement with service
        return CreatedAtAction(nameof(GetById), new { id = team.Id }, team);
    }

    // PUT: api/teams/5
    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] Team team)
    {
        // TODO: Implement with service
        return NoContent();
    }

    // DELETE: api/teams/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        // TODO: Implement with service
        return NoContent();
    }
}
```

---

## 🔥 Swagger Kommer Automatiskt Visa:

När du kör `dotnet run` och går till `/swagger`:

```
Teams
  GET    /api/teams          Get all teams
  GET    /api/teams/{id}     Get team by ID
  POST   /api/teams          Create new team
  PUT    /api/teams/{id}     Update team
  DELETE /api/teams/{id}     Delete team
```

**Klicka "Try it out"** för att testa direkt! 🎯

---

## 🎨 Förbättra Swagger Dokumentation

### Lägg till beskrivningar:

```csharp
/// <summary>
/// Gets all teams
/// </summary>
/// <returns>List of all teams</returns>
[HttpGet]
public ActionResult<List<Team>> GetAll()
{
    // ...
}

/// <summary>
/// Creates a new team
/// </summary>
/// <param name="team">Team to create</param>
/// <returns>Created team</returns>
[HttpPost]
public ActionResult<Team> Create([FromBody] Team team)
{
    // ...
}
```

### Aktivera XML-kommentarer i Swagger:

**MyProject.Api.csproj:**

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

**Program.cs:**

```csharp
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
```

---

## 🗄️ Lägg till Entity Framework

### Skapa DbContext:

**MyProject.Core/Data/AppDbContext.cs:**

```csharp
using Microsoft.EntityFrameworkCore;
using MyProject.Core.Entities;

namespace MyProject.Core.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
}
```

### Registrera i Program.cs:

```csharp
// Lägg till DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));
```

### Skapa första migration:

```bash
cd MyProject.Api
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## ✅ Projekt-Struktur

```
MyProject/
├── MyProject.Api/
│   ├── Controllers/
│   │   ├── TeamsController.cs
│   │   └── PlayersController.cs
│   ├── Program.cs              👈 Swagger redan konfigurerat!
│   └── appsettings.json
├── MyProject.Core/
│   ├── Entities/
│   │   ├── Team.cs
│   │   └── Player.cs
│   ├── Services/
│   │   ├── TeamService.cs
│   │   └── ITeamService.cs
│   └── Data/
│       └── AppDbContext.cs
├── MyProject.Tests/
│   ├── TeamServiceTests.cs
│   └── TeamsControllerTests.cs
└── MyProject.sln
```

---

## 🧪 TDD med API:er

### 1. Skriv Test Först (RED)

```csharp
[Fact]
public void GetById_ExistingId_ReturnsTeam()
{
    // Arrange
    var mockService = new Mock<ITeamService>();
    mockService.Setup(s => s.GetById(1))
               .Returns(new Team { Id = 1, Name = "Test Team" });

    var controller = new TeamsController(mockService.Object);

    // Act
    var result = controller.GetById(1);

    // Assert
    result.Result.Should().BeOfType<OkObjectResult>();
}
```

### 2. Implementera (GREEN)

```csharp
[HttpGet("{id}")]
public ActionResult<Team> GetById(int id)
{
    var team = _teamService.GetById(id);
    return Ok(team);
}
```

### 3. Refaktorera (REFACTOR)

---

## 🚀 Kör & Testa

```bash
# Kör API
dotnet run --project MyProject.Api

# Kör tester
dotnet test

# Öppna Swagger
# https://localhost:7XXX/swagger
```

---

## 🎯 Vanliga Endpoints

### CRUD för Teams:

| Method | Endpoint | Beskrivning |
|--------|----------|-------------|
| GET | /api/teams | Hämta alla teams |
| GET | /api/teams/{id} | Hämta specifikt team |
| POST | /api/teams | Skapa nytt team |
| PUT | /api/teams/{id} | Uppdatera team |
| DELETE | /api/teams/{id} | Ta bort team |

---

## 💡 Pro-Tips

### 1. CORS (om ni bygger frontend senare)

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

app.UseCors("AllowAll");
```

### 2. Validering med Data Annotations

```csharp
public class Team
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
}
```

### 3. Global Error Handling

```csharp
app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext context) =>
{
    return Results.Problem();
});
```

---

## 🆘 Felsökning

### Problem: "Swagger inte tillgänglig i produktion"

**Lösning:** Det är MENINGEN! Swagger ska bara köras i Development:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

### Problem: "Port redan används"

**Lösning:** Ändra port i `launchSettings.json`:

```json
"applicationUrl": "https://localhost:7001;http://localhost:5001"
```

### Problem: "EF migrations fungerar inte"

**Lösning:**

```bash
# Installera EF tools globalt
dotnet tool install --global dotnet-ef

# Kör migrations från Api-mappen
cd MyProject.Api
dotnet ef migrations add MigrationName
```

---

## ✅ Checklist Innan Demo Day

- [ ] Swagger UI fungerar och är snygg
- [ ] Alla endpoints dokumenterade
- [ ] Tester för alla controllers
- [ ] EF Core migrations funkar
- [ ] README beskriver API endpoints
- [ ] Build badge visar grönt

---

**Lycka till! 🚀**

> **Detta är så jäkla coolt!** Ni bygger ett RIKTIGT API som andra utvecklare kan använda! Swagger gör att de direkt kan se och testa era endpoints. Det är precis så vi jobbar i verkligheten! 💪

---

_© Campus Mölndal 2026 - TDD Grupprojekt_
