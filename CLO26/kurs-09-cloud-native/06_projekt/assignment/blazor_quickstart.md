---

title: 🔥 Blazor Quickstart - Full-Stack C#
author: Marcus Ackre Medina
type: assignment
topic: projekt
difficulty: 1
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/4_test_and_quality_assurance/assignment/cheatsheets/blazor_quickstart.md"
description: "För TDD Grupprojekt - Modern Web Apps"
tags: ["blazor", "full-stack", "installation", "projekt", "quickstart", "razor", "visual-studio"]
week_fit: []
---

# 🔥 Blazor Quickstart - Full-Stack C#

🟢


**För TDD Grupprojekt - Modern Web Apps**

> **Vill ni bygga en snygg webbapp UTAN att lära er JavaScript?** Blazor är lösningen! Skriv frontend OCH backend i C#! 🎯

---

## 🌟 Vad är Blazor?

**Blazor** = Build web apps using C# instead of JavaScript

**Två varianter:**
- **Blazor Server** - Körs på servern, snabb att utveckla
- **Blazor WebAssembly** - Körs i webbläsaren, kan deployas statiskt

**Vi rekommenderar Blazor Server för detta projekt!** Enklare att komma igång! 🚀

---

## ⚡ Snabbstart (20 minuter)

### Steg 1: Skapa Solution & Projekt (5 min)

```bash
# Skapa solution
dotnet new sln -n MyProject

# Skapa Blazor Server App
dotnet new blazorserver -n MyProject.Web

# Skapa Core-bibliotek för business logic
dotnet new classlib -n MyProject.Core

# Skapa test-projekt
dotnet new xunit -n MyProject.Tests

# Lägg till alla i solution
dotnet sln add MyProject.Web
dotnet sln add MyProject.Core
dotnet sln add MyProject.Tests
```

**Vad får vi?**
- ✅ Blazor Server projekt med exempelkomponenter
- ✅ Bootstrap CSS redan installerat
- ✅ Routing och navigation setup
- ✅ Hot reload (kod uppdateras live!)

---

### Steg 2: Lägg till Project References (2 min)

```bash
# Web behöver Core
cd MyProject.Web
dotnet add reference ../MyProject.Core

# Tests behöver både Web och Core
cd ../MyProject.Tests
dotnet add reference ../MyProject.Web
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

# I MyProject.Web - för EF migrations
cd ../MyProject.Web
dotnet add package Microsoft.EntityFrameworkCore.Design

# I MyProject.Tests - för testing
cd ../MyProject.Tests
dotnet add package Moq
dotnet add package FluentAssertions
dotnet add package bunit  # 👈 För att testa Blazor-komponenter!

cd ..
```

---

### Steg 4: Testa att Det Fungerar (2 min)

```bash
# Kör projektet
cd MyProject.Web
dotnet run

# Öppna i webbläsare:
# https://localhost:7XXX
```

**Du borde se:**
- 🏠 Home-sida med "Hello, world!"
- 🔢 Counter-sida (klickräknare)
- 🌦️ Weather-sida (exempel-data)

**Hot Reload:** Ändra kod → Spara → Sidan uppdateras automatiskt! 🔥

---

## 📝 Blazor Grunderna

### Blazor Komponent (`.razor` fil)

En Blazor-komponent = HTML + C# i samma fil!

**Pages/Teams.razor:**

```razor
@page "/teams"
@using MyProject.Core.Entities

<PageTitle>Teams</PageTitle>

<h1>Teams</h1>

<button class="btn btn-primary" @onclick="AddTeam">
    Add Team
</button>

<ul>
    @foreach (var team in teams)
    {
        <li>@team.Name</li>
    }
</ul>

@code {
    private List<Team> teams = new();

    protected override void OnInitialized()
    {
        // Ladda teams
        teams.Add(new Team { Id = 1, Name = "Team A" });
        teams.Add(new Team { Id = 2, Name = "Team B" });
    }

    private void AddTeam()
    {
        teams.Add(new Team { Id = teams.Count + 1, Name = $"Team {teams.Count + 1}" });
    }
}
```

