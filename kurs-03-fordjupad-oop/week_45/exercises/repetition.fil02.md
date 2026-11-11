# Repetition — WriteAllLines, ReadAllLines och enskilda rader

🟢 Spara en lista med rader till en fil. Läs tillbaka och skriv ut rad 3.

```csharp
// Steg 1: Spara
var lines = new List<string>
{
    "rad ett",
    "rad två",
    "rad tre",
    "rad fyra",
    "rad fem"
};
// File.WriteAllLines("rader.txt", lines)

// Steg 2: Läs tillbaka
// string[] loaded = File.ReadAllLines("rader.txt")

// Steg 3: Hämta rad 3
// Kom ihåg: index börjar på 0, så rad 3 är index 2
// Console.WriteLine(loaded[2])
```

**Förväntad output:**
```
rad tre
```

---

🟡 Läs tillbaka samma fil. Skriv ut rad 5 — och hur många rader filen totalt innehåller.

```csharp
string[] loaded = File.ReadAllLines("rader.txt");
// Skriv ut loaded.Length
// Skriv ut loaded[4]
```

**Förväntad output:**
```
Filen har 5 rader.
Rad 5: rad fem
```

---

🟡 Lägg till en ny rad i filen utan att skriva över de gamla. Tips: `File.AppendAllText`.

```csharp
File.AppendAllText("rader.txt", "\nrad sex");
string[] updated = File.ReadAllLines("rader.txt");
Console.WriteLine($"Nu har filen {updated.Length} rader.");
Console.WriteLine($"Sista raden: {updated[updated.Length - 1]}");
```

**Förväntad output:**
```
Nu har filen 6 rader.
Sista raden: rad sex
```
