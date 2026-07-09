# Inlämningsuppgift — Tärningsspelet

**Vecka:** 03  
**Deadline:** Söndag [vecka 3] 23:59  
**Förlängd deadline:** Fredag [vecka 4] 23:59  
**Inlämning:** Länk till din fork i Google Classroom

---

## Bakgrunden

Lasse och Kalle sitter vid köksbordet och slåss om vem som ska diska.

Reglerna är enkla: den som får flest poäng på fem omgångar slipper. Det finns en tärning. Det finns ingen rättvisa. Det finns bara slumpen.

Din uppgift är att bygga det spelet.

---

## Vad gäller för den här inlämningen

*Som utbildare vill jag att du...*

- [ ] kan använda `Random` för att simulera slump
- [ ] kan använda en loop för att upprepa ett förlopp ett bestämt antal gånger
- [ ] kan spåra och summera värden med variabler
- [ ] kan skriva en enkel metod och anropa den
- [ ] kan använda `if`/`else` för att jämföra resultat och presentera en vinnare

*Bocka av dem själv innan du lämnar in.*

---

## Hur man skapar slumpmässiga tal i C#

```csharp
Random random = new Random();
int roll = random.Next(1, 7); // ger ett tal mellan 1 och 6 (7 är exklusivt)
```

---

## Krav för Godkänt (G)

- [ ] Programmet skapar ett `Random`-objekt och använder det för att "kasta" tärningen
- [ ] Spelet körs i **minst 5 omgångar** via en loop
- [ ] Spelet håller reda på **båda spelarnas totalpoäng** med variabler
- [ ] Varje omgång skriver ut vad respektive spelare slog
- [ ] Efter alla omgångar skrivs totalpoängen ut och en vinnare presenteras (eller oavgjort)
- [ ] Programmet innehåller **minst en metod**, till exempel `static int RollDice(Random random)`
- [ ] Ingen `var` används

---

## Krav för Väl Godkänt (VG)

*Alla G-krav måste vara uppfyllda. VG är ett tillägg, inte en ersättning.*

- [ ] Tärningskastet är extraherat till en tydlig metod (`RollDice` eller liknande) som används konsekvent
- [ ] Oavgjort hanteras korrekt och skrivs ut på ett tydligt sätt
- [ ] Koden är välstrukturerad och lätt att läsa — bra variabelnamn, logisk uppdelning
- [ ] Det finns kommentarer som förklarar vad de viktiga delarna gör

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

*(Ditt program ska producera liknande output — exakta siffror varierar eftersom de är slumpmässiga.)*

---

## Tips

Det finns inga startfiler. Du bygger detta från scratch.

Börja med att skriva pseudokod på papper — vad ska hända i varje steg — innan du öppnar VS Code.  
Om du fastnar i mer än 15 minuter: fråga klassen → AI → Marcus. I den ordningen.

---

## Bedömning

Rättning sker veckan efter deadline.  
Feedback per mail och i Google Classroom.  
Commits efter deadline beaktas inte — spara commit-hashen du lämnar in på.
