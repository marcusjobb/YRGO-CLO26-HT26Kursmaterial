# DCL — Programmeringstermer

## Data Control Language (DCL)
Den del av SQL som hanterar behörigheter och säkerhet: GRANT, REVOKE.

## GRANT
Gerb behörigheter till en användare eller roll. Kan specificeras på global, databas-, tabell- eller kolumnnivå.

```sql
GRANT SELECT, INSERT ON webshop.* TO 'anna'@'localhost';
```

## REVOKE
Tar bort tidigare givna behörigheter.

```sql
REVOKE DELETE ON webshop.Products FROM 'anna'@'localhost';
```

## Användare (User)
Ett konto som kan ansluta till databasen. Identifieras av användarnamn + värd (`'anna'@'localhost'`).

## Roll (Role)
En grupp av behörigheter som kan tilldelas användare. Infördes i MySQL 8+. Förenklar administration av många användare.

```sql
CREATE ROLE 'read_only';
GRANT SELECT ON webshop.* TO 'read_only';
GRANT 'read_only' TO 'anna'@'localhost';
```

## Minsta behörighet (Principle of Least Privilege)
Säkerhetsprincip: ge aldrig mer behörighet än vad som krävs för uppgiften. Exempel: en support-användare behöver SELECT och UPDATE, inte DROP eller DELETE.

## SQL Injection
Säkerhetsattack där användaren matar in skadlig SQL-kod via formulärfält eller URL-parametrar. Förhindras med parameterized queries.

```csharp
// OSÄKERT — strängkonkatenering
var sql = "SELECT * FROM User WHERE Name = '" + input + "'";

// SÄKERT — parameterized query
var sql = "SELECT * FROM User WHERE Name = @name";
```

## FLUSH PRIVILEGES
Kommando som laddar om behörighetstabellerna efter GRANT/REVOKE. I MySQL 8+ behövs det oftast inte (ändringar slår igenom direkt).

## WITH GRANT OPTION
Tillägg till GRANT som låter användaren vidarebefordra sina rättigheter till andra.

```sql
GRANT ALL ON webshop.* TO 'admin'@'localhost' WITH GRANT OPTION;
```

## Autentisering
Verifiering av vem användaren är — lösenord, SSH-nyckel, certifikat.

## Auktorisering
Bestämmer vad en autentiserad användare får göra — GRANT/REVOKE styr detta.
