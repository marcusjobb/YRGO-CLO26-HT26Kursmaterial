---
marp: true
theme: default
class: invert
paginate: true
---

# DCL och Databassäkerhet

**Kurs:** Databashantering och -design
**Modul:** 07 — UML och databasdesign

---

## Vad ska vi lära oss idag?

- **DCL** — Data Control Language (GRANT, REVOKE)
- **Användare och roller** — skapa och hantera
- **Minsta behörighet** — principen som skyddar din data
- **SQL Injection** — vanligaste attacken och hur du skyddar dig
- **Backup och återställning**

---

## Varför DCL?

I en databas med flera användare vill du inte att alla ska kunna:

- ❌ Tabort tabeller
- ❌ Se andras lösenord
- ❌ Ändra priser i produktdatabasen
- ❌ Köra DROP DATABASE för skojs skull

**DCL = Vem får göra vad?**

---

## Skapa användare

```sql
-- Skapa en vanlig användare
CREATE USER 'anna'@'localhost' IDENTIFIED BY 'säkert_lösenord123';

-- Användare från vilken maskin som helst
CREATE USER 'anna'@'%' IDENTIFIED BY 'säkert_lösenord123';

-- Se alla användare
SELECT User, Host FROM mysql.user;
```

`'anna'@'localhost'` = bara från samma maskin
`'anna'@'%'` = från vilken maskin som helst (mer riskabelt)

---

## DCL — GRANT

Ge behörigheter:

```sql
-- Allt på en specifik databas
GRANT ALL PRIVILEGES ON webshop.* TO 'anna'@'localhost';

-- Bara SELECT på hela servern
GRANT SELECT ON *.* TO 'rapport'@'localhost';

-- Specifika rättigheter på en tabell
GRANT SELECT, INSERT, UPDATE ON webshop.Orders TO 'orderadmin'@'localhost';

-- Skapa användare + ge rättigheter
GRANT ALL PRIVILEGES ON *.* TO 'admin'@'localhost'
WITH GRANT OPTION;  -- Kan även ge rättigheter till andra
```

---

## DCL — REVOKE

Ta bort behörigheter:

```sql
-- Ta bort en specifik rättighet
REVOKE DELETE ON webshop.Products FROM 'anna'@'localhost';

-- Ta bort alla rättigheter för en användare
REVOKE ALL PRIVILEGES ON webshop.* FROM 'anna'@'localhost';

-- Ta bort möjligheten att ge rättigheter
REVOKE GRANT OPTION FROM 'anna'@'localhost';

-- Glöm inte att spara ändringarna!
FLUSH PRIVILEGES;
```

---

## Behörighetsnivåer

```
Globala rättigheter (*.*)
    └── Databasrättigheter (webshop.*)
            └── Tabellrättigheter (webshop.Orders)
                    └── Kolumnrättigheter (webshop.Orders.Price)
```

```sql
-- Global nivå (alla databaser)
GRANT SELECT ON *.* TO 'reader'@'localhost';

-- Databasnivå
GRANT ALL ON webshop.* TO 'dev'@'localhost';

-- Tabellnivå
GRANT SELECT, INSERT ON webshop.Orders TO 'support'@'localhost';

-- Kolumnnivå
GRANT SELECT (Name, Email) ON webshop.Customer TO 'marketing'@'localhost';
```

---

## Roller (MySQL 8+)

Skapa roller för att gruppera behörigheter:

```sql
-- Skapa roller
CREATE ROLE 'read_only', 'read_write', 'db_admin';

-- Ge roller rättigheter
GRANT SELECT ON *.* TO 'read_only';
GRANT SELECT, INSERT, UPDATE, DELETE ON webshop.* TO 'read_write';
GRANT ALL ON webshop.* TO 'db_admin';

-- Tilldela roller till användare
GRANT 'read_only' TO 'rapport'@'localhost';
GRANT 'read_write' TO 'anna'@'localhost';
GRANT 'db_admin' TO 'peter'@'localhost';

-- Aktivera roll (standard vid inloggning)
SET DEFAULT ROLE 'read_only' TO 'rapport'@'localhost';
```

