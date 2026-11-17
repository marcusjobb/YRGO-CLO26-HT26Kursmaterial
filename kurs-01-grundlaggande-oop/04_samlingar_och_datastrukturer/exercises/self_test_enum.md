# Self-test — enum

Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

---

### Fråga 1

Vad är en `enum` i C#?

a. En typ av loop<br>b. En samling namngivna konstanter<br>c. En sorts array med fasta värden<br>d. En metod som räknar element

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En samling namngivna konstanter

  **Förklaringar:**

  - ❌ **a) En loop** - FEL: `enum` är en typdeklaration, inte en kontrollstruktur
  - ✅ **b) Namngivna konstanter** - **RÄTT**: `enum Riktning { Norr, Söder, Öst, Väst }` — du skriver `Riktning.Norr` istället för siffran `0` eller strängen `"norr"`. Kompilatorn vet vilka värden som är giltiga
  - ❌ **c) En array** - FEL: En array lagrar data i en sekvens. En enum definierar möjliga namngivna alternativ — det är inte samma sak
  - ❌ **d) En metod** - FEL: `enum` är en typdeklaration med nyckelordet `enum`, inte en metod
</details>

---

### Fråga 2

Var bör man deklarera en `enum` i en C#-fil?

a. Inuti `Main()`<br>b. Inuti klassen, precis som fält<br>c. Utanför klassen, på namespace-nivå<br>d. Det spelar ingen roll — alla platser är likvärdiga

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Utanför klassen, på namespace-nivå

  **Förklaringar:**

  - ❌ **a) Inuti Main** - FEL: Det är inte giltig C#-syntax. Typdeklarationer (klasser, enum, etc.) kan inte ligga inuti en metod
  - ❌ **b) Inuti klassen** - DELVIS: Det funkar tekniskt, men enums som används av flera klasser bör ligga utanför för att vara tillgängliga överallt
  - ✅ **c) Utanför klassen** - **RÄTT**: Placera enumen ovanför eller under `class Program { }` i filen. Då är den tillgänglig för alla klasser i projektet
  - ❌ **d) Spelar ingen roll** - FEL: Placeringen avgör synligheten — en enum inuti en klass är bara nåbar via den klassen
</details>

---

### Fråga 3

Vad är det underliggande heltalsvärdet på `Tisdag` i denna enum?

```csharp
enum Dag { Måndag, Tisdag, Onsdag }
```

a. `1`<br>b. `2`<br>c. `0`<br>d. Det har inget heltalsvärde

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `1`

  **Förklaringar:**

  - ✅ **a) 1** - **RÄTT**: Enum-värden börjar på `0` som standard. `Måndag = 0`, `Tisdag = 1`, `Onsdag = 2`. Du kan kasta: `(int)Dag.Tisdag` ger `1`
  - ❌ **b) 2** - FEL: Det är `Onsdag` som är 2
  - ❌ **c) 0** - FEL: `Måndag` är 0, inte `Tisdag`
  - ❌ **d) Inget heltalsvärde** - FEL: Alla enum-värden har ett underliggande heltal. Standardtypen är `int`
</details>

---

### Fråga 4

Hur skriver man en switch-sats på en enum-variabel?

```csharp
enum Riktning { Norr, Söder, Öst, Väst }
Riktning r = Riktning.Norr;
```

a. `switch (r) { case "Norr": ... }`<br>b. `switch (r) { case 0: ... }`<br>c. `switch (r) { case Riktning.Norr: ... }`<br>d. `switch (Riktning.r) { case Norr: ... }`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `switch (r) { case Riktning.Norr: ... }`

  **Förklaringar:**

  - ❌ **a) Sträng** - FEL: `r` är av typen `Riktning`, inte `string`. Kompilatorn accepterar inte `case "Norr"`
  - ❌ **b) Heltal** - FEL: Det funkar ibland, men är dålig stil — hela poängen med enum är att slippa anonyma siffror
  - ✅ **c) Enum.Värde** - **RÄTT**: `case Riktning.Norr:` är korrekt syntax. Kompilatorn varnar om du glömmer ett case-värde om du lägger till ett nytt enum-alternativ
  - ❌ **d) Riktning.r** - FEL: `r` är variabeln, inte en del av enum-typen. `Riktning.r` finns inte
</details>

---

### Fråga 5

Varför är `enum` bättre än en `string` för att representera fasta alternativ som veckodagar?

a. enum är alltid snabbare att skriva<br>b. Kompilatorn fångar stavfel och Intellisense visar alla giltiga alternativ<br>c. enum kan innehålla fler värden än en sträng<br>d. Det är ingen skillnad — det är en smakfråga

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Kompilatorn fångar stavfel och Intellisense visar alla giltiga alternativ

  **Förklaringar:**

  - ❌ **a) Snabbare att skriva** - FEL: Det tar ungefär lika lång tid — fördelarna är säkerhet, inte skriv­hastighet
  - ✅ **b) Kompilatorskydd + Intellisense** - **RÄTT**: `if (dag == "Måndag")` — om du skriver `"måndag"` (liten bokstav) funkar det inte och du ser inget fel förrän du kör. Med `if (dag == Dag.Måndag)` fångar kompilatorn stavfel och Intellisense visar `Dag.Måndag`, `Dag.Tisdag`, etc.
  - ❌ **c) Fler värden** - FEL: En sträng kan innehålla vad som helst — det är en nackdel, inte en fördel
  - ❌ **d) Ingen skillnad** - FEL: Enum ger tydligt bättre typskydd och läsbarhet för fasta alternativ
</details>

---

### Fråga 6

Vad skriver följande kod ut?

```csharp
enum Fas { Solid, Flytande, Gas }
Console.WriteLine((int)Fas.Gas);
```

a. `"Gas"`<br>b. `0`<br>c. `2`<br>d. Det är ett kompileringsfel

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `2`

  **Förklaringar:**

  - ❌ **a) "Gas"** - FEL: `(int)Fas.Gas` castar till heltal, inte till sträng. `Fas.Gas.ToString()` skulle ge `"Gas"`
  - ❌ **b) 0** - FEL: `Solid` är 0. `Flytande` är 1. `Gas` är 2
  - ✅ **c) 2** - **RÄTT**: Enum-värden börjar på 0 som standard. `Solid=0`, `Flytande=1`, `Gas=2`. Casten `(int)Fas.Gas` ger `2`
  - ❌ **d) Kompileringsfel** - FEL: Det är giltig C# — enum kan alltid castas till `int` och tillbaka
</details>
