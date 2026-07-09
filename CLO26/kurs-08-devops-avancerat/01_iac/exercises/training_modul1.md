# Träningsuppgifter: DevOps Avancerat — Modul 1

> **Modul:** 01 — IaC (Terraform, Bicep, ARM), 02 — CI/CD avancerat (blue-green, canary, rollback)

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är IaC (Infrastructure as Code)?

a. Att skriva dokumentation om din infrastruktur<br>b. Att definiera och hantera infrastruktur (servrar, nätverk, databaser) genom kod och deklarativa konfigurationsfiler — istället för manuella processer<br>c. En kod-editor<br>d. Ett sätt att kompilera kod

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att definiera och hantera infrastruktur genom kod och deklarativa konfigurationsfiler

  **Förklaringar:**

  - ❌ **a) Dokumentation** - FEL: IaC är INTE dokumentation — koden ÄR infrastrukturen
  - ✅ **b) Infrastruktur som kod** - **RÄTT**: Du skriver `main.tf` (Terraform) eller `main.bicep` som beskriver: 3 VMs, ett virtuellt nätverk, en databas. `terraform apply` skapar allt. Versionhanterat i Git. Reproducerbart
  - ❌ **c) Kod-editor** - FEL: IaC är ett arbetssätt, inte ett verktyg för att skriva kod
  - ❌ **d) Kompilera** - FEL: IaC handlar om att PROVISIONERA infrastruktur, inte kompilera program
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är skillnaden mellan Terraform och Bicep?

a. Terraform fungerar med flera molnleverantörer (Azure, AWS, GCP), Bicep är Azure-specifikt och är en förenkling av ARM-templates<br>b. De är samma sak<br>c. Bicep fungerar med alla moln, Terraform bara med Azure<br>d. Båda är deprekerade

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Terraform fungerar med flera moln, Bicep är Azure-specifikt (förenkling av ARM)

  **Förklaringar:**

  - ✅ **a) Multi-cloud vs Azure-only** - **RÄTT**: Terraform använder HCL (HashiCorp Language) och providers för varje moln. Bicep är Microsofts DSL som kompileras till ARM-templates. Väljer du multi-cloud → Terraform. Bara Azure → Bicep (enklare, bättre integration)
  - ❌ **b) Samma sak** - FEL: Olika språk, olika ekosystem
  - ❌ **c) Omvänt** - FEL: Tvärtom — Bicep är Azure-only
  - ❌ **d) Deprekerade** - FEL: Båda är aktiva och välanvända
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad innebär Terraform state?

a. En loggfil<br>b. En fil (terraform.tfstate) som håller koll på verklig infrastruktur — Terraform jämför koden mot staten för att veta vad som ska skapas, ändras eller tas bort<br>c. En databas<br>d. En konfigurationsfil

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En fil som håller koll på verklig infrastruktur — för att veta vad som ska skapas/ändras/tas bort

  **Förklaringar:**

  - ❌ **a) Loggfil** - FEL: State är inte loggar — det är en karta över din infrastruktur
  - ✅ **b) State = verklighetskoll** - **RÄTT**: Terraform läser state för att se: "Just nu finns VM A och Nätverk B. Koden vill ha VM A, Nätverk B, DB C. Skillnad = skapa DB C." State ska lagras säkert (remote backend: Azure Storage, S3) — aldrig i Git. Låsning (locking) förhindrar race conditions
  - ❌ **c) Databas** - FEL: State är en JSON-fil, inte en databas
  - ❌ **d) Konfigurationsfil** - FEL: .tf-filer är konfiguration. .tfstate är state
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är en ARM-template i Azure?

a. En mall för att skriva C#-kod<br>b. En JSON-fil som deklarativt beskriver Azure-resurser och deras konfiguration — Resource Manager-template<br>c. En ritning på papper<br>d. Ett PowerShell-skript

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En JSON-fil som deklarativt beskriver Azure-resurser och deras konfiguration

  **Förklaringar:**

  - ❌ **a) C#-mall** - FEL: ARM är för Azure-infrastruktur, inte applikationskod
  - ✅ **b) JSON + deklarativ** - **RÄTT**: ARM-template (Azure Resource Manager) är JSON med: `resources` (VMs, networks, storage), `parameters` (användarens input), `variables`, `outputs`. Bicep kompileras till ARM. Används för repeatable deployments
  - ❌ **c) Ritning** - FEL: 😄 Nej, det är digitalt
  - ❌ **d) PowerShell-skript** - FEL: ARM är deklarativ JSON. PowerShell är imperativt — ARM säger VAD, PowerShell säger HUR
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad innebär blue-green deployment?

