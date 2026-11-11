# Observer: exempel med förklaringar

Projekten ligger i `source/repos`: `Designmonster_Observer`, `Designmonster_Observer_Bank`, `Designmonster_Observer_Bank_Event` och `Observer_Live`.

**Idén:** ett objekt berättar när något händer, och alla som prenumererat får veta det. Objektet vet inte vilka lyssnarna är. Frågor för när mönstret passar finns i [flödesschemat](../notes/flodesschema_designmonster.md#observer).

---

## 1. `Designmonster_Observer` (Mira och lyssnarna)

### 1.1 Interfacet för lyssnarna

```csharp
namespace Designmonster_Observer.Interfaces;

public interface IHealthObserver
{
    void OnHealthChanged(string heroName, int health);
}
```

**Vad händer:** Varje lyssnare måste kunna ta emot en förändring i hälsan. Det är allt som `Hero` vet om dem.

**Stil:** Metodnamnet börjar med `On`, vilket är en konvention för händelsehanterare. Den som läser `OnHealthChanged` förstår att det är en reaktion på något som hänt.

### 1.2 `Hero` som berättar

```csharp
public class Hero
{
    public string Name { get; }
    public int Health { get; private set; }

    private readonly List<IHealthObserver> observers = new();

    public void Subscribe(IHealthObserver observer)
    {
        observers.Add(observer);
    }

    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount); // Aldrig under 0

        // Hero vet inte vilka som lyssnar, bara att de vill veta
        foreach (IHealthObserver observer in observers)
        {
            observer.OnHealthChanged(Name, Health);
        }
    }
}
```

**Vad händer:** `Subscribe` lägger till en lyssnare i en lista. `TakeDamage` ändrar hälsan och går sedan igenom listan och säger till alla.

**Stil:**
- `private set` gör att bara `Hero` kan ändra hälsan. Utifrån kan man bara läsa den.
- `readonly` på listan: själva listan byts inte ut, men elementen kan läggas till.
- `Math.Max(0, ...)` gör att hälsan aldrig blir negativ. Kommentaren säger det.

### 1.3 Två lyssnare

```csharp
public class HealthBar : IHealthObserver
{
    public void OnHealthChanged(string heroName, int health)
    {
        // En # per 10 hälsa, alltid lika bred (-10 fyller ut till 10 tecken)
        string bar = new string('#', health / 10);
        Console.WriteLine($"[{bar,-10}] {heroName}: {health}");
    }
}

public class SoundPlayer : IHealthObserver
{
    public void OnHealthChanged(string heroName, int health)
    {
        if (health < 30)
        {
            Console.WriteLine("♪ Varningsljud: låg hälsa!");
        }
    }
}
```

**Vad händer:** `HealthBar` skriver ut en stapel. `SoundPlayer` skriver bara något när hälsan är låg. Båda är lyssnare, men gör olika saker.

**Stil:**
- `{bar,-10}` i interpolerad sträng: fyll ut med mellanslag till 10 tecken. Stapeln blir lika bred oavsett längd.
- `SoundPlayer` har inget tillstånd. Den reagerar bara på värdet den får.

### 1.4 Programmet

```csharp
Hero mira = new Hero("Mira", 100);
mira.Subscribe(new HealthBar());
mira.Subscribe(new SoundPlayer());

mira.TakeDamage(30);
mira.TakeDamage(50);

// Hero känner inte till HealthBar eller SoundPlayer. Ny lyssnare = ny klass + Subscribe.
```

**Vad händer:** Mira prenumererar med två lyssnare, och tar sedan skada två gånger.

**Stil:** Kommentaren på sista raden säger vad som är poängen. Ingen ändring i `Hero` krävs när en tredje lyssnare kommer.

---

## 2. `Designmonster_Observer_Bank` (interface, loggning)

Här loggar ett bankkonto alla händelser. Det finns tre händelser: insättning, uttag och uttag som misslyckas.

### 2.1 Interfacet med tre metoder

```csharp
public interface IBankObserver
{
    void OnDeposit(string account, double amount, double newBalance);
    void OnWithdraw(string account, double amount, double newBalance);
    void OnWithdrawFailed(string account, double amount, double balance, string reason);
}
```

**Vad händer:** Varje händelse har sin egen metod. Lyssnaren får de värden den behöver för just den händelsen.

**Stil:** `OnWithdrawFailed` har fler parametrar än de andra, eftersom den behöver veta varför det misslyckades. Det är en medveten skillnad.

### 2.2 `BankAccount` som skickar händelser

```csharp
public void Withdraw(double amount)
{
    if (amount <= 0 || amount > Balance)
    {
        string reason = amount <= 0 ? "Beloppet måste vara positivt" : "Otillräckligt saldo";

        foreach (IBankObserver observer in observers)
        {
            observer.OnWithdrawFailed(Name, amount, Balance, reason);
        }
        return;
    }

    Balance -= amount;

    foreach (IBankObserver observer in observers)
    {
        observer.OnWithdraw(Name, amount, Balance);
    }
}
```

**Vad händer:** Om uttaget inte går igenom skickas en händelse om det, med en anledning. `return` gör att kontot inte ändras. Om det går bra ändras saldot och en annan händelse skickas.

**Stil:**
- Villkoret är ett `||`: antingen negativt belopp eller för lite pengar. Då slipper man två `if` som gör samma sak.
- `reason` är en variabel med en ternary. Det är en enkel jämförelse, så det får vara en rad.
- Det finns två `foreach`-loopar. Den ena är för misslyckanden och den andra för lyckade uttag. Det är lätt att se vilken händelse som skickas var.

### 2.3 `BankLogger`

```csharp
public class BankLogger : IBankObserver
{
    public void OnDeposit(string account, double amount, double newBalance)
    {
        Log($"INSÄTTNING   {account}: +{amount} → saldo {newBalance}");
    }

    // ...OnWithdraw och OnWithdrawFailed följer samma form

    private static void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
}
```

**Vad händer:** Varje metod formaterar en rad. `Log` lägger på en tidsstämpel och skriver ut raden.

**Stil:**
- `private static` för hjälpmetoden `Log`: den använder inget objekttillstånd, så den behöver inte vara en instansmetod.
- Formatet `HH:mm:ss` är en tidsstämpel. Pilen `→` gör utskriften lättare att läsa än ett `->`.

### 2.4 Programmet

```csharp
BankAccount konto = new BankAccount("Mira");
konto.Subscribe(new BankLogger());

konto.Deposit(100);
konto.Withdraw(30);
konto.Withdraw(200);   // misslyckas: för lite pengar
konto.Deposit(50);
konto.Withdraw(120);
```

**Vad händer:** Loggern prenumererar. Det uttag som misslyckas skrivs ut med anledningen, och resten skrivs ut som vanliga händelser.

---

## 3. `Designmonster_Observer_Bank_Event` (events i C#)

Samma bankkonto, men nu med C#:s inbyggda `event` i stället för ett eget interface.

### 3.1 Argumenten till händelserna

```csharp
public class BankEventArgs : EventArgs
{
    public string Account { get; }
    public double Amount { get; }
    public double Balance { get; }
    public string? Reason { get; }

    public BankEventArgs(string account, double amount, double balance, string? reason = null)
    {
        Account = account;
        Amount = amount;
        Balance = balance;
        Reason = reason;
    }
}
```

**Vad händer:** Ett paket med information som skickas med varje händelse. Alla händelser får samma typ, och `Reason` är bara ifylld när något misslyckats.

**Stil:**
- Ärver från `EventArgs`, som är .NET:s standardbas för händelseargument.
- `string?` betyder att `Reason` får vara `null`. Det är tydligt för den som läser.
- Standardvärdet `= null` gör att konstruktorn kan anropas utan anledning.

### 3.2 Händelserna i `BankAccount`

```csharp
public class BankAccount
{
    public event EventHandler<BankEventArgs>? Deposited;
    public event EventHandler<BankEventArgs>? Withdrawn;
    public event EventHandler<BankEventArgs>? WithdrawFailed;

    public void Deposit(double amount)
    {
        // ...
        Balance += amount;
        Deposited?.Invoke(this, new BankEventArgs(Name, amount, Balance));
    }
}
```

**Vad händer:** Tre händelser deklareras. `Deposit` skickar en av dem när insättningen är klar.

**Stil:**
- `event` gör att utifrån bara `+=` och `-=` går att använda. Ingen kan anropa eventet direkt, bara kontot.
- `?.Invoke(...)` är en nullkontroll och ett anrop i ett. Om ingen prenumererar är eventet `null`, och då händer ingenting.
- `this` är första argumentet, avsändaren. Det är .NET-standarden, så lyssnaren kan se vem som skickade.

### 3.3 Loggern: metoder med standardsignatur

```csharp
public class BankLogger
{
    public void OnDeposited(object? sender, BankEventArgs e)
    {
        Log($"INSÄTTNING   {e.Account}: +{e.Amount} → saldo {e.Balance}");
    }

    public void OnWithdrawFailed(object? sender, BankEventArgs e)
    {
        Log($"UTTAG MISSLYCKADES  {e.Account}: försökte ta ut {e.Amount}, saldo {e.Balance} ({e.Reason})");
    }
}
```

**Vad händer:** Samma utskrifter som i interface-versionen, men nu tar varje metod emot ett paket i stället för separata värden.

**Stil:**
- Signaturen `(object? sender, BankEventArgs e)` är samma som `EventHandler<T>` kräver. Då kan metoden kopplas direkt till eventet.
- Ingen interface behövs. Loggern är en vanlig klass. Det är en skillnad mot föregående projekt.

### 3.4 Programmet

```csharp
BankAccount konto = new BankAccount("Mira");
BankLogger logger = new BankLogger();

// += är prenumerationen. Kontot vet inte vem som lyssnar.
konto.Deposited += logger.OnDeposited;
konto.Withdrawn += logger.OnWithdrawn;
konto.WithdrawFailed += logger.OnWithdrawFailed;
```

**Vad händer:** Tre prenumerationer, en per händelse. Kontot behöver inte ha ett interface för loggern.

**Stil:** `+=` är det vanliga sättet att prenumerera på events i C#. Samma syntax används för knappar i Windows Forms, vilket är en bra koppling för den som redan jobbat med det.

---

## 4. `Observer_Live` (livekodning, bankkonto med interface)

Det här är bankexemplet från livekodningen. Det liknar `Designmonster_Observer_Bank`, men har några skillnader som är värda att jämföra.

### 4.1 Interfacet

```csharp
internal interface ICashObserver
{
    void OnMoneyChange(string account, double newValue);
}
```

**Vad händer:** En enda metod för alla händelser. Lyssnaren får bara en text och ett värde, och får själv avgöra vad som hänt.

**Stil:** `internal` i stället för `public`. Det betyder att klassen bara syns inne i projektet, vilket är rimligt för ett litet övningsprojekt.

### 4.2 `BankAccount`

```csharp
public void Withdraw(double amount)
{
    Console.WriteLine($"Tar ut {amount}:-");
    if (amount < 0) return;
    if (amount > cash) return;
    cash -= amount;

    foreach(var observer in peepers)
    {
        observer.OnMoneyChange("Cash withdrawn", cash);
    }
}
```

**Vad händer:** Skriver ut att uttaget görs, och kontrollerar sedan beloppet. Om det är fel returneras utan något mer. Lyssnarna får bara veta om uttaget lyckades.

**Stil och värt att lägga märke till:**
- `Console.WriteLine` ligger före kontrollen. Ett uttag som misslyckas skrivs alltså ut som "Tar ut 200:-" utan att något mer händer. Det är ett bra exempel på varför ett misslyckat uttag borde ge en händelse, som i bankexemplet ovan.
- `if (...) return;` utan klammerparenteser är kort, men bara för en sats. Det är lätt att missa om man lägger till en rad senare.
- `foreach(var observer ...)` har inget mellanslag efter `foreach`. Det är en stilfråga, men i resten av projekten skrivs det med mellanslag.

### 4.3 Programmet

```csharp
using Observer_Live;

Console.WriteLine("Hello, observer!");

BankAccount account = new();
account.Subscribe(new MoneyCheck());

account.Deposit(100);
account.Withdraw(200);
Console.WriteLine("Saldo "+account.Cash);
account.Withdraw(50);
```

**Vad händer:** Insättning 100, ett uttag på 200 som misslyckas, och ett uttag på 50 som lyckas.

**Stil:** `new()` utan typnamn och `"Saldo "+account.Cash` utan mellanslag runt `+`. Samma sak som ovan: en stilfråga, men det är bra att välja en och hålla fast vid den.

---

## Vad är skillnaden mellan de tre bankvarianterna?

| | `Observer_Live` | `Designmonster_Observer_Bank` | `Designmonster_Observer_Bank_Event` |
|---|---|---|---|
| Hur lyssnaren kopplas | `Subscribe(...)` | `Subscribe(...)` | `+=` på event |
| Misslyckade uttag | Syns bara i utskrift | Egen händelse, med anledning | Egen händelse, med anledning |
| Typ för lyssnaren | Interface | Interface | Vanlig klass med metoder |
| Händelsedata | Två värden | Separata parametrar | Ett `BankEventArgs`-objekt |
