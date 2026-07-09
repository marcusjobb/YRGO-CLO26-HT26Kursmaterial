# Facit — Varberg vs Sisjön

Motsvarar: `03_villkor_och_loopar/exercises/varberg_vs_sisjoen/varberg_vs_sisjoen.md`

---

## Lösning

```csharp
// VarbergVsSisjoen.cs
// Räknar ut vilket köpalternativ som är billigast totalt med bensinkostnad inräknad

class VarbergVsSisjoen
{
    static void Main()
    {
        int tvVarberg = 9490;
        int tvSisjön = 9590;
        int bensinPerLiter = 17;
        int literVarberg = 15;
        int literSisjön = 2;

        // Räkna ut bensinkostnaden för varje resa
        int bensinVarberg = literVarberg * bensinPerLiter;
        int bensinSisjön = literSisjön * bensinPerLiter;

        // Räkna ut totalkostnaden inklusive bensinen
        int totalVarberg = tvVarberg + bensinVarberg;
        int totalSisjön = tvSisjön + bensinSisjön;

        Console.WriteLine($"NetOnNet Varberg:    {tvVarberg} kr");
        Console.WriteLine($"Elgiganten Sisjön:  {tvSisjön} kr");
        Console.WriteLine();
        Console.WriteLine($"Bensin till Varberg: {bensinVarberg} kr");
        Console.WriteLine($"Bensin till Sisjön:  {bensinSisjön} kr");
        Console.WriteLine();
        Console.WriteLine($"Totalt Varberg: {totalVarberg} kr");
        Console.WriteLine($"Totalt Sisjön:  {totalSisjön} kr");
        Console.WriteLine();

        // Jämför och visa det billigaste alternativet
        if (totalVarberg < totalSisjön)
            Console.WriteLine($"Kör till Varberg! Du sparar {totalSisjön - totalVarberg} kr.");
        else
            Console.WriteLine($"Köp på Sisjön! Du sparar {totalVarberg - totalSisjön} kr.");
    }
}
```

**Rätt svar:** Sisjön är billigast med 121 kr.
- Bensin Varberg: 15 × 17 = 255 kr → totalt 9 745 kr
- Bensin Sisjön: 2 × 17 = 34 kr → totalt 9 624 kr

---

## Vad tränar övningen

Övningen tränar aritmetik i flera steg, att lagra mellanresultat i variabler och att använda if/else för att jämföra och presentera ett svar. Poängen är att billigare prislapp inte alltid betyder billigast totalt — ett klassiskt verkligt problem.

## Vanliga misstag

- Jämför tv-priserna direkt utan att räkna in bensinen — missar hela poängen med uppgiften
- Skriver `totalVarberg - totalSisjön` utan att kontrollera vilket som är störst — kan ge negativt tal i utskriften
- Glömmer mellanvariablerna och försöker räkna allt i ett enda långt uttryck — svårare att läsa och felsöka

## Alternativa lösningar

Uppgiften kan också lösas med en gemensam metod som beräknar totalkostnaden och tar pris och liter som parametrar. Det är ett bra nästa steg att diskutera, men krävs inte för att lösa övningen.
