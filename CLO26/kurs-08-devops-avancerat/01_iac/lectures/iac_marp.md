---
marp: true
theme: nion-dark
paginate: true
---

# Infrastructure as Code

**Kurs:** DevOps-avancerat
**Modul:** 01 — IaC med Terraform och Bicep
Marcus Ackre Medina · YRGO · CLO26

---

## Vad ska vi lära oss idag?

- **Varför IaC?** — Problemet med att klicka i portalen
- **Deklarativ vs Imperativ** — Vad kontra hur
- **Bicep** — param, var, module, output
- **Terraform** — provider, resource, output, state
- **Idempotens** — samma kod, samma resultat, alltid
- **State management** — Terraforms hemliga bok

---

## Problemet med manuell infrastruktur

Du loggade in i portalen. Du klickade runt i 40 minuter. Storage Account, App Service, Key Vault — klart.

Två veckor senare: *"Hur skapade du den servern exakt?"*

❌ Ingen vet  
❌ Ingen kan reproducera det  
❌ Staging-miljön är inte identisk med prod  
❌ En kollega skapar den "lite annorlunda"  

**Det är inte ett tekniskt problem. Det är ett reproducerbarhetsproblem.**

---

## IaC löser det

```bash
# Före IaC — manuellt, timmar
# Logga in → Portal → Klicka → Konfigurera → Klicka → Klicka...

# Med IaC — en rad
az deployment group create \
  --resource-group rg-myapp \
  --template-file main.bicep \
  --parameters env=prod appNamn=myapp
```

Tre minuter. Identisk miljö varje gång.  
Versionshantera infrastrukturen precis som du versionshantera kod.

---

## Deklarativ vs Imperativ

| Aspekt | Deklarativ | Imperativ |
|--------|------------|-----------|
| Beskriver | **Vad** (önskat tillstånd) | **Hur** (steg för steg) |
| Exempel | Bicep, Terraform | Bash, PowerShell |
| Idempotent | ✅ Ja | ❌ Nej (vanligtvis) |
| Drift-hantering | Ser avvikelser och korrigerar | Kör bara skriptet |

```bash
# Imperativt — kör bara om resursen saknas?
az storage account create --name mystorage --sku Standard_LRS

# Deklarativt — Bicep bestämmer om create/update/skip behövs
resource storage 'Microsoft.Storage/storageAccounts@2023-01-01' = { ... }
```

---

## Bicep — Anatomi

```bicep
// 1. PARAM — indata som kan variera per miljö
@description('Miljönamn — påverkar namngivning och SKU')
@allowed(['dev', 'staging', 'prod'])
param env string = 'dev'

// 2. VAR — beräknade värden, används internt
var plats = resourceGroup().location
var storageSkus = {
  dev: 'Standard_LRS'
  staging: 'Standard_GRS'
  prod: 'Premium_LRS'
}

// 3. RESOURCE — det som faktiskt skapas i Azure
resource storage 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: 'st${env}myapp'
  location: plats
  sku: { name: storageSkus[env] }
  kind: 'StorageV2'
}

// 4. OUTPUT — värden som exponeras efter deployment
output storageNamn string = storage.name
```

---

## Bicep — Moduler

Dela upp en stor template i återanvändbara delar:

```
miljofabriken/
├── main.bicep          — orkestrerar allt
├── main.bicepparam     — parameter-fil för dev
└── modules/
    ├── storage.bicep   — hanterar bara Storage Account
    └── appservice.bicep — hanterar bara App Service
```

```bicep
// main.bicep — anropa modulerna
module lagring 'modules/storage.bicep' = {
  name: 'storage-deployment'
  params: {
    plats: plats
    env: env
    sku: storageSkus[env]
  }
}

// Ta emot output från modulen
output storageUrl string = lagring.outputs.primaryEndpoint
```

---

## Bicep — What-if och Idempotens

```bash
# Kör always — se vad som skulle hända UTAN att faktiskt göra det
az deployment group what-if \
  --resource-group rg-myapp \
  --template-file main.bicep \
  --parameters env=dev

# Output:
#   ~ Modify: storageAccounts/stdevmyapp (sku changed)
#   = NoChange: sites/app-dev-myapp
#   + Create: vaults/kv-dev-myapp
```

**Idempotens:** Kör deploymenten två gånger med identiska parametrar.  
Vad händer? Ingenting. Azure ser att tillståndet redan matchar.

✅ Trygg att köra i CI/CD  
✅ Inga "dubbla resurser"  
✅ Drift-detection: om någon ändrar manuellt korrigeras det nästa deploy

---

## Terraform — Provider och Resource

Terraform är **multi-cloud**: samma verktyg för Azure, AWS, GCP.

```hcl
# 1. PROVIDER — vilket moln och autentisering
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0"
    }
  }
}

provider "azurerm" {
  features {}
}

# 2. RESOURCE — infrastruktur att skapa
resource "azurerm_resource_group" "main" {
  name     = "rg-${var.env}-myapp"
  location = var.location
}
```

---

## Terraform — Variables och Output

