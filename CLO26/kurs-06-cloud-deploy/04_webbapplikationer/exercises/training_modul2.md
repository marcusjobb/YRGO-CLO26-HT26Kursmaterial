# Träningsuppgifter: Cloud Deploy — Modul 2

> **Moduler:** 03 — Container-appar, 04 — Webbapplikationer, 06 — Datalagring i moln

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är en container-app (Container App) i Azure?

a. En virtuell maskin med Docker installerat<br>b. En serverlös plattform som kör containrar utan att du hanterar orkestrering — som Azure Container Apps<br>c. En databas<br>d. Ett verktyg för att bygga containrar

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En serverlös plattform som kör containrar utan att du hanterar orkestrering

  **Förklaringar:**

  - ❌ **a) VM med Docker** - FEL: Det är Azure Container Instances. Container Apps är mer abstraherat
  - ✅ **b) Serverlös containerplattform** - **RÄTT**: Azure Container Apps hanterar skalning, lastbalansering, och uppdateringar automatiskt. Du pushar bara din container-image. Perfekt för microservices
  - ❌ **c) Databas** - FEL: Container Apps kör applikationer, inte databaser
  - ❌ **d) Bygga containrar** - FEL: Det är Docker, inte Container Apps
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är en REST API?

a. Ett API som bara fungerar när systemet startas om<br>b. Ett API som använder HTTP-metoderna (GET, POST, PUT, DELETE) för att arbeta med resurser — Representational State Transfer<br>c. En webbplats<br>d. En databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett API som använder HTTP-metoderna (GET, POST, PUT, DELETE) för att arbeta med resurser

  **Förklaringar:**

  - ❌ **a) Startas om** - FEL: REST är ett arkitekturmönster, inget om start
  - ✅ **b) HTTP-metoder för resurser** - **RÄTT**: GET /users (hämta alla), GET /users/1 (hämta en), POST /users (skapa), PUT /users/1 (uppdatera), DELETE /users/1 (radera). Stateless — varje anrop innehåller all information som behövs
  - ❌ **c) Webbplats** - FEL: Ett REST API levererar data (JSON/XML), inte HTML-sidor
  - ❌ **d) Databas** - FEL: REST API är gränssnittet mellan klient och server
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är HTTP vs HTTPS?

a. Samma sak<br>b. HTTPS är HTTP med kryptering via SSL/TLS — all data är skyddad mellan webbläsare och server<br>c. HTTPS är snabbare än HTTP<br>d. HTTP använder port 443, HTTPS använder port 80

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** HTTPS är HTTP med kryptering via SSL/TLS

  **Förklaringar:**

  - ❌ **a) Samma sak** - FEL: HTTPS har ett extra säkerhetslager (SSL/TLS)
  - ✅ **b) Krypterad HTTP** - **RÄTT**: HTTPS krypterar all trafik så att ingen kan avlyssna lösenord, kreditkort eller API-nycklar. Standard för alla moderna webbplatser. Du ser hänglåset i adressfältet
  - ❌ **c) Snabbare** - FEL: HTTPS har en liten overhead för handskakning, men skillnaden är försumbar
  - ❌ **d) Omvänt** - FEL: HTTP = port 80, HTTPS = port 443
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är en lastbalanserare (load balancer)?

a. En server som lagrar data<br>b. En enhet som distribuerar inkommande trafik över flera servrar för att undvika överbelastning<br>c. Ett sätt att mäta prestanda<br>d. En databas-replik

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En enhet som distribuerar inkommande trafik över flera servrar

  **Förklaringar:**

  - ❌ **a) Datalagring** - FEL: Lagring är separata tjänster (Blob Storage, DB)
  - ✅ **b) Distribuerar trafik** - **RÄTT**: Om du har 3 servrar och en får 1000 anrop/sekund — lastbalanseraren sprider anropen jämnt. Om en server går ner skickas trafiken till de andra. Azure Load Balancer, Application Gateway
  - ❌ **c) Prestandamätning** - FEL: Lastbalanserare FÖRBÄTTRAR prestanda, den mäter inte
  - ❌ **d) Databas-replik** - FEL: Repliker hanterar databasläsningar, lastbalanserare hanterar trafik
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är Azure Blob Storage?

a. En relationsdatabas i molnet<br>b. En tjänst för att lagra ostrukturerad data — bilder, videor, backups, loggar — i "blobbar"<br>c. En virtuell server<br>d. Ett nätverksverktyg

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En tjänst för att lagra ostrukturerad data — bilder, videor, backups, loggar

  **Förklaringar:**

  - ❌ **a) Relationsdatabas** - FEL: Det är Azure SQL Database
  - ✅ **b) Ostrukturerad data** - **RÄTT**: Blob Storage lagrar allt från textfiler till 4K-videor. Data organiseras i containers. Extremt skalbart — petabyte. Används för statiska filer, backups, media
  - ❌ **c) Virtuell server** - FEL: Blob Storage är lagring, inte compute
  - ❌ **d) Nätverksverktyg** - FEL: Det är en lagringstjänst
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är DBaaS (Database as a Service)?

a. Att installera en databas på en egen server<br>b. En molnhanterad databas — du betalar för databasen utan att hantera servern, OS:et eller installationen<br>c. Ett sätt att designa databaser<br>d. En databas-driver

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En molnhanterad databas — du betalar för databasen utan att hantera servern

  **Förklaringar:**

  - ❌ **a) Egen server** - FEL: Det är traditionell on-premises databas
  - ✅ **b) Hanterad databas i molnet** - **RÄTT**: Azure SQL Database, Amazon RDS, MongoDB Atlas. Molnleverantören hanterar: patchar, backups, replikering, hög tillgänglighet. Du bara ansluter och använder
  - ❌ **c) Designa databaser** - FEL: DBaaS är en driftsättningsmodell, inte en designmetod
  - ❌ **d) Databas-driver** - FEL: En driver är för att ansluta från kod (t.ex. MySQL Connector)
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
