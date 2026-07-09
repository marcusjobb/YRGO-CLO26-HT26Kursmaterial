# Facit — Nedräkning

Motsvarar: `03_villkor_och_loopar/exercises/countdown/countdown.md`

---

## Lösning

### Steg 1 — for-loop som räknar ned

```csharp
// Countdown.cs
// Räknar ned från 10 till 0 och skriver ut ett startmeddelande

class Countdown
{
    static void Main()
    {
        for (int i = 10; i >= 0; i--)
            Console.WriteLine(i);

        Console.WriteLine("Dags att börja!");
    }
}
```

### Steg 2 — while-loop med jämna tal

**Variant A — modulo-kontroll:**

```csharp
// Skriver ut jämna tal från 2 till 20 med hjälp av modulo
int tal = 1;
while (tal <= 20)
{
    if (tal % 2 == 0)
        Console.WriteLine(tal);
    tal++;
}
```

**Variant B — börja på 2 och hoppa med 2:**

```csharp
// Skriver ut jämna tal från 2 till 20 genom att stega med 2
int tal = 2;
while (tal <= 20)
{
    Console.WriteLine(tal);
    tal += 2;
}
```

---

## Vad tränar övningen

Steg 1 tränar for-loopens syntax med ett negativt steg (`i--`). Steg 2 tränar while-loopen med ett eget avslutningsvillkor och ger utrymme för två olika lösningsstrategier — båda är korrekta.

## Vanliga misstag

- Skriver `i > 0` i stället för `i >= 0` i for-loopen — nollan skrivs aldrig ut
- Glömmer `tal++` i while-loopen (variant A) — oändlig loop
- Startar `tal` på 0 i variant A — skriver ut 0 som ett jämnt tal, vilket tekniskt är rätt men ofta inte är avsikten

## Alternativa lösningar

Variant B är mer effektiv eftersom den inte behöver kontrollera varje tal, men variant A är pedagogiskt värdefull eftersom modulo-operatorn (`%`) är ett viktigt verktyg att lära sig tidigt. Båda varianterna godkänns — lyft gärna båda i genomgången.
