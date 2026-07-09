# Installera MySQL och phpMyAdmin med Docker

🟢


## Beskrivning av uppgiften

Målet är att installera **MySQL** och **phpMyAdmin** på en lokal Docker-miljö. Vi kommer att skapa ett internt nätverk i Docker där phpMyAdmin kan ansluta till MySQL-servern. Vi ska använda standard `pull` och `run` kommandon för Docker, istället för att använda Docker Compose.

## Tekniker som ska användas

- **Docker:** Användning av Docker-kommandon för att skapa och hantera containrar.
- **Docker Networking:** Skapa ett anpassat nätverk för att koppla samman containrar.
- **MySQL:** Konfigurera MySQL-inställningar via miljövariabler.
- **phpMyAdmin:** Installera och konfigurera phpMyAdmin för att ansluta till MySQL.

## Steg-för-steg Guide

### Steg 1: Skapa ett Docker-nätverk
Först skapar vi ett anpassat nätverk i Docker. Detta gör att våra containrar enkelt kan kommunicera med varandra.

```bash
docker network create mysql-net
```

### Steg 2: Starta MySQL-Containern
Kör MySQL i en container och konfigurera den med de nödvändiga miljövariablerna.

```bash
docker run --name mysqlCampus --network mysql-net -e MYSQL_ROOT_PASSWORD=Campus -e MYSQL_USER=JIN -e MYSQL_PASSWORD=Campus -d -p 3306:3306  mysql:latest
```

För att logga in på MySQL-servern, använd följande kommando:

```bash
docker exec -it mysqlCampus mysql -u root -p
```

Skapa en användare (exempelvis ditt namn) och tilldela lösenord och ge den alla privilegier:
```sql
CREATE USER 'Marcus'@'localhost' IDENTIFIED BY 'lösenord';
GRANT ALL PRIVILEGES ON *.* TO 'Marcus'@'%' IDENTIFIED BY 'lösenord';
```

Nu kan vi lämna MySQL consolen och återgå till den vanliga consolen
```
exit
```

### Steg 3: Starta phpMyAdmin-Containern
Starta phpMyAdmin och koppla den till det nätverk vi skapade, samt konfigurera den att ansluta till MySQL-servern.   
_Glöm inte att lämna MySQL consolen om du är kvar därinne, skriv `exit;`_

```bash
docker run --name myadmin --network mysql-net -d -e PMA_HOST=mysqlCampus -p 8080:80 phpmyadmin/phpmyadmin
```

### Steg 4: Verifiera Installationen
För att kontrollera att allt fungerar, öppna webbläsaren och gå till `http://localhost:8080`. Du bör nu kunna logga in på phpMyAdmin med användarnamnet `JIN` och lösenordet `Campus`.

## Lägga till ytterligare containrar

För att lägga till ytterligare Docker-images till samma nätverk och så att de kan hanteras genom samma phpMyAdmin-instans, bör du följa dessa steg:

1. **Ansluta Nya Containrar till Nätverket:** När du startar nya containrar som behöver ansluta till MySQL-servern, se till att de är anslutna till det samma nätverket `mysql-net`. Detta gör du genom att lägga till `--network mysql-net` i ditt `docker run`-kommando. Exempel:

```bash
docker run --name another-service --network mysql-net -d some-image
```

2. **Konfigurera Access:** Om de nya tjänsterna behöver tillgång till MySQL, konfigurera dem med rätt databas-credentials och hostnamn (`mysqlCampus` i det här fallet). Detta kan variera beroende på vilken image du använder, men oftast innebär det att sätta miljövariabler eller uppdatera en konfigurationsfil inuti containern.

3. **Administrera via phpMyAdmin:** PhpMyAdmin är nu inställd för att ansluta till MySQL-servern. För att administrera nya databaser eller tabeller som skapats av andra containrar, logga in på phpMyAdmin som du gjort tidigare. Du bör kunna se och hantera alla databaser på MySQL-servern.

4. **Kontrollera Nätverkskommunikation:** Ibland kan det vara nödvändigt att kontrollera att nätverkskommunikationen fungerar som den ska mellan containrarna. Du kan göra detta genom att använda `docker exec` för att ansluta till en container och sedan använda nätverksverktyg som `ping` eller `mysql-client` för att testa anslutningen till MySQL-servern.

Genom att följa dessa steg kan du enkelt lägga till och hantera flera Docker-containrar inom samma nätverk, där alla kan interagera med samma MySQL-instans och hanteras via samma phpMyAdmin-gränssnitt.

### Exempel

För att visa hur detta fungerar i praktiken, ska vi skapa en mediawiki container och ansluta den till samma nätverk som MySQL och phpMyAdmin. Vi kommer att använda den officiella mediawiki-bilden från Docker Hub.

```bash
docker run --name mediawiki --network mysql-net -e MEDIAWIKI_DB_HOST=mysqlCampus -e MEDIAWIKI_DB_USER=JIN -e MEDIAWIKI_DB_PASSWORD=Campus -e MEDIAWIKI_DB_NAME=mediawiki -p 8081:80 -d mediawiki
```

Vi har nu en mediawiki-container som är ansluten till samma nätverk som MySQL och phpMyAdmin. Vi har också konfigurerat den att ansluta till MySQL-servern och skapa en ny databas för mediawiki. Om vi nu går till `http://localhost:8081` i webbläsaren, bör vi kunna se mediawiki-installationssidan.

Ange följande värden för att slutföra installationen:

- **Database host:** mysqlCampus
- **Database name:** mediawiki
- **Database username:** JIN
- **Database password:** Campus

När konfigurationen är klar kan du ladda upp din localsettings.php-fil till servern. Det är den filen som laddades ner när du konfigurerat sin installation. Om du inte har den filen, kan du ladda ner den från din mediawiki-container med följande kommando:

```bash
docker cp mediawiki:/var/www/html/LocalSettings.php .
```

Du kan nu logga in på mediawiki med användarnamnet `Admin` och lösenordet `Campus`.

för att skicka filen till din docker container kör du följande kommando:

```bash
docker cp LocalSettings.php mediawiki:/var/www/html/LocalSettings.php
```

från windows borde det bli något i stil med
    
```bash
docker cp Downloads\LocalSettings.php mediawiki:/var/www/html/LocalSettings.php
```

### Wordpress

Vi ska prova om det funkar med Wordpress också. Vi använder den officiella Wordpress-bilden från Docker Hub.

```bash
docker run --name wordpress --network mysql-net -e WORDPRESS_DB_HOST=mysqlCampus -e WORDPRESS_DB_USER=JIN -e WORDPRESS_DB_PASSWORD=Campus -e WORDPRESS_DB_NAME=wordpress -p 8082:80 -d wordpress
```

## Summan av kardemumman

Genom att följa dessa steg har du framgångsrikt installerat och konfigurerat en MySQL-server och phpMyAdmin i Docker. Ett internt nätverk används för att säkerställa smidig kommunikation mellan containrarna.

## Obligatorisk dad joke

Varför programmerare gillar naturen? Eftersom det inte finns några buggar!
