# Inventering: Gammalt kursmaterial för kurs-02 Databaser

**Datum:** 2026-06-15
**Syfte:** Kartlägg återanvändbart material inför CLO26 kurs-02 (Databaser, 4 veckor)

**Genomsökta sökvägar:**
- `/home/marcus/git/Old_courses/2025/2_databases/` — ✅ Finns, rikt material
- `/home/marcus/git/Old_courses/2024/csharp/2_db/` — ✅ Finns
- `/home/marcus/git/Old_courses/2024/java/2_databases/` — ✅ Finns
- `/home/marcus/git/Old_courses/2023/java/SQL/` — ✅ Finns (en fil)
- `/home/marcus/git/Old_courses/2022/SQL/` — ✅ Finns
- `/home/marcus/git/Old_courses/Material från Codic/` — ✅ Finns (binärer, docx — ej textbaserat)
- `/home/marcus/git/Old_courses/books/csharp_cmyh/C-Sharp/databases/` — ✅ Finns

---

## Kvalitetsdefinitioner

| Etikett | Betydelse |
|---------|-----------|
| **Modern** | Marp-format, Mermaid-diagram, svensk text, CLO25-stil med dark theme |
| **Gammal/oklar** | Plain markdown utan Marp, äldre dokumentformat eller Java-specifik syntax |
| **Campus Mölndal** | Tydlig Campus Mölndal/CampusYH-header — behöver avidentifieras och anpassas |
| **Kodic** | docx/pdf-format, inte konverterbart utan att skriva om |

---

## 1. SQL-grunder: SELECT, WHERE, INSERT, UPDATE, DELETE (Vecka 1)

### Källa: 2025/2_databases

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `lectures/crud/databaser_crud_marp.md` | Marp-föreläsning | Modern — dark theme, CLO25-stil | Kan användas direkt, byt CLO25→CLO26 |
| `lectures/crud/livedemo.sql` | Livekod SQL | Modern | Kan användas direkt |
| `lectures/aggregation/databaser_aggregering_marp.md` | Marp-föreläsning | Modern | Kan användas direkt |
| `lectures/ovningar_setup.sql` | SQL-seeddatabas | Modern | Kan användas direkt |
| `lectures/ovningar_losningar.sql` | Lösningsfil SQL | Modern | Kan användas direkt (lösningsfil → `_teacher/solutions/`) |
| `exercises/repetition/1_repeat_sql/` | Repetitionsövning (create, seed, exercises.sql) | Modern | Kan användas direkt |
| `exam/mysql_basics_training.md` | Träningsfrågor tenta | Modern | Kan användas direkt |
| `exam/sql_crud_training.md` | Träningsfrågor CRUD | Modern | Kan användas direkt |
| `exam/aggregering_training.md` | Träningsfrågor aggregering | Modern | Kan användas direkt |

### Källa: 2024/csharp/2_db

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `lectures/01_introduction/4_sql_crud.md` | Föreläsningstext m. Mermaid | Gammal stil — plain markdown, men solid innehåll | Moderniseras till Marp |
| `lectures/01_introduction/5_pk_fk.md` | Föreläsningstext PK/FK | Gammal stil | Moderniseras |
| `lectures/04_db_mysql/sql_3.sql` – `sql_5.sql` | Livekod SQL-filer | Gammal stil men återanvändbar SQL | Kan plocka SQL-kod direkt |

### Källa: 2022/SQL

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `CRUD-Create.md` | Kortfattad SQL-referens | Gammal stil — minimalistisk | Behöver skrivas om |
| `CRUD-Read.md` | Kortfattad SQL-referens | Gammal stil | Behöver skrivas om |
| `CRUD-Update.md` | Kortfattad SQL-referens | Gammal stil | Behöver skrivas om |
| `CRUD-Delete.md` | Kortfattad SQL-referens | Gammal stil | Behöver skrivas om |

