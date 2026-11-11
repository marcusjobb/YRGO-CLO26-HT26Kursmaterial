# Tentafrågor — SQL Hardcore

Öva inför tentan. Varje fråga har fyra alternativ — ett rätt, ett lite roligt, och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — UPDATE utan WHERE

Vad händer om du kör `UPDATE Product SET Price = 0` utan WHERE-klausul?

A) Alla rader i Product-tabellen uppdateras — alla produkter kostar nu 0 kr
B) Databasen frågar dig om du är säker innan den kör
C) Bara den senast inlagda raden uppdateras
D) Ingenting händer, databasen kräver alltid WHERE 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Utan WHERE påverkas ALLA rader. Det finns ingen inbyggd varning. Resultatet är omedelbart och svårt att ångra utan backup.
**B)** Databasen frågar inte — den kör direkt. Det är din kod som eventuellt kan lägga till en bekräftelse.
**C)** Det stämmer för varken UPDATE eller DELETE — alla rader påverkas.
**D)** WHERE är valfritt i SQL-standarden, inte obligatoriskt.

</details>

---

## Fråga 2 — INNER JOIN vs LEFT JOIN

Vad är skillnaden mellan INNER JOIN och LEFT JOIN?

C) INNER JOIN returnerar bara rader som matchar i BÅDA tabellerna. LEFT JOIN returnerar alla rader från vänster tabell, även de utan matchning (NULL för höger sida)
A) INNER JOIN är snabbare, LEFT JOIN är långsammare
B) LEFT JOIN kräver att tabellerna har samma antal kolumner
D) INNER JOIN används för siffror, LEFT JOIN för textvärden 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**C)** Rätt. INNER JOIN: bara matchande rader. LEFT JOIN: alla rader från vänster tabell, NULL för saknade matchningar till höger.
**A)** Prestanda beror på index och data, inte på join-typ i sig.
**B)** Tabellerna kan ha hur många kolumner som helst — join-villkoret avgör kopplingen.
**D)** Datatypen avgör inte vilken join du väljer.

</details>

---

## Fråga 3 — GROUP BY och COUNT

Vad returnerar följande fråga?
```sql
SELECT CategoryId, COUNT(*) AS Antal FROM Product GROUP BY CategoryId;
```

B) Antal produkter per kategori
A) Alla produkter sorterade efter CategoryId
C) Den kategori med flest produkter
D) En lista på alla kategorier med deras namn 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. GROUP BY grupperar rader per CategoryId och COUNT(*) räknar antalet rader per grupp.
**A)** ORDER BY sorterar — GROUP BY grupperar och aggregerar.
**C)** Det skulle kräva ORDER BY COUNT(*) DESC LIMIT 1 ovanpå frågan.
**D)** Frågan hämtar inte kategorinamn — CategoryId är en siffra, inte ett namn.

</details>

---

## Fråga 4 — WHERE vs HAVING

Vad är skillnaden mellan WHERE och HAVING?

A) WHERE filtrerar enskilda rader INNAN gruppering. HAVING filtrerar grupper EFTER GROUP BY
B) WHERE används bara med SELECT, HAVING används med UPDATE och DELETE
C) HAVING är en äldre syntax och WHERE ersätter det i moderna databaser
D) WHERE är snabbare och HAVING är ett reservord som sällan används 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. WHERE sållar bort rader innan de grupperas. HAVING sållar bort grupper efter att GROUP BY körts.
**B)** Båda används primärt med SELECT (WHERE även med UPDATE/DELETE, men HAVING är specifikt för aggregerade resultat).
**C)** Båda är aktiva delar av SQL-standarden.
**D)** Prestanda beror på frågan och index, inte på om du använder WHERE eller HAVING.

</details>

---

## Fråga 5 — LIKE och wildcards

Vad returnerar `WHERE Name LIKE 'A%'`?

D) Alla rader där Name börjar med bokstaven A
A) Alla rader där Name slutar med A
B) Alla rader där Name innehåller exakt ett A
C) Alla rader där Name matchar det reguljära uttrycket A* 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: D**

**D)** Rätt. `%` matchar noll eller fler tecken. `'A%'` = börjar med A. `'%A'` = slutar med A. `'%A%'` = innehåller A.
**A)** Det hade been `LIKE '%A'`.
**B)** `_` (understreck) matchar exakt ett tecken, inte `%`.
**C)** LIKE använder inte reguljära uttryck — det är `REGEXP`-operatorn.

