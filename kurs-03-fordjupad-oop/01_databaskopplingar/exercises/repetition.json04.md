# Repetition — JSON → Lista

🟢 Du har en JSON-array som sträng. Deserialisera den till en `List<Book>`.
Skriv ut hur många böcker det finns, och skriv sedan ut varje boks titel.

```csharp
using System.Text.Json;

class Book
{
    public string Title { get; set; } = "";
    public int Pages { get; set; }
}

string json = """
[
  {"Title":"The Hobbit","Pages":310},
  {"Title":"1984","Pages":328},
  {"Title":"Dune","Pages":412}
]
""";

// Deserialisera json till List<Book>
// Skriv ut antalet böcker
// Loopa och skriv ut varje titel
```

**Förväntad output:**
```
3 böcker hittades:
- The Hobbit
- 1984
- Dune
```
