# Extra övning: arv, abstrakta klasser och interfaces

**Mål:** Öva på när en abstrakt klass och när ett interface passar, och se hur arv och interface låter samma kod hantera olika typer.

**Tid:** ca 90 minuter, tre små projekt.

**Förkunskap:** Klasser, arv och `override` från kursen. Builder och Factory från övningarna i veckan.

Det här är extra övning för att befästa arv. Ingen inlämning, men kör gärna varje projekt och skriv en mening om vad du lärt dig.

---

## Projekt A: Djurparken 🟡

Parken har hundar, katter och fåglar. Alla djur har ett namn och ett läte, men lätet låter olika.

### 🟢 Läs och kör
Startkoden:

```csharp
abstract class Animal
{
    public string Name { get; }

    protected Animal(string name)
    {
        Name = name;
    }

    public abstract string Sound();

    public void Introduce()
    {
        Console.WriteLine($"{Name} säger {Sound()}");
    }
}

class Dog : Animal
{
    public Dog(string name) : base(name) { }
    public override string Sound() => "Voff";
}

class Cat : Animal
{
    public Cat(string name) : base(name) { }
    public override string Sound() => "Mjau";
}
```

Vad händer om du försöker skriva `new Animal("Blomma")`? Varför?

<details markdown="1">
<summary>Tips</summary>

En abstrakt klass är en mall. Den säger vad alla djur måste kunna, men den säger inte hur. Titta på vad som står `abstract` och vad som står `override`.

</details>

### 🟡 Lägg till en fågel
Skriv en klass `Bird` som ärver `Animal`. Den har också en metod `Fly()`, men inte alla fåglar kan flyga. Lägg `Fly()` i `Bird` och fundera på om en `Penguin` ska ärva `Bird`.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
class Bird : Animal
{
    public Bird(string name) : base(name) { }
    public override string Sound() => "Kvitter";
    public virtual void Fly() => Console.WriteLine($"{Name} flyger");
}

class Penguin : Bird
{
    public Penguin(string name) : base(name) { }
    public override void Fly() => Console.WriteLine($"{Name} kan inte flyga, men simmar");
}
```

Det är ett arvsproblem: `Penguin` ärver något som inte stämmer. Ett interface löser det i nästa steg.

</details>

### 🔴 Flygande djur som interface
Skapa `interface IFlyer { void Fly(); }`. Låt endast de djur som kan flyga implementera det. Skriv en metod `ShowAllFliers(IEnumerable<Animal> animals)` som skriver ut alla som kan flyga.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
interface IFlyer
{
    void Fly();
}

class Sparrow : Animal, IFlyer
{
    public Sparrow(string name) : base(name) { }
    public override string Sound() => "Kvitter";
    public void Fly() => Console.WriteLine($"{Name} flyger");
}

class Penguin : Animal
{
    public Penguin(string name) : base(name) { }
    public override string Sound() => "Råp";
}

// I Program:
static void ShowAllFliers(IEnumerable<Animal> animals)
{
    foreach (Animal animal in animals)
    {
        if (animal is IFlyer flyer)
        {
            flyer.Fly();
        }
    }
}
```

`Penguin` ärver inte längre något den inte ska ha. Det är skillnaden mellan "är ett" (arv) och "kan göra" (interface).

</details>

---

## Projekt B: Uthyrning av fordon 🟡

En uthyrningsfirma har bilar och cyklar. Båda hyrs ut per dag, men de räknar pris och lämningsregler olika.

### 🟢 Abstrakt basklass
Skapa `abstract class Vehicle` med en egenskap `Name` och en abstrakt metod `decimal PricePerDay()`. Skriv två subklasser: `Car` (500 kr per dag) och `Bike` (150 kr per dag).

<details markdown="1">
<summary>Tips</summary>

Skriv `Vehicle` först, och låt kompilatorn säga vad som saknas i subklasserna. Det är ett snabbt sätt att se vad en abstrakt klass kräver.

</details>

### 🟡 Hyr flera dagar
Lägg till metoden `decimal Rent(int days)` i `Vehicle` som returnerar `PricePerDay() * days`. Varför är det bra att den metoden *inte* är abstrakt?

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
abstract class Vehicle
{
    public string Name { get; }

