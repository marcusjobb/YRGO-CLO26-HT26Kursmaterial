---

title: 3. Introduktion till SQLite
author: Marcus Ackre Medina
type: lecture
topic: databaser
difficulty: 1
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/2_db/lectures/01_introduction/3_sqlite_intro.md"
description: "SQLite är en lättviktig, filbaserad relationsdatabas som kräver minimal konfiguration och administration. Den är inbäddningsbar, vilket betyder att den kan integreras direkt i applikationer utan behov"
tags: ["databaser", "installation", "rider", "sql", "sqlite", "ssh", "till", "verktyg", "visual-studio"]
week_fit: []
---

# 3. Introduktion till SQLite

🟢


## Föreläsningsmaterial (20 minuter)

### 3.1 Vad är SQLite?

SQLite är en lättviktig, filbaserad relationsdatabas som kräver minimal konfiguration och administration. Den är inbäddningsbar, vilket betyder att den kan integreras direkt i applikationer utan behov av en separat serverprocess.

#### 3.1.1 Huvudegenskaper

1. **Serverless:** Kräver ingen separat serverprocess eller konfiguration.
2. **Självständig:** Hela databasen lagras i en enda fil.
3. **Kompakt:** Mycket liten kodbas, perfekt för inbäddade system och mobila enheter.
4. **Pålitlig:** Fullt ACID-kompatibel, vilket garanterar dataintegritet.
5. **Plattformsoberoende:** Fungerar på olika operativsystem och enheter.

### 3.2 Fördelar med SQLite

<div class="mermaid" style="zoom: 1.4;">

```mermaid
graph TD
    A[SQLite] --> B[Enkel att använda]
    A --> C[Kräver ingen konfiguration]
    A --> D[Portabel - en enda fil]
    A --> E[Snabb för de flesta operationer]
    A --> F[Ingen separat serverprocess]
    A --> G[Öppen källkod]
```

</div>

### 3.3 Användningsområden för SQLite

1. **Mobila applikationer:** Lokal datalagring i Android och iOS-appar.
2. **Inbäddade system:** Datalagring i IoT-enheter och andra inbäddade system.
3. **Desktopapplikationer:** Lokal datalagring för mindre program.
4. **Webbläsare:** Firefox använder SQLite för att lagra bokmärken och historik.
5. **Prototyping:** Snabb utveckling och testning av databasdriven funktionalitet.

### 3.4 Användning av SQLite i Visual Studio och Rider, eller DB Browser för SQLite

För de som använder **Visual Studio**, finns det en inbyggd SQLite-integrering via **System.Data.SQLite**. Detta innebär att du kan skapa och hantera SQLite-databaser direkt från IDE:n utan att behöva några externa verktyg.

- **Installation:**

  - Installera **System.Data.SQLite** via NuGet-paketet i Visual Studio.
  - Använd pakethanteraren för att lägga till referenser:

  ```bash
  Install-Package System.Data.SQLite
  ```

- **Skapa en ny databas i Visual Studio:**
  1. Skapa en ny fil med ändelsen `.sqlite`.
  2. Använd verktygslådan i Visual Studio för att lägga till och redigera tabeller.
  3. Använd Entity Framework om du vill ha ett ORM-lager för att hantera din SQLite-databas.

- **DB Browser för SQLite:**
  - Ett enkelt verktyg för att skapa, redigera och visa SQLite-databaser.
  - Ladda ner från [SQLite Browser](https://sqlitebrowser.org/).
  - Användbart för att snabbt visa och redigera SQLite-databaser utan att behöva öppna Visual Studio.
  - Skapa en ny databas, lägg till tabeller och data, och kör SQL-frågor direkt från gränssnittet. (Marcus favvo)

För de som använder **Rider** eller andra IDE:er, kan **DataGrip** eller andra externa verktyg som **DB Browser för SQLite** vara användbara.

### 3.5 Installation och användning av DataGrip för SQLite (för Rider-användare)

1. Installera DataGrip via JetBrains Toolbox eller ladda ner från JetBrains webbplats.
2. Skapa en ny databas genom att välja **New Database** och välja **SQLite**.
3. Ange databasens namn och välj platsen för att skapa filen.
4. Lägg till tabeller och data direkt från gränssnittet.

#### 3.5.1 Exempel

Skapa tabellen "Books" med följande kolumner:

```sql
CREATE TABLE Books (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Author TEXT NOT NULL,
    PublicationYear INTEGER NOT NULL
);
```

---

## Övningsuppgifter

### Övning 1: Skapa en SQLite-databas i Visual Studio (för Visual Studio-användare)

1. Skapa en ny SQLite-fil direkt i Visual Studio.
2. Använd det inbyggda verktyget för att skapa en tabell "Böcker" med följande kolumner:

   - ID (Integer, Primary Key)
   - Titel (Text)
   - Författare (Text)
   - Utgivningsår (Integer)

3. Lägg till några böcker genom att använda verktyget eller köra SQL-frågor.

### Övning 2: Använd DataGrip (för Rider-användare)

1. Skapa en SQLite-databas i DataGrip med samma tabellstruktur som ovan.
2. Lägg till några böcker via gränssnittet eller SQL-fliken.

### Övning 3: Använd DB Browser för SQLite (för alla)

1. Skapa en ny SQLite-databas med tabellen "Kunder" och lägg till några kunder.
   - Kolumner: 
     - ID (Integer, Primary Key), 
     - Namn (Text), 
     - E-post (Text), 
     - Telefon (Text).
2. Utforska verktyget och bekanta dig med dess funktioner för att skapa och redigera databaser.

### Avslutande reflektion (5 minuter)

- Diskutera fördelarna med att använda SQLite direkt i Visual Studio eller med ett externt verktyg som DataGrip.
- Fundera över när SQLite är lämpligt att använda jämfört med andra databashanterare som MySQL eller PostgreSQL.

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
