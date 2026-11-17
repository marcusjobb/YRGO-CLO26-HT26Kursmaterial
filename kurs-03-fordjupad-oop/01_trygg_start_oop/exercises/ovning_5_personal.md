# Övning 5: personalsystemet (repetition av abstract och interface)

Samma steg som i övning 3 och 4, men i ett system du kan möta på jobbet: **personal** på ett företag.
Inget nytt att lära sig. Ni gör om det ni redan gjort, i ett mer allvarligt sammanhang.

**Scenario:** ett företag har tre sorters personal: **chefer**, **konsulter** och **vikarier**. Ni bygger
den del av personalsystemet som räknar ut månadens lön och som låter chefer godkänna tidrapporter.

**Hur ni jobbar:** ni sitter i par. **En skriver, en läser högt.** Byt roller efter varje deluppgift.
Skapa ett nytt projekt: `dotnet new console -o Personal`.

## Så här ordnar ni filerna

**En fil per klass, och mappar som visar vad filen är.** Det gäller från och med nu, i alla övningar.
Skriv aldrig flera klasser i `Program.cs`. Där ligger bara koden som kör programmet.

```
Personal/
├── Program.cs
├── Abstracts/
│   └── Employee.cs
├── Interfaces/
│   └── IApprover.cs
└── Models/
    ├── Manager.cs
    ├── Consultant.cs
    └── Substitute.cs
```

- Filen heter som klassen, med stor bokstav: `Employee.cs`, `IApprover.cs`.
- `Abstracts/` för abstrakta klasser, `Interfaces/` för interface, `Models/` för de vanliga klasserna.
- Ni behöver inte skriva `namespace` ännu. Mapparna är till för att ni ska hitta rätt.
- Skapa mappar och filer i VS Code (högerklicka i Explorer) eller i terminalen: `mkdir Abstracts Interfaces Models`.

Varje uppgift börjar med en ruta: **Vad** det är, **när** man använder det och **hur** man skriver det.
Läs rutan först, gör uppgiften sedan.

---

## Namn som alla förstår

Från och med den här övningen följer vi de namnregler som C#-utvecklare använder överallt:

| Vad | Regel | Exempel |
|---|---|---|
| Klass, metod, property | **PascalCase**: varje ord börjar med stor bokstav | `Employee`, `CalculatePay`, `HoursWorked` |
| Parameter och lokal variabel | **camelCase**: första ordet med liten bokstav | `name`, `hoursWorked`, `staff` |
| Interface | Börjar med **`I`** och namnges efter förmågan | `IApprover`, `IRepairer` |
| Fil | Heter som klassen | `Employee.cs`, `IApprover.cs` |

I .NET slutar interface som beskriver en förmåga ofta på **-able** eller **-er**: `IComparable`
(kan jämföras), `IDisposable` (kan städas bort), `IApprover` (kan godkänna). Läs namnet som meningen
"den här klassen *kan* ...".

Är du osäker på ett namn: fråga dig om en kollega kan gissa vad det gör utan att öppna filen.

---

## 🟢 Uppgift 1: abstract och CalculatePay

> **Vad:** en `abstract class` är en ritning som aldrig blir ett objekt.
> **När:** när alla barn är *samma sorts sak*, delar fält, men räknar eller beter sig olika. "Anställd" i allmänhet får ingen lön. Det får en chef, en konsult eller en vikarie.
> **Hur:** `abstract class Employee` och `public abstract int CalculatePay();`. Barnen skriver `override`.

1. Skapa `abstract class Employee` i `Abstracts/Employee.cs` med fälten `Name` (`string`), `HoursWorked` (`int`) och `IsApproved` (`bool`, börjar som `false`). Skriv en konstruktor som tar emot `name` och `hoursWorked`.
2. Lägg till `public abstract int CalculatePay();`.
3. Skapa `Manager` och `Consultant` i `Models/` (en fil var). De ärver från `Employee` och skriver över `CalculatePay()`:
   - `Manager`: fast månadslön, **55000** kr, oavsett antal timmar.
   - `Consultant`: **950** kr per timme. Lön = `HoursWorked * 950`.
4. I `Program.cs`: skapa en lista `staff` med `Anna` (chef, 160 timmar) och `Pelle` (konsult, 120 timmar) och skriv ut lönen för var och en.

Prova också `new Employee("???", 0);`. Vad säger kompilatorn? Ta bort raden igen.

**Förväntad output:**
```
Anna får 55000 kr.
Pelle får 114000 kr.
```

<details markdown="1">
<summary>Ledtråd</summary>

Konstruktorn i en barnklass kan skrivas på två sätt. Båda gör exakt samma sak.

**Det vanliga sättet** (som i övning 3 och 4):

```cs
public Manager(string name, int hoursWorked) : base(name, hoursWorked) { }
```

