# 01 Kubernetes Intro — Programmeringstermer

## Kubernetes (K8s)
Open source-plattform för orkestrering av containrar. Automatiserar deployment, skalning och hantering.

## Cluster
Uppsättning av noder (servrar) som kör Kubernetes. Minst en master-node och flera worker-nodes.

## Pod
Minsta enheten i Kubernetes. En eller flera containrar som delar nätverk och lagring.

## Deployment
Kubernetes-resurs som hanterar önskat antal pod-instanser. Rolling updates och rollbacks.

## Service
Abstraktion som exponerar en grupp poddar som en nätverkstjänst. ClusterIP, NodePort, LoadBalancer.

## Namespace
Virtuell klusteruppdelning. Separera dev/test/prod inom samma fysiska kluster.

## kubectl
Kommandoradsverktyg för Kubernetes. `kubectl get pods`, `kubectl apply -f deployment.yaml`.

## Node
En server (virtuell eller fysisk) i Kubernetes-klustret. Worker nodes kör poddar.

## Control Plane
Hanteringslager i Kubernetes. API Server, Scheduler, Controller Manager, etcd.

## etcd
Distribuerad nyckel-värde-databas som lagrar klusterstatus. KRITISK — kräver regelbunden backup.

