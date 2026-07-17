# Lärarfacit — BasicAlgorithms

## Lösning

```csharp
namespace Algorithms
{
    public class BasicAlgorithms
    {
        public int Average(params int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
                sum += numbers[i];
            int result = sum / numbers.Length;
            Console.WriteLine("Medelvärdet är: " + result);
            return result;
        }

        public (int min, int max) MinAndMax(params int[] numbers)
        {
            int max = int.MinValue;
            int min = int.MaxValue;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > max) max = numbers[i];
                if (numbers[i] < min) min = numbers[i];
            }
            Console.WriteLine("Min och max: " + (min, max));
            return (min, max);
        }

        public int SumDigits(string number)
        {
            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                if (int.TryParse(number.Substring(i, 1), out int n))
                    sum += n;
            }
            Console.WriteLine("Summa: " + sum);
            return sum;
        }
    }
}
```

## Pedagogisk poäng

Tre distinkta algoritmiska mönster i en övning:
- **Average:** Ackumulatormönster (`sum += x`, dela på längd)
- **MinAndMax:** Spårningmönster (`int.MinValue`/`MaxValue` som startvärden)
- **SumDigits:** Filtreringsmönster (`TryParse` som grindvakt)

`params` är det nya konceptet här — poängtera att det är syntax-socker: kompilatorn gör det till en array, den som anropar behöver inte veta det.

Vanlig fallgrop i `MinAndMax`: att starta med `min = 0, max = 0` — visar felet med negativa tal live.
