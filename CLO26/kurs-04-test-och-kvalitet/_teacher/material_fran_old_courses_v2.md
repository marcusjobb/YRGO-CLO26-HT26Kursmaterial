# Inventering av gammalt kursmaterial — CLO26 kurs-04 Test och Kvalitetssäkring

**Datum:** 2026-06-15
**Kurs:** kurs-04 — Test och Kvalitetssäkring (6 veckor)
**Syfte:** Kartlägga befintligt material i `/home/marcus/git/Old_courses/` inför ny kurs

---

## 1. BÖCKER — Kapitelkartläggning

### Bok 1: csharp_cmyh (Campus Mölndal YH-bok, C#)

**Sökväg:** `/home/marcus/git/Old_courses/books/csharp_cmyh/C-Sharp/`

Boken innehåller ett eget kapitel för kvalitetssäkring:

| Fil | Innehåll | Stödjer |
|-----|----------|---------|
| `quality_control/index.md` | Kvalitetssäkring — vad det är, TDD, xUnit, MSTest, NUnit, Moq, code coverage. Komplett, pedagogisk artikel med kodexempel (Measurements-klass), terminologilista. ⭐⭐⭐⭐ | Vecka 1 — intro |
| `quality_control/tdd.md` | (refererad i index.md) TDD-artikel |  Vecka 1-2 |
| `quality_control/code_reviews.md` | Kodgranskning i praktiken | Vecka 2, 6 |
| `qa/` | Sannolikt ytterligare QA-relaterat material (katalog ej fullständigt inventerad) | — |

**Bedömning:** quality_control/index.md kan användas direkt som läsmaterial för studerande vecka 1. Innehåller xUnit, MSTest, NUnit, Moq och coverage — allt i en fil. Behöver uppdateras till NSubstitute (Moq används som exempel). Boka kapitlet som läsning till lektion 1.

---

### Bok 2: JIN23 (Java, Campus Mölndal)

**Sökväg:** `/home/marcus/git/Old_courses/books/JIN23/aualityassurance/`

| Fil | Innehåll | Stödjer |
|-----|----------|---------|
| `tdd.md` | TDD-introduktion på svenska, Java-syntax men all teori är språkoberoende. Red-Green-Refactor-cykeln förklarad steg för steg. ⭐⭐⭐ | Vecka 1-2 |
| `budgetexempel.md` | Sannolikt Java-budget-kata | Vecka 1-2 |
| `redgreenblue.md` | Red-Green-Refactor-visualisering | Vecka 2 |
| `stringhelper.md` | String-validator-liknande kata i Java | Vecka 1 |
| `moreexemples.md` | Fler exempel | — |
| `index.md` | Kapitelöversikt | — |

**Bedömning:** Teoridelen i tdd.md är C#-oberoende och kan användas som kompletterande läsning. Java-kodexemplen kan INTE användas direkt — måste porteras. Rekommenderas som teoriunderlag, inte kodmall.

---

## 2. KATAS — Komplett lista

### Kata 1: Calculator (TDD) ⭐⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/kursplanering.md` (specifierad i kursplan) + `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/bdd_gherkin/calculator.feature`

**Domän:** Matematik (kalkylator)

**Klass/metodstruktur:**
```csharp
// Förväntat (från kursplanering):
class Calculator
{
    int Add(int a, int b)
    int Subtract(int a, int b)
    int Multiply(int a, int b)
    double Divide(int a, int b) // kräver division-by-zero-hantering
}
```

**Gherkin-feature finns:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/bdd_gherkin/calculator.feature`
```gherkin
Egenskap: Kalkylator
Scenario: Addera två positiva tal
  Givet att jag har talet 5
  Och jag har talet 3
  När jag adderar talen
  Så ska resultatet vara 8
```

**Rekommenderad vecka:** Vecka 1 (onsdag)
**Status:** Kan användas direkt — ingen källfil behövs, studerande bygger från scratch med TDD

---

### Kata 2: String Validator (TDD) ⭐⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/bdd_gherkin/EmailValidator.cs` + `EmailValidatorTests.cs`

