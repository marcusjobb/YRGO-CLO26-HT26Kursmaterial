# Övning — Kaffemaskinen ☕

🟡 Mellannivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Klockan är 07:23. Marcus jobbar hemifrån. Datorn lyser. Tanken är klar.

Kaffet är slut.

Inte behållaren — maskinen. Inget vatten. Inga bönor. Ingenting. Han stirrar på den i tre sekunder och bestämmer sig för att lösa problemet på det enda sättet han vet: med kod.

Du ska bygga kaffemaskinen.

---

## Vad gäller för den här övningen

- [ ] Kan skriva en klass med privata fält och läsbara properties
- [ ] Kan skriva metoder som ändrar klassens inre tillstånd
- [ ] Förstår varför klassen kontrollerar sina egna gränser

---

## Uppgift

Skapa en klass `Kaffemaskin` med:

**Privata fält:**
- `_vatten` (int) — milliliter vatten, max 1000
- `_bönor` (int) — antal bönor, max 100

**Properties** (läsbara utifrån, skrivbara bara inifrån):
- `Vatten` — returnerar `_vatten`
- `Bönor` — returnerar `_bönor`

**Konstruktor** utan parametrar — maskinen startar tom (0 vatten, 0 bönor)

**Metoder:**
- `FyllPåVatten(int mängd)` — fyller på vatten, överstiger aldrig 1000 ml
- `FyllPåBönor(int mängd)` — fyller på bönor, överstiger aldrig 100
- `BrewKaffe()` — returnerar `true` om det gick att brygga (kräver 200 ml + 10 bönor), annars `false`. Drar av rätt mängd om det lyckas.
- `VisaStatus()` — skriver ut nuvarande vatten och bönor

**I `Main()`:**
- Skapa en maskin
- Visa status (tom)
- Fyll på vatten och bönor
- Visa status igen
- Brygga kaffe och skriv ut om det gick

---

## Exempeloutput

```
Status: 0 ml vatten, 0 bönor
Status: 500 ml vatten, 30 bönor
Brygger kaffe...
☕ Kaffe klart!
Status: 300 ml vatten, 20 bönor
```

---

## Att fundera på

- Vad händer om man anropar `FyllPåVatten(9999)`? Varför är det bra att klassen hanterar det själv?
- Varför returnerar `BrewKaffe()` ett `bool` istället för att skriva ut resultatet direkt?
- Vad är skillnaden mellan `_vatten` och `Vatten` — när används vilket?

---

## Lösningsförslag

<details>
<summary>Klicka för att visa lösningen</summary>

```csharp
class Kaffemaskin
{
    private int _vatten;
    private int _bönor;

    public int Vatten => _vatten;
    public int Bönor => _bönor;

    public Kaffemaskin()
    {
        _vatten = 0;
        _bönor = 0;
    }

    public void FyllPåVatten(int mängd)
    {
        _vatten += mängd;
        if (_vatten > 1000)
            _vatten = 1000;
    }

    public void FyllPåBönor(int mängd)
    {
        _bönor += mängd;
        if (_bönor > 100)
            _bönor = 100;
    }

    public bool BrewKaffe()
    {
        Console.WriteLine("Brygger kaffe...");
        if (_vatten >= 200 && _bönor >= 10)
        {
            _vatten -= 200;
            _bönor -= 10;
            return true;
        }
        return false;
    }

    public void VisaStatus()
    {
        Console.WriteLine($"Status: {_vatten} ml vatten, {_bönor} bönor");
    }
}

class Program
{
    static void Main()
    {
        Kaffemaskin maskin = new Kaffemaskin();

        maskin.VisaStatus();

        maskin.FyllPåVatten(500);
        maskin.FyllPåBönor(30);

        maskin.VisaStatus();

        if (maskin.BrewKaffe())
            Console.WriteLine("☕ Kaffe klart!");
        else
            Console.WriteLine("❌ Inte tillräckligt med vatten eller bönor.");

        maskin.VisaStatus();
    }
}
```

`_vatten` och `_bönor` är privata — ingen utanför klassen kan råka sätta `maskin._vatten = -500`. Klassen äger sina egna gränser. `Vatten => _vatten` är en läs-bara property: omvärlden kan läsa, men aldrig skriva direkt.

</details>
