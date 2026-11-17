# Repetition — WriteAllText och ReadAllText

🟢 Spara en mening till en fil. Läs tillbaka den och skriv ut.

```csharp
// Steg 1: Spara
string message = "Hej från filen!";
// File.WriteAllText(filnamn, text)

// Steg 2: Läs tillbaka
// string loaded = File.ReadAllText(filnamn)
// Console.WriteLine(loaded)
```

**Förväntad output:**
```
Hej från filen!
```

---

🟡 Spara ett tal som text, läs tillbaka det och konvertera till `int`.

```csharp
int score = 9000;
// Spara score som text till "score.txt"
// Läs tillbaka som string
// Konvertera till int med int.Parse
// Skriv ut: "Poäng: 9000"
```

**Förväntad output:**
```
Poäng: 9000
```
