# Facit — Piratskatten

Motsvarar: `03_villkor_och_loopar/exercises/pirate_game/pirate_game.md`

---

## Lösning

```csharp
// PirateGame.cs
// Spel där spelaren letar reda på en gömd skatt genom att välja platser

class PirateGame
{
    static void Main()
    {
        string[] platser = { "Stranden", "Grottan", "Skogen", "Fyrtornet" };
        string[] beskrivningar =
        {
            "Inget här utom sand och krabbor.",
            "Bara fladdermus och mörker.",
            "Täta träd men ingen skatt.",
            "Gömd bakom en gammal lykta!"
        };
        bool[] sökt = { false, false, false, false };
        int skattPlats = 3;
        bool hittadSkatt = false;

        Console.WriteLine("--- Piratskatten ---");

        while (!hittadSkatt)
        {
            // Räkna hur många platser som redan är sökta
            int antalSökt = 0;
            for (int i = 0; i < sökt.Length; i++)
            {
                if (sökt[i]) antalSökt++;
            }

            // Avbryt om alla platser är genomsökta utan att skatten hittades
            if (antalSökt == sökt.Length)
            {
                Console.WriteLine("Du har sökt igenom alla platser utan att hitta skatten...");
                break;
            }

            // Visa vilka platser som inte är sökta än
            Console.WriteLine("\nPlatser kvar att söka:");
            for (int i = 0; i < platser.Length; i++)
            {
                if (!sökt[i])
                    Console.WriteLine($"  {i + 1}. {platser[i]}");
            }

            // Läs in spelarens val
            Console.Write("\nVälj plats (1-4): ");
            string inmatning = Console.ReadLine();
            int val = int.Parse(inmatning);
            int index = val - 1;

            // Kontrollera att valet är giltigt och inte redan sökt
            if (index < 0 || index >= platser.Length || sökt[index])
            {
                Console.WriteLine("Ogiltigt val — välj en plats som inte redan är sökt.");
                continue;
            }

            // Markera platsen som sökt och visa beskrivningen
            sökt[index] = true;
            Console.Write($"Du letar på {platser[index]}... ");

            if (index == skattPlats)
            {
                Console.WriteLine($"Du hittar skatten! {beskrivningar[index]}");
                hittadSkatt = true;
                Console.WriteLine($"\nDu vann! Skatten var gömd i {platser[index]}.");
            }
            else
            {
                Console.WriteLine(beskrivningar[index]);
            }
        }
    }
}
```

---

## Vad tränar övningen

Övningen tränar while-loopens struktur med ett sammansatt avslutningsvillkor (`!hittadSkatt`). Arrays och `Console.ReadLine()` används som stödkonstruktioner — de är avsiktliga förhandsvisningar, inte det centrala lärandemålet. Det centrala är att hålla reda på spelets tillstånd i en loop och bryta ur den på rätt sätt.

## Vanliga misstag

- Använder `val` direkt som arrayindex i stället för `index = val - 1` — off-by-one-fel, index 4 finns inte
- Glömmer `sökt[index] = true` — samma plats kan sökas hur många gånger som helst
- Skriver `while (true)` utan tydlig `break`-logik — fungerar ibland men är svårare att läsa och lättare att få fel

## Alternativa lösningar

Loopen kan avbrytas med enbart `break` i stället för flaggan `hittadSkatt`, men flagg-varianten är tydligare och lättare att utvidga. Studerande som väljer `break`-varianten har inte fel — diskutera gärna för- och nackdelar.

---

**Notering till läraren:** Övningen använder arrays och `Console.ReadLine()` innan de formellt introducerats i kursen. Det är ett avsiktligt val — utmaningsnivå. Studerande som fastnar på array-syntaxen ska vägledas, inte poängsättas ned. Bedöm i första hand om while-loop-strukturen och avslutningsvillkoret är rätt.
