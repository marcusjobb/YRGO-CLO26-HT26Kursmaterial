# Läxa A: Hjältens äventyr, kapitel 1

**Till:** tisdag 3 november · **Tid:** ca 3–4 timmar för 🟢 + 🟡 · **Gör den:** ensam

## Berättelsen

Mira bor i en liten by vid en bro. Ända sedan morgonen har det hörts ett dovt
mullrande från andra sidan floden. Byborna tittar på Mira. Hon är den enda som
har ett svärd.

I kapitel 1 samlar hon ett gäng, möter monstret Gruff vid bron och får hjälp av
trollkvinnan Zelda. Koden är verktyget som löser det åt henne.

Allt du behöver har du sett i går. Du har inte bråttom, och du behöver inte göra
allt.

## Så här fungerar läxan

| Märke | Betyder | Hur många |
|-------|---------|-----------|
| 🟢 Obligatorisk | Alla ska klara dessa | 2 per del |
| 🟡 Mer övning | De flesta klarar dessa | 2 per del |
| 🥷 Ninja | Frivillig utmaning | 2 per del |

- Fyra delar: **klass och objekt**, **arv**, **abstract**, **interface**. Totalt 24 uppgifter.
- Skapa ett **nytt konsolprojekt per del** (`Del1`, `Del2`, `Del3`, `Del4`), så att klasserna inte krockar.
- Lägg klasserna i egna filer (t.ex. `Hero.cs`) eller längst ned i `Program.cs`, under koden i `Main`. Står de ovanför får du felet CS8803.
- Skriv koden själv. Kopiera inte från föreläsningsbilderna. Då lär du dig mer.
- Förväntad output visar de **nya raderna** för uppgiften. Kommentera bort tidigare utskrifter med `//` om du vill se just de nya.
- Identifierare (variabler, metoder, klasser) skrivs på engelska. Utskrifterna är på svenska.
- Fastnar du: öppna ledtråden. Fastnar du fortfarande: skriv ner frågan och ta med den till fredag.

---

# Del 1: Klass och objekt

*Mira samlar ihop ett gäng hjältar. Först behöver hon en ritning.*

### Uppgift 1.1 🟢 Skapa ritningen

1. Skapa klassen `Hero` med fälten `Name` (`string`) och `Health` (`int`).
2. Ge klassen en konstruktor som tar emot båda värdena.
3. Skapa ett objekt: Mira med 100 i hälsa. Skriv ut namn och hälsa.

**Förväntad output:**
```
Mira har 100 hälsa.
```

<details markdown="1">
<summary>Ledtråd</summary>

Konstruktorn har samma namn som klassen och ingen returtyp. Inuti den skriver du
`Name = name;` och `Health = health;`.

</details>

### Uppgift 1.2 🟢 Hälsa på varandra

1. Lägg till metoden `Greet()` i `Hero`. Den skriver ut `Hej, jag heter {Name}!`.
2. Skapa Tobbe med 80 i hälsa.
3. Låt både Mira och Tobbe hälsa.

**Förväntad output:**
```
Hej, jag heter Mira!
Hej, jag heter Tobbe!
```

<details markdown="1">
<summary>Ledtråd</summary>

Du anropar metoden på objektet: `mira.Greet();`. Varje objekt använder sitt eget `Name`.

</details>

### Uppgift 1.3 🟡 Ta skada

1. Lägg till metoden `TakeDamage(int amount)` som drar `amount` från `Health`.
2. Hälsan får aldrig bli lägre än 0.
3. Låt Mira ta 30 skada och Tobbe ta 200 skada. Skriv ut hälsan för båda.

**Förväntad output:**
```
Mira har 70 hälsa kvar.
Tobbe har 0 hälsa kvar.
```

<details markdown="1">
<summary>Ledtråd</summary>

Efter att du dragit av skadan: `if (Health < 0) { Health = 0; }`.

</details>

### Uppgift 1.4 🟡 Hela gänget

Gör den här uppgiften efter 1.3, så att Miras och Tobbes hälsa redan är ändrad.