a. Att ha blå och gröna servrar som dekoration<br>b. En deployment-strategi där du har två identiska miljöer (blue = live, green = ny version). När green är testad, byter du trafik — omedelbar switch med noll stillestånd<br>c. Att måla servrarna blåa och gröna<br>d. En A/B-testningsmetod för användargränssnitt

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Två identiska miljöer — en live, en med ny version. Byt trafik när den nya är testad

  **Förklaringar:**

  - ❌ **a) Dekoration** - FEL: 😄 Blue/green är namn på miljöerna, inte färg på servrar
  - ✅ **b) Två miljöer, snabb switch** - **RÄTT**: Blue = production (alla användare). Green = deployad med v2. Testa green internt. När green är OK → switcha load balancer (router, DNS). Vid problem → switcha tillbaka till blue. Rollback på sekunder
  - ❌ **c) Måla servrar** - FEL: Nej, det är metaforiska namn
  - ❌ **d) A/B-test** - FEL: A/B testar olika funktioner på användare. Blue/green byter HELA appen
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är canary release?

a. Att släppa en ny version till en liten andel användare först — för att upptäcka problem innan alla påverkas<br>b. Att skicka ut appen till alla samtidigt<br>c. En release som bara fungerar på fredagar<br>d. Att använda kanariefåglar för att testa luftkvalitet

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att släppa en ny version till en liten andel användare först

  **Förklaringar:**

  - ✅ **a) Gradvis rullout** - **RÄTT**: 5% av användarna får v2, 95% får v1. Övervaka metrics (errors, latency). Om allt ser bra ut → öka till 25%, sen 50%, sen 100%. Om problem → rulla tillbaka bara de 5%. Namnet från "canary in a coal mine"
  - ❌ **b) Alla samtidigt** - FEL: Det är en "big bang release", motsatsen till canary
  - ❌ **c) Fredagar** - FEL: 😄 Inget med veckodag att göra
  - ❌ **d) Kanariefåglar** - FEL: Metaforen kommer från gruvindustrin, men det är inte bokstavligt
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är en rollback-strategi?

a. Att aldrig deploya ny kod<br>b. En plan för att snabbt återgå till en tidigare, stabil version om en deployment orsakar problem — minimera downtime<br>c. Att ta bort all data<br>d. Att skriva en buggrapport

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En plan för att snabbt återgå till en tidigare, stabil version om en deployment orsakar problem

  **Förklaringar:**

  - ❌ **a) Aldrig deploya** - FEL: Då kommer ingen kod till produktion alls
  - ✅ **b) Snabb återgång** - **RÄTT**: Blue-green: byt trafik tillbaka. Canary: stoppa trafik till ny version. Rolling: redeploya föregående version. Viktigt: databasmigrationer måste vara bakåtkompatibla (v2 kan fungera med v1-databas)
  - ❌ **c) Ta bort data** - FEL: Rollback = återställ kod, inte radera data
  - ❌ **d) Buggrapport** - FEL: Buggrapport är EFFER att problemet upptäckts. Rollback är att ÅTGÄRDA det
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad innebär "idempotent" inom IaC?

a. Att en operation alltid ger samma resultat oavsett hur många gånger den körs — applicera samma konfiguration flera gånger utan att skapa dubbletter<br>b. Att operationen är snabb<br>c. Att operationen kräver lösenord<br>d. Att operationen bara fungerar en gång

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En operation som alltid ger samma resultat oavsett hur många gånger den körs

  **Förklaringar:**

  - ✅ **a) Samma resultat varje gång** - **RÄTT**: Terraform `apply` en gång → skapa resurser. Terraform `apply` igen → "Nothing to do" (allt finns redan som koden säger). Terraform tar bort manuella ändringar för att återställa till kodens önskade tillstånd. Detta är styrkan med deklarativ IaC
  - ❌ **b) Snabb** - FEL: Idempotent handlar om korrekthet, inte hastighet
  - ❌ **c) Lösenord** - FEL: Inget med autentisering att göra
  - ❌ **d) Bara en gång** - FEL: Tvärtom — det fungerar likadant varje gång
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
