---

title: Example 1: Docker-based testing for a weather forecasting API
author: Marcus Ackre Medina
type: lecture
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/4_test/lectures/06_isolated_test_environments/2_example.md"
description: "FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build"
tags: ["csharp", "docker-based", "forecasting", "installation", "testing", "verktyg", "weather"]
week_fit: []
---

### **Exempel 1: Docker-baserad testning för väderprognos-API**

#### Dockerfile

```Dockerfile
# Example 1: Docker-based testing for a weather forecasting API

🟢

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet build --no-restore -c Release
CMD ["dotnet", "test", "--logger:trx", "--results-directory:/testresults"]
```

#### C#-kod (med MsTestV2)

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass]
public class WeatherForecastTests
{
    [TestMethod]
    public async Task GetForecast_ReturnsValidData()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        mockHttpClient.Setup(client => client.GetStringAsync(It.IsAny<string>()))
            .ReturnsAsync("{\"temperature\": 25, \"conditions\": \"Sunny\"}");

        var weatherService = new WeatherService(mockHttpClient.Object);

        // Act
        var forecast = await weatherService.GetForecast("London");

        // Assert
        Assert.AreEqual(25, forecast.Temperature);
        Assert.AreEqual("Sunny", forecast.Conditions);
    }
}

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherForecast> GetForecast(string city)
    {
        var response = await _httpClient.GetStringAsync($"api/weather/{city}");
        // Parse JSON response and return WeatherForecast object
        return new WeatherForecast { Temperature = 25, Conditions = "Sunny" };
    }
}

public class WeatherForecast
{
    public int Temperature { get; set; }
    public string Conditions { get; set; }
}
```

---

### **Exempel 2: Docker Compose för e-handelsapplikation**

#### Docker Compose-fil

```yaml
version: "3.8"
services:
  app:
    build:
      context: .
      dockerfile: Dockerfile
    depends_on:
      - db
    environment:
      - ConnectionStrings__DefaultConnection=Server=db;Database=ECommerceDB;User=sa;Password=YourStrong@Passw0rd;
  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      SA_PASSWORD: "YourStrong@Passw0rd"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
```

#### C#-kod (med MsTestV2)

```csharp
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class OrderProcessingTests : IDisposable
{
    private readonly ECommerceContext _context;
    private readonly OrderProcessor _orderProcessor;

    public OrderProcessingTests()
    {
        var options = new DbContextOptionsBuilder<ECommerceContext>()
            .UseSqlServer("Server=db;Database=ECommerceDB;User=sa;Password=YourStrong@Passw0rd;")
            .Options;
        _context = new ECommerceContext(options);
        _context.Database.EnsureCreated();
        _orderProcessor = new OrderProcessor(_context);
    }

    [TestMethod]
    public async Task ProcessOrder_ValidOrder_ReducesInventory()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test Product", Inventory = 10 };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var order = new Order { ProductId = 1, Quantity = 2 };

        // Act
        await _orderProcessor.ProcessOrder(order);

        // Assert
        var updatedProduct = await _context.Products.FindAsync(1);
        Assert.AreEqual(8, updatedProduct.Inventory);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}

public class ECommerceContext : DbContext
{
    public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Inventory { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderProcessor
{
    private readonly ECommerceContext _context;

    public OrderProcessor(ECommerceContext context)
    {
        _context = context;
    }

    public async Task ProcessOrder(Order order)
    {
        var product = await _context.Products.FindAsync(order.ProductId);
        if (product != null && product.Inventory >= order.Quantity)
        {
            product.Inventory -= order.Quantity;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
    }
}
```

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
