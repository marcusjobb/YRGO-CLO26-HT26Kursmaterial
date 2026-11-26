# Null-operatorerna: från nästlade if till en rad

Ett värde som kan vara `null` är en av de vanligaste orsakerna till att program kraschar. I det här exemplet har vi en kund som kanske har en adress, som kanske har en stad. Vi ser hur `?.` och `??` kortar ner kontrollerna till en enda rad.

## Genomgång från lektionen

Först klasserna. `Address` har en stad, och `City` får vara `null` (därför `string?`):

```csharp
namespace Nullcheck_01_Live
{
    class Address
    {
        public string? City { get; set; }
    }

}
```

`Customer` har ett namn och en adress. Båda får vara `null`:

```csharp
namespace Nullcheck_01_Live
{
    class Customer
    {
        public string? Name { get; set; }
        public Address? Address { get; set; }
    }

}
```

Nu skapar vi en kund utan adress. Raden med `Rävemåla` är bortkommenterad, så `Address` är `null`:

```csharp
Customer c = new();

//c.Address = new() { City = "Rävemåla" };

Console.WriteLine("City:" + GetCity(c));
Console.WriteLine("City:" + GetCity(null));
```

Vi anropar `GetCity` två gånger: en gång med en kund utan adress, och en gång med `null` i stället för kund.

Det gamla sättet är tre `if` i varandra. Varje nivå kan vara `null`, så vi måste kolla alla:

```csharp
string GetCity(Customer? customer)
{
    /*
    if (customer != null)
        if (customer.Address != null)
            if (customer.Address.City != null)
                return customer.Address.City;
    
    return "okänd";
    */
```

Nu samma sak i en rad:

```csharp
    return customer?.Address?.City ?? "Okänd";
}
```

Lägg märke till att `?.` stoppar så fort något är `null` och ger `null` tillbaka i stället för att krascha. `??` säger sedan "om det blev `null`, ta det här i stället". Båda anropen skriver ut `City:Okänd`.

## Varför har vi nytta av detta?

Tre nästlade `if` blir en rad som går att läsa från vänster till höger. Vi slipper också glömma en nivå och få en `NullReferenceException`.

## När kan vi använda detta?

Överallt där ett värde kan saknas: en kund utan adress, ett svar från ett API som saknar ett fält, en rad i databasen med tomma kolumner. `??` ger oss ett rimligt standardvärde att visa i stället.

## Hela koden

`Address.cs`:

```csharp
using System;
using System.Collections.Generic;
using System.Text;

namespace Nullcheck_01_Live
{
    class Address
    {
        public string? City { get; set; }
    }

}
```

`Customer.cs`:

```csharp
namespace Nullcheck_01_Live
{
    class Customer
    {
        public string? Name { get; set; }
        public Address? Address { get; set; }
    }

}
```

`Program.cs`:

```csharp
using System.Runtime.CompilerServices;
using Nullcheck_01_Live;

Console.WriteLine("Hello, Null World!");

Customer c = new();

//c.Address = new() { City = "Rävemåla" };

Console.WriteLine("City:" + GetCity(c));
Console.WriteLine("City:" + GetCity(null));


string GetCity(Customer? customer)
{
    /*
    if (customer != null)
        if (customer.Address != null)
            if (customer.Address.City != null)
                return customer.Address.City;
    
    return "okänd";
    */

    return customer?.Address?.City ?? "Okänd";
}
```

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
