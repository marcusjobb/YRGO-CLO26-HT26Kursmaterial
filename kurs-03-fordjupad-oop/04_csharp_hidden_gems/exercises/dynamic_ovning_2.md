# Dynamiska fält med ExpandoObject

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

1. Skapa ett `dynamic person = new ExpandoObject();` och lägg till `Name` och `Age` på det.
2. Skriv ut båda fälten.
3. Lägg till ett fält till som du inte deklarerat i förväg. Vad händer?

Fyll i det som står `/* din kod */`.

```csharp
using System.Dynamic;

dynamic person = new ExpandoObject();

/* din kod (steg 1):
   ge person namnet "Ada"
   ge person åldern 36 */

/* din kod (steg 3):
   ge person ett nytt fält City med värdet "Göteborg" */

/* din kod (steg 2):
   skriv ut namn, ålder och stad på en rad, åtskilda med komma */
```

Exempeloutput:

```text
Ada, 36, Göteborg
```

<details markdown="1">
<summary>Tips 1: ingen klass behövs</summary>

Ett `ExpandoObject` har inga fält från början. Fälten skapas i samma stund som du ger dem ett värde.

</details>

<details markdown="1">
<summary>Tips 2: så skapar du ett fält</summary>

Skriv precis som om fältet redan fanns: `person.Name = "Ada";`. Gör likadant med `Age` och `City`.

</details>

<details markdown="1">
<summary>Tips 3: skriv ut</summary>

Använd en interpolerad sträng: `$"{person.Name}, {person.Age}, ..."`. Det blir inget kompileringsfel för `City`, eftersom kompilatorn inte kontrollerar `dynamic`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
using System.Dynamic;

dynamic person = new ExpandoObject();
person.Name = "Ada";
person.Age = 36;
person.City = "Göteborg";   // Nytt fält, inget kompileringsfel
Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
