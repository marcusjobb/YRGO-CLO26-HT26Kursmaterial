# Automation med Bash och PowerShell — Fördjupning

## När använda Bash vs PowerShell?

| Aspekt | Bash | PowerShell |
|--------|------|------------|
| Plattform | Linux, macOS, WSL | Windows, Linux, macOS |
| Typ | Text-baserad | Object-baserad |
| Azure-stöd | Azure CLI (az) | Azure PowerShell (Az module) |
| CI/CD | GitHub Actions, GitLab | Azure DevOps, GitHub Actions |
| Filhantering | `ls`, `grep`, `awk`, `sed` | `Get-ChildItem`, `Select-String` |
| Web | `curl`, `wget` | `Invoke-RestMethod`, `Invoke-WebRequest` |

## Bash — Viktiga Mönster

```bash
#!/bin/bash
set -euo pipefail  # Stoppa vid fel, odefinierade variabler, pipe-fel

# Variabler
NAME="Marcus"
echo "Hej, $NAME!"

# Loop
for FILE in *.txt; do
  echo "Bearbetar $FILE"
done

# Funktion
deploy() {
  echo "Deployar $1 till $2"
  az webapp deploy --name "$1" --resource-group "$2"
}

# Fånga output
RESULT=$(az group list --query "[].name" -o tsv)
echo "Resursgrupper: $RESULT"
```

## PowerShell — Viktiga Mönster

```powershell
# Variabler
$name = "Marcus"
Write-Host "Hej, $name!"

# Loop
Get-ChildItem *.txt | ForEach-Object {
  Write-Host "Bearbetar $($_.Name)"
}

# Funktion
function Deploy-App {
  param($Name, $ResourceGroup)
  az webapp deploy --name $Name --resource-group $ResourceGroup
}

# Fånga output med Azure PowerShell
$groups = Get-AzResourceGroup | Select-Object -ExpandProperty ResourceGroupName
```

## Azure CLI vs Azure PowerShell

**Azure CLI (az):**
- Cross-platform, Python-baserad
- JMESPath för query: `--query "[?location=='westeurope'].name"`
- Output-format: json, tsv, table, yaml

```bash
az vm list --query "[?powerState=='VM running'].{Name:name, Size:hardwareProfile.vmSize}" -o table
```

**Azure PowerShell (Az module):**
- PowerShell-modul, .NET-baserad
- Pipeline med objekt
- Mer "PowerShell-naturligt"

```powershell
Get-AzVM | Where-Object PowerState -eq 'VM running' | Select-Object Name, VmSize
```

## Automation Accounts

Azure Automation kör schemalagda PowerShell/Python-runbooks:

1. Skapa Automation Account
2. Importera moduler (Az module)
3. Skapa Runbook (PowerShell eller Python)
4. Skapa schema (varje natt, varje timme)
5. Lägg till webhook för extern trigger

Användningsområden: nattlig backup, start/stoppa VM, loggrensning, rapportgenerering.
