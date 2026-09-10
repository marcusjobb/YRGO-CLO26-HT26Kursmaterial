# Övning — Rymdfärja-start

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟡

Du är kontrollingenjör på ESA i Kourou, Franska Guyana. Uppskjutningen av Ariane 6 är om 10 minuter. Fem kritiska delsystem måste kontrolleras — ett i taget — innan starten kan godkännas. Varje kontroll slumpar fram ett resultat. Får ni grönt på alla fem är det dags att tända motorerna.

---

## Steg 1: Kontrollera de fem delsystemen

Loopa igenom de fem delsystemen med en `for`-loop. Namnge delsystemen med fem enkla `string`-variabler (eller skriv namnen direkt i loop-kroppen med ett `switch`-uttryck — välj själv). För varje system: slumpa ett heltal 1–10 med `Random`. Om värdet är 4 eller högre är systemet OK. Annars: fel.

Håll räkning på antalet fel med en `int`-variabel. Efter loopen: om inga fel → godkänn uppskjutning, annars → avbryt och visa antalet fel.

### Förväntad output — alla gröna
```plaintext
Kontrollerar delsystem 1: Framdrivning ... OK
Kontrollerar delsystem 2: Navigering ... OK
Kontrollerar delsystem 3: Kommunikation ... OK
Kontrollerar delsystem 4: Bränslesystem ... OK
Kontrollerar delsystem 5: Livsstöd ... OK

Uppskjutning godkänd! T-minus 10 sekunder.
```

### Förväntad output — fel upptäckta
```plaintext
Kontrollerar delsystem 1: Framdrivning ... OK
Kontrollerar delsystem 2: Navigering ... FEL
Kontrollerar delsystem 3: Kommunikation ... OK
Kontrollerar delsystem 4: Bränslesystem ... FEL
Kontrollerar delsystem 5: Livsstöd ... OK

Uppskjutning avbruten. 2 fel hittades.
```
