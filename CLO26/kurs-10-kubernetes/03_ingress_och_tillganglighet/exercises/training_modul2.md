# Träningsuppgifter: Kubernetes — Modul 2

> **Modul:** 03 — Ingress och tillgänglighet (Ingress Controller, TLS, health checks, replicas), 04 — Säkerhet och CI/CD (Secrets, RBAC, pipeline-integration)

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är en Ingress i Kubernetes?

a. En container som tar emot trafik<br>b. Ett API-objekt som hanterar extern HTTP/HTTPS-trafik till tjänster i klustret — med routing, TLS, och name-based virtual hosting<br>c. En typ av Service<br>d. En databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett API-objekt som hanterar extern HTTP/HTTPS-trafik med routing och TLS

  **Förklaringar:**

  - ❌ **a) Container** - FEL: Ingress är en konfigurationsresurs, inte en container
  - ✅ **b) Extern HTTP/HTTPS-hantering** - **RÄTT**: En Ingress-definition: `host: myapp.com → service: myapp-svc:80`. `host: api.myapp.com → service: api-svc:80`. Kräver en Ingress Controller (t.ex. NGINX, Traefik) som implementerar reglerna. TLS-certifikat kan kopplas. Istället för en LoadBalancer per tjänst har du en Ingress som central ingång
  - ❌ **c) Service** - FEL: Service är för intern trafik. Ingress är för extern och kan routa till Services
  - ❌ **d) Databas** - FEL: Ingress hanterar nätverkstrafik, inte data
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är en Ingress Controller?

a. En pod eller tjänst som faktiskt implementerar Ingress-reglerna — den kör en reverse proxy (NGINX, Traefik, HAProxy) och uppdaterar konfigurationen när Ingress-objekt ändras<br>b. En YAML-fil<br>c. En databas<br>d. Ett certifikat

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En pod/tjänst som implementerar Ingress-reglerna — en reverse proxy

  **Förklaringar:**

  - ✅ **a) Den som kör proxyn** - **RÄTT**: Ingress-objektet är bara en regel (deklarativ). Ingress Controller är DEN SOM UTFÖR reglerna. Den kör som en Pod med en konfigurerbar reverse proxy. När du skapar en Ingress → Controller läser den → uppdaterar sin proxys config. Vanligast: NGINX Ingress Controller, Azure Application Gateway Ingress Controller (AGIC)
  - ❌ **b) YAML-fil** - FEL: Controller är en körande tjänst, inte en fil
  - ❌ **c) Databas** - FEL: Controller är för nätverkstrafik
  - ❌ **d) Certifikat** - FEL: Controller använder certifikat för TLS, är inte certifikatet
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är en health check (liveness/readiness probe) i Kubernetes?

a. En kontroll av klustrets hälsa<br>b. Kubernetes kontrollerar regelbundet om en container lever (liveness) och är redo att ta emot trafik (readiness) — om inte, startas den om eller tas ur rotation<br>c. En användarundersökning<br>d. En backup

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Kubernetes kontrollerar om containern lever (liveness) och är redo (readiness)

  **Förklaringar:**

  - ❌ **a) Klusterhälsa** - FEL: Health probes kollar per container/pod, inte hela klustret
  - ✅ **b) Liveness + Readiness** - **RÄTT**: Liveness probe: "Är appen vid liv?" Om nej → starta om containern. Readiness probe: "Kan den ta emot trafik?" Om nej → ta bort från Service (ingen trafik). Startup probe: för appar som startar långsamt (undviker omstarter under startup). Defineras i Pod-spec: `httpGet`, `tcpSocket`, `exec command`
  - ❌ **c) Användarundersökning** - FEL: 😄 Nej
  - ❌ **d) Backup** - FEL: Health probes är runtime-kontroller, inte datasäkerhet
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är en ReplicaSet i Kubernetes?

a. En kopia av databasen<br>b. En resurs som säkerställer att ett specificerat antal Pod-repliker körs samtidigt — om en pod dör skapar ReplicaSet en ny<br>c. En backup av konfigurationen<br>d. En nätverksregel

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En resurs som håller ett specificerat antal Pod-repliker igång

  **Förklaringar:**

  - ❌ **a) Databaskopia** - FEL: ReplicaSet är för compute-repliker, inte data
  - ✅ **b) Pod-repliker** - **RÄTT**: `replicas: 3` i Deployment → ReplicaSet skapas → håller 3 Pods igång. Om en pod kraschar → ReplicaSet skapar en ny. Om du manuellt skalar upp till 5 → ReplicaSet skapar 2 till. Deployment hanterar ReplicaSet (en per version). Rolling update = ny ReplicaSet skalas upp, gammal skalas ner
  - ❌ **c) Konfig-backup** - FEL: ReplicaSet är för körning, inte backup
  - ❌ **d) Nätverksregel** - FEL: Network Policies styr trafik
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är en Kubernetes Secret?

