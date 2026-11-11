# Repetition — List&lt;T&gt;

En lista kan växa och krympa. Du lägger till, tar bort och hämtar saker med index.

```csharp
var names = new List<string>();
//               index: 0       1        2
names.Add("Mira");
names.Add("Tobbe");
names.Add("Zelda");

Console.WriteLine(names[0]);       // Mira
Console.WriteLine(names.Count);    // 3
```

---

🟢 Skapa en tom `List<string>`. Lägg till tre namn. Skriv ut det andra namnet och hur många som finns.

```csharp
var names = new List<string>();
// Lägg till tre namn med .Add()
// Skriv ut names[1]
// Skriv ut names.Count
```

**Förväntad output:**
```
Tobbe
3
```

---

🟢 Loopa igenom hela listan och skriv ut varje namn med sitt nummer.

```csharp
// Använd for-loop med names.Count som gräns
// Skriv ut: "1. Mira", "2. Tobbe", "3. Zelda"
```

**Förväntad output:**
```
1. Mira
2. Tobbe
3. Zelda
```

---

🟢 Lägg till ett fjärde namn. Ta bort det andra. Skriv ut listan efteråt.

```csharp
names.Add("Anna");
names.Remove("Tobbe");   // tar bort första matchningen
// Skriv ut alla namn med foreach
```

**Förväntad output:**
```
Mira
Zelda
Anna
```

---

🟡 Skapa en `List<int>` med fem tal. Skriv ut summan, det minsta och det största.

```csharp
var numbers = new List<int> { 4, 17, 2, 9, 11 };
// Skriv ut numbers.Sum()
// Skriv ut numbers.Min()
// Skriv ut numbers.Max()
// (kräver: using System.Linq)
```

**Förväntad output:**
```
Summa: 43
Minst: 2
Störst: 17
```

---

🟡 Skapa en lista med namn. Filtrera ut dem som börjar på "M" och skriv ut träffarna.

```csharp
var names = new List<string> { "Mira", "Tobbe", "Max", "Zelda", "Maria" };
// Loopa med foreach
// if (name.StartsWith("M")) → skriv ut
```

**Förväntad output:**
```
Mira
Max
Maria
```
