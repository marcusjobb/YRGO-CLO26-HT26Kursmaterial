# Träningsuppgifter: Cloud Native — Modul 1

> **Modul:** 01 — Continuous Delivery (deployment-gates, feature flags, automatiserat rollout), 02 — Serverless (Azure Functions, AWS Lambda, event-driven)

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är en deployment-gate i en CI/CD-pipeline?

a. En grind som måste öppnas fysiskt<br>b. En automatisk eller manuell kontrollpunkt som måste passeras innan koden får gå vidare till nästa miljö — t.ex. "alla tester gröna" eller "godkänd av verksamheten"<br>c. En kod-editor<br>d. En databas-fråga

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En kontrollpunkt som måste passeras innan koden får gå vidare till nästa miljö

  **Förklaringar:**

  - ❌ **a) Fysisk grind** - FEL: 😄 Deployment-gates är digitala
  - ✅ **b) Kontrollpunkt** - **RÄTT**: Exempel: Bygge → Test (gate: alla tester gröna) → Stage (gate: godkänd av testare) → Production (gate: godkänd av verksamheten + ingen critical alert). Azure DevOps: "Approvals and gates" — kräver godkännande eller väntar på en extern tjänst
  - ❌ **c) Kod-editor** - FEL: Gates är i pipelinen, inte i utvecklingsverktyget
  - ❌ **d) Databas-fråga** - FEL: Gates är villkor i pipeline-flödet
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är en feature flag (feature toggle)?

a. En flagga som visas på webbplatsen<br>b. En mekanism för att slå på/av funktioner i produktion utan att deploya ny kod — ofta via konfiguration eller A/B-testning<br>c. En git-branch<br>d. En flagga i koden som markerar TODO

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En mekanism för att slå på/av funktioner utan att deploya ny kod

  **Förklaringar:**

  - ❌ **a) Webbplats-flagga** - FEL: Feature flags är osynliga för användaren — de styr beteende
  - ✅ **b) Toggle i produktion** - **RÄTT**: `if (featureFlags.isEnabled("new-checkout")) { ... }`. Du deployar koden men funktionen är AV. Slå PÅ när du är redo. Används för: canary (10% får nya flödet), kill switch (stäng av om fel upptäcks), trunk-based development (deploya ofta utan att alla features är klara)
  - ❌ **c) Git-branch** - FEL: Feature branches är i Git. Feature flags är i runtime
  - ❌ **d) TODO-flagga** - FEL: TODO i koden är kommentarer, inte runtime-kontroll
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är serverless computing?

a. Att köra kod utan att hantera servrar — molnleverantören skalar och hanterar infrastrukturen, du betalar bara när koden körs<br>b. Att inte använda servrar alls<br>c. Att använda en server utan operativsystem<br>d. Att hyra fysiska servrar

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Köra kod utan att hantera servrar — molnleverantören skalar, du betalar per exekvering

  **Förklaringar:**

  - ✅ **a) Serverlös körning** - **RÄTT**: Serverless ≠ inga servrar. Servrar finns men du ser dem aldrig. Du skriver en funktion, molnet kör den. Azure Functions, AWS Lambda. Skalning: 0 → 1000 instanser på sekunder. Betalning: per anrop och förbrukad tid (ms)
  - ❌ **b) Inga servrar** - FEL: Det finns fortfarande servrar — du bara administrerar dem inte
  - ❌ **c) Utan OS** - FEL: Servrar har OS, du ser dem bara inte
  - ❌ **d) Hyra fysiska** - FEL: Fysiska servrar är motsatsen — det är on-premises / bare metal
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är en Azure Function?

a. En virtuell maskin i Azure<br>b. En serverless compute-tjänst som kör en specifik funktion när en händelse inträffar — t.ex. HTTP-anrop, filuppladdning, kö-meddelande<br>c. En databas<br>d. Ett nätverksverktyg

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En serverless compute-tjänst som kör en funktion vid en händelse

  **Förklaringar:**

  - ❌ **a) VM** - FEL: VMs kräver hantering. Azure Functions är serverless
  - ✅ **b) Händelsestyrd funktion** - **RÄTT**: `[FunctionName("ProcessOrder")]` → triggas av HTTP, Queue (Storage Queue), Timer (cron-schema), CosmosDB change feed, Event Grid. Koden är bara funktionen — ingen serverkonfiguration. Kör C#, JS, Python, Java
  - ❌ **c) Databas** - FEL: Functions är compute, inte lagring
  - ❗ **d) Nätverksverktyg** - FEL: Functions är för applikationslogik, inte nätverk
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är en event-driven arkitektur?

a. Att programmet väntar på användarens knapptryckningar innan det gör något<br>b. En arkitektur där komponenter kommunicerar via händelser (events) — en komponent publicerar en händelse, andra prenumererar och reagerar asynkront<br>c. En arkitektur där allt händer samtidigt<br>d. En databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Komponenter kommunicerar via händelser — publicera/prenumerera, asynkront

  **Förklaringar:**

  - ❌ **a) Knapptryckningar** - FEL: Det är UI-event, inte arkitekturmönster
  - ✅ **b) Publicera/prenumerera** - **RÄTT**: Order-service publicerar "OrderPlaced". Payment-service prenumererar → hanterar betalning. Notification-service prenumererar → skickar bekräftelse. Inga tjänster vet om varandra direkt. Löst kopplade. Azure Event Grid, Kafka, RabbitMQ
  - ❌ **c) Allt samtidigt** - FEL: Event-driven är ofta asynkront, inte nödvändigtvis parallellt
  - ❌ **d) Databas** - FEL: Event-driven är ett arkitekturmönster, inte en databas
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är cold start i serverless?

a. När servern startas efter att ha varit avstängd<br>b. Fördröjningen första gången en serverless-funktion anropas efter att den varit inaktiv — plattformen måste ladda runtime och din kod i minnet<br>c. När databasen startar<br>d. En temperatur i serverhallen

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Fördröjningen första gången en serverless-funktion anropas efter inaktivitet

  **Förklaringar:**

  - ❌ **a) Server start** - FEL: Det finns ingen traditionell server att starta
  - ✅ **b) Laddning av runtime** - **RÄTT**: Efter en period av inaktivitet frigör plattformen resurserna. Nästa anrop måste: ladda runtime (.NET runtime, Node.js, etc.), ladda din kod, exekvera. Detta kan ta 1–10 sekunder. Lösningar: Premium-plan (alltid varm), provisioned concurrency (AWS), eller hålla funktionen varm med en timer-trigger
  - ❌ **c) Databas start** - FEL: Cold start handlar om serverless-funktionen, inte databasen
  - ❌ **d) Temperatur** - FEL: 😄 Metafor, inte bokstavlig temperatur
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
