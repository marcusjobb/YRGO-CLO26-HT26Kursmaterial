# Repetition: klasser, arv, abstract och interface

Det här är läsversionen av måndagens lektion. Du kan läsa den i lugn och ro, utan att ha varit på plats. Allt bygger på hjälten Mira och hennes äventyr.

## 1. Klass och objekt

En **klass** är en ritning. Ett **objekt** är huset som byggs av ritningen.

```csharp
class Hero
{
    public string Name;
    public int Health;

    public Hero(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void Greet()
    {
        Console.WriteLine($"Hej, jag heter {Name}!");
    }
}

Hero mira = new Hero("Mira", 100);
Hero tobbe = new Hero("Tobbe", 80);
mira.Greet();
tobbe.Greet();
```

**Output:**
```
Hej, jag heter Mira!
Hej, jag heter Tobbe!
```

Två objekt av samma klass. Ändrar du Mira, ändras inte Tobbe. Konstruktorn körs i samma ögonblick som `new` bygger objektet.

## 2. Arv

Mira möter ett monster. Båda har `Name` och `Health`. I stället för att skriva det två gånger lägger vi det i en basklass.

```csharp
class Character
{
    public string Name;
    public int Health;

    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public virtual string Speak()
    {
        return "...";
    }
}

class Hero : Character
{
    public Hero(string name, int health) : base(name, health) { }

    public override string Speak()
    {
        return "Jag räddar dagen!";
    }
}

class Monster : Character
{
    public Monster(string name, int health) : base(name, health) { }

    public override string Speak()
    {
        return "GRRR!";
    }
}
```

`:` läser du som "är en sorts". `virtual` betyder att barnen får ändra metoden, `override` betyder att barnet ändrar den.

```csharp
List<Character> scene = new List<Character>
{
    new Hero("Mira", 100),
    new Monster("Gruff", 60)
};

foreach (Character c in scene)
{
    Console.WriteLine($"{c.Name}: {c.Speak()}");
}
```

**Output:**
```
Mira: Jag räddar dagen!
Gruff: GRRR!
```

Listan vet bara att de är `Character`. Ändå körs rätt `Speak()` för varje objekt. Det kallas polymorfism.

## 3. Abstract

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

## 4. Interface

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

## 5. Filtrera typer

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

Termer finns i `termer/oop_repetition.md`.
