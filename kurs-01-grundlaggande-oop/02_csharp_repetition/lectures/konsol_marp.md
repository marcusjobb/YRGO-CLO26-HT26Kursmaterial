---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Konsolen och strängar

### Skriva ut, läsa in och bygga text

_Kurs 01 · Vecka 2 · Nion Education_

---

## Console.WriteLine — skriv en rad

Den enklaste utskriften: skriv något och hoppa till nästa rad.

```csharp
Console.WriteLine("Hej världen!");
Console.WriteLine("Programmet är igång.");
Console.WriteLine(42);
Console.WriteLine(3.14);
```

```
Hej världen!
Programmet är igång.
42
3.14
```

`Console.WriteLine` skriver ut **vad som helst** — text, tal, sant/falskt.
Varje anrop ger en ny rad.

---

## Console.Write — skriv utan radbrytning

`Write` (utan `Line`) stannar kvar på samma rad.

```csharp
Console.Write("Hej ");
Console.Write("du ");
Console.WriteLine("där!");
```

```
Hej du där!
```

Bra när du vill bygga ihop en rad i flera steg.

---

## Stränginterpolation — $"..."

Det smidigaste sättet att blanda text och variabler.
Sätt ett `$` framför strängen och skriv variabeln inuti `{}`.

```csharp
string namn = "Alex";
int ålder = 22;

Console.WriteLine($"Hej {namn}!");
Console.WriteLine($"{namn} är {ålder} år gammal.");
Console.WriteLine($"Om 10 år är {namn} {ålder + 10} år.");
```

```
Hej Alex!
Alex är 22 år gammal.
Om 10 år är Alex 32 år.
```

Du kan räkna direkt inuti `{}` — C# evaluerar uttrycket åt dig.

---

## Konkatenering med +

Ett äldre sätt: sätt ihop strängar med `+`.

```csharp
string namn = "Alex";
int ålder = 22;

Console.WriteLine("Hej " + namn + "!");
Console.WriteLine(namn + " är " + ålder + " år gammal.");
```

Det fungerar, men interpolation är tydligare.
Välj `$"..."` — det är standard i modern C#.

> 💬 _"Interpolation läses som en mening. Konkatenering läses som ett pussel."_

---

## Console.ReadLine — läs ett svar

`ReadLine` väntar tills användaren skriver något och trycker Enter.
Svaret kommer tillbaka som en `string`.

```csharp
Console.Write("Vad heter du? ");
string namn = Console.ReadLine();
Console.WriteLine($"Hej {namn}!");
```

```
Vad heter du? Alex
Hej Alex!
```

`ReadLine` returnerar alltid en `string` —
även om användaren skriver ett tal är det text tills du konverterar det.

---

## Konvertera strängar till tal

`int.Parse` och `double.Parse` omvandlar text till tal.

```csharp
Console.Write("Hur gammal är du? ");
string inmatning = Console.ReadLine();
int ålder = int.Parse(inmatning);

Console.WriteLine($"Om 10 år är du {ålder + 10} år.");
```

```
Hur gammal är du? 22
Om 10 år är du 32 år.
```

Skriver användaren något som inte är ett tal kraschar programmet —
mer om det när vi pratar om felhantering.

---

## Formatering — tal i utskriften

Vill du styra hur ett tal skrivs ut?

```csharp
double pris = 1234.5;
double procent = 0.1575;

Console.WriteLine($"Pris: {pris:N2} kr");       // tusentalsavskiljare, 2 decimaler
Console.WriteLine($"Moms: {procent:P1}");        // procent, 1 decimal
Console.WriteLine($"Pris: {pris:C}");            // valutaformat (systemets locale)
```

```
Pris: 1 234,50 kr
Moms: 15,8%
Pris: 1 234,50 kr
```

Format-koder skrivs efter `:` inuti `{}`.
`N2` = Number med 2 decimaler, `P1` = Percent med 1 decimal.

---

## Escape-tecken

Vissa tecken har speciell betydelse i strängar.
Backslash `\` inleder ett escape-tecken.

| Kod | Effekt |
|-----|--------|
| `\n` | Ny rad |
| `\t` | Tab |
| `\"` | Citattecken inuti en sträng |
| `\\` | Backslash |

```csharp
Console.WriteLine("Rad 1\nRad 2\nRad 3");
Console.WriteLine("Hon sa:\t\"Hej!\"");
```

```
Rad 1
Rad 2
Rad 3
Hon sa:	"Hej!"
```

---

## Allt tillsammans

```csharp
Console.Write("Vad heter du? ");
string namn = Console.ReadLine();

Console.Write("Hur gammal är du? ");
int ålder = int.Parse(Console.ReadLine());

Console.WriteLine();
Console.WriteLine($"Hej {namn}!");
Console.WriteLine($"Du är {ålder} år gammal.");
Console.WriteLine($"Om 10 år är du {ålder + 10} år.");
```

```
Vad heter du? Alex
Hur gammal är du? 22

Hej Alex!
Du är 22 år gammal.
Om 10 år är du 32 år.
```

---

<!-- _class: title -->

# Nu är det din tur

### Övningar finns i `exercises/`

🟢 `variables.md` — deklarera och skriv ut variabler  
🟢 `mystic_string.md` — bygg strängar med interpolation  
🟡 `console_gui.md` — gör ett snyggt gränssnitt i konsolen

_Läs inmatning från användaren i minst en övning._
