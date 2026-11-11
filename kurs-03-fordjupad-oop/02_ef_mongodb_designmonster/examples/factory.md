# Factory: exempel med förklaringar

Projekten ligger i `source/repos`: `Designmonster_Factory`, `Designmonster_Factory_Monster` och `Designmonster_Factory_Vapen`.

**Idén:** en klass samlar allt `new` på ett ställe. Den som beställer säger *vad* den vill ha, och fabriken bestämmer *vilken* klass som skapas. Frågor för när mönstret passar finns i [flödesschemat](../notes/flodesschema_designmonster.md#factory).

---

## 1. `Designmonster_Factory` (allt i en fil)

Det här projektet har alla fyra uppgifterna i samma `Program.cs`, med monster och vapen i samma fil.

### 1.1 Uppgift 1: beställ monster

```csharp
Monster first = MonsterFactory.Create("goblin");
Monster second = MonsterFactory.Create("troll");

Console.WriteLine($"{first.Name} ({first.Health} hälsa): {first.Roar()}");
Console.WriteLine($"{second.Name} ({second.Health} hälsa): {second.Roar()}");
```

**Vad händer:** Koden här skapar inga monster själv. Den skickar en text till fabriken och får tillbaka en `Monster`. Den vet inte om det är en `Goblin` eller en `Troll`, och behöver inte veta det.

**Stil:** Variabeln har typen `Monster` (basklassen), inte `Goblin`. Det är själva poängen: koden som använder objektet ska bara kräva det gemensamma.

### 1.2 Uppgift 2: ett nytt monster

```csharp
Monster skeleton = MonsterFactory.Create("skeleton");
Console.WriteLine($"{skeleton.Name} ({skeleton.Health} hälsa): {skeleton.Roar()}");
```

**Vad händer:** Det här är det som ska vara enkelt: ett nytt monster kräver två ändringar. En ny klass, och en ny rad i fabriken. Ingen annan kod i programmet ändras.

**Stil:** Kommentarerna `// Uppgift 2` och `// Den enda nya raden i fabriken` visar var ändringen gjordes. Det är ett bra sätt att dokumentera en övning i koden.

### 1.3 Uppgift 3: en våg från text

```csharp
string waveText = "goblin,goblin,troll,unicorn";
string[] types = waveText.Split(',');

List<Monster> wave = new List<Monster>();
foreach (string type in types)
{
    try
    {
        wave.Add(MonsterFactory.Create(type));
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Hoppar över: {ex.Message}");
    }
}
```

**Vad händer:** Texten delas upp i en lista med namn. "unicorn" finns inte i fabriken, så den kastar ett `ArgumentException`. Det fångas här, skrivs ut, och loopen fortsätter med nästa namn.

**Stil:** `try/catch` ligger runt anropet, inte runt hela loopen. Då hoppar man bara över det enda felet, och resten av vågen byggs klart.

### 1.4 Monsterklasserna

```csharp
abstract class Monster
{
    public string Name;
    public int Health;

    public Monster(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public abstract string Roar();
}

class Goblin : Monster
{
    public Goblin() : base("Goblin", 30) { }

    public override string Roar()
    {
        return "Hihihi!";
    }
}
```

**Vad händer:** `Monster` är en basklass med ett abstrakt `Roar()`. Varje monster måste ha sitt eget ljud, och det tvingas kompilatorn att kontrollera.

**Stil:**
- Fälten är `public`. Det är enkelt, men i nästa projekt används `{ get; }` i stället.
- `: base("Goblin", 30)` skickar värdena till basklassens konstruktor. Varje monster har sitt eget startvärde.

### 1.5 Fabriken för monster

```csharp
static class MonsterFactory
{
    public static Monster Create(string type)
    {
        return type switch
        {
            "goblin" => new Goblin(),
            "troll" => new Troll(),
            "skeleton" => new Skeleton(),// Den enda nya raden i fabriken (uppgift 2)
            _ => throw new ArgumentException($"Det finns inget monster som heter '{type}'."),
        };
    }
}
```

**Vad händer:** En switch-uttryck väljer klass utifrån texten. Okänd text kastar ett fel, som den som beställer kan fånga (se uppgift 3).

**Stil:**
- `static class` går inte att skapa en instans av. Fabriken har inget eget tillstånd, så den behöver inte vara ett objekt.
- Switch-uttrycket (`switch` med `=>`) är kortare än `switch`-satsen. Standardgrenen `_` är obligatorisk om man vill fånga okända värden.
- Kommentaren efter raden har inget mellanslag före `//`. Det fungerar, men det är bra att skriva ett mellanslag för läsbarheten.

### 1.6 Uppgift 4: fabriken för vapen

```csharp
static class WeaponFactory
{
    public static IAttackStrategy Create(string type)
    {
        switch (type)
        {
            case "sword":
                return new SwordAttack();
            case "bow":
                return new BowAttack();
            case "fireball":
                return new FireballAttack();
            default:
                throw new ArgumentException($"Det finns inget vapen som heter '{type}'.");
        }
    }
}
```

**Vad händer:** Den här fabriken returnerar `IAttackStrategy`, interfacet, och inte en konkret klass. Hjälten vet inte vilket vapen den får.

**Stil:**
- Den här skriver `switch`-satsen med `case` och `return`. Det är samma logik som switch-uttrycket ovan, bara längre. Båda stilarna finns i samma projekt, vilket är bra att lägga märke till.
- `default` med `throw` gör att okänd text inte blir tyst. Det går inte att få en `null` i stället för ett vapen.

---

## 2. `Designmonster_Factory_Monster` (separata filer och namespace)

Samma monster, men uppdelade i `Models/`, `Factories/` och `Demos/`.

### 2.1 Basklassen `Monster`

```csharp
namespace Designmonster.Factory.Models;

/// <summary>Basklassen för alla monster. Varje monster vrålar på sitt eget sätt.</summary>
public abstract class Monster
{
    public string Name { get; }
    public int Health { get; }

    protected Monster(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public abstract string Roar();
}
```

**Vad händer:** Samma som förut, men med properties i stället för publika fält.

**Stil:**
- `protected` konstruktor: bara underklasser kan anropa den. Ingen kan skapa ett "allmänt" monster utifrån.
- `{ get; }` ger skrivskyddade properties, så `Health` inte kan ändras av misstag.

### 2.2 `Goblin` med expression-bodied member

```csharp
public class Goblin : Monster
{
    public Goblin() : base("Goblin", 30) { }

    public override string Roar() => "Hihihi!";
}
```

**Stil:** `=> "Hihihi!"` är en kortare form av `{ return "Hihihi!"; }`. Den passar när metoden bara har en rad.

### 2.3 Fabriken i en egen fil

```csharp
public static class MonsterFactory
{
    public static Monster Create(string type) => type switch
    {
        "goblin" => new Goblin(),
        "troll" => new Troll(),
        "skeleton" => new Skeleton(),   // Den enda raden som lades till i uppgift 2
        _ => throw new ArgumentException($"Det finns inget monster som heter '{type}'.")
    };
}
```

**Vad händer:** Samma logik som i `Designmonster_Factory`. Skillnaden är att hela metoden är ett uttryck (`=>`), så det finns inga `return`.

**Stil:** Hela fabriken är en enda sats. Det är lätt att se vilka typer som stöds.

### 2.4 Demon: beställ och skapa vågen

```csharp
public static class MonsterDemo
{
    public static void RunWave()
    {
        Console.WriteLine("--- Våg ---");
        string waveText = "goblin,goblin,troll,unicorn";

        List<Monster> wave = new();
        foreach (string type in waveText.Split(','))
        {
            try
            {
                wave.Add(MonsterFactory.Create(type));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Hoppar över: {ex.Message}");
            }
        }

        Console.WriteLine($"Vågen har {wave.Count} monster.");
    }
}
```

**Vad händer:** Samma som uppgift 3 i det första projektet, men med `new()` i stället för `new List<Monster>()`.

**Stil:** `new()` är target-typed new. Typen står redan i variabeln, så den behöver inte skrivas två gånger.

---

## 3. `Designmonster_Factory_Vapen` (fabriken för strategier)

Det här projektet handlar bara om vapen. Det kopplar fabriken till Strategy-mönstret: `WeaponFactory` skapar `IAttackStrategy`-objekt.

### 3.1 Interfacet

```csharp
namespace Designmonster.Factory.Interfaces;

/// <summary>Ett sätt att attackera. Varje vapen är en egen implementation.</summary>
public interface IAttackStrategy
{
    string Attack(string attackerName);
}
```

**Vad händer:** Ett kontrakt. Alla vapen måste kunna attackera med ett namn, och det är allt som hjälten vet.

**Stil:** Interfacet ligger i en egen mapp (`Interfaces/`). Strategierna ligger i en annan (`Strategies/Attack/`). Då syns skillnaden mellan kontrakt och implementation direkt i mappstrukturen.

### 3.2 Hjälten

```csharp
public class Hero
{
    public string Name { get; }
    public IAttackStrategy Weapon { get; set; }

    public Hero(string name, IAttackStrategy weapon)
    {
        Name = name;
        Weapon = weapon;
    }

    public string Attack() => Weapon.Attack(Name);
}
```

**Vad händer:** `Weapon` har typen av interfacet. Hjälten kan byta vapen med `Weapon = ...`, eftersom det har `set`.

**Stil:** `Name` har bara `get`, eftersom namnet inte ska ändras. `Weapon` har `set`, eftersom vapnet ska kunna bytas. Det är en medveten skillnad, och den syns i koden.

### 3.3 Fabriken

```csharp
public static class WeaponFactory
{
    public static IAttackStrategy Create(string type) => type switch
    {
        "sword" => new SwordAttack(),
        "rusty sword" => new SwordAttack(),
        "bow" => new BowAttack(),
        "fireball" => new FireballAttack(),
        _ => throw new ArgumentException($"Det finns inget vapen som heter '{type}'.")
    };
}
```

**Vad händer:** `"sword"` och `"rusty sword"` ger båda ett `SwordAttack`, alltså samma klass. Skillnaden ligger i de värden fabriken skickar med: `"rusty sword"` får en annan beskrivning och 6 i skada i stället för 10. Fabriken väljer klass *och* egenskaper, och hjälten ser bara resultatet.

**Stil:** Fabriken är det enda stället där namnen kopplas till klasser och värden. Ett nytt namn kräver en rad här, och ingen annan kod ändras. Den här raden visar att man kan återanvända en klass med olika egenskaper, i stället för att skriva en ny klass för varje variant.

### 3.4 Demon

```csharp
public static class WeaponDemo
{
    public static void Run()
    {
        Hero mira = new("Mira", WeaponFactory.Create("sword"));
        Console.WriteLine(mira.Attack());

        mira.Weapon = WeaponFactory.Create("rusty sword");
        Console.WriteLine(mira.Attack());

        mira.Weapon = WeaponFactory.Create("fireball");
        Console.WriteLine(mira.Attack());
    }
}
```

**Vad händer:** Mira får ett svärd från fabriken, byter till ett rostigt svärd, och sedan till eldklotet. Det rostiga svärdet är samma klass som vanliga svärdet, med andra värden. Hon byter vapen på samma sätt som i Strategy-exemplet, men vapnen skapas nu av fabriken.

**Stil:** Demon innehåller ingen `new` för vapnen, bara anrop till `WeaponFactory.Create`. Det är det som visar att fabriken används.
