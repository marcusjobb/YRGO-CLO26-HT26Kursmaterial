# Träningspass — Konstruktoröverlagring

🟢 Grundläggande → 🟡 Mellannivå

> Samma klass, flera sätt att skapa objekt.  
> Fastnar du mer än 10 minuter? Fråga grannen. Sedan AI. Sedan Marcus.

---

## Vad är konstruktoröverlagring?

En klass kan ha **flera konstruktorer** med olika parametrar.  
Du väljer vilken som passar när du skapar objektet.

```csharp
class Person
{
    public string Namn { get; private set; }
    public int Ålder { get; private set; }

    public Person(string namn, int ålder)   // full konstruktor
    {
        Namn = namn;
        Ålder = ålder;
    }

    public Person(string namn)              // utan ålder — ålder sätts till 0
        : this(namn, 0) { }                 // anropar den första konstruktorn
}
```

`this(...)` anropar en annan konstruktor i **samma klass**.  
Det är ett sätt att undvika att skriva samma initiering flera gånger.

---

## KO1 — Pizzan

Skapa klassen `Pizza` med:
- Properties: `Namn` (string), `Storlek` (string), `ExtraOst` (bool)
- Tre konstruktorer:
  - `Pizza(string namn, string storlek, bool extraOst)` — full
  - `Pizza(string namn, string storlek)` — ExtraOst = false
  - `Pizza(string namn)` — Storlek = "Medium", ExtraOst = false

Metod `Presentera()` — skriver ut namn, storlek och om det är extra ost.

**I Main:**
```csharp
Pizza p1 = new Pizza("Margherita", "Stor", true);
Pizza p2 = new Pizza("Vesuvio", "Liten");
Pizza p3 = new Pizza("Hawaii");

p1.Presentera();
p2.Presentera();
p3.Presentera();
```

**Förväntad output:**
```
Margherita | Stor | Extra ost: ja
Vesuvio | Liten | Extra ost: nej
Hawaii | Medium | Extra ost: nej
```

---

## KO2 — Användaren

Skapa klassen `Användare` med:
- Properties: `Användarnamn` (string), `Roll` (string), `ÄrAktiv` (bool)
- Tre konstruktorer:
  - `Användare(string namn, string roll, bool aktiv)`
  - `Användare(string namn, string roll)` — ÄrAktiv = true
  - `Användare(string namn)` — Roll = "Gäst", ÄrAktiv = true

Metod `Presentera()` — skriver ut alla tre fält.

**I Main:**
```csharp
Användare u1 = new Användare("admin", "Administratör", true);
Användare u2 = new Användare("marcus", "Lärare");
Användare u3 = new Användare("besökare");

u1.Presentera();
u2.Presentera();
u3.Presentera();
```

**Förväntad output:**
```
admin | Administratör | Aktiv: ja
marcus | Lärare | Aktiv: ja
besökare | Gäst | Aktiv: ja
```

---

## KO3 — Produkten

Skapa klassen `Produkt` med:
- Properties: `Namn` (string), `Pris` (double), `Lager` (int)
- Konstruktorer:
  - Full: namn, pris, lager
  - Utan lager: lager = 0
  - Utan pris och lager: pris = 0.0, lager = 0

Metod `Presentera()` — skriver ut alla tre.

**I Main:**
```csharp
Produkt p1 = new Produkt("Tangentbord", 599.0, 12);
Produkt p2 = new Produkt("Mus", 299.0);
Produkt p3 = new Produkt("Sladd");

p1.Presentera();
p2.Presentera();
p3.Presentera();
```

**Förväntad output:**
```
Tangentbord | 599 kr | 12 i lager
Mus | 299 kr | 0 i lager
Sladd | 0 kr | 0 i lager
```

---

## KO4 — Fordonet med arv

Basklass `Fordon`:
- Properties: `Märke` (string), `Hastighet` (int)
- Konstruktor: tar märke och hastighet

Subklass `Bil` — ärver från `Fordon`:
- Extra property: `AntalDörrar` (int)
- Konstruktorer:
  - `Bil(string märke, int hastighet, int dörrar)` — full, anropar `base(...)`
  - `Bil(string märke, int hastighet)` — AntalDörrar = 4
  - `Bil(string märke)` — Hastighet = 120, AntalDörrar = 4

Metod `Presentera()` i `Bil` — skriver ut märke, hastighet och antal dörrar.

**I Main:**
```csharp
Bil b1 = new Bil("Volvo", 180, 2);
Bil b2 = new Bil("Tesla", 250);
Bil b3 = new Bil("LADA");

b1.Presentera();
b2.Presentera();
b3.Presentera();
```

**Förväntad output:**
```
Volvo | 180 km/h | 2 dörrar
Tesla | 250 km/h | 4 dörrar
LADA | 120 km/h | 4 dörrar
```

---

## KO5 — Frågor

Svara som kommentarer i koden:

1. Vad är skillnaden mellan `this(...)` och `base(...)`?
2. Varför är det bättre att använda `this(...)` istället för att upprepa koden i varje konstruktor?
3. Vilken konstruktor anropas när du skriver `new Bil("LADA")`? Vilken kedja av anrop sker?

---
