# Kvalitetssäkring

🔴


Nu ska vi kolla på kvalitetssäkring enligt TDD. Låt oss ställa några
grundläggande frågor för att sätta tonen: Vad är kvalitetssäkring, varför är
det viktigt i C-Sharp-projekt, och vilka metoder och tillvägagångssätt kan du
använda för att säkerställa hög kvalitet i dina kodbaser?

## TL;DR

Kvalitetssäkring i C-Sharp är en integrerad process för att säkerställa att koden är pålitlig, korrekt
och effektiv. Genom att tillämpa kvalitetssäkringsmetoder kan du förbättra dina
programmeringsprojekt, minimera buggar och leverera pålitliga applikationer till användarna.

## Viktig regel

En **testare** är en programmerares bästa vän!

Ta inte på allvar det som memes antyder om att det är en ständig krig mellan kodare och testare.
Det är lögn, testare är de som räddar oss från pinsamma errors och framtida krascher.

**Testare är våra bästa vänner!**

## Kapitlets innehåll

- [Kodgranskning i praktiken](code_reviews.md) – hur du ger och tar emot feedback utan drama.
- [Testdriven utveckling (TDD)](tdd.md) – bygg funktioner genom att skriva tester först.

## När du läst detta ska du kunna

- Förstå och förklara vad kvalitetssäkring är och dess relevans inom C-Sharp-programmering.
- Diskutera vikten av kvalitetssäkring och hur det påverkar slutprodukten.
- Identifiera olika metoder och tillvägagångssätt för kvalitetssäkring i C-Sharp.
- Förstå fördelar och nackdelar med kvalitetssäkring i programmeringsprojekt.
- Sammanfatta viktiga insikter och rekommendationer för att förbättra ditt arbete med kvalitetssäkring.

## Vad är Kvalitetssäkring i C-Sharp?

Kvalitetssäkring är en process som fokuserar på att säkerställa att koden är korrekt, robust och
effektiv. Inom mjukvaruutveckling handlar kvalitetssäkring om att säkerställa att en applikation
uppfyller de förväntningar och krav som ställs på den. Det handlar om att testa och granska koden
för att hitta fel och brister och säkerställa att den fungerar som avsett.

I C-Sharp-programmering innebär kvalitetssäkring att tillämpa olika tekniker och metoder för att testa
och validera koden. Detta kan inkludera enhetstester, integrationstester, användartester och
statisk kodanalys.

## TDD med .NET och NUnit/xUnit

För att kunna köra TDD med .NET behöver du lägga till ett testramverk. Här är exempel för olika
ramverk:

### xUnit (rekommenderat för nya projekt)

```xml

<PackageReference Include="xunit" Version="2.4.2" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.4.5">
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  <PrivateAssets>all</PrivateAssets>
</PackageReference>
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.1" />

```

### MSTest

```xml

<PackageReference Include="MSTest.TestFramework" Version="3.1.1" />
<PackageReference Include="MSTest.TestAdapter" Version="3.1.1" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.1" />

```

### NUnit

```xml

<PackageReference Include="NUnit" Version="3.13.3" />
<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.7.1" />

```

## Historik och Användning

Historiskt sett har kvalitetssäkring sina rötter i traditionell ingenjörskonst, där man använde
olika metoder för att säkerställa att produkter uppfyllde kvalitetsstandarder. Med framväxten av
mjukvaruutveckling blev kvalitetssäkring en viktig aspekt för att säkerställa att mjukvaruprodukter
fungerar som avsett.

I C-Sharp-projekt är kvalitetssäkring avgörande eftersom C-Sharp används inom olika områden, från små
webbapplikationer till stora enterprise-lösningar. Fel och brister i koden kan leda till allvarliga
konsekvenser, såsom driftstörningar, säkerhetsproblem eller förlorade intäkter. Genom att tillämpa
kvalitetssäkringsmetoder kan utvecklare minimera risken för sådana problem och skapa mer pålitliga
och stabila applikationer.

## Viktigheten av Kvalitetssäkring

Kvalitetssäkring är av central betydelse för ett lyckat projekt. Genom att tidigt identifiera och
åtgärda buggar och fel kan man undvika kostsamma och tidskrävande problem senare i
utvecklingscykeln. Dessutom är användarnas förtroende för applikationen avgörande, och
kvalitetssäkring hjälper till att säkerställa att användarna får en smidig och pålitlig
användarupplevelse.

