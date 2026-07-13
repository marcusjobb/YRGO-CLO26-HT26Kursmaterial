# Övning: VM-hantering med PowerShell 🟡

**Tid:** 15 minuter  
**Scenario:** Din kollega på HyresBil-projektet jobbar på Windows. Bash-skriptet från förra övningen fungerar inte för hen — PowerShell är standardspråket på Windows och det är dit Azure-automatisering på Microsoft-stacken naturligt hör hemma. Din uppgift är att skriva ett PowerShell-skript som gör samma jobb: lista VMs, starta en specifik VM, vänta tills den körs och sedan ansluta via fjärrkommando.

---

## Förutsättningar

- PowerShell 7+ installerat (`pwsh --version` ska visa 7.x)
- Du är inloggad på ett Azure-konto med rättigheter att hantera virtuella maskiner
- En VM finns i din resursgrupp (t.ex. `hyresbil-vm`)

**Saknar du PowerShell 7?**

```powershell
# Windows (via winget)
winget install Microsoft.PowerShell

# macOS
brew install --cask powershell

# Ubuntu/Debian
sudo snap install powershell --classic
```

## Steg 1 — Installera Az-modulen

Az-modulen är det officiella PowerShell-biblioteket för Azure. Den installeras en gång per maskin.

```powershell
Install-Module Az -Scope CurrentUser -Force -AllowClobber
```

`-Scope CurrentUser` — installeras bara för dig, kräver inga administratörsrättigheter.  
`-Force` — hoppar över bekräftelsefrågor.  
`-AllowClobber` — tillåter att modulen skriver över äldre kommandon med samma namn om du har gamla versioner installerade.

Installationen tar 1–3 minuter. Verifiera efteråt:

```powershell
Get-Module Az -ListAvailable | Select-Object Name, Version | Select-Object -First 3
```

Du ska se `Az` med en version under 12.x eller högre.

> **En gång räcker.** Du behöver inte köra `Install-Module` varje gång du öppnar PowerShell — modulen finns kvar. Däremot behöver du ladda in den per session med `Import-Module Az`, eller så sker det automatiskt när du kallar på ett `*-Az*`-kommando.

## Steg 2 — Logga in på Azure

```powershell
Connect-AzAccount
```

En webbläsare öppnas. Logga in med ditt Azure-konto. När du är klar visar terminalen vilket konto och vilken prenumeration som är aktiv.

**Om du har flera prenumerationer** — lista dem och välj rätt:

```powershell
Get-AzSubscription | Select-Object Name, Id, State
Set-AzContext -SubscriptionId "din-prenumerations-id"
```

Kontrollera att rätt prenumeration är aktiv:

```powershell
Get-AzContext | Select-Object Name, Account, Subscription
```

## Steg 3 — Lista alla VMs

```powershell
Get-AzVM -Status | Select-Object Name, ResourceGroupName, PowerState
```

`Get-AzVM -Status` — hämtar alla virtuella maskiner i din aktiva prenumeration, inklusive deras aktuella driftstatus.  
`Select-Object` — väljer ut de kolumner du vill se. Utan det får du ett långt objekt med all information.

### Förväntad output

```
Name          ResourceGroupName    PowerState
----          -----------------    ----------
hyresbil-vm   hyresbil-rg          VM deallocated
```

`VM deallocated` betyder att VM:en är stoppad och inte kostar datorkraft — men IP-adress och disk kostar fortfarande.

## Steg 4 — Starta en specifik VM

```powershell
Start-AzVM -ResourceGroupName "hyresbil-rg" -Name "hyresbil-vm"
```

Kommandot returnerar inte förrän VM:en har startat. Det tar normalt 1–2 minuter.

### Förväntad output

```
OperationId : 7a3c1f45-...
Status      : Succeeded
StartTime   : 2026-07-13 09:14:22 +00:00
EndTime     : 2026-07-13 09:15:47 +00:00
```

`Status: Succeeded` bekräftar att Azure lyckades starta VM:en. Verifiera med `Get-AzVM -Status` igen — `PowerState` ska nu vara `VM running`.

## Steg 5 — Det kompletta skriptet

Nu sätter du ihop allt i ett skript. Skriptet listar VMs, startar en specifik VM, väntar tills den faktiskt körs och kör sedan ett fjärrkommando via Azure — utan att du behöver SSH-nyckel i terminalen.

Spara filen som `_scripts/manage-vm.ps1`:

