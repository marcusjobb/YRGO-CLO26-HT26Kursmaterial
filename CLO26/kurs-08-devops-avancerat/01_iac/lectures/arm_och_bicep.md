# ARM-templates och Bicep

Infrastructure as Code handlar om att beskriva sin infrastruktur i kod istället för att klicka runt i portalen. ARM-templates är Azures inbyggda sätt att göra det. Bicep är ett bättre sätt att göra samma sak.

---

## Vad är en ARM-template?

ARM står för **Azure Resource Manager** — den motor som hanterar alla resurser i Azure. Oavsett om du klickar i portalen, kör `az`-kommandon eller deployar via CI/CD, så är det ARM som faktiskt utför arbetet.

En ARM-template är en **JSON-fil** som beskriver vilka resurser du vill ha och hur de ska se ut. Deklarativt betyder att du beskriver *slutresultatet*, inte steg-för-steg-instruktioner. Azure räknar ut vägen dit.

```json
{
  "type": "Microsoft.Storage/storageAccounts",
  "apiVersion": "2023-01-01",
  "name": "mystorageaccount",
  "location": "swedencentral",
  "sku": { "name": "Standard_LRS" },
  "kind": "StorageV2"
}
```

Det funkar. Men en riktig ARM-template för en VM med nätverk, NSG och public IP? Det kan bli 400 rader JSON. Svårläst, svårt att felsöka, svårt att återanvända.

---

## Varför Bicep?

Bicep är ett eget litet språk som är designat för exakt det här problemet. Det kompileras till ARM-JSON, så under huven händer exakt samma sak — men du slipper skriva JSON för hand.

Samma storage account i Bicep:

```bicep
resource storageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: 'mystorageaccount'
  location: 'swedencentral'
  sku: { name: 'Standard_LRS' }
  kind: 'StorageV2'
}
```

Renare. Färre klammerparenteser. Och Bicep-linter fångar misstag redan innan du deployar.

---

## Grundläggande Bicep-syntax

Bicep har fyra nyckelbegrepp som du behöver känna till.

**param** — indata till din template. Gör templaten återanvändbar för olika miljöer.

```bicep
param location string = 'swedencentral'
param storageAccountName string
```

**var** — variabler för att undvika upprepning.

```bicep
var skuName = 'Standard_LRS'
```

**resource** — den faktiska Azure-resursen du vill skapa.

```bicep
resource storageAccount 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: storageAccountName
  location: location
  sku: { name: skuName }
  kind: 'StorageV2'
}
```

**output** — värden du vill exportera efter deploy, exempelvis en URL eller ett resurs-ID.

```bicep
output storageId string = storageAccount.id
```

---

## Kompilera Bicep till ARM

Du behöver inte alltid kompilera manuellt — Azure CLI kan deploya `.bicep`-filer direkt. Men det är bra att veta hur det fungerar.

```bash
az bicep build --file main.bicep
```

Det här skapar en `main.json`-fil med den genererade ARM-JSON:en. Användbart om du vill granska vad som faktiskt skickas till Azure, eller om du arbetar i ett system som bara accepterar JSON.

---

## what-if: testa innan du deployar

Det här är ett av de mest användbara kommandona du kommer lära dig. `what-if` låter dig se exakt vilka ändringar en deploy skulle göra — utan att faktiskt göra dem.

```bash
az deployment group what-if \
  --resource-group min-rg \
  --template-file main.bicep
```

Azure svarar med en lista: vad som skapas, ändras eller tas bort. Grön plus = nytt. Gul tilde = ändring. Röd minus = borttagning. Kör alltid `what-if` i produktion innan du trycker på deploy.

---

## Modules — återanvändbara delar

När en Bicep-fil börjar växa delar man upp den i **modules**. En modul är en separat `.bicep`-fil som du anropar från din huvudfil.

```bicep
module storage 'modules/storage.bicep' = {
  name: 'storageDeployment'
  params: {
    storageAccountName: 'mystorageaccount'
    location: location
  }
}
```

Det gör det möjligt att bygga upp ett bibliotek av återanvändbara delar — en modul för nätverk, en för VM, en för databas. Samma modul används i dev, test och produktion, med olika parametervärden.

---

## Sammanfattning

ARM är Azures motor. Bicep är språket du pratar med den motorn. Du skriver Bicep, Azure kompilerar till ARM, och ARM skapar dina resurser. Med `param` styr du indata, med `var` undviker du upprepning, med `output` exporterar du resultat, och med `what-if` kollar du vad som händer innan det händer.

Det är IaC i praktiken: versionerat, testbart, återanvändbart.
