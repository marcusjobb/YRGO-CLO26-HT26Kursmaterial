# Repetition — Lista: spara och ladda

Kombinationsövning: lista + JSON + fil. Samma steg som alltid, nu med en lista.

---

🟢 Skapa en lista med namn. Serialisera och spara till fil. Läs tillbaka och skriv ut varje namn.

```csharp
using System.Text.Json;

var names = new List<string> { "Mira", "Tobbe", "Zelda" };

// Steg 1: Serialisera listan till JSON
// string json = JsonSerializer.Serialize(...)

// Steg 2: Spara till "names.json"
// File.WriteAllText(...)

// Steg 3: Läs tillbaka från filen
// string text = File.ReadAllText(...)

// Steg 4: Deserialisera till List<string>
// List<string> loaded = JsonSerializer.Deserialize<List<string>>(text)!

// Steg 5: Loopa och skriv ut
```

**Förväntad output:**
```
Mira
Tobbe
Zelda
```

---

🟢 Gör samma sak med en lista av objekt. Spara tre `Book`-objekt till fil och läs tillbaka.

```csharp
using System.Text.Json;

class Book
{
    public string Title { get; set; } = "";
    public int Pages { get; set; }
}

var books = new List<Book>
{
    new Book { Title = "The Hobbit", Pages = 310 },
    new Book { Title = "1984",       Pages = 328 },
    new Book { Title = "Dune",       Pages = 412 }
};

// Serialisera med WriteIndented = true
// Spara till "books.json"
// Läs tillbaka
// Deserialisera till List<Book>
// Skriv ut varje boks titel och sidantal
```

**Förväntad output:**
```
The Hobbit (310 sidor)
1984 (328 sidor)
Dune (412 sidor)
```

---

🟡 Lägg till en bok, spara om filen och skriv ut den uppdaterade listan.

```csharp
// Ladda listan från "books.json"
// Lägg till: new Book { Title = "Brave New World", Pages = 311 }
// Spara om filen
// Ladda igen och skriv ut alla böcker
```

**Förväntad output:**
```
The Hobbit (310 sidor)
1984 (328 sidor)
Dune (412 sidor)
Brave New World (311 sidor)
```

---

🟡 Ta bort en bok ur listan och spara om. Skriv ut hur många böcker som finns kvar.

```csharp
// Ladda listan från "books.json"
// Ta bort boken med Title == "1984"
//   Tips: books.RemoveAll(b => b.Title == "1984")
// Spara om filen
// Ladda igen och skriv ut: "X böcker kvar."
```

**Förväntad output:**
```
3 böcker kvar.
```
