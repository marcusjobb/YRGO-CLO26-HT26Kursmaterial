---
marp: true
theme: nion-dark
paginate: true
---

# Serverless och event-driven arkitektur

**Kurs:** Cloud-native
**Modul:** 02 — Serverless
Marcus Ackre Medina · YRGO · CLO26

---

## Vad ska vi lära oss idag?

- **Vad är serverless?** — modellen och tanken bakom
- **Azure Functions** — triggers, bindings, livscykel
- **AWS Lambda** — kort jämförelse
- **Triggers** — HTTP, timer, queue, blob
- **Cold start** — vad det är och hur du hanterar det
- **Event-driven arkitektur** — ett nytt sätt att tänka på flöden

---

## Vad är serverless?

Analogin: tanka bilen vid pump istället för att äga ett eget oljeraffinaderi.

```
Traditionell server             Serverless
─────────────────               ─────────────────
Du betalar 24/7                 Du betalar per anrop
Du underhåller OS               Leverantören underhåller allt
Kapacitet = fast                Kapacitet = automatisk
Alltid igång                    Vaknar vid behov, sover annars
```

Serverless betyder inte att det inte finns servrar. Det betyder att du inte behöver bry dig om dem.

---

## Azure Functions — grundmodellen

```
HTTP-anrop / Timer / Queue-meddelande
         │
         ▼
  ┌──────────────────┐
  │  Azure Function  │  ← Din kod, körs i millisekunder
  └──────────────────┘
         │
         ▼
  Databas / API / Queue / Storage
```

Du skriver funktionen. Azure hanterar:
- Skalning (0 till 1000 instanser automatiskt)
- Infrastruktur och patching
- Lastbalansering

---

## Skapa en Azure Function

```bash
# Installera Azure Functions Core Tools
npm install -g azure-functions-core-tools@4

# Skapa nytt projekt
func init MyFunctionApp --worker-runtime dotnet-isolated

# Lägg till en funktion
func new --name HelloWorld --template "HTTP trigger"

# Kör lokalt
func start
```

Hosting-plan: **Consumption** (betala per anrop) eller **Premium** (alltid varm instans).

---

## HTTP Trigger

```csharp
// HttpTrigger — anropas via GET eller POST
[Function("HelloWorld")]
public IActionResult Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
{
    // ❌ Logga aldrig känslig data i produktionsfunktioner
    // ✅ Logga request-id för spårbarhet
    _logger.LogInformation("HTTP trigger anropad.");

    string name = req.Query["name"];
    return new OkObjectResult($"Hej, {name}!");
}
```

**Authorization levels:** Anonymous, Function, Admin — styr vem som får anropa.

---

## Timer Trigger

```csharp
// TimerTrigger — kör enligt schema (CRON-uttryck)
[Function("NattligRensning")]
public void Run(
    [TimerTrigger("0 0 2 * * *")] TimerInfo myTimer)
    // ─────────────────────────
    // Sekund Minut Timme Dag Månad Veckodag
    // → Kör kl 02:00 varje natt
{
    _logger.LogInformation($"Rensning startad: {DateTime.Now}");
    // Städa upp gamla loggposter, skicka rapporter etc.
}
```

Vanliga mönster: nattlig backup, schemalagda rapporter, cache-rensning.

---

## Queue Trigger

```csharp
// QueueTrigger — vaknar när ett meddelande hamnar i kön
[Function("BearbetaOrder")]
public void Run(
    [QueueTrigger("order-queue", Connection = "AzureWebJobsStorage")]
    Order order)
{
    // ✅ Idempotent: samma meddelande kan komma två gånger
    // ✅ Kontrollera om ordern redan bearbetats innan du agerar
    _logger.LogInformation($"Bearbetar order {order.Id}");
    BearbetaOrder(order);
}
```

Om funktionen misslyckas återköar Azure meddelandet automatiskt (upp till 5 försök).

---

## Blob Trigger

```csharp
// BlobTrigger — reagerar när en fil laddas upp till Storage
[Function("ResaBild")]
public void Run(
    [BlobTrigger("bilder/{name}", Connection = "AzureWebJobsStorage")]
    Stream bildData,
    string name)
{
    _logger.LogInformation($"Ny bild uppladdad: {name}");
    // Ändra storlek, konvertera format, analysera med AI etc.
}
```

Typiska användningsfall: bildbehandling, dokumentkonvertering, filvalidering.

---

## Cold Start — problemet

