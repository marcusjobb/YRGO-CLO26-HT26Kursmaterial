# Records: samma värden betyder samma sak

Två kunder som heter Pelle och är 25 år, är det samma kund? För en vanlig klass säger C# nej, och varje objekt får sin egen hashkod automatiskt, även när värdena är exakt lika. En record får samma hashkod när värdena är lika. I det här exemplet jämför vi hashkoderna för en vanlig klass och en record, och ser hur `with` ger oss en ändrad kopia.

## Genomgång från lektionen

Först en vanlig klass, `Person`, med två properties:

```csharp
class Person                          // en vanlig klass
{
    public string Name { get; set; }  // går att ändra när som helst
    public string City { get; set; }

}
```

Vi skapar två personer med exakt samma värden och jämför dem:

```csharp
var kalle = new Person() { Name = "Kalle", City = "Göteborg" };   // objekt 1 i minnet
var kalle2 = new Person() { Name = "Kalle", City = "Göteborg" };  // objekt 2, med exakt samma värden

Console.WriteLine(kalle == kalle2);          // False: det är två olika objekt
Console.WriteLine(kalle.GetHashCode());      // ett tal, räknas ut ur objektets plats i minnet
Console.WriteLine(kalle2.GetHashCode());     // ett annat tal, trots samma värden
```

Utskriften börjar med `False`, och sedan två olika hashkoder. Lägg märke till att `kalle` och `kalle2` har samma värden, men ändå får de varsin hashkod. C# räknar ut den ur objektets plats i minnet och inte ur värdena, och jämför på samma sätt om det är samma objekt. Det är det inte, så svaret blir `False`.

Nu samma sak med en record. Hela deklarationen är en rad:

```csharp
public record Customer(string Name, int Age);   // en record: Name och Age är med i jämförelsen och hashkoden
```

Vi skapar två kunder med samma värden:

```csharp
var pelle = new Customer(Name: "Pelle", Age: 25);    // objekt 1
var pelle2 = new Customer(Name: "Pelle", Age: 25);   // objekt 2, samma värden

Console.WriteLine(pelle==pelle2);            // True: en record jämför värdena
Console.WriteLine(pelle.GetHashCode());      // ett tal, räknas ut ur "Pelle" och 25
Console.WriteLine(pelle2.GetHashCode());     // samma tal, eftersom värdena är lika
```

Nu blir det `True`, och de två hashkoderna är lika. En record räknar ut hashkoden ur värdena (`Name` och `Age`) och inte ur objektets plats i minnet. Det är skillnaden som exemplet ville visa.

Propertyerna i en record som `Customer(string Name, int Age)` går bara att sätta när objektet skapas. Raden med `pelle.Age++` är därför bortkommenterad, den skulle inte kompilera. Den vanliga klassen `Person` går däremot att ändra, så `kalle.City = "Mölndal"` fungerar. Vill vi ha en ändrad version av en record gör vi en kopia med `with`:

```csharp
kalle.City = "Mölndal";     // går bra, en vanlig klass går att ändra
//pelle.Age++;              // går inte: en record går inte att ändra efteråt

var newPelle = pelle with {Age=45};           // en ny kopia av pelle, med bara Age ändrad. pelle är kvar som förut
Console.WriteLine(newPelle.GetHashCode());    // ett annat tal än pelle, eftersom Age är en annan
```

`newPelle` är en ny kund med samma namn men ny ålder. `pelle` är kvar som den var. Eftersom värdena skiljer sig får `newPelle` en annan hashkod.

## Tillägg efter lektionen: en egen GetHashCode

Det här hann vi inte riktigt med i lektionen, så det är skrivet i efterhand och ingår inte i koden ovan.

En record gör jämförelsen åt oss. Vill vi att en vanlig klass ska bete sig likadant får vi göra jobbet själva. Det är tre saker som hör ihop:

1. `Equals` säger när två objekt räknas som lika.
2. `GetHashCode` räknar ut en siffra av samma properties. Två objekt som är lika måste ge samma siffra.
3. `==` och `!=` måste vi också skriva själva. Att skriva över `Equals` ändrar inte vad `==` gör, och det är lätt att tro att det gör det.

```csharp
class Person
{
    public string Name { get; set; } = "";
    public string City { get; set; } = "";

    public override bool Equals(object? obj)
    {
        return obj is Person other && Name == other.Name && City == other.City;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, City);
    }

    public static bool operator ==(Person? a, Person? b) => Equals(a, b);
    public static bool operator !=(Person? a, Person? b) => !Equals(a, b);
}
```

`HashCode.Combine` tar de properties som ska räknas med och blandar ihop dem till en siffra. Vi behöver inte hitta på någon egen formel. Ta med exakt de properties som `Equals` jämför, varken fler eller färre.

Med den klassen blir samma test som förut:

```csharp
var kalle = new Person() { Name = "Kalle", City = "Göteborg" };
var kalle2 = new Person() { Name = "Kalle", City = "Göteborg" };

Console.WriteLine(kalle == kalle2);                                  // True
Console.WriteLine(kalle.GetHashCode() == kalle2.GetHashCode());      // True
```

Om vi sedan ändrar `kalle.City = "Mölndal"` blir både jämförelsen och hashkoden olika. Det är också en varning: ett objekt vars hashkod ändras medan det ligger i en `Dictionary` eller `HashSet` går inte att hitta igen. Det är en anledning till att records är skrivskyddade från början.

Vi kan alltså göra en klass värdebaserad själva, men det är fyra metoder att hålla ihop. Med en record får vi allt det utan att skriva det.

## Varför har vi nytta av detta?

Vi slipper skriva `Equals`, `GetHashCode` och `==` för klasser som bara bär data. Och vi får ändrade kopior med `with` i stället för att ändra på objekt som andra delar av programmet redan använder.

## När kan vi använda detta?

För värden som beskriver något utan att ha en egen identitet: en adress, en kund i ett svar från ett API, ett resultat. Två adresser med samma gata, stad och postnummer är samma adress. För sådant som har en identitet över tid, som en EF-entitet, använder vi fortfarande en vanlig klass.

## Hela koden

```csharp
Console.WriteLine("Hello, World!");

var kalle = new Person() { Name = "Kalle", City = "Göteborg" };
var kalle2 = new Person() { Name = "Kalle", City = "Göteborg" };

Console.WriteLine(kalle == kalle2);
Console.WriteLine(kalle.GetHashCode());
Console.WriteLine(kalle2.GetHashCode());

var pelle = new Customer(Name: "Pelle", Age: 25);
var pelle2 = new Customer(Name: "Pelle", Age: 25);

Console.WriteLine(pelle==pelle2);
Console.WriteLine(pelle.GetHashCode());
Console.WriteLine(pelle2.GetHashCode());

kalle.City = "Mölndal";
//pelle.Age++;

var newPelle = pelle with {Age=45};
Console.WriteLine(newPelle.GetHashCode());

public record Customer(string Name, int Age);

class Person
{
    public string Name { get; set; }
    public string City { get; set; }

}
```
