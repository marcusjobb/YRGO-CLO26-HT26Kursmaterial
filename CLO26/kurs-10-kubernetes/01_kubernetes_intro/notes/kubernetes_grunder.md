# Kubernetes — Grundläggande

## Vad är Kubernetes?

Kubernetes (K8s) är en open source-plattform för containerorkestrering. Den automatiserar deployment, skalning och hantering av containeriserade applikationer.

**Ursprung:** Skapat av Google (baserat på Borg), nu hanterat av CNCF.

## Arkitektur

### Control Plane
Hanteringslagret som styr klustret:
- **API Server** — all kommunikation går härigenom (kubectl, UI, appar)
- **Scheduler** — bestämmer var poddar ska köras
- **Controller Manager** — övervakar och korrigerar önskat tillstånd
- **etcd** — distribuerad nyckelvärdesdatabas (klustrets "hjärna")

### Worker Nodes
Serverar som kör applikationerna:
- **Kubelet** — agenten som pratar med API Server
- **Kube-proxy** — nätverksproxy och load balancing
- **Container Runtime** — Docker, containerd, CRI-O

## Grundläggande Objekt

### Pod
Minsta enheten i Kubernetes. En eller flera containrar som delar nätverk och storage.

### Deployment
Hanterar önskat antal pod-instanser. Automatisk återställning vid fel.

### Service
Exponera en grupp poddar:

| Typ | Tillgänglighet |
|-----|---------------|
| **ClusterIP** | Bara internt i klustret |
| **NodePort** | Port på varje nods IP |
| **LoadBalancer** | Extern IP från molnleverantör |

### Namespace
Organisera resurser logiskt — isolera miljöer (dev, test, prod) eller team.

## kubectl — Dagliga Kommandon

```bash
kubectl get nodes              # Lista noder
kubectl get pods               # Lista poddar
kubectl get services           # Lista services
kubectl describe pod <namn>    # Detaljer om pod
kubectl logs <pod>             # Se loggar
kubectl exec -it <pod> -- bash # Gå in i container
```

## Kom igång med AKS

```bash
az aks create --name my-aks --resource-group myRG --node-count 3
az aks get-credentials --name my-aks --resource-group myRG
kubectl get nodes
```

## Viktigaste lärdomarna

- K8s orkestrerar containers — cluster, nodes, pods
- Control Plane = hjärnan, Workers = musklerna
- Deployment = önskat antal, Service = exponering
- kubectl är ditt viktigaste verktyg — lär dig grundkommandona
- Namespaces = organisera och isolera
