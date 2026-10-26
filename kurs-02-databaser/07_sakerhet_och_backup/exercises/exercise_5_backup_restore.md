# Övning 5: Backup & Restore 💾

🔴

**Fort Knox Protocol - Del 5 (FINAL)**

## Mål
Lär dig skapa, hantera och återställa MySQL-backups. I slutet kan du hantera disaster recovery som en pro och implementera automatiska backup-strategier!

## Förutsättningar
- Övning 2 klar (Heroes på MySQL)
- MySQL container körande med data

## Tidsåtgång
60-75 minuter

---

## Del 1: Varför Backup?

### Scenario-workshop

**Diskutera i par:** Vad skulle hända i dessa scenarion utan backup?

1. **🐛 Buggen:** Din kod har en bug som kör DELETE FROM Heroes utan WHERE
2. **💥 Hårdvara:** Servern kraschar och hårddisken dör
3. **🧑‍💻 Hackare:** Ransomware krypterar hela databasen
4. **👤 Mänskligt fel:** Du kör DROP TABLE i produktion istället för dev
5. **🔥 Katastrof:** Datacentret brinner ner

**Utan backup:**
- All data förlorad
- Verksamheten stoppad
- Kunder förlorar förtroende
- Företaget kan gå i konkurs

**Med backup:**
- Återställ senaste backup (max X timmars data förlorad)
- Verksamheten kan fortsätta
- Minimal påverkan

✅ **Checkpoint 1:** Du förstår vikten av backup!

---

## Del 2: 3-2-1 Backup Rule

**Guldregeln för backups:**

- **3** kopior av data
  - Original (produktionsdatabasen)
  - Lokal backup
  - Offsite backup

- **2** olika media
  - Hårddisk (lokal)
  - Cloud storage (AWS S3, Azure Blob, Google Cloud)
  - Eller: SSD + Spinning disk

- **1** offsite (geografiskt separerad)
  - Annan stad/land
  - Skyddar mot brand, översvämning, etc.

**Exempel:**
1. **Original:** MySQL i Docker på din server
2. **Lokal backup:** `/backups/` på samma server
3. **Cloud backup:** AWS S3 i annan region

✅ **Checkpoint 2:** Du förstår 3-2-1 regeln!

---

## Del 3: mysqldump Basics

### Steg 1: Enklaste backupen

```bash
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  > heroes_backup.sql
```

**Vad händer:**
1. `docker exec mysql-heroes` - kör kommando i containern
2. `mysqldump` - MySQL's backup-verktyg
3. `-uroot -pSuperSecret123` - autentisering
4. `heroesdb` - vilken databas att backa upp
5. `> heroes_backup.sql` - skicka output till fil

### Steg 2: Kolla filen

```bash
ls -lh heroes_backup.sql
```

**Förväntat resultat:**
```
-rw-r--r-- 1 user user 4.2K Jan 16 14:30 heroes_backup.sql
```

Storlek beror på hur mycket data du har!

### Steg 3: Inspektera innehållet

```bash
head -n 50 heroes_backup.sql
```

**Du ser:**
```sql
-- MySQL dump 10.13  Distrib 8.0.40, for Linux (x86_64)
--
-- Host: localhost    Database: heroesdb
-- ------------------------------------------------------
-- Server version	8.0.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
-- ... massa SET-kommandon ...

--
-- Table structure for table `Heroes`
--

DROP TABLE IF EXISTS `Heroes`;
CREATE TABLE `Heroes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RealName` longtext NOT NULL,
  `HeroName` longtext NOT NULL,
  `Phone` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `Heroes`
--

LOCK TABLES `Heroes` WRITE;
INSERT INTO `Heroes` VALUES
  (1,'Tony Stark','Iron Man','555-STARK'),
  (2,'Peter Parker','Spider-Man','555-WEB'),
  (3,'Thor Odinson','Thor','555-THUNDER');
UNLOCK TABLES;
```

**Komplett återställningsbar snapshot!**

