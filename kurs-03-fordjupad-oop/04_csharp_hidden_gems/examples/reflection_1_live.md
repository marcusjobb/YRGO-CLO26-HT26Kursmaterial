# Reflection: läsa och sätta properties utan att veta klassens namn i förväg

Vanligtvis skriver vi `kund.Name` och kompilatorn vet exakt vilken klass vi menar. Med reflection frågar vi i stället programmet vid körning: vilka properties har den här typen, och vad är värdet? Vi kan till och med skapa objekt och sätta värden bara med hjälp av namn i text. Det är så bibliotek som JSON-läsare och databaskopplingar kan arbeta med klasser de aldrig har sett.

## Genomgång från lektionen

Vi börjar med en vanlig klass, `Customer`, i en egen fil. Tre properties, alla får vara tomma:

```csharp
class Customer
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int? Age { get; set; }
}
```

Vi skapar en kund, Ada, med namn men utan telefonnummer:

```csharp
using System.Diagnostics;

Customer ada = new Customer { Name = "Ada", Phone = null };
```

Nu frågar vi objektet vilka properties det har. `GetType().GetProperties()` ger oss en lista, och för varje property kan vi läsa namn, typ och värde:

```csharp
foreach (var p in ada.GetType().GetProperties())
{
    Console.WriteLine($"{p.Name} {p.PropertyType}= {p.GetValue(ada)}");
}
```

Utskriften:

```
Name System.String= Ada
Phone System.String= 
Age System.Nullable`1[System.Int32]= 
```

Lägg märke till att vi aldrig skrev `ada.Name` eller `ada.Phone`. Vi gick igenom alla properties utan att känna till dem. `Phone` och `Age` är tomma (`null`), så ingenting skrivs efter likhetstecknet. Typen för `Age` ser lite konstig ut, `Nullable`1[System.Int32]`, men det är bara `int?` i sin fulla form.

Nu går vi åt andra hållet: vi skapar ett objekt utifrån en typ. `typeof(Customer)` ger typen, och `Activator.CreateInstance` gör samma sak som `new Customer()`:

```csharp
Type type = typeof(Customer);
object? obj = Activator.CreateInstance(type);   // new Customer(), via typen
```

Sedan sätter vi ett värde med namnet i text, och läser tillbaka det:

```csharp
if (obj != null)
{
    type.GetProperty("Name")?.SetValue(obj, "Ada");   // obj.Name = "Ada", via namnet
    Console.WriteLine(type.GetProperty("Name")?.GetValue(obj));   // Ada
}
```

Sista raden skriver ut `Ada`. Vi skapade objektet och satte `Name` utan att skriva `new Customer()` eller `.Name =` en enda gång. Namnet "Name" var bara en text.

## Hela koden

`Customer.cs`:

```csharp
class Customer
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int? Age { get; set; }
}
```

`Program.cs`:

```csharp
using System.Diagnostics;

Customer ada = new Customer { Name = "Ada", Phone = null };

foreach (var p in ada.GetType().GetProperties())
{
    Console.WriteLine($"{p.Name} {p.PropertyType}= {p.GetValue(ada)}");
}

// -----------------

Type type = typeof(Customer);
object? obj = Activator.CreateInstance(type);   // new Customer(), via typen

if (obj != null)
{
    type.GetProperty("Name")?.SetValue(obj, "Ada");   // obj.Name = "Ada", via namnet
    Console.WriteLine(type.GetProperty("Name")?.GetValue(obj));   // Ada
}
```
