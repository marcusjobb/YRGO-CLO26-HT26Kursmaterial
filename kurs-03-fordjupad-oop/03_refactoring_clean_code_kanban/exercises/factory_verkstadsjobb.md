# Factory: en fabrik för verkstadsjobb

**Mål:** Förstå när en Factory hjälper, och skriv en som väljer rätt subklass utan att resten av koden behöver veta vilken.

**Tid:** ca 45 minuter.

**Förkunskap:** Arv, abstrakta klasser och `override`.

**Berättelsen:** På Hjulet finns fyra typer av jobb. Varje jobb räknar ut sitt pris och sin beskrivning på sitt eget sätt. Receptionen tar emot en jobbtyp som text, till exempel `"däck"`, och vill få tillbaka rätt jobb.

## Startkod

```csharp
abstract class WorkshopJob
{
    public abstract string Description { get; }
    public abstract decimal Price { get; }

    public void Print()
    {
        Console.WriteLine($"{Description}: {Price} kr");
    }
}

class TireChange : WorkshopJob
{
    public override string Description => "Däckbyte";
    public override decimal Price => 800m;
}

class OilChange : WorkshopJob
{
    public override string Description => "Oljebyte";
    public override decimal Price => 1200m;
}

class BrakeService : WorkshopJob
{
    public override string Description => "Bromsservice";
    public override decimal Price => 2500m;
}

class CarWash : WorkshopJob
{
    public override string Description => "Tvätt";
    public override decimal Price => 250m;
}
```

Så här skapas jobben i dag:

```csharp
WorkshopJob job = new TireChange();
job.Print();
```

## Del 1: 🟢 Vad vet anroparen?

I startkoden väljer anroparen klass själv med `new TireChange()`. Vad händer med koden om Hjulet lägger till ett nytt jobb, till exempel `Lackering`? Hur många rader behöver ändras, och var?

<details markdown="1">
<summary>Tips</summary>

Tänk på var `new TireChange()` finns. Det är svårt att hitta alla ställen där en subklass används, om varje ställe skapar den själv.

</details>

## Del 2: 🟡 Skriv en JobFactory

**Uppgift:** Skriv en `static class JobFactory` med metoden `Create(string jobType)`. Den returnerar rätt `WorkshopJob`:

| Inmatning | Ger |
|---|---|
| `"däck"` | `TireChange` |
| `"olja"` | `OilChange` |
| `"broms"` | `BrakeService` |
| `"tvätt"` | `CarWash` |
| något annat | ett `ArgumentException` med ett tydligt meddelande |

Testa med: `JobFactory.Create("broms").Print();`

<details markdown="1">
<summary>Tips</summary>

En `switch`-uttryck med `_ =>` som sista fall passar bra här. Kasta felet i `_`-grenen, inte i ett `if` som är svårt att läsa.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
static class JobFactory
{
    public static WorkshopJob Create(string jobType) => jobType switch
    {
        "däck"  => new TireChange(),
        "olja"  => new OilChange(),
        "broms" => new BrakeService(),
        "tvätt" => new CarWash(),
        _       => throw new ArgumentException($"Okänd jobbtyp: {jobType}")
    };
}
```

Anroparen behöver nu bara känna till `JobFactory` och `WorkshopJob`. Den vet inte vilka subklasser som finns.

</details>

## Del 3: 🔴 Utöka utan att ändra anroparen

Lägg till en ny subklass, `Inspection` ("Besiktning", 600 kr), och lägg till den i fabriken. Skriv ner vilka filer du ändrade. Hur många var det? Vad hade behövts om det inte fanns någon fabrik?

## Reflektion

Skriv tre meningar: När är det onödigt att ha en fabrik? Vad hände med anropet `new TireChange()` när du flyttade det? Vad är skillnaden mellan en Factory och en Builder?
