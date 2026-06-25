# Veckoplan dag-för-dag — Kurs 03: Fördjupad OOP i C#

**Kurs:** 5 veckor | 25 YH-poäng  
**Schema:** Mån fm + Mån em + Tis fm + Tis em + Ons (online) = 5 pass/vecka  
**Examination:** Skriftligt prov + inlämningsuppgifter/projekt

---

## Ämnen med tidsuppskattning

| Ämne | Tid | Nivå | Vecka |
|------|-----|------|-------|
| Repetition klasser + arv (inheritance) | ½ pass | 🟢 | V1 |
| virtual, override, base — arvshierarkier i kod | 1 pass | 🟡 | V1 |
| abstract class vs interface — när vilket | 1 pass | 🟡 | V1 |
| Polymorfism i praktiken | ½ pass | 🟡 | V1 |
| Fildatabas vs minnesdatabas vs databasserver | ½ pass | 🟢 | V2 |
| Entity Framework Core intro (Code First, DbContext) | 1 pass | 🟡 | V2 |
| EF Core CRUD (Add, Find, Update, Remove) | 1 pass | 🟡 | V2 |
| Migrering (Add-Migration, Update-Database) | ½ pass | 🟡 | V2 |
| Design patterns: intro + bakgrund | ½ pass | 🟡 | V3 |
| Creational: Singleton, Factory | 1 pass | 🟡 | V3 |
| Structural: Decorator, Adapter | 1 pass | 🔴 | V3 |
| Behavioral: Observer, Strategy | 1 pass | 🔴 | V3 |
| SOLID — djupare (SRP, OCP, LSP, ISP, DIP) | 1 pass | 🔴 | V4 |
| Refactoring: Extract Method, Rename, Move | 1 pass | 🟡 | V4 |
| Clean code + kodgranskning | ½ pass | 🟡 | V4 |
| Kanban intro | ½ pass | 🟢 | V4 |
| Repetition + handledning | 1 pass | — | V5 |
| Tenta | 1 pass | — | V5 |

**Totalt:** ~14 pass aktiv undervisning + 4 pass handledning/övning

---

## Vecka 1 — Arv, polymorfism, abstraktion

**Mål:** Kan skriva arvshierarkier. Förstår skillnaden abstract class vs interface. Kan använda polymorfism.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | fm | Repetition: klasser, properties, konstruktor. Arv intro: vad ärver en klass? UML för arvshierarkier. Föreläsning: `Animal → Dog, Cat` | Arv |
| Mån | em | `virtual`, `override`, `base`. Live-kod: djur som rör sig på olika sätt men via samma basklassreferens. `sealed` nämns | override/virtual |
| Tis | fm | `abstract class` vs `interface`: när väljer man vad? Regler: abstract class = "är en", interface = "kan göra". Genomgång med diagram | Abstract/interface |
| Tis | em | Polymorfism i praktiken: lista av basklassobjekt, loop anropar override-metoder. Shape → Circle, Rectangle, Triangle | Polymorfism |
| Ons | online | Övningar: bygga en rollspelshierarki (Character → Warrior, Mage). Handledning | Övning |

**Nyckelord denna vecka:** `arv`, `basklass`, `subklass`, `override`, `virtual`, `abstract`, `interface`, `polymorfism`, `sealed`

**Inlämning:** Ingen.

---

## Vecka 2 — Entity Framework Core och databaskopplingar

**Mål:** Kan koppla en C#-applikation till en databas med EF Core. Kan göra migrering och CRUD.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | fm | Tre typer av databas: fildatabas (SQLite/CSV), minnesdatabas (InMemory), databasserver (PostgreSQL/MySQL). När väljer man vad? | Databastyper |
| Mån | em | EF Core intro: Code First, `DbContext`, `DbSet<T>`, modell-klasser. Installera NuGet-paket live | EF Core intro |
| Tis | fm | CRUD med EF Core: `context.Add()`, `context.Find()`, `context.SaveChanges()`, `context.Remove()`. Live-kod: en Todo-app | EF Core CRUD |
| Tis | em | Migrering: `Add-Migration`, `Update-Database`. Förstå vad migreringsfilen gör. Lägg till en property → migrera | Migrering |
| Ons | online | Övningar: bygg en produktkatalog med EF Core mot SQLite. Handledning | Övning |

**Nyckelord denna vecka:** `Entity Framework`, `DbContext`, `DbSet`, `migration`, `Code First`, `ORM`, `LINQ`, `SQLite`

**Inlämning:** Ingen.

---

## Vecka 3 — Design patterns

