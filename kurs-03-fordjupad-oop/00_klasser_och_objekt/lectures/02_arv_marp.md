---
marp: true
theme: nion-dark
paginate: true
---

## Paus. Sedan: Mira får en vän

Mira möter ett monster. Både hon och monstret har `Name` och `Health`.

Ska vi skriva samma sak två gånger?

---

## Arv: skriv gemensamt på ett ställe

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
}
```

Det gemensamma hamnar i en basklass, `Character`.

---

## Barnen ärver

```csharp
class Hero : Character
{
    public Hero(string name, int health)
        : base(name, health) { }
}

class Monster : Character
{
    public Monster(string name, int health)
        : base(name, health) { }
}
```

`:` betyder "är en sorts". En `Hero` är en sorts `Character`.

---

## `virtual`: barnen får ändra

```csharp
class Character
{
    // ... Name, Health, konstruktor

    public virtual string Speak()
    {
        return "...";
    }
}
```

`virtual` säger: "barnen får ändra det här".

---

## `override`: jag ändrar det

```csharp
class Hero : Character
{
    // ... konstruktor

    public override string Speak()
    {
        return "Jag räddar dagen!";
    }
}

class Monster : Character
{
    // ... konstruktor

    public override string Speak()
    {
        return "GRRR!";
    }
}
```

---

## Samma anrop, olika resultat

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

Listan vet bara att de är `Character`. Rätt `Speak()` körs ändå.

---

## Övning 2: arv (i par)

🟢 Skapa `Character`, `Hero` och `Monster` enligt bilden.
🟢 Låt `Hero` och `Monster` säga olika saker med `override`.
🟡 Lägg till en tredje sort, `Wizard`, med eget svar.
