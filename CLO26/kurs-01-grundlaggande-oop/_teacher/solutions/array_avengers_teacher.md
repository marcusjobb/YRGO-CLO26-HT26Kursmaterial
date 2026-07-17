# Lärarfacit — Avengers-registret

## Lösning

```csharp
class Program
{
    static void Main()
    {
        string[] roster = { "Iron Man", "Captain America", "Thor", "Black Widow", "Hulk", "Hawkeye" };

        VisaAlla(roster);
        Console.WriteLine();

        HittaHjalte(roster, "Thor");
        HittaHjalte(roster, "Loki");
        Console.WriteLine();

        AntalHjaltar(roster);
    }

    static void VisaAlla(string[] roster)
    {
        Console.WriteLine("Tillgängliga hjältar:");
        for (int i = 0; i < roster.Length; i++)
            Console.WriteLine($"{i + 1}. {roster[i]}");
    }

    static void HittaHjalte(string[] roster, string namn)
    {
        Console.WriteLine($"Söker efter {namn}...");
        for (int i = 0; i < roster.Length; i++)
        {
            if (roster[i] == namn)
            {
                Console.WriteLine($"{namn} hittades på position {i + 1}.");
                return;
            }
        }
        Console.WriteLine($"{namn} finns inte i registret.");
    }

    static void AntalHjaltar(string[] roster)
    {
        Console.WriteLine($"Antal hjältar: {roster.Length}");
    }
}
```

## Utmaning — VisaOmvänt

```csharp
static void VisaOmvänt(string[] roster)
{
    Console.WriteLine("Hjältar (omvänd ordning):");
    for (int i = roster.Length - 1; i >= 0; i--)
        Console.WriteLine($"{roster.Length - i}. {roster[i]}");
}
```

## Pedagogisk poäng

`i + 1` i `VisaAlla` kontra `i` som index — ett bra tillfälle att prata om skillnaden mellan position för användaren (1-baserat) och index i minnet (0-baserat). Det är ett av de vanligaste off-by-one-misstagen.

`return` i `HittaHjalte` — visa hur `return` avbryter en metod mitt i en loop. Det är effektivare än en bool-flagga och tydligare att läsa.
