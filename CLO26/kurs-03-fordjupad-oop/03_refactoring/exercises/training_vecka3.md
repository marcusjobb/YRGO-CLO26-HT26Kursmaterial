# Träningsuppgifter: Fördjupad OOP — Vecka 3

> **Tema:** Refactoring och kodgranskning  
> **Modul:** 03 — Refactoring

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad står SOLID för?

a. Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion<br>b. Simple Objects, Large Interfaces, Data<br>c. Sort, Order, Link, Insert, Delete<br>d. Static Object Language Integrated Design

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion

  **Förklaringar:**

  - ✅ **a) SOLID-principerna** - **RÄTT**: Fem principer för objektorienterad design som gör koden mer underhållbar, testbar och flexibel
  - ❌ **b) Simple Objects** - FEL: Hittar på egna betydelser
  - ❌ **c) CRUD-operationer** - FEL: Det är SQL-operationer, inte SOLID
  - ❌ **d) Static Object...** - FEL: Helt påhittat
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad innebär Single Responsibility Principle (SRP)?

a. En klass ska göra allt så att koden är på ett ställe<br>b. En klass ska bara ha ETT ansvar — en enda anledning att ändras<br>c. En klass ska bara användas en gång<br>d. En metod ska bara ha en rad kod

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En klass ska bara ha ETT ansvar — en enda anledning att ändras

  **Förklaringar:**

  - ❌ **a) Göra allt** - FEL: Det är motsatsen — SRP säger TVÄRTOM: dela upp!
  - ✅ **b) Ett ansvar** - **RÄTT**: `User`-klassen ska bara representera user-data. `UserRepository` sparar i databasen. `EmailService` skickar mail. `UserValidator` validerar. När mail-API:t ändras påverkas bara EmailService, inte User
  - ❌ **c) Användas en gång** - FEL: Handlar om ansvar, inte användningsfrekvens
  - ❌ **d) En rad kod** - FEL: Metoder kan vara flera rader långa — de ska bara göra EN sak
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad betyder Open/Closed Principle (OCP)?

a. Klasser ska vara öppna för alla att ändra<br>b. Klasser ska vara stängda för all utökning<br>c. Klasser ska vara öppna för utökning men stängda för modifiering<br>d. Klasser ska varken vara öppna eller stängda

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Klasser ska vara öppna för utökning men stängda för modifiering

  **Förklaringar:**

  - ❌ **a) Alla får ändra** - FEL: Då går det inte att lita på klassens beteende
  - ❌ **b) Stängd för utökning** - FEL: Då kan du inte lägga till ny funktionalitet
  - ✅ **c) Utöka utan att ändra** - **RÄTT**: Lägg till ny rabatt med en NY klass `StudentDiscount : IDiscountStrategy` istället för att ändra i `DiscountCalculator` med en ny `if`. Ny funktionalitet = ny fil, inte ändring i gammal
  - ❌ **d) Varken eller** - FEL: OCP är tydlig — öppen för utökning, stängd för modifiering
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad innebär Liskov Substitution Principle (LSP)?

a. En härledd klass ska kunna ersätta sin basklass utan att programmet går sönder<br>b. Subklasser ska alltid vara mindre än basklasser<br>c. Alla klasser måste ha samma gränssnitt<br>d. Subklasser ska aldrig användas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En härledd klass ska kunna ersätta sin basklass utan att programmet går sönder

  **Förklaringar:**

  - ✅ **a) Ersätta basklass** - **RÄTT**: Om du har `Shape shape = new Rectangle()` ska allt fungera. Och om du byter till `Shape shape = new Square()` ska det FORTFARANDE fungera. Om Square måste kasta ett undantag i en metod från Shape — då bryts LSP
  - ❌ **b) Mindre storlek** - FEL: Handlar om beteende, inte storlek
  - ❌ **c) Samma gränssnitt** - FEL: Subklasser kan utöka, de ska bara inte bryta basklassens kontrakt
  - ❌ **d) Aldrig användas** - FEL: Arv är grunden i OOP — LSP säger BARA att det ska fungera korrekt
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad betyder Interface Segregation Principle (ISP)?

a. Stora, feta interfaces med allt på en gång<br>b. Små, specifika interfaces istället för ett stort generellt<br>c. Interfaces ska inte användas<br>d. Alla klasser måste implementera samma interface

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Små, specifika interfaces istället för ett stort generellt

  **Förklaringar:**

  - ❌ **a) Stora interfaces** - FEL: Det är det ISP säger att du INTE ska göra
  - ✅ **b) Små, specifika interfaces** - **RÄTT**: `IWorkable { void Work(); }`, `IFeedable { void Eat(); }`, `ISleepable { void Sleep(); }` istället för `IWorker { void Work(); void Eat(); void Sleep(); }`. En Robot är `IWorkable`, en Human är alla tre. Inga påtvingade onödiga implementationer
  - ❌ **c) Inte använda interfaces** - FEL: ISP säger hur du ska designa interfaces, inte att du ska undvika dem
  - ❌ **d) Alla samma** - FEL: Tvärtom — olika klasser ska ha olika interfaces
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad innebär Dependency Inversion Principle (DIP)?