En annan viktig aspekt av kvalitetssäkring är att den gör det möjligt att upptäcka och åtgärda
säkerhetsproblem i tid. Mjukvara som inte är ordentligt kvalitetssäkrad kan vara sårbar för
attacker och hot, vilket kan få allvarliga konsekvenser för både användare och organisationer.

## Fördelar

Fördelarna med kvalitetssäkring är många och omfattar flera aspekter av programmeringsprojekt:

1. **Buggminimering:** Genom att tillämpa kvalitetssäkringsmetoder kan utvecklare identifiera och
åtgärda buggar tidigt i utvecklingsprocessen, vilket minimerar risken för felaktig funktionalitet
och förenklar felsökning.

2. **Ökad produktivitet:** Genom att ha väldefinierade kvalitetssäkringsprocesser och
automatiserade tester kan utvecklarteamet arbeta mer effektivt och snabbt identifiera problem.

3. **Förtroende och rykte:** Kvalitetssäkring bidrar till att bygga förtroende hos användarna genom
att leverera pålitliga och robusta produkter. Detta leder till ett positivt rykte för
organisationen och produkten.

4. **Säkerhet:** Genom att tillämpa säkerhetstester och statisk kodanalys kan kvalitetssäkring
hjälpa till att identifiera potentiella säkerhetshot och säkerställa att applikationen är skyddad
mot attacker.

## Nackdelar och Begränsningar

Som med alla processer har kvalitetssäkring också sina nackdelar och begränsningar:

1. **Tids- och resurskrävande:** Kvalitetssäkring kan kräva extra tid och resurser, särskilt i
början av ett projekt. Det kan vara lockande att hoppa över kvalitetssäkring för att snabbare
slutföra projektet, men det kan i längden leda till fler problem och ökad arbetsbelastning.

2. **Komplexitet:** Att säkerställa hög kvalitet i komplexa system kan vara utmanande, och det kan
vara svårt att täcka alla aspekter av koden med tester.

3. **Balansering:** Det kan vara svårt att hitta en balans mellan att utföra tillräckligt med
tester för att säkerställa kvaliteten och att undvika överdriven testning, vilket kan fördröja
utvecklingsprocessen.

## Metoder och Tillvägagångssätt

I C-Sharp-projekt kan du använda olika metoder för kvalitetssäkring, beroende på projektets omfattning
och krav:

1. **Enhetstester (Unit Tests):** Enhetstester testar enskilda komponenter av koden för att
säkerställa att de
fungerar korrekt i isolering. Detta görs genom att skriva tester som validerar att enskilda metoder
och funktioner returnerar förväntade resultat.

2. **Integrationstester:** Integrationstester testar hur olika komponenter i systemet samverkar med
varandra. Detta säkerställer att integrationen mellan olika delar av koden fungerar som avsett.

3. **End-to-End tester:** Tester som simulerar verkliga användarscenarier från början till slut.

4. **Statisk kodanalys:** Statisk kodanalys är en metod för att analysera koden utan att faktiskt
köra den. Detta hjälper till att identifiera potentiella fel och förbättringsmöjligheter genom att
granska kodens struktur och syntax.

## Exempelkod - Enhetstestning i C-Sharp

Här är ett exempel på hur du kan implementera en enkel enhetstest i C-Sharp med hjälp av xUnit:

```csharp

using Xunit;

public class CalculatorTest
{
    [Fact]
    public void TestGetDeciliters()
    {
        // Arrange
        var measurements = new Measurements();

        // Act
        float result = measurements.GetDeciliters(1);

        // Assert
        Assert.Equal(2.37f, result, 2); // 2 decimaler precision
    }

    [Theory]
    [InlineData(1, 2.37f)]
    [InlineData(2, 4.74f)]
    [InlineData(0, 0)]
    public void TestGetDeciliters_MultipleValues(float cups, float expected)
    {
        // Arrange
        var measurements = new Measurements();

        // Act
        float result = measurements.GetDeciliters(cups);

        // Assert
        Assert.Equal(expected, result, 2);
    }
}

public class Measurements
{
    public float GetDeciliters(float cups)
    {
        return cups * 2.36588237f;
    }
}

```

