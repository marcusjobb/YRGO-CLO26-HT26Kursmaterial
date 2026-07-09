---
marp: true
theme: default
class: invert
paginate: true
---

# CI/CD — Continuous Integration och Deployment

**Kurs:** Cloud Deploy
**Modul:** 01 — CI/CD

---

## Vad ska vi lära oss idag?

- **CI/CD-konceptet** — varför automation?
- **Azure DevOps** — pipelines i Azure
- **GitHub Actions** — pipelines i GitHub
- **YAML-pipelines** — infrastruktur som kod
- **Pipeline-steg** — build, test, deploy

---

## Innan CI/CD — The Dark Ages

```
Utvecklare ↦ Bygg manuellt ↦ Testa ↦ Zip:a ↦ Skicka till drift
                                                      ↦ Drift lägger upp på server
                                                      ↦ Hoppas att det fungerar
```

Problem:
- ❌ Manuella steg glöms bort
- ❌ "Det fungerade på min maskin"
- ❌ Veckor mellan kod → produktion
- ❌ Ingen vet vad som deployats

---

## Med CI/CD

```
Git push ↦ Build ↦ Test ↦ Staging ↦ Godkänn ↦ Produktion
  (CI)     (CI)   (CI)     (CD)       (Gate)    (CD)
```

Fördelar:
- ✅ Automatiserat — inga manuella steg
- ✅ Varje push byggs och testas
- ✅ Deployment är tråkig (alltid samma)
- ✅ Omedelbar feedback vid fel

---

## CI — Continuous Integration

**Integration = merge:a kod till main**

```yaml
# azure-pipelines.yml (CI-del)
trigger:
- main

pool:
  vmImage: 'ubuntu-latest'

steps:
- task: NuGetToolInstaller@1
- task: NuGetCommand@2
  inputs:
    restoreSolution: '**/*.sln'
- task: DotNetCoreCLI@2
  inputs:
    command: 'build'
    projects: '**/*.csproj'
- task: DotNetCoreCLI@2
  inputs:
    command: 'test'
    projects: '**/*Tests.csproj'
```

Varje push → bygg + test. Om något failar → omedelbar notifiering.

---

## CD — Continuous Delivery/Deployment

**Delivery:** redo att deploya (manuellt godkännande)
**Deployment:** automatisk deployment till produktion

```yaml
# Azure DevOps — Deploy stage
- stage: Deploy
  jobs:
  - deployment: DeployToProd
    environment: 'production'
    strategy:
      runOnce:
        deploy:
          steps:
          - task: AzureWebApp@1
            inputs:
              appName: 'my-app'
              package: '$(System.DefaultWorkingDirectory)/**/*.zip'
```

---

## GitHub Actions — Alternativet

```yaml
# .github/workflows/deploy.yml
name: Deploy to Azure

on:
  push:
    branches: [main]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v4
    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: '8.0'
    - name: Build & Test
      run: |
        dotnet build
        dotnet test
    - name: Deploy
      uses: azure/webapps-deploy@v3
      with:
        app-name: my-app
        publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
```

---

## YAML — Varför?

Förr: GUI-redigerade pipelines i Azure DevOps (klassiska UI-pipelines).

Nu: **YAML-pipelines** — definitionsfilen versionshanteras med koden.

**Fördelar:**
- ✅ Samma branch som koden
- ✅ Code review på pipeline-ändringar
- ✅ Olika pipelines per branch
- ✅ Återanvändbara templates

---

## Pipeline-steg

| Steg | Beskrivning | Exempel |
|------|-------------|---------|
| **Trigger** | Vad startar pipelinen? | push till main |
| **Build** | Kompilera koden | dotnet build |
| **Test** | Kör enhetstester | dotnet test |
| **Publish** | Spara byggresultat | PublishBuildArtifacts |
| **Deploy** | Lägg på server | AzureWebApp |
| **Approve** | Manuell kontroll | Approval gate |

---

## Sammanfattning

- ✅ CI = bygg och testa varje ändring automatiskt
- ✅ CD = automatisera deployment till produktion
- ✅ Azure DevOps och GitHub Actions = två populära verktyg
- ✅ YAML = pipeline som kod, versionshanteras
- ✅ Pipeline: Build → Test → Deploy
- ➡️ Nästa: Docker — containerisera din applikation

---
