# Kurs 2 — Databashantering och design (CLO01H26)

**YH-poäng:** 20 | **Veckor:** 4 | **Veckor i schema:** v.42–45
**Examination:** Skriftligt prov (terminologi) + inlämningsuppgifter

## Kursens syfte

Lägger till en ny dimension — datan bakom applikationen. Studerande lär sig modellera,
designa och implementera både relations- och dokumentdatabaser. Kursen behandlar säkerhet
och attackvektorer. EF Core introduceras INTE här — det tillkommer i Kurs 3 när OOP-
förkunskaperna är starka nog. Här lär de sig SQL direkt mot databasen.

## Moduler och veckor

| Vecka | Dag | Moduler | Tema |
|-------|-----|---------|------|
| v.42 | Måndag | 01_intro_databaser | Vad är en databas? Relationsmodellen, ER-diagram |
| v.42 | Onsdag | 02_sql_hardcore | SELECT, WHERE, INSERT, UPDATE, DELETE — SQLite Browser |
| v.43 | Måndag | 03_normalisering_gdpr | 1NF–3NF, GDPR och personuppgifter |
| v.43 | Onsdag | 04_sql_avancerat | JOINs, GROUP BY, subqueries, MoSCoW-prioritering |
| v.44 | Måndag | 05_mongodb | Dokumentdatabaser — vad, varför, när? MongoDB CRUD |
| v.44 | Onsdag | 05_mongodb | MongoDB vs SQL — jämförelse, aggregation pipeline |
| v.45 | Måndag | 07_sakerhet_och_backup | SQL Injection live-hack, parameteriserade frågor, backup |
| v.45 | Onsdag | 07_uml_och_design + 08_projekt | ER-diagram fördjupning + examination |

> MongoDB får en hel vecka (v.44) för att ge tillräcklig känsla för dokumentdatabaser.
> Balansen SQL:MongoDB ≈ 2:1 — SQL är grunden, MongoDB är det viktiga alternativet.

## Lärandemål

### Kunskap (Terminologi — tentamen)
- Redogöra för terminologi och uppbyggnad av relationsdatabaser
- Redogöra för terminologi och uppbyggnad av dokumentdatabaser (MongoDB)
- Redogöra för skillnader och likheter mellan dokument- och relationsdatabaser
- Redogöra för nivåerna för normalisering
- Redogöra för SQL-språkets syntax och funktion (CRUD)
- Redogöra för säkerhet, autentisering, attackvektorer och backup
- Beskriva UMLs struktur vid databasdesign

### Färdighet (Inlämningar)
- Hantera data i relationsdatabas med SQL (CRUD)
- **VG:** Avancerade SQL — subqueries och JOIN över flera tabeller
- Hantera data i dokumentdatabas (MongoDB CRUD)
- Skapa tabeller och relationer
- **VG:** Skapa tabeller enligt normaliseringsfilosofin
- Hantera säkerhet — autentisering, attackvektorer, backup

### Kompetens (Projekt)
- Analysera och förbättra en befintlig databasdesign
- **VG:** Reflektera över alternativa lösningar och konsekvenser
- Modellera, designa och skapa normaliserad relationsdatabas med SQL
- **VG:** Resonera kring vald design och alternativa, argumentera för val

## Inlämningar

| Inlämning | Innehåll | Täcker |
|-----------|----------|--------|
| Inlämning 1 | SQL-frågor mot given databas (SELECT, JOIN, aggregat) | SQL, normalisering |
| Inlämning 2 | MongoDB CRUD + jämförelse med SQL-alternativet | Dokumentdatabaser |
| Slutinlämning | Databasdesign (ER-diagram + SQL + normalisering + reflection) | Allt + VG-resonemang |

## Extra installation (kurs 2)

- Docker (MySQL-container)
- SQLite Browser
- MongoDB Compass (GUI för MongoDB)

## Återkommande inslag

- **SQL Injection live-hack (v.45):** Hacka en loginruta med `' OR '1'='1` live
  Källmaterial: `Old_courses/2025/2_databases/exercises/ado/`
- **ER-diagram finns med i varje inlämning** (introducerat v.42, obligatoriskt från v.43)
- **Definition of Done** introduceras v.42 — när är en inlämning faktiskt klar?

## OBS: EF Core ingår INTE här

EF Core introduceras i Kurs 3 (Fördjupad OOP) — studerande har då starka OOP- och
SQL-förkunskaper och kan förstå vad EF abstraherar bort. Kurs 2 använder direkt SQL.
