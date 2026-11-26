# Reflection och extension methods: SetDefaults som fungerar på egna klasser

I förra exemplet var fabriken en vanlig metod. Nu gör vi den till en extension method, `SetDefaults`, som vi kan anropa direkt på ett objekt: `person.SetDefaults()`. Den fyller bara i det som saknas, och den vägrar arbeta på sådant den inte ska röra, till exempel en `string`.

## Genomgång från lektionen

Först två klasser att testa på. `Person` har tre properties: `Name` och `Age` saknar värde, men `Nickname` har redan fått "Kim" direkt i klassen:

```csharp
class Person
{
    public string? Name { get; set; }
    public string? Nickname { get; set; } = "Kim";   // redan ifyllt, rörs inte
    public int? Age { get; set; }
}
```

Och en `Lastbil` med en enda text, som inte får vara `null`:

```csharp
class Lastbil
{
    public string Modell { get; set; }
}
```

Så här ska det användas. Vi skapar en person, anropar `SetDefaults` och skriver ut:

```csharp
var person = new Person();
person.SetDefaults();

Console.WriteLine($"{person.Name}, {person.Age}, {person.Nickname}");
```

Samma sak med lastbilen:

```csharp
var ls = new Lastbil();
ls.SetDefaults();
Console.WriteLine(ls.Modell);
```

Utskriften blir `okänd, 1, Kim` för personen och `okänd` för lastbilen. `Nickname` är kvar som "Kim", eftersom den redan var ifylld. Och det som är värt att titta på en gång till: kommentaren i koden nedan säger att saknade tal blir 0, men koden sätter 1, och därför blir `Age` 1.

Nu extension methoden. Allra först ett skydd. Reflection på en `string` eller på en `int` ger ingenting vettigt, så metoden kastar ett fel i stället för att göra något konstigt:

```csharp
static class ObjectExtender
{
    public static void SetDefaults(this object obj)
    {
        Type type = obj.GetType();

        // Bara egna klasser. string, int, enum och liknande har inget att fylla i.
        if (type == typeof(string) || type.IsPrimitive || type.IsValueType)
            throw new ArgumentException($"SetDefaults fungerar bara för egna klasser, inte för {type.Name}.", nameof(obj));
```

Sedan går vi igenom propertyerna, men hoppar över det vi inte kan eller ska skriva till. Skrivskyddade properties går inte att sätta, och indexerare har parametrar, så `GetValue` utan parametrar skulle krascha:

```csharp
        foreach (var property in type.GetProperties())
        {
            if (!property.CanWrite || property.GetIndexParameters().Length > 0)
                continue;                              // skrivskyddad eller indexerare

            if (property.GetValue(obj) != null)
                continue;                              // redan ifyllt, rör inte
```

Det är de två raderna med `continue` som gör att `Nickname` får vara ifylld. Till sist själva fyllningen:

```csharp
            if (property.PropertyType == typeof(string))
                property.SetValue(obj, "okänd");       // text som saknas blir "okänd"
            else if (property.PropertyType == typeof(int?))
                property.SetValue(obj, 1);             // tal som saknas blir 0
        }
    }
}
```

Sist i koden finns ett anrop som är bortkommenterat. Tar vi bort kommentarsmarkeringen kastas `ArgumentException`, eftersom en `string` inte är en egen klass:

```csharp
// Det här kastar ArgumentException, eftersom en string inte är en egen klass:
// string something = "";
// something.SetDefaults();
```

Skyddet och de två `continue` är vad som gör en reflection-metod trygg att släppa lös på okända klasser. Reflection tittar på allt, så vi måste själva säga vad den inte ska röra.

## Hela koden

```csharp
Console.WriteLine("Hello, World!");

var person = new Person();
person.SetDefaults();

Console.WriteLine($"{person.Name}, {person.Age}, {person.Nickname}");

// Det här kastar ArgumentException, eftersom en string inte är en egen klass:
// string something = "";
// something.SetDefaults();

var ls = new Lastbil();
ls.SetDefaults();
Console.WriteLine(ls.Modell);



class Person
{
    public string? Name { get; set; }
    public string? Nickname { get; set; } = "Kim";   // redan ifyllt, rörs inte
    public int? Age { get; set; } 
}

class Lastbil
{
    public string Modell { get; set; }
}

static class ObjectExtender
{
    public static void SetDefaults(this object obj)
    {
        Type type = obj.GetType();

        // Bara egna klasser. string, int, enum och liknande har inget att fylla i.
        if (type == typeof(string) || type.IsPrimitive || type.IsValueType)
            throw new ArgumentException($"SetDefaults fungerar bara för egna klasser, inte för {type.Name}.", nameof(obj));

        foreach (var property in type.GetProperties())
        {
            if (!property.CanWrite || property.GetIndexParameters().Length > 0)
                continue;                              // skrivskyddad eller indexerare

            if (property.GetValue(obj) != null)
                continue;                              // redan ifyllt, rör inte

            if (property.PropertyType == typeof(string))
                property.SetValue(obj, "okänd");       // text som saknas blir "okänd"
            else if (property.PropertyType == typeof(int?))
                property.SetValue(obj, 1);             // tal som saknas blir 0
        }
    }
}
```

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
