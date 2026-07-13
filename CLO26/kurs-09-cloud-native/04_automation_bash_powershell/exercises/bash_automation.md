# Övning: Automatisera deployment med Bash 🟡

**Tid:** 15 minuter  
**Scenario:** Du är backend-utvecklare på HyresBil AB. Varje fredag kl. 16:30 gör teamet manuell deployment. Det tar 45 minuter, saker glöms bort och någon sitter alltid kvar en timme efter arbetstid och felsöker. Din uppgift: ersätt hela processen med ett enda bash-skript.

---

Du får ett halvfärdigt skript. Din uppgift är att fylla i de delar som saknas och göra det körbart.

```bash
#!/usr/bin/env bash
# deploy.sh — HyresBil automatisk deployment
# Kör: ./deploy.sh
# Kräver: SSH-nyckel konfigurerad mot VM_HOST

set -euo pipefail

# ── Konfiguration ─────────────────────────────────────────────────────────────
APP_NAME="hyresbil-api"
PROJECT_DIR="./src/HyresBilApi"
PUBLISH_DIR="./publish"
REMOTE_DIR="/opt/hyresbil"

VM_HOST="${VM_HOST:?Sätt VM_HOST som miljövariabel}"
VM_USER="${VM_USER:-azureuser}"
SERVICE_NAME="${SERVICE_NAME:-hyresbil-api}"
HEALTH_URL="${HEALTH_URL:-http://${VM_HOST}:5000/health}"

# ── Hjälpfunktioner ───────────────────────────────────────────────────────────
log()     { echo "[$(date '+%H:%M:%S')] $*"; }
success() { echo "[OK]  $*"; }
fail()    { echo "[FEL] $*" >&2; exit 1; }

# ── Steg 1: Bygg applikationen ────────────────────────────────────────────────
build() {
  log "Bygger $APP_NAME..."
  rm -rf "$PUBLISH_DIR"
  dotnet publish "$PROJECT_DIR" \
    --configuration Release \
    --output "$PUBLISH_DIR" \
    --nologo \
    --quiet
  success "Build klar"
}

# ── Steg 2: Kopiera filer till VM ─────────────────────────────────────────────
deploy_files() {
  log "Kopierar filer till $VM_USER@$VM_HOST:$REMOTE_DIR..."
  # TODO: använd scp -r för att kopiera PUBLISH_DIR till VM
  # Format: scp -r <källa> <användare>@<host>:<mål>
  # skriv din scp-rad här

  success "Filer kopierade"
}

# ── Steg 3: Starta om tjänsten ────────────────────────────────────────────────
restart_service() {
  log "Startar om $SERVICE_NAME på VM..."
  # TODO: använd ssh för att köra systemctl restart på distans
  # ssh <användare>@<host> "sudo systemctl restart <tjänst>"
  # skriv din ssh-rad här

  success "Tjänst omstartad"
}

# ── Steg 4: Hälsokontroll ─────────────────────────────────────────────────────
health_check() {
  local max_attempts=10
  local attempt=1

  log "Kontrollerar hälsa mot $HEALTH_URL..."

  while [[ $attempt -le $max_attempts ]]; do
    if curl --silent --fail --max-time 3 "$HEALTH_URL" > /dev/null; then
      success "Hälsokontroll godkänd (försök $attempt)"
      return 0
    fi
    log "Försök $attempt/$max_attempts misslyckades — väntar 3 sekunder..."
    sleep 3
    (( attempt++ ))
  done

  fail "Hälsokontroll misslyckades efter $max_attempts försök"
}

# ── Steg 5: Notifiering ───────────────────────────────────────────────────────
notify() {
  local status="$1"
  local timestamp
  timestamp=$(date '+%Y-%m-%d %H:%M:%S')

  echo ""
  echo "════════════════════════════════════════"
  echo "  DEPLOYMENT: $status"
  echo "  App:     $APP_NAME"
  echo "  VM:      $VM_HOST"
  echo "  Tid:     $timestamp"
  echo "════════════════════════════════════════"
}

# ── Huvudflöde ────────────────────────────────────────────────────────────────
main() {
  log "Startar deployment av $APP_NAME"

  build
  deploy_files
  restart_service
  health_check
  notify "LYCKADES"
}

main "$@"
```

