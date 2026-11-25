# Tuples: returnera flera värden från en metod

En metod kan bara returnera ett värde. Med en tuple kan vi ändå returnera flera, i stället för att skapa en klass för det.

## Övningen

Skriv klart metoden `GetHighestNumber`. Den tar en lista med heltal och returnerar det lägsta och det högsta värdet som en tuple.

```csharp
var lista = new int[] { 5, 1, 13, 87, 89, 22 };

var (min, max) = GetHighestNumber(lista);
Console.WriteLine($"{min} {max}");

(int min, int max) GetHighestNumber(int[] list)
{
    /* din kod:
       hitta det lägsta talet i list
       hitta det högsta talet i list
       returnera båda, lägst först */
}
```

Exempeloutput:

```text
1 89
```

<details markdown="1">
<summary>Tips 1: vad ska metoden lämna tillbaka?</summary>

Returtypen är `(int min, int max)`, alltså två värden. Du returnerar dem i en parentes, i samma ordning: `return (lägst, högst);`

</details>

<details markdown="1">
<summary>Tips 2: låt LINQ räkna</summary>

En array har metoderna `Min()` och `Max()` från LINQ. Glöm inte parenteserna. `list.Min` utan `()` är själva metoden, inte värdet, och då klagar kompilatorn.

</details>

<details markdown="1">
<summary>Tips 3: göra det för hand</summary>

Vill du göra det utan LINQ? Börja med `int lägst = list[0];` och `int högst = list[0];`. Gå sedan igenom resten av listan med en loop, och byt värde varje gång du hittar något lägre eller högre.

</details>

<details markdown="1">
<summary>Tips 4: utskriften</summary>

Raden `var (min, max) = GetHighestNumber(lista);` är redan skriven. Den plockar isär tuplen i två variabler (deconstruction), så du behöver bara skriva klart metoden.

</details>

## Genomgång från lektionen

Läs vidare när du har försökt själv. Här är lösningen vi skrev tillsammans.

Uppgiften är att skriva en metod som tar en lista med heltal och returnerar det lägsta och det högsta värdet:

```csharp
var lista = new int[]{ 5, 1, 13, 87, 89, 22 };
```

Metoden deklareras med en tuple som returtyp. Namnen `min` och `max` hjälper den som läser koden, men de är bara namn:

```csharp
(int min, int max) GetHighestNumber(int[] list)
{
    // ...
}
```

Anropet använder **deconstruction**. Tuplen delas upp direkt i två variabler:

```csharp
var (min, max) = GetHighestNumber(lista);

Console.WriteLine($"{min} {max}");
```

Utskriften blir `1 89`.

I metoden finns en lång kommentar med en for-loop som jämför varje värde. Det är ett alternativt sätt att hitta min och max, och vi har kvar det som ett exempel på hur man gör det för hand. Det är bra att kunna, men den färdiga versionen använder `Min()` och `Max()` från LINQ, som gör samma jobb på två rader:

```csharp
int lowest = list.Min();
int highest = list.Max();

return (lowest,highest);
```

Lägg märke till att `return` skickar två värden samtidigt. Det är därför returtypen är en tuple.

## Extra: deconstruction, att plocka isär en tuple

Deconstruction betyder att vi plockar isär en tuple i separata variabler. Tänk på en tuple som ett paket med flera saker. Deconstruction packar upp paketet.

```csharp
var person = ("Maria", 25);

var (namn, ålder) = person;
Console.WriteLine($"{namn} är {ålder} år");
```

Varje variabel får ett värde i samma ordning som i tuplen. `namn` får `"Maria"` och `ålder` får `25`. Utskriften blir `Maria är 25 år`.

Om vi inte behöver ett av värdena kan vi skriva `_` i stället för ett namn. Då ignoreras det:

```csharp
var (namn, _) = person;
Console.WriteLine(namn);
```

### Tre sätt att packa upp något

Det finns tre vanliga sätt att få något att kunna plockas isär. Alla ger samma resultat i slutet, men de kommer från olika ställen.

**1. En tuple.** C# vet redan hur en tuple ska plockas isär. Du behöver inte skriva något extra.

```csharp
var person = ("Maria", 25);
var (namn, ålder) = person;
```

**2. En egen klass med en `Deconstruct`-metod.** Här måste du tala om för C# hur klassen ska plockas isär. Metoden heter `Deconstruct` och tar värdena som `out`-parametrar:

```csharp
class Person
{
    public string Namn { get; }
    public int Ålder { get; }

    public Person(string namn, int ålder)
    {
        Namn = namn;
        Ålder = ålder;
    }

    public void Deconstruct(out string namn, out int ålder)
    {
        namn = Namn;
        ålder = Ålder;
    }
}
```

**3. En record.** Du behöver inte skriva `Deconstruct` själv. C# gör det automatiskt när du skriver de två värdena i parentes efter namnet:

```csharp
record Person(string Namn, int Ålder);

var person = new Person("Maria", 25);
var (namn, ålder) = person;
```

Det enda som skiljer dem åt är hur mycket du behöver skriva själv. En tuple är enklast, en egen klass ger full kontroll, och en record är ett kompromiss mellan de två.

Samma sak händer i liveprogrammet. `var (min, max) = GetHighestNumber(lista);` plockar isär tuplen som metoden returnerar, och `(a, b) = (b, a);` gör det i omvänd ordning för att byta värden.

## Varför har vi nytta av detta?

Metoden kan returnera två värden utan att vi skapar en klass bara för det. Deconstruction ger oss variablerna direkt i anropet, så koden blir lätt att läsa.

## När kan vi använda detta?

Det passar när en metod räknar fram två eller flera värden, till exempel min och max. Det passar också när två värden hör ihop men inte har några egna regler, för då är en klass onödigt tung. Och när du vill plocka isär ett objekt kan du använda en egen `Deconstruct` eller en record.

## Hela koden

```csharp
Console.WriteLine("Hello, Tuples!");

// Skriv en metod som tar en lista med heltal och returnerar (int min, int max).
// Använd deconstruction för att skriva ut min och max.

var lista = new int[]{ 5, 1, 13, 87, 89, 22 };

var (min, max) = GetHighestNumber(lista);

Console.WriteLine($"{min} {max}");

(int min, int max) GetHighestNumber(int[] list)
{

    //int highest = list[0];
    //int lowest = list[0];
    //for(int i=1;i<list.Length;i++)
    //{
    //    Console.Write($"high:{highest}<{list[i]}-->");

    //    if (highest < list[i])
    //        highest = list[i];
    //    Console.WriteLine(highest);

    //    Console.Write($"low :{lowest}<{list[i]}-->");
    //    if (lowest > list[i])
    //        lowest = list[i];
    //    Console.WriteLine(lowest);
    //}

    int lowest = list.Min();
    int highest = list.Max();

    return (lowest,highest);
}
```
