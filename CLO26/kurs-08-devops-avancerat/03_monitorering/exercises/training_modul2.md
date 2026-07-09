# Träningsuppgifter: DevOps Avancerat — Modul 2

> **Modul:** 03 — Monitorering (Application Insights, Prometheus, Grafana, alerting), 04 — Säkerhet och nätverk (NSG, VPN, Key Vault, peering)

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är skillnaden mellan monitorering och observability?

a. Samma sak<br>b. Monitorering = du vet vad du ska mäta (fördefinierade metrics). Observability = du kan ställa nya frågor om systemets tillstånd utan att förbereda dem i förväg (logs, metrics, traces)<br>c. Observability är för utvecklare, monitorering är för drift<br>d. Monitorering är dyrare

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Monitorering = fördefinierade metrics. Observability = utforska systemet med logs, metrics, traces

  **Förklaringar:**

  - ❌ **a) Samma sak** - FEL: Observability är en utveckling av monitorering — mer kraftfullt
  - ✅ **b) Mätt vs utforska** - **RÄTT**: Monitorering: "Är CPU över 90%?" — du satte upp en regel. Observability: "Varför svarade API:et långsamt klockan 14:32?" — du har distributed tracing, strukturloggar, metrics som låter dig gräva i efterhand. De tre pelarna: Metrics, Logs, Traces
  - ❌ **c) Utvecklare vs drift** - FEL: Båda behövs av båda grupperna
  - ❌ **d) Pris** - FEL: Kostnad beror på implementation, inte koncept
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad används Prometheus till?

a. Att skicka e-post<br>b. En open-source monitoring- och alerting-system — samlar in metrics från tjänster och lagrar dem som tidsserier<br>c. En databas för användardata<br>d. Ett CI/CD-verktyg

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En open-source monitoring- och alerting-system — samlar in metrics som tidsserier

  **Förklaringar:**

  - ❌ **a) E-post** - FEL: 😄 Nej
  - ✅ **b) Tidsseriebaserad monitoring** - **RÄTT**: Prometheus "scrapar" HTTP-endpoints på dina tjänster (t.ex. `/metrics`). Lagrar data med tidsstämpel: `http_requests_total{method="GET", endpoint="/api"} 1024`. Har ett kraftfullt frågespråk (PromQL). Ofta ihop med Grafana
  - ❌ **c) Användardatabas** - FEL: Prometheus lagrar metrics, inte användarprofiler
  - ❌ **d) CI/CD** - FEL: Prometheus är för monitoring. CI/CD = Jenkins, GitHub Actions
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är Grafana?

a. En databas<br>b. En visualiseringsplattform — skapa dashboards med diagram och grafer från Prometheus, Azure Monitor, InfluxDB med mera<br>c. En container-orkestrerare<br>d. Ett språk

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En visualiseringsplattform — dashboards med diagram från många datakällor

  **Förklaringar:**

  - ❌ **a) Databas** - FEL: Grafana LÄSER från databaser, är inte en själv
  - ✅ **b) Dashboards och visualisering** - **RÄTT**: Grafana kopplas till Prometheus (eller Azure Monitor, eller SQL, eller...). Du bygger dashboards: CPU över tid, antal 500-fel, svarstider, minnesanvändning. Används av teamet för att "sov gott om natten"
  - ❌ **c) Orkestrerare** - FEL: Kubernetes orkestrerar containrar. Grafana visualiserar
  - ❌ **d) Språk** - FEL: Grafana är en applikation, inte ett programmeringsspråk
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är en NSG (Network Security Group) i Azure?

a. En grupp av servrar<br>b. En brandväggsregel som styr trafik till och från Azure-resurser — tillåter/blockerar baserat på källa, port, protokoll<br>c. En nätverkskabel<br>d. En VPN-anslutning

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En brandväggsregel som styr trafik till och från Azure-resurser

  **Förklaringar:**

  - ❌ **a) Servergrupp** - FEL: NSG är regler, inte servrar
  - ✅ **b) Brandväggsregler** - **RÄTT**: NSG kopplas till subnät eller nätverkskort. Regler: "Allow SSH from 10.0.0.0/8 on port 22", "Deny all other inbound". Default: all inbound blocked, all outbound allowed. Prioritet på varje regel (100, 200...). Azure Application Gateway för web traffic
  - ❌ **c) Nätverkskabel** - FEL: 😄 Virtuella resurser har inga kablar
  - ❌ **d) VPN** - FEL: VPN kopplar ihop nätverk. NSG SKYDDAR resurser inom ett nätverk
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är Azure Key Vault?

