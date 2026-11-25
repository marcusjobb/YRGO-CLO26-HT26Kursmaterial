# async och await: tre väntanden, en i taget eller alla på en gång

Tre jobb som tar en sekund var. Väntar vi på dem efter varandra tar det tre sekunder. Startar vi dem alla på en gång och väntar på alla tre tillsammans tar det en sekund. Här mäter vi det med en klocka, så att vi ser skillnaden i siffror.

## Genomgång från lektionen

Vi börjar med en `Stopwatch`, och väntar på tre pauser, en efter en. Varje `await` stannar tills just den pausen är klar:

```csharp
using System.Diagnostics;

var sw = Stopwatch.StartNew();
await Task.Delay(1000);
await Task.Delay(1000);
await Task.Delay(1000);
Console.WriteLine($"En i taget: {sw.Elapsed.TotalSeconds:F0} s");   // 3 s
```

Utskriften blir `En i taget: 3 s`. Pauserna startade först när den förra var klar, så tiden läggs ihop.

Nu nollställer vi klockan och startar alla tre pauserna samtidigt. `Task.WhenAll` väntar tills alla är klara:

```csharp
sw.Restart(); // starta om klockan
// Alla tre pauserna på en gång
await Task.WhenAll(Task.Delay(1000), Task.Delay(1000), Task.Delay(1000));
Console.WriteLine($"Alla samtidigt: {sw.Elapsed.TotalSeconds:F0} s");   // 1 s
```

Nu blir det `Alla samtidigt: 1 s`. Tre jobb, men vi väntade bara på det längsta av dem. Det är därför man startar flera anrop till databasen eller internet samtidigt när de inte beror på varandra.

## Hela koden

```csharp
using System.Diagnostics;

Console.WriteLine("Hello, World!");

var sw = Stopwatch.StartNew();
await Task.Delay(1000);
await Task.Delay(1000);
await Task.Delay(1000);
Console.WriteLine($"En i taget: {sw.Elapsed.TotalSeconds:F0} s");   // 3 s

sw.Restart(); // starta om klockan
// Alla tre pauserna på en gång
await Task.WhenAll(Task.Delay(1000), Task.Delay(1000), Task.Delay(1000));
Console.WriteLine($"Alla samtidigt: {sw.Elapsed.TotalSeconds:F0} s");   // 1 s
```
