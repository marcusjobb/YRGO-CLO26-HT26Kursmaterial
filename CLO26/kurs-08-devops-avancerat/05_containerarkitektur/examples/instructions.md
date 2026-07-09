# MySQL Docker

🟢


Här är en komplett guide som inkluderar steg för hur man kan ansluta till MySQL-databasen med IntelliJ IDEA.
Samt kommandon för att visa vilka databaser som finns, samt skapa och visa innehållet i databaserna.

### Steg 1: Skapa en projektmapp

Skapa en ny mapp för ditt MySQL Docker-projekt:

```sh
mkdir mysql-docker
cd mysql-docker
```

### Steg 2: Skapa en Dockerfile för MySQL

Skapa en fil med namnet `Dockerfile` med följande innehåll:

```dockerfile
# Dockerfile
FROM mysql:latest

# Ställ in miljövariabler för MySQL
ENV MYSQL_ROOT_PASSWORD=root
ENV MYSQL_DATABASE=mydb
ENV MYSQL_USER=user
ENV MYSQL_PASSWORD=password

# Exponera standard MySQL port
EXPOSE 3306

# Starta MySQL-servern
CMD ["mysqld"]
```

### Steg 3: Bygg Docker-imagen

Bygg Docker-imagen med följande kommando:

```sh
docker build -t my-mysql .
```

### Steg 4: Kör Docker-containern med automatisk städning

Kör Docker-containern med `--rm` flaggan för att automatiskt ta bort containern när den stoppas:

```sh
docker run --rm -d -p 3306:3306 --name my-mysql-container my-mysql
```

### Steg 5: Anslut till MySQL via kommandoraden

Anslut till MySQL-servern för att verifiera att allt fungerar korrekt:

```sh
mysql -h 127.0.0.1 -P 3306 -u user -p
```

Ange lösenordet när du blir ombedd (`password` i detta fall).

### Steg 6: Visa och hantera databaser

#### Visa alla databaser

För att visa alla databaser, använd följande kommando efter att du har anslutit till MySQL:

```sql
SHOW DATABASES;
```

#### Skapa en ny databas

För att skapa en ny databas, använd följande kommando:

```sql
CREATE DATABASE testdb;
```

#### Använd en databas

För att använda en specifik databas, använd följande kommando:

```sql
USE testdb;
```

#### Visa tabeller i en databas

För att visa alla tabeller i den aktuella databasen, använd följande kommando:

```sql
SHOW TABLES;
```

#### Skapa en tabell

För att skapa en ny tabell, använd följande kommando:

```sql
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL
);
```

#### Infoga data i tabellen

För att infoga data i tabellen, använd följande kommando:

```sql
INSERT INTO users (username, email, password) VALUES ('john_doe', 'john@example.com', 'securepassword');
```

#### Visa data i tabellen

För att visa data i tabellen, använd följande kommando:

```sql
SELECT * FROM users;
```

### Steg 7: Anslut till MySQL med IntelliJ IDEA

1. **Öppna IntelliJ IDEA**: Starta IntelliJ IDEA och öppna ditt projekt.

2. **Öppna Database Verktygsfönster**: Gå till `View` > `Tool Windows` > `Database`.

3. **Lägg till en ny Databasanslutning**:
   - Klicka på `+` ikonen i Database verktygsfönstret och välj `Data Source` > `MySQL`.
   - Om MySQL-anslutningsdrivrutinen inte redan är installerad, klicka på `Download Driver`.

4. **Konfigurera anslutningen**:
   - **Host:** `127.0.0.1`
   - **Port:** `3306`
   - **User:** `user`
   - **Password:** `password`
   - **Database:** `mydb` (eller välj den databas du vill ansluta till efter att ha anslutit)

5. **Testa anslutningen**: Klicka på `Test Connection` för att säkerställa att allt är korrekt konfigurerat. Om anslutningen är framgångsrik, klicka på `OK`.

6. **Använda databasen**: Nu ska du kunna se din databas i Database verktygsfönstret och köra SQL-frågor direkt från IntelliJ IDEA.

### Steg 8: Ta bort volymer (om några har skapats)

Om du har använt volymer för att lagra data, se till att ta bort dem efter att containern har stoppats. För att lista alla volymer:

```sh
docker volume ls
```

För att ta bort en specifik volym:

```sh
docker volume rm <volume_name>
```

### Komplett projektstruktur

Projektstrukturen ser ut så här:

```
mysql-docker/
└── Dockerfile
```

### Docker-kommandon sammanfattning

1. Bygg Docker-imagen:
   ```sh
   docker build -t my-mysql .
   ```

2. Kör Docker-containern med automatisk städning:
   ```sh
   docker run --rm -d -p 3306:3306 --name my-mysql-container my-mysql
   ```

3. Anslut till MySQL:
   ```sh
   mysql -h 127.0.0.1 -P 3306 -u user -p
   ```

4. Visa databaser:
   ```sql
   SHOW DATABASES;
   ```

5. Skapa en ny databas:
   ```sql
   CREATE DATABASE testdb;
   ```

6. Använd en databas:
   ```sql
   USE testdb;
   ```

7. Visa tabeller:
   ```sql
   SHOW TABLES;
   ```

8. Skapa en tabell:
   ```sql
   CREATE TABLE users (
       id INT AUTO_INCREMENT PRIMARY KEY,
       username VARCHAR(50) NOT NULL,
       email VARCHAR(50) NOT NULL,
       password VARCHAR(50) NOT NULL
   );
   ```

9. Infoga data:
   ```sql
   INSERT INTO users (username, email, password) VALUES ('john_doe', 'john@example.com', 'securepassword');
   ```

10. Visa data:
    ```sql
    SELECT * FROM users;
    ```

11. Anslut till MySQL med IntelliJ IDEA:
   - Öppna Database verktygsfönstret och lägg till en ny MySQL-anslutning.
   - Konfigurera anslutningen och testa den.

12. Ta bort volymer (om några):
    ```sh
    docker volume ls
    docker volume rm <volume_name>
    ```
    
13. Stoppa och ta bort containern:
    ```sh
    docker stop my-mysql-container
    docker rm my-mysql-container
    ```

Med dessa steg har du skapat och kört en standalone MySQL Docker-container, anslutit till den med IntelliJ IDEA, och visat hur du kan hantera databaser och tabeller. Låt mig veta om du behöver ytterligare hjälp!
