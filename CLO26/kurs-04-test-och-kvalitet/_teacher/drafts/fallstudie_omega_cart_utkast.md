# Fallet "The Omega Cart" — UTKAST

**Status:** Parkerad. Passar kurs-04 (test och kvalitet) — parallellt med SlarvigKod-genomgången.
**Att göra:**
- Marcusifiera språket ("tekniskt underverk", "kompakt och vackert" luktar AI)
- Ta bort Mailchimp-referensen (för specifikt)
- Bikupa-frågorna är bra men behöver TDD-kopplingen stärkas
- Kan fungera som inledning till varför enhetstester är omöjliga i tät kod

---

## Berättelsen i korthet

RetroGlow — e-handel för vintagedatorer. Senior-utvecklare sätter på synthwave, kodar i tre dygn, föder en 4 500 rader lång fil: `OmegaCartService`.

Den bryter mot varenda känd princip:
- Prisberäkningar, lagercheck, Mailchimp-anrop och HTML-strängar — allt i samma klass
- SQL-frågor inline i valideringsloopen
- CSS-klassnamnet `"btn-checkout-green"` hårdkodat i en if-sats för att identifiera betalningsflödet

**Black Friday.** Marknadschef vill byta text på knappen. Junior ändrar `btn-checkout-green` till `btn-checkout-retro`. Kreditkortsvalidering slutar fungera. Databas når max connections. Sajten nere 4 timmar. 2,4 miljoner i förlust.

Enhetstester var omöjliga att skriva — koden anropade databasen direkt.

---

## Diskussionsfrågor

1. Hur många olika klasser borde `OmegaCartService` ha delats upp i?
2. Vad är faran med att backend-kod är beroende av CSS-klassnamn?
3. Varför var det omöjligt att skriva enhetstester för den här koden — och vad hade TDD förhindrat?
