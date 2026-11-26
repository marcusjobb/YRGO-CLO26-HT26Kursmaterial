# Extension methods: lägg till en metod på string

`string` är förseglad, så vi kan inte ärva från den och lägga till egna metoder. En extension method låter oss ändå skriva `name.UpperFirstLetter()`, som om metoden alltid hade funnits.

## Genomgång från lektionen

Så här vill vi kunna använda den. Vi har ett namn med bara gemener och vill ha en stor första bokstav:

```csharp
Console.WriteLine("Hello, Extensions!");

string name = "marcus";
Console.WriteLine(name.UpperFirstLetter());

string hello = "hello".UpperFirstLetter();
```

Lägg märke till att metoden anropas direkt på en `string`, och även direkt på en text i citattecken. Utskriften blir `Marcus`.

Själva metoden ligger i en `static` klass:

```csharp
static class StringExtender // Namnet på klassen är oviktig egentligen 
{                           // Namner förklarar dock vad vi har klassen till
```

Klassens namn spelar ingen roll för att det ska fungera, men ett bra namn berättar för den som läser vad klassen är till för.

Metoden är `static`, och det som gör den till en extension method är ordet `this` framför första parametern:

```csharp
    // datatypen efter this talar om vilken klass vi påverkar
    public static string UpperFirstLetter(this string name)
    {
        string newName = name[..1].ToUpper() + name[1..].ToLower();
        return newName;
    }


}
```

Datatypen efter `this` är den typ vi lägger till metoden på. Här är det `string`. Metoden tar första bokstaven (`name[..1]`) och gör den stor, och resten (`name[1..]`) och gör den liten.

I samma projekt låg även en klass, `whatever`, med en kommentar om att `string` redan har massor av extension methods:

```csharp
namespace Extensions_01_Live
{
    //using System.Linq;
    internal class whatever
    {

        public void something()
        {

            string name = "";
            // string får massvis med förvirrande extensions

        }

    }
}
```

### Vad har `string` på riktigt, och vad är extension methods?

När vi skriver `name.` i Visual Studio dyker en lång lista upp, och det ser ut som att allt är "metoder på string". Det är det inte. Listan blandar två olika saker.

**1. Metoder som `string` själv har.** De ligger i klassen `string` och finns alltid, utan något `using`:

```csharp
string name = "marcus";
Console.WriteLine(name.ToUpper());   // MARCUS
Console.WriteLine(name.Length);      // 6
Console.WriteLine(name.Contains("cus")); // True
```

Andra exempel är `ToLower`, `Substring`, `Replace`, `Split`, `Trim`, `StartsWith` och `IndexOf`. Det här är stringens verkliga metoder.

**2. Extension methods som någon annan har lagt dit.** Det är static metoder i en annan klass, som ser ut som att de hör till `string`. När vi tar bort kommentaren på `using System.Linq;` får `string` plötsligt fler metoder i listan, eftersom en `string` också är en samling tecken (`IEnumerable<char>`) och LINQ har extension methods för alla samlingar:

```csharp
Console.WriteLine(name.Count());     // 6
Console.WriteLine(name.First());     // m
Console.WriteLine(new string(name.Reverse().ToArray())); // sucram
```

Metoderna `Count`, `First` och `Reverse` finns inte i klassen `string`. De är extension methods från `System.Linq`, och det är de som gör listan "förvirrande": de ligger blandade med stringens egna metoder.

Vår egen `UpperFirstLetter` från exemplet ovan hamnar i samma lista, med samma blå pil. Skillnaden mot LINQ är bara att den kommer från din klass `StringExtender`.

Några saker att veta:

- **Ett `using` krävs.** En extension method syns bara när klassens namnrymd är importerad. `UpperFirstLetter` ligger i samma fil, men LINQ-metoderna kräver `using System.Linq;`. I projekt med `ImplicitUsings` på läggs `System.Linq` till åt oss, och då slipper vi skriva raden.
- **Stringens egna metoder vinner.** Finns både en vanlig metod och en extension method med samma namn och samma parametrar används den vanliga. Det är därför `name.Contains("cus")` anropar stringens egen `Contains`.
- **Extension methods ändrar inte `string`.** De är vanliga static metoder som bara *ser ut* som att de hör dit. `name.UpperFirstLetter()` betyder egentligen `StringExtender.UpperFirstLetter(name)`.
- **Ikonen visar skillnaden.** I Visual Studio har en extension method en liten blå pil på ikonen i förslagslistan, och beskrivningen börjar med `(extension)`.

## Varför har vi nytta av detta?

Vi kan lägga till egna verktyg på typer vi inte äger, som `string`, `int` eller `List`, utan att ärva eller ändra dem. Koden läser också bättre: `name.UpperFirstLetter()` går att läsa från vänster till höger.

## När kan vi använda detta?

När vi ofta gör samma sak med en typ vi inte kan ändra, till exempel att städa en text, kolla ett datum eller summera en lista. LINQ (`Where`, `Select`, `OrderBy`) är i grunden bara extension methods på samlingar.

## Hela koden

`Program.cs`:

```csharp
using System.Reflection.PortableExecutable;

Console.WriteLine("Hello, Extensions!");

string name = "marcus";
Console.WriteLine(name.UpperFirstLetter());

string hello = "hello".UpperFirstLetter();

static class StringExtender // Namnet på klassen är oviktig egentligen 
{                           // Namner förklarar dock vad vi har klassen till
    
    // datatypen efter this talar om vilken klass vi påverkar
    public static string UpperFirstLetter(this string name)
    {
        string newName = name[..1].ToUpper() + name[1..].ToLower();
        return newName;
    }


}
```

`whatever.cs`:

```csharp
namespace Extensions_01_Live
{
    //using System.Linq;
    internal class whatever
    {

        public void something()
        {

            string name = "";
            // string får massvis med förvirrande extensions

        }

    }
}
```

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
