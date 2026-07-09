# YAML och Helm — Övningar

## Förutsättningar

- Kubernetes-kluster (lokal med Kind, Minikube, eller AKS)
- Helm installerat: `winget install Helm.Helm` / `brew install helm`

## Övning 1: Skriva YAML-manual

Skapa en YAML-fil för varje K8s-resurs:

**Deployment (`deployment.yaml`):**
```yaml
# TODO: Skapa en Deployment som:
# - heter "webapp"
# - kör image: nginx:latest
# - har 3 replikor
# - använder label "app: webapp"
# - exponerar port 80
```

**Service (`service.yaml`):**
```yaml
# TODO: Skapa en Service som:
# - heter "webapp-service"
# - är av typ ClusterIP
# - matchar poddar med label "app: webapp"
# - exponerar port 80 → containerPort 80
```

Applicera med `kubectl apply -f deployment.yaml -f service.yaml`.

---

## Övning 2: Debugga YAML

Nedan är en Deployment med fel. Hitta och fixa felen:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: my-app
spec:
  replicas: 3
  selector:
    matchLabels:
      app: my-app
  template:
    metadata:
      labels:
        app: my-app
    spec:
      containers:
      - name: app
        Image: myapp:latest
        ports:
        - containerPort: 8080
        env:
        - name: DATABASE_URL
          value: "localhost"
        - name: DATABASE_PORT
          value: 3306
```

**Hur många fel hittade du?** Applicera filen och se om `kubectl` accepterar den.

---

## Övning 3: Skapa ett Helm-chart

```bash
helm create myapp
```

1. Granska strukturen — vad finns i varje fil?
2. Ändra `values.yaml`:
   - `replicaCount: 2`
   - `image.repository: nginx`
   - `image.tag: alpine`
3. Ändra `service.type: ClusterIP`
4. Installera: `helm install my-release ./myapp`
5. Verifiera: `kubectl get pods,svc`

---

## Övning 4: Anpassa templates

Lägg till en miljövariabel i `templates/deployment.yaml`:

```yaml
env:
- name: APP_ENV
  value: {{ .Values.environment }}
```

Lägg till i `values.yaml`:
```yaml
environment: production
```

Uppgradera releasen:
```bash
helm upgrade my-release ./myapp --set environment=staging
```

**Verifiera:** `kubectl describe pod <pod-name>` — ser du `APP_ENV=staging`?

---

## Övning 5: Värden per miljö

Skapa en `prod-values.yaml`:
```yaml
replicaCount: 5
environment: production
image:
  tag: stable
resources:
  limits:
    cpu: 500m
    memory: 512Mi
```

Installera med:
```bash
helm install prod-release ./myapp -f prod-values.yaml
```

**Fråga:** Varför är det bra att separera values per miljö?

---

## Övning 6: Helm Rollback

1. Uppgradera releasen 2-3 gånger (ändra replicaCount)
2. Visa historik: `helm history my-release`
3. Rollback till revision 1: `helm rollback my-release 1`
4. Verifiera att antalet poddar återgått

**Fråga:** När skulle du använda `helm rollback` i produktion?

---

## Övning 7: Reflektion

1. Varför är YAML känsligt för indentering? Vad händer om du använder tab?
2. Vad är fördelen med Helm jämfört med rena YAML-filer?
3. Vad är skillnaden mellan `values.yaml` och `--set`?
4. När är Helm overkill? (dvs. när räcker rena YAML-filer?)
