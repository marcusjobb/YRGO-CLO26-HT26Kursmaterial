---
marp: true
theme: nion-dark
paginate: true
---

# SCRUM och agilt arbetssätt

**Kurs:** Test och kvalitetssäkring
**Modul:** 04 — SCRUM och agilt
Marcus Ackre Medina · YRGO · CLO26

---

## Varför agilt?

Traditionell mjukvaruutveckling:

- Lång planering → lång utveckling → leverans → "Det var inte vad vi ville"

Agilt:

- Kort cykel → leverans → feedback → justera → kort cykel igen

**Agilt handlar om att kunna anpassa sig, inte om att gå snabbt.**

---

## Det agila manifestet (2001)

Fyra värderingar:

- **Individer och interaktioner** framför processer och verktyg
- **Fungerande programvara** framför omfattande dokumentation
- **Kundsamarbete** framför kontraktsförhandling
- **Anpassning till förändring** framför att följa en plan

Det finns värde i punkterna till höger — men vänster prioriteras.

---

## Vad är SCRUM?

SCRUM är ett ramverk för att hantera komplexa projekt.

Det bygger på tre pelare:

1. **Transparens** — alla vet vad som händer
2. **Inspektion** — kontrollera regelbundet
3. **Anpassning** — justera när det behövs

---

## SCRUM-rollerna

| Roll | Ansvar |
|------|--------|
| **Product Owner (PO)** | Prioriterar backloggen, representerar kunden |
| **Scrum Master (SM)** | Skyddar teamet, faciliterar ceremonier, tar bort hinder |
| **Development Team** | Bygger produkten, självorganiserat |

---

## Sprinten

En sprint är en **time-boxad period** på 1–4 veckor.

I slutet av varje sprint levereras ett fungerande inkrement.

```
Sprint Planning
     ↓
  Dagligt arbete
  + Daily Standup
     ↓
Sprint Review      ← Demo för intressenter
     ↓
Sprint Retrospective ← Teamets förbättring
     ↓
Ny sprint
```

---

## Sprint Planning

I starten av varje sprint:

- PO presenterar prioriterade items från backloggen
- Teamet diskuterar och estimerar
- Teamet väljer **vad** som ryms i sprinten
- Teamet bestämmer **hur** de ska lösa det

Resultat: **Sprint Backlog** + ett tydligt **Sprint Goal**.

---

## Daily Standup — 15 minuter

Stående möte, varje dag, max 15 minuter.

Tre frågor per person:

1. Vad gjorde jag igår?
2. Vad ska jag göra idag?
3. Finns det något som blockerar mig?

**Det är inte en statusrapport — det är synkronisering.**

---

## Product Backlog

En prioriterad lista med allt som ska byggas.

```
Product Backlog (prioriterat):
1. [Hög] Användare kan logga in           → 5 SP
2. [Hög] Visa lista med produkter         → 3 SP
3. [Medel] Filtrera produkter på kategori → 8 SP
4. [Låg] Exportera som PDF                → 13 SP
```

PO äger och prioriterar backloggen kontinuerligt.

---

## Sprint Review — demo

I slutet av sprinten demonstrerar teamet vad som byggts.

- Stakeholders ser faktisk, körbar kod
- Feedback samlas in direkt
- PO beslutar om acceptance

Resulterar ofta i ny inmatning till backloggen.

---

## Sprint Retrospective — förbättring

Teamets eget möte, efter Review.

Tre frågor:

1. Vad gick **bra**?
2. Vad gick **sämre**?
3. Vad ska vi **förbättra** nästa sprint?

**Utan retrospektiv lär sig teamet ingenting. Det är SCRUM:s viktigaste ceremoni.**

---

## SCRUM vs Kanban

| Aspekt | SCRUM | Kanban |
|--------|-------|--------|
| Sprintar | Ja — fasta tidsboxar | Nej — kontinuerligt flöde |
| Roller | PO, SM, Team | Inga förutbestämda |
| Estimat | Story Points | Vanligtvis inga |
| Förändringar | Mellan sprintar | När som helst |

---

## Agilt i praktiken — vanliga missförstånd

❌ "Agilt = inga regler"
→ Agilt kräver disciplin och tydliga processer

❌ "Daily standup = statusrapport"
→ Det är synkronisering, inte rapportering till chefen

❌ "Vi hoppar över retrospektivet"
→ Då förbättras aldrig teamet

❌ "Agilt = snabbare"
→ Agilt = bättre anpassning, inte nödvändigtvis snabbare

---

## Kanban-tavlan

Gör arbetet synligt:

```
| Backlog | To Do | In Progress | Review | Done |
|---------|-------|-------------|--------|------|
| Story A | Bug X | Feature Y   |        |  Z   |
| Story B |       |             |        |      |
```

WIP-gränser: begränsa antalet aktiva uppgifter för att undvika multitasking.

---

## Estimering med Story Points

Story Points mäter relativ komplexitet — inte tid.

```
1 SP  = Väldigt enkelt (ändra en text)
3 SP  = Enkelt (nytt formulär)
5 SP  = Medel (ny feature med API-anrop)
8 SP  = Komplext (ny modul med databas)
13 SP = Mycket komplext — dela upp!
```

**Velocity** = hur många SP ett team klarar per sprint i genomsnitt.

---

## Sammanfattning

- ✅ Agilt = anpassning genom korta iterationer
- ✅ Rollerna: Product Owner, Scrum Master, Development Team
- ✅ Sprinten: Planning → Work → Review → Retro
- ✅ Daily Standup: synkronisering, inte statusrapport
- ✅ Retrospektivet är teamets förbättringsmotor

**Nästa: User Stories och Gherkin**
