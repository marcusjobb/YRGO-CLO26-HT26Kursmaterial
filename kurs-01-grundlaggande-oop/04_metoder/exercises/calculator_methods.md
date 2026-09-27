# Övning: Kalkylator med metoder 🟢

I den här övningen ska du skapa en enkel kalkylator där varje räkneoperation är en egen metod.
Det är ett bra sätt att träna på att bryta upp kod i tydliga, återanvändbara delar.

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

## Din uppgift

Skapa fyra statiska metoder:

- `static double Add(double a, double b)` — addition
- `static double Subtract(double a, double b)` — subtraktion
- `static double Multiply(double a, double b)` — multiplikation
- `static double Divide(double a, double b)` — division

Metoden `Divide` ska hantera division med noll: om `b` är `0`, skriv ut ett felmeddelande och returnera `0`.

Anropa alla fyra metoder i `Main` med värdena `10` och `5`. Anropa också `Divide` en extra gång med `0` som andra argument.

## Förväntad output

```
Addition: 10 + 5 = 15
Subtraktion: 10 - 5 = 5
Multiplikation: 10 * 5 = 50
Division: 10 / 5 = 2
Division med noll: Kan inte dividera med noll!
```

## Lösning

<details>
<summary>Visa lösning</summary>

```csharp
class Calculator
{
    static double Add(double a, double b)
    {
        return a + b;
    }

    static double Subtract(double a, double b)
    {
        return a - b;
    }

    static double Multiply(double a, double b)
    {
        return a * b;
    }

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
        double a = 10;
        double b = 5;

        double addResult = Add(a, b);
        Console.WriteLine("Addition: " + a + " + " + b + " = " + addResult);

        double subtractResult = Subtract(a, b);
        Console.WriteLine("Subtraktion: " + a + " - " + b + " = " + subtractResult);

        double multiplyResult = Multiply(a, b);
        Console.WriteLine("Multiplikation: " + a + " * " + b + " = " + multiplyResult);

        double divideResult = Divide(a, b);
        Console.WriteLine("Division: " + a + " / " + b + " = " + divideResult);

        Divide(a, 0);
    }
}
```

</details>