1. Lägg Mira, Tobbe och en ny hjälte, Sigrid med 120 i hälsa, i en `List<Hero>`.
2. Gå igenom listan med `foreach` och skriv ut varje namn.
3. Räkna ihop gängets totala hälsa med en variabel som du ökar i loopen.

**Förväntad output:**
```
Hjälte: Mira
Hjälte: Tobbe
Hjälte: Sigrid
Total hälsa: 190
```

<details markdown="1">
<summary>Ledtråd</summary>

Skapa `int total = 0;` före loopen. I loopen: `total += hero.Health;`.

</details>

### Uppgift 1.5 🥷 Lever eller slagen?

1. Lägg till en egenskap `IsAlive` i `Hero` som är `true` när `Health` är större än 0. Du skriver en `get`-del och ingen `set`.
2. Gå igenom listan från 1.4 och skriv ut om varje hjälte lever eller är slagen.

**Förväntad output:**
```
Mira: lever
Tobbe: är slagen
Sigrid: lever
```

<details markdown="1">
<summary>Ledtråd</summary>

```csharp
public bool IsAlive
{
    get { return Health > 0; }
}
```

För utskriften kan du använda `hero.IsAlive ? "lever" : "är slagen"`.

</details>

### Uppgift 1.6 🥷 Hur många hjältar finns det?

1. Lägg till ett `static`-fält `Count` i `Hero`. Det hör till klassen, inte till ett enskilt objekt.
2. Öka det med 1 i konstruktorn.
3. Skriv ut `Hero.Count` efter att alla hjältar är skapade.

**Förväntad output:**
```
Antal hjältar: 3
```

<details markdown="1">
<summary>Ledtråd</summary>

`public static int Count;` och i konstruktorn `Count++;`. Du anropar det med klassnamnet, inte med ett objekt.

</details>

---

# Del 2: Arv

*Gruff dyker upp vid bron. Han har också `Name` och `Health`. Samma kod två gånger? Nej.*

Skapa ett nytt projekt. `Hero` ändras nu så att den ärver från en gemensam klass.

### Uppgift 2.1 🟢 En gemensam bas

1. Skapa klassen `Character` med `Name`, `Health` och en konstruktor.
2. Skapa `Hero : Character` och `Monster : Character`. De skickar vidare till basklassen med `: base(name, health)`.
3. Skapa Mira (`Hero`, 100) och Gruff (`Monster`, 60). Skriv ut namn och hälsa för båda.

**Förväntad output:**
```
Mira (100)
Gruff (60)
```

<details markdown="1">
<summary>Ledtråd</summary>

```csharp
class Hero : Character
{
    public Hero(string name, int health) : base(name, health) { }
}
```

</details>

### Uppgift 2.2 🟢 Olika röster

1. Lägg till `public virtual string Speak()` i `Character`. Den returnerar `"..."`.
2. Låt `Hero` skriva över den med `override` och returnera `"Jag räddar dagen!"`.
3. Låt `Monster` returnera `"GRRR!"`.
4. Skriv ut vad Mira och Gruff säger.

**Förväntad output:**
```
Mira: Jag räddar dagen!
Gruff: GRRR!
```

<details markdown="1">
<summary>Ledtråd</summary>

`virtual` i basklassen betyder "barnen får ändra". `override` i barnet betyder "jag ändrar".

</details>

### Uppgift 2.3 🟡 Zelda och listan

1. Skapa `Wizard : Character` som säger `"Abrakadabra!"`.
2. Skapa Zelda (70 i hälsa).
3. Lägg Mira, Gruff och Zelda i en `List<Character>`.
4. Gå igenom listan och skriv ut `{Name}: {Speak()}` för alla.

**Förväntad output:**
```
Mira: Jag räddar dagen!
Gruff: GRRR!
Zelda: Abrakadabra!
```

<details markdown="1">
<summary>Ledtråd</summary>

Listan har typen `Character`, men rätt `Speak()` körs ändå för varje objekt.

</details>

### Uppgift 2.4 🟡 Skriv ut ett objekt snyggt