a. Beroenden ska peka på konkreta klasser, inte abstraktioner<br>b. Hög-nivå moduler ska inte bero på låg-nivå moduler — båda ska bero på abstraktioner<br>c. All kod ska vara i en fil<br>d. Beroenden ska undvikas helt

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Hög-nivå moduler ska inte bero på låg-nivå moduler — båda ska bero på abstraktioner

  **Förklaringar:**

  - ❌ **a) Konkreta klasser** - FEL: DIP säger TVÄRTOM — bero på abstraktioner (interfaces)
  - ✅ **b) Abstraktioner, inte konkreta** - **RÄTT**: `UserService` beror på `IUserRepository` (interface), inte på `SqlUserRepository` (konkret klass). Du kan byta databas, mocka i tester, och ändra lågnivådetaljer utan att påverka högnivålogik
  - ❌ **c) Allt i en fil** - FEL: Det är motsatsen till DIP
  - ❌ **d) Undvik beroenden** - FEL: Beroenden är oundvikliga — DIP handlar om HUR du hanterar dem
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är ett "magiskt tal" (magic number) i kod?

a. Ett tal som är svårt att räkna ut<br>b. Ett hårdkodat numeriskt värde utan förklaring — som 86400 istället för `SecondsPerDay`<br>c. Ett slumpmässigt tal<br>d. Ett tal som är större än 1000

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett hårdkodat numeriskt värde utan förklaring

  **Förklaringar:**

  - ❌ **a) Svårt tal** - FEL: Handlar om kodläsbarhet, inte matematiksvårighet
  - ✅ **b) Hårdkodat utan förklaring** - **RÄTT**: `if (age > 65)` vs `if (age > RetirementAge)`. Magic numbers gör koden svårläst och svår att ändra — varför just 65? Använd konstanter: `const int RetirementAge = 65`
  - ❌ **c) Slumpmässigt** - FEL: Ingenting med slump att göra
  - ❌ **d) Större än 1000** - FEL: Ett magiskt tal kan vara 1, 7, 24, 3600 — alla storlekar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är en bra tumregel för metodlängd enligt Clean Code?

a. En metod ska vara max 3 rader<br>b. Om en metod är större än halva skärmen är den för lång<br>c. Metoder ska alltid vara minst 20 rader<br>d. Det finns ingen regel

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Om en metod är större än halva skärmen är den för lång

  **Förklaringar:**

  - ❌ **a) Max 3 rader** - FEL: En tumregel, men inte en absolut sanning. Vissa metoder behöver vara längre
  - ✅ **b) Halva skärmen** - **RÄTT**: En bra riktlinje. Om du måste scrolla för att se hela metoden är den för lång. Bryt ner i mindre metoder med beskrivande namn
  - ❌ **c) Minst 20 rader** - FEL: Korta metoder är ofta bättre — de gör en sak och gör den bra
  - ❌ **d) Ingen regel** - FEL: Det finns riktlinjer — korta, fokuserade metoder är Clean Code
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad är bra namn på en metod enligt Clean Code?

a. `DoStuff()`<br>b. Metodnamnet ska förklara vad metoden gör — t.ex. `CalculateTotalPrice()` istället för `Calc()`<br>c. `M1()`<br>d. Namnet spelar ingen roll

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Metodnamnet ska förklara vad metoden gör

  **Förklaringar:**

  - ❌ **a) DoStuff** - FEL: Säger ingenting om vad metoden faktiskt gör
  - ✅ **b) Beskrivande namn** - **RÄTT**: `SaveToDatabase()`, `SendEmail()`, `ValidateUserInput()` — metodnamnet är dokumentation. Läsaren ska förstå vad som händer utan att läsa implementationen
  - ❌ **c) M1()** - FEL: Helt meningslöst — den första av många otydliga metoder
  - ❌ **d) Spelar ingen roll** - FEL: Namngivning är en av de VIKTIGASTE sakerna i Clean Code
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad menas med "koda mot interface, inte implementation"?

a. Du ska alltid använda `new` för att skapa objekt<br>b. Variabler och parametrar ska deklareras med interfacetyp, inte konkret klass — `IRepository` istället för `SqlRepository`<br>c. Interfaces ska undvikas<br>d. All kod ska vara abstrakt

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Variabler och parametrar ska deklareras med interfacetyp, inte konkret klass

  **Förklaringar:**

  - ❌ **a) Alltid new** - FEL: `new` binder dig till en konkret klass, vilket är motsatsen till interfacetänk
  - ✅ **b) Interface-typ** - **RÄTT**: `IProductRepository repo` istället för `SqlProductRepository repo`. Du kan byta implementation utan att ändra resten. Testning blir enklare — du kan skicka in en mock
  - ❌ **c) Undvika interfaces** - FEL: Interfaces är verktyget för att koda mot abstraktioner
  - ❌ **d) Allt abstrakt** - FEL: Balans — koda mot interface där det ger flexibilitet, använd konkreta klasser där det är tillräckligt
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
