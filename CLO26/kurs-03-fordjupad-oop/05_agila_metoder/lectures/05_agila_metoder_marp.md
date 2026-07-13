---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Agila Metoder och Kanban

**Kurs:** Fördjupad OOP
**Modul:** 05 — Agila metoder

Marcus Ackre Medina · YRGO · CLO26

---

## Varför pratar vi om det här?

Du kan snart skriva kod. Men kan du leverera?

Det är skillnaden mellan att vara en junior som sitter ensam och knappar — och en junior som faktiskt funkar i ett team.

Agila metoder är hur de flesta dev-team jobbar idag. Scrum, Kanban, sprints, backlogs — du kommer stöta på det dag ett på din LIA-plats.

Bättre att förstå det nu än att nicka förvirrat i standups.

---

## Agila manifestet — 2001

År 2001 samlades 17 mjukvaruutvecklare på ett skidresort i Utah.

De var trötta på vattenfallsprojekt som levererade fel sak, för sent, för dyrt.

Resultatet: **The Agile Manifesto** — 4 värderingar och 12 principer.

> _"We are uncovering better ways of developing software by doing it and helping others do it."_

Det är fortfarande grunden för hur de flesta team jobbar idag.

---

## De 4 värderingarna — översikt

Manifestet säger **inte** att det ena är dåligt. Det säger att det ena värderas **mer**.

| Vi värdesätter... | ...mer än |
|---|---|
| Individer och interaktion | Processer och verktyg |
| Fungerande programvara | Uttömmande dokumentation |
| Kundsamarbete | Kontraktsförhandling |
| Anpassning till förändring | Att följa en plan |

Vi tar dem en i taget.

---

## Värdering 1 — Individer och interaktion

> _"Individuals and interactions over processes and tools"_

En Jira-bräda med 200 kort löser inga problem om teamet inte pratar med varandra.

I praktiken: gå och prata med din kollega istället för att skapa ett ticket. Stå upp i standup och säg vad som faktiskt blockerar dig. Fråga om hjälp — det är inte svaghet, det är agilt.

Verktyg hjälper. Kommunikation är kärnan.

---

## Värdering 2 — Fungerande programvara

> _"Working software over comprehensive documentation"_

Kunden vill se appen köra, inte en 80-sidig kravspec.

I praktiken: leverera något litet men fungerande varje sprint. En halvfärdig feature som fungerar är mer värd än 10 features i ett Word-dokument.

Dokumentation behövs — men den ska tjäna koden, inte tvärtom.

---

## Värdering 3 — Kundsamarbete

> _"Customer collaboration over contract negotiation"_

Om du träffar kunden dag 1, skriver kontrakt, och sedan inte ses igen på 6 månader — då vet du redan hur det slutar.

I praktiken: bjud in kunden till Sprint Reviews. Visa vad som byggts. Fråga om det är rätt. Byt riktning tidigt om det behövs, inte när projektet är "klart".

---

## Värdering 4 — Anpassning till förändring

> _"Responding to change over following a plan"_

Planer är bra. Men världen ändras. Kunder ändrar sig. Marknader ändras.

I praktiken: om du planerat feature X men kunden nu behöver feature Y — ändra planen. Det är inte misslyckande, det är poängen med agilt.

_Det krävs mod att säga "vi gör om det här". Det är en professionell egenskap._

---

## Waterfall — allt planeras i förväg

Waterfall är sekventiellt: krav → design → implementation → test → leverans.

```
Vecka 1–4:   Krav och analys
Vecka 5–10:  Design
Vecka 11–20: Programmering
Vecka 21–24: Test
Vecka 25:    Leverans
```

Problemet: när kunden ser resultatet på vecka 25 vill de ha något annat.

Waterfall passar bra när kraven är stabila och förändringar är dyra — t.ex. byggnadsprojekt, rymdfärjor.

---

## Agile — leverera kontinuerligt

Istället för att vänta månader levererar du **litet och ofta**.

```
Sprint 1:  Logga in funkar → visa kunden
Sprint 2:  Produktlista funkar → visa kunden
Sprint 3:  Kundvagn funkar → visa kunden
```

Kunden ser riktig mjukvara tidigt. Om något är fel hittar du det efter 2 veckor — inte efter 6 månader.

_Feedback tidigt kostar lite. Feedback sent kostar massor._

---

## Scrum — översikt

Scrum är det vanligaste agila ramverket. Det är inte en metod med exakta regler — det är ett ramverk med tydliga roller, ceremonier och artefakter.

**Tre roller**
Product Owner · Scrum Master · Development Team

**Fyra ceremonier**
Sprint Planning · Daily Scrum · Sprint Review · Retrospective

**Tre artefakter**
Product Backlog · Sprint Backlog · Increment

---

## Scrum — roller

**Product Owner (PO)**
Äger Product Backlog. Prioriterar vad som ska byggas. Representerar kunden och affärsvärdet. _Inte_ en projektledare.

**Scrum Master (SM)**
Faciliterar. Tar bort hinder. Ser till att Scrum fungerar. Coachar teamet. _Inte_ en chef.

**Development Team**
3–9 personer. Självorganiserande. Ansvarar för leveransen. Alla roller i ett — dev, test, design.

---

## Scrum — ceremonier

**Sprint Planning** — vad ska vi göra den här sprinten?
Teamet väljer uppgifter från Product Backlog och gör en plan.

**Daily Scrum** — 15 minuter, varje dag
Tre frågor: Vad gjorde jag igår? Vad gör jag idag? Finns det något som blockerar mig?

**Sprint Review** — vad byggde vi?
Demo för kunden/intressenter. Feedback in i backloggen.

