# Tentafrågor — Datastrukturer

Öva inför tentan. Varje fråga har fyra alternativ — ett rätt, ett lite roligt, och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — Arrayindex

Vilket index har det första elementet i en array?

A) 1 — det är mer naturligt att börja räkna på 1
B) 0 — arrayer är nollindexerade i C#
C) Det beror på hur arrayen skapades
D) –1 — man börjar alltid bakifrån i programmering 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Naturligt att tänka så, men C# (liksom de flesta språk) börjar på 0. `veckodagar[0]` är `"Måndag"`.
**B)** Rätt. Det första elementet är alltid index 0, och det sista är `längd - 1`.
**C)** Indexeringen bestäms av språket, inte av hur man skapar arrayen.
**D)** Negativt index kastar ett `IndexOutOfRangeException` — inte en rekommenderad start.

</details>

---

## Fråga 2 — Array vs List

Vad är den viktigaste skillnaden mellan en array och en `List<T>`?

A) En array kan bara lagra tal, en `List<T>` kan lagra vilken typ som helst
B) En array har fast storlek vid skapandet, en `List<T>` kan växa och krympa dynamiskt
C) `List<T>` är snabbare än en array vid alla operationer
D) En array är en klass, men en `List<T>` är egentligen en array i en kostym 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Fel — en `string[]` lagrar bara strängar, och en `int[]` bara heltal. Typen sätts för båda.
**B)** Rätt. `string[] veckodagar = new string[5]` är alltid fem platser. En `List<string>` kan växa med `Add()` och krympa med `Remove()`.
**C)** Det stämmer inte generellt — arrayer är ofta snabbare för direktåtkomst eftersom de inte har overhead för dynamisk storlek.
**D)** Internt är `List<T>` faktiskt byggd på en array — men det är en detalj för en annan dag.

</details>

---

## Fråga 3 — Dictionary

Vad lagrar ett `Dictionary<string, int>`?

A) En sorterad lista med heltal, där nycklarna är automatiska index
B) Par av nyckel och värde, där nyckeln är en `string` och värdet ett `int`
C) Enbart strängar, där `int` anger hur många som får plats
D) Alla ord i det svenska språket, sorterade efter popularitet 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Det beskriver en array eller lista med automatiska index, inte ett dictionary.
**B)** Rätt. `highscores["Anna"] = 9500` lagrar strängen `"Anna"` som nyckel och `9500` som tillhörande värde.
**C)** `int` i typargumentet anger typen på värdena, inte kapaciteten.
**D)** Det vore ett imponerande dictionary, men det är inte det C# erbjuder direkt.

</details>

---

## Fråga 4 — Enum

Vad är ett enum?

A) En lista med objekt som kan ärva från varandra
B) En datastruktur som lagrar nyckel-värde-par
C) En typ som definierar en begränsad uppsättning namngivna konstanta värden
D) En speciell loop som räknar baklänges 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Det är en beskrivning av arv och klasser — inget med enum att göra.
**B)** Det beskriver ett `Dictionary`.
**C)** Rätt. `enum Väderlek { Soligt, Molnigt, Regnigt, Snöigt }` skapar fyra möjliga värden. Kompilatorn hindrar att man råkar tilldela något ogiltigt.
**D)** `for`-loopen kan räkna baklänges, men det kallas inte enum.

</details>

---

## Fråga 5 — ContainsKey

Hur kontrollerar man om en nyckel finns i ett `Dictionary<string, int>` som heter `highscores`?

A) `highscores.Contains("Anna")`
B) `highscores.HasKey("Anna")`
C) `highscores.ContainsKey("Anna")`
D) `"Anna" in highscores` — det ser ju nästan ut som Python 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** `Contains` finns på `List<T>`, inte på `Dictionary`. Det ger kompileringsfel här.
**B)** `HasKey` är inte en metod som finns i C# — det låter rimligt men existerar inte.
**C)** Rätt. `highscores.ContainsKey("Anna")` returnerar `true` eller `false` beroende på om nyckeln finns.
**D)** Python-syntax fungerar inte i C# — men tanken var rätt.

</details>

---

## Fråga 6 — List.Count

Vad returnerar `shoppinglista.Count` om listan skapades med fem varor och sedan togs en bort med `Remove()`?

A) 5 — `Count` visar den ursprungliga storleken
B) 4 — `Count` visar det aktuella antalet element i listan
C) 0 — `Remove()` tömmer hela listan
D) En string som säger "4 varor kvar" 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** `Count` är dynamisk — den speglar alltid listans nuvarande innehåll, inte hur den såg ut när den skapades.
**B)** Rätt. Fem varor lades till, en togs bort med `Remove("Bröd")` — `Count` är då 4.
**C)** `Remove()` tar bara bort det angivna elementet, inte hela listan.
**D)** `Count` returnerar ett `int`, inte en sträng — formatering sköter man själv.

</details>

---
