# Kursguide — för lärare och efterträdare

## Pedagogisk tanke

Kursen är uppdelad i ämnesmoduler som publiceras en vecka i förväg.
Den studerande navigerar på ämnen (inte veckor) för bättre överblick.
Publicering styrs av veckoplanerna i `_teacher/week_NN.md`.

---

## Bakåtplanering — så här planeras en vecka

Planera alltid bakifrån. Börja med slutmålet och bygg bakåt:

1. **Inlämningen** — vad ska de kunna visa att de förstått?
2. **Övningarna** — vad tränar dem inför inlämningen?
3. **Lektionerna** — vad behöver de lära sig för att klara övningarna?
4. **Veckans nördiska** — vilka termer dyker upp i materialet?

Skapa alltid i denna ordning. Aldrig tvärtom.

---

## Lektionsschema

Lektioner är 45 minuter. Schemat är 9–12 och 13–15.

### Dag 1 — Intro och grundläggning

| Tid         | Innehåll                                       |
| ----------- | ---------------------------------------------- |
| 9:00–9:15   | Närvaro och intro                              |
| 9:15–9:45   | Veckans ämne — presentation och demo           |
| 9:45–10:00  | Rast                                           |
| 10:00–10:45 | Läsa lästexten / Q&A om demot                  |
| 10:45–11:00 | Rast                                           |
| 11:00–12:00 | Övning 1 — koda vilt                           |
| 12:00–13:00 | Lunch                                          |
| 13:00–13:20 | Genomgång av övning 1 (matkoma-anpassat tempo) |
| 13:20–13:45 | Djupare dykning i ämnet                        |
| 13:45–14:00 | Rast                                           |
| 14:00–14:30 | Övning 2 — koda vilt                           |
| 14:30–14:45 | Läxor, Q&A, inlämning presenteras (ej vecka 1) |

### Dag 2 — Fördjupning och repetition

| Tid         | Innehåll                                |
| ----------- | --------------------------------------- |
| 9:00–9:15   | Närvaro och intro                       |
| 9:15–9:45   | Genomgång av läxan                      |
| 9:45–10:00  | Rast                                    |
| 10:00–10:45 | Fördjupning i ämnet                     |
| 10:45–11:00 | Rast                                    |
| 11:00–12:00 | Koda vilt                               |
| 12:00–13:00 | Lunch                                   |
| 13:00–13:20 | Genomgång av övningarna (matkoma-tempo) |
| 13:20–13:45 | Sammanfattning av veckan                |
| 13:45–14:00 | Rast                                    |
| 14:00–14:30 | Inlämning Q&A                           |
| 14:30–14:45 | Fri kodning och övrig Q&A               |

### Dag 3 — Handledning

| Tid        | Innehåll                          |
| ---------- | --------------------------------- |
| 9:00–11:00 | Online handledning — Discord/Meet |

---

### Kanban-dag (ersätter dag 1 när Kanban introduceras)

Används från kurs 3 vecka 3 och framåt när grupper arbetar med Kanban.
Presentationer är inbyggda i dagen — grupper visar vad de gjort löpande.

| Tid         | Innehåll |
| ----------- | -------- |
| 9:00–9:15   | Närvaro + intro |
| 9:15–9:45   | Lektion 1 — Kanban teori och demo |
| 9:45–10:00  | Rast |
| 10:00–10:45 | Lektion 2 — Grupper planerar projekt på Kanban-board |
| 10:45–11:00 | Rast |
| 11:00–11:45 | Lektion 3 — Grupper presenterar sin planering för klassen |
| 11:45–12:00 | Sammanfattning och Q&A |
| 12:00–13:00 | Lunch |
| 13:00–13:45 | Fördjupning i Kanban / nytt moment |
| 13:45–14:00 | Rast |
| 14:00–14:30 | Ny gruppuppgift — planera och köra |
| 14:30–14:45 | Grupper presenterar kort + läxa + Q&A |

---

## Inlämningar

- **Vecka 1:** Ingen inlämning
- **Sista veckan:** Tenta + fri tid — minimalt med lektioner
- **Deadline:** Söndag 23:59
- **Rättning:** Måndag

### Sista veckan
Inga nya moment. De studerande behöver tid att:
- Koda ikapp om de ligger efter
- Plugga inför tentan
- Ställa frågor via handledning

Håll lektionerna korta och fokusera på repetition och Q&A.

### Inlämningarnas struktur

Varje inlämning består av två delar:

**1. Koduppgift** — samma typ som övningarna men med nytt tema
(katt-övning → hund-inlämning). Visar att de förstått konceptet, inte kopierat.

**2. Reflektion** med dessa frågor:
- Vad var svårt?
- Vad var lätt?
- Vad kan du använda detta till? (projekt, arbete, idéer)
- Vad har du lärt dig?
- Vilken hjälp fick du av AI?
  
Reflektionen tränar dem inför slutinlämningen som sammanfattar alla lärandemål.

### Betygsättning

- **G** kräver: alla inlämningar godkända + godkänd tenta (tentan är enbart G såvida inte de får koda live under tentan vilket de inte kommer att vilja :D)
- **VG** kräver: G-kraven uppfyllda + VG-prestationer i inlämningar
- Slutinlämningen täcker samtliga lärandemål från kursplanen
- Om en studerande failar på en del av inläming 1 (exempelvis arrays)  men klarar det på slutinlämningen då är läroämnet (arrays) godkänd
  
---

## Rättningsprocess

1. Hämta den studerandes fork — spara commit-hash + datum
2. Claude skriver feedbackutkast utifrån betygskriterierna i `kursplan.md`
3. Marcus granskar och justerar (claude får inte sätta betyg, det är lag på att utbildaren ska godkänna)
4. Feedback mailas till den studerande (sparas som draft i mailboxen så utbildaren måste godkänna och skicka)
5. Logga i `_teacher/grading/week_NN.md`

```markdown
# Rättning vecka 03 — YYYY-MM-DD

| Studerande    | Fork URL                     | Commit  | Betyg | Mailat |
| ------------- | ---------------------------- | ------- | ----- | ------ |
| Anna Svensson | github.com/annasv/uppgift_03 | a1b2c3d | G     | ✅     |
```

Commit-hash sparas alltid — den studerande kan inte pusha ändringar efter rättning.

---

## Mappstruktur

```
_teacher/
├── kursplan.md       Lärandemål och betygskriterier
├── KURSGUIDE.md      Den här filen
├── solutions/            Lösningar — aldrig i studerandemappen
├── grading/         Rättningslogg per vecka
└── week_NN.md       Veckoplan + publiceringsmanifest
```

## Scripts

Se `_scripts/README.md` för dokumentation av alla scripts.
