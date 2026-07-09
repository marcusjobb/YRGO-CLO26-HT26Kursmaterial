# Träningsuppgifter: Cloud Deploy — Modul 1

> **Moduler:** 01 — CI/CD, 02 — Docker

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är CI/CD?

a. Continuous Integration / Continuous Deployment — automatisera bygge, test och leverans av kod<br>b. Computer Interface / Computer Design<br>c. Code Integration / Code Deployment<br>d. Central Internet / Cloud Database

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Continuous Integration / Continuous Deployment — automatisera bygge, test och leverans av kod

  **Förklaringar:**

  - ✅ **a) Continuous Integration/Deployment** - **RÄTT**: CI = bygg och testa automatiskt varje push. CD = deploya automatiskt till produktion när testerna är gröna. GitHub Actions, Azure DevOps, GitLab CI
  - ❌ **b) Computer Interface** - FEL: Hittar på
  - ❌ **c) Code Integration** - FEL: "Continuous" är en viktig del — det är löpande, inte en engångshändelse
  - ❌ **d) Central Internet** - FEL: Inte ens nära
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är en GitHub Actions workflow?

a. En manuell process för att deploya kod<br>b. En automatiserad pipeline definierad i YAML som triggas av händelser (push, PR, schedule)<br>c. En action-film på GitHub<br>d. Ett sätt att skriva dokumentation

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En automatiserad pipeline definierad i YAML som triggas av händelser

  **Förklaringar:**

  - ❌ **a) Manuell process** - FEL: Poängen med CI/CD är AUTOMATISERING
  - ✅ **b) YAML-pipeline** - **RÄTT**: `.github/workflows/build.yml` — definierar: när ska det köras (on: push), vad ska göras (build, test, deploy). Allt automatiskt
  - ❌ **c) Action-film** - FEL: 😄 Tyvärr inte
  - ❌ **d) Dokumentation** - FEL: Workflows är för automation, inte dokumentation
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är en container (Docker)?

a. En virtuell maskin<br>b. Ett standardiserat paket med kod och alla dess beroenden — körs isolerat med delad värdkärna<br>c. En typ av server<br>d. En databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett standardiserat paket med kod och alla dess beroenden — körs isolerat med delad värdkärna

  **Förklaringar:**

  - ❌ **a) Virtuell maskin** - FEL: Containrar delar värdens OS-kärna, VM har eget OS. Container = MB, VM = GB. Container startar på sekunder, VM på minuter
  - ✅ **b) Paket med kod + beroenden** - **RÄTT**: En Docker-image innehåller ALLT: kod, runtime, bibliotek, miljövariabler. "It works on my machine" blir ett minne blott
  - ❌ **c) Server** - FEL: Containrar KÖRS på servrar, de är inte servrar själva
  - ❌ **d) Databas** - FEL: Containrar kan köra databaser, men är inte databaser
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är skillnaden mellan en Docker-image och en Docker-container?

a. De är samma sak<br>b. En image är mallen/ritningen, en container är en KÖRANDE instans av en image<br>c. En container är mallen, image är instansen<br>d. Image används för utveckling, container för produktion

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En image är mallen/ritningen, en container är en KÖRANDE instans av en image

  **Förklaringar:**

  - ❌ **a) Samma sak** - FEL: Image = klass (ritning), Container = objekt (instans)
  - ✅ **b) Image = ritning, Container = instans** - **RÄTT**: `docker build` skapar en image. `docker run` skapar en container från imagen. Du kan skapa hur många containers som helst från samma image
  - ❌ **c) Omvänt** - FEL: Det är tvärtom
  - ❌ **d) Utveckling vs produktion** - FEL: Båda används i alla miljöer
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad gör en Dockerfile?

a. Startar en container<br>b. Definierar HUR en image ska byggas — bas-image, kommandon, filer som kopieras<br>c. Lagrar lösenord<br>d. Tar bort containrar

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Definierar HUR en image ska byggas — bas-image, kommandon, filer som kopieras

  **Förklaringar:**

  - ❌ **a) Startar** - FEL: `docker run` startar. Dockerfile BYGGER
  - ✅ **b) Bygginstruktioner** - **RÄTT**: 
    ```
    FROM mcr.microsoft.com/dotnet/aspnet:8.0
    COPY bin/Release/publish/ /app/
    ENTRYPOINT ["dotnet", "MyApp.dll"]
    ```
    Varje rad skapar ett lager i imagen
  - ❌ **c) Lösenord** - FEL: Lösenord ska INTE vara i Dockerfile
  - ❌ **d) Ta bort** - FEL: `docker rm` tar bort, inte Dockerfile
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är Docker Compose?

a. Ett verktyg för att skapa Docker-images<br>b. Ett verktyg för att definiera och köra multi-container applikationer med en YAML-fil<br>c. En container-registry<br>d. Ett sätt att komprimera images

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett verktyg för att definiera och köra multi-container applikationer med en YAML-fil

  **Förklaringar:**

  - ❌ **a) Skapa images** - FEL: `docker-compose build` kan bygga, men Compose är främst för ORKESTRERING
  - ✅ **b) Multi-container app** - **RÄTT**: `docker-compose.yml` definierar: web-app, databas, cache, message queue. `docker-compose up` startar ALLA med ett kommando. Alla containrar pratar med varandra på ett internt nätverk
  - ❌ **c) Registry** - FEL: Registry (Docker Hub) lagrar images. Compose kör dem
  - ❌ **d) Komprimera** - FEL: Ingenting med komprimering att göra
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
