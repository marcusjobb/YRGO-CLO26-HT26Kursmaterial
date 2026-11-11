# Self-test — Arrayer

Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

---

### Fråga 1

Vad är en array i C#?

a. En samling värden av olika typer<br>b. En ordnad samling värden av samma typ med fast storlek<br>c. En lista som kan växa och krympa<br>d. En metod för att loopa

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En ordnad samling värden av samma typ med fast storlek

  **Förklaringar:**

  - ❌ **a) Olika typer** - FEL: En array innehåller värden av *samma* typ — alla `string`, alla `int`, etc.
  - ✅ **b) Samma typ, fast storlek** - **RÄTT**: `string[] veckodagar = { "Måndag", "Tisdag", "Onsdag" };` — storleken är låst när du skapar den
  - ❌ **c) Växa och krympa** - FEL: Det beskriver `List<T>`. En array har alltid samma antal platser
  - ❌ **d) En metod** - FEL: En array är en datastruktur, inte en metod
</details>

---

### Fråga 2

Vilket påstående om arraydeklaration är korrekt?

a. `int[] tal = new int[3];` skapar en array med plats för 3 heltal<br>b. `int[] tal = new int[3];` skapar en array med värdena 1, 2, 3<br>c. `int tal[] = new int[3];` är den korrekta syntaxen i C#<br>d. Man måste ange värden direkt — `new int[3]` fungerar inte

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `int[] tal = new int[3];` skapar en array med plats för 3 heltal

  **Förklaringar:**

  - ✅ **a) Plats för 3 heltal** - **RÄTT**: Arrayen har tre platser — alla initierade till standardvärdet `0` för `int`
  - ❌ **b) Värdena 1, 2, 3** - FEL: `new int[3]` sätter alla till `0`, inte 1, 2, 3. För det skriver man `{ 1, 2, 3 }`
  - ❌ **c) `int tal[]`** - FEL: I C# sitter hakparentesen på typen: `int[]`, inte på variabelnamnet. `int tal[]` är Java-stil och funkar inte
  - ❌ **d) Fungerar inte** - FEL: `new int[3]` är helt giltigt och vanligt
</details>

---

### Fråga 3

Vad händer om du kör `Console.WriteLine(arr[arr.Length]);`?

a. Du får det sista elementet<br>b. Du får `null`<br>c. Programmet kastar ett `IndexOutOfRangeException`<br>d. Du får `0`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Programmet kastar ett `IndexOutOfRangeException`

  **Förklaringar:**

  - ❌ **a) Sista elementet** - FEL: Det sista elementet är `arr[arr.Length - 1]`. Index slutar ett steg *innan* Length
  - ❌ **b) null** - FEL: C# kastar ett undantag — det returnerar inte null för arrayer
  - ✅ **c) IndexOutOfRangeException** - **RÄTT**: En array med 5 element har index 0–4. `arr[5]` (dvs. `arr.Length`) är utanför gränsen och kraschar
  - ❌ **d) 0** - FEL: Det vore bekvämt, men C# skyddar dig inte tyst — det kraschar
</details>

---

### Fråga 4

Vad skriver följande kod ut?

```csharp
int[] tal = { 10, 20, 30 };
Console.WriteLine(tal.Length);
```

a. `2`<br>b. `3`<br>c. `30`<br>d. `0`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `3`

  **Förklaringar:**

  - ❌ **a) 2** - FEL: `Length` anger *antalet element*, inte det sista indexet. Tre element ger Length = 3
  - ✅ **b) 3** - **RÄTT**: `{ 10, 20, 30 }` innehåller tre element → `Length` är 3. Sista indexet är 2
  - ❌ **c) 30** - FEL: Det är värdet på sista elementet, inte längden
  - ❌ **d) 0** - FEL: En tom array `new int[0]` ger Length = 0, men inte den här
</details>

---

### Fråga 5

Vilket är rätt sätt att loopa igenom alla element med `for`?

a. `for (int i = 1; i <= arr.Length; i++)`<br>b. `for (int i = 0; i < arr.Length; i++)`<br>c. `for (int i = 0; i <= arr.Length; i++)`<br>d. `for (int i = arr.Length; i > 0; i--)`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `for (int i = 0; i < arr.Length; i++)`

  **Förklaringar:**

  - ❌ **a) Börja på 1** - FEL: Första elementet är på index 0 — du missar det om du börjar på 1
  - ✅ **b) 0 till < Length** - **RÄTT**: Start på `0`, sluta *innan* `Length`. Det är standarden för att gå igenom alla element utan att krascha
  - ❌ **c) 0 till <= Length** - FEL: `<=` innebär att du försöker nå `arr[arr.Length]` sista varvet — det ger `IndexOutOfRangeException`
  - ❌ **d) Baklänges från Length** - FEL: Det börjar på `arr[arr.Length]` vilket kraschar direkt
</details>

---

### Fråga 6

Vad är den viktigaste skillnaden mellan `for` och `foreach` när man loopar en array?

a. `foreach` är snabbare än `for`<br>b. `for` ger dig index och kan ändra element, `foreach` kan inte ändra element<br>c. `foreach` fungerar bara med strängar<br>d. `for` och `foreach` är exakt samma sak

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `for` ger dig index och kan ändra element, `foreach` kan inte ändra element

  **Förklaringar:**

  - ❌ **a) foreach snabbare** - FEL: I praktiken är de ungefär lika snabba för arrayer
  - ✅ **b) for kan ändra, foreach kan inte** - **RÄTT**: `foreach (string s in arr) { s = "ny"; }` fungerar inte — `s` är en kopia. Med `for` skriver du `arr[i] = "ny"` och ändrar faktiskt arrayen
  - ❌ **c) Bara strängar** - FEL: `foreach` fungerar med alla typer: `int[]`, `string[]`, `List<T>`, etc.
  - ❌ **d) Samma sak** - FEL: De beter sig lika vid läsning, men skiljer sig vid skrivning och när du behöver index
</details>
