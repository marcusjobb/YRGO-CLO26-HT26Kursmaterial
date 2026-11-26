---
marp: true
theme: nion-dark
paginate: true
footer: "© 2025–2026 Nion Education. Får inte användas av andra skolor utan tillstånd."
---

# Indexers

**Vecka 48 · Hellweek · Hidden gems**

---

## Använd objektet som en array

```csharp
class Shelf
{
    private readonly string[] books = new string[5];

    public string this[int index]
    {
        get => books[index];
        set => books[index] = value;
    }
}

var shelf = new Shelf();
shelf[0] = "Clean Code";
Console.WriteLine(shelf[0]);
```

---

## Varför?

Koden som använder klassen blir lika kort som om det vore en lista. Men klassen bestämmer själv vad som händer.

---

## Wow: kontrollen sitter i klassen

Lägg till en gräns i `get` och `set`. Då kan ingen skriva utanför hyllan, oavsett var i programmet det görs.

---

## Sammanfattning

- En indexer är `this[...]` i en klass.
- Den gör ett objekt indexerbart, precis som en array.
- Lägg kontroller i klassen, inte i anroparen.