1. Skriv över `ToString()` i `Character` så att den returnerar `{Name} ({Health})`.
2. Gå igenom listan från 2.3 och skriv ut varje objekt direkt: `Console.WriteLine(c);`.

**Förväntad output:**
```
Mira (100)
Gruff (60)
Zelda (70)
```

<details markdown="1">
<summary>Ledtråd</summary>

Alla klasser i C# ärver redan `ToString()`. Du skriver `public override string ToString()`.

</details>

### Uppgift 2.5 🥷 Draken

1. Skapa `Dragon : Monster`.
2. Låt den skriva över `Speak()`, men börja med att anropa basens version: `base.Speak()`. Lägg sedan till `" ELD!"`.
3. Skapa draken Eldor (200) och skriv ut vad han säger.

**Förväntad output:**
```
Eldor: GRRR! ELD!
```

<details markdown="1">
<summary>Ledtråd</summary>

`return base.Speak() + " ELD!";`. `base` betyder "föräldern".

</details>

### Uppgift 2.6 🥷 Golemen som tål mer

1. Flytta in `TakeDamage` i `Character` som en `virtual`-metod (hälsan får inte bli lägre än 0).
2. Skapa `Golem : Monster`. Den ska ta hälften så mycket skada: skriv över `TakeDamage` och anropa `base.TakeDamage(amount / 2)`.
3. Skapa Stenis (100), låt honom ta 40 skada och skriv ut honom.

**Förväntad output:**
```
Stenis (80)
```

<details markdown="1">
<summary>Ledtråd</summary>

```csharp
public override void TakeDamage(int amount)
{
    base.TakeDamage(amount / 2);
}
```

</details>

---

# Del 3: Abstract

*Striden vid bron börjar. Alla karaktärer ska kunna attackera, men "en karaktär i allmänhet" finns inte i spelet.*

Skapa ett nytt projekt. Startkoden är `Character` med `Name`, `Health` och en konstruktor, samt `Hero`, `Monster` och `Wizard` som ärver från den (se del 2).

### Uppgift 3.1 🟢 En ritning som inte får bli ett hus

1. Gör `Character` till `abstract class`.
2. Skriv `Character c = new Character("X", 10);` i `Main`. Bygg projektet och läs felet.
3. Ta bort raden igen.

**Förväntat felmeddelande:**
```
error CS0144: Cannot create an instance of the abstract type or interface 'Character'
```

<details markdown="1">
<summary>Ledtråd</summary>

Felet är meningen. Det visar att kompilatorn skyddar dig mot att skapa något som inte
ska finnas. Du kan fortfarande skapa `Hero`, `Monster` och `Wizard`.

</details>

### Uppgift 3.2 🟢 Ett krav på alla barn

1. Lägg till `public abstract string Attack();` i `Character`. Metoden har ingen kropp.
2. Bygg projektet. Läs felen: vem saknar vad?
3. Implementera `Attack()` med `override`:
   - `Hero`: `{Name} svingar svärdet!`
   - `Monster`: `{Name} biter!`
   - `Wizard`: `{Name} kastar en gnista!`
4. Lägg Mira (100), Gruff (60) och Zelda (70) i en `List<Character>` och skriv ut allas attack.

**Förväntad output:**
```
Mira svingar svärdet!
Gruff biter!
Zelda kastar en gnista!
```

<details markdown="1">
<summary>Ledtråd</summary>

En abstrakt metod är ett krav. Varje klass som ärver måste fylla i den, annars bygger inte projektet.

</details>

### Uppgift 3.3 🟡 Hjältarna anfaller

1. Skapa Tobbe (`Hero`, 80) och lägg till honom i listan.
2. Skriv `Hjältarna anfaller:` och sedan attacken för **enbart** hjältarna. Använd `OfType<Hero>()`.

**Förväntad output:**
```
Hjältarna anfaller:
Mira svingar svärdet!
Tobbe svingar svärdet!
```

<details markdown="1">
<summary>Ledtråd</summary>

```csharp
foreach (Hero h in scene.OfType<Hero>())
{
    Console.WriteLine(h.Attack());
}
```

