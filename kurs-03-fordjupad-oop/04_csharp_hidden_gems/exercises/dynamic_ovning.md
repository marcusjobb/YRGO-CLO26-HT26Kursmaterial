# Dynamic: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Skriv ett program med `dynamic x = 5;`. Byt till en sträng och skriv ut längden.
2. Lägg till `x.Foo();` och kör. Vad händer? Varför kompilerar det?
3. Byt `dynamic` till `var` och kör igen. Vad ändras i felmeddelandet?

Fyll i det som står `/* din kod */`.

```csharp
using Microsoft.CSharp.RuntimeBinder;

dynamic x = 5;
Console.WriteLine(x);

/* din kod (steg 1):
   ge x en ny sträng, till exempel "Hej Göteborg"
   skriv ut längden på x */

/* din kod (steg 2):
   anropa x.Foo()
   lägg anropet i try, fånga RuntimeBinderException
   skriv ut "Fel vid körning: " och felmeddelandet */

/* din kod (steg 3):
   byt dynamic till var på raden högst upp
   försök köra igen och läs felet */
```

Exempeloutput för steg 1 och 2:

```text
5
12
Fel vid körning: 'string' does not contain a definition for 'Foo'
```

I steg 3 startar programmet inte alls. Kompilatorn stoppar det med ett fel i stil med:

```text
error CS1061: 'int' does not contain a definition for 'Foo' ...
```

Med `var` blir det också ett fel redan på raden `x = "Hej Göteborg";`, eftersom `x` då är en `int`.

<details markdown="1">
<summary>Tips 1: dynamic får byta typ</summary>

En variabel av typen `dynamic` kan få ett nytt värde av en annan typ. Du kan alltså skriva `x = "..."` fast den först var ett tal.

</details>

<details markdown="1">
<summary>Tips 2: längden</summary>

En sträng har egenskapen `Length`. Skriv ut `x.Length`. Kompilatorn kontrollerar inte att den finns. Det sker först när programmet körs.

</details>

<details markdown="1">
<summary>Tips 3: fånga felet</summary>

Lägg `x.Foo();` inuti `try { ... }`. Skriv sedan `catch (RuntimeBinderException ex)` och skriv ut `ex.Message`. Utan `try` kraschar programmet med samma meddelande.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

Svar: Steg 2 kompilerar eftersom kompilatorn inte kontrollerar `dynamic`. Det kraschar i körningen med `RuntimeBinderException`. Med `var` får du ett kompileringsfel direkt.

```csharp
using Microsoft.CSharp.RuntimeBinder;

dynamic x = 5;
Console.WriteLine(x);

x = "Hej Göteborg";
Console.WriteLine(x.Length);

try
{
    x.Foo();
}
catch (RuntimeBinderException ex)
{
    Console.WriteLine($"Fel vid körning: {ex.Message}");
}
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
