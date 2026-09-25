# Konsol och variabler — livekodning 2026-09-07

En genomgång av grundsyntax i ett svep: utskrifter, strängar, variabler, inmatning, formatering, escape-tecken och inkrementoperatorer. Varje del byggdes, kördes, och kommenterades sedan bort innan nästa del — så här ser hela vägen ut.

## Grundläggande utskrift

```csharp
Console.WriteLine("Hello, CLO26!");
Console.WriteLine("Hej världen!");
Console.WriteLine("Programmet är igång.");
Console.WriteLine(42);
Console.WriteLine(3.14);
Console.Write("Svart");
Console.WriteLine("Katt".PadLeft(15));
```

`WriteLine` skriver ut och byter rad, `Write` skriver ut utan radbrytning. `WriteLine` tar emot vad som helst — text, heltal, decimaltal. `PadLeft(15)` fyller ut strängen med mellanslag till vänster tills den är 15 tecken lång.

## Strängar — konkatenering

```csharp
string name = "Taylor";
string lastName = "Swift";

Console.WriteLine(name + " " + lastName);
```

Värdet som byggs upp steg för steg när `+` används på strängar:
- `""`
- `"Taylor"`
- `"Taylor "`
- `"Taylor Swift"`

`Console.WriteLine` förväntar sig en sträng som argument. `sträng (name) + sträng (" ") + sträng (lastName)` ger en ny sträng.

## Strängar — interpolering

```csharp
Console.WriteLine($"{name} {lastName}");
```

Samma resultat, men vi skickar in **en** sträng som redan innehåller `name` och `lastName` separerade av ett mellanslag — inget `+` behövs.

```csharp
string namn = "Alex";
int ålder = 22;

Console.WriteLine($"Hej {namn}!");
Console.WriteLine($"{namn} är {ålder} år gammal.");
Console.WriteLine($"Om 10 år är {namn} {ålder + 10} år.");
```

Interpolering funkar med uttryck också, inte bara variabler rakt av — `{ålder + 10}` räknas ut innan det skrivs ut.

## Inmatning från användaren

```csharp
Console.Write("Vad heter du? ");
string användarens_coola_namn_hamnar_här = Console.ReadLine();
Console.WriteLine($"Hej {användarens_coola_namn_hamnar_här}!");
```

```csharp
Console.Write("Hur gammal är du? ");
// Datorn skriver "Hur gammal är du? "
string inmatning = Console.ReadLine();
// datorn frågar efter information, den förväntar sig alltid text även om vi skriver in siffror.

int age = int.Parse(inmatning);
// int betyder heltal, alltså inga decimaler.
// int har en inbyggd funktion kallad Parse som kan omvandla en sträng till ett heltal.

Console.WriteLine($"Om 10 år är du {age + 10} år.");
```

`Console.ReadLine()` returnerar **alltid** en `string`, oavsett vad användaren skrev. Ska du räkna med det som skrevs in måste du omvandla det — `int.Parse(...)` gör text till heltal.

## Formatering av tal

```csharp
double pris = 1234.5;
double procent = 0.1575;

Console.WriteLine($"Pris: {pris:N2} kr");       // tusentalsavskiljare, 2 decimaler
Console.WriteLine($"Moms: {procent:P1}");        // procent, 1 decimal
Console.WriteLine($"Pris: {pris:C}");            // valutaformat (systemets locale)
```

Formatkoden efter kolonet styr hur talet visas: `N2` = med tusentalsavskiljare och två decimaler, `P1` = som procent med en decimal, `C` = som valuta enligt datorns språkinställning.

## Escape-tecken

```csharp
Console.WriteLine("Hej\nVärlden!"); // \n betyder ny rad
Console.WriteLine("Hej\tVärlden!"); // \t betyder tabulator
Console.WriteLine("Mitt namn är Marcus men vissa kallar mig \"Mackan\"."); // \" betyder att vi vill skriva ut ett citattecken
Console.WriteLine("Backslash: \\"); // \\ betyder att vi vill skriva ut ett backslash
```

Ett backslash-tecken markerar att nästa tecken ska tolkas specialt istället för bokstavligt — `\n` är inte ett "n", det är en ny rad.

## Rensa konsolen

```csharp
Console.Clear();
```

## Inkrementoperatorer

```csharp
int ålder = 19;
ålder = ålder + 1; // ålder = 20
// Minnesplatsen där ålder förbereds för nytt värde
// ålder + 1 = 20
// 20 skickas till minnesplatsen där ålder finns
ålder++;      // ålder = 21
ålder += 1;   // ålder = 22
ålder += 5;   // ålder = 27
ålder *= 2;   // ålder = 54
```

Fyra sätt att ändra en variabel, i ökande kompakthet: `ålder = ålder + 1` (skriv ut hela uttrycket), `ålder++` (öka med exakt 1), `ålder += n` (öka med `n`), `ålder *= n` (multiplicera med `n`). Alla fyra modifierar samma minnesplats.

## Strängreferenser

```csharp
string marcus = "Marcus";
string pelle = "Pelle";
string Nånannan = pelle;
```

`Nånannan` får samma värde som `pelle` hade vid tilldelningen — "Pelle". Ändrar man `pelle` senare ändras inte `Nånannan` automatiskt; strängar är oföränderliga (immutable) i C#, så `Nånannan` fick sin egen kopia av värdet "Pelle", inte en länk till `pelle`-variabeln.

## if/else

```csharp
bool isBroke = true;

if (isBroke)
{
    Console.WriteLine("Jag har inga pengar :'( .");
}
else
{
    Console.WriteLine("Jag har pengar! :D");
}
```

En `bool` styr vilken gren som körs — `true` går in i `if`-blocket, `false` hade gått till `else`.
