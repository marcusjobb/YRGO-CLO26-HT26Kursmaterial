# 5. Prepared Statements, Views och Indexering

🟢


## **Övergripande frågeställning**

**Hur kan vi använda Prepared Statements, Views och Indexering för att förbättra säkerhet, prestanda och dataåtkomst i MySQL-databaser?**

---

## **Prepared Statements**

### **Vad är Prepared Statements?**

Prepared Statements är fördefinierade SQL-frågor med platshållare som kompileras och optimeras av databasen innan exekvering. Detta förbättrar både prestanda och säkerhet genom att separera SQL-koden från indata.

### **Fördelar med Prepared Statements**

1. **Säkerhet:** Förhindrar SQL-injektion genom att parametrar hanteras separat från SQL-logik.
2. **Prestanda:** Snabbare exekvering för återkommande frågor eftersom frågan bara kompileras en gång.
3. **Läsbarhet:** Gör koden renare och enklare att underhålla.

### **Syntax i MySQL**

```sql
PREPARE stmt FROM 'SELECT * FROM users WHERE id = ?';
SET @id = 1;
EXECUTE stmt USING @id;
DEALLOCATE PREPARE stmt;
```

### **Användning i Programmeringsspråk**

**C# med MySQL Connector:**

```csharp
MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE id = @id", connection);
cmd.Parameters.AddWithValue("@id", userId);
MySqlDataReader reader = cmd.ExecuteReader();
```

**Python med MySQL Connector:**

```python
cursor = connection.cursor(prepared=True)
query = "SELECT * FROM users WHERE id = %s"
cursor.execute(query, (user_id,))
results = cursor.fetchall()
```

---

## **Views**

### **Vad är en View?**

En View är en virtuell tabell som genereras av en SQL-fråga. Views lagrar inte data fysiskt utan presenterar data från en eller flera underliggande tabeller.

### **Fördelar med Views**

1. **Förenkla komplexa frågor:** Inkapslar JOIN-operationer och subqueries.
2. **Dataskydd:** Begränsar åtkomst till specifika kolumner eller rader.
3. **Flexibilitet:** Gör det möjligt att ändra databasschema utan att påverka applikationer.

### **Syntax för Views**

**Skapa en View:**

```sql

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
CREATE VIEW view_name AS
SELECT column1, column2
FROM table_name
WHERE condition;
```

**Exempel:**

```sql
CREATE VIEW expensive_books AS
SELECT title, price FROM books WHERE price > 100;

SELECT * FROM expensive_books;
```

**Uppdatera en View:**  
Vissa views kan uppdateras om de inte innehåller aggregeringar eller subqueries:

```sql
CREATE VIEW authors_view AS
SELECT author_id, first_name, last_name FROM authors;

UPDATE authors_view SET last_name = 'Smith' WHERE author_id = 1;
```

---

## **Indexering**

### **Vad är Indexering?**

Indexering skapar datastrukturer som förbättrar hastigheten för dataåtkomst. Liknar ett index i en bok där du kan hitta information snabbt utan att behöva läsa varje sida.

### **Typer av Index**

1. **Primärnyckelindex:** Automatiskt skapade för primärnycklar.
2. **Unika index:** Säkerställer unika värden i en kolumn.
3. **Sammansatta index:** Index på flera kolumner.

### **Syntax för Indexering**

**Skapa ett index:**

```sql
CREATE INDEX index_name ON table_name (column1, column2);
```

**Exempel:**

```sql
CREATE INDEX idx_last_name ON authors(last_name);
```

**Ta bort ett index:**

```sql
DROP INDEX index_name ON table_name;
```

### **Analysera med EXPLAIN**

**Syntax:**

```sql
EXPLAIN SELECT * FROM table_name WHERE condition;
```

**Exempel:**

```sql
EXPLAIN SELECT * FROM books WHERE price > 100;
```

**Resultat:** Visar om och hur index används i frågan.

---

## **Sammanfattning av Prepared Statements, Views och Indexering**

| Funktion                | Fördelar                                               | Begränsningar                                                      |
| ----------------------- | ------------------------------------------------------ | ------------------------------------------------------------------ |
| **Prepared Statements** | Säkerhet mot SQL-injektion, snabb exekvering.          | Kräver implementation i kod.                                       |
| **Views**               | Förenklar frågor, dataskydd, flexibilitet.             | Begränsad uppdaterbarhet och påverkar prestanda vid stora queries. |
| **Indexering**          | Snabbare sökningar, bättre prestanda i läsoperationer. | Ökar lagringskrav och kan sakta ner skrivoperationer.              |

---

## **Övningsuppgifter**

### **Uppgift 1: Prepared Statements**

1. Skapa ett prepared statement som lägger till en ny bok i `books`-tabellen.
2. Uppdatera priset på en bok baserat på dess ISBN.
3. Hämta alla böcker skrivna av en specifik författare.

---

### **Uppgift 2: Views**

1. Skapa en view som visar titlar och priser för böcker dyrare än 100 kr.
2. Skapa en view som kombinerar tabellerna `authors` och `books` och visar författarens namn och deras böcker.
3. Testa om en av dina views går att uppdatera och analysera varför eller varför inte.

---

### **Uppgift 3: Indexering**

1. Skapa ett index på kolumnen `price` i `books`-tabellen.
2. Använd EXPLAIN för att analysera en fråga som söker efter böcker baserat på `price`.
3. Skapa ett sammansatt index på `first_name` och `last_name` i `authors`-tabellen.

---

## **Quiz-frågor**

1. Vad är huvudsyftet med Prepared Statements?  
   a) Förbättra skrivprestanda  
   b) Förhindra SQL-injektion  
   c) Skapa temporära tabeller

2. Vilken är en fördel med Views?  
   a) Förbättrar skrivprestanda  
   b) Begränsar dataåtkomst  
   c) Förhindrar dataduplikat

3. Vad påverkas mest negativt av överindexering?  
   a) SELECT-frågor  
   b) INSERT/UPDATE/DELETE-operationer  
   c) Databasens storlek

**Svar:**

1. b
2. b
3. b

---

## **Reflektionsövning**

Skriv en kort reflektion (150-200 ord) där du diskuterar:

1. Hur Prepared Statements bidrar till säkerhet i applikationer.
2. I vilka situationer Views är mest användbara.
3. Vilka faktorer du skulle överväga innan du implementerar ett index.

Använd verktyg som Notion eller Obsidian för att skriva och spara din reflektion.

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
