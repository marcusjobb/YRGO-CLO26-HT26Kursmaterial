# Factory: objekten skapas på ett ställe

Läsversion av andra passet om designmönster. Förra delen: `strategy.md`. Övning: `exercises/ovning_factory.md`.

**Efter det här kan du:** känna igen när en fabrik passar, och samla koden som skapar objekt på ett ställe.

## Problemet

I Strategy-övningen valde vi vapen med en `if`/`else if` mitt i `Program.cs`. Det går bra en gång.
Men i ett riktigt spel skapas monster på många ställen: när en nivå startar, när en grotta öppnas, när spelet laddas.
Då finns samma val på flera ställen. Ett nytt monster betyder ändringar överallt, och missar man ett ställe kraschar spelet.

## Lösningen: en fabrik

En fabrik är en klass vars enda jobb är att skapa objekt. Resten av programmet beställer, fabriken bygger.

```csharp
// Hela programmet: Factory, alla monster skapas på ett ställe
List<Monster> wave = new List<Monster>();
wave.Add(MonsterFactory.Create("goblin"));
wave.Add(MonsterFactory.Create("troll"));
wave.Add(MonsterFactory.Create("dragon"));

foreach (Monster monster in wave)
{
    Console.WriteLine($"{monster.Name} ({monster.Health} hälsa): {monster.Roar()}");
}

// Ett okänt monster ger ett fel, så vi fångar det
try
{
    Monster unknown = MonsterFactory.Create("unicorn");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Fel: {ex.Message}");
}

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

class Troll : Monster
{
    public Troll() : base("Troll", 80) { }

    public override string Roar()
    {
        return "GRRRUUMF!";
    }
}

class Dragon : Monster
{
    public Dragon() : base("Drake", 200) { }

    public override string Roar()
    {
        return "ROOOAAAR!";
    }
}

static class MonsterFactory
{
    // Det enda stället i programmet där vi skriver new Goblin(), new Troll() och new Dragon()
    public static Monster Create(string type)
    {
        switch (type)
        {
            case "goblin":
                return new Goblin();
            case "troll":
                return new Troll();
            case "dragon":
                return new Dragon();
            default:
                throw new ArgumentException($"Det finns inget monster som heter '{type}'.");
        }
    }
}
```

```
Goblin (30 hälsa): Hihihi!
Troll (80 hälsa): GRRRUUMF!
Drake (200 hälsa): ROOOAAAR!
Fel: Det finns inget monster som heter 'unicorn'.
```

Lägg märke till:

1. Koden högst upp vet bara att den får en `Monster`. Den vet inte om det är en `Goblin` eller en `Dragon`.
2. Ett nytt monster betyder en ny klass och en ny rad i fabriken. Inget annat.
3. En felaktig beställning ger ett tydligt fel direkt, i stället för konstiga fel långt senare.

`static class` betyder att vi inte behöver skapa en fabrik med `new`. Vi anropar `MonsterFactory.Create(...)` direkt.

## I verkligheten: kunden väljer betalsätt

I en webbshop trycker kunden på en knapp: "Kort" eller "Swish". Programmet får en text och ska skapa rätt betalsätt.
Här skapar fabriken **strategier** från förra passet. Mönster jobbar gärna ihop.

```csharp
// Hela programmet: Factory i verkligheten, kunden väljer betalsätt
string choice = "swish";   // I en riktig app kommer det här från knappen kunden tryckte på

// Tänk på att lägga till Try Catch
IPaymentMethod payment = PaymentFactory.Create(choice);
payment.Pay(249m);

interface IPaymentMethod
{
    void Pay(decimal amount);
}

class CardPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Drar {amount} kr från kortet.");
    }
}

class SwishPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Skickar en Swish-förfrågan på {amount} kr.");
    }
}

static class PaymentFactory
{
    public static IPaymentMethod Create(string choice)
    {
        switch (choice)
        {
            case "card":
                return new CardPayment();
            case "swish":
                return new SwishPayment();
            default:
                throw new ArgumentException($"Okänt betalsätt: {choice}");
        }
    }
}
```

```
Skickar en Swish-förfrågan på 249 kr.
```

## Så känner du igen Factory

> Programmet ska skapa **ett av flera** slags objekt, och valet görs på **flera ställen** eller utifrån text
> (en meny, en fil, en inställning).

Exempel: monster från en nivåfil, betalsätt från en knapp, export som PDF, Word eller text.

Det passar **inte** när det bara finns en sort och den skapas på ett ställe. Då räcker `new`.
