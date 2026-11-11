# UML med AI: använd prompten, förstå resultatet

Läsversion till passet om UML. Det du får med dig är en prompt som ger dig ett komplett UML-dokument för ett projekt. Den är ett verktyg, inte ett facit.

**Efter det här kan du:** använda en prompt för att få fram UML-diagram för ett projekt, läsa diagrammen och förklara dem för någon annan, och upptäcka när diagrammet inte stämmer med koden.

## Du får använda prompten

Det är helt OK att använda den. Den finns här för att ni ska kunna jobba snabbare och få se hur ett projekt är uppbyggt.

Men: **du ska kunna förstå och förklara diagrammet.** Ett diagram som du inte kan läsa är inte till någon nytta för dig, för kursen eller på en genomgång. Det är det som bedöms, inte om du skrev prompten själv.

## Så här gör du

1. Öppna projektet du vill dokumentera.
2. Ge AI:n tillgång till koden, till exempel genom att bifoga projektmappen, och klistra in prompten nedan.
3. Spara resultatet som `UML.md` i projektets rot.
4. Öppna `UML.md` där diagrammen renderas. GitHub och VS Code med ett Mermaid-tillägg fungerar. Visual Studio visar inte Mermaid i sin förhandsvisning.
5. Gå igenom diagrammen mot koden, enligt kontrollistan nedan.

## Kontrollista innan du förstår diagrammet

- Finns varje klass i diagrammet i koden? Finns varje klass i koden i diagrammet?
- Stämmer pilarna? Ärver en klass verkligen från den basklass som diagrammet visar?
- Stämmer metoderna och fälten, med rätt synlighet (`+`, `#`, `-`)?
- Stämmer programflödet med `Main` eller motsvarande startpunkt?
- Finns det metoder i en implementation som saknas i kontraktet? Varför?

Om något inte stämmer: rätta det i `UML.md` och skriv en rad om varför. Det är en del av uppgiften.

## Kunna förklara diagrammet

Välj ett diagram och svara på dessa tre frågor, utan att titta i diagrammet:

1. Vilken klass är startpunkten, och vad gör den?
2. Vilken klass vet om vilken, och varför behöver den veta det?
3. Om jag vill lägga till en ny variant, vilka filer behöver jag ändra? Varför just de?

Om du inte kan svara på någon av frågorna: läs koden igen, och titta på diagrammet en gång till. Det är där förståelsen sitter.

## Prompten

````markdown
# Prompt: Generera UML-dokumentation för ditt projekt

Analysera det projekt jag har klistrat in / bifogat / gett dig tillgång till,
och skapa en fil `UML.md` med fullständig UML-dokumentation i Mermaid.

## Steg 1 — Kartlägg koden

- Läs ALLA källkodsfiler (hoppa över `bin/`, `obj/`, `node_modules/` och annan build-output)
- Identifiera alla typer (klasser, interface, record, struct) med:
  - fält/properties och metoder, med synlighetsprefix: `+` publik, `#` skyddad, `-` privat
  - konstruktorer och om de gör något intressant (t.ex. skapar tabell, öppnar anslutning)
- Notera relationer:
  - arv och interface-implementeringar
  - komposition/aggregation (t.ex. `List<Katt>`-fält → `o--` med multiplicitet)
  - beroenden mellan klasser (vem skapar/använder vem)
  - startpunkt/entry point och vad den använder
  - externa paket (NuGet, npm, ...) och var data lagras (filer, databas)
- Följ kodflödet från entry point för att förstå körningsordningen

## Steg 2 — Skriv UML.md med dessa sektioner

1. **Klassdiagram** (`classDiagram`): alla typer fullt ut — medlemmar med
   synlighet, `«abstract»`/`«interface»`-stereotyper, arvspilar med etiketten
   `ärver`, aggregation (`o--` med multiplicitet), beroenden (`..>`) inklusive
   entry point-klassen som använder resten.
2. **Beroenden & paket** (`flowchart LR`): entry point → klasser →
   lagring/paket. Streckade pilar (`-.->`) för utkommenterade/valfria
   beroenden. Databas-form `[( )]` för databaser, filform för filer.
3. **Programflöde** (`flowchart TD`): huvudflödet från start till slut med
   beslutsnoder, menyval och terminaler `([Start])`/`([Slut])`.
4. **Sekvensdiagram** (`sequenceDiagram`): ett centralt use case end-to-end —
   deltagare, meddelanden och svar, inklusive lagring.
5. **Implementationer av kontrakt** (`flowchart TB`) — ENDAST om det finns
   basklasser/interface med flera implementationer: visa kontraktet överst och
   varje implementation med sina operationer.
6. **Not-ruta** (blockquote): iakttagelser om arkitekturen — t.ex. metoder som
   finns i en implementation men saknas i kontraktet, oavslutad kod,
   platshållare som kastar `NotImplementedException`.

Mellan diagrammen: kort förklarande text på svenska (2–5 punkter per diagram).
Under klassdiagrammet en **Läsning:**-lista med en rad per klass.

## Stilregler för Mermaid

- Körbar syntax ovan allt:
  - citattecken runt etiketter som innehåller specialtecken eller radbrytningar
  - `<br/>` för radbrytning i noder (aldrig `\n`)
  - `\\` för backslash i etiketter
  - undvik `(`, `)`, `[`, `]`, `{`, `}` i ociterad text
- Generiska typer i `classDiagram` skrivs med tilde: `List~Katt~`
- Abstrakta medlemmar markeras med `*` efter signaturen
- Granska varje diagram mot Mermaid-syntaxen innan resultatet lämnas —
  alla diagram ska rendera felfritt

## Ton och språk

- Svenska, korta pedagogiska förklaringar — läsaren ska förstå systemet utan
  att öppna koden
- Börja dokumentet med en kort intro (typ av app, mål-framework/SDK, syfte)
- Avsluta med Not-observationer som en kollega skulle göra vid genomgång

## Leverans

- Spara resultatet som `UML.md` i projektets rot — kan du inte skriva filer,
  visa hela innehållet i ett enda kodblock som går att kopiera
````
