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