```powershell
#Requires -Modules Az.Accounts, Az.Compute

<#
.SYNOPSIS
    Listar, startar och ansluter till HyresBil-VM via Azure PowerShell.

.DESCRIPTION
    1. Listar alla VMs och deras driftstatus
    2. Startar en specifik VM om den är stoppad
    3. Väntar tills VM:en är i läget "VM running"
    4. Kör ett fjärrkommando via az vm run-command

.PARAMETER ResourceGroup
    Resursgruppens namn. Standard: hyresbil-rg

.PARAMETER VmName
    VM:ens namn. Standard: hyresbil-vm

.EXAMPLE
    .\manage-vm.ps1
    .\manage-vm.ps1 -ResourceGroup "annan-rg" -VmName "annan-vm"
#>

param(
    [string]$ResourceGroup = "hyresbil-rg",
    [string]$VmName        = "hyresbil-vm"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

# ── Hjälpfunktioner ──────────────────────────────────────────────────────────

function Write-Log {
    param([string]$Message, [string]$Level = "INFO")
    $timestamp = Get-Date -Format "HH:mm:ss"
    Write-Host "[$timestamp] [$Level] $Message"
}

function Get-VmPowerState {
    param([string]$RG, [string]$Name)
    $vm = Get-AzVM -ResourceGroupName $RG -Name $Name -Status
    return ($vm.Statuses | Where-Object Code -like "PowerState/*").DisplayStatus
}

# ── Steg 1: Lista alla VMs ───────────────────────────────────────────────────

Write-Log "Hämtar alla VMs i prenumerationen..."

try {
    $allVMs = Get-AzVM -Status | Select-Object Name, ResourceGroupName, PowerState
    $allVMs | Format-Table -AutoSize
}
catch {
    Write-Log "Kunde inte hämta VM-lista: $_" -Level "FEL"
    exit 1
}

# ── Steg 2: Starta VM om den inte redan körs ─────────────────────────────────

Write-Log "Kontrollerar status för '$VmName' i '$ResourceGroup'..."

try {
    $currentState = Get-VmPowerState -RG $ResourceGroup -Name $VmName
    Write-Log "Aktuell status: $currentState"

    if ($currentState -ne "VM running") {
        Write-Log "Startar '$VmName'..."
        Start-AzVM -ResourceGroupName $ResourceGroup -Name $VmName | Out-Null
        Write-Log "Start-kommando skickat."
    }
    else {
        Write-Log "'$VmName' körs redan — hoppar över start."
    }
}
catch {
    Write-Log "Fel vid start av VM: $_" -Level "FEL"
    exit 1
}

# ── Steg 3: Vänta tills VM:en är Running ─────────────────────────────────────

Write-Log "Väntar tills '$VmName' är i läget 'VM running'..."

$maxVäntetid  = 120   # sekunder
$kontrollInterval = 5 # sekunder
$förfluten    = 0

try {
    do {
        Start-Sleep -Seconds $kontrollInterval
        $förfluten += $kontrollInterval
        $state = Get-VmPowerState -RG $ResourceGroup -Name $VmName
        Write-Log "Status efter ${förfluten}s: $state"

        if ($förfluten -ge $maxVäntetid) {
            Write-Log "Timeout — VM kom inte upp inom $maxVäntetid sekunder." -Level "FEL"
            exit 1
        }
    } while ($state -ne "VM running")

    Write-Log "VM är uppe och körs."
}
catch {
    Write-Log "Fel under väntan: $_" -Level "FEL"
    exit 1
}

# ── Steg 4: Kör fjärrkommando via az vm run-command ──────────────────────────
# az vm run-command körs via Azure-plattformen, inte direkt SSH.
# Det fungerar även om port 22 är stängd — bra för snabb diagnostik.

Write-Log "Kör diagnostikkommando på '$VmName' via run-command..."

try {
    $resultat = az vm run-command invoke `
        --resource-group $ResourceGroup `
        --name $VmName `
        --command-id RunShellScript `
        --scripts "hostname && uptime && echo 'HyresBil API: OK'" `
        --output json | ConvertFrom-Json

    $output = $resultat.value[0].message
    Write-Log "Svar från VM:"
    Write-Host $output
}
catch {
    Write-Log "run-command misslyckades: $_" -Level "FEL"
    exit 1
}

# ── Sammanfattning ────────────────────────────────────────────────────────────

Write-Host ""
Write-Host "════════════════════════════════════════"
Write-Host "  VM-HANTERING KLAR"
Write-Host "  VM:  $VmName"
Write-Host "  RG:  $ResourceGroup"
Write-Host "  Tid: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
Write-Host "════════════════════════════════════════"
```

Kör skriptet:

```powershell
.\manage-vm.ps1
```

Med egna parametrar:

```powershell
.\manage-vm.ps1 -ResourceGroup "min-rg" -VmName "min-vm"
```

### Förväntad output (utdrag)

```
[09:14:01] [INFO] Hämtar alla VMs i prenumerationen...

Name          ResourceGroupName    PowerState
----          -----------------    ----------
hyresbil-vm   hyresbil-rg          VM deallocated

