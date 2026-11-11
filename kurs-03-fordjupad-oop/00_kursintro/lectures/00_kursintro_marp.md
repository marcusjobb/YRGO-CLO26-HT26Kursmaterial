---
marp: true
theme: nion-dark
paginate: true
---

# Nästa kurs: Fördjupad OOP i C#
## Kurs 3 · 25 YH-poäng · 5 veckor

**Start:** måndag 2 november (v45)
**Slut:** söndag 6 december (v49)

Marcus Ackre Medina · YRGO · CLO26

---

## Var är vi nu?

| # | Kurs | |
|---|---|---|
| 1 | Grundläggande OOP i C# | ✅ klar |
| 2 | Databashantering och -design | ✅ klar |
| **3** | **Fördjupad OOP i C#** | **👈 här börjar vi** |
| 4 | Test och kvalitetssäkring | därefter |
| 5 | Introduktion till Cloudutveckling | sen |

*Kurs 3 är där allt ni lärt er hittills möts: C#, OOP och databaser i samma program.*

---

## Vad bygger vi vidare på?

**Från kurs 1:**
- Klasser, properties, konstruktorer, arv
- Listor, metoder, Git

**Från kurs 2:**
- SQL, `JOIN`, aggregat
- Normalisering och GDPR
- C# mot databas, och varför parametrar är **inte** valfria

*Allt det här kommer tillbaka. Inget är bortkastat.*

---

## Efter kursen kan du

- 🗄️ **Koppla C# till databaser**: fil, minne och server
- 🧰 **Använda ramverk** (Entity Framework Core) i stället för att skriva allt för hand
- 🧩 **Känna igen och använda designmönster**
- 🧹 **Refaktorera** ful kod till ren kod
- 👀 **Granska kod** med SOLID och clean code som måttstock
- 📐 **Planera med UML**
- 📋 **Arbeta agilt** med Kanban i grupp

---

## Kursen på en sida

| Vecka | Datum | Tema |
|---|---|---|
| **1** · v45 | 2–7 nov | Databaskopplingar och ramverk + **UML** |
| **2** · v46 | 9–14 nov | Designmönster |
| **3** · v47 | 16–21 nov | Refactoring, clean code + **Kanban** |
| **4** · v48 | 23–28 nov | 🔥 **Hellweek**: C# hidden gems |
| **5** · v49 | 30 nov–6 dec | **Tenta** + grupppresentationer |

---

## Vecka 1 · Databaskopplingar

*2–7 november · ingen inlämning*

- Tre sorters databaser: **fil** (SQLite), **minne** (InMemory), **server** (MySQL, SQL Server)
- ADO.NET: det manuella sättet ni redan kan
- **Entity Framework Core**: Code First, `DbContext`, CRUD
- **Migrationer**: versionshantering för databasen
- **Repository pattern**: ett lager mellan logik och data
- 📐 **UML** introduceras första lektionen

---

## Vecka 2 · Designmönster

*9–14 november · inlämning, deadline **söndag 15 nov 23:59***

- Varför designmönster finns ("Gang of Four")
- **Factory Method** · **Observer** · **Strategy** · **Builder** · **Repository**
- Hur väljer man rätt mönster?
- 📐 **UML ingår i inlämningen**
- 🍃 **Onsdag:** **JSON** och **MongoDB**: samma Repository, ny databas

*Ni har redan använt flera mönster utan att veta det.*

---

## Vecka 3 · Refactoring och Kanban

*16–21 november · **gruppinlämning**, deadline **söndag 22 nov 23:59***

- **Code smells**: lång metod, God Class, magiska tal, duplicerad kod
- **SOLID**, DRY och KISS
- Refaktoreringstekniker: *Extract Method*, *Rename*
- **Kodgranskning** med checklista
- 🔎 **Onsdag:** **Regex**: mönster i text, och när det blir oläsligt
- 📋 **Kanban**: To Do / In Progress / Done, och grupparbetet startar

---

## Vecka 4 · 🔥 Hellweek

*23–28 november · ingen inlämning · **inte tentastoff***

C#-features som dyker upp överallt i riktig kod (och i AI-genererad kod):

`dynamic` · pattern matching · tuples · indexers · `?.` `??` `??=` · extension methods · records · async/await

> *"Det här kommer inte på tentan. Men nästa gång du ser något av det här vet du vad det är."*

Livekod, demos och "wow, kan man göra så?"

---

## Vecka 5 · Tenta och presentationer

*30 november–6 december*

- **Onsdag 2 december, 09:00–12:00: tentan**
- Grupppresentationer efter tentan
- Repetition, handledning och tid att koda ikapp

---

## Så blir du godkänd

| Del | Vad |
|---|---|
| **Inlämningar** | En i vecka 2 och en **gruppinlämning** i vecka 3, med **UML** |
| **Tenta** | Skriftlig, onsdag 2 dec |
| **Grupparbete** | Planering på Kanban-board + presentation |

- **G:** alla inlämningar godkända + godkänd tenta
- **VG:** G-kraven + VG-nivå i inlämningarna

*Varje inlämning har en kort reflektion: vad var svårt, vad var lätt, vad lärde du dig, och vilken hjälp fick du av AI?*

---

## Så ser en vecka ut

| Dag | Upplägg |
|---|---|
| **Måndag** | Repetition + nytt ämne: demo, läsning, övningar, genomgång |
| **Tisdag** | Fördjupning, koda vilt, inlämnings-Q&A |
| **Onsdag** | Fördjupning av måndagen |
| **Torsdag** | Som tisdag |
| **Fredag** | Som torsdag, och 10:00–12:00 finns **1:1-handledning** |

Lektionerna är 45 minuter, rast emellan. Övningarna är tänkta att tränas **före** inlämningen.

---

## Innan måndag 2 november

- ✅ Visual Studio (eller annan C#-miljö) och **.NET SDK** fungerar
- ✅ **Git** och GitHub fungerar
- ✅ En SQLite-läsare, till exempel *DB Browser for SQLite*
- 🔁 Kolla igenom tentafrågorna ni missade i kurs 2, särskilt **GDPR**

*Ingen panik. Vi börjar lugnt och bygger på det ni redan kan.*

---

## Frågor?

**Kurs 3: Fördjupad OOP i C#**
Start måndag 2 november 🚀
