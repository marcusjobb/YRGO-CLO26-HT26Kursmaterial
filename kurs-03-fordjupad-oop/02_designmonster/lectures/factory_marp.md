---
marp: true
theme: nion-dark
paginate: true
---

# Designmönster: snälla verktyg

**Del 2 av 3: Factory**

---

## Kommer ni ihåg uppgift 4?

```csharp
IAttackStrategy weapon;
if (input == "2")
{
    weapon = new BowAttack();
}
else if (input == "3")
{
    weapon = new FireballAttack();
}
else
{
    weapon = new SwordAttack();
}
```

Den här koden **väljer och skapar** objekt. Var ska den bo?

---

## Problemet: `new` överallt

Tänk dig ett spel där monster skapas

- när en ny nivå startar
- när en grotta öppnas
- när spelaren sparar och laddar

Samma `if` för att välja monster finns på tre ställen.
Ett nytt monster? Ändra på tre ställen. Missa ett, och spelet kraschar.

---

## Idén: en fabrik

**All kod som skapar monster finns på ett enda ställe.**

```csharp
static class MonsterFactory
{
    public static Monster Create(string type)
    {
        switch (type)
        {
            case "goblin":
                return new Goblin();
            case "troll":
                return new Troll();
            default:
                throw new ArgumentException($"Det finns inget monster som heter '{type}'.");
        }
    }
}
```

---

## Monstren känner ni igen

```csharp
abstract class Monster
{
    public string Name;
    public int Health;

    // ... konstruktor

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

`abstract` och `override` från förra veckan. Inget nytt här.

---

## Den som beställer vet bara "Monster"

```csharp
List<Monster> wave = new List<Monster>();
wave.Add(MonsterFactory.Create("goblin"));
wave.Add(MonsterFactory.Create("troll"));

foreach (Monster monster in wave)
{
    Console.WriteLine($"{monster.Name}: {monster.Roar()}");
}
```

```
Goblin: Hihihi!
Troll: GRRRUUMF!
```

Programmet beställer ett monster. Fabriken bestämmer vilken klass som byggs.

---

## Fel beställning

```csharp
try
{
    Monster unknown = MonsterFactory.Create("unicorn");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Fel: {ex.Message}");
}
```

```
Fel: Det finns inget monster som heter 'unicorn'.
```

Fabriken säger tydligt ifrån. Det är bättre än ett tyst fel långt senare.

---

## I verkligheten: kunden väljer betalsätt

```csharp
string choice = "swish";   // Kommer från knappen kunden tryckte på

// Tänk på att lägga till Try Catch
IPaymentMethod payment = PaymentFactory.Create(choice);
payment.Pay(249m);
```

Fabriken skapar en **strategi** från förra passet. Mönster jobbar gärna ihop.

---

## Så känner du igen Factory

> Programmet ska skapa **ett av flera** slags objekt,
> och valet görs på **flera ställen** eller från text (en meny, en fil, en inställning).

- monster från en nivåfil
- betalsätt från en knapp
- dokument som PDF, Word eller text

Bara ett ställe som skapar objektet, och bara en sort? Då behövs ingen fabrik.

---

## Nu: övning

`exercises/ovning_factory.md`

Börja på 🟢.
