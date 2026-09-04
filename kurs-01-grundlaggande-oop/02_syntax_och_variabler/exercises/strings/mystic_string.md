# Bonus: Mystic string

Det finns ett hemligt meddelande gömt i strängen nedan. Extrahera det med hjälp av
indexering.

**Ledtrad:** meddelandet är åtta tecken långt.

```csharp
string message = "hellogoodmorningtogoodbye";
```

**Sa har du tanker:** Du kommer att indexera strängen — `message[0]` ger `'h'`,
`message[1]` ger `'e'`, och sa vidare. Vilka positioner ger ett sammanhangande
ord? Testa dig fram!

Skriv koden som plockar ut och skriver meddelandet:

```csharp
// Skriv din kod har
```

---

<details>
<summary><strong>Losningsforslag</strong></summary>

**Strangens index:**

| Index | Tecken |
|-------|--------|
| 0 | h |
| 1 | e |
| 2 | l |
| 3 | l |
| 4 | o |
| 5 | g |
| 6 | o |
| 7 | o |
| 8 | d |
| 9 | m |
| 10 | o |
| 11 | r |
| 12 | n |
| 13 | i |
| 14 | n |
| 15 | g |
| 16 | t |
| 17 | o |
| 18 | g |
| 19 | o |
| 20 | o |
| 21 | d |
| 22 | b |
| 23 | y |
| 24 | e |

**Losning:**
```csharp
Console.WriteLine(
    $"{message[5]}{message[6]}{message[16]}{message[17]}" +
    $"{message[0]}{message[1]}{message[2]}{message[3]}"
);

```


*"Den roligaste koden ar den som berattar en historia — men bara for den som letar."*
</details>