### Källa: books/csharp_cmyh

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `sql/database.md` | Fördjupad lästext SQL | Campus Mölndal-header | Moderniseras — ta bort header, anpassa till Marcus-röst |
| `crud_operations.md` | CRUD-referenstext | Campus Mölndal | Moderniseras |
| `sql/sql_categories.md` | Kategorisering DDL/DML/DCL | Campus Mölndal | Bra referens, moderniseras |
| `sql_datatyper.md` | SQL-datatyper | Campus Mölndal | Behöver anpassas |

---

## 2. Normalisering, relationer, ER-diagram (Vecka 1–2)

### Källa: 2025/2_databases

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `lectures/01_databas_kaos_analys.md` | Marp-föreläsning — normalisering via kaos-analys | Modern | Kan användas direkt |
| `lectures/02_denormalisering_for_ai.md` | Marp-föreläsning — denormalisering/AI | Modern | Kan användas direkt |
| `exercises/01_radda_kaos_databasen.md` | Workshop-övning normalisering (WildShop AB, 4 timmar) | Modern — hög kvalitet | Kan användas direkt |
| `exercises/normalizing/01_studenter_och_kurser.md` | Normaliseringsövning 1NF→3NF | Modern, men använder emoji-ikoner | Kan användas direkt |
| `exercises/normalizing/02_bibliotek_och_låntagare.md` | Normaliseringsövning | Modern | Kan användas direkt |
| `exercises/normalizing/03_restaurang_beställningar.md` | Normaliseringsövning | Modern | Kan användas direkt |
| `exercises/normalizing/04_skolschema.md` – `07_sjukhus_system.md` | Ytterligare normaliseringsövningar (4 st) | Modern | Kan användas direkt, välj 2–3 |
| `exercises/chaos_whole_shebang.md` | Masterövning kaos-databas | Modern | Kan användas direkt |
| `exercises/kaos_database.sql` + `normalized_database.sql` | SQL-filer för kaos-workshop | Modern | Kan användas direkt |
| `exercises/chaos_sample.db` | SQLite-fil för övning | Modern | Kan användas direkt |
| `exam/sql_normalization_training.md` | Träningsfrågor normalisering | Modern | Kan användas direkt |

### Källa: 2024/csharp/2_db

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `lectures/01_introduction/2_relations_er_diagram.md` | Föreläsningstext ER-diagram | Gammal stil | Moderniseras till Marp |
| `lectures/02_db_sqlite/4_normalisering.md` | Föreläsningstext normalisering | Gammal stil | Moderniseras |

### Källa: books/csharp_cmyh

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `normalisering.md` | Djupgående lästext normalisering (1NF–5NF) | Campus Mölndal-header, annars Modern | Avidentifiera header, använd som läsmaterial |
| `relational_databases.md` | Teori relationsdatabaser | Campus Mölndal | Moderniseras |
| `uml_database_design.md` | UML vid databasdesign | Campus Mölndal | Bra underlag, moderniseras |
| `database_planning.md` + `databasplanering_example.md` | Databasplanering med exempel | Campus Mölndal | Moderniseras |

---

## 3. Joins — INNER, LEFT, RIGHT (Vecka 2)

### Källa: 2025/2_databases

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `lectures/joins/databaser_joins_marp.md` | Marp-föreläsning joins | Modern | Kan användas direkt |
| `exam/joins_training.md` | Träningsfrågor INNER/LEFT JOIN | Modern — frågor med `<details>`-svar | Kan användas direkt |

### Källa: 2024/csharp/2_db

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `lectures/02_db_sqlite/3_joins.md` | Föreläsningstext joins | Gammal stil | Moderniseras |
| `lectures/04_db_mysql/4_mysql_joins.md` | MySQL-specifik joins-text | Gammal stil | SQL återanvänds, omsätts till Marp |
| `lectures/05_db_mysql_advanced/5_advanced_joins.md` | Avancerade joins | Gammal stil | Avancerat material — välj ut delar |

### Källa: books/csharp_cmyh

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `sql/joins.md` | Lästext joins (INNER/LEFT/RIGHT) | Campus Mölndal-header, annars Modern | Avidentifiera, kan användas som läsmaterial |
| `sql/aggregation.md` + `sql/aggregering.md` | Aggregeringsteori | Campus Mölndal | Moderniseras |

