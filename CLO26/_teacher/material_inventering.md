# Materialinventering — CLO26

Alla källfiler från old_courses kartlagda till rätt kurs. Format spelar ingen roll just nu — vi omvandlar medan vi går igenom.

**Källor:**
- `old_courses/books/` ⭐ **Primärkällan** — Färdiga markdown-böcker med förklaringar + kodexempel
- `old_courses/2025/` — Marcus förra kursomgång (CLO25)
- `old_courses/Material från Codic/` — Marcus tid på Codic (övningar, slides, inlämningsuppgifter)
- `old_courses/Coud_Development_CLO25/` — Cloud Development-kursen (BCD/ACD)

---

## BOOKS — Primärkällan ⭐

`/home/marcus/git/Old_courses/books/` innehåller färdiga markdown-böcker. Dessa ska vara basen för kursanteckningar och läsmaterial — vi behöver inte skriva från noll.

### csharp_cmyh — Huvudboken (Campus Mölndal, uppdaterad 2025)

**Sökväg:** `books/csharp_cmyh/C-Sharp/`  
**Format:** markdown + kodexempel, Jekyll-site, senast uppdaterad 2025  
**Innehåll — komplett kapitelöversikt:**

| Kapitel | Innehåll | CLO26-kurs |
|---------|----------|------------|
| `begin_csharp/` | Hello World, if/loop-exempel, klasser-intro, hemliga meddelanden | Kurs 01 |
| `variables_and_types/` | Djupgående: int, double, string, bool, char, DateTime, GUID, enum, nullable, var, tuples, records | Kurs 01 |
| `control_structures/if/` | if, else if, else, ternary | Kurs 01 |
| `control_structures/loop/` | for, while, do-while, foreach | Kurs 01 |
| `control_structures/switch/` | switch, pattern matching | Kurs 01 |
| `datastructures/` | Arrays, List, Dictionary | Kurs 01 |
| `oop/` | Properties, access modifiers, delegates, events | Kurs 01/03 |
| `oop/polymorphism/abstractclasses/` | Abstract class, Shape → Circle/Rectangle | Kurs 03 |
| `oop/polymorphism/interfaces/` | Interface, plugins | Kurs 03 |
| `databases/sql/` | SQL i C#-kontext | Kurs 02 |
| `entityframework/` | EF Core, context | Kurs 03 |
| `file_handling/` | CSV, JSON, XML, Text, Binary | Kurs 03 bonus |
| `api/` | REST, ASP.NET API, endpoints, CORS, JSON, GraphQL, gRPC | Kurs 03/Cloud |
| `asp.net/` | MVC, aspnetcore, cookies, session | Kurs 03/Cloud |
| `asynkron/` | async/await | Kurs 03 bonus |
| `quality_control/tdd.md` | TDD-kapitel | Kurs 04 |
| `quality_control/code_reviews.md` | Code reviews | Kurs 03/04 |
| `qa/dotnet-core-interview-questions.md` | Intervjufrågor | Kurs 04 bonusmaterial |
| `common_algorithms/` | Algoritmer | Kurs 03 bonus |
| `git/` + `github/` | Git och GitHub | Kurs 01 |
| `windows_terminal/` | Terminal-guide | Kurs 01 |

### devbook — Parallell C# + Java (perfekt för Java→C#-konvertering)

**Sökväg:** `books/devbook/Docs/`  
**Innehåll:** Identiskt innehåll i `csharp/` och `java/` — samma kapitel, olika språk  
**Kapitel:** variabler, if, loopar, datastrukturer, oop, sql, filhantering, entityframework, asp.net, asynkron, api

**Användning:** Java-versionen → direkt referens när vi konverterar Java-kod till C# övningar. Samma förklaring, bara byt syntax.

### CLO22 — Äldre C#-bok (Campus Mölndal 2022)

**Sökväg:** `books/CLO22/Docs/`  
**Innehåll:** Liknande csharp_cmyh men äldre. Kan användas som komplement eller backup-källa.

### JIN23 — Java-boken (Campus Mölndal 2023)

**Sökväg:** `books/JIN23/`  
**Innehåll:** Java-version av hela kursboken: variables, if, loop, datastructures, oop (polymorfism, abstrakta klasser, interfaces), sql, filhantering, asynkron, api, quality assurance  
**Användning:** Java→C#-konvertering ämne för ämne. JIN23 är Marcus Java-kurs — devbook har parallellen.

