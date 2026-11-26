# Repetition — Lista → JSON

🟢 Skapa tre `Book`-objekt, lägg dem i en `List<Book>` och serialisera hela listan.
Använd `WriteIndented = true` så att output är läsbar.

```csharp
using System.Text.Json;

class Book
{
    public string Title { get; set; } = "";
    public int Pages { get; set; }
}

// Skapa tre böcker
// Lägg dem i en List<Book>
// Serialisera med WriteIndented = true
// Console.WriteLine
```

**Förväntad output:**
```json
[
  {
    "Title": "The Hobbit",
    "Pages": 310
  },
  {
    "Title": "1984",
    "Pages": 328
  },
  {
    "Title": "Dune",
    "Pages": 412
  }
]
```

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