---

## 4. Databasdesign och säkerhet (Vecka 3)

### Källa: 2025/2_databases

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `lectures/ef_core/part2_mysql_docker/lecture/02_security_backup_marp.md` | Marp-föreläsning säkerhet + backup | Modern | Kan användas direkt |
| `lectures/ef_core/part2_mysql_docker/exercises/exercise_3_bobby_tables.md` | SQL Injection-övning | Modern | Kan användas direkt |
| `lectures/ef_core/part2_mysql_docker/exercises/exercise_4_readonly_user.md` | Behörighetssäkerhet (readonly user) | Modern | Kan användas direkt |
| `lectures/ef_core/part2_mysql_docker/exercises/exercise_5_backup_restore.md` | Backup/restore-övning | Modern | Kan användas direkt |
| `exercises/ado/SQL_injection_1_Bobby_tables.md` | Bobby Tables-övning (C#) | Modern — hög pedagogisk kvalitet | Kan användas direkt |
| `exercises/ado/SQL_injection_2_admin_hack.md` | SQL Injection del 2 | Modern | Kan användas direkt |
| `exam/sql_injection_training.md` | Träningsfrågor SQL Injection | Modern | Kan användas direkt |
| `exam/mysql_permissions_training.md` | Träningsfrågor behörigheter | Modern | Kan användas direkt |
| `exam/mysql_backup_restore_training.md` | Träningsfrågor backup | Modern | Kan användas direkt |

### Källa: books/csharp_cmyh

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `sql_security.md` | Teori SQL-säkerhet | Campus Mölndal | Moderniseras — bra teoriunderlag |
| `sql/constraints.md` | SQL constraints (NOT NULL, UNIQUE etc.) | Campus Mölndal | Moderniseras |

---

## 5. ADO.NET (koppling mellan C# och databas) (Vecka 3)

> Obs: CLO26 kurs-02 har "Entity Framework intro" i vecka 4. ADO.NET verkar ingå i föregående CLO25. Kontrollera kursplanen för CLO26 — om ADO.NET ska läras ut ingår dessa. Om inte, är de ändå bakgrundsförståelse för EF-avsnittet.

### Källa: 2025/2_databases

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `lectures/ado/databaser_adonet_marp.md` | Marp-föreläsning ADO.NET | Modern | Kan användas direkt |
| `lectures/ado/demo/CustomerRegistry/` | Livekod ADO.NET (Facade-mönster, SQLite) | Modern — Clean Code, C# 14-stil | Kan användas direkt |
| `lectures/ado/demo/ADOTwoTables/` | Livekod ADO.NET med två tabeller | Modern | Kan användas direkt |
| `lectures/ado/demo/TeacherScript.md` | Lärarmanus för ADO.NET-demo | Modern | Kan användas direkt |
| `lectures/ado/live/ADOKundregister_live/` | Livekod (Facades, Interface, Seeder) | Modern — god arkitektur | Kan användas direkt |
| `exercises/ado/1_sqlite_setup.md` | Övning 1 SQLite-setup | Modern | Kan användas direkt |
| `exercises/ado/2_crud_operations.md` | Övning 2 CRUD i C# | Modern | Kan användas direkt |
| `exercises/ado/3_batch_operations.md` | Övning 3 batch | Modern | Kan användas direkt |
| `exercises/ado/4_error_handling.md` | Övning 4 felhantering | Modern | Kan användas direkt |
| `exercises/ado/5_diary_app_clean_code.md` | Övning 5 Clean Code-dagbok | Modern | Kan användas direkt |
| `exercises/repetition/2_repeat_cs_sql/` | Repetitionsövning C#+SQL (EcommerceApp) | Modern | Kan användas direkt |
| `exam/adonet_basics_training.md` | Träningsfrågor ADO.NET | Modern | Kan användas direkt |
| `exam/adonet_training.md` | Träningsfrågor ADO.NET fördjupning | Modern | Kan användas direkt |

### Källa: books/csharp_cmyh

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `ado_net_sqlite.md` | Teori ADO.NET + SQLite | Campus Mölndal | Moderniseras |
| `database_planning_csharp_sql.md` | Databasplanering med C#+SQL | Campus Mölndal | Bra referens, moderniseras |

---

## 6. MongoDB / NoSQL-intro (Vecka 3–4)

### Källa: 2025/2_databases

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `lectures/MongoDB/document_databases_marp.md` | Marp-föreläsning dokumentdatabaser | Modern | Kan användas direkt |
| `lectures/MongoDB/nosql_overview_marp.md` | Marp NoSQL-översikt | Modern | Kan användas direkt |
| `lectures/MongoDB/mongodb_marp.md` | Marp MongoDB-introduktion | Modern | Kan användas direkt |
| `lectures/MongoDB/mongodb_crud_marp.md` | Marp MongoDB CRUD | Modern | Kan användas direkt |
| `lectures/MongoDB/sql-vs-nosql.md` | Jämförelsedokument SQL vs NoSQL | Modern | Kan användas direkt |
| `lectures/MongoDB/mongodb-atlas-compass-setup.md` | Installationsguide Atlas+Compass | Modern | Kan användas direkt |
| `lectures/MongoDB/livecode_1_compass_basics.md` | Livekod Compass-grunder | Modern | Kan användas direkt |
| `lectures/MongoDB/livecode_2_csharp_mongo.md` | Livekod C# + MongoDB | Modern | Kan användas direkt |
| `lectures/MongoDB/nosql_lecture_code.md` | Kodexempel NoSQL | Modern | Kan användas direkt |
| `exercises/mongodb/mongodb_exercise_1.md` | Övning 1 MongoDB CRUD (Star Wars) | Modern — kreativt scenario | Kan användas direkt |
| `exercises/mongodb/mongodb_exercise_2.md` | Övning 2 MongoDB fördjupning | Modern | Kan användas direkt |
| `exercises/mongodb/mongodb_facade_exercise.md` | Övning MongoDB Facade-mönster (C#) | Modern | Kan användas direkt |
| `exercises/document_database/document_database_exercise_1.md` | Dokumentdatabas-övning 1 | Modern | Kan användas direkt |
| `exercises/document_database/document_database_exercise_2.md` | Dokumentdatabas-övning 2 | Modern | Kan användas direkt |
| `exercises/repetition/4_repeat_mongodb_compass/` | Repetitionsövning MongoDB Compass | Modern | Kan användas direkt |
| `exercises/repetition/5_repeat_mongodb_cs/` | Repetitionsövning C#+MongoDB (ContactLogApp) | Modern | Kan användas direkt |
| `exam/entity_framework_training.md` | Träningsfrågor EF (kan brytas ut för NoSQL) | Modern | Granska — innehåller blandad EF-info |

### Källa: 2024/csharp/2_db

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `lectures/06_db_nosql_mongodb/` | Föreläsningstext MongoDB (5 delar + quizzar) | Gammal stil — plain markdown | Moderniseras |
| `Assignments/Assignment_mongodb/requirements.md` | Uppgiftsbeskrivning MongoDB | Gammal stil | Behöver skrivas om |

### Källa: books/csharp_cmyh

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `brief/05_mongodb.md` | Kortreflektion MongoDB | Campus Mölndal | Ej prioriterat |
| `sql-vs-nosql.md` | Jämförelsetext SQL vs NoSQL | Campus Mölndal | Komplement till 2025-versionen |

---

## 7. Entity Framework Core — intro (Vecka 4)

### Källa: 2025/2_databases

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `lectures/ef/03_entity_framework_magin_marp.md` | Marp-föreläsning EF Core (huvud-föreläsning) | Modern | Kan användas direkt |
| `lectures/ef_core/part2_mysql_docker/lecture/01_mysql_docker_marp.md` | Marp MySQL + Docker | Modern | Kan användas direkt (om Docker ingår) |
| `lectures/ado/demo/EF_STUDENT_GUIDE.md` | Studentguide EF Core | Modern | Kan användas direkt |
| `exercises/ef/03_00_hello_ef.md` | Övning 1 Hello EF Core (SQLite, Code First) | Modern — tydlig steg-för-steg | Kan användas direkt |
| `exercises/ef/03_00_simple_crud.md` | Övning enkel CRUD med EF | Modern | Kan användas direkt |
| `exercises/ef/03_00_simple_relations.md` | Övning relationer med EF | Modern | Kan användas direkt |
| `exercises/ef/03_01_pre_flight_checklist.md` | Pre-flight checklista EF Core | Modern | Kan användas direkt |
| `exercises/ef/03_02_code_first_workshop.md` | Workshop Code First (LINQ, Include, migrationer) | Modern — hög pedagogisk kvalitet | Kan användas direkt |
| `exercises/ef/03_03_mysql_takeoff.md` – `03_12_moviemix.md` | EF-övningar (10 st, ökande svårighet) | Modern | Välj 3–5 efter kursplan |
| `exercises/repetition/3_repeat_cs_ef/` | Repetitionsövning EF (SupportTicketApp) | Modern | Kan användas direkt |
| `EF_WEEK3_SUMMARY.md` | Sammanfattning EF-veckan | Modern | Bra studieguide |
| `exam/entity_framework_training.md` | Träningsfrågor EF Core | Modern | Kan användas direkt |
| `database_planning_csharp_ef.md` (books) | Databasplanering C#+EF | Campus Mölndal | Moderniseras |

---

## 8. Inlämningsuppgifter (Assignments)

### Källa: 2025/2_databases

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `assignment/part_1/monster_tracker.md` | Inlämningsuppgift del 1 — SQLite + CRUD + 3NF (Halloween-tema) | Modern — komplett med scenariobeskrivning | Kan användas direkt, byt eventuellt tema |
| `assignment/part_1/teacher_matrix.md` | Bedömningsmatris del 1 | Modern — G/VG-kriterier | Kan användas direkt |
| `assignment/part_1/teacher_instructions.md` | Lärarinstruktioner del 1 | Modern | Kan användas direkt |
| `assignment/part_1/monstertracker_schema.sql` | SQL-schema för uppgiften | Modern | Kan användas direkt |
| `assignment/part_2/monster_costume_emporium.md` | Inlämningsuppgift del 2 — ADO.NET → EF Core | Modern — hög komplexitet, pedagogisk progression | Kan användas direkt |
| `assignment/part_2/teacher_matrix.md` | Bedömningsmatris del 2 | Modern | Kan användas direkt |
| `assignment/part_2/MonsterCostumeAdo/` | Startmaterial C# (komplett projekt) | Modern | Kan användas direkt |

### Källa: 2024/csharp/2_db

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `Assignments/Assignment1_library/1_sqlite_g_v2.md` | Uppgift biblioteksdatabas (SQLite) | Gammal stil | Behöver skrivas om |
| `Assignments/Assignment_mongodb/requirements.md` | MongoDB-uppgift | Gammal stil | Behöver skrivas om |

### Källa: Material från Codic

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `C#/Inlämningar/Dörrlogg (SQL)/Inlämningsuppgift1.docx` | Uppgift databas (Dörrlogg) | Gammal stil — docx | Behöver konverteras och skrivas om |
| `C#/Inlämningar/Dörrlogg 2 (EF)/Inlämningsuppgift2.docx` | Uppgift EF (Dörrlogg) | Gammal stil — docx | Behöver konverteras och skrivas om |
| `C#/Inlämningar/Webbutik (Databas & ASP.net)/` | Uppgift webbutik databas | Gammal stil — docx | Behöver konverteras och skrivas om |

---

## 9. Tentafrågor och träning

### Källa: 2025/2_databases — exam/

| Fil | Innehåll | Åtgärd |
|-----|----------|--------|
| `mysql_basics_training.md` | SQL-grunder | Kan användas direkt |
| `sql_crud_training.md` | CRUD-träning | Kan användas direkt |
| `aggregering_training.md` | Aggregeringsfrågor | Kan användas direkt |
| `joins_training.md` | Joins-träning | Kan användas direkt |
| `sql_normalization_training.md` | Normaliseringsfrågor | Kan användas direkt |
| `sql_injection_training.md` | SQL Injection-frågor | Kan användas direkt |
| `mysql_permissions_training.md` | Behörighetsfrågor | Kan användas direkt |
| `mysql_backup_restore_training.md` | Backup/restore-frågor | Kan användas direkt |
| `adonet_basics_training.md` | ADO.NET-frågor | Kan användas direkt |
| `adonet_training.md` | ADO.NET fördjupning | Kan användas direkt |
| `entity_framework_training.md` | EF Core-frågor | Kan användas direkt |
| `docker_basics_training.md` | Docker-frågor | Granska — bara om Docker ingår i CLO26 |

---

## 10. Kursplan och referensmaterial

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|----------|-----|----------|--------|
| `2025/2_databases/kursplan.md` | Kursplan CLO25 Databaser (20 yhp, 4 veckor) | Modern | Bra mall — anpassa till CLO26 kursplan |
| `books/csharp_cmyh/C-Sharp/databases/index.md` | Innehållsförteckning C# + databaser | Campus Mölndal | Referens |
| `books/csharp_cmyh/C-Sharp/databases/brief/` | Kortreflektioner per DB-motor | Campus Mölndal | Bakgrundsläsning |

---

## Sammanfattning

### Gott om material — kan användas direkt eller med minimala justeringar

| Område | Vad som finns |
|--------|--------------|
| **Marp-föreläsningar** | Komplett uppsättning i 2025-materialet: CRUD, aggregering, joins, normalisering, ADO.NET, EF Core, MongoDB, säkerhet. Alla i CLO25-dark-theme-stil. Kräver bara CLO25→CLO26-byte och eventuell ton-anpassning till Marcus-röst. |
| **Övningar** | Rik bank: 7 normaliseringsövningar, 5 ADO.NET-övningar, 3 MongoDB-övningar, 10+ EF-övningar, kaos-databas-workshops. Majoriteten moderna och klara. |
| **Tentafrågor** | 11 träningsfiler med `<details>`-svar. Direkt återanvändbart för vecka 4-repetition. |
| **Inlämningsuppgifter** | Två kompletta uppgifter med bedömningsmatriser och lärarmanus (Monster Tracker + Monster Costume Emporium). Hög pedagogisk kvalitet. |
| **SQL-filer** | kaos_database.sql, normalized_database.sql, seed-filer, livekod-SQL. Konkret och körbart. |
| **Livekod C#** | CustomerRegistry + ADOKundregister med Facade, Interface och Seeder — Clean Code-exemplar. |

### Saknas eller behöver skapas från grunden

| Vad som saknas | Kommentar |
|----------------|-----------|
| **Marp-föreläsning om ER-diagram** | 2024-materialet har text (`2_relations_er_diagram.md`) men ingen Marp. Behöver byggas nytt eller 2024-texten konverteras. |
| **Lästext (notes/) som matchar Marp-bilderna** | 2025-materialet har föreläsningar men ingen systematisk läsversion. Campus Mölndal-materialet från `books/` kan moderniseras som komplement. |
| **Repetition-quiz för normalisering** | `sql_normalization_training.md` finns men är kortare än de andra. Kan behöva utökas. |
| **Kursintro/välkomstföreläsning** | `databaser_kursintro_marp.md` finns i 2025 — granska om den passar CLO26-upplägget. |

### Noteringar

- **2023/java/SQL/lecture.md** — bara en fil. Troligen för gammal och Java-specifik för att vara relevant.
- **2022/SQL/** — minimalistiska referensfiler, inget undervisningsvärde utan omskrivning.
- **Material från Codic/** — docx/pdf-format. Dörrlogg-uppgifterna har en intressant idé (loggning) men kräver full omskrivning som Markdown.
- **Campus Mölndal-materialet** (`books/csharp_cmyh/`) har konsekvent `author: Campus Mölndal`-header. Ta bort dessa headers och anpassa text till Marcus-röst innan publicering till studerande.
