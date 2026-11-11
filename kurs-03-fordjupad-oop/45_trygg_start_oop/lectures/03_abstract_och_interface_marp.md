---
marp: true
theme: nion-dark
paginate: true
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
