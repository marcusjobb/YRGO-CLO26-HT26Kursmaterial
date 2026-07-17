# Lärarfacit — Intro: Varför finns OOP?

## Del 1 — Kalle börjar enkelt

```csharp
string kalle_telefon = "070-123 45 67";
string jonna_telefon = "070-678 90 12";

Console.WriteLine("Jonnas telefonnummer: " + jonna_telefon);
Console.WriteLine("Kalles telefonnummer: " + kalle_telefon);
```

## Del 2 — Chefen vill ha mer

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

## Del 3 — Problemet

Konkreta problem att lyfta om studerande inte hittar dem:
- Varje ny klient kräver 5–6 nya variabler
- Stavfel i variabelnamn → fel data skrivs ut (t.ex. `kalle_stad` vs `karin_stad`)
- Data som hör ihop är utspridd — omöjligt att skicka "en klient" till en metod

## Del 4 — Klassen löser det

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

## Del 5 — Utmaning (IsInCity)

```csharp
public bool IsInCity(string city)
{
    return _city == city;
}
```

Obs: utmaningen kräver en publik `Name`-property för att kunna skriva ut i filtret.  
Lägg till i klassen:

```csharp
public string Name => _name;
```

Eller ändra `_name` till en property:

```csharp
public string Name { get; private set; }
```

## Pedagogisk poäng att lyfta

Klassövergången i Del 4 är det viktiga ögonblicket: en ny klient är *en rad*, inte sex.
Skriv det på tavlan bredvid "Del 2"-versionen så de ser skillnaden direkt.
