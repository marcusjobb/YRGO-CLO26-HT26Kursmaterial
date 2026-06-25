# Lektionsplan — Databashantering och Design
**Kalender:** v41–44 | **Datum:** 5 okt – 1 nov 2026

## Interaktiva resurser (används löpande)

- **SQLzoo** — sqlzoo.net — interaktiva SQL-övningar, körs direkt i webbläsaren
- **W3Schools SQL** — w3schools.com/sql — referens och tryout-miljö
- **Story-driven code** — Sherlock Holmes-mordet (SQL-versionen) finns i `_teacher/` som inspiration

## Läxa vecka 1 — SQL-mysterier (story-driven)

De studerande väljer minst ett av dessa och löser det till nästa lektion:

| Resurs | Beskrivning | Status |
|--------|-------------|--------|
| [SQL Murder Mystery](https://mystery.knightlab.com/) | Lös ett mord med SQL — välkänd och beprövad | ✅ Testad |
| [SQL Police Department](https://sqlpd.com/) | SQL-övningar med polistema | ✅ Testad |
| [SQL Noir](https://www.sqlnoir.com/) | Noir-detektiv med SQL — gratis, ingen inloggning krävs | ✅ Testad |

Knyt ihop läxan med dag 2 intro: *"Vad löste ni? Hur löste ni det? Vilka SQL-kommandon behövde ni?"*

> ⚠️ **Lärarnotering:** Kör ALDRIG `DELETE` eller `DROP` live på SQLzoo — det är en delad miljö.
> Marcus körde det en gång som demo för klassen och SQLzoo låg nere i några timmar.
> De studerande skrattade gott. Lär av historien — eller upprepa den avsiktligt som demo av
> varför databasrättigheter och behörigheter finns. 😇
>
> [Marcus, berätta den här historien i slides om SQL-säkerhet — det är en perfekt anekdot]

## Veckans ämnen

| Kursvecka | Kalendervecka | Tema | Inlämning |
|-----------|--------------|------|-----------|
| 1 | v41 | Introduktion databaser, terminologi, UML och databasdesign | Nej |
| 2 | v42 | SQL grunder — CRUD och relationsdatabaser | Ja (deadline sön 18 okt) |
| 3 | v43 | SQL avancerat, normalisering och säkerhet | Ja (deadline sön 25 okt) |
| 4 | v44 | MongoDB och dokumentdatabaser — tenta | Tenta |

---

## Vecka 1 (v41) — 5–10 okt 2026
*Tema: Introduktion databaser, terminologi, UML och databasdesign*
*Ingen inlämning*

### Dag 1 — måndag 5 okt

| Tid | Innehåll |
|-----|----------|
| 9:00–9:15 | Närvaro + veckans nördiska |
| 9:15–9:45 | Vad är en databas? Terminologi och koncept (slides) |
| 9:45–10:00 | Rast |
| 10:00–10:45 | Relationsmodellen och UML |
| 10:45–11:00 | Rast |
| 11:00–12:00 | Övning 1 — modellera en databas på papper |
| 12:00–13:00 | Lunch |
| 13:00–13:20 | Genomgång övning 1 |
| 13:20–13:45 | Skillnader: relations- vs dokumentdatabaser |
| 13:45–14:00 | Rast |
| 14:00–14:30 | Övning 2 |
| 14:30–14:45 | Q&A |

### Dag 2 — tisdag 6 okt

| Tid | Innehåll |
|-----|----------|
| 9:00–9:15 | Närvaro |
| 9:15–9:45 | Genomgång läxan |
| 9:45–10:00 | Rast |
| 10:00–10:45 | Installera och sätta upp MySQL/SQLite |
| 10:45–11:00 | Rast |
| 11:00–12:00 | Koda vilt |
| 12:00–13:00 | Lunch |
| 13:00–13:20 | Genomgång övningar |
| 13:20–13:45 | Sammanfattning |
| 13:45–14:00 | Rast |
| 14:00–14:30 | Q&A |
| 14:30–14:45 | Info om inlämning v2 |

### Dag 3 — onsdag 7 okt

| Tid | Innehåll |
|-----|----------|
| 9:00–11:00 | Online handledning — Discord/Meet |

---

## Vecka 2 (v42) — 12–17 okt 2026
*Tema: SQL grunder — CRUD och relationsdatabaser*
*Inlämning: deadline söndag 18 okt 23:59*

> Dagstruktur som dag 1/2 ovan — fyll i SQL-specifikt innehåll.

---

## Vecka 3 (v43) — 19–24 okt 2026
*Tema: SQL avancerat, normalisering och säkerhet*
*Inlämning: deadline söndag 25 okt 23:59*

> Dagstruktur som dag 1/2 ovan — fyll i avancerat SQL-innehåll.

---

## Vecka 4 (v44) — 26–31 okt 2026
*Tema: MongoDB och dokumentdatabaser — tenta*
*Ingen inlämning, tenta*

### Dag 1 — måndag 26 okt

| Tid | Innehåll |
|-----|----------|
| 9:00–10:45 | MongoDB intro och repetition |
| 11:00–12:00 | Fri kodning / koda ikapp |
| 13:00–15:00 | Handledning och Q&A |

### Dag 2 — tisdag 27 okt — TENTA

| Tid | Innehåll |
|-----|----------|
| 9:00–12:00 | Tenta |

### Dag 3 — onsdag 28 okt

| Tid | Innehåll |
|-----|----------|
| 9:00–11:00 | Online handledning — Discord/Meet |

---

## TODO — Story-driven SQL-mysterium

Metropolitan Club-logiken (14 ledtrådar, 4 misstänkta, löses med deduktion) ska
göras om till en **ny story med andra ledtrådar** för SQL-versionen.

Original (logikpussel utan kod): `kurs-01/.../exercises/logik_01_metropolitanclub.md`
SQL-versionen: ny story, samma logik, löses med INSERT/UPDATE/SELECT.

Inspiration: `Old_courses/2024/csharp/2_db/lectures/03_db_h2/sherlock_murder.md`
