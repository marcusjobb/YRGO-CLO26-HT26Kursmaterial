# Träningsuppgifter: Cloud Intro — Modul 1

> **Modul:** 01 — Cloud Intro (IaaS, PaaS, SaaS, molnmodeller)

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad står IaaS för?

a. Internet as a Service<br>b. Infrastructure as a Service — virtuella servrar, nätverk och lagring du hyr i molnet<br>c. Integration as a Service<br>d. Information as a Service

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Infrastructure as a Service — virtuella servrar, nätverk och lagring du hyr i molnet

  **Förklaringar:**

  - ❌ **a) Internet** - FEL: Inte en molntjänstmodell
  - ✅ **b) Infrastructure as a Service** - **RÄTT**: IaaS ger dig virtuella maskiner, nätverk och lagring. Du ansvarar själv för OS, middleware och appar. Exempel: Azure VM, AWS EC2
  - ❌ **c) Integration** - FEL: Det är iPaaS, inte IaaS
  - ❌ **d) Information** - FEL: Finns som begrepp men inte standard molnmodell
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är skillnaden mellan IaaS, PaaS och SaaS?

a. De är samma sak med olika namn<br>b. Olika nivåer av ansvar — IaaS ger mest kontroll och mest ansvar, SaaS minst kontroll och minst ansvar<br>c. IaaS är för stora företag, PaaS för medelstora, SaaS för små<br>d. De är olika prismodeller

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Olika nivåer av ansvar — IaaS ger mest kontroll och mest ansvar, SaaS minst kontroll och minst ansvar

  **Förklaringar:**

  - ❌ **a) Samma sak** - FEL: Olika nivåer av abstraktion
  - ✅ **b) Olika ansvarsnivåer** - **RÄTT**: IaaS = du hyr hårdvara, PaaS = du hyr plattform (Azure App Service), SaaS = du hyr färdig app (Office 365). Lägre nivå = mer kontroll, mer ansvar
  - ❌ **c) Företagsstorlek** - FEL: Alla modeller används av alla företagsstorlekar
  - ❤ **d) Prismodeller** - FEL: Det handlar om tjänstenivå, inte prissättning
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad innebär Shared Responsibility Model i molnet?

a. Molnleverantören ansvarar för all säkerhet<br>b. Du ansvarar för all säkerhet<br>c. Säkerhetsansvaret delas — molnleverantören ansvarar för molnets säkerhet, du ansvarar för säkerheten i molnet<br>d. Inget ansvar alls

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Säkerhetsansvaret delas — molnleverantören ansvarar för molnets säkerhet, du ansvarar för säkerheten i molnet

  **Förklaringar:**

  - ❌ **a) Leverantören ansvarar** - FEL: Delat ansvar, inte bara leverantören
  - ❌ **b) Du ansvarar** - FEL: Leverantören ansvarar för den fysiska infrastrukturen
  - ✅ **c) Delat ansvar** - **RÄTT**: Azure ansvarar för fysisk säkerhet i datacenter, nätverk, hypervisor. Du ansvarar för åtkomstkontroll, data, OS-konfiguration, nätverkssäkerhet i din miljö
  - ❌ **d) Inget ansvar** - FEL: Du har ALLTID ansvar för din data
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vilka är de tre vanligaste molnmodellerna?

a. Stor, Mellan, Liten<br>b. Publik moln, Privat moln, Hybridmoln<br>c. Azure, AWS, GCP<br>d. Gratis, Premium, Enterprise

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Publik moln, Privat moln, Hybridmoln

  **Förklaringar:**

  - ❌ **a) Storlek** - FEL: Molnmodeller handlar om ägande och placering
  - ✅ **b) Publik, Privat, Hybrid** - **RÄTT**: Publik = delad infrastruktur (Azure, AWS). Privat = egna servrar (on-premises). Hybrid = blandning. Många företag kör hybrid — känslig data lokalt, övrigt i molnet
  - ❌ **c) Leverantörer** - FEL: Det är molnleverantörer, inte molnmodeller
  - ❌ **d) Priskategorier** - FEL: Inte molnmodeller
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är en "region" i molnet?

a. Ett lands gränser<br>b. Ett geografiskt område med ett eller flera datacenter — närmare region = lägre latens<br>c. En typ av virtuell maskin<br>d. En prismodell

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett geografiskt område med ett eller flera datacenter — närmare region = lägre latens

  **Förklaringar:**

  - ❌ **a) Landsgränser** - FEL: Regioner följer inte landgränser (även om de ofta ligger inom ett land)
  - ✅ **b) Geografiskt datacenterområde** - **RÄTT**: Azure har regioner som "West Europe" (Nederländerna), "North Europe" (Irland). Välj region nära dina användare för låg latens. Vissa regioner finns bara för specifika datalagar
  - ❌ **c) Virtuell maskin** - FEL: Regioner är var datacenter finns, inte VM-typer
  - ❌ **d) Prismodell** - FEL: Regioner påverkar pris, men är inte en prismodell
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Varför är Linux standard i molnet?

a. För att Windows inte fungerar i molnet<br>b. Linux är gratis, stabilt, skalbart och dominerar servervärlden — de flesta molntjänster körs på Linux<br>c. För att utvecklare gillar Linux<br>d. För att Microsoft tvingar alla att använda Linux

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Linux är gratis, stabilt, skalbart och dominerar servervärlden

  **Förklaringar:**

  - ❌ **a) Windows fungerar inte** - FEL: Windows fungerar utmärkt i molnet, men Linux är mer använt
  - ✅ **b) Gratis, stabilt, dominant** - **RÄTT**: Linux är open source (inga licenskostnader), extremt stabilt (år av uptime), och används på majoriteten av världens servrar. Containrar (Docker) bygger på Linux-kärnan
  - ❌ **c) Utvecklare gillar det** - FEL: Sant men inte huvudanledningen
  - ❌ **d) Microsoft tvingar** - FEL: Microsoft äger Azure och stöder Linux fullt ut — mer än hälften av Azure VM kör Linux
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
