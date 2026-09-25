# Guide: Bygg en bra InputHandler

När man upprepar en viss funktion många gånger, speciellt när man måste kontrollera resultatet, kan det vara bra att samla alla anrop på ett ställe. Tänk dig att du ska debugga din kod och undrar, var frågar spelet om detta? Om du har en klass som hanterar det, så har du också mycket lättare att hitta var frågan ställs.

En `InputHandler` samlar all inläsning från användaren på ett ställe. Istället för att ha `Console.ReadLine()` utspritt i hela programmet anropar du bara `InputHandler.InputText("Vad heter du?")` — och klassen sköter resten.

Det här är en bra OOP-vana: **ett ansvar per klass**, och `InputHandler` har ansvaret för all inmatning.

---

## Grundversionen

Startpunkten är en statisk klass med två metoder. `static` gör att du inte behöver skapa ett objekt — du anropar den direkt med klassnamnet.

```csharp
static class InputHandler
{
    public static string InputText(string prompt)
    {
        Console.Write(prompt + "> ");
        return Console.ReadLine();
    }

    public static int InputNumber(string prompt)
    {
        Console.Write(prompt + "> ");
        return int.Parse(Console.ReadLine());
    }
}
```

Lägg märke till att `int.Parse` kraschar om användaren skriver något som inte är ett tal. Det löser vi i nästa steg.

---

## Steg 1 — Loopa tills texten inte är tom

`InputText` returnerar en tom sträng om användaren bara trycker Enter. Det kan orsaka buggar längre fram i programmet. Lösningen är att loopa tills vi faktiskt fått något.

```csharp
public static string InputText(string prompt)
{
    string input;
    do
    {
        Console.Write(prompt + "> ");
        input = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(input));

    return input;
}
```

`IsNullOrWhiteSpace` fångar både tom sträng och bara mellanslag — användaren tvingas skriva något meningsfullt.

---

## Steg 2 — Säker talhämtning med TryParse

`int.Parse` kastar ett undantag om inmatningen inte är ett tal. `int.TryParse` är säkrare: den returnerar `false` istället för att krascha, och vi kan loopa tills vi får ett giltigt tal.

```csharp
public static int InputNumber(string prompt)
{
    int result;
    do
    {
        Console.Write(prompt + "> ");
    } while (!int.TryParse(Console.ReadLine(), out result));

    return result;
}
```

`out result` fyller variabeln `result` direkt inuti `TryParse`. Om inmatningen inte är ett tal börjar loopen om.

---

## Steg 3 — Talvalidering med min och max

Nu kan vi lägga till en inparameter som säger vilket intervall som är giltigt. Användaren tvingas skriva ett tal mellan `min` och `max`.

```csharp
public static int InputNumber(string prompt, int min, int max)
{
    int result;
    do
    {
        Console.Write($"{prompt} ({min}–{max})> ");
    } while (!int.TryParse(Console.ReadLine(), out result) || result < min || result > max);

    return result;
}
```

Anrop: `int val = InputHandler.InputNumber("Välj svårighetsgrad", 1, 3);`

Loopen fortsätter om: inmatningen inte är ett tal **eller** talet är utanför intervallet.

---

## Steg 4 — GetKeyText: menyval med ett tangenttryck

Ibland vill du inte att användaren ska skriva och trycka Enter — du vill att de trycker **en tangent direkt**, som i ett spelmeny. `Console.ReadKey(true)` läser ett tangenttryck utan att visa det i konsolen.

```csharp
public static string GetKeyText(string prompt, string[] options)
{
    Console.WriteLine(prompt);
    for (int i = 0; i < options.Length; i++)
    {
        Console.WriteLine($"  {i + 1}. {options[i]}");
    }

    ConsoleKey key;
    int index;
    do
    {
        Console.Write("> Tryck en siffra: ");
        key = Console.ReadKey(true).Key;
        index = (int)key - (int)ConsoleKey.D1;
    } while (index < 0 || index >= options.Length);

    Console.WriteLine(options[index]);
    return options[index];
}
```

Anrop:

```csharp
string svårighetsgrad = InputHandler.GetKeyText(
    "Välj svårighetsgrad:",
    new[] { "Lätt", "Normal", "Svår" }
);
```

`ConsoleKey.D1` är siffertangenten 1 — vi räknar ut indexet genom att subtrahera. Trycker användaren 2 blir `index` = 1, vilket pekar på `options[1]`.

---

## Steg 5 — GetKeyNumber: returnera numret direkt

Ibland vill du ha siffran istället för texten — till exempel om du har en meny med numrerade val.

```csharp
public static int GetKeyNumber(string prompt, int min, int max)
{
    Console.WriteLine(prompt);

    ConsoleKey key;
    int number;
    do
    {
        Console.Write($"> Tryck {min}–{max}: ");
        key = Console.ReadKey(true).Key;
        number = (int)key - (int)ConsoleKey.D0;
    } while (number < min || number > max);

    Console.WriteLine(number);
    return number;
}
```

Anrop:

```csharp
int val = InputHandler.GetKeyNumber("Välj menyalternativ:", 1, 4);
```

`ConsoleKey.D0` är tangenten 0 — `number` räknas ut på samma sätt som i `GetKeyText`.

---

## Hela klassen

```csharp
static class InputHandler
{
    public static string InputText(string prompt)
    {
        string input;
        do
        {
            Console.Write(prompt + "> ");
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }

    public static int InputNumber(string prompt, int min, int max)
    {
        int result;
        do
        {
            Console.Write($"{prompt} ({min}–{max})> ");
        } while (!int.TryParse(Console.ReadLine(), out result) || result < min || result > max);

        return result;
    }

    public static string GetKeyText(string prompt, string[] options)
    {
        Console.WriteLine(prompt);
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"  {i + 1}. {options[i]}");
        }

        ConsoleKey key;
        int index;
        do
        {
            Console.Write("> Tryck en siffra: ");
            key = Console.ReadKey(true).Key;
            index = (int)key - (int)ConsoleKey.D1;
        } while (index < 0 || index >= options.Length);

        Console.WriteLine(options[index]);
        return options[index];
    }

    public static int GetKeyNumber(string prompt, int min, int max)
    {
        Console.WriteLine(prompt);

        ConsoleKey key;
        int number;
        do
        {
            Console.Write($"> Tryck {min}–{max}: ");
            key = Console.ReadKey(true).Key;
            number = (int)key - (int)ConsoleKey.D0;
        } while (number < min || number > max);

        Console.WriteLine(number);
        return number;
    }
}
```

---

## Experimentera vidare

- Kan du lägga till en `InputBool`-metod som frågar ja/nej och returnerar `true`/`false`?
- Kan du göra `GetKeyText` till ett overload av `InputText` — samma namn, annan parameterlista?
- Vad händer om `options` är en tom array i `GetKeyText`? Hur skyddar du mot det?
