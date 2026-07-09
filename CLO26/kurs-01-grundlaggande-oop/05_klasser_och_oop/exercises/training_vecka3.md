# Träningsuppgifter: Vecka 3 — Klasser och OOP

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är skillnaden mellan en klass och ett objekt?

a. De är samma sak<br>
b. En klass är en ritning, ett objekt är ett byggt hus från den ritningen<br>
c. Ett objekt är ritningen, en klass är huset<br>
d. En klass kan bara finnas i en fil, ett objekt kan finnas i flera

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En klass är en ritning, ett objekt är ett byggt hus från den ritningen

  **Förklaringar:**

  - ❌ **a) Samma sak** - FEL: Klassen är ritningen, objektet är instansen. Precis som en husritning inte är ett hus
  - ✅ **b) Ritning vs hus** - **RÄTT**: `class BankAccount { }` är ritningen. `new BankAccount()` skapar ett objekt (instans). Från en klass kan du skapa hur många objekt som helst
  - ❌ **c) Omvänt** - FEL: Omvänt — klassen är ritningen, objektet är det byggda
  - ❌ **d) En fil vs flera** - FEL: Både klasser och objekt kan finnas var som helst
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad gör en konstruktor?

a. Den förstör objektet när du är klar<br>
b. Den sätter startvärden när ett objekt skapas<br>
c. Den ritar upp klassen i minnet<br>
d. Den skriver ut alla fält

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Den sätter startvärden när ett objekt skapas

  **Förklaringar:**

  - ❌ **a) Förstör objektet** - FEL: Det är en destruktor (används sällan i C#)
  - ✅ **b) Sätter startvärden** - **RÄTT**: Konstruktorn körs automatiskt när du skriver `new BankAccount("Anna", 1000)`. Den ser till att objektet har giltiga startvärden
  - ❌ **c) Ritar upp i minnet** - FEL: C# sköter minneshanteringen automatiskt, konstruktorn sätter bara värden
  - ❌ **d) Skriver ut** - FEL: En konstruktor kan innehålla utskrifter, men dess huvudsakliga jobb är att initiera objektet
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad betyder `private` i en klass?

a. All kod i programmet kan nå värdet<br>
b. Bara koden inuti samma klass kan nå värdet<br>
c. Bara koden i samma fil kan nå värdet<br>
d. Ingenting — alla fält är privata som standard

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Bara koden inuti samma klass kan nå värdet

  **Förklaringar:**

  - ❌ **a) All kod kan nå** - FEL: Det är `public`
  - ✅ **b) Bara klassen själv** - **RÄTT**: `private` är grunden för inkapsling. Ett privat fält kan bara läsas/ändras av metoder inuti samma klass
  - ❌ **c) Samma fil** - FEL: `private` begränsar till klassen, inte filen. En fil kan ha flera klasser
  - ❌ **d) Standard** - FEL: I C# är fält `private` som standard om du inte skriver något — men var tydlig ändå!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är en property med `private set`?

a. Alla kan läsa, bara klassen kan ändra<br>
b. Bara klassen kan läsa, alla kan ändra<br>
c. Alla kan både läsa och ändra<br>
d. Ingen kan läsa eller ändra

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Alla kan läsa, bara klassen kan ändra

  **Förklaringar:**

  - ✅ **a) Alla läser, bara klassen ändrar** - **RÄTT**: `public double Saldo { get; private set; }` — utifrån kan du läsa `konto.Saldo`, men `konto.Saldo = 999` ger kompileringsfel
  - ❌ **b) Bara klassen läser, alla ändrar** - FEL: Det vore bakvänt och skulle bryta mot inkapsling
  - ❌ **c) Alla kan både läsa och ändra** - FEL: Det är `public get; public set;` — full öppenhet
  - ❌ **d) Ingen kan något** - FEL: En property med `private set` är fullt läsbar utifrån
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är inkapsling (encapsulation)?

a. Att göra all data publik så alla kan nå den<br>
b. Att skydda data genom privata fält och kontrollerade metoder<br>
c. Att skriva all kod i en enda klass<br>
d. Att göra koden snabbare

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att skydda data genom privata fält och kontrollerade metoder

  **Förklaringar:**

  - ❌ **a) Göra all data publik** - FEL: Det är motsatsen till inkapsling
  - ✅ **b) Skydda data** - **RÄTT**: Fälten är privata. Utsidan kommunicerar via publika metoder och properties. Saldo kan inte bli negativt om `TaUt()` validerar
  - ❌ **c) En enda klass** - FEL: Inkapsling handlar om synlighet på klassnivå, inte antal klasser
  - ❌ **d) Snabbare kod** - FEL: Inkapsling påverkar kodorganisering, inte prestanda
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad blir utskriften?