### learncoding — Flerspråkig grundbok

**Sökväg:** `books/learncoding/docs/`  
**Innehåll:** Basics i C#, Java, Python, JavaScript, Bash, PowerShell — på svenska och engelska  
**Användning:** Referens, eventuellt för cloud-kursen (Bash/PowerShell)

---

---

## Kurs 01 — OOP i C# Grund

### Från Codic (C# + OOP 2021)

**Föreläsningar/slides (pptx/pdf):**
- `Codic/Objektorienterad programmering i C# 2021/Variabler/Variabler intro.pptx`
- `Codic/Objektorienterad programmering i C# 2021/Variabler/Lite information om typer.docx`
- `Codic/Objektorienterad programmering i C# 2021/Iterationer/Loopar.pptx`
- `Codic/Objektorienterad programmering i C# 2021/Pseudokod/Pseudokod.pptx`
- `Codic/Objektorienterad programmering i C# 2021/Pseudokod/Flödesschema.pptx`
- `Codic/Objektorienterad programmering i C# 2021/Switch/Switch.pptx`
- `Codic/Objektorienterad programmering i C# 2021/OOP/Klasser.pptx`
- `Codic/Objektorienterad programmering i C# 2021/OOP/Klasser (examples).pptx`
- `Codic/Objektorienterad programmering i C# 2021/OOP/Intro.pptx`

**Övningar (docx/pdf):**
- `Codic/Objektorienterad programmering i C# 2021/Variabler/Övningar variabler.docx`
- `Codic/Objektorienterad programmering i C# 2021/Iterationer/Övningar Iterationer.docx`
- `Codic/Objektorienterad programmering i C# 2021/Switch/Switch lätt övning.docx`
- `Codic/Objektorienterad programmering i C# 2021/Switch/Switch svår övning.docx`
- `Codic/Objektorienterad programmering i C# 2021/Try Catch/Övning try catch.docx`
- `Codic/Objektorienterad programmering i C# 2021/Repetition av första delen/Övning repetition.docx`
- `Codic/C#/Övningar/Arrays och listor/Övningar - Array.docx`
- `Codic/C#/Övningar/Arrays och listor/Övningar - Listor.docx`

**Inlämningar (kan bli mall/inspiration):**
- `Codic/C#/Inlämningar/Tärningsspel (OOP)/Inlämning 1 - tärningar.docx` — OOP-inlämning med klasser

### Från CLO25 (förra årets kurs)
- `2025/1_oop/` — story_driven_code, variables, if, loop, methods, arrays, datastructures, begin_csharp, problem_solving, markdown, git, classes
- `2025/1_oop/exam/` — variables_and_types_training.md, if_statements_training.md, loops_training.md, naming_conventions_training.md, git_basics_training.md

---

## Kurs 02 — Databaser

### Från Codic (SQL)

**Övningar — nybörjare (kan bli direkta övningar):**
- `Codic/SQL/Övningar/Nybörjare/Mordet på Metropolitan Club.pdf` — klassisk pusselövning (redan refererad i kurs-01 kursplan!)
- `Codic/SQL/Övningar/Nybörjare/SQL CRUD för nybörjare.docx`
- `Codic/SQL/Övningar/Nybörjare/SQL filmdatabatas.docx`
- `Codic/SQL/Övningar/Nybörjare/Tankeövningar om databaser.docx`
- `Codic/SQL/Övningar/Nybörjare/Specialpizza (SQL övning).pdf`
- `Codic/SQL/Övningar/Nybörjare/Sammanfattning av SQL.docx`

**Övningar — avancerat:**
- `Codic/SQL/Övningar/Övningar - Lektion 2.docx` till `Lektion 10.docx` (9 lektioner)
- `Codic/SQL/Övningar/Show me the money.docx` — SQL-funktioner
- `Codic/SQL/Övningar/Mer sökningar i SQL.pdf`
- `Codic/SQL/Övningar/10-Practice-SQL-Final-Query-Questions.pdf`
- `Codic/SQL/Övningar/SQL övning Arash/SQL Övningsuppgifter 1.pdf` + svar

**Inlämningar (mall/inspiration):**
- `Codic/SQL/Inlämningar/Inlämning 1 SQL och databaser.pdf`
- `Codic/C#/Inlämningar/Dörrlogg (SQL)/Inlämningsuppgift1.docx` — C# + SQL-integration
- `Codic/SQL/Inlämningar/FamilyTree.pdf`

