# Klockan i text

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 40 minuter.

**Förkunskap:** Extension methods och switch-uttryck.

Skriv två extension methods på `TimeOnly`: en som hälsar utifrån klockslaget, och en som skriver klockslaget som svensk text.

## Varför vill man ha den här?

En app som hälsar "God morgon, Kim" när du loggar in. En väckarklocka för barn som säger "kvart över sju" i stället för 07:15. En skärmläsare som läser upp tiden för någon som inte ser skärmen. Alla behöver göra om ett klockslag till vanlig svenska.

`TimeOnly` vet vad klockan är, men inte hur man säger det på svenska. Med `nu.Greeting()` och `nu.ToSwedishText()` får typen de förmågorna, utan att du behöver ändra i den.

## Uppgifter

1. **🟢 Hälsning.** Skriv `Greeting(this TimeOnly time)`. Den returnerar "God morgon" (05–11), "God eftermiddag" (12–17), "God kväll" (18–22) eller "Godnatt" (23–04).
2. **🟡 Klockan i text.** Skriv `ToSwedishText(this TimeOnly time)`. Testa med 15:15 (ska bli "kvart över tre på eftermiddagen") och 09:00 ("nio på morgonen"). Börja med hela timmar och kvarter. Lägg till övriga minuter när det fungerar.
3. **🔴 Utmaning.** Gör 00:00 till "midnatt" och 12:00 till "tolv på dagen" i stället för "tolv på natten" och "tolv på eftermiddagen".

Om klockan är efter midnatt och du fortfarande kodar: vaffan gör du uppe så här sent 😴

Fyll i det som står `/* din kod */`.

```csharp
TimeOnly[] times = { new(7, 5), new(9, 0), new(12, 0), new(15, 15), new(21, 30), new(0, 0) };

foreach (var time in times)
    Console.WriteLine($"{time:HH:mm}  {time.Greeting()}, klockan är {time.ToSwedishText()}");

public static class TimeOnlyExtensions
{
    private static readonly string[] HourWords =
        { "tolv", "ett", "två", "tre", "fyra", "fem", "sex", "sju", "åtta", "nio", "tio", "elva" };

    public static string Greeting(this TimeOnly time)
    {
        /* din kod 🟢:
           titta på timmen
           05–11 ger God morgon, 12–17 God eftermiddag
           18–22 ger God kväll, annars Godnatt */
    }

    public static string ToSwedishText(this TimeOnly time)
    {
        /* din kod 🔴:
           om klockan är 00:00, returnera midnatt
           om klockan är 12:00, returnera tolv på dagen */

        /* din kod 🟡:
           gör om timmen till 12-timmarsformat och hämta ordet ur HourWords
           hämta också ordet för nästa timme
           välj minuttext: hel timme, fem över, kvart över, halv, kvart i ...
           välj tid på dygnet: på morgonen, på förmiddagen, på eftermiddagen ...
           returnera minuttexten och tiden på dygnet */
    }
}
```

Exempeloutput:

```text
07:05  God morgon, klockan är fem över sju på morgonen
09:00  God morgon, klockan är nio på morgonen
12:00  God eftermiddag, klockan är tolv på dagen
15:15  God eftermiddag, klockan är kvart över tre på eftermiddagen
21:30  God kväll, klockan är halv tio på kvällen
00:00  Godnatt, klockan är midnatt
```

Utan 🔴 blir 12:00 "tolv på eftermiddagen" och 00:00 "tolv på natten".

<details markdown="1">
<summary>Tips 1: hälsningen</summary>

Ett switch-uttryck på `time.Hour` med intervall passar bra, till exempel `>= 5 and < 12 => "God morgon"`. Låt `_` ta hand om natten, eftersom den går över midnatt.

</details>

<details markdown="1">
<summary>Tips 2: timmen som ord</summary>

`time.Hour % 12` ger timmen i 12-timmarsformat. Det ger 0 för både tolv och midnatt, och därför ligger "tolv" först i `HourWords`. Nästa timme får du med `(hour + 1) % 12`. Den behövs för "halv tre" och "kvart i tre".

</details>

<details markdown="1">
<summary>Tips 3: minuterna och tiden på dygnet</summary>

Gör ett switch-uttryck på minuterna: `0 => words`, `15 => $"kvart över {words}"`, `30 => $"halv {nextWords}"`, `45 => $"kvart i {nextWords}"` och så vidare. Vill du klara alla minuter kan du avrunda nedåt först: `time.Minute / 5 * 5`. Gör sedan ett till switch-uttryck på `time.Hour` för "på morgonen", "på kvällen" och de andra, och sätt ihop de två texterna. För 🔴 räcker två `if` allra först i metoden.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
TimeOnly[] times = { new(7, 5), new(9, 0), new(12, 0), new(15, 15), new(21, 30), new(0, 0) };

foreach (var time in times)
    Console.WriteLine($"{time:HH:mm}  {time.Greeting()}, klockan är {time.ToSwedishText()}");

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
        // 🔴 Specialfall
        if (time.Hour == 0 && time.Minute == 0) return "midnatt";
        if (time.Hour == 12 && time.Minute == 0) return "tolv på dagen";

        int hour = time.Hour % 12;
        string words = HourWords[hour];
        string nextWords = HourWords[(hour + 1) % 12];

        // Avrunda nedåt till närmaste fem minuter
        string minutes = (time.Minute / 5 * 5) switch
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
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