**Domän:** Validering (email, telefon, lösenord)

**Klass/metodstruktur:**
```csharp
// EmailValidator.cs — KOMPLETT IMPLEMENTERAD
namespace BddDemo
{
    public class EmailValidator
    {
        public bool IsValid(string email)     // regex-baserad
        public string GetValidationError(string email)  // specifikt felmeddelande
    }
}
```

Även `ZodiacSign.cs` finns — alternativ domän (stjärntecken → datumlogik).

**Rekommenderad vecka:** Vecka 1 (onsdag eftermiddag)
**Status:** Kan användas direkt som facit/lärarexempel. EmailValidator.cs är ett rent och pedagogiskt exempel. ZodiacSign är utmärkt för att demonstrera hardcoding → riktig implementation i TDD.

---

### Kata 3: Shopping Cart (TDD med rabatter) ⭐⭐⭐⭐⭐

**Sökväg:** Specificerad i `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/kursplanering.md`, finns som Gherkin-exempel i `lectures/bdd_gherkin/presentation.md`

**Domän:** E-handel (kundvagn)

**Klass/metodstruktur:**
```csharp
// Förväntat (från kursplan):
class ShoppingCart
{
    void AddItem(Item item)
    void RemoveItem(Item item)
    decimal CalculateTotal()
    bool ApplyDiscount(string code)    // "SAVE10" → 10%
    // VG-nivå: membership discount, buy-X-get-Y
}

class Item { string Name; decimal Price; }
```

**Gherkin-scenario finns:**
```gherkin
Scenario: Rabattkod ger 10% rabatt
  Given en kundvagn med totalpris 1000 kr
  When användaren anger rabattkoden "SAVE10"
  Then ska totalpriset bli 900 kr
```

**Rekommenderad vecka:** Vecka 1 (torsdag)
**Status:** Kan byggas från scratch med TDD baserat på kursplanens specifikation.

---

### Kata 4: BankAccount (MSTest + DataRow) ⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2022/TDD/ConsolTDD/ConsolTDDTests/AccountTests.cs`

**Domän:** Bank

**Klass/metodstruktur:**
```csharp
// AccountTests.cs (MSTest)
class Account {
    double Balance { get; }
    void Deposit(double amount)
    void Withdraw(double amount)
}
// Tester: DataRow-parametrisering, negativa belopp, övertrassering
```

**Rekommenderad vecka:** Vecka 1 (som jämförelseexempel MSTest vs xUnit)
**Status:** Kan användas direkt. Äldre MSTest-syntax — men det är en poäng: bra för att jämföra MSTest/xUnit. Kräver ingen anpassning förutom att byta framework vid behov.

---

### Kata 5: BankAccount (2025, xUnit + NSubstitute, avancerad) ⭐⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/exercises/projects/BankAccount/`

**Domän:** Bank

**Klass/metodstruktur:**
```csharp
// BankAccount.cs — KOMPLETT IMPLEMENTERAD
public class BankAccount
{
    public string AccountNumber { get; private set; }  // 10 siffror
    public decimal Balance { get; protected set; }
    public virtual void Deposit(decimal amount)        // kastar exception om <= 0
    public virtual void Withdraw(decimal amount)       // kastar exception om overdraft
    public void Transfer(BankAccount targetAccount, decimal amount)
}

public class CreditAccount : BankAccount { ... }  // arv, övertrassering tillåten
public class User { ... }
```

**Rekommenderad vecka:** Vecka 2 (refactoring + arv)
**Status:** Komplett, väl dokumenterad produktionskod med tydliga XML-kommentarer på svenska. Utmärkt för integration tests och mocking av repository.

---

### Kata 6: MetricConverter (TDD-scaffolding, TODOs) ⭐⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/exercises/projects/MetricConverter/`

**Domän:** Enhetskonvertering (mm/cm/dm/m)

