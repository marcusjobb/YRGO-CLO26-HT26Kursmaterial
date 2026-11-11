# DDL — Programmeringstermer

## Data Definition Language (DDL)
Den del av SQL som används för att definiera och ändra databasstruktur: CREATE, ALTER, DROP, TRUNCATE.

## CREATE
DDL-kommando för att skapa databasobjekt: databaser, tabeller, index, vyer.

```sql
CREATE TABLE Product (
    ProductID INT PRIMARY KEY,
    Name VARCHAR(100)
);
```

## ALTER
DDL-kommando för att ändra befintliga databasobjekt. Kan lägga till, ändra eller ta bort kolumner och constraints.

```sql
ALTER TABLE Product ADD COLUMN Price DECIMAL(10,2);
```

## DROP
DDL-kommando för att permanent ta bort databasobjekt. OÅTERKALLELIGT i de flesta databaser.

```sql
DROP TABLE Product;  -- Tabellen försvinner
```

## TRUNCATE
DDL-kommando som tar bort ALL data i en tabell men behåller struktur och index. Snabbare än DELETE men kan inte ROLLBACK i MySQL.

```sql
TRUNCATE TABLE Product;  -- Alla rader borta, tabellen finns kvar
```

## Constraint
Regel som begränsar vilka värden som kan lagras i en tabell.

- **PRIMARY KEY** — unik + ej NULL
- **FOREIGN KEY** — referens till annan tabell
- **UNIQUE** — inga dubbletter
- **NOT NULL** — måste ha värde
- **CHECK** — villkor (t.ex. Price > 0)
- **DEFAULT** — standardvärde

## Index
Databasstruktur som snabbar upp SELECT-sökningar på bekostnad av långsammare INSERT/UPDATE.

```sql
CREATE INDEX idx_name ON Product(Name);
```

## Vy (View)
Sparad SELECT-sats som beter sig som en virtuell tabell. Används för säkerhet och förenkling.

## Datatyp
Anger typen av data i en kolumn: INT, VARCHAR, DECIMAL, DATE, TEXT, BOOLEAN, etc.

## AUTO_INCREMENT
Speciell egenskap som automatiskt genererar unika numeriska värden. Används ofta för Primary Key.

## Schema
Strukturen av en databas — vilka tabeller som finns, deras kolumner, datatyper och relationer.
