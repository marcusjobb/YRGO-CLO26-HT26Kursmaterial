# Enum — Labyrintspelet

Vi tar emot spelarens input och skriver ut vad de valde:

```cs
Console.WriteLine("Du befinner dig i en labyrint");
Console.WriteLine("Du kan gå Nord, Söder, Öst, Väst");
Console.WriteLine("Vart vill du gå");
string input = Console.ReadLine();

Console.WriteLine("Du valde att gå " + input);
```

`Enum.Parse` omvandlar strängen till ett enum-värde. Flaggan `true` gör sökningen skiftlägesokänslig — "söder", "Söder" och "SÖDER" fungerar alla:

```cs
Riktning val = Enum.Parse<Riktning>(input, true);
```

Nu kan vi jämföra med `==` istället för att jämföra strängar, vilket är säkrare och tydligare:

```cs
if (val == Riktning.Söder)
    Console.WriteLine("Du kommer till labyrintens mitt");
else if (val == Riktning.Norr)
    Console.WriteLine("Du vandra norrut och hittar fler gångar");
else if (val == Riktning.Väst)
    Console.WriteLine("Du vandrar västerut och hittar en fontän");
else if (val == Riktning.Öst)
    Console.WriteLine("Du vandrar Österut och hittar en grotta");
```

Varje enum-värde har ett underliggande heltal. Standardstart är 0, men vi kan sätta egna värden om vi vill:

```cs
Console.WriteLine(Riktning.Norr + " är " + (int)Riktning.Norr);
Console.WriteLine(Riktning.Söder + " är " + (int)Riktning.Söder);
Console.WriteLine(Riktning.Öst + " är " + (int)Riktning.Öst);
Console.WriteLine(Riktning.Väst + " är " + (int)Riktning.Väst);
```

Enum-definitionen läggs utanför `Main` (top-level statements):

```cs
enum Riktning
{
    Upp,
    Ner,
    Norr,
    Söder,
    Öst,
    Väst,
}
```

## Hela koden

```cs
Console.WriteLine("Hello, Player!");

Console.WriteLine("Du befinner dig i en labyrint");
Console.WriteLine("Du kan gå Nord, Söder, Öst, Väst");
Console.WriteLine("Vart vill du gå");
string input = Console.ReadLine();

// Första bokstaven blir stor bokstav, resten tvingas till små bokstäver
// input = input.ToUpper()[0] + input.ToLower().Substring(1);
Console.WriteLine("Du valde att gå " + input);

// Omvandlar input till enum
Riktning val = Enum.Parse<Riktning>(input, true);

if (val == Riktning.Söder)
    Console.WriteLine("Du kommer till labyrintens mitt");
else if (val == Riktning.Norr)
    Console.WriteLine("Du vandra norrut och hittar fler gångar");
else if (val == Riktning.Väst)
    Console.WriteLine("Du vandrar västerut och hittar en fontän");
else if (val == Riktning.Öst)
    Console.WriteLine("Du vandrar Österut och hittar en grotta");

Console.WriteLine(Riktning.Norr + " är " + (int)Riktning.Norr);
Console.WriteLine(Riktning.Söder + " är " + (int)Riktning.Söder);
Console.WriteLine(Riktning.Öst + " är " + (int)Riktning.Öst);
Console.WriteLine(Riktning.Väst + " är " + (int)Riktning.Väst);

enum Riktning
{
    Upp,
    Ner,
    Norr,
    Söder,
    Öst,
    Väst,
}
```
