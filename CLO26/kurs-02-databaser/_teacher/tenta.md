# Tenta — Databashantering och design

**Kurs:** Kurs 2 — Databashantering och design  
**Program:** CLO26  
**Datum:** tisdag 27 oktober 2026  
**Tid:** 09:00–12:00 (3 timmar)  
**Lärare:** Marcus Ackre Medina

---

**Hjälpmedel:** Inga hjälpmedel tillåtna.

**Poängfördelning:**

| Del | Innehåll | Poäng |
|-----|----------|-------|
| A | Flervalsfrågor | 16p (8 × 2p) |
| B | Kortsvarsfrågror | 20p (5 × 4p) |
| C | SQL-uppgifter | 24p (2 × 12p) |
| **Totalt** | | **60p** |

**Betygsgränser:**

- G (Godkänd): 36 poäng (60 %)
- VG (Väl Godkänd): 48 poäng (80 %)

---

## Del A — Flervalsfrågor

*Markera ett svarsalternativ per fråga. Rätt svar ger 2 poäng.*

---

**Fråga 1**

Vad är en primärnyckel (primary key)?

a) Ett lösenord som krävs för att ansluta till databasen  
b) En kolumn (eller kombination av kolumner) som unikt identifierar varje rad i en tabell  
c) En kolumn som alltid refererar till en annan tabell  
d) En kolumn vars värde alltid genereras automatiskt

---

**Fråga 2**

Vilket SQL-kommando används för att lägga till nya rader i en tabell?

a) `ADD ROW INTO`  
b) `UPDATE`  
c) `INSERT INTO`  
d) `CREATE TABLE`

---

**Fråga 3**

Vad innebär tredje normalformen (3NF)?

a) Tabellen har exakt tre kolumner  
b) Alla icke-nyckelfält beror direkt på primärnyckeln — inga transitiva beroenden  
c) Data är delad i tre separata databaser  
d) Tabellen har tre primärnycklar

---

**Fråga 4**

Vad returnerar ett `INNER JOIN`?

a) Alla rader från den vänstra tabellen, även om det saknas matchning i den högra  
b) Alla möjliga kombinationer av rader från båda tabellerna  
c) Bara de rader där det finns en matchning i båda tabellerna  
d) Alla rader från båda tabellerna utan hänsyn till matchning

---

**Fråga 5**

Vad är en dokumentdatabas?

a) En databas som lagrar Word-dokument och PDF-filer  
b) En NoSQL-databas som lagrar data som flexibla, JSON-liknande dokument utan fast schema  
c) En databas som kräver tabeller och kolumner med strikt schema  
d) En databas som enbart lagrar textfiler

---

**Fråga 6**

Vilket av följande skyddar effektivast mot SQL-injektion?

a) Att dölja felmeddelanden för användaren  
b) Att använda parameteriserade frågor (prepared statements) i stället för strängkonkatenering  
c) Att använda `LOCK TABLE` innan varje fråga  
d) Att kryptera hela databasen

---

**Fråga 7**

Vad visar ett ER-diagram (Entity-Relationship-diagram)?

a) Hur ett programs källkod är organiserad  
b) Vilka SQL-frågor som körs mot databasen  
c) Databasens entiteter, deras attribut och relationerna mellan dem  
d) En tidslinje för databastransaktioner

---

**Fråga 8**

Vad är en utländsk nyckel (foreign key)?

a) En krypteringsnyckel som skyddar databasen  
b) En kolumn vars värde alltid genereras automatiskt  
c) En kolumn som unikt identifierar varje rad i sin tabell  
d) En kolumn som refererar till primärnyckeln i en annan tabell

---

## Del B — Kortsvarsfrågror

*Svara i löpande text. Varje fråga ger upp till 4 poäng.*

---

**Fråga 9**

Förklara skillnaden mellan en relationsdatabas och en dokumentdatabas.  
Ge ett konkret exempel på ett scenario där du skulle välja relationsdatabas, och ett annat scenario där dokumentdatabas passar bättre.

