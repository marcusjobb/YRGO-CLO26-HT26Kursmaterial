# Klockan i text

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 40 minuter.

**Förkunskap:** Extension methods och switch-uttryck.

Skriv två extension methods på `TimeOnly`: en som hälsar utifrån klockslaget, och en som skriver klockslaget som svensk text.

## Uppgifter

1. **🟢 Hälsning.** Skriv `Greeting(this TimeOnly time)`. Den returnerar "God morgon" (05–11), "God eftermiddag" (12–17), "God kväll" (18–22) eller "Godnatt" (23–04).
2. **🟡 Klockan i text.** Skriv `ToSwedishText(this TimeOnly time)`. Testa med 15:15 (ska bli "kvart över tre på eftermiddagen") och 09:00 ("nio på morgonen"). Börja med hela timmar och kvarter. Lägg till övriga minuter när det fungerar.
3. **🔴 Utmaning.** Gör 00:00 till "midnatt" och 12:00 till "tolv på dagen" i stället för "tolv på natten" och "tolv på eftermiddagen".

Tips: Vill du räkna fram timmen i 12-timmarsformat? `time.Hour % 12` ger 0 för både tolv och midnatt. Då kan du hämta ordet ur en array.

Om klockan är efter midnatt och du fortfarande kodar: vaffan gör du uppe så här sent 😴

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
public static class TimeOnlyExtensions
{
    private static readonly string[] HourWords =
        { "tolv", "ett", "två", "tre", "fyra", "fem", "sex", "sju", "åtta", "nio", "tio", "elva" };

    public static string Greeting(this TimeOnly time) => time.Hour switch
    {
        >= 5 and < 12 => "God morgon",
        >= 12 and < 18 => "God eftermiddag",
        >= 18 and < 23 => "God kväll",
        _ => "Godnatt"
    };

    public static string ToSwedishText(this TimeOnly time)
    {
        int hour = time.Hour % 12;
        string words = HourWords[hour];
        string nextWords = HourWords[(hour + 1) % 12];

        string minutes = time.Minute switch
        {
            0 => words,
            5 => $"fem över {words}",
            10 => $"tio över {words}",
            15 => $"kvart över {words}",
            20 => $"tjugo över {words}",
            25 => $"fem i halv {nextWords}",
            30 => $"halv {nextWords}",
            35 => $"fem över halv {nextWords}",
            40 => $"tjugo i {nextWords}",
            45 => $"kvart i {nextWords}",
            50 => $"tio i {nextWords}",
            _ => $"fem i {nextWords}"
        };

        string period = time.Hour switch
        {
            >= 5 and < 10 => "på morgonen",
            >= 10 and < 12 => "på förmiddagen",
            >= 12 and < 18 => "på eftermiddagen",
            >= 18 and < 23 => "på kvällen",
            _ => "på natten"
        };

        return $"{minutes} {period}";
    }
}

// Användning
Console.WriteLine(new TimeOnly(15, 15).ToSwedishText());   // kvart över tre på eftermiddagen
Console.WriteLine(new TimeOnly(7, 5).Greeting());          // God morgon
```

</details>