**Mål:** Förstår vad design patterns är och varför de finns. Kan identifiera och implementera Singleton, Factory, Strategy, Observer.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | fm | Vad är design patterns? Historia (GoF, 1994). Tre kategorier: Creational, Structural, Behavioral. "Patterns är vokabulär" | Intro |
| Mån | em | Creational: **Singleton** (ett objekt i hela systemet), **Factory Method** (skapa objekt utan att ange konkret typ). Live-kod + UML | Singleton, Factory |
| Tis | fm | Structural: **Decorator** (lägg till beteende utan arv), **Adapter** (koppla ihop inkompatibla gränssnitt). Live-kod + UML | Decorator, Adapter |
| Tis | em | Behavioral: **Observer** (event-system), **Strategy** (utbytbar algoritm). Live-kod: SortStrategy, EventBus | Observer, Strategy |
| Ons | online | Övningar: implementera ett valfritt pattern i eget projekt. Handledning | Övning |

**Nyckelord denna vecka:** `design pattern`, `Singleton`, `Factory`, `Decorator`, `Adapter`, `Observer`, `Strategy`, `Creational`, `Structural`, `Behavioral`

**Notering:** Fördjupa inte varje pattern — visa en tydlig use-case per pattern. Kvalitet > kvantitet.

**Inlämning:** Ingen.

---

## Vecka 4 — SOLID + refactoring + kodgranskning

**Mål:** Kan identifiera SOLID-brott i kod. Kan refaktorera ful kod till ren. Förstår vad en kodgranskning tittar på.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | fm | SOLID djupare: SRP (en anledning att ändras), OCP (öppen för utvidgning, stängd för modifiering), LSP (subklass ska funka som basklass), ISP (smala interface), DIP (beroend mot abstraktioner) | SOLID |
| Mån | em | Refactoring live: ta ful "God Object"-kod → extrahera metoder, dela upp ansvar, döp om variabler. "Rescue Mission" | Refactoring |
| Tis | fm | Clean code: namngivning, metodlängd, kommentarer, döda kod. Kodgranskning: vad tittar vi på? Parprogrammering: granska varandras kod | Clean code + code review |
| Tis | em | Kanban-intro: To Do / In Progress / Done. GitHub Projects som Kanban board. **Inlämningen presenteras** | Kanban + inlämning |
| Ons | online | Handledning: planera inlämningen, ställ frågor | Handledning |

**Nyckelord denna vecka:** `SOLID`, `SRP`, `OCP`, `LSP`, `ISP`, `DIP`, `refactoring`, `kodgranskning`, `Kanban`, `teknisk skuld`

**Inlämning:** Presenteras denna vecka. Deadline sista dagen av kurs 03.

---

## Vecka 5 — Projekt + tenta

**Mål:** Tentan visar förståelse för arv, patterns, SOLID och EF Core. Projektet visar tillämpning.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | fm | Repetition: arv, abstract/interface, EF Core, design patterns, SOLID | Repetition |
| Mån | em | Handledning: inlämning och projektfrågor | Handledning |
| Tis | fm | **TENTA** | Tenta |
| Tis | em | Tentaigenomgång. Handledning: inlämning | Genomgång |
| Ons | online | Sista handledning | Handledning |

**Tenta täcker:** Arv, abstract class vs interface, polymorfism, EF Core (CRUD + migration), designmönster (identifiera + förklara), SOLID-principerna

---

## Sammanfattning: svårighetsgrad och tid per ämne

```
Arv + override/virtual        🟡 1 pass  — bekant från kurs 01, bygg vidare
Abstract class vs interface   🟡 1 pass  — "är en" vs "kan göra" tar tid att sätta
Polymorfism                   🟡 ½ pass  — klickar när de kört koden
EF Core intro                 🟡 1 pass  — ORM-tanken är ny, ge tid
EF Core CRUD + migrering      🟡 1½ pass — migrering är magisk, visa steg-för-steg
Design patterns (intro)       🟡 ½ pass  — fokus på syfte, inte syntax
Singleton + Factory           🟡 1 pass  — konkret, ge bra use-cases
Decorator + Adapter           🔴 1 pass  — abstrakt, behöver visuellt
Observer + Strategy           🔴 1 pass  — Observer är svår, Strategy är lättare
SOLID                         🔴 1 pass  — välj ett brott per princip, visa i riktig kod
Refactoring                   🟡 1 pass  — live-demo med ful kod = störst inlärning
Kodgranskning                 🟡 ½ pass  — par-övning funkar bra
Kanban                        🟢 ½ pass  — enkelt men viktigt
```

---

## Nästa: Kurs 04 — Test och kvalitet (TDD, xUnit, SCRUM, CI/CD)
