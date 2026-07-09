# UML och Databasdesign — Övningar

## Övning 1: ER-diagram från textbeskrivning

Rita ett ER-diagram (på papper, whiteboard eller digitalt) för:

*"Ett bibliotek har böcker och medlemmar. Varje bok har ett ISBN, titel, författare och publiceringsår. Medlemmar har namn, email och medlemsnummer. En medlem kan låna flera böcker, och varje lån registreras med datum. Böcker kan vara utlånade av högst en medlem åt gången."*

**Identifiera:**
- Entiteter (tabeller)
- Attribut (kolumner)
- Relationer (FK)
- Kardinalitet (1:N, N:N)

---

## Övning 2: Skapa DDL från ER-diagram

Skriv SQL för att skapa tabellerna från Övning 1.

**Krav:**
- Använd `CREATE TABLE` med lämpliga datatyper
- Sätt `PRIMARY KEY` och `FOREIGN KEY` constraints
- Använd `NOT NULL` där det är lämpligt
- Lägg till `CHECK` constraints för t.ex. lånedatum i framtiden

**Exempel:**
```sql
CREATE TABLE Bok (
    ISBN VARCHAR(13) PRIMARY KEY,
    Titel VARCHAR(200) NOT NULL
);
```

---

## Övning 3: Normalisera en tabell

Du får denna tabell:

| OrderId | Kund | Adress | Produkter | Summa |
|---------|------|--------|-----------|-------|
| 1 | Anna | Storgatan 1 | Äpple, Päron, Banan | 45 |
| 2 | Björn | Lillgatan 3 | Äpple, Apelsin | 23 |
| 3 | Anna | Storgatan 1 | Apelsin, Kiwi | 18 |

Normalisera till 3NF. Skapa ett ER-diagram och SQL.

**Vad är problemen med denna struktur?** (upprepning, uppdateringsanomalier, etc.)

---

## Övning 4: DML — CRUD med SQL

Använd databasen från Övning 2 och skriv SQL för:

1. **INSERT** — lägg till 3 böcker och 2 medlemmar
2. **INSERT** — registrera ett lån
3. **SELECT** — hitta alla böcker som är utlånade just nu
4. **SELECT** — visa alla lån för en specifik medlem (JOIN)
5. **UPDATE** — ändra titel på en bok
6. **DELETE** — ta bort en medlem som aldrig lånat

---

## Övning 5: Säkerhet — DCL

Skapa SQL-kommandon för:

```sql
-- Skapa en användare för bibliotekarien
-- Ge bibliotekarien rättighet att läsa och skriva alla tabeller
-- Skapa en användare för besökare (read-only)
-- Ge besökaren rättighet att bara läsa Böcker-tabellen
-- Testa: försök INSERT som besökare (förväntat: denied)
```

**Varför är det viktigt att begränsa behörigheter per användare?**

---

## Övning 6: Reflektion

1. Vad är skillnaden mellan ett ER-diagram och ett UML-klassdiagram?
2. När använder du en 1:N-relation vs N:M?
3. Vad är en weak entity? Ge ett exempel.
4. Varför normaliserar man en databas? Finns det nackdelar?