```csharp
class BankAccount
{
    public double Saldo { get; private set; }
    public BankAccount(double startSaldo)
    {
        Saldo = startSaldo;
    }
    public void SättIn(double belopp)
    {
        if (belopp > 0)
            Saldo += belopp;
    }
}

BankAccount konto = new BankAccount(1000);
konto.SättIn(500);
Console.WriteLine(konto.Saldo);
```

a. 500<br>b. 1000<br>c. 1500<br>d. 0

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** 1500

  **Förklaringar:**

  - ❌ **a) 500** - FEL: 500 är beloppet som sätts in, men startvärdet var 1000
  - ❌ **b) 1000** - FEL: 1000 är startvärdet, men 500 har satts in därefter
  - ✅ **c) 1500** - **RÄTT**: 1000 (start) + 500 (insättning) = 1500. `SättIn()` validerar att belopp > 0 innan det läggs till
  - ❌ **d) 0** - FEL: Saldo startar på 1000 via konstruktorn
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad händer om du försöker göra `konto.Saldo = 5000` med `private set`?

a. Saldo ändras till 5000<br>
b. Programmet kraschar vid körning<br>
c. Kompilatorn ger ett fel — du kan inte skriva till en private set-property utifrån<br>
d. Ingenting — värdet ignoreras tyst

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Kompilatorn ger ett fel

  **Förklaringar:**

  - ❌ **a) Ändras till 5000** - FEL: `private set` förhindrar detta. Hade det varit `public set` hade det fungerat
  - ❌ **b) Kraschar vid körning** - FEL: Felet upptäcks *innan* programmet körs, vid kompilering
  - ✅ **c) Kompileringsfel** - **RÄTT**: C# skyddar dig redan vid kompilering. Du får ett tydligt felmeddelande att propertyn bara har en private setter
  - ❌ **d) Ignoreras tyst** - FEL: C# är typsäkert — den låter dig inte skriva kod som inte fungerar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är nyckelordet `this` i en konstruktor?

a. En referens till det aktuella objektet — skiljer på parameter och fält<br>
b. En referens till klassen själv<br>
c. Ett sätt att skapa ett nytt objekt<br>
d. Samma som `base`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En referens till det aktuella objektet

  **Förklaringar:**

  - ✅ **a) Referens till aktuellt objekt** - **RÄTT**: `this.owner = owner;` — `this.owner` är fältet, `owner` är parametern. Utan `this` skulle parametern bara skriva över sig själv
  - ❌ **b) Referens till klassen** - FEL: `this` pekar på *instansen* (objektet), inte klassen
  - ❌ **c) Skapa nytt objekt** - FEL: `new` skapar objekt, inte `this`
  - ❌ **d) Samma som base** - FEL: `base` används för att nå basklassen vid arv, `this` är det egna objektet
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Varför ska fält vara privata?

a. För att spara minne<br>b. För att förhindra att kod utifrån sätter ogiltiga värden<br>c. För att göra koden svårare att läsa<br>d. För att programmet ska gå snabbare

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** För att förhindra att kod utifrån sätter ogiltiga värden

  **Förklaringar:**

  - ❌ **a) Spara minne** - FEL: Private/public påverkar inte minnesanvändning
  - ✅ **b) Förhindra ogiltiga värden** - **RÄTT**: Utan private kan vem som helst skriva `konto.balance = -99999`. Med private + en `TaUt()`-metod med validering är datan skyddad
  - ❌ **c) Göra koden svårare** - FEL: Tvärtom — inkapsling gör koden lättare att förstå och underhålla
  - ❌ **d) Snabbare** - FEL: Ingen prestandaskillnad mellan private och public
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Hur skapar du två separata objekt från samma klass?

a. `BankAccount konto1 = new BankAccount(); BankAccount konto2 = new BankAccount();`<br>
b. `BankAccount konto1 = new BankAccount; BankAccount konto2 = konto1;`<br>
c. `BankAccount konto1 = BankAccount(); BankAccount konto2 = BankAccount();`<br>
d. `var konto1 = BankAccount; var konto2 = BankAccount;`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `new BankAccount()` två gånger

  **Förklaringar:**

  - ✅ **a) `new` två gånger** - **RÄTT**: Varje `new` skapar ett nytt, oberoende objekt i minnet. De delar samma klass (ritning) men har olika data
  - ❌ **b) konto2 = konto1** - FEL: Det skapar inte ett nytt objekt — båda variablerna pekar på *samma* objekt. Ändrar du i en påverkas den andra
  - ❌ **c) Utan `new`** - FEL: Du måste använda `new` för att skapa ett objekt i C#
  - ❌ **d) `var` utan `new`** - FEL: `var konto1 = BankAccount;` försöker tilldela själva klassen som värde — det går inte
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
