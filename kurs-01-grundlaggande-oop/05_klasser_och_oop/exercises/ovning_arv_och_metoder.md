# Övning — Arv och metoder

🟢 Grundläggande → 🟡 Mellannivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Beatrice jobbar på ett djursjukhus. Hon behöver ett system som håller koll på djuren — och kan presentera dem, låta dem hälsa och räkna ut deras ålder i "mänskliga år".

---

## Del 1 — Basklass med metod

Skapa klassen `Djur`:

**Property (publik get, privat set):**
- `Namn` (string)
- `ÅlderIÅr` (int)

**Konstruktor** som tar `namn` och `ålder`

**Metod:**
- `Presentera()` — skriver ut `"Jag heter [Namn] och är [ÅlderIÅr] år gammal."`

**Virtual metod:**
- `LåtaLjud()` — tom implementation i basklassen

---

## Del 2 — Subklasser med override

Skapa `Hund` och `Katt` som ärver från `Djur`.

Båda tar `namn` och `ålder` i konstruktorn och skickar vidare med `base(...)`.

**`Hund.LåtaLjud()`** skriver ut `"Voff!"`  
**`Katt.LåtaLjud()`** skriver ut `"Mjau!"`

---

## Del 3 — Metod med beräkning

Lägg till metoden `MänskligaÅr()` i **basklassen** `Djur`:

```
Hund: hundens ålder × 7
Katt: kattens ålder × 5
Annan Djur: ålder × 4
```

Problemet: alla djur delar samma basklass men ska ge olika svar.  
Gör `MänskligaÅr()` virtual i `Djur` — och override i `Hund` och `Katt`.

**Exempeloutput:**

```
Fido är 35 mänskliga år.
Luna är 25 mänskliga år.
```

---

## Del 4 — Sätt ihop i Main

```csharp
Hund fido = new Hund("Fido", 5);
Katt luna = new Katt("Luna", 5);

fido.Presentera();
fido.LåtaLjud();
Console.WriteLine($"{fido.Namn} är {fido.MänskligaÅr()} mänskliga år.");

luna.Presentera();
luna.LåtaLjud();
Console.WriteLine($"{luna.Namn} är {luna.MänskligaÅr()} mänskliga år.");
```

---

## Frågor — svara som kommentarer i koden

Skriv svaren direkt i din fil under en kommentar `// Frågor`:

1. Varför är `Presentera()` inte virtual — men `LåtaLjud()` är det?
2. Vad händer om du tar bort `override` från `Hund.MänskligaÅr()`? Vad körs då?
3. Varför skickar du `namn` och `ålder` till `base(...)` istället för att sätta dem direkt i subklassen?
4. Lägg till `class Kanin : Djur` med `LåtaLjud()` = "Nöff!" och `MänskligaÅr()` = ålder × 6. Hur mycket ny kod behövde du skriva?

---

## Klar snabbt? Utmaning 🔴

Beatrice vill ha en lista med alla djur:

```csharp
List<Djur> djur = new List<Djur>();
djur.Add(new Hund("Fido", 5));
djur.Add(new Katt("Luna", 5));
djur.Add(new Kanin("Nisse", 3));

foreach (Djur d in djur)
{
    d.Presentera();
    d.LåtaLjud();
    Console.WriteLine($"{d.Namn} är {d.MänskligaÅr()} mänskliga år.");
}
```

Koden ovan fungerar direkt om du löst Del 1-3.  
Förklara med egna ord (som kommentar): varför kan du lägga `Hund`, `Katt` och `Kanin` i en `List<Djur>`?

---

<details>
<summary>Lösningsförslag — Del 1–3</summary>

```csharp
class Djur
{
    public string Namn { get; private set; }
    public int ÅlderIÅr { get; private set; }

    public Djur(string namn, int ålder)
    {
        Namn = namn;
        ÅlderIÅr = ålder;
    }

    public void Presentera()
    {
        Console.WriteLine($"Jag heter {Namn} och är {ÅlderIÅr} år gammal.");
    }

    public virtual void LåtaLjud() { }

    public virtual int MänskligaÅr() => ÅlderIÅr * 4;
}

class Hund : Djur
{
    public Hund(string namn, int ålder) : base(namn, ålder) { }

    public override void LåtaLjud() => Console.WriteLine("Voff!");

    public override int MänskligaÅr() => ÅlderIÅr * 7;
}

class Katt : Djur
{
    public Katt(string namn, int ålder) : base(namn, ålder) { }

    public override void LåtaLjud() => Console.WriteLine("Mjau!");

    public override int MänskligaÅr() => ÅlderIÅr * 5;
}
```

</details>
