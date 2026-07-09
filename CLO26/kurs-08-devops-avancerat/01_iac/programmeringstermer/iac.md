# 01 Iac — Programmeringstermer

## IaC
Infrastructure as Code. Hantera infrastruktur (VM, nätverk, databaser) med kod istället för manuell konfiguration.

## Bicep
Microsofts domänspecifika språk för att deploya Azure-resurser. Enklare än ARM JSON, kompileras till ARM.

## ARM Template
Azure Resource Manager-mall. JSON-fil som beskriver Azure-resurser och deras konfiguration.

## Terraform
HashiCorps IaC-verktyg. Multi-cloud (Azure, AWS, GCP). Använder HCL (HashiCorp Configuration Language).

## Declarativ vs Imperativ
Deklarativ: beskriv SLA önskade tillstånd (vad). Imperativ: beskriv stegen (hur). Bicep/Terraform är deklarativa.

## State File
Terraform-fil (.tfstate) som håller reda på verkligt infrastrukturtillstånd. KRITISK — förvara säkert.

## Modul
Återanvändbar IaC-komponent. Exempel: modul för virtuellt nätverk som används av flera projekt.

## Idempotens
Egenskap att samma konfiguration kan köras flera gånger med samma resultat. Centralt i IaC.

## Azure Blueprints
Mallar som definierar policy, roller och resurser för hela prenumerationer eller hanteringsgrupper.

## Policy as Code
Azure Policy definierad i kod. Säkerställer compliance: 'alla VM måste ha backup aktiverat'.

