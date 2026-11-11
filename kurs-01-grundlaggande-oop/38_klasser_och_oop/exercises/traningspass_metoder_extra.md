# Träningspass — Metoder (extra)

🟢 Grundläggande → 🟡 Mellannivå

> Mål: fler metoder med returvärden — bygga automatiken.  
> Fastnar du mer än 10 minuter? Fråga grannen. Sedan AI. Sedan Marcus.

---

## M8 — Celsius och Fahrenheit 🟢

Skriv två konverteringsmetoder:

```csharp
static double TillFahrenheit(double celsius)   // celsius * 9/5 + 32
static double TillCelsius(double fahrenheit)   // (fahrenheit - 32) * 5/9
```

**I Main:**
```csharp
Console.WriteLine(TillFahrenheit(0));
Console.WriteLine(TillFahrenheit(100));
Console.WriteLine(TillCelsius(32));
Console.WriteLine(TillCelsius(212));
```

**Förväntad output:**
```
32
212
0
100
```

---

## M9 — Trafikljuset 🟢

Skriv metoden:
```csharp
static string Trafikljus(int poäng)
```

| Poäng | Färg |
|-------|------|
| 80–100 | "grön" |
| 50–79 | "gul" |
| 0–49 | "röd" |

**I Main:**
```csharp
Console.WriteLine(Trafikljus(92));
Console.WriteLine(Trafikljus(65));
Console.WriteLine(Trafikljus(30));
```

**Förväntad output:**
```
grön
gul
röd
```

---

## M10 — Räkna tecknet 🟢

Skriv metoden:
```csharp
static int RäknaTecken(string text, char tecken)
```

Returnerar hur många gånger `tecken` förekommer i `text` — case-insensitive.

**I Main:**
```csharp
Console.WriteLine(RäknaTecken("Hej världen", 'e'));
Console.WriteLine(RäknaTecken("Mississippi", 's'));
Console.WriteLine(RäknaTecken("AABABBA", 'a'));
```

**Förväntad output:**
```
2
4
4
```

Tips: `char.ToLower(c)` konverterar ett tecken till lowercase.

---

## M11 — Siffersumman 🟡

Skriv metoden:
```csharp
static int Siffersumma(int tal)
```

Returnerar summan av alla siffror i `tal`. Ignorera eventuellt minustecken.

**I Main:**
```csharp
Console.WriteLine(Siffersumma(1234));
Console.WriteLine(Siffersumma(99));
Console.WriteLine(Siffersumma(-567));
```

**Förväntad output:**
```
10
18
18
```

Tips: `Math.Abs(tal).ToString()` ger siffrorna som en sträng du kan loopa igenom.

---

## M12 — Primtal 🟡

Skriv metoden:
```csharp
static bool ÄrPrimtal(int tal)
```

Returnerar `true` om `tal` är ett primtal (delbart bara med 1 och sig självt).  
Tal mindre än 2 är inte primtal.

**I Main:**
```csharp
Console.WriteLine(ÄrPrimtal(2));
Console.WriteLine(ÄrPrimtal(7));
Console.WriteLine(ÄrPrimtal(9));
Console.WriteLine(ÄrPrimtal(1));
```

**Förväntad output:**
```
True
True
False
False
```

Tips: loopa från 2 till `Math.Sqrt(tal)` och kolla om något tal delar jämnt (`tal % i == 0`).

---
