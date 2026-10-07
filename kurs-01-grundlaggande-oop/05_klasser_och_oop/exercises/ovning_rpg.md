# Övning — Striden ⚔️

🟡 Mellannivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Leeroy Jenkins är en legend inom gaming. År 2005 rusade han in i ett dungeon-rum utan att vänta på sitt lag — och fick hela gruppen utraderad på tre sekunder. Världen glömmer aldrig.

Nu är det din tur att bygga striden.

Du ska skapa en klass `Karaktär` som kan attackera, överleva och till slut avgöra vem som vinner.

---

## Vad gäller för den här övningen

- [ ] Kan skriva en klass med privata fält och properties
- [ ] Kan skriva en metod som tar emot ett objekt av samma klass som parameter
- [ ] Förstår hur en while-loop kan styras av ett objekts tillstånd

---

## Uppgift

Skapa en klass `Karaktär` med:

**Privata fält:**
- `_namn` (string)
- `_hälsa` (int)
- `_attackStyrka` (int)

**Properties** (publik get, privat set):
- `Namn`
- `Hälsa`
- `AttackStyrka`

**Konstruktor** som tar in namn, hälsa och attackStyrka

**Metoder:**
- `Presentera()` — skriver ut namn, hälsa och attackstyrka
- `ÄrLevande()` — returnerar `true` om hälsan är över 0
- `Attackera(Karaktär mål)` — minskar målets hälsa med angriparens attackstyrka och skriver ut vad som hände

**I `Main()`:**
- Skapa två karaktärer
- Låt dem slåss i en while-loop tills en av dem dör
- Skriv ut vem som vann

---

## Exempeloutput

```
=== STRID BÖRJAR ===
Leeroy Jenkins: 100 HP | Attack: 25
Stor drake: 80 HP | Attack: 20

Runda 1
Leeroy Jenkins attackerar Stor drake för 25 skada!
Stor drake: 55 HP kvar
Stor drake attackerar Leeroy Jenkins för 20 skada!
Leeroy Jenkins: 80 HP kvar

Runda 4
Leeroy Jenkins attackerar Stor drake för 25 skada!
Stor drake har fallit!

🏆 Leeroy Jenkins vinner!
```

---

## Att fundera på

- Varför tar `Attackera` emot ett `Karaktär`-objekt som parameter istället för bara ett int-värde?
- Vad händer om båda karaktärerna har exakt lika mycket hälsa och attackstyrka?
- Hur skulle du ändra koden för att låta spelaren välja sina egna värden?

---

## Lösningsförslag

<details>
<summary>Klicka för att visa lösningen</summary>

```csharp
class Karaktär
{
    public string Namn { get; private set; }
    public int Hälsa { get; private set; }
    public int AttackStyrka { get; private set; }

    public Karaktär(string namn, int hälsa, int attackStyrka)
    {
        Namn = namn;
        Hälsa = hälsa;
        AttackStyrka = attackStyrka;
    }

    public void Presentera()
    {
        Console.WriteLine($"{Namn}: {Hälsa} HP | Attack: {AttackStyrka}");
    }

    public bool ÄrLevande()
    {
        return Hälsa > 0;
    }

    public void Attackera(Karaktär mål)
    {
        mål.Hälsa -= AttackStyrka;

        Console.WriteLine($"{Namn} attackerar {mål.Namn} för {AttackStyrka} skada!");

        if (mål.Hälsa <= 0)
        {
            mål.Hälsa = 0;
            Console.WriteLine($"{mål.Namn} har fallit!");
        }
        else
        {
            Console.WriteLine($"{mål.Namn}: {mål.Hälsa} HP kvar");
        }
    }
}

class Program
{
    static void Main()
    {
        Karaktär hjälte = new Karaktär("Leeroy Jenkins", 100, 25);
        Karaktär motståndare = new Karaktär("Stor drake", 80, 20);

        Console.WriteLine("=== STRID BÖRJAR ===");
        hjälte.Presentera();
        motståndare.Presentera();
        Console.WriteLine();

        int runda = 1;
        while (hjälte.ÄrLevande() && motståndare.ÄrLevande())
        {
            Console.WriteLine($"Runda {runda}");
            hjälte.Attackera(motståndare);

            if (motståndare.ÄrLevande())
                motståndare.Attackera(hjälte);

            Console.WriteLine();
            runda++;
        }

        if (hjälte.ÄrLevande())
            Console.WriteLine($"🏆 {hjälte.Namn} vinner!");
        else
            Console.WriteLine($"💀 {motståndare.Namn} vinner!");
    }
}
```

`mål.Hälsa -= AttackStyrka` fungerar inifrån klassen trots `private set` — i C# är `private` typbaserat, inte instansbaserat. Alla metoder i `Karaktär` kan läsa och skriva `private set`-properties på vilken `Karaktär`-instans som helst.

</details>