`OfType<T>()` plockar ut det som passar typen `T` och hoppar över resten. `System.Linq` finns med som standard i .NET 10, så du behöver ingen extra `using`.

</details>

### Uppgift 3.4 🟡 Hur mycket skada?

1. Lägg till `public abstract int GetDamage();` i `Character`. Hjältar gör 15, monster 10, trollkvinnor 25.
2. Skapa ett till monster, Snurf (40), och lägg till honom i listan.
3. Skriv ut `{Name} gör {GetDamage()} skada` för alla i listan.
4. Räkna ihop skadan för **enbart monstren** med `OfType<Monster>()` och skriv ut summan.

**Förväntad output:**
```
Mira gör 15 skada
Gruff gör 10 skada
Zelda gör 25 skada
Tobbe gör 15 skada
Snurf gör 10 skada
Alla monster tillsammans: 20
```

<details markdown="1">
<summary>Ledtråd</summary>

Summera med en variabel, precis som i del 1: `damage += m.GetDamage();` inuti en `foreach` över `scene.OfType<Monster>()`.

</details>

### Uppgift 3.5 🥷 Kämpa

1. Lägg till `TakeDamage(int amount)` i `Character` (hälsan får inte bli lägre än 0).
2. Lägg till en vanlig metod `Fight(Character target)` i `Character`. Den skriver ut sin egen `Attack()`, låter `target` ta `GetDamage()` i skada och skriver ut `{target.Name} har {target.Health} hälsa kvar.`.
3. Låt Mira kämpa mot Gruff.

**Förväntad output:**
```
Mira svingar svärdet!
Gruff har 45 hälsa kvar.
```

<details markdown="1">
<summary>Ledtråd</summary>

`Fight` är vanlig kod som anropar de abstrakta metoderna `Attack()` och `GetDamage()`. Basklassen vet inte vad de gör. Den litar på att barnen vet.

</details>

### Uppgift 3.6 🥷 Två sorters monster

1. Gör `Monster` till en `abstract class`. Den ska inte längre implementera `Attack()`, men får gärna implementera `GetDamage()`.
2. Skapa `Goblin : Monster` (`{Name} hugger med en liten kniv!`) och `Troll : Monster` (`{Name} slår med klubban!`).
3. Byt ut Gruff mot en `Troll` och Snurf mot en `Goblin`.
4. Skriv ut attacken för alla monster i listan med `OfType<Monster>()`.

**Förväntad output:**
```
Gruff slår med klubban!
Snurf hugger med en liten kniv!
```

<details markdown="1">
<summary>Ledtråd</summary>

En abstrakt klass kan ärva från en annan abstrakt klass och lämna delar öppna. Först de konkreta barnen (`Goblin`, `Troll`) måste fylla i allt.

</details>

---

# Del 4: Interface

*Gruff är starkare än väntat. Mira behöver hjälp. Zelda kan läka, och ibland flyga. Det är inget man *är*, det är något man *kan*.*

Skapa ett nytt projekt. Startkod:

```csharp
abstract class Character
{
    public string Name;
    public int Health;

    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public abstract string Attack();
}

class Hero : Character
{
    public Hero(string name, int health) : base(name, health) { }
    public override string Attack() { return $"{Name} svingar svärdet!"; }
}

class Monster : Character
{
    public Monster(string name, int health) : base(name, health) { }
    public override string Attack() { return $"{Name} biter!"; }
}
```

### Uppgift 4.1 🟢 Förmågan att läka

1. Skapa `interface IHealer` med metoden `void Heal(Character target);`.
2. Skapa `Wizard : Character, IHealer`. Attacken är `{Name} kastar en gnista!`. `Heal` ökar `target.Health` med 20 och skriver ut `{Name} läker {target.Name}.`.
3. Skapa Mira (`Hero`, 70) och Zelda (`Wizard`, 70). Låt Zelda läka Mira och skriv ut Miras hälsa.

**Förväntad output:**
```
Zelda läker Mira.
Mira har 90 hälsa.
```

<details markdown="1">
<summary>Ledtråd</summary>

