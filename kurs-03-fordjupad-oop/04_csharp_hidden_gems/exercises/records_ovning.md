# Records: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Skapa `record Booking(string Guest, DateTime CheckIn, int Nights)`.
2. Skapa två bokningar med samma värden och jämför dem med `==`.
3. Gör en ny bokning med `with` som förlänger vistelsen med en natt.

Fyll i det som står `/* din kod */`.

```csharp
/* din kod 2:
   skapa bokning a: Ada, 20 november 2026, 2 nätter
   skapa bokning b med exakt samma värden */
Console.WriteLine(a == b);

/* din kod 3:
   gör en kopia av a som heter longer
   ändra bara antalet nätter till 3 */
Console.WriteLine(longer.Nights);

/* din kod 1:
   deklarera en record Booking med gäst, incheckningsdatum och antal nätter */
```

Exempeloutput:

```text
True
3
```

<details markdown="1">
<summary>Tips 1: var hamnar recorden?</summary>

I ett program med top-level statements måste typer som `record` stå sist i filen, efter all kod som körs. Annars får du felet CS8803.

</details>

<details markdown="1">
<summary>Tips 2: en record på en rad</summary>

En positionell record skrivs som en metodsignatur med `record` framför: `public record Namn(typ Egenskap, ...);`. Du skapar en bokning med `new Booking(...)` och ett datum med `new DateTime(2026, 11, 20)`.

</details>

<details markdown="1">
<summary>Tips 3: jämföra och kopiera</summary>

En record jämförs på värden, så `a == b` blir `True` när alla egenskaper är lika. För kopian skriver du `a with { Nights = ... }`. Då får du ett nytt objekt och `a` ändras inte.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
var a = new Booking("Ada", new DateTime(2026, 11, 20), 2);
var b = new Booking("Ada", new DateTime(2026, 11, 20), 2);
Console.WriteLine(a == b);                 // True
var longer = a with { Nights = 3 };
Console.WriteLine(longer.Nights);          // 3

public record Booking(string Guest, DateTime CheckIn, int Nights);
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
