# CLO26 — Cloud-utvecklare, YRGO

Kursmaterial för utbildningen **Cloud-utvecklare (400 YH-poäng)** vid YRGO.
Omgång CLO26. Kurs 1 (grundläggande OOP) startar höst 2026, v.35 (24 augusti).

Materialet täcker kurserna 1–4 (grundläggande programmering till testutveckling).
Kurserna 5–12 (cloud, Kubernetes, examensarbete m.m.) hanteras av separat lärare.

---

## Struktur

```
CLO26/
├── kurs-01-grundlaggande-oop/
├── kurs-02-databaser/
├── kurs-03-fordjupad-oop/
└── kurs-04-test-och-kvalitet/
```

Varje kurs är identiskt uppbyggd:

```
kurs-XX-namn/
├── _teacher/               # Syns aldrig för studerande
│   ├── kursplan.md         # Lärandemål, betygskriterier
│   ├── KURSGUIDE.md        # Pedagogik och tips till efterträdare
│   ├── solutions/              # Lösningar till övningar
│   ├── grading/           # Rättningslogg per vecka
│   ├── course_plans/         # Fullständiga kursplaner för hela programmet
│   └── week_NN.md         # Veckoplan + publiceringsmanifest
├── _scripts/
│   ├── README.md           # Dokumentation för alla scripts
│   └── publish_week.sh     # Publicerar en veckas material
├── _templates/
│   ├── forelasning.md      # Marp-mall
│   ├── ovning.md           # Övningsmall
│   ├── uppgift.md          # Inlämningsmall
│   └── gitignore_template  # .gitignore-mall för startrepos
├── 01_modulnamn/
│   ├── README.md           # Vad den studerande ska kunna efter modulen
│   ├── lectures/             # Marp-presentationer (.md)
│   ├── exercises/           # Övningar med exempeloutput
│   └── assignment/            # Inlämningsuppgift (om det finns en)
└── README.md               # Den studerandes ingångspunkt
```

---

## Designprinciper

### Publicering i förväg
Material publiceras **en vecka i förväg** via `publish_week.sh`.
Den studerande ser aldrig hela kursen på en gång — det minskar överväldigande känslan
och håller fokus på rätt saker i rätt tid.

Varje veckoplan (`_teacher/week_NN.md`) är ett manifest:
```markdown
# Vecka 3
publish:
  - 02_oop/lectures/klasser.md
  - 02_oop/exercises/ovning_1.md
  - 02_oop/assignment/
```

`publish_week.sh 03` läser manifestet och pushar exakt dessa filer till studerande-repot.

### Ämnesbaserad navigation, veckobaserad publicering
Den studerande navigerar på ämnen (`02_oop/`, `03_databaser/`) — inte veckor.
Det ger bättre överblick och gör materialet återanvändbart mellan omgångar.
Läraren planerar i veckor via manifestfilerna.

### Separation: lärare vs studerande
- `_teacher/` committas aldrig till studerande-repot
- `_scripts/` är lärarverktyg, inte material för de studerande
- Facit ligger alltid i `_teacher/solutions/`, aldrig i de studerandes mappar

---

## Inlämningar

- **Max 1 inlämning per vecka**
- Den studerande forkar ett startrepo som läraren skapar på GitHub (under Nion-Education)
- Deadline angiven i uppgiften — commits efter deadline beaktas inte
- Missad deadline → förlängd till **påföljande fredag**
- **Rättning sker på fredagar**

### Rättningsprocess
1. Hämta den studerandes fork och notera commit-hash + datum
2. Claude skriver feedbackutkast baserat på betygskriterierna i `_teacher/kursplan.md`
3. Läraren granskar och justerar
4. Feedback mailas till den studerande
5. Hash, betyg och mailstatus loggas i `_teacher/grading/week_NN.md`

Commit-hash sparas för att förhindra dispyter om post-deadline-pushar.

### Betyg
- **G** — Godkänt: lärandemålen uppfyllda enligt kursplanen
- **VG** — Väl Godkänt: uppfyller VG-kriterierna i kursplanen

---

## Tentor

- Max 2 tentor per kurs
- Frågor i `XX_modul/exam/` — publiceras **inte** i förväg
- Tentaform framgår av respektive `_teacher/kursplan.md`

---

## Scripts

Se `_scripts/README.md` i respektive kurs för fullständig dokumentation.

### publish_week.sh
```bash
bash _scripts/publish_week.sh 03
```
Publicerar vecka 3:s material till studerande-repot.
Kräver att `studerande`-remote är uppsatt:
```bash
git remote add studerande git@github-jobb:Nion-Education/[kursnamn]-studerande.git
```

---

## Konventioner

| Vad | Format |
|-----|--------|
| Mappar | `snake_case` |
| Markdown-filer | `snake_case.md` |
| C#-filer | `PascalCase.cs` |
| Marp-slides | `NN_amne.md` (numrerade) |
| Språk | Svenska (tekniska termer på engelska) |
| Verktyg | Marp, Mermaid, .NET 10 / C# 14 |

---

## Verktyg som krävs

```bash
# Marp (presentationer)
npm install -g @marp-team/marp-cli

# .NET SDK
sudo apt install dotnet-sdk-10.0
```

---

## För den som tar över

1. Läs `_teacher/KURSGUIDE.md` i respektive kurs — där finns pedagogiken
2. Kursplanerna (lärandemål + betygskriterier) finns i `_teacher/kursplan.md`
3. Alla scripts är dokumenterade i `_scripts/README.md`
4. Veckoplanerna i `_teacher/week_NN.md` visar vad som publicerats och när
5. Rättningslogg finns i `_teacher/grading/`

Kurserna 5–12 i CLO26-programmet hanteras av separat lärare — kontakta NionIT.