Efter klassens basklass kommer interfacen, med komma emellan: `class Wizard : Character, IHealer`.
Metoden i interfacet ska vara `public` i klassen.

</details>

### Uppgift 4.2 🟢 Förmågan att flyga

1. Skapa `interface IFlyer` med metoden `string Fly();`.
2. Skapa `Dragon : Monster, IFlyer`. `Fly()` returnerar `{Name} flyger iväg!`.
3. Skapa draken Eldor (200) och skriv ut vad `Fly()` returnerar.

**Förväntad output:**
```
Eldor flyger iväg!
```

<details markdown="1">
<summary>Ledtråd</summary>

`Dragon` ärver `Attack()` från `Monster`. Du lägger till det som interfacet kräver.

</details>

### Uppgift 4.3 🟡 Alla som kan läka

1. Skapa `Priest : Character, IHealer` (Attack: `{Name} läser en bön!`). `Heal` ökar hälsan med 30 och skriver ut `{Name} läker {target.Name}.`.
2. Skapa Tilda (`Hero`, 50) och Bror (`Priest`, 60).
3. Lägg Zelda, Bror, Tilda och Eldor i en `List<Character>`.
4. Låt **alla som kan läka** läka Tilda. Använd `OfType<IHealer>()`. Skriv sedan ut Tildas hälsa.

**Förväntad output:**
```
Zelda läker Tilda.
Bror läker Tilda.
Tilda har 100 hälsa.
```

<details markdown="1">
<summary>Ledtråd</summary>

```csharp
foreach (IHealer healer in scene.OfType<IHealer>())
{
    healer.Heal(tilda);
}
```

Tilda och Eldor hoppas över, eftersom de inte kan läka.

</details>

### Uppgift 4.4 🟡 Två förmågor i en klass

1. Låt `Wizard` också implementera `IFlyer`. `Fly()` returnerar `{Name} svävar på sin kvast!`.
2. Gå igenom listan med `OfType<IFlyer>()` och skriv ut vad varje flygare säger.

**Förväntad output:**
```
Zelda svävar på sin kvast!
Eldor flyger iväg!
```

<details markdown="1">
<summary>Ledtråd</summary>

En klass kan ärva från en enda basklass, men implementera hur många interface som helst: `class Wizard : Character, IHealer, IFlyer`.

</details>

### Uppgift 4.5 🥷 Räkna typer

1. Skriv en generisk metod `static int CountOf<T>(List<Character> scene)` som returnerar hur många objekt i listan som är av typen `T`.
2. Använd den fyra gånger, med `Hero`, `Monster`, `IHealer` och `IFlyer`.

**Förväntad output:**
```
Hjältar: 1
Monster: 1
Läkare: 2
Flygare: 2
```

<details markdown="1">
<summary>Ledtråd</summary>

`return scene.OfType<T>().Count();`. Du anropar den så här: `CountOf<Hero>(scene)`.

</details>

### Uppgift 4.6 🥷 En loop, tre frågor

1. Gå igenom listan med en enda `foreach` över `Character`.
2. Inuti loopen, ställ tre frågor med `is`, en efter en:
   - `c is Hero`: skriv ut `{Name} är en hjälte.`
   - `c is IHealer healer`: låt den läka Tilda.
   - `c is IFlyer flyer`: skriv ut `flyer.Fly()`.

**Förväntad output:**
```
Zelda läker Tilda.
Zelda svävar på sin kvast!
Bror läker Tilda.
Tilda är en hjälte.
Eldor flyger iväg!
```

<details markdown="1">
<summary>Ledtråd</summary>

`if (c is IHealer healer) { healer.Heal(tilda); }` kollar typen och ger dig variabeln `healer` i samma steg. Zelda svarar ja på två frågor, Tilda på en.

</details>

---

## Till fredagens handledning

Ta med ett svar eller en fråga på var och en:

1. **När väljer du `abstract` och när `interface`?** Förklara med egna ord och ett exempel från Miras värld.
2. Vilken uppgift gjorde dig mest osäker, och vad var det som var oklart?

Du har kommit långt. Det du inte hann är inget misslyckande. Det är fredagens fråga.
