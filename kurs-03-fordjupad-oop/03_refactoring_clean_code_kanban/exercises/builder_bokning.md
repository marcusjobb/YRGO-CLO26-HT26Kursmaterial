# Builder: bygg en bokning steg för steg

**Mål:** Förstå varför en konstruktor med många parametrar är svår att läsa, och bygg en Builder som gör anropet tydligt.

**Tid:** ca 45 minuter.

**Förkunskap:** Klasser, konstruktorer och metoder som returnerar `this`.

**Berättelsen:** Sara ringer Hjulet och vill boka tid. Bokningsappen skapar en `Booking` med sju parametrar. Den som läser anropet måste räkna positioner för att förstå vad som är vad.

## Startkod

```csharp
class Booking
{
    public Booking(string customerName, string licensePlate, string carModel,
                   int hours, bool tireChange, bool wash, string comment)
    {
        CustomerName = customerName;
        LicensePlate = licensePlate;
        CarModel = carModel;
        Hours = hours;
        TireChange = tireChange;
        Wash = wash;
        Comment = comment;
    }

    public string CustomerName { get; }
    public string LicensePlate { get; }
    public string CarModel { get; }
    public int Hours { get; }
    public bool TireChange { get; }
    public bool Wash { get; }
    public string Comment { get; }
}

// Så här skapas en bokning i dag:
Booking booking = new Booking("Sara", "ABC 123", "Volvo V70", 2, true, false, "");
```

## Del 1: 🟢 Läs anropet

Vad betyder `2`, `true` och `false` i anropet ovan? Skriv svaret med egna ord, utan att titta i koden.

<details markdown="1">
<summary>Tips</summary>

Fråga dig vad du skulle behöva kolla i dokumentationen för att förstå varje värde. Om du måste räkna positioner är det ett tecken på att anropet är svårt att läsa.

</details>

## Del 2: 🟡 Skapa en BookingBuilder

**Uppgift:** Skriv en klass `BookingBuilder` med metoderna nedan. Varje metod ska returnera `this` så att anropet kan kedjas.

- `ForCustomer(string customerName)`
- `WithCar(string licensePlate, string carModel)`
- `Hours(int hours)`
- `WithTireChange()`
- `WithWash()`
- `Comment(string comment)`
- `Build()` returnerar en `Booking`

Målet är att det här ska fungera:

```csharp
Booking booking = new BookingBuilder()
    .ForCustomer("Sara")
    .WithCar("ABC 123", "Volvo V70")
    .Hours(2)
    .WithTireChange()
    .Build();
```

<details markdown="1">
<summary>Tips</summary>

Ha ett privat fält per värde i builderen, med ett standardvärde. `WithTireChange()` sätter bara ett fält till `true`. Det behövs ingen parameter.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
class BookingBuilder
{
    private string _customerName = "";
    private string _licensePlate = "";
    private string _carModel = "";
    private int _hours = 1;
    private bool _tireChange = false;
    private bool _wash = false;
    private string _comment = "";

    public BookingBuilder ForCustomer(string customerName)
    {
        _customerName = customerName;
        return this;
    }

    public BookingBuilder WithCar(string licensePlate, string carModel)
    {
        _licensePlate = licensePlate;
        _carModel = carModel;
        return this;
    }

    public BookingBuilder Hours(int hours)
    {
        _hours = hours;
        return this;
    }

    public BookingBuilder WithTireChange()
    {
        _tireChange = true;
        return this;
    }

    public BookingBuilder WithWash()
    {
        _wash = true;
        return this;
    }

    public BookingBuilder Comment(string comment)
    {
        _comment = comment;
        return this;
    }

    // Kontrollen ligger här, på ett ställe. Ingen bokning utan kund.
    public Booking Build()
    {
        if (string.IsNullOrWhiteSpace(_customerName))
        {
            throw new InvalidOperationException("Kund måste anges.");
        }

        return new Booking(_customerName, _licensePlate, _carModel,
                           _hours, _tireChange, _wash, _comment);
    }
}
```

Det går att lösa på fler sätt. Det viktiga är att anropet blir självförklarande och att valideringen finns på ett ställe.

</details>

## Del 3: 🔴 Frivilligt

Lägg till en kontroll i `Build()` så att `Hours` aldrig kan vara 0 eller mindre. Vad ska hända om någon anropar `Hours(-2)`? Ska felet kastas direkt i `Hours()` eller först i `Build()`? Motivera ditt val i en kommentar.

## Reflektion

Skriv tre meningar: Vilket anrop är lättast att läsa, builder eller konstruktor? När skulle du *inte* använda en builder? Vad blev svårast?