**Så coolt! HTML och C# tillsammans!** 🤯

---

## 🎨 Komponent-Struktur

### Anatomy of a Blazor Component:

```razor
@* 1. DIREKTIV - Routing, using statements *@
@page "/teams"
@inject ITeamService TeamService

@* 2. HTML/MARKUP - Din UI *@
<div class="container">
    <h1>@title</h1>

    @if (isLoading)
    {
        <p>Loading...</p>
    }
    else
    {
        <ul>
            @foreach (var team in teams)
            {
                <li>@team.Name</li>
            }
        </ul>
    }
</div>

@* 3. CODE BLOCK - C# logik *@
@code {
    private string title = "Teams";
    private bool isLoading = true;
    private List<Team> teams = new();

    protected override async Task OnInitializedAsync()
    {
        teams = await TeamService.GetAllAsync();
        isLoading = false;
    }
}
```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

---

## 🔄 Dependency Injection i Blazor

### 1. Skapa en Service

**MyProject.Core/Services/ITeamService.cs:**

```csharp
public interface ITeamService
{
    Task<List<Team>> GetAllAsync();
    Task<Team?> GetByIdAsync(int id);
    Task<Team> CreateAsync(Team team);
}
```

### 2. Implementera Service

**MyProject.Core/Services/TeamService.cs:**

```csharp
public class TeamService : ITeamService
{
    private readonly AppDbContext _context;

    public TeamService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Team>> GetAllAsync()
    {
        return await _context.Teams.ToListAsync();
    }

    // ... more methods
}
```

### 3. Registrera i Program.cs

**MyProject.Web/Program.cs:**

```csharp
// Lägg till services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddScoped<ITeamService, TeamService>();
```

### 4. Injicera i Komponent

```razor
@page "/teams"
@inject ITeamService TeamService

@code {
    private List<Team> teams = new();

    protected override async Task OnInitializedAsync()
    {
        teams = await TeamService.GetAllAsync();
    }
}
```

---

## 📋 CRUD Example - Teams Management

### Teams.razor (Complete Example)

```razor
@page "/teams"
@inject ITeamService TeamService
@inject NavigationManager Navigation

<PageTitle>Teams</PageTitle>

<div class="container mt-4">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h1>Teams</h1>
        <button class="btn btn-primary" @onclick="ShowCreateForm">
            ➕ Add Team
        </button>
    </div>

    @if (isLoading)
    {
        <div class="spinner-border" role="status">
            <span class="visually-hidden">Loading...</span>
        </div>
    }
    else if (teams.Count == 0)
    {
        <div class="alert alert-info">
            No teams found. Create your first team!
        </div>
    }
    else
    {
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Players</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var team in teams)
                {
                    <tr>
                        <td>@team.Name</td>
                        <td>@team.Players?.Count ?? 0</td>
                        <td>
                            <button class="btn btn-sm btn-info"
                                    @onclick="() => ViewTeam(team.Id)">
                                View
                            </button>
                            <button class="btn btn-sm btn-warning"
                                    @onclick="() => EditTeam(team.Id)">
                                Edit
                            </button>
                            <button class="btn btn-sm btn-danger"
                                    @onclick="() => DeleteTeam(team.Id)">
                                Delete
                            </button>
                        </td>
                    </tr>
                }
            </tbody>
        </table>
    }

    @if (showCreateForm)
    {
        <div class="card mt-3">
            <div class="card-header">
                <h5>Create New Team</h5>
            </div>
            <div class="card-body">
                <EditForm Model="@newTeam" OnValidSubmit="@HandleValidSubmit">
                    <DataAnnotationsValidator />
                    <ValidationSummary />

                    <div class="mb-3">
                        <label>Team Name:</label>
                        <InputText class="form-control" @bind-Value="newTeam.Name" />
                    </div>

                    <button type="submit" class="btn btn-success">Create</button>
                    <button type="button" class="btn btn-secondary"
                            @onclick="CancelCreate">
                        Cancel
                    </button>
                </EditForm>
            </div>
        </div>
    }
</div>

@code {
    private List<Team> teams = new();
    private Team newTeam = new();
    private bool isLoading = true;
    private bool showCreateForm = false;

    protected override async Task OnInitializedAsync()
    {
        await LoadTeams();
    }

    private async Task LoadTeams()
    {
        isLoading = true;
        teams = await TeamService.GetAllAsync();
        isLoading = false;
    }

    private void ShowCreateForm()
    {
        newTeam = new Team();
        showCreateForm = true;
    }

    private void CancelCreate()
    {
        showCreateForm = false;
    }

    private async Task HandleValidSubmit()
    {
        await TeamService.CreateAsync(newTeam);
        showCreateForm = false;
        await LoadTeams();
    }

    private void ViewTeam(int id)
    {
        Navigation.NavigateTo($"/teams/{id}");
    }

    private void EditTeam(int id)
    {
        Navigation.NavigateTo($"/teams/{id}/edit");
    }

    private async Task DeleteTeam(int id)
    {
        if (confirm("Are you sure?")) // JavaScript interop
        {
            await TeamService.DeleteAsync(id);
            await LoadTeams();
        }
    }
}
```

