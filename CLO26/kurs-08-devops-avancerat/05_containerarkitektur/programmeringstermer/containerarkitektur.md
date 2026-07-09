# 05 Containerarkitektur — Programmeringstermer

## Mikroservice
Arkitektur där applikationen delas i små, självständiga tjänster som kommunicerar via API:er.

## Service Mesh
Infrastrukturlager för service-to-service-kommunikation: trafikstyrning, säkerhet, observability. Exempel: Istio, Linkerd.

## Sidecar Pattern
Container som körs i samma pod som huvudcontainern. Hanterar loggning, proxy, övervakning.

## Orkestrering
Hantering av containrar i stor skala: starta, stoppa, skala, övervaka.

## Service Discovery
Mekanism för tjänster att hitta varandra. I Kubernetes: DNS och Environment Variables.

## Ingress Controller
Hanterar extern trafik in i Kubernetes-klustret. Exempel: Nginx Ingress, Traefik.

## HPA
Horizontal Pod Autoscaler. Skalar antalet poddar baserat på CPU/minne eller custom metrics.

## Health Probe
Kubernetes-kontroll av containerhälsa: liveness (lever?), readiness (tar emot trafik?), startup (startad?).

## Secrets Management
Säker hantering av lösenord, API-nycklar, certifikat. Azure Key Vault, Kubernetes Secrets.

## Container Image Scanning
Skanna container-images för sårbarheter före deployment. Trivy, Snyk, Azure Defender.

