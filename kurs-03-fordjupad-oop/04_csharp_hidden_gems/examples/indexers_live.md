# Indexers: en egen klass som går att använda med hakparenteser

Vi är vana vid `lista[2]` på en `List`. Med en indexer kan vi ge våra egna klasser samma sätt att komma åt innehållet. I det här exemplet bygger vi en bokhylla.

## Genomgång från lektionen

Först repetition: en `List<string>` som vi redan kan. Vi hämtar ett värde med hakparenteser:

```csharp
var lista = new List<string>()
{
    "Hello World",
    "Hello",
    "My cat"
};

// Skriver ut hela listan
foreach (var item in lista)
{
    Console.WriteLine(item);
}

Console.WriteLine(lista[2]); // My cat
```

Utskriften är de tre raderna i listan, och sedan `My cat` en gång till från `lista[2]`.

Nu vill vi ha samma sak på vår egen klass. Så här ska den användas:

```csharp
var shelf = new Shelf();                // ingen utskrift, en tom hylla med 5 platser
shelf[0] = "Clean Code";                // ingen utskrift, boken ställs på plats 0
Console.WriteLine(shelf[0]);            // Clean Code
Console.WriteLine(shelf[10]);           // Ingen hylla här
shelf[2] = "Cookbook";                  // ingen utskrift, boken ställs på plats 2
Console.WriteLine(shelf[2]);            // Cookbook
shelf[2] = "Coding book";               // Platsen är upptagen
```

Kommentarerna är tillagda för att visa vad varje rad ger. Det vi vill se: `shelf[10]` ligger utanför hyllan, och `shelf[2]` används två gånger.

Vi gör `Shelf` till en egen modul som sköter sig själv. Den som använder hyllan ska inte behöva tänka på var den börjar eller slutar, eller vilket index som är det högsta. Det ska bara gå att använda hyllan. Det är vi som skyddar både användaren och koden, så all kontroll läggs inuti klassen.

Hyllan har plats för fem böcker, i en privat array. Den är `private` så att ingen utifrån kan nå arrayen och råka skriva utanför den:

```csharp
class Shelf
{
    private readonly string[] books = new string[5];
```

Själva indexern skrivs som en property, men med `this[int index]` i stället för ett namn. Först `get`, som hämtar en bok. Ligger platsen utanför hyllan får vi ett vänligt svar i stället för en krasch:

```csharp
    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= books.Length)
                return "Ingen hylla här";
            return books[index];
        }
```

Sedan `set`, som ställer en bok på hyllan. Här har vi två kontroller: platsen måste finnas, och den får inte redan vara upptagen:

```csharp
        set
        {
            if (index < 0 || index >= books.Length)
            {
                Console.WriteLine("Ingen plats i hyllan");
                return;
            }
            if (! string.IsNullOrEmpty(books[index]))
            {
                Console.WriteLine("Platsen är upptagen");
                return;
            }
            books[index] = value;
        }
    }
```

Lägg märke till `value`. Det är det som står till höger om `=` när vi skriver `shelf[0] = "Clean Code"`.

Till sist en vanlig property, så att andra kan fråga hur många platser hyllan har:

```csharp
    public int Length { get { return books.Length; } }

}
```

Utskriften från hyllan blir:

```text
Clean Code
Ingen hylla här
Cookbook
Platsen är upptagen
```

Sista raden kommer från `shelf[2] = "Coding book"`. Platsen var redan upptagen av `Cookbook`, så `set` skriver ut ett meddelande och lämnar boken kvar.

## Varför har vi nytta av detta?

Den som använder klassen skriver `shelf[0]` och behöver inte veta att det finns en array inuti, eller hur stor den är. Den som använder klassen behöver inte tänka på minsta eller högsta index, för `Shelf` skyddar både användaren och koden: en plats utanför hyllan ger ett vänligt svar i stället för en krasch (`IndexOutOfRangeException`), och en upptagen plats skrivs inte över av misstag. Reglerna ligger på ett ställe, i klassen, i stället för att kontrolleras överallt där hyllan används. Det är idén med en modul som sköter sig själv.

## När kan vi använda detta?

När en klass i grunden är en samling av något: en hylla, en spelplan, en tabell. `List` och `Dictionary` är byggda på precis det här sättet.

## Hela koden

```csharp
Console.WriteLine("Hello, Indexer!");

var lista = new List<string>()
{
    "Hello World",
    "Hello",
    "My cat"
};

// Skriver ut hela listan
foreach (var item in lista)
{
    Console.WriteLine(item);
}

Console.WriteLine(lista[2]); // My cat

// --------------------

var shelf = new Shelf();
shelf[0] = "Clean Code";
Console.WriteLine(shelf[0]);
Console.WriteLine(shelf[10]);
shelf[2] = "Cookbook";
Console.WriteLine(shelf[2]);
shelf[2] = "Coding book";

class Shelf
{
    private readonly string[] books = new string[5];

    public string this[int index]
    {
        get
        {
            if (index < 0 || index >= books.Length)
                return "Ingen hylla här";
            return books[index];
        }
        set
        {
            if (index < 0 || index >= books.Length)
            {
                Console.WriteLine("Ingen plats i hyllan");
                return;
            }
            if (! string.IsNullOrEmpty(books[index]))
            {
                Console.WriteLine("Platsen är upptagen");
                return;
            }
            books[index] = value;
        }
    }

    public int Length { get { return books.Length; } }

}
```

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
