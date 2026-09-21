# Dictionary — Poängsystem

Vi börjar med att skapa ett tomt dictionary och lägger in tre namn med poäng. Nyckeln är en `string`, värdet är en `int`:

```cs
Dictionary<string, int> poäng = new Dictionary<string, int>();
poäng.Add("Bella", 87);
poäng.Add("Carlos", 65);
poäng.Add("Alex", 42);
```

Nu hämtar vi värden via nyckel. Viktigt: om nyckeln inte finns kraschar programmet — därav `ContainsKey`:

```cs
Console.WriteLine(poäng["Bella"]);   // 87
Console.WriteLine(poäng["Alex"]);    // 42
if (poäng.ContainsKey("Jenny"))
{
    Console.WriteLine(poäng["Jenny"]); // ?
}
else
{
    Console.WriteLine("Jenny har inte Poängsatts än");
}
```

En hjälpmetod som sköter kontrollen och ändrar textfärg beroende på om personen finns eller inte:

```cs
void VisaPoäng(string namn)
{
    string text = "";
    if (poäng.ContainsKey(namn))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        text = namn + " har " + poäng[namn] + " poäng";
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        text = namn + " har inte poängsatts än";
    }
    
    Console.WriteLine(text);
    Console.ResetColor(); // <-- återgå till vanlig textfärg
}
```

Vi anropar den för fem namn — några som finns, några som saknas:

```cs
VisaPoäng("Carlos");
VisaPoäng("Jenny");
VisaPoäng("Alex");
VisaPoäng("Bella");
VisaPoäng("Betty");
```

Sedan loopar vi igenom hela dictionaryt med `foreach`. Linq ger oss `OrderBy` för att sortera på nyckel (namn) eller värde (poäng):

```cs
foreach (KeyValuePair<string,int> info in poäng)
{
    Console.WriteLine($"{info.Key} har {info.Value} poäng");
}

Console.WriteLine("Sorterad på namn");
foreach (KeyValuePair<string, int> info in poäng.OrderBy(namn => namn.Key))
{
    Console.WriteLine($"{info.Key} har {info.Value} poäng");
}

Console.WriteLine("Sorterad på poäng");
foreach (var info in poäng.OrderByDescending(poäng => poäng.Value))
{
    Console.WriteLine($"{info.Key} har {info.Value} poäng");
}
```

## Hela koden

```cs
// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;

Console.WriteLine("Hello, Dictionary!");

Dictionary<string, int> poäng = new Dictionary<string, int>();
poäng.Add("Bella", 87);
poäng.Add("Carlos", 65);
poäng.Add("Alex", 42);

Console.WriteLine(poäng["Bella"]);   // 87
Console.WriteLine(poäng["Alex"]);    // 42
if (poäng.ContainsKey("Jenny"))
{
    Console.WriteLine(poäng["Jenny"]); // ?
}
else
{
    Console.WriteLine("Jenny har inte Poängsatts än");
}
Console.WriteLine("----------------------------------------------");
VisaPoäng("Carlos");
VisaPoäng("Jenny");
VisaPoäng("Alex");
VisaPoäng("Bella");
VisaPoäng("Betty");

Console.WriteLine("----------------------------------------------");
Console.WriteLine("Foreach");

foreach (KeyValuePair<string,int> info in poäng)
{
    // interpolation av strängen istället för konkatenering
    // snyggare och mer lättläst
    Console.WriteLine($"{info.Key} har {info.Value} poäng");
}

Console.WriteLine("Sorterad på namn");
// Linq är en cool "plugin" till C#, den ger våra dictionaries OrderBy sortering
foreach (KeyValuePair<string, int> info in poäng.OrderBy(namn => namn.Key))
{
    Console.WriteLine($"{info.Key} har {info.Value} poäng");
}

Console.WriteLine("Sorterad på poäng");
foreach (var info in poäng.OrderByDescending(poäng => poäng.Value))
{
    Console.WriteLine($"{info.Key} har {info.Value} poäng");
}

// They said it couldn't be done...
// bra motto


void VisaPoäng(string namn)
{
    string text = "";
    if (poäng.ContainsKey(namn))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        text = namn + " har " + poäng[namn] + " poäng";
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        text = namn + " har inte poängsatts än";
    }
    
    Console.WriteLine(text);
    Console.ResetColor(); // <-- återgå till vanlig textfärg
}
```
