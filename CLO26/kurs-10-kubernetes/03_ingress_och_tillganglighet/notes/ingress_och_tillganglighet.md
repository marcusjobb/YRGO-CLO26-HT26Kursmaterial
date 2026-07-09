# Ingress och Tillgänglighet

## Ingress vs Service

| Service | Ingress |
|---------|---------|
| Exponerar EN tjänst | Router för flera tjänster |
| En IP per tjänst | En IP + vägväljning |
| L4 (TCP/UDP) | L7 (HTTP/HTTPS) |

Ingress är rätt val när du har flera tjänster som ska exponeras via samma IP.

## Ingress Controller

Ingress-resursen är bara en deklaration — den gör ingenting utan en Ingress Controller som tolkar den.

Populära controllers:
- **Nginx Ingress Controller** — stabil, funktionsrik
- **Traefik** — enkel, automatisk TLS
- **Azure Application Gateway Ingress** — Azure-native

## TLS med cert-manager

cert-manager automatiserar SSL-certifikat från Let's Encrypt:

1. Installera cert-manager i klustret
2. Skapa en ClusterIssuer (Let's Encrypt config)
3. Lägg till annotations i din Ingress

cert-manager fixar resten — utfärdande, förnyelse, och lagring som Kubernetes Secrets.

## HPA — Horizontal Pod Autoscaler

Automatisk skalning baserat på resursanvändning:

```yaml
metrics:
- type: Resource
  resource:
    name: cpu
    target:
      type: Utilization
      averageUtilization: 70
```

Skalar upp när CPU > 70%, skalar ner när CPU < 70%.

## Health Probes

| Probe | Syfte | Vid fail |
|-------|-------|----------|
| **liveness** | Är containern frisk? | Omstart av containern |
| **readiness** | Kan ta emot trafik? | Tas bort från Service |
| **startup** | Har den startat? | Fördröjer liveness/readiness |

Alla tre bör implementeras för produktionsapplikationer.

## Network Policies

Default: alla poddar kan prata med alla. Med Network Policies skapar du en brandvägg inuti klustret.

Använd för att isolera:
- Frontend → backend (men inte backend → databas)
- Dev-namespace från prod-namespace

## Viktigaste lärdomarna

- Ingress = L7-router (host/path → service), Service = L4 (en tjänst)
- Ingress Controller (Nginx, Traefik) krävs för att Ingress ska fungera
- cert-manager ger gratis automatisk TLS
- HPA skalar horisontellt baserat på CPU/minne
- Liveness + Readiness + Startup = produktionsredo
- Network Policies = säkerhet i klustret