**Klass/metodstruktur:**
```csharp
// MetricConverter.cs — SKELETT med TODOs (perfekt för TDD-övning)
public class MetricConverter
{
    public double MillimetersToCentimeters(double millimeters)  // TODO
    public double MillimetersToDecimeters(double millimeters)   // TODO
    public double MillimetersToMeters(double millimeters)       // TODO
    public double CentimetersToMillimeters(double centimeters)  // TODO
    // ... 12 konverteringsmetoder totalt
}
```

**Rekommenderad vecka:** Vecka 1 (dag 1-2, klassisk TDD-kata)
**Status:** Perfekt TDD-scaffolding. Studerande får skelett med TODOs och skriver tester + implementering. Testerna i `MetricConverter.Tests/` är förmodligen tomma/ska skrivas av studeranden.

---

### Kata 7: GiftRegistry (Gherkin → xUnit) ⭐⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/exercises/gherkin_tdd_kata.md`

**Domän:** Presentregistry (gäster, önskemål, reservationer) — JULTEMAT (se sektion 5)

**Klass/metodstruktur:**
```csharp
// GiftRegistry — komplett facit finns i övningsfilen
public class GiftRegistry
{
    void AddWish(string guest, string name, decimal price)
    Result Reserve(string giftName, string buyerName)
    Wish? GetWish(string name)
    List<Wish> GetAllWishes()
    List<Wish> GetAvailableWishes()
    decimal GetBudgetFor(string buyerName)
}

public class Wish { string GuestName; string Name; decimal Price; bool IsReserved; string? ReservedBy; }
public class Result { bool IsSuccess; string ErrorMessage; }
```

**Gherkin-scenarios:** 5 kompletta scenarios med facit
**xUnit-tester:** 5 tester med facit

**Rekommenderad vecka:** Vecka 3 (BDD/Gherkin-veckan)
**Status:** Komplett övning med facit. Domänen är "gästlista-presenter" (inte jultemat, men relaterat). Kan användas direkt. Se sektion 5 för neutral ersättningsdomän.

---

### Kata 8: SantaDeliverySystem (Mocking + DI + async) ⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/exercises/projects/SantaDeliverySystem/`

**Domän:** Julklappsleverans — STARKT JULTEMAT (se sektion 5)

**Klass/metodstruktur:**
```csharp
// Services/SantaDeliveryService.cs — KOMPLETT IMPLEMENTERAD
public class SantaDeliveryService
{
    // Konstruktor: IChildDatabase, IWeatherService, IGiftFactory
    DeliveryMethod GetDeliveryMethod(WeatherForecast forecast)
    Task<DeliveryResult> DeliverGiftToChildAsync(Child child)
    Task<List<DeliveryResult>> DeliverGiftsToGoodChildrenAsync()
    List<Child> GetGoodChildrenByCountry(string country)
    decimal CalculateTotalGiftWeight(List<Child> children)
    bool IsSafeToDeliver(WeatherForecast forecast)
}

// Services/GiftFactory.cs — KOMPLETT IMPLEMENTERAD
public class GiftFactory : IGiftFactory
{
    Gift CreateGift(Child child)
    List<Gift> CreateGiftsForChildren(List<Child> children)
    bool HasGiftInStock(string giftName)
    int GetStockCount(string giftName)
}

// Interfaces: IChildDatabase, IWeatherService, IGiftFactory (alla mockbara)
```

**Tester:** 9 exempel-tester i `.Tests/` (5 för SantaDeliveryService, 4 för GiftFactory)
**Bedömning:** Utmärkt exempel för NSubstitute-mocking, DI, async/await, Theory/InlineData. 50 testförslag finns i README. **Juldomänen måste ersättas** för CLO26 (se sektion 5).

**Rekommenderad vecka:** Vecka 2 (mocking + DI)

---

### Kata 9: ChristmasWishlistAPI (Integration tests + NSubstitute) ⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/exercises/projects/ChristmasWishlistAPI/`

**Domän:** Jul-önskelista API — STARKT JULTEMAT (se sektion 5)

