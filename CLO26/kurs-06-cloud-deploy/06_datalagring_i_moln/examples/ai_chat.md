Assistant: Hello! What coding task do you need help with?

User: jag har två Dockerfile som ser ut så här:

```dockefile

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
# Dockerfile med Multistage-build för att bygga applikationen direkt från källkoden

🟢


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
# Bygg applikationen med Maven till en jar-fil
RUN mvn clean package -DskipTests

# Steg 2
# Använd en slimmad Amazon Corretto 21 som bas image för att starta applikationen
FROM amazoncorretto:21.0.5-alpine
# Skapa och sätt arbetsmapp
WORKDIR /app

# Installera curl för health checks
RUN apk --no-cache add curl

# Kopiera över applikationen från steg 1 "build" och döp den till app.jar
COPY --from=build /app/target/*.jar app.jar
# Exponera porten som applikationen kommer att köras på
EXPOSE 8080
# Starta applikationen
CMD ["java", "-jar", "app.jar"]
```

```dockefile
# Byggsteg
FROM node:20-alpine as builder
WORKDIR /app
COPY . .
RUN npm install && npm run build

# Produktionssteg
FROM nginx:alpine
COPY --from=builder /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80

```

docker-compose.yml

```yml
services:
  frontend:
    build: ./cat_frontend
    container_name: cat-frontend
    ports:
      - '${FRONTEND_PORT}:80'
    environment:
      - VITE_APP_API_URL=${FRONTEND_API_URL}
    depends_on:
      backend:
        condition: service_healthy
    networks:
      - cat-network

  backend:
    build: ./cat_api
    container_name: cat-backend
    ports:
      - '${BACKEND_PORT}:8080'
    volumes:
      - h2-data:/app/data
    environment:
      - SPRING_DATASOURCE_URL=${BACKEND_DB_URL}
      - SPRING_DATASOURCE_DRIVER_CLASS_NAME=org.h2.Driver
      - SPRING_DATASOURCE_USERNAME=${DB_USERNAME}
      - SPRING_DATASOURCE_PASSWORD=${DB_PASSWORD}
      - SPRING_JPA_DATABASE_PLATFORM=org.hibernate.dialect.H2Dialect
      - SPRING_JPA_HIBERNATE_DDL_AUTO=update
      - SPRING_H2_CONSOLE_ENABLED=true
      - SPRING_H2_CONSOLE_PATH=/h2-console
    healthcheck:
      test: ['CMD', 'curl', '-f', 'http://localhost:8080/']
      interval: 10s
      timeout: 5s
      retries: 5
      start_period: 30s
    networks:
      - cat-network

networks:
  cat-network:
    driver: bridge

volumes:
  h2-data:

```

hur ska jag göra om den för att kunna skicka in variabeler likt man gör med:
`$ docker run --name some-mysql -e MYSQL_ROOT_PASSWORD=my-secret-pw -d mysql:tag`

när jag använder docker-compose?

Assistant: För att skicka in variabler till containrar när du använder Docker Compose, finns det primärt två sätt:

1. Genom `.env`-filer (som du redan delvis använder)
2. Genom att direkt ange miljövariabler i `docker-compose.yml`

Här är hur du kan uppdatera ditt system:

```yml
version: '3.8'

services:
  frontend:
    build:
      context: ./cat_frontend
      args:
        - API_URL=${FRONTEND_API_URL}
    container_name: cat-frontend
    ports:
      - '${FRONTEND_PORT:-80}:80'
    environment:
      - VITE_APP_API_URL=${FRONTEND_API_URL}
    depends_on:
      backend:
        condition: service_healthy
    networks:
      - cat-network

  backend:
    build: ./cat_api
    container_name: cat-backend
    ports:
      - '${BACKEND_PORT:-8080}:8080'
    volumes:
      - h2-data:/app/data
    environment:
      - SPRING_DATASOURCE_URL=${BACKEND_DB_URL}
      - SPRING_DATASOURCE_DRIVER_CLASS_NAME=${DB_DRIVER:-org.h2.Driver}
      - SPRING_DATASOURCE_USERNAME=${DB_USERNAME}
      - SPRING_DATASOURCE_PASSWORD=${DB_PASSWORD}
      - SPRING_JPA_DATABASE_PLATFORM=${DB_PLATFORM:-org.hibernate.dialect.H2Dialect}
      - SPRING_JPA_HIBERNATE_DDL_AUTO=${DB_DDL_AUTO:-update}
      - SPRING_H2_CONSOLE_ENABLED=${H2_CONSOLE_ENABLED:-true}
      - SPRING_H2_CONSOLE_PATH=${H2_CONSOLE_PATH:-/h2-console}
      - CUSTOM_PARAMETER=${CUSTOM_PARAMETER:-default_value}
    healthcheck:
      test: ['CMD', 'curl', '-f', 'http://localhost:8080/']
      interval: 10s
      timeout: 5s
      retries: 5
      start_period: 30s
    networks:
      - cat-network

networks:
  cat-network:
    driver: bridge

volumes:
  h2-data:
```

