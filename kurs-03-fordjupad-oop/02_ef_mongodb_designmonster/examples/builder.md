# Builder: exempel med förklaringar

Projekten ligger i `source/repos`: `Designmonster_Builder`, `Designmonster_Builder_Karaktar` och `Designmonster_Builder_Titel`.

**Idén:** samla värden steg för steg, och skapa objektet först i slutet med `Build()`. Frågor för när mönstret passar finns i [flödesschemat](../notes/flodesschema_designmonster.md#builder).

---

## 1. `Designmonster_Builder` (allt i en fil)

Det här projektet har alla tre uppgifterna i samma `Program.cs`. Det är bra att läsa först, eftersom resten bara delar upp koden.

### 1.1 Uppgift 1: Mira, en mening i taget

```csharp
Character mira = new CharacterBuilder().Named("Mira").IsA(CharacterKind.Hero).Build();
Console.WriteLine($"{mira.Name} är en {mira.Kind}");
```

**Vad händer:** Mira får namn och art. Allt annat blir standardvärde, eftersom builderns fält har startvärden.

**Stil:** Hela uttrycket står på en rad, eftersom det är kort. Läs det vänster till höger, som en mening.

### 1.2 Uppgift 2: Grölf och Nisse

```csharp
Character grolf = new CharacterBuilder()
    .Named("Grölf")
    .IsEvil()
    .IsA(CharacterKind.Ogre)
    .Speaks("aggghhh")
    .Likes("to mame")
    .WeaponOfChoice("Club")
    .Build();

Character nisse = new CharacterBuilder().Named("Nisse").Build();
```

**Vad händer:** Grölf får alla egenskaper. Nisse får bara namnet, och resten blir standardvärden. Ordningen mellan stegen spelar ingen roll, och `IsEvil()` tar inga argument, eftersom det bara är ett "ja".

**Stil:** När kedjan är lång lägger man en metod per rad. Det går att läsa utan att räkna parametrar. Nisse står på en rad, eftersom kedjan är kort.

### 1.3 Uppgift 3: titeln syns bara när den finns

```csharp
Character miraTitled = new CharacterBuilder()
    .Named("Mira")
    .IsA(CharacterKind.Hero)
    .Titled("Drakslayer")
    .Build();

Console.WriteLine(miraTitled);
Console.WriteLine(nisse);
```

**Vad händer:** Mira med titel skrivs ut med titeln i parentes. Nisse skrivs ut utan titel, eftersom hans titel är tom. Den skillnaden hanterar `Character.ToString()`, inte byggaren.

**Stil:** Samma `nisse` används två gånger. Ett objekt som är byggt kan läsas flera gånger utan att något ändras.

### 1.4 Klassen `Character`

```csharp
class Character
{
    public string Name { get; }
    public CharacterKind Kind { get; }
    public bool IsEvil { get; }
    public string Speech { get; }
    public string Likes { get; }
    public string WeaponOfChoice { get; }
    public string Title { get; }

    public Character(string name, CharacterKind kind, bool isEvil, string speech,
                     string likes, string weaponOfChoice, string title)
    {
        Name = name;
        Kind = kind;
        IsEvil = isEvil;
        Speech = speech;
        Likes = likes;
        WeaponOfChoice = weaponOfChoice;
        Title = title;
    }

    public override string ToString()
    {
        string side = IsEvil ? "ond" : "god";
        string title = Title != "" ? $" (\"{Title}\")" : "";
        return $"{Name}{title} är en {side} {Kind}. Säger \"{Speech}\", gillar {Likes} och slåss med {WeaponOfChoice}.";
    }
}
```

**Vad händer:** Alla värden sätts i konstruktorn, och det finns inga set-metoder. `ToString()` bygger en mening av värdena. Titeln är en tom sträng när den saknas, och då skrivs den inte ut.

**Stil:**
- `{ get; }` gör propertyn skrivskyddad efter konstruktorn. Ingen kan ändra en karaktär i efterhand.
- Konstruktorns parametrar har samma namn som propertyerna. Det gör det lätt att se vilket värde som går vart.
- `? :` i en interpolerad sträng är kort och fungerar bra för en enkel jämförelse. Vid mer logik bryt ut det till en egen variabel.

### 1.5 Klassen `CharacterBuilder`

```csharp
class CharacterBuilder
{
    private string name = "Okänd";
    private CharacterKind kind = CharacterKind.Hero;
    private bool isEvil = false;
    private string speech = "";
    private string likes = "";
    private string weaponOfChoice = "käpp";
    private string title = "";

    public CharacterBuilder Named(string name)
    {
        this.name = name;
        return this;
    }

    public CharacterBuilder IsEvil()
    {
        this.isEvil = true;
        return this;
    }

    // ...de övriga stegen följer samma mönster

    public Character Build()
    {
        return new Character(name, kind, isEvil, speech, likes, weaponOfChoice, title);
    }
}
```

**Vad händer:** Varje steg ändrar ett fält och returnerar sig själv. `Build()` är det enda stället där `new Character(...)` anropas. Fram till dess är allt bara förberedelser.

**Stil:**
- Startvärden på fälten: en karaktär blir alltid komplett, även om man glömmer ett steg.
- `return this;` är det som gör kedjan möjlig. Utan det finns inget objekt kvar att anropa nästa metod på.
- `this.` används när parameternamnet är samma som fältet, som i `this.name = name`. Det är en konvention. Det behövs inte när namnen skiljer sig.
- Metodnamnen (`Named`, `IsA`, `IsEvil`) är skrivna som meningar. Det är poängen med mönstret.

---

## 2. `Designmonster_Builder_Karaktar` (separata mappar och namespace)

Samma idé, men uppdelad i mappar: `Models/`, `Builders/`, `Demos/` och `Program.cs`.

### 2.1 `Program.cs`

```csharp
using Designmonster.Builder.Demos;

// Övning: Builder, del 1 (karaktärer)
CharacterDemo.RunMira();           // Uppgift 1
CharacterDemo.RunGrolfAndNisse();  // Uppgift 2
```

**Vad händer:** `Program.cs` anropar bara demoklasser. Själva koden för varje uppgift ligger i `Demos/CharacterDemo.cs`.

**Stil:** Huvudprogrammet blir en innehållsförteckning. Kommentarerna pekar ut vilken uppgift som körs. Det gör det lätt att hitta rätt sak senare.

### 2.2 Namespace med filscope

```csharp
namespace Designmonster.Builder.Models;

public class Character
{
    // ...
}
```

**Stil:** `namespace X;` utan klammerparenteser, som kallas file-scoped namespace. Det sparar en indrag per fil. Klasserna i de här mapparna använder det, medan `Designmonster_Builder` har alla klasser utan namespace.

### 2.3 `Character` med XML-kommentar

```csharp
/// <summary>En karaktär. Alla värden sätts i konstruktorn, och det finns inga setters.</summary>
public class Character
{
    public string Name { get; }
    public CharacterKind Kind { get; }
    // ...
}
```

**Vad händer:** Samma idé som i det första projektet, men utan `Title`. `Titel` är en egen övning.

**Stil:** `/// <summary>` är en XML-kommentar. Visual Studio visar den när man håller muspekaren över klassen, så den som använder klassen ser beskrivningen utan att öppna filen.

### 2.4 `CharacterBuilder` med egna standardvärden

```csharp
public class CharacterBuilder
{
    private string name = "Okänd";
    private CharacterKind kind = CharacterKind.Hero;
    private bool isEvil = false;
    private string speech = "Hullo";
    private string likes = "Bacon";
    private string weaponOfChoice = "käpp";

    public CharacterBuilder IsEvil()
    {
        isEvil = true;
        return this;
    }

    // ...

    public Character Build() =>
        new(name, kind, isEvil, speech, likes, weaponOfChoice);
}
```

**Vad händer:** Nisse säger "Hullo" och gillar "Bacon", eftersom det är builderns standardvärden. I `Designmonster_Builder` var de tomma strängar.

**Stil:**
- Standardvärdena är en designfråga. Det finns inget rätt svar, men valet syns i utskriften.
- `IsEvil()` skriver `isEvil = true;` utan `this.`. I föregående projekt används `this.`. Båda fungerar, men det är bra att välja en stil och hålla fast vid den.
- `=> new(...)`: target-typed new. Kompilatorn vet att det är en `Character`, så typnamnet behövs inte två gånger.

---

## 3. `Designmonster_Builder_Titel` (uppgift 3)

Det här projektet lägger bara till titeln. Det visar hur man bygger vidare på en byggare utan att ändra stegen som redan finns.

### 3.1 Metoden `Titled`

```csharp
public CharacterBuilder Titled(string title)
{
    this.title = title;
    return this;
}
```

**Vad händer:** Lägger till ett nytt steg. Byggaren och `Build()` ändras inte, bara fältet och metoden.

**Stil:** Samma mönster som de övriga stegen. När man lägger till ett steg ska det se ut som de andra. Då syns det vilka steg som finns.

### 3.2 `TitleDemo`

```csharp
public static void Run()
{
    Character mira = new CharacterBuilder()
        .Named("Mira")
        .IsA(CharacterKind.Hero)
        .Titled("Drakslayer")
        .Build();

    Character nisse = new CharacterBuilder().Named("Nisse").Build();

    Console.WriteLine(mira);
    Console.WriteLine(nisse);
}
```

**Vad händer:** Två karaktärer byggs på samma sätt, en med titel och en utan. Titelraden syns bara för Mira.

**Stil:** Demoklassen har en `Run()`-metod, och en XML-kommentar som säger vilken uppgift den hör till.
