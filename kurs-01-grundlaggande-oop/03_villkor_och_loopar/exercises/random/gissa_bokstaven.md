# Övning — Gissa en bokstav

🟡

Programmet väljer en hemlig stor bokstav A–Z. Du gissar en bokstav i taget. Det berättar om din gissning kommer tidigare eller senare i alfabetet. Spelet fortsätter tills du hittar rätt.

---

## Vad du tränar på

- Skapa en slumpmässig bokstav från ett tecken
- Jämföra tecken (`char`) med `<` och `>`
- `while`-loop med avslutsvillkor
- `Console.ReadLine()` med teckenhantering

---

## Hur man skapar en slumpmässig bokstav

Bokstäverna A–Z ligger i ordning i teckentabellen. `'A'` är tecknet A, och om du lägger till ett tal får du nästa bokstav i alfabetet:

```csharp
Random slump = new Random();
char hemligBokstav = (char)('A' + slump.Next(0, 26)); // A till Z
```

`(char)` omvandlar ett heltal till ett tecken. `'A' + 0` ger `'A'`, `'A' + 25` ger `'Z'`.

---

## Uppgiften

Skriv ett program med en `while`-loop som:

1. Väljer en hemlig stor bokstav A–Z
2. Skriver ut: `Jag tänker på en stor bokstav — kan du gissa?`
3. I loopen:
   - Skriver ut: `Din gissning: `
   - Läser in en rad med `Console.ReadLine()` och plockar ut första tecknet med `[0]`
   - Omvandlar till versaler med `.ToString().ToUpper()[0]`
   - Räknar upp antalet försök
   - Jämför med `<` och `>` — char kan jämföras direkt!
   - Skriver ut `"Senare i alfabetet!"`, `"Tidigare i alfabetet!"` eller rätt svar
4. Skriver ut antalet försök när spelaren hittat rätt

---

## Förväntad output

```plaintext
Jag tänker på en stor bokstav — kan du gissa?

Din gissning: M
Senare i alfabetet!

Din gissning: T
Tidigare i alfabetet!

Din gissning: P
Rätt! Bokstaven var P. Det tog 3 försök.
```

---

## Utmanande frågor

1. En optimal strategi halverar alltid alfabetet — precis som med tal. Hur många gissningar räcker det med som mest för att hitta rätt bland 26 bokstäver?
2. Hur ändrar du programmet så att det bara accepterar stora bokstäver och skriver ut ett felmeddelande om spelaren skriver något annat?
3. Hur lägger du till ett maxantal försök — t.ex. 5 — och avslöjar bokstaven om spelaren misslyckas?

---

> 💡 Fler slump-exempel: [marcusjobb/UsborneBooks](https://github.com/marcusjobb/UsborneBooks) — gamla Usborne-spel från 80-talet övertatta till C#.

<details><summary>Tips: hur jämför jag tecken?</summary>

`char` i C# kan jämföras direkt med `<`, `>` och `==` — precis som `int`. Bokstäverna jämförs i alfabetisk ordning:

```csharp
char a = 'D';
char b = 'P';
Console.WriteLine(a < b); // True — D kommer före P
```

</details>

<details><summary>Tips: hur plockar jag ut ett tecken från en sträng?</summary>

```csharp
string inmatning = Console.ReadLine();
char gissning = inmatning.ToUpper()[0]; // ta första tecknet, gör det versalt
```

`[0]` plockar ut tecknet på position 0 (det första). Om spelaren skriver "p" omvandlar `.ToUpper()` det till "P" innan du plockar ut tecknet.

</details>

<details><summary>Lösningsförslag</summary>

```csharp
Random slump = new Random();
char hemligBokstav = (char)('A' + slump.Next(0, 26));
int antalFörsök = 0;
bool rätt = false;

Console.WriteLine("Jag tänker på en stor bokstav — kan du gissa?");

while (!rätt)
{
    Console.Write("\nDin gissning: ");
    char gissning = Console.ReadLine().ToUpper()[0];
    antalFörsök++;

    if (gissning == hemligBokstav)
    {
        Console.WriteLine($"Rätt! Bokstaven var {hemligBokstav}. Det tog {antalFörsök} försök.");
        rätt = true;
    }
    else if (gissning < hemligBokstav)
    {
        Console.WriteLine("Senare i alfabetet!");
    }
    else
    {
        Console.WriteLine("Tidigare i alfabetet!");
    }
}
```

</details>
