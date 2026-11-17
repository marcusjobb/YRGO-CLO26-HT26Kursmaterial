# Strategy: samma uppgift, flera sätt

Läsversion av första passet om designmönster. Övning: `exercises/ovning_strategy.md`.

**Efter det här kan du:** känna igen när Strategy passar, och byta beteende på ett objekt utan att ändra klassen.

## Vad är ett designmönster?

Ett designmönster är ett bra sätt att lösa ett vanligt problem, som har fått ett namn.
Det är inte en regel. Det är ett verktyg du kan ta fram när problemet dyker upp.

Namnet är halva värdet. Säger du "här passar Strategy" på jobbet förstår alla direkt hur koden är tänkt.

## Problemet

Mira ska kunna slåss med olika vapen. Det första man skriver ser ofta ut så här:

```csharp
// Hela programmet: utan Strategy (så här börjar det ofta)
Hero mira = new Hero("Mira", "sword");
Console.WriteLine(mira.Attack());

mira.Weapon = "bow";
Console.WriteLine(mira.Attack());

class Hero
{
    public string Name;
    public string Weapon;

    public Hero(string name, string weapon)
    {
        Name = name;
        Weapon = weapon;
    }

    public string Attack()
    {
        // Varje nytt vapen = en ny gren här. Hero-klassen växer och växer.
        if (Weapon == "sword")
        {
            return $"{Name} svingar svärdet!";
        }
        else if (Weapon == "bow")
        {
            return $"{Name} skjuter en pil!";
        }
        else
        {
            return $"{Name} vet inte vad hon ska göra.";
        }
    }
}
```

Det fungerar. Men med tio vapen blir `Attack()` lång, och varje nytt vapen betyder att vi öppnar `Hero` igen.
`Hero` borde handla om hjälten, inte om alla vapen i hela spelet.

## Lösningen: vapnet blir en egen klass

Vi gör ett `interface` som lovar att man kan attackera. Varje vapen blir en egen klass som håller löftet.
`Hero` får ett fält av interfacets typ och frågar vapnet när det är dags att attackera.

```csharp
// Hela programmet: Strategy, hjälten byter vapen
Hero mira = new Hero("Mira", new SwordAttack());
Console.WriteLine(mira.Attack());

mira.Weapon = new BowAttack();       // En rad, och Mira slåss på ett nytt sätt
Console.WriteLine(mira.Attack());

mira.Weapon = new FireballAttack();
Console.WriteLine(mira.Attack());

// Strategin: ett löfte om att kunna attackera
interface IAttackStrategy
{
    string Attack(string attackerName);
}

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

class FireballAttack : IAttackStrategy
{
    public string Attack(string attackerName)
    {
        return $"{attackerName} kastar ett eldklot! (15 skada)";
    }
}

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

```
Mira svingar svärdet! (10 skada)
Mira skjuter en pil på håll! (7 skada)
Mira kastar ett eldklot! (15 skada)
```

Lägg märke till två saker:

1. Mira byter vapen med **en rad**, medan programmet kör.
2. `FireballAttack` lades till **utan att `Hero` ändrades**. Vi lade till ny kod, vi rörde inte gammal kod som fungerade.

## I verkligheten: kassan i en webbshop

En webbshop tar betalt med kort, Swish eller faktura. Kassan ska inte behöva veta hur varje betalsätt fungerar.

```csharp
// Hela programmet: Strategy i verkligheten, betalning i en webbshop
Checkout checkout = new Checkout(new CardPayment());
checkout.Pay(249m);

checkout.PaymentMethod = new SwishPayment();
checkout.Pay(249m);

checkout.PaymentMethod = new InvoicePayment();
checkout.Pay(249m);

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

class InvoicePayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Skickar en faktura på {amount} kr, betala inom 30 dagar.");
    }
}

class Checkout
{
    public IPaymentMethod PaymentMethod;

    public Checkout(IPaymentMethod paymentMethod)
    {
        PaymentMethod = paymentMethod;
    }

    public void Pay(decimal amount)
    {
        PaymentMethod.Pay(amount);
    }
}
```

```
Drar 249 kr från kortet.
Skickar en Swish-förfrågan på 249 kr.
Skickar en faktura på 249 kr, betala inom 30 dagar.
```

## Så känner du igen Strategy

> Samma uppgift kan göras på **flera sätt**, och du vill kunna **byta** sätt.

Exempel: vapen i ett spel, betalsätt, fraktsätt, rabatter, sätt att sortera.
Ser du en lång `if`/`else if` som väljer *hur* något ska göras? Där kan Strategy passa.

Det passar **inte** om det bara finns ett sätt och inget tyder på att det kommer fler. Då är en vanlig metod enklast.
