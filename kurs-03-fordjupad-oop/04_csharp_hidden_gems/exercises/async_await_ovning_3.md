# Fel i en asynkron metod

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Nivå:** 🔴

**Tid:** ca 20 minuter.

## Uppgift

Skriv en async-metod som kastar ett undantag efter en fördröjning. Anropa den med `await` i en `try`/`catch` och skriv ut felmeddelandet.

Fyll i det som står `/* din kod */`.

```csharp
async Task FailAfterDelay()
{
    /* din kod:
       vänta en halv sekund
       kasta ett InvalidOperationException med texten "Något gick fel" */
}

/* din kod:
   anropa FailAfterDelay med await
   fånga InvalidOperationException
   skriv ut "Fångade: " följt av felmeddelandet */
```

Exempeloutput (efter ca en halv sekund):

```text
Fångade: Något gick fel
```

<details markdown="1">
<summary>Tips 1: två delar</summary>

Metoden ska först vänta och sedan kasta ett fel. Sedan behöver huvudprogrammet ta emot felet, så att programmet inte kraschar.

</details>

<details markdown="1">
<summary>Tips 2: vänta och kasta</summary>

`await Task.Delay(500);` väntar en halv sekund. Ett fel kastar du med `throw new InvalidOperationException("...")`.

</details>

<details markdown="1">
<summary>Tips 3: try och catch</summary>

Lägg `await FailAfterDelay();` inuti ett `try`-block. I `catch (InvalidOperationException ex)` skriver du ut `ex.Message`. Tack vare `await` hamnar felet i din `catch`, precis som i vanlig kod.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
async Task FailAfterDelay()
{
    await Task.Delay(500);
    throw new InvalidOperationException("Något gick fel");
}

try
{
    await FailAfterDelay();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Fångade: {ex.Message}");
}
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