Sedan skapar du en `.env`-fil i samma katalog som din `docker-compose.yml`:

```
# Frontend konfiguration
FRONTEND_PORT=80
FRONTEND_API_URL=http://localhost:8080

# Backend konfiguration
BACKEND_PORT=8080
BACKEND_DB_URL=jdbc:h2:file:/app/data/catdb
DB_USERNAME=sa
DB_PASSWORD=password
DB_DRIVER=org.h2.Driver
DB_PLATFORM=org.hibernate.dialect.H2Dialect
DB_DDL_AUTO=update
H2_CONSOLE_ENABLED=true
H2_CONSOLE_PATH=/h2-console

# Anpassad parameter
CUSTOM_PARAMETER=my_value
```

Med denna inställning kan du:

1. Starta med standardvärden: `docker-compose up -d`
2. Åsidosätta variabler vid start: `DB_PASSWORD=new_password docker-compose up -d`
3. Använda en annan .env-fil: `docker-compose --env-file custom.env up -d`

Jag har lagt till standardvärden med syntaxen `${VAR:-default}` så att tjänsterna startar även om vissa variabler saknas.

User: jag använder redan .env som du säger men när jag vill skicka upp bygget till dockerhub eller online så vill jag inte lagra variablerna i en env fil

Assistant: ```dockerfile

# Skapa en docker-compose.yml för att publicera och köra utan .env fil

version: '3.8'

services:
  frontend:
    image: ${DOCKER_REGISTRY:-yourusername}/cat-frontend:${TAG:-latest}
    build:
      context: ./cat_frontend
      args:
        - API_URL=${FRONTEND_API_URL:-http://backend:8080}
    container_name: cat-frontend
    ports:
      - '${FRONTEND_PORT:-80}:80'
    environment:
      - VITE_APP_API_URL=${FRONTEND_API_URL:-<http://backend:8080}>
    depends_on:
      backend:
        condition: service_healthy
    networks:
      - cat-network

  backend:
    image: ${DOCKER_REGISTRY:-yourusername}/cat-backend:${TAG:-latest}
    build: ./cat_api
    container_name: cat-backend
    ports:
      - '${BACKEND_PORT:-8080}:8080'
    volumes:
      - h2-data:/app/data
    environment:
      - SPRING_DATASOURCE_URL=${BACKEND_DB_URL:-jdbc:h2:file:/app/data/catdb}
      - SPRING_DATASOURCE_DRIVER_CLASS_NAME=${DB_DRIVER:-org.h2.Driver}
      - SPRING_DATASOURCE_USERNAME=${DB_USERNAME:-sa}
      - SPRING_DATASOURCE_PASSWORD=${DB_PASSWORD:-password}
      - SPRING_JPA_DATABASE_PLATFORM=${DB_PLATFORM:-org.hibernate.dialect.H2Dialect}
      - SPRING_JPA_HIBERNATE_DDL_AUTO=${DB_DDL_AUTO:-update}
      - SPRING_H2_CONSOLE_ENABLED=${H2_CONSOLE_ENABLED:-true}
      - SPRING_H2_CONSOLE_PATH=${H2_CONSOLE_PATH:-/h2-console}
    healthcheck:
      test: ['CMD', 'curl', '-f', 'http://localhost:8080/']
      interval: 10s
      timeout: 5s
      retries: 5
      start_period: 30s
    networks:
      - cat-network

networks:
  cat-network:
    driver: bridge

volumes:
  h2-data:

```

User: hur använder jag nu denna docker compose utan env?

