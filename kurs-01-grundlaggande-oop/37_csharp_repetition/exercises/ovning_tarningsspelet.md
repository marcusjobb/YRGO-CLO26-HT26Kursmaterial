# Övning — Tärningsspelet 🎲

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟡

---

## Bakgrunden

Lasse och Kalle sitter vid köksbordet och slåss om vem som ska diska.

Reglerna är enkla: den som får flest poäng på fem omgångar slipper. Det finns en tärning. Det finns ingen rättvisa. Det finns bara slumpen.

Din uppgift är att bygga det spelet.

---

## Vad du tränar på

*Som utbildare vill jag att du...*

- [ ] kan använda `Random` för att simulera slump
- [ ] kan använda en loop för att upprepa ett förlopp ett bestämt antal gånger
- [ ] kan spåra och summera värden med variabler
- [ ] kan skriva en enkel metod och anropa den
- [ ] kan använda `if`/`else` för att jämföra resultat och presentera en vinnare

---

## Hur man skapar slumpmässiga tal i C#

```csharp
Random random = new Random();
int roll = random.Next(1, 7); // ger ett tal mellan 1 och 6 (7 är exklusivt)
```

---

## Bonusutmaning 🔴

- Extrahera tärningskastet till en tydlig metod som används konsekvent
- Hantera oavgjort korrekt med ett tydligt meddelande
- Lägg till en tredje spelare
- Låt spelarna kasta om vid lika poäng tills någon vinner

---

## Exempeloutput

```
=== TÄRNINGSSPEL ===
Omgång 1: Spelare 1 slog 4, Spelare 2 slog 2
Omgång 2: Spelare 1 slog 1, Spelare 2 slog 6
Omgång 3: Spelare 1 slog 5, Spelare 2 slog 5
Omgång 4: Spelare 1 slog 3, Spelare 2 slog 4
Omgång 5: Spelare 1 slog 6, Spelare 2 slog 1

Spelare 1 totalpoäng: 19
Spelare 2 totalpoäng: 18
Spelare 1 vinner!
```

---

## Tips

Det finns inga startfiler. Du bygger detta från scratch.

Börja med att skriva pseudokod på papper — vad ska hända i varje steg — innan du öppnar VS Code.  
Om du fastnar i mer än 15 minuter: fråga klassen → AI → Marcus. I den ordningen.
