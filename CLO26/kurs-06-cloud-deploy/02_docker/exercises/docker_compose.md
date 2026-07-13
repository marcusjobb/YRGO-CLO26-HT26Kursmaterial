# Docker Compose — Lokal utvecklingsmiljö med HyresBil

🟡

## Mål

Sätt upp en komplett lokal utvecklingsmiljö för HyresBil-appen med Docker Compose. API:et byggs från en Dockerfile och MongoDB körs som en separat container — allt startas med ett enda kommando.

> **Det du lär dig:**
>
> * Hur Docker Compose definierar flera containers i en enda fil
> * Hur containers pratar med varandra via service-namn
> * Hur environment-variabler skickar konfiguration till din app
> * Hur named volumes gör databasdatan persistent
> * Varför Compose passar lokalt men inte i produktion

## Förutsättningar

> **Innan du börjar, se till att du har:**
>
> * ✓ Docker Desktop installerat och igång
> * ✓ Kört docker-övningarna innan den här (du vet vad en image och container är)
> * ✓ En terminal öppen
> * ✓ VS Code eller annan editor

## Bakgrund

HyresBil-appen är ett enkelt .NET Web API för att hantera biluthyrningar. Den behöver en MongoDB-databas för att lagra bilar och bokningar.

Alternativ 1: installera MongoDB på din dator. Skapar skräp, krockar med versioner, "fungerar på min dator"-problem.

Alternativ 2: kör allt med Docker Compose. Ett kommando. Städas upp med ett kommando. Fungerar lika på alla maskiner.

Vi kör alternativ 2.

## Övningssteg

### Översikt

1. **Titta på Dockerfile**
2. **Analysera docker-compose.yml**
3. **Starta stacken**
4. **Testa att appen funkar**
5. **Testa persistent data**
6. **Utforska kommandon**

---

### **Steg 1:** Titta på Dockerfile

Skapa katalogen `exercise-compose/HyresBil/` och lägg in dessa filer.

Börja med Dockerfile:

> `exercise-compose/HyresBil/Dockerfile`

```dockerfile
# Byggsteg — kompilerar appen
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Kopiera projektfil och hämta beroenden separat (cacheoptimiering)
COPY HyresBil.csproj .
RUN dotnet restore

# Kopiera resten och bygg
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Körningssteg — lättviktig runtime-image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "HyresBil.dll"]
```

Läs igenom Dockerfile och fundera: varför är `COPY HyresBil.csproj .` + `RUN dotnet restore` separerat från `COPY . .`?

> ℹ **Koncept**
>
> Det handlar om Docker-cache. Varje rad i en Dockerfile är ett lager. Om ett lager inte förändrats sedan förra bygget återanvänder Docker cachen.
>
> `dotnet restore` drar ner alla NuGet-paket — det tar tid. Lägger vi det före `COPY . .` behöver vi bara köra restore igen om `.csproj` förändrats. Ändrar vi bara en `.cs`-fil hoppar Docker direkt till det steget.
>
> Det är en liten sak som sparar minuter när du bygger ofta.
>
> ✓ **Snabbkoll:** Kan du förklara skillnaden mellan `build`-steget och `runtime`-steget?

---

### **Steg 2:** Analysera docker-compose.yml

Skapa docker-compose.yml en nivå upp, i `exercise-compose/`:

> `exercise-compose/docker-compose.yml`

```yaml
services:
  api:
    build:
      context: ./HyresBil
    container_name: hyresbil-api
    restart: unless-stopped
    ports:
      - "127.0.0.1:5050:8080"
    environment:
      - MongoDB__ConnectionString=mongodb://mongodb:27017
      - MongoDB__DatabaseName=hyresbil
    depends_on:
      - mongodb

  mongodb:
    image: mongo:7.0
    container_name: hyresbil-mongodb
    restart: unless-stopped
    ports:
      - "127.0.0.1:27017:27017"
    volumes:
      - mongo-data:/data/db

volumes:
  mongo-data:
```

Läs igenom filen. Hitta svaren på de här frågorna innan du startar:

1. Vilken port når du appen på, från din dator?
2. Vad heter MongoDB-servern som appen ska ansluta till?
3. Vart i containern lagras MongoDB-datan?
4. Vad händer om du startar `api`-servicen utan att MongoDB är igång?

