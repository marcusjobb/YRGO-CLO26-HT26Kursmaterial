# Facit — Karaktärsklass

Motsvarar: `03_villkor_och_loopar/exercises/character_class/character_class.md`

---

## Lösning

```csharp
// CharacterClass.cs
// RPG-karaktärsväljare med switch-sats

class CharacterClass
{
    static void Main()
    {
        int val = 1;

        switch (val)
        {
            case 1:
                Console.WriteLine("Du valde: Krigar");
                Console.WriteLine("Styrka: 9");
                Console.WriteLine("Intelligens: 3");
                Console.WriteLine("Smidighet: 5");
                break;
            case 2:
                Console.WriteLine("Du valde: Trollkarl");
                Console.WriteLine("Styrka: 2");
                Console.WriteLine("Intelligens: 10");
                Console.WriteLine("Smidighet: 4");
                break;
            case 3:
                Console.WriteLine("Du valde: Lömsk");
                Console.WriteLine("Styrka: 5");
                Console.WriteLine("Intelligens: 6");
                Console.WriteLine("Smidighet: 9");
                break;
            case 4:
                Console.WriteLine("Du valde: Dvärg");
                Console.WriteLine("Styrka: 8");
                Console.WriteLine("Intelligens: 5");
                Console.WriteLine("Smidighet: 3");
                break;
            case 5:
                Console.WriteLine("Du valde: Hobbit");
                Console.WriteLine("Styrka: 3");
                Console.WriteLine("Intelligens: 7");
                Console.WriteLine("Smidighet: 8");
                break;
            default:
                Console.WriteLine("Ogiltigt val. Välj ett tal mellan 1 och 5.");
                break;
        }
    }
}
```

---

## Vad tränar övningen

Övningen tränar switch-satsen som ett alternativ till långa if/else if-kedjor, samt hur `default` hanterar oväntade indata. Studerande ser att switch passar bra när ett värde jämförs mot ett fast antal kända alternativ.

## Vanliga misstag

- Glömmer `break` i en eller flera cases — programmet "faller igenom" till nästa case (fall-through)
- Utelämnar `default` helt — programmet ger ingen utskrift vid ogiltigt val utan att någon felkod visas
- Räknar fel på case-numreringen (t.ex. skriver `case 0` för Krigar i stället för `case 1`)

## Alternativa lösningar

I C# 8+ finns switch expressions med pil-syntax (`val switch { 1 => ..., _ => ... }`). Det är en giltig och modern variant, men vid den här nivån är den klassiska switch-satsen rätt — switch expressions introduceras senare i kursen.
