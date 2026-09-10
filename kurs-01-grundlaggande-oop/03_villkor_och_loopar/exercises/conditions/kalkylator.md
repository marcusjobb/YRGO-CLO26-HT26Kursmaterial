# Övning — Kalkylatorn

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟡

Nattväktaren på serverrummet på Klippan Data AB behöver räkna ut kapacitetsvärden snabbt — utan att ta fram telefonen. Du bygger en minimal kalkylator som körs i terminalen: mata in två tal och ett räknesätt, få svaret direkt.

---

## Steg 1: Läs in tal och operator

Läs in två decimaltal (`double`) och ett operatortecken som en `string` (+, -, *, /). Använd `switch` på operatorn för att räkna ut och skriva ut resultatet.

### Förväntad output
```plaintext
Första talet: 15
Operator (+, -, *, /): *
Andra talet: 4
Svar: 15 * 4 = 60
```

```plaintext
Första talet: 10
Operator (+, -, *, /): -
Andra talet: 3.5
Svar: 10 - 3,5 = 6,5
```

## Steg 2: Hantera division med noll

Om användaren försöker dela med noll ska programmet skriva ett vänligt felmeddelande — inte krascha.

```plaintext
Första talet: 8
Operator (+, -, *, /): /
Andra talet: 0
Kan inte dela med noll.
```

## Steg 3: Okänd operator

```plaintext
Första talet: 5
Operator (+, -, *, /): %
Andra talet: 2
Okänd operator: %
```
