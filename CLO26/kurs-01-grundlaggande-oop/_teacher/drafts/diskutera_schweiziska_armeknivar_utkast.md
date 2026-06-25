# Den magiska schweiziska armékniven — UTKAST

**Status:** Parkerad. Passar vecka 3 (klasser och OOP) — efter att de sett vad kopplad kod faktiskt ser ut i praktiken.
**Att göra:**
- Ta bort "Varför berättar vi det här på er första dag?" — för mycket moralkaka
- Presentera SRP/KISS/SoC som begrepp att diskutera, inte som regler att memorera
- Marcusifiera språket — "supersmart", "bara flyter fram" är okej, men structurera om slutsektionen
- Gör bikupa-frågorna mer öppna — fråga 2 är lite ledande ("varför tyckte utvecklaren det var bra?")

---

## Berättelsen

Ett stort företag som säljer skor på nätet ska bygga en ny digital kundvagn. De ger uppdraget till en jätteduktig utvecklare.

Utvecklaren sätter på sig hörlurarna, hamnar i "zonen" och bygger. Det känns fantastiskt bra, koden bara flyter fram. Men istället för att bygga en sak i taget vill utvecklaren vara smart. Utvecklaren bygger en **schweizisk armékniv**.

När kundvagnen var klar efter några dagar gjorde den inte bara det en kundvagn ska göra. Den hade blivit ett monster:

1. Den räknade ut priset och drog av rabatter.
2. Den pratade direkt med lagret för att se om skorna fanns kvar.
3. Den byggde designen och färgen på "Köp"-knappen i webbläsaren.
4. Den skickade automatiskt ett reklammejl till kunden om de ångrade sig.

Allt i en och samma långa kodfil.

## Katastrofen

Årets största reasdag. Sajten proppfull. Marknadschefen kommer springande:

*"Texten på Köp-knappen måste ändras från 'Köp nu' till 'Slutför köp' — annars matchar det inte reklamkampanjen."*

En junior utvecklare ändrar en enda textrad.

Sajten kraschade inom två minuter. Nere i fyra timmar. Miljontals kronor.

Allt för att någon ändrade texten på en knapp.

---

## Bikupor

1. Om ni ska städa ett kök — är det bäst att en person gör allt samtidigt, eller att man delar upp uppgifterna? Hur relaterar det till historien?
2. Vem bär störst ansvar för att sajten kraschade?
