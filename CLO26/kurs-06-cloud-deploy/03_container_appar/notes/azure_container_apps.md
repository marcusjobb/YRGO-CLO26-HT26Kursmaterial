# Azure Container Apps (ACA)

## Vad är Azure Container Apps?

Azure Container Apps är en serverless containerplattform som låter dig köra containeriserade applikationer utan att hantera Kubernetes.

**Serverless betyder:**
- Du betalar per aktiv container, inte per server
- Plattformen skalar automatiskt (inklusive till 0)
- Inga noder att patch:a eller uppgradera

## ACA vs AKS vs App Service

| Behov | Välj |
|-------|------|
| "Bara köra min container, enkelt" | **ACA** |
| "Full kontroll, komplex arkitektur" | **AKS** |
| "Ingen container, bara .NET-app" | **App Service** |

ACA är sweet spot för de flesta mikroservices-API:er.

## Revisions och trafikdelning

ACA har inbyggd revisionshantering:

- Varje deployment → ny revision
- Styr trafik mellan revisioner (t.ex. 10% v2, 90% v1)
- Kanary-releaser utan extra verktyg

```bash
az containerapp ingress traffic set \
    --name my-app \
    --revision-weight my-app--v1=10 \
    --revision-weight my-app--v2=90
```

## Skalning

ACA skalar baserat på:
- Antal samtidiga HTTP-förfrågningar
- CPU- och minnesanvändning
- Custom scaling rules (KEDA)

**Viktigt:** `minReplicas: 0` innebär zero cost vid inaktivitet, men första anropet blir långsammare (cold start).

## Miljövariabler och Secrets

Miljövariabler och secrets (Azure Key Vault-referenser) stöds direkt:

```csharp
var connString = Environment.GetEnvironmentVariable("DB_CONNECTION");
```

## Viktigaste lärdomarna

- ACA = serverless containrar utan K8s-komplexitet
- Perfekt för API:er, mikroservices, event-drivna appar
- Revisioner möjliggör canary-deployments
- Skalning från 0 = ingen kostnad vid inaktivitet
- Inbyggd HTTPS och custom domain
