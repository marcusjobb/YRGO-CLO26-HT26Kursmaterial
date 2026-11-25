# async och await: starta jobbet nu, vänta på svaret sen

Ibland tar något tid: en fråga till en databas, ett anrop till internet. Om programmet bara står och väntar går ingenting annat att göra. Med `async` och `await` startar vi jobbet, gör annat under tiden, och hämtar svaret när vi faktiskt behöver det. Det här exemplet är det allra minsta: en metod som väntar i fyra sekunder och sedan svarar "hej".

## Genomgång från lektionen

Först metoden som tar tid. Den är `async` och returnerar en `Task<string>`, ett löfte om en text som kommer senare. `Task.Delay` är vårt låtsasjobb: det väntar utan att frysa tråden.

```csharp
async Task<string> FetchGreetingAsync()
{
    await Task.Delay(4000);       // Väntar en sekund utan att frysa tråden
    return "hej";
}
```

Lägg märke till att siffran är `4000`, alltså fyra sekunder. Kommentaren i koden säger en sekund, men det är siffran som styr.

Nu startar vi jobbet. Vi skriver inte `await` här, så programmet går vidare direkt:

```csharp
var greetingTask = FetchGreetingAsync();   // Startar jobbet, men väntar inte på det
Console.WriteLine("!!!");                  // Skrivs ut direkt, medan väntan pågår
```

`greetingTask` är inte texten "hej". Det är löftet om att få den. Därför skrivs `!!!` ut direkt, medan jobbet fortfarande går.

Först nu, när vi vill ha svaret, väntar vi på det:

```csharp
var greeting = await greetingTask;         // Nu väntar vi, men utan att blockera tråden
Console.WriteLine(greeting);
```

Utskriften blir `Hello, World!`, direkt efter det `!!!`, och först fyra sekunder senare `hej`. Det är hela poängen: `!!!` hann komma före svaret.

## Hela koden

```csharp
Console.WriteLine("Hello, World!");

async Task<string> FetchGreetingAsync()
{
    await Task.Delay(4000);       // Väntar en sekund utan att frysa tråden
    return "hej";
}

var greetingTask = FetchGreetingAsync();   // Startar jobbet, men väntar inte på det
Console.WriteLine("!!!");                  // Skrivs ut direkt, medan väntan pågår

var greeting = await greetingTask;         // Nu väntar vi, men utan att blockera tråden
Console.WriteLine(greeting);
```
