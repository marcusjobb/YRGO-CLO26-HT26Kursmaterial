# Self-test — List&lt;T&gt;

Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

---

### Fråga 1

Vad är den avgörande skillnaden mellan en array och en `List<T>`?

a. En array kan bara innehålla tal<br>b. En array har fast storlek, en `List<T>` kan växa och krympa<br>c. En `List<T>` är alltid sorterad<br>d. En array är snabbare för alla operationer

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En array har fast storlek, en `List<T>` kan växa och krympa

  **Förklaringar:**

  - ❌ **a) Bara tal** - FEL: Arrayer kan vara av alla typer: `string[]`, `bool[]`, etc.
  - ✅ **b) Fast vs dynamisk** - **RÄTT**: `string[] arr = new string[3];` — alltid 3 platser. `List<string> lista = new List<string>();` — börjar tom och växer med `Add()`
  - ❌ **c) Alltid sorterad** - FEL: `List<T>` behåller insättningsordningen. Du kan sortera med `lista.Sort()`, men det sker inte automatiskt
  - ❌ **d) Array alltid snabbare** - FEL: Array är något snabbare vid indexåtkomst, men skillnaden märks sällan i vanlig kod
</details>

---

### Fråga 2

Hur lägger man till ett element sist i en `List<string>`?

a. `lista.Insert("hej")`<br>b. `lista.Append("hej")`<br>c. `lista.Add("hej")`<br>d. `lista.Push("hej")`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `lista.Add("hej")`

  **Förklaringar:**

  - ❌ **a) Insert** - FEL: `Insert` finns, men kräver ett index: `lista.Insert(0, "hej")` lägger till *på position 0*
  - ❌ **b) Append** - FEL: `Append` finns inte på `List<T>` i C# (det finns på `StringBuilder`)
  - ✅ **c) Add** - **RÄTT**: `lista.Add("hej")` lägger till elementet sist i listan. Det är den vanligaste operationen
  - ❌ **d) Push** - FEL: `Push` finns på `Stack<T>`, inte på `List<T>`
</details>

---

### Fråga 3

Hur tar man bort det första förekomsten av ett specifikt värde från en lista?

a. `lista.Delete("hej")`<br>b. `lista.Remove("hej")`<br>c. `lista.RemoveAt("hej")`<br>d. `lista.Pop("hej")`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `lista.Remove("hej")`

  **Förklaringar:**

  - ❌ **a) Delete** - FEL: `Delete` finns inte på `List<T>`
  - ✅ **b) Remove** - **RÄTT**: `lista.Remove("hej")` tar bort den *första* förekomsten av "hej" och returnerar `true` om det lyckades
  - ❌ **c) RemoveAt** - FEL: `RemoveAt` tar ett *index*, inte ett värde: `lista.RemoveAt(0)` tar bort det första elementet oavsett vad det är
  - ❌ **d) Pop** - FEL: `Pop` finns på `Stack<T>`, inte på `List<T>`
</details>

---

### Fråga 4

Vad returnerar `lista.Count` direkt efter `var lista = new List<int>();`?

a. `1`<br>b. `null`<br>c. `Length`<br>d. `0`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `0`

  **Förklaringar:**

  - ❌ **a) 1** - FEL: En nyligen skapad lista är tom — den har inga element
  - ❌ **b) null** - FEL: `Count` är en `int`, inte en referenstyp. Den kan inte vara null
  - ❌ **c) Length** - FEL: Arrayer har `Length`. Listor använder `Count`
  - ✅ **d) 0** - **RÄTT**: `new List<int>()` skapar en tom lista. `Count` är 0 tills man anropar `Add()`
</details>

---

### Fråga 5

Hur kollar man om ett element finns i en `List<string>`?

a. `lista.Has("hej")`<br>b. `lista.Exists("hej")`<br>c. `lista.Contains("hej")`<br>d. `lista.Find("hej")`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `lista.Contains("hej")`

  **Förklaringar:**

  - ❌ **a) Has** - FEL: `Has` finns inte på `List<T>`
  - ❌ **b) Exists** - FEL: `lista.Exists(...)` finns, men kräver ett predikat: `lista.Exists(x => x == "hej")` — inte en enkel sträng
  - ✅ **c) Contains** - **RÄTT**: `lista.Contains("hej")` returnerar `true` om "hej" finns, annars `false`. Enkel och tydlig
  - ❌ **d) Find** - FEL: `Find` returnerar *värdet* (eller default) — det är inte ett ja/nej-svar
</details>

---

### Fråga 6

Vilket är rätt sätt att skapa en `List<int>` med tre startvärden 1, 2, 3?

a. `List<int> tal = new List<int>(1, 2, 3);`<br>b. `List<int> tal = new List<int>[1, 2, 3];`<br>c. `List<int> tal = new List<int> { 1, 2, 3 };`<br>d. `List<int> tal = List.Of(1, 2, 3);`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `List<int> tal = new List<int> { 1, 2, 3 };`

  **Förklaringar:**

  - ❌ **a) Parentes med värden** - FEL: `new List<int>(3)` skapar en lista med *kapacitet* 3, inte värdena 1, 2, 3
  - ❌ **b) Hakparenteser** - FEL: Det är array-syntaxen, inte list-syntaxen
  - ✅ **c) Klammerparentes** - **RÄTT**: Klammerparentes `{ 1, 2, 3 }` är *collection initializer* — det funkar för alla samlingar i C#
  - ❌ **d) List.Of** - FEL: `List.Of` finns i Java, inte i C#
</details>
