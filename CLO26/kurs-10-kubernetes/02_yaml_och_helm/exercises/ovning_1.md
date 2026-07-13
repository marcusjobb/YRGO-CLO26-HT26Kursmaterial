# Övning 1 — Deployment och Service i YAML

🟢 Grundnivå

---

## Bakgrunden

En Deployment utan en Service är som ett café utan skylt — appen kör, men ingen kan nå den. I den här övningen skriver du båda manifesten och kopplar ihop dem via labels.

Det är det vanligaste mönstret i Kubernetes: en Deployment som håller pods igång, och en Service som ger dem en stabil nätverksadress.

---

## Vad gäller

- [ ] Som utbildare vill jag att du kan skriva en komplett Deployment med korrekt struktur
- [ ] Som utbildare vill jag att du kan skriva en Service som matchar Deploymentens pods via `selector`
- [ ] Som utbildare vill jag att du förstår hur labels kopplar ihop resurserna

---

## Uppgift

**Steg 1 — Deployment**

Skapa filen `deployment.yaml`:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: guestbook
spec:
  replicas: 3
  selector:
    matchLabels:
      app: guestbook
  template:
    metadata:
      labels:
        app: guestbook
    spec:
      containers:
      - name: guestbook
        image: nginx:alpine
        ports:
        - containerPort: 80
```

**Steg 2 — Service**

Skapa filen `service.yaml`:

```yaml
apiVersion: v1
kind: Service
metadata:
  name: guestbook-service
spec:
  selector:
    app: guestbook
  ports:
  - port: 80
    targetPort: 80
  type: ClusterIP
```

**Steg 3 — Applicera**

```bash
kubectl apply -f deployment.yaml
kubectl apply -f service.yaml
```

Eller båda på en gång:

```bash
kubectl apply -f deployment.yaml -f service.yaml
```

**Steg 4 — Verifiera**

```bash
kubectl get pods
kubectl get service guestbook-service
kubectl describe service guestbook-service
```

Titta på `Endpoints` i describe-outputen. Matchar de IP-adresserna pods?

```bash
kubectl get pods -o wide
```

**Steg 5 — Testa anslutningen**

```bash
kubectl run curl-test --image=curlimages/curl --restart=Never --rm -it -- curl guestbook-service
```

**Steg 6 — Svara på frågorna**

1. Vad händer om du ändrar `selector.app` i Service till ett värde som inte matchar någon pod?
2. Vad är skillnaden mellan `port` och `targetPort` i en Service?
3. Varför används `ClusterIP` och inte `LoadBalancer` för intern kommunikation?

## Förväntad output

```output
NAME                 TYPE        CLUSTER-IP      EXTERNAL-IP   PORT(S)   AGE
guestbook-service    ClusterIP   10.96.148.220   <none>        80/TCP    10s
```

```output
Name:              guestbook-service
Selector:          app=guestbook
Endpoints:         10.244.0.5:80,10.244.0.6:80,10.244.0.7:80
```

## Tips

> Labels är limmet i Kubernetes. Deployment-poddarna får label `app: guestbook` via `template.metadata.labels`. Service hittar dem via `selector.app: guestbook`. Om de inte matchar får Service inga endpoints — appen är oåtkomlig internt.

> Du kan lägga alla manifester i en enda fil separerade med `---`. Det är praktiskt för enkla appar.

> ⏱️ 15-minutersregeln: om endpoints är tomma (`<none>`) — kontrollera att label-värdena stämmer exakt, inklusive versaler.

---

*Facit finns hos läraren.*
