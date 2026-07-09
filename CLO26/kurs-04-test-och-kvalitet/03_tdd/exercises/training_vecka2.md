# Träningsuppgifter: Test & Kvalitet — Vecka 2

> **Tema:** Unit-testning och TDD  
> **Modul:** 02 — Unit-testning, 03 — TDD

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad står TDD för?

a. Test-Driven Development — testdriven utveckling, där du skriver testet före koden<br>b. Technical Design Document<br>c. Test-Deploy-Delete<br>d. Total Debugging Day

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Test-Driven Development — testdriven utveckling, där du skriver testet före koden

  **Förklaringar:**

  - ✅ **a) Test-Driven Development** - **RÄTT**: TDD vänder på traditionell utveckling: skriv testet FÖRST, sen koden som får testet att passera
  - ❌ **b) Technical Design Document** - FEL: TDD är en utvecklingsmetodik, inte ett dokument
  - ❌ **c) Test-Deploy-Delete** - FEL: Hittar på egna betydelser
  - ❌ **d) Total Debugging Day** - FEL: Inte en officiell term
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vilka är stegen i TDD-cykeln?

a. Kod → Testa → Debugga → Upprepa<br>b. Red → Green → Refactor → Upprepa<br>c. Plan → Kod → Test → Leverera<br>d. Design → Implementera → Testa → Dokumentera

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Red → Green → Refactor → Upprepa

  **Förklaringar:**

  - ❌ **a) Kod först, test sen** - FEL: Det är traditionell utveckling, inte TDD
  - ✅ **b) Red-Green-Refactor** - **RÄTT**: 🔴 RED = skriv test som misslyckas. 🟢 GREEN = skriv minsta kod som får testet att passera. ♻️ REFACTOR = förbättra koden medan testen fortfarande är gröna
  - ❌ **c) Vattenfallsmodellen** - FEL: Det är traditionell projektplanering, inte TDD
  - ❌ **d) Dokumentera först** - FEL: TDD dokumenterar genom tester
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är syftet med steg 1 (🔴 RED) i TDD?

a. Att skriva ett test som kompilerar och är grönt direkt<br>b. Att skriva ett test som misslyckas — antingen för att koden inte finns eller för att logiken är fel<br>c. Att skriva all produktionskod först<br>d. Att köra alla tester

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att skriva ett test som misslyckas — antingen för att koden inte finns eller för att logiken är fel

  **Förklaringar:**

  - ❌ **a) Grönt direkt** - FEL: RED betyder att testet SKA misslyckas — det bevisar att testet testar något
  - ✅ **b) Test som misslyckas** - **RÄTT**: `Calculator.Add(2, 3)` — kraschar först (Calculator finns inte). Detta är OK! Misslyckandet bevisar att testet faktiskt testar något
  - ❌ **c) All produktionskod först** - FEL: I TDD skriver du produktionskod FÖRST i steg 2 (GREEN)
  - ❌ **d) Köra alla tester** - FEL: I RED kör du bara ett nytt test som ska misslyckas
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är målet i steg 2 (🟢 GREEN) i TDD?

a. Att skriva perfekt, optimerad kod<br>b. Att skriva MINSTA möjliga kod som får testet att passera — även om det är hårdkodat<br>c. Att skriva alla metoder på en gång<br>d. Att refaktorera koden

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att skriva MINSTA möjliga kod som får testet att passera — även om det är hårdkodat

  **Förklaringar:**

  - ❌ **a) Perfekt kod** - FEL: Det kommer i REFACTOR-steget. GREEN är bara att få det att fungera
  - ✅ **b) Minsta möjliga kod** - **RÄTT**: `public int Add(int a, int b) { return 5; }` — ja, det är hårdkodat! Men testet blir grönt. Nästa test tvingar fram riktig logik
  - ❌ **c) Allt på en gång** - FEL: TDD är inkrementellt — en sak i taget
  - ❌ **d) Refactor** - FEL: Det är steg 3, inte 2
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad innebär AAA i ett test?

a. A, A, A — enkelt att komma ihåg<br>b. Arrange, Act, Assert — förbered, utför, verifiera<br>c. Add, Activate, Assert<br>d. Alla som arbetar med test

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Arrange, Act, Assert — förbered, utför, verifiera

  **Förklaringar:**

  - ❌ **a) Enkelt att minnas** - FEL: Bokstäverna är bara första bokstaven
  - ✅ **b) Arrange-Act-Assert** - **RÄTT**: 
    - **Arrange**: Skapa objekt och förbered (var calculator = new Calculator())
    - **Act**: Utför operationen (var result = calculator.Add(2, 3))
    - **Assert**: Verifiera resultatet (Assert.Equal(5, result))
  - ❌ **c) Add-Activate** - FEL: Inte standard-terminologi
  - ❌ **d) Alla som arbetar** - FEL: Ingenting med personal att göra
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är skillnaden mellan `[Fact]` och `[Theory]` i xUnit?

