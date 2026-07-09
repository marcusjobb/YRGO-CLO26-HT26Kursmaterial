# Continuous Delivery

## Continuous Delivery vs Continuous Deployment

**Delivery:** Varje ändring är redo att deployas till produktion, men en människa trycker på knappen (approval gate).

**Deployment:** Varje ändring som passerar testerna deployas automatiskt till produktion.

Många team börjar med Delivery och övergår till Deployment när förtroendet för testerna är tillräckligt högt.

## Release Train

Schemalagda releaser med fast tidtabell:

- "Det som är klart till torsdag kl 15 åker med på fredagens tåg"
- Förutsägbart för både team och verksamhet
- Minskar stressen — du måste inte få in allt i just denna release

## Semantic Versioning (SemVer)

```
MAJOR.MINOR.PATCH
```

| Exempel | När |
|---------|-----|
| 2.0.0 | Bakåtinkompatibel API-ändring |
| 1.1.0 | Ny funktion, bakåtkompatibel |
| 1.0.1 | Bugfix, bakåtkompatibel |
| 1.0.0-beta | Pre-release |

## Artifact Management

Var byggresultat (artifacts) lagras och versionshanteras.

**God praxis:**
- Inkludera buildnummer i artifact-namn
- Sätt retention policy (30–90 dagar)
- Signera artifacts för integritet

## Supply Chain Security

| Hot | Skydd |
|-----|-------|
| Komprometterat paket | Lås versioner, verifiera signatur |
| Man-in-the-middle | HTTPS för alla feeds |
| Föråldrade beroenden | Dependabot/Renovate |
| Okända sårbarheter | SBOM + scanning |

**SBOM (Software Bill of Materials):** En inventeringslista över alla beroenden i din applikation. Genereras automatiskt och används för sårbarhetsanalys.

## Viktigaste lärdomarna

- Delivery = manuellt godkännande, Deployment = automatiskt
- Release train = förutsägbarhet och lugn
- SemVer = tydlig kommunikation om ändringar
- Versionshantera och signera alla artifacts
- SBOM och scanning = säkra din supply chain