> ℹ **Koncept — connection string utan localhost**
>
> Lägg märke till `mongodb://mongodb:27017` i connection string. Inte `localhost`.
>
> Inne i en container är `localhost` containern själv — inte din dator och inte en annan container. Docker Compose skapar ett internt nätverk där varje service når de andra via service-namnet som DNS-namn. `mongodb` i connection stringen löser upp till rätt container automatiskt.
>
> Det är enkelt när du vet det. Tills du vet det är det den vanligaste orsaken till att appen inte kan prata med databasen.
>
> ℹ **Koncept — depends_on**
>
> `depends_on: mongodb` säger åt Compose att starta mongodb-containern innan api-containern. Observera: det garanterar att containern startat, inte att MongoDB är redo att ta emot anslutningar. MongoDB behöver ett par sekunder på sig. Om appen kraschar vid start — vänta och starta om den.
>
> ✓ **Snabbkoll:** Varför är det fel att skriva `mongodb://localhost:27017` i connection stringen?

---

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

---

### **Steg 3:** Starta stacken

Navigera till `exercise-compose/` och kör:

```bash
docker compose up -d --build
```

Flaggorna:
- `-d` — kör i bakgrunden (detached)
- `--build` — bygg om images innan start

Kolla att båda services är uppe:

```bash
docker compose ps
```

Du ska se något i stil med:

```
NAME                STATUS      PORTS
hyresbil-api        running     127.0.0.1:5050->8080/tcp
hyresbil-mongodb    running     127.0.0.1:27017->27017/tcp
```

> ⚠ **Vanliga misstag**
>
> * **YAML-fel:** YAML kräver mellanslag — inte tabbar. Varje indenteringsnivå är två mellanslag. Felaktigt indenterad YAML ger konstiga felmeddelanden.
> * **Portkonflikt:** Kör något annat på port 5050 eller 27017? Byt host-porten i docker-compose.yml (vänstra sidan av `:`).
> * **Gammalt bygge:** Ändrade du kod men glömde `--build`? Compose återanvänder förra imagen. Kör alltid `--build` efter kodändringar.
>
> ✓ **Snabbkoll:** `docker compose ps` visar båda services med status `running`

---

### **Steg 4:** Testa att appen funkar

Öppna en browser och gå till `http://localhost:5050/swagger` (eller det endpoint-mönster appen använder).

Skapa en biluthyrning via API:et.

Kolla sedan loggarna för att se att det faktiskt gick till MongoDB:

```bash
docker compose logs api
```

Vill du följa loggarna live, lägg till `-f`:

```bash
docker compose logs -f api
```

Avbryt med `Ctrl+C`.

> ℹ **Koncept — docker compose logs**
>
> `docker compose logs` visar output från alla services i stacken. Lägg till service-namnet för att filtrera:
>
> ```bash
> docker compose logs mongodb
> ```
>
> Det är din primära felsökningsyta. Om något är fel — börja här.
>
> ✓ **Snabbkoll:** Du kan se logg-output från appen. Inga uppenbara felmeddelanden.

---

### **Steg 5:** Testa persistent data

Det här är det viktiga steget. Named volumes är varför databasdatan överlever att du tar ner stacken.

1. Skapa en eller flera biluthyrningar via API:et
2. Ta ner stacken:

```bash
docker compose down
```

3. Starta den igen:

```bash
docker compose up -d
```

4. Kontrollera att datan finns kvar via API:et

Datan ska vara kvar. MongoDB lagrar den i named volume `mongo-data` som lever utanför container-livscykeln.

Prova sedan att ta bort volymen också:

```bash
docker compose down -v
```

```bash
docker compose up -d --build
```

Nu är datan borta. `-v`-flaggan tar bort named volumes med. Använd det medvetet — det är en ren slate.

> ℹ **Koncept — named volumes vs container-lagring**
>
> En container har ett skrivlager ovanpå imagen. Det försvinner när containern tas bort. Lagrar MongoDB datan där — försvinner datan.
>
> En named volume är separat från containern. Docker hanterar var den lagras på disk. Containern kan tas bort och skapas om — volymen finns kvar.
>
> | | Named volume | Container-lagring |
> |---|---|---|
> | Överlever `docker compose down` | ✓ | ✗ |
> | Överlever `docker compose down -v` | ✗ | ✗ |
> | Deklareras i Compose-filen | Under `volumes:` | Behövs inte |
>
> ⚠ **Varning:** Kör aldrig `docker compose down -v` i produktion på en riktig databas. Det är precis vad det låter som.
>
> ✓ **Snabbkoll:** Datan finns kvar efter `down` + `up`. Datan är borta efter `down -v` + `up`.

