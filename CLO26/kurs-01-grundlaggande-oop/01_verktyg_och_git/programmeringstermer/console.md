# Programmeringstermer — Console

`Console` är klassen som kopplar ditt program till terminalfönstret — allt du skriver ut, allt en användare skriver in, och var på skärmen det hamnar går genom den.

---

## Console.WriteLine()

Skriver ut text **och** hoppar till en ny rad direkt efteråt. Det du redan använt sedan dag 1.

```csharp
Console.WriteLine("System online.");
Console.WriteLine("Scanning for intelligent lifeforms...");
Console.WriteLine("...Something in front of the monitor");
Console.WriteLine("");
Console.WriteLine("...No intelligent lifeforms found");
```

### Output
```plaintext
System online.
Scanning for intelligent lifeforms...
...Something in front of the monitor

...No intelligent lifeforms found
```

Varje `WriteLine()`-anrop hamnar garanterat på sin egen rad — det är hela poängen med "Line" i namnet.

**Se även:** [Console.Write()](#consolewrite) — samma sak, men utan radbytet.

---

## Console.Write()

Skriver ut text **utan** att hoppa till en ny rad. Nästa utskrift, oavsett om det är `Write()` eller `WriteLine()`, fortsätter på samma rad.

```csharp
Console.Write("Laddar");
Console.Write(".");
Console.Write(".");
Console.Write(".");
Console.WriteLine(" klar!");
```

### Output
```plaintext
Laddar... klar!
```

<details><summary>Varför blev det en enda rad?</summary>

`Write()` lägger bara till text på samma plats den senaste utskriften slutade — ingen radbrytning läggs till automatiskt. De fyra första anropen bygger upp `"Laddar..."` bit för bit på samma rad. Det är först det avslutande `WriteLine(" klar!")` som lägger till radbrytningen, i slutet av allt.

</details>

---

## Console.ReadLine()

Vägen tillbaka — `WriteLine`/`Write` skickar text **till** terminalen, `ReadLine()` läser text **från** den. Programmet pausar och väntar tills användaren skrivit något och tryckt Enter.

```csharp
Console.Write("Ange ditt namn: ");
string namn = Console.ReadLine();
Console.WriteLine($"Välkommen, {namn}!");
```

### Output
```plaintext
Ange ditt namn: Batman
Välkommen, Batman!
```

<details><summary>Varför `Console.Write()` och inte `WriteLine()` på frågan?</summary>

Du vill att svaret hamnar på **samma rad** som frågan ("Ange ditt namn: Batman"), inte på en ny rad under. `Write()` (utan radbyte) lämnar markören precis efter kolonet, redo för användaren att skriva direkt där.

</details>

**Se även:** [Console.ReadKey()](../../06_datastrukturer/programmeringstermer/console.md#consolereadkey) — om du bara behöver ett enda tangenttryck istället för en hel rad (vanligt i spel).

---

## Console.Clear()

Rensar hela terminalfönstret — allt som stått där tidigare försvinner, och markören hamnar längst upp till vänster igen.

```csharp
Console.WriteLine("Detta försvinner snart...");
Console.ReadKey();   // väntar på ett tangenttryck
Console.Clear();
Console.WriteLine("Ny, tom skärm.");
```

Går inte att visa som en `### Output`-ruta på samma sätt som de andra exemplen — hela poängen är att allt **innan** `Clear()` försvinner. Typisk användning: rensa skärmen mellan menyval i ett konsolprogram, så användaren inte ser gammal text blandad med ny.

---

## Färger — ForegroundColor / BackgroundColor

Sätter färg på texten (`ForegroundColor`) respektive bakgrunden (`BackgroundColor`) för allt som skrivs ut **efter** att du satt egenskapen — inte bara nästa rad.

```csharp
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("System online.");
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("VARNING: Obehörig åtkomst upptäckt.");
Console.ResetColor();
Console.WriteLine("Tillbaka till normalt.");
```

### Output
```plaintext
System online.
VARNING: Obehörig åtkomst upptäckt.
Tillbaka till normalt.
```

*(Färgerna syns förstås inte i denna textfil — kör koden i din egen terminal för att se grönt, rött och sedan standardfärgen.)*

<details><summary>Varför `ResetColor()` på sista raden?</summary>

`ForegroundColor` ändrar färgen **permanent** tills du själv ändrar den igen — den återgår inte automatiskt. Glömmer du `ResetColor()` fortsätter resten av allt du skriver ut (även efter programmet avslutats, i samma terminalfönster) att vara rött. Det är en klassisk nybörjarbugg: "varför är hela min terminal röd nu?"

</details>

---

## Cursor-position — CursorLeft / CursorTop / SetCursorPosition

Flyttar var **nästa** utskrift hamnar på skärmen, räknat i kolumn (`Left`) och rad (`Top`) — båda räknat från 0, precis som strängindex (se [Variabler i minnet](datatyper.md#variabler-i-minnet)).

```csharp
Console.WriteLine("Laddar: 0%");
Console.SetCursorPosition(9, 0);   // kolumn 9, rad 0 — rätt ovanpå "0%"
Console.Write("50%");
```

### Output
```plaintext
Laddar: 50%
```

<details><summary>Hur funkar det att skriva "ovanpå" något som redan står där?</summary>

`SetCursorPosition(9, 0)` flyttar inte texten — den flyttar **var nästa tecken skrivs ut**. Eftersom "0%" stod på exakt den positionen, och "50%" är lika långt (3 tecken), skrivs det rakt över och ser ut som en uppdatering på plats. Det är exakt så en progressbar i terminalen fungerar — samma rad uppdateras om och om igen istället för att skriva en ny rad för varje steg.

</details>

---

## Nästa steg: mer avancerat

`Console.ReadKey()`/`Console.ReadLine()` (läsa in från användaren), `Console.WindowWidth`/`WindowHeight` (känna till terminalens storlek), och riktiga progressbarer/menyer byggda med cursor-positionering hör hemma i en framtida, mer avancerad kategorifil — när vi är där i kursen.
