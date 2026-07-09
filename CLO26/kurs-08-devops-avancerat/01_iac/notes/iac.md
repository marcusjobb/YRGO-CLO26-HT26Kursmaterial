# Infrastructure as Code — Fördjupning

## Varför IaC?

Traditionellt: logga in på server, installera program, konfigurera manuellt. Detta är:
- **Tidskrävande** — timmar för en server
- **Felbenäget** — lätt att missa ett steg
- **Ospårbart** — ingen vet exakt vad som ändrats

IaC löser detta genom att infrastruktur definieras i filer som versionshanteras med Git.

## Deklarativ vs Imperativ

| Aspekt | Deklarativ | Imperativ |
|--------|------------|-----------|
| Beskriver | **Vad** (önskat tillstånd) | **Hur** (steg för steg) |
| Exempel | Bicep, Terraform | Bash, PowerShell |
| Idempotent | Ja (samma resultat varje gång) | Nej (kör stegen oavsett) |
| Drift | Upptäcker och korrigerar avvikelser | Kör bara skriptet |

## Bicep vs Terraform

| Aspekt | Bicep | Terraform |
|--------|-------|-----------|
| Ägare | Microsoft | HashiCorp |
| Plattform | Bara Azure | Multi-cloud (Azure, AWS, GCP) |
| Syntax | Enklare än ARM, C#-liknande | HCL (HashiCorp Language) |
| State | State lagras i Azure (valfritt) | State-fil (.tfstate) — KRITISK |
| Moduler | Ja, enkla | Ja, väletablerade |

## Bicep — Grunderna

```bicep
param location string = resourceGroup().location
param adminPassword string

resource vm 'Microsoft.Compute/virtualMachines@2023-03-01' = {
  name: 'myVM'
  location: location
  properties: {
    hardwareProfile: {
      vmSize: 'Standard_B2s'
    }
    osProfile: {
      computerName: 'myVM'
      adminUsername: 'azureuser'
      adminPassword: adminPassword
    }
  }
}
```

## Terraform — Grunderna

```hcl
provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "main" {
  name     = "myResources"
  location = "West Europe"
}

resource "azurerm_virtual_network" "main" {
  name                = "myNetwork"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  address_space       = ["10.0.0.0/16"]
}
```

## Terraform State

State-filen är **kritisk** — utan den vet Terraform inte vad som skapats.

- Förvara ALDRIG .tfstate i Git
- Använd remote state (Azure Storage, AWS S3)
- Lås statet (prevent concurrent modifications)
- Team: alla använder samma remote state

## Idempotens

IaC ska vara idempotent — samma kod ska ge samma resultat oavsett hur många gånger den körs. Om någon manuellt ändrat en resurs ska IaC återställa den till önskat tillstånd vid nästa körning.
