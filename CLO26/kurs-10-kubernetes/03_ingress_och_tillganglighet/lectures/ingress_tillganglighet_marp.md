---
marp: true
theme: default
class: invert
paginate: true
---

# Ingress och Tillgänglighet

**Kurs:** Kubernetes
**Modul:** 03 — Ingress och Tillgänglighet

---

## Vad ska vi lära oss idag?

- **Ingress** — exponera HTTP/HTTPS-rutter
- **Ingress Controller** — Nginx, Traefik
- **TLS/SSL** — HTTPS med cert-manager
- **HPA** — autoscaling
- **Health Probes** — liveness, readiness, startup
- **Network Policies** — brandvägg i klustret

---

## Ingress vs Service

```
                        ┌──────────────┐
                        │  Ingress     │
                        │  (Nginx)     │
                        └──────┬───────┘
              ┌────────────────┼────────────────┐
              ▼                ▼                ▼
        ┌──────────┐    ┌──────────┐    ┌──────────┐
        │ Service  │    │ Service  │    │ Service  │
        │ webapp   │    │   api    │    │  admin   │
        └──────────┘    └──────────┘    └──────────┘
```

**Service (NodePort/LB):** Exponerar en tjänst. En IP per tjänst.
**Ingress:** Router. En IP + vägväljning baserat på hostname/path.

---

## Ingress Manifest

```yaml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: my-ingress
  annotations:
    cert-manager.io/cluster-issuer: letsencrypt-prod
spec:
  ingressClassName: nginx
  tls:
  - hosts:
    - app.minasajt.se
    secretName: app-tls
  rules:
  - host: app.minasajt.se
    http:
      paths:
      - path: /
        pathType: Prefix
        backend:
          service:
            name: webapp
            port:
              number: 80
      - path: /api
        pathType: Prefix
        backend:
          service:
            name: api
            port:
              number: 5000
```

---

## Ingress Controller

Ingress-resursen gör ingenting utan en Ingress Controller:

```bash
# Installera Nginx Ingress Controller
kubectl apply -f https://raw.githubusercontent.com/...
```

Populära controllers:
- **Nginx** — stabil, funktionsrik, standard
- **Traefik** — enkel, modern, automatisk TLS
- **Azure Application Gateway Ingress** — Azure-native

---

## TLS med cert-manager

Automatisk SSL-certifikathantering:

```bash
# Installera cert-manager
kubectl apply -f https://github.com/cert-manager/cert-manager/releases/...

# Skapa en ClusterIssuer för Let's Encrypt
apiVersion: cert-manager.io/v1
kind: ClusterIssuer
metadata:
  name: letsencrypt-prod
spec:
  acme:
    server: https://acme-v02.api.letsencrypt.org/directory
    privateKeySecretRef:
      name: letsencrypt-account-key
    solvers:
    - http01:
        ingress:
          class: nginx
```

Kombinera med Ingress-annotations så sköts allt automatiskt.

---

## HPA — Horizontal Pod Autoscaler

```yaml
apiVersion: autoscaling/v2
kind: HorizontalPodAutoscaler
metadata:
  name: webapp-hpa
spec:
  scaleTargetRef:
    apiVersion: apps/v1
    kind: Deployment
    name: webapp
  minReplicas: 2
  maxReplicas: 10
  metrics:
  - type: Resource
    resource:
      name: cpu
      target:
        type: Utilization
        averageUtilization: 70
  - type: Resource
    resource:
      name: memory
      target:
        type: Utilization
        averageUtilization: 80
```

---

## Health Probes

```yaml
spec:
  containers:
  - name: app
    livenessProbe:         # Är containern frisk? Om den failar → omstart
      httpGet:
        path: /healthz
        port: 8080
      initialDelaySeconds: 5
      periodSeconds: 10
    
    readinessProbe:        # Kan containern ta emot trafik? Om den failar → ta bort från Service
      httpGet:
        path: /readyz
        port: 8080
      initialDelaySeconds: 3
      periodSeconds: 5

    startupProbe:          # Har containern startat? Används för långsamma starter
      httpGet:
        path: /startupz
        port: 8080
      failureThreshold: 30
      periodSeconds: 10
```

---

## Network Policies

Brandvägg inne i klustret:

```yaml
apiVersion: networking.k8s.io/v1
kind: NetworkPolicy
metadata:
  name: allow-frontend-only
spec:
  podSelector:
    matchLabels:
      app: backend
  policyTypes:
  - Ingress
  ingress:
  - from:
    - podSelector:
        matchLabels:
          app: frontend
    ports:
    - port: 5000
```

**Default:** Alla poddar kan prata med alla (öppet nätverk).
**Med Network Policies:** Explicit tillåt trafik.

---

## Sammanfattning

- ✅ Ingress = smart routing (host/path → service)
- ✅ Ingress Controller = Nginx, Traefik
- ✅ cert-manager = automatisk TLS (Let's Encrypt)
- ✅ HPA = autoscaling baserat på CPU/minne
- ✅ Health Probes = liveness, readiness, startup
- ✅ Network Policies = brandvägg i klustret

---
