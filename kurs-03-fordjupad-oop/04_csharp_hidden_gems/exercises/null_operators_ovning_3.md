# Cache med ??=

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv en klass `Cache` med ett fält `List<string>? items`. Metoden `Add(string item)` ska skapa listan första gången den används, och sedan lägga till objektet. Använd `??=`.

Fyll i det som står `/* din kod */`.

```csharp
var cache = new Cache();
Console.WriteLine($"Före: {cache.Count}");

cache.Add("kaffe");
cache.Add("bulle");
Console.WriteLine($"Efter: {cache.Count}");

class Cache
{
    private List<string>? items;

    public void Add(string item)
    {
        /* din kod:
           skapa listan om den inte finns än
           lägg till item i listan */
    }

    public int Count => items?.Count ?? 0;
}
```

Exempeloutput:

```text
Före: 0
Efter: 2
```

<details markdown="1">
<summary>Tips 1: varför vänta med listan</summary>

Fältet `items` är `null` från början. Listan skapas först när någon faktiskt lägger till något. Det kallas att skapa listan "lat", alltså först när den behövs.

</details>

<details markdown="1">
<summary>Tips 2: vad gör ??=</summary>

`x ??= värde` betyder: om `x` är `null`, sätt `x` till `värde`. Är `x` redan satt händer ingenting. Det är en kortare form av en `if` som kollar `null`.

</details>

<details markdown="1">
<summary>Tips 3: två rader</summary>

Metoden behöver bara två rader. Den första börjar så här:

```csharp
items ??= ...
```

På andra raden lägger du till `item` i listan som vanligt.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
var cache = new Cache();
Console.WriteLine($"Före: {cache.Count}");

cache.Add("kaffe");
cache.Add("bulle");
Console.WriteLine($"Efter: {cache.Count}");

class Cache
{
    private List<string>? items;

    public void Add(string item)
    {
        items ??= new List<string>();
        items.Add(item);
    }

    public int Count => items?.Count ?? 0;
}
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
