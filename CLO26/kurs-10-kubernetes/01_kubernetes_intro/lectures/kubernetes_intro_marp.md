---
marp: true
theme: default
class: invert
paginate: true
---

# Introduktion till Kubernetes

**Kurs:** Kubernetes
**Modul:** 01 — Grundläggande Kubernetes

---

## Vad ska vi lära oss idag?

- **Vad är Kubernetes?** — containerorkestrering
- **Arkitektur** — cluster, nodes, pods
- **kubectl** — kommandoradsverktyget
- **Deployments** — hantera önskat antal poddar
- **Services** — exponera dina poddar
- **Namespace** — organisera ditt kluster

---

## Vad är Kubernetes?

Kubernetes (K8s) är en open source-plattform för att automatisera deployment, skalning och hantering av containeriserade applikationer.

```
┌─────────────────────────────────────────┐
│           ETT KUBERNETES CLUSTER         │
├─────────────┬─────────────┬─────────────┤
│  Worker 1   │  Worker 2   │  Worker 3   │
│ ┌───┐ ┌───┐ │ ┌───┐ ┌───┐ │ ┌───┐ ┌───┐ │
│ │P₁ │ │P₂ │ │ │P₃ │ │P₄ │ │ │P₅ │ │P₆ │ │
│ └───┘ └───┘ │ └───┘ └───┘ │ └───┘ └───┘ │
└─────────────┴─────────────┴─────────────┘
```

- **Cluster:** Uppsättning av servrar (noder)
- **Node:** En server (VM eller fysisk)
- **Pod:** Minsta enheten (en eller flera containrar)

---

## Kubernetes Arkitektur

```
               Control Plane
┌──────────────────────────────────────┐
│  API     Scheduler  Controller   etcd│
│  Server             Manager          │
└──────────────────────────────────────┘
          │
          ▼
┌──────────────────┬──────────────────┐
│   Worker 1       │    Worker 2      │
│ ┌────┐ ┌────┐   │  ┌────┐ ┌────┐   │
│ │kube│ │kube│   │  │kube│ │kube│   │
│ │let │ │-prx│   │  │let │ │-prx│   │
│ └────┘ └────┘   │  └────┘ └────┘   │
└──────────────────┴──────────────────┘
```

**Control Plane:** Hanteringslager (API, schemaläggning, etc.)
**Worker:** Kör poddar (kubelet + kube-proxy)

---

## Kubectl — Ditt Viktigaste Verktyg

```bash
# Status
kubectl get nodes
kubectl get pods
kubectl get services
kubectl get all

# Detaljer
kubectl describe pod min-pod
kubectl logs min-pod

# Skapa/ta bort
kubectl apply -f deployment.yaml
kubectl delete pod min-pod

# Debug
kubectl exec -it min-pod -- /bin/bash
kubectl port-forward pod/min-pod 8080:80
```

---

## Deployment

En Deployment hanterar önskat antal pod-instanser:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: webapp
spec:
  replicas: 3                    # Vill ha 3 poddar
  selector:
    matchLabels:
      app: webapp
  template:
    metadata:
      labels:
        app: webapp
    spec:
      containers:
      - name: app
        image: myapp:latest
        ports:
        - containerPort: 8080
```

---

## Service

En Service exponerar en grupp poddar internt eller externt:

```yaml
apiVersion: v1
kind: Service
metadata:
  name: webapp-service
spec:
  type: LoadBalancer              # Extern IP från molnleverantören
  selector:
    app: webapp                   # Matchar poddarnas labels
  ports:
  - port: 80                      # Service-port
    targetPort: 8080              # Container-port
```

**Service-typer:**
- **ClusterIP** — bara internt i klustret
- **NodePort** — port på varje nods IP
- **LoadBalancer** — extern load balancer (Azure, AWS)

---

## Namespace

Organisera resurser i logiska grupper:

```bash
# Skapa namespace
kubectl create namespace dev
kubectl create namespace prod

# Arbeta i ett namespace
kubectl get pods -n dev
kubectl apply -f app.yaml -n prod

# Alla namespaces
kubectl get pods --all-namespaces
```

**Användning:** isolera miljöer (dev, test, prod) eller team inom samma kluster.

---

## Kom igång med AKS

```bash
# Skapa AKS-kluster
az aks create     --name my-aks     --resource-group myRG     --node-count 3     --enable-managed-identity

# Hämta kubeconfig
az aks get-credentials     --name my-aks     --resource-group myRG

# Verifiera anslutning
kubectl get nodes
```

---

## Sammanfattning

- ✅ Kubernetes = containerorkestrering
- ✅ Cluster → Nodes → Pods
- ✅ kubectl = ditt huvudverktyg
- ✅ Deployment = önskat antal poddar
- ✅ Service = exponera poddar (ClusterIP, NodePort, LoadBalancer)
- ✅ Namespace = organisera resurser

---
