---
marp: true
theme: nion-dark
paginate: true
---

# Builder: en karaktär, en mening i taget

Designmönster · Fördjupad OOP i C#
_Marcus absoluta favorit designmönster_

---

## Det var en gång en hjälte, Mira

Hon skulle ut i världen. Men först behövde hon bli till.

```csharp
Character mira = new Character("Mira", CharacterKind.Hero, false, "Ta fram svärdet!", "drakar som inte bråkar", "svärd");
```

Vad betyder `false`? Vilket av de tre sista orden är vad?

Ingen i byn vet utan att gå till trollkarlens bok.

---

## Sagan växer

Nu ska även ogren Grölf finnas med. Han är ond, han säger bara "aggghhh", och han slåss med en klubba.

```csharp
Character grolf = new Character("Grölf", CharacterKind.Ogre, true, "aggghhh", "to mame", "Club");
```

Och varje ny egenskap blir en ny plats i formeln. Det blir fort svårt att läsa.

---

## Trollkarlen blandar ihop orden

```csharp
Character grolf = new Character("Grölf", CharacterKind.Ogre, true, "to mame", "aggghhh", "Club");
```

Kompilatorn säger ingenting. Både `"to mame"` och `"aggghhh"` är text, så formeln går igenom.
Grölf säger nu "to mame" och gillar "aggghhh". Ingen märker det förrän någon läser utskriften.

---

## Builder: en karaktär, en mening i taget

```csharp
Character grolf = new CharacterBuilder()
    .Named("Grölf")
    .IsEvil()
    .IsA(CharacterKind.Ogre)
    .Speaks("aggghhh")
    .Likes("to mame")
    .WeaponOfChoice("Club")
    .Build();
```

Läs det som en mening: *Grölf är ond, han är en ogre, han säger aggghhh, han gillar att mame och hans vapen är en Club.*

---

## Hur det fungerar

- Varje metod sparar ett värde och returnerar byggaren själv.
- `.IsEvil()` tar inga parametrar. Det är en egenskap som bara sätts på.
- `Build()` skapar karaktären en gång, när alla meningar är sagda.
- Utelämnar du en mening får karaktären standardvärdet. En karaktär utan `.IsEvil()` är god.

---

## Varför kan vi kedja?

Du har redan kedjat metoder:

```csharp
string svar = Console.ReadLine().ToLower().Trim();
```

Varje metod returnerar en `string`, och nästa metod kan anropas på den.

---

## Samma idé i Builder

```csharp
public CharacterBuilder Speaks(string speech)
{
    this.speech = speech;
    return this;   // byggaren själv, så nästa punkt kan anropas
}
```

Är `Speaks` i stället `void` bryts kedjan: det finns inget kvar att anropa `.Likes(...)` på.

---

## Byggaren i C#

```csharp
class CharacterBuilder
{
    private string name = "Okänd";
    private CharacterKind kind = CharacterKind.Hero;
    private bool isEvil = false;
    private string speech = "";
    private string likes = "";
    private string weaponOfChoice = "käpp";

    public CharacterBuilder Named(string name) { this.name = name; return this; }
    public CharacterBuilder IsA(CharacterKind kind) { this.kind = kind; return this; }
    public CharacterBuilder IsEvil() { isEvil = true; return this; }
    public CharacterBuilder Speaks(string speech) { this.speech = speech; return this; }
    public CharacterBuilder Likes(string likes) { this.likes = likes; return this; }
    public CharacterBuilder WeaponOfChoice(string weapon) { weaponOfChoice = weapon; return this; }
    public Character Build() => new Character(name, kind, isEvil, speech, likes, weaponOfChoice);
}
```

---

## Karaktären i C#

```csharp
enum CharacterKind { Hero, Ogre }

class Character
{
    public string Name { get; }
    public CharacterKind Kind { get; }
    public bool IsEvil { get; }
    public string Speech { get; }
    public string Likes { get; }
    public string WeaponOfChoice { get; }

    public Character(string name, CharacterKind kind, bool isEvil, string speech, string likes, string weaponOfChoice)
    {
        Name = name;
        Kind = kind;
        IsEvil = isEvil;
        Speech = speech;
        Likes = likes;
        WeaponOfChoice = weaponOfChoice;
    }

    public override string ToString()
    {
        string side = IsEvil ? "ond" : "god";
        return $"{Name} är en {side} {Kind}. Säger \"{Speech}\", gillar {Likes} och slåss med {WeaponOfChoice}.";
    }
}
```

---

## Använd den

```csharp
Character mira = new CharacterBuilder()
    .Named("Mira")
    .IsA(CharacterKind.Hero)
    .Speaks("Ta fram svärdet!")
    .Likes("drakar som inte bråkar")
    .WeaponOfChoice("svärd")
    .Build();
Console.WriteLine(mira);

Character grolf = new CharacterBuilder()
    .Named("Grölf")
    .IsEvil()
    .IsA(CharacterKind.Ogre)
    .Speaks("aggghhh")
    .Likes("to mame")
    .WeaponOfChoice("Club")
    .Build();
Console.WriteLine(grolf);
```

Utskrift:
```
Mira är en god Hero. Säger "Ta fram svärdet!", gillar drakar som inte bråkar och slåss med svärd.
Grölf är en ond Ogre. Säger "aggghhh", gillar to mame och slåss med Club.
```

---

## Vad vinner vi?

```csharp
Character grolf = new CharacterBuilder()
    .Named("Grölf")
    .IsEvil()
    .Speaks("aggghhh")
    .Build();
```

- **Ordningen spelar ingen roll.**
- **Det som inte sägs får ett rimligt värde.**
- **Varje rad är en mening.** Ingen dokumentation behövs.

---

## Jämförelse med Factory

| | Factory | Builder |
|---|---|---|
| Frågan den svarar på | *Vilken* typ ska skapas? | *Hur* ska objektet sättas ihop? |
| Exempel | Pizzerians kök väljer rätt pizza | Du väljer tilltugg, ost och sås själv |

Factory väljer. Builder bygger steg för steg.

---

<!-- WOW-MOMENT: "Läs karaktären som en mening": samma information, men den ena versionen går att läsa utan att titta i klassen. -->

## Ett sista sagoknep

Kompilatorn säger inget om du byter ordning på två textparametrar i konstruktorn.
Men om du skriver `.WeaponOfChoice("aggghhh")` och `.Speaks("Club")` syns felet direkt när du läser raden.

Det är därför en saga med namngivna meningar är tryggare än en formel med många tal som ingen minns.

---

## Utan builder går det också

Du kan skapa karaktären direkt med namngivna argument:

```csharp
Character mira = new Character(
    name: "Mira",
    kind: CharacterKind.Hero,
    isEvil: false,
    speech: "Ta fram svärdet!",
    likes: "drakar som inte bråkar",
    weaponOfChoice: "svärd");
```

- **Ordningen spelar ingen roll**, eftersom argumenten har namn.
- **Antalet spelar roll.** Alla parametrar måste anges, om de inte har standardvärden eller finns en annan konstruktor.
- **Namnen måste stämma exakt** Håll koll på CamelCase där! 

---

## Det här gjorde vi i dag

- ✅ Varför en konstruktor med många parametrar blir svår att läsa
- ✅ Builder: en byggare med en mening per egenskap
- ✅ Factory och Builder: välja, respektive bygga steg för steg

---

## Härnäst

- **Övningen Builder** (14:00): uppgift 1 och 2 är 🟢, uppgift 3 är 🟡.
- **Läxa D**: Mira och verktygslådan. Där övar ni Observer igen.
