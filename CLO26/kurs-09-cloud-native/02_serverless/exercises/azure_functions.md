# Övning — Azure Functions med HyresBil

🟡

---

## Vad du ska göra

HyresBil behöver två bakgrundsjobb som körs utan en dedikerad server:

1. En HTTP-trigger som tar emot en bokning och "skickar ett bekräftelsemejl" (skriver ut i konsolen)
2. En Timer-trigger som städar upp utgångna bokningar varje natt vid midnatt

Du skapar båda i ett och samma Azure Functions-projekt med .NET 8 (isolated worker), testar lokalt med `func start`, och deployar sedan till Azure.

Du behöver inte integrera ett riktigt e-postsystem. Fokus ligger på Functions-modellen, inte på tredjepartstjänster.

---

## Förutsättningar

- Azure Functions Core Tools installerat (`func --version` visar `4.x.x`)
- .NET 8 SDK installerat (`dotnet --version` visar `8.x.x`)
- Azure CLI inloggad (`az account show` visar ditt konto)
- Terminal öppen i en lämplig arbetsmapp

Saknar du Core Tools? Installera med:

```
npm install -g azure-functions-core-tools@4 --unsafe-perm true
```

---

## Steg 1 — Skapa projektet

Skapa ett nytt Functions-projekt med isolated worker-modellen:

```
func init HyresBilFunctions --worker-runtime dotnet-isolated --target-framework net8.0
cd HyresBilFunctions
```

Projektet skapas med tre filer du behöver känna till:

- `host.json` — global konfiguration för Functions-hosten
- `local.settings.json` — lokala inställningar (ersätter miljövariabler, checkas inte in)
- `HyresBilFunctions.csproj` — vanlig .csproj-fil

Öppna `local.settings.json` och kontrollera att den ser ut så här:

> `HyresBilFunctions/local.settings.json`

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated"
  }
}
```

`AzureWebJobsStorage` pekar på Azurites lokala emulator när du kör lokalt. Functions behöver ett storage-konto för intern koordinering — även om dina egna funktioner inte använder blob eller queue.

> **Kontrollpunkt:** Projektet skapas utan fel. Alla tre filerna finns på plats.

---

## Steg 2 — HttpTrigger: ta emot en bokning

Skapa funktionsfilen för HTTP-triggern:

```
func new --name BokningBekraftelse --template "HTTP trigger" --authlevel anonymous
```

Ersätt hela innehållet i den skapade filen med:

> `HyresBilFunctions/BokningBekraftelse.cs`

```csharp
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace HyresBilFunctions;

// Representerar en inkommande bokningsförfrågan
public record BokningsRequest(
    string KundNamn,
    string Bilmodell,
    DateTime HamtDatum,
    DateTime LamnasDatum
);

public class BokningBekraftelse
{
    private readonly ILogger<BokningBekraftelse> _logger;

    public BokningBekraftelse(ILogger<BokningBekraftelse> logger)
    {
        _logger = logger;
    }

    [Function("BokningBekraftelse")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "bokning")] HttpRequestData req)
    {
        // Läs in bokningsdata från request-bodyn
        var kropp = await req.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(kropp))
        {
            var felSvar = req.CreateResponse(HttpStatusCode.BadRequest);
            await felSvar.WriteStringAsync("Bokningsdata saknas i förfrågan.");
            return felSvar;
        }

        var bokning = JsonSerializer.Deserialize<BokningsRequest>(kropp, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (bokning is null)
        {
            var felSvar = req.CreateResponse(HttpStatusCode.BadRequest);
            await felSvar.WriteStringAsync("Ogiltig bokningsdata — kontrollera JSON-formatet.");
            return felSvar;
        }

        // Simulerar att ett bekräftelsemejl skickas
        _logger.LogInformation(
            "Skickar bekräftelsemejl till {Kund}: {Bil} hämtas {Hamtdag} och lämnas {Lamnadag}.",
            bokning.KundNamn,
            bokning.Bilmodell,
            bokning.HamtDatum.ToShortDateString(),
            bokning.LamnasDatum.ToShortDateString()
        );

        var svar = req.CreateResponse(HttpStatusCode.OK);
        await svar.WriteAsJsonAsync(new
        {
            meddelande = "Bokning mottagen. Bekräftelsemejl skickat.",
            kund       = bokning.KundNamn,
            bil        = bokning.Bilmodell
        });

        return svar;
    }
}
```

> **Kontrollpunkt:** Filen kompilerar utan fel — kör `dotnet build` och kontrollera att inga errors visas.

---

## Steg 3 — TimerTrigger: rensa utgångna bokningar

Skapa funktionsfilen för Timer-triggern:

```
func new --name RensaUtgangna --template "Timer trigger"
```

Ersätt hela innehållet med:

> `HyresBilFunctions/RensaUtgangna.cs`

```csharp
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HyresBilFunctions;

