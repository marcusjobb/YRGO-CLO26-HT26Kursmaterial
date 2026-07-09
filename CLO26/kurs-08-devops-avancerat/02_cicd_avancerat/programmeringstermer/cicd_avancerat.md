# 02 Cicd Avancerat — Programmeringstermer

## Multi-stage Pipeline
CI/CD-pipeline med flera miljöer: dev → test → staging → production. Manuell approval till prod.

## Approval Gate
Manuellt godkännande i pipeline. Krävs av en person (t.ex. release manager) innan deployment.

## Environment
Miljö i Azure DevOps/ GitHub Actions. Spårar vilka pipelines som deployats till dev/staging/prod.

## Deployment Strategy
Strategi för att lansera ny kod: rolling, blue-green, canary, feature flags.

## Blue-Green
Två identiska miljöer. Blue = nuvarande, Green = ny. Byt trafik när Green är testad.

## Canary Release
Rulla ut ny kod till en liten procent användare först. Öka gradvis. Låg risk.

## Feature Flags
Toggla funktioner av/på utan deployment. Används för A/B-testning och gradvis utrullning.

## Self-hosted Agent
Build agent som du installerar och hanterar själv. Används vid specialiserade krav eller säkerhet.

## Pipeline Caching
Cacha beroenden (NuGet, npm) mellan pipeline-körningar. Minskar byggtiden dramatiskt.

## Retention Policy
Regler för hur länge pipeline-körningar och artifacts sparas. Balans mellan lagringskostnad och spårbarhet.

