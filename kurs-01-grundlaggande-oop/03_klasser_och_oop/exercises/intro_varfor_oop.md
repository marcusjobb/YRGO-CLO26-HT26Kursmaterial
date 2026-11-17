# Intro — Varför finns OOP?

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟢 Grundnivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Kalle har precis börjat som sommarpraktikant på en liten advokatbyrå. Chefen ber honom skriva ett enkelt program som håller koll på klienternas kontaktuppgifter.

"Det ska vara enkelt," säger chefen. "Börja med telefonnummer."

Kalle börjar koda. Det verkar enkelt. Det *är* enkelt — tills det inte är det längre.

Den här övningen visar dig varför klasser uppfinns — inte för att de låter häftiga, utan för att det annars går åt skogen rätt snabbt.

---

## Vad gäller för den här övningen

- [ ] Förstår varför separata variabler inte skalas
- [ ] Kan se sambandet mellan data som "hör ihop"
- [ ] Kan skriva en enkel klass som samlar data och beteende
- [ ] Kan skapa objekt och anropa metoder på dem

---

## Del 1 — Kalle börjar enkelt

Kalle skriver det här:

```csharp
string kalle_telefon = "070-123 45 67";
string jonna_telefon = "070-678 90 12";

Console.WriteLine("Jonnas telefonnummer: " + jonna_telefon);
Console.WriteLine("Kalles telefonnummer: " + kalle_telefon);
```

**Kör koden.** Fungerar det? Ja. Är det snyggt? Kanske.

### Förväntad output

```
Jonnas telefonnummer: 070-678 90 12
Kalles telefonnummer: 070-123 45 67
```

---

## Del 2 — Chefen vill ha mer

Chefen tittar på programmet och säger: "Bra! Lägg också till adress, stad och e-post per klient."

**Din uppgift:** Lägg till adress, stad och e-post för *båda* klienterna med samma variabelstrategi. Skriv sedan ut all info för Jonna.

```csharp
// Din kod här — samma stil som del 1, men med fler fält
```

### Förväntad output

```
Jonnas telefonnummer: 070-678 90 12
Jonnas adress: Västra gatan 45
Jonnas stad: Göteborg
Jonnas e-post: jonna@example.com
```

<details>
<summary>💡 Lösningsförslag del 2</summary>

```csharp
string kalle_telefon = "070-123 45 67";
string kalle_adress = "Södra gatan 3";
string kalle_stad = "Malmö";
string kalle_epost = "kalle@example.com";

string jonna_telefon = "070-678 90 12";
string jonna_adress = "Västra gatan 45";
string jonna_stad = "Göteborg";
string jonna_epost = "jonna@example.com";

Console.WriteLine("Jonnas telefonnummer: " + jonna_telefon);
Console.WriteLine("Jonnas adress: " + jonna_adress);
Console.WriteLine("Jonnas stad: " + jonna_stad);
Console.WriteLine("Jonnas e-post: " + jonna_epost);
```

Det funkar. Men hur många variabler är det nu? Räkna dem.  
Tänk dig att chefen nu vill lägga till 10 klienter till.

</details>

---

## Del 3 — Problemet

Chefen kommer tillbaka: "Fantastiskt! Nu vill vi ha 8 klienter till. Och personnummer. Och organisationsnummer på de som är företag."

Pausa ett ögonblick.

**Diskutera med dig själv (eller en kompis):** Vad händer med koden om vi fortsätter på samma spår? Skriv ner *ett konkret problem* du ser.

> *Det finns inget rätt svar här — det handlar om att se mönstret.*

<details>
<summary>💡 Vad brukar hända</summary>

- Varje ny klient kräver 5–6 nya variabler
- Namnen på variablerna måste matcha — ett stavfel och du skriver ut fel persons data
- Det är lätt att blanda ihop `kalle_stad` och `karin_stad`
- Data som *logiskt hör ihop* är utspridd i lösa variabler som inte vet om varandra
- Om du vill skriva en metod som skriver ut en klient — vilken data skickar du in? Allt separat?

Det saknas något: ett sätt att *paketera* data som hör ihop.

</details>

---

## Del 4 — Klassen löser det

En klass är ett sätt att säga: "Den här datan hör ihop, och det här är vad man kan göra med den."

**Din uppgift:** Fyll i klassen `Client` och använd den i `Main()`.

```csharp
class Client
{
    // Privata fält
    private string _name;
    private string _phone;
    private string _address;
    private string _city;
    private string _email;

    // Konstruktor — fyll i parametrarna
    public Client(/* din kod */)
    {
        // din kod
    }

    // Metod som skriver ut all info
    public void PrintInfo()
    {
        // din kod
    }
}

class Program
{
    static void Main()
    {
        Client jonna = new Client("Jonna", "070-678 90 12", "Västra gatan 45", "Göteborg", "jonna@example.com");
        Client kalle = new Client("Kalle", "070-123 45 67", "Södra gatan 3", "Malmö", "kalle@example.com");

        jonna.PrintInfo();
        Console.WriteLine();
        kalle.PrintInfo();
    }
}
```

### Förväntad output

```
--- Klientinfo ---
Namn:     Jonna
Telefon:  070-678 90 12
Adress:   Västra gatan 45
Stad:     Göteborg
E-post:   jonna@example.com

--- Klientinfo ---
Namn:     Kalle
Telefon:  070-123 45 67
Adress:   Södra gatan 3
Stad:     Malmö
E-post:   kalle@example.com
```

<details>
<summary>💡 Lösningsförslag del 4</summary>

```csharp
class Client
{
    private string _name;
    private string _phone;
    private string _address;
    private string _city;
    private string _email;

    public Client(string name, string phone, string address, string city, string email)
    {
        _name    = name;
        _phone   = phone;
        _address = address;
        _city    = city;
        _email   = email;
    }

    public void PrintInfo()
    {
        Console.WriteLine("--- Klientinfo ---");
        Console.WriteLine($"Namn:     {_name}");
        Console.WriteLine($"Telefon:  {_phone}");
        Console.WriteLine($"Adress:   {_address}");
        Console.WriteLine($"Stad:     {_city}");
        Console.WriteLine($"E-post:   {_email}");
    }
}
```

Lägg märke till: för att lägga till en nionde klient skriver du *en rad*, inte sex.

</details>

---

## Del 5 — Klar snabbt? Utmaning 🔴

Chefen vill att programmet också ska kunna *söka*. Lägg till en metod:

```csharp
public bool IsInCity(string city)
```

Metoden returnerar `true` om klienten bor i den angivna staden, annars `false`.

Använd den i `Main()` för att skriva ut alla klienter som bor i Göteborg.

### Förväntad output (med Göteborg-filter)

```
Klienter i Göteborg:
- Jonna
```

<details>
<summary>💡 Lösningsförslag utmaning</summary>

```csharp
public bool IsInCity(string city)
{
    return _city == city;
}
```

I `Main()`:

```csharp
Client[] clients = { jonna, kalle };

Console.WriteLine("Klienter i Göteborg:");
foreach (Client c in clients)
{
    if (c.IsInCity("Göteborg"))
        Console.WriteLine("- " + c.Name); // kräver en Name-property
}
```

För att det ska fungera behöver du en publik property `Name` — klassen ska inte lämna ut ett privat fält direkt, men en property med `get` är OK.

</details>

---

*Facit finns hos läraren.*
