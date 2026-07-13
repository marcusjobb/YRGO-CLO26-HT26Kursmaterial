---
marp: true
theme: nion-dark
paginate: true
---

# Unit-testning med xUnit

**Kurs:** Test och kvalitetssäkring
**Modul:** 02 — Unit-testning
Marcus Ackre Medina · YRGO · CLO26

---

## Vad är ett unit-test?

Ett test som verifierar att **en enskild enhet** av koden beter sig som förväntat.

En "enhet" är vanligtvis en metod.

Testet är isolerat — det beror inte på databas, nätverk eller andra klasser.

---

## Varför unit-testa?

- Fångar buggar tidigt — när de är billiga att fixa
- Ger trygghet vid refactoring
- Är levande dokumentation av hur koden ska användas
- Gör dig mer säker på att koden faktiskt fungerar

**Manuell testning skalar inte. Automatiserade tester gör det.**

---

## xUnit — det rekommenderade ramverket

xUnit är standardvalet för nya .NET-projekt.

```xml
<!-- Lägg till i testprojektets .csproj -->
<PackageReference Include="xunit" Version="2.4.2" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.4.5" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.1" />
```

Skapa ett separat projekt av typen **xUnit Test Project**.

---

## Projektstruktur

```
MySolution/
├── MyApp/                   ← Produktionskod
│   └── Calculator.cs
└── MyApp.Tests/             ← Testprojekt
    └── CalculatorTests.cs
```

Testprojektet refererar till produktionsprojektet.

---

## AAA — Arrange, Act, Assert

Varje test har tre tydliga delar.

```csharp
[Fact]
public void Add_TwoPositiveNumbers_ReturnsCorrectSum()
{
    // Arrange — förbered
    var calculator = new Calculator();

    // Act — utför
    int result = calculator.Add(2, 3);

    // Assert — verifiera
    Assert.Equal(5, result);
}
```

---

## [Fact] — ett enskilt test

```csharp
[Fact]
public void Divide_ByZero_ThrowsException()
{
    // Arrange
    var calculator = new Calculator();

    // Act & Assert
    Assert.Throws<DivideByZeroException>(
        () => calculator.Divide(10, 0)
    );
}
```

`[Fact]` = ett test med fasta värden.

---

## [Theory] — datadriven testning

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(0, 0, 0)]
[InlineData(-1, 1, 0)]
[InlineData(-5, -3, -8)]
public void Add_VariousInputs_ReturnsCorrectSum(
    int a, int b, int expected)
{
    var calculator = new Calculator();
    var result = calculator.Add(a, b);
    Assert.Equal(expected, result);
}
```

`[Theory]` + `[InlineData]` = samma test med olika värden.

---

## Assert — vad du kan kontrollera

```csharp
Assert.Equal(expected, actual);       // Värden är lika
Assert.NotEqual(wrong, actual);       // Värden är INTE lika
Assert.True(condition);               // Villkor är sant
Assert.False(condition);              // Villkor är falskt
Assert.Null(obj);                     // Objekt är null
Assert.NotNull(obj);                  // Objekt är INTE null
Assert.Throws<T>(() => ...);         // Undantag kastas
Assert.Contains(item, collection);   // Samling innehåller
```

---

## Testnamn — ett kontrakt i ord

❌
```csharp
public void Test1() { }
public void AddTest() { }
```

✅
```csharp
public void Add_TwoPositiveNumbers_ReturnsSum() { }
public void Add_NegativeAndPositive_ReturnsCorrectDifference() { }
public void Divide_ByZero_ThrowsDivideByZeroException() { }
```

**Mönster: MetodNamn_Scenario_FörväntadOutput**

---

## Testa edge cases

Tänk: vad kan gå snett?

```csharp
[Theory]
[InlineData(null)]
[InlineData("")]
[InlineData("   ")]
public void IsValidEmail_EmptyOrNull_ReturnsFalse(string email)
{
    var validator = new StringValidator();
    bool result = validator.IsValidEmail(email);
    Assert.False(result);
}
```

**Null, tom sträng, blanktecken — testa gränsfall.**

---

## Isolering med interface

Testet ska inte bero på en riktig databas.

```csharp
public interface IUserRepository
{
    User GetById(int id);
}

// I testet: skapa en falsk implementation
public class FakeUserRepository : IUserRepository
{
    public User GetById(int id) =>
        new User { Id = id, Name = "Test User" };
}
```

---

## Mocking med Moq

```csharp
[Fact]
public void RegisterUser_ShouldSendWelcomeEmail()
{
    // Arrange
    var mockEmail = new Mock<IEmailService>();
    var service = new UserService(mockEmail.Object);

    // Act
    service.RegisterUser("test@example.com");

    // Assert — verifiera att e-post skickades
    mockEmail.Verify(x => x.Send(
        "test@example.com", It.IsAny<string>()
    ), Times.Once);
}
```

---

## Vad gör ett bra test?

- Testar **ett** beteende per test
- Är **oberoende** av andra tester
- Kör **snabbt** (millisekunder)
- Har ett **tydligt namn** som förklarar vad som testas
- Misslyckas av **rätt anledning** när koden är fel

---

## Vanliga misstag

❌ Testa implementationsdetaljer istället för beteende
❌ Ha delat state mellan tester (ordningsberoende)
❌ Skriva tester som alltid är gröna oavsett kod
❌ Testa privata metoder direkt
❌ Överhoppa Assert-steget

---

## Kör testerna

```bash
# Via terminal
dotnet test

# Med coverage-rapport
dotnet test --collect:"XPlat Code Coverage"
```

I Visual Studio: öppna **Test Explorer** och kör därifrån.

Grönt = OK. Rött = något är fel — antingen testet eller koden.

---

## Sammanfattning

- ✅ xUnit — `[Fact]` för enstaka test, `[Theory]` för datadriven
- ✅ AAA-mönstret — Arrange, Act, Assert
- ✅ Testnamn: MetodNamn_Scenario_FörväntadOutput
- ✅ Isolera beroenden med interface och mock
- ✅ Testa edge cases, inte bara happy path

**Nästa: TDD — skriv testet INNAN koden**