✅ **Checkpoint 3:** Din första backup klar!

---

## Del 4: Backup Med Timestamp

**Problem:** `heroes_backup.sql` skrivs över varje gång!

### Steg 1: Backup med datum

```bash
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  > heroes_backup_$(date +%Y%m%d_%H%M%S).sql
```

**Resultat:**
```
heroes_backup_20250116_143022.sql
```

### Steg 2: Skapa flera backups

Kör kommandot 3 gånger (vänta 5 sekunder mellan varje):

```bash
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  > heroes_backup_$(date +%Y%m%d_%H%M%S).sql && sleep 5
```

### Steg 3: Lista backups

```bash
ls -lh heroes_backup_*.sql
```

**Förväntat resultat:**
```
heroes_backup_20250116_143022.sql
heroes_backup_20250116_143027.sql
heroes_backup_20250116_143032.sql
```

**Versionering!** Du kan gå tillbaka till olika tidpunkter!

✅ **Checkpoint 4:** Timestamps implementerat!

---

## Del 5: Disaster Recovery Drill

Nu simulerar vi en katastrof och återställning!

### Steg 1: Kolla nuvarande data

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT COUNT(*) AS 'Total Heroes' FROM Heroes;"
```

**Anteckna antalet!**

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT * FROM Heroes;"
```

**Anteckna vilka hjältar som finns!**

### Steg 2: SKAPA BACKUP (viktigt!)

```bash
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  > disaster_recovery_backup.sql
```

**✓ FALLSKÄRM KLAR!**

### Steg 3: SIMULERA KATASTROF 💥

**Välj ett scenario:**

**A) Bug - DELETE utan WHERE:**
```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "DELETE FROM Heroes;"
```

**B) Mänskligt fel - DROP TABLE:**
```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "DROP TABLE Heroes;"
```

**C) Total destruction - DROP DATABASE:**
```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 \
  -e "DROP DATABASE heroesdb;"
```

Kör ETT av dessa! (Jag rekommenderar A för första gången)

### Steg 4: PANIK! 😱

Kolla skadan:

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT COUNT(*) FROM Heroes;"
```

**Om du körde A (DELETE):**
```
COUNT(*) = 0
```

**Om du körde B (DROP TABLE):**
```
ERROR 1146: Table 'heroesdb.Heroes' doesn't exist
```

**Om du körde C (DROP DATABASE):**
```
ERROR 1049: Unknown database 'heroesdb'
```

**DATA BORTA!**

### Steg 5: ÅTERSTÄLLNING! 🚑

**Om du körde A eller B (databasen finns kvar):**

```bash
docker exec -i mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  < disaster_recovery_backup.sql
```

**Om du körde C (databasen borta):**

```bash
# Skapa databasen igen
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 \
  -e "CREATE DATABASE heroesdb;"

# Återställ data
docker exec -i mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  < disaster_recovery_backup.sql
```

### Steg 6: VERIFIERA! ✅

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT COUNT(*) FROM Heroes;"
```

**COUNT ska matcha originalet!**

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT * FROM Heroes;"
```

**ALLA HJÄLTAR TILLBAKA!** 🎉

**Andas ut.** Du räddade företaget! 😌

✅ **Checkpoint 5:** Disaster recovery lyckad!

---

## Del 6: Backup Entire Container

Förutom data vill vi också backa upp container-konfiguration!

### Steg 1: Export container

```bash
docker export mysql-heroes > mysql-heroes-container.tar
```

**Detta sparar:**
- Hela filsystemet i containern
- Alla config-filer
- MySQL-binärer
- Data (men använd mysqldump för data!)

### Steg 2: Kolla storlek

```bash
ls -lh mysql-heroes-container.tar
```

**Förväntat resultat:** ~500MB (hela MySQL-installationen!)

**Varning:** Detta är STORT! Bättre att använda mysqldump för data och Docker Compose för container-config.

### Steg 3: (Optional) Import container

Om du någonsin behöver återskapa:

```bash
docker import mysql-heroes-container.tar restored-mysql:latest
```

**Men bättre metod:** Använd Docker volumes!

✅ **Checkpoint 6:** Container export bemästrad!

---

## Del 7: Docker Volumes (Best Practice)

Persistent data som överlever container-borttagning!

### Steg 1: Stoppa och ta bort nuvarande container

**VARNING:** Gör BACKUP först!

```bash
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  > before_volume_migration.sql
```

```bash
docker stop mysql-heroes
docker rm mysql-heroes
```

### Steg 2: Skapa container med named volume

```bash
docker run --name mysql-heroes \
  -e MYSQL_ROOT_PASSWORD=SuperSecret123 \
  -e MYSQL_DATABASE=heroesdb \
  -p 3306:3306 \
  -v heroes-data:/var/lib/mysql \
  -d mysql:latest
