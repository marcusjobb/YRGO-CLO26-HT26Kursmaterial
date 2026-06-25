# Material från Old Courses — Kurs-02 Databaser (v2)

Djupinventering genomförd 2026-06-15. Fokus: böckerna och allt som missades i runda 1.

---

## Innehållsförteckning

1. [Böcker — vad varje fil täcker](#1-böcker--vad-varje-fil-täcker)
   - csharp_cmyh/C-Sharp/databases/
   - csharp_cmyh/C-Sharp/entityframework/
   - CLO22/Docs/sql/ och entityframework/
   - devbook/Docs/csharp/sql/ och entityframework/
   - JIN23/sql/
2. [2025 — Marp-slides och övningar](#2-2025--marp-slides-och-övningar)
3. [2024 — csharp och java](#3-2024--csharp-och-java)
4. [2022 SQL](#4-2022-sql)
5. [Material från Codic](#5-material-från-codic)
6. [Bokkapitel → Marp-lektioner — veckomappning](#6-bokkapitel--marp-lektioner--veckomappning)
7. [Story-driven scenarios och verkliga dataset](#7-story-driven-scenarios-och-verkliga-dataset)
8. [Sammanfattning: vad är direkt återanvändbart](#8-sammanfattning-vad-är-direkt-återanvändbart)

---

## 1. Böcker — vad varje fil täcker

### 1.1 csharp_cmyh/C-Sharp/databases/

Rotsökväg: `/home/marcus/git/Old_courses/books/csharp_cmyh/C-Sharp/databases/`

Den mest kompletta bokserien för CLO26. Skriven av Campus Mölndal, uppdaterad höst 2025. Allt är på svenska, har TL;DR-sektion och är designad för att läsas som en kurs.

| Fil | Täcker | Direkt användbar? |
|-----|--------|-------------------|
| `index.md` | Översikt, rekommenderad studieordning (7 steg) | Ja — som kursöversikt vecka 1 |
| `relational_databases.md` | PK/FK, ACID, 1:1/1:N/M:M relationer, e-handelsexempel med komplett schema | Ja — direkt som läsmaterial |
| `normalisering.md` | 1NF, 2NF, 3NF, BCNF, 4NF, 5NF. Bokhandels-exempel, C#-koppling (listor → tabeller), denormalisering | Ja — guldgruva. 2000 rader genomarbetad text |
| `sql/joins.md` | INNER/LEFT/RIGHT/CROSS/SELF JOIN, alias, GROUP BY + JOIN, fallgropar, dad joke | Ja — kan bli Marp-slides direkt |
| `sql/aggregation.md` | COUNT, SUM, AVG, MIN, MAX | Ja |
| `sql/aggregering.md` | Alternativ fil, svenska rubriker | Kolla dubblett mot aggregation.md |
| `sql/constraints.md` | NOT NULL, UNIQUE, CHECK, PRIMARY KEY, FOREIGN KEY | Ja |
| `sql/database.md` | CREATE DATABASE, DROP DATABASE | Ja — kort, intro-material |
| `sql/tables.md` | CREATE TABLE, ALTER TABLE, DROP TABLE | Ja |
| `sql/index.md` | Navigationsindex för SQL-sektionen | — |
| `sql/sql_categories.md` | DDL/DML/DCL/TCL — kategorisering av SQL-kommandon | Ja — bra strukturöversikt |
| `sql_datatyper.md` | INT, TEXT, REAL, BLOB, NULL, datetime-typer | Ja |
| `sql_security.md` | SQL Injection (Bobby Tables / XKCD #327), parameteriserade frågor, minsta behörighets-principen, testning | Ja — hög pedagogisk kvalitet |
| `sql-vs-nosql.md` | 10 projekt-exempel (bank, Instagram, e-handel etc.) som avgör SQL vs NoSQL | Ja — perfekt intro till NoSQL-veckan |
| `sqlite_och_db_browser.md` | Installera DB Browser for SQLite, grundläggande användning | Ja — verktygsguide vecka 1 |
| `sql_verktyg_och_praktik.md` | Praktiska övningar med verktyg | Ja |
| `ado_net_sqlite.md` | ADO.NET med SQLite: Connection, Command, Reader, CRUD, parametrar, transactions, error handling. Jämförelse med EF | Ja — bokkapitel för vecka 2 |
| `database_planning.md` | Databasplanering generellt | Ja |
| `database_planning_csharp_sql.md` | Planering med C# + SQL | Ja |
| `database_planning_csharp_ef.md` | Planering med C# + EF | Ja |
| `databasplanering_example.md` | Konkret exempelplanering | Ja |
| `uml_database_design.md` | UML/ER-diagram för databasdesign | Ja — kursplanekrav |
| `story.md` | **"En liten saga om en databas"** — Pelle, Kalle och Maria firar födelsedag. All SQL lärs ut via berättelse: INSERT, UPDATE, DELETE, FK, JOINs. 12 kapitel + epilog | Ja — enastående. Se avsnitt 7 |

**brief/-undermapp** (sammanfattande kursbok-kapitel):

| Fil | Täcker |
|-----|--------|
| `brief/00_forord.md` | Förord |
| `brief/01_sqlite.md` | SQLite-kapitel |
| `brief/02_mysql.md` | MySQL-kapitel |
| `brief/03_localdb.md` | LocalDB-kapitel |
| `brief/04_sqlserver_docker.md` | SQL Server + Docker |
| `brief/05_mongodb.md` | MongoDB: SQL vs NoSQL, terminologi, installation, Docker, C#-driver, CRUD, collections | Ja — bra NoSQL-intro |
| `brief/06_reflektion.md` | Reflektion kring databasval |
| `brief/bilagor.md` | Bilagor |
| `brief/index.md` | Index |

---

### 1.2 csharp_cmyh/C-Sharp/entityframework/

Rotsökväg: `/home/marcus/git/Old_courses/books/csharp_cmyh/C-Sharp/entityframework/`

| Fil | Täcker |
|-----|--------|
| `EntityFrameworkCore.md` | Vad är ORM, fördelar, begränsningar, användningsområden | Intro-läsning |
| `index.md` | Navigationssida |
| `example.md` | Exempelkod | Kodreferens |
| `migrationer.md` | Migrations-workflow: add-migration, update-database, rollback | Viktigt för vecka 3 |
| `context/index.md` | Vad är DbContext | |
| `context/dbcontext_lifecycle.md` | DbContext-livscykel, dependency injection, dispose | |
| `context/konfiguration.md` | Konfiguration av EF Core context | |

---

### 1.3 CLO22/Docs/sql/ och entityframework/

Rotsökväg: `/home/marcus/git/Old_courses/books/CLO22/Docs/`

Äldre version av samma material. Fyra SQL-filer:

| Fil | Täcker |
|-----|--------|
| `sql/index.md` | Index |
| `sql/Databas.md` | CREATE/DROP DATABASE |
| `sql/Tabeller.md` | CREATE TABLE |
| `sql/Constraints.md` | Constraints |

EF-filer (äldre version):

| Fil | Täcker |
|-----|--------|
| `entityframework/EntityFrameworkCore.md` | Intro EF Core |
| `entityframework/exempel.md` | Exempelkod |
| `entityframework/index.md` | Index |
| `entityframework/kontext/index.md` | DbContext |

**Bedömning:** Innehållet är sämre än csharp_cmyh-versionen. Använd csharp_cmyh som primärkälla.

---

### 1.4 devbook/Docs/csharp/sql/ och entityframework/

Rotsökväg: `/home/marcus/git/Old_courses/books/devbook/Docs/csharp/`

Hybridbok med C# och Java. SQL-kapitlet är identiskt med CLO22-versionen (Marcus M. ursprungsversion 2022).

| Fil | Täcker |
|-----|--------|
| `sql/Databas.md` | CREATE/DROP DATABASE, skillnader SQLite/MySQL/SQL Server |
| `sql/Tabeller.md` | CREATE TABLE |
| `sql/Constraints.md` | Constraints |
| `sql/index.md` | Index |
| `entityframework/EntityFrameworkCore.md` | EF Core intro |
| `entityframework/exempel.md` | Exempelkod |
| `entityframework/kontext/index.md` | DbContext |

Javadelen har också `devbook/Docs/java/sql/` med identiska SQL-filer (samma innehåll, annat kontext).

**Bedömning:** Använd csharp_cmyh — devbook är äldre. Devbook kan vara referens om studerande frågar om Java.

---

### 1.5 JIN23/sql/ (Java-kursboken)

Rotsökväg: `/home/marcus/git/Old_courses/books/JIN23/sql/`

Java-kursboken har ett dedikerat SQL-kapitel.

| Fil | Täcker |
|-----|--------|
| `index.md` | Index |
| `databas.md` | CREATE/DROP DATABASE. Välskriven intro med "Vad är en databas?", skillnader SQL Server/SQLite/MySQL, dad joke | Bra, på svenska |
| `tabeller.md` | CREATE TABLE |
| `constraints.md` | Constraints |
| `servers.md` | Jämförelse SQL Server vs SQLite vs MySQL. Tydlig tabell med fördelar per system | Ja — kan användas direkt för CLO26 |

**Notering:** JIN23-boken täcker inte joins eller normalisering i sql/-mappen. Det hanterades i föreläsnings-markdown-filer istället.

---

## 2. 2025 — Marp-slides och övningar

Rotsökväg: `/home/marcus/git/Old_courses/2025/2_databases/`

### 2.1 Lectures — Marp-slides

| Fil | Innehåll (rubriker) | Vecka |
|-----|---------------------|-------|
| `lectures/databaser_kursintro_marp.md` | Kursintro. Veckoplan: V1 SQL, V2 Arkitekt, V3 EF, V4 NoSQL + tentamen. Verktyg, teamarbete, progression | Vecka 1 dag 1 |
| `lectures/crud/databaser_crud_marp.md` | Vad är relationsdatabas? CRUD, CREATE/INSERT/SELECT/UPDATE/DELETE, WHERE, datatyper, constraints, PK/FK, säkerhetsbästa praxis, övning skolsystem | Vecka 1 |
| `lectures/joins/databaser_joins_marp.md` | INNER JOIN, LEFT JOIN, RIGHT JOIN, FULL JOIN, Self JOIN, JOIN+GROUP BY, alias, WHERE vs ON, fallgropar | Vecka 1-2 |
| `lectures/aggregation/databaser_aggregering_marp.md` | GROUP BY, HAVING, COUNT/SUM/AVG/MIN/MAX, WHERE vs HAVING | Vecka 1-2 |
| `lectures/01_databas_kaos_analys.md` | Vad är kaos-databas? Redundans, uppdaterings-/borttagnings-/insättningsanomalier. 1NF/2NF/3NF. Räddningsplan: normaliserad design, migreringsplan | Vecka 1 dag 2 |
| `lectures/02_denormalisering_for_ai.md` | Bryta normaliseringsregler medvetet för AI/ML-träningsdata. Feature engineering, RFM-analys, one-hot encoding | Vecka 1 dag 3 |
| `lectures/ado/databaser_adonet_marp.md` | ADO.NET: vad är det, komponenter, Connection string, SQLiteConnection, ExecuteScalar/NonQuery/Reader, CRUD, parametrar, NULL-hantering | Vecka 2 |
| `lectures/ef/03_entity_framework_magin_marp.md` | EF Core intro, NuGet, Code First, migrationer, Docker+MySQL (torsdag), Fort Knox (fredag), koppling till övningar | Vecka 3 |
| `lectures/ef_core/part2_mysql_docker/lecture/01_mysql_docker_marp.md` | SQLite vs MySQL, Docker-koncept, starta MySQL container, koppla EF Core till MySQL, migration för MySQL | Vecka 3 torsdag |
| `lectures/ef_core/part2_mysql_docker/lecture/02_security_backup_marp.md` | Fort Knox Protocol: Bobby Tables, SQL Injection, EF Core-skydd, när EF inte skyddar, minsta behörighet, read-only user, app user, backup/restore | Vecka 3 fredag |
| `lectures/MongoDB/nosql_overview_marp.md` | NoSQL-intro: relationsdatabaser vs NoSQL, dokumentdatabaser, när använda MongoDB | Vecka 4 måndag |
| `lectures/MongoDB/mongodb_marp.md` | MongoDB: terminologi (collection/document/BSON), Compass, Atlas, Docker, skapa collection, queries | Vecka 4 tisdag |
| `lectures/MongoDB/mongodb_crud_marp.md` | MongoDB CRUD-operationer | Vecka 4 |
| `lectures/MongoDB/nosql_lecture_code.md` | Kodexempel NoSQL | Referens |
| `lectures/MongoDB/livecode_1_compass_basics.md` | Livecoding Compass | Referens |
| `lectures/MongoDB/livecode_2_csharp_mongo.md` | Livecoding C# + MongoDB | Referens |
| `lectures/MongoDB/livecode_3_design_showdown.md` | Livecoding design-jämförelse SQL vs MongoDB | Referens |
| `lectures/MongoDB/sql-vs-nosql.md` | SQL vs NoSQL-jämförelse | Referens |
| `lectures/MongoDB/mongodb-atlas-compass-setup.md` | Setup-guide | Referens |

**Direkta demo-filer:**
- `lectures/crud/livedemo.sql` — Live-demo SQL för CRUD-lektion
- `lectures/ovningar_setup.sql` — Setup-SQL för övningarna
- `lectures/ovningar_losningar.sql` — Lösnings-SQL
- `lectures/ado/demo/` — ADO.NET demo-projekt (CustomerRegistry, ADOTwoTables)
- `lectures/ado/live/` — ADO.NET live-coding projekt

---

### 2.2 Exercises — Övningar

#### Normalisering (7 övningar)

| Fil | Scenario | Nivå |
|-----|----------|------|
| `exercises/normalizing/01_studenter_och_kurser.md` | Studentregister → normalisera, UML-diagram | Grundläggande |
| `exercises/normalizing/02_bibliotek_och_låntagare.md` | Bibliotekssystem | Grundläggande |
| `exercises/normalizing/03_restaurang_beställningar.md` | Restaurangbeställningar | Medel |
| `exercises/normalizing/04_skolschema.md` | Skolschema | Medel |
| `exercises/normalizing/05_webshop.md` | Webshop-databas | Medel |
| `exercises/normalizing/06_projekthantering.md` | Projekthantering | Avancerad |
| `exercises/normalizing/07_sjukhus_system.md` | Sjukhussystem | Avancerad |

#### Kaos-databasen (tema-övning)

| Fil | Scenario |
|-----|----------|
| `exercises/01_radda_kaos_databasen.md` | WildShop AB: analysera och rädda katastrofalt designad webshop-databas |
| `exercises/01_radda_kaos_databasen_sqlite.md` | SQLite-version av ovanstående |
| `exercises/02_denormalisering_for_ai.md` | Denormalisera medvetet för AI/ML |
| `exercises/02_denormalisering_for_ai_sqlite.md` | SQLite-version |

#### ADO.NET-övningar (5 steg)

| Fil | Innehåll |
|-----|----------|
| `exercises/ado/1_sqlite_setup.md` | Setup SQLite + ADO.NET |
| `exercises/ado/2_crud_operations.md` | CRUD med ADO.NET |
| `exercises/ado/3_batch_operations.md` | Batch-operationer |
| `exercises/ado/4_error_handling.md` | Felhantering |
| `exercises/ado/5_diary_app_clean_code.md` | Dagboksapp med Clean Code |
| `exercises/ado/SQL_injection_1_Bobby_tables.md` | SQL Injection-övning: Bobby Tables |
| `exercises/ado/SQL_injection_2_admin_hack.md` | SQL Injection-övning: admin-hack |

#### Entity Framework-övningar (13 övningar)

| Fil | Scenario | Relationer |
|-----|----------|------------|
| `exercises/ef/03_00_hello_ef.md` | Hello EF — första projektet | Basic |
| `exercises/ef/03_00_simple_crud.md` | CRUD med EF | Basic |
| `exercises/ef/03_00_simple_relations.md` | Enkla relationer | 1:N |
| `exercises/ef/03_01_pre_flight_checklist.md` | Checklista före EF-projekt | — |
| `exercises/ef/03_02_code_first_workshop.md` | Code First workshop | Grundläggande |
| `exercises/ef/03_03_mysql_takeoff.md` | MySQL med EF | Grundläggande |
| `exercises/ef/03_04_fort_knox_protocol.md` | Säkerhet: läsbehörighet, app user | Säkerhet |
| `exercises/ef/03_04_heroes.md` | Hjälte-databas | 1:N |
| `exercises/ef/03_05_pethospital.md` | **Djursjukhus**: ägare → djur (1:N), CRUD | 1:N |
| `exercises/ef/03_06_diary.md` | **Dagbok**: enkla relationer | 1:N |
| `exercises/ef/03_07_pizza.md` | **Pizzadatabas**: deg 1:N, toppings N:M, kategori-filtrering, slumpa "Dagens pizza" | 1:N + N:M |
| `exercises/ef/03_08_hogwarts.md` | **Hogwarts Sorteringshatten**: Student → House (1:N) + Course (N:M), LINQ, affärslogik | 1:N + N:M |
| `exercises/ef/03_09_plantpal.md` | Plantskola | 1:N |
| `exercises/ef/03_10_zombie_apocalypse.md` | **Zombie Apocalypse**: DeleteBehavior.Restrict, index för prestanda, GroupBy + LEFT JOIN i LINQ | Avancerad |
| `exercises/ef/03_11_efmatch.md` | EF-matchning-övning | Medel |
| `exercises/ef/03_12_moviemix.md` | Filmdatabas | Medel |

#### MySQL + Docker-övningar (5 steg)

| Fil | Innehåll |
|-----|----------|
| `lectures/ef_core/part2_mysql_docker/exercises/exercise_1_docker_mysql_setup.md` | Sätta upp Docker + MySQL |
| `lectures/ef_core/part2_mysql_docker/exercises/exercise_2_migrate_heroes_to_mysql.md` | Migrera Heroes-app till MySQL |
| `lectures/ef_core/part2_mysql_docker/exercises/exercise_3_bobby_tables.md` | Bobby Tables i EF-kontext |
| `lectures/ef_core/part2_mysql_docker/exercises/exercise_4_readonly_user.md` | Skapa read-only-användare |
| `lectures/ef_core/part2_mysql_docker/exercises/exercise_5_backup_restore.md` | mysqldump backup och restore |

#### MongoDB-övningar

| Fil | Innehåll |
|-----|----------|
| `exercises/mongodb/mongodb_exercise_1.md` | Grundläggande MongoDB-övning |
| `exercises/mongodb/mongodb_exercise_2.md` | Avancerad MongoDB-övning |
| `exercises/mongodb/mongodb_facade_exercise.md` | Facade-pattern med MongoDB |
| `exercises/document_database/document_database_exercise_1.md` | Dokumentdatabas övning 1 |
| `exercises/document_database/document_database_exercise_2.md` | Dokumentdatabas övning 2 |

#### Repetitions-övningar (5 serie)

| Fil | Innehåll |
|-----|----------|
| `exercises/repetition/1_repeat_sql/` | Repetition SQL: create_database.sql, seed_data.sql, exercises.sql, instructions.md |
| `exercises/repetition/2_repeat_cs_sql/` | Repetition C# + ADO.NET: EcommerceApp (Customer, Order, Product, OrderItem, EcommerceFacade) |
| `exercises/repetition/3_repeat_cs_ef/` | Repetition C# + EF: SupportTicketApp (Customer, SupportAgent, Ticket, TicketMessage) |
| `exercises/repetition/4_repeat_mongodb_compass/` | Repetition MongoDB Compass: instructions.md + sample_contacts.json |
| `exercises/repetition/5_repeat_mongodb_cs/` | Repetition MongoDB C#: ContactLogApp (Contact, FollowUp, Interaction, ContactRepository) |

#### Gruppuppgift

| Fil | Innehåll |
|-----|----------|
| `exercises/groupassignment/halloween-webshop-database.md` | Halloween-webshop databas — gruppuppgift |
| `exercises/groupassignment/groups.md` | Grupplista |
| `exercises/groupassignment/TEACHER_SOLUTION_GUIDE.md` | Lärarguide |

#### Examen-träning (11 filer)

Alla i `exercises/../exam/`:
`mysql_basics_training.md`, `sql_crud_training.md`, `joins_training.md`, `aggregering_training.md`, `sql_normalization_training.md`, `adonet_basics_training.md`, `adonet_training.md`, `entity_framework_training.md`, `sql_injection_training.md`, `mysql_permissions_training.md`, `docker_basics_training.md`, `mysql_backup_restore_training.md`

#### SQLite-databaser (verkliga datasets)

| Fil | Innehåll |
|-----|----------|
| `exercises/chaos_denormalized_for_AI.db` | Kaotisk webshop-databas (denormaliserad) |
| `exercises/chaos_normalized.db` | Normaliserad version av samma databas |
| `exercises/chaos_sample.db` | Sample-version |
| `exercises/kaos_database.sql` | SQL-script för kaos-databasen |
| `exercises/normalized_database.sql` | SQL-script normaliserad version |

---

### 2.3 Assignment (Inlämningsuppgift 2025)

| Fil | Innehåll |
|-----|----------|
| `assignment/part_1/readme.md` | Del 1: Designa och implementera databas |
| `assignment/part_1/monster_tracker.md` | MonsterTracker-scenario |
| `assignment/part_1/monstertracker_schema.sql` | SQL-schema för MonsterTracker |
| `assignment/part_1/teacher_instructions.md` | Lärarinstruktioner |
| `assignment/part_1/teacher_matrix.md` | Bedömningsmatris |
| `assignment/part_1/example_data_inspiration.md` | Inspiration för exempeldata |
| `assignment/part_1/kursplan_relevance.md` | Koppling till kursplan |
| `assignment/part_2/monster_costume_emporium.md` | Del 2: MonsterCostume Emporium — ADO.NET-uppgift |
| `assignment/part_2/teacher_matrix.md` | Bedömningsmatris del 2 |

---

## 3. 2024 — csharp och java

### 3.1 csharp/2_db/lectures/

Rotsökväg: `/home/marcus/git/Old_courses/2024/csharp/2_db/lectures/`

Strukturerat i tre block (01_introduction, 02_db_sqlite, 03_db_h2). Enkel markdown, inga Marp-slides.

| Mapp | Filer | Täcker |
|------|-------|--------|
| `01_introduction/` | `1_intro_db.md`, `2_relations_er_diagram.md`, `3_sqlite_intro.md`, `4_sql_crud.md`, `5_pk_fk.md`, `exercises.md`, `summary_01.md` | Intro, ER-diagram, SQLite, CRUD, PK/FK |
| `02_db_sqlite/` | `1_recap_tables_and_relations.md`, `3_joins.md`, `4_normalisering.md`, `5_sql_syntax.md`, `exercises.md`, `terms.md` | Joins, normalisering, syntax, terminologi |
| `03_db_h2/` | `1_h2_intro.md`–`5_h2_compare.md`, `sherlock_murder.md` | H2 (Java-databas), **Sherlock Holmes mordmysterium** |

**Noterbart:** `sherlock_murder.md` — Sherlock Holmes löser ett mord med SQL på Metropolitan Club. Filen baseras på boken "Elementary Basic – learning to program computers in Basic with Sherlock Holmes" (ISBN 91-14601-6). Story-driven scenario med 4 misstänkta och SQL-ledtrådar. Se avsnitt 7.

### 3.2 java/2_databases/

Exakt samma struktur som csharp/2_db/lectures/ men för Java (lecture_1 till lecture_5). Tillägget är lecture_4 med MySQL och lecture_5 med:

| Fil | Täcker |
|-----|--------|
| `lecture_5/1_indexering.md` | Databasindexering |
| `lecture_5/2_transaktioner.md` | Transaktioner |
| `lecture_5/3_stored_procedures.md` | Stored procedures |
| Quizfiler (4 st) | `1_quiz.md` – `4_quiz.md` |

---

## 4. 2022 SQL

Rotsökväg: `/home/marcus/git/Old_courses/2022/SQL/`

Minimal markdown-samling. Ursprungsmaterial från Marcus tidiga kurser.

| Fil | Täcker |
|-----|--------|
| `Database.md` | CREATE DATABASE, DROP DATABASE, RENAME |
| `Tabeller.md` | CREATE TABLE |
| `Constraints.md` | Constraints |
| `CRUD-Create.md` | INSERT INTO |
| `CRUD-Read.md` | SELECT: alla, vissa, WHERE, AND, OR |
| `CRUD-Update.md` | UPDATE SET WHERE |
| `CRUD-Delete.md` | DELETE WHERE |
| `Relationer - OneToMany.md` | 1:N-relationer |
| `Relationer-ManyToMany.md` | M:M-relationer |
| `Readme.md` | Index |

**Bedömning:** Historiskt värde. Innehållet är nu betydligt bättre täckt i csharp_cmyh. Kan vara referens för att se hur grundnivån var 2022 kontra nu.

---

## 5. Material från Codic

Rotsökväg: `/home/marcus/git/Old_courses/Material från Codic/`

Materialet är äldre (binära .docx/.pdf/.pptx-filer) och täcker inte direkt databas-teorin. Databasrelevanta filer:

| Fil | Innehåll |
|-----|----------|
| `C#/Inlämningar/Dörrlogg (SQL)/Inlämningsuppgift1 (uppdaterad).docx` | Inlämningsuppgift SQL-baserad dörrlogg (C#) |
| `C#/Inlämningar/Dörrlogg 2 (EF)/Inlämningsuppgift2.docx` | Uppföljningsuppgift med Entity Framework |
| `C#/Inlämningar/Webbutik (Databas & ASP.net)/Inlämning 2 - Webbutik.docx` | Webbutik med databas |

**Bedömning:** Dörrlogg-uppgifterna (SQL → EF) är ett klassiskt Codic-spår. Intressant som pedagogisk progression (del 1 SQL, del 2 EF) men filformaten är .docx och behöver konverteras. Inte direkt användbart utan extra arbete.

---

## 6. Bokkapitel → Marp-lektioner — veckomappning

Tanken: vilka bokavsnitt kan bli Marp-lektioner för kurs-02, och i vilken vecka?

### Vecka 1 — SQL-grunder + Normalisering + ER-diagram

| Bokkapitel (exakt sökväg) | Marp-lektion titel | Dag |
|---------------------------|-------------------|-----|
| `csharp_cmyh/.../databases/story.md` | "En liten saga om en databas" — intro-föreläsning | V1 dag 1 |
| `csharp_cmyh/.../databases/relational_databases.md` | "Vad är en relationsdatabas?" — PK, FK, ACID, relationstyper | V1 dag 1 |
| `csharp_cmyh/.../databases/sql/database.md` + `tables.md` + `sql_datatyper.md` | "SQL-grunder: CREATE, datatyper, constraints" | V1 dag 1 |
| `csharp_cmyh/.../databases/sql_security.md` (Bobby Tables-sektionen) | Integreras i CRUD-lektion som säkerhetsvarning | V1 dag 1-2 |
| `csharp_cmyh/.../databases/normalisering.md` (1NF–3NF) | "Normalisering: rädda kaos-databasen" | V1 dag 2 |
| `csharp_cmyh/.../databases/uml_database_design.md` | "ER-diagram och UML för databaser" | V1 dag 2-3 |
| `csharp_cmyh/.../databases/sql/joins.md` | "INNER JOIN och LEFT JOIN" | V1 dag 4 |
| `csharp_cmyh/.../databases/sql/aggregation.md` | "GROUP BY och aggregering" | V1 dag 5 |

### Vecka 2 — Databasdesign + C# + SQL

| Bokkapitel (exakt sökväg) | Marp-lektion titel | Dag |
|---------------------------|-------------------|-----|
| `csharp_cmyh/.../databases/database_planning.md` + `database_planning_csharp_sql.md` | "Databasplanering och arkitektur" | V2 dag 1 |
| `csharp_cmyh/.../databases/sql/sql_categories.md` | "DDL, DML, DCL — SQL i kategorier" | V2 dag 1 (bakgrundsmaterial) |
| `csharp_cmyh/.../databases/ado_net_sqlite.md` | "ADO.NET: C# möter SQL" | V2 dag 3-4 |
| `csharp_cmyh/.../databases/sql_security.md` (parameterisering + minsta behörighet) | "SQL-säkerhet: parameteriserade frågor" | V2 dag 4 |

### Vecka 3 — Entity Framework

| Bokkapitel (exakt sökväg) | Marp-lektion titel | Dag |
|---------------------------|-------------------|-----|
| `csharp_cmyh/.../entityframework/EntityFrameworkCore.md` | "Vad är ORM och EF Core?" | V3 dag 1 |
| `csharp_cmyh/.../entityframework/context/dbcontext_lifecycle.md` + `konfiguration.md` | "DbContext: livscykel och konfiguration" | V3 dag 2 |
| `csharp_cmyh/.../entityframework/migrationer.md` | "Migrationer utan drama" | V3 dag 2-3 |
| `csharp_cmyh/.../databases/database_planning_csharp_ef.md` | "Databasdesign med EF Core" | V3 dag 3 |
| `csharp_cmyh/.../databases/sql_security.md` (Fort Knox-delen) | "Fort Knox: säkerhet i produktion" | V3 dag 5 |

### Vecka 4 — NoSQL + MongoDB

| Bokkapitel (exakt sökväg) | Marp-lektion titel | Dag |
|---------------------------|-------------------|-----|
| `csharp_cmyh/.../databases/sql-vs-nosql.md` | "SQL vs NoSQL: 10 verkliga projekt" | V4 dag 1 |
| `csharp_cmyh/.../databases/brief/05_mongodb.md` | "MongoDB: dokumentdatabasens värld" | V4 dag 2 |

---

## 7. Story-driven scenarios och verkliga dataset

### Story-driven scenarios

#### "En liten saga om en databas" — Pelle, Kalle och Maria
**Sökväg:** `/home/marcus/git/Old_courses/books/csharp_cmyh/C-Sharp/databases/story.md`

12 kapitel + epilog. Tre vänner (Maria firar födelsedag, Pelle och Kalle köper present). Hela SQL-grunden lärs ut via berättelsen:
- Kapitel 2–3: CREATE TABLE, INSERT (personer, saker)
- Kapitel 4: Händelse-tabell (verb i berättelsen)
- Mellanspel: Foreign Key, referensintegritet, kopplingstabeller
- Kapitel 5–12: UPDATE, transaktioner, relationer
- Epilog: UNIQUE, CHECK constraints, INDEX, JOINs läser tillbaka sagan

**Rekommendation:** Använd som intro-material vecka 1. Kan läsas som hemuppgift eller genomgås i klassen.

#### Sherlock Holmes löser ett mord med SQL
**Sökvägar:**
- `/home/marcus/git/Old_courses/2024/java/2_databases/lecture_3/sherlock_murder.md`
- `/home/marcus/git/Old_courses/2024/csharp/2_db/lectures/03_db_h2/sherlock_murder.md`

Baserat på boken "Elementary Basic – learning to program computers in Basic with Sherlock Holmes" (ISBN 91-14601-6). Fyra misstänkta (Sir Raymond Jasper, Robert Holman, Reginald Woodley, James Pope) på Metropolitan Club. Studerande löser mordet med SQL-frågor och ledtrådar (karta ingår). Klassiskt CSI-tänk.

**Rekommendation:** Perfekt för dag 2 ("CSI: Database Division" i 2025-kursintron). Hög engagemangs-faktor.

#### Kaos-databasen — WildShop AB
**Sökväg:** `/home/marcus/git/Old_courses/2025/2_databases/exercises/01_radda_kaos_databasen.md`

WildShop AB med 5 000+ kunder, 20 000+ ordrar — katastrofalt designad. Studerande analyserar, identifierar brott mot normalformer, och normaliserar. Verkliga SQLite-databaser medföljer (`chaos_sample.db`, `chaos_normalized.db`).

**Rekommendation:** Kärnövning vecka 1–2. Finns i både MySQL- och SQLite-variant.

#### Hogwarts Sorteringshatten
**Sökväg:** `/home/marcus/git/Old_courses/2025/2_databases/exercises/ef/03_08_hogwarts.md`

Hogwarts-tema för EF Core: Student → House (1:N) + Course (N:M). Sorteringsalgoritm baserad på studentegenskaper (Courage, Ambition, Wisdom, Loyalty). LINQ, `Include()`, `OnDelete(DeleteBehavior.SetNull)`.

**Rekommendation:** Stark EF-övning vecka 3. Välkänt universum ökar engagemanget.

### Övriga story-scenarios (EF-övningar)

| Scenario | Sökväg | Tema |
|----------|--------|------|
| Pizzadatabas | `exercises/ef/03_07_pizza.md` | Pizza: deg (1:N), toppings (N:M), "Dagens pizza" slumpa |
| Zombie Apocalypse | `exercises/ef/03_10_zombie_apocalypse.md` | Överlevare/zombies, baser, farliga zoner, DeleteBehavior.Restrict |
| Djursjukhus | `exercises/ef/03_05_pethospital.md` | Ägare → djur (1:N), CRUD |
| Dagbok | `exercises/ef/03_06_diary.md` | Enkel 1:N |
| PlantPal | `exercises/ef/03_09_plantpal.md` | Plantor |
| MovieMix | `exercises/ef/03_12_moviemix.md` | Filmer |

### Verkliga dataset

| Dataset | Sökväg | Innehåll |
|---------|--------|----------|
| Kaos-databas (SQLite) | `exercises/chaos_denormalized_for_AI.db` | Webshop-ordrar, denormaliserade |
| Normaliserad version | `exercises/chaos_normalized.db` | Samma data, normaliserad |
| SQL-scripts | `exercises/kaos_database.sql`, `exercises/normalized_database.sql` | Reproducerbara scripts |
| Repetitions-ecommerce | `exercises/repetition/2_repeat_cs_sql/EcommerceApp/` | Customer, Order, Product, OrderItem |
| MongoDB sample | `exercises/repetition/4_repeat_mongodb_compass/sample_contacts.json` | JSON-kontaktdata |
| Books SQL-schema | `2024/java/2_databases/lecture_1/src/books.sql` + `school.sql` | Böcker och skola |

---

## 8. Sammanfattning: vad är direkt återanvändbart

### Kan användas direkt utan omskrivning

| Material | Sökväg | Anmärkning |
|----------|--------|------------|
| Normalisering (komplett) | `csharp_cmyh/.../databases/normalisering.md` | 1NF–5NF, 2000 rader, svenska |
| Joins-kapitel | `csharp_cmyh/.../databases/sql/joins.md` | Komplett med fallgropar och dad joke |
| SQL-säkerhet | `csharp_cmyh/.../databases/sql_security.md` | Bobby Tables, parameterisering |
| ADO.NET-kapitel | `csharp_cmyh/.../databases/ado_net_sqlite.md` | Heltäckande |
| EF Core intro | `csharp_cmyh/.../entityframework/EntityFrameworkCore.md` | Solid intro |
| Migrationer | `csharp_cmyh/.../entityframework/migrationer.md` | Workflow |
| SQL vs NoSQL | `csharp_cmyh/.../databases/sql-vs-nosql.md` | 10 scenario-exempel |
| MongoDB-kapitel | `csharp_cmyh/.../databases/brief/05_mongodb.md` | Komplett |
| Kaos-övning + .db-filer | `2025/2_databases/exercises/01_radda_kaos_databasen.md` | Inkl. verklig databas |
| Normaliseringsövningar (7st) | `2025/2_databases/exercises/normalizing/` | Alla klara |
| EF-övningarna (13st) | `2025/2_databases/exercises/ef/` | Alla klara med scenarios |
| Examen-träningsfiler (11st) | `2025/2_databases/exam/` | Direkt tentamensförberedelse |
| Marp-slides (12 presentationer) | `2025/2_databases/lectures/` | Mörkt tema, klar att visa |

### Kräver omskrivning/anpassning till CLO26-stil

| Material | Vad behöver ändras |
|----------|-------------------|
| `story.md` (Pelle/Kalle/Maria) | Ev. anpassa referens från "Campus Mölndal" till CLO26. Annars redo. |
| Sherlock-mordet | Kodspråk var Java/H2 2024 — behöver portas till C#/SQLite om det ska användas |
| 2022 SQL-filer | För enkla, bättre versioner finns i csharp_cmyh |
| Codic-material (.docx) | Behöver konverteras till Markdown |

### Saknas i old_courses (behöver skapas nytt för CLO26)

- ER-diagram-övning med Mermaid (böckerna nämner UML men övningar saknas)
- Dedikerat slide-deck om databastyper (när SQLite vs MySQL vs PostgreSQL i ett C#-projekt)
- Övning som explicit täcker SELECT med subquery (VG-krav i kursplanen)

---

*Inventering av: Claude (agent, marcus-yh-claude-assistent) · 2026-06-15*