Assistant: ```bash
# För att bygga och push:a images till Docker Hub
docker-compose build
docker-compose push

# För att köra utan .env filen med standardvärden
docker-compose up -d

# För att köra med specifika värden direkt från kommandoraden
docker-compose up -d \
  -e DB_USERNAME=admin \
  -e DB_PASSWORD=secure_password \
  -e BACKEND_DB_URL=jdbc:h2:file:/app/data/production_db

# Alternativt, ange alla variabler på samma rad
DB_USERNAME=admin DB_PASSWORD=secure_password BACKEND_DB_URL=jdbc:h2:file:/app/data/production_db docker-compose up -d

# För deployment till en server, skapa en deploy.sh script
docker-compose \
  -e DOCKER_REGISTRY=yourcompany \
  -e TAG=v1.0 \
  -e DB_USERNAME=production_user \
  -e DB_PASSWORD=production_pass \
  -e BACKEND_DB_URL=jdbc:h2:file:/app/data/prod_db \
  up -d
```

User: kanon. kan du även beskriva flödet för hur jag använder detta i portainer

Assistant: # Arbetsgång för Docker Compose i Portainer

```
# 1. Bygg dina images lokalt
docker-compose build

# 2. Tagga och pusha images till Docker Hub
docker-compose push
```

När du har dina images på Docker Hub, så här använder du dem i Portainer:

1. **Logga in på Portainer**
   - Gå till din Portainer instans (vanligtvis på `http://your-server-ip:9000`)

2. **Skapa en Stack**
   - Gå till "Stacks" i sidofältet
   - Klicka på "Add stack"
   - Ge din stack ett namn (t.ex. "cat-application")

3. **Lägg till din docker-compose.yml**
   - Välj "Web editor" eller "Upload"
   - Klistra in din docker-compose.yml i editorn eller ladda upp filen

4. **Konfigurera miljövariabler**
   - I Portainer, under "Environment variables" sektionen, lägg till dina variabler:
     - DOCKER_REGISTRY = ditt-registry-namn
     - TAG = önskad-version
     - DB_USERNAME = produktionsanvändare
     - DB_PASSWORD = säkert-lösenord
     - Osv för andra variabler du behöver anpassa

5. **Klicka på "Deploy the stack"**

6. **Hantera din stack**
   - Du kan nu se och hantera din stack i Portainer
   - Övervaka containers, se logs och hantera deployment

7. **Uppdatera stacken**
   - När du behöver uppdatera:
     - Bygg nya images lokalt
     - Pusha till registry med ny tag
     - I Portainer, uppdatera TAG miljövariabel
     - Klicka på "Update the stack"

8. **Enkel skalning i Portainer**
   - Gå till din stack
   - Klicka på service
   - Ändra replicas
   - Klicka på "Scale"

Fördelen med denna metod är att du aldrig behöver lagra känsliga variabler i filer eller kod, utan allt konfigureras direkt i Portainer's UI.

User: kanon, jag har en calculator spring boot app som har följande application.properties:

```properties
# Configuration for the Calculator service
spring.application.name=2_calculator

# Server Port
server.port=8080

# Swagger Configuration
springdoc.swagger-ui.operationsSorter=method
```

Kan du göra porten och springdoc.swagger-ui.operationsSorter sättbara via dena dockerfile:

```dockerfile
# Multi stage docker file for calculator application

########################################################
# Stage 1: Build the application
########################################################

FROM maven:3.9.7-amazoncorretto-21 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the pom.xml file and download the dependencies
COPY pom.xml .
RUN mvn dependency:go-offline -B

# Copy the entire project and build the application
COPY src ./src
RUN mvn clean package -DskipTests

########################################################
# Stage 2: Create the final image
########################################################
FROM amazoncorretto:21

# Set the working directory inside the container
WORKDIR /app

# Copy the jar file from the previous stage
COPY --from=build /app/target/calculator-0.0.1-SNAPSHOT.jar app.jar
# Efter detta steget så finns app.jar i mappen /app på sökvägen /app/app.jar

# Expose the port the application runs on
EXPOSE 8080

# Command to run the application
ENTRYPOINT ["java", "-jar", "app.jar"]
```

Ge mig även docker run strängen jag behöver för att starta applikationen

