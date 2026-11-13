# Berättelsen: Verkstaden Hjulet

Veckans röda tråd. Alla lektioner, övningar och exempel den här veckan utspelar sig i samma verkstad, med samma människor och samma kod.

## Platsen

**Verkstaden Hjulet** på Hisingen i Göteborg. En cykelverkstad med tre mekaniker, en mottagning och ett bokningssystem som växt fram under åtta år.

## Människorna

- **Nils**, mekaniker i 30 år. Han skriver snabb kod, och han vet exakt hur systemet fungerar, men bara i huvudet. Hans kod är full av smells, och han ångrar det inte. Ännu.
- **Elin**, nyanställd utvecklare, med fyra månader i verkstan. Hon ska få ordning på bokningsappen innan säsongens första rusning.
- **Sara**, kund. Hon kommer varje vår med en gammal cykel och en lång lista med önskemål.

## Bokningsappen

Appen räknar ut vad en servicetid kostar.

- Arbetstid: 650 kr per timme.
- Delar läggs till efter inköpspris.
- Moms: 25 % på hela summan.
- Registreringsnummer och telefonnummer för kunden ska kontrolleras innan bokningen sparas.

Nils version är en enda metod, `CalculateEverything`, med 200 rader och en del magiska tal.

## Veckans förlopp

| Lektion | Vad händer på Hjulet |
|---|---|
| 01 Code smells | Elin öppnar `CalculateEverything` för första gången och räknar smellsen. |
| 02 Refaktorering | Elin bryter ut `CalculateVat` och byter namn på `x` till `hours`. Nils ser och säger: "Det var det jag menade." |
| 03 SOLID, DRY och KISS | Elin delar upp klassen `BookingManager` i tre: bokning, prissättning och utskrift. Nils lär sig varför. |
| 04 Kodgranskning | Elin och Nils granskar varandras ändringar i par, med checklista. De är inte alltid överens. |
| 05 Kanban | Hjulet har en tavla på väggen. Nils flyttar kort till Working, och Elin ser flaskhalsen i Test. |
| 06 AI-planering och Trello | Elin ber en AI dela upp säsongens städning. Nils granskar, och de bestämmer tillsammans vad som stämmer. |
| 07 Regex grund | Sara skriver ett registreringsnummer med mellanslag, och appen måste förstå det. |
| 08 Regex ankare och misstag | En kund skriver in ett nummer som bara innehåller rätt del av texten. Appen släpper igenom det. Elin hittar felet. |

## Så använder du berättelsen

- Varje bildspel börjar med en scen från verkstan.
- Övningarna använder samma kod och samma kunder.
- Lösningarna visar hur Elin tänkte, inte bara vad hon skrev.
