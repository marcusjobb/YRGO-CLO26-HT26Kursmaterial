# Övning — Rätt hälsning

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟢

Receptionen på Hotel Riviera vill ha ett nytt check-in-system. Receptionisten ska slippa tänka på klockslaget — systemet hälsar automatiskt rätt beroende på om gästen anländer på morgonen, dagen, kvällen eller natten.

---

## Steg 1: Hälsa rätt

Läs in ett klockslag som heltal (0–23). Använd `switch` med grupperade `case`-grenar för att skriva ut rätt hälsning.

Gruppering:
- 6–11 → God morgon!
- 12–17 → God dag!
- 18–22 → God kväll!
- 23, 0–5 → God natt!

### Förväntad output
```plaintext
Ange timme (0-23): 9
God morgon!

Ange timme (0-23): 14
God dag!

Ange timme (0-23): 20
God kväll!

Ange timme (0-23): 2
God natt!
```

## Steg 2: Ogiltigt klockslag

Vad bör programmet skriva om användaren anger 24 eller -1? Lägg till ett `default`-fall.

```plaintext
Ange timme (0-23): 25
Ogiltigt klockslag.
```