public class RensaUtgangna
{
    private readonly ILogger<RensaUtgangna> _logger;

    public RensaUtgangna(ILogger<RensaUtgangna> logger)
    {
        _logger = logger;
    }

    // CRON-uttrycket "0 0 0 * * *" betyder: sekund 0, minut 0, timme 0 = midnatt varje dag
    [Function("RensaUtgangna")]
    public void Run([TimerTrigger("0 0 0 * * *")] TimerInfo timer)
    {
        _logger.LogInformation("Nattrensning startad: {Tid}", DateTime.UtcNow);

        // Simulerar databasrensning av utgångna bokningar
        // I produktion: hämta från databas och ta bort poster där LamnasDatum < DateTime.UtcNow
        var antalRensade = SimuleraRensning();

        _logger.LogInformation("Nattrensning klar. {Antal} utgångna bokningar togs bort.", antalRensade);
    }

    private static int SimuleraRensning()
    {
        // Returnerar ett hårdkodat värde — ersätt med riktig databaslogik
        return 3;
    }
}
```

**Vad betyder CRON-uttrycket?**

Azure Functions använder ett sexfält-CRON-uttryck: `{sekund} {minut} {timme} {dag} {månad} {veckodag}`

| Fält     | Värde | Betydelse       |
|----------|-------|-----------------|
| Sekund   | `0`   | vid sekund 0    |
| Minut    | `0`   | vid minut 0     |
| Timme    | `0`   | klockan 00      |
| Dag      | `*`   | varje dag       |
| Månad    | `*`   | varje månad     |
| Veckodag | `*`   | oavsett veckodag |

`"0 0 0 * * *"` = körs exakt vid midnatt UTC varje dag.

> **Kontrollpunkt:** `dotnet build` ger fortfarande inga fel. Båda funktionsfilerna finns i projektmappen.

---

## Steg 4 — Kör lokalt

Starta Functions-hosten:

```
func start
```

Du ser något i stil med:

```
Azure Functions Core Tools
Core Tools Version:       4.x.x
Function Runtime Version: 4.x.x

Functions:
        BokningBekraftelse: [POST] http://localhost:7071/api/bokning
        RensaUtgangna: timerTrigger

For detailed output, use func start --verbose
```

Öppna en ny terminal och testa HTTP-triggern:

```
curl -X POST http://localhost:7071/api/bokning \
  -H "Content-Type: application/json" \
  -d '{
    "kundNamn": "Anna Svensson",
    "bilmodell": "Volvo XC60",
    "hamtDatum": "2026-08-01T10:00:00",
    "lamnasDatum": "2026-08-07T10:00:00"
  }'
```

### Förväntad output (curl)

```json
{
  "meddelande": "Bokning mottagen. Bekräftelsemejl skickat.",
  "kund": "Anna Svensson",
  "bil": "Volvo XC60"
}
```

### Förväntad output (i func-terminalen)

```
[Information] Skickar bekräftelsemejl till Anna Svensson: Volvo XC60 hämtas 2026-08-01 och lämnas 2026-08-07.
```

Timer-triggern körs automatiskt vid midnatt. Vill du testa den manuellt just nu? Byt temporärt CRON-uttrycket till `"*/30 * * * * *"` (var 30:e sekund) och spara — `func start` plockar upp ändringen vid omstart.

> **Kontrollpunkt:** curl returnerar rätt JSON. Loggraden med kundnamn syns i func-terminalen. Stoppa med `Ctrl+C`.

---

## Steg 5 — Deploya till Azure

### Skapa resurser

Anpassa namnen — funktionsappens namn måste vara globalt unikt i Azure:

```
az group create \
  --name rg-hyresbil \
  --location swedencentral

