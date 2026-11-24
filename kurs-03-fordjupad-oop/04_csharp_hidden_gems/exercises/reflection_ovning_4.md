# Ninja: en fabrik som kan vilken klass som helst

**Frivillig.** Det här är ninjanivån. Den ingår inte i bedömningen och är inte tentastoff. Läs lösningen långsamt, rad för rad. Förklaringarna är med för att du ska förstå *varför*, inte bara vad.

**Nivå:** 🥷 Ninja

**Tid:** ca 45 minuter, mest läsning och experiment.

## Berättelsen

Anders har nu sex klasser i sin kioskapp: `Customer`, `Product`, `Order` och fler. Alla behöver standardvärden när något saknas. Han tröttnar på att skriva en fabriksmetod per klass. Sara frågar: "Kan inte en enda metod göra det åt alla?"

Det kan den, med reflection. Men Sara har också sett vad det kostar, så hon vill ha en version som är rimligt snabb och inte kraschar på konstiga properties.

## Uppgift

Skriv en klass `DefaultFactory` med metoden `Create<T>()` som:

1. Skapar ett nytt objekt av typen `T`.
2. Fyller i standardvärden: `string` blir `"okänd"`, `int` och `int?` blir `0`, `DateTime` blir 1 januari 2000.
3. **Hoppar över** properties som saknar `set` (de går inte att skriva till).
4. **Sparar** (cachar) vilka properties en typ har, så att reflection bara slår upp dem en gång per typ.

Testa med en `Customer` som har `Name`, `Phone`, `Age`, `Created` och en `Id` som bara har `get`.

<details markdown="1">
<summary>Tips</summary>

- `property.CanWrite` är `true` om propertyn har `set`.
- En `Dictionary<Type, PropertyInfo[]>` kan spara properties per typ. `TryGetValue` frågar "finns den redan?" utan att krascha.
- Starta med en version utan cache. Lägg till cachen sist.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
using System.Diagnostics;
using System.Reflection;

// Satserna ligger först i filen. Typerna kommer efteråt.
Customer customer = DefaultFactory.Create<Customer>();
Console.WriteLine($"{customer.Name}, {customer.Phone}, {customer.Age}, {customer.Created:yyyy-MM-dd}");
// okänd, okänd, 0, 2000-01-01

var stopwatch = Stopwatch.StartNew();
for (int i = 0; i < 100_000; i++)
    DefaultFactory.Create<Customer>();
Console.WriteLine($"100 000 objekt: {stopwatch.ElapsedMilliseconds} ms");

class DefaultFactory
{
    // Cachen: typen är nyckeln, dess skrivbara properties är värdet.
    // static betyder att den delas av alla anrop, så den överlever mellan anropen.
    private static readonly Dictionary<Type, PropertyInfo[]> cache = new();

    // where T : new() lovar att T har en tom konstruktor.
    // Då kan vi skriva new T() i stället för Activator.CreateInstance.
    public static T Create<T>() where T : new()
    {
        T obj = new T();

        foreach (PropertyInfo property in GetWritableProperties(typeof(T)))
        {
            object? value = GetDefault(property.PropertyType);

            // null betyder att vi inte kan typen. Då lämnar vi propertyn ifred.
            if (value != null)
                property.SetValue(obj, value);
        }
        return obj;
    }

    private static PropertyInfo[] GetWritableProperties(Type type)
    {
        // Frågar cachen först. Finns typen redan slipper vi reflection helt.
        if (!cache.TryGetValue(type, out PropertyInfo[]? properties))
        {
            // Första gången: slå upp, filtrera bort det som saknar set, spara.
            properties = type.GetProperties().Where(p => p.CanWrite).ToArray();
            cache[type] = properties;
        }
        return properties;
    }

    private static object? GetDefault(Type type)
    {
        if (type == typeof(string)) return "okänd";
        if (type == typeof(int) || type == typeof(int?)) return 0;
        if (type == typeof(DateTime)) return new DateTime(2000, 1, 1);
        return null;   // okänd typ
    }
}

class Customer
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int? Age { get; set; }
    public DateTime Created { get; set; }
    public string Id { get; } = "A-1";   // bara get: hoppas över, annars kraschar SetValue
}
```

**Vad gör egentligen varje del?**

- **`where T : new()`** är ett villkor på `T`. Det betyder "bara typer som går att skapa med `new T()`". Kompilatorn stoppar dig om du försöker med en klass som saknar tom konstruktor. Det är ett bra exempel på att kompilatorn hjälper dig, något reflection annars inte gör.
- **`property.CanWrite`** behövs. Hade vi försökt `SetValue` på `Id` (som bara har `get`) hade programmet kraschat med ett undantag.
- **Cachen** är den viktiga insikten. Att slå upp properties med reflection är det dyra. Gör vi det en gång per typ i stället för en gång per objekt sparar vi mest tid.
- **`GetDefault`** är medvetet enkel. Varje ny typ du vill stödja är en rad till.

**Prova själv, så ser du det:**

1. Ta bort cachen och kör mätningen igen. Hur mycket långsammare blir det?
2. Byt `new T()` mot `Activator.CreateInstance<T>()`. Märker du skillnad?
3. Lägg till en `bool` och bestäm själv ett standardvärde.
4. Jämför med en vanlig `new Customer { Name = "okänd", ... }` i samma loop. Det är den snabbaste varianten, och det är poängen: reflection är ett verktyg för när du *inte* vet typen i förväg.

**Proffsens tips (Marcus):** den här sortens kod hittar du i ramverk, till exempel när ett bibliotek fyller dina objekt med data från en databas eller från JSON. Du skriver den sällan själv, men nu vet du vad som händer under huven.

</details>
