# MySQL på Docker

🟢


Nu ska vi docka MySQL och en hanterare till den, nämligen Marcus favoritprogram PHPMyAdmin.

## Ladda ner MySQL image

Kör detta i konsolen för att ladda ner MySQL image

```bash
docker run --name mysql_container -e MYSQL_ROOT_PASSWORD=admin2024 -e MYSQL_DATABASE=CampusDB -e MYSQL_USER=CampusStudent -e MYSQL_PASSWORD=fluffycat123 -p 3306:3306 -d mysql:latest
```

och sen

```bash	
docker run --name phpmyadmin_container --link mysql_container:db -p 8080:80 -e PMA_HOST=mysql_container -e MYSQL_ROOT_PASSWORD=admin2024 -d phpmyadmin/phpmyadmin:latest
```

och slutligen öppnar du browsern och går till `localhost:8080` och loggar in med användarnamn `root` och lösenord `admin2024`.

Yaaay!

Nu kan du använda MySQL också.

## Specialanvändare

För att använda en specialanvändare, öppna konsolen och skriv:
    
```bash 
docker exec -it mysql_container mysql -u root -p
GRANT ALL PRIVILEGES ON *.* TO 'CampusStudent'@'%' WITH GRANT OPTION;
FLUSH PRIVILEGES;
exit;
```

Nu har du en användare `CampusStudent` med lösenord `fluffycat123` som har alla rättigheter.


## Connectionstring

```plaintext
Server=localhost;Port=3306;Database=CampusDB;Uid=CampusStudent;Pwd=fluffycat123;
```

## Koppla dig till eländet

```plaintext
http://localhost:8080
user: CampusStudent
pass: fluffycat123
user: admin
pass: admin2024
```