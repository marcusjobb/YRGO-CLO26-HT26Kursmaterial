---

title: Serverinfrastruktur och Containerisering
author: Marcus Ackre Medina
type: lecture
topic: server
difficulty: 1
status: adapted
marcus_voice: true
source: "/home/nionit/git/marcus-yh-claude-assistent/reference/lectures_to_spread_out/Serverinfrastruktur-och-Containerisering.md"
description: "En genomgång av serverinfrastruktur och containerisering – från molnets kraft till grundläggande infrastruktur, VPS, molnleverantörer och Docker."
tags: ["server", "infrastructure", "containerization", "docker", "lecture"]
week_fit: []
---

## Slide 1

- Serverinfrastruktur och Containerisering

- Från molnets kraft till grundläggande infrastruktur

- Yrkeshögskolan - Molnintegration

---

## Slide 2

- Agenda

- Varför grundläggande infrastruktur?

- Molnförståelse och kostnadsoptimering

- Infrastrukturlager

- Från hårdvara till molnabstraktioner

- VPS vs Molnleverantörer

- Kostnad, komplexitet och kontroll

- Säker serveruppsättning

- SSH, brandväggar och användarhantering

- Container deployment

- Från utveckling till produktion

- Infrastructure as Code

- Dokumentation som automatisering

---

## Slide 3

- Varför är grundläggande infrastruktur viktigt?

- Scenario: Azure-regionen går ner i fyra timmar - företaget förlorar miljoner eftersom teamet inte förstår alternativ infrastruktur

- Molnförståelse

- Kostnadsoptimering

- Problemlösning

- Bättre molnarkitekt genom förståelse av underliggande system

- Förstå molnkostnader genom manuell drift

- Felsöka när molntjänster havererar

- Portabilitet

- Robusthet

- Flexibilitet mellan olika plattformar

- Bygga säkra, portabla lösningar

---

## Slide 4

- Infrastrukturlager och abstraktioner

- Varje lager abstraherar komplexitet men kräver förståelse för effektiv användning

---

## Slide 5

- VPS vs Molnleverantörer

- Aspekt

- VPS (Hetzner)

- Molnleverantör (Azure/AWS)

- Kostnad

- Låg, förutsägbar

- Variabel, kan eskalera

- Komplexitet

- Medel, manuell hantering

- Hög, många tjänster

- Flexibilitet

- Begränsad till server-nivå

- Hög, managed services

- Ansvar

- OS, säkerhet, backup

- Delade ansvarsmodeller

- Skalning

- Manuell

- Automatisk

- Lärande

- Grundläggande infrastruktur

- Molnspecifika patterns

---

## Slide 6

- Containerisering som portabilitetslager

- Containers kapslar in applikationer med alla beroenden - identisk körning oavsett värdmiljö

---

## Slide 7

- Grundläggande serverarkitektur

---

## Slide 8

- Utveckling vs Produktion - Kritiska skillnader

- Aspekt

- Utveckling

- Produktion

- Säkerhet

- Öppen för localhost

- Exponerad för internet

- Nätverk

- Automatisk portmappning

- Manuell brandvägg

- Data

- Temporär/testdata

- Persistent produktionsdata

- Övervakning

- GUI-verktyg

- Kommandoradsbaserad

- Backup

- Inte kritiskt

- Essentiellt för kontinuitet

- Säkerhet är den mest kritiska skillnaden - utvecklingsmiljöer körs lokalt medan produktion exponeras för internet

---

## Slide 9

- SSH-säkerhet: Nycklar vs Lösenord

- Varför SSH-nycklar är säkrare:

- Praktisk implementation:

- Asymmetrisk kryptering - Privat nyckel lämnar aldrig din maskin

- # Generera nyckelpar
- ssh-keygen -t ed25519 -C "din@email.com"
- # Kopiera till server
- ssh-copy-id deployuser@server_ip

- Ingen känslig data över nätverket - Endast signaturer överförs

- Brute force-resistent - Omöjligt att gissa rätt nyckel

- Enkel revokering - Ta bort publik nyckel för att stoppa åtkomst

- Ovanstående kommandon visar hur du enkelt kan implementera SSH-nycklar. Den första raden genererar ett nytt SSH-nyckelpar, där den privata nyckeln stannar säkert på din maskin. Den andra raden kopierar den publika nyckeln till den angivna användaren på servern, vilket möjliggör säker och lösenordsfri inloggning.

---

## Slide 10

- Brandväggskonfiguration för webbapplikationer

- Princip: Öppna endast nödvändiga portar enligt minsta privilegium

- Kritiska portar att öppna:

- # Grundläggande säkerhetsinställning
- ufw default deny incoming
- ufw default allow outgoing
- # Nödvändiga portar för Spring Boot webapp
- ufw allow ssh # Port 22 - administration
- ufw allow 80/tcp # HTTP - webåtkomst
- ufw allow 443/tcp # HTTPS - säker webåtkomst
- ufw allow 8080/tcp # Spring Boot - applikation
- ufw --force enable

---

## Slide 11

- Docker säkerhetspraxis

- Bäst säkerhetspraxis för produktion:

- # KORREKT - Säker produktionskonfiguration
- docker run -d \
- --name my-app \
- -p 8080:8080 \
- --user appuser \    # Icke-root användare
- --memory=512m \     # Resursbegränsning
- --restart unless-stopped \  # Automatic restart
- my-spring-app:latest

- # FELAKTIGT - Osäkert
- docker run -d --privileged --user root my-app:latest

- Nyckelprinciper

- Icke-root användare

- Resursbegränsning

- Restart policy

---

## Slide 12

- Multi-stage Docker builds för säkerhet

- Flerstegs-Docker-byggen är en bästa praxis för att skapa effektivare och säkrare Docker-avbildningar. De innebär att du använder flera FROM-instruktioner i din Dockerfile.

- # Byggsteg - innehåller utvecklingsverktyg
- FROM maven:3.8.4-openjdk-17 AS build
- WORKDIR /app
- COPY pom.xml .
- COPY src ./src
- RUN mvn clean package -DskipTests
- # Produktionssteg - minimal image
- FROM openjdk:17-jre-slim
- WORKDIR /app
- # Icke-root användare
- RUN groupadd -r appuser && useradd -r -g appuser appuser
- COPY --from=build /app/target/*.jar app.jar
- RUN chown appuser:appuser app.jar
- USER appuser
- EXPOSE 8080
- ENTRYPOINT ["java", "-jar", "app.jar"]

- Huvudsyftet är att separera byggmiljön (som kan innehålla kompilatorer, utvecklingsverktyg och tillfälliga filer) från den slutgiltiga körmiljön. Detta resulterar i en minimal avbildning som endast innehåller de nödvändiga applikationsfilerna och dess beroenden.

- De främsta fördelarna är:

- Minskad avbildningsstorlek: Slutavbildningen blir betydligt mindre eftersom den inte innehåller onödiga byggverktyg eller cachade paket.

- Förbättrad säkerhet: Genom att ta bort byggverktyg och andra icke-nödvändiga komponenter minskar du avsevärt avbildningens attackyta.

---

## Slide 13

- Serveruppsättning steg-för-steg

- Docker-installation

- Initial säkerhet

- # Lägg till Docker repository och installera
```csharp
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | \
```

- gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
- apt install docker-ce docker-compose-plugin -y
- usermod -aG docker deployuser

- # Skapa icke-root användare
- adduser deployuser
- usermod -aG sudo deployuser
- # Kopiera SSH-nycklar
- cp /root/.ssh/authorized_keys /home/deployuser/.ssh/
- chown -R deployuser:deployuser /home/deployuser/.ssh

---

## Slide 14

- Container deployment med nätverk

- # Skapa dedikerat nätverk
- docker network create app-network
- # Starta database
- docker run -d \
- --name postgres-db \
- --network app-network \
- -e POSTGRES_DB=myapp \
- -v postgres-data:/var/lib/postgresql/data \
- postgres:14-alpine
- # Starta applikation
- docker run -d \
- --name my-spring-app \
- --network app-network \
- -p 8080:8080 \
- -e DB_HOST=postgres-db \
- my-spring-app:latest

---

## Slide 15

- Skalning: Horisontell vs Vertikal

- # Horisontell skalning - multipla instanser
```csharp
for i in {1..3}; do
```

- docker run -d --name app-$i -p $((8080+$i)):8080 myapp:latest
- done

- Horisontell skalning (fler instanser)

- Lämpligt för: Stateless applikationer med lastfördelning

- 1

- Fördelar: Bättre fault tolerance och flexibilitet

- Krav: Applikation utan shared state

- Vertikal skalning (mer resurser)

- Lämpligt för: Stateful applikationer eller begränsad arkitektur

- 2

- Fördelar: Enklare implementation

```csharp
Begränsning: Single point of failure
```


---

## Slide 16

- Infrastructure as Code fördelar

- IaC behandlar infrastruktur som kod:

- # docker-compose.yml exempel
- version: '3.8'
- services:
- app:
- build: .
- ports: ["8080:8080"]
- depends_on: [database]
- restart: unless-stopped
- database:
- image: postgres:14-alpine
- volumes: ["postgres-data:/var/lib/postgresql/data"]

- Versionskontroll - Spåra alla infrastrukturändringar

- Code review - Peer review av infrastrukturkonfiguration

- Reproducerbarhet - Identiska miljöer varje gång

- Eliminerar "configuration drift" - Konsistenta miljöer

---

## Slide 17

- Från manuell drift till automatisering

- Manuell

- Lär dig grunderna

- Skriptad

- Dokumentera processer

- Deklarativ

- Beskriv önskat tillstånd

- Automatiserad

- Kontinuerlig deployment

---

## Slide 18

- Sammanfattning

- Nyckelinsikter från denna lektion:

- VPS som lärplattform - Manuell administration ger djup förståelse för molnabstraktioner

- Säkerhet från grunden - SSH-nycklar, brandväggar och icke-root drift är fundamentalt

- Container-portabilitet - Docker eliminerar miljöskillnader mellan utveckling och produktion

- IaC-tänkande - Dokumentera alla manuella processer för framtida automatisering

- Gradvis automatisering - Från kommandon till skript till Infrastructure as Code

- Molnabstraktioner fungerar bäst när du förstår vad som händer "under huven"

---

## Slide 19

- Quiz

---

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