---

### **Steg 6:** Utforska kommandon

Lär dig kommandona som du kommer använda dagligen:

Lista körande services:

```bash
docker compose ps
```

Visa loggar (alla services):

```bash
docker compose logs
```

Visa loggar för en specifik service:

```bash
docker compose logs mongodb
```

Följ loggar live:

```bash
docker compose logs -f api
```

Starta om en specifik service:

```bash
docker compose restart api
```

Stoppa och ta bort containers (behåll volymer):

```bash
docker compose down
```

Stoppa, ta bort containers och volymer:

```bash
docker compose down -v
```

> ℹ **Koncept — Compose vs produktion**
>
> Docker Compose är utmärkt för lokal utveckling. En fil, ett kommando, hela stacken.
>
> I produktion räcker det inte. Varför?
>
> * Compose körs på en enda maskin. Om maskinen dör, dör hela appen.
> * Compose har ingen inbyggd autoskalning — trafiken ökar, appen klarar inte mer.
> * Compose hanterar inte rullande uppdateringar utan driftstopp.
> * Compose har inte inbyggd health-checking som startar om sjuka containers automatiskt.
>
> I produktion används orkestreringssystem som Kubernetes (k8s) eller molntjänster som Azure Container Apps. De löser alla de problemen — men de är också mer komplexa att sätta upp.
>
> Compose = skridskor för lokalt. Kubernetes = racerbil för produktion. Du kör inte racerbil till ICA.
>
> ✓ **Snabbkoll:** Kan du namnge tre anledningar till att Compose inte passar i produktion?

---

## Vanliga problem

> **Om det strular:**
>
> **"Cannot connect to MongoDB":** Appen ansluter med fel adress. Kontrollera att connection stringen använder service-namnet `mongodb`, inte `localhost`.
>
> **"Port is already allocated":** Något annat kör på den porten. Byt host-porten i docker-compose.yml eller stäng av det som blockerar.
>
> **YAML parse error:** Troligtvis en tabbar-istället-för-mellanslag-miss. Kontrollera indentering noggrant.
>
> **Appen kraschar direkt:** MongoDB hinner inte initializera innan appen försöker ansluta. Kör `docker compose restart api` — det brukar lösa sig.
>
> **Gammal kod körs:** Du glömde `--build`. Kör `docker compose up -d --build`.
>
> **Ingen data efter omstart:** Volymen saknas i docker-compose.yml, eller du körde `down -v` av misstag. Kontrollera att `volumes: mongo-data:` finns deklarerat längst ner i filen.
>
> **Fortfarande fast?** Kör `docker compose logs` och leta efter felmeddelanden. Det är den snabbaste vägen till svaret.

## Sammanfattning

Du har satt upp en komplett lokal utvecklingsstack med Docker Compose.

* ✓ Dockerizerat ett .NET API med en multi-stage Dockerfile
* ✓ Definierat två services (api + mongodb) i en enda `docker-compose.yml`
* ✓ Konfigurerat connection string via environment-variabler
* ✓ Gjort MongoDB-datan persistent med named volume
* ✓ Lärt dig de viktigaste Compose-kommandona

> **Kärnan:** Docker Compose förvandlar en komplex uppstart till ett enda kommando. Hela teamet kör exakt samma miljö. Det är inga "fungerar på min dator"-diskussioner. Named volumes ger databasen minnesvärde mellan omstarter. Och när du är klar städas allt upp lika enkelt.

## Vill du gå djupare?

> **Frivilligt:**
>
> * Lägg till en `healthcheck` på mongodb-servicen så att `depends_on` väntar på att MongoDB är redo — inte bara startad
> * Testa `docker compose exec mongodb mongosh` för att öppna en MongoDB-shell direkt i containern
> * Kolla in Docker Compose profiles — ett sätt att slå på/av delar av stacken beroende på miljö
> * Prova `docker compose top` för att se vilka processer som körs inuti containers

---

Snyggt jobbat. Nu kör du som ett proffs lokalt — nästa steg är att förstå vad som händer när det ska ut i världen. Koda vilt!
