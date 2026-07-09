# Träningsuppgifter: Cloud Native — Modul 2

> **Modul:** 03 — Skalning och lastbalansering (horisontell skalning, load balancer, auto-scaling), 04 — Automation Bash/PowerShell (skriptautomation)

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är skillnaden mellan vertikal och horisontell skalning?

a. Vertikal = byta till större server (skala upp). Horisontell = lägga till fler servrar (skala ut)<br>b. Vertikal = fler servrar, horisontell = större server<br>c. Samma sak<br>d. Vertikal är för databaser, horisontell är för appar

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Vertikal = större server (skala upp). Horisontell = fler servrar (skala ut)

  **Förklararningar:**

  - ✅ **a) Upp vs ut** - **RÄTT**: Vertikal: byt VM från 4 cores/16GB till 16 cores/64GB. Enklare men har en övre gräns. Horisontell: lägg till 3 till instanser bakom en load balancer. Nästan obegränsad. Kräver stateless design (ingen session på servern)
  - ❌ **b) Omvänt** - FEL: Det är tvärtom
  - ❌ **c) Samma** - FEL: Fundamentalt olika strategier
  - ❌ **d) DB vs app** - FEL: Båda kan skalas vertikalt och horisontellt
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är auto-scaling i molnet?

a. Att manuellt lägga till servrar när det blir mycket trafik<br>b. En automatisk mekanism som ökar eller minskar antalet instanser baserat på regler — t.ex. CPU > 70% i 5 min → lägg till 2 instanser<br>c. Att stänga av servrar på natten<br>d. Att byta till snabbare processor

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Automatisk ökning/minskning av instanser baserat på regler

  **Förklaringar:**

  - ❌ **a) Manuellt** - FEL: Auto-scaling är just AUTOMATISK — ingen människa behöver vakna kl 03
  - ✅ **b) Regelbaserad skalning** - **RÄTT**: Azure App Service auto-scale: "Om CPU > 80% i 10 min → skala ut till 5 instanser. Om CPU < 30% i 10 min → skala ner till 2 instanser." Min/Max-instanser hindrar för mycket/ lite. Schemalagd skalning för kända mönster (black Friday)
  - ❌ **c) Stänga av natt** - FEL: Auto-scaling reagerar på belastning, inte tid (schemalagd skalning finns men är inte detsamma)
  - ❌ **d) Snabbare processor** - FEL: Det är vertikal skalning, inte auto-scaling
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad innebär "stateless" i en horisontellt skalad applikation?

a. Appen sparar inget tillstånd lokalt — all session-data lagras externt (Redis, databas) så vilken instans som helst kan hantera vilken begäran som helst<br>b. Appen har inget tillstånd alls — den fungerar inte<br>c. Appen sparar allt i minnet på servern<br>d. Appen använder inte databaser

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ingen session lagras lokalt — allt tillstånd lagras externt

  **Förklaringar:**

  - ✅ **a) Externt tillstånd** - **RÄTT**: Användare A → instans 1 (första anropet). Användare A → instans 3 (andra anropet). Fungerar bara om sessionen finns i Redis eller databas. "Sticky sessions" (klistra användare till en instans) är en workaround, inte en lösning
  - ❌ **b) Fungerar inte** - FEL: Stateless appar fungerar utmärkt — de lagrar bara tillstånd externt
  - ❌ **c) Lokalt minne** - FEL: Det är stateful och fungerar inte med horisontell skalning
  - ❌ **d) Inga databaser** - FEL: Stateless handlar om session, inte om databaser
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är Bash?

a. Ett grafiskt operativsystem<br>b. Ett kommandoskal och skriptspråk för Linux/Unix — används för automation, filhantering, systemadministration<br>c. En databas<br>d. En molntjänst

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett kommandoskal och skriptspråk för Linux/Unix

  **Förklaringar:**

  - ❌ **a) Grafiskt OS** - FEL: Bash är textbaserat, inte grafiskt
  - ✅ **b) Kommandoskal + skriptspråk** - **RÄTT**: Bash (Bourne Again SHell) är standard på Linux. Du kör kommandon direkt eller skriver skript (`.sh`-filer). Används i DevOps för: provisionering, deployment, logganalys, cron-jobb. `grep`, `sed`, `awk`, `curl`, `jq` är dina vänner
  - ❌ **c) Databas** - FEL: Bash pratar med databaser men är inte en själv
  - ❌ **d) Molntjänst** - FEL: Bash körs på din maskin eller i en container
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är PowerShell?

