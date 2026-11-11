# Träningspass — Metoder

🟢 Grundläggande repetition

> Mål: bygga muskelminne kring metoder — parametrar, returvärden och void.  
> Fastnar du mer än 10 minuter? Fråga grannen. Sedan AI. Sedan Marcus.

---

## M1 — Hälsa rätt

Skriv tre metoder i `Program`-klassen (eller en hjälpklass):

```csharp
static string HälsaFormellt(string namn)   // "God dag, [namn]."
static string HälsaLedigt(string namn)     // "Tjena [namn]!"
static string HälsaNeutralt(string namn)   // "Hej, [namn]."
```

**I Main:**
```csharp
Console.WriteLine(HälsaFormellt("Svensson"));
Console.WriteLine(HälsaLedigt("Alex"));
Console.WriteLine(HälsaNeutralt("Kim"));
```

**Förväntad output:**
```
God dag, Svensson.
Tjena Alex!
Hej, Kim.
```

---

## M2 — Räknaren

Skriv fyra metoder som tar två doubles och returnerar ett resultat:

```csharp
static double Addera(double a, double b)
static double Subtrahera(double a, double b)
static double Multiplicera(double a, double b)
static double Dividera(double a, double b)   // returnera 0 om b == 0
```

**I Main:**
```csharp
Console.WriteLine(Addera(10, 3));
Console.WriteLine(Subtrahera(10, 3));
Console.WriteLine(Multiplicera(10, 3));
Console.WriteLine(Dividera(10, 3));
Console.WriteLine(Dividera(10, 0));
```

**Förväntad output:**
```
13
7
30
3,333...
0
```

---

## M3 — Störst och minst

Skriv metoder som tar en `int[]` och returnerar max respektive min:

```csharp
static int Störst(int[] tal)
static int Minst(int[] tal)
```

**I Main:**
```csharp
int[] tal = { 4, 17, 2, 99, 8, 43 };
Console.WriteLine($"Störst: {Störst(tal)}");
Console.WriteLine($"Minst: {Minst(tal)}");
```

**Förväntad output:**
```
Störst: 99
Minst: 2
```

---

## M4 — Lösenordskollen

Skriv metoden:
```csharp
static bool GiltigtLösenord(string lösenord)
```

Returnerar `true` om lösenordet:
- Är minst 8 tecken långt
- Innehåller minst en siffra

Annars `false`.

**I Main:**
```csharp
Console.WriteLine(GiltigtLösenord("abc123"));       // false — för kort
Console.WriteLine(GiltigtLösenord("abcdefgh"));     // false — ingen siffra
Console.WriteLine(GiltigtLösenord("abcdefg1"));     // true
```

**Förväntad output:**
```
False
False
True
```

Tips: `lösenord.Any(char.IsDigit)` kollar om någon bokstav är en siffra.

---

## M5 — Poängbetyget

Skriv metoden:
```csharp
static string Betyg(int poäng)
```

| Poäng | Betyg |
|-------|-------|
| 90–100 | "A" |
| 75–89 | "B" |
| 60–74 | "C" |
| 50–59 | "D" |
| 0–49 | "F" |

**I Main:**
```csharp
Console.WriteLine(Betyg(95));
Console.WriteLine(Betyg(72));
Console.WriteLine(Betyg(45));
```

**Förväntad output:**
```
A
C
F
```

---

## M6 — Palindrom

Skriv metoden:
```csharp
static bool ÄrPalindrom(string ord)
```

Returnerar `true` om ordet är likadant baklänges.

Tips: `new string(ord.Reverse().ToArray())` vänder en sträng.

**I Main:**
```csharp
Console.WriteLine(ÄrPalindrom("racecar"));
Console.WriteLine(ÄrPalindrom("hej"));
Console.WriteLine(ÄrPalindrom("level"));
```

**Förväntad output:**
```
True
False
True
```

---

## M7 — Räkna vokaler

Skriv metoden:
```csharp
static int AntalVokaler(string text)
```

Räknar antal vokaler (a, e, i, o, u, å, ä, ö) — case-insensitive.

**I Main:**
```csharp
Console.WriteLine(AntalVokaler("Hej världen"));
Console.WriteLine(AntalVokaler("rhythm"));
```

**Förväntad output:**
```
3
0
```

---