**Retrospective** — hur jobbade vi?
Vad gick bra? Vad ska vi förbättra? En konkret förbättring till nästa sprint.

---

## Scrum — artefakter

**Product Backlog**
En prioriterad lista med allt teamet *kan* bygga. Levande dokument — förändras hela tiden.

**Sprint Backlog**
Uppgifterna teamet *valt* för den här sprinten. Fixat tills sprinten är klar.

**Increment**
Det som faktiskt levererades — fungerande mjukvara som uppfyller Definition of Done.

_Artefakterna svarar på: vad finns att göra? Vad gör vi nu? Vad har vi byggt?_

---

## Sprint — vad händer egentligen?

En sprint är en tidsbegränsad arbetsperiod. Vanligtvis **1–4 veckor** — många team kör 2 veckor.

```
Dag 1:        Sprint Planning — välj uppgifter
Dag 2–13:     Bygg. Daglig standup. Löpande.
Dag 14:       Sprint Review + Retrospective
Dag 15:       Ny Sprint Planning → upprepa
```

Under sprinten: inga nya uppgifter läggs till. Målet är fast.

Efter sprinten: något fungerande ska finnas — inte halvklart, inte "nästan klart".

---

## Kanban — visualisera arbetet

Kanban kommer från Toyota på 1950-talet. Grundidén: **gör arbetet synligt**.

En Kanban-tavla har kolumner som representerar flödet:

```
| Att göra | Pågående | Granskning | Klart |
|----------|----------|------------|-------|
| Uppgift A| Uppgift B| Uppgift C  |       |
| Uppgift D|          |            |       |
```

Varje uppgift är ett kort. Du drar kortet framåt när du börjar jobba.

Ingen sprint. Inget planerat block. Kontinuerligt flöde.

---

## Kanban — WIP-gränser

WIP = Work in Progress. WIP-gränser sätter ett tak på hur många uppgifter som får vara i en kolumn samtidigt.

```
| Att göra | Pågående (max 3) | Granskning (max 2) | Klart |
```

Varför? Multitasking är en myt. Ju fler saker på gång, desto mer tappar du fokus — och allt tar längre tid.

WIP-gränsen tvingar teamet att **slutföra** innan de börjar något nytt.

_"Stop starting. Start finishing."_

---

## Scrum vs Kanban — när passar vilket?

| | Scrum | Kanban |
|---|---|---|
| **Arbetsflöde** | Sprints (tidsbegränsat) | Kontinuerligt |
| **Planering** | Sprint Planning | Vid behov |
| **Roller** | PO, SM, Dev Team | Inga definierade roller |
| **Ändringar** | Inte under sprinten | Kan alltid läggas till |
| **Passar för** | Produktutveckling | Support, ops, underhåll |

Många team mixar — "Scrumban". Kör sprints men har en Kanban-tavla för löpande buggar.

---

## User Stories — ett gemensamt språk

En User Story är ett enkelt sätt att formulera en funktionalitet ur **användarens** perspektiv.

```
Som [roll]
vill jag [funktionalitet]
så att [nytta]
```

**Exempel:**
> Som inloggad kund vill jag kunna spara en adress så att jag inte behöver fylla i den igen vid nästa köp.

User Stories håller fokus på värdet — inte på tekniken. "Lägga till address-tabell i databasen" är inte en User Story. Det är en teknisk uppgift.

---

## Story Points och Planning Poker

Story Points mäter **relativ storlek** — inte tid. En 8-poängsuppgift är ungefär dubbelt så stor som en 4-poängsuppgift.

Vi använder **Fibonacci-serien**: 1, 2, 3, 5, 8, 13, 21 — för att tvinga tydliga val.

**Planning Poker:**
1. PO läser upp en User Story
2. Alla väljer en siffra hemligt
3. Alla visar sina kort samtidigt
4. Diskutera skillnader — tills ni är ense

Poängen är diskussionen, inte siffran.

---

## Definition of Done

"Klart" är inte när du pushat till main.

**Definition of Done (DoD)** är teamets gemensamma avtal om vad "klart" faktiskt betyder.

**Exempel på en DoD:**
- ✅ Koden är skriven och fungerar
- ✅ Unit-tester är skrivna och gröna
- ✅ Code review är genomförd
- ✅ Driftsatt i test-miljö
- ✅ Dokumentation uppdaterad

Utan DoD kommer teamet ha 17 olika definitioner av "klart". Det slutar alltid illa.

---

## Agila metoder i det här projektet

Nu när du vet vad det heter — du gör det redan.

I kursens projektarbete använder vi:

- **Kanban-tavla** — GitHub Projects eller en fysisk whiteboard
- **User Stories** — beskriv features ur slutanvändarens perspektiv
- **Definition of Done** — sätt er egna kriterier i gruppen
- **Korta iterationer** — leverera något fungerande varje vecka, inte allt på slutet

Du behöver inte köra Scrum fullt ut för att tänka agilt. Börja med tavlan och DoD.

---

<!-- _class: title -->

## Sammanfattning

- ✅ Agila manifestet (2001) — 4 värderingar, 12 principer
- ✅ Waterfall levererar en gång; Agile levererar kontinuerligt
- ✅ Scrum — roller (PO, SM, Team), ceremonier (Planning, Daily, Review, Retro), artefakter (Backlog, Increment)
- ✅ Sprint — tidsbegränsat block, fast mål, fungerande leverans
- ✅ Kanban — synligt flöde, WIP-gränser, kontinuerligt
- ✅ User Stories — "Som [roll] vill jag [X] så att [nytta]"
- ✅ Story Points — relativ storlek, Planning Poker
- ✅ Definition of Done — teamets gemensamma avtal om "klart"

**Nästa gång: Projektarbete**
