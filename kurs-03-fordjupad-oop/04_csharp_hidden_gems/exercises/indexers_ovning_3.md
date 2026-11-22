# Inställningar med nyckel

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skapa `class Settings` med en indexer av typen `string` som tar en nyckel. Använd en `Dictionary<string, string>` inuti. Om nyckeln saknas ska get returnera en tom sträng i stället för att krascha.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
class Settings
{
    private readonly Dictionary<string, string> values = new();

    public string this[string key]
    {
        get => values.TryGetValue(key, out var value) ? value : "";
        set => values[key] = value;
    }
}

var settings = new Settings();
settings["theme"] = "mörk";
Console.WriteLine(settings["theme"]);   // mörk
Console.WriteLine(settings["language"]); // (tom rad)
```

</details>
