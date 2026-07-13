# Continuous Delivery — att leverera kod utan drama

Förr i tiden levererades mjukvara i stora paket. Man jobbade i månader, slog ihop allt, och sedan hoppades man att ingenting gick sönder. Det gick ofta sönder. Releaser var händelser man fruktade, inte fräste.

Continuous Delivery är svaret på det.

---

## CI, CD och CD — tre förkortningar, tre saker

Det är lätt att blanda ihop de här tre begreppen, men de beskriver olika saker i kedjan.

**Continuous Integration (CI)** betyder att alla i teamet integrerar sin kod till en gemensam branch ofta — helst flera gånger om dagen. Varje push kör automatiska byggen och tester. Det handlar om att hålla kodbasen i ett fungerande tillstånd hela tiden, inte om att samla på sig ändringar och slå ihop dem i slutet av veckan.

**Continuous Delivery (CD)** bygger vidare på CI och innebär att koden alltid är redo att deployas till produktion. Man kan trycka på en knapp och få ut en release. Den slutliga knappklicken är manuell — men systemet garanterar att koden är i ett skick där det är säkert att göra det.

**Continuous Deployment** tar bort den knappen helt. Varje ändring som passerar alla automatiska tester går direkt ut i produktion, utan mänskligt ingripande. Det är den mest aggressiva varianten och kräver att testsviten är mycket pålitlig.

De flesta organisationer startar med Continuous Delivery och utvärderar sedan om Continuous Deployment passar dem.

---

## Deployment pipeline: kod som reser

Tänk dig koden som ett paket som ska levereras från en avsändare till en mottagare. Paketet passerar ett antal kontrollstationer innan det når fram. Det är det en deployment pipeline är.

En typisk pipeline ser ut ungefär så här:

**Build** — Koden kompileras och paketeras. Om den inte bygger, stannar allt här. Snabb feedback, inga dolda synkroniseringsproblem.

**Test** — Automatiska enhetstester, integrationstester och eventuella statiska analyser körs. Det är här de flesta problem fångas.

**Staging** — Koden driftsätts i en miljö som liknar produktion så noga som möjligt. Acceptanstester och prestandatester körs. Staging är sista kontrollen.

**Production** — Koden är live. Riktiga användare påverkas. Nu handlar det om övervakning, inte om testning.

Varje steg ska vara automatiserat och deterministiskt. Samma kod in, samma resultat ut — varje gång.

---

## Feature flags — koden är inne, funktionen är inte på

En vanlig missuppfattning är att "deploya" och "releasa" är samma sak. Det behöver de inte vara.

Feature flags är en teknik där man skickar ut kod i produktion, men stänger av funktionen med en konfigurationspärm. Koden finns där, den körs inte. När man är redo — eller bara för en delmängd av användarna — slår man på flaggan.

Det gör det möjligt att deploya ofta utan att exponera halvfärdiga funktioner. Det separerar teknisk leverans från affärsbeslut. "Vi deployas idag, men vi lanserar den nya checkout-sidan nästa tisdag" är ett vanligt resonemang i team som arbetar med feature flags.

---

## Blue-green och canary — försiktig lansering i produktion

Även med en solid pipeline kan man vilja begränsa risken när något nytt går ut.

**Blue-green deployment** innebär att man har två identiska produktionsmiljöer. En är live (blue), den andra tar emot den nya versionen (green). När green är klar och verifierad, byter man trafiken dit. Rollback tar sekunder — man pekar bara om till blue igen.

**Canary releases** är mer graduellt. Man skickar en liten andel av trafiken — kanske 1-5 % — till den nya versionen. Ser det bra ut mätt i felokvot och svarstider, rullar man ut till fler. Går det dåligt, drar man tillbaka utan att majoriteten av användarna märkt något.

---

## DORA metrics — hur vet man att det faktiskt fungerar?

DevOps Research and Assessment-gruppen identifierade fyra mätvärden som förutsäger om ett team levererar bra:

- **Deployment frequency** — hur ofta deployas det till produktion? Elitteam deployas flera gånger om dagen.
- **Lead time for changes** — hur lång tid tar det från en committad kodrad till att den är i produktion?
- **Mean time to recovery (MTTR)** — hur snabbt återhämtar sig systemet efter ett incident?
- **Change failure rate** — hur stor andel av deployments orsakar ett problem som måste åtgärdas?

De fyra hänger ihop. Hög deployment frequency kombinerat med låg change failure rate är det kombinationen som skiljer elitteam från medelmåttan.

---

## Varför testsviten är piplinen hela grunden

Continuous Delivery är bara trovärdig om testsviten är pålitlig. En pipeline som ibland lyckas, ibland misslyckas av slumpmässiga orsaker — flaky tests — är värdelös. Det är som ett brandlarm som larmar på slump: man slutar lita på det.

En solid testsvit innebär att en grön pipeline faktiskt betyder att koden är bra. Det är det som gör att man vågar trycka på knappen.

---

Continuous Delivery är till hälften teknik och till hälften kultur — ett lag som deployas sällan men dramatiskt har inte ett pipline-problem, det har ett förtroendeproblem, och det löser man inte med verktyg ensamt.
