# Säkerhet och CI/CD med Kubernetes — Övningar

## Förutsättningar

- Ett Kubernetes-kluster (lokal med Kind/Minikube eller AKS)
- `kubectl` installerat
- Ett Helm-chart från föregående modul (valfritt)

## Övning 1: RBAC — Skapa roller

**Scenario:** Du har ett team med tre personer:
- **Anna** — utvecklare, ska kunna läsa poddar i namespace `dev`
- **Björn** — admin, ska kunna göra allt i namespace `prod`
- **CI/CD** — ska kunna skapa/uppdatera deployments i `prod`

Skapa Role + RoleBinding för varje:

```yaml
# TODO: Anna — read-only poddar i dev
---
# TODO: Björn — full access i prod (använd ClusterRole admin)
---
# TODO: CI/CD — deployment-manager i prod
```

**Verifiera:**
```bash
kubectl auth can-i get pods --as anna -n dev
kubectl auth can-i delete pods --as anna -n dev  # Ska vara NO
```

---

## Övning 2: Secrets — lagra och använda

1. Skapa en Secret manuellt:
```bash
kubectl create secret generic db-credentials \
    --from-literal=username=admin \
    --from-literal=password=hemligt123
```

2. Skapa en pod som använder secret som miljövariabler:
```yaml
# TODO: Pod som använder db-credentials
# - Namn: secret-test-pod
# - Image: nginx
# - Läs username och password från secret
```

3. Verifiera:
```bash
kubectl exec secret-test-pod -- printenv | grep DB_
```

**⚠️ Viktigt:** Secrets är bara Base64-kodade. Hur krypterar du dem på riktigt?

---

## Övning 3: Pod Security Standards

Skapa ett namespace med restricted security:

```yaml
apiVersion: v1
kind: Namespace
metadata:
  labels:
    pod-security.kubernetes.io/enforce: restricted
  name: restricted-ns
```

Försök skapa en Deployment i detta namespace:

```yaml
# TODO: En Deployment som använder privileged: true
# Förväntat: DENIED av Pod Security Admission
```

Testa sedan med en deployment som uppfyller restricted-standarden (read-only root filesystem, inga privileged containers).

**Fråga:** Varför vill du ha olika security-nivåer i olika namespaces?

---

## Övning 4: GitOps — Flux

Om du har tillgång till ett kluster, prova Flux:

```bash
# Installera Flux CLI
# (winget, brew, eller curl)
flux check --pre

# Bootstrap (kräver GitHub-token)
flux bootstrap github \
    --owner=<ditt-github-namn> \
    --repository=k8s-config \
    --branch=main \
    --path=./clusters/my-cluster
```

**Utan kluster:** Beskriv vad som händer i GitOps-flödet:

1. Utvecklare pushar YAML till Git
2. Flux upptäcker ändringen → ?
3. Klustret uppdateras → ?
4. Om någon ändrar i klustret direkt → ?

---

## Övning 5: CI/CD-pipeline för K8s

Skriv en GitHub Actions workflow som:

1. Bygger en Docker-image
2. Pushar till Azure Container Registry (ACR)
3. Uppdaterar en Kubernetes-deployment med nya imagen

```yaml
name: Deploy to AKS

on:
  push:
    branches: [main]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      # TODO: Checkout kod
      # TODO: Logga in på ACR
      # TODO: Build + push Docker image
      # TODO: Sätt kontext till AKS
      # TODO: kubectl set image med nya taggen
```

**Varför använda `github.sha` som image-tag?**

---

## Övning 6: Container Scanning

Integrera Trivy i en pipeline:

```yaml
- name: Scan Docker image
  uses: aquasecurity/trivy-action@master
  with:
    image-ref: 'myapp:latest'
    format: 'sarif'
    output: 'trivy-results.sarif'
```

**Frågor:**
1. Vad händer om scanningen hittar en kritisk sårbarhet?
2. Ska pipelinen faila eller bara varna?
3. Hur ofta bör du scanna? Varför?

---

## Övning 7: Säkerhets-checklista

Gå igenom din Deployment från förra modulen och checka av:

- [ ] Använder min app en ServiceAccount med minimala rättigheter?
- [ ] Är secrets lagrade som Kubernetes Secrets (inte i image)?
- [ ] Har jag resources limits/requests på alla containrar?
- [ ] Använder jag liveness + readiness probes?
- [ ] Kör containern som icke-root?
- [ ] Är images från en trusted registry?
- [ ] Har jag Network Policies som begränsar trafik?

**Fixar du något som inte är grönt?**
