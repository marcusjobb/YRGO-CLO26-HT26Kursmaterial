// ConditionExamples.cs
// Exempel på villkor och loopar i C#
// Används som referens under vecka 03 — Villkor och loopar

class ConditionExamples
{
    // Metod: returnerar en beskrivning utifrån ett betyg (1–5)
    static string GetGradeDescription(int grade)
    {
        // switch är bra när man jämför ett värde mot fasta alternativ
        switch (grade)
        {
            case 5:
                return "Utmärkt";
            case 4:
                return "Bra";
            case 3:
                return "Godkänt";
            case 2:
                return "Underkänt";
            case 1:
                return "Mycket underkänt";
            default:
                return "Ogiltigt betyg";
        }
    }

    static void Main()
    {
        // === if / else if / else ===
        // Används när man vill testa villkor och köra olika kod beroende på resultatet

        int score = 72;

        Console.WriteLine("=== BETYGSÄTTNING ===");

        if (score >= 90)
        {
            Console.WriteLine("Du fick A — Utmärkt!");
        }
        else if (score >= 70)
        {
            Console.WriteLine("Du fick B — Bra jobbat!");
        }
        else if (score >= 50)
        {
            Console.WriteLine("Du fick C — Godkänt.");
        }
        else
        {
            Console.WriteLine("Du fick F — Försök igen.");
        }

        Console.WriteLine();

        // === switch ===
        // Anropar metoden ovan med olika betygsvärden

        Console.WriteLine("=== BETYG ===");

        for (int i = 5; i >= 1; i--)
        {
            // Interpolation: väver in variabler direkt i textsträngen
            Console.WriteLine($"Betyg {i}: {GetGradeDescription(i)}");
        }

        Console.WriteLine();

        // === while-loop ===
        // Körs så länge villkoret är sant — bra när man inte vet i förväg hur många varv som behövs

        Console.WriteLine("=== NEDRÄKNING ===");

        int countdown = 5;

        while (countdown > 0)
        {
            Console.WriteLine("T minus " + countdown + "...");
            countdown--;   // minska med 1 varje varv, annars loopas för evigt
        }

        Console.WriteLine("Liftoff!");

        Console.WriteLine();

        // === for-loop ===
        // Bra när man vet exakt hur många gånger man vill loopa

        Console.WriteLine("=== MULTIPLIKATIONSTABELL FÖR 3 ===");

        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine("3 × " + i + " = " + (3 * i));
        }

        Console.WriteLine();

        // === foreach-loop ===
        // Loopar igenom varje element i en samling — enkel och läsbar

        Console.WriteLine("=== VECKODAGAR ===");

        string[] weekdays = { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };

        foreach (string day in weekdays)
        {
            Console.WriteLine("Dag: " + day);
        }
    }
}