**Lösningsförslag:**
- `Codic/SQL/Övningar/Lösningsförslag - Lektion 2.docx` till `Lektion 9`

### Från CLO25
- `2025/2_databases/kursplan.md` — förra kursplanen
- `2025/2_databases/EF_WEEK3_SUMMARY.md` — EF Core hörde till databaskursen förra året (flytta till kurs-03 nu)

---

## Kurs 03 — Fördjupad OOP

### Från Codic (C# avancerat + Clean Code)

**Arv och polymorfism:**
- `Codic/Objektorienterad programmering i C# 2021/Polymorfism/Övning Polymorfism (medelsvår).docx`
- `Codic/C#/Övningar/` (C#-mappen har även: API, Algoritmer, Dictionary, Enum, BDD-övningar)

**Clean Code:**
- `Codic/Clean Code/Clean code.pptx` — Marcus egna slides
- `Codic/Clean Code/Tentamen/` — tentamaterial
- `Codic/Clean Code/Inlämningar/`

**EF Core-inlämning:**
- `Codic/C#/Inlämningar/Dörrlogg 2 (EF)/Inlämningsuppgift2.docx` — C# + EF Core-inlämning
- `Codic/C#/Inlämningar/Webbutik (Databas & ASP.net)/Inlämning 2 - Webbutik.docx`

**DevOps/Mjukvaruanalys:**
- `Codic/DevOps/Inlämningsuppgift Mjukvaruanalys.docx`

### Från CLO25
- `2025/3_advanced_oop/` — innehåller: arv, abstraktion, polymorfism, design_patterns_overview, SOLID, refactoring, entity_framework, MVC, behavioral/creational/structural patterns, git_flow, caching, uml, api_clients
- `2025/3_advanced_oop/exam/` — inheritance_training.md, polymorphism_training.md, abstraction_training.md, strategy_pattern_training.md, refactoring_training.md, fluent_builder_pattern_training.md, api_client_csharp_training.md, caching_database_training.md

---

## Kurs 04 — Test och kvalitetssäkring

### Från Codic (C# + TDD)

**TDD-inlämningar (kan bli inspirationskälla för uppgifter):**
- `Codic/C#/Inlämningar/Blackjack (TDD)/Casino PlayNPay.docx` — Blackjack med TDD
- `Codic/C#/Inlämningar/Game of life (TDD)/Conways Game of Life.docx` — Game of Life med TDD
- `Codic/C#/Inlämningar/Geometri (TDD)/TDD Inläming 1 - Geometri.docx`
- `Codic/C#/Inlämningar/Hushållsekonomi (TDD)/Hushållsekonomi.docx`

**BDD-övningar:**
- `Codic/C#/Övningar/BDD/Bankkonton och TDD.docx`
- `Codic/C#/Övningar/BDD/BDD Övningar.docx`
- `Codic/C#/Övningar/BDD/Johan och pizzor.docx`

**CI/CD:**
- `Codic/CICD/Övningar/Testning på Github.pdf`
- `Codic/CICD/Innehåll/` — Python PyTest-videor (mkv) — troligen Python-specifika, men PyTest-koncepten gäller

### Från CLO25
- `2025/4_test_and_quality_assurance/kursplanering.md` — fullständig kursplan med vecka-för-vecka (V49-V4), dopamin-driven metodik
- `2025/4_test_and_quality_assurance/resurser_per_dag.md` — alla externa länkresurser per dag
- `2025/4_test_and_quality_assurance/lectures/` — föreläsningsfiler
- `2025/4_test_and_quality_assurance/exercises/`
- `2025/4_test_and_quality_assurance/quiz/`

---

## Cloud-kursen (ännu ej planerad för CLO26)

**Källa:** `old_courses/Coud_Development_CLO25/` — BCD Basic Cloud Development, 10 veckor

### Vecka-för-vecka (BCD)

| Vecka | Tema | Vad som finns |
|-------|------|---------------|
| V1 | Virtual Servers | Azure VM, SSH, Linux, Compute-teori |
| V2 | Infrastructure as Code | ARM templates, Bicep, AZ CLI, bash-scripts |
| V3 | Web Development | ASP.NET MVC, Hello World, forms, validation, Bootstrap |
| V4 | Virtual Networks | IP/CIDR, firewalls, VNet, network architecture |
| V5 | Web Development Advanced | Service layer, unit tests, Repository pattern, MongoDB, CosmosDB |
| V6 | Storage | CosmosDB, Azure Blob Storage, persistent storage |
| V7 | CI/CD | (innehåll saknas i markdown — behöver fyllas) |
| V8 | Secret Management | (innehåll saknas — behöver fyllas) |
| V9 | Monitoring | (innehåll saknas — behöver fyllas) |
| V10 | Wrap-up | Projektavslut |

**Dessutom finns:**
- `exercises/10-webapp-development/` — 5 spår: presentation, service, data layer, auth/authorization, ASP.NET Identity
- `exercises/15-code-collaboration/` — Git-exercises
- `infrastructure-fundamentals/` — compute, network, storage, IaC-teori
- `intro-cloud-development/` — cloud concepts, service models

**ACD (Advanced Cloud Development):**
- `week-by-week/acd/week-1.md` till `week-3.md` — 3 veckor finns, ej komplett

### Notering om cloud

Marcus har inte undervisat cloud-kursen än. BCD-materialet (10 veckor) är engelska markdown-filer med fullständiga övningar — bra bas. Rekommenderat:
1. Läs igenom BCD-veckor 1-10 för att förstå bredden
2. Hitta en cloud-kollega (Zane?) att luta sig mot för V7–V9 (CI/CD, secrets, monitoring) som saknar innehåll

---

## Java-material → konvertera till C#

**Princip:** All Java vi hittar skrivs om till C# vid omvandlingen. Koncepten är identiska — syntax byter språk. Bra för övningsvarianter: "här är samma problem, nu i annan form."

### javaoop (Campus Mölndal — GitHub Pages)

Repon är en Jekyll-site. Innehållet finns inte lokalt — bara navigation.yml och config. Ämnen enligt navigationen:

| Ämne i Java | Destination i C# | Kurs |
|-------------|-----------------|------|
| Advanced OOP Concepts | Arv, abstract, interface | Kurs 03 |
| Builder Pattern (med/utan Lombok) | Builder Pattern i C# | Kurs 03 |
| Factory Pattern (med/utan Lombok) | Factory Pattern i C# | Kurs 03 |
| OOP vs Functional Programming | Jämförelse / diskussion | Kurs 03 |
| Zip Handling (load/save files) | File I/O i C# | Kurs 03 bonus |

**Åtgärd:** Hämta innehållet från GitHub om vi behöver det, eller bygg nya C#-versioner av ämnena.

### JinTDDProject (Java + JUnit 5)

Studentprojekt-mall i Java med JUnit 5 (tom test-klass). Konverteras till:
- C# xUnit-projektmall med samma README-struktur
- Bra som startprojekt för kurs-04-inlämning

**Åtgärd:** Skapa `kurs-04-test-och-kvalitet/assignment/tdd-projekt-mall/` med C# xUnit-scaffold + README-mall.

---

## Övrigt material (ej kartlagt till kurs än)

| Källa | Material | Trolig destination |
|-------|----------|--------------------|
| `Codic/React/` | Inlämningar, övningar, tentamen | Ej aktuellt för CLO26 |
| `Codic/HTML_CSS/` | (ej öppnat) | Möjlig webb-kurs |
| `Codic/Objektorienterad programmering i C# 2021/Ninja Skillz/` | Extensions.pdf, VS-genvägar | kurs-01 bonusmaterial |
| `Codic/Objektorienterad programmering i C# 2021/WindowsForms/` | Mini Windows forms guide | kurs-01/03 optional |
| `Codic/C#/Övningar/API/Filmsökning via OMDB API.docx` | API-anrop från C# | kurs-03 |
| `Codic/C#/Övningar/Algoritmer/` | Binär sökning, rekursion, swap | kurs-03 extra |
| `old_courses/2026/` | (ej öppnat) | Kolla om det finns nytt material |
| `old_courses/javaoop/` | Java OOP | Ej aktuellt |

---

## Nästa steg

- [ ] Börja med kurs 01: omvandla Codic-material ämne för ämne
- [ ] Kurs 02: Metropolitan Club-övningen → markdown. SQL lektion 2-10 → välj ut de bästa
- [ ] Kurs 03: Clean Code-slides → Marp. Dörrlogg-inlämningarna → mall
- [ ] Kurs 04: Blackjack/Game of Life/Geometri-inlämningarna → uppdaterade versioner
- [ ] Cloud: Läs BCD V1-10, kartlägg vad som saknas, prata med Zane
