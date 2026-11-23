# Inställningar med nyckel

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skapa `class Settings` med en indexer av typen `string` som tar en nyckel. Använd en `Dictionary<string, string>` inuti. Om nyckeln saknas ska get returnera en tom sträng i stället för att krascha.

Fyll i det som står `/* din kod */`.

```csharp
var settings = new Settings();
settings["theme"] = "mörk";
Console.WriteLine($"theme: {settings["theme"]}");             // theme: mörk
Console.WriteLine($"language: \"{settings["language"]}\"");   // language: "" (tom sträng)

class Settings
{
    private readonly Dictionary<string, string> values = new();

    public string this[string key]
    {
        /* din kod:
           get: finns nyckeln i values, returnera värdet
                annars returnera en tom sträng
           set: spara värdet under nyckeln */
    }
}
```

Exempeloutput:

```text
theme: mörk
language: ""
```

<details markdown="1">
<summary>Tips 1: en indexer med text</summary>

Indexern tar en `string` i stället för en `int`. Annars fungerar den som vanligt, med en `get` och en `set`.

</details>

<details markdown="1">
<summary>Tips 2: set är enkel</summary>

I `set` heter det nya värdet `value`. Skriv `values[key] = value;`. Det lägger till nyckeln om den saknas, och skriver över den om den finns.

</details>

<details markdown="1">
<summary>Tips 3: get utan krasch</summary>

`values[key]` kraschar om nyckeln saknas. Använd `values.TryGetValue(key, out var value)` i stället. Den returnerar `true` om nyckeln fanns. Med `? :` kan du då välja mellan `value` och `""` på en rad.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
var settings = new Settings();
settings["theme"] = "mörk";
Console.WriteLine($"theme: {settings["theme"]}");             // theme: mörk
Console.WriteLine($"language: \"{settings["language"]}\"");   // language: "" (tom sträng)

class Settings
{
    private readonly Dictionary<string, string> values = new();

    public string this[string key]
    {
        get => values.TryGetValue(key, out var value) ? value : "";
        set => values[key] = value;
    }
}
```

</details>
