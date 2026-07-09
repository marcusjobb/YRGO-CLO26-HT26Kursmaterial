# TDD

🟢


Nu ska vi vända upp och ner på allt du lärt dig hittils om programmering... Bokstavligt talat.

I denna artikel kommer vi att utforska TDD som ett kraftfullt programmeringsmönster som kan hjälpa
dig skapa pålitliga och väl fungerande program. Istället för att skriva kod först och sedan testa
den, tar vi en annan inriktning genom att först skriva testerna och sedan utveckla metoder som
uppfyller testkraven. Låt oss fördjupa oss i vad TDD är och varför det är en så viktig del av modern
programmering.

Du ska nu skriva metoder som anropar dina vanliga metoder i klassen, innan du implementerat din
klass, och bestämma vilka inputs du vill skicka till klassen och vilka outputs du förväntar dig. Sen
implementerar du metoden... weird va?

## TL;DR

Testdriven Utveckling (TDD) är ett programmeringsmönster där vi skriver testerna först innan vi
implementerar metoder eller funktionalitet. Det hjälper oss att säkra programmets funktionalitet och
överlevnad genom att låta testerna styra vår utvecklingsriktning. Med TDD kan vi känna oss trygga i
att vårt program fungerar som förväntat och även underlättar vidare utveckling.

## När du läst detta ska du kunna

- Förstå konceptet bakom Testdriven Utveckling (TDD) och dess fördelar.
- Förklara hur TDD kan säkerställa att programmet fungerar korrekt.
- Identifiera skillnader mellan TDD och funktionsdriven utveckling.
- Förstå hur man implementerar TDD i sitt eget programmeringsarbete.
- Förstå fördelarna med att använda TDD för att skapa robusta och pålitliga program.

## Vad är Testdriven Utveckling (TDD)?

Testdriven Utveckling (TDD) är ett programmeringsmönster där vi först skriver tester innan vi
skriver själva koden för att implementera den önskade funktionaliteten. Detta innebär att vi låter
testerna styra vår utvecklingsprocess. Genom att först definiera testkraven kan vi säkerställa att
vårt program fungerar korrekt innan vi går vidare till implementeringen. TDD hjälper oss att
fokusera på att skriva enkla och väl strukturerade metoder som löser specifika problem.

## Red-Green-Refactor Cykeln

TDD följer en specific cykel som kallas "Red-Green-Refactor":

1. **Red**: Skriv ett test som misslyckas (rött)
2. **Green**: Skriv minsta möjliga kod för att få testet att passera (grönt)
3. **Refactor**: Förbättra koden utan att ändra funktionaliteten

<div class="mermaid">

graph LR
    A[Red: Skriv test] --> B[Green: Implementera]
    B --> C[Refactor: Förbättra]
    C --> A

</div>

## Fördelar med TDD

TDD erbjuder många fördelar för programmerare. Några av de viktigaste fördelarna inkluderar:

1. **Säkerhet och Robusthet**: Genom att skriva tester innan du skriver själva koden kan du
säkerställa att ditt program fungerar korrekt och uppfyller kraven från början. Detta minskar risken
för buggar och fel i koden.

2. **Bättre Kodkvalitet**: TDD främjar utveckling av välstrukturerad och lättläslig kod. Testerna
fungerar som en slags dokumentation för hur koden ska användas och vilket beteende som förväntas.

3. **Förbättrad Utvecklingshastighet**: Trots att det kan verka som om TDD tar längre tid i början,
hjälper det dig att undvika senare tidsspill på felsökning och buggfixning.

4. **Ökad Tillförlitlighet**: Med TDD kan du vara säker på att ditt program kommer att fungera
korrekt, även när du gör ändringar eller lägger till ny funktionalitet.

5. **Regression Protection**: Testerna skyddar mot att tidigare fungerande funktionalitet går sönder
vid ändringar.

## Skillnaden mellan TDD och Funktionsdriven Utveckling

I funktionsdriven utveckling följer vi traditionell programmeringsmetodik, där vi skriver koden
först och sedan utvecklar tester för att kontrollera att koden fungerar som förväntat. Med TDD
vänder vi på denna ordning genom att först skriva testerna och sedan skriva koden som uppfyller
testfallen. På så sätt säkerställer vi att vår kod löser de problem som testerna har definierat.

## Hur man Implementerar TDD

Implementeringen av TDD kan delas in i följande steg:

