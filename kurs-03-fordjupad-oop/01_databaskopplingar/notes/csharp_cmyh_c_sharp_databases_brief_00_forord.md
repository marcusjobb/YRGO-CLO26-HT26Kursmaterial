# Förord

🟢


## Välkommen till Databashäftet!

Det här häftet är din guide genom databasernas värld – från enkla filbaserade lösningar som SQLite till kraftfulla serverdatabaser som MySQL och SQL Server, och slutligen in i NoSQL-världen med MongoDB.

## Syfte och mål

Efter att ha arbetat igenom det här häftet kommer du att kunna:

- **Välja rätt databas** för olika projekt och situationer
- **Installera och konfigurera** fyra olika databastekniker
- **Koppla C#-applikationer** till alla dessa databaser
- **Använda Entity Framework Core** där det är lämpligt
- **Förstå skillnaden** mellan SQL och NoSQL
- **Hantera JSON-data** i både MongoDB och C#
- **Använda Docker** för databascontainrar
- **Göra CRUD-operationer** i alla databaser
- **Ta backup och återställa** data

## SQL vs NoSQL – den korta versionen

### SQL (Relationsdatabaser)
**Exempel:** SQLite, MySQL, SQL Server, PostgreSQL

**Passar för:**
- Strukturerad data med fasta tabeller
- Data med tydliga relationer (kunder → ordrar → produkter)
- När du behöver ACID-garantier (transactions)
- När du behöver komplex query-logik (JOIN, GROUP BY, etc.)

**Tänk dig:** Ett Excel-ark med flera flikar som är länkade till varandra.

### NoSQL (Dokumentdatabaser)
**Exempel:** MongoDB, Cosmos DB, CouchDB

**Passar för:**
- Flexibel data som kan ändra struktur över tid
- JSON-liknande dokument
- När du behöver hög skalbarhet horisontellt
- När relationer inte är huvudfokus

**Tänk dig:** En mapp full med JSON-filer där varje fil kan ha olika struktur.

## Så här använder du häftet

### 📖 Läs kapitlen i ordning
Kapitlen bygger på varandra, från enklast (SQLite) till mest avancerat (MongoDB).

### 💻 Följ exemplen
Varje kapitel har kodexempel – skriv av dem och testa själv!

### 🔧 Installera verktygen
Varje kapitel börjar med installation av nödvändiga verktyg.

### ✅ Använd TL;DR-sektionerna
Varje kapitel avslutas med en snabb sammanfattning – perfekt för repetition.

### 🐛 Konsultera felsökningssektionerna
När något inte fungerar, kolla "Vanliga fel och lösningar".

### 📚 Använd bilagorna
I slutet finns referensmaterial du kan slå upp när du behöver.

## Vad du behöver ha installerat från början

- **Visual Studio 2022** eller **Visual Studio Code** med C# Dev Kit
- **.NET 8 SDK** (eller senare)
- **Docker Desktop** (för MySQL och SQL Server i containers)
- En textredigerare (VS Code räcker utmärkt)

Övriga verktyg installerar vi kapitel för kapitel!

## Häftets struktur

```
Kapitel 1: SQLite (enklast, ingen server)
    ↓
Kapitel 2: MySQL (klassisk server + Docker)
    ↓
Kapitel 3: LocalDB/SQL Server (Microsoft-stacken)
    ↓
Kapitel 4: SQL Server i Docker (avancerat)
    ↓
Kapitel 5: MongoDB (NoSQL-världen)
    ↓
Kapitel 6: Jämförelse och reflektion
```

## Tips för bästa lärande

1. **Skriv kod själv** – kopiera inte bara, förstå vad varje rad gör
2. **Experimentera** – ändra värden, bryt saker, fixa dem igen
3. **Bygg småprojekt** – använd varje databas i ett litet projekt
4. **Jämför** – se skillnaderna mellan databaserna
5. **Fråga** – om något är oklart, fråga lärare eller klasskamrater

## Notering om Entity Framework

I vissa kapitel använder vi **Entity Framework Core** (EF Core), i andra inte. Här är varför:

- **SQLite, MySQL, SQL Server**: EF Core fungerar utmärkt
- **MongoDB**: EF Core stöds inte officiellt, vi använder MongoDB.Driver istället

Detta ger dig erfarenhet av båda arbetssätten!

## Versioner och datum

- **Häftet skapad**: 2025
- **Målversion**: .NET 8+
- **Entity Framework Core**: 8.0+
- **MongoDB.Driver**: 2.24+

Om du läser detta längre fram kan versionsnummer ha ändrats, men koncepten är desamma.

---

**Lycka till på din resa genom databasernas värld!** 🚀

*Låt oss börja med det enklaste – SQLite!*

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
