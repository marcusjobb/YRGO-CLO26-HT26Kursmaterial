# 07 Sakerhet Och Backup — Programmeringstermer

## DCL
Data Control Language. GRANT och REVOKE för att styra databasbehörigheter.

## GRANT
Ge en användare behörigheter: `GRANT SELECT, INSERT ON db.* TO 'user'@'host';`

## REVOKE
Ta bort behörigheter: `REVOKE DELETE ON db.* FROM 'user'@'host';`

## SQL Injection
Attack där skadlig SQL injiceras via användarinput. Förhindras med parameterized queries.

## Backup
Kopia av data för återställning vid fel. Full, differentiell, transaktionslogg.

## mysqldump
MySQL-verktyg för backup: `mysqldump -u root db > backup.sql`

## Point-in-Time Recovery
Återställning till en specifik tidpunkt. Kräver full backup + transaktionsloggar.

## Minsta Behörighet
Säkerhetsprincip: ge användare bara de rättigheter de absolut behöver.

## Roll
Grupp av behörigheter. MySQL 8+ har inbyggt rollstöd: `CREATE ROLE 'read_only';`

## Kryptering
Skydda känslig data. I transit (TLS) och i vila (AES, Always Encrypted).

