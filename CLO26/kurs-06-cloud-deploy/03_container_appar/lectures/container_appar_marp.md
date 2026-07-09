---
marp: true
theme: default
class: invert
paginate: true
---

# Azure Container Apps

**Kurs:** Cloud Deploy
**Modul:** 03 — Containerapplikationer

---

## Vad ska vi lära oss idag?

- **Azure Container Apps (ACA)** — serverless containrar
- **Skillnad mot AKS och App Service** — när välja vad?
- **Deploya en container** — från image till app
- **Skalning** — automatisk skalning
- **Dapr** — distributed application runtime
- **Miljövariabler och secrets**

---

## Vad är Azure Container Apps?

Azure Container Apps (ACA) är en serverless containerplattform.

```
                    ACA
┌──────────────────────────────────────┐
│  Container App (revisions)           │
│  ┌────────┐ ┌────────┐ ┌────────┐  │
│  │ App v1 │ │ App v2 │ │ API:v1 │  │
│  │ :8080  │ │ :8080  │ │ :5000  │  │
│  └────────┘ └────────┘ └────────┘  │
└──────────────────────────────────────┘
```

**Serverless:** Du betalar per aktiv container, inte per server.

---

## ACA vs AKS vs App Service

| Aspekt | ACA | AKS | App Service |
|--------|-----|-----|-------------|
| Hantering | Serverless | Du hanterar noder | Serverless |
| Container | Ja | Ja | Nej (PaaS) |
| Kubernetes | Nej (abstraherat) | Ja (full K8s) | Nej |
| Skalning | Autoskala till 0 | Manuell/auto | Autoskala (min 1) |
| Kostnad | Per aktiv container | Per nod | Per plan |
| Komplexitet | Låg | Hög | Låg |

---

## Deploya en Container App

```bash
# Skapa miljö
az containerapp env create \
    --name my-aca-env \
    --resource-group myRG \
    --location westeurope

# Skapa container app
az containerapp create \
    --name my-app \
    --resource-group myRG \
    --environment my-aca-env \
    --image mcr.microsoft.com/dotnet/samples:aspnetapp \
    --target-port 8080 \
    --ingress external
```

---

## Revisions — Versionshantering

Varje deployment skapar en ny revision.

```bash
# Ny revision (fullständig)
az containerapp update \
    --name my-app \
    --image myapp:v2

# Styr trafik mellan revisioner
az containerapp ingress traffic set \
    --name my-app \
    --revision-weight my-app--v1=10 \
    --revision-weight my-app--v2=90
```

**Use case:** Canary releases — skicka 10% trafik till ny version, övervaka, öka gradvis.

---

## Skalning

```yaml
# Bicep-exempel: konfigurera skalning
resource app 'Microsoft.App/containerApps@2023-05-01' = {
  properties: {
    template: {
      scale: {
        minReplicas: 0    // Skala till 0 vid inaktivitet
        maxReplicas: 10   // Max 10 instanser
        rules: [
          {
            name: 'http-scaling'
            http: {
              metadata: {
                concurrentRequests: '100'  // Skala vid 100 samtidiga requests
              }
            }
          }
        ]
      }
    }
  }
}
```

---

## Miljövariabler

```bash
az containerapp create \
    --name my-app \
    --environment my-aca-env \
    --image myapp:latest \
    --env-vars \
        "ASPNETCORE_ENVIRONMENT=Production" \
        "DB_CONNECTION=Server=..." \
    --secrets \
        "db-password=hemligt123..."
```

Referera secrets i koden:
```csharp
var password = Environment.GetEnvironmentVariable("db-password");
```

---

## Sammanfattning

- ✅ ACA = serverless containrar, ingen K8s-komplexitet
- ✅ Perfekt för mikroservices och API
- ✅ Skalning från 0 (ingen kostnad vid inaktivitet)
- ✅ Revisions för canary deployments
- ✅ Simpel ingress (HTTPS, custom domain)
- ➡️ Nästa: Webbapplikationer — Azure App Service

---