---

## 🧩 Återanvändbara Komponenter

### TeamCard.razor (Shared Component)

**Shared/TeamCard.razor:**

```razor
<div class="card mb-3">
    <div class="card-header">
        <h5>@Team.Name</h5>
    </div>
    <div class="card-body">
        <p>Players: @Team.Players?.Count</p>
        <button class="btn btn-primary" @onclick="OnViewClicked">
            View Details
        </button>
    </div>
</div>

@code {
    [Parameter]
    public Team Team { get; set; } = new();

    [Parameter]
    public EventCallback<int> OnViewClicked { get; set; }
}
```

### Användning:

```razor
@foreach (var team in teams)
{
    <TeamCard Team="@team"
              OnViewClicked="@(() => ViewTeam(team.Id))" />
}
```

---

## 🎨 Navigering & Routing

### NavMenu.razor (Lägg till i menyn)

**Shared/NavMenu.razor:**

```razor
<div class="nav-item px-3">
    <NavLink class="nav-link" href="teams">
        <span class="oi oi-people" aria-hidden="true"></span> Teams
    </NavLink>
</div>
```

### Parametriserad Route:

```razor
@page "/teams/{TeamId:int}"

@code {
    [Parameter]
    public int TeamId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var team = await TeamService.GetByIdAsync(TeamId);
    }
}
```

---

## 🧪 Testa Blazor-Komponenter med bUnit

### Install bUnit:

```bash
cd MyProject.Tests
dotnet add package bunit
```

### Test Example:

```csharp
using Bunit;
using Xunit;
using FluentAssertions;

public class TeamsComponentTests : TestContext
{
    [Fact]
    public void Teams_RendersCorrectly()
    {
        // Arrange
        var mockService = new Mock<ITeamService>();
        mockService.Setup(s => s.GetAllAsync())
                   .ReturnsAsync(new List<Team>
                   {
                       new Team { Id = 1, Name = "Team A" }
                   });

        Services.AddSingleton(mockService.Object);

        // Act
        var cut = RenderComponent<Teams>();

        // Assert
        cut.Find("h1").TextContent.Should().Be("Teams");
        cut.FindAll("tr").Count.Should().Be(2); // Header + 1 team
    }
}
```

---

## 🗄️ Entity Framework Setup

### Program.cs:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Lägg till services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Lägg till DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Lägg till Services
builder.Services.AddScoped<ITeamService, TeamService>();

var app = builder.Build();

