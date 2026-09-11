# Övning — Fisken 🐟

🔴

En klassiker från de tidiga datordagarnas ZX81-era: rita en fisk på skärmen med hjälp av sinus. Kroppen ritas med en matematisk kurva, bubblorna flutrar upp i slumpmässiga positioner.

Det är mer konst än verktyg — men det ger en känsla för hur matematik och kod möts, och hur `Math.Sin()` kan producera organiska, levande former.

---

## Bakgrund

På ZX81 ritade programmerare på 1980-talet former med matematiska funktioner — utan grafikkort, utan sprites. Bara ASCII-tecken och ekvationer. Den känslan lever kvar.

I den här övningen ritar du en fisk där:
- Kroppen är **övre och undre kant**, vardera en sinuskurva från 0 till π
- Bubblorna är **slumpmässiga `o`-tecken** utspridda ovanför fisken
- Fisken får en **slumpmässig färg** varje körning: cyan, grön eller magenta

---

## Vad du tränar på

- `Math.Sin()` med vinklar 0→π — ger en halv period (upp och tillbaka till noll)
- `Math.PI` — konstanten π i C#
- `Console.SetCursorPosition(x, y)` — rita på godtycklig plats
- `Random` — slumpmässig färg och bubblornas placering
- `for`-loop för att rita kropp och bubblor

---

## Nyckeln: `Math.Sin(t * Math.PI / totalLängd)`

Det kritiska är vilken vinkel du matar in i sinus.

`Math.Sin(t * Math.PI / totalLängd)` ger:
- vid `t = 0`: sin(0) = **0** — kroppen är smal (start)
- vid `t = totalLängd/2`: sin(π/2) = **1** — kroppen är som bredast (mitten)
- vid `t = totalLängd`: sin(π) = **0** — kroppen är smal igen (slut)

Det är exakt formen på en fisk — tunn i vardera ände, bred i mitten.

Rita övre kanten som `centerY - sin(...)` och undre kanten som `centerY + sin(...)`.

---

## Uppgiften

Deklarera dessa variabler:

```csharp
int centerX    = 38;
int centerY    = 13;
int halvLängd  = 25;   // fisken sträcker sig halvLängd kolumner åt varje håll
int halvHöjd   = 5;    // fisken är halvHöjd rader tjock vid mitten
int totalLängd = halvLängd * 2;
```

Skriv ett program som:

1. Anropar `Console.Clear()` och sätter `Console.CursorVisible = false`
2. Väljer en slumpmässig fiskfärg med `switch`:
   - `0` → `ConsoleColor.Cyan`
   - `1` → `ConsoleColor.Green`
   - `2` → `ConsoleColor.Magenta`
3. Ritar **övre kroppen** — loopa `t` från `0` till `totalLängd`:
   - `x = centerX - halvLängd + t`
   - `y = centerY - (int)(Math.Sin(t * Math.PI / totalLängd) * halvHöjd)`
   - Skriv `"~"` på `(x, y)`
4. Ritar **undre kroppen** — samma loop men `y = centerY + ...` (plustecken)
5. Ritar **huvud** `">"` på `(centerX + halvLängd + 1, centerY)`
6. Ritar **stjärt** — fyra tecken längst till vänster:
   - `(centerX - halvLängd - 3, centerY - 2)` → `"\\"`
   - `(centerX - halvLängd - 2, centerY - 1)` → `"\\"`
   - `(centerX - halvLängd - 2, centerY + 1)` → `"/"`
   - `(centerX - halvLängd - 3, centerY + 2)` → `"/"`
7. Ritar **15 bubblor** ovanför fisken med `ConsoleColor.White`
8. Återställer färgen och skriver `"[tryck Enter]"` längst ner

---

## Förväntad output (ungefärlig)

```plaintext
                         o   o     o      o
                       o         o    o
                     o      o            o

                     \
                      \      ~~~~~~~~~~
                        ~~~~          ~~~~
                      ~~                  ~~
                     ~                      ~
                    ~                        ~>
                     ~                      ~
                      ~~                  ~~
                        ~~~~          ~~~~
                              ~~~~~~~~~~
                      /
                     /
```

*(Konsolfärg syns inte i texten — fisken lyser i cyan, grön eller magenta.)*

---

## Utmanande frågor