```

**Ny del:** `-v heroes-data:/var/lib/mysql`
- `heroes-data` = named volume
- `/var/lib/mysql` = MySQL's data directory i containern

### Steg 3: Återställ data

```bash
# Vänta tills MySQL startar (ca 10 sekunder)
sleep 10

docker exec -i mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  < before_volume_migration.sql
```

### Steg 4: Testa persistence

Verifiera data:

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT * FROM Heroes;"
```

**Radera containern (men inte volumet!):**

```bash
docker stop mysql-heroes
docker rm mysql-heroes
```

**Skapa NY container med SAMMA volume:**

```bash
docker run --name mysql-heroes \
  -e MYSQL_ROOT_PASSWORD=SuperSecret123 \
  -e MYSQL_DATABASE=heroesdb \
  -p 3306:3306 \
  -v heroes-data:/var/lib/mysql \
  -d mysql:latest
```

**Kolla data:**

```bash
sleep 10
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT * FROM Heroes;"
```

**DATA FINNS KVAR TROTS ATT CONTAINERN RADERADES!** 🎉

✅ **Checkpoint 7:** Persistent volumes funkar!

---

## Del 8: Automatiska Backups

### Linux/Mac - Crontab

**Steg 1: Skapa backup-script**

`backup_heroes.sh`:

```bash
#!/bin/bash

# Konfiguration
BACKUP_DIR="/backups/heroes"
TIMESTAMP=$(date +%Y%m%d_%H%M%S)
CONTAINER_NAME="mysql-heroes"
DATABASE="heroesdb"
USER="root"
PASSWORD="SuperSecret123"
RETENTION_DAYS=7

# Skapa backup-katalog om den inte finns
mkdir -p $BACKUP_DIR

# Skapa backup
docker exec $CONTAINER_NAME mysqldump -u$USER -p$PASSWORD $DATABASE \
  > $BACKUP_DIR/heroes_$TIMESTAMP.sql

# Komprimera
gzip $BACKUP_DIR/heroes_$TIMESTAMP.sql

# Ta bort gamla backups (äldre än 7 dagar)
find $BACKUP_DIR -name "heroes_*.sql.gz" -mtime +$RETENTION_DAYS -delete

echo "Backup klar: heroes_$TIMESTAMP.sql.gz"
```

**Steg 2: Gör körbar**

```bash
chmod +x backup_heroes.sh
```

**Steg 3: Testa**

```bash
./backup_heroes.sh
```

**Steg 4: Lägg till i crontab**

```bash
crontab -e
```

Lägg till (varje natt kl 02:00):

```
0 2 * * * /path/to/backup_heroes.sh >> /var/log/heroes_backup.log 2>&1
```

### Windows - Task Scheduler

**Steg 1: Skapa backup.bat**

```batch

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
@echo off
SET BACKUP_DIR=C:\backups\heroes
SET TIMESTAMP=%date:~0,4%%date:~5,2%%date:~8,2%_%time:~0,2%%time:~3,2%%time:~6,2%
SET TIMESTAMP=%TIMESTAMP: =0%

mkdir %BACKUP_DIR% 2>nul

docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb ^
  > %BACKUP_DIR%\heroes_%TIMESTAMP%.sql

echo Backup klar: heroes_%TIMESTAMP%.sql
```