```
Anrop 1 (kall instans):         Anrop 2 (varm instans):
──────────────────────          ──────────────────────
Starta container     ~800ms     Köra din kod         ~10ms
Ladda runtime        ~400ms
Initiera DI          ~200ms
Köra din kod          ~10ms
─────────────────               ─────────────────
Total:              ~1400ms     Total:                ~10ms
```

**Problem:** Consumption-plan slår av instanser efter ~5 min inaktivitet. Nästa anrop tar lång tid.

---

## Cold Start — lösningar

| Strategi | Hur | Trade-off |
|----------|-----|-----------|
| Premium-plan | Alltid en varm instans | Kostar mer |
| Timer-trick | Anropa funktionen var 5:e minut | Hacky, kostar anrop |
| Minimera startuppstid | Undvik tung DI, lazy loading | Kräver koddisciplin |
| Durable Functions | Stateful, stannar igång | Mer komplext |

```csharp
// ❌ Tung initialisering i konstruktorn
public MyFunction(IMyService service, IDbContext db, IHeavyClient c) { }

// ✅ Lazy loading av det som inte alltid behövs
private readonly Lazy<IHeavyClient> _client = new(() => new HeavyClient());
```

---

## Event-driven arkitektur — tanken

```
Traditionellt (request/response):     Event-driven:
──────────────────────────────        ──────────────
A anropar B direkt                    A publicerar ett event
A väntar på svar                      B, C, D lyssnar oberoende
A och B är kopplade                   A vet inte om B existerar
```

**Fördelar:**
- Löst kopplat — tjänster vet inte om varandra
- Skalbart — lyssnarens antal påverkar inte producenten
- Resilient — om B är nere tappar A inte ett anrop, eventet köas

---

## Azure Event Grid och Service Bus

```
                    ┌─────────────┐
Producent ────────▶ │ Event Grid  │ ────────▶ Function A
                    │ Service Bus │ ────────▶ Function B
                    └─────────────┘ ────────▶ Logic App
```

| Tjänst | Passar för |
|--------|------------|
| Event Grid | Reaktion på Azure-resurshändelser (fil uploadad, VM startad) |
| Service Bus | Tillförlitlig meddelandeförmedling, order i rätt sekvens |
| Event Hubs | Högt genomflöde, telemetri, loggströmmar |

---

## AWS Lambda — jämförelse

| Aspekt | Azure Functions | AWS Lambda |
|--------|----------------|------------|
| Språk | C#, JS, Python, Java, Go | JS, Python, Java, C#, Go, Ruby |
| Triggrar | HTTP, Timer, Queue, Blob, Event Grid | API GW, S3, SNS, DynamoDB, EventBridge |
| Cold start | 200ms–1s (Consumption) | 100ms–1s |
| Max körtid | 10 min (Consumption) | 15 min |
| Lokal dev | func start | AWS SAM / LocalStack |

Samma grundidé, olika ekosystem. Välj det din organisation redan är i.

---

## Vanliga misstag

```csharp
// ❌ Statisk state — delas mellan anrop på samma instans
public static int _counter = 0;

// ✅ Använd extern lagring för state (Cosmos DB, Redis)
await _redis.IncrementAsync("counter");

// ❌ Synkron I/O i en asynkron funktion
var result = GetDataFromDb().Result;  // Deadlock-risk

// ✅ Awaita allt I/O
var result = await GetDataFromDb();

// ❌ För lång funktion (timeout-risk i Consumption-plan)
// ✅ Bryt upp i kortare steg med Durable Functions
```

---

## Prova själv

1. Skapa en Azure Function med HTTP trigger som returnerar aktuellt datum och tid.
2. Lägg till en Timer trigger som loggar ett meddelande varje minut (testa lokalt).
3. Lägg till felhantering: om `name`-parametern saknas, returnera 400 Bad Request.

```bash
func init CloudNativeDemo --worker-runtime dotnet-isolated
cd CloudNativeDemo
func new --name TidFunktion --template "HTTP trigger"
func start
```

---

## Sammanfattning

- ✅ Serverless = du betalar per anrop, leverantören hanterar infrastrukturen
- ✅ Azure Functions har fyra viktiga triggers: HTTP, Timer, Queue, Blob
- ✅ Cold start uppstår när en inaktiv instans vaknar — Premium-plan löser det
- ✅ Event-driven = producenter och konsumenter vet inte om varandra
- ✅ Azure Event Grid / Service Bus kopplar ihop tjänster löst
- ➡️ Nästa: Skalning och lastbalansering
