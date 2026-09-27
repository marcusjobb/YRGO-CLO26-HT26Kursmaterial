# Övning — Stjärnhimlen

🟡

Du ritar en stjärnhimmel direkt i konsolfönstret. Programmet placerar ett antal stjärnor på slumpmässiga positioner med slumpmässiga färger — vitt, gult eller mörkgult — och skriver ett litet meddelande mitt på skärmen.

Det kräver ingen inmatning. Kör programmet och se himlen förändras varje gång.

---

## Vad du tränar på

- `Console.Clear()` — töm skärmen
- `Console.SetCursorPosition(x, y)` — placera markören var du vill
- `Console.WindowWidth` och `Console.WindowHeight` — skärmens storlek
- `Console.ForegroundColor` och `Console.ResetColor()` — färglägg text
- `Random` inuti en `for`-loop

---

## Uppgiften

Skriv ett program som:

1. Anropar `Console.Clear()` och sätter `Console.CursorVisible = false`
2. Skapar ett `Random`-objekt
3. Loopar **80 gånger** och för varje iteration:
   - Väljer en slumpmässig x-position: `0` till `Console.WindowWidth - 2`
   - Väljer en slumpmässig y-position: `0` till `Console.WindowHeight - 4`
   - Väljer en slumpmässig färg med ett tal `0`–`2` och en `switch`:
     - `0` → `ConsoleColor.White`
     - `1` → `ConsoleColor.Yellow`
     - `2` → `ConsoleColor.DarkYellow`
   - Sätter markören till `(x, y)` och skriver ut `"*"`
4. Väljer ett **slumpmässigt meddelande** med ett tal `0`–`3` och en `switch`:
   - `0` → `"✨ du är fantastisk ✨"`
   - `1` → `"🌟 solen lyser för dig 🌟"`
   - `2` → `"💫 drömmar tar tid, men de händer 💫"`
   - `3` → `"🌙 god natt, stjärnsamlare 🌙"`

   > Skriv gärna in egna meddelanden — det är bara att lägga till fler `case`-grenar.

5. Skriver ut meddelandet centrerat på skärmen:
   - Y-position: `Console.WindowHeight / 2`
   - X-position: `(Console.WindowWidth - meddelande.Length) / 2`
   - Färg: `ConsoleColor.Cyan`
6. Återställer färgen med `Console.ResetColor()`
7. Placerar markören längst ner och avslutar

---

## Förväntad output

Ungefär så här — men stjärnorna ser aldrig likadana ut:

```plaintext
    *          *    *
          *              *
  *                 *
       *        *
  *         *       *
            ✨ du är fantastisk ✨
       *            *
    *       *            *
              *    *
```

---

## Utmanande frågor

1. Hur ändrar du så att ungefär hälften av stjärnorna är vita och de andra delar lika på gult och mörkgult?
2. Vad händer om du använder `"★"` istället för `"*"`? Prova — ändrar det något?
3. Hur lägger du till en andra text-rad under meddelandet, t.ex. `"— stjärnhimlen 2024 —"`?

---

> 💡 Fler slump-exempel: [marcusjobb/UsborneBooks](https://github.com/marcusjobb/UsborneBooks) — gamla Usborne-spel från 80-talet övertatta till C#.

<details><summary>Tips: hur centrerar jag texten?</summary>

Räkna ut hur många kolumner texten behöver och subtrahera från halva skärmbredden:

```csharp
string meddelande = "✨ du är fantastisk ✨";
int meddelandeX = (Console.WindowWidth - meddelande.Length) / 2;
int meddelandeY = Console.WindowHeight / 2;
Console.SetCursorPosition(meddelandeX, meddelandeY);
Console.Write(meddelande);
```

</details>

<details><summary>Tips: hur sätter jag färg med switch?</summary>

```csharp
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
```

Glöm inte `Console.ResetColor()` när du är klar, annars förblir terminalen färgad.

</details>

<details><summary>Lösningsförslag</summary>

```csharp
Random slump = new Random();
Console.Clear();
Console.CursorVisible = false;

int antalStjärnor = 80;

int meddelandeVal = slump.Next(0, 4);
string meddelande;
switch (meddelandeVal)
{
    case 0:
        meddelande = "✨ du är fantastisk ✨";
        break;
    case 1:
        meddelande = "🌟 solen lyser för dig 🌟";
        break;
    case 2:
        meddelande = "💫 drömmar tar tid, men de händer 💫";
        break;
    default:
        meddelande = "🌙 god natt, stjärnsamlare 🌙";
        break;
}
// Lägg till fler case-grenar och höj Next(0, X) för fler meddelanden

for (int i = 0; i < antalStjärnor; i++)
{
    int x = slump.Next(0, Console.WindowWidth - 1);
    int y = slump.Next(0, Console.WindowHeight - 4);

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

    Console.SetCursorPosition(x, y);
    Console.Write("*");
}

int meddelandeX = (Console.WindowWidth - meddelande.Length) / 2;
int meddelandeY = Console.WindowHeight / 2;

Console.ForegroundColor = ConsoleColor.Cyan;
Console.SetCursorPosition(meddelandeX, meddelandeY);
Console.Write(meddelande);

Console.ResetColor();
Console.CursorVisible = true;
Console.SetCursorPosition(0, Console.WindowHeight - 2);
```

</details>
