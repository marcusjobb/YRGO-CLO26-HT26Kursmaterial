# Arv, virtual och override

Läsversion av andra passet. Förra delen: `klass_och_objekt.md`. Nästa del: `abstract_och_interface.md`.

## Arv

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