---

## Minsta behörighet (Principle of Least Privilege)

**Ge aldrig mer än vad användaren behöver.**

| Roll | Behöver | Behörighet |
|------|---------|------------|
| Kundsupport | Se ordrar, uppdatera status | SELECT, UPDATE på Orders |
| Rapportverktyg | Läsa data | SELECT på relevanta tabeller |
| Orderadministratör | Skapa/ändra ordrar | SELECT, INSERT, UPDATE på Orders |
| Utvecklare | Bygga funktioner | ALL på dev-databasen, SELECT i produktion |
| DBA | Allt | ALL PRIVILEGES + GRANT OPTION |

---

## SQL Injection — den farligaste attacken

**Sårbar kod (C#):**
```csharp
string sql = "SELECT * FROM User WHERE Username = '" + input + "'";
```

Användaren skriver in: `' OR '1'='1`

SQL blir: `SELECT * FROM User WHERE Username = '' OR '1'='1'`

💥 **Alla användare läcks!**

Värre: `'; DROP TABLE User; --`

---

## Skydda mot SQL Injection

**Använd parameterized queries — ALDRIG strängkonkatenering!**

```csharp
// ✅ Säkert med EF Core
var user = context.Users
    .FirstOrDefault(u => u.Username == input);

// ✅ Säkert med ADO.NET
string sql = "SELECT * FROM User WHERE Username = @username";
var cmd = new SqlCommand(sql, conn);
cmd.Parameters.AddWithValue("@username", input);
```

**Regler:**
1. Använd ORM (EF Core) — inbyggt skydd
2. Validera all input (längd, format, typ)
3. Använd stored procedures (med parameterar, inte strängar)
4. Ge appen MINSTA möjliga behörighet (SELECT, inte DROP)

---

## Backup och återställning

```bash
# MySQL dump (kommandorad)
mysqldump -u root -p webshop > webshop_backup.sql

# Återställ
mysql -u root -p webshop < webshop_backup.sql

# Alla databaser
mysqldump -u root -p --all-databases > full_backup.sql

# Bara struktur (ingen data)
mysqldump -u root -p --no-data webshop > webshop_structure.sql
```

**Backupstrategi:**
- Daglig full backup (nattetid)
- Timvis inkrementell (bara förändringar)
- Testa återställning regelbundet!
- Lagra backup på annan plats än databasen

---

## Kryptering av känslig data

```sql
-- Kryptera vid INSERT
INSERT INTO Customer (Name, SSN)
VALUES ('Anna', AES_ENCRYPT('19850101-1234', 'hemlig_nyckel'));

-- Dekryptera vid SELECT
SELECT Name,
       CAST(AES_DECRYPT(SSN, 'hemlig_nyckel') AS CHAR) AS SSN
FROM Customer;
```

**Bättre alternativ:** Kryptering på applikationsnivå (C#) innan data skickas till databasen.

---

## Säkerhetschecklista

- [ ] Alla standardlösenord är bytta
- [ ] Ingen använder root i produktion
- [ ] Appen har minsta möjliga behörighet
- [ ] Parameterized queries (ingen SQL injection)
- [ ] Backup körs och TESTAS regelbundet
- [ ] Bakom brandvägg (inte öppen mot Internet)
- [ ] Anslutning via TLS (inte okrypterat)
- [ ] Lösenord hashas (inte i klartext)

---

## Sammanfattning

- ✅ DCL = GRANT / REVOKE — vem får göra vad
- ✅ Roller grupperar behörigheter
- ✅ Minsta behörighet = minst skada
- ✅ SQL Injection = parameterized queries
- ✅ Backup = din livlina
- ➡️ Nu har du verktygen för att designa, bygga och säkra databaser!

---
