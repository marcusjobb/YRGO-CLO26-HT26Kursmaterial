# Tentafacit — Databashantering och design

**Kurs:** Kurs 2 — Databashantering och design  
**Datum:** 27 oktober 2026

> **Lärarnotering:** Facit används vid rättning. Kortsvarsfrågorna har en "godkänd kärna" — det är den minimala insikten som krävs för G-poäng. Svar som visar djupare förståelse eller konkreta exempel ger ytterligare poäng upp till max.

---

## Del A — Flervalsfrågor (16 poäng)

| Fråga | Rätt svar | Motivering |
|-------|-----------|------------|
| 1 | **b** | Primary key = unik identifierare per rad |
| 2 | **c** | `INSERT INTO` lägger till rader |
| 3 | **b** | 3NF: icke-nyckelfält beror bara direkt på primärnyckeln |
| 4 | **c** | INNER JOIN returnerar bara matchande rader |
| 5 | **b** | Dokumentdatabas = NoSQL, JSON-liknande, utan fast schema |
| 6 | **b** | Parameteriserade frågor förhindrar SQL-injektion |
| 7 | **c** | ER-diagram visar entiteter, attribut och relationer |
| 8 | **d** | Foreign key refererar till primärnyckeln i annan tabell |

*2 poäng per rätt svar. 0 poäng vid fel eller blankt.*

---

## Del B — Kortsvarsfrågror (20 poäng)

### Fråga 9 — Relationsdatabas vs dokumentdatabas (4p)

**Godkänd kärna (2p):**
- Relationsdatabas = tabeller med kolumner och rader, fast schema, SQL, relationer via nycklar
- Dokumentdatabas = dokument (JSON-liknande), flexibelt schema, NoSQL, inga tvingade relationer

**Full poäng (4p) kräver dessutom:**
- Konkret scenario för relationsdatabas: t.ex. bokföringssystem, ordersystem, HR-system — där data är strukturerad och relationer viktiga
- Konkret scenario för dokumentdatabas: t.ex. produktkatalog med varierande attribut, loggdata, innehållshantering — där schemat varierar per post

**Exempel på bra svar:**
> Relationsdatabasen passar ett bokningssystem för ett hotell — varje bokning, gäst och rum har fasta kolumner och tydliga kopplingar. Dokumentdatabasen passar bättre för en produktkatalog i en webbutik, där en t-shirt har storlekar och en TV har skärmupplösning — olika produkter har olika attribut.

---

### Fråga 10 — Normalisering (4p)

**Godkänd kärna (2p):**
- Normalisering = processen att strukturera en databas för att minska redundans och säkerställa dataintegritet
- Man normaliserar för att undvika att samma data lagras på flera ställen (vilket leder till inkonsekvenser)

**Full poäng (4p) kräver dessutom:**
- Den viktigaste fördelen: om data lagras på ett ställe uppdateras det på ett ställe — inga inkonsistenser
- Alternativt: det är lättare att underhålla, lättare att ändra schema, bättre datakvalitet

**Exempel på bra svar:**
> Normalisering innebär att man delar upp data i separata tabeller så att varje uppgift lagras på ett enda ställe. Om en kunds adress lagras i kundtabellen (inte i varje orderrad) behöver man bara ändra på ett ställe när kunden flyttar. Den viktigaste fördelen är att man slipper inkonsistenser — om samma data finns på tio ställen räcker det att uppdatera nio och den tionde är fel.

---

### Fråga 11 — SQL-injektion (4p)

**Godkänd kärna (2p):**
- SQL-injektion = angriparen skickar SQL-kod som en del av användarinput, och den körs av databasen
- Skydd: parameteriserade frågor / prepared statements

**Full poäng (4p) kräver dessutom:**
- Konkret attackexempel: t.ex. att en inloggningsfråga med `' OR '1'='1` bypass:ar autentisering
- Mer om skyddsmekanismer: aldrig konkatenera användarinput direkt i SQL-strängar

**Exempel på bra svar:**
> SQL-injektion innebär att en angripare skriver SQL-kod i ett formulärfält. Om ett inloggningsformulär bygger frågan `"SELECT * FROM users WHERE name = '" + input + "'"` kan angriparen skriva `admin' OR '1'='1` och logga in utan lösenord. Skyddet är att använda parameteriserade frågor: `@name` som parameter, inte som del av SQL-strängen. Databasen hanterar parametern som data, inte som kod.

---

### Fråga 12 — Backup (4p)

**Godkänd kärna (2p):**
- Backup = en kopia av databasen som kan återställas om data försvinner eller skadas
- Man behöver backup för att skydda mot hårdvarufel, mänskliga misstag, ransomware

**Full poäng (4p) kräver dessutom:**
- Varför en enda kopia inte räcker: om backup-disken och produktionsdisken sitter i samma server kan båda gå sönder simultaneously
- Bra backupstrategi: regelbundet schema, flera generationer (inte bara senaste), offsite-kopia, testad återställning

