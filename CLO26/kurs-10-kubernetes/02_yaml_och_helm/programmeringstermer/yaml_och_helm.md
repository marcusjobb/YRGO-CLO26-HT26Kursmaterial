# 02 Yaml Och Helm — Programmeringstermer

## YAML
Human-readable data serialisering. Används för Kubernetes-manifest. Indentering med MELLANSLAG (2 spaces).

## Helm
Package manager för Kubernetes. Mallbaserade templates, versionshantering, dependencies.

## Chart
Helm-paket. Innehåller templates, värden och metadata för att deploya en applikation.

## values.yaml
Helm-konfigurationsfil som innehåller standardvärden för templates.

## Template
Helm-mall (Go templates) som genererar Kubernetes-manifest med variabler från values.yaml.

## Release
En installerad instans av ett Helm-chart. Du kan ha flera releases av samma chart.

## Rollback (Helm)
`helm rollback <release> <revision>`. Återgå till tidigare version.

## Repository (Helm)
Plats där Helm-charts lagras. Artifact HUB, Azure Container Registry, GitHub Pages.

## Kubernetes Manifest
YAML-fil som definierar Kubernetes-resurser (apiVersion, kind, metadata, spec).

## Declarative Config
Kubernetes-konfiguration är deklarativ: du beskriver önskat tillstånd, Kubernetes gör det.

