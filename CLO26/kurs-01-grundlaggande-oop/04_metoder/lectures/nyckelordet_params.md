---

title: Params – den mystiska parameter
author: Marcus Ackre Medina
type: lecture
topic: methods
difficulty: 2
language: csharp
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/Nyckelordet Params.md"
description: "Först och främst måste det klargöras att params inte har något över huvudtaget att göra med SQL kommandots parameter. Params är ett nyckelord i C# för att hantera inparameter i en metod."
tags: ["arrays", "csharp", "methods", "metoder", "mystiska", "nyckelord", "parameter", "params"]
week_fit: []
---

# Params – den mystiska parameter

🟡



Först och främst måste det klargöras att params inte har något över huvudtaget att göra med SQL kommandots parameter. Params är ett nyckelord i C# för att hantera inparameter i en metod.

Microsoft har en beskrivning och exempel på det här 
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/params

Den används som ett sätt att hantera arrays som skickas som inparameter till en metod.

Vi tar en titt på  ett kodexempel

```


static void Main(string[] args)
{
var names = new string[] { "James", "Bond", "007" };
    WriteTheNames(names);
}
private static void WriteTheNames(string[] names)
{
    foreach (var name in names)
    {
        Console.WriteLine(name);
    }
}
```


Kodsnutten skapar en array med namn som sedan skickas till en metod som skriver ut dem. Då det är en array så kan vi ha hur många namn vi vill i den.
var names = new string[] { "Peter Parker","Spiderman"};

eller
var names = new string[] { "Batman"};


vår metod kommer inte att bry sig om hur många namn vi skickar med i listan. Den kommer bara att skriva ut dem.

Ett enklare sätt att skriva anropet till metoden som skriver ut namnen är detta
WriteTheNames(new string[] { "Ironman","Pepper Potts","J.A.R.V.I.S." });

Då skapar vi en array samtidigt som vi skickar in den till metoden som skriver ut listan på namn.

Men detta borde man kunna göra på ett snyggare sätt, och det är där Params kommer in i bilden. Vi ändrar i metodhuvudet och lägger till params nyckelordet.

```


private static void WriteTheNames(params string[] names)
{
foreach (var name in names)
{
Console.WriteLine(name);
}
}
```

Om vi kör koden som vi skrivit innan kommer det att fungera precis likadant
WriteTheNames(new string[] { "Ironman","Pepper Potts","J.A.R.V.I.S." });

Men vi får nu möjligheten att göra en sak vi inte kunnat göra innan och det är att skicka in värden i följd och låta .Net omvandla det till en array.

Vi kan alltså skriva såhär och metoden kommer ändå att acceptera vår input.
WriteTheNames("Ironman","Pepper Potts","J.A.R.V.I.S.");

Vi kan ändra antalet inparametrar vi skickar in precis som innan
WriteTheNames("Batman","Robin","Catwoman");

eller

```
WriteTheNames("Darkman");
```

Metoden tar fortfarande emot en Array men vi skickar arrayen som inparameter istället för en array. Man gör så för att koden ska bli lite enklare att läsa och det ser snyggare ut, men i övrigt är det absolut ingen skillnad på metoden, den tar och emot och bearbetar en array.

Params är ett sätt att få koden att bli snyggare bara, inget annat än det.

Med params nyckelordet kan du skriva

var names = new string[] { "James", "Bond", "007" };
WriteTheNames(names);

eller

```
WriteTheNames(new string[] { "Ironman","Pepper Potts","J.A.R.V.I.S." });
```


eller

WriteTheNames("Batman","Robin","Catwoman");


En viktig sak dock…
Då params omvandlar alla inparametrar som följer, till en array så måste params nyckelordet finnas som sista inparameter i en metod

Detta kommer inte att fungera, då params inte är sist.

```
private static void WriteTheNames(params string[] names, int numbers)
```


Men detta fungerar, då params har lagts till i sista inparametern

```
private static void WriteTheNames(int numbers, params string[] names)
```


Därmed har vi avmystifierat nyckelordet params.


Happy coding!
/Marcus

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
