# Kodgranskning i praktiken

🟢


Tester fångar buggar, men kodgranskningar fångar människorna bakom buggarna. När kollegor läser din kod
innan den går ut känns det kanske nervöst – men resultatet blir bättre kod, större kunskap och färre
"oj då" i produktion.

## TL;DR

- Kodgranskning är en konversation, inte en domstol.
- Små pull requests är lättare att granska, lättare att förstå och snabbare att godkänna.
- Använd checklistor så att viktiga saker (säkerhet, loggning, null-kontroller) inte faller mellan stolarna.

## Varför vi gör det

- **Kvalitet:** Fler ögon hittar kanter du missat.
- **Kunskapsdelning:** Du lär dig nya idiom, teamet får koll på fler kodytor.
- **Busfaktor:** Om någon är sjuk kan någon annan ändå ta över.
- **Stil:** Enhetligt kodspråk gör projektet lättare att underhålla.

## Så här förbereder du pull requesten

1. **Håll den fokuserad:** En funktion, en buggfix. En gigantisk "allt på en gång"-PR gör alla trötta.
2. **Beskriv vad som ändrats:** Två meningar om "varför" hjälper varje granskare.
3. **Länka kontext:** Issue-id, designskiss, API-kontrakt – allt som förklarar varför du valt lösningen.
4. **Skriv egna notiser:** "Jag testade X men valde Y på grund av..." sparar tid och visar att du tänkt igenom alternativ.
5. **Kör tester lokalt:** Det finns inget värre än att någon annan behöver säga "den kraschar".

## Checklistor att bocka av

| Område             | Fråga                                                                 |
| ------------------ | --------------------------------------------------------------------- |
| Logik              | Koden gör exakt det som står i beskrivningen?                         |
| Felhantering       | Vad händer vid null, tom lista, timeout?                              |
| Säkerhet           | Finns inputvalidering, proper auth/claims?                            |
| Prestanda          | Är eventuell LINQ/loop rimlig för mängden data?                       |
| Naming & struktur  | Är variabelnamn tydliga, är metoderna lagom korta?                    |
| Testbarhet         | Finns tester? Om nej: varför?                                         |
| Dokumentation      | Behövs README/API-kommentarer uppdateras?                            |

Skriv gärna ut checklistan i din PR-template så att alla följer samma vana.

## Tips för granskaren

- **Läs beskrivningen först.** Hoppa inte direkt in i diffen – fattar du syftet går allt snabbare.
- **Fokusera på risk.** Lägg mer tid på säkerhet, databas och parallellism än på whitespace.
- **Var tydlig med ton.** "Kan vi byta ut `DateTime.Now` mot `DateTime.UtcNow`?" låter bättre än "Det här är fel".
- **Godkänn inte allt på en gång.** Markera gärna "Approved pending" när småsaker återstår.
- **Testa lokalt när det behövs.** Speciellt när UI eller komplex logik ändrats.

## Tips för den som får feedback

- Se kommentarer som en present, inte ett personligt angrepp.
- Fråga när du inte förstår: "Vad menar du med.. ?" är bättre än att gissa.
- Besvara varje kommentar, även "fixat". Då vet granskaren att du sett den.
- Reagera snabbt. Om du behöver längre tid, skriv det.

## Verktyg som hjälper

- **Pull Request Templates** – checklista + sammanfattning återanvänds automatiskt.
- **GitHub Suggested Changes** – låt granskaren klistra in kodsnutten direkt.
- **Dotnet format & analyzers** – automatisera stilfrågor så att granskningen kan fokusera på logiken.
- **Pair review** – ibland är 10 minuter över Teams det snabbaste sättet att lösa en diskussion.

## När hoppar du över granskning?

- Fixar i pipeline/skript som redan granskas automatiskt.
- Dokumentation som inte påverkar kodspel.
- Prototyper/spikes som ändå kastas. Men skriv gärna "ingen granskning – slängs sen" så att alla vet.

## Sammanfattning

- Kodgranskning är teamets brandvarnare. Den ska pipa innan det börjar ryka.
- Förbered PR:en tydligt, håll den liten och kör tester innan du skickar upp den.
- Använd checklistor för att hålla fokus, inte vinna diskussioner.
- Feedback ska vara saklig och respektfull – annars tappar processen värdet.

## Dad joke

Varför älskar pull requests kaffe? För att de alltid får en "review" innan de blir serverade.

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