a. Ett kommandoskal för Windows (nu även Linux/Mac) — objektorienterat skriptspråk för systemadministration och automation<br>b. Ett grafiskt verktyg<br>c. En text-editor<br>d. En molntjänst

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett kommandoskal för Windows (nu cross-platform) — objektorienterat skriptspråk

  **Förklaringar:**

  - ✅ **a) Objektorienterat skal** - **RÄTT**: Skillnad mot Bash: PowerShell jobbar med objekt (inte text). `Get-Process` returnerar process-objekt — du filtrerar med `Where-Object`, inte `grep`. PowerShell Core (pwsh) körs på Linux/Mac. `.ps1`-filer. Bra för Azure-automation (Azure Az-module)
  - ❌ **b) Grafiskt** - FEL: PowerShell är textbaserat (med GUI-verktyg som ISE)
  - ❌ **c) Text-editor** - FEL: PowerShell kör kommandon, den redigerar inte filer (det gör VS Code)
  - ❌ **d) Molntjänst** - FEL: PowerShell är ett verktyg för att hantera molntjänster, inte en tjänst i sig
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är ett shell-skript?

a. En samling kommandon i en fil som körs sekventiellt — automation av repetitiva uppgifter<br>b. En kompilerad programfil<br>c. En databas-fråga<br>d. En konfigurationsfil för en webbserver

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En samling kommandon i en fil som körs sekventiellt — automation

  **Förklaringar:**

  - ✅ **a) Automationsskript** - **RÄTT**: `#!/bin/bash` följt av kommandon. Exempel: backup-skript som tar en dump, komprimerar, laddar upp till Blob Storage, och skickar ett Slack-meddelande. Körs manuellt eller via cron. Gör utvecklare produktiva — en gång skriven, körs tusentals gånger
  - ❌ **b) Kompilerad fil** - FEL: Skript tolkas (interpreted), inte kompileras
  - ❌ **c) Databas-fråga** - FEL: Ett shell-skript innehåller OS-kommandon, inte SQL
  - ❌ **d) Konfigurationsfil** - FEL: Konfigurationsfiler (nginx.conf, appsettings.json) sätter parametrar. Skript UTFÖR handlingar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är en load balancers hälsoavsökning (health probe)?

a. En kontroll av databasens hälsa<br>b. En regelbunden kontroll som load balancern gör mot varje server — om servern inte svarar tas den ur rotation tills den återhämtat sig<br>c. En användarundersökning<br>d. En typ av backup

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En regelbunden kontroll som load balancern gör mot varje server — ohälsosamma servrar tas ur rotation

  **Förklaringar:**

  - ❌ **a) Databaskontroll** - FEL: Health probe kollar app-serverns hälsa, inte databasen
  - ✅ **b) Server-hälsokontroll** - **RÄTT**: Load balancern skickar en HTTP-fråga till `/health` var 5:e sekund. Om servern svarar 200 OK → den är frisk. Om 503 eller timeout 3 gånger → servern tas ur rotation. När servern svarar OK igen → tillbaka i rotation. Detta säkerställer att användare aldrig skickas till en trasig server
  - ❌ **c) Användarundersökning** - FEL: 😄 Användarna bryr sig om appen är uppe, inte att kolla
  - ❌ **d) Backup** - FEL: Health probe är för tillgänglighet, inte datasäkerhet
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är ett cron-jobb i Linux?

a. Ett jobb som körs manuellt<br>b. En schemalagd uppgift som körs vid bestämda tidpunkter — t.ex. varje natt kl 03:00 ta backup och rensa loggar<br>c. En typ av databas<br>d. Ett nätverksprotokoll

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En schemalagd uppgift som körs vid bestämda tidpunkter

  **Förklaringar:**

  - ❌ **a) Manuellt** - FEL: Cron är AUTOMATISK schemaläggning
  - ✅ **b) Tidsbaserad automation** - **RÄTT**: `crontab -e` redigerar dina cron-jobb. Syntax: `0 3 * * * /skript/backup.sh" = "kör varje dag kl 03:00". Fem fält: minut, timme, dag i månaden, månad, dag i veckan. PowerShell Scheduled Tasks är motsvarigheten på Windows
  - ❌ **c) Databas** - FEL: Cron är en tidshanterare, inte en databas
  - ❌ **d) Nätverksprotokoll** - FEL: Cron är lokal schemaläggning på en Linux-maskin
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