**Steg 2: Schemalägg i Task Scheduler**

1. Öppna Task Scheduler
2. Create Basic Task → "Heroes MySQL Backup"
3. Trigger: Daily, 02:00
4. Action: Start a program → `C:\path\to\backup.bat`
5. Finish!

✅ **Checkpoint 8:** Automatiska backups konfigurerade!

---

## 🟢 Uppgift 1: Retention Testing (Basic)

Skapa 10 backup-filer med olika timestamps:

```bash
for i in {1..10}; do
  docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
    > heroes_test_$i.sql
  sleep 1
done
```

Skapa ett script som:
1. Listar alla backups
2. Behåller de 5 senaste
3. Raderar de äldre

**Hint:** Använd `ls -t` för att sortera efter tid!

---

## 🟡 Uppgift 2: Backup Verification (Advanced)

Backups är värdelösa om de inte går att återställa! Skapa ett script som:

1. Skapar backup
2. Skapar en TEST-databas
3. Återställer backupen till TEST-databasen
4. Kör validering (räkna rader, checksums)
5. Rapporterar om backupen är OK eller korrupt

**Pseudokod:**

```bash
# Backup
mysqldump heroesdb > backup.sql

# Restore till test
mysql -e "CREATE DATABASE heroesdb_test;"
mysql heroesdb_test < backup.sql

# Validera
PROD_COUNT=$(mysql heroesdb -e "SELECT COUNT(*) FROM Heroes;")
TEST_COUNT=$(mysql heroesdb_test -e "SELECT COUNT(*) FROM Heroes;")

if [ "$PROD_COUNT" == "$TEST_COUNT" ]; then
  echo "✅ Backup verifierad!"
else
  echo "❌ Backup korrupt!"
fi

# Cleanup
mysql -e "DROP DATABASE heroesdb_test;"
```

---

## 🔴 Uppgift 3: Offsite Backup (Pro)

Implementera offsite backup till AWS S3 (eller motsvarande):

### Steg 1: Installera AWS CLI

```bash
# Linux/Mac
curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "awscliv2.zip"
unzip awscliv2.zip
sudo ./aws/install

# Windows
# Ladda ner från: https://aws.amazon.com/cli/
```

### Steg 2: Konfigurera credentials

```bash
aws configure
# AWS Access Key ID: [din key]
# AWS Secret Access Key: [din secret]
# Default region: eu-north-1
```

### Steg 3: Skapa backup och ladda upp

```bash
#!/bin/bash

# Backup
TIMESTAMP=$(date +%Y%m%d_%H%M%S)
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  | gzip > heroes_$TIMESTAMP.sql.gz

# Ladda upp till S3
aws s3 cp heroes_$TIMESTAMP.sql.gz s3://your-backup-bucket/heroes/

# Verifiera
aws s3 ls s3://your-backup-bucket/heroes/

# Lokal kopia i 7 dagar, S3 i 90 dagar
find . -name "heroes_*.sql.gz" -mtime +7 -delete
```

**Bonus:** Sätt upp S3 Lifecycle Policy för automatisk arkivering till Glacier efter 30 dagar!

---

## 🎯 Bonus: Point-in-Time Recovery

MySQL har binary logs för PITR!

### Steg 1: Aktivera binary logging

**docker-compose.yml:**

```yaml
version: '3.8'

services:
  mysql:
    image: mysql:latest
    container_name: mysql-heroes
    environment:
      MYSQL_ROOT_PASSWORD: SuperSecret123
      MYSQL_DATABASE: heroesdb
    ports:
      - "3306:3306"
    volumes:
      - heroes-data:/var/lib/mysql
      - ./mysql-conf:/etc/mysql/conf.d
    command: --log-bin=mysql-bin --server-id=1

volumes:
  heroes-data:
```

### Steg 2: Testa PITR

