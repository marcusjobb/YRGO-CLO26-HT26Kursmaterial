# Träningspass — Arv

🟢 Grundläggande repetition

> Samma mönster: basklass → subklass → override. Ny kontext varje gång.  
> Fastnar du mer än 10 minuter? Fråga grannen. Sedan AI. Sedan Marcus.

---

## A1 — Fordon

Basklass `Fordon`:
- Property: `Märke` (string)
- Konstruktor tar märke
- Virtual metod `Beskriv()` — skriver ut `"Fordon: [Märke]"`

Subklasser `Bil` och `Motorcykel`:
- `Bil.Beskriv()` — skriver ut `"Bil: [Märke]"`
- `Motorcykel.Beskriv()` — skriver ut `"Motorcykel: [Märke]"`

**I Main:**
```csharp
Fordon[] fordon = { new Bil("Volvo"), new Motorcykel("Harley") };
foreach (var f in fordon) f.Beskriv();
```

**Förväntad output:**
```
Bil: Volvo
Motorcykel: Harley
```

---

## A2 — Anställda

Basklass `Anställd`:
- Properties: `Namn` (string), `Lön` (double)
- Konstruktor tar namn och lön
- Virtual metod `Presentera()` — skriver ut `"[Namn], lön: [Lön] kr"`

Subklass `Chef` — extra property `Bonus` (double):
- Konstruktor tar namn, lön och bonus
- Override `Presentera()` — skriver ut namn, lön + bonus

Subklass `Konsult` — extra property `TimLön` (double):
- Konstruktor tar namn och timlön (Lön = 0)
- Override `Presentera()` — skriver ut namn och timlön

**Förväntad output:**
```
Anna, lön: 45000 kr
Erik, lön: 80000 kr + 15000 kr bonus
Kim, timlön: 950 kr
```

---

## A3 — Former

Basklass `Form`:
- Property: `Färg` (string)
- Konstruktor tar färg
- Virtual metod `Area()` — returnerar 0.0
- Metod `Presentera()` — skriver ut `"[klassnamn] ([Färg]): area = [Area()]"`

Subklass `Cirkel`:
- Extra property: `Radie` (double)
- Override `Area()` — returnerar `Math.PI * Radie * Radie`

Subklass `Rektangel`:
- Extra properties: `Bredd`, `Höjd` (double)
- Override `Area()` — returnerar `Bredd * Höjd`

**I Main:**
```csharp
Form[] former = { new Cirkel("röd", 5), new Rektangel("blå", 4, 6) };
foreach (var f in former) f.Presentera();
```

**Förväntad output:**
```
Cirkel (röd): area = 78,54
Rektangel (blå): area = 24
```

Tips: `GetType().Name` ger klassnamnet.

---

## A4 — Karaktärer

Basklass `Karaktär`:
- Properties: `Namn` (string), `Hälsa` (int)
- Konstruktor tar namn och hälsa
- Virtual metod `Attack()` — returnerar 5 (baskada)
- Metod `Presentera()` — skriver ut namn och hälsa

Subklass `Krigare`:
- Extra property: `Styrka` (int)
- Override `Attack()` — returnerar `Styrka * 2`

Subklass `Magiker`:
- Extra property: `Mana` (int)
- Override `Attack()` — returnerar `Mana / 2`

Subklass `Bågskyttare`:
- Ingen extra property
- Override `Attack()` — returnerar `15` (alltid)

**I Main:**
```csharp
Karaktär[] lag = { new Krigare("Björn", 100, 12), new Magiker("Iris", 80, 60), new Bågskyttare("Sven", 90) };
foreach (var k in lag)
{
    k.Presentera();
    Console.WriteLine($"  Attack: {k.Attack()} skada");
}
```

**Förväntad output:**
```
Björn — 100 hp
  Attack: 24 skada
Iris — 80 hp
  Attack: 30 skada
Sven — 90 hp
  Attack: 15 skada
```

---

## A5 — Betalningar

Basklass `Betalning`:
- Property: `Belopp` (double)
- Konstruktor tar belopp
- Virtual metod `Avgift()` — returnerar 0.0
- Metod `TotalKostnad()` — returnerar `Belopp + Avgift()`
- Metod `Presentera()` — skriver ut belopp, avgift och total

Subklass `KortBetalning` — Avgift = 2% av beloppet  
Subklass `SwishBetalning` — Avgift = 0 (alltid gratis)  
Subklass `FakturBetalning` — Avgift = fast 29 kr

**I Main:**
```csharp
Betalning[] betalningar = {
    new KortBetalning(1000),
    new SwishBetalning(1000),
    new FakturBetalning(1000)
};
foreach (var b in betalningar) b.Presentera();
```

**Förväntad output:**
```
Belopp: 1000 kr | Avgift: 20 kr | Total: 1020 kr
Belopp: 1000 kr | Avgift: 0 kr | Total: 1000 kr
Belopp: 1000 kr | Avgift: 29 kr | Total: 1029 kr
```

---

## A6 — Snabbutmaning 🔴

Ta valfri klass från A1–A5 och lägg till `ToString()` override.  
Sätt ihop alla subklasser i en `List<[Basklass]>` och skriv ut med `Console.WriteLine(d)`.

Verifiera att rätt `ToString()` anropas för varje subklass.

---