**Klass/metodstruktur:**
```csharp
// WishlistService.cs
public class WishlistService
{
    WishlistService(IWishlistRepository repo)
    Task<Wishlist> GetWishlistAsync(int id)
    Task<decimal> CalculateTotalPriceAsync(int id)
}

// WishlistRepository.cs — med EF Core WishlistContext (InMemory)
// Models: Wishlist { Id, ChildName, List<Gift> }, Gift { Id, Name, Price }
// Interfaces: IWishlistRepository
```

**Tester (WishlistServiceTests.cs):**
```csharp
// NSubstitute — 3 tester:
GetWishlist_ExistingId_ReturnsWishlist()
CalculateTotalPrice_ThreeGifts_ReturnsSum()
Constructor_NullRepository_ThrowsException()
```

**Rekommenderad vecka:** Vecka 2-3 (integration tests + InMemory DB)
**Status:** Komplett med NSubstitute, async, Repository pattern, InMemory EF Core. Juldomänen måste ersättas.

---

### Kata 10: ZodiacSign (BDD + Theory) ⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/bdd_gherkin/ZodiacSign.cs` + `ZodiacSignTests.cs`

**Domän:** Stjärntecken (datumlogik)

**Klass/metodstruktur:**
```csharp
// ZodiacSign.cs — KOMPLETT IMPLEMENTERAD
public class ZodiacSign
{
    public string GetSign(DateTime birthDate)
    // Returnerar: Väduren, Oxen, Tvillingarna, Kräftan, Lejonet,
    //             Jungfrun, Vågen, Skorpionen, Skytten, Stenbocken,
    //             Vattumannen, Fiskarna
}
```

**Zodiac.feature finns:**
```gherkin
Scenario: Född i mars är Fiskarna
  Givet att jag är född den 10 mars
  När jag frågar efter mitt stjärntecken
  Så ska jag få svaret "Fiskarna"
```

**Rekommenderad vecka:** Vecka 3 (BDD + Theory/InlineData — 12 tecken = 12 [InlineData])
**Status:** Kan användas direkt. Domän är neutral. Utmärkt för att visa [Theory] + [InlineData].

---

### Kata 11: Moq/NSubstitute BankAccount (2022) ⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2022/TDD/MoqDemo/`

**Domän:** Bank

**Klass/metodstruktur:**
```csharp
// Interface-baserat mocking-exempel (Moq)
interface IBankAccount { double Balance; void Deposit(double amount); void Withdraw(double amount); }
interface INameList { ... }

// BankAccountTests.cs: Moq.Setup, moq.Object, TestInitialize
// NameListTests.cs: mockar INameList
```

**Rekommenderad vecka:** Vecka 2 (mocking-intro, NSubstitute jämförelse)
**Status:** Äldre Moq-syntax. Kan visas som "så här ser Moq ut" innan man introducerar NSubstitute. Historiskt och pedagogiskt värde, används INTE som övningsuppgift.

---

## 3. PLAYWRIGHT / E2E

**Sökning utförd:** `.md`, `.cs`, `.txt` med playwright/e2e/end.to.end/selenium/cypress

**Resultat:**

Playwright nämns på **tre ställen** i 2025-materialet:

1. `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/kursplanering.md` — Playwright listad som "Valfritt (bonus)" i teknisk stack, med länk till `playwright.dev/dotnet/docs/intro`.

2. `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/security_testing/security_testing_marp.md` — En Playwright-kodslide (demo-kod):
```csharp
[Test]
public async Task UserCanLoginAndViewAccounts()
{
    await Page.GotoAsync("http://localhost:5000");
    await Page.FillAsync("#username", "testuser");
    await Page.FillAsync("#password", "password123");
    await Page.ClickAsync("button[type='submit']");
    await Expect(Page).ToHaveURLAsync(".*accounts");
    await Expect(Page.Locator("h1")).ToContainTextAsync("My Accounts");
}
```

3. `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/quiz/e2e_training.md` — Quiz om E2E.

**Ingen befintlig Playwright-övning eller komplett exempelprojekt hittades.**

**Slutsats:** Playwright måste byggas **från grunden** för CLO26. Det finns dokumentation av vad som ska visa (demo-kod i security_testing_marp.md är en bra startpunkt) och quiz-frågor, men inga övningsuppgifter. Räkna med 3-4 timmars arbete för att skapa en komplett Playwright-lektion med övning.

