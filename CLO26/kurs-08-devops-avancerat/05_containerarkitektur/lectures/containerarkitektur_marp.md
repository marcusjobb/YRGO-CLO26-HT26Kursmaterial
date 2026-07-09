---
marp: true
theme: default
class: invert
paginate: true
---

# Containerarkitektur och Mikroservices

**Kurs:** DevOps Avancerat
**Modul:** 05 — Containerarkitektur

---

## Vad ska vi lära oss idag?

- **Mikroservices** — dela upp monoliten
- **Service Mesh** — kommunikationslager med Istio/Linkerd
- **API Gateway** — entrépunkt för alla tjänster
- **Stateful vs Stateless** — designa för containrar
- **Observability** — loggar, metrics, traces i container-miljö

---

## Monolit vs Mikroservices

```
Monolit                     Mikroservices
┌──────────────┐           ┌──┐ ┌──┐ ┌──┐ ┌──┐
│              │           │Au│ │  │ │  │ │  │
│   UI + API   │           │th│ │  │ │  │ │  │
│   + Business │           │  │ │  │ │  │ │  │
│   + Data     │           ├──┤ ├──┤ ├──┤ ├──┤
│              │           │Ca│ │Or│ │Pr│ │Re│
│   Allt i ett │           │ta│ │dr│ │od│ │co│
│   deploy     │           │lo│ │er│ │   │ │m.│
└──────────────┘           └──┘ └──┘ └──┘ └──┘
```

---

## När välja Mikroservices?

| Fördelar | Nackdelar |
|----------|-----------|
| ✅ Självständig deployment | ❌ Distribuerad komplexitet |
| ✅ Skala per tjänst | ❌ Nätverksfördröjning |
| ✅ Teknikval per tjänst | ❌ Debugging över tjänster |
| ✅ Mindre team äger varje tjänst | ❌ Datakonsistens (eventuell) |
| ✅ Isolation (en kraschar inte alla) | ❌ Deployment-koordination |

**Rekommendation:** Börja med monolit, dela upp när du känner smärtan.

---

## Service Mesh

Ett infrastrukturlager för service-to-service-kommunikation.

```
┌─────────────────────────────────┐
│         Service Mesh            │
│  (Istio, Linkerd, Consul)       │
├────┬────┬────┬────┬────┬───────┤
│Trf │Säk │Obs │Retr│Cir │Traffic│
│fik │het │erva│ies │cuit│Split  │
└────┴────┴────┴────┴────┴───────┘
```

**Sidecar-mönstret:** Varje pod har en proxy-container som hanterar all nätverkskommunikation. Appen behöver inte veta något om nätverket.

---

## API Gateway

Entrépunkt för alla klienter:

```
                     ┌───────────┐
Klient (web) ───────→│           │──→ Auth Service
                     │   API     │──→ Order Service
Klient (mobile) ────→│  Gateway  │──→ Product Service
                     │           │──→ Recommendation
Klient (tredje part) →│           │──→ ...
                     └───────────┘
```

**Vad API Gateway gör:**
- Autentisering (kontrollera JWT)
- Rate limiting
- Routing till rätt tjänst
- Caching
- Request/response-transformation
- Aggregera svar från flera tjänster

---

## Stateful vs Stateless

| Aspekt | Stateless | Stateful |
|--------|-----------|----------|
| Data | Skickas med varje request | Lagras på servern |
| Skalning | Enkel (lägg till fler instanser) | Komplex (dela state) |
| Session | I token/cookie | I databas/cache |
| Exempel | REST API | WebSocket-spel |
| Container | Problemfritt | Kräver StatefulSet |

**Mål:** Så stateless som möjligt. Lagra state i Redis, databas eller Cosmos DB.

---

## Container-observability

```yaml
# Liveness probe — är containern frisk? Om den failar → starta om
livenessProbe:
  httpGet:
    path: /health
    port: 8080
  initialDelaySeconds: 5
  periodSeconds: 10

# Readiness probe — kan containern ta emot trafik? Om den failar → ta bort från Service
readinessProbe:
  httpGet:
    path: /ready
    port: 8080
  initialDelaySeconds: 3
  periodSeconds: 5
```

---

## Sammanfattning

- ✅ Mikroservices = frikoppling, självständig skalning
- ✅ Service Mesh = trafik, säkerhet, observability som infrastruktur
- ✅ API Gateway = entrépunkt för alla klienter
- ✅ Stateless = container-vänlig design
- ✅ Liveness/Readiness = viktiga för Kubernetes

---
