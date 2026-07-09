# CI/CD — Continuous Integration och Continuous Delivery/Deployment

## Varför CI/CD?

Utan CI/CD är deployment en manuell, riskfylld process som sällan görs — och när den görs är risken för fel hög.

**Före CI/CD:**
- Kod → manuell build → manuella tester → zip:a → skicka till drift → hoppas

**Med CI/CD:**
- Git push → automatisk build → automatiska tester → automatisk deployment

## Continuous Integration (CI)

**Mål:** Bygga och testa varje ändring automatiskt så fort den mergas till main-branchen.

**Nyckelkrav:**
- Versionshantering (Git)
- Automatiserad build
- Automatiserade enhetstester
- Snabb feedback (< 10 min)

## Continuous Delivery vs Continuous Deployment

| Aspekt | Continuous Delivery | Continuous Deployment |
|--------|--------------------|------------------------|
| Godkännande | Manuellt (approval gate) | Automatiskt |
| Till produktion | Någon trycker på knappen | När tester passerar |
| Kontroll | Hög (människa i loopen) | Lägre (tester avgör) |
| Risk | Lägre | Högre (men snabbare) |

## Pipeline-steg

1. **Trigger** — vad startar pipelinen? (push till main, PR, schedule)
2. **Build** — kompilera koden, återställ NuGet-paket
3. **Test** — kör enhets-, integrations- och ev. UI-tester
4. **Publish** — spara byggresultat (artifacts)
5. **Deploy** — lägg på server (staging, sedan production)
6. **Approve** — manuellt godkännande (valfritt)

## YAML-pipelines

Modern pipelines definieras som YAML och versionshanteras med koden:

**Fördelar:**
- Samma branch som koden
- Code review på pipeline-ändringar
- Olika pipelines per branch
- Återanvändbara templates

## Azure DevOps vs GitHub Actions

| Azure DevOps | GitHub Actions |
|-------------|----------------|
| Integrerad med Azure | Integrerad med GitHub |
| Klassiska UI-pipelines + YAML | Bara YAML |
| Starkare enterprise-funktioner | Större community-marknad |
| Boards + Repos + Pipelines | Actions + Issues + Projects |

## Viktigaste lärdomarna

- CI = bygg och testa varje ändring automatiskt
- CD = automatisera vägen till produktion
- Delivery = manuellt godkännande, Deployment = helt automatiskt
- YAML-pipelines = infrastruktur som kod
- Välj verktyg baserat på var din kod bor (GitHub vs Azure DevOps)
