# Extension methods: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Nivå:** 🟢

**Tid:** ca 20 minuter.

## Varför vill man ha den här?

Tänk dig hotellets bokningssystem. Det räknar procent hela tiden: 25 % moms, 10 % rabatt för stamgäster, 5 % i avgift vid kortbetalning. Utan en hjälpmetod skrivs `pris * 25 / 100m` på tjugo ställen. Förr eller senare blir ett av dem fel, till exempel när någon glömmer `m` eller delar med 10.

Med `pris.Percent(25)` läses koden nästan som en mening, och formeln finns på ett enda ställe. Ska du avrunda till hela ören en dag, ändrar du på ett ställe.

Varför en extension method? Du kan inte lägga till metoder i `decimal`, för den typen äger Microsoft. En extension method låter dig ändå skriva `pris.Percent(25)`, som om metoden alltid hade funnits där.

## Uppgifter

1. Skriv en extension method `Percent(this decimal value, int percent)` som returnerar procenten av värdet.
2. Använd den på 200 med 25.

Fyll i det som står `/* din kod */`.

```csharp
Console.WriteLine(200m.Percent(25));   // 50

public static class DecimalExtensions
{
    public static decimal Percent(this decimal value, int percent)
    {
        /* din kod:
           multiplicera värdet med procenten
           dela med 100
           returnera svaret */
    }
}
```

Exempeloutput:

```text
50
```

<details markdown="1">
<summary>Tips 1: var hamnar klassen?</summary>

I ett program med top-level statements måste klassen stå sist i filen, efter raden med `Console.WriteLine`. Annars får du felet CS8803.

</details>

<details markdown="1">
<summary>Tips 2: vad gör this?</summary>

Ordet `this` framför den första parametern gör metoden till en extension method. Därför kan du skriva `200m.Percent(25)`. Då blir `value` 200 och `percent` 25.

</details>

<details markdown="1">
<summary>Tips 3: räkna procent</summary>

25 procent av 200 är 200 gånger 25, delat med 100. Dela med `100m` så att svaret blir ett `decimal`. Hela metoden får plats på en rad med `return`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
Console.WriteLine(200m.Percent(25));   // 50

public static class DecimalExtensions
{
    public static decimal Percent(this decimal value, int percent)
    {
        return value * percent / 100m;
    }
}
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
