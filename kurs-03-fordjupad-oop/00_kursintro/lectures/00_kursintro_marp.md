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
| **1** · v45 | 2–6 nov | Klasser, arv, abstract och interface, JSON |
| **2** · v46 | 9–13 nov | EF Core, MongoDB och designmönster + **Inlämning 1** |
| **3** · v47 | 16–20 nov | Refactoring, clean code och **Kanban** |
| **4** · v48 | 23–27 nov | 🔥 **Hellweek**: C# hidden gems + **Inlämning 2 delas ut** |
| **5** · v49 | 30 nov–4 dec | **Tenta** + grupppresentationer |

---

## Vecka 1 · Trygg start: klasser, arv och interface

*2–6 november · ingen inlämning*

- Klasser och objekt, arv med `virtual` och `override`
- `abstract` eller `interface`: när väljer man vilket?
- Filtrera typer med `OfType`
- 🍃 **Onsdag:** spara ett objekt som JSON

*Repetition först, sedan nytt. Läxorna följer hjältens äventyr.*

---

## Vecka 2 · EF Core, MongoDB och designmönster

*9–13 november · **Inlämning 1 presenteras måndag 9 nov, deadline söndag 22 nov 23:59***

- **Entity Framework Core**: spara, hämta och koppla ihop två tabeller
- Migrationer för databasen
- 🍃 **Måndag em:** MongoDB i lätt version
- Designmönster: **Strategy**, **Factory** och **Observer**, plus **Builder**
- 📐 **UML-klassdiagram** ingår i inlämningen

*Ni har redan använt flera mönster utan att veta det.*

---

## Vecka 3 · Refactoring, clean code och Kanban

*16–20 november · ingen inlämning*

- **Code smells**: lång metod, God Class, magiska tal, duplicerad kod
- **SOLID**, DRY och KISS
- Refaktoreringstekniker: *Extract Method*, *Rename*
- **Kodgranskning** med checklista, i par
- 🔎 **Onsdag:** **Regex**: mönster i text, och när det blir oläsligt
- 📋 **Kanban**: Backlog, Working, Test och Done. Planera med AI och skapa kort i Trello

---

## Vecka 4 · 🔥 Hellweek

*23–27 november · **Inlämning 2 (grupp) delas ut måndag 23 nov** · **inte tentastoff***

C#-features som dyker upp överallt i riktig kod (och i AI-genererad kod):

`dynamic` · pattern matching · tuples · indexers · `?.` `??` `??=` · extension methods · records · async/await

> *"Det här kommer inte på tentan. Men nästa gång du ser något av det här vet du vad det är."*

Livekod, demos och "wow, kan man göra så?"

---

## Vecka 5 · Tenta och presentationer

*30 november–4 december*

- **Måndag 30 november:** repetition av databaser och designmönster
- **Onsdag 2 december, 09:00–12:00: tentan**
- Grupppresentationer efter tentan
- **Inlämning 2: deadline söndag 6 december 23:59.** Sikta på att vara klar fredag 4 december, så finns tid kvar för buggar.

---

## Så blir du godkänd

| Del | Vad |
|---|---|
| **Inlämning 1** | Hotel Grandioso, individuell, EF Core och UML. Deadline 22 nov |
| **Inlämning 2** | Grupparbete, med UML. Deadline 6 dec |
| **Tenta** | Skriftlig, onsdag 2 dec |
| **Grupparbete** | Planering på Kanban-board + presentation |

- **G:** alla inlämningar godkända + godkänd tenta
- **VG:** G-kraven + VG-nivå i inlämningarna

*Varje inlämning har en kort reflektion: vad var svårt, vad var lätt, vad lärde du dig, och vilken hjälp fick du av AI?*

---

## Så ser en vecka ut

| Dag | Upplägg |
|---|---|
| **Måndag** | Lektion 09–15, lunch 12–13 |
| **Tisdag** | Eget arbete. Ingen undervisning |
| **Onsdag** | Lektion 09–15, lunch 12–13 |
| **Torsdag** | Eget arbete. Ingen undervisning |
| **Fredag** | Handledning 09–11 (distans) |

Varje lektionstimme är 45 minuter, med 15 minuters rast. Övningarna är tänkta att tränas **före** inlämningen.

---|---|
| **Måndag** | Repetition + nytt ämne: demo, läsning, övningar, genomgång |
| **Tisdag** | Fördjupning, koda vilt, inlämnings-Q&A |
| **Onsdag** | Fördjupning av måndagen |
| **Torsdag** | Som tisdag |
| **Fredag** | Som torsdag, och 10:00–12:00 finns **1:1-handledning** |

Övningarna är tänkta att tränas **före** inlämningen.

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
