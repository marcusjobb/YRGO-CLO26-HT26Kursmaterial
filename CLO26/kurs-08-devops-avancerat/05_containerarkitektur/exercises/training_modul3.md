# Träningsuppgifter: DevOps Avancerat — Modul 3

> **Modul:** 05 — Containerarkitektur (service mesh, sidecar-mönster, microservices)

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är en microservice-arkitektur?

a. En stor applikation där all kod ligger i en enda fil<br>b. En arkitektur där applikationen delas upp i flera små, självständiga tjänster — varje tjänst har ett ansvar, egen databas, egen deployment<br>c. En databas<br>d. Ett sätt att skriva mindre kod

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En arkitektur där applikationen delas upp i flera små, självständiga tjänster

  **Förklaringar:**

  - ❌ **a) En stor fil** - FEL: Det är motsatsen — monolit vs microservices
  - ✅ **b) Små självständiga tjänster** - **RÄTT**: Monolit = en process, en databas, en deployment. Microservices = order-service (port 5001, egen DB), payment-service (port 5002, egen DB), user-service (port 5003, egen DB). Varje tjänst kan skalas, deployas, utvecklas OBEROENDE
  - ❌ **c) Databas** - FEL: Microservices är ett arkitekturmönster, inte en databas
  - ❌ **d) Mindre kod** - FEL: Microservices är ofta MER kod (nätverkskommunikation, orkestrering)
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är en monolit jämfört med microservices?

a. En monolit är en applikation där alla funktioner körs i EN process — enklare initialt men svår att skala. Microservices delar upp i flera processer — mer komplext men bättre för stora team<br>b. Monolit är bättre än microservices<br>c. Microservices är alltid bättre än monoliter<br>d. De är samma sak

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Monolit = en process, enklare initialt. Microservices = flera processer, bättre i stor skala

  **Förklaringar:**

  - ✅ **a) En process vs flera** - **RÄTT**: Monolit: enkelt att starta, testa, debugga. Men: en liten ändring = hela appen deployas. En minnesläcka = hela appen kraschar. Microservices: komplexare (nätverk, konsistens, observability) men varje team äger sina tjänster, skalar oberoende
  - ❌ **b) Alltid bättre** - FEL: För små appar är monolit ofta rätt val
  - ❌ **c) Alltid bättre** - FEL: Microservices har overhead — fel val om du har 2 utvecklare
  - ❌ **d) Samma sak** - FEL: Fundamentalt olika arkitekturer
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är ett sidecar-mönster i containerarkitektur?

a. En container som sitter bredvid huvudcontainern i samma pod — den utökar eller hanterar funktionalitet som logging, monitoring, proxy<br>b. En motorcykel<br>c. En separat server<br>d. En databas-container

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En container som sitter bredvid huvudcontainern i samma pod — utökar funktionalitet

  **Förklaringar:**

  - ✅ **a) Sidecar-container** - **RÄTT**: Huvudcontainern (app) + sidecar (t.ex. Envoy proxy, logg-samlare, metrics-exporter). De delar samma nätverk och storage i pod:en. Appen behöver inte veta om sidecar:en — den bara fungerar. Kafka-Sidecar: hanterar TLS för appen som inte har TLS-stöd
  - ❌ **b) Motorcykel** - FEL: 😄 Sidecar är mönster, inte fordon
  - ❌ **c) Server** - FEL: Sidecar är en container i samma pod, inte en separat server
  - ❌ **d) Databas** - FEL: Sidecars är för cross-cutting concerns, inte datalagring
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är ett service mesh?

a. Ett dedikerat infrastrukturlager för tjänst-till-tjänst-kommunikation i microservices — hanterar trafik, säkerhet, observability utan att ändra applikationskoden<br>b. Ett sätt att binda ihop databaser<br>c. En typ av API<br>d. Ett nätverk av servrar

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett infrastrukturlager för tjänst-till-tjänst-kommunikation — trafik, säkerhet, observability

  **Förklaringar:**

  - ✅ **a) Infrastrukturlager för kommunikation** - **RÄTT**: Istio, Linkerd, Consul. Varje tjänst får en proxy-sidecar (Envoy). Proxyn hanterar: mTLS (krypterad trafik mellan tjänster), retry/logic, load balancing, tracing, metrics. Appkod = oförändrad. Allt konfigureras via YAML
  - ❌ **b) Binda databaser** - FEL: Service mesh hanterar tjänst-tjänst, inte databaser
  - ❌ **c) API** - FEL: Ett API är ett gränssnitt. Service mesh är ett nätverkslager UNDER API:et
  - ❌ **d) Nätverk av servrar** - FEL: Service mesh är ett konceptuellt lager, inte fysiska servrar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är mTLS (mutual TLS) i service mesh-sammanhang?

