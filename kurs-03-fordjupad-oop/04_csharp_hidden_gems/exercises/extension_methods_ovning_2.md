# WordCount på string

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Varför vill man ha den här?

Ett kommentarsfält tillåter högst 200 ord. En ansökan ska vara minst 50 ord. En blogg visar "Lästid: 3 min", och det räknas fram från antalet ord. Alla de här behöver räkna ord.

`string` har `Length`, men den räknar tecken, inte ord. Med `text.WordCount()` känns det som att `string` alltid har kunnat räkna ord. Metoden dyker också upp i IntelliSense när du skriver en punkt efter en sträng, så att den som kommer efter dig hittar den.

## Uppgift

Skriv en extension method `WordCount(this string text)` som returnerar antalet ord. Tom sträng ger 0. Testa med "hej där världen" och med en tom sträng.

Fyll i det som står `/* din kod */`.

```csharp
Console.WriteLine("hej där världen".WordCount());   // 3
Console.WriteLine("".WordCount());                  // 0

public static class StringExtensions
{
    public static int WordCount(this string text)
    {
        /* din kod:
           om texten är tom, returnera 0
           dela upp texten vid mellanslag
           returnera hur många delar det blev */
    }
}
```

Exempeloutput:

```text
3
0
```

<details markdown="1">
<summary>Tips 1: börja med den tomma strängen</summary>

Ta hand om specialfallet först. Om texten är tom, eller bara innehåller mellanslag, ska metoden returnera 0 direkt.

</details>

<details markdown="1">
<summary>Tips 2: dela upp texten</summary>

`text.Split(' ')` ger en array med orden. Antalet ord är längden på arrayen, alltså `.Length`.

</details>

<details markdown="1">
<summary>Tips 3: dubbla mellanslag</summary>

Med två mellanslag i rad får du tomma delar i arrayen. Skicka med `StringSplitOptions.RemoveEmptyEntries` till `Split` så tas de bort. För tom-kontrollen passar `string.IsNullOrWhiteSpace(text)`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
Console.WriteLine("hej där världen".WordCount());   // 3
Console.WriteLine("".WordCount());                  // 0

public static class StringExtensions
{
    public static int WordCount(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;
        return text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
```

</details>
