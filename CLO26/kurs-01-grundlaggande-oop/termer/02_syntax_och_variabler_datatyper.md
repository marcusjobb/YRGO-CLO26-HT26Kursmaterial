# Programmeringstermer — Datatyper

I den här kursen använder vi fem typer som täcker det mesta du stöter på: `int`, `double`, `string`, `bool` och `char`. De är valda för att de täcker heltal, decimaltal, text, ja/nej och enskilda tecken — det räcker väldigt långt.

| Typ | Vad den lagrar | Exempel |
|-----|---------------|---------|
| `int` | Heltal | `42`, `-7`, `0` |
| `double` | Decimaltal | `3.14`, `-0.5` |
| `string` | Text (en eller flera tecken) | `"Hej"`, `"Anna"` |
| `bool` | Sant eller falskt | `true`, `false` |
| `char` | Exakt ett tecken | `'A'`, `'!'`, `'3'` |

**Se även:** [variabler.md](variabler.md) — hur du deklarerar och namnger variabler, [strangar.md](strangar.md) — metoder som hör till `string`.

---

## int

**Standardvalet för heltal.** Rymmer hela tal från -2 147 483 648 till 2 147 483 647 — det räcker för poäng, åldrar, antal, och det mesta du räknar med i vardaglig kod.

```csharp
int ålder = 25;
int antalStuderande = 30;
int minusGrader = -12;

Console.WriteLine(ålder);
Console.WriteLine(antalStuderande);
Console.WriteLine(minusGrader);
```

### Output
```plaintext
25
30
-12
```

Använd `int` när du vet att talet aldrig kommer ha decimaler — åldrar, antal saker, poäng. Behöver du decimaler, välj `double`.

**Se även:** [aritmetik.md](aritmetik.md) — vad som händer när du delar två `int` med varandra.

---

## double

**Standardvalet för decimaltal.** Namnet kommer från "double precision" — det är dubbelt så precist som `float`, en äldre och sämre variant som vi inte använder. Välj `double` varje gång du behöver decimaler.

```csharp
double pris = 49.90;
double temperatur = -3.5;
double pi = 3.14159;

Console.WriteLine(pris);
Console.WriteLine(temperatur);
Console.WriteLine(pi);
```

### Output
```plaintext
49.9
-3.5
3.14159
```

<details><summary>Varför 49.9 och inte 49.90?</summary>

C# skriver inte ut en avslutande nolla — `49.90` och `49.9` är samma tal. Om du vill ha en specifik utskrift med två decimaler, formatera det: `Console.WriteLine(pris.ToString("F2"))` ger `49.90`. Det tar vi upp mer i strängformatering.

</details>

<details><summary>Är double alltid exakt?</summary>

Nej — och det är viktigt att veta. Datorn lagrar `double` i binärt format, och vissa decimaltal (som `0.1`) kan inte representeras exakt i binärt. Det kan ge konstiga svar:

```csharp
double a = 0.1;
double b = 0.2;
Console.WriteLine(a + b);
```

### Output
```plaintext
0.30000000000000004
```

Det är inte ett fel i din kod — det är ett välkänt beteende hos binär aritmetik. För pengar ska du använda `decimal` istället (mer om det i en senare kurs). För matematikuppgifter och simuleringar är `double` helt rätt.

</details>

---

## string

Text. En `string` är tekniskt sett en sekvens av `char`-tecken, men du behöver inte tänka på det — du skriver bara texten inom dubbla citattecken.

```csharp
string namn = "Anna";
string hälsning = "Hej, välkommen!";
string tom = "";

Console.WriteLine(namn);
Console.WriteLine(hälsning);
Console.WriteLine(tom.Length); // antal tecken i den tomma strängen
```

### Output
```plaintext
Anna
Hej, välkommen!
0
```

Till skillnad från `int`, `double`, `bool` och `char` är `string` en **referenstyp** — inte en värdetyp. Den detaljen spelar ingen praktisk roll än, men det förklarar varför `string` beter sig lite annorlunda i jämförelser längre fram i kursen.

**Se även:** [strangar.md](strangar.md) — hela verktygslådan av strängmetoder (`Trim`, `Replace`, `Split`...).

---

## bool

Den enklaste typen — antingen `true` (sant) eller `false` (falskt). Inget däremellan.

```csharp
bool ärTillgänglig = true;
bool harBeställt = false;

Console.WriteLine(ärTillgänglig);
Console.WriteLine(harBeställt);
```

### Output
```plaintext
True
False
```

Lägg märke till stor bokstav i outputen (`True`, `False`) — C# skriver ut bool-värden med versal, även om du skriver `true`/`false` med gemen i koden.

`bool` används hela tiden i villkorssatser och loopar: `if (ärTillgänglig)` är mer läsbar kod än `if (ärTillgänglig == true)` — det sistnämnda är onödigt, variabeln är ju redan en bool.

**Se även:** [bool.md](bool.md) — jämförelseoperatorer, logiska operatorer och hur bool används i villkor.

---

## char

Exakt ett tecken — bokstav, siffertecken, symbol eller mellanslag. Skrivs inom **enkla** citattecken. Det är skillnaden mot `string`: `"A"` är en sträng med ett tecken, `'A'` är ett enskilt `char`-värde.

```csharp
char bokstav = 'A';
char siffra = '7';
char mellanslag = ' ';

Console.WriteLine(bokstav);
Console.WriteLine(siffra);
Console.WriteLine((int)bokstav); // teckenkoden bakom 'A'
```

### Output
```plaintext
A
7
65
```

<details><summary>Varför blev 'A' plötsligt 65?</summary>

Varje tecken har ett nummer bakom kulisserna — en teckenkod ur standarden Unicode (en utbyggnad av den äldre ASCII-standarden). `'A'` är 65, `'B'` är 66, `'a'` är 97 (litet a är ett helt annat nummer än stort A), och `'0'` är 48. `char` är i grunden ett heltal som C# väljer att *visa* som ett tecken. Castet `(int)bokstav` plockar fram det underliggande talet.

Det är också därför `"A" == "a"` är `false` — de är bokstavligen olika tal under ytan.

</details>

**Se även:** [strangar.md](strangar.md) — en `string` är en sekvens av `char`, vilket förklarar varför `namn[0]` ger dig den första bokstaven.