</details>

---

## Fråga 6 — Aggregeringsfunktioner

Vad är skillnaden mellan `COUNT(*)` och `COUNT(Email)`?

B) COUNT(*) räknar alla rader. COUNT(Email) räknar bara rader där Email inte är NULL
A) COUNT(*) är snabbare, COUNT(Email) är exaktare
C) De returnerar alltid samma värde
D) COUNT(*) räknar antalet tabeller i databasen 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. `COUNT(*)` räknar alla rader oavsett innehåll. `COUNT(Email)` hoppar över rader där Email är NULL. Det kan ge olika resultat om inte alla rader har ett e-postfält.
**A)** Prestanda kan skilja sig men det är inte den principiella skillnaden.
**C)** Stämmer bara om ingen rad har NULL i Email-kolumnen.
**D)** Det är inte vad COUNT(*) gör.

</details>

---

## Fråga 7 — ORDER BY

Vad gör `ORDER BY Price DESC, Name ASC`?

A) Sorterar efter pris fallande (dyrast först), och vid lika pris sorteras på namn stigande (A–Ö)
C) Sorterar efter namn stigande och ignorerar pris
B) Sorterar slumpmässigt när priset är detsamma
D) Sorterar i omvänd bokstavsordning på alla kolumner 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Första sorteringskolumnen avgör ordningen, andra kolumnen används som tiebreaker när värdet i den första kolumnen är identiskt.
**C)** Pris är primär sorteringskolumn och ignoreras inte.
**B)** SQL sorterar deterministiskt — inte slumpmässigt.
**D)** DESC gäller bara den kolumn det skrivs efter.

</details>

---

## Fråga 8 — INSERT

Vad händer om du glömmer kolumnlistan i INSERT?

```sql
INSERT INTO Product VALUES (29, 100);
```

C) Du måste ange värden i exakt den ordning kolumnerna definierades — i fel ordning hamnar fel värde i fel kolumn
A) Databasen frågar vilka kolumner du menade
B) SQL ignorerar extra kolumner och sätter resten till NULL
D) INSERT kräver alltid kolumnlistan, annars ger databasen syntaxfel 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**C)** Rätt. Utan kolumnlista gäller ordningen exakt som i CREATE TABLE. Fel ordning → fel värde i fel kolumn, utan felmeddelande.
**A)** SQL frågar inte interaktivt.
**B)** SQL hoppar inte över kolumner — det är en strikt sekvens.
**D)** Syntaxen är giltig — det är semantiken som är riskfylld.

</details>

---

## Fråga 9 — Alias

Vad gör `SELECT Name AS Produktnamn, Price AS Pris FROM Product`?

B) Ger kolumnerna tillfälliga namn i resultatmängden — tabellen påverkas inte
A) Byter permanent namn på kolumnerna i tabellen
C) Skapar en ny tabell med kolumnerna Produktnamn och Pris
D) Gör koden lite snyggare men påverkar ingenting 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. AS skapar ett alias som bara gäller i den aktuella frågan. Tabellen och kolumnerna ändras inte.
**A)** För att byta namn permanent används ALTER TABLE RENAME COLUMN.
**C)** SELECT skapar ingen ny tabell — det returnerar en tillfällig resultatmängd.
**D)** Det gör faktiskt en skillnad — aliasnamnet visas i resultatet och kan användas av klienter och rapportverktyg.

</details>

---

## Fråga 10 — DELETE

Vilket påstående om DELETE är korrekt?

A) DELETE utan WHERE raderar alla rader i tabellen permanent
B) DELETE kan ångras med CTRL+Z i de flesta databaser
C) DELETE raderar tabellstrukturen men behåller datan
D) DELETE kräver Root-behörighet i alla databaser 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. DELETE utan WHERE = alla rader borta. Utan en aktiv transaktion med ROLLBACK eller en backup finns det ingen ångra-funktion.
**B)** CTRL+Z fungerar inte i databaser. ROLLBACK fungerar om du har en öppen transaktion.
**C)** DELETE tar bort rader, inte tabellstrukturen. DROP TABLE tar bort strukturen.
**D)** DELETE kräver DELETE-behörighet på tabellen, inte nödvändigtvis root.

</details>
