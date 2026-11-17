# Övning — Gissa ett tal

🟡

Programmet väljer ett hemligt tal mellan 1 och 100. Du gissar. Det berättar om svaret är för högt, för lågt eller rätt. Spelet fortsätter tills du gissar rätt.

---

## Uppgiften

Skriv ett program med en `while`-loop som:

1. Väljer ett hemligt heltal `1`–`100` med `Random`
2. Skriver ut: `Jag tänker på ett tal mellan 1 och 100.`
3. I loopen:
   - Skriver ut: `Din gissning: `
   - Läser in spelarens gissning med `Console.ReadLine()` och omvandlar till `int` med `int.Parse()`
   - Räknar upp antalet försök
   - Skriver ut `"För högt!"`, `"För lågt!"` eller rätt svar

4. När spelaren gissar rätt avslutas loopen och antalet försök skrivs ut

---

## Förväntad output

```plaintext
Jag tänker på ett tal mellan 1 och 100.

Din gissning: 50
För lågt!

Din gissning: 75
För högt!

Din gissning: 63
Rätt! Det tog 3 försök.
```

---

## Utmanande frågor

1. Hur många gissningar behövs som mest om du halverar intervallet varje gång? (Det finns ett matematiskt svar — leta upp "binär sökning".)
2. Hur lägger du till ett maxantal försök — t.ex. 7 — och förlorar om du inte gissar rätt i tid?
3. Hur ändrar du svårighetsgraden så att spelaren väljer ett intervall innan spelet börjar: `"Vill du gissa på 1–50, 1–100 eller 1–500? (1/2/3)"`?

---

> 💡 Fler slump-exempel: [marcusjobb/UsborneBooks](https://github.com/marcusjobb/UsborneBooks) — gamla Usborne-spel från 80-talet övertatta till C#.

<details><summary>Tips: hur läser jag in och omvandlar inmatning?</summary>

```csharp
Console.Write("Din gissning: ");
string inmatning = Console.ReadLine();
int gissning = int.Parse(inmatning);
```

`int.Parse()` kraschar om spelaren skriver något som inte är ett tal. Det är OK i den här övningen.

</details>

<details><summary>Tips: hur strukturerar jag loopen?</summary>

```csharp
bool rätt = false;
int antalFörsök = 0;

while (!rätt)
{
    // läs in gissning
    // räkna upp antalFörsök
    // jämför med hemligTal
    // om rätt: sätt rätt = true
}
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
Random slump = new Random();
int hemligTal = slump.Next(1, 101);
int antalFörsök = 0;
bool rätt = false;

Console.WriteLine("Jag tänker på ett tal mellan 1 och 100.");

while (!rätt)
{
    Console.Write("\nDin gissning: ");
    int gissning = int.Parse(Console.ReadLine());
    antalFörsök++;

    if (gissning < hemligTal)
    {
        Console.WriteLine("För lågt!");
    }
    else if (gissning > hemligTal)
    {
        Console.WriteLine("För högt!");
    }
    else
    {
        Console.WriteLine($"Rätt! Det tog {antalFörsök} försök.");
        rätt = true;
    }
}
```

</details>
