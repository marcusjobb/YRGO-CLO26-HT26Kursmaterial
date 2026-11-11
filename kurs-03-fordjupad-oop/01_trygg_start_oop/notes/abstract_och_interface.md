# Abstract, interface och att filtrera typer

Läsversion av tredje passet. Förra delen: `arv.md`. Övning på samma sak med ett nytt tema: `exercises/ovning_4_garaget.md`.

Termer finns i `termer/oop_repetition.md`.

## Abstract

En allmän "karaktär" finns inte i spelet. Vi vill hindra att någon skapar en.

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

    public override string Attack()
    {
        return $"{Name} svingar svärdet!";
    }
}
```

`new Character(...)` ger nu ett kompileringsfel, och det är meningen. `abstract string Attack()` tvingar varje barn att skriva sin egen version.

## Interface

Vissa kan läka. Det är en förmåga, inte en sorts karaktär.

```csharp
interface IHealer
{
    void Heal(Character target);
}

class Wizard : Character, IHealer
{
    public Wizard(string name, int health) : base(name, health) { }

    public override string Attack()
    {
        return $"{Name} kastar en gnista!";
    }

    public void Heal(Character target)
    {
        target.Health += 20;
        Console.WriteLine($"{Name} läker {target.Name}.");
    }
}
```

| | `abstract class` | `interface` |
|---|---|---|
| Betyder | "är en sorts" | "kan göra" |
| Hur många? | En enda | Hur många som helst |
| Exempel | `Character` | `IHealer` |

## Vad, när och hur

| | `abstract class` | `interface` |
|---|---|---|
| **Vad** | En ritning som aldrig blir ett objekt | Ett löfte om vad en klass kan göra |
| **När** | Alla barn är samma sorts sak och delar fält och kod | En förmåga som olika sorters saker kan dela |
| **Hur** | `abstract class Character` + `abstract string Attack();` | `interface IHealer` + `void Heal(Character target);` |
| **Barnet skriver** | `: Character` och `override` | `: Character, IHealer` (basklassen först) |
| **Prova med meningen** | "Wizard **är en sorts** Character" | "Wizard **kan** läka" |

Är du osäker: ställ båda meningarna. Den som låter naturlig visar vägen.

## Filtrera typer

Både en klass och ett interface kan användas för att plocka ut det som passar ur en lista.

Vi lägger till en `Wizard` i scenen, så att det finns någon som kan läka:

```csharp
List<Character> scene = new List<Character>
{
    new Hero("Mira", 100),
    new Monster("Gruff", 60),
    new Wizard("Zelda", 70)
};

foreach (Hero h in scene.OfType<Hero>())
{
    Console.WriteLine($"Hjälte: {h.Name}");
}

foreach (IHealer healer in scene.OfType<IHealer>())
{
    healer.Heal(scene[0]);
}
```

`OfType<T>()` returnerar bara de objekt som är av typen `T`. Det fungerar lika bra på en basklass som på ett interface.

## Att ta med dig

1. Klassen är ritningen, objektet är huset.
2. Det som är gemensamt skrivs en gång, i basklassen.
3. `abstract` hindrar att man skapar något som inte finns på riktigt.
4. `interface` beskriver en förmåga som flera olika klasser kan dela.
5. `OfType<T>()` filtrerar efter typ.

Övning på samma sak med ett nytt tema: `exercises/ovning_4_garaget.md`.

Termer finns i `termer/oop_repetition.md`.