```hcl
# variables.tf — indata (motsvarar Bicep param)
variable "env" {
  description = "Miljönamn: dev, staging, prod"
  type        = string
  default     = "dev"

  validation {
    condition     = contains(["dev", "staging", "prod"], var.env)
    error_message = "env måste vara dev, staging eller prod."
  }
}

variable "location" {
  type    = string
  default = "swedencentral"
}

# outputs.tf — exponerade värden (motsvarar Bicep output)
output "resource_group_id" {
  value = azurerm_resource_group.main.id
}

output "storage_primary_endpoint" {
  value = azurerm_storage_account.main.primary_blob_endpoint
}
```

---

## Terraform — State (Kritisk!)

Terraform håller koll på vad som skapats i en **state-fil** (`.tfstate`).

```
terraform apply → skapar resurser → sparar i .tfstate
terraform plan  → jämför .tfstate med koden → visar diff
terraform apply → uppdaterar bara det som förändrats
```

```hcl
# Förvara state ALDRIG lokalt i ett team — remote backend!
terraform {
  backend "azurerm" {
    resource_group_name  = "rg-terraform-state"
    storage_account_name = "stterraformstate"
    container_name       = "tfstate"
    key                  = "prod.terraform.tfstate"
  }
}
```

❌ `.tfstate` i Git = lösenord exponerade + merge-konflikter  
✅ Remote state = team kan samarbeta utan kollisioner

---

## Terraform — State Locking

Vad händer om två pipelines kör `terraform apply` samtidigt?

```
Pipeline A: Läser state → börjar skapa resurser...
Pipeline B: Läser SAMMA state → börjar skapa resurser...
→ Konflikt, korrupt state, resurser i odefinierat tillstånd
```

**State Locking** förhindrar det:

```hcl
# Azure Storage stöder automatisk locking via Blob Lease
backend "azurerm" {
  # Lock aktiveras automatiskt — inget extra behövs
  use_azuread_auth = true
}
```

✅ Bara en `apply` åt gången  
✅ Om processen kraschar frigörs låset automatiskt efter timeout

---

## Bicep vs Terraform

| Aspekt | Bicep | Terraform |
|--------|-------|-----------|
| Moln | Bara Azure | Multi-cloud (Azure, AWS, GCP) |
| Ägare | Microsoft | HashiCorp |
| State | Hanteras av Azure | Extern .tfstate-fil |
| Syntax | `param`, `var`, `resource`, `output` | HCL — `variable`, `resource`, `output` |
| IDE-stöd | Utmärkt i VS Code | Bra med Terraform extension |
| Modulsystem | `module 'modules/x.bicep'` | `module "x" { source = "./modules/x" }` |

**Välj Bicep** om ni bara kör Azure.  
**Välj Terraform** om ni kör flera moln eller vill ha ett enhetligt verktyg.

---

## IaC i Pipeline

```yaml
# .github/workflows/infra.yml — Terraform i GitHub Actions
name: Deploy Infrastructure

on:
  push:
    branches: [main]
    paths: ['infra/**']

jobs:
  terraform:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      
      - name: Setup Terraform
        uses: hashicorp/setup-terraform@v3
      
      - name: Terraform Init
        run: terraform init
        working-directory: ./infra
      
      - name: Terraform Plan
        run: terraform plan -out=tfplan
        working-directory: ./infra
      
      - name: Terraform Apply
        run: terraform apply tfplan
        working-directory: ./infra
```

---

## Vanliga misstag

```
❌ Hårdkodade värden i templates
   resource 'Microsoft.Storage/storageAccounts@2023-01-01' = {
     name: 'stprodmarcus'  // ← aldrig!
   }

✅ Parametrar styr allt
   name: 'st${env}${appNamn}'

❌ .tfstate committad till Git
   # .gitignore saknar:
   *.tfstate
   *.tfstate.backup

✅ Remote backend alltid
   backend "azurerm" { ... }

❌ Ingen what-if / plan innan apply i prod
✅ CI/CD: plan på PR → apply efter godkänd merge
```

---

## Prova själv

Skapa en Bicep-template som deployar:

1. **Storage Account** med en blob-container `bilder`
2. **App Service Plan + App Service** (gratis F1-tier)
3. **Key Vault**

Krav:
- Parametrar: `env` (dev/staging/prod), `appNamn`
- SKU:er styrs av `env` via variabelmap
- Uppdelad i minst två moduler
- Kör `what-if` innan deployment

```bash
az deployment group create \
  --resource-group rg-[dittnamn] \
  --template-file main.bicep \
  --parameters env=dev appNamn=[dittnamn]
```

---

## Sammanfattning

- ✅ IaC = infrastruktur som kod — versionshanterat, reproducerbart
- ✅ Deklarativt = beskriv önskat tillstånd, verktyget bestämmer hur
- ✅ Bicep: `param` → `var` → `resource` → `output` → `module`
- ✅ Terraform: `variable` → `resource` → `output`, state i remote backend
- ✅ Idempotens: kör tio gånger — exakt samma resultat
- ✅ State = Terraforms hjärna — förvara den säkert, låst, i molnet
- ➡️ Nästa: CI/CD Avancerat — deploya med strategy och rollback
