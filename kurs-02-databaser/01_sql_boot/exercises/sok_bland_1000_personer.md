# Sök bland 1000 personer

🟡

Fem personer går att hålla i huvudet. Tusen går inte.

I den här övningen skapar du en egen databas med 1000 påhittade personer och letar sedan i den med SQL. Du kommer inte att kunna se svaret genom att titta på tabellen. Du måste fråga.

**Du behöver:**

- [DB Browser for SQLite](https://sqlitebrowser.org/dl/), som är gratis och finns för Windows, macOS och Linux
- En webbläsare för [Mockaroo](https://www.mockaroo.com/)

> 💬 **15-minutersregeln:** Fastnar du i mer än 15 minuter, fråga klassen, sedan AI och sedan mig. I den ordningen.

> 🔒 **Varför påhittade personer?** Riktiga personuppgifter hör inte hemma i en övningsdatabas, inte ens dina klasskamraters. Mockaroo hittar på allt: namn, e-postadresser, husdjur och skostorlekar. Så ska man alltid göra när man testar: med data som ser riktig ut, men inte är det.

---

## Del 1: Skapa databasen

### Steg 1: En ny, tom databas

1. Öppna DB Browser for SQLite.
2. Klicka på **New Database** (Ny databas).
3. Spara filen som `population.db` någonstans där du hittar den igen.
4. Ett fönster för att skapa en tabell öppnas. Klicka på **Cancel** (Avbryt). Tabellen kommer från Mockaroo i stället.

Nu har du en tom databas.

### Steg 2: Låt Mockaroo hitta på 1000 personer

Gå till [mockaroo.com](https://www.mockaroo.com/). Där finns redan sex fält, men dem byter vi ut.

1. Klicka på **Generate fields using AI...**
2. Kopiera exempeldatan nedan och klistra in den i rutan **Or provide some example data**:

   ```csv
   id,first_name,last_name,age,gender,pet,car_make,country,email,shoe_size,hairstyle,hair_colour,eye_colour,favorite_movie_genre
   1,Anna,Lindqvist,34,Female,cat,Volvo,Sweden,anna.lindqvist@example.com,38,bob,Blonde,Blue,Comedy
   2,Omar,Haddad,27,Male,,Toyota,Lebanon,omar.haddad@example.com,43,short,Black,Brown,Action|Thriller
   3,Camila,Rojas,19,Female,dog,,Chile,camila.rojas@example.com,37,ponytail,Brown,Green,Horror
   ```

3. Ändra **# Fields** till `14`, eftersom exemplet har 14 kolumner.
4. Klicka på **Replace existing fields**.

Mockaroo läser exemplet och gissar vilken sorts data varje kolumn ska innehålla. Den gissar ofta rätt, men inte alltid.

### Steg 2b: Rätta det AI:n gjorde fel

Gå igenom fälten ett i taget och jämför med tabellen nedan. Det som står i **fetstil** är sådant AI:n brukar göra fel på.

| Field Name | Type | Inställning |
|---|---|---|
| `id` | Row Number | |
| `first_name` | First Name | |
| `last_name` | Last Name | |
| `age` | Number | min **16**, max **90**, decimals 0 |
| `gender` | Gender | |
| `pet` | Custom List | t.ex. `cat, dog, hamster, rabbit, parrot`, random, **blank 40 %** |
| `car_make` | **Car Make** | **blank 30 %** |
| `country` | Country | |
| `email` | Email Address | |
| `shoe_size` | Number | min **35**, max **47**, decimals 0 |
| `hairstyle` | Custom List | t.ex. `short, long, curly, bob, ponytail, mohawk` |
| `hair_colour` | **Color** | |
| `eye_colour` | **Color** | |
| `favorite_movie_genre` | **Movie Genres** | |

Det här brukar behöva rättas:

- **Fältnamn med mellanslag.** Har AI:n skrivit `first name` i stället för `first_name`? Byt till understreck. Ett kolumnnamn med mellanslag måste skrivas med citattecken, `"first name"`, i varje fråga, och det blir fel hela tiden.
- **Färger som Custom List.** Har `hair_colour` eller `eye_colour` blivit *Custom List*? Byt till den inbyggda typen **Color**. Mockaroo har en bugg som gör att för många Custom List-fält kan ge felet `undefined method 'first' for nil` i stället för riktiga värden. Ser du det felet i förhandsvisningen (**Preview**) har du för många Custom List-fält, och då ska fler bytas till inbyggda typer.
- **Skostorlek.** AI:n väljer ibland amerikanska storlekar, 4–15. Vi använder europeiska, 35–47.
- **Ålder.** 0–100 ger nyfödda med bil. 16–90 är rimligare.
- **Bilmärke, inte bilmodell.** Typen *Car Model* ger bara modellen, t.ex. `Corolla`, utan märke. Därför använder vi *Car Make*, som ger märket.
- **Blank** på `pet` och `car_make` gör att ungefär fyra av tio saknar husdjur och tre av tio saknar bil. Det behöver vi för att öva på `NULL`.

Mockaroos färger är inte alltid realistiska. Lila ögon och turkost hår är helt okej i den här databasen. 😄

Klicka på **Preview** och kontrollera att alla kolumner har riktiga värden innan du går vidare.

Längst ned på sidan:

| Inställning | Värde |
|---|---|
| **# Rows** | `1000` |
| **Format** | `SQL` |
| **Table Name** | `people` |
| **Include CREATE TABLE** | ✅ ibockad |

Klicka på **Generate Data**. En fil som heter ungefär `people.sql` laddas ner.

Öppna gärna filen i en texteditor och titta. Överst står en `create table people (...)`, och sedan kommer tusen `insert into people ...`, en för varje person.

### Steg 3: Importera till SQLite

Tillbaka i DB Browser:

1. Välj **File → Import → Database from SQL file...** (Arkiv → Importera → Databas från SQL-fil).
2. Välj filen du laddade ner.
3. Får du frågan om du vill skapa en ny databas svarar du **No**. Datan ska in i `population.db`, som redan är öppen.
4. Klicka på **Write Changes** (Skriv ändringar), eller tryck `Ctrl+S`.

> ⚠️ **Glöm inte Write Changes!** DB Browser sparar inget förrän du klickar på den. Stänger du programmet utan att spara är allt borta, och det är ett av de vanligaste misstagen.

### Steg 4: Kontrollera att allt kom med

Gå till fliken **Execute SQL** och kör:

```sql
SELECT COUNT(*) FROM people;
```

Svaret ska vara **1000**.

> 💡 **Allas data är olika!** Mockaroo hittar på nya personer varje gång, och AI:n kan dessutom ha valt lite olika sorters värden åt dig och din kompis. Dina svar kommer därför inte att vara samma som någon annans, och facit visar bara *frågorna*, inte siffrorna. Det är meningen. Frågan är det viktiga.

---

## Del 2: Granska och städa

Du har inte skrivit datan själv. Det har en AI gjort. Innan du börjar leta behöver du därför veta **vad som faktiskt står i tabellen**.

### Titta på värdena

`DISTINCT` visar varje värde en gång. Kör det på kolumnerna med text:

```sql
SELECT DISTINCT gender FROM people;
SELECT DISTINCT pet FROM people;
SELECT DISTINCT hair_colour FROM people;
SELECT DISTINCT eye_colour FROM people;
SELECT DISTINCT hairstyle FROM people;
SELECT DISTINCT car_make FROM people;
SELECT DISTINCT favorite_movie_genre FROM people;
```

Och på talen, kolla att de är rimliga:

```sql
SELECT MIN(age), MAX(age), MIN(shoe_size), MAX(shoe_size) FROM people;
```

Anteckna vad du ser:

- Hur är värdena stavade? `Cat` eller `cat`? `Blonde` eller `Blond`?
- Finns det värden som borde vara samma, men är stavade olika?
- Är åldrarna och skostorlekarna rimliga? Ingen bör vara 3 eller 300 år, eller ha skostorlek 2.
- Hur ser `favorite_movie_genre` ut? Står det ibland flera genrer i samma ruta, t.ex. `Action|Thriller`?

Du behöver svaren när du ska skriva `WHERE` i del 3.

### Städa om det behövs

Hittar du samma sak stavad på olika sätt, städa! Det kallas **datasanering**, och det görs alltid **innan** man börjar räkna och söka.

```sql
-- Exempel: om det finns både "Brown" och "Brown " (med mellanslag på slutet)
UPDATE people SET hair_colour = TRIM(hair_colour);

-- Exempel: om det finns både "Blonde" och "blonde"
UPDATE people SET hair_colour = 'Blonde' WHERE LOWER(hair_colour) = 'blonde';
```

Kör `SELECT DISTINCT` igen och kontrollera att det blev rätt. Klicka sedan på **Write Changes**.

Mer om sanering, och varför den kommer före allt annat, finns i [En liten saga om en databas](../notes/en_liten_saga_om_en_databas.md#först-städa-sanering).

> 🤔 **Ingen smuts alls?** Bra! Då har du ändå lärt dig det viktigaste: att *titta först*. I riktiga projekt är datan nästan aldrig så ren.

---

## Del 3: Leta!

Nu ska vi leka detektiv. Skriv en SQL-fråga för varje uppgift. Länkarna går till sidan som förklarar kommandot, om du behöver repetera.

Facit använder värden som `'cat'` och `'Blue'`. Har din AI valt andra stavningar, använd dina egna värden från del 2.

### 🟢 Grundnivå

**1.** Visa de tio första personerna i tabellen. *([SELECT](../notes/sql/select.md), [ORDER BY](../notes/sql/order-by.md))*

**2.** Vilka bor i Sverige? Visa förnamn, efternamn och ålder. *([WHERE](../notes/sql/where.md))*

**3.** Vilka har ett förnamn som börjar på M? *([WHERE](../notes/sql/where.md))*

**4.** Hur många har en Gmail-adress? *([WHERE](../notes/sql/where.md), [Aggregatfunktioner](../notes/sql/aggregat.md))*

**5.** Hur många saknar husdjur? *([WHERE](../notes/sql/where.md))*

**6.** Hur många har skostorlek 40 eller större? *([WHERE](../notes/sql/where.md))*

<details>
<summary>💡 Klicka här för lösningsförslag till grundnivån</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
-- 1
SELECT * FROM people LIMIT 10;

-- 2
SELECT first_name, last_name, age
FROM people
WHERE country = 'Sweden';

-- 3
SELECT first_name, last_name
FROM people
WHERE first_name LIKE 'M%';

-- 4
SELECT COUNT(*)
FROM people
WHERE email LIKE '%@gmail.com';

-- 5
SELECT COUNT(*)
FROM people
WHERE pet IS NULL;

-- 6
SELECT COUNT(*)
FROM people
WHERE shoe_size >= 40;
```

- **2:** Länderna är troligen på engelska, så det är `'Sweden'` och inte `'Sverige'`. Får du noll träffar, kör `SELECT DISTINCT country FROM people;` och se hur de är stavade. Det kan också vara så att ingen av dina tusen bor i Sverige!
- **5:** `IS NULL`, aldrig `= NULL`. Svaret bör ligga runt 400, eftersom du satte 40 % blank.

</details>

### 🟡 Mellannivå

**7.** Vilka är mellan 18 och 25 år? *([WHERE](../notes/sql/where.md))*

**8.** Vilka är de tio äldsta? *([ORDER BY](../notes/sql/order-by.md))*

**9.** Hur många har en katt? *([WHERE](../notes/sql/where.md))*

**10.** Hur många som identifierar sig som kvinnor har blå ögon och gillar skräckfilm? *Tips: en person kan gilla flera genrer.* *([WHERE](../notes/sql/where.md))*

**11.** Vilka som kör Volvo eller Tesla är 30 år eller äldre? *([WHERE](../notes/sql/where.md))*

**12.** Vilka fem länder har flest personer? *([GROUP BY](../notes/sql/group-by.md))*

**13.** Vilka länder har fler än 20 personer? *([GROUP BY](../notes/sql/group-by.md))*

**14.** Vilket är det vanligaste husdjuret, bland dem som har ett? *([GROUP BY](../notes/sql/group-by.md))*

<details>
<summary>💡 Klicka här för lösningsförslag till mellannivån</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
-- 7
SELECT first_name, last_name, age
FROM people
WHERE age BETWEEN 18 AND 25;

-- 8
SELECT first_name, last_name, age
FROM people
ORDER BY age DESC
LIMIT 10;

-- 9
SELECT COUNT(*)
FROM people
WHERE pet = 'cat';

-- 10
SELECT COUNT(*)
FROM people
WHERE gender = 'Female'
  AND eye_colour = 'Blue'
  AND favorite_movie_genre LIKE '%Horror%';

-- 11
SELECT first_name, last_name, car_make, age
FROM people
WHERE (car_make = 'Volvo' OR car_make = 'Tesla')
  AND age >= 30;

-- 12
SELECT country, COUNT(*) AS antal
FROM people
GROUP BY country
ORDER BY antal DESC
LIMIT 5;

-- 13
SELECT country, COUNT(*) AS antal
FROM people
GROUP BY country
HAVING COUNT(*) > 20
ORDER BY antal DESC;

-- 14
SELECT pet, COUNT(*) AS antal
FROM people
WHERE pet IS NOT NULL
GROUP BY pet
ORDER BY antal DESC
LIMIT 1;
```

- **7:** `BETWEEN` räknar med gränserna, så både 18- och 25-åringar kommer med.
- **10:** `favorite_movie_genre` kan innehålla flera genrer i samma ruta, t.ex. `Comedy|Horror`. Då räcker inte `= 'Horror'`, men `LIKE '%Horror%'` hittar ordet var det än står. Att flera värden står i samma ruta bryter för övrigt mot **första normalformen**. Se [En liten saga om en databas](../notes/en_liten_saga_om_en_databas.md#det-här-kallas-normalisering).
- **11:** Prova **utan parenteser** och jämför antalet! Utan dem läses frågan som "alla Volvo-förare, oavsett ålder, *eller* Tesla-förare som är 30 eller äldre", och då blir det fler träffar.
- **14:** Utan `WHERE pet IS NOT NULL` blir `NULL` ofta det "vanligaste husdjuret", eftersom 40 % saknar husdjur.

</details>

### 🔴 Utmaning

**15.** Vad är snittskostorleken för varje kön? Visa också hur många det är i varje grupp, och avrunda snittet till en decimal. *([GROUP BY](../notes/sql/group-by.md))*

**16.** Visa namn och husdjur för alla. Den som saknar husdjur ska få texten `inget husdjur`. *([COALESCE](../notes/sql/coalesce.md))*

**17.** Är de som har husdjur i snitt äldre eller yngre än de som inte har det? Svara med en enda fråga. *([GROUP BY](../notes/sql/group-by.md))*

**18.** Vilka fem kombinationer av hårfärg och ögonfärg är vanligast? *([GROUP BY](../notes/sql/group-by.md))*

**19.** Hur många unika förnamn, unika efternamn och unika kombinationer av för- och efternamn finns det? Finns det namnkusiner? *([SELECT](../notes/sql/select.md), [Aggregatfunktioner](../notes/sql/aggregat.md))*

<details>
<summary>💡 Klicka här för lösningsförslag till utmaningen</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
-- 15
SELECT gender,
       ROUND(AVG(shoe_size), 1) AS snittstorlek,
       COUNT(*)                 AS antal
FROM people
GROUP BY gender
ORDER BY snittstorlek DESC;

-- 16
SELECT first_name, last_name,
       COALESCE(pet, 'inget husdjur') AS pet
FROM people;

-- 17
SELECT pet IS NOT NULL      AS har_husdjur,
       ROUND(AVG(age), 1)   AS snittalder,
       COUNT(*)             AS antal
FROM people
GROUP BY har_husdjur;

-- 18
SELECT hair_colour, eye_colour, COUNT(*) AS antal
FROM people
GROUP BY hair_colour, eye_colour
ORDER BY antal DESC
LIMIT 5;

-- 19
SELECT COUNT(DISTINCT first_name)                     AS fornamn,
       COUNT(DISTINCT last_name)                      AS efternamn,
       COUNT(DISTINCT first_name || ' ' || last_name) AS hela_namn
FROM people;
```

- **17:** `pet IS NOT NULL` blir `1` (sant) eller `0` (falskt) i SQLite. Då kan du gruppera på det, och få två högar: med och utan husdjur.
- **18:** `GROUP BY` på två kolumner ger en hög för varje *kombination*.
- **19:** Är `hela_namn` mindre än 1000 finns det personer som heter exakt likadant. Hitta dem med `GROUP BY first_name, last_name HAVING COUNT(*) > 1`.

</details>

---

## Del 4: Bonus

### 🔴 Bonus 1: Dubbelgångare

Finns det personer som ser likadana ut, alltså med samma kön, frisyr, hårfärg och ögonfärg?

1. Visa alla kombinationer som förekommer mer än en gång.
2. Hittar du få eller inga? Räkna ut **hur många olika kombinationer som är möjliga** med en enda fråga, och jämför med att du har 1000 personer.

*Tips: om det finns 3 kön och 4 frisyrer finns det 3 × 4 = 12 sätt att kombinera dem.*

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
-- 1
SELECT gender, hairstyle, hair_colour, eye_colour, COUNT(*) AS antal
FROM people
GROUP BY gender, hairstyle, hair_colour, eye_colour
HAVING COUNT(*) > 1
ORDER BY antal DESC;

-- 2
SELECT COUNT(DISTINCT gender)
     * COUNT(DISTINCT hairstyle)
     * COUNT(DISTINCT hair_colour)
     * COUNT(DISTINCT eye_colour) AS mojliga_kombinationer
FROM people;
```

Med t.ex. 8 kön, 6 frisyrer och 20 färger för både hår och ögon blir det 8 × 6 × 20 × 20 = **19 200** möjliga utseenden. De ska fördelas på bara 1000 personer, så de flesta kombinationerna används aldrig, och nästan ingen får en dubbelgångare.

Det är samma matte som gör att ett lösenord blir starkare för varje tecken du lägger till. Varje ny egenskap *gångrar* antalet möjligheter, den lägger inte bara till.

Har du inte sanerat i del 2 kan samma utseende dessutom dyka upp som två olika kombinationer, t.ex. en med `Brown` och en med `Brown␣`. Då blir det ännu svårare att hitta dubbelgångarna.

</details>

### 🔴 Bonus 2: Vanligaste e-postdomänen

Vilka tio domäner (allt efter `@`) är vanligast bland e-postadresserna?

*Tips: `INSTR(text, '@')` ger positionen för `@`, och `SUBSTR(text, start)` ger texten från en viss position och framåt.*

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT SUBSTR(email, INSTR(email, '@') + 1) AS domain,
       COUNT(*) AS antal
FROM people
GROUP BY domain
ORDER BY antal DESC
LIMIT 10;
```

`+ 1` gör att själva `@` inte kommer med. Du kan `GROUP BY` på något du har räknat fram, inte bara på kolumner.

</details>

### 🔴 Bonus 3: Normalisera länderna

`country` står som text på varje rad, alltså samma problem som i [En liten saga om en databas](../notes/en_liten_saga_om_en_databas.md). Bygg en egen ö för länderna:

1. Skapa en tabell `country` med `id` och `name`.
2. Fyll den med alla unika länder från `people`.
3. Lägg till en kolumn `country_id` i `people` och fyll den med rätt id.
4. Visa de fem länderna med flest personer, men med en `JOIN` mot `country` i stället för att använda textkolumnen.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
-- 1
CREATE TABLE country (
    id   INTEGER PRIMARY KEY,
    name TEXT NOT NULL UNIQUE
);

-- 2
INSERT INTO country (name)
SELECT DISTINCT country FROM people WHERE country IS NOT NULL ORDER BY country;

-- 3
ALTER TABLE people ADD COLUMN country_id INTEGER REFERENCES country(id);

UPDATE people
SET country_id = (SELECT id FROM country WHERE country.name = people.country);

-- Kontroll: ska ge 0
SELECT COUNT(*) FROM people WHERE country_id IS NULL;

-- 4
SELECT c.name, COUNT(*) AS antal
FROM people AS p
JOIN country AS c ON p.country_id = c.id
GROUP BY c.name
ORDER BY antal DESC
LIMIT 5;
```

Svaret i steg 4 ska vara exakt samma som i uppgift 12. Samma data, bättre struktur.

`ALTER TABLE` är DDL, eftersom den ändrar tabellens *struktur*, och `UPDATE` är DML, eftersom den ändrar *innehållet*. Du använde alltså två av SQL:s familjer i samma uppgift.

Nästa steg vore att ta bort den gamla textkolumnen `country`, så att länderna bara står på **ett** ställe. Samma sak skulle kunna göras för husdjur, bilmärken och hårfärger. Vilka andra kolumner skulle må bra av en egen ö?

</details>

### 💬 Bonus 4: Byt frågor med ett annat par

Byt SQL-frågor med ett annat par och granska varandras:

- **Läsbarhet:** Har varje del (`SELECT`, `FROM`, `WHERE`...) en egen rad? Förstår du frågan utan att köra den?
- **Parenteser:** Finns det `AND` och `OR` i samma `WHERE`? Står det parenteser runt dem?
- **`NULL`:** Används `IS NULL`, och inte `= NULL`? Har någon glömt att `pet` och `car_make` kan vara `NULL`?
- **Samma svar?** Ni har olika data, och kanske olika stavningar. Fungerar era frågor på varandras databaser? Om inte, varför?

> 💬 *Min lösning är inte facit. Om din fråga fungerar och är läsbar, bra jobbat.*

---

## Mer att öva på: SQLZoo

[SQLZoo](https://sqlzoo.net/) har gratis övningar som du gör direkt i webbläsaren, utan att installera något. De passar bra som repetition efter den här övningen:

| SQLZoo | Övar på | Motsvarar |
|---|---|---|
| [SELECT basics](https://sqlzoo.net/wiki/SELECT_basics) | `SELECT`, `WHERE`, `IN`, `BETWEEN` | Grundnivån |
| [SELECT from WORLD](https://sqlzoo.net/wiki/SELECT_from_WORLD_Tutorial) | `WHERE`, `LIKE`, räkna i `SELECT` | Grundnivån och mellannivån |
| [SELECT from Nobel](https://sqlzoo.net/wiki/SELECT_from_Nobel_Tutorial) | `WHERE` med flera villkor, `ORDER BY` | Mellannivån |
| [SELECT within SELECT](https://sqlzoo.net/wiki/SELECT_within_SELECT_Tutorial) | subqueries | Fördjupning |
| [SUM and COUNT](https://sqlzoo.net/wiki/SUM_and_COUNT) | aggregat, `GROUP BY`, `HAVING` | Uppgift 12–18 |
| [The JOIN operation](https://sqlzoo.net/wiki/The_JOIN_operation) | `JOIN` | Bonus 3 |
| [Using Null](https://sqlzoo.net/wiki/Using_Null) | `NULL`, `LEFT JOIN`, `COALESCE` | Uppgift 5, 14 och 16 |

SQLZoo kör en annan databasmotor än SQLite i bakgrunden. Det mesta är samma SQL som du har skrivit här, men små saker kan skilja.

---

Tusen personer, och du hittade dem alla med några rader SQL. Det är precis så det fungerar i riktiga system, fast där är det ofta miljoner. Snyggt jobbat! 💪

*Av Marcus Ackre Medina · Nion Education · marcus.medina@nionit.com*
