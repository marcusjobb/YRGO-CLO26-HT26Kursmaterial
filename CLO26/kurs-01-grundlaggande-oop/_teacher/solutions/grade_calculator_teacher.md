# Lärarfacit — Betygskalkylator

## Lösning

```csharp
class GradeCalculator
{
    static string GetGrade(int score)
    {
        if (score >= 75) return "VG";
        if (score >= 50) return "G";
        return "IG";
    }

    static double CalculateAverage(int[] scores)
    {
        int total = 0;
        foreach (int score in scores)
            total += score;
        return (double)total / scores.Length;
    }

    static void Main()
    {
        int[] scores = { 65, 80, 45, 90, 70 };
        double average = CalculateAverage(scores);
        int rounded = (int)average;
        string grade = GetGrade(rounded);

        Console.Write("Poäng: ");
        for (int i = 0; i < scores.Length; i++)
            Console.Write(i < scores.Length - 1 ? scores[i] + ", " : scores[i] + "\n");

        Console.WriteLine("Genomsnitt: " + rounded);
        Console.WriteLine("Betyg: " + grade);
    }
}
```

## Pedagogisk poäng

Metoder som anropar varandra: `Main` → `CalculateAverage` → returnerar till `Main` → `GetGrade`.
Det är ett bra tillfälle att visa hur ett programs flöde rör sig mellan metoder.

Vanlig fallgrop: `(double)` castet glöms bort i `CalculateAverage` → heltalsdelning → fel genomsnitt.
Låt dem se felet live innan du visar lösningen.
