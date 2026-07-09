# Kurs 3 — Fördjupad OOP i C# (CLO03H26)

**YH-poäng:** 25 | **Veckor:** 5 | **Veckor i schema:** v.46–50
**Examination:** Skriftligt prov (terminologi) + inlämningsuppgifter + projekt

## Kursens syfte

Bygger vidare på Kurs 1 (OOP-grunder) och Kurs 2 (SQL-kunskaper) för att nå
nästa nivå. Studerande lär sig koppla C#-applikationer mot databaser via EF Core,
introduceras till designmönster och refactoring, och möter agila arbetsmetoder
i praktiken. Kursen avslutas med Hellweek — ett intensivt grupprojekt.

EF Core introduceras HÄR eftersom studerande nu har stark förståelse för OOP
och SQL. De förstår vad EF abstraherar — de har gjort det för hand.

## Moduler och veckor

| Vecka | Dag | Moduler | Tema |
|-------|-----|---------|------|
| v.46 | Måndag | 01_databaskopplingar | ADO.NET, Entity Framework Code First, migrations |
| v.46 | Onsdag | 01_databaskopplingar | EF CRUD, relationer, navigation properties |
| v.47 | Måndag | 02_designmonster | Factory, Singleton — problem FÖRE lösning |
| v.47 | Onsdag | 02_designmonster | Observer, Repository |
| v.48 | Måndag | 03_refactoring | Clean Code-principer, SOLID intro, code smells |
| v.48 | Onsdag | 04_uml_och_planering + 05_agila_metoder | UML-diagram, Kanban, MoSCoW |
| v.49 | Måndag | Hellweek start | Grupprojekt: kravspec + design + sprint 1 |
| v.49 | Onsdag | Hellweek | Sprint 2 + kodgranskning |
| v.50 | Måndag | 07_blazor | Blazor WebAssembly intro — frontend för C#-utvecklare |
| v.50 | Onsdag | 06_projekt | Projekt-presentation + skriftlig examination |

> **Hellweek (v.49):** Intensiv gruppvecka. Studerande kravspecar, designar och
> bygger en EF-driven app från scratch på två dagar. Ingen facit — bara krav.
> Läraren är coach, inte hjälpare.

## Lärandemål

### Kunskap (Terminologi — tentamen)
- Redogöra för hur C# kopplas mot databas (ADO.NET vs EF Core)
- Redogöra för designmönstren Factory, Singleton, Observer och Repository
- Förklara vad refactoring är och när det behövs
- Redogöra för UML-diagram (klass-, sekvens-, tillståndsdiagram)
- Redogöra för Kanban och agilt arbetssätt
- Beskriva SOLID-principernas syfte

### Färdighet (Inlämningar)
- Skapa EF Core-projekt med Code First + migrations
- Implementera CRUD mot databas via EF
- **VG:** Designa relationer och navigation properties korrekt
- Implementera minst ett designmönster i ett projekt
- **VG:** Argumentera för designbeslutet och visa alternativ
- Refaktorera given kod med motivering

### Kompetens (Projekt)
- Planera och genomföra ett grupprojekt med EF Core och designmönster
- **VG:** Resonera kring designbeslut och alternativa lösningar
- Granska andras kod och ge konstruktiv feedback
- **VG:** Identifiera förbättringsmöjligheter med konkret motiv

## Inlämningar

| Inlämning | Innehåll | Täcker |
|-----------|----------|--------|
| Inlämning 1 | EF Core-app: Code First + CRUD + relationer | EF, databaskopplingar |
| Inlämning 2 | Designmönster-implementation med motivering | Factory, Observer eller Repository |
| Projekt (Hellweek) | Grupprojekt — EF-driven app med minst ett mönster | Allt + VG-resonemang |

## Återkommande inslag

- **Hellweek (v.49):** Märks i kursguiden som en av de viktigaste veckorna. Se `_teacher/KURSGUIDE.md`.
- **Kodgranskning:** Schemalagt v.49 onsdag — studerande granskar varandras kod mot Clean Code-checklista.
- **Design FÖRE kod:** Varje modul börjar med ett diagram-steg. Ingen kodar förrän UML är godkänt.

## OBS: Koppling till Kurs 2

EF Core förutsätter att studerande kan SQL. Den naturliga frågan "vad gör migrations?"
besvaras med "det som ni gjorde för hand i kurs-02 — nu automatiserat." Aktivera denna
koppling aktivt i föreläsningen v.46.

## Projekt-alternativ (06_projekt/)

Primärt: **Djursjukhus (Entity Framework)** — EF Code First, SQL Server/SQLite, CRUD
Alternativ: **Lagerhanteringssystem** — CSV-import + lagerlogik (om grupp vill göra något eget)
