# Reflection

Det här är dagens sista ämne och en demo. Titta, förstå vad som händer. Du ska inte kunna skriva det utantill. Reflection ingår inte i tentan, hidden gems är kursens buffert. Nu vet du vad det är när du ser `GetType()` i andras kod.

## Anders loggar fel

När något går fel vill Anders skriva ut allt som står i ett objekt. Vilket objekt som helst. Han vill inte skriva en `ToString()` för varje klass. Går det att be ett objekt berätta vad det består av?

Det går. **Reflection** betyder att programmet kan titta på sin egen ritning medan det körs: vilka properties finns, vad heter de, vad är de värda. Tänk på en genomskinlig ritning av huset som du kan läsa medan du bor i det.

## Klassen vi använder

```csharp
class Customer
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int? Age { get; set; }
}
```

`?` betyder att värdena får vara `null`. Det är kunden i Anders app.

## Fråga typen vad den innehåller

```csharp
Customer ada = new Customer { Name = "Ada", Phone = null };

foreach (var p in ada.GetType().GetProperties())
{
    Console.WriteLine($"{p.Name} = {p.GetValue(ada)}");
}
```

`GetType()` ger ritningen. `GetProperties()` listar fälten. `GetValue` läser dem.

## Exempel 1: skapa en klass

```csharp
Type type = typeof(Customer);
object? obj = Activator.CreateInstance(type);   // new Customer(), via typen

if (obj != null)
{
    type.GetProperty("Name")?.SetValue(obj, "Ada");   // obj.Name = "Ada", via namnet
    Console.WriteLine(type.GetProperty("Name")?.GetValue(obj));   // Ada
}
```

Programmet skapar objektet och sätter värdet utan att veta klassens namn i förväg. Och `if (obj != null)` är kladdkakan igen.

## Tanke: reflection som fabrik

En fabrik skapar objekt åt dig. Med reflection kan en enda metod skapa vilken klass som helst och ge den standardvärden:

```csharp
static object? CreateWithDefaults(Type type)
{
    object? obj = Activator.CreateInstance(type);
    if (obj == null) return null;

    foreach (var property in type.GetProperties())
    {
        if (property.PropertyType == typeof(string))
            property.SetValue(obj, "okänd");   // alla string-properties
    }
    return obj;
}
```

Cool tanke, men den kostar. Jämför med en vanlig fabrik:

```csharp
static Customer CreateCustomer()
{
    return new Customer { Name = "okänd", Phone = "okänd" };
}
```

- **Snabbare.** Kompilatorn vet exakt vad som händer. Reflection tappar bitcyklar på att slå upp typen och egenskaperna varje gång.
- **Säkrare.** Skriver du fel namn, `Nmae`, får du ett kompileringsfel med `new`. Med reflection ger `GetProperty("Nmae")` bara `null`. Ingen krasch, men ingenting händer heller.

Använd reflection när du måste (typen är okänd i förväg), inte när du kan skriva `new`.

## Exempel 2: standardvärden

```csharp
public static void SetDefaults(this object obj)
{
    foreach (var property in obj.GetType().GetProperties())
    {
        if (property.GetValue(obj) != null)
            continue;                          // redan ifyllt, rör inte

        if (property.PropertyType == typeof(string))
            property.SetValue(obj, "okänd");   // text som saknas blir "okänd"
        else if (property.PropertyType == typeof(int?))
            property.SetValue(obj, 0);         // tal som saknas blir 0
    }
}
```

Metoden går igenom alla properties och fyller bara i de som saknar värde.

## Exempel 2: prova det

```csharp
var customer = new Customer { Name = "Ada" };
customer.SetDefaults();

Console.WriteLine($"{customer.Name}, {customer.Phone}, {customer.Age}");
// Ada, okänd, 0
```

Namnet var redan ifyllt, så det rörs inte. `Phone` var `null` och blev `"okänd"`. `Age` var `null` och blev `0`. Metoden väljer själv efter typ, och den fungerar på vilken klass som helst.

## En extension method på object

```csharp
public static string ToDebugString(this object obj)
{
    // Tänk på att lägga till Try Catch
    var parts = obj.GetType().GetProperties()
        .Select(p => $"{p.Name} = {p.GetValue(obj) ?? "saknas"}");
    return string.Join(", ", parts);
}

Console.WriteLine(ada.ToDebugString());   // Name = Ada, Phone = saknas, Age = saknas
```

Samma metod fungerar på **alla** klasser då vi kopplar den till `object` som är moderklassen till allt. Extension methods, `??` och reflection i en och samma rad. Det här är Anders loggverktyg.

## Vad kostar det?

- Reflection är långsammare än vanlig kod.
- Den går förbi kompilatorns kontroller: skriver du fel namn får du felet först när programmet körs.
- Använd den sparsamt. Många ramverk gör det åt dig, till exempel EF Core och JSON-biblioteken.

## Proffsens tips: låt ramverken göra det

Du behöver sällan skriva reflection själv. Det är motorn under EF Core (Entity Framework Core) och JSON (JavaScript Object Notation) -biblioteken, och det är därför de kan läsa vilken klass som helst. Skriv egen reflection bara när du verkligen saknar ett annat sätt, till exempel ett litet felsökningsverktyg som `ToDebugString`. Och om du hittar den i andras kod: nu vet du vad `GetType()` gör.

## Summan av kardemumman

- Reflection låter ett program undersöka typer medan det körs.
- Du kan skapa objekt och sätta värden utan att känna till klassen i förväg.
- Med en extension method blir det en metod du kan använda överallt.
- Inte tentastoff. Nu vet du vad det är när du ser `GetType()` i andras kod.
