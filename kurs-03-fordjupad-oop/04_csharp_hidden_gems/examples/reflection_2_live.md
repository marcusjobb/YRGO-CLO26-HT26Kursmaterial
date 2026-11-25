# Reflection: en fabrik som fyller i standardvärden åt vilken klass som helst

Nu använder vi det vi lärde oss i första exemplet till något nyttigt. En metod som får en typ, skapar ett objekt av den och fyller i alla text-properties med "okänd" och alla heltal med -1. Samma metod fungerar för `Demo` idag och för vilken annan klass som helst i morgon.

## Genomgång från lektionen

Själva metoden först. Den tar en `Type`, skapar objektet med `Activator.CreateInstance` och returnerar `null` om det inte gick:

```csharp
static object? CreateWithDefaults(Type type)
{
    object? obj = Activator.CreateInstance(type);
    if (obj == null) return null;
```

Sedan går den igenom alla properties i typen. Är en property en `string` sätts den till "okänd", och är den en `int` sätts den till -1:

```csharp
    foreach (var property in type.GetProperties())
    {
        if (property.PropertyType == typeof(string))
            property.SetValue(obj, "okänd");   // alla string-properties

        if (property.PropertyType == typeof(int))
            property.SetValue(obj, -1);   // alla string-properties
    }
    return obj;
}
```

Lägg märke till att kommentaren på `int`-raden är kopierad från raden ovanför och säger "string-properties". Koden är rätt, bara kommentaren är fel.

Klassen vi testar på har en text och två heltal:

```csharp
class Demo
{
    public string? Info { get; set; }
    public int Number { get; set; }
    public int Digit { get; set; }
}
```

Och så anropar vi metoden med `typeof(Demo)`. Den returnerar ett `object?`, så vi gör om det till `Demo` med `as`, och skriver ut de tre värdena:

```csharp
Demo obj = CreateWithDefaults(typeof(Demo)) as Demo;

Console.WriteLine(
    
    obj.Info + "\n"+obj.Number + "\n" +obj.Digit

    );
```

Utskriften:

```
okänd
-1
-1
```

Ingen av properties sattes i koden vi skrev för `Demo`. Fabriken tittade på typerna och fyllde i dem själv. Lägg till en fjärde property i `Demo` och den får sitt värde utan att fabriken ändras.

## Hela koden

```csharp
Console.WriteLine("Hello, Reflection!");

Demo obj = CreateWithDefaults(typeof(Demo)) as Demo;

Console.WriteLine(
    
    obj.Info + "\n"+obj.Number + "\n" +obj.Digit

    );

static object? CreateWithDefaults(Type type)
{
    object? obj = Activator.CreateInstance(type);
    if (obj == null) return null;

    foreach (var property in type.GetProperties())
    {
        if (property.PropertyType == typeof(string))
            property.SetValue(obj, "okänd");   // alla string-properties

        if (property.PropertyType == typeof(int))
            property.SetValue(obj, -1);   // alla string-properties
    }
    return obj;
}

class Demo
{
    public string? Info { get; set; }
    public int Number { get; set; }
    public int Digit { get; set; }
}
```