---

## 4. SPECFLOW / GHERKIN / BDD

**Sökning utförd:** `.md`, `.cs`, `.feature`

**Hittades:**

### .feature-filer (3 stycken) ⭐⭐⭐⭐⭐

| Fil | Innehåll |
|-----|----------|
| `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/bdd_gherkin/calculator.feature` | Kalkylator, 2 scenarios, svenska (Givet/När/Så) |
| `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/bdd_gherkin/email.feature` | Email-validator, Gherkin-scenarios |
| `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/bdd_gherkin/zodiac.feature` | Stjärntecken, 1 scenario |

### SpecFlow-teori (C#) ⭐⭐⭐⭐

**Fil:** `/home/marcus/git/Old_courses/2024/csharp/4_test/lectures/08_user_story_and_acceptance_criteria/3_automatisering_av_acceptanstester_med_specflow.md`

Komplett teoriartikel: vad SpecFlow är, workflow-diagram (Mermaid), Step Definitions, Hooks, best practices. Skriven för CLO24. Kan användas direkt med minimal anpassning.

### Marp-presentation BDD/Gherkin ⭐⭐⭐⭐⭐

**Fil:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/bdd_gherkin/presentation.md`

Komplett Marp-presentation: BDD vs traditionell, Gherkin-syntax, Given-When-Then, kopplingen AAA↔GWT, shopping cart-exempel, fördelar, flöde. 15+ slides. Direkt användbar.

### Livekodnings-guide BDD ⭐⭐⭐⭐⭐

**Fil:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/bdd_gherkin/LIVEKODNING_CSHARP.md`

Detaljerad guide för ZodiacSign-demot: setup, ordning (1. visa feature, 2. skriv test, 3. hardcoda, 4. implementera, 5. Theory), kommandon, tips. Komplett livekodningsunderlag.

**Bedömning:** BDD/Gherkin-materialet är det starkaste i hela inventarieset. Presentation + .feature-filer + livekodningsguide + gherkin_tdd_kata.md + SpecFlow-teoriartikel = komplett lektion. Kan räddas i sin helhet, men GiftRegistry-domänen har julkoppling (se sektion 5).

---

## 5. JULTEMAT-FILER

Följande filer har tydlig jul/christmas-koppling och **behöver bytas domän** inför CLO26:

### Fil 1: SantaDeliverySystem (övningsprojekt) ⭐⭐⭐⭐ (kod) / problem (domän)

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/exercises/projects/SantaDeliverySystem/`

**Klassstruktur:** SantaDeliveryService, GiftFactory, IChildDatabase, IWeatherService, IGiftFactory, Child, Gift, WeatherForecast, DeliveryResult, DeliveryMethod (enum), WeatherCondition (enum)

**Julkoppling:** Santa, Tomte, renar, julklappar, "snälla barn/stygga barn", "Coal", julklappsleverans.

**Förslag på neutral ersättningsdomän:** DroneDeliverySystem
- Drone → leveransdrönare
- Child → Customer (kund)
- Gift → Package (paket)
- GiftFactory → PackageFactory
- Coal → "Standard Package" / Avvisad leverans
- WeatherCondition behålls (neutral)
- Snälla/stygga barn → premiumkund/spärrad kund

Alternativt: **LibraryDeliverySystem** (bibliotekets hemleverans)
- Child → BorrowingCard (lånekort)
- Gift → Book
- Coal → LateReturnFee
- WeatherService behålls

---

### Fil 2: ChristmasWishlistAPI (övningsprojekt) ⭐⭐⭐⭐ (kod) / problem (domän)

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/exercises/projects/ChristmasWishlistAPI/`

**Klassstruktur:** WishlistService, WishlistRepository, WishlistContext (EF Core), Wishlist { Id, ChildName, List<Gift> }, Gift { Id, Name, Price }, IWishlistRepository

**Julkoppling:** Christmas, ChildName (barnets namn), Gift.

