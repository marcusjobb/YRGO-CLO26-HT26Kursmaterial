# Tuples

Ibland vill en metod ge tillbaka två värden som hör ihop, till exempel en summa och en moms. Då kan du skapa en klass, men för två värden utan egna regler blir det tungt. En tuple är ett lätt alternativ.

## Två värden från en metod

```csharp
(decimal sum, decimal vat) Calculate(decimal price)
{
    return (price, price * 0.25m);
}

var (sum, vat) = Calculate(100m);
Console.WriteLine($"Summa: {sum}, moms: {vat}");
```

Metoden returnerar två värden, och du tar emot dem i ett svep med deconstruction, `var (sum, vat) = ...`. Namnen `sum` och `vat` är bara bekvämlighet för dig. Kompilatorn bryr sig inte om dem.

## Ge fälten namn

```csharp
var person = ("Ada", 36);
Console.WriteLine(person.Item1);   // Ada
```

Utan namn heter fälten `Item1` och `Item2`. Det är svårt att läsa, särskilt när koden är längre än några rader. Ge alltid namn i tuple-typen:

```csharp
var person = (Name: "Ada", Age: 36);
Console.WriteLine(person.Name);    // Ada
Console.WriteLine(person.Age);     // 36
```

Nu är det tydligt vad varje värde betyder, både för dig och för den som läser koden efter dig. Namnen är också en kontroll: kompilatorn hjälper dig att skriva `person.Name` och inte `person.Nmae`.

## Varför har vi nytta av detta?

Vi slipper skapa en klass för två värden som bara används på ett ställe. Ett byte med en tuple är dessutom kortare och tydligare än med en tillfällig variabel.

## När kan vi använda detta?

Vi använder tuples när en metod behöver ge tillbaka två värden som hör ihop, och när vi vill byta två värden eller sätta flera variabler i en rad. Vi kan också ge fälten namn, så att den som läser koden förstår vad värdena betyder.

## När ska du inte använda tuples?

Om värdena hör ihop och har egna regler, till exempel att en bokning måste sluta efter att den börjat, då är en klass eller en `record` bättre. En tuple kan inte säga nej till något.

```csharp
// Med tuple: ingen regel skyddar datumen
var booking = (Start: new DateOnly(2026, 11, 20), End: new DateOnly(2026, 11, 18));   // Slutet före starten, ingen varnar

// Med klass: regeln ligger där datumen skapas
public class Booking
{
    public Booking(DateOnly start, DateOnly end)
    {
        if (end < start)
            throw new ArgumentException("Utcheckning kan inte vara före incheckning.");
        Start = start;
        End = end;
    }

    public DateOnly Start { get; }
    public DateOnly End { get; }
}
```

Med klassen kan ingen skapa en bokning som slutar före den börjar. Felet upptäcks där det uppstår, inte långt senare i programmet.

## Sammanfattning

- Tuples skickar tillbaka flera värden utan en ny klass.
- Deconstruction plockar isär dem i variabler.
- Ge fälten namn, och byt till en klass när värdena får egna regler.

## Prova själv

Gör `tuples_ovning.md`, `tuples_ovning_2.md` och `tuples_ovning_3.md`.
