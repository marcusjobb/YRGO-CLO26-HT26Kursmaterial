# Träningsuppgifter: Vecka 4 — Datastrukturer

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är en array i C#?

a. En samling värden av olika typer<br>
b. En ordnad samling värden av samma typ, med fast storlek<br>
c. En databas<br>
d. En typ av loop

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En ordnad samling värden av samma typ, med fast storlek

  **Förklaringar:**

  - ❌ **a) Olika typer** - FEL: En array innehåller värden av *samma* typ — alla `string`, alla `int`, etc.
  - ✅ **b) Samma typ, fast storlek** - **RÄTT**: `string[] veckodagar = { "Måndag", "Tisdag", "Onsdag" }` — när du skapat arrayen kan du inte ändra storleken
  - ❌ **c) En databas** - FEL: En array är en enkel datastruktur i minnet, inte en databas
  - ❌ **d) En loop** - FEL: En array lagrar data, en loop (som `for`/`foreach`) låter dig gå igenom datan
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vilket index har första elementet i en array?

a. 1<br>b. 0<br>c. -1<br>d. Det beror på

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** 0

  **Förklaringar:**

  - ❌ **a) 1** - FEL: Det är en av de vanligaste missuppfattningarna. I C# (och de flesta programspråk) börjar index på 0, inte 1
  - ✅ **b) 0** - **RÄTT**: `string[] veckodagar = { "Måndag", "Tisdag", "Onsdag" }` — `veckodagar[0]` = "Måndag", `veckodagar[1]` = "Tisdag"
  - ❌ **c) -1** - FEL: Negativa index fungerar inte i C#-arrayer (det gör det i Python)
  - ❌ **d) Det beror på** - FEL: Array-index börjar ALLTID på 0 i C#
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är skillnaden mellan en array och en `List<T>`?

a. En array har fast storlek, en List kan växa och krympa<br>
b. En List har fast storlek, en array kan ändras<br>
c. De är exakt samma sak<br>
d. En array kan bara innehålla text

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En array har fast storlek, en List kan växa och krympa

  **Förklaringar:**

  - ✅ **a) Array fast, List dynamisk** - **RÄTT**: `string[] arr = new string[5];` — alltid 5 platser. `List<string> list = new List<string>(); list.Add("hej");` — växer efter behov
  - ❌ **b) Omvänt** - FEL: Det är tvärtom — List är dynamisk, array är fast
  - ❌ **c) Samma sak** - FEL: Liknande, men olika användningsområden. List har fler inbyggda metoder som Add(), Remove(), Contains()
  - ❌ **d) Array bara text** - FEL: Arrayer kan vara av alla typer: `int[]`, `double[]`, `string[]`, etc.
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vilken metod lägger till ett element i en `List<T>`?

a. `list.Put(element)`<br>b. `list.Insert(element)`<br>c. `list.Add(element)`<br>d. `list.Push(element)`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `list.Add(element)`

  **Förklaringar:**

  - ❌ **a) Put** - FEL: Finns inte på List i C#
  - ❌ **b) Insert** - FEL: `Insert` finns, men den sätter in på en specifik position: `list.Insert(0, "ny")`
  - ✅ **c) Add** - **RÄTT**: `shoppinglista.Add("Mjölk")` lägger till "Mjölk" sist i listan
  - ❌ **d) Push** - FEL: `Push` finns på Stack, inte på List
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad returnerar `shoppinglista.Count` på en lista med 3 element?

a. `3`<br>b. `2`<br>c. `Length`<br>d. `Count` används inte för listor

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** 3

  **Förklaringar:**

  - ✅ **a) 3** - **RÄTT**: `Count` är egenskapen som anger antalet element i en List (precis som `Length` är för arrayer)
  - ❌ **b) 2** - FEL: `Count` är 1-indexerad i praktiken — 3 element ger Count = 3
  - ❌ **c) Length** - FEL: `Length` används på arrayer. Listor använder `Count`
  - ❌ **d) Används inte** - FEL: `Count` är den primära egenskapen för att få antalet element i en List
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är en `Dictionary<K, V>` bra för?

a. Att lagra värden i en bestämd ordning<br>
b. Att koppla ihop en unik nyckel med ett värde — som en telefonbok<br>
c. Att räkna antalet element<br>
d. Att sortera data automatiskt

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att koppla ihop en unik nyckel med ett värde — som en telefonbok

  **Förklaringar:**

  - ❌ **a) Bestämd ordning** - FEL: Dictionary har ingen garanterad ordning. För ordning, använd List
  - ✅ **b) Nyckel → värde** - **RÄTT**: `Dictionary<string, string> telefonbok` — du slår upp "Alex" och får "070-123 45 67". Perfekt när du vill hitta något på ett namn/en kod
  - ❌ **c) Räkna element** - FEL: `Count` finns, men det är inte Dictionarys styrka
  - ❌ **d) Sortera** - FEL: Dictionary sorterar inte automatiskt
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad händer om du slår upp en nyckel som inte finns i ett Dictionary?

