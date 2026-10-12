# Docker-kommandon

🟢

En snabbreferens för de Docker-kommandon du använder mest — containers, compose och databasspecifika kommandon.

---

## Grundläggande Docker-kommandon

```bash
# Visa alla körande containers
docker ps

# Visa alla containers (även stoppade)
docker ps -a

# Starta en container
docker start <container_name>

# Stoppa en container
docker stop <container_name>

# Ta bort en container
docker rm <container_name>

# Visa loggar från en container
docker logs <container_name>

# Följ loggar live
docker logs -f <container_name>

# Kör kommando i körande container
docker exec -it <container_name> <command>

# Öppna bash-shell i container
docker exec -it <container_name> bash

# Kopiera fil från container till host
docker cp <container_name>:/path/in/container ./local/path

# Kopiera fil från host till container
docker cp ./local/path <container_name>:/path/in/container
```

---

## Docker Compose-kommandon

```bash
# Starta alla services (detached mode)
docker-compose up -d

# Stoppa alla services
docker-compose down

# Stoppa och ta bort volymer (RADERAR DATA!)
docker-compose down -v

# Visa loggar från alla services
docker-compose logs

# Följ loggar live
docker-compose logs -f

# Starta om alla services
docker-compose restart

# Visa status på services
docker-compose ps

# Bygg om images
docker-compose build
```

---

## Databasspecifika kommandon

### MySQL

```bash
# Kör MySQL-shell
docker exec -it mysql_dev mysql -u root -p

# Backup
docker exec mysql_dev mysqldump -u root -p myapp > backup.sql

# Restore
docker exec -i mysql_dev mysql -u root -p myapp < backup.sql
```

### SQL Server

```bash
# Kör sqlcmd
docker exec -it sqlserver_dev /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'YourPassword'

# Backup
docker exec sqlserver_dev /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'pass' -Q "BACKUP DATABASE MyApp TO DISK='/var/opt/mssql/backup/MyApp.bak'"
```

### MongoDB

```bash
# Kör mongosh
docker exec -it mongodb_dev mongosh -u admin -p adminpassword

# Backup
docker exec mongodb_dev mongodump --username admin --password adminpassword --db myapp --out /dump

# Restore
docker exec mongodb_dev mongorestore --username admin --password adminpassword --db myapp /dump/myapp
```

---

Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