1. Gör full backup kl 10:00
2. Lägg till data kl 10:30
3. Radera data av misstag kl 11:00
4. Återställ full backup (10:00)
5. Applica binary logs från 10:00 till 10:59
6. Data från 10:30 finns kvar, men inte DELETE från 11:00!

**Detta är avancerat - för nyfikna!**

---

## Sammanfattning

Du har nu:
- ✅ Förstått vikten av backups (3-2-1 regeln)
- ✅ Skapat backups med mysqldump
- ✅ Implementerat timestamps för versionering
- ✅ Genomfört disaster recovery drill
- ✅ Lärt dig container export
- ✅ Använt Docker volumes för persistence
- ✅ Skapat automatiska backup-scripts
- ✅ (Bonus) Implementerat offsite backup
- ✅ (Bonus) Lärt dig PITR

**GRATTIS! Du har klarat Fort Knox Protocol! 🔐🎉**

---

## Troubleshooting

### Problem: "No such file or directory" vid restore

**Lösning:**
Kolla att backupfilen finns:

```bash
ls -lh disaster_recovery_backup.sql
```

Om den finns, kolla sökvägen (relativ vs absolut):

```bash
# Relativ
docker exec -i mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  < ./disaster_recovery_backup.sql

# Absolut
docker exec -i mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  < /full/path/to/disaster_recovery_backup.sql
```

### Problem: Restore hänger sig

**Lösning:**
Glöm inte `-i` flaggan!

```bash
# FEL (hänger):
docker exec mysql-heroes mysql -uroot -pSuperSecret123 heroesdb < backup.sql

# RÄTT:
docker exec -i mysql-heroes mysql -uroot -pSuperSecret123 heroesdb < backup.sql
```

### Problem: "Got error: 1045: Access denied"

**Lösning:**
Dubbelkolla lösenord i både backup och restore:

```bash
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb > backup.sql
docker exec -i mysql-heroes mysql -uroot -pSuperSecret123 heroesdb < backup.sql
```

Lösenorden måste matcha!

---

## Diskussionsfrågor

1. **Hur ofta ska man ta backups?**
   - Beror på verksamhetskrav
   - Daglig? Varje timme? Continuous (binary logs)?
   - Balans mellan diskutrymme och data loss tolerance

2. **Vad är skillnaden mellan full och incremental backup?**
   - Full: Hela databasen varje gång
   - Incremental: Bara ändringar sedan senaste backup
   - mysqldump = full, binary logs = incremental

3. **Varför testa återställning regelbundet?**
   - Backups kan vara korrupta
   - Processen kan ha ändrats
   - "Untested backup = no backup"

4. **3-2-1 regeln - är det overkill för små projekt?**
   - Beror på value of data
   - Hobby-projekt: Kanske OK med 1 backup
   - Produktion: ALLTID 3-2-1 (eller mer!)

Diskutera i grupp!

---

## Pappaskämt-paus 😄

**Fråga:** Varför gillade databasen att ta backup?
**Svar:** För att det kändes skönt att ha något att falla tillbaka på!

**Fråga:** Vad sa backup-scriptet till kraschen?
**Svar:** "Jag räddade dina bytes!"

**Fråga:** Varför var backup-filen ledsen?
**Svar:** För att den aldrig blev använd - men det är ju BRA!

---

## 🏆 Fort Knox Certificate

**GRATTIS!** Du har genomfört alla 5 övningar i Fort Knox Protocol!

Du kan nu:
- ✅ Sätta upp MySQL i Docker
- ✅ Migrera från SQLite till MySQL
- ✅ Skydda mot SQL Injection
- ✅ Implementera Least Privilege med användarbehörigheter
- ✅ Skapa, hantera och återställa backups

**Din databas är nu säker som Fort Knox! 🔐**

**Nästa nivå:**
- Replication (master-slave setup)
- Load balancing med ProxySQL
- Monitoring med Prometheus + Grafana
- High Availability med Kubernetes

**Men först - fira! Du har lärt dig produktionsklar databashantering! 🎉**

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