## Exempel med MSTest

```csharp

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CalculatorTest
{
    [TestMethod]
    public void TestGetDeciliters()
    {
        // Arrange
        var measurements = new Measurements();

        // Act
        float result = measurements.GetDeciliters(1);

        // Assert
        Assert.AreEqual(2.37f, result, 0.01f);
    }
}

```

## Mocking och Dependency Injection

För mer avancerade tester kan du använda mocking-bibliotek som Moq:

```csharp

using Moq;
using Xunit;

public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}

public class UserService
{
    private readonly IEmailService _emailService;

    public UserService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public void RegisterUser(string email)
    {
        // Registration logic
        _emailService.SendEmail(email, "Welcome", "Welcome to our service!");
    }
}

public class UserServiceTests
{
    [Fact]
    public void RegisterUser_ShouldSendWelcomeEmail()
    {
        // Arrange
        var mockEmailService = new Mock<IEmailService>();
        var userService = new UserService(mockEmailService.Object);

        // Act
        userService.RegisterUser("test@example.com");

        // Assert
        mockEmailService.Verify(x => x.SendEmail(
            "test@example.com",
            "Welcome",
            "Welcome to our service!"
        ), Times.Once);
    }
}

```

## Code Coverage

Du kan mäta kodtäckning med verktyg som coverlet:

```xml

<PackageReference Include="coverlet.collector" Version="6.0.0">
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  <PrivateAssets>all</PrivateAssets>
</PackageReference>

```

Kör tester med coverage:

```bash

dotnet test --collect:"XPlat Code Coverage"

```

## Rekommendationer

Här är några rekommendationer för att förbättra ditt arbete med kvalitetssäkring:

- **Automatisera tester:** Automatisering av tester kan hjälpa till att spara tid och resurser och
säkerställa att testerna utförs korrekt och konsekvent.

- **Använd testramverk:** Testramverk som xUnit, MSTest och Moq kan hjälpa till att förenkla och effektivisera testprocessen.

- **Minst ett test per metod:** Varje metod bör ha minst ett test som validerar dess
funktionalitet. "Ett test är bättre än inget test." som Bob Martin säger.

- **Testa gränserna:** Som ungdom brukar man få tillsägningar om att inte testa gränserna, men det
är precis vad vi måste göra i TDD. Se till att dina tester testar gränsvärdena för att säkerställa
att din kod. Om du dividerar måste du alltid kolla att ditt program inte kraschar vid division mot
0,
om du använder objekt, kontrollera hur null hanteras.

- **AAA-mönstret:** Strukturera dina tester med Arrange-Act-Assert mönstret för klarhet.

- **Beskrivande testnamn:** Använd tydliga namn som beskriver vad testet validerar.

## Termer

Här finns en lista på termer som används i artikeln:

| Term               | Förklaring                                                                                                                                |
| ------------------ | ----------------------------------------------------------------------------------------------------------------------------------------- |
| Kvalitetssäkring   | Processen att säkerställa att koden är korrekt och pålitlig genom testning och validering.                                                |
| Enhetstester       | Tester som testar enskilda komponenter av koden för korrekthet. Enhetstester kan utföras med hjälp av olika testramverk som xUnit i C-Sharp. |
| Integrationstester | Tester som testar hur olika komponenter samverkar i systemet för att säkerställa att integrationen fungerar som avsett.                   |
| Statisk kodanalys  | En metod för att analysera koden statiskt för att identifiera fel och förbättringsmöjligheter. Detta görs utan att faktiskt köra koden.   |
| Mocking           | Teknik för att skapa "falska" objekt för testning som ersätter riktiga beroenden.                                                        |
| Code Coverage     | Mått på hur stor del av koden som täcks av tester.                                                                                       |
| TDD               | Test-Driven Development - utvecklingsmetod där tester skrivs före produktionskoden.                                                      |

## Slutsats

Kvalitetssäkring är en oumbärlig del av C-Sharp-programmering och något som alla utvecklare bör
prioritera. Genom att tillämpa olika metoder och tillvägagångssätt kan du förbättra kvaliteten på
dina projekt, minimera buggar och fel, och skapa mer pålitliga och stabila applikationer. Så se
till att inkludera kvalitetssäkring i din utvecklingsprocess och se dina projekt blomstra till
framgång!

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
