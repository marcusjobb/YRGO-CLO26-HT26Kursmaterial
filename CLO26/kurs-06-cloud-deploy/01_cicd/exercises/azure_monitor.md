# Azure Monitor — håll koll på HyresBil-appen

🟡

---

Du har deployt **HyresBil**-appen på en Azure VM. Appen rullar. Studerande kan boka bilar. Skönt.

Men vad händer när VM:en knakar under hög last — eller går ner mitt i natten? Du märker det när en arg kund ringer. Det är inte idealiskt.

Den här övningen handlar om att ändra det. Vi sätter upp Azure Monitor så att du **vet** när CPU:n skenar eller när VM:en tystnar — innan någon annan märker det.

> **15-minutersregeln:** Fastnar du mer än 15 minuter på ett steg — fråga. Stolt av det, inte skamset.

---

## Du lär dig

- Skapa ett Log Analytics Workspace (LAWS) som samlar in all data
- Installera Azure Monitor Agent (AMA) på din VM
- Sätta upp en alert som pingar dig när CPU:n går över 80 %
- Läsa Metrics i portalen — CPU, nätverk, disk
- Förstå skillnaden mellan **Metrics** (realtid) och **Logs** (historik + KQL)

## Du behöver

- En köande HyresBil-VM i Azure (Ubuntu, med din app eller bara tom Nginx räcker)
- Tillgång till [portal.azure.com](https://portal.azure.com)
- En e-postadress du faktiskt kan ta emot mail på

---

## Steg 1 — Skapa ett Log Analytics Workspace

Tänk på Log Analytics Workspace som en **svart låda** — allting som Azure Monitor samlar in hamnar här. Metrics, loggar, heartbeats. Utan den har du ingen övervakning.

1. Sök på `Log Analytics workspaces` i portalen
2. Klicka `+ Create`
3. Fyll i:
   - **Subscription**: din prenumeration
   - **Resource Group**: skapa ny, kalla den `HyresBilRG` (eller lägg till i befintlig)
   - **Name**: `hyresbil-laws`
   - **Region**: `North Europe`
4. Klicka `Review + Create` → `Create`

> **Snabbkoll:** Workspace-statusen visar `Succeeded` i portalen.

> **Notera:** Ett nytt workspace tar 5–10 minuter på att "värma upp" innan data börjar dyka upp i queries. Det är normalt — kör vidare.

---

## Steg 2 — Installera Azure Monitor Agent på VM:en

Azure Monitor Agent (AMA) är den lilla tjänsten som körs på din VM och skickar data till workspace:t. Den ersätter det gamla Log Analytics-agenten (MMA/OMS) — använd alltid AMA nu.

För att AMA ska få kommunicera med Azure behöver VM:en en **hanterad identitet** (managed identity). Det är som ett ID-kort inbyggt i VM:en — inga lösenord sparas någonstans.

1. Gå till din VM i portalen
2. I vänstermenyn: **Monitoring → Insights**
3. Klicka `Enable`
4. Kryssa i `Enable guest performance`
5. Under **Data collection rule**: välj `Create New`
   - **Name**: `hyresbil-dcr`
   - **Log Analytics workspace**: `hyresbil-laws`
   - Klicka `Create`
6. Klicka `Configure`

Det här steget gör flera saker på en gång: skapar en hanterad identitet, installerar AMA som ett VM-tillägg, och sätter upp en Data Collection Rule (DCR) för CPU, minne och disk.

> **Snabbkoll:** Under Insights → Performance ser du grafer för CPU, Memory och Disk (kan ta 15–20 min att dyka upp).

> **Begreppslåda — Data Collection Rules (DCR):**
> En DCR är en konfigurationsfil som definierar *vad* som ska samlas in, *hur* det ska behandlas och *vart* det ska skickas. Den håller konfigurationen separat från agenten — du kan uppdatera vad som samlas in utan att röra agenten. Modulärt, precis som bra kod.

---

## Steg 3 — Sätt upp en alert för CPU > 80 %

Nu det roliga. Vi sätter upp en alert som skickar mail om HyresBil-VM:en maxar CPU:n — t.ex. när någon kör en hård query eller om appen läcker minne.

1. Gå till din VM
2. I vänstermenyn: **Monitoring → Alerts**
3. Klicka `+ Create → Alert rule`

**Villkor (Condition):**
4. Klicka `Add condition`
5. Välj signal: **Percentage CPU**
6. Sätt:
   - **Operator**: Greater than
   - **Threshold value**: `80`
   - **Aggregation type**: Average
   - **Aggregation granularity**: 5 minutes
7. Klicka `Done`

**Actions:**
8. Klicka `Add action groups` → `Create action group`
9. Fyll i:
   - **Action group name**: `hyresbil-alert-group`
   - **Display name**: `HyresBil Ops`
10. Under fliken **Notifications**:
    - **Notification type**: Email/SMS/Push/Voice
    - Välj `Email` → fyll i din e-postadress
    - **Name**: `min-mail`
11. Klicka `Review + create` → `Create`

**Detaljer på alert-regeln:**
12. Tillbaka i alert-flödet:
    - **Alert rule name**: `CPU-over-80-procent`
    - **Severity**: 2 - Warning
13. Klicka `Review + create` → `Create`

> **Snabbkoll:** Alert-regeln syns under Monitoring → Alerts → Alert rules med status `Enabled`.

> **OBS:** Alertar på platform metrics (som CPU) triggar snabbt. Alertar på Log Analytics-queries har 5–10 min fördröjning.

---

## Steg 4 — Titta på Metrics i portalen

Metrics är **realtidsdata** — vad händer *just nu* på VM:en. Bra för att debugga ett akut problem eller se ett mönster under hög last.

1. Gå till din VM
2. I vänstermenyn: **Monitoring → Metrics**
3. Lägg till dessa mätvärden en i taget via `Add metric`:

| Metric | Aggregation |
|--------|-------------|
| Percentage CPU | Average |
| Network In Total | Sum |
| Network Out Total | Sum |
| OS Disk Read Bytes/sec | Average |
| OS Disk Write Bytes/sec | Average |

4. Justera tidsrymden (uppe till höger) — testa **Last hour** och **Last 24 hours**
5. Klicka `Pin to dashboard` om du vill ha det som en snabb översikt

> **Snabbkoll:** Du ser grafer med faktisk data, inte bara tomma axlar.

---

## Steg 5 — Metrics vs Logs: vad är skillnaden?

Det här är en av de viktigaste förståelserna för molnövervakning — och den är enklare än den låter.

### Metrics — realtid, lätt att rita

Metrics är **numeriska mätvärden** som samlas in tätt (var 60:e sekund eller oftare). CPU i procent, bytes per sekund, anslutningar. De är förbehandlade och redo att rita upp som grafer direkt.

- Bra för: dashboards, alertar, snabb diagnos
- Lagras i: Azure Metrics Store (separat från Log Analytics)
- Retention: 93 dagar som standard
- Fördröjning: sekunder

### Logs — historik, flexibelt, KQL

Logs är **strukturerade händelser** med fritext och metadata. Syslog-meddelanden, app-loggar, heartbeats, InsightsMetrics. De samlas in av AMA och skickas till Log Analytics Workspace.

- Bra för: historisk analys, felsökning, alertar på mönster
- Lagras i: Log Analytics Workspace
- Retention: 30 dagar som standard (kan utökas)
- Fördröjning: 5–15 minuter

Kör de här tre KQL-queries under **Monitoring → Logs** på din VM:

```kql
// Alla heartbeats de senaste 24 timmarna — bevisar att agenten lever
Heartbeat
| where TimeGenerated > ago(24h)
| summarize count() by bin(TimeGenerated, 1h)
```

```kql
// Genomsnittlig CPU de senaste 6 timmarna, per minut
InsightsMetrics
| where TimeGenerated > ago(6h)
| where Name == "UtilizationPercentage"
| summarize avg(Val) by bin(TimeGenerated, 1m)
| render timechart
```

```kql
// Topp 10 mest CPU-intensiva processer just nu
InsightsMetrics
| where TimeGenerated > ago(1h)
| where Namespace == "Processor" and Name == "UtilizationPercentage"
| top 10 by Val desc
| project TimeGenerated, Val, Tags
```

> **Tips:** KQL (Kusto Query Language) är Azure:s frågespråk för loggar. Det liknar SQL men är designat för tidsseriedata. Du kommer se det mycket i molnjobb — värt att bekanta dig med.

---

## Utmanande frågor

<details><summary>Tips 1</summary>

```
En alert-regel har ett tröskelvärde och en aktionsgrupp.
Men vad händer om CPU är 85 % i bara 10 sekunder?
Varför väljer vi "Average over 5 minutes" istället för "Maximum"?
```

</details>

<details><summary>Tips 2</summary>

```
Metrics lagras automatiskt av Azure — du behöver inte konfigurera något.
Men varför syns inte CPU-data i Log Analytics utan AMA installerad?
Vad är skillnaden på data i Metrics Store vs Log Analytics Workspace?
```

</details>

<details><summary>Tips 3</summary>

```
Data Collection Rules (DCR) styr vad AMA samlar in.
Om du lägger till en ny VM senare — behöver du installera AMA igen,
eller räcker det att associera den nya VM:en med en befintlig DCR?
```

</details>

<details><summary>Lösningsförslag och förklaringar</summary>

**Tips 1 — Varför Average och inte Maximum?**

En CPU-topp på 10 sekunder är ofta normal — din app startar ett task, gör en query, är klar. Om du alertar på Maximum fångar du allt det bruset och får mail mitt i natten för ingenting.

Average over 5 minutes betyder: "CPU:n var *i snitt* 85 % under fem minuter". Det är ett tecken på ett verkligt problem — inte en tillfällig topp.

Tumregel: Maximum passar för "nånting gick fel just nu". Average passar för "systemet är under hög last".

**Tips 2 — Metrics Store vs Log Analytics**

Azure samlar automatiskt in **platform metrics** (CPU, nätverk, disk) till Metrics Store — utan att du gör något. Det är därför du kan se CPU i Metrics även utan AMA.

Men Metrics Store är begränsat: du kan rita grafer och sätta alertar, men du kan inte köra frågor mot historiken med KQL.

AMA skickar *InsightsMetrics* till Log Analytics Workspace — samma mätvärden men som loggposter. Det låter dig köra KQL-queries, analysera trender, koppla ihop med syslog-data, och ha längre retention.

Summan: Metrics Store = snabbt och enkelt. Log Analytics = kraftfullt och flexibelt.

**Tips 3 — DCR och flera VM:ar**

Ja, du kan associera en befintlig DCR med en ny VM utan att konfigurera om allt. Det är precis poängen med DCR:s — konfigurationen är separat från agenten.

Steg för en ny VM: installera AMA, ge den en managed identity, associera den med din befintliga DCR. Klart. Det är det som gör det skalbart.

</details>

---

## Summan av kardemumman

Du har nu:

- Skapat ett Log Analytics Workspace som samlar all övervakningsdata
- Installerat Azure Monitor Agent via VM Insights
- Satt upp en alert som skickar mail vid hög CPU-last
- Utforskat Metrics i portalen — CPU, nätverk, disk
- Förstått när du använder Metrics (realtid, snabbt) och när du använder Logs (historik, KQL, kraft)

HyresBil-appen sover inte. Nu gör du inte heller det — men åtminstone slipper du vakna av att en kund ringer.

Koda vilt! 🚗

---

*Marcus Ackre Medina — Nion Education — marcus.medina@nionit.com*
