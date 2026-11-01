---
marp: true
theme: nion-dark
paginate: true
---

# Välkommen! Vi börjar med det ni redan kan

**Kurs:** Fördjupad OOP i C#
**Modul:** 00 Repetition: klasser, arv, abstract och interface

Marcus Ackre Medina · YRGO · CLO26

---

## Den här kursen

- 5 veckor. Lektion måndag och onsdag, eget arbete tisdag och torsdag, handledning fredag.
- Läxorna bär halva kursen. Ni gör dem själva, så de är byggda för att gå att klara själv.
- Betyg **G** och **VG**. Det går att nå VG genom att fördjupa sig, aldrig genom att gissa vad jag vill.
- Ni kommer att klara det här. Idag börjar vi med sådant ni redan har sett.

---

## Idag, i fyra steg

| Tid | Vad |
|-----|-----|
| 09 | Klass och objekt |
| 11 | Arv |
| 14 | `abstract` och `interface` |
| Mellan | Övningar, i par |

Ett steg i taget. Exempel först, förklaringen efteråt.

---

## Berättelsen

Mira är en hjälte i ett litet äventyrsspel.

Hon behöver ett namn, lite hälsa och ett sätt att säga hej.

Hur beskriver vi henne i kod?

---

## Klass = ritning, objekt = huset

```csharp
class Hero
{
    public string Name;
    public int Health;

    public void Greet()
    {
        Console.WriteLine($"Hej, jag heter {Name}!");
    }
}
```

`Hero` är ritningen. Än finns ingen hjälte i spelet.

---

## `new` bygger huset

```csharp
Hero mira = new Hero();
mira.Name = "Mira";
mira.Health = 100;
mira.Greet();
```

**Output:**
```
Hej, jag heter Mira!
```

Nu finns det ett objekt. Samma ritning kan bygga hur många som helst.

---

## Fler hjältar, samma ritning

```csharp
Hero tobbe = new Hero();
tobbe.Name = "Tobbe";
tobbe.Health = 80;
tobbe.Greet();
```

**Output:**
```
Hej, jag heter Tobbe!
```

Två objekt, två helt skilda uppsättningar värden. Ändrar vi Mira, ändras inte Tobbe.

---

## Konstruktorn: vad händer när objektet föds?

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
}

Hero mira = new Hero("Mira", 100);
```

Konstruktorn körs i exakt det ögonblick `new` bygger objektet. Ingen hjälte utan namn.

<!-- WOW-MOMENT: alla studerandes egna hjältar står bredvid varandra i samma äventyr på storbild -->

---

## Wow: era hjältar möts

Alla hittar på en hjälte: ett namn och ett hälsovärde.

Ni skriver in den i en gemensam lista, och så kör vi äventyret på storbild.

```csharp
List<Hero> party = new List<Hero>();
party.Add(new Hero("Mira", 100));
// ... och så er hjälte!

foreach (Hero hero in party)
{
    hero.Greet();
}
```

---

## Övning 1: din egen hjälte (i par)

Roller: **en skriver, en läser högt.** Byt efter varje deluppgift.

🟢 Skapa klassen `Hero` med `Name` och `Health`.
🟢 Skapa två hjältar och låt dem hälsa.
🟡 Lägg till metoden `TakeDamage(int amount)`.

Klara på 15 minuter? Räck upp handen, så får ni nästa steg.

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

---

## Lunch. Sedan: två nya verktyg

Frågan: kan vi skapa en `Character` som inte är hjälte eller monster?

```csharp
Character c = new Character("???", 10);
```

Det går, men ger ingen mening. En "karaktär" i allmänhet finns inte i spelet.

---

## `abstract`: en ritning som aldrig får bli ett hus

```csharp
abstract class Character
{
    // ... Name, Health, konstruktor

    // Alla barn MÅSTE svara på det här
    public abstract string Attack();
}
```

`new Character(...)` ger nu ett kompileringsfel. Det är meningen.

---

## Barnen måste svara

```csharp
class Hero : Character
{
    // ... konstruktor

    public override string Attack()
    {
        return $"{Name} svingar svärdet!";
    }
}
```

Glömmer `Hero` att skriva `Attack()` får vi ett kompileringsfel. Löftet måste hållas.

---

## `interface`: ett löfte om vad man kan

Vissa karaktärer kan hela andra. Det är inte en sorts karaktär, det är en förmåga.

```csharp
interface IHealer
{
    void Heal(Character target);
}
```

Interfacet lovar bara att det *finns* en `Heal`. Hur den läker bestämmer klassen.

---

## Wizard håller löftet

```csharp
class Wizard : Character, IHealer
{
    // ... konstruktor, Attack()

    public void Heal(Character target)
    {
        target.Health += 20;
        Console.WriteLine($"{Name} läker {target.Name}.");
    }
}
```

`Wizard` ärver från en basklass (`Character`) och lovar en förmåga (`IHealer`).

---

## Abstract eller interface?

| | `abstract class` | `interface` |
|---|---|---|
| Betyder | "är en sorts" | "kan göra" |
| Hur många? | En enda | Hur många som helst |
| Kan ha kod | Ja | Löfte (kod är valfri) |
| Exempel | `Character` | `IHealer` |

---

## Filtrera typer med båda

Vem i scenen är en hjälte? Vem kan läka?

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

`OfType<T>()` plockar ut det som passar. Det fungerar på en klass, en basklass och ett interface.

---

## Övning 3: abstract och interface (i par)

🟢 Gör `Character` abstrakt med `abstract string Attack()`.
🟢 Skapa `IHealer` och låt `Wizard` använda det.
🟡 Filtrera ut alla `IHealer` med `OfType<IHealer>()` och låt dem läka.

---

## Läxa till i morgon

Ni har fått kapitel 1 av hjältens äventyr, med fyra ämnen:

- Klass och objekt
- Arv
- `abstract`
- `interface`

Varje ämne har 🟢 **två obligatoriska**, 🟡 **två mer övning** och 🥷 **två Ninja**.

Ninja är frivilligt. Fastnar ni: ledtrådar finns i hopfällbara block. Ta med en fråga till fredag.

---

## Sammanfattning

Skriv själva, 2 minuter, utan att titta:

1. Vad är skillnaden mellan klass och objekt?
2. Vad gör `override`?
3. När väljer du `abstract` och när `interface`?

Vi ses på onsdag. Då får hjälten en sparfil.
