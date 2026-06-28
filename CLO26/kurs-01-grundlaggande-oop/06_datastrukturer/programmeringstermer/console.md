# Programmeringstermer — Console (fördjupning)

Fortsättning på [grundläggande Console](../../01_verktyg_och_git/programmeringstermer/console.md) (`WriteLine`/`Write`/`Clear`/färger/cursor-position). Det här är verktygen du faktiskt behöver för att bygga spelet i sista veckans projekt — inläsning utan att låsa programmet, koll på terminalens storlek, och hur man bygger en uppdaterande vy istället för att spamma raden.

---

## Console.ReadKey()

Du känner redan [`Console.ReadLine()`](../../01_verktyg_och_git/programmeringstermer/console.md#consolereadline) — läser in en hel rad och väntar på Enter. I ett spel räcker det sällan; du vill att något händer **direkt** när spelaren trycker en tangent, utan att vänta på Enter. Det är jobbet för `ReadKey()` — läser in **ett enda** tangenttryck.

```csharp
Console.WriteLine("Tryck en pil-tangent för att röra dig...");
ConsoleKeyInfo tangent = Console.ReadKey(true);

switch (tangent.Key)
{
    case ConsoleKey.UpArrow:
        Console.WriteLine("Du rör dig uppåt!");
        break;
    case ConsoleKey.DownArrow:
        Console.WriteLine("Du rör dig nedåt!");
        break;
    default:
        Console.WriteLine("Den tangenten gör ingenting än.");
        break;
}
```

### Output
```plaintext
Tryck en pil-tangent för att röra dig...
Du rör dig uppåt!
```

<details><summary>Vad gör `true` i `ReadKey(true)`?</summary>

Argumentet styr om tangenten du trycker **syns** i terminalen eller inte (`true` = göm den, `false` eller inget argument = visa den, som ett vanligt tecken). I ett spel vill du nästan alltid gömma den — annars fylls skärmen med bokstäver varje gång spelaren styr sin karaktär, istället för att bara se spelet uppdateras.

</details>

**Se även:** [Console.KeyAvailable](#consolekeyavailable) — för att kolla efter tangenttryck utan att stanna programmet helt.

---

## Console.KeyAvailable

`ReadKey()` **stannar** programmet och väntar tills någon trycker en tangent. Det funkar dåligt i ett spel där saker ska hända kontinuerligt (en fiende som rör sig, en timer som tickar) oavsett om spelaren trycker något just nu. `KeyAvailable` löser det — den kollar **om** en tangent väntar, utan att stanna och vänta på den.

```csharp
Console.WriteLine("Spelet kör... tryck valfri tangent för att avsluta.");

bool kor = true;
int tick = 0;

while (kor)
{
    tick++;
    Console.WriteLine($"Tick {tick}...");

    if (Console.KeyAvailable)
    {
        Console.ReadKey(true); // töm tangenten ur kön
        kor = false;
    }

    System.Threading.Thread.Sleep(500); // vänta en halvsekund mellan varje tick
}

Console.WriteLine("Spelet avslutat.");
```

### Output
```plaintext
Spelet kör... tryck valfri tangent för att avsluta.
Tick 1...
Tick 2...
Tick 3...
Spelet avslutat.
```

<details><summary>Det här är grunden för en spel-loop</summary>

Mönstret `while (kor) { uppdatera tillstånd; kolla input; rita om skärmen; vänta lite; }` är, lite förenklat, hur de allra flesta enkla spel fungerar under huven — oavsett om det är ett terminalspel eller ett grafiskt spel med ett bibliotek. `KeyAvailable` är vad som låter spelet fortsätta tugga på *även när spelaren inte gör något*.

</details>

---

## Console.WindowWidth / WindowHeight

Talar om hur många kolumner respektive rader det aktuella terminalfönstret faktiskt har just nu. Användbart för att centrera text, rita en ram, eller se till att spelplanen får plats.

```csharp
Console.WriteLine($"Terminalen är {Console.WindowWidth} kolumner bred och {Console.WindowHeight} rader hög.");

string rubrik = "MITT SPEL";
int vänsterMarginal = (Console.WindowWidth - rubrik.Length) / 2;
Console.SetCursorPosition(vänsterMarginal, 0);
Console.WriteLine(rubrik);
```

### Output
```plaintext
Terminalen är 120 kolumner bred och 30 rader hög.
                                                  MITT SPEL
```

*(De exakta talen beror på din egen terminal — testa själv och se vad du får.)*

**Se även:** [Console.BufferWidth / BufferHeight](#consolebufferwidth--bufferheight) — den *andra* storleken, lätt att blanda ihop med denna.

---

## Console.BufferWidth / BufferHeight

`Window`-storleken är hur mycket du **ser** just nu. `Buffer`-storleken är hur mycket som faktiskt **finns** — bufferten kan vara mycket större än fönstret, och det du inte ser just nu går att scrolla fram till.

```csharp
Console.WriteLine($"Synligt fönster: {Console.WindowWidth} x {Console.WindowHeight}");
Console.WriteLine($"Faktisk buffert: {Console.BufferWidth} x {Console.BufferHeight}");
```

### Output
```plaintext
Synligt fönster: 120 x 30
Faktisk buffert: 120 x 9001
```

<details><summary>Varför skulle bufferten vara större än fönstret?</summary>

Tänk på hur scroll-historiken i din terminal funkar — du kan scrolla uppåt och se rader som skrevs ut för länge sedan, även om de inte syns just nu. Det är bufferten. Fönstret är bara den del av bufferten som råkar visas på skärmen just nu.

För ett spel vill du normalt att de är **lika stora** (ingen scroll, ingen förvirring om var spelplanen egentligen är) — annars kan `SetCursorPosition` hamna utanför det synliga fönstret utan att du förstår varför. Sätt dem lika med `Console.SetWindowSize(bredd, höjd)` och `Console.SetBufferSize(bredd, höjd)` i den ordningen (bufferten måste vara minst lika stor som fönstret, så krymp fönstret innan du krymper bufferten om du går åt det hållet).

</details>

---

## Console.CursorVisible

Visar eller döljer den blinkande markören. I ett spel vill du nästan alltid ha den dold — den blinkande strecket mitt i spelplanen ser bara förvirrande ut.

```csharp
Console.CursorVisible = false;
// ... resten av spelet ...
Console.CursorVisible = true; // kom ihåg att sätta tillbaka den när programmet avslutas
```

---

## Bygga en enkel animation

Kombinerar `SetCursorPosition` (från grundnivån) med en loop och `Thread.Sleep` — exakt mönstret bakom en laddningsanimation eller en rörlig karaktär.

```csharp
string[] frames = { "|", "/", "-", "\\" };

for (int i = 0; i < 20; i++)
{
    Console.SetCursorPosition(0, 0);
    Console.Write($"Laddar... {frames[i % frames.Length]}");
    System.Threading.Thread.Sleep(150);
}

Console.SetCursorPosition(0, 0);
Console.WriteLine("Laddar... Klart!");
```

<details><summary>Varför `i % frames.Length`?</summary>

`frames` har bara 4 element (index 0–3), men loopen kör 20 varv. `i % frames.Length` ("i modulo 4") ger resten vid heltalsdivision — talet hoppar 0, 1, 2, 3, 0, 1, 2, 3... och börjar om varje gång det skulle gått utanför arrayen. Det är så du loopar genom en kort lista av "frames" oändligt många gånger utan att krascha med ett index utanför gränserna.

</details>

---

## Nästa steg

Allt ovan räcker gott och väl för ett enkelt terminalspel — en spelplan ritad med `WriteLine`/cursor-position, input via `ReadKey`/`KeyAvailable`, och en loop som håller allt vid liv. Vill ni senare bygga något snyggare (riktiga färgteman, menyer med piltangentnavigering, smidigare rendering) finns externa bibliotek som `Spectre.Console` — men det är frivilligt överkurs, inte ett krav för projektveckan.
