# Facit — Tärningsspelet (vecka 03)

Motsvarar: `03_villkor_och_loopar/assignment/dice_game.md`

---

## G-lösning

```csharp
// DiceGame.cs
// Tärningsspel för två spelare — vecka 03

class DiceGame
{
    // Kastar en tärning och returnerar ett tal mellan 1 och 6
    static int RollDice(Random random)
    {
        return random.Next(1, 7);
    }

    static void Main()
    {
        // Antal omgångar i spelet
        int numberOfRounds = 5;

        // Skapa ett Random-objekt som används genom hela programmet
        Random random = new Random();

        // Variabler för att hålla reda på totalpoängen
        int scorePlayer1 = 0;
        int scorePlayer2 = 0;

        Console.WriteLine("=== TÄRNINGSSPEL ===");

        // Loopa igenom varje omgång
        for (int round = 1; round <= numberOfRounds; round++)
        {
            // Kasta tärningen för varje spelare
            int rollPlayer1 = RollDice(random);
            int rollPlayer2 = RollDice(random);

            // Lägg till omgångens poäng till totalen
            scorePlayer1 += rollPlayer1;
            scorePlayer2 += rollPlayer2;

            // Skriv ut omgångens resultat
            Console.WriteLine("Omgång " + round + ": Spelare 1 slog " + rollPlayer1 + ", Spelare 2 slog " + rollPlayer2);
        }

        // Skriv ut totalpoängen
        Console.WriteLine();
        Console.WriteLine("Spelare 1 totalpoäng: " + scorePlayer1);
        Console.WriteLine("Spelare 2 totalpoäng: " + scorePlayer2);

        // Jämför och presentera vinnaren
        if (scorePlayer1 > scorePlayer2)
        {
            Console.WriteLine("Spelare 1 vinner!");
        }
        else if (scorePlayer2 > scorePlayer1)
        {
            Console.WriteLine("Spelare 2 vinner!");
        }
        else
        {
            Console.WriteLine("Oavgjort!");
        }
    }
}
```

---

## Bedömningskommentarer

**G-kontroll:**
- `Random`-objekt skapas en gång och skickas vidare — korrekt
- `for`-loop kör exakt 5 omgångar
- Totalpoängen ackumuleras med `+=`
- Varje omgång skrivs ut direkt
- Vinnare + oavgjort hanteras med `if`/`else if`/`else`
- `RollDice`-metoden är extraherad — uppfyller metodkravet

**Vanliga G-misstag att titta efter:**
- Studerande skapar `new Random()` inuti loopen → ger samma tal varje omgång (klassisk bugg, bra lärmoment)
- Glömmer hantera oavgjort
- Skriver ut resultaten efter loopen utan att ha sparat omgångsvärdena

**VG-kontroll:**
- `RollDice` används konsekvent — inte bara definierad utan faktiskt nyttjad
- Oavgjort-grenan finns och är korrekt
- Variabelnamn är beskrivande (`scorePlayer1`, inte `p1`)
- Kommentarer förklarar logiken, inte upprepande vad koden redan säger

---

## Alternativ VG-lösning med string interpolation

```csharp
Console.WriteLine($"Omgång {round}: Spelare 1 slog {rollPlayer1}, Spelare 2 slog {rollPlayer2}");
```

Båda varianterna är acceptabla. String interpolation är snyggare och kan lyftas som ett tips i feedbacken.
