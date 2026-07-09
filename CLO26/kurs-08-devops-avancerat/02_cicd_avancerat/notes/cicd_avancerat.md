# CI/CD Avancerat

## Multi-stage Pipelines

En pipeline med flera steg (stages) möjliggör separation mellan miljöer:

```
Build → Test → Deploy to Staging → Approval → Deploy to Production
```

Varje stage kan ha olika krav, agenter och godkännanden.

## Deployment Strategies

### Rolling Update
Gradvis ersättning av instanser. Standard i Kubernetes.

**Fördel:** Enkel, ingen extra infrastruktur.
**Nackdel:** Båda versionerna kör samtidigt under övergången.

### Blue-Green
Två identiska miljöer. Växla trafik när den nya är testad.

**Fördel:** Omedelbar rollback (växla tillbaka).
**Nackdel:** Dubbel infrastruktur under deployment.

### Canary Release
Rulla ut till en liten andel användare först, övervaka, öka gradvis.

**Fördel:** Minimerar risk — testa på liten grupp.
**Kräver:** Bra monitorering och metrics.

## Feature Flags

Kontrollera funktioner i produktion utan ny deployment:

```csharp
if (featureManager.IsEnabled("DarkMode"))
{
    // Visa dark mode-knapp
}
```

**Användning:** A/B-testning, gradual rollouts, kill switches.

## Branchstrategier för CI/CD

| Strategi | Passar |
|----------|--------|
| **Trunk-Based** | CI/CD, DevOps-mogna team — kortlivade branches, main dagligen |
| **GitHub Flow** | De flesta team — feature branch → PR → main |
| **GitFlow** | Stora team med release-cykler |

Trunk-based rekommenderas för snabb CI/CD.

## Pipeline Caching

Cacha beroenden för 50–80% snabbare byggen:

- **NuGet** — `~/.nuget/packages`
- **npm** — `~/.npm`
- **Docker** — lager-cache

## Viktigaste lärdomarna

- Blue-green och canary = minimal risk vid deployment
- Feature flags frikopplar deployment från release
- Trunk-based branching = snabbare CI/CD
- Caching påskyndar pipelines dramatiskt
- Approval gates ger kontroll utan att bromsa
