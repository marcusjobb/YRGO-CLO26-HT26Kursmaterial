# Säkerhet och CI/CD med Kubernetes

## RBAC — Role-Based Access Control

Vem får göra vad i klustret?

**Tre delar:**
1. **Roll (Role/ClusterRole)** — definierar vad som är tillåtet (resurser + verbs)
2. **Subjekt** — vem (User, Group, ServiceAccount)
3. **Bindning (RoleBinding/ClusterRoleBinding)** — kopplar ihop roll med subjekt

**Princip:** Ge lägsta möjliga behörighet. Börja restriktivt och öka vid behov.

## Secrets i Kubernetes

Kubernetes Secrets är Base64-kodade (inte krypterade som standard).

**Använd extern secret store för riktig säkerhet:**
- Azure Key Vault + CSI Driver
- HashiCorp Vault
- External Secrets Operator

## Pod Security Standards

Tre nivåer:
| Nivå | Beskrivning |
|------|-------------|
| **Privileged** | Obegränsad — bara för systemkomponenter |
| **Baseline** | Minimalt restriktiv — för de flesta applikationer |
| **Restricted** | Strikt — read-only root filesystem, inga privileged containers |

## GitOps

Principen: Git-repot är den enda sanna källan. Klustret synkroniserar automatiskt.

```mermaid
Git push → GitOps Operator (Flux/ArgoCD) → K8s Cluster
```

**Flux:** Microsoft-ekosystemet, enklare
**ArgoCD:** Webb-UI, multi-cluster, mer funktioner

## CI/CD för Kubernetes

Standard-pipeline:
```
Build image → Push to registry → Update deployment → Apply
```

**GitHub Actions + AKS:**
```yaml
- run: docker build -t myapp:${{ github.sha }} .
- run: docker push myregistry.azurecr.io/myapp:${{ github.sha }}
- run: kubectl set image deployment/my-app app=myregistry.azurecr.io/myapp:${{ github.sha }}
```

## Container Scanning

Integrera scanning i din pipeline för att upptäcka sårbarheter tidigt:

- **Trivy** — snabb, open source, integreras med CI/CD
- **Snyk** — kommersiell, djupare analys
- **Azure Defender** — inbyggt i Azure

Skanna både OS-paket (apt, apk) och språkberoenden (NuGet, npm).

## Viktigaste lärdomarna

- RBAC = minsta möjliga behörighet — alltid
- Secrets är bara Base64-kodade — använd extern store för produktion
- Pod Security Standards = Privileged/Baseline/Restricted
- GitOps = Git som sanna källan, Flux/ArgoCD som synkroniseringsmotor
- CI/CD för K8s = build → push → kubectl set image
- Container scanning = upptäck sårbarheter före produktion
