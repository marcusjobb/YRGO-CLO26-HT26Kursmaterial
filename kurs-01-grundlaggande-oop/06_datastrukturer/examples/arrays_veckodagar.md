# Array — Veckodagar

Tänk dig att du ska hålla koll på alla veckodagar utan en array:

```cs
string dag1 = "Måndag";
string dag2 = "Tisdag";
string dag3 = "Onsdag";
string dag4 = "Torsdag";
string dag5 = "Fredag";

Console.WriteLine(dag1);
Console.WriteLine(dag2);
// ... och så vidare
```

Fem variabler för fem dagar. Vad händer när du har 100 produkter? Det finns ett bättre sätt.

En array är en ordnad samling av värden av samma typ. Storleken bestäms när du skapar den och ändras aldrig:

```cs
string[] veckodagar = { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };
```

Fem dagar. En variabel. En rad kod.

Varje plats i arrayen har ett index. Och här är den viktiga grejen: index börjar på noll — inte ett. Det beror på hur minnet fungerar. Minnesadressen för position 0 är startadressen + 0, position 1 är startadressen + 1, och så vidare. Därav 0 till 4 när det är 5 platser:

```cs
Console.WriteLine(veckodagar[0]);  // Måndag
Console.WriteLine(veckodagar[1]);  // Tisdag
Console.WriteLine(veckodagar[4]);  // Fredag
```

`Length` berättar hur många element arrayen har. Med `for` kan vi gå igenom varje element — notera att vi skriver `i + 1` i utskriften för att visa "Dag 1" istället för "Dag 0":

```cs
Console.WriteLine(veckodagar.Length);  // 5

for (int i = 0; i < veckodagar.Length; i++)
{
    Console.WriteLine("Dag " + (i + 1) + ": " + veckodagar[i]);
}
```

`foreach` är enklare att läsa när vi inte behöver indexet — vi vill bara komma åt värdena:

```cs
foreach (string dag in veckodagar)
{
    Console.WriteLine("Dag: " + dag);
}
```

Tumregel: använd `for` när du behöver indexet. Använd `foreach` när du bara vill komma åt värdena.

## Hela koden

```cs
string[] veckodagar = { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag" };

Console.WriteLine(veckodagar[0]);  // Måndag
Console.WriteLine(veckodagar[1]);  // Tisdag
Console.WriteLine(veckodagar[4]);  // Fredag

Console.WriteLine(veckodagar.Length);  // 5

for (int i = 0; i < veckodagar.Length; i++)
{
    Console.WriteLine("Dag " + (i + 1) + ": " + veckodagar[i]);
}

foreach (string dag in veckodagar)
{
    Console.WriteLine("Dag: " + dag);
}
```
