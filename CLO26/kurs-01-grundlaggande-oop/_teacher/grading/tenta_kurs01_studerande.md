# Tenta — Grundläggande OOP i C#

**Datum:** Fredag 2 okt 2026  
**Tid:** 09:00–11:00  
**Hjälpmedel:** Inga  
**Betygsgräns:** Meddelas vid rättning

Skriv ditt namn överst på varje sida. Svara med egna ord.  
Du behöver inte skriva perfekt kod — vi testar förståelse, inte syntax.

---

## Del 1 — Begrepp (30p)

*Förklara med egna ord. Kodexempel är okej om det hjälper, men inte nödvändigt.*

**1.** Vad är en klass? Vad är ett objekt?

**2.** Vad är arv? Ge ett exempel med egna ord.

**3.** Vad är en subklass? Vad är en basklass?

**4.** Vad innebär det att en variabel eller metod är `private`?

**5.** Vad är en property? Hur skiljer den sig från ett vanligt fält?

**6.** Vad är skillnaden mellan `int`, `string`, `bool` och `double`?

**7.** Vad är en konstruktor? Varför används den?

**8.** Vad menas med inkapsling?

**9.** Vad är polymorfism?

**10.** Varför delar man upp kod i klasser?

---

## Del 2 — Vad är fel? (40p)

*Titta på varje kodsnutt. Förklara vad som är fel och varför det inte fungerar.*

---

**Fråga 1**

```csharp
string name = 42;
```

---

**Fråga 2**

```csharp
int x = "hello";
```

---

**Fråga 3**

```csharp
class Animal
{
    private string name;
}

class Program
{
    static void Main()
    {
        Animal a = new Animal();
        Console.WriteLine(a.name);
    }
}
```

---

**Fråga 4**

```csharp
class Dog
{
    public string Name { get; set; }
}

class GoldenRetriever : Dog
{
    public string Name { get; set; }
}
```

*Koden kompilerar — men något är designmässigt fel. Vad?*

---

**Fråga 5**

```csharp
bool isAlive = "true";
```

---

**Fråga 6**

```csharp
class Monster
{
    public int HP { get; set; }
    public int Attack { get; set; }
}

class Goblin : Monster
{
    public Goblin() { HP = 30; Attack = 5; }
}

class Troll : Monster
{
    public Troll() { HP = 30; Attack = 5; }
}
```

*Koden fungerar. Vad är ändå problematiskt med designen?*

---

**Fråga 7**

```csharp
class Character
{
    public int HP;
    public int MaxHP;

    public void LevelUp()
    {
        MaxHP += 20;
        HP = MaxHP;
    }
}
```

*Koden fungerar — men det finns en designmässig svaghet. Vad?*

---

**Fråga 8**

```csharp
List<Monster> monsters = new List<Monster>();
monsters.Add(new Goblin());
monsters.Add(new Troll());

foreach (Monster m in monsters)
{
    m.Attack();
}
```

*Vad krävs för att det här ska fungera?*

---

## Del 3 — Läs koden, förstå systemet (30p)

```csharp
class Weapon
{
    public string Name { get; private set; }
    public int AttackBonus { get; private set; }
    public int Price { get; private set; }

    public Weapon(string name, int attackBonus, int price)
    {
        Name = name;
        AttackBonus = attackBonus;
        Price = price;
    }
}

class Sword : Weapon
{
    public Sword() : base("Svärd", 10, 50) { }
}

class Bow : Weapon
{
    public Bow() : base("Pilbåge", 7, 35) { }
}
```

**Fråga 1**  
Förklara vad `: base("Svärd", 10, 50)` gör. Varför är det där?

**Fråga 2**  
Varför är `AttackBonus` och `Price` deklarerade med `private set`?

**Fråga 3**  
Någon föreslår att lägga till:

```csharp
class MagicStaff : Weapon
{
    public MagicStaff() : base("Trollstav", 15, 80) { }
    public int MagicBonus { get; private set; } = 20;
}
```

Vad tillför `MagicBonus` som `Sword` och `Bow` inte har? Är det rätt sätt att göra det?