**Förslag på neutral ersättningsdomän:** ProductWishlistAPI (e-handel)
- Wishlist → WishList (önskelista)
- ChildName → UserName
- Gift → Product { Id, Name, Price }
- WishlistContext → WishListContext (EF Core identiskt)

Alternativt: **BookReservationAPI** (bibliotek)
- Wishlist → ReadingList
- ChildName → MemberName
- Gift → Book { Id, Title, Price }

---

### Fil 3: GiftRegistry kata ⭐⭐⭐⭐⭐ (pedagogik) / mild julkoppling

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/exercises/gherkin_tdd_kata.md`

**Julkoppling:** "present", "julklapp" (implicit — "gift registry" för gäster).

**Bedömning:** Domänen är egentligen en bröllops/födelsedag-gästlista, inte specifikt jul. Rubriken "Gift Registry" är neutral i sig. Kan behållas eller byta till "EventRegistry" / "WeddingRegistry".

---

### Fil 4: SantaDeliverySystem README ⭐⭐⭐ (instruktioner bra) / stark julkoppling

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/exercises/projects/SantaDeliverySystem/README.md`

50 testförslag, G/VG-kriterier, kodexempel för Theory/async/Received(). Kan återanvändas till fullo efter domänbyte. Strukturen (G: 30 tester, 60% coverage; VG: 50 tester, 80%, Theory, async, integration) är utmärkt för CLO26.

---

## 6. OWASP / SECURITY

**Hittades:**

### OWASP Quick Reference ⭐⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/security_testing/OWASP_TOP_10_QUICK_REFERENCE.md`

Komplett snabbreferens för alla 10 OWASP Top 10 sårbarheter. Förklarad på svenska med kodexempel (SQL injection, XSS). Pedagogisk, lämplig för studerande. **Kan användas direkt utan ändringar.**

### Security Testing Marp-presentation ⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/lectures/security_testing/security_testing_marp.md`

Komplett Marp-presentation: OWASP Top 10-lista, SQL Injection demo (sårbar kod vs säker EF Core-kod), Division by zero, felberäkningar, XSS-demo, Playwright E2E-kodexempel, Security Quest-övning (Monster Arena), async/await-snabbgenomgång. **Kan användas direkt.**

Innehåller en komplett lektionsstruktur med:
- Del 1: Security Quest — studerande "hackar" en sårbar app (VulnerableBank.sln)
- Del 2: Monster Arena — bygger ett system och testar varandras kod i grupp-rotation

**Obs:** `lectures/security_testing/demo_vulnerable_code/VulnerableBank.sln` **refereras** i presentationen men hittades INTE i inventariet. Kontrollera om den existerar eller om den måste skapas.

### OWASP Quiz ⭐⭐⭐⭐

**Sökväg:** `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/quiz/owasp_training.md`

Quiz-frågor för OWASP. Kan användas direkt i CLO26.

### Java Security (referensmaterial, EJ användbar direkt) ⭐⭐

**Sökvägar:**
- `/home/marcus/git/Old_courses/2023/java/apiwebservices/lecture2/Lecture01-securitythreats.md`
- `/home/marcus/git/Old_courses/2023/java/apiwebservices/lecture1/lecture-securitymechanism.md`
- `/home/marcus/git/Old_courses/2024/java/7_system_integration/14_secutity_testing/` (hel mapp med tester, marp, övningar)

Java/Spring Security — kan ge idéer men kan INTE användas direkt.

**Slutsats:** OWASP-materialet från 2025 är komplett och direkt användbart. VulnerableBank.sln (sårbar app) måste verifieras/skapas.

---

## 7. JINTDDPROJECT

**Sökväg:** `/home/marcus/git/Old_courses/JinTDDProject/`

