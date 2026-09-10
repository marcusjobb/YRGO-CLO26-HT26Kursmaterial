# Övning — Bankräntan

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟢

Du har precis fått sommarjobb och lagt 5 000 kr på sparkontot. Banken lovar 3,5 % ränta per år. Hur mycket har du om tio år? Skriv ett program som räknar ut saldot år för år med en `for`-loop.

---

## Steg 1: År för år

Läs in startbelopp, ränta i procent och antal år från användaren. Loopa sedan och visa saldot för varje år.

### Förväntad output (exempel: 5000 kr, 3.5%, 5 år)
```plaintext
År 1: 5 175,00 kr
År 2: 5 356,13 kr
År 3: 5 543,59 kr
År 4: 5 737,61 kr
År 5: 5 938,43 kr
```

## Steg 2: Sammanfattning

Lägg till en rad sist som visar hur mycket ränta du tjänade totalt.

### Förväntad output (fortsättning)
```plaintext
Insatt belopp:  5 000,00 kr
Slutsaldo:      5 938,43 kr
Ränta totalt:     938,43 kr
```

## Tips

- Saldot varje år: `saldo = saldo * (1 + ranta / 100)`
- Använd `double` för alla beräkningar
- Formatera med `{saldo:N2}` för två decimaler och tusentalsavgränsare