a. Du får `null` tillbaka<br>b. Programmet kastar ett undantag (kraschar)<br>c. Dictionary lägger automatiskt till nyckeln<br>d. Du får en varning men programmet fortsätter

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Programmet kastar ett undantag (kraschar)

  **Förklaringar:**

  - ❌ **a) null** - FEL: I C# kastar Dictionary ett `KeyNotFoundException` — det returnerar inte null
  - ✅ **b) Kastar ett undantag** - **RÄTT**: `poäng["Okänd"]` kraschar med `KeyNotFoundException`. Använd alltid `ContainsKey()` först för att vara säker
  - ❌ **c) Lägger till nyckeln** - FEL: Dictionary gör det inte automatiskt (däremot kan `TryGetValue` hantera det snyggt)
  - ❌ **d) Varning** - FEL: Det är ett runtime-fel, inte en varning. Programmet stoppas
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Hur loopar du igenom ett Dictionary med foreach?

a. `foreach (string key in poäng)`<br>b. `foreach (KeyValuePair<K, V> par in poäng)`<br>c. `foreach (var value in poäng.Values)`<br>d. Alla tre fungerar beroende på vad du behöver

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Alla tre fungerar beroende på vad du behöver

  **Förklaringar:**

  - ❌ **a) Bara nycklar** - DELVIS RÄTT: Detta loopar bara över nycklarna, inte värdena
  - ❌ **b) KeyValuePair** - DELVIS RÄTT: Ger dig både `.Key` och `.Value` för varje element
  - ❌ **c) Bara värden** - DELVIS RÄTT: Loopar bara över värdena utan nycklarna
  - ✅ **d) Alla tre fungerar** - **RÄTT**: Välj efter behov. `KeyValuePair` är vanligast när du behöver både nyckel och värde
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad är en `enum`?

a. En typ av loop<br>b. En uppsättning namngivna konstanter<br>c. En sorts array<br>d. En metod för att räkna

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En uppsättning namngivna konstanter

  **Förklaringar:**

  - ❌ **a) En loop** - FEL: enum är en datatyp, inte en kontrollstruktur
  - ✅ **b) Namngivna konstanter** - **RÄTT**: `enum Riktning { Norr, Söder, Öst, Väst }` — du kan använda `Riktning.Norr` istället för siffran 0 eller strängen "norr"
  - ❌ **c) En array** - FEL: enum är inte en samling, den definierar möjliga värden
  - ❌ **d) En metod** - FEL: enum är en typdeklaration, inte en metod
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Varför är `enum` bättre än en sträng för fasta alternativ?

a. Kompilatorn fångar stavfel, Intellisense visar alla alternativ<br>
b. enum är snabbare än strängar<br>
c. enum tar mindre plats i minnet<br>
d. Alla ovanstående

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Alla ovanstående

  **Förklaringar:**

  - ✅ **a-c alla stämmer** - **RÄTT**: Kompilatorn fångar stavfel (till skillnad från strängen "norre"), Intellisense visar möjliga värden, och enum är både snabbare och tar mindre minne än strängar
  - ❌ **a) Bara stavfel** - DELVIS: Stämmer men inte hela sanningen
  - ❌ **b) Bara snabbare** - DELVIS: Stämmer men inte hela sanningen
  - ❌ **c) Bara minne** - DELVIS: Stämmer men inte hela sanningen
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 11

När ska du välja `List<T>` över en array?

a. När du vill lägga till eller ta bort element under körning<br>
b. När du vet exakt hur många element du behöver<br>
c. När du bara har ett fåtal element<br>
d. List är alltid bättre än array

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** När du vill lägga till eller ta bort element under körning

  **Förklaringar:**

  - ✅ **a) Add/Remove under körning** - **RÄTT**: Om du inte vet i förväg hur många element som behövs (t.ex. en inköpslista där användaren lägger till varor) är List det rätta valet
  - ❌ **b) Vet exakt antal** - FEL: Då är en array ofta bättre — enklare och något snabbare
  - ❌ **c) Få element** - FEL: Båda fungerar för få element, valet handlar om behovet att ändra storlek
  - ❌ **d) Alltid bättre** - FEL: Array är bättre när storleken är känd och fix — t.ex. veckans dagar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 12

Vad gör `poäng.ContainsKey("Alex")`?

a. Returnerar true om "Alex" finns som nyckel i Dictionaryt<br>
b. Lägger till "Alex" i Dictionaryt<br>
c. Tar bort "Alex" från Dictionaryt<br>
d. Skriver ut "Alex" poäng

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Returnerar true om "Alex" finns som nyckel i Dictionaryt

  **Förklaringar:**

  - ✅ **a) true om nyckeln finns** - **RÄTT**: `ContainsKey` är ett säkerhetsnät — använd den *innan* du slår upp ett värde för att undvika `KeyNotFoundException`
  - ❌ **b) Lägger till** - FEL: `Add` eller indexerare `["Alex"] = 42` lägger till. `ContainsKey` bara kollar
  - ❌ **c) Tar bort** - FEL: `Remove` tar bort. `ContainsKey` bara frågar
  - ❌ **d) Skriver ut** - FEL: `ContainsKey` returnerar `true`/`false`, den skriver inte ut något
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