**Vad är det:**
Ett Java-projekt (IntelliJ IDEA, Maven) med JUnit Jupiter 5. Troligen en gammal inlämningsuppgift eller demo från JIN-kursen (Java-studenter, inte YH C#).

**Filinnehåll:**
- `TDDproject/src/main/java/org/campusmolndal/Main.java` — Tom main-klass
- `TDDproject/src/test/java/org/campusmolndal/MainTest.java` — Tom testklass (`class MainTest {}`)
- `README.md` — Standard GitHub-mall (tom, ej ifylld)
- `ertNamnDokumentation.md` — Namnmall-doc

**Bedömning:** JinTDDProject är ett **tomt Java-skelett** med IntelliJ-konfiguration. Ingen implementering finns. Projektstrukturen visar hur ett TDD-projekt kan scaffoldas i Java, men innehållet är minimalt och irrelevant för C#-kursen CLO26.

**Kan användas:** NEJ — Java-projekt, tom implementation, inte relevant för CLO26.

---

## 8. LUCKOR — Ämnen som saknas och måste byggas från grunden

### Hög prioritet (krävs för kursens kärnmål)

| Ämne | Veckotillhörighet | Uppskattad byggtid |
|------|-------------------|--------------------|
| **Playwright E2E-övning** (komplett projekt med övningsuppgift) | Vecka 4 | 4 tim |
| **VulnerableBank.sln** (sårbar app för Security Quest — refereras i security_testing_marp.md men saknas) | Vecka 4 | 3 tim |
| **GitHub Actions CI/CD-övning** (YAML-fil + guide) | Vecka 3 | 2 tim |
| **Integration tests med InMemory EF Core** (separat lektion — finns teori men inte C#-övning) | Vecka 2 | 2 tim |
| **Domänbyte SantaDeliverySystem → neutral domän** | Vecka 2 | 1 tim |
| **Domänbyte ChristmasWishlistAPI → neutral domän** | Vecka 2-3 | 1 tim |

### Medelhög prioritet (förbättrar kursen)

| Ämne | Veckotillhörighet | Uppskattad byggtid |
|------|-------------------|--------------------|
| **SpecFlow-övning med .feature-fil och Step Definitions** (teori finns, ingen övning) | Vecka 3 | 2 tim |
| **Flaky tests-demo** (nämns i kursplan men inget material finns) | Vecka 4 | 1 tim |
| **Marp-slides för Integration Tests** (README finns, slides saknas) | Vecka 2 | 1 tim |

### Låg prioritet (kan improviseras)

| Ämne | Notering |
|------|----------|
| **Theory/InlineData-lektion** | ZodiacSign + kursplanens beskrivning räcker |
| **async/await-testning** | security_testing_marp.md har kodexempel |
| **Repository Pattern-lektion** | csharp_cmyh-boken + kursplan räcker |

---

## Sammanfattning — Material per vecka

| Vecka | Ämne | Material som kan återanvändas | Måste byggas |
|-------|------|-------------------------------|--------------|
| **1** | xUnit, AAA, Calculator/String/ShoppingCart | kursplanering.md (klar), aaa_pattern_marp.md (klar), calculator.feature, EmailValidator.cs, MetricConverter-skelett, csharp_cmyh quality_control | — |
| **2** | Refactoring, Red-Green-Refactor, Mocking, DI, Integration Tests | BankAccount-projekt (komplett), SantaDeliverySystem (ny domän behövs), MoqDemo (referens), security_testing_marp.md (async-bit) | Domänbyte SantaDeliverySystem, InMemory EF Core-övning |
| **3** | SCRUM, User Stories, Gherkin/BDD, SpecFlow, GitHub Actions | presentation.md (BDD), LIVEKODNING_CSHARP.md, gherkin_tdd_kata.md, 3_automatisering_av_acceptanstester_med_specflow.md (teori), scrum_intro-mapp | SpecFlow-övning, GitHub Actions-guide |
| **4** | Theory/InlineData, async, flaky, OWASP, security, E2E Playwright | security_testing_marp.md (OWASP), OWASP_TOP_10_QUICK_REFERENCE.md, owasp_training.md (quiz) | Playwright-övning, VulnerableBank.sln |
| **5-6** | CI/CD, projektarbete, demo, tenta | Kursplanens projektspecifikation, tentafrågor (exam/) | — |

---

_Rapport skapad 2026-06-15 av Claude (nionit). Granskas och godkänns av Marcus Ackre Medina._
