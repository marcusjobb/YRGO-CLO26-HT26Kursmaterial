# Övning 2 — Skriv ditt första Deployment-manifest

🟡 Mellannivå

---

## Bakgrunden

En pod som du skapar med `kubectl run` är tillfällig — om den kraschar är den borta. I riktiga miljöer vill du att Kubernetes automatiskt startar om applikationen om något går fel.

Det är precis vad en Deployment gör. I den här övningen skriver du ett eget Deployment-manifest från grunden och ser vad som händer när en pod tas bort.

---

## Vad gäller

- [ ] Som utbildare vill jag att du kan skriva ett korrekt Deployment-manifest i YAML
- [ ] Som utbildare vill jag att du förstår vad `replicas`, `selector` och `template` gör
- [ ] Som utbildare vill jag att du kan observera self-healing i praktiken

---

## Uppgift

**Steg 1 — Skriv manifestet**

Skapa filen `webapp-deployment.yaml`:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: webapp
  labels:
    app: webapp
spec:
  replicas: 2
  selector:
    matchLabels:
      app: webapp
  template:
    metadata:
      labels:
        app: webapp
    spec:
      containers:
      - name: webapp
        image: nginx:alpine
        ports:
        - containerPort: 80
```

**Steg 2 — Applicera**

```bash
kubectl apply -f webapp-deployment.yaml
```

**Steg 3 — Kontrollera**

```bash
kubectl get deployments
kubectl get replicasets
kubectl get pods
```

Notera att pods-namnen innehåller ett slumpmässigt suffix. Varför?

**Steg 4 — Testa self-healing**

Kopiera namn på en av pods. Ta sedan bort den:

```bash
kubectl delete pod <pod-namn>
```

Direkt efteråt:

```bash
kubectl get pods
```

Vad ser du?

**Steg 5 — Skala**

```bash
kubectl scale deployment webapp --replicas=4
kubectl get pods
```

Sedan tillbaka:

```bash
kubectl scale deployment webapp --replicas=1
```

**Steg 6 — Svara på frågorna**

1. Vad är skillnaden mellan att ta bort en pod och att ta bort en deployment?
2. Vad händer om du ändrar `replicas: 2` i YAML-filen och kör `kubectl apply` igen?
3. Varför har pods slumpmässiga namn men en deployment ett fast namn?

**Steg 7 — Städa upp**

```bash
kubectl delete deployment webapp
```

## Förväntad output

```output
NAME     READY   UP-TO-DATE   AVAILABLE   AGE
webapp   2/2     2            2           45s
```

```output
NAME                      READY   STATUS    RESTARTS   AGE
webapp-7d4b9c6f8f-2vxqp   1/1     Running   0          45s
webapp-7d4b9c6f8f-9krtm   1/1     Running   0          45s
```

Efter att ha tagit bort en pod:

```output
NAME                      READY   STATUS              RESTARTS   AGE
webapp-7d4b9c6f8f-9krtm   1/1     Running             0          2m
webapp-7d4b9c6f8f-xp8wn   0/1     ContainerCreating   0          3s
```

## Tips

> YAML är indenteringskänslig. Använd 2 mellanslag — aldrig tab. Om du får ett fel om `mapping values are not allowed` är det nästan alltid indentering.

> `kubectl apply` är idempotent — du kan köra det flera gånger. Det ändrar bara det som är annorlunda. `kubectl create` misslyckas om resursen redan finns.

> ⏱️ 15-minutersregeln: kör `kubectl describe deployment webapp` om något inte fungerar. Events-sektionen längst ner berättar vad som gick fel.

---

*Facit finns hos läraren.*
