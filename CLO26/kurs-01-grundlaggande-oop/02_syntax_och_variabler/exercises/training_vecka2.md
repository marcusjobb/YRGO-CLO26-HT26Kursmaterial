# Träningsuppgifter: Vecka 2 — C# Repetition

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad händer när du skriver `int ålder = 25;` i C#?

a. En text sträng "25" sparas i minnet<br>
b. C# bokar en minnesplats som heter `ålder` och lägger in värdet 25<br>
c. Ingenting — koden måste köras först<br>
d. Den skriver ut 25 på skärmen

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** C# bokar en minnesplats som heter `ålder` och lägger in värdet 25

  **Förklaringar:**

  - ❌ **a) Textsträng** - FEL: int är ett heltal, inte text. Hade `string` använts hade det varit en sträng
  - ✅ **b) Bokar minnesplats** - **RÄTT**: Deklaration + tilldelning i ett steg. C# reserverar plats för ett heltal och döper den till `ålder`
  - ❌ **c) Ingenting** - FEL: Detta är deklaration och tilldelning på samma gång — C# gör jobbet direkt
  - ❌ **d) Skriver ut** - FEL: `int ålder = 25` skriver inte ut något. För utskrift krävs `Console.WriteLine()`
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vilken datatyp ska du använda för ett bankkonto med decimaler?

a. int<br>
b. bool<br>
c. double<br>
d. string

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** double

  **Förklaringar:**

  - ❌ **a) int** - FEL: int kan bara lagra heltal. 99.50 skulle trunkeras till 99
  - ❌ **b) bool** - FEL: bool lagrar bara `true` eller `false`
  - ✅ **c) double** - **RÄTT**: double hanterar decimaltal som 99.50, 3.14, eller momssatser som 0.25
  - ❌ **d) string** - FEL: string lagrar text. Du kan skriva "99.50" men då kan du inte räkna med det
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad blir utskriften?

```csharp
int a = 7;
int b = 2;
int resultat = a / b;
Console.WriteLine(resultat);
```

a. 3.5<br>
b. 3<br>
c. 3.0<br>
d. 1

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** 3

  **Förklaringar:**

  - ❌ **a) 3.5** - FEL: När båda värdena är `int` utförs heltalsdivision. Decimaldelen kapas
  - ✅ **b) 3** - **RÄTT**: `7 / 2` med int → 3 eftersom C# trunkerar decimaldelen. För 3.5 måste du använda `double`
  - ❌ **c) 3.0** - FEL: Eftersom `resultat` är `int` lagras värdet som 3, inte 3.0
  - ❌ **d) 1** - FEL: 1 är resten, inte kvoten. Resten får du med `%`-operatorn: `7 % 2` = 1
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är skillnaden mellan `=` och `==` i C#?

a. Ingen skillnad — de gör samma sak<br>
b. `=` tilldelar ett värde, `==` jämför två värden<br>
c. `=` jämför, `==` tilldelar<br>
d. `=` används bara för text, `==` för siffror

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `=` tilldelar ett värde, `==` jämför två värden

  **Förklaringar:**

  - ❌ **a) Samma sak** - FEL: De är helt olika. Att blanda ihop dem är en av de vanligaste buggarna
  - ✅ **b) = tilldelar, == jämför** - **RÄTT**: `int x = 5` betyder "lägg 5 i x". `if (x == 5)` betyder "är x lika med 5?"
  - ❌ **c) Tvärtom** - FEL: Omvändningen stämmer inte
  - ❌ **d) Text vs siffror** - FEL: `=` och `==` funkar likadant oavsett datatyp
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad skriver koden ut?

```csharp
int poäng = 72;
if (poäng >= 90)
    Console.WriteLine("A");
else if (poäng >= 70)
    Console.WriteLine("B");
else if (poäng >= 50)
    Console.WriteLine("C");
else
    Console.WriteLine("F");
```

a. A<br>b. B<br>c. C<br>d. F

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** B

  **Förklaringar:**

  - ❌ **a) A** - FEL: 72 är inte >= 90, så första villkoret hoppas över
  - ✅ **b) B** - **RÄTT**: 72 >= 70 är sant, så B skrivs ut. C# testar uppifrån och ner och kör första matchningen
  - ❌ **c) C** - FEL: Även om 72 >= 50 också är sant, hinner loopen aldrig dit — den första matchningen (B) körs och resten hoppas över
  - ❌ **d) F** - FEL: else-körs bara om INGET villkor är sant
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad betyder `&&` i ett villkor?

a. Minst ett villkor måste vara sant<br>
b. Båda villkoren måste vara sanna<br>
c. Inget av villkoren får vara sant<br>
d. Villkoren måste vara olika

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Båda villkoren måste vara sanna

  **Förklaringar:**

  - ❌ **a) Minst ett sant** - FEL: Det är `||` (eller)
  - ✅ **b) Båda måste vara sanna** - **RÄTT**: `if (ålder >= 18 && harID)` — du måste vara både myndig OCH ha legitimation
  - ❌ **c) Inget får vara sant** - FEL: Det är `!` (not) i kombination med `||`
  - ❌ **d) Olika** - FEL: `&&` handlar om båda är sanna, inte om de är olika
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad gör en `for`-loop?

