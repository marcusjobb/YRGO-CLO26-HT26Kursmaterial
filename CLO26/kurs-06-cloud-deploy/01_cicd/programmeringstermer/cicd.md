# 01 Cicd — Programmeringstermer

## CI
Continuous Integration. Kodändringar byggs och testas automatiskt flera gånger dagligen.

## CD
Continuous Delivery/Deployment. Automatiserad utlansering av kod till produktion.

## Pipeline
Automatiserad kedja av steg: bygg → test → deploy. Varje steg måste lyckas innan nästa körs.

## Build Agent
Server som kör CI/CD-jobben. Kan vara Microsoft-värd (Azure DevOps) eller self-hosted.

## Artifact
Byggresultat (t.ex. .dll, .exe, .zip) som sparas och kan användas i senare pipeline-steg.

## Trigger
Händelse som startar en pipeline. Exempel: push till main, pull request, schemalagd tid.

## YAML Pipeline
CI/CD-pipeline definierad i YAML-format (azure-pipelines.yml). Versionshanteras med koden.

## Stage
Logisk del av en pipeline: Build, Test, Deploy. Varje stage kan ha flera jobs.

## Job
Steg i en pipeline som körs på en build agent. Ett job kan ha flera tasks.

## Task
Enskilt kommando i en pipeline: dotnet build, dotnet test, PublishArtifact.

