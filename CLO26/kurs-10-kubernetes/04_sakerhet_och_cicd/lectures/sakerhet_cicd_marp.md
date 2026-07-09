---
marp: true
theme: default
class: invert
paginate: true
---

# Säkerhet och CI/CD med Kubernetes

**Kurs:** Kubernetes
**Modul:** 04 — Säkerhet och CI/CD

---

## Vad ska vi lära oss idag?

- **Kubernetes-säkerhet** — RBAC, Secrets, Pod Security
- **CIS Benchmark** — säkerhetsstandard för K8s
- **GitOps** — Flux och ArgoCD
- **CI/CD för Kubernetes** — Azure DevOps och GitHub Actions
- **Container scanning** — sårbarhetsdetektering

---

## RBAC — Role-Based Access Control

Vem får göra vad i klustret?

```yaml
# Roll — definierar vad som är tillåtet
apiVersion: rbac.authorization.k8s.io/v1
kind: Role
metadata:
  namespace: dev
  name: pod-reader
rules:
- apiGroups: [""]
  resources: ["pods"]
  verbs: ["get", "watch", "list"]
---
# RoleBinding — koppla roll till användare
apiVersion: rbac.authorization.k8s.io/v1
kind: RoleBinding
metadata:
  namespace: dev
  name: pod-reader-binding
subjects:
- kind: User
  name: "anna@company.se"
roleRef:
  kind: Role
  name: pod-reader
```

---

## Service Accounts

Poddar använder Service Accounts, inte personkonton:

```yaml
apiVersion: v1
kind: ServiceAccount
metadata:
  name: my-app-sa
---
apiVersion: rbac.authorization.k8s.io/v1
kind: RoleBinding
metadata:
  name: my-app-binding
subjects:
- kind: ServiceAccount
  name: my-app-sa
roleRef:
  kind: Role
  name: pod-reader
---
# Använd i Deployment
spec:
  template:
    spec:
      serviceAccountName: my-app-sa
```

---

## Secrets

Känslig data i Kubernetes:

```yaml
apiVersion: v1
kind: Secret
metadata:
  name: db-credentials
type: Opaque
data:
  username: YWRtaW4=     # Base64-kodat (inte krypterat!)
  password: cGFzc3dvcmQ=
```

```yaml
# Använd i pod
spec:
  containers:
  - env:
    - name: DB_USER
      valueFrom:
        secretKeyRef:
          name: db-credentials
          key: username
```

**⚠️ Varning:** Secrets är bara Base64-kodade som standard. Använd extern secret store (Azure Key Vault, HashiCorp Vault) för riktig kryptering.

---

## Pod Security Standards

| Nivå | Beskrivning | Exempel restriktioner |
|------|-------------|----------------------|
| **Privileged** | Obegränsad | Inga begränsningar |
| **Baseline** | Minimalt restriktiv | Inga privileged containers, root-förbud |
| **Restricted** | Strikt | Read-only root filesystem, inga add capabilities |

```yaml
# Pod Security Admission (K8s 1.23+)
apiVersion: v1
kind: Namespace
metadata:
  labels:
    pod-security.kubernetes.io/enforce: restricted
```

---

## GitOps — Flux och ArgoCD

**Princip:** Git-repot är den enda sanna källan. Klustret synkroniserar automatiskt med Git.

```
Git push
   │
   ▼
┌──────────┐     ┌─────────────┐     ┌──────────┐
│  Git      │     │  GitOps     │     │   K8s    │
│  Repo     │ ◄── │  Operator   │ ──► │  Cluster │
│  (YAML)   │     │  (Flux/     │     │          │
│           │     │   ArgoCD)   │     │          │
└──────────┘     └─────────────┘     └──────────┘
```

**Flux:** Microsoft-ekosystemet, Git-baserat, enkelt
**ArgoCD:** Webb-UI, multi-cluster, mer funktioner

---

## CI/CD för Kubernetes (GitHub Actions)

```yaml
name: Deploy to AKS

on:
  push:
    branches: [main]

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4
    
    - name: Build and push Docker image
      run: |
        docker build -t myapp:${{ github.sha }} .
        docker push myregistry.azurecr.io/myapp:${{ github.sha }}
    
    - name: Deploy to AKS
      uses: azure/aks-set-context@v3
      with:
        resource-group: myRG
        cluster-name: my-aks
    
    - name: Update deployment
      run: |
        kubectl set image deployment/my-app app=myregistry.azurecr.io/myapp:${{ github.sha }}
```

---

## Container Scanning

```bash
# Trivy — snabb sårbarhetsscanner
trivy image myapp:latest

# Integrera i pipeline
- name: Scan container
  uses: aquasecurity/trivy-action@master
  with:
    image-ref: 'myapp:latest'
    format: 'sarif'
    output: 'trivy-results.sarif'
```

**Vad skannas?**
- OS-paket (apt, apk, yum)
- Språkberoenden (NuGet, npm, pip)
- Kända sårbarheter (CVE-databas)
- Secret-läckage (hittar lösenord i images)

---

## CIS Benchmark för Kubernetes

Checklista (urval):

- [ ] etcd krypterad och säkrad
- [ ] API Server — anonym autentisering avstängd
- [ ] kubelet — autentisering påslagen
- [ ] Inga privilegierade containers (förutom system)
- [ ] RBAC aktiverat — ingen anonym åtkomst
- [ ] Secrets krypterade i etcd
- [ ] Audit logging påslagen
- [ ] Container resources definierade (limits/requests)

---

## Sammanfattning

- ✅ RBAC = Role-Based Access Control för K8s
- ✅ Secrets = känslig data (använd extern store för riktig kryptering)
- ✅ Pod Security Standards = Privileged/Baseline/Restricted
- ✅ GitOps = Flux, ArgoCD — Git är sanna källan
- ✅ CI/CD = build → push → deploy med kubectl set image
- ✅ Container scanning = Trivy, Snyk, Azure Defender

---