**Det moderna sättet** (kallas *primary constructor*, finns från C# 12). Parametrarna står direkt
efter klassnamnet, och basklassen får dem på samma rad:

```cs
class Manager(string name, int hoursWorked) : Employee(name, hoursWorked), IApprover
{
    // ...
}
```

Prova det moderna sättet i `Manager`, så ser ni skillnaden. Ni får välja själva i de andra klasserna.
Basklassen `Employee` behåller sin vanliga konstruktor.

Glömmer en barnklass `CalculatePay()` får ni ett kompileringsfel. Det är meningen.

</details>

## 🟢 Uppgift 2: lägg till en vikarie

> **Vad:** en ny sorts anställd, utan att ändra de gamla klasserna.
> **När:** när verksamheten får en ny roll. Det är en av anledningarna till att man har en basklass.
> **Hur:** skriv en ny klass som ärver från `Employee` och lägg objektet i listan.

1. Skapa `Substitute` i `Models/Substitute.cs`. Den ärver från `Employee`. Lön = `HoursWorked * 180`.
2. Lägg till `Sara` (vikarie, 80 timmar) i listan.
3. Kör programmet igen. Behövde `Manager`, `Consultant` eller loopen ändras?

**Förväntad output:**
```
Anna får 55000 kr.
Pelle får 114000 kr.
Sara får 14400 kr.
```

<details markdown="1">
<summary>Ledtråd</summary>

Samma mönster som `Consultant`, men med en annan timpenning. Loopen över `staff` är oförändrad.

</details>

## 🟢 Uppgift 3: IApprover och chefen

> **Vad:** ett `interface` är ett löfte om vad en klass *kan göra*.
> **När:** när en förmåga delas av en del, men inte alla. Chefer kan godkänna tidrapporter. Konsulter och vikarier kan inte det.
> **Hur:** `interface IApprover { void Approve(Employee target); }`. Klassen skriver `: Employee, IApprover` och bestämmer själv hur den godkänner.

1. Skapa `interface IApprover` i `Interfaces/IApprover.cs` med metoden `void Approve(Employee target);`.
2. Låt `Manager` använda interfacet: `class Manager : Employee, IApprover`.
3. Implementera `Approve`: sätt målets `IsApproved` till `true` och skriv `Anna godkänner Pelles tidrapport.` (med rätt namn).

<details markdown="1">
<summary>Ledtråd</summary>

Basklassen står först, sedan interfacet. Metoden måste vara `public`. Namnet blir `{Name}` för den som godkänner och `{target.Name}` för den som godkänns.

</details>

## 🟡 Uppgift 4: filtrera ut dem som kan godkänna

> **Vad:** `OfType<T>()` plockar ur en lista det som är av typen `T`.
> **När:** när listan innehåller blandade roller och du bara vill åt dem som kan en viss sak.
> **Hur:** `staff.OfType<IApprover>()`. Det fungerar på ett interface och på en basklass.

1. Använd `OfType<IApprover>()` i en `foreach` för att hitta alla som kan godkänna.
2. Låt var och en godkänna den andra i listan, `staff[1]` (Pelle).
3. Skriv ut om Pelles tidrapport är godkänd.

**Förväntad output (efter lönerna):**
```
Anna godkänner Pelles tidrapport.
Pelle: godkänd = True
```

<details markdown="1">
<summary>Ledtråd</summary>

```cs
foreach (IApprover approver in staff.OfType<IApprover>())
{
    approver.Approve(staff[1]);
}
```

Konsulten och vikarien hoppas över, för de har inte lovat att kunna godkänna.

</details>

## 🟡 Uppgift 5: abstract eller interface?

Samma fråga som på sista bilden i lektionen. Nu får ni svara med personalsystemet.

Sortera in varje rad under **abstract class** (är en sorts) eller **interface** (kan göra):

| | abstract class eller interface? |
|---|---|
| a | Att kunna **godkänna tidrapporter** (chefer kan, konsulter och vikarier kan inte) |
| b | **Personal** (chefen, konsulten och vikarien är alla en sorts) |
| c | Att kunna **hålla en kundpresentation** (chefer och konsulter kan, vikarier gör det inte) |
| d | **Externa leverantörer** (ett bemanningsbolag och en frilansare är båda en sorts) |

Testa svaret med meningen: *"En chef **är en sorts** ___"* eller *"En chef **kan** ___"*. Den som passar bäst visar vägen.

<details markdown="1">
<summary>Svar</summary>

- a: **interface** (`IApprover`). Förmågan delas av en del, men inte alla.
- b: **abstract class** (`Employee`). Alla är samma sorts sak och delar fält.
- c: **interface** (`IPresenter`). Roller som inte är samma sorts sak kan dela förmågan.
- d: **abstract class** (`ExternalSupplier`). Samma sorts sak.

Fastnade ni? Fråga er om det är en *sorts* eller en *förmåga*.

</details>

---

## Prata med din partner

Titta på `Employee` och `IApprover` i er kod. Varför blev det en `abstract class` på ena stället och ett `interface` på det andra?
Svara med "är en sorts" och "kan göra".

---

## Avsluta: byt kod och ge en gåva 🎁

Byt med ett annat par. Ni läser varandras kod och ger varandra återkoppling. Ta ca 10 minuter, fem åt varje håll.

1. Öppna det andra parets projekt. Titta på mapparna och filerna först, läs sedan koden.
2. **Testkör programmet.** Skriver det ut samma sak som den förväntade outputen i uppgifterna?
3. **Leta efter faror.** Fundera på vad som händer när programmet inte får det det förväntar sig.
   Prova gärna i en **kopia** av projektet, så att originalet är kvar. Några frågor att utgå från:
   - Listan har bara en person och koden skriver `staff[1]`. Vad händer?
   - `Approve` får `null` i stället för en anställd. Vad händer?
   - `HoursWorked` är negativ, eller noll. Vilken lön räknas ut?
   - Chefen godkänner sin egen tidrapport. Ska det vara möjligt?
   - Programmet läste in timmar med `Console.ReadLine()`, och användaren skrev text i stället för en siffra. Vad skulle hända med `int.Parse("abc")`?

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