*(4 poäng)*

---

**Fråga 10**

Förklara vad normalisering av databaser innebär och varför man gör det.  
Vad är den viktigaste fördelen med en normaliserad databas jämfört med en onormaliserad?

*(4 poäng)*

---

**Fråga 11**

Vad är SQL-injektion?  
Beskriv ett konkret exempel på hur en attack kan gå till och hur man skyddar sin applikation.

*(4 poäng)*

---

**Fråga 12**

Förklara vad backup innebär i ett databassammanhang.  
Vad bör en bra backupstrategi innehålla, och varför räcker det inte att bara ha en kopia?

*(4 poäng)*

---

**Fråga 13**

Vad är syftet med UML vid databasdesign?  
Förklara vad ett ER-diagram visar och nämn minst tre element som ett ER-diagram brukar innehålla.

*(4 poäng)*

---

## Del C — SQL-uppgifter

*Skriv fullständiga SQL-frågor. Syntaxen ska vara korrekt.*

---

### Scenariot

Du arbetar med en databas för ett litet bibliotek. Tabellerna ser ut så här:

```sql
CREATE TABLE forfatter (
    id   INT PRIMARY KEY,
    namn VARCHAR(100) NOT NULL,
    land VARCHAR(50)
);

CREATE TABLE bok (
    id             INT PRIMARY KEY,
    titel          VARCHAR(200) NOT NULL,
    forfatter_id   INT REFERENCES forfatter(id),
    genre          VARCHAR(50),
    publiceringsaar INT
);

CREATE TABLE utlaning (
    id          INT PRIMARY KEY,
    bok_id      INT REFERENCES bok(id),
    lanarnamn   VARCHAR(100),
    utlanat     DATE NOT NULL,
    aterlamnat  DATE   -- NULL om boken fortfarande är utlånad
);
```

Exempeldata (för att förstå strukturen):

| forfatter | | |
|-----------|--|--|
| 1 | Stieg Larsson | Sverige |
| 2 | Jo Nesbø | Norge |
| 3 | Astrid Lindgren | Sverige |

| bok | | | | |
|-----|--|--|--|--|
| 1 | Män som hatar kvinnor | 1 | Kriminalroman | 2005 |
| 2 | Harry Hole: Kniven | 2 | Kriminalroman | 2022 |
| 3 | Emil i Lönneberga | 3 | Barnbok | 1963 |

| utlaning | | | | |
|----------|--|--|--|--|
| 1 | 1 | Sara Nilsson | 2026-10-01 | NULL |
| 2 | 2 | Kalle Berg | 2026-09-15 | 2026-10-10 |

---

**Uppgift C1** *(12 poäng)*

Skriv en SQL-fråga som visar, för varje **genre**, hur många böcker som finns och vilket **genomsnittligt publiceringsår** genren har.

Frågan ska:
- Visa kolumnerna `genre`, `antal_bocker` och `snitt_ar`
- Bara visa genrer som har **minst 2 böcker**
- Sorteras på `antal_bocker` i fallande ordning (flest böcker först)

Förväntad resultatstruktur (exempelvärden):

```
genre           | antal_bocker | snitt_ar
Kriminalroman   |      6       |  2008.3
Barnbok         |      3       |  1978.0
```

---

**Uppgift C2** *(12 poäng)*

Skriv en SQL-fråga som hämtar titel, författarens namn och land för alla böcker som **just nu är utlånade** (det vill säga `aterlamnat` är NULL) och som skrevs av en **svensk** författare (land = 'Sverige').

Frågan ska:
- Hämta data från minst två tabeller med JOIN
- Filtrera på att boken är utlånad nu
- Filtrera på att författaren är från Sverige
- Sorteras på titel i bokstavsordning

Förväntad resultatstruktur (exempelvärden):

```
titel                  | namn              | land
Män som hatar kvinnor  | Stieg Larsson     | Sverige
```

---

*Lycka till.*
