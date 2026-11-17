# Träningsuppgifter: Fördjupad OOP — Vecka 2

> **Tema:** Designmönster  
> **Modul:** 02 — Designmönster, 04 — UML och planering

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är ett designmönster (design pattern)?

a. En färdig lösning på ett återkommande problem inom mjukvaruutveckling<br>b. En specifik kod som alltid ser likadan ut<br>c. Ett sätt att rita UML-diagram<br>d. Ett testverktyg

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En färdig lösning på ett återkommande problem inom mjukvaruutveckling

  **Förklaringar:**

  - ✅ **a) Återanvändbar lösning** - **RÄTT**: Designmönster är beprövade arkitekturlösningar — du behöver inte uppfinna hjulet igen. Andra utvecklare känner igen "ah, du använder Factory Pattern där"
  - ❌ **b) Identisk kod** - FEL: Mönstret beskriver *strukturen*, inte den exakta koden. Implementation varierar
  - ❌ **c) UML** - FEL: UML-diagram kan *beskriva* designmönster, men de är inte samma sak
  - ❌ **d) Testverktyg** - FEL: Designmönster handlar om arkitektur, inte testning
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad gör Singleton-mönstret?

a. Skapar en ny instans varje gång den anropas<br>b. Säkerställer att en klass bara har EN instans och ger en global åtkomstpunkt till den<br>c. Delar upp data i flera instanser<br>d. Skapar flera objekt samtidigt

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Säkerställer att en klass bara har EN instans och ger en global åtkomstpunkt till den

  **Förklaringar:**

  - ❌ **a) Ny instans varje gång** - FEL: Det är motsatsen — Singleton skapar bara en enda instans
  - ✅ **b) En instans för alla** - **RÄTT**: `MySingleton.Instance` — oavsett var du är i koden får du samma objekt. Används för loggning, konfiguration, connection pools
  - ❌ **c) Delar upp data** - FEL: Singleton samlar data, den delar inte upp den
  - ❌ **d) Flera samtidigt** - FEL: Singleton begränsar till en. Flera samtidigt kallas Pooling
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

När är Factory Pattern användbart?

a. När du vill skapa objekt utan att specificera den exakta klassen i förväg<br>b. När du bara har en typ av objekt<br>c. När du vill förstöra objekt<br>d. När du vill göra en fabrik

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** När du vill skapa objekt utan att specificera den exakta klassen i förväg

  **Förklaringar:**

  - ✅ **a) Skapa objekt utan att specificera klass** - **RÄTT**: `INotificationService service = NotificationFactory.Create("email")` — fabriken bestämmer om det blir EmailNotification eller SmsNotification. Anroparen behöver inte veta vilken konkret klass som skapas
  - ❌ **b) Bara en typ** - FEL: Factory är överkurs om du bara har en typ — använd `new` direkt
  - ❌ **c) Förstöra objekt** - FEL: Förstörning är inte Factorys jobb (det är Garbage Collector)
  - ❌ **d) Göra en fabrik** - FEL: Cirkelresonemang — Factory är mönstret, inte målet
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är Observer-mönstret?

a. Ett sätt att observera databasen<br>b. Ett mönster där ett objekt (subject) meddelar flera beroende objekt (observers) när dess tillstånd ändras<br>c. Ett sätt att logga händelser<br>d. En metod för övervakning

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett mönster där ett objekt (subject) meddelar flera beroende objekt (observers) när dess tillstånd ändras

  **Förklaringar:**

  - ❌ **a) Observera databasen** - FEL: Handlar om objekt-till-objekt-kommunikation, inte databas
  - ✅ **b) Subjekt meddelar observatörer** - **RÄTT**: "Prenumerationsmönstret" — en knapp (subject) meddelar alla prenumeranter när den klickas. I C# används events/delegates: `button.Click += OnButtonClick`
  - ❌ **c) Logga händelser** - FEL: Loggning kan *använda* Observer, men det är inte mönstret i sig
  - ❌ **d) Övervakning** - FEL: Teknisk övervakning är en annan sak
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad gör Strategy-mönstret?

a. Definierar en familj av algoritmer, gör dem utbytbara och låter algoritmen varieras oberoende av klienten<br>b. Väljer en slumpmässig strategi<br>c. Alltid använda samma algoritm<br>d. Kombinerar flera strategier

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Definierar en familj av algoritmer, gör dem utbytbara och låter algoritmen varieras oberoende av klienten

  **Förklaringar:**

  - ✅ **a) Utbytbara algoritmer** - **RÄTT**: `ISortStrategy` med `BubbleSort`, `QuickSort`, `MergeSort`. Du väljer strategi vid runtime: `sorter.Sort(data, new QuickSort())`. Klienten bryr sig inte om hur sorteringen fungerar
  - ❌ **b) Slumpmässig** - FEL: Strategi väljs medvetet, inte slumpmässigt
  - ❌ **c) Samma algoritm** - FEL: Poängen är just att kunna byta algoritm
  - ❌ **d) Kombinera** - FEL: Att kombinera flera är mer Decorator eller Composite
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är Decorator-mönstret bra för?

