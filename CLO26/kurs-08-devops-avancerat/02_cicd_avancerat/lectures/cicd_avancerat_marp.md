---
marp: true
theme: default
class: invert
paginate: true
---

# CI/CD Avancerat

**Kurs:** DevOps Avancerat
**Modul:** 02 — CI/CD på Nästa Nivå

---

## Vad ska vi lära oss idag?

- **Multi-stage pipelines** — dev → staging → prod
- **Deployment strategies** — rolling, blue-green, canary
- **Feature flags** — kontrollera funktioner utan deployment
- **Git-flow vs Trunk-based** — strategier för branching
- **Pipeline caching** — snabbare byggen

---

## Deployment Strategies

### Rolling Update

Gradvis ersätt instanser. Standard i Kubernetes.

```
v1 → v2 → v1 → v2 → v1 → v2 → v2 → v2 → v2
     ↑     ↑     ↑
     Rulla en instans i taget
```

✅ Enkel, fungerar alltid
❌ Båda versionerna kör samtidigt under övergången

---

### Blue-Green

Två identiska miljöer. Växla trafik när den nya är testad.

```
         ┌──────┐
         │ Blue │ ← 100% trafik (nuvarande)
         └──────┘
              ↓ Växla trafik
         ┌──────┐
         │ Green│ ← 100% trafik (ny version)
         └──────┘
```

✅ Omedelbar rollback (växla tillbaka)
❌ Dubbel infrastruktur under deployment

---

### Canary Release

Rulla ut till en liten andel användare först.

```
v2 ── 10% ──→ övervaka → 50% → 100%
v1 ── 90% ──→         → 50% → 0%
```

✅ Minimerar risk — testa på liten grupp först
✅ A/B-testning möjlig
❌ Kräver bra monitorering och metrics

---

## Feature Flags

Kontrollera funktioner i produktion utan deployment:

```csharp
if (featureManager.IsEnabled("DarkMode"))
{
    // Visa dark mode-knapp
}
```

```json
// appsettings.json
{
  "FeatureManagement": {
    "DarkMode": true,
    "NewCheckout": false,
    "AiRecommendations": true
  }
}
```

**Verktyg:** Microsoft.FeatureManagement, LaunchDarkly, Flagsmith

---

## Branchstrategier

| Strategi | Beskrivning | Passar |
|----------|-------------|--------|
| **GitFlow** | main + develop + feature + release + hotfix | Stora team, release-cykler |
| **Trunk-Based** | Kortlivade branches → main dagligen | CI/CD, DevOps-mogna team |
| **GitHub Flow** | feature branch → PR → main | Enkelt, de flesta team |

**Trunk-Based rekommenderas för CI/CD:**
- Färre merge-konflikter
- Snabbare feedback
- Deployment när som helst

---

## Pipeline Caching

Cacha beroenden för snabbare byggen:

```yaml
# GitHub Actions — NuGet cache
- name: Cache NuGet packages
  uses: actions/cache@v4
  with:
    path: ~/.nuget/packages
    key: ${{ runner.os }}-nuget-${{ hashFiles('**/*.csproj') }}
    restore-keys: |
      ${{ runner.os }}-nuget-
```

**Vad cacha?** NuGet, npm, Maven, Docker layers
**Vinst:** 50-80% snabbare byggen

---

## Approval Gates

Kräv manuellt godkännande före deployment till produktion:

```yaml
# Azure DevOps
- stage: DeployToProd
  dependsOn: DeployToStaging
  condition: succeeded()
  jobs:
  - deployment: DeployToProd
    environment: 'production'
    strategy:
      runOnce:
        preDeploy:
          steps:
          - task: ManualValidation@0
            inputs:
              instructions: 'Godkänn deployment till produktion?'
```

---

## Sammanfattning

- ✅ Multi-stage pipelines = separation mellan miljöer
- ✅ Blue-green och canary = minimal risk
- ✅ Feature flags = toggle funktioner utan deployment
- ✅ Trunk-based = snabbare CI/CD
- ✅ Pipeline caching = snabbare byggen
- ✅ Approval gates = kontroll före produktion

---