**Exempel på bra svar:**
> Backup är en sparad kopia av databasen som kan återställas om något går fel. Man kan förlora data av många skäl: disk som kraschar, en administratör som råkar köra DROP TABLE, ransomware. En bra strategi har regelbundna säkerhetskopior (t.ex. dagligen), sparar flera versioner bakåt (inte bara den senaste), och lagrar kopior på en annan plats eller i molnet. Det räcker inte med en kopia på samma disk — om disken går sönder är backup:en också borta.

---

### Fråga 13 — UML och ER-diagram (4p)

**Godkänd kärna (2p):**
- UML används för att visualisera system — vid databasdesign används framförallt ER-diagram
- ER-diagram visar entiteter (tabeller), deras attribut (kolumner) och relationer mellan dem

**Full poäng (4p) kräver dessutom:**
- Minst tre element: entiteter, attribut, relationer (och gärna kardinalitet: 1-till-många, etc.)
- Varför det används: planering innan man skriver SQL, kommunikation i team

**Exempel på bra svar:**
> UML (Unified Modeling Language) är ett standardiserat sätt att visualisera system. Vid databasdesign används ER-diagram för att planera strukturen innan man skriver en rad SQL. Diagrammet innehåller: entiteter (de saker man lagrar data om, t.ex. Kund eller Order), attribut (kolumnerna, t.ex. namn och email) och relationer (hur entiteterna är kopplade, t.ex. en kund kan ha många orders). Kardinaliteten (1:1, 1:N, M:N) visar hur många instanser som kan kopplas ihop.

---

## Del C — SQL-uppgifter (24 poäng)

### Uppgift C1 — Genre-statistik (12 poäng)

**Rätt svar:**

```sql
SELECT
    b.genre,
    COUNT(b.id)         AS antal_bocker,
    AVG(b.publiceringsaar) AS snitt_ar
FROM bok b
GROUP BY b.genre
HAVING COUNT(b.id) >= 2
ORDER BY antal_bocker DESC;
```

**Alternativt acceptabelt:**
- `COUNT(*)` i stället för `COUNT(b.id)` — godkänt
- `AVG(publiceringsaar)` utan alias `b.` — godkänt om tydligt
- `HAVING COUNT(*) >= 2` — godkänt

**Poänguppdelning (12p):**

| Element | Poäng |
|---------|-------|
| Rätt SELECT med `genre`, COUNT och AVG (inklusive alias) | 3p |
| Rätt FROM (tabellen `bok`) | 1p |
| Rätt GROUP BY på `genre` | 3p |
| Rätt HAVING med COUNT >= 2 | 3p |
| Rätt ORDER BY fallande | 2p |

**Vanliga misstag:**
- `WHERE COUNT(b.id) >= 2` — fel, WHERE körs före aggregering; ska vara HAVING
- ORDER BY utan `DESC` — minus 1p
- Glömt GROUP BY — ger 0p på GROUP BY-raden men övriga element kan ge poäng

---

### Uppgift C2 — Utlånade böcker av svenska författare (12 poäng)

**Rätt svar:**

```sql
SELECT
    b.titel,
    f.namn,
    f.land
FROM bok b
JOIN forfatter f ON b.forfatter_id = f.id
JOIN utlaning u  ON u.bok_id = b.id
WHERE u.aterlamnat IS NULL
  AND f.land = 'Sverige'
ORDER BY b.titel;
```

**Alternativt acceptabelt:**
- `INNER JOIN` i stället för `JOIN` — identiskt, godkänt
- `f.land = 'sweden'` eller `f.land = 'SE'` — ge 1p av 2p om logiken är rätt men värdet tveksamt
- Tabellordning spelar ingen roll (bok–forfatter–utlaning eller annan ordning) — godkänt om joins är korrekta

**Poänguppdelning (12p):**

| Element | Poäng |
|---------|-------|
| SELECT med rätt tre kolumner | 2p |
| FROM bok + JOIN forfatter med rätt ON-villkor | 3p |
| JOIN utlaning med rätt ON-villkor | 3p |
| WHERE aterlamnat IS NULL | 2p |
| WHERE f.land = 'Sverige' | 1p |
| ORDER BY titel | 1p |

**Vanliga misstag:**
- `WHERE aterlamnat = NULL` — fel syntax (NULL jämförs med IS NULL, inte = NULL) — minus 2p
- Glömt JOIN utlaning men rätt logik i övrigt — minus 3p
- `ORDER BY` på fel kolumn — minus 1p

---

## Sammanräkning

Skriv in poäng per del:

| Del | Max | Faktisk poäng |
|-----|-----|---------------|
| A (flerval) | 16p | |
| B (kortsvar) | 20p | |
| C (SQL) | 24p | |
| **Totalt** | **60p** | |

**Betyg:**

| Poäng | Betyg |
|-------|-------|
| 0–35 | Underkänd |
| 36–47 | G |
| 48–60 | VG |
