# Dockerisera Spring Boot Calculator-applikation med Maven och Multistage-build

🟢


### Steg 1: Skapa en Dockerfile

Skapa en fil med namnet `Dockerfile` i roten av ditt projekt med följande innehåll:

```dockerfile
# Dockerfile med Multistage-build för att bygga applikationen direkt från källkoden

# Steg 1
# Använd Maven 3.9.9 och Amazon Corretto 21 som bas image för att bygga applikationen och namnge den som build så att vi kan anropa den ifrån steg 2
FROM maven:3.9.9-amazoncorretto-21-debian AS build
# Skapa och sätt arbetsmapp
WORKDIR /app

# Kopiera pom.xml
COPY pom.xml .
# Ladda ner beroenden
RUN mvn dependency:go-offline

# Kopiera över källkoden till arbetsmappen
COPY src ./src
# Bygg applikationen med Maven
RUN mvn clean package -DskipTests

# Steg 2
# Använd en slimmad Amazon Corretto 21 som bas image för att starta applikationen
FROM amazoncorretto:21.0.5-alpine
# Skapa och sätt arbetsmapp
WORKDIR /app
# Kopiera över applikationen från steg 1 "build" och döp den till app.jar
COPY --from=build /app/target/*.jar app.jar
# Starta applikationen
CMD ["java", "-jar", "app.jar"]
```

### Steg 2: Bygg Docker-image

Bygg Docker-imagen med följande kommando:

```sh
docker build -t cat-api-app-multistage .
```

### Steg 3: Kör Docker-container

Kör Docker-containern med följande kommando:

```sh
docker run -d -p 8080:8080 cat-api-app-multistage
```

### Steg 4: Testa applikationen

Öppna din webbläsare och navigera till `http://localhost:8080/api/v1/cat` för att testa addition, och använd liknande URL:er för subtraktion, multiplikation och division.

Eller använd `http://localhost:8080/swagger-ui/index.html#/` för att testa API:et med Swagger UI.

### Komplett projektstruktur

Efter att ha lagt till Docker-support, ser din projektstruktur ut ungefär så här:

### Sammanfattning av kommandon

1. Bygg jar-filen:
   ```sh
   mvn clean package
   ```

2. Bygg Docker-imagen:
   ```sh
   docker build -t calculator-app .
   ```

3. Kör Docker-containern:
   ```sh
   docker run -d -p 8080:8080 calculator-app
   ```

### Rensa efter dig

När du är klar med att testa applikationen, stäng av Docker-containern:

```sh
docker stop <container-id>
```

Och ta bort containern:

```sh
docker rm <container-id>
```
