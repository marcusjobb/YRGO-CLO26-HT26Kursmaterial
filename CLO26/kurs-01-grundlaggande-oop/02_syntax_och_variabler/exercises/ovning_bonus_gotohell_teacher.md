# Bonus: GOTO HELL — Lärarversion / Hemlig utmaning

*PUBLICERAS INTE förrän vi är på chars/strängar (vecka 2 eller 3)*
*Studerandefil skapas när det är dags: `ovning_bonus_gotohell.md`*

---

## Idén

Studerande får en till synes oskyldig sträng och uppmaningen att "extrahera ett hemligt meddelande".
Utan att de vet vad meddelandet är — det hittar de genom att indexera sig fram.

Är både ett GOTO-skämt OCH en pedagogisk poäng: GOTO är ett historiskt ökänt programmeringskoncept.

> **Dijkstra (1968):** *"Go To Statement Considered Harmful"* — ett av de mest citerade breven i programmeringshistorien. Argumenterade för att GOTO-satser gör kod oläslig och svårunderhållen. Lade grunden för strukturerad programmering.

---

## Strängen och lösningen

```csharp
string message = "hellogoodmorningtogoodbye";
```

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

**Lösning:**
```csharp
Console.WriteLine(
    $"{message[5]}{message[6]}{message[16]}{message[17]}" +
    $"{message[0]}{message[1]}{message[2]}{message[3]}"
);
// gotohell
```

---

## Studerandeuppgiften (när den publiceras)

Ge studerande strängen och uppmaningen:
> *"Det finns ett hemligt meddelande gömt i den här strängen. Extrahera det med hjälp av indexering. Ledtråd: det är åtta tecken långt."*

Ingen mer info. Låt dem utforska.

---

## Efterdiskussion

När de hittat det — förklara GOTO-referensen:
- GOTO är en äldre programmeringskonstruktion som hoppar till en godtycklig rad i koden
- Dijkstra argumenterade 1968 för att det gör kod oläslig ("spaghettikod")
- "goto hell" = vad koden-du-skriver hamnar i om du använder GOTO utan att tänka
- I C# finns `goto` faktiskt kvar — men används nästan aldrig

> 💬 *"Den roligaste koden är den som berättar en historia — men bara för den som letar."*
