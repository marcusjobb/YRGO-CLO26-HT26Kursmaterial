# Träningsuppgifter: Kubernetes — Modul 1

> **Modul:** 01 — Kubernetes-intro (arkitektur, begrepp, kubectl, minikube), 02 — YAML och Helm (manifester, Helm Charts, releases, värdefiler)

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är Kubernetes?

a. En container-engine som Docker<br>b. En plattform för orkestrering av containrar — hanterar deployment, skalning, och drift av container-applikationer över ett kluster av maskiner<br>c. En databas<br>d. En molntjänst för lagring

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En plattform för orkestrering av containrar — deployment, skalning, drift

  **Förklaringar:**

  - ❌ **a) Container-engine** - FEL: Docker är en container-engine. Kubernetes ORKESTRERAR containrar (ofta Docker) över flera maskiner
  - ✅ **b) Orkestreringsplattform** - **RÄTT**: Kubernetes (K8s) sköter: var containrar körs, hur de hittar varandra (Service Discovery), skalning (ReplicaSet), uppdateringar (Rolling Update), återhämtning (självläkning). "Kubernetes" är grekiska för "kapten" eller "pilot"
  - ❌ **c) Databas** - FEL: K8s orkestrerar, det lagrar inte applikationsdata
  - ❌ **d) Lagringstjänst** - FEL: K8s kan använda lagring, men är inte en lagringstjänst
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är en Pod i Kubernetes?

a. En virtuell maskin<br>b. Den minsta körbara enheten i Kubernetes — en eller flera containrar som delar nätverk och storage<br>c. En databas<br>d. En nätverksregel

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Den minsta körbara enheten — en eller flera containrar som delar nätverk och storage

  **Förklaringar:**

  - ❌ **a) VM** - FEL: Pods är abstraktioner över containrar, inte virtuella maskiner
  - ✅ **b) Minsta enheten** - **RÄTT**: En Pod = 1+ containrar. Alla containrar i en Pod delar: samma IP, samma portrymd, samma volymer (storage). Oftast 1 container per Pod. Sidecar-mönster: app-container + logg-container i samma Pod
  - ❌ **c) Databas** - FEL: Pods är compute, inte storage
  ❌ **d) Nätverksregel** - FEL: Nätverksregler är Network Policies, inte Pods
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är skillnaden mellan en Deployment och en Pod?

a. Samma sak<br>b. En Deployment är en deklarativ beskrivning av ett önskat tillstånd — den skapar och hanterar ReplicaSets som i sin tur skapar Pods. Deployment = "jag vill ha 3 instanser av denna container"<br>c. Pods skapar Deployments<br>d. Deployments är för databaser, Pods är för appar

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En Deployment beskriver önskat tillstånd och hanterar Pods via ReplicaSets

  **Förklaringar:**

  - ❌ **a) Samma** - FEL: Deployment är ett HÖGRE koncept som HANTERAR Pods
  - ✅ **b) Deployment → ReplicaSet → Pods** - **RÄTT**: Du skriver en Deployment YAML: `replicas: 3`, `image: myapp:2.0`. Deployment skapar en ReplicaSet som håller 3 Pods igång. Vid uppdatering (image:2.1) → ny ReplicaSet skapas → gradvis ersättning (rolling update). Rollback till 2.0 är en kommandobort
  - ❌ **c) Pods skapar Deployments** - FEL: Tvärtom — Deployment skapar och hanterar Pods
  - ❌ **d) Databaser vs appar** - FEL: Båda används för alla typer av arbetsbelastningar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är en Service i Kubernetes?

a. En abstraktion som exponerar en uppsättning Pods som en nätverkstjänst — med en stabil IP och DNS-namn<br>b. En typ av container<br>c. Ett sätt att starta om Pods<br>d. En databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En abstraktion som exponerar Pods som en nätverkstjänst — stabil IP och DNS

  **Förklaringar:**

  - ✅ **a) Stabil nätverkspunkt** - **RÄTT**: Pods kommer och går (dör, startas om, skalas). Deras IP ändras. En Service (ClusterIP) ger en STABIL IP och DNS som alltid pekar på de Pods som matchar dess selector. Typer: ClusterIP (internt), NodePort (exponera på nodens port), LoadBalancer (extern load balancer)
  - ❌ **b) Container** - FEL: Service är ett nätverkskoncept, inte en container
  - ❌ **c) Starta om** - FEL: Det är Deployment med `kubectl rollout restart`
  - ❌ **d) Databas** - FEL: Service är för nätverkstrafik
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är kubectl?

