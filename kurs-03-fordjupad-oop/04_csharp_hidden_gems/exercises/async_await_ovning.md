# Async och await: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Skriv tre metoder som var och en väntar en sekund och sedan returnerar ett tal.
2. Kör dem efter varandra med `await`. Mät tiden.
3. Kör dem samtidigt med `Task.WhenAll`. Mät tiden igen.

<details markdown="1">
<summary>Lösningsförslag</summary>

Steg 2 tar ungefär tre sekunder. Steg 3 tar ungefär en sekund, eftersom de väntar samtidigt.

</details>
