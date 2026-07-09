# Övningar och annat skoj

🔴



# Challenge of the brains

Skriv en klass som hanterar användarnamn

- Om användarnamn inte finns ska den genereras

- Maxlängd 25 tecken

- Förslag på användarnamn ska baseras på det riktiga namnet

- Förslag: 3 första bokstäverna på namn och efternamn (Bruce Willis => BruWil)

- Förslag: 3 första bokstäverna på namn och 3 sista på efternamn (Bruce Willis => Brulis)

- Lägg till 3 slumpmässiga siffror

- Användarnamn får bara innehålla bokstäver och siffor (inga mellanslag)

- Emailadress ska kollas

- Maxlängd 60 tecken

- Namndelen får bara innehålla bokstäver, siffor, punkt och streck

- Får bara ha ett @ symbol

- Domänen måste ha åtminstone en punkt

- Classen ska kunna plocka ut initialer (Bruce Willie => BW)


Klassen ska ha properties för

- (String) Namn

- (String) Efternamn

- (String) Initialer

- (String) Användarnamn

- (List<string>) FörslagPåAnvändarnamn


Dessa ska fyllas ifrån constructorn


När du är klar, lägg upp den på github och bjud in en klasskamrat som får skriva tester till den

Testa din kamrats kod och se om hur mycket ni kan få varandras kod att krascha genom tester


# Challenge of the brains II

Skriv en klass som ska ta


Implementera följande klass


```


public class BankAccount
{
private readonly string m_customerName;
private double m_balance;
```

```

private BankAccount() { }
```

```


public BankAccount(string customerName, double balance)
{
}
```

```

public string CustomerName => m_customerName;
```

```

public double Balance => m_balance;
```

```


public void Debit(double amount)
{
// Implementera utgift
}
```

```


public void Credit(double amount)
{
// Implementera inkomst
}
}
```

```


public static void Main()
{
BankAccount ba = new BankAccount("Mr. Medina", 11.99);
```

```


ba.Credit(5.77);
ba.Debit(11.22);
Console.WriteLine("Current balance is ${0}", ba.Balance);
}
```


När du är klar, lägg upp den på github och bjud in en klasskamrat som får skriva tester till den

Testa din kamrats kod och se om hur mycket ni kan få varandras kod att krascha genom tester

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
