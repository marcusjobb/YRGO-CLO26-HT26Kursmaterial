# Träningspass — Klasser och objekt (extra)

🟢 Grundläggande → 🟡 Mellannivå

> Samma mönster som förut — ny kontext. Bygger på det du redan kan.  
> Fastnar du mer än 10 minuter? Fråga grannen. Sedan AI. Sedan Marcus.

---

## K8 — Parkeringen 🟡

Skapa klassen `Parkering` med:
- Properties: `Registreringsnummer` (string), `AntalTimmar` (int)
- Konstruktor som tar registreringsnummer och antal timmar
- Property `Avgift` (double, beräknad) — 25 kr/timme, minst 1 timme
- Metod `Presentera()` — skriver ut registreringsnummer, timmar och avgift

**I Main:**
```csharp
Parkering p1 = new Parkering("ABC123", 3);
Parkering p2 = new Parkering("XYZ789", 1);
p1.Presentera();
p2.Presentera();
```

**Förväntad output:**
```
ABC123 | 3 timmar | 75 kr
XYZ789 | 1 timme | 25 kr
```

---

## K9 — Paketet 🟡

Skapa klassen `Paket` med:
- Properties: `Avsändare` (string), `Mottagare` (string), `ViktKg` (double)
- Konstruktor som tar alla tre
- Metod `Fraktpris()` — returnerar priset:
  - Under 1 kg: 49 kr
  - 1–5 kg: 99 kr
  - Över 5 kg: 199 kr
- Metod `Presentera()` — skriver ut avsändare, mottagare och fraktpris

**I Main:**
```csharp
Paket p1 = new Paket("Maja", "Erik", 0.5);
Paket p2 = new Paket("Lotta", "Sven", 3.2);
Paket p3 = new Paket("NionIT", "Kunden", 8.0);
p1.Presentera();
p2.Presentera();
p3.Presentera();
```

**Förväntad output:**
```
Maja → Erik | 49 kr
Lotta → Sven | 99 kr
NionIT → Kunden | 199 kr
```

---

## K10 — Mötet 🟡

Skapa klassen `Möte` med:
- Properties: `Rubrik` (string), `StartTimme` (int), `SlutTimme` (int)
- Konstruktor som tar alla tre
- Metod `Duration()` — returnerar `SlutTimme - StartTimme` (int, antal timmar)
- Metod `Presentera()` — skriver ut rubrik, tid och duration

**I Main:**
```csharp
Möte m1 = new Möte("Planeringsmöte", 9, 11);
Möte m2 = new Möte("Lunchsnack", 12, 13);
m1.Presentera();
m2.Presentera();
```

**Förväntad output:**
```
Planeringsmöte | 09:00–11:00 | 2 timmar
Lunchsnack | 12:00–13:00 | 1 timme
```

Tips: använd `$"{StartTimme:D2}:00"` för att få `09:00`-formatet.

---

## K11 — Filmkritiken 🟡

Skapa klassen `Filmkritik` med:
- Properties: `Titel` (string), `Betyg` (int, 1–5), `Recension` (string)
- Konstruktor som tar alla tre
- Metod `Rekommenderas()` — returnerar `true` om Betyg >= 4
- Metod `Presentera()` — skriver ut titel, betyg som stjärnor och rekommendation

**I Main:**
```csharp
Filmkritik f1 = new Filmkritik("Interstellar", 5, "Fantastisk film!");
Filmkritik f2 = new Filmkritik("Medelfilm", 2, "Ganska tråkig.");
f1.Presentera();
f2.Presentera();
```

**Förväntad output:**
```
Interstellar | ★★★★★ | Rekommenderas: ja
Medelfilm | ★★ | Rekommenderas: nej
```

Tips: `new string('★', Betyg)` bygger en sträng med rätt antal stjärnor.

---

## K12 — Receptet 🟡

Skapa klassen `Recept` med:
- Properties: `Namn` (string), `Portioner` (int), privat `List<string> _ingredienser`
- Konstruktor som tar namn och portioner — skapar en tom ingredienslista
- Metod `LäggTillIngrediens(string ingrediens)` — lägger till i listan
- Metod `DubblaPortion()` — dubblerar `Portioner`
- Metod `Presentera()` — skriver ut namn, portioner och ingredienser

**I Main:**
```csharp
Recept r = new Recept("Pannkakor", 4);
r.LäggTillIngrediens("Mjöl");
r.LäggTillIngrediens("Mjölk");
r.LäggTillIngrediens("Ägg");
r.DubblaPortion();
r.Presentera();
```

**Förväntad output:**
```
Pannkakor (8 portioner)
- Mjöl
- Mjölk
- Ägg
```

---