a. Att bara servern har ett certifikat<br>b. Att BÅDE klienten och servern visar upp certifikat för varandra — dubbelriktad autentisering och kryptering mellan tjänster<br>c. Att ingen har certifikat<br>d. Att använda VPN

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Både klient och server visar certifikat — dubbelriktad autentisering

  **Förklaringar:**

  - ❌ **a) Bara server** - FEL: Det är vanlig TLS (HTTPS). mTLS är ömsesidigt
  - ✅ **b) Båda håller upp cert** - **RÄTT**: Vanlig TLS: klienten verifierar serverns cert. mTLS: även servern verifierar klientens cert. I service mesh (Istio) sker detta automatiskt via sidecar-proxyn. Varje tjänst har ett identitetscertifikat. Ingen VPN eller brandväggsregler behövs
  - ❌ **c) Inga cert** - FEL: mTLS bygger på certifikat
  - ❌ **d) VPN** - FEL: mTLS och VPN är olika säkerhetsmekanismer
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är fördelen med att separera databaser per microservice?

a. Det är enklare att ha en gemensam databas<br>b. Varje tjänst äger sin data och kan välja rätt databasteknik för sitt behov — ingen delad databas skapar koppling mellan tjänsterna<br>c. Det är billigare<br>d. Databaser behövs inte i microservices

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Varje tjänst äger sin data och väljer rätt teknik — ingen delad databas

  **Förklaringar:**

  - ❌ **a) Gemensam DB** - FEL: Delad databas skapar koppling (coupling) — en ändring i en tabell påverkar alla
  - ✅ **b) Databaser per tjänst** - **RÄTT**: Order-service kan använda PostgreSQL (ACID för orders). Product-service kan använda MongoDB (flexibel för produkter). User-service kan använda Redis (snabb för sessioner). Kommunikation bara via API — aldrig via delad databas
  - ❌ **c) Billigare** - FEL: Fler databaser = oftast dyrare
  - ❌ **d) Behövs inte** - FEL: De behövs, bara separerade
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är en API Gateway i en microservice-arkitektur?

a. En portvalktare — en enda ingångspunkt för alla klienter som routar till rätt tjänst, hanterar auth, rate limiting och aggregering<br>b. En databas<br>c. En server<br>d. Ett container-register

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En enda ingångspunkt som routar, autentiserar, rate-limitar och aggregerar

  **Förklaringar:**

  - ✅ **a) Portvalktare** - **RÄTT**: Istället för att klienten vet om alla microservices (order:5001, payment:5002, user:5003) anropar den bara API Gateway på port 443. Gatewayen: routar till rätt tjänst, kollar JWT, rate-limit (100 req/min), cache:ar svar. Azure API Management, Kong, Ocelot (.NET)
  - ❌ **b) Databas** - FEL: Gateway är för routing och auth, inte lagring
  - ❌ **c) Server** - FEL: Gateway är en tjänst, inte en specifik server
  - ❌ **d) Container-register** - FEL: Registry (Docker Hub) lagrar images
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är "circuit breaker"-mönstret?

a. Att stänga av en tjänst när den är överbelastad<br>b. Ett mönster som förhindrar att en tjänst anropar en trasig fjärrtjänst om och om igen — den "öppnar kretsen" och returnerar snabbt fel tills tjänsten återhämtat sig<br>c. Att koppla bort databasen<br>d. En elektrisk säkring

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Förhindrar att anropa en trasig tjänst upprepade gånger — öppnar kretsen

  **Förklaringar:**

  - ❌ **a) Stänga av** - FEL: Circuit breaker är en temporär FÖRHINDRA anrop, inte permanent avstängning
  - ✅ **b) Öppna kretsen** - **RÄTT**: Closed (normalt) → anrop går igenom. Open (fel tröskel nådd) → anrop returnerar direkt med "Service unavailable". Half-Open (efter en timeout) → testa ett anrop. OK → Closed. Fel → Open igen. Polly (.NET), Resilience4j (Java). Skyddar hela systemet från kedjefel
  - ❌ **c) Koppla bort DB** - FEL: Circuit breaker skyddar mot fjärrtjänst-fel, inte databasen
  - ❌ **d) Elektrisk säkring** - FEL: 😄 Metaforen är från elektricitet, men det är en mjukvarumekanism
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
