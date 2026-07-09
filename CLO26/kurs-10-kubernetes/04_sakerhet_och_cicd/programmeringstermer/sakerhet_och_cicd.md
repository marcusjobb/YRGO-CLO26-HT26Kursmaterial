# 04 Sakerhet Och Cicd — Programmeringstermer

## RBAC
Role-Based Access Control. Styr vem som får göra vad i Kubernetes. Roller + bindningar.

## Service Account
Konto för pods, inte människor. Ge en pod specifika rättigheter via RBAC.

## Secret
Kubernetes-resurs för känslig data (lösenord, API-nycklar, certifikat). Base64-kodad (inte krypterad som standard).

## Pod Security Policy (PSP)
Säkerhetspolicy för pods: får podden köra som root? Vilka capabilities? Security contexts?

## Seccomp
Secure computing mode. Begränsa vilka systemanrop en container får göra.

## CIS Benchmark
Säkerhetsstandard för Kubernetes. Checklista: stärk etcd, API Server, kubelet.

## Falco
Runtime security-verktyg för Kubernetes. Upptäcker avvikande beteende i containrar.

## Kubernetes + Azure DevOps
CI/CD-pipeline som deployar till Kubernetes. Azure DevOps ansluter till AKS.

## Flux CD
GitOps-verktyg för Kubernetes. Synkronisera klustret med Git-repot automatiskt.

## ArgoCD
GitOps-verktyg (alternativ till Flux). Webb-UI, multi-cluster, sync policies.

