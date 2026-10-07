# Övning: Betygskalkylator 🟡

I den här övningen ska du skapa en enkel betygskalkylator för en kurs.
Du tränar på att kombinera metoder som anropar varandra och arbetar med arrayer.

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

## Din uppgift

Skapa följande statiska metoder:

- `static string GetGrade(int score)` — returnerar betyget som sträng baserat på poängen:
  - 0–49 poäng → `"IG"`
  - 50–74 poäng → `"G"`
  - 75–100 poäng → `"VG"`
- `static double CalculateAverage(int[] scores)` — beräknar och returnerar medelvärdet av alla poäng i arrayen

I `Main`:
1. Skapa en array med fem poängvärden
2. Räkna ut genomsnittet med `CalculateAverage`
3. Hämta betyget med `GetGrade` (avrunda genomsnittet till `int`)
4. Skriv ut poängen, genomsnittet och betyget

## Förväntad output

```
Poäng: 65, 80, 45, 90, 70
Genomsnitt: 70
Betyg: G
```

## Lösning

<details>
<summary>Visa lösning</summary>

```csharp
class GradeCalculator
{
    static string GetGrade(int score)
    {
        if (score >= 75)
        {
            return "VG";
        }
        else if (score >= 50)
        {
            return "G";
        }
        else
        {
            return "IG";
        }
    }

    static double CalculateAverage(int[] scores)
    {
        int total = 0;

        foreach (int score in scores)
        {
            total = total + score;
        }

        return (double)total / scores.Length;
    }

    static void Main()
    {
        int[] scores = { 65, 80, 45, 90, 70 };

        double average = CalculateAverage(scores);
        int roundedAverage = (int)average;
        string grade = GetGrade(roundedAverage);

        Console.Write("Poäng: ");
        for (int i = 0; i < scores.Length; i++)
        {
            if (i < scores.Length - 1)
            {
                Console.Write(scores[i] + ", ");
            }
            else
            {
                Console.WriteLine(scores[i]);
            }
        }

        Console.WriteLine("Genomsnitt: " + roundedAverage);
        Console.WriteLine("Betyg: " + grade);
    }
}
```

</details>
