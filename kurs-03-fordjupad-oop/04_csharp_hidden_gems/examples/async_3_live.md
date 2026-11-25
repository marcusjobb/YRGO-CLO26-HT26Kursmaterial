# async och await: tre stekpannor, och den som blir klar först skriver ut sig först

Tänk dig en frukost med tre stekpannor på spisen: bacon, pannkaka och ägg. De tar olika lång tid. Du står inte och stirrar på baconet i tre sekunder. Du går runt, tittar på varje panna, och säger till när något är klart. Det är precis så en asynkron loop fungerar.

## Genomgång från lektionen

Vi startar klockan och lägger de tre pannorna i en lista. Varje panna är ett namn och en `Task`, ett jobb som tar tid. Alla tre startar direkt:

```csharp
using System.Diagnostics;

var sw = Stopwatch.StartNew();

var pans = new List<(string name, Task task)>
{
    ("Baconet", Task.Delay(3000)),     // tar 3 sekunder
    ("Pannkakan", Task.Delay(1000)),   // tar 1 sekund
    ("Ägget", Task.Delay(2000))        // tar 2 sekunder
};
```

Lägg märke till ordningen: baconet står först i listan men tar längst tid. Det spelar roll nedan.

Vi behöver också veta vilka pannor som redan är rapporterade klara:

```csharp
var done = new List<string>();
```

Nu själva rundan. Så länge inte alla är klara går vi igenom pannorna. Är en klar, och vi inte redan har sagt det, skriver vi ut det:

```csharp
while (done.Count < pans.Count)
{
    foreach (var (name, task) in pans)
    {
        if (task.IsCompleted && !done.Contains(name))
        {
            done.Add(name);
            Console.WriteLine($"Klart: {name} ({sw.Elapsed.TotalSeconds:F0} s)");
        }
    }
    // Här kan programmet göra något annat medan pannorna steker
    await Task.Delay(100);
}
```

`await Task.Delay(100)` ger en liten paus mellan varje runda, så att datorn inte snurrar för fullt. Och på raden över den kan programmet göra något helt annat medan pannorna steker.

Utskriften kommer i den ordning pannorna blir klara, inte i listans ordning:

```
Klart: Pannkakan (1 s)
Klart: Ägget (2 s)
Klart: Baconet (3 s)
```

Baconet stod först i listan men skrivs ut sist, eftersom det tog längst tid. Tiden kan hamna på 4 i stället för 3 vid en kall första körning, eftersom klockan avrundas och starten tar en stund.

## Hela koden

```csharp
using System.Diagnostics;

var sw = Stopwatch.StartNew();

var pans = new List<(string name, Task task)>
{
    ("Baconet", Task.Delay(3000)),     // tar 3 sekunder
    ("Pannkakan", Task.Delay(1000)),   // tar 1 sekund
    ("Ägget", Task.Delay(2000))        // tar 2 sekunder
};

var done = new List<string>();

while (done.Count < pans.Count)
{
    foreach (var (name, task) in pans)
    {
        if (task.IsCompleted && !done.Contains(name))
        {
            done.Add(name);
            Console.WriteLine($"Klart: {name} ({sw.Elapsed.TotalSeconds:F0} s)");
        }
    }
    // Här kan programmet göra något annat medan pannorna steker
    await Task.Delay(100);
}
```
