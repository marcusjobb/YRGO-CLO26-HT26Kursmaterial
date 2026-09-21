# Strings — Recap

Det här projektet heter `recap_live` och består av två filer: `StringHelper.cs` med en hjälpmetod, och `Program.cs` som använder den.

## StringHelper.cs

En statisk hjälpklass med en metod som ersätter ett tecken på en viss position. `public static` betyder att vi kan anropa den utan att skapa ett objekt:

```cs
namespace recap_live;

public class StringHelper
{
    // public för att andra ska kunna använda det
    public static string MyReplace(string input, int position, string replacement)
    {
        string newString = input.Substring(0, position-1); // första delen av strängen
        newString += replacement;
        newString += input.Substring(position);
        return newString;
    }
}
```

## Program.cs

Olika datatyper — lägg märke till suffix som `f` på float och att `decimal` är mer exakt än `double`:

```cs
short number = 100; // mindre än int
int svaret = 42; // heltal
long storTal = 1337; // Dubbelt så stora tal som int

float decimaltal = 5.44f; // float är inte lika exakt som double
double trouble = 4.43; // decimaltal, double är inte lika exakt som decimal
decimal money = 500; // decimal är mer exakt och har fler decimaltal

string name = "            Jonathan Harker       "; // Samling av characters
char symbol = '*';
```

Strängmanipulering: `Trim` tar bort mellanslag i kanterna, `ToUpper`/`ToLower` ändrar skiftläge, `Contains` och `IndexOf` hittar tecken:

```cs
Console.WriteLine($"Hello, {name}!");
Console.WriteLine($"{name.Length}");
Console.WriteLine($"{name.TrimStart()}");
Console.WriteLine($"{name.TrimStart().Length}");
Console.WriteLine($"{name.TrimEnd()+"!"}");
Console.WriteLine($"{name.Trim().Length}");

name = name.Trim(); // tvingar bort alla tomma mellanslag före och efter
Console.WriteLine(($"{name.ToUpper()}")); // CAPS
Console.WriteLine(($"{name.ToLower()}")); // små bokstäver

Console.WriteLine($"{name.Contains('k')}");
Console.WriteLine($"{name.IndexOf('k')}");
```

`Substring` för att ersätta ett enskilt tecken — strängar är oföränderliga i C# så vi bygger en ny:

```cs
// name[12] = 'x';
name = name.Substring(0,11)+'x'+ name.Substring(12);
Console.WriteLine($"{name}");
Console.WriteLine($"{name.Substring(3,4)}"); // Börja från bokstav a och läs 4 tecken
```

Och med vår egna `StringHelper.MyReplace`:

```cs
name = StringHelper.MyReplace(name, 13, "k");

Console.WriteLine($"{name}");
Console.WriteLine($"{name.Replace('a','e')}");
```

En kommentar om hur man tänker på klasser — bra att ha i bakhuvudet:

```cs
//-----------------
// Klasser och benämning
// program.cs <--- startpunkt static main()
// Helper klasser som har metoder andra använder, men annars ingen intelligens
// DTO = har information men annars ingen specifik intelligens som (person: namn, efternamn, telefonnummer)
// Datatyp = klass med funktionalitet för en viss sorts data Kilo kg=new Kilo(); 
// Modell = klass som ska likna något som finns i verkligheten, Car volvo=new Car();
```

## Hela koden — StringHelper.cs

```cs
namespace recap_live;

public class StringHelper
{
    // public för att andra ska kunna använda det
    public static string MyReplace(string input, int position, string replacement)
    {
        string newString = input.Substring(0, position-1); // första delen av strängen
        newString += replacement;
        newString += input.Substring(position);
        return newString;
    }
}
```

## Hela koden — Program.cs

```cs
using recap_live;

Console.WriteLine("Hello, Recap!");

// variabler

short number = 100; // mindre än int
int svaret = 42; // heltal
long storTal = 1337; // Dubbelt så stora tal som int

float decimaltal = 5.44f; // float är inte lika exakt som double
double trouble = 4.43; // decimaltal, double är inte lika exakt som decimal
decimal money = 500; // decimal är mer exakt och har fler decimaltal

string name = "            Jonathan Harker       "; // Samling av characters
char symbol = '*';

// ---------------------------------------------

Console.WriteLine($"Hello, {name}!");
Console.WriteLine($"{name.Length}");
Console.WriteLine($"{name.TrimStart()}");
Console.WriteLine($"{name.TrimStart().Length}");
Console.WriteLine($"{name.TrimEnd()+"!"}");
Console.WriteLine($"{name.Trim().Length}");

name = name.Trim(); // tvingar bort alla tomma mellanslag före och efter
Console.WriteLine(($"{name.ToUpper()}")); // CAPS
Console.WriteLine(($"{name.ToLower()}")); // små bokstäver

Console.WriteLine($"{name.Contains('k')}");
Console.WriteLine($"{name.IndexOf('k')}");

// name[12] = 'x';
name = name.Substring(0,11)+'x'+ name.Substring(12);
Console.WriteLine($"{name}");
Console.WriteLine($"{name.Substring(3,4)}"); // Börja från bokstav a och läs 4 tecken

name = StringHelper.MyReplace(name, 13, "k");

Console.WriteLine($"{name}");
Console.WriteLine($"{name.Replace('a','e')}");

//-----------------
// Klasser och benämning
// program.cs <--- startpunkt static main()
// Helper klasser som har metoder andra använder, men annars ingen intelligens
// DTO = har information men annars ingen specifik intelligens som (person: namn, efternamn, telefonnummer)
// Datatyp = klass med funktionalitet för en viss sorts data Kilo kg=new Kilo(); 
// Modell = klass som ska likna något som finns i verkligheten, Car volvo=new Car();
```
