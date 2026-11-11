// MethodExamples.cs
// Exempel på olika typer av metoder i C#
// Används som referens under vecka 04 — Metoder

class MethodExamples
{
    // En void-metod tar emot parametrar men returnerar inget värde
    static void PrintGreeting(string name)
    {
        Console.WriteLine("Hej, " + name + "! Välkommen till kursen.");
    }

    // En metod som tar emot två tal och returnerar deras summa
    static int Add(int firstNumber, int secondNumber)
    {
        return firstNumber + secondNumber;
    }

    // En metod som kontrollerar om ett tal är jämnt
    static bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    // En metod som tar emot ett dagnummer (1–7) och returnerar dagnamnet
    static string GetDayName(int dayNumber)
    {
        if (dayNumber == 1) return "Måndag";
        if (dayNumber == 2) return "Tisdag";
        if (dayNumber == 3) return "Onsdag";
        if (dayNumber == 4) return "Torsdag";
        if (dayNumber == 5) return "Fredag";
        if (dayNumber == 6) return "Lördag";
        if (dayNumber == 7) return "Söndag";
        return "Ogiltigt dagnummer";
    }

    static void Main()
    {
        // Anropar void-metoden — inget returvärde att spara
        PrintGreeting("Anna");

        // Anropar Add och sparar resultatet i en variabel
        int result = Add(8, 3);
        Console.WriteLine("8 + 3 = " + result);

        // Anropar IsEven och använder returvärdet direkt i en if-sats
        int testNumber = 7;
        if (IsEven(testNumber))
        {
            Console.WriteLine(testNumber + " är ett jämnt tal.");
        }
        else
        {
            Console.WriteLine(testNumber + " är ett udda tal.");
        }

        // Anropar GetDayName med dagnummer och skriver ut resultatet
        string dayName = GetDayName(3);
        Console.WriteLine("Dag 3 är: " + dayName);
    }
}
