# Träningspass — Klasser och objekt

🟢 Grundläggande repetition

> Mål: bygga muskelminne. Samma mönster, ny kontext varje gång.  
> Fastnar du mer än 10 minuter? Fråga grannen. Sedan AI. Sedan Marcus.

---

## K1 — Boken

Skapa klassen `Bok` med:
- Properties: `Titel` (string), `Författare` (string), `Sidor` (int)
- Konstruktor som tar alla tre
- Metod `Presentera()` — skriver ut `"[Titel] av [Författare] ([Sidor] sidor)"`

**I Main:**
```csharp
Bok b = new Bok("Sagan om ringen", "Tolkien", 1200);
b.Presentera();
```

**Förväntad output:**
```
Sagan om ringen av Tolkien (1200 sidor)
```

---

## K2 — Temperaturen

Skapa klassen `Temperatur` med:
- Property: `Grader` (double, publik get, privat set)
- Konstruktor som tar `grader`
- Metod `TillFahrenheit()` — returnerar `Grader * 9 / 5 + 32`
- Metod `Presentera()` — skriver ut `"[Grader]°C = [fahrenheit]°F"`

**I Main:**
```csharp
Temperatur t = new Temperatur(100);
t.Presentera();
```

**Förväntad output:**
```
100°C = 212°F
```

---

## K3 — Rektangeln

Skapa klassen `Rektangel` med:
- Properties: `Bredd` (double), `Höjd` (double)
- Konstruktor som tar bredd och höjd
- Metod `Area()` — returnerar `Bredd * Höjd`
- Metod `Omkrets()` — returnerar `2 * (Bredd + Höjd)`
- Metod `Presentera()` — skriver ut area och omkrets

**I Main:**
```csharp
Rektangel r = new Rektangel(5, 3);
r.Presentera();
```

**Förväntad output:**
```
Area: 15
Omkrets: 16
```

---

## K4 — Kontot

Skapa klassen `Konto` med:
- Property: `Saldo` (double, publik get, privat set)
- Property: `Ägare` (string, publik get, privat set)
- Konstruktor som tar `ägare` och `startSaldo`
- Metod `SättIn(double belopp)` — lägger till om belopp > 0
- Metod `TaUt(double belopp)` — drar av om belopp > 0 och saldo räcker, annars inget
- Metod `Presentera()` — skriver ut ägare och saldo

**I Main:**
```csharp
Konto k = new Konto("Alex", 1000);
k.SättIn(500);
k.TaUt(200);
k.Presentera();
```

**Förväntad output:**
```
Alex: 1300 kr
```

---

## K5 — Spelaren

Skapa klassen `Spelare` med:
- Properties: `Namn` (string), `Tröjnummer` (int), `Mål` (int)
- Konstruktor som tar namn och tröjnummer (Mål börjar på 0)
- Metod `GörMål()` — ökar Mål med 1
- Metod `Presentera()` — skriver ut `"#[nr] [namn] — [mål] mål"`

**I Main:**
```csharp
Spelare s = new Spelare("Haaland", 9);
s.GörMål();
s.GörMål();
s.GörMål();
s.Presentera();
```

**Förväntad output:**
```
#9 Haaland — 3 mål
```

---

## K6 — Lampan

Skapa klassen `Lampa` med:
- Property: `ÄrPå` (bool, publik get, privat set) — börjar som false
- Property: `Namn` (string)
- Konstruktor som tar namn
- Metod `TändSläck()` — växlar ÄrPå mellan true/false
- Metod `Status()` — skriver ut `"[namn]: på"` eller `"[namn]: av"`

**I Main:**
```csharp
Lampa l = new Lampa("Taklampa");
l.Status();
l.TändSläck();
l.Status();
l.TändSläck();
l.Status();
```

**Förväntad output:**
```
Taklampa: av
Taklampa: på
Taklampa: av
```

---

## K7 — Varukorgen

Skapa klassen `Varukorg` med:
- Privat `List<string> _varor`
- Konstruktor som skapar en tom lista
- Metod `LäggI(string vara)` — lägger till en vara
- Metod `TaBort(string vara)` — tar bort om den finns
- Metod `SkrivUt()` — skriver ut alla varor, en per rad

**I Main:**
```csharp
Varukorg v = new Varukorg();
v.LäggI("Mjölk");
v.LäggI("Bröd");
v.LäggI("Ägg");
v.TaBort("Bröd");
v.SkrivUt();
```

**Förväntad output:**
```
Mjölk
Ägg
```

---
