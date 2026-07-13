# Övning — Automatisk deploy med GitHub Actions

🟡

---

## Bakgrunden

HyresBil AB har precis anställt dig som molnutvecklare. De har en .NET 10 Minimal API som kör på en Azure VM — men just nu deployas den manuellt. Någon kör `dotnet publish`, kopierar filer med SCP och startar om tjänsten för hand.

Det är ett problem. Förra fredagen deployaddes fel version av koden till produktionen av misstag. Fredagar ska vara fredagar — inte katastrofdagar.

Din uppgift: sätt upp en CI/CD-pipeline med GitHub Actions. Varje push till `main` ska automatiskt bygga, testa och deploya HyresBil-appen. Utan att någon behöver lyfta ett finger.

---

## Vad gäller för den här övningen

- [ ] Som utbildare vill jag att du kan skapa en komplett GitHub Actions workflow som triggas på push
- [ ] Som utbildare vill jag att du förstår skillnaden mellan CI (bygga och testa) och CD (deploya)
- [ ] Som utbildare vill jag att du kan lagra känslig information i GitHub Secrets — aldrig i YAML-filen
- [ ] Som utbildare vill jag att du kan läsa och tolka en Actions-körning i GitHub-gränssnittet
- [ ] Som utbildare vill jag att du förstår varför `dotnet test` måste passera innan deployment startar

---

## Förutsättningar

Innan du börjar behöver du ha:

- En Azure VM med Ubuntu 24.04 och .NET 10 Runtime installerat
- En systemd-tjänst för HyresBil som heter `hyresbil.service`
- SSH-åtkomst till VM:en med ett nyckelpar (inte lösenord)
- Ett GitHub-repo med HyresBil-koden
- Minst ett test i projektet (annars jobbar CI utan nät)

Har du inte VM och tjänsten redo? Gör klart [övning: Deploy .NET MVC App med Systemd](clo25_exercises_3_deployment_2_deploy_dotnet_mvc_scp_systemd.md) först.

---

## Uppgift

### Del 1 — Förstå vad du ska bygga

En GitHub Actions workflow är en YAML-fil som beskriver en pipeline. Den triggas automatiskt av händelser — i det här fallet ett `push` till `main`.

Pipelinen delas upp i två delar:

**CI — Continuous Integration**
> Syftet är att fånga fel tidigt. Varje push bygger och testar koden på GitHub:s egna servrar. Om ett test misslyckas stoppas allt — inget deployas.

**CD — Continuous Deployment**
> Syftet är att automatisera leveransen. När CI är grön kopieras den publicerade appen till VM:en via SSH, och tjänsten startas om.

```
Push till main
    │
    ▼
┌─────────────────────────────────────┐
│  CI-jobb (körs på GitHub-server)    │
│  checkout → build → test → publish  │
│  Sparar artifacts om allt är grönt  │
└────────────────┬────────────────────┘
                 │ needs: build (väntar på att CI är klar)
                 ▼
┌─────────────────────────────────────┐
│  CD-jobb (körs på GitHub-server)    │
│  Laddar ned artifacts               │
│  Kopierar filer till VM via SCP     │
│  SSH in → stoppa → ersätt → starta  │
└─────────────────────────────────────┘
```

---

### Del 2 — Lägg till GitHub Secrets

SSH-nyckeln och VM:ens IP-adress ska **aldrig** stå i YAML-filen. GitHub Secrets krypterar dem och gör dem tillgängliga som miljövariabler i pipelinen.

1. Gå till ditt GitHub-repo
2. Klicka på **Settings → Secrets and variables → Actions**
3. Klicka **New repository secret** och lägg till dessa tre:

| Secret-namn | Värde |
|---|---|
| `SSH_PRIVATE_KEY` | Innehållet i din privata SSH-nyckel (`~/.ssh/id_rsa`) |
| `VM_IP` | VM:ens publika IP-adress |
| `VM_USERNAME` | Vanligtvis `azureuser` |

Hämta din privata nyckel med:

```bash
cat ~/.ssh/id_rsa
```

Kopiera allt — inklusive `-----BEGIN OPENSSH PRIVATE KEY-----` och `-----END OPENSSH PRIVATE KEY-----`.

> **Varför?** Om du committar SSH-nyckeln till repot är den permanent exponerad — även om du tar bort den i nästa commit. Git-historiken glömmer aldrig. GitHub Secrets är den rätta platsen.

---

### Del 3 — Skapa workflow-filen

Skapa katalogstrukturen i ditt projekt:

```
.github/
  workflows/
    deploy.yml
```

Klistra in hela workflow-filen nedan. Läs igenom den **innan** du commitar — förstå vad varje steg gör.