---

## Din uppgift

**Del 1 — Fyll i luckorna**

Hitta de två `TODO`-kommentarerna i skriptet och skriv de rader som saknas.

- `deploy_files`: en `scp -r`-rad som kopierar `$PUBLISH_DIR` till `$VM_USER@$VM_HOST:$REMOTE_DIR`
- `restart_service`: en `ssh`-rad som kör `sudo systemctl restart $SERVICE_NAME` på VM

**Del 2 — Gör skriptet körbart**

Spara skriptet som `_scripts/deploy.sh` och kör:

```bash
chmod +x _scripts/deploy.sh
```

Verifiera att det syns som körbart:

```bash
ls -l _scripts/deploy.sh
```

### Förväntad output

```
-rwxr-xr-x 1 azureuser azureuser 1842 jul 13 16:00 _scripts/deploy.sh
```

**Del 3 — Kör med miljövariabler**

Skriptet ska aldrig ha hårdkodade IP-adresser. Testa att starta det utan miljövariabler och läs felmeddelandet:

```bash
./deploy.sh
```

### Förväntad output

```
./deploy.sh: rad 16: VM_HOST: Sätt VM_HOST som miljövariabel
```

Kör sedan med rätt miljövariabler (byt ut mot din faktiska VM):

```bash
VM_HOST=20.240.10.55 VM_USER=azureuser ./deploy.sh
```

**Del 4 — Felsök med tracing**

Bash har ett inbyggt felsökningsläge som skriver ut varje kommando innan det körs. Testa det:

```bash
VM_HOST=20.240.10.55 bash -x _scripts/deploy.sh 2>&1 | head -30
```

Titta på outputen. Vad ser du? Varför är det användbart att se exakt vilka kommandon som körs?

---

## Utmanande frågor

1. Skriptet använder `set -euo pipefail` på rad 7. Vad gör de tre flaggorna `-e`, `-u` och `-o pipefail` var för sig — och vad händer om du tar bort en av dem?

2. `health_check`-funktionen försöker upp till 10 gånger med 3 sekunders mellanrum. Är det idempotent att köra `health_check` tio gånger i rad? Motivera.

3. Varför är `${VM_HOST:?Sätt VM_HOST som miljövariabel}` bättre än att bara skriva `$VM_HOST` och låta skriptet krascha längre ned i exekveringen?

---

## Lösningsförslag

Skriptet använder `scp` och `ssh` för att kommunicera med VM:en. De saknade raderna är enkla, men de visar exakt samma mönster du använder i manuell deployment — bara automatiserat.

**`deploy_files`:**

```bash
scp -r "$PUBLISH_DIR" "$VM_USER@$VM_HOST:$REMOTE_DIR"
```

Du kopierar hela publish-mappen rekursivt. Variablerna gör att samma skript fungerar mot dev-, staging- och prod-VM:er utan ändringar i koden — du byter bara miljövariabel.

**`restart_service`:**

```bash
ssh "$VM_USER@$VM_HOST" "sudo systemctl restart $SERVICE_NAME"
```

`ssh` kör ett enda kommando på distans och returnerar sedan. Inga interaktiva prompts, ingen terminal — precis vad automation kräver.

**Varför idempotent design spelar roll:**

Om `health_check` misslyckas och du kör skriptet igen startar det från början. `dotnet publish` skriver över gamla filer. `scp` skriver över gamla filer. `systemctl restart` startar om tjänsten oavsett om den redan körde. Varje steg kan köras hur många gånger som helst och ger samma slutresultat. Det är idempotens i praktiken.
