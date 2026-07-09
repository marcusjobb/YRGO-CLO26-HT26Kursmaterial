// VariableExamples.cs
// Exempel på datatyper och variabler i C#
// Används som referens under vecka 02 — Syntax och variabler

class VariableExamples
{
    static void Main()
    {
        // --- Heltalstypen int ---
        // Används för hela tal, positiva och negativa
        int age = 25;
        int temperature = -3;
        Console.WriteLine("Ålder: " + age);
        Console.WriteLine("Temperatur: " + temperature + " grader");

        // --- Decimaltypen double ---
        // Används när man behöver decimaler
        double price = 49.90;
        double height = 1.75;
        Console.WriteLine("Pris: " + price + " kr");
        Console.WriteLine("Längd: " + height + " m");

        // --- Textsträngen string ---
        // Används för text — alltid med citattecken
        string firstName = "Kalle";
        string lastName = "Johansson";
        Console.WriteLine("Namn: " + firstName + " " + lastName);

        // --- Sanningsvärdet bool ---
        // Kan bara vara true eller false
        bool isLoggedIn = true;
        bool hasPermission = false;
        Console.WriteLine("Inloggad: " + isLoggedIn);
        Console.WriteLine("Behörighet: " + hasPermission);

        // --- Tecknet char ---
        // Ett enda tecken — alltid med enkla citattecken
        char grade = 'A';
        char initial = 'K';
        Console.WriteLine("Betyg: " + grade);
        Console.WriteLine("Initial: " + initial);

        Console.WriteLine();

        // --- Aritmetiska operationer ---
        int a = 10;
        int b = 3;

        Console.WriteLine("Addition:      " + a + " + " + b + " = " + (a + b));
        Console.WriteLine("Subtraktion:   " + a + " - " + b + " = " + (a - b));
        Console.WriteLine("Multiplikation:" + a + " * " + b + " = " + (a * b));
        Console.WriteLine("Heltalsdivision:" + a + " / " + b + " = " + (a / b));    // heltalsdivision — ger 3
        Console.WriteLine("Rest (modulo): " + a + " % " + b + " = " + (a % b));     // resten av division — ger 1

        Console.WriteLine();

        // --- String interpolation ---
        // Ett smidigare sätt att bygga textsträngar med variabler
        // Skriv $ framför citattecknet och sätt variabler i klamrar
        string city = "Göteborg";
        int year = 2026;
        Console.WriteLine($"Välkommen till {city}!");
        Console.WriteLine($"Det är år {year}.");
        Console.WriteLine($"Om 10 år är det {year + 10}.");
    }
}
