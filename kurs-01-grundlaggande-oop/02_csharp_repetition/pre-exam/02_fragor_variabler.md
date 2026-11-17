# Tentafrågor — Syntax och variabler

Öva inför tentan. Varje fråga har fyra alternativ — ett rätt, ett lite roligt, och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — heltalsdeklaration

Vilket alternativ deklarerar en heltalsvariabel på rätt sätt i C#?

B) `integer tal = 42;`<br>
A) `int tal = 42;`<br>
C) `Int tal = 42;`<br>
D) `tal = 42;` — kompilatorn fattar nog vad man menar 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**B)** `integer` är inte ett nyckelord i C# — det är Java som inte heller använder det. Heltal heter `int`.<br>
**A)** Rätt. `int tal = 42;` — explicit typ, rätt syntax, tydlig avsikt.<br>
**C)** `Int` med stort I är en klass i .NET, men man använder nyckelordet `int` (liten bokstav) i vanlig kod.<br>
**D)** Kompilatorn fattar faktiskt inte — en variabel måste deklareras med en typ innan den används.

</details>

---

## Fråga 2 — decimaltal

Vilken datatyp används för att lagra decimaltal i C#?

B) `double` är standardvalet för decimaltal<br>
C) `float` är den vanligaste typen för decimaltal<br>
A) `decimal` — men bara för pengar<br>
D) `int` med ett komma i, typ `int tal = 3,14;` 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. `double tal = 3.14;` — `double` är 64-bitars och standardvalet för decimaltal i C#.<br>
**C)** `float` är en 32-bitars typ med lägre precision än `double`. Den används när precision är mindre viktig.<br>
**A)** `decimal` finns och är precis — men den används främst för ekonomiberäkningar, inte som standardval.<br>
**D)** `int` kan bara hålla hela tal — ett kommatecken i värdet är ett kompileringsfel.

</details>

---

## Fråga 3 — stränginterpolation

Hur ser korrekt stränginterpolation ut i C#?

A) `"Hej " + namn + "!"`<br>
C) `"Hej {namn}!"`<br>
B) `$"Hej {namn}!"`<br>
D) `"Hej $(namn)!"` — som i terminalen, borde funka 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Det fungerar, men det är sammanslagning med `+`, inte interpolation — klumpigare och svårare att läsa.<br>
**C)** Utan `$` framför strängen är `{namn}` bara bokstavliga klammerparenteser, inte ett uttryck.<br>
**B)** Rätt. `$` framför citattecknet aktiverar interpolation — `{namn}` ersätts med variabelns värde.<br>
**D)** `$()` är bash-syntax. C# känner inte till den — kompilatorn protesterar.

</details>

---

## Fråga 4 — heltalsdivision

Vad blir resultatet av `7 / 2` i C#?

B) `3.5` — det är det matematiskt korrekta svaret<br>
C) `3` — heltalsdivision trunkerar decimaldelen<br>
A) `4` — resultatet avrundas till närmaste heltal<br>
D) `3.5` men bara om det är tisdag 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**B)** Matematiskt korrekt, men C# bryr sig om typerna. Två `int`-värden ger ett `int`-resultat.<br>
**C)** Rätt. `7 / 2` ger `3` — decimaldelen kastas bort utan avrundning. Vill du ha `3.5` behöver du `7.0 / 2` eller `(double)7 / 2`.<br>
**A)** C# avrundar inte vid heltalsdivision — den trunkerar (kapar av).<br>
**D)** C# har ännu inte implementerat veckodagsbaserad matematik. Kanske i C# 15.

</details>

---

## Fråga 5 — namngivningskonvention

Vilken namngivningskonvention används för variabler i C#?

C) `camelCase` — liten bokstav på första ordet, stor bokstav på efterföljande<br>
B) `PascalCase` — stor bokstav på varje ord, även det första<br>
A) `snake_case` — ord separeras med understreck<br>
D) `SCREAMING_SNAKE_CASE` — för att visa att man menar allvar 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**C)** Rätt. Variabler i C# skrivs med `camelCase`: `int antalPoang`, `string fornamn`, `bool arAktiv`.<br>
**B)** `PascalCase` används i C# — men för klasser, metoder och properties, inte variabler.<br>
**A)** `snake_case` används i språk som Python och Ruby. I C# är det ovanligt och ses som dålig stil.<br>
**D)** `SCREAMING_SNAKE_CASE` används för konstanter i en del språk — i C# används `PascalCase` även för konstanter.

</details>

---

## Fråga 6 — bool

Vad är en `bool`?

B) En typ som kan hålla heltal mellan 0 och 1<br>
C) En typ för korta textsträngar, max ett tecken<br>
A) En typ som representerar sant (`true`) eller falskt (`false`)<br>
D) En typ uppkallad efter matematikern George Boole, fast han stavade det annorlunda 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**B)** Det låter som `int` med begränsat värde — `bool` håller inte siffror, bara logiska tillstånd.<br>
**C)** Det är `char` som håller ett enskilt tecken — `bool` har ingenting med text att göra.<br>
**A)** Rätt. `bool arInloggad = true;` — används för villkor, flaggor och logiska uttryck.<br>
**D)** Det stämmer faktiskt — George Boole är upphovsmannen, och typen är uppkallad efter honom. Men det är inte svaret på vad en `bool` *är*.

</details>

---
