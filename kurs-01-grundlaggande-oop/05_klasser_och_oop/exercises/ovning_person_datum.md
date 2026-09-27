# Övning — Personen och dagräknaren 📅

🔴 Utmaning

> Fastnar du i mer än 20 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Kommunens folkbokföringskontor vill ha ett enkelt system för att hålla koll på invånare. De behöver inte bara veta vem någon är — de behöver kunna räkna ut hur gammal de är och hur många dagar de levt. Exakt, inte ungefär.

Du ska bygga det med **två klasser** som samarbetar.

---

## Vad gäller för den här övningen

- [ ] Kan skriva en klass med privata fält och properties
- [ ] Kan skriva en hjälpklass med metoder som tar emot ett objekt som parameter
- [ ] Förstår hur `DateTime` används för datumräkning

---

## Uppgift

### Klass 1 — `Person`

**Privata fält:**
- `_förnamn` (string)
- `_efternamn` (string)
- `_födelsedatum` (DateTime)

**Properties** (publik get, privat set):
- `FullständigtNamn` — returnerar förnamn och efternamn ihopsatta
- `Födelsedatum`

**Konstruktor** som tar in förnamn, efternamn och födelsedatum

---

### Klass 2 — `DateCalc`

En **hjälpklass** utan egna fält — bara metoder.

**Metoder:**
- `GetDays(Person person)` — returnerar hur många dagar personen levt fram till idag
- `GetAge(Person person)` — returnerar personens faktiska ålder i hela år

---

### I `Main()`

- Skapa minst tre personer med olika födelseår — en av dem är `Nils Sjöberg`, född 13 december 1989
- Skapa ett `DateCalc`-objekt
- Skriv ut varje persons namn, ålder och antal levda dagar

---

## Exempeloutput

```
Anna Svensson — 34 år — 12 541 dagar
Nils Sjöberg — 36 år — 13 423 dagar
Priya Nilsson — 8 år — 3 102 dagar
```

---

## Tips

**`DateTime` — skapa ett datum:**

```csharp
DateTime datum = new DateTime(1989, 12, 13);  // år, månad, dag
```

**Dagens datum:**

```csharp
DateTime idag = DateTime.Today;
```

**Räkna dagar mellan två datum:**

Subtrahera — resultatet är en `TimeSpan`. Använd `.Days` för hela dagar som `int`.

```csharp
TimeSpan skillnad = DateTime.Today - person.Födelsedatum;
int dagar = skillnad.Days;
```

**Ålder i hela år — fallgropen:**

`DateTime.Today.Year - person.Födelsedatum.Year` ger fel svar om personen inte haft sin födelsedag än i år. Till exempel: om Nils fyller år i december och det är september — då säger den 37, men rätt svar är 36.

Korrekt sätt:

```csharp
int ålder = DateTime.Today.Year - person.Födelsedatum.Year;
if (DateTime.Today < person.Födelsedatum.AddYears(ålder))
    ålder--;
```

---

## Lösningsförslag

<details>
<summary>Klicka för att visa lösningen</summary>

```csharp
class Person
{
    private string _förnamn;
    private string _efternamn;

    public string FullständigtNamn { get; private set; }
    public DateTime Födelsedatum { get; private set; }

    public Person(string förnamn, string efternamn, DateTime födelsedatum)
    {
        _förnamn = förnamn;
        _efternamn = efternamn;
        FullständigtNamn = $"{_förnamn} {_efternamn}";
        Födelsedatum = födelsedatum;
    }
}

class DateCalc
{
    public int GetDays(Person person)
    {
        return (DateTime.Today - person.Födelsedatum).Days;
    }

    public int GetAge(Person person)
    {
        int ålder = DateTime.Today.Year - person.Födelsedatum.Year;
        if (DateTime.Today < person.Födelsedatum.AddYears(ålder))
            ålder--;
        return ålder;
    }
}

class Program
{
    static void Main()
    {
        Person p1 = new Person("Anna", "Svensson", new DateTime(1990, 3, 15));
        Person p2 = new Person("Nils", "Sjöberg", new DateTime(1989, 12, 13));
        Person p3 = new Person("Priya", "Nilsson", new DateTime(2017, 6, 22));

        DateCalc calc = new DateCalc();

        Person[] personer = { p1, p2, p3 };
        foreach (var person in personer)
        {
            int ålder = calc.GetAge(person);
            int dagar = calc.GetDays(person);
            Console.WriteLine($"{person.FullständigtNamn} — {ålder} år — {dagar} dagar");
        }
    }
}
```

</details>

---

## Att fundera på

- Varför är `DateCalc` en separat klass och inte bara två metoder i `Person`?
- Vad är fördelen med att `GetDays` och `GetAge` tar en `Person` som parameter istället för ett `DateTime`-värde direkt?
- Vad händer om du skapar en `Person` med ett födelsedatum i framtiden — och anropar `GetDays`?
