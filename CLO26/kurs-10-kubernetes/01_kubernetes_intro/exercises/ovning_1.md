# Övning 1 — Utforska klustret med kubectl

🟢 Grundnivå

---

## Bakgrunden

Du har precis fått åtkomst till ett Kubernetes-kluster (minikube eller AKS). Ingen har berättat vad som kör där. Din uppgift är att ta reda på det — med hjälp av kubectl.

Det är exakt vad du gör när du börjar på ett nytt jobb och ska förstå en miljö du aldrig sett förut.

---

## Vad gäller

- [ ] Som utbildare vill jag att du kan köra grundläggande kubectl-kommandon och läsa deras output
- [ ] Som utbildare vill jag att du förstår skillnaden mellan vad `get` och `describe` visar
- [ ] Som utbildare vill jag att du kan identifiera en pods status och förklara vad den betyder

---

## Uppgift

**Steg 1 — Starta klustret**

```bash
minikube start
```

Vänta tills den är klar. Kontrollera att du har kontakt:

```bash
kubectl cluster-info
```

**Steg 2 — Lista resurserna**

Kör följande och notera vad du ser:

```bash
kubectl get nodes
kubectl get pods --all-namespaces
kubectl get namespaces
```

**Steg 3 — Skapa en enkel pod**

```bash
kubectl run testpod --image=nginx:alpine --restart=Never
```

**Steg 4 — Inspektera podden**

```bash
kubectl get pods
kubectl describe pod testpod
kubectl logs testpod
```

**Steg 5 — Svara på frågorna**

1. Vad är statusen på din pod? Vad betyder den statusen?
2. Vilken nod kör podden på?
3. Vad ser du i `Events`-sektionen av `describe`?
4. Vad skriver `kubectl logs` ut — och varför?

**Steg 6 — Städa upp**

```bash
kubectl delete pod testpod
```

Bekräfta att den försvann:

```bash
kubectl get pods
```

## Förväntad output

```output
NAME       STATUS    RESTARTS   AGE
testpod    Running   0          30s
```

`kubectl describe pod testpod` ska visa bl.a.:

```output
Node:         minikube/...
Status:       Running
Containers:
  testpod:
    Image: nginx:alpine
Events:
  ... Scheduled ... Pulled ... Created ... Started
```

## Tips

> `kubectl get pods` visar en ögonblicksbild. Lägg till `-w` (watch) för att se ändringar i realtid: `kubectl get pods -w`

> Om statusen är `ImagePullBackOff` — klustret kan inte hämta imagen. Kontrollera att du har nätverksåtkomst och att imagenamnet är rätt.

> ⏱️ 15-minutersregeln: om du kört igenom stegen men `describe` fortfarande är obegriplig — skriv ner en specifik fråga och ta upp den med läraren. Det är bättre än att stirra på skärmen.

---

*Facit finns hos läraren.*
