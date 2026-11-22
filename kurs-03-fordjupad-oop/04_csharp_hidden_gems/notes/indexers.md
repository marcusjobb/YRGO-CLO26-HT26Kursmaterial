# Indexers

En array och en lista används med hakparenteser: `list[0]`. En indexer gör att din egen klass kan användas på samma sätt. Det ger en klass som känns naturlig för den som använder den.

## En hylla med böcker

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

`this[int index]` är själva indexern. `get` och `set` fungerar som en property, och koden som använder klassen behöver inte veta att det finns en array inuti.

## Varför inte bara göra arrayen public?

Om arrayen är `public` kan vem som helst ändra vilken plats som helst, och den enda kontrollen sitter hos anroparen. Med en indexer bestämmer klassen själv vad som får hända.

## Kontrollen sitter i klassen

```csharp
public string this[int index]
{
    get => books[CheckIndex(index)];
    set => books[CheckIndex(index)] = value;
}

private int CheckIndex(int index)
{
    if (index < 0 || index >= books.Length)
        throw new ArgumentOutOfRangeException(nameof(index));
    return index;
}
```

Nu gäller kontrollen oavsett var i programmet som hyllan används. Det är en av de största fördelarna med att kapsla in data.

## Indexers med nycklar

Indexern behöver inte ta ett tal. Den kan ta en text, och då är den ett sätt att slå upp värden efter namn. Det liknar en `Dictionary`, men du bestämmer själv vad som händer om nyckeln saknas.

## Sammanfattning

- En indexer är `this[...]` i en klass.
- Den gör ett objekt användbart med hakparenteser.
- Lägg kontrollerna i klassen, inte hos den som använder den.

## Prova själv

Gör `indexers_ovning.md`, `indexers_ovning_2.md` och `indexers_ovning_3.md`.
