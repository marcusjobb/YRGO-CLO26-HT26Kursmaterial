---

title: SQL vs NoSQL 🗄️
author: Marcus Ackre Medina
type: article
topic: databaser
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/csharp_cmyh/C-Sharp/databases/sql-vs-nosql.md"
description: "10 projekt-exempel från verkligheten"
tags: ["csharp", "databaser", "git", "nosql", "oop", "sql"]
week_fit: []
---

# SQL vs NoSQL 🗄️

🟢


---

## När ska du välja vad?

*10 projekt-exempel från verkligheten*

---

## 📊 Projekt 1: Banksystem

**Scenario:**
En bank behöver hantera:
- Kundkonton med saldo
- Transaktioner mellan konton
- Låneansökningar
- Säkerhetskontroller

**Krav:** Pengar får aldrig försvinna vid överföringar

<div class="decision sql">
✅ SQL (PostgreSQL/SQL Server)
</div>

**Varför?** ACID-transactions garanterar att pengar aldrig försvinner. Om överföring misslyckas rullas allt tillbaka.

---

## 📱 Projekt 2: Instagram-klon

**Scenario:**
Social media-app med:
- Användarprofiler med bio/avatar
- Posts med bilder + text
- Miljontals likes och kommentarer
- Feed måste ladda snabbt

**Krav:** Snabb läsning, okej med eventual consistency

<div class="decision nosql">
✅ NoSQL (MongoDB/DynamoDB)
</div>

**Varför?** Posts är dokument (bild + metadata + kommentarer). Läshastighet viktigare än perfekt konsistens. Enkelt att skala horisontellt.

---

## 🛒 Projekt 3: E-handel (Webhallen-klon)

**Scenario:**
Webshop med:
- Produktkatalog med kategorier
- Ordrar med orderrader
- Lagersaldo som måste stämma
- Betalningar

**Krav:** Lagersaldo får ALDRIG bli negativt

<div class="decision sql">
✅ SQL (PostgreSQL)
</div>

**Varför?** Relationer mellan Order → OrderRader → Produkter. Transactions säkerställer att två kunder inte köper sista produkten samtidigt.

---

## 📝 Projekt 4: Wikipedia-klon

**Scenario:**
Wiki-system med:
- Artiklar i Markdown/HTML
- Versionshistorik
- Sökfunktion
- Miljontals artiklar

**Krav:** Snabb textsökning och skalbarhet

<div class="decision nosql">
✅ NoSQL (Elasticsearch + MongoDB)
</div>

**Varför?** Artiklar är dokument utan komplexa relationer. Elasticsearch ger kraftfull full-text search. MongoDB för flexibel dokumentstruktur.

---

## 🏥 Projekt 5: Sjukvårdsjournal

**Scenario:**
Journalsystem med:
- Patientdata
- Diagnoser kopplat till ICD-10 koder
- Ordinationer (mediciner)
- Besökshistorik

**Krav:** Journaler får ALDRIG blandas ihop mellan patienter

<div class="decision sql">
✅ SQL (SQL Server med HIPAA compliance)
</div>

**Varför?** Kritiska relationer (Patient → Diagnos → Medicin). Kräver ACID + audit logging. Stark konsistens viktigare än prestanda.

---

## 🎮 Projekt 6: Multiplayer-spel (leaderboard)

**Scenario:**
Online-spel med:
- Spelarsessions
- Realtids-poäng
- Global leaderboard
- Achievements

**Krav:** Miljontals skrivningar per sekund, eventual consistency OK

<div class="decision nosql">
✅ NoSQL (Redis + Cassandra)
</div>

**Varför?** Redis för in-memory leaderboard (snabbast). Cassandra för spelarsessions (write-heavy). Eventual consistency räcker för leaderboards.

---

## 📚 Projekt 7: Skoladministration

**Scenario:**
Skolsystem med:
- Elever kopplat till klasser
- Lärare undervisar kurser
- Betyg kopplade till elev + kurs
- Närvarorapportering

**Krav:** Betyg får inte hamna på fel elev

<div class="decision sql">
✅ SQL (PostgreSQL)
</div>

**Varför?** Komplexa relationer (många-till-många mellan Elever ↔ Kurser ↔ Lärare). Foreign keys förhindrar orphaned records. Betyg kräver strong consistency.

---

## 📊 Projekt 8: Analytics Dashboard (Google Analytics-klon)

**Scenario:**
Web analytics med:
- Miljoner events per dag
- Sidvisningar, klick, scrolls
- Aggregeringar (summor, medelvärden)
- Tidsserie-data

**Krav:** Skriv mycket, läs aggregat snabbt

<div class="decision nosql">
✅ NoSQL (ClickHouse/TimescaleDB)
</div>

**Varför?** Time-series optimerat. Write-heavy workload. Eventual consistency OK. ClickHouse är columnar = snabba aggregeringar.

---

## ✈️ Projekt 9: Bokningssystem (Flyg/Hotell)

**Scenario:**
Bokningssystem med:
- Flygstolar/hotellrum
- Bokningar med betalningar
- Inga dubbelbokningar
- Priser per datum

**Krav:** En stol kan bara bokas EN gång

<div class="decision sql">
✅ SQL (PostgreSQL)
</div>

**Varför?** Optimistic locking + transactions förhindrar dubbelbokningar. Relationer mellan Flygning → Stolar → Bokningar. ACID kritiskt.

---

## 🌍 Projekt 10: IoT-sensorer (Smart Hem)

**Scenario:**
IoT-plattform med:
- Tusentals sensorer
- Temperatur, luftfuktighet var 10:e sekund
- Olika sensortyper (flexibel schema)
- Historik för grafer

**Krav:** Write-heavy, flexibel struktur, eventual consistency OK

<div class="decision nosql">
✅ NoSQL (InfluxDB/MongoDB)
</div>

**Varför?** Time-series data. Olika sensorer = olika fält (schemaless). Write-heavy. InfluxDB optimerat för tidsserie-data.

---

## 🎯 Sammanfattning

| Välj SQL när...                          | Välj NoSQL när...                        |
|------------------------------------------|------------------------------------------|
| ✅ Relationer är komplexa                | ✅ Dokument/key-value räcker             |
| ✅ ACID transactions krävs               | ✅ Eventual consistency OK               |
| ✅ Data är strukturerad                  | ✅ Flexibel schema behövs                |
| ✅ Strong consistency måste             | ✅ Horisontell skalning behövs          |
| ✅ Rapporter med JOINs                   | ✅ Write-heavy workload                  |

**Pro-tips:** Många projekt använder BÅDE (polyglot persistence)!

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
