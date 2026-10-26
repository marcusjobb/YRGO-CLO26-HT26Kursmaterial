# Backup — SQLite, MySQL, PostgreSQL och SQL Server

🟢

Backup är det enda som räddar dig när allt annat gått åt skogen. En server kan krascha, en kollega kan råka köra `DELETE FROM orders` utan `WHERE`, en hacker kan kryptera hela disken. Backup är inte "nice to have" — det är skillnaden mellan en jobbig dag och ett katastrofalt slut.

Det som gör backup intressant är att det ser annorlunda ut beroende på vilket databasystem du använder. SQLite gör det löjligt enkelt. MySQL, PostgreSQL och MSSQL kräver var sitt verktyg och sin strategi.

## Innehållsförteckning

- [3-2-1-regeln — det enda du behöver komma ihåg](#3-2-1-regeln)
- [Typer av backup](#typer-av-backup)
- [SQLite — kopiera filen, det är allt](#sqlite--kopiera-filen-det-är-allt)
- [MySQL — mysqldump](#mysql--mysqldump)
- [PostgreSQL — pg_dump](#postgresql--pg_dump)
- [SQL Server (MSSQL) — BACKUP DATABASE](#sql-server-mssql--backup-database)
- [Jämförelse — en snabb överblick](#jämförelse--en-snabb-överblick)
- [Testa återställning](#testa-återställning)
- [TL;DR](#tldr)

---

## 3-2-1-regeln

Det finns en tumregel som alla databasproffs levt efter i decennier:

> **3** kopior av data · **2** olika lagringsmedia · **1** kopia offsite

Tre kopior: originalet + två backups. Två media: till exempel lokal disk och molnet. En offsite: om kontoret brinner ska backupen överleva.

I praktiken för ett litet projekt: kör daglig backup till en mapp, synka den mappen till cloud storage (Azure Blob, S3, Google Cloud Storage). Voilà — 3-2-1.

---

## Typer av backup

| Typ | Vad kopieras | Fördel | Nackdel |
|-----|-------------|--------|---------|
| **Full** | Hela databasen | Enkel återställning | Tar plats och tid |
| **Differentiell** | Allt som ändrats sedan senaste full | Snabbare än full | Kräver senaste full-backup för restore |
| **Inkrementell** | Allt sedan senaste backup (oavsett typ) | Minst data | Restore kräver alla inkrementella + full |
| **Point-in-Time** | Transaktionsloggar | Återställ till exakt sekund | Kräver full backup + loggar |

För de flesta YH-projekt: **full backup** dagligen är mer än tillräckligt.

---

## SQLite — kopiera filen, det är allt

SQLite lagrar hela databasen i en enda fil. Det är det bästa och det sämsta med SQLite på samma gång — men för backup är det en gåva.

```bash
# Backup — kopiera filen
cp database.db backups/database_$(date +%Y%m%d_%H%M%S).db
```

Klart. Det är hela backup-lösningen.

### Varning om öppen databas

Om applikationen skriver till databasen när du kopierar filen riskerar du att få en inkonsekvent kopia. SQLite har en inbyggd lösning för det:

```bash
# Säkert backup med sqlite3-kommandot (rekommenderat)
sqlite3 database.db ".backup 'backups/database_backup.db'"
```

`.backup`-kommandot använder SQLites interna kopierings-API och hanterar transaktioner korrekt — du får en konsekvent snapshot även om applikationen skriver samtidigt.

### Restore

```bash
# Restore — kopiera tillbaka
cp backups/database_20241025_143022.db database.db
```

Stoppa applikationen → kopiera filen → starta applikationen.

### Exportera till SQL (textformat)

```bash
# Exportera som SQL — mer portabelt
sqlite3 database.db .dump > backup.sql

# Importera tillbaka
sqlite3 ny_database.db < backup.sql
```

SQL-formatet är textläsbart och fungerar att flytta till en annan databas om du bestämmer dig för att migrera.

---

## MySQL — mysqldump

MySQL inkluderar `mysqldump` — ett kommandoradsverktyg som exporterar databasen till ett SQL-skript. Det är det enklaste sättet och fungerar för de flesta situationer.

### Grundläggande backup

```bash
# Backup av en databas
mysqldump -u root -p heroesdb > backup.sql

# Backup med tidsstämpel
mysqldump -u root -p heroesdb > heroesdb_$(date +%Y%m%d_%H%M%S).sql

# Backup av alla databaser
mysqldump -u root -p --all-databases > all_databases.sql
```

### Med Docker

```bash
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  > heroesdb_$(date +%Y%m%d_%H%M%S).sql
```

### Restore

```bash
# Restore till befintlig databas
mysql -u root -p heroesdb < backup.sql

# Med Docker
docker exec -i mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  < backup.sql
```

### Användbara flaggor

```bash
# Inkludera CREATE DATABASE-satsen
mysqldump -u root -p --databases heroesdb > backup.sql

# Komprimera direkt (sparar disk)
mysqldump -u root -p heroesdb | gzip > backup.sql.gz

# Restore från komprimerad fil
gunzip < backup.sql.gz | mysql -u root -p heroesdb
```

### Automatisk backup med cron (Linux/Mac)

```bash
# Kör varje dag kl 02:00 — lägg i crontab (crontab -e)
0 2 * * * mysqldump -uroot -pSuperSecret123 heroesdb \
  > /backups/heroesdb_$(date +\%Y\%m\%d).sql
```

---

## PostgreSQL — pg_dump

PostgreSQL använder `pg_dump` och `pg_dumpall`. Kommandona liknar mysqldump men har lite annan syntax.

### Grundläggande backup

```bash
# Backup av en databas (SQL-format)
pg_dump -U postgres heroesdb > backup.sql

# Backup med anpassat format (snabbare restore, stöder parallellism)
pg_dump -U postgres -Fc heroesdb > backup.dump

# Backup av alla databaser inklusive roller och inställningar
pg_dumpall -U postgres > all_databases.sql
```

### Restore

```bash
# Restore från SQL-fil
psql -U postgres heroesdb < backup.sql

# Restore från anpassat format (kräver pg_restore)
pg_restore -U postgres -d heroesdb backup.dump

# Restore med parallellism (snabbare för stora databaser)
pg_restore -U postgres -d heroesdb -j 4 backup.dump
```

`-j 4` betyder fyra parallella jobb — perfekt för servrar med flera kärnor.

### Miljövariabel för lösenord

```bash
# Slipp lösenordsprompten med PGPASSWORD
PGPASSWORD=mittlosenord pg_dump -U postgres heroesdb > backup.sql
```

### Automatisk backup

```bash
# crontab — varje dag kl 03:00
0 3 * * * PGPASSWORD=mittlosenord pg_dump -U postgres heroesdb \
  | gzip > /backups/heroesdb_$(date +\%Y\%m\%d).sql.gz
```

---

## SQL Server (MSSQL) — BACKUP DATABASE

SQL Server har backup inbyggt direkt i SQL — du kör T-SQL-kommandon precis som vanliga databasfrågor.

### Full backup

```sql
-- Full backup till fil
BACKUP DATABASE HeroesDb
TO DISK = 'C:\Backups\HeroesDb_Full.bak'
WITH COMPRESSION, STATS = 10;
```

`WITH COMPRESSION` halverar ungefär filstorleken. `STATS = 10` skriver ut progress var tionde procent.

### Differentiell backup

```sql
-- Differentiell backup (allt sedan senaste full)
BACKUP DATABASE HeroesDb
TO DISK = 'C:\Backups\HeroesDb_Diff.bak'
WITH DIFFERENTIAL, COMPRESSION;
```

### Transaktionslogg (för Point-in-Time Recovery)

```sql
-- Backup av transaktionslogg
BACKUP LOG HeroesDb
TO DISK = 'C:\Backups\HeroesDb_Log.bak'
WITH COMPRESSION;
```

Med transaktionslogg-backup kan du återställa databasen till exakt vilken sekund som helst — extremt användbart om du vill backa precis till innan någon körde fel.

### Restore

```sql
-- Restore från full backup
RESTORE DATABASE HeroesDb
FROM DISK = 'C:\Backups\HeroesDb_Full.bak'
WITH REPLACE;

-- Restore full + differentiell + logg (i rätt ordning)
RESTORE DATABASE HeroesDb
FROM DISK = 'C:\Backups\HeroesDb_Full.bak'
WITH NORECOVERY;  -- håller databasen offline för fler steg

RESTORE DATABASE HeroesDb
FROM DISK = 'C:\Backups\HeroesDb_Diff.bak'
WITH NORECOVERY;

RESTORE LOG HeroesDb
FROM DISK = 'C:\Backups\HeroesDb_Log.bak'
WITH RECOVERY;  -- sista steget — databasen öppnas
```

### Via SQL Server Agent (automatisering)

SQL Server inkluderar SQL Server Agent för schemalagda jobb. Det är GUI-baserat i SSMS (SQL Server Management Studio) — högerklicka på "SQL Server Agent" → "New Job" → välj schema och kommando.

---

## Jämförelse — en snabb överblick

| | SQLite | MySQL | PostgreSQL | SQL Server |
|--|--------|-------|------------|------------|
| **Backupverktyg** | `cp` / `.backup` | `mysqldump` | `pg_dump` | `BACKUP DATABASE` |
| **Restoreverktyg** | `cp` / `sqlite3` | `mysql` | `psql` / `pg_restore` | `RESTORE DATABASE` |
| **Utdataformat** | Fil / SQL | SQL | SQL / custom (.dump) | .bak (proprietärt) |
| **Komprimering** | Nej (inbyggt) | Via gzip | Via gzip / -Fc | `WITH COMPRESSION` |
| **Point-in-Time** | Nej | Med binlog | Med WAL | Med transaktionslogg |
| **Svårighetsgrad** | 🟢 Trivial | 🟢 Enkel | 🟡 Måttlig | 🟡 Måttlig |

---

## Testa återställning

En backup du aldrig testat är ingen backup. Den kan vara korrupt, den kan ha sparats fel, eller återställningsproceduren kan ha ett fel du aldrig märkt.

**Regel:** testa restore minst en gång i månaden i en testmiljö.

```bash
# SQLite — testa restore till annan fil
cp backups/heroesdb_backup.db /tmp/test_restore.db
sqlite3 /tmp/test_restore.db "SELECT COUNT(*) FROM Heroes;"
# Förväntat: samma antal som originalet

# MySQL — testa restore till testdatabas
mysql -u root -p -e "CREATE DATABASE test_restore;"
mysql -u root -p test_restore < backup.sql
mysql -u root -p -e "SELECT COUNT(*) FROM test_restore.Heroes;"

# PostgreSQL — testa restore
createdb -U postgres test_restore
pg_restore -U postgres -d test_restore backup.dump
psql -U postgres test_restore -c "SELECT COUNT(*) FROM heroes;"
```

---

## TL;DR

- **SQLite:** kopiera `.db`-filen med `cp` eller använd `.backup`-kommandot. Restore = kopiera tillbaka.
- **MySQL:** `mysqldump -u root -p databas > backup.sql` · Restore: `mysql -u root -p databas < backup.sql`
- **PostgreSQL:** `pg_dump -U postgres databas > backup.sql` · Restore: `psql -U postgres databas < backup.sql`
- **MSSQL:** `BACKUP DATABASE HeroesDb TO DISK = 'backup.bak' WITH COMPRESSION` · Restore: `RESTORE DATABASE`
- **3-2-1-regeln:** 3 kopior, 2 media, 1 offsite — alltid.
- **Testa restore** innan du behöver den på riktigt.

Sådärja. Nu vet du vad du gör oavsett vilken databas projektet råkar köra.