a. En tjänst för att låsa virtuella maskiner<br>b. En molntjänst för att säkert lagra och hantera hemligheter — API-nycklar, lösenord, certifikat, anslutningssträngar<br>c. Ett låssystem för kontoret<br>d. En databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En molntjänst för att säkert lagra och hantera hemligheter

  **Förklaringar:**

  - ❌ **a) Låsa VMs** - FEL: Key Vault lagrar hemligheter, den låser inte resurser
  - ✅ **b) Hemlighetshantering** - **RÄTT**: Key Vault lagrar: API-nycklar (t.ex. för Stripe), databas-connection strings, certifikat, krypteringsnycklar. Åtkomst via Azure AD (Managed Identity). Din app läser hemligheten vid runtime — aldrig i koden eller config-filer. Automatisk rotation av certifikat
  - ❌ **c) Låssystem** - FEL: 😄 Key Vault är digitalt, inte fysiskt
  - ❌ **d) Databas** - FEL: Key Vault lagrar hemligheter, inte applikationsdata
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är VNet-peering i Azure?

a. Att koppla ihop två virtuella nätverk (VNet) så de kan kommunicera som om de vore ett nätverk — låg latens, ingen gateway<br>b. Att koppla ihop en VM med Internet<br>c. Att skapa ett nytt virtuellt nätverk<br>d. Att dela upp ett nätverk i subnät

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att koppla ihop två virtuella nätverk så de kan kommunicera som ett nätverk

  **Förklaringar:**

  - ✅ **a) Ihopkoppling av VNets** - **RÄTT**: Du har VNet-A (10.0.0.0/16) och VNet-B (10.1.0.0/16). Peering → resurser i A kan prata med B med privat IP. Ingen gateway behövs. Trafiken stannar i Microsofts backbone. Används för hub-and-spoke, region-till-region
  - ❌ **b) VM till Internet** - FEL: Det är en NAT-gateway eller public IP
  - ❌ **c) Nytt VNet** - FEL: Peering KOPPLAR IHOP befintliga, skapar inte nya
  - ❌ **d) Subnät** - FEL: Subnät är inom ett VNet. Peering är MELLAN VNets
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är Azure Application Insights?

a. En tjänst för att göra A/B-tester<br>b. En Application Performance Management (APM)-tjänst — övervakar live-applikationer, upptäcker avvikelser, diagnostiserar fel<br>c. En CI/CD-pipeline<br>d. En loggfil

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En APM-tjänst — övervakar live-applikationer och diagnostiserar fel

  **Förklaringar:**

  - ❌ **a) A/B-tester** - FEL: Application Insights är för övervakning, inte experiment
  - ✅ **b) APM** - **RÄTT**: Application Insights är en del av Azure Monitor. Den instrumenterar din app med ett SDK: samlar in: requests, exceptions, dependencies (databasanrop), page views, traces. Du får "Smart Detection" — AI som upptäcker avvikelser utan att du konfigurerar. Distribuerad tracing
  - ❌ **c) CI/CD** - FEL: Application Insights övervakar efter deployment, den bygger inte
  - ❌ **d) Loggfil** - FEL: Det är en tjänst med dashboard, sökning, alerting — inte bara en fil
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad innebär att sätta upp en alert (varning) för ett produktionssystem?

a. Att skicka ett brev till chefen<br>b. Att konfigurera en tröskel (t.ex. CPU > 90% i 5 minuter) som utlöser en notifikation (mejl, SMS, PagerDuty, Slack) — så driftteamet agerar innan kunder påverkas<br>c. Att starta om alla servrar<br>d. Att stänga av systemet

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En tröskel som utlöser notifikation så driftteamet agerar innan kunder påverkas

  **Förklaringar:**

  - ❌ **a) Brev** - FEL: 😄 Lite för långsamt
  - ✅ **b) Tröskel + notifikation** - **RÄTT**: Bra alert = rätt tröskel (inte för känslig → alarm fatigue, inte för slö → missar problem). Severity: Critical (svarstid > 5s), Warning (svarstid > 2s). Runbook för varje alert: "Vad gör jag när denna larmar?"
  - ❌ **c) Starta om** - FEL: Alerts NOTIFIERAR, de agerar inte automatiskt (om du inte har auto-remediation)
  - ❌ **d) Stänga av** - FEL: 😄 Det skulle göra problemet värre
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
