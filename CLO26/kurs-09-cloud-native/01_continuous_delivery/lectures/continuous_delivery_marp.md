---
marp: true
theme: default
class: invert
paginate: true
---

# Continuous Delivery

**Kurs:** Cloud Native
**Modul:** 01 — Continuous Delivery

---

## Vad ska vi lära oss idag?

- **Continuous Delivery vs Deployment** — skillnaden
- **Release strategies** — när och hur lansera
- **Semantic Versioning** — versionshantering som fungerar
- **Artifact management** — lagra och versionshantera byggen
- **Supply chain security** — säkra dina beroenden

---

## Continuous Delivery vs Deployment

```
CI                              CD (Delivery)            CD (Deployment)
──────────                      ──────────────────       ──────────────────
Bygg + testa                    Redo att deploya         Automatiskt till prod
automatiskt                     ✅ Manuellt              🔥 Automatiskt
✅ Varje push                     godkännande             (efter tester)
                                (Approval gate)
```

**Delivery:** Vi bygger och testar allt. En människa trycker på knappen för att lansera.
**Deployment:** Allt är automatiskt — testerna bestämmer om det går till produktion.

---

## Release Train

Schemalagda releaser (t.ex. varje fredag):

```
Vecka 1     Vecka 2     Vecka 3     Vecka 4     Vecka 5
┌──────┐    ┌──────┐    ┌──────┐    ┌──────┐    ┌──────┐
│ Dev  │    │ Dev  │    │ Dev  │    │ Dev  │    │ Dev  │
└──────┘    └──────┘    └──────┘    └──────┘    └──────┘
   │           │           │           │           │
   ▼           ▼           ▼           ▼           ▼
┌──────┐    ┌──────┐    ┌──────┐    ┌──────┐    ┌──────┐
│ Rel. │    │ Rel. │    │ Rel. │    │ Rel. │    │ Rel. │
└──────┘    └──────┘    └──────┘    └──────┘    └──────┘
```

**Regel:** Det som är klart till torsdag kl 15 åker med på fredagens tåg. Resten väntar till nästa vecka.

---

## Semantic Versioning (SemVer)

Format: `MAJOR.MINOR.PATCH`

| Version | När öka | Exempel |
|---------|---------|---------|
| **MAJOR** | Bakåtinkompatibel ändring | 2.0.0, 3.0.0 |
| **MINOR** | Ny funktion, bakåtkompatibel | 1.1.0, 1.2.0 |
| **PATCH** | Bugfix, bakåtkompatibel | 1.0.1, 1.0.2 |

```yaml
# I CI/CD-pipeline: auto-generera version
- name: Tag release
  run: |
    git tag -a v${{ github.event.release.tag_name }} -m "Release"
```

**Pre-release:** 1.0.0-alpha, 1.0.0-beta, 1.0.0-rc.1
**Build metadata:** 1.0.0+build.20260630

---

## Artifact Management

Var byggresultat lagras och hur de hanteras:

```yaml
# Azure DevOps — Publish artifact
- task: PublishBuildArtifacts@1
  inputs:
    artifactName: 'webapp-drop'
    pathToPublish: '$(Build.ArtifactStagingDirectory)/publish'

# GitHub Actions — Upload artifact
- uses: actions/upload-artifact@v4
  with:
    name: webapp
    path: ./publish/
```

**God praxis:**
- Versionshantera artifacts (inkludera buildnummer)
- Sätt retention policy (ta bort gamla efter 30 dagar)
- Signera artifacts (säkerställ integritet)

---

## Supply Chain Security

Skydda din beroendekedja från attacker:

| Hot | Skydd |
|-----|-------|
| Komprometterat NuGet-paket | Lås versioner, verifiera signatur |
| Man-in-the-middle | HTTPS för alla feeds |
| Föråldrade beroenden | Dependabot/Renovate, automatisk uppdatering |
| Okända sårbarheter | SBOM, sårbarhetsscanning |

**SBOM (Software Bill of Materials):**
```bash
# Generera SBOM för .NET-projekt
dotnet tools install --global CycloneDX
dotnet CycloneDX myapp.csproj -o bom.json
```

---

## Sammanfattning

- ✅ CD Delivery = redo att deploya (manuellt godkännande)
- ✅ CD Deployment = automatiskt till produktion
- ✅ Release train = schemalagda releaser
- ✅ SemVer = MAJOR.MINOR.PATCH
- ✅ Artifact management = spara och versionshantera byggen
- ✅ Supply chain security = SBOM, sårbarhetsscanning

---
