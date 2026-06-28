---

title: Bankkonto
author: Marcus Ackre Medina
type: article
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/CLO22/Docs/oop/events/exempel.md"
description: "Vi ska skapa ett bankkonto där vi kan sätta in pengar och ta ut pengar. Ett bankkonto är en vanlig komponent i finansiella system och används för att hantera insättningar, uttag och saldo."
tags: ["bankkonto", "conditions", "csharp", "exempel", "git", "oop"]
week_fit: []
---

# Bankkonto

🟢


Vi ska skapa ett bankkonto där vi kan sätta in pengar och ta ut pengar. Ett bankkonto är en vanlig komponent i finansiella system och används för att hantera insättningar, uttag och saldo.

## Beskrivning

Vårt bankkonto kommer att ha flera events som informerar oss om olika händelser som inträffar på kontot. Dessa events inkluderar:

- Insättning: När pengar sätts in på kontot.
- Uttag: När pengar tas ut från kontot.
- Saldo mindre än 0: När saldot på kontot blir mindre än 0.
- Insättning med felaktig summa: När en insättning görs med en ogiltig summa (t.ex. 0 eller negativt belopp).
- Uttag med felaktig summa: När ett uttag görs med en ogiltig summa.

Vi kommer också att ha en särskild händelse för insättningar över en viss gräns, till exempel 15000 kr. Denna händelse kan användas för att varna för eventuell kriminell aktivitet.

För att implementera detta bankkonto använder vi C# och skapar en klass som heter "Account". Klassen har egenskapen "Balance" för att hålla reda på kontots saldo, samt olika events för att rapportera händelser till intresserade lyssnare.

Här är den C#-koden som implementerar bankkontot:

```csharp
public class Account
{
    public event EventHandler<DepositEventArgs> Deposit;
    public event EventHandler<WithdrawEventArgs> Withdraw;
    public event EventHandler<BalanceBelowZeroEventArgs> BalanceBelowZero;
    public event EventHandler<DepositInvalidAmountEventArgs> DepositInvalidAmount;
    public event EventHandler<WithdrawInvalidAmountEventArgs> WithdrawInvalidAmount;
    public event EventHandler<DepositAboveLimitEventArgs> DepositAboveLimit;

    public int Balance { get; private set; }

    public void Deposit(int amount)
    {
        if (amount <= 0)
        {
            DepositInvalidAmount?.Invoke(this, new DepositInvalidAmountEventArgs(amount));
            return;
        }

        if (amount > 15000)
        {
            DepositAboveLimit?.Invoke(this, new DepositAboveLimitEventArgs(amount));
        }

        Balance += amount;
        Deposit?.Invoke(this, new DepositEventArgs(amount));
    }

    public void Withdraw(int amount)
    {
        if (amount <= 0)
        {
            WithdrawInvalidAmount?.Invoke(this, new WithdrawInvalidAmountEventArgs(amount));
            return;
        }

        if (Balance - amount < 0)
        {
            BalanceBelowZero?.Invoke(this, new BalanceBelowZeroEventArgs(amount));
            return;
        }

        Balance -= amount;
        Withdraw?.Invoke(this, new WithdrawEventArgs(amount));
    }
}

public class DepositEventArgs : EventArgs
{
    public int Amount { get; }

    public DepositEventArgs(int amount)
    {
        Amount = amount;
    }
}

public class WithdrawEventArgs : EventArgs
{
    public int Amount { get; }

    public WithdrawEventArgs(int amount)
    {
        Amount = amount;
    }
}

public class BalanceBelowZeroEventArgs : EventArgs
{
    public int Amount { get; }

    public BalanceBelowZeroEventArgs(int amount)
    {
        Amount = amount;
    }
}

public class DepositInvalidAmountEventArgs : EventArgs
{
    public int Amount { get; }

    public DepositInvalidAmountEventArgs(int amount)
    {


        Amount = amount;
    }
}

public class WithdrawInvalidAmountEventArgs : EventArgs
{
    public int Amount { get; }

    public WithdrawInvalidAmountEventArgs(int amount)
    {
        Amount = amount;
    }
}

public class DepositAboveLimitEventArgs : EventArgs
{
    public int Amount { get; }

    public DepositAboveLimitEventArgs(int amount)
    {
        Amount = amount;
    }
}

public static class Program
{
    public static void Main()
    {
        var account = new Account();
        account.Deposit += Account_Deposit;
        account.Withdraw += Account_Withdraw;
        account.BalanceBelowZero += Account_BalanceBelowZero;
        account.DepositInvalidAmount += Account_DepositInvalidAmount;
        account.WithdrawInvalidAmount += Account_WithdrawInvalidAmount;
        account.DepositAboveLimit += Account_DepositAboveLimit;

        account.Deposit(1000);
        account.Withdraw(500);
        account.Withdraw(600);
        account.Deposit(-100);
        account.Withdraw(-100);
        account.Deposit(20000);
    }

    private static void Account_DepositAboveLimit(object sender, DepositAboveLimitEventArgs e)
    {
        Console.WriteLine($"Insättning på {e.Amount} kr är över gränsen");
        Console.WriteLine("Ring SÄPO!");
    }

    private static void Account_WithdrawInvalidAmount(object sender, WithdrawInvalidAmountEventArgs e)
    {
        Console.WriteLine($"Uttag på {e.Amount} kr är inte tillåtet");
    }

    private static void Account_DepositInvalidAmount(object sender, DepositInvalidAmountEventArgs e)
    {
        Console.WriteLine($"Insättning på {e.Amount} kr är inte tillåtet");
    }

    private static void Account_BalanceBelowZero(object sender, BalanceBelowZeroEventArgs e)
    {
        Console.WriteLine($"Uttag på {e.Amount} kr är inte tillåtet då det skulle sätta saldo under 0");
    }

    private static void Account_Withdraw(object sender, WithdrawEventArgs e)
    {
        Console.WriteLine($"Uttag på {e.Amount} kr");
    }

    private static void Account_Deposit(object sender, DepositEventArgs e)
    {
        Console.WriteLine($"Insättning på {e.Amount} kr");
    }
}
```

I det här exemplet skapas en instans av klassen "Account" och olika händelselyssnare kopplas till dess events. Därefter utförs några operationer på kontot, som insättningar och uttag, vilket resulterar i att relevanta events triggas och meddelanden skrivs ut till konsolen.

Detta är bara en grundläggande implementation av ett bankkonto i C#. Beroende på användningsområdet kan det finnas ytterligare funktioner och logik som behöver läggas till.

## Slutsats

I denna artikel har vi skapat ett bankkonto i C# och implementerat funktioner för att sätta in pengar, ta ut pengar och hantera olika händelser som kan inträffa på kontot. Att förstå hur man implementerar ett bankkonto är viktigt för att kunna bygga robusta och säkra finansiella system.

## TL;DR-sammanfattning:

I denna artikel har vi skapat ett bankkonto i C# och visat hur man kan sätta in pengar

, ta ut pengar och hantera olika händelser som kan inträffa på kontot. En grundläggande förståelse av bankkonton är viktig för att bygga finansiella system.

## Obligatorisk Dad-joke:

Varför ville bankrånaren bli poet?

För att han ville stjäla pennor och dikter.

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
