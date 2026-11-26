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

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