a. En offentlig nyckel<br>b. Ett objekt som lagrar känslig information (lösenord, API-nycklar, certifikat) — base64-kodad, kan monteras som fil eller miljövariabel<br>c. En databas-tabell<br>d. Ett lösenord

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett objekt som lagrar känslig information — base64-kodad, monteras som fil eller miljövariabel

  **Förklaringar:**

  - ❌ **a) Offentlig nyckel** - FEL: Secrets lagrar HEMLIG information, inte offentlig
  - ✅ **b) Känslig data** - **RÄTT**: `kubectl create secret generic db-cred --from-literal=password=s3cret`. Secrets lagras i etcd (borde krypteras i vila). Används: `envFrom:` eller `volumes:` i Pod-spec. Viktigt: Secrets är base64, inte krypterade som standard — använd extern secret-operator (Azure Key Vault, HashiCorp Vault) för riktig säkerhet
  - ❌ **c) Databas-tabell** - FEL: Secrets är K8s-objekt, inte databasrader
  - ❌ **d) Lösenord** - FEL: Secrets lagrar lösenord, men är INTE lösenordet — det är en behållare för hemligheter
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är RBAC (Role-Based Access Control) i Kubernetes?

a. En metod för att kryptera data<br>b. Ett system som styr vem som får göra vad i klustret — roller definierar vilka åtgärder som är tillåtna på vilka resurser<br>c. En nätverks-brandvägg<br>d. Ett backup-system

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett system som styr vem som får göra vad i klustret — roller och bindningar

  **Förklaringar:**

  - ❌ **a) Kryptering** - FEL: RBAC är auktorisering, inte kryptering
  - ✅ **b) Rollbaserad åtkomstkontroll** - **RÄTT**: Role (inom namespace) eller ClusterRole (hela klustret): tillåtna verb (get, list, create, delete) på resurser (pods, secrets, deployments). RoleBinding kopplar roll till användare/grupp/SA. Exempel: "Utvecklare: get/list på pods i dev-namespace. Admin: allt i alla namespaces"
  - ❌ **c) Brandvägg** - FEL: RBAC styr användaråtkomst, inte nätverkstrafik
  - ❌ **d) Backup** - FEL: RBAC är säkerhet och auktorisering
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är ett Namespace i Kubernetes?

a. En virtuell klusterpartition — ett sätt att dela upp klustret i isolerade miljöer för olika team/projekt<br>b. En server<br>c. En container<br>d. En typ av Pod

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En virtuell klusterpartition — isolerade miljöer i samma kluster

  **Förklaringar:**

  - ✅ **a) Virtuell partition** - **RÄTT**: Du kan ha: `dev`, `stage`, `prod` namespaces i samma kluster. Resurser i ett namespace är isolerade från andra. `kubectl get pods -n dev`. Resource quotas per namespace. Bra för: separera team, miljöer, eller kunder i samma kluster
  - ❌ **b) Server** - FEL: Namespaces är logiska partitioner, inte fysiska servrar
  - ❌ **c) Container** - FEL: Namespaces innehåller resurser (Pods, Services), de är inte containrar
  - ❌ **d) Pod** - FEL: Pods skapas INOM namespace, de är inte namespaces
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Hur integreras Kubernetes i en CI/CD-pipeline?

a. Pipelines bygger en container-image, pushar till registry, och uppdaterar Kubernetes-deployment med nya imagen — `kubectl set image deployment/myapp myapp=myregistry/myapp:v2`<br>b. Pipelines deployar direkt till Kubernetes via SSH<br>c. Pipelines skapar VMs<br>d. Pipelines lagrar data i databasen

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Pipeline bygger image → pushar till registry → uppdaterar deployment med ny image

  **Förklaringar:**

  - ✅ **a) Build → Registry → Deploy** - **RÄTT**: GitHub Actions: 1) Checkout code. 2) Build Docker image. 3) Push to Docker Hub / ACR. 4) `kubectl set image deployment/myapp myapp=myreg/app:v2` (eller `helm upgrade myapp ./chart --set image.tag=v2`). Detta triggar en Rolling Update i K8s. För säkerhet: kubeconfig lagras som GitHub Secret
  - ❌ **b) SSH** - FEL: Du ska inte SSH:a in i Kubernetes-noder. Använd kubectl eller Helm
  - ❌ **c) VMs** - FEL: CI/CD bygger + deployar, den skapar inte VMs
  - ❌ **d) Databas** - FEL: CI/CD hanterar kod och deployment, inte data
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
