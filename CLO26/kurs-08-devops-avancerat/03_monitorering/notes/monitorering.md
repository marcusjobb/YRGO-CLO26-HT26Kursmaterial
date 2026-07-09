# Monitorering och Observability — Fördjupning

## Varför Monitorering?

Utan monitorering är du blind. Du vet inte:
- Om appen ligger nere förrän en användare klagar
- Vilken endpoint som är långsam
- Om minnesläckan du fixade i v2 faktiskt är borta
- Vilket fel som inträffade och varför

## The Three Pillars of Observability

1. **Logs** — strukturerad eller ostrukturerad text om händelser. 'Vi gjorde X och fick Y'
2. **Metrics** — numeriska mätvärden över tid. 'CPU 75%, requests 200/min, latency 350ms'
3. **Traces** — spåra en request genom hela systemet. 'Request gick från web → API → DB → return'

## Application Insights — Komma igång

Installera i ASP.NET Core:
```bash
dotnet add package Microsoft.ApplicationInsights.AspNetCore
```

```csharp
builder.Services.AddApplicationInsightsTelemetry();
```

Automatiskt spårat:
- HTTP-requests och svarstider
- Exceptions (med stack trace)
- Beroenden (SQL, HTTP-anrop till andra tjänster)
- Page views och klient-telemetry (om du lägger till JS)
- Metrics: request rate, failure rate, server response time

## KQL — Kusto Query Language

KQL är sök-språket för Azure Monitor. Exempel:

```kusto
// Hitta långsamma requests de senaste 24 timmarna
requests
| where timestamp > ago(24h)
| where duration > 5000
| project timestamp, name, duration, resultCode
| order by duration desc
| take 20
```

```kusto
// Räkna fel per endpoint
requests
| where timestamp > ago(1h)
| where success == false
| summarize ErrorCount = count() by name
| order by ErrorCount desc
```

## Alerts

Skapa larm för att agera proaktivt:

| Alert | Tröskel | Åtgärd |
|-------|---------|--------|
| Hög svarstid | duration > 2000ms i 5 min | Öka resurser eller optimera |
| Många 500-fel | failure count > 10 på 5 min | Larma utvecklingsteam |
| Hög CPU | CPU > 90% i 10 min | Auto-scaling |
| Låg disk | Free space < 20% | Rensa eller expandera |
| Certifikat utgår | < 30 dagar kvar | Förnya certifikat |

## Dashboard

Sammanfatta viktigaste måtten på en skärm:
- Request rate (RPS) och svarstid
- Felfrekvens
- CPU/minne per instans
- Antal aktiva användare
- Data ut/in per tjänst

## Distributed Tracing

I en mikroservices-arkitektur kan en request gå genom 5+ tjänster. Distributed tracing (OpenTelemetry, Jaeger, Zipkin) låter dig följa hela kedjan och identifiera var fördröjningen uppstår.

Nyckelbegrepp:
- **Trace:** Hela resan från klient till svar
- **Span:** Ett steg i resan (t.ex. API-anrop, databasfråga)
- **Trace ID:** Unikt ID som följer med varje request
