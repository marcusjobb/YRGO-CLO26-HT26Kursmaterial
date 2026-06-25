# Veckoplan dag-för-dag — Kurs 02: Databaser

**Kurs:** 4 veckor | 20 YH-poäng  
**Schema:** Mån fm + Mån em + Tis fm + Tis em + Ons (online) = 5 pass/vecka  
**Examination:** Skriftligt prov + inlämningsuppgifter

---

## Ämnen med tidsuppskattning

| Ämne | Tid | Nivå | Vecka |
|------|-----|------|-------|
| Vad är en databas? Terminologi, begrepp | ½ pass | 🟢 | V1 |
| MySQL installation + första SQL-frågor | ½ pass | 🟢 | V1 |
| SELECT, WHERE, ORDER BY, LIMIT | 1 pass | 🟢 | V1 |
| ER-diagram (UML för databaser) | ½ pass | 🟡 | V1 |
| Normalisering (1NF, 2NF, 3NF) | 1 pass | 🔴 | V2 |
| PRIMARY KEY, FOREIGN KEY, relationer (1:1, 1:N, N:M) | 1 pass | 🟡 | V2 |
| JOIN (INNER, LEFT, RIGHT) | 1 pass | 🟡 | V2 |
| Aggregat: COUNT, SUM, GROUP BY, HAVING | ½ pass | 🟡 | V2 |
| Subqueries | ½ pass | 🔴 | V2 |
| UPDATE, DELETE, transactions | ½ pass | 🟡 | V3 |
| Säkerhet: SQL injection, autentisering, backup | 1 pass | 🟡 | V3 |
| MongoDB intro: dokumentdatabas vs relational | ½ pass | 🟡 | V3 |
| MongoDB CRUD | ½ pass | 🟡 | V3 |
| Repetition + normalisera befintlig design | 1 pass | 🟡 | V4 |
| Tenta | 1 pass | — | V4 |

**Totalt:** ~11 pass aktiv undervisning + 4 pass handledning/övning

---

## Vecka 1 — SQL-grunder och miljö

**Mål:** Alla har MySQL igång. Kan skriva SELECT-frågor, förstår vad en databas är för något.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | fm | Vad är en databas? Terminologi: tabell, rad, kolumn, schema, primärnyckel. MySQL-installation (Workbench eller DBeaver). Skapa första databasen live | Intro + miljö |
| Mån | em | Första SQL: `CREATE TABLE`, `INSERT INTO`, `SELECT * FROM`. Live-kod: en enkel Products-tabell | SQL CRUD del 1 |
| Tis | fm | `WHERE`, `AND/OR`, `ORDER BY`, `LIMIT`. Övningar: filtrera produkter, sortera resultat | SELECT-frågor |
| Tis | em | ER-diagram: symboler, entiteter, attribut, relationer. Rita INNAN vi kodar (penna + whiteboard). Modell: webshop | ER-diagram |
| Ons | online | Handledning + övningsuppgifter | Övning |

**Nyckelord denna vecka:** `databas`, `tabell`, `rad`, `kolumn`, `primärnyckel`, `SQL`, `SELECT`, `INSERT`, `schema`

**Inlämning:** Ingen.

---

## Vecka 2 — Relationer och normalisering

**Mål:** Förstår relationer mellan tabeller. Kan JOIN:a. Vet vad normalisering innebär och varför det finns.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | fm | Normalisering: vad är redundans? 1NF, 2NF, 3NF — live-genomgång med ett dåligt schema som vi förbättrar steg för steg | Normalisering |
| Mån | em | `PRIMARY KEY`, `FOREIGN KEY`, relationer: 1:1, 1:N, N:M. Skapa Orders + OrderItems kopplat till Products | Relationer |
| Tis | fm | `INNER JOIN`, `LEFT JOIN`, `RIGHT JOIN`. Visuell genomgång (Venn-diagram) + övningar: hämta orders med produktnamn | JOIN |
| Tis | em | `COUNT`, `SUM`, `AVG`, `GROUP BY`, `HAVING`. Subqueries: `SELECT ... WHERE id IN (SELECT ...)` | Aggregat + subqueries |
| Ons | online | Övningar: bygga ut webshop-schemat. Handledning | Övning |

**Nyckelord denna vecka:** `normalisering`, `1NF`, `2NF`, `3NF`, `primärnyckel`, `främmande nyckel`, `JOIN`, `relation`, `aggregat`

**Inlämning:** Ingen.

---

## Vecka 3 — Avancerat SQL + säkerhet + MongoDB

**Mål:** Klarar UPDATE/DELETE säkert. Förstår SQL injection och hur man skyddar sig. Har provat MongoDB.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | fm | `UPDATE`, `DELETE` med och utan `WHERE` (visa vad som händer utan!). Transactions: `BEGIN`, `COMMIT`, `ROLLBACK` | UPDATE/DELETE/transaktioner |
| Mån | em | Säkerhet: SQL injection live-demo (visa hur det fungerar, sedan hur man förhindrar det). Autentisering. Backup-strategier | Säkerhet |
| Tis | fm | MongoDB: vad är en dokumentdatabas? JSON-dokument, collections, skillnader mot SQL. Installation + MongoDB Compass | MongoDB intro |
| Tis | em | MongoDB CRUD: `find()`, `insertOne()`, `updateOne()`, `deleteOne()`. Jämförelse med SQL | MongoDB CRUD |
| Ons | online | **Inlämningsuppgiften presenteras**. Handledning | Inlämning |

**Nyckelord denna vecka:** `transaction`, `rollback`, `SQL injection`, `parametriserade frågor`, `dokumentdatabas`, `collection`, `JSON-dokument`

**Inlämning:** Presenteras denna vecka.

---

## Vecka 4 — Repetition + tenta

**Mål:** Tentan visar förståelse för SQL, normalisering, relationer och skillnaden relational vs document.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | fm | Repetition: alla ämnen på en rad. Q&A om inlämningen | Repetition |
| Mån | em | Designövning: ett dåligt schema → normalisera det tillsammans. Diskussion: när väljer man SQL vs MongoDB? | Designanalys |
| Tis | fm | **TENTA** | Tenta |
| Tis | em | Genomgång av tentan. Handledning: inlämningsuppgiften | Genomgång |
| Ons | online | Handledning inlämning | Handledning |

**Tenta täcker:** SQL CRUD, SELECT med filter/sortering, JOIN, normalisering (1NF–3NF), ER-diagram, säkerhet, skillnad SQL vs NoSQL

---

## Sammanfattning: svårighetsgrad och tid per ämne

```
Terminologi + installation    🟢 ½ pass  — konkret, alla lyckas
SELECT / WHERE / ORDER BY     🟢 1 pass  — logisk syntax
ER-diagram                    🟡 ½ pass  — rita = förstå, värt investeringen
Normalisering                 🔴 1 pass  — abstrakt, ta tid att gå igenom med dåliga exempel
PRIMARY/FOREIGN KEY           🟡 1 pass  — kräver genomgång av varför
JOIN                          🟡 1 pass  — visuellt, Venn-diagram hjälper
Aggregat + subqueries         🟡 ½+½ pass — GROUP BY tar tid att fatta
UPDATE/DELETE/transaktioner   🟡 ½ pass  — transactions är nytt tänk
Säkerhet (SQL injection)      🟡 1 pass  — live-demo är det som sätter sig
MongoDB                       🟡 1 pass  — annorlunda tankesätt, gå inte för djupt
```

---

## Nästa: Kurs 03 — Fördjupad OOP (Entity Framework, design patterns, refactoring)
