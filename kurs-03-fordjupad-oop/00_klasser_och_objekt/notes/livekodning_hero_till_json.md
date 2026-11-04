# Livekodning: från hjälte till fil

**Lektion 4 nov 2026** · Kurs-03 Fördjupad OOP

Vi kodade oss igenom hela kedjan idag: från en enkel klass, via arv och interface,
till att spara och läsa en hjälte från fil. Det händde mycket — det här är kartan.

---

## Steg 1: En klass med fält

Vi vill skapa en hjälte. Enklaste möjliga start.

```csharp
var mira = new Hero("Mira", 100);
mira.Speak();
```

```csharp
public class Hero(string name, int health)
{
    public string Name { get; set; } = name;
    public int Health { get; set; } = health;

    public void Speak()
    {
        Console.WriteLine($"Hello! I'm {Name} and I am Cool");
    }
}
```

Notera: `Hero("Mira", 100)` — konstruktorn tar emot värden direkt i klassrubriken (primary constructor, C# 12).

---

## Steg 2: Flera hjältar i en lista

```csharp
var party = new List<Hero>() { mira, pelle, tobbe };

foreach (var hero in party)
{
    Console.WriteLine($"{hero.Name} has health {hero.Health}");
}
```

En lista av `Hero` — alla beter sig likadant. Men vad händer när vi vill ha monster med?

---

## Steg 3: Arv — en gemensam basklass

Problemet: `Hero` och `Monster` delar `Name` och `Health`, men har olika `Speak()` och `Attack()`.
Lösning: flytta det gemensamma till `Character`.

```csharp
public class Character(string name, int health)
{
    public string Name { get; set; } = name;
    public int Health { get; set; } = health;
}
```

```csharp
public class Hero(string name, int health) : Character(name, health)
{
    public override void Speak() =>
        Console.WriteLine($"Hello! I'm {Name} and I am Cool");
}

public class Monster(string name, int health) : Character(name, health)
{
    public override void Speak() =>
        Console.WriteLine("Grrr!");
}
```

Nu kan vi ha en lista av `Character` som innehåller både hjältar och monster:

```csharp
var characters = new List<Character>() { mira, monster };

foreach (var thing in characters)
{
    thing.Speak(); // Rätt Speak() körs beroende på typ — det är polymorfism
}
```

---

## Steg 4: Abstract — tvinga på subklasserna

Problemet: vad händer om man glömmer skriva `Speak()` i en ny klass?
Lösning: gör `Character` abstrakt och `Speak()` till ett krav.

```csharp
public abstract class Character(string name, int health)
{
    public string Name { get; set; } = name;
    public int Health { get; set; } = health;

    public abstract void Speak();   // Inget innehåll — subklassen MÅSTE implementera
    public abstract void Attack(Character enemy);
}
```

```csharp
// Går inte att göra detta längre:
var c = new Character("?", 0); // Kompileringsfel — abstract klass kan inte instansieras
```

Abstrakt = "den här klassen finns bara för att ärvas, inte för att användas direkt".

---

## Steg 5: Interface — en extra förmåga

Problemet: bara `Wizard` ska kunna hela. Arv passar inte — `Wizard` ärver redan `Character`.
Lösning: ett interface som läggs på de klasser som behöver förmågan.

```csharp
public interface IHealer
{
    void Heal(Character wounded);
}
```

```csharp
public class Wizard(string name, int health) : Character(name, health), IHealer
{
    public void Heal(Character wounded)
    {
        Console.WriteLine("Hokus Pokus, I cannot focus!");
        wounded.Health = 100;
    }

    public override void Speak() =>
        Console.WriteLine($"Hello! I'm {Name} and I am Wiz");

    public override void Attack(Character enemy) =>
        enemy.Health -= 5;
}
```

Vi kan kolla i runtime om en karaktär är en healer:

```csharp
foreach (var thing in characters)
{
    thing.Speak();
    if (thing is IHealer)
    {
        (thing as IHealer).Heal(mira);
    }
}
```

---

## Steg 6: Spara hjälten till JSON-fil

Nu har vi en hjälte — låt oss spara henne till disk så hon överlever nästa körning.

```csharp
using System.Text.Json;

var mira = new Hero("Mira", 99);

string json = JsonSerializer.Serialize(mira,
    new JsonSerializerOptions { WriteIndented = true });

Console.WriteLine(json);
// {
//   "namn": "Mira",
//   "health": 99,
//   "level": 1,
//   ...
// }
```

För att styra JSON-nycklarnas namn, lägg attribut på egenskaperna i klassen:

```csharp
[JsonPropertyName("namn")]
public string Name { get; set; } = name;
```

Spara till fil:

```csharp
string file = Path.Combine(@"c:\git", "Mira.json");
File.WriteAllText(file, json);
```

---

## Steg 7: Läsa tillbaka hjälten

```csharp
string json = File.ReadAllText(file);
var mira2 = JsonSerializer.Deserialize<Hero>(json);

if (mira2 is not null)
{
    Console.WriteLine(mira2.Name);
    Console.WriteLine(mira2.Health);
    foreach (string item in mira2.Inventory)
        Console.Write(item + " ");
}
```

`Deserialize<Hero>` tar en JSON-sträng och skapar ett `Hero`-objekt av den.
Det fungerar bara om JSON-nycklarna matchar det `[JsonPropertyName]` säger.

---

## Steg 8: Refaktorering — flytta ut fillogiken

Problemet: `Program.cs` vet för mycket. Den hanterar användargränssnitt OCH filsystem.
Lösning: en `HeroStorage`-klass tar hand om allt som rör filer.

```csharp
class HeroStorage
{
    public string SavePath { get; set; } = "";

    public void SaveHero(Hero hero)
    {
        string json = JsonSerializer.Serialize(hero);
        File.WriteAllText(GetFilename(hero.Name), json);
    }

    public Hero? LoadHero(string name)
    {
        string json = File.ReadAllText(GetFilename(name));
        return JsonSerializer.Deserialize<Hero>(json);
    }

    public bool HeroExists(string name) =>
        File.Exists(GetFilename(name));

    private string GetFilename(string name) =>
        Path.Combine(SavePath, name + ".json");
}
```

`Program.cs` blir minimal — den vet bara om användaren:

```csharp
var hs = new HeroStorage() { SavePath = @"c:\git" };

Console.WriteLine("Vad heter din hjälte?");
string name = Console.ReadLine() ?? "";
Hero? hero;

if (!hs.HeroExists(name))
{
    Console.WriteLine("Välkommen newbie!");
    hero = new Hero(name, 100);
}
else
{
    hero = hs.LoadHero(name);
}

Console.WriteLine(hero.Name + " has " + hero.Health + " and is level " + hero.Level);
```

`Program.cs` vet inte hur filen sparas. Det är `HeroStorage`s ansvar. Det är SRP — Single Responsibility Principle.

---

## Hela resan i korthet

| Steg | Vad vi lärde oss |
|------|-----------------|
| Klass | Samla data och beteende på ett ställe |
| Arv | Dela kod utan att kopiera den |
| Abstract | Tvinga subklasser att implementera metoder |
| Interface | Ge en klass extra förmågor oberoende av arv |
| JSON | Serialisera ett objekt till text och tillbaka |
| Refaktorering | Flytta ansvar till rätt klass — SRP |

Koden finns på GitHub:
- [Mira_hero_01_livekodning](https://github.com/marcusjobb/Mira_hero_01_livekodning)
- [CatJson](https://github.com/marcusjobb/CatJson)
