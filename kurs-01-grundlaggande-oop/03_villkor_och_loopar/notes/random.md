# Lästext — Random

## Vad är slump i ett program?

Datorer är egentligen inte slumpmässiga — de följer exakta instruktioner, steg för steg. Men vi kan låta dem *simulera* slump med hjälp av matematiska formler som producerar tal som verkar oförutsägbara. Det kallas pseudoslump, och det räcker utmärkt för spel, simuleringar och övningar.

I C# hanteras detta av klassen `Random`.

---

## Skapa ett Random-objekt

Precis som med en variabel av typen `int` eller `string` skapar du ett `Random`-objekt och ger det ett namn:

```csharp
Random slump = new Random();
```

Nu har du ett verktyg som kan ge dig slumpmässiga tal. Du behöver bara skapa det *en gång* — använd sedan samma objekt om och om igen.

> **Vanligt misstag:** skapa ett nytt `Random`-objekt inuti en loop. Då återstartas fröet och du kan råka ut för att alltid få samma tal.

```csharp
// Fel — skapa aldrig Random inuti loopen
for (int i = 0; i < 10; i++)
{
    Random slump = new Random(); // ❌
    Console.WriteLine(slump.Next(1, 7));
}

// Rätt — skapa Random en gång, utanför loopen
Random slump = new Random(); // ✅
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(slump.Next(1, 7));
}
```

---

## Slumpmässiga heltal — Next()

`Next(min, max)` returnerar ett heltal från och med `min`, upp till men **inte** `max`.

```csharp
Random slump = new Random();

int tärning = slump.Next(1, 7);   // 1, 2, 3, 4, 5 eller 6
int procent = slump.Next(0, 101); // 0 till 100
int index   = slump.Next(0, 4);   // 0, 1, 2 eller 3
```

Regeln att komma ihåg: **min är inklusivt, max är exklusivt.** Vill du ha 1–6 skriver du `Next(1, 7)`.

---

## Slumpmässiga decimaltal — NextDouble()

`NextDouble()` returnerar ett tal från `0.0` till `0.999...` — aldrig exakt 1.0.

```csharp
double sannolikhet = slump.NextDouble(); // t.ex. 0.7341
```

Det används ofta i kombination med en multiplikation:

```csharp
double temperatur = slump.NextDouble() * 30.0; // 0.0 till ~30.0
```

---

## Vanliga mönster

### Välja ett alternativ bland flera

```csharp
Random slump = new Random();
int val = slump.Next(0, 3); // 0, 1 eller 2

switch (val)
{
    case 0:
        Console.WriteLine("Sten");
        break;
    case 1:
        Console.WriteLine("Sax");
        break;
    default:
        Console.WriteLine("Påse");
        break;
}
```

### Slumpmässig stor bokstav

`'A'` är tecknet A, och bokstäverna i alfabetet ligger i ordning i teckentabellen. A = 65, B = 66, … Z = 90.

```csharp
Random slump = new Random();
char bokstav = (char)('A' + slump.Next(0, 26)); // A–Z
Console.WriteLine(bokstav);
```

`(char)` omvandlar ett heltal till ett tecken. `'A' + 0` blir `'A'`, `'A' + 25` blir `'Z'`.

### Slumpmässig ConsoleFärg

```csharp
Random slump = new Random();
int färgval = slump.Next(0, 3);

switch (färgval)
{
    case 0:
        Console.ForegroundColor = ConsoleColor.White;
        break;
    case 1:
        Console.ForegroundColor = ConsoleColor.Yellow;
        break;
    default:
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        break;
}

Console.WriteLine("★");
Console.ResetColor();
```

---

## Math.Sin och Math.Cos — kurvor och former

`Math.Sin(vinkel)` och `Math.Cos(vinkel)` returnerar ett decimaltal mellan `-1.0` och `1.0` beroende på vinkeln (i radianer). Multiplice med en amplitud för att skala ut kurvan:

```csharp
double vinkel = 0.5;                // radianer
double värde  = Math.Sin(vinkel);   // ca 0.479

int y = (int)(Math.Sin(vinkel) * 5); // skalar till –5 till 5
```

Det låter matematiskt, men i praktiken är det ett sätt att rita vågiga linjer och kurvor på skärmen. Loopa igenom x-värden och beräkna y för varje steg — resultatet ser organiskt och levande ut.

```csharp
for (int x = 0; x < 60; x++)
{
    int y = 10 + (int)(Math.Sin(x * 0.3) * 4);
    Console.SetCursorPosition(x, y);
    Console.Write("~");
}
```

---

## Console.SetCursorPosition — placera text var som helst

Normalt skriver konsolen rad för rad uppifrån. Med `Console.SetCursorPosition(kolumn, rad)` kan du hoppa direkt till en godtycklig position på skärmen:

```csharp
Console.SetCursorPosition(20, 5); // kolumn 20, rad 5
Console.Write("Hej!");
```

Kolumn räknas från vänster (börjar på 0), rad räknas uppifrån (börjar på 0). `Console.WindowWidth` och `Console.WindowHeight` ger skärmens bredd och höjd.

Kombinera med `Console.Clear()` för att börja på ett rent blad och rita något helt eget.

---

## Sammanfattning

| Vad | Kod |
|-----|-----|
| Skapa Random | `Random slump = new Random();` |
| Heltal (min inkl., max exkl.) | `slump.Next(min, max)` |
| Decimaltal 0.0–0.999 | `slump.NextDouble()` |
| Slumpmässig bokstav A–Z | `(char)('A' + slump.Next(0, 26))` |
| Sinuskurva | `Math.Sin(x * faktor) * amplitud` |
| Placera markör | `Console.SetCursorPosition(x, y)` |

---

## Mer inspiration

[marcusjobb/UsborneBooks](https://github.com/marcusjobb/UsborneBooks) — klassiska Usborne-datorspel från 1980-talet, övertatta till C#. Innehåller massor av slump-logik: gissningsspel, labyrintar, stridssystem. Bra att läsa igenom och förstå hur Random användes redan i BASIC-eran.
