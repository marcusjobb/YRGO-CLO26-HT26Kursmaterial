# Komplett inventering av /home/marcus/git/Old_courses/

Skapad för snabb referens när man letar efter material från tidigare år.

---

## 2022/ — Första året (C# och webbutveckling)

**Typ:** Kurs (CLO22 — gamla curriculumet)  
**Språk:** C#, HTML/CSS, Git  
**Totalt:** ~370 filer (72 .md, 180 .cs)  
**Status:** Startkurs för complete beginners

### Moduler

| Modul | Innehål | Filer | Fokus |
|-------|---------|-------|-------|
| **CSharp/** | Filhantering (SQL, CSV, JSON), classes, LINQ | 6 .md | Filhantering + LINQ |
| **Git/** | GitHub-intro, SSH-nycklar, arbetsflöde | 15 .md, 3 .cs | Git basics + GitHub |
| **Grund/** | Variabler, typer, outputs | 1 .md, 2 .cs | Allergi-enkel intro |
| **Kodkvalite/** | Clean Code, Refactoring, Testning, Code Review | 12 .md | Kodstandard & principper |
| **Kodexempel/** | Verkliga kodexempel från lektioner | 41 .cs | Live-code referens |
| **Linq/** | LINQ-expression och queries | 8 .cs | LINQ examples |
| **Metoder/** | Metoders struktur, parametrar, return | 1 .md, 4 .cs | Methods-basics |
| **Polymorfism/** | Polymorfism, interfaces, abstraktion | 1 .md, 4 .cs | OOP-polymorfism |
| **Programflöde/** | if/else, loops, switch | 3 .cs | Kontrollflöde |
| **SQL/** | Grundläggande SQL, databasuttryck | 9 .md | SQL intro |
| **TDD/** | Test-driven development | 10 .cs | TDD-exempel |
| **Typer/** | Datatyper, casting, conversions | 1 .md, 29 .cs | Många typexempel |
| **Tentarepetition/** | Examprov, gamla tentor | 17 .cs | Exam prep |
| **Repetition/** | Mixade övningar | 41 .cs | Repetition |

### Highlights

- **Kodkvalite/** — systematisk genomgång av Clean Code, Refactoring, Testning — bra referens för kodstandard
- **Kodexempel/** — konkret live-code från klassrum
- **TDD/** — tidigt introducerad test-driven development

### Bäst för kurs

Grundkurs (01 OOP Basics). Historia och inspiration.

---

## 2023/ — År två (Parallell C# och Java)

**Typ:** Två parallella kurser  
**Språk:** C# (CLO23) och Java (JIN23)  
**Totalt:** ~1900 filer (900 .md, 100+ .cs, 340+ .java)  
**Status:** Etablerad, mycket innehål

### 2023/csharp/ — 478 filer

**Undervål fokusområden:**

| Modul | Innehål | Filer |
|-------|---------|-------|
| **Assignments/** | Inlämningsuppgifter + lösningar | ~100 filer |
| **DesignPatterns/** | Factory, Singleton, Observer m.fl. | .md & .cs |
| **Tentarepetition/** | Gamla tentor, övningsprov | ~50 filer |
| **TDD/** | Test-driven exercises | ~30 .cs |
| **GitFlowDemo/** | Git workflow med branches | Demo |
| **MarcusTddLive/** | Live TDD-session från klassrum | Exempel |

### 2023/java/ — 1397 filer

**Mycket omfattande.** Två huvudlinjer:

| Modul | Innehål | Filer |
|-------|---------|-------|
| **OOP/** | Classes, Methods, Inheritance, Polymorphism | ~200+ .java |
| **DesignPatterns/** | 3 lectures + examples | Pattern-fokus |
| **SQL/** | JDBC, SQL queries, database connections | ~40 filer |
| **CleanCode/** | Refactoring, SOLID principles | Teori + övningar |
| **Cheatsheets/** | Snabbreferenser för syntax | .md |
| **Git/** | GitHub, Git workflow, branching | Guides |
| **IntelliJ/** | IDE setup, plugins, workflow | Installationsguider |
| **Livekod/** | Livekodsessions från klassrum | ~100 .java |
| **VariablerLive/** | Live variable examples | Demo |
| **AI/** | ChatGPT-integration, prompt engineering | Relaterat till AI |
| **ChatGPT/** | AI-assisted coding | Exempel |
| **Utmaning/** | Challengeuppgifter (högre svårighet) | Stretch tasks |
| **RandomSnippets/** | Ad-hoc kodsnippets | Diverse |

### Highlights

- **Java/DesignPatterns/** — välstrukturerad design-patterns-serie (3 lectures)
- **Java/CleanCode/** — grundlig SOLID + refactoring
- **Java/Livekod/** — många examples från faktiska klassrumslektioner
- **Assignments/** (båda språk) — kompletta inlämningsuppsatser med lösningar

### Bäst för kurs

OOP-introduktion (01). Massa referensmaterial. Java är mycket fullständigt.

---

## 2024/ — År tre (Strukturerad CLO25/JIN24)

**Typ:** Två fullständiga, strukturerade kurser  
**Språk:** C# (2024/csharp/) och Java (2024/java/)  
**Totalt:** ~2400 .md, 280 .cs, många .java  
**Status:** Mest professionell struktur hittills, vecka-för-vecka

### 2024/csharp/ — Modulär uppbyggnad

```
1_oop/                   (OOP-introduktion)
├── lectures/            Marp slides + notes
├── exercises/           Week exercises
├── assignments/         Graded work
└── solutions/           Answer keys

2_db/                    (Databases + SQL + EF)
├── lectures/
├── exercises/
└── assignments/

3_oop_adv/               (Advanced OOP)
├── Design Patterns
├── SOLID principles
└── Architecture

4_test/                  (Testing)
├── Unit Testing
├── Integration Testing
└── TDD cycle
```

**Övriga mappar:**
- **CSharpRepetition/** — Gamla exam + övning
- **DatabaseLibrary/** — Databasexempel
- **livecode/** — Klassrumslive-code
- **practice_exercises/** — Extra övningar
- **lectures/** — Samlade slides

### 2024/java/ — Omfattande kurs

```
1_oop_basics/           (Classes, Methods, OOP-grund)
├── lecture_2_basic_java
├── lecture_3_classes_and_objects
├── lecture_4_methods
├── lecture_5_datatypes
├── lecture_6_git_github_assignment_algorithms
├── lecture_7_inheritance
├── lecture_8_design_patterns
├── lecture_9_planning
└── lecture_10_ai_theme
    └── summaries/

2_databases/            (SQL + JDBC + Spring Data)
3_oop_advanced/         (Inheritance, Polymorphism)
4_test/                 (JUnit, Mockito, TDD)
5_api/                  (REST, HTTP, Endpoints)
6_ai/                   (AI-integration)
7_system_integration/   (Larger systems)
8_cloud_integration/    (Cloud deployment)
```

### Highlights

- **Vecka-för-vecka struktur** — mycket professionell layout
- **Lectures/ + exercises/ + assignments/** — konsekvent pedagogisk progression
- **1_oop_basics/** — 10 samordnade lectures från scratch
- **Design Patterns (både språk)** — väl dokumenterat
- **gemensamhets_projekt/** — Collaborative project examples
- **installation/** — Setup guides för både IDEs
- **planning/** — Course planning documents

### Bäst för kurs

**Nästan direkt kopia-färdig för CLO26.** 2024-strukturen är den mest moderna och väl organiserad.

---

## 2026/ — Framtidsmaterial (tomt)

**Typ:** Placeholder/struktur för nästa år  
**Innehål:** 
- `Kursplaner CLO25_Rev 2025-08-12.pdf` — Aktuella kurskrav
- `weeks.png` — Kursöversikt

**Status:** Tom, redo för fylla

---

## books/csharp/ — Jekyll-site (2023)

**Typ:** Webbsida / Jekyll-struktur  
**Språk:** C#  
**Format:** Markdown + Jekyll YAML  
**Innehål:** Redirect / Setup  
**Status:** Shallow content, antagligen bara boilerplate

**Innehål:**
- Mycket minimal — `Index.md` + Jekyll config
- Landing page för CLO22-kurs
- Inte uppdaterad efter 2023

**Bäst för:** Inte direkt användbar. Arkiv.

---

## books/csharp_bgu/ — BGU C# Course (offline bok)

**Typ:** Offline-bok (Jekyll-baserad)  
**Format:** Markdown + Jekyll  
**Målgrupp:** BGU-studenter (grundläggande)  
**Totalt:** ~100 .md filer  

### Struktur

```
book/
├── index.md                 Kursöversikt
├── visualstudio/
│   ├── index.md            IDE-intro
│   ├── installation/        Setup
│   └── addons/             Plugins
├── variabler/
│   ├── index.md
│   └── long.md             Long-datatype
└── metoder/
    └── index.md
```

**Innehål:**
- Introduktion till C#, Visual Studio, variabler, metoder
- Pedagogisk, step-by-step
- Fokus på beginners

**Bäst för:** Mycket grundläggande introduktion. Kan användas för repetition av terminologi.

---

## books/csharp_cmyh/ — Campus Mölndal C# (känt)

ÖVERSPRINGAD — redan känd från brieffing

---

## books/devbook/ — Developer Handbook (känt)

ÖVERSPRINGAD — redan känd

---

## books/JIN23/ — Java kursboken (känt)

ÖVERSPRINGAD — redan känd

---

## books/CLO22/ — C# kursboken (känt)

ÖVERSPRINGAD — redan känd

---

## books/learncoding/ — Intro-ressurs (känt)

ÖVERSPRINGAD — redan känd

---

## programmering/ — Tomt placeholder

**Typ:** Tom mapp  
**Innehål:** Bara README.md med "# programmering"  
**Status:** Oanvänd

---

## Material från Codic/ — Tredjeparts-resurser (känt)

ÖVERSPRINGAD — redan känd

---

## Coud_Development_CLO25/ — Cloud-kurs (känt)

ÖVERSPRINGAD — redan känd (vecka 1–10)

---

## javaoop/ — Jekyll-site (tomt)

**Typ:** Jekyll-struktur  
**Status:** Bara boilerplate, inget innehål  

---

## JinTDDProject/ — TDD-mallprojekt

**Typ:** Maven-projekt (Java)  
**Innehål:** TDD-template för Java  
**Status:** Grundstruktur för TDD övningar

---

---

## SAMMANFATTNING FÖR MARCUS

### Bästa referensmaterial för CLO26

1. **2024/csharp/** — direkt struktur-mall. Kopiera denna för ny CLO26.
2. **2024/java/** — samma för Java-delen.
3. **2023/java/DesignPatterns/** — väl dokumenterad patterns-serie.
4. **2022/Kodkvalite/** — Clean Code referens för alla år.

### Snabb lookup-tabell

| Ämne | Bästa källa | Path |
|------|-------------|------|
| OOP Basics | 2024/csharp/1_oop/ | `/home/marcus/git/Old_courses/2024/csharp/1_oop/` |
| Design Patterns | 2023/java/DesignPatterns/ | `/home/marcus/git/Old_courses/2023/java/DesignPatterns/` |
| SQL + Databaser | 2024/csharp/2_db/ eller 2024/java/2_databases/ | `/home/marcus/git/Old_courses/2024/` |
| Testing + TDD | 2024/csharp/4_test/ | `/home/marcus/git/Old_courses/2024/csharp/4_test/` |
| Clean Code | 2022/Kodkvalite/ | `/home/marcus/git/Old_courses/2022/Kodkvalite/` |
| Git/GitHub | 2022/Git/ eller 2023/java/Git/ | `/home/marcus/git/Old_courses/2022/Git/` |
| Livekod exempel | 2023/java/Livekod/ eller 2024/*/livecode/ | `/home/marcus/git/Old_courses/2023/java/Livekod/` |
| Gamla tentor | 2024/csharp/exam* eller 2023/csharp/Tentarepetition/ | `/home/marcus/git/Old_courses/2024/csharp/` |
| SOLID Principles | 2024/csharp/3_oop_adv/ eller 2023/java/CleanCode/ | `/home/marcus/git/Old_courses/2024/csharp/3_oop_adv/` |

---

**Skapad:** 2026-06-15  
**Katalogiserad av:** Claude Code  
**Notering:** 2024/ är mest professionell och rekommenderad som mall för nya kurser.
