# 🧪 Testing Types Guide - Unit, BDD, Integration

🟢


**Förstå skillnaden mellan olika testtyper**

---

## 🎯 Overview

När du bygger en applikation behöver du olika **typer av tester** för olika syften:

| Test Type            | Vad testas?       | Isolerad?         | Hastighet      | När?                   |
| -------------------- | ----------------- | ----------------- | -------------- | ---------------------- |
| **Unit Test**        | En funktion/metod | ✅ Ja (mock allt) | ⚡ Snabbt      | Ofta, under utveckling |
| **BDD Test**         | User behavior     | ❌ Nej            | 🐢 Långsamt    | Features, user stories |
| **Integration Test** | Hela flödet       | ❌ Nej            | 🐌 Långsammast | Efter implementation   |

**Analogier:**

- **Unit Test** = Testa en enskild bil-del (motor)
- **BDD Test** = Testa att bilen kör som föraren förväntar sig
- **Integration Test** = Testa hela bilen med allt påkopplat

---

## 🔬 Unit Testing

### Vad är det?

**Unit Test = Testa EN funktion i ISOLATION**

- Testar business logic utan beroenden
- Mockar databas, externa API:er, services
- Snabba att köra (millisekunder)
- Red-Green-Refactor cykeln

### Exempel: Budget Calculator

**Funktion att testa:**

```csharp
public class BudgetCalculator
{
    public decimal CalculateRemainingBudget(decimal budget, List<decimal> expenses)
    {
        if (budget <= 0)
            throw new ArgumentException("Budget must be positive");

        var totalExpenses = expenses.Sum();
        return budget - totalExpenses;
    }
}
```

**Unit Test:**

```csharp
public class BudgetCalculatorTests
{
    [Fact]
    public void CalculateRemainingBudget_ValidInput_ReturnsCorrectAmount()
    {
        // Arrange
        var calculator = new BudgetCalculator();
        var budget = 1000m;
        var expenses = new List<decimal> { 200m, 300m, 150m };

        // Act
        var result = calculator.CalculateRemainingBudget(budget, expenses);

        // Assert
        Assert.Equal(350m, result); // 1000 - 650 = 350
    }

    [Fact]
    public void CalculateRemainingBudget_ZeroBudget_ThrowsException()
    {
        // Arrange
        var calculator = new BudgetCalculator();

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => calculator.CalculateRemainingBudget(0, new List<decimal>()));
    }

    [Theory]
    [InlineData(1000, 500, 500)]
    [InlineData(2000, 1500, 500)]
    [InlineData(500, 0, 500)]
    public void CalculateRemainingBudget_VariousInputs_ReturnsExpected(
        decimal budget, decimal expense, decimal expected)
    {
        // Arrange
        var calculator = new BudgetCalculator();

        // Act
        var result = calculator.CalculateRemainingBudget(
            budget, new List<decimal> { expense });

        // Assert
        Assert.Equal(expected, result);
    }
}
```

**Vad testas?**

- ✅ Business logic (beräkning)
- ✅ Edge cases (noll-budget)
- ✅ Olika inputs med parametriserade tester
- ❌ INTE databas
- ❌ INTE externa API:er

---

## 🎭 Mocking för Unit Tests

### Varför mocka?

**Problem:** Dina funktioner har beroenden:

```csharp
public class CustomerService
{
    private readonly ICustomerRepository _repo; // Databas!
    private readonly IEmailService _emailService; // Extern service!

    public async Task<bool> RegisterCustomerAsync(Customer customer)
    {
        // Spara i databas
        await _repo.AddAsync(customer);

        // Skicka välkomst-email
        await _emailService.SendWelcomeEmailAsync(customer.Email);

        return true;
    }
}
```

**Om vi INTE mockar:**

- 🐌 Testet tar sekunder (databas + email)
- ❌ Behöver riktig databas igång
- ❌ Skickar faktiska emails (oops!)
- ❌ Testet failar om internet är nere

**Med mocking:**

- ⚡ Testet tar millisekunder
- ✅ Isolerad testning
- ✅ Kontrollerat beteende
- ✅ Funkar offline

### NSubstitute - Mocking Library

**Installation:**

```bash
dotnet add package NSubstitute
```

**Exempel med Mocking:**