a. Kör kod så länge ett villkor är sant, med räknare<br>
b. Kör kod precis en gång<br>
c. Väntar på användarens input<br>
d. Loopar för evigt

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Kör kod så länge ett villkor är sant, med räknare

  **Förklaringar:**

  - ✅ **a) Kör med räknare** - **RÄTT**: `for (int i = 0; i < 10; i++)` — startvärde, villkor, steg. Perfekt när du vet antalet varv
  - ❌ **b) En gång** - FEL: En loop upprepar, en if-sats körs en gång
  - ❌ **c) Väntar på input** - FEL: `Console.ReadLine()` väntar på input, inte en loop
  - ❌ **d) För evigt** - FEL: En for-loop tar slut när villkoret blir falskt. (Om du inte glömmer uppdatera räknaren!)
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vilken loop är bäst när du vill gå igenom alla element i en array eller lista?

a. while<br>b. for<br>c. foreach<br>d. do-while

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** foreach

  **Förklaringar:**

  - ❌ **a) while** - FEL: while kräver att du själv håller koll på index, onödigt omständligt
  - ❌ **b) for** - FEL: Fungerar, men foreach är tydligare när du inte behöver indexet
  - ✅ **c) foreach** - **RÄTT**: `foreach (string dag in veckodagar)` — du behöver inte hålla koll på räknare eller index. Koden läses som en mening
  - ❌ **d) do-while** - FEL: do-while kör minst en gång och används för helt andra situationer
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad gör `return` i en metod?

a. Startar om metoden<br>b. Skickar ett värde tillbaka till den som anropade metoden<br>c. Skriver ut ett värde på skärmen<br>d. Stänger av programmet

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Skickar ett värde tillbaka till den som anropade metoden

  **Förklaringar:**

  - ❌ **a) Startar om** - FEL: `return` avslutar metoden, den startar inte om den
  - ✅ **b) Skickar tillbaka ett värde** - **RÄTT**: `int summa = Addera(3, 5)` — `return` i `Addera` skickar 8 tillbaka så det hamnar i variabeln `summa`
  - ❌ **c) Skriver ut** - FEL: `Console.WriteLine()` skriver ut. `return` returnerar data till koden som anropade metoden
  - ❌ **d) Stänger av** - FEL: `return` avslutar bara metoden, inte hela programmet
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vilket metodnamn följer C#-konventionen (PascalCase)?

a. skrivHälsning<br>b. SkrivHälsning<br>c. skriv_hälsning<br>d. SKRIVHÄLSNING

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** SkrivHälsning

  **Förklaringar:**

  - ❌ **a) skrivHälsning** - FEL: camelCase används för variabler, inte metoder
  - ✅ **b) SkrivHälsning** - **RÄTT**: Metoder i C# namnges med PascalCase — första bokstaven i varje ord är stor
  - ❌ **c) skriv_hälsning** - FEL: snake_case används i Python, inte C#
  - ❌ **d) SKRIVHÄLSNING** - FEL: Alla versaler används för konstanter, inte metoder
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 11

Vad är en parameter i en metod?

a. En variabel som metoden tar emot som indata<br>b. Det värde metoden returnerar<br>c. Metodens namn<br>d. En global variabel

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En variabel som metoden tar emot som indata

  **Förklaringar:**

  - ✅ **a) Indata till metoden** - **RÄTT**: `static void SkrivHälsning(string namn)` — `namn` är en parameter. När du anropar `SkrivHälsning("Alex")` är "Alex" argumentet
  - ❌ **b) Returvärde** - FEL: Returvärdet är det som kommer *ut* ur metoden med `return`
  - ❌ **c) Metodens namn** - FEL: Namnet är det du skriver före parentesen, t.ex. `SkrivHälsning`
  - ❌ **d) Global variabel** - FEL: Parametrar är lokala för metoden, inte globala
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 12

Vad skriver koden ut?

```csharp
static bool ÄrMyndig(int ålder)
{
    return ålder >= 18;
}

Console.WriteLine(ÄrMyndig(20));
```

a. 20<br>b. True<br>c. 18<br>d. Myndig

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** True

  **Förklaringar:**

  - ❌ **a) 20** - FEL: Metoden returnerar ett bool-värde (sant/falskt), inte åldern
  - ✅ **b) True** - **RÄTT**: `ÄrMyndig(20)` returnerar `true` eftersom 20 >= 18. `Console.WriteLine` skriver ut "True"
  - ❌ **c) 18** - FEL: Siffran 18 syns inte i utskriften, den används bara i jämförelsen
  - ❌ **d) Myndig** - FEL: Metoden returnerar `true`, inte texten "Myndig"
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
