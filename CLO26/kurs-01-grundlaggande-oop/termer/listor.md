# Programmeringstermer — Listor

## List\<T\>

`List<T>` är en dynamisk samling — till skillnad från en array kan den växa och krympa medan programmet kör. `T` är en platshållare för typen av element listan ska hålla.

```csharp
List<string> names = new List<string>();
List<int> scores = new List<int>();
```

Du behöver inte bestämma storleken i förväg. Lägg till element med `Add()` och listan växer automatiskt.

```csharp
List<string> names = new List<string>();
names.Add("Anna");
names.Add("Björn");
names.Add("Camilla");

Console.WriteLine(names.Count);   // 3
```

### Output
```plaintext
3
```

`<T>` kallas generics — du berättar för kompilatorn vilken typ listan ska innehålla, och den ser till att du inte råkar lägga in fel typ av värden. Det här är en introduktion; generics som koncept går djupare längre fram i kursen.

**Se även:** [Array](arrayer.md), [Add Remove Count Contains Clear](#add-remove-count-contains-clear).

---

## Add, Remove, Count, Contains, Clear

De vanligaste metoderna och properties du använder med en lista:

```csharp
List<string> fruits = new List<string>();

// Lägg till element
fruits.Add("Äpple");
fruits.Add("Banan");
fruits.Add("Citron");

// Antal element
Console.WriteLine(fruits.Count);               // 3

// Kolla om ett värde finns
Console.WriteLine(fruits.Contains("Banan"));   // True
Console.WriteLine(fruits.Contains("Druva"));   // False

// Ta bort ett element
fruits.Remove("Banan");
Console.WriteLine(fruits.Count);               // 2

// Töm hela listan
fruits.Clear();
Console.WriteLine(fruits.Count);               // 0
```

### Output
```plaintext
3
True
False
2
0
```

<details><summary>Mer om Remove</summary>

`Remove()` tar bort den **första** förekomsten av det värde du anger. Om värdet inte finns i listan händer ingenting — inget fel kastas. Vill du ta bort ett element på ett visst index använder du `RemoveAt(int index)` istället:

```csharp
List<string> fruits = new List<string> { "Äpple", "Banan", "Citron" };
fruits.RemoveAt(1);   // tar bort "Banan" (index 1)
```

</details>

---

## foreach med listor

`foreach` fungerar med listor på exakt samma sätt som med arrayer:

```csharp
List<string> fruits = new List<string> { "Äpple", "Banan", "Citron" };

foreach (string fruit in fruits)
{
    Console.WriteLine(fruit);
}
```

### Output
```plaintext
Äpple
Banan
Citron
```

Du kan också använda en `for`-loop med index om du behöver det:

```csharp
for (int i = 0; i < fruits.Count; i++)
{
    Console.WriteLine((i + 1) + ". " + fruits[i]);
}
```

Observera att listor använder `Count`, inte `Length` som arrayer gör.

---

## När ska jag använda List vs Array?

| | Array | List\<T\> |
|---|---|---|
| Storlek | Fast — bestäms vid skapandet | Dynamisk — växer och krymper |
| Syntax för att skapa | `string[] arr = { "a", "b" }` | `List<string> list = new List<string>()` |
| Antal element | `arr.Length` | `list.Count` |
| Lägga till | Inte möjligt (fast storlek) | `list.Add(...)` |
| Ta bort | Inte möjligt | `list.Remove(...)` |
| Bra när... | Du vet exakt hur många element och de aldrig ändras | Antalet element varierar under körning |

I praktiken är `List<T>` det vanligaste valet när du inte vet i förväg hur många element du kommer att ha. Arrayer används när storleken är känd och fast — till exempel en bräda i ett spel med alltid 8x8 rutor.

<details><summary>Initiera en lista med värden direkt</summary>

Du kan initiera en lista med värden i samma uttryck, precis som en array:

```csharp
List<string> fruits = new List<string> { "Äpple", "Banan", "Citron" };
```

Det är en samlingsinitierare — kompilatorn anropar `Add()` för varje värde åt dig. Listan är fortfarande dynamisk efteråt, trots att du startade med kända värden.

</details>

**Se även:** [Array](arrayer.md), [Dictionary](dictionary.md).
