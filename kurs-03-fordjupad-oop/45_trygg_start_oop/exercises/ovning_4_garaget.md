# Övning 4: garaget (repetition av abstract och interface)

Samma steg som i övning 3, men med ett nytt tema: **fordon** i stället för hjältar.
Inget nytt att lära sig här. Ni gör om det ni redan gjort, så att det sätter sig.

**Scenario:** i *Lilla Bildalen* står en bil, en cykel och en rostig bärgningsbil i samma garage.
Bilen heter Blixten, och hon är snabb. Bärgis kan laga andra fordon på plats. Trampy har ingen motor alls.

**Hur ni jobbar:** ni sitter i par. **En skriver, en läser högt.** Byt roller efter varje deluppgift.
Skapa ett nytt projekt: `dotnet new console -o Garaget`.

## Så här ordnar ni filerna

**En fil per klass, och mappar som visar vad filen är.** Det gäller från och med nu, i alla övningar.
Skriv aldrig flera klasser i `Program.cs`. Där ligger bara koden som kör programmet.

```
Garaget/
├── Program.cs
├── Abstracts/
│   └── Vehicle.cs
├── Interfaces/
│   └── IRepairer.cs
└── Models/
    ├── Car.cs
    ├── Bicycle.cs
    └── TowTruck.cs
```

- Filen heter som klassen, med stor bokstav: `Vehicle.cs`, `IRepairer.cs`.
- `Abstracts/` för abstrakta klasser, `Interfaces/` för interface, `Models/` för de vanliga klasserna.
- Ni behöver inte skriva `namespace` ännu. Mapparna är till för att ni ska hitta rätt.
- Skapa mappar och filer i VS Code (högerklicka i Explorer) eller i terminalen: `mkdir Abstracts Interfaces Models`.

Varje uppgift börjar med en ruta: **Vad** det är, **när** man använder det och **hur** man skriver det.
Läs rutan först, gör uppgiften sedan.

---

## 🟢 Uppgift 1: abstract och Honk

> **Vad:** en `abstract class` är en ritning som aldrig blir ett objekt.
> **När:** när alla barn är *samma sorts sak* och delar fält, men ska svara olika. Ett fordon i allmänhet finns inte, bara bilar och cyklar.
> **Hur:** `abstract class Vehicle` och `public abstract string Honk();`. Barnen skriver `override`.

1. Skapa `abstract class Vehicle` i `Abstracts/Vehicle.cs` med fälten `Name` (`string`) och `Condition` (`int`), samt en konstruktor.
2. Lägg till `public abstract string Honk();`.
3. Skapa `Car` och `Bicycle` i `Models/` (en fil var). De ärver från `Vehicle`. Låt dem skriva över `Honk()`:
   - `Car`: `Blixten tutar: Wrooom!` (med fordonets namn)
   - `Bicycle`: `Trampy ringer: Pling pling!`
4. I `Program.cs`: skapa en lista med `Blixten` (bil, 100) och `Trampy` (cykel, 60) och skriv ut `Honk()` för alla.

Prova också `new Vehicle("???", 10);`. Vad säger kompilatorn? Ta bort raden igen.

**Förväntad output:**
```
Blixten tutar: Wrooom!
Trampy ringer: Pling pling!
```

<details markdown="1">
<summary>Ledtråd</summary>

Konstruktorn i barnklassen ser ut så här:

```cs
public Car(string name, int condition) : base(name, condition) { }
```

Glömmer en barnklass `Honk()` får ni ett kompileringsfel. Det är meningen.

</details>

## 🟢 Uppgift 2: IRepairer och TowTruck

> **Vad:** ett `interface` är ett löfte om vad en klass *kan göra*.
> **När:** när en förmåga kan finnas hos helt olika sorters saker. En bärgningsbil kan laga andra fordon på plats, men en cykel kan inte det, och inte alla bilar heller.
> **Hur:** `interface IRepairer { void Repair(Vehicle target); }`. Klassen skriver `: Vehicle, IRepairer` och bestämmer själv hur den reparerar.

1. Skapa `interface IRepairer` i `Interfaces/IRepairer.cs` med metoden `void Repair(Vehicle target);`.
2. Skapa `TowTruck` i `Models/TowTruck.cs` (`Bärgis`, 70). Den ärver från `Vehicle` **och** använder `IRepairer`.
3. `TowTruck` ska skriva över `Honk()`: `Bärgis tutar rostigt: Töff töff!`
4. Implementera `Repair`: öka målets `Condition` med 20 och skriv `Bärgis reparerar Blixten.` (med rätt namn).
5. Lägg `Bärgis` sist i listan.

<details markdown="1">
<summary>Ledtråd</summary>

Basklassen står först, sedan interfacet: `class TowTruck : Vehicle, IRepairer`. Metoden måste vara `public`.