Assistant: ```dockerfile

# Multi stage docker file for calculator application

########################################################

# Stage 1: Build the application

########################################################

FROM maven:3.9.7-amazoncorretto-21 AS build

# Set the working directory inside the container

WORKDIR /app

# Copy the pom.xml file and download the dependencies

COPY pom.xml .
RUN mvn dependency:go-offline -B

# Copy the entire project and build the application

COPY src ./src
RUN mvn clean package -DskipTests

########################################################

# Stage 2: Create the final image

########################################################
FROM amazoncorretto:21

# Set the working directory inside the container

WORKDIR /app

# Copy the jar file from the previous stage

COPY --from=build /app/target/calculator-0.0.1-SNAPSHOT.jar app.jar

# Install curl for health checks

RUN microdnf install -y curl && microdnf clean all

# Expose the port the application runs on

EXPOSE 8080

# Command to run the application

ENTRYPOINT ["java", "-Dserver.port=${SERVER_PORT:8080}", "-Dspringdoc.swagger-ui.operationsSorter=${SWAGGER_SORTER:method}", "-jar", "app.jar"]

```

Docker run-kommando för att starta applikationen:

```bash
docker run -d \
  --name calculator \
  -p 8080:8080 \
  -e SERVER_PORT=8080 \
  -e SWAGGER_SORTER=method \
  your-image-name:tag
```

Om du vill ändra port och sorteringsmetod:

```bash
docker run -d \
  --name calculator \
  -p 9090:9090 \
  -e SERVER_PORT=9090 \
  -e SWAGGER_SORTER=alpha \
  your-image-name:tag
```

User: kan du förklara denna raden "ENTRYPOINT ["java", "-Dserver.port=${SERVER_PORT:8080}", "-Dspringdoc.swagger-ui.operationsSorter=${SWAGGER_SORTER:method}", "-jar", "app.jar"]"

Assistant: Den ENTRYPOINT-raden definierar kommandot som körs när containern startar:

```dockerfile
ENTRYPOINT ["java", "-Dserver.port=${SERVER_PORT:8080}", "-Dspringdoc.swagger-ui.operationsSorter=${SWAGGER_SORTER:method}", "-jar", "app.jar"]
```

Förklaring del för del:

1. `"java"` - Startar Java runtime

2. `"-Dserver.port=${SERVER_PORT:8080}"` - Sätter JVM system property `server.port`:
   - `${SERVER_PORT:8080}` är Docker's syntax för miljövariabler med standardvärde
   - Om miljövariabeln `SERVER_PORT` är definierad, används dess värde
   - Om `SERVER_PORT` inte är definierad, används standardvärdet `8080`
   - Dessa JVM system properties (`-D`) åsidosätter värden i application.properties

3. `"-Dspringdoc.swagger-ui.operationsSorter=${SWAGGER_SORTER:method}"` - Sätter Swagger-konfigurationen:
   - Använder värdet från miljövariabeln `SWAGGER_SORTER` om den finns
   - Annars används standardvärdet `method`

4. `"-jar", "app.jar"` - Kör JAR-filen som innehåller din applikation

Detta tillåter konfiguration av applikationen genom miljövariabler utan att behöva ändra application.properties eller bygga om applikationen. När du kör containern med Docker kan du ändra dessa värden:

```bash
docker run -e SERVER_PORT=9090 -e SWAGGER_SORTER=alpha ...
```

Denna metod är standard för att göra containeriserade applikationer konfigurerbara enligt "12-factor app" principer.

User: kanon, så jag kan starta containern med standardvärden om jag inte väljer att sätta dem?
Vad är "12-factor app" principer?

Assistant: Ja, precis! Om du kör containern utan att ange några miljövariabler, kommer den att starta med standardvärdena (port 8080 och swagger-sorterings-metod "method"). Du kan köra containern enkelt med:

```bash
docker run -d --name calculator -p 8080:8080 your-image-name
```

Eller om du vill ändra värdena:

```bash
docker run -d --name calculator -p 9090:9090 -e SERVER_PORT=9090 -e SWAGGER_SORTER=alpha your-image-name
```

## 12-Factor App principer

"The Twelve-Factor App" är en metodologi för att bygga moderna, skalbara och underhållbara mjukvaruapplikationer, särskilt för molntjänster. Den utvecklades av utvecklare på Heroku och har blivit en de facto standard för molnapplikationer.

De 12 faktorerna är:

1. **Codebase** - En kodversion i versionshantering, många deploys
2. **Dependencies** - Explicit deklarera och isolera beroenden
3. **Config** - Lagra konfiguration i miljön (inte i koden)
4. **Backing Services** - Behandla stödtjänster som anslutna resurser
5. **Build, Release, Run** - Strikt separation mellan bygg- och körningsfaser
6. **Processes** - Kör applikationen som en eller flera stateless processer
7. **Port Binding** - Exponera tjänster via port binding
8. **Concurrency** - Skala ut via process-modellen
9. **Disposability** - Maximera robusthet genom snabb uppstart och graceful shutdown
10. **Dev/Prod Parity** - Håll utveckling, testning och produktion så lika som möjligt
11. **Logs** - Behandla loggar som event streams
12. **Admin Processes** - Kör admin/skötseluppgifter som engångsprocesser

I fallet med din Dockerfile adresserar vi särskilt faktor #3 (Config) genom att lagra konfiguration i miljövariabler istället för hårdkodade värden. Detta gör din container mer flexibel och anpassningsbar för olika miljöer utan att behöva bygga om den.

User: Kanon, jag skulle vilja snabba på byggprocessen genom att skapa 2 dedikerade FROM images samt göra images så små som möjligt. För att snabba på byggprocessen så bör min container redan innehålla mina maven paket med korrekta versioner från min pom fil. Kan du skapa de 2 images jag behöver samt instruktioner hur jag bygger dem. då jag har en arm64 processor och ska bygga för amd64 och pusha till dockerhub så behöver jag även extrastegen för buildx. använd steg för att bygga och pusha samtidigt i ett steg. mit konto på dockerhub är "deskavaenkelt".

Assistant: ```dockerfile

# Base Build Image - Dockerfile.base-build

FROM maven:3.9.7-amazoncorretto-21 AS base-build

# Create directory for custom app dependencies

WORKDIR /app
COPY pom.xml .

# Pre-download all dependencies to cache them in this image

RUN mvn dependency:go-offline -B

# Keep the image minimal by removing Maven local repo clutter

RUN find ~/.m2 -name "_remote.repositories" -exec rm -f {} \;
RUN find ~/.m2 -name "*.lastUpdated" -exec rm -f {} \;

# This will be our base build image

```

```dockerfile
# Runtime Base Image - Dockerfile.base-runtime
FROM amazoncorretto:21-alpine

# Install common utilities and keep the image small
RUN apk add --no-cache curl tzdata && \
    rm -rf /var/cache/apk/*

# Add a non-root user to run the application
RUN addgroup -S appgroup && adduser -S appuser -G appgroup
USER appuser

# Set the working directory
WORKDIR /app

# This will be our base runtime image
```

```dockerfile
# Final Application Dockerfile - Dockerfile
# Using multi-stage build with our optimized base images

# Build stage
FROM deskavaenkelt/calculator-base-build:latest AS build

# Set the working directory
WORKDIR /app

# Copy source code
COPY src ./src

# Build the application using the pre-cached dependencies
RUN mvn clean package -DskipTests

# Runtime stage
FROM deskavaenkelt/calculator-base-runtime:latest

# Copy the JAR from the build stage
COPY --from=build /app/target/calculator-0.0.1-SNAPSHOT.jar app.jar

# Expose application port
EXPOSE 8080

# Set the entry point with configurable properties
ENTRYPOINT ["java", "-Dserver.port=${SERVER_PORT:8080}", "-Dspringdoc.swagger-ui.operationsSorter=${SWAGGER_SORTER:method}", "-jar", "app.jar"]
```

```bash
#!/bin/bash
# build-and-push.sh - Script för att bygga och pusha multi-platform images

# Skapa och konfigurera buildx-builder
docker buildx create --name multiplatform-builder --driver docker-container --use
docker buildx inspect --bootstrap

# 1. Bygg och pusha Base Build Image
echo "Building and pushing calculator-base-build image..."
docker buildx build --platform linux/amd64,linux/arm64 \
  -t deskavaenkelt/calculator-base-build:latest \
  -f Dockerfile.base-build \
  --push .

# 2. Bygg och pusha Base Runtime Image
echo "Building and pushing calculator-base-runtime image..."
docker buildx build --platform linux/amd64,linux/arm64 \
  -t deskavaenkelt/calculator-base-runtime:latest \
  -f Dockerfile.base-runtime \
  --push .

# 3. Bygg och pusha den slutliga applikationen
echo "Building and pushing calculator application image..."
docker buildx build --platform linux/amd64,linux/arm64 \
  -t deskavaenkelt/calculator:latest \
  -f Dockerfile \
  --push .

echo "All images built and pushed successfully!"
```