a. Att dekorera användargränssnittet<br>b. Att dynamiskt lägga till ansvar till ett objekt utan att ändra dess klass<br>c. Att måla om objekt<br>d. Att ta bort funktionalitet

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att dynamiskt lägga till ansvar till ett objekt utan att ändra dess klass

  **Förklaringar:**

  - ❌ **a) UI-dekoration** - FEL: Decorator handlar om att utöka beteende, inte utseende
  - ✅ **b) Lägga till ansvar dynamiskt** - **RÄTT**: Du har `ICoffee` och lägger på `MilkDecorator`, `SugarDecorator`, `WhippedCreamDecorator`. Varje decorator omsluter och utökar föregående. Bygger funktionalitet som legobitar
  - ❌ **c) Måla om** - FEL: Ingenting med grafik att göra
  - ❌ **d) Ta bort funktionalitet** - FEL: Decorator lägger TILL funktionalitet, den tar inte bort
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är Adapter-mönstret?

a. Ett mönster som gör att två inkompatibla gränssnitt kan fungera tillsammans<br>b. En elkontakt<br>c. Ett sätt att anpassa användargränssnittet<br>d. En metod för att konvertera data

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett mönster som gör att två inkompatibla gränssnitt kan fungera tillsammans

  **Förklaringar:**

  - ✅ **a) Koppla ihop inkompatibla gränssnitt** - **RÄTT**: Precis som en svensk resadapter låter dig koppla en EU-kontakt i ett brittiskt vägguttag. Du har `IXmlLogger` men behöver anropa `IJsonLogger` — Adaptern översätter anropen
  - ❌ **b) Elkontakt** - FEL: Analogin, inte mönstret
  - ❌ **c) Anpassa UI** - FEL: Adapter handlar om kodgränssnitt, inte användargränssnitt
  - ❌ **d) Datakonvertering** - FEL: Nära, men adapter handlar om *gränssnitt* snarare än dataformat
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad visar ett UML-klassdiagram?

a. Hur användaren interagerar med systemet<br>b. Tidsschemat för projektet<br>c. Klasser, deras attribut/metoder och relationer mellan dem<br>d. Databasens prestanda

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Klasser, deras attribut/metoder och relationer mellan dem

  **Förklaringar:**

  - ❌ **a) Användarinteraktion** - FEL: Det visar Use Case-diagram, inte klassdiagram
  - ❌ **b) Tidsschema** - FEL: Det är Gantt-schema, inte UML
  - ✅ **c) Klasser och relationer** - **RÄTT**: UML-klassdiagram visar: `User` med `+Id`, `+Name` och pil till `Order` ("1" → "0..*"). Det visar arv (`△`), associationer (→) och beroenden (--→)
  - ❌ **d) Prestanda** - FEL: UML är en design- och kommunikationsritning, inte ett prestandaverktyg
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad betyder "composition" i ett UML-diagram (fylld romb)?

a. En svag koppling där det ena objektet kan leva utan det andra<br>b. Ett starkt ägandeförhållande — om ägaren förstörs, förstörs också innehållet<br>c. Ett gränssnitt<br>d. Att klassen är abstrakt

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett starkt ägandeförhållande — om ägaren förstörs, förstörs också innehållet

  **Förklaringar:**

  - ❌ **a) Svag koppling** - FEL: Det är aggregation (ofylld romb), inte composition
  - ✅ **b) Starkt ägande** - **RÄTT**: Fylld romb = composition. Ett `House`-objekt äger sina `Room`-objekt — när huset förstörs finns inte rummen kvar. "Har-ett-och-äger-det"
  - ❌ **c) Interface** - FEL: Interface visas med `<<interface>>`-stereotyp
  - ❌ **d) Abstrakt** - FEL: Abstrakta klasser markeras med kursiv stil eller `{abstract}`
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad är ett "query scope" i Repository Pattern?

a. Databasens tidszon<br>b. Ett sätt att begränsa databasanslutningar<br>c. Ett sätt att återanvända filtreringslogik med Specification Pattern<br>d. En måttenhet för databasfrågor

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett sätt att återanvända filtreringslogik med Specification Pattern

  **Förklaringar:**

  - ❌ **a) Tidszon** - FEL: Ingenting med tid att göra
  - ❌ **b) Begränsa anslutningar** - FEL: Det är connection pooling
  - ✅ **c) Återanvänd filtrering** - **RÄTT**: `ISpecification<T>` med `Criteria`, `Includes`, `OrderBy`. Du definierar `new ProductsInCategorySpecification("Electronics")` och återanvänder den överallt. Undviker duplicering av `.Where().Include().OrderBy()`-kedjor
  - ❌ **d) Måttenhet** - FEL: Ingenting med mått att göra
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