```yaml
# .github/workflows/deploy.yml
# Deployas automatiskt när du pushar till main.
# CI: bygger och testar koden på GitHub-servrar
# CD: deployas till Azure VM via SSH

name: HyresBil — Build och Deploy

on:
  push:
    branches:
      - main
  workflow_dispatch:  # Gör det möjligt att starta pipelinen manuellt

jobs:

  # ─────────────────────────────────────────────
  # CI-JOB — Continuous Integration
  # Körs på GitHub:s egna servrar (gratis)
  # ─────────────────────────────────────────────
  build:
    name: Bygg och testa
    runs-on: ubuntu-latest

    steps:
      - name: Hämta koden från repot
        uses: actions/checkout@v4

      - name: Installera .NET 10 SDK
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '10.0.x'

      - name: Återställ NuGet-paket
        run: dotnet restore

      - name: Bygg projektet
        run: dotnet build --no-restore --configuration Release

      - name: Kör alla tester
        # Om ett test misslyckas stoppas hela pipelinen här.
        # Inget deployas om tester är röda.
        run: dotnet test --no-build --configuration Release --verbosity normal

      - name: Publicera appen (release-build)
        run: dotnet publish --no-build --configuration Release --output ./publish

      - name: Ladda upp artifacts till GitHub
        # Sparar publish-mappen så att CD-jobbet kan hämta den
        uses: actions/upload-artifact@v4
        with:
          name: hyresbil-app
          path: ./publish
          retention-days: 3

  # ─────────────────────────────────────────────
  # CD-JOB — Continuous Deployment
  # Körs bara om CI-jobbet lyckades
  # ─────────────────────────────────────────────
  deploy:
    name: Deploya till Azure VM
    runs-on: ubuntu-latest
    needs: build  # Väntar på att build-jobbet är klart och grönt

    steps:
      - name: Ladda ned artifacts från GitHub
        uses: actions/download-artifact@v4
        with:
          name: hyresbil-app
          path: ./publish

      - name: Kopiera filer till VM via SCP
        uses: appleboy/scp-action@v0.1.7
        with:
          host: ${{ secrets.VM_IP }}
          username: ${{ secrets.VM_USERNAME }}
          key: ${{ secrets.SSH_PRIVATE_KEY }}
          source: "./publish/*"
          target: "/tmp/hyresbil-deploy"
          strip_components: 1

      - name: Stoppa tjänst, ersätt filer och starta om
        uses: appleboy/ssh-action@v1.0.3
        with:
          host: ${{ secrets.VM_IP }}
          username: ${{ secrets.VM_USERNAME }}
          key: ${{ secrets.SSH_PRIVATE_KEY }}
          script: |
            # Stoppa applikationen innan vi byter ut filerna
            sudo systemctl stop hyresbil.service

            # Ta bort gamla filer och flytta nya på plats
            sudo rm -rf /opt/hyresbil
            sudo mv /tmp/hyresbil-deploy /opt/hyresbil

            # Sätt rätt ägare på filerna
            sudo chown -R www-data:www-data /opt/hyresbil

            # Starta applikationen igen
            sudo systemctl start hyresbil.service

            # Verifiera att tjänsten faktiskt startade
            sudo systemctl is-active --quiet hyresbil.service && echo "Tjänsten är igång" || echo "FEL: Tjänsten startade inte"
```

---

### Del 4 — Pusha och följ körningen

Committa och pusha workflow-filen:

```bash
git add .github/workflows/deploy.yml
git commit -m "ci: lägg till GitHub Actions deploy-pipeline"
git push origin main
```

Gå sedan till ditt GitHub-repo och klicka på **Actions**-fliken. Du ser nu din pipeline köra i realtid.

Vad du tittar på:

- Det gula snurrande hjulet = jobbet körs just nu
- Grön bock = jobbet lyckades
- Röd X = något gick fel — klicka på jobbet för att se exakt vilket steg som failade och felmeddelandet

Klicka på **build**-jobbet och expandera varje steg för att se vad som hände. `dotnet test`-steget visar vilka tester som kördes och om de passerade.

När `build` är grön startar `deploy`-jobbet automatiskt. Följ stegen — du ser SSH-sessionen live medan den kopierar filer och startar om tjänsten.

---

### Del 5 — Verifiera att det faktiskt fungerar

SSH:a in på VM:en och kontrollera att tjänsten körs:

```bash
ssh azureuser@<VM_IP>
sudo systemctl status hyresbil.service
```

Förväntad output:

```
● hyresbil.service - HyresBil Minimal API
     Loaded: loaded (/etc/systemd/system/hyresbil.service; enabled)
     Active: active (running) since ...
```

Kontrollera också att appen svarar:

```bash
curl http://localhost:5000/
```

Gör sedan en liten ändring i koden (t.ex. ändra en route-text), pusha till `main` och se hur pipelinen kör automatiskt och appen uppdateras på servern.

---

## Förväntad output i Actions-fliken

En lyckad körning ser ut så här:

```
HyresBil — Build och Deploy
  ✓ build    (Bygg och testa)        ~45s
  ✓ deploy   (Deploya till Azure VM) ~30s

Alla jobb lyckades.
```

Om ett test misslyckas ser du:

```
HyresBil — Build och Deploy
  ✗ build    (Bygg och testa)        ~20s
  ○ deploy   (Deploya till Azure VM) Skippades — build misslyckades

1 jobb misslyckades.
```

Det är CI som gör sitt jobb. Inget deployas till produktion förrän koden är grön.

---

## Tips

> `needs: build` i deploy-jobbet är det som kopplar ihop CI och CD. Utan den skulle båda jobben köra parallellt — och deploy kan börja innan build ens är klar.

> Secrets nås i YAML med `${{ secrets.DITT_NAMN }}`. Om du stavar fel på secret-namnet misslyckas steget tyst — Actions visar inte att värdet är tomt av säkerhetsskäl. Kontrollera stavningen under **Settings → Secrets**.

> `workflow_dispatch:` i `on:`-blocket låter dig starta pipelinen manuellt från Actions-fliken. Bra för att testa utan att behöva pusha dummy-commits.

> ⏱️ **15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

---

*Facit finns hos läraren.*
