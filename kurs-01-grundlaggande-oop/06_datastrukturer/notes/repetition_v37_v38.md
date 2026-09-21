# Repetition — variabler, strängar och klasser

Snabbrepetition från lektion v39 av det vi gick igenom under v37–v38.  
Loopar togs upp i ett separat block — se `03_villkor_och_loopar/notes/loopar.md`.

---

## Numeriska typer

C# har flera heltalstyper. Skillnaden är hur stora tal de kan hålla:

```csharp
short number = 100;    // −32 768 till 32 767
int svaret   = 42;     // −2 miljarder till 2 miljarder (standard)
long storTal = 1337;   // ungefär ±9 × 10¹⁸
```

**Tumregel:** Använd `int` om du inte har skäl att välja något annat.  
`short` sparar lite minne. `long` behövs för mycket stora värden (tidsstämplar, id:n i stora system).

Decimaltal finns i tre smaker:

```csharp
float   decimaltal = 5.44f;  // notera f-suffixet — krävs av kompilatorn
double  trouble    = 4.43;   // standard för decimaltal
decimal money      = 500;    // högst precision, används för pengar
```

| Typ | Precision | Typisk användning |
|-----|-----------|-------------------|
| `float` | ~7 signifikanta siffror | grafik, spel — när prestanda väger mer än precision |
| `double` | ~15 signifikanta siffror | vetenskapliga beräkningar |
| `decimal` | ~28 signifikanta siffror | pengar, finansiella beräkningar |

---

## string och char

```csharp
string name   = "            Jonathan Harker       "; // en sekvens av tecken
char   symbol = '*';                                  // ett enda tecken, enkelfnuttar
```

En `string` är egentligen en samling `char`-värden. `name[0]` är det första tecknet.

---

## String-metoder

`string` har en rad inbyggda metoder. Viktigt att komma ihåg: **strängar är oföränderliga** — metoderna returnerar en ny sträng, de ändrar inte originalet.

**Trimma bort mellanslag:**

```csharp
name.TrimStart()  // tar bort mellanslag i början
name.TrimEnd()    // tar bort mellanslag i slutet
name.Trim()       // tar bort båda
```

```csharp
name = name.Trim(); // för att spara resultatet måste vi tilldela tillbaka
```

**Ändra skiftläge:**

```csharp
name.ToUpper()  // "JONATHAN HARKER"
name.ToLower()  // "jonathan harker"
```

**Söka i en sträng:**

```csharp
name.Contains('k')  // true/false — finns tecknet?
name.IndexOf('k')   // vilket index har tecknet? (-1 om det inte finns)
```

**Plocka ut en del:**

```csharp
name.Substring(3, 4)  // börja på index 3, läs 4 tecken
```

**Ersätta tecken eller text:**

```csharp
name.Replace('a', 'e')  // ersätt alla 'a' med 'e'
```

**Manuell teckenbyte (utan Replace):**

```csharp
name = name.Substring(0, 11) + 'x' + name.Substring(12);
```

Här plockar vi ut allt *före* index 11, sätter in `'x'`, och lägger till allt *från* index 12. Lite som att klippa och klistra i en sträng. `name[12] = 'x'` fungerar inte — strängar är oföränderliga.

---

## Klass-terminologi

Under repetitionen gick vi igenom hur vi namnger och kategoriserar klasser:

| Typ | Vad det är | Exempel |
|-----|-----------|---------|
| **Program** | Startpunkt med `Main()` | `Program.cs` |
| **Helper** | Metoder som andra klasser använder — ingen "intelligens" av sig själv | `StringHelper` |
| **DTO** | Data Transfer Object — håller information, minimal logik | `Person` med namn, efternamn, telefon |
| **Datatyp** | Klass med funktionalitet för en viss sorts data | `Kilo kg = new Kilo();` |
| **Modell** | Klass som representerar något i verkligheten | `Car volvo = new Car();` |

En **Helper** används av andra — den innehåller återanvändbara metoder men har inget eget tillstånd.  
En **DTO** är i praktiken ett paket med data — den innehåller properties men sällan metoder med logik.  
Skiljelinjen mellan DTO och Modell är flytande i vardagen, men modellen är mer kopplad till ett verkligt koncept med beteende.

---

## Hela koden från lektionen

```csharp
short  number      = 100;
int    svaret      = 42;
long   storTal     = 1337;
float  decimaltal  = 5.44f;
double trouble     = 4.43;
decimal money      = 500;
string name        = "            Jonathan Harker       ";
char   symbol      = '*';

Console.WriteLine($"Hello, {name}!");
Console.WriteLine($"{name.Length}");
Console.WriteLine($"{name.TrimStart()}");
Console.WriteLine($"{name.TrimStart().Length}");
Console.WriteLine($"{name.TrimEnd() + "!"}");
Console.WriteLine($"{name.Trim().Length}");

name = name.Trim();
Console.WriteLine(name.ToUpper());
Console.WriteLine(name.ToLower());

Console.WriteLine(name.Contains('k'));
Console.WriteLine(name.IndexOf('k'));

name = name.Substring(0, 11) + 'x' + name.Substring(12);
Console.WriteLine(name);
Console.WriteLine(name.Substring(3, 4));

Console.WriteLine(name.Replace('a', 'e'));
```