az storage account create \
  --name stghyresbil \
  --resource-group rg-hyresbil \
  --location swedencentral \
  --sku Standard_LRS

az functionapp create \
  --name func-hyresbil-clo26 \
  --resource-group rg-hyresbil \
  --storage-account stghyresbil \
  --consumption-plan-location swedencentral \
  --runtime dotnet-isolated \
  --runtime-version 8 \
  --functions-version 4
```

`--consumption-plan-location` är nyckeln — det är Consumption-planen som aktiverar serverless-modellen. Du betalar per anrop, inte per timme.

### Publicera

```
func azure functionapp publish func-hyresbil-clo26
```

Kommandot bygger projektet, paketterar det och laddar upp till Azure. Du ser URL:erna för dina funktioner i utskriften när det är klart.

### Testa i molnet

Byt ut URL:en mot den du fick i utskriften:

```
curl -X POST https://func-hyresbil-clo26.azurewebsites.net/api/bokning \
  -H "Content-Type: application/json" \
  -d '{
    "kundNamn": "Erik Lindgren",
    "bilmodell": "Tesla Model 3",
    "hamtDatum": "2026-09-10T09:00:00",
    "lamnasDatum": "2026-09-14T09:00:00"
  }'
```

### Förväntad output

```json
{
  "meddelande": "Bokning mottagen. Bekräftelsemejl skickat.",
  "kund": "Erik Lindgren",
  "bil": "Tesla Model 3"
}
```

Visa loggar direkt i terminalen för att se att Timer-triggern registrerats:

```
func azure functionapp logstream func-hyresbil-clo26
```

> **Kontrollpunkt:** curl mot Azure-URL:en returnerar rätt JSON. Logstreamen visar att appen är igång utan fel.

---

## Serverless-modellen förklarad

En vanlig server (t.ex. en Azure VM eller App Service) kostar pengar per timme — oavsett om den gör något eller inte. Du betalar för kapaciteten, inte för arbetet.

Azure Functions på Consumption-planen fungerar tvärtom:

- **Ingen trafik** → ingen kostnad. Funktionen existerar men förbrukar ingenting.
- **En bokning skickas in** → Azure startar en instans, kör funktionen, stänger ner igen.
- **Du betalar** för antalet anrop och den CPU-tid varje anrop förbrukar.

De första 1 000 000 anropen per månad ingår gratis i Consumption-planen. För HyresBil, som kanske hanterar hundra bokningar om dagen, är kostnaden i praktiken noll.

**Avvägningen:** Kalla starter. Om ingen har anropat funktionen på ett tag kan Azure ha tagit bort instansen helt. Första anropet efter en paus kan ta 1–3 sekunder extra medan Azure startar upp .NET-processen. Det kallas cold start. För bakgrundsjobb och låg-frekvenstjänster är det acceptabelt. För en tjänst som kräver konsekvent låg latens väljer du istället Premium-planen, som håller instanser varma.

---

## Kontrollpunkter

Gå igenom listan innan du markerar övningen som klar:

- [ ] Projektet skapas med `func init` och `dotnet build` ger inga fel
- [ ] `local.settings.json` är korrekt konfigurerad för lokal körning
- [ ] `func start` visar båda funktionerna i terminalen
- [ ] curl mot lokal URL returnerar rätt JSON med kundnamn och bilmodell
- [ ] Loggraden med bekräftelsemejl syns i func-terminalen
- [ ] Du kan förklara vad de sex fälten i CRON-uttrycket `"0 0 0 * * *"` betyder
- [ ] Funktionsappen skapas i Azure med Consumption-plan
- [ ] `func azure functionapp publish` lyckas utan fel
- [ ] curl mot Azure-URL:en returnerar rätt JSON
- [ ] Du kan förklara skillnaden mellan att betala per timme och betala per anrop

---

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