## Instruktioner

1. Skapa tre filer:
   - `Dockerfile.base-build` - Basimage för byggprocessen med Maven-dependencies
   - `Dockerfile.base-runtime` - Slimmad basimage för runtime
   - `Dockerfile` - Din faktiska applikation som använder de två basimages
   - `build-and-push.sh` - Byggscript för multi-architecture image building

2. Logga in på Docker Hub:

   ```bash
   docker login
   ```

3. Ge exekveringsrättigheter till byggscriptet:

   ```bash
   chmod +x build-and-push.sh
   ```

4. Kör byggscriptet:

   ```bash
   ./build-and-push.sh
   ```

### Fördelar med detta upplägg

1. **Snabbare byggen**: När du ändrar din kod behöver Maven inte ladda ner dependencies varje gång
2. **Mindre images**: Runtime-imagen är baserad på Alpine och innehåller bara nödvändiga verktyg
3. **Multi-platform**: Buildx gör att du kan bygga för både ARM64 och AMD64 samtidigt
4. **Förbättrad säkerhet**: Runtime-imagen kör som en icke-root användare
5. **Caching**: Docker caching fungerar effektivt med denna uppdelning

För att köra containern när du är klar:

```bash
docker run -d -p 8080:8080 deskavaenkelt/calculator:latest
```

User: kan jag få ett skript utan arm delen då jag ska dema hur man gör allt på en verifierings/byggserver som kör amd64

Assistant: ```bash

# !/bin/bash

# build-and-push-amd64.sh - Script för att bygga och pusha endast AMD64 images

# Logga in på Docker Hub om det behövs

# docker login

# 1. Bygg och pusha Base Build Image

echo "Building and pushing calculator-base-build image..."
docker build -t deskavaenkelt/calculator-base-build:latest -f Dockerfile.base-build .
docker push deskavaenkelt/calculator-base-build:latest

# 2. Bygg och pusha Base Runtime Image

echo "Building and pushing calculator-base-runtime image..."
docker build -t deskavaenkelt/calculator-base-runtime:latest -f Dockerfile.base-runtime .
docker push deskavaenkelt/calculator-base-runtime:latest

# 3. Bygg och pusha den slutliga applikationen

echo "Building and pushing calculator application image..."
docker build -t deskavaenkelt/calculator:latest -f Dockerfile .
docker push deskavaenkelt/calculator:latest

echo "All images built and pushed successfully for AMD64!"

```

User: hur kan jag rensa byggda och nedladdade images?

Assistant: ```bash
#!/bin/bash
# clean-docker.sh - Script för att rensa Docker-images och containers

# Stoppa alla körande containers
echo "Stopping all running containers..."
docker stop $(docker ps -aq) 2>/dev/null || echo "No running containers to stop."

# Ta bort alla containers
echo "Removing all containers..."
docker rm $(docker ps -aq) 2>/dev/null || echo "No containers to remove."

# Ta bort specifikt calculator images
echo "Removing calculator images..."
docker rmi deskavaenkelt/calculator:latest 2>/dev/null || echo "No calculator image to remove."
docker rmi deskavaenkelt/calculator-base-build:latest 2>/dev/null || echo "No calculator-base-build image to remove."
docker rmi deskavaenkelt/calculator-base-runtime:latest 2>/dev/null || echo "No calculator-base-runtime image to remove."

# Ta bort alla oanvända/dangling images (de utan tag)
echo "Removing dangling images..."
docker image prune -f

# Alternativ: Rensa allt (använd med försiktighet)
# echo "Would you like to remove ALL unused Docker resources (images, containers, networks, volumes)? (y/n)"
# read answer
# if [ "$answer" = "y" ]; then
#   echo "Removing all unused Docker resources..."
#   docker system prune -a -f
# fi

echo "Docker cleanup completed!"
```

För mer specifik rensning, här är några användbara Docker-kommandon:

```bash
# Endast ta bort dangling/oanvända images
docker image prune

# Ta bort alla oanvända containers, networks, images och optional volumes
docker system prune

# Ta bort alla oanvända resurser, inklusive images som inte används av någon container
docker system prune -a

# Ta bort allt lokalt Docker content (använd med extrem försiktighet)
docker system prune -a --volumes

# Lista images för att se vad som finns
docker images

# Ta bort specifika images
docker rmi image_name:tag

# Ta bort alla images
docker rmi $(docker images -q)
```