// Ensure database created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
```

---

## ✅ Projekt-Struktur

```
MyProject/
├── MyProject.Web/
│   ├── Pages/
│   │   ├── Index.razor           (Home page)
│   │   ├── Teams.razor           (Teams list)
│   │   └── TeamDetails.razor     (Single team)
│   ├── Shared/
│   │   ├── MainLayout.razor
│   │   ├── NavMenu.razor
│   │   └── TeamCard.razor        (Reusable component)
│   ├── wwwroot/
│   │   └── css/
│   │       └── app.css
│   ├── _Imports.razor            (Global using statements)
│   ├── App.razor
│   └── Program.cs
├── MyProject.Core/
│   ├── Entities/
│   ├── Services/
│   └── Data/
├── MyProject.Tests/
│   └── TeamsComponentTests.cs
└── MyProject.sln
```

---

## 🎨 Styling med Bootstrap

Blazor kommer med Bootstrap 5 installerat!

```razor
<div class="container">
    <div class="row">
        <div class="col-md-6">
            <div class="card">
                <div class="card-body">
                    @* Content *@
                </div>
            </div>
        </div>
    </div>
</div>
```

### Custom CSS:

**wwwroot/css/app.css:**

```css
.team-card {
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    color: white;
    border-radius: 10px;
    padding: 20px;
}

.team-card:hover {
    transform: scale(1.05);
    transition: transform 0.2s;
}
```

---

## 💡 Pro-Tips för Blazor

### 1. Loading State Pattern

```razor
@if (isLoading)
{
    <LoadingSpinner />
}
else if (errorMessage != null)
{
    <ErrorAlert Message="@errorMessage" />
}
else
{
    @* Show content *@
}
```

### 2. Event Callbacks för Parent-Child Kommunikation

**Child Component:**
```razor
@code {
    [Parameter]
    public EventCallback<Team> OnTeamSelected { get; set; }

    private async Task SelectTeam(Team team)
    {
        await OnTeamSelected.InvokeAsync(team);
    }
}
```

**Parent Component:**
```razor
<TeamList OnTeamSelected="@HandleTeamSelected" />

@code {
    private void HandleTeamSelected(Team team)
    {
        // Do something with selected team
    }
}
```

### 3. StateHasChanged() för Manual Refresh

```razor
@code {
    private async Task UpdateData()
    {
        // Uppdatera data
        await Task.Delay(1000);

        // Force UI refresh
        StateHasChanged();
    }
}
```

---

## 🚀 Kör & Testa

```bash
# Kör Blazor app
dotnet run --project MyProject.Web

# Kör tester
dotnet test

# Watch mode (auto-restart)
dotnet watch run --project MyProject.Web
```

---

## 🆘 Felsökning

### Problem: "Komponenten renderar inte om"

**Lösning:** Anropa `StateHasChanged()` efter data-uppdateringar

```csharp
private async Task RefreshData()
{
    teams = await TeamService.GetAllAsync();
    StateHasChanged(); // 👈 Force refresh
}
```

### Problem: "Null reference exception i @code"

**Lösning:** Använd null-conditional operators:

```razor
<p>@team?.Name</p>
<p>@team?.Players?.Count ?? 0</p>
```

### Problem: "CSS inte uppdateras"

**Lösning:** Hard refresh i webbläsaren (Ctrl+Shift+R)

---

## 📚 Blazor vs API - När använder man vad?

| Blazor Server | REST API |
|---------------|----------|
| Full-stack C# | Bara backend |
| UI included | Behöver frontend |
| Enklare för mindre projekt | Bättre för större system |
| SignalR WebSocket | HTTP REST |
| Bra för interna system | Bra för public APIs |

**Båda är perfekta för TDD-projektet!** Välj det ni tycker är roligast! 🎯

---

## ✅ Checklist Innan Demo Day

- [ ] Alla sidor fungerar utan errors
- [ ] Navigation mellan sidor funkar
- [ ] CRUD operationer fungerar
- [ ] Formulär har validering
- [ ] Snyggt designat med Bootstrap
- [ ] Tester för viktiga komponenter
- [ ] README beskriver hur man kör projektet

---

**Lycka till! 🔥**

> **Blazor är MAGI!** Ni skriver C# och får en snygg webbapp - ingen JavaScript! Det är som att Harry Potter mötte programmering! 🪄 Ni kommer älska det här! 💚

---

_© Campus Mölndal 2026 - TDD Grupprojekt_
