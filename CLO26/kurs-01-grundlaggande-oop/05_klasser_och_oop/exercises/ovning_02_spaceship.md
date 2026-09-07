# Övning — Spaceship-klassen

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟡

Samma upplägg som bil-övningen — klassen har fler fält den här gången, men strukturen är identisk. Properties med `private set`, konstruktor, metoder med validering.

Rymden väntar.

---

## Steg 1: Skapa klassen och metoden `Presentera()`

Skapa en klass som heter `Spaceship` med följande properties (alla med `{ get; private set; }`):

- `Namn` — string
- `Kapten` — string
- `MaxBesättning` — int
- `Bränsle` — double (börjar på 100.0)
- `ÄrAktivt` — bool (börjar på true)

Konstruktorn tar emot `namn`, `kapten` och `maxBesättning`.

Lägg till metoden `Presentera()` som skriver ut skeppets information på det här formatet:

```
[Namn] | Kapten: [Kapten] | Bränsle: [Bränsle]% | Besättning: [MaxBesättning]
```

Skapa två skepp i `Main()` och anropa `Presentera()` på båda.

### Förväntad output

```plaintext
Millennium Falcon | Kapten: Han Solo | Bränsle: 100% | Besättning: 6
Enterprise | Kapten: Kirk | Bränsle: 100% | Besättning: 430
```

<details><summary>Tips — bränsle som heltal i utskriften</summary>

`Bränsle` är en `double`, men i `Presentera()` vill vi visa den utan decimaler. Prova att casta till `int`:

```csharp
Console.WriteLine($"Bränsle: {(int)Bränsle}%");
```

</details>

---

## Steg 2: Lägg till `Flyg()` och `Tanka()`

Lägg till två metoder:

- `Flyg(double bränsleförbrukning)` — minskar `Bränsle` med `bränsleförbrukning` och skriver ut ett meddelande med kvarvarande bränsle. Bränslet kan inte gå under 0.
- `Tanka(double mängd)` — ökar `Bränsle` med `mängd`, men aldrig över 100.0. Skriver ut ett meddelande.

Testa i `Main()` med båda skeppen.

### Förväntad output

```plaintext
Millennium Falcon | Kapten: Han Solo | Bränsle: 100% | Besättning: 6
Enterprise | Kapten: Kirk | Bränsle: 100% | Besättning: 430

Millennium Falcon flyger... Bränsle kvar: 75%
Enterprise flyger... Bränsle kvar: 90%

Tankar Millennium Falcon...
Millennium Falcon | Kapten: Han Solo | Bränsle: 100% | Besättning: 6
```

<details><summary>Lösningsförslag</summary>

```csharp
class Spaceship
{
    // properties — läsbara utifrån, skrivbara bara inifrån klassen
    public string Namn { get; private set; }
    public string Kapten { get; private set; }
    public int MaxBesättning { get; private set; }
    public double Bränsle { get; private set; }
    public bool ÄrAktivt { get; private set; }

    // konstruktor — körs när objektet skapas
    public Spaceship(string namn, string kapten, int maxBesättning)
    {
        Namn = namn;
        Kapten = kapten;
        MaxBesättning = maxBesättning;
        Bränsle = 100.0;
        ÄrAktivt = true;
    }

    // skriver ut skeppets information
    public void Presentera()
    {
        Console.WriteLine($"{Namn} | Kapten: {Kapten} | Bränsle: {(int)Bränsle}% | Besättning: {MaxBesättning}");
    }

    // flyger och förbrukar bränsle — aldrig under 0
    public void Flyg(double bränsleförbrukning)
    {
        Bränsle -= bränsleförbrukning;
        if (Bränsle < 0)
        {
            Bränsle = 0;
        }
        Console.WriteLine($"{Namn} flyger... Bränsle kvar: {(int)Bränsle}%");
    }

    // tankar upp — aldrig över 100
    public void Tanka(double mängd)
    {
        Bränsle += mängd;
        if (Bränsle > 100.0)
        {
            Bränsle = 100.0;
        }
        Console.WriteLine($"Tankar {Namn}...");
    }

    static void Main()
    {
        Spaceship skepp1 = new Spaceship("Millennium Falcon", "Han Solo", 6);
        Spaceship skepp2 = new Spaceship("Enterprise", "Kirk", 430);

        skepp1.Presentera();
        skepp2.Presentera();

        Console.WriteLine();

        skepp1.Flyg(25);
        skepp2.Flyg(10);

        Console.WriteLine();

        skepp1.Tanka(25);
        skepp1.Presentera();
    }
}
```

</details>

---

## Steg 3 (valfritt) 🔴

Lägg till metoden `Docka(Spaceship annatSkepp)` som tar emot ett annat `Spaceship`-objekt och skriver ut ett dockningsmeddelande med båda skeppens namn.

Exempel på output:
```plaintext
Millennium Falcon dockar med Enterprise.
```

<details><summary>Tips — objekt som parameter</summary>

En metod kan ta emot ett objekt av en annan klass som parameter, precis som `int` eller `string`:

```csharp
public void Docka(Spaceship annatSkepp)
{
    Console.WriteLine($"{Namn} dockar med {annatSkepp.Namn}.");
}
```

Eftersom `Namn` är en `public` property kan du läsa den utifrån — men inte ändra den.

</details>
