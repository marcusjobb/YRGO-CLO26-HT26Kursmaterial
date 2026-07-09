# Docker — Fördjupning

## Varför Docker?

Docker löser "det fungerar på min maskin"-problemet. Genom att paketera applikationen med alla beroenden i en container säkerställer du att den körs likadant överallt: utveckling, test, staging, produktion.

## Container vs VM

| Aspekt | Container | Virtual Machine |
|--------|-----------|----------------|
| OS | Delar värdens OS-kärna | Eget fullt OS |
| Starttid | Millisekunder | Minuter |
| Storlek | MB | GB |
| Isolation | Process-nivå | Hypervisor-nivå |
| Resurser | Delar kernel | Dedikerade per VM |

En container är i princip en process som körs i en isolerad miljö med eget filsystem, nätverk och process-träd.

## Dockerfile — Bästa Praktik

### Multi-stage Builds

Separera byggmiljö från runtime:

```dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app

# Stage 2: Runtime — minimal image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "app.dll"]
```

Detta ger en mycket mindre slut-image (bara runtime + din kod, inga SDK-verktyg).

### Layer Caching

Varje Dockerfile-kommando skapar ett lager som cachas. Ordna kommandona från mest stabila till mest föränderliga:

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /src
COPY *.csproj .          # Ändras sällan → cachas
RUN dotnet restore        # Ändras sällan → cachas
COPY . .                  # Ändras ofta
RUN dotnet publish
```

## Docker Compose

För flera containrar som samverkar:

```yaml
version: '3.8'
services:
  web:
    build: .
    ports:
      - "5000:80"
    depends_on:
      - db
  db:
    image: mysql:8
    environment:
      MYSQL_ROOT_PASSWORD: example
    volumes:
      - db_data:/var/lib/mysql

volumes:
  db_data:
```

## Vanliga Kommandon

| Kommando | Beskrivning |
|----------|-------------|
| `docker build -t name .` | Bygg image |
| `docker run -p 8080:80 name` | Starta container |
| `docker ps` | Lista körande containrar |
| `docker stop id` | Stoppa container |
| `docker logs id` | Visa loggar |
| `docker exec -it id bash` | Gå in i containern |
| `docker compose up` | Starta alla tjänster |
| `docker system prune` | Rensa (varning: tar bort stoppade containrar) |