[09:14:03] [INFO] Kontrollerar status för 'hyresbil-vm' i 'hyresbil-rg'...
[09:14:04] [INFO] Aktuell status: VM deallocated
[09:14:04] [INFO] Startar 'hyresbil-vm'...
[09:14:06] [INFO] Start-kommando skickat.
[09:14:06] [INFO] Väntar tills 'hyresbil-vm' är i läget 'VM running'...
[09:14:11] [INFO] Status efter 5s: VM starting
[09:14:16] [INFO] Status efter 10s: VM starting
[09:14:21] [INFO] Status efter 15s: VM running
[09:14:21] [INFO] VM är uppe och körs.
[09:14:21] [INFO] Kör diagnostikkommando på 'hyresbil-vm' via run-command...
[09:14:30] [INFO] Svar från VM:
hyresbil-vm
 09:14:29 up 0 min,  0 users,  load average: 0.52, 0.13, 0.04
HyresBil API: OK

════════════════════════════════════════
  VM-HANTERING KLAR
  VM:  hyresbil-vm
  RG:  hyresbil-rg
  Tid: 2026-07-13 09:14:30
════════════════════════════════════════
```

## Bash vs PowerShell — när använder du vad?

Du har nu gjort samma arbete i två olika skriptspråk. Här är den praktiska skillnaden:

| Aspekt | Bash | PowerShell |
|--------|------|------------|
| Plattform | Linux, macOS, WSL | Windows, Linux, macOS |
| Jobbar med | Text-strängar | .NET-objekt |
| Azure-verktyg | `az` CLI | `Az`-modulen + `az` CLI |
| Pipe | Text vidare till nästa kommando | Objekt vidare — egenskaper finns kvar |
| Error handling | `set -euo pipefail` + exitkoder | `try/catch` + `$ErrorActionPreference` |
| Typiskt hemma i | GitHub Actions på Linux-runners | Azure DevOps, Windows-servrar |
| Läsbarhet | Kompakt, men kan bli kryptiskt | Utförligare, mer explicit |

**Konkret skillnad:**

```bash
# Bash — text-pipe
az vm list --query "[].{Name:name}" -o tsv | grep "hyresbil"
```

```powershell
# PowerShell — objekt-pipe
Get-AzVM | Where-Object { $_.Name -like "*hyresbil*" } | Select-Object Name
```

Bash filtrerar text med `grep`. PowerShell filtrerar objekt med `Where-Object` — och `$_.Name` är en riktigt egenskap, inte en strängsökning.

**Tumregel:** Jobbar teamet på Linux och deployas koden till Linux-VM:er? Bash. Jobbar de på Windows eller i Azure DevOps-pipelines med Microsoft-stack? PowerShell. I ett blandat team — lär dig båda.

## Utmanande frågor

1. Skriptet använder `$ErrorActionPreference = "Stop"` istället för `Set-StrictMode`. Vad är skillnaden — och vad händer om du tar bort `$ErrorActionPreference`-raden och en Azure-funktion returnerar ett fel?

2. `az vm run-command` ansluter via Azure-plattformen, inte direkt SSH. Vad är fördelen med det jämfört med `ssh`? I vilka situationer är det sämre?

3. Skriptet har en `do...while`-loop med en maxväntetid på 120 sekunder. Om VM:en tar 180 sekunder att starta — vad händer? Hur skulle du göra den mer robust utan att öka maxväntetiden blint?

---

## Lösningsförslag

**Fråga 1 — `$ErrorActionPreference`:**

`$ErrorActionPreference = "Stop"` gör att alla PowerShell-fel (inklusive cmdlet-fel) kastar ett terminating exception som din `try/catch` fångar. Utan den raden kan icke-terminating errors passera och `catch`-blocket körs aldrig — skriptet fortsätter som om ingenting hänt. `Set-StrictMode` handlar om variabelanvändning (t.ex. att använda odefinierade variabler), inte om felhantering.

**Fråga 2 — `run-command` vs SSH:**

Fördel: `run-command` kräver ingen SSH-port öppen och ingen nätverksanslutning direkt till VM:en — allt går via Azure-plattformen. Det fungerar direkt efter att en VM startat, utan att vänta på att SSH-daemonen är uppe. Nackdel: det är långsammare (Azure-plattformen mellanlagrar kommandot), det fungerar bara med kommandon som passar i en enkel sträng, och du kan inte köra interaktiva processer.

**Fråga 3 — Timeout-robusthet:**

VM:en fastnar i loopen och skriptet skriver `Timeout — VM kom inte upp inom 120 sekunder` och avslutar med `exit 1`. En mer robust lösning är att lägga timeoutvärdet som parameter, fånga `Azure-tillståndet` `VM starting` vs `VM stopping` (en stoppande VM startar aldrig), och ge ett tydligare felmeddelande beroende på vilket tillstånd VM:en fastnade i.

---

**15-minutersregeln:** Kom ihåg att stänga av VM:en när övningen är klar. En startad VM kostar per timme.

```powershell
Stop-AzVM -ResourceGroupName "hyresbil-rg" -Name "hyresbil-vm" -Force
```

`-Force` hoppar över bekräftelsedialogen. VM:en avslutas men resurserna (disk, IP) finns kvar till nästa start.