```csharp
using NSubstitute;

public class CustomerServiceTests
{
    [Fact]
    public async Task RegisterCustomer_ValidInput_SavesAndSendsEmail()
    {
        // Arrange
        var mockRepo = Substitute.For<ICustomerRepository>();
        var mockEmailService = Substitute.For<IEmailService>();
        var service = new CustomerService(mockRepo, mockEmailService);

        var customer = new Customer
        {
            Name = "Anna Andersson",
            Email = "anna@test.se"
        };

        // Act
        var result = await service.RegisterCustomerAsync(customer);

        // Assert
        Assert.True(result);

        // Verifiera att AddAsync kallades med rätt customer
        await mockRepo.Received(1).AddAsync(customer);

        // Verifiera att email skickades
        await mockEmailService.Received(1)
            .SendWelcomeEmailAsync("anna@test.se");
    }

    [Fact]
    public async Task RegisterCustomer_RepoThrows_ReturnsFalse()
    {
        // Arrange
        var mockRepo = Substitute.For<ICustomerRepository>();
        var mockEmailService = Substitute.For<IEmailService>();

        // Simulera att databasen failar
        mockRepo.AddAsync(Arg.Any<Customer>())
            .Returns(Task.FromException(new Exception("DB error")));

        var service = new CustomerService(mockRepo, mockEmailService);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(
            () => service.RegisterCustomerAsync(new Customer()));

        // Email ska INTE skickas om databas failar
        await mockEmailService.DidNotReceive()
            .SendWelcomeEmailAsync(Arg.Any<string>());
    }
}
```

**NSubstitute Syntax:**

```csharp
// Skapa mock
var mock = Substitute.For<IMyService>();

// Konfigurera return value
mock.GetById(5).Returns(new Customer { Id = 5 });

// Verifiera att metod kallades
mock.Received(1).GetById(5); // Exakt 1 gång
mock.Received().GetById(Arg.Any<int>()); // Med vilket ID som helst
mock.DidNotReceive().Delete(Arg.Any<int>()); // INTE kallad

// Simulera exception
mock.GetById(Arg.Any<int>())
    .Returns(x => throw new NotFoundException());

// Asynkrona metoder
await mock.GetAllAsync().Returns(Task.FromResult(new List<Customer>()));
```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

---

## 🥒 BDD (Behavior-Driven Development)

### Vad är det?

**BDD = Testa BETEENDE ur användarens perspektiv**

- Skrivet i **Gherkin** (Given-When-Then)
- Läsbart av icke-programmerare
- Fokus på user stories och acceptance criteria
- Testar hela flödet (inklusive databas)

### Gherkin Syntax

```gherkin
Feature: Customer Registration

Scenario: Registrera ny kund med giltig data
  Given att systemet är tomt
  When jag registrerar kund:
    | Name            | Email           |
    | Anna Andersson  | anna@test.se    |
  Then ska kunden sparas i databasen
  And ett välkomstmail ska skickas till "anna@test.se"
  And svaret ska vara 201 Created

Scenario: Försök registrera kund med samma email
  Given att kund "anna@test.se" redan finns
  When jag försöker registrera ny kund med email "anna@test.se"
  Then ska jag få felmeddelande "Email already exists"
  And svaret ska vara 400 Bad Request
```

**Struktur:**

- **Given** = Förbered systemet (setup)
- **When** = Utför action (act)
- **Then** = Verifiera resultat (assert)
- **And** = Lägg till fler steg

### BDD med SpecFlow (C#)

**Installation:**

```bash
dotnet add package SpecFlow.xUnit
dotnet add package SpecFlow.Tools.MsBuild.Generation
```

**Feature fil (CustomerRegistration.feature):**

```gherkin
Feature: Customer Registration
  As a customer
  I want to register an account
  So that I can use the system

Scenario: Register with valid data
  Given the system is empty
  When I register a customer with:
    | Name           | Email          |
    | Anna Andersson | anna@test.se   |
  Then the customer should be saved
  And a welcome email should be sent to "anna@test.se"
```

