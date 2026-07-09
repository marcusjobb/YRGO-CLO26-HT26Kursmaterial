# Kurs 4 — Test och kvalitetssäkring (CLO11H26)

**YH-poäng:** 30 | **Veckor:** 6 | **Veckor i schema:** v.51–53 (HT26) + v.1–2 (VT27)
**Examination:** Skriftligt prov (terminologi) + inlämningsuppgift + grupprojekt

## Kursens syfte

Lär studerande att testa sin kod — och sin förståelse av den. Kursen täcker
Clean Code-principer, TDD (Red-Green-Refactor), xUnit-testning och agila
arbetsmetoder (SCRUM, User Stories, Gherkin). Avslutningen är ett grupprojekt
där studerande bygger TDD-driven kod från krav.

Kursen börjar HT26 och avslutas VT27. Julbrottet (v.52-53) är planerat —
labb 1 lämnas in i julen, labb 2 startar i januari.

## Moduler och veckor

| Vecka | Dag | Moduler | Tema |
|-------|-----|---------|------|
| v.51 | Måndag | 01_clean_code | Varför Clean Code? Namngivning, metodlängd, kommentarer |
| v.51 | Onsdag | 01_clean_code | SOLID intro — SRP i praktiken |
| v.52 | Måndag | 02_unit_testning | xUnit: Arrange, Act, Assert. Vad ska man egentligen testa? |
| v.52 | Onsdag | 02_unit_testning | Mockning, testdubbletter, isolering |
| v.53 | Måndag | 03_tdd | Red → Green → Refactor. TDD från scratch. |
| v.53 | Onsdag | 03_tdd | Kata: bygg en kalkylator TDD |
| — | — | *Jullov* | Labb 1 (individuell TDD-inlämning) deadline: söndag v.53 23:59 |
| v.1 | Måndag | 04_scrum_och_agilt | SCRUM i teorin — sprintar, ceremonies, artefakter |
| v.1 | Onsdag | 05_user_stories_gherkin | User Stories, Acceptance Criteria, Gherkin |
| v.2 | Måndag | 06_kodkvalitet | Code coverage, statisk analys, CI-kvalitetsgates |
| v.2 | Onsdag | 07_projekt | Projekt-presentation + skriftlig examination |

> **Labb 1 (individuell):** TDD-drivet klass-bibliotek med full testtäckning.
> **Labb 2 (grupp):** Hushållsekonomi-systemet — budgetkalkylator driven av TDD.
> Labb 2 startar v.1 och presenteras v.2 onsdag.

## Lärandemål

### Kunskap (Terminologi — tentamen)
- Redogöra för Clean Code-principer och varför de finns
- Redogöra för TDD:s tre faser (Red-Green-Refactor)
- Förklara vad ett enhetstest testar och inte testar
- Redogöra för SCRUM-rollerna och ceremonierna
- Beskriva Gherkin-syntaxen (Given-When-Then)
- Redogöra för vad testtäckning (coverage) mäter

### Färdighet (Inlämningar)
- Skriva enhetstester med xUnit (Arrange, Act, Assert)
- **VG:** Testa edge cases och felfall, inte bara happy path
- Implementera en klass TDD-drivet från en kravspec
- **VG:** Refaktorera med bibehållen grön testsvit
- Skriva Gherkin-scenarios för en given feature

### Kompetens (Projekt)
- Planera och genomföra ett TDD-drivet grupprojekt
- **VG:** Självständigt identifiera och dokumentera teststrategin
- Granska testkod och identifiera brister i coverage eller testdesign
- **VG:** Argumentera för testdesignbeslut mot alternativ

## Inlämningar

| Inlämning | Innehåll | Deadline |
|-----------|----------|----------|
| Labb 1 (individuell) | TDD-drivet klassbibliotek, alla publika metoder testade | v.53 söndag |
| Labb 2 (grupp) | Hushållsekonomisystemet — TDD + SCRUM + reflektion | v.2 söndag |
| Tentamen | Kursmål 1-6: Clean Code, TDD, enhetstester, SCRUM, User Stories, Gherkin | v.2 onsdag |

## Projekt (07_projekt/)

**Primärt: Hushållsekonomi (grupprojekt)**
Budgetkalkylator som TDD-testar alla beräkningar. Gruppen planerar med Kanban,
arbetar i sprintar och reflekterar över samarbetet PPU-stil.

**Utmanande extra: Sport League Manager API**
Lärarmaterial finns i `07_projekt/assignment/teacher_instructions.md`. Används om
en grupp vill höja ribban med ett REST API + tester.

**Julextra (valfri fördjupning):**
Jultomtens Kodäventyr (parts 2-4) är en valfri utmaning för de som vill
fortsätta koda under jullovet. Ingår inte i betygsättningen.

## Övergång HT26 → VT27

Inget krav på uppstart i januari — studerande har allt de behöver från v.53.
Labb 2-grupper bildas i slutet av v.53. Inchecknings-handledning v.53 fredag (distans).
