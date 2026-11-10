# Builder: en karaktär, en mening i taget

Läsversion av Builder, som hålls på onsdagen efter Observer. Bildspel: `lectures/builder_marp.md`. Övning: `exercises/ovning_builder.md`.

**Efter det här kan du:** bygga ett objekt med många egenskaper steg för steg, och se vad som händer när ett steg utelämnas.

## Problemet

Mira ska skapas med en konstruktor. Den tar namn, art, ond eller god, vad hen säger, vad hen gillar och vapen.

```csharp
Character mira = new Character("Mira", CharacterKind.Hero, false, "Ta fram svärdet!", "drakar som inte bråkar", "svärd");
```

Vad betyder `false`? Och vilket av de två sista orden är vapnet?

Ännu värre: två textparametrar kan byta plats utan att kompilatorn säger något.

```csharp
Character grolf = new Character("Grölf", CharacterKind.Ogre, true, "to mame", "aggghhh", "Club");
```

Här säger Grölf "to mame" och gillar "aggghhh". Koden körs, och ingen märker det förrän någon läser utskriften.

## Lösningen: byggaren

En byggare är en klass som samlar värden ett i taget och skapar objektet sist.
Varje steg är en metod som returnerar byggaren själv. Det gör att stegen kan skrivas som en kedja.

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

Läs det som en mening: Grölf är ond, han är en ogre, han säger aggghhh, han gillar att mame och hans vapen är en Club.

## Varför kan vi kedja?

Kedjan `Console.ReadLine().ToLower().Trim()` fungerar, eftersom varje metod returnerar en `string`, och nästa metod kan anropas på den. Builder fungerar likadant. Varje `With...`-metod returnerar `CharacterBuilder`, så `.Speaks(...)` kan anropas direkt efter `.Named(...)`.

Om en av metoderna i stället vore `void` skulle kedjan brytas: det finns inget objekt kvar att anropa nästa metod på.

## Hur byggaren är skriven

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

Tre saker att lägga märke till:

1. **`return this;`** är det som gör kedjan möjlig. Varje metod lämnar tillbaka samma byggare, så nästa punkt kan börja direkt efter.
2. **Startvärdena** gör att en karaktär alltid får ett värde. Den som inte säger att hen är ond är god.
3. **`Build()`** är det enda stället där objektet skapas. Fram till dess är allt bara förberedelser.

## Vad vinner vi?

- **Ordningen spelar ingen roll.** `.Speaks` före eller efter `.Likes` ger samma karaktär.
- **Det som inte sägs får ett rimligt värde.** Ingen `.IsEvil()` betyder en god karaktär.
- **Det går att läsa.** Varje rad är en mening, och du behöver ingen dokumentation för att förstå den.
- **Karaktären behöver inte veta hur den byggs.** Det sköter byggaren.

## Builder och Factory

| | Factory | Builder |
|---|---|---|
| Frågan den svarar på | *Vilken* typ ska skapas? | *Hur* ska objektet sättas ihop? |
| Exempel | Pizzerians kök väljer rätt pizza | Du väljer tilltugg, ost och sås själv |

Factory väljer. Builder bygger steg för steg.

## Kontrollfrågor

1. Varför måste varje metod i byggaren returnera `this`?
2. Vad händer med Nisse om du bygger honom med `new CharacterBuilder().Named("Nisse").Build()`?
3. Vilket av de två är lättare att läsa efter ett halvår, konstruktorn med sex parametrar eller byggaren? Varför?