**Step Definitions (C#):**

```csharp
[Binding]
public class CustomerRegistrationSteps
{
    private readonly ScenarioContext _context;
    private readonly CustomerService _service;
    private Customer _registeredCustomer;

    public CustomerRegistrationSteps(ScenarioContext context)
    {
        _context = context;
        _service = new CustomerService(/* real dependencies */);
    }

    [Given(@"the system is empty")]
    public void GivenTheSystemIsEmpty()
    {
        // Clear database
        _context["Database"].Clear();
    }

    [When(@"I register a customer with:")]
    public async Task WhenIRegisterCustomer(Table table)
    {
        var row = table.Rows[0];
        var customer = new Customer
        {
            Name = row["Name"],
            Email = row["Email"]
        };

        _registeredCustomer = await _service.RegisterCustomerAsync(customer);
    }

    [Then(@"the customer should be saved")]
    public void ThenCustomerShouldBeSaved()
    {
        Assert.NotNull(_registeredCustomer);
        Assert.True(_registeredCustomer.Id > 0);
    }

    [Then(@"a welcome email should be sent to ""(.*)""")]
    public void ThenWelcomeEmailSent(string email)
    {
        // Verifiera att email skickades (check email service logs)
        var emailService = _context.Get<IEmailService>("EmailService");
        // ... verification logic
    }
}
```

---

## 🔗 Integration Testing

### Vad är det?

**Integration Test = Testa HELA systemet tillsammans**

- Riktig databas (test-databas)
- Riktig API (in-memory test server)
- Externa anrop (mockade eller test-endpoints)
- Testar att alla delar fungerar ihop

### Exempel: API Integration Test

```csharp
public class CustomersControllerIntegrationTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public CustomersControllerIntegrationTests(
        WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Använd in-memory databas för tester
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // Seed test data
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                SeedTestData(db);
            });
        });

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ReturnsAllCustomers()
    {
        // Act
        var response = await _client.GetAsync("/api/customers");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var customers = JsonSerializer.Deserialize<List<CustomerDto>>(content);

        Assert.NotNull(customers);
        Assert.True(customers.Count > 0);
    }

    [Fact]
    public async Task Create_ValidCustomer_ReturnsCreated()
    {
        // Arrange
        var newCustomer = new CreateCustomerDto
        {
            Name = "Test Customer",
            Email = "test@example.com"
        };

        var json = JsonSerializer.Serialize(newCustomer);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/customers", content);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var createdCustomer = JsonSerializer.Deserialize<CustomerDto>(responseContent);

        Assert.NotNull(createdCustomer);
        Assert.Equal("Test Customer", createdCustomer.Name);

        // Verifiera att Location header finns
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task GetById_NonExistent_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync("/api/customers/99999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Update_ValidCustomer_ReturnsNoContent()
    {
        // Arrange
        var updateDto = new UpdateCustomerDto
        {
            Id = 1,
            Name = "Updated Name"
        };

        var json = JsonSerializer.Serialize(updateDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PutAsync("/api/customers/1", content);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verifiera att uppdateringen faktiskt sparades
        var getResponse = await _client.GetAsync("/api/customers/1");
        var getContent = await getResponse.Content.ReadAsStringAsync();
        var customer = JsonSerializer.Deserialize<CustomerDto>(getContent);

        Assert.Equal("Updated Name", customer.Name);
    }

    private void SeedTestData(AppDbContext db)
    {
        db.Customers.AddRange(
            new Customer { Id = 1, Name = "Anna", Email = "anna@test.se" },
            new Customer { Id = 2, Name = "Bob", Email = "bob@test.se" }
        );
        db.SaveChanges();
    }
}
```

---

## 🎯 Mocka Externt API i Tester

### Problem

Ditt projekt kallar på externt API (Gemini, OpenWeather, etc):

```csharp
public class WeatherService
{
    private readonly HttpClient _httpClient;

    public async Task<WeatherData> GetWeatherAsync(string city)
    {
        var response = await _httpClient.GetAsync(
            $"https://api.openweathermap.org/data/2.5/weather?q={city}");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<WeatherData>(json);
    }
}
```

**Problem utan mock:**

- 🐌 Långsamt (nätverksanrop)
- 💸 Kostar pengar (API rate limits)
- ❌ Kan faila om service är nere
- ❌ Svårt att testa edge cases

### Lösning 1: Mock HttpClient

```csharp
public class WeatherServiceTests
{
    [Fact]
    public async Task GetWeather_ValidCity_ReturnsWeatherData()
    {
        // Arrange
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{
                    ""main"": { ""temp"": 15.5 },
                    ""weather"": [{ ""description"": ""clear sky"" }]
                }")
            });

        var httpClient = new HttpClient(mockHandler.Object);
        var service = new WeatherService(httpClient);

        // Act
        var result = await service.GetWeatherAsync("Stockholm");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(15.5, result.Temperature);
        Assert.Equal("clear sky", result.Description);
    }
}
```

### Lösning 2: Interface Abstraction

**Bättre approach:**

```csharp
public interface IWeatherApiClient
{
    Task<WeatherData> GetWeatherAsync(string city);
}

public class WeatherService
{
    private readonly IWeatherApiClient _apiClient;

    public WeatherService(IWeatherApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<string> GetWeatherReportAsync(string city)
    {
        var weather = await _apiClient.GetWeatherAsync(city);

        return weather.Temperature > 20
            ? $"Det är varmt i {city}! ({weather.Temperature}°C)"
            : $"Det är kallt i {city}. ({weather.Temperature}°C)";
    }
}
```

**Test:**

```csharp
public class WeatherServiceTests
{
    [Fact]
    public async Task GetWeatherReport_WarmWeather_ReturnsWarmMessage()
    {
        // Arrange
        var mockApiClient = Substitute.For<IWeatherApiClient>();
        mockApiClient.GetWeatherAsync("Stockholm")
            .Returns(new WeatherData
            {
                Temperature = 25,
                Description = "sunny"
            });

        var service = new WeatherService(mockApiClient);

        // Act
        var result = await service.GetWeatherReportAsync("Stockholm");

        // Assert
        Assert.Contains("varmt", result);
        Assert.Contains("25", result);
    }

    [Theory]
    [InlineData(-5, "kallt")]
    [InlineData(10, "kallt")]
    [InlineData(25, "varmt")]
    [InlineData(30, "varmt")]
    public async Task GetWeatherReport_VariousTemperatures_ReturnsCorrectMessage(
        double temp, string expectedWord)
    {
        // Arrange
        var mockApiClient = Substitute.For<IWeatherApiClient>();
        mockApiClient.GetWeatherAsync(Arg.Any<string>())
            .Returns(new WeatherData { Temperature = temp });

        var service = new WeatherService(mockApiClient);

        // Act
        var result = await service.GetWeatherReportAsync("Stockholm");

        // Assert
        Assert.Contains(expectedWord, result);
    }
}
```

---

## 📊 Jämförelse & Rekommendationer

### När ska jag använda vad?

| Scenario              | Test Type     | Varför?              |
| --------------------- | ------------- | -------------------- |
| Testar business logic | Unit          | Snabbt, isolerat     |
| Testar beräkningar    | Unit + Theory | Många inputs         |
| Testar user story     | BDD           | Dokumentation        |
| Testar API endpoint   | Integration   | End-to-end           |
| Testar databas-lager  | Integration   | Verklig data         |
| Mockar externt API    | Unit          | Kontroll & hastighet |

### Test Pyramid

```
         /\
        /  \  E2E (Integration Tests)
       /----\
      /      \ BDD (Feature Tests)
     /--------\
    /          \ Unit Tests
   /____________\
```

**Princip:**

- **70%** Unit tests (snabba, många)
- **20%** BDD tests (features)
- **10%** Integration tests (end-to-end)

### För ert projekt

**G-nivå (80% coverage):**

- ✅ Unit tests för all business logic
- ✅ Integration tests för minst 3 API endpoints
- ✅ Mocka externt API med NSubstitute
- ✅ Några parametriserade tester (`[Theory]`)

**VG-nivå (90% coverage):**

- ✅ Allt ovan +
- ✅ BDD tests med Gherkin för minst 3 user stories
- ✅ Edge case testing
- ✅ Mocka alla beroenden korrekt
- ✅ Test documentation (XML comments)

---

## 🔧 Setup & Best Practices

### Project Structure

```
MyProject/
├── src/
│   ├── MyProject.Api/
│   ├── MyProject.Core/
│   └── MyProject.Data/
└── tests/
    ├── MyProject.UnitTests/
    │   ├── Services/
    │   ├── Calculators/
    │   └── ...
    ├── MyProject.BDD/
    │   ├── Features/
    │   └── StepDefinitions/
    └── MyProject.IntegrationTests/
        ├── Controllers/
        └── ...
```

### Test Naming

```csharp
// ✅ BRA
[Fact]
public void CalculateBudget_NegativeAmount_ThrowsException()

[Fact]
public void GetCustomerById_ExistingId_ReturnsCustomer()

[Fact]
public void CreateOrder_InvalidInput_ReturnsBadRequest()

// ❌ DÅLIGT
[Fact]
public void Test1()

[Fact]
public void CustomerTest()

[Fact]
public void ItWorks()
```

**Pattern:** `MethodName_Scenario_ExpectedResult`

### Arrange-Act-Assert

```csharp
[Fact]
public void Example_Test()
{
    // Arrange - Setup
    var service = new MyService();
    var input = "test";

    // Act - Execute
    var result = service.DoSomething(input);

    // Assert - Verify
    Assert.Equal("expected", result);
}
```

---

## 🎯 Sammanfattning

**Unit Tests:**

- ⚡ Snabba, isolerade
- 🎯 Testar EN funktion
- 🎭 Mockar alla beroenden
- 📊 70% av dina tester

**BDD Tests:**

- 📖 Läsbara scenarios (Gherkin)
- 👤 User-perspektiv
- 📝 Dokumentation
- 📊 20% av dina tester

**Integration Tests:**

- 🔗 Testar hela systemet
- 💾 Riktig databas (test)
- 🌐 Riktig API
- 📊 10% av dina tester

**Mocking:**

- Använd NSubstitute
- Mocka databas, externa API:er, services
- Verifiera att metoder kallas
- Simulera errors och edge cases

**För projektet:**

- ✅ 80%+ coverage (G), 90%+ (VG)
- ✅ Unit tests för business logic
- ✅ Mocka externt API
- ✅ Integration tests för endpoints
- ✅ BDD för user stories (VG)

**Lycka till med testningen! 🚀**

---

_© Campus Mölndal 2025 - Test och Kvalitetssäkring CLO25_