a. Ett grafiskt användargränssnitt för Kubernetes<br>b. Kommandoradsverktyget för att interagera med Kubernetes-kluster — deploya, inspektera, felsöka resurser<br>c. En container-engine<br>d. En databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Kommandoradsverktyget för att interagera med Kubernetes-kluster

  **Förklaringar:**

  - ❌ **a) GUI** - FEL: kubectl är CLI. Det finns Dashboard för GUI
  - ✅ **b) CLI-verktyg** - **RÄTT**: `kubectl get pods`, `kubectl logs -f my-pod`, `kubectl apply -f deployment.yaml`, `kubectl exec -it my-pod -- bash`. Allt du gör i K8s går via kubectl (eller via API direkt). Autentisering via kubeconfig-fil
  - ❌ **c) Container-engine** - FEL: kubectl pratar med Kubernetes API, inte med containrar direkt
  - ❌ **d) Databas** - FEL: kubectl är ett administrationsverktyg
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är Helm i Kubernetes-ekosystemet?

a. En container-engine<br>b. En "package manager" för Kubernetes — paketerar YAML-manifester som "Charts" med versionshantering, värdefiler och templating<br>c. En nätverks-plugin<br>d. En monitoreringstjänst

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En package manager för Kubernetes — Charts med versionshantering och templating

  **Förklaringar:**

  - ❌ **a) Container-engine** - FEL: Docker är engine. Helm paketerar konfiguration
  - ✅ **b) Package manager** - **RÄTT**: Helm Chart = katalog med templatade YAML-filer + en `values.yaml`. `helm install my-app ./chart` — sätter in värdena och skapar resurserna. `helm upgrade my-app ./chart --set image.tag=v2`. Helm gör komplexa applikationer återanvändbara. "Helm repo" — dela Charts (Artifact Hub)
  - ❌ **c) Nätverks-plugin** - FEL: Helm hanterar deployment, inte nätverk
  - ❌ **d) Monitorering** - FEL: Prometheus/Grafana monitorerar. Helm distribuerar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är en Helm values-fil (values.yaml)?

a. En hårdkodad konfiguration<br>b. En fil som definierar standardvärden för ett Helm Chart — användaren kan överrida dem vid installation/uppgrade<br>c. En loggfil<br>d. En databas-schema

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En fil som definierar standardvärden — användaren överridar vid installation

  **Förklaringar:**

  - ❌ **a) Hårdkodad** - FEL: Poängen med values.yaml är just att den INTE är hårdkodad — den parametriserar
  - ✅ **b) Parametrar** - **RÄTT**: `values.yaml` innehåller: `replicaCount: 3`, `image.tag: latest`, `ingress.host: myapp.com`. Användaren vid deployment: `helm install myapp ./chart --set image.tag=v2` eller en egen `values-prod.yaml` som överridar. Detta gör att samma Chart kan deployas till dev, stage, prod med olika värden
  - ❌ **c) Loggfil** - FEL: Values är input, inte output
  - ❌ **d) Databas-schema** - FEL: Values är konfiguration för din app, inte för databasen
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är en Node i Kubernetes?

a. En virtuell maskin<br>b. En arbetsmaskin (fysisk eller virtuell) i klustret som kör Pods — hanteras av kontrollplanet (control plane)<br>c. En container<br>d. En nätverks-switch

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En arbetsmaskin i klustret som kör Pods — hanteras av kontrollplanet

  **Förklaringar:**

  - ❌ **a) VM** - FEL: En Node KAN vara en VM, men begreppet är bredare — det är en "worker machine"
  - ✅ **b) Worker machine** - **RÄTT**: Kluster = Control Plane (API Server, Scheduler, etcd) + Worker Nodes (kubelet, kube-proxy, container runtime). Varje Node kör Pods (containers). Du skalar klustret genom att lägga till/fler Nodes. `kubectl get nodes` visar alla. En Node har resurser (CPU, RAM, storage) som Pods förbrukar
  - ❌ **c) Container** - FEL: Nodes KÖR containrar, de är inte containrar själva
  - ❌ **d) Switch** - FEL: Node är en server, inte nätverksutrustning
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
