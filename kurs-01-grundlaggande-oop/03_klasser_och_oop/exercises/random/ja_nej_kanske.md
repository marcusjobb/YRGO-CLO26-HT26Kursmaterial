# Övning — Ja, Nej eller Kanske

🟢

Du bygger en magisk boll — ungefär som en Magic 8-ball. Programmet väljer slumpmässigt ett svar och skriver ut det. Kör om programmet och svaret ändras.

Ingen inmatning från tangentbordet. Ingen loopp. Bara Random och switch.

---

## Uppgiften

Deklarera en `Random` och ta fram ett slumpmässigt heltal `0`, `1` eller `2`.

Skriv sedan en `switch` som matchar det talet och skriver ut ett av svaren nedan:

| Tal | Svar |
|-----|------|
| 0 | Ja, absolut! |
| 1 | Nej, definitivt inte. |
| 2 | Kanske... det beror på. |

---

## Förväntad output (ett av tre möjliga)

```plaintext
🎱 Den magiska bollen säger:
Ja, absolut!
```

eller

```plaintext
🎱 Den magiska bollen säger:
Nej, definitivt inte.
```

eller

```plaintext
🎱 Den magiska bollen säger:
Kanske... det beror på.
```

---

## Utmanande frågor

> Sitter du fast i mer än 15 minuter? Be om hjälp — det är inte ett tecken på att du inte kan, det är precis vad 15-minutersregeln är till för.

1. Hur ändrar du sannolikheten så att "Ja" är dubbelt så troligt som de andra svaren?
2. Vad händer om du ändrar `Next(0, 3)` till `Next(0, 4)` men glömmer lägga till ett fjärde `case`? Vilket svar visas då?
3. Hur lägger du till ett fjärde svar: "Fråga igen senare."?

---

> 💡 Fler slump-exempel: [marcusjobb/UsborneBooks](https://github.com/marcusjobb/UsborneBooks) — gamla Usborne-spel från 80-talet övertatta till C#.

<details><summary>Tips: hur skapar jag ett Random-objekt?</summary>

```csharp
Random slump = new Random();
int val = slump.Next(0, 3); // ger 0, 1 eller 2
```

`Next(0, 3)` ger ett tal från 0 upp till *men inte* 3.

</details>

<details><summary>Lösningsförslag</summary>

```csharp
Random slump = new Random();
int val = slump.Next(0, 3);

Console.WriteLine("🎱 Den magiska bollen säger:");

switch (val)
{
    case 0:
        Console.WriteLine("Ja, absolut!");
        break;
    case 1:
        Console.WriteLine("Nej, definitivt inte.");
        break;
    default:
        Console.WriteLine("Kanske... det beror på.");
        break;
}
```

</details>