1. **Skriv ett Test**: Definiera ett test som beskriver den funktionalitet du vill ha i ditt
program.

2. **Kör Testet**: Kör testet och se till att det misslyckas eftersom den nödvändiga funktionaliteten ännu inte är implementerad.

3. **Skriv Koden**: Skriv den minimala koden som krävs för att passera testet.

4. **Kör Testet Igen**: Kör testet igen och se till att det nu passerar.

5. **Refaktorera Koden**: Förbättra koden om det behövs utan att ändra dess funktionalitet.

6. **Upprepa Processen**: Upprepa dessa steg för varje ny funktion eller ändring.

## Exempelkod - Testdriven Utveckling i Praktiken

Låt oss titta på ett enkelt exempel där vi implementerar en funktion som beräknar summan av två tal
med hjälp av TDD:

### Steg 1: Skriv testet först (Red)

```csharp

using Xunit;

public class CalculatorTests
{
    [Fact]
    public void Add_TwoPositiveNumbers_ReturnsCorrectSum()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(2, 3);

        // Assert
        Assert.Equal(5, result);
    }
}

```

### Steg 2: Skapa minimal implementation (Green)

```csharp

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b; // Enklaste implementationen som får testet att passera
    }
}

```

### Steg 3: Lägg till fler tester

```csharp

public class CalculatorTests
{
    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(0, 0, 0)]
    [InlineData(-1, 1, 0)]
    [InlineData(-5, -3, -8)]
    public void Add_VariousInputs_ReturnsCorrectSum(int a, int b, int expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }
}

```

## Mer Avancerat Exempel - String Validator

Låt oss implementera en string-validator med TDD:

### Test först:

```csharp

public class StringValidatorTests
{
    [Fact]
    public void IsValidEmail_ValidEmail_ReturnsTrue()
    {
        // Arrange
        var validator = new StringValidator();

        // Act
        bool result = validator.IsValidEmail("test@example.com");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValidEmail_InvalidEmail_ReturnsFalse()
    {
        // Arrange
        var validator = new StringValidator();

        // Act
        bool result = validator.IsValidEmail("invalid-email");

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void IsValidEmail_EmptyOrNull_ReturnsFalse(string email)
    {
        // Arrange
        var validator = new StringValidator();

        // Act
        bool result = validator.IsValidEmail(email);

        // Assert
        Assert.False(result);
    }
}

```

### Implementation:

```csharp

using System.Text.RegularExpressions;

public class StringValidator
{
    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return emailRegex.IsMatch(email);
    }
}

```

## TDD Best Practices

1. **Skriv tester för edge cases**: Testa gränsvärden, null-värden och felaktiga inputs
2. **Håll tester enkla**: Ett test bör testa en sak åt gången
3. **Använd beskrivande testnamn**: Namnet ska tydligt beskriva vad som testas
4. **Skriv bara kod för att få testet att passera**: Undvik överkomplicerade lösningar
5. **Refaktorera regelbundet**: Håll både test- och produktionskod ren

## Vanliga Misstag i TDD

1. **Skriva för komplexa tester**: Håll testerna enkla och fokuserade
2. **Hoppa över refaktorering**: Detta leder till dålig kodkvalitet över tid
3. **Testa implementation istället för beteende**: Fokusera på vad koden ska göra, inte hur
4. **Glömma att köra tester**: Automatisera testkörning när det är möjligt

## Verktyg för TDD i C-Sharp

- **Testramverk**: xUnit, NUnit, MSTest
- **Mocking**: Moq, NSubstitute
- **Code Coverage**: Coverlet, dotCover
- **IDE-stöd**: Visual Studio Test Explorer, JetBrains Rider

## Slutsats

Testdriven Utveckling (TDD) är en kraftfull metod för att säkerställa att våra program fungerar
korrekt och är robusta. Genom att skriva testerna först får vi en tydlig bild av hur vår kod ska
fungera och vilket beteende som förväntas. Detta hjälper oss att skriva enkel och lättläslig kod som
är lätt att underhålla och utveckla vidare.

Nä alltså helt ärligt. Du kommer att skriva mindre kod när du vet att du enbart ska skapa kod som
uppfyller specifika kriterier. Detta är en av de största fördelarna med TDD.

TDD kan kännas konstigt i början, men när du väl har vant dig vid arbetssättet kommer du att märka
hur mycket tryggare och mer konfident du blir i din kod. Det är som att ha en säkerhetsväst när du
programmerar!

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
