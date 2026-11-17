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
MySQL-verktyg för backup: `mysqldump -u root db > backup.sql`. Restore: `mysql -u root db < backup.sql`

## pg_dump
PostgreSQL-verktyg för backup: `pg_dump -U postgres databas > backup.sql`. Restore: `psql -U postgres databas < backup.sql`. Anpassat format (snabbare): `pg_dump -Fc databas > backup.dump`

## pg_restore
PostgreSQL-verktyg för att återställa från ett anpassat format (.dump): `pg_restore -U postgres -d databas backup.dump`

## SQLite-backup
SQLite är en fil — backup är att kopiera den: `cp database.db backup.db`. Säkert alternativ när applikationen är igång: `sqlite3 database.db ".backup 'backup.db'"`

## BACKUP DATABASE (MSSQL)
SQL Server-kommando för backup: `BACKUP DATABASE HeroesDb TO DISK = 'backup.bak' WITH COMPRESSION`. Restore: `RESTORE DATABASE HeroesDb FROM DISK = 'backup.bak' WITH REPLACE`

## Point-in-Time Recovery
Återställning till en specifik tidpunkt. Kräver full backup + transaktionsloggar (MySQL binlog, PostgreSQL WAL, SQL Server transaktionslogg).

## 3-2-1-regeln
Tumregel för backup: 3 kopior av data, 2 olika lagringsmedia, 1 kopia offsite (geografiskt separerad).

## Full backup
Komplett kopia av hela databasen vid ett givet tillfälle. Enkel att återställa, men tar mest plats.

## Differentiell backup
Kopia av allt som ändrats sedan senaste full backup. Snabbare än full, men restore kräver senaste full-backup + den differentiella.

## Inkrementell backup
Kopia av allt som ändrats sedan senaste backup (oavsett typ). Minst data, men restore kräver full backup + alla inkrementella i rätt ordning.

## Minsta Behörighet
Säkerhetsprincip: ge användare bara de rättigheter de absolut behöver.

## Roll
Grupp av behörigheter. MySQL 8+ har inbyggt rollstöd: `CREATE ROLE 'read_only';`

## Kryptering
Skydda känslig data. I transit (TLS) och i vila (AES, Always Encrypted i MSSQL).

