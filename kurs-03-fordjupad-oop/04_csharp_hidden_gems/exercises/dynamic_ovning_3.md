# Kontrollera typen innan du använder den

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

1. Skriv en metod `static void PrintLength(dynamic? value)` som skriver ut längden om värdet är en sträng.
2. Om det inte är en sträng, skriv ut "Inte text".
3. Anropa den med `"hej"`, `42` och `null`.

Frågetecknet i `dynamic?` betyder att värdet får vara `null`. Utan det får du en varning när du skickar in `null`.

Fyll i det som står `/* din kod */`.

```csharp
static void PrintLength(dynamic? value)
{
    /* din kod (steg 1):
       om value är en sträng: skriv ut "Längd: " och längden */

    /* din kod (steg 2):
       annars: skriv ut "Inte text" */
}

/* din kod (steg 3):
   anropa PrintLength med "hej", med 42 och med null */
```

Exempeloutput:

```text
Längd: 3
Inte text
Inte text
```

<details markdown="1">
<summary>Tips 1: fråga efter typen</summary>

Du kan inte lita på att `value` har en `Length`. Ett tal har ingen längd. Kontrollera därför typen först.

</details>

<details markdown="1">
<summary>Tips 2: nyckelordet is</summary>

`value is string` blir `true` bara om värdet är en sträng. För `42` och för `null` blir det `false`.

</details>

<details markdown="1">
<summary>Tips 3: spara strängen direkt</summary>

Skriv `if (value is string text)`. Då får du en variabel `text` av typen `string` inne i if-blocket och kan skriva ut `text.Length`. Lägg `Console.WriteLine("Inte text");` i `else`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
static void PrintLength(dynamic? value)
{
    if (value is string text)
        Console.WriteLine($"Längd: {text.Length}");
    else
        Console.WriteLine("Inte text");
}

PrintLength("hej");   // Längd: 3
PrintLength(42);      // Inte text
PrintLength(null);    // Inte text
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
