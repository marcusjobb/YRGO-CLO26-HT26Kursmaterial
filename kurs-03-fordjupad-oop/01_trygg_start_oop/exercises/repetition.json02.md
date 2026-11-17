# Repetition — JSON → Objekt

🟢 Du har en färdig JSON-sträng. Deserialisera den till ett `Book`-objekt och skriv ut Title och Pages.

```csharp
using System.Text.Json;

class Book
{
    public string Title { get; set; } = "";
    public int Pages { get; set; }
}

string json = """{"Title":"The Hobbit","Pages":310}""";

// Deserialisera json till ett Book-objekt
// Skriv ut Title och Pages
```

**Förväntad output:**
```
Titel: The Hobbit
Sidor: 310
```