    protected Vehicle(string name)
    {
        Name = name;
    }

    public abstract decimal PricePerDay();

    // Inte abstrakt: alla fordon räknar pris på samma sätt, bara dagspriset skiljer.
    public decimal Rent(int days)
    {
        return PricePerDay() * days;
    }
}

class Car : Vehicle
{
    public Car(string name) : base(name) { }
    public override decimal PricePerDay() => 500m;
}

class Bike : Vehicle
{
    public Bike(string name) : base(name) { }
    public override decimal PricePerDay() => 150m;
}
```

Det som är lika ligger i basklassen. Det som skiljer sig är abstrakt.

</details>

### 🔴 Interface för återlämning
Skapa `interface IReturnable { void Return(); }`. Låt `Car` implementera det med en kontroll av mätarställning, men låt `Bike` inte göra det. Skriv en metod som tar `IReturnable` som parameter och skriver "Återlämnad" när den anropas. Kan du anropa den med en `Bike`?

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
interface IReturnable
{
    void Return();
}

class Car : Vehicle, IReturnable
{
    public Car(string name) : base(name) { }
    public override decimal PricePerDay() => 500m;
    public void Return() => Console.WriteLine($"{Name} återlämnad, mätarställning kontrollerad");
}

static void Checkin(IReturnable item)
{
    item.Return();
}

// Checkin(new Car("Volvo V70"));   // går bra
// Checkin(new Bike("Crescent"));   // fel: Bike är inte IReturnable
```

Kompilatorn hindrar dig från att anropa `Checkin` med en cykel. Det är ett interface som gör sitt jobb.

</details>

---

## Projekt C: Betalningsmetoder 🟢

En kassa tar emot betalning med kort, faktura eller mobilbetalning. Kassans kod ska inte bry sig om vilken metod det är, bara att den kan betala.

### 🟢 Gemensamt kontrakt
Skapa `interface IPaymentMethod { void Pay(decimal amount); }`. Skriv tre klasser som implementerar det: `CardPayment`, `InvoicePayment` och `MobilePayment`. Varje klass skriver ut sitt eget meddelande.

<details markdown="1">
<summary>Tips</summary>

Ett interface säger *vad* som ska kunna göras. Varje klass bestämmer *hur*. Om en klass inte kan implementera metoden har du fel interface.

</details>

### 🟢 Kassan
Skriv en klass `Checkout` med metoden `Process(IPaymentMethod method, decimal amount)`. Den ska anropa `Pay` och skriva ut "Kvitto" efteråt. (En metod får inte heta likadant som sin klass.) Lägg märke till att `Checkout` inte vet vilken betalningsmetod som används.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
class Checkout
{
    public void Process(IPaymentMethod method, decimal amount)
    {
        method.Pay(amount);
        Console.WriteLine($"Kvitto: {amount} kr");
    }
}

class Program
{
    static void Main()
    {
        var checkout = new Checkout();
        checkout.Process(new CardPayment(), 450m);
        checkout.Process(new InvoicePayment(), 1200m);
        checkout.Process(new MobilePayment(), 80m);
    }
}
```

`Checkout` känner bara till interfacet. Om du lägger till en ny betalningsmetod behöver `Checkout` inte ändras.

</details>

### 🟡 Lägg till en ny metod utan att ändra kassan
Lägg till `CryptoPayment` som implementerar `IPaymentMethod`. Ändra inget i `Checkout`. Fungerar det? Om inte, varför?

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
class CryptoPayment : IPaymentMethod
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Betalar {amount} kr med kryptovaluta");
    }
}
```

Det fungerar utan ändringar i `Checkout`. Om det inte fungerade hade du troligen lagt logiken för `CryptoPayment` i `Checkout` i stället för i klassen själv.

</details>

---

## Reflektion

Skriv tre meningar:
1. När skulle du välja en abstrakt klass, och när ett interface?
2. Vilket projekt var svårast att se "är ett" kontra "kan göra"?
3. Vad skulle du ha gjort annorlunda om du bara hade haft arv och inget interface?

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
