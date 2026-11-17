---
marp: true
theme: nion-dark
paginate: true
---

# Designmönster: snälla verktyg

**Del 1 av 3: Strategy**

Kurs 3 · Fördjupad OOP i C#

---

## Ni har redan gjort det här

I måndags gjorde ni samma sak gång på gång:

- en klass som pratar med databasen
- en klass för varje sak som sparas
- en lista som håller dem

Ingen sa åt er att göra så. Det var bara ett bra sätt.

**Ett designmönster är ett bra sätt som har fått ett namn.**

---

## Idag: tre mönster

| Mönster | Löser |
|---------|-------|
| **Strategy** | Samma uppgift, flera sätt att göra den |
| **Factory** | Var objekten skapas |
| **Observer** | Hur andra får veta att något hänt |

Målet idag: **känna igen** dem. Ni behöver inte kunna dem utantill.

---

## Problemet: Mira ska slåss

```csharp
class Hero
{
    public string Name;
    public string Weapon;

    public string Attack()
    {
        if (Weapon == "sword")
        {
            return $"{Name} svingar svärdet!";
        }
        else if (Weapon == "bow")
        {
            return $"{Name} skjuter en pil!";
        }
        // ... och så vidare
    }
}
```

Det fungerar. Vad händer när spelet har tio vapen?

---

## Varje nytt vapen ändrar Hero

- tio `else if` i samma metod
- varje nytt vapen = vi öppnar `Hero` igen
- en miss i en gren kan förstöra de andra

`Hero` borde handla om hjälten, inte om alla vapen i hela spelet.

---

## Idén: vapnet blir en egen klass

```csharp
// Strategin: ett löfte om att kunna attackera
interface IAttackStrategy
{
    string Attack(string attackerName);
}
```

Känner ni igen det? Det är ett `interface`, precis som `IHealer` förra veckan.

---

## Vapnen

```csharp
class SwordAttack : IAttackStrategy
{
    public string Attack(string attackerName)
    {
        return $"{attackerName} svingar svärdet! (10 skada)";
    }
}

class BowAttack : IAttackStrategy
{
    public string Attack(string attackerName)
    {
        return $"{attackerName} skjuter en pil på håll! (7 skada)";
    }
}
```

Varje vapen vet hur det självt fungerar. Inget annat.

---

## Hero frågar vapnet

```csharp
class Hero
{
    public string Name;
    public IAttackStrategy Weapon;

    public Hero(string name, IAttackStrategy weapon)
    {
        Name = name;
        Weapon = weapon;
    }

    public string Attack()
    {
        // Hero vet inte HUR attacken går till. Den frågar vapnet.
        return Weapon.Attack(Name);
    }
}
```

---

<!-- WOW-MOMENT: Mira byter vapen med en enda rad medan programmet kör. Lägg sedan till FireballAttack live utan att röra Hero, och kör igen. -->

## Byt vapen med en rad

```csharp
Hero mira = new Hero("Mira", new SwordAttack());
Console.WriteLine(mira.Attack());

mira.Weapon = new BowAttack();       // En rad, och Mira slåss på ett nytt sätt
Console.WriteLine(mira.Attack());
```

```
Mira svingar svärdet! (10 skada)
Mira skjuter en pil på håll! (7 skada)
```

---

## Ett nytt vapen, och Hero rörs inte

```csharp
class FireballAttack : IAttackStrategy
{
    public string Attack(string attackerName)
    {
        return $"{attackerName} kastar ett eldklot! (15 skada)";
    }
}
```

```csharp
mira.Weapon = new FireballAttack();
```

Vi lade till. Vi ändrade ingenting som redan fungerade.

---

## I verkligheten: kassan i en webbshop

```csharp
interface IPaymentMethod
{
    void Pay(decimal amount);
}

class Checkout
{
    public IPaymentMethod PaymentMethod;

    // ... konstruktor

    public void Pay(decimal amount)
    {
        PaymentMethod.Pay(amount);
    }
}
```

Kort, Swish, faktura: tre strategier. Kassan bryr sig inte om vilken.

---

## Så känner du igen Strategy

> Samma uppgift kan göras på **flera sätt**,
> och du vill kunna **byta** sätt.

- vapen i ett spel
- betalsätt i en kassa
- fraktsätt, rabatter, sortering

Ser du en lång `if`/`else if` som väljer *hur* något ska göras? Där kan Strategy passa.

---

## Nu: övning

`exercises/ovning_strategy.md`

Börja på 🟢. Ni sitter i par: en skriver, en läser högt.