</details>

## 🟡 Uppgift 3: filtrera ut dem som kan reparera

> **Vad:** `OfType<T>()` plockar ur en lista det som är av typen `T`.
> **När:** när listan innehåller blandade saker och du bara vill åt dem som kan en viss sak.
> **Hur:** `garage.OfType<IRepairer>()`. Det fungerar på ett interface och på en basklass.

1. Använd `OfType<IRepairer>()` i en `foreach` för att hitta alla som kan reparera.
2. Låt var och en reparera den första i listan, `garage[0]`.
3. Skriv ut Blixtens skick efteråt.

**Förväntad output (efter tutningen):**
```
Bärgis reparerar Blixten.
Blixten har 120 i skick.
```

<details markdown="1">
<summary>Ledtråd</summary>

```cs
foreach (IRepairer repairer in garage.OfType<IRepairer>())
{
    repairer.Repair(garage[0]);
}
```

Bilen och cykeln hoppas över, för de har inte lovat att kunna reparera.

</details>

## 🟡 Uppgift 4: abstract eller interface?

Samma fråga som på sista bilden i lektionen. Nu får ni svara med fordon.

Sortera in varje rad under **abstract class** (är en sorts) eller **interface** (kan göra):

| | abstract class eller interface? |
|---|---|
| a | Att kunna **tanka** (bilen, traktorn och båten kan) |
| b | **Motorfordon** (bilen, mopeden och lastbilen är alla en sorts) |
| c | Att kunna **dra en släp** (traktorn och lastbilen kan, men inte mopeden) |
| d | **Fartyg** (färjan och segelbåten är en sorts) |

Testa svaret med meningen: *"En bil **är en sorts** ___"* eller *"En bil **kan** ___"*. Den som passar bäst visar vägen.

<details markdown="1">
<summary>Svar</summary>

- a: **interface** (`IFuelable`). Förmågan delas av olika sorters saker.
- b: **abstract class** (`MotorVehicle`). Alla är samma sorts sak och delar fält.
- c: **interface** (`ITowing`). Bara vissa kan, och de är inte samma sorts sak.
- d: **abstract class** (`Ship`). Samma sorts sak.

Fastnade ni? Fråga er om det är en *sorts* eller en *förmåga*.

</details>

---

## Prata med din partner

Titta på `Vehicle` och `IRepairer` i er kod. Varför blev det en `abstract class` på ena stället och ett `interface` på det andra?
Svara med "är en sorts" och "kan göra".

---

## Avsluta: byt kod och ge en gåva 🎁

Byt med ett annat par. Ni läser varandras kod och ger varandra återkoppling. Ta ca 10 minuter, fem åt varje håll.

1. Öppna det andra parets projekt. Titta på mapparna och filerna först, läs sedan koden.
2. **Testkör programmet.** Skriver det ut samma sak som den förväntade outputen i uppgifterna?
3. **Leta efter faror.** Fundera på vad som händer när programmet inte får det det förväntar sig.
   Prova gärna i en **kopia** av projektet, så att originalet är kvar. Några frågor att utgå från:
   - Listan är tom och koden skriver `garage[0]`. Vad händer?
   - `Repair` får `null` i stället för ett fordon. Vad händer?
   - Ett fordon får ett tomt namn, eller `Condition` blir negativ. Är det okej?
   - Blixten har 120 i skick efter reparationen. Borde skicket kunna gå över 100?
   - Programmet läste in siffror med `Console.ReadLine()`, och användaren skrev text i stället för en siffra. Vad skulle hända med `int.Parse("abc")`?

   Ni behöver inte *fixa* något. Det räcker att hitta och skriva ner det. Det är precis sådant som tester
   fångar i kursen om unit testing senare.
4. Berätta för dem:
   - 🟢 **Något som fungerar bra**, och var du ser det. ("Jag gillar att varje barnklass har sin egen fil. Då hittar jag direkt.")
   - ❓ **En fråga** som du undrar över. ("Varför lade ni den metoden i basklassen?")
   - ⚠️ **En fara du hittade**, formulerad som en fråga. ("Vad tror ni händer om listan är tom?")
   - 💡 **Ett förslag**, formulerat som en möjlighet. ("Ni skulle kunna prova att ...")
5. Tacka varandra.

**Feedback är en gåva.** Den som ger den delar något den har tänkt på, för att den vill väl.
Den som tar emot den bestämmer själv vad gåvan ska bli: något att använda nu, något att fundera på
till senare, eller något att lämna kvar. Ingen gåva är ett krav, och ingen behöver försvara sig.
Det räcker att säga "tack".

**Att tänka på när du ger:** tala om koden, inte om personen ("koden gör ..." i stället för "du gör ..."),
och var konkret. Det som går att peka på går också att använda.
