# Lärarfacit — Kalkylator med metoder

## Lösning

```csharp
class Calculator
{
    static double Add(double a, double b) => a + b;

    static double Subtract(double a, double b) => a - b;

    static double Multiply(double a, double b) => a * b;

    static double Divide(double a, double b)
    {
        if (b == 0)
        {
            Console.WriteLine("Division med noll: Kan inte dividera med noll!");
            return 0;
        }
        return a / b;
    }

    static void Main()
    {
        double a = 10, b = 5;
        Console.WriteLine($"Addition: {a} + {b} = {Add(a, b)}");
        Console.WriteLine($"Subtraktion: {a} - {b} = {Subtract(a, b)}");
        Console.WriteLine($"Multiplikation: {a} * {b} = {Multiply(a, b)}");
        Console.WriteLine($"Division: {a} / {b} = {Divide(a, b)}");
        Divide(a, 0);
    }
}
```

## Pedagogisk poäng

Varje operation är en egen metod — DRY i praktiken. `Divide` är det intressanta:
den enda metoden med ett kantfall att hantera. Lyfta att `return 0` efter felmeddelandet
är ett designval — alternativet är att kasta ett exception, men det är för tidigt i kursen.

Vanlig fallgrop: studerande skriver `Console.WriteLine` i `Add`, `Subtract` osv —
påminn om att metoden ska returnera ett värde, inte skriva ut det. Utskriften sköts i `Main`.
