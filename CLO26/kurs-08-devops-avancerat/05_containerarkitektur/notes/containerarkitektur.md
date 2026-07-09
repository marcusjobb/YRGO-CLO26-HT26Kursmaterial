# Containerarkitektur och Mikroservices

## Monolit vs Mikroservices

**Börja med en monolit.** Dela upp först när du känner smärtan — för tidig uppdelning skapar onödig komplexitet.

| Fördelar mikroservices | Nackdelar |
|------------------------|-----------|
| Självständig deployment | Distribuerad komplexitet |
| Skala per tjänst | Nätverksfördröjning |
| Isolation | Debugging över tjänster |
| Teknikval per tjänst | Datakonsistens (eventuell) |

## Service Mesh

Ett infrastrukturlager som hanterar service-to-service-kommunikation utan att ändra applikationskoden.

**Sidecar-mönstret:** Varje pod har en proxy-container som hanterar nätverk, säkerhet, observability.

**Populära:** Istio, Linkerd, Consul.

## API Gateway

Entrépunkt för alla klienter som hanterar:
- Autentisering (JWT-validering)
- Rate limiting
- Routing till rätt tjänst
- Caching
- Request/response-transformation

## Stateful vs Stateless

Designa för stateless-containrar. State hör hemma i databas, cache eller storage.

| Stateless | Stateful |
|-----------|----------|
| Skala horisontellt enkelt | Kräver StatefulSet |
| Session i token | Session på server |
| Problemfritt i containrar | Komplex persistens |

## Container Observability

Varje container bör ha:

- **Liveness probe** — är containern frisk? Om den failar → omstart
- **Readiness probe** — kan containern ta emot trafik? Om den failar → ta bort från Service
- **Startup probe** — har containern startat? (för långsamma starter)

## Viktigaste lärdomarna

- Mikroservices = verktyg, inte mål — börja monolitiskt
- Service Mesh lägger till trafik, säkerhet och observability som infrastruktur
- API Gateway är entrépunkt — en plats för auth, routing, rate limiting
- Stateless containers skalar enkelt — state i databas/cache
- Health probes är kritiska för containerplattformar
