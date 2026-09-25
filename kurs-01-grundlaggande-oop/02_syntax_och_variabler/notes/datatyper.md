# Data Types

🟢

En datatyp talar om för C# vad för slags data som ska lagras i en variabel — och hur mycket plats det tar i minnet. Utan det vet kompilatorn ingenting. "En siffra? En text? Ett ja eller nej?" Kompilatorn är noga på den punkten.

## De vanligaste typerna

Det finns massor av typer i C#, men i praktiken använder du dessa hela tiden:

| Typ       | Vad den lagrar              | Exempel                        |
| --------- | --------------------------- | ------------------------------ |
| `int`     | Heltal                      | `42`, `-7`, `1138`             |
| `double`  | Decimaltal                  | `3.14`, `-0.5`, `9999.99`      |
| `decimal` | Decimaltal (exakt, pengar)  | `199.99m`, `0.01m`             |
| `string`  | Text                        | `"Hej!"`, `"Obi Wan Kenobi"`   |
| `bool`    | Sant eller falskt           | `true`, `false`                |
| `char`    | Ett enda tecken             | `'A'`, `'!'`, `'9'`            |

## Så här ser det ut i kod

```csharp
// Antal poäng i ett spel — heltal
int score = 1138;

// Priset på en kaffekopp — exakt decimaltal
decimal price = 29.90m;

// Spelarens namn
string playerName = "Pelle";

// Är spelaren vid liv?
bool isAlive = true;

// Första bokstaven i betyget
char grade = 'A';
```

## Välj rätt typ

Varje typ finns av en anledning. Väljer du fel typ funkar koden ändå — men du riskerar konstiga fel eller onödig minnesanvändning.

```csharp
// Inte bra: du förlorar decimaldelen
int pris = 29; // 29.90 avrundas till 29 — pengarna försvinner

// Bättre
decimal pris = 29.90m;

// Inte bra: string kan inte räkna
string antal = "5";
int summa = antal + 3; // Fungerar inte — du kan inte addera text och siffror

// Bättre
int antal = 5;
int summa = antal + 3; // = 8
```

## double vs decimal

Båda lagrar decimaltal — men de är inte samma sak.

- `double` är snabb och tar lite minne. Används för matte, fysik, koordinater.
- `decimal` är exakt men tyngre. Används när varje öre räknas — priser, skatter, finans.

```csharp
double pi = 3.14159;      // Tillräckligt exakt för koordinater
decimal pris = 149.95m;   // Exakt — inget avrundningsfel
```

`m` efter ett tal (`149.95m`) talar om för C# att det är en `decimal`, inte en `double`.

## string är speciell

`string` är tekniskt sett en referenstyp — inte en värdestyp som `int` och `bool`. Det märks om du börjar jämföra strängar på konstiga sätt. Men för nu: använd den som vanligt, det funkar.

```csharp
string city = "Göteborg";
int length = city.Length; // Antal tecken — 8
string upper = city.ToUpper(); // "GÖTEBORG"
```

---
Sådärja. Nu vet du vad du stoppar in i variablerna. Nästa steg — testa med fel typ och se vad kompilatorn säger. Det lär man sig bra av.