a. `[Fact]` är för tester utan parametrar, `[Theory]` gör att du kan köra samma test med olika indata via `[InlineData]`<br>b. De är samma sak<br>c. `[Theory]` är för enhetstester, `[Fact]` för integrationstester<br>d. `[Fact]` är snabbare än `[Theory]`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `[Fact]` är för tester utan parametrar, `[Theory]` gör att du kan köra samma test med olika indata via `[InlineData]`

  **Förklaringar:**

  - ✅ **a) Fact vs Theory** - **RÄTT**: `[Fact] public void Add_Works()` — ett test, inga parametrar. `[Theory] [InlineData(2, 3, 5)] [InlineData(10, 5, 15)] public void Add_Works(int a, int b, int expected)` — KÖRS TVÅ GÅNGER med olika data
  - ❌ **b) Samma sak** - FEL: Fact = enkeltest, Theory = datadrivet test
  - ❌ **c) Enhet vs integration** - FEL: Båda används för enhetstester
  - ❌ **d) Snabbare** - FEL: Ingen prestandaskillnad
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är en av fördelarna med TDD?

a. Du skriver mindre kod totalt<br>b. Du får en testad design och trygghet att refaktorera — testerna fångar regressioner<br>c. Du slipper skriva dokumentation<br>d. Du behöver inte planera

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Du får en testad design och trygghet att refaktorera — testerna fångar regressioner

  **Förklaringar:**

  - ❌ **a) Mindre kod** - FEL: TDD innebär ofta MER kod (test + produktion)
  - ✅ **b) Trygg refactoring** - **RÄTT**: Tester = säkerhetsnät. Du kan byta implementation, dela upp metoder, byta namn — testerna säger till om något går sönder. Utan tester är refactoring risky
  - ❌ **c) Slipper dokumentation** - FEL: Testerna är dokumentation, men du kan behöva annan dokumentation också
  - ❌ **d) Behöver inte planera** - FEL: TDD kräver STÖRRE eftertanke — du måste veta vad koden SKA göra innan du skriver den
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är ett bra namn på ett test i xUnit?

a. `Test1()`<br>b. `MethodName_Scenario_ExpectedResult` — t.ex. `Add_TwoNumbers_ReturnsSum`<br>c. `AddTest()`<br>d. `MyTest()`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `MethodName_Scenario_ExpectedResult` — t.ex. `Add_TwoNumbers_ReturnsSum`

  **Förklaringar:**

  - ❌ **a) Test1** - FEL: Säger ingenting om vad som testas
  - ✅ **b) Metod_Scenario_Förväntat** - **RÄTT**: När testet misslyckas ser du direkt: `Add_TwoNumbers_ReturnsSum` — FAILED. Du vet exakt vilken metod, vilket scenario och vad som förväntades
  - ❌ **c) AddTest** - FEL: Bättre, men säger inte VAD den testar (summa? negativa tal? noll?)
  - ❌ **d) MyTest** - FEL: Otydligt och inte standard
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad gör `Assert.Equal(5, result)`?

a. Kollar att 5 är lika med 5 — alltid sant<br>b. Verifierar att result är lika med 5 — om inte, misslyckas testet<br>c. Sätter result till 5<br>d. Skriver ut 5

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Verifierar att result är lika med 5 — om inte, misslyckas testet

  **Förklaringar:**

  - ❌ **a) Alltid sant** - FEL: `Assert.Equal(5, result)` jämför med VARIABELN result, inte med konstanten 5
  - ✅ **b) Verifierar likhet** - **RÄTT**: Första parametern = förväntat värde (expected). Andra = faktiska (actual). Om result är 3 blir testet rött med meddelandet: "Expected: 5, Actual: 3"
  - ❌ **c) Sätter värde** - FEL: Assert ändrar inget — det bara kontrollerar
  - ❌ **d) Skriver ut** - FEL: Assert.Equal skriver inte ut, det kastar undantag om det misslyckas
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad är en testmetod utan AAA som bara testar det allra enklaste?

a. Ett "smoke test"<br>b. Ett "baby step" — minsta möjliga test för att starta TDD-cykeln<br>c. Ett integrationstest<br>d. Ett prestandatest

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett "baby step" — minsta möjliga test för att starta TDD-cykeln

  **Förklaringar:**

  - ❌ **a) Smoke test** - FEL: Smoke test kollar att systemet startar, inte specifik logik
  - ✅ **b) Baby step** - **RÄTT**: I TDD börjar du med det enklaste möjliga testet: `Add(0, 0) == 0`. När det är grönt lägger du till ett till: `Add(1, 1) == 2`. Små steg = säkra steg
  - ❌ **c) Integrationstest** - FEL: Ett baby step är ett enhetstest, inte integrationstest
  - ❌ **d) Prestandatest** - FEL: Baby step handlar om att validera logik, inte prestanda
</details>

<div style="text-align: container; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
