# Kursplansförslag för Zane — Kurserna 5–12 (Cloud)

Skapad av Marcus Medina som överlämning till Zane.
Baserad på Lars Appels CLO25s kursstruktur (cloud-dev-25.educ8.se) som inspiration för ämnesordning.
**Zane bygger eget material** — detta är en planeringsöversiktsförslag, inte ett manuskript.

Se `hur_vi_bygger_kurser.md` för hur vi strukturerar kurser, mallar och konventioner.

---

## Kurs 5 — Introduktion till Cloudutveckling
**10 YH-poäng | 2 veckor | Kalender: v41–42 (5–18 okt 2026)**
*Inga inlämningar — ingen tenta vecka 2 (för kort kurs)*

| Vecka | Tema            | Innehåll                                                    |
| ----- | --------------- | ----------------------------------------------------------- |
| 1     | Virtual Servers | Azure-konto, första VM, SSH, Linux-terminal, vad är cloud?  |
| 2     | IaC och CLI     | Azure CLI, ARM/Bicep, cloud-init, repeaterbar infrastruktur |

**Lärande:** Från "vad är cloud?" till "jag kan provisionera en VM på tre sätt."

---

## Kurs 6 — Grundläggande molnapplikationer
**40 YH-poäng | 8 veckor | Kalender: v43–50 (19 okt – 13 dec 2026)**
*Inlämning var tredje vecka — tenta sista veckan*

| Vecka | Tema                 | Innehåll                                                         |
| ----- | -------------------- | ---------------------------------------------------------------- |
| 1     | Webbutveckling intro | ASP.NET Core MVC, HTTP-grunder, .NET-plattformen                 |
| 2     | Virtuella nätverk    | VNets, NSG, brandväggar, OSI-modellen, bastion host              |
| 3     | Web Deep Dive        | 3-tier arkitektur, Dependency Injection, konfiguration per miljö |
| 4     | Lagring              | CosmosDB, Blob Storage, SQL vs NoSQL, databaser i molnet         |
| 5     | CI/CD                | GitHub Actions, pipelines, bygg/test/deploy automatiserat        |
| 6     | Secret Management    | Azure Key Vault, Managed Identities, OIDC, inga lösenord i kod   |
| 7     | Monitoring           | Application Insights, Log Analytics, KQL, Health Checks, Alerts  |
| 8     | Wrap-up + Tenta      | Repetition, koda ikapp, tenta                                    |

**Inlämningar:**
- Inlämning 1 (efter v3): VM + IaC + MVC-app deployad
- Inlämning 2 (efter v6): Databas + CI/CD + Secret Management
- Tenta vecka 8

---

## Kurs 7 — Konsultmässighet och kommunikation
**10 YH-poäng | 2 veckor**
*Hanteras av Marcus — se kurs-04-test-och-kvalitet/_teacher/course_plans/07_konsultmassighet.md*

---

## Kurs 8 — Molnapplikationer fördjupning
**40 YH-poäng | 8 veckor**
*Kalender: Period 2 HT27 — exakt schema TBD*

| Vecka | Tema                    | Innehåll                                                          |
| ----- | ----------------------- | ----------------------------------------------------------------- |
| 1     | Agilt arbetsflöde       | Jira, Scrum, branching-strategi, pull requests, code review       |
| 2     | Docker och Compose      | Containers vs VMs, Dockerfile, multi-stage builds, Docker Compose |
| 3     | Auth och AuthZ          | Cookie auth, JWT, Roles/Claims, ASP.NET Core Identity             |
| 4     | CI/CD till Azure        | Azure Container Apps, privat registry, OIDC-deployment            |
| 5     | Logging och Monitoring  | ILogger, Log Analytics, Application Insights, SLOs                |
| 6     | REST API och DTOs       | REST-principer, DTOs, Swagger, versioning, rate limiting          |
| 7     | Blob Storage och Health | File uploads, deep health probes, managed identity                |
| 8     | Wrap-up + Tenta         | Examination                                                       |

---

## Kurs 9 — Skalbara molnapplikationer
**40 YH-poäng | 8 veckor**
*Innehåll TBD av Zane — fokus på skalbarhet, load balancing, multi-region, kostnad*

---

## Kurs 10 — Kubernetes
**20 YH-poäng | 4 veckor**
*Innehåll TBD av Zane — Pods, Deployments, Services, Helm, CI/CD med k8s*

---

## Kurs 11 — Examensarbete
**40 YH-poäng | 8 veckor**
*Se `_teacher/course_plans/11_examensarbete.md` för lärandemål och betygskriterier.*

---

## Kurs 12 — Lärande i arbete (LIA)
**100 YH-poäng | 20 veckor**
*Se `_teacher/course_plans/12_larande_i_arbete.md` för lärandemål och betygskriterier.*

---

## Tips till Zane

- Läs `hur_vi_bygger_kurser.md` — pedagogiken och konventionerna vi kommit överens om
- Mallarna för föreläsningar, övningar och inlämningar finns i `_templates/`
- Skrivstiilen finns i `writing_rules.md` — materialet ska hålla Marcus Medina-känslan
- Kursplanerna med officiella lärandemål finns i `_teacher/course_plans/`
- Referensmaterial från CLO25 (inspiration, ej kopiera) finns lokalt på lärardatorn

**Kom ihåg:** Det är OK att göra saker annorlunda — detta är ett förslag, inte ett facit.
