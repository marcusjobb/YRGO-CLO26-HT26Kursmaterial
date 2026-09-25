# Bank — livekodning 2026-09-14

> Det här är den faktiska livekodade versionen, inte den omskrivna referensversionen i `BankAccount.cs` i den här mappen — den blev förvrängd när en tidigare dokumenteringsomgång "förbättrade" koden istället för att återge den. Här är vad som verkligen kördes.

**Fil:** `Program.cs`

Ett bankkonto med ett skyddat saldo — du kan sätta in och ta ut pengar genom metoder, men inte peta i saldot direkt utifrån.

## Klassen: BankAccount

```csharp
class BankAccount
{
    private double saldo = 1000;   // publikt fält — farligt!
    public double Saldo
    {
        get
        {
            return saldo;
        }
        private set
        { // special variabel som heter value
            if (value > 0)
            {
                saldo = value;
            }
            else
            {
                Console.WriteLine("Du har inte råd med det");
            }
        }
    }
    public void Insättning(double saldo)
    {
        if (saldo < 0)
            Console.WriteLine("GTFO!");
        else if (saldo > 20000)
            Console.WriteLine("Du är en skummis!");
        else
            Saldo += saldo;
    }

    public void Uttag(double saldo)
    {
        if (saldo < 0)
            Console.WriteLine("GTFO!");
        else
            Saldo -= saldo;
    }
}
```

`Saldo` är en property med `private set` — utifrån kan man läsa `acc.Saldo`, men bara klassen själv kan ändra det, och bara genom `Insättning()`/`Uttag()`. Kommentaren `// publikt fält — farligt!` syftar på det bakomliggande `saldo`-fältet: om det vore `public` kunde vem som helst sätta det till vad som helst, förbi all kontroll.

Lägg märke till namnkrocken med avsikt: metodparametern heter också `saldo` (litet s) — samma namn som fältet, olika saker. Bra tillfälle att prata om scope och varför C# inte klagar (parametern skuggar fältet inuti metoden).

De utkommenterade raderna i `set` visar ett stopp mitt i tanken — ett `else` som skulle skrivit ut ett felmeddelande om man försökte sätta ett negativt saldo, men som lämnades kvar avstängt. Bra exempel på att livekod inte alltid är städad, och det är okej.

## Användningen

```csharp
Console.WriteLine("Hello, World!");

BankAccount acc = new BankAccount();
acc.Insättning(4500);
Console.WriteLine($"Mitt bankkonto har {acc.Saldo} spänn!");

Console.WriteLine("Jag handlar elprylar för 2500");
acc.Uttag(2500);
Console.WriteLine($"Jag har nu {acc.Saldo} kvar.");

acc.Uttag(-510);
Console.WriteLine("Jag handlar koncertbiljetter för 3500");
acc.Uttag(3500);
Console.WriteLine($"Jag har nu {acc.Saldo} kvar.");
```

`acc.Uttag(-510)` är med med flit — ett negativt uttag ska avvisas (`"GTFO!"`), inte lägga till pengar. Bra att köra igenom och se att skyddet faktiskt håller innan man går vidare.