1. Vad händer om du ändrar `halvHöjd` från 5 till 2 — ser fisken mager ut?
2. Hur lägger du till ett öga — ett `"o"` inuti fisken nära huvudet?
3. Hur ritar du en andra fisk med `centerY + 10` och färg `ConsoleColor.DarkCyan`?

---

> 💡 Fler slump-exempel: [marcusjobb/UsborneBooks](https://github.com/marcusjobb/UsborneBooks) — gamla Usborne-spel från 80-talet övertatta till C#.

<details><summary>Tips: varför Math.PI / totalLängd?</summary>

Sinus gör en komplett cykel på 2π radianer. Vi vill bara ha **halva** cykeln (0 → π) — det ger en puckelformad kurva som börjar och slutar i noll, med max i mitten.

`t * Math.PI / totalLängd` mappar `t` från `0` till `totalLängd` till vinklar `0` till `π`.

Om du använder `t * 2 * Math.PI / totalLängd` istället får du en hel cykel — och fisken börjar se ut som ett S eller en åttondels-not.

</details>

<details><summary>Tips: hur ritar jag övre och undre kroppen?</summary>

Övre kanten böjer **uppåt** (minus):

```csharp
for (int t = 0; t <= totalLängd; t++)
{
    int x = centerX - halvLängd + t;
    int y = centerY - (int)(Math.Sin(t * Math.PI / totalLängd) * halvHöjd);
    Console.SetCursorPosition(x, y);
    Console.Write("~");
}
```

Undre kanten är identisk men med plus:

```csharp
for (int t = 0; t <= totalLängd; t++)
{
    int x = centerX - halvLängd + t;
    int y = centerY + (int)(Math.Sin(t * Math.PI / totalLängd) * halvHöjd);
    Console.SetCursorPosition(x, y);
    Console.Write("~");
}
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
Random slump = new Random();
Console.Clear();
Console.CursorVisible = false;

int färgval = slump.Next(0, 3);
switch (färgval)
{
    case 0:
        Console.ForegroundColor = ConsoleColor.Cyan;
        break;
    case 1:
        Console.ForegroundColor = ConsoleColor.Green;
        break;
    default:
        Console.ForegroundColor = ConsoleColor.Magenta;
        break;
}

int centerX    = 38;
int centerY    = 13;
int halvLängd  = 25;
int halvHöjd   = 5;
int totalLängd = halvLängd * 2;

// Övre kroppen
for (int t = 0; t <= totalLängd; t++)
{
    int x = centerX - halvLängd + t;
    int y = centerY - (int)(Math.Sin(t * Math.PI / totalLängd) * halvHöjd);
    Console.SetCursorPosition(x, y);
    Console.Write("~");
}

// Undre kroppen
for (int t = 0; t <= totalLängd; t++)
{
    int x = centerX - halvLängd + t;
    int y = centerY + (int)(Math.Sin(t * Math.PI / totalLängd) * halvHöjd);
    Console.SetCursorPosition(x, y);
    Console.Write("~");
}

// Huvud
Console.SetCursorPosition(centerX + halvLängd + 1, centerY);
Console.Write(">");

// Stjärt
Console.SetCursorPosition(centerX - halvLängd - 3, centerY - 2);
Console.Write("\\");
Console.SetCursorPosition(centerX - halvLängd - 2, centerY - 1);
Console.Write("\\");
Console.SetCursorPosition(centerX - halvLängd - 2, centerY + 1);
Console.Write("/");
Console.SetCursorPosition(centerX - halvLängd - 3, centerY + 2);
Console.Write("/");

// Bubblor
Console.ForegroundColor = ConsoleColor.White;
for (int i = 0; i < 15; i++)
{
    int bx = slump.Next(centerX, centerX + halvLängd + 3);
    int by = slump.Next(2, centerY - halvHöjd - 1);
    if (by >= 1 && bx < Console.WindowWidth - 1)
    {
        Console.SetCursorPosition(bx, by);
        Console.Write("o");
    }
}

Console.ResetColor();
Console.CursorVisible = true;
Console.SetCursorPosition(0, Console.WindowHeight - 2);
Console.WriteLine("[tryck Enter]");
Console.ReadLine();
Console.ResetColor();
Console.Clear();
```

</details>
