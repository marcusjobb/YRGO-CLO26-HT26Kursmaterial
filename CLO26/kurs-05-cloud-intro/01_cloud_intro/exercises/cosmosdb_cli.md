# Övning — CosmosDB via CLI

🟡

## Vad du ska göra

Du har redan skapat Jobbloggen i portalen. Det gick bra — men tänk om du behöver göra om det.

Kanske raderar du resursen av misstag. Kanske ska en kollega sätta upp sin egna miljö. Kanske vill du ha en test-miljö och en produktions-miljö. Att klicka sig igenom portalen varje gång är inte ett alternativ.

Det är där CLI:et kommer in. Du skriver kommandon en gång. Sedan kan du köra dem igen imorgon och få exakt samma resultat.

I den här övningen bygger du ett bash-script som sätter upp hela Jobbloggens infrastruktur på en gång. Resurser, databas och samling — allt i ett svep.

## Förutsättningar

- Du är inloggad på ett Azure-konto med rättigheter att skapa resurser
- Azure CLI är installerat (`az --version` ska visa en version)
- Du har en terminal öppen (bash, zsh eller Azure Cloud Shell fungerar)
- Du har gjort portal-övningen och vet vad CosmosDB är

## Steg

### 1. Logga in på Azure

```bash
az login
```

En webbläsare öppnas. Logga in med ditt Azure-konto. Terminalen väntar tills du är klar och visar sedan dina prenumerationer.

**Om du har flera prenumerationer** — lista dem och välj rätt:

```bash
az account list --output table
az account set --subscription "Namn på din prenumeration"
```

Kontrollera att rätt prenumeration är aktiv:

```bash
az account show --query "{name:name, id:id}" --output table
```

### 2. Skapa en resursgrupp

En resursgrupp är en behållare. Allt som hör ihop hamnar i samma grupp — det gör det enkelt att se vad som hör ihop och att städa upp.

```bash
az group create \
  --name jobbloggen-rg \
  --location northeurope
```

`--name` — vad gruppen heter. Välj ett namn som visar vad den innehåller.  
`--location` — vilken Azure-region resurserna hamnar i. `northeurope` är nära Sverige.

Kontrollera att den skapades:

```bash
az group show --name jobbloggen-rg --query "{namn:name, plats:location}" --output table
```

### 3. Skapa CosmosDB-kontot

Det här är huvudresursen. Allt annat lever inuti kontot.

```bash
az cosmosdb create \
  --name jobbloggen-cosmos-DITTNAMN \
  --resource-group jobbloggen-rg \
  --kind MongoDB \
  --server-version "4.2" \
  --capabilities EnableServerless \
  --locations regionName=northeurope failoverPriority=0 isZoneRedundant=false
```

Byt ut `DITTNAMN` mot ditt eget namn eller dina initialer. Kontonamnet måste vara unikt globalt — ingen annan i hela Azure får ha samma namn.

`--kind MongoDB` — väljer MongoDB API. Det går inte att ändra efteråt.  
`--capabilities EnableServerless` — serverless innebär att du bara betalar när du faktiskt använder databasen. Ingenting körs i bakgrunden och kostar pengar.  
`--server-version "4.2"` — vilken version av MongoDB-protokollet kontot talar.

Kommandot tar 3–8 minuter. Det är normalt. Azure provisionerar infrastruktur under huven.

Kontrollera efteråt:

```bash
az cosmosdb show \
  --name jobbloggen-cosmos-DITTNAMN \
  --resource-group jobbloggen-rg \
  --query "{namn:name, api:kind, kapacitet:capabilities[0].name}" \
  --output table
```

Du ska se `MongoDB` under `api` och `EnableServerless` under `kapacitet`.

### 4. Skapa databasen

Databasen är ett logiskt lager inuti kontot. Den heter `jobbloggen_db`.

```bash
az cosmosdb mongodb database create \
  --account-name jobbloggen-cosmos-DITTNAMN \
  --resource-group jobbloggen-rg \
  --name jobbloggen_db
```

`--account-name` — kopieras exakt från steget ovan. Stavfel ger felmeddelande.  
`--name` — vad databasen ska heta.

Verifiera att den finns:

```bash
az cosmosdb mongodb database list \
  --account-name jobbloggen-cosmos-DITTNAMN \
  --resource-group jobbloggen-rg \
  --query "[].name" \
  --output tsv
```

Du ska se `jobbloggen_db` i svaret.

### 5. Skapa samlingen

Samlingen (`ansokningar`) är där dokumenten lagras. Varje ansökan i Jobbloggen är ett dokument.

```bash
az cosmosdb mongodb collection create \
  --account-name jobbloggen-cosmos-DITTNAMN \
  --resource-group jobbloggen-rg \
  --database-name jobbloggen_db \
  --name ansokningar \
  --shard "status"
```

`--name ansokningar` — samlingens namn.  
`--shard "status"` — shard key bestämmer hur CosmosDB fördelar data internt. Vi väljer `status` eftersom ansökningar har olika statusar: `skickad`, `intervju`, `avslag`, `erbjudande`. Notera att CLI-kommandot inte har något `/` framför — det läggs till automatiskt.

Verifiera:

```bash
az cosmosdb mongodb collection list \
  --account-name jobbloggen-cosmos-DITTNAMN \
  --resource-group jobbloggen-rg \
  --database-name jobbloggen_db \
  --query "[].name" \
  --output tsv
```

Du ska se `ansokningar`.

### 6. Hämta connection string

Din app behöver en connection string för att prata med databasen.

```bash
az cosmosdb keys list \
  --name jobbloggen-cosmos-DITTNAMN \
  --resource-group jobbloggen-rg \
  --type connection-strings \
  --query "connectionStrings[0].connectionString" \
  --output tsv
```

`--type connection-strings` — hämtar hela anslutningssträngen i MongoDB-format, inte bara nyckeln.  
`--output tsv` — ger ren text utan JSON-citattecken. Det är viktigt om du ska använda värdet i en variabel.

Spara den i en miljövariabel:

```bash
export COSMOS_CONNECTION_STRING=$(az cosmosdb keys list \
  --name jobbloggen-cosmos-DITTNAMN \
  --resource-group jobbloggen-rg \
  --type connection-strings \
  --query "connectionStrings[0].connectionString" \
  --output tsv)
```

Kontrollera att den sattes korrekt (visa bara de 30 första tecknen — strängen innehåller din åtkomstnyckel):

```bash
echo "Börjar med: ${COSMOS_CONNECTION_STRING:0:30}..."
```

Du ska se något som börjar med `mongodb://`.

> **Säkerhet:** Connection string innehåller din åtkomstnyckel. Lägg den aldrig i en fil som hamnar i Git. Använd miljövariabler eller Azure Key Vault i produktion.

## Det kompletta scriptet

Nu sätter du ihop allt till ett enda script. Det här är poängen med övningen — du behöver aldrig klicka i portalen igen för att sätta upp den här infrastrukturen.

Skapa filen `provision-jobbloggen.sh` och lägg in följande innehåll:

```bash
#!/bin/bash
set -euo pipefail

# ============================================================
# provision-jobbloggen.sh
# Sätter upp hela CosmosDB-infrastrukturen för Jobbloggen.
# Kör: bash provision-jobbloggen.sh
# ============================================================

# Konfiguration — ändra ACCOUNT_NAME till ditt eget namn
RESOURCE_GROUP="jobbloggen-rg"
LOCATION="northeurope"
ACCOUNT_NAME="jobbloggen-cosmos-DITTNAMN"
DATABASE_NAME="jobbloggen_db"
COLLECTION_NAME="ansokningar"
SHARD_KEY="status"

echo "=== Jobbloggen — CosmosDB-provisionering ==="
echo ""

# Steg 1: Resursgrupp
echo "Skapar resursgrupp '$RESOURCE_GROUP' i '$LOCATION'..."
az group create \
  --name "$RESOURCE_GROUP" \
  --location "$LOCATION" \
  --output none

echo "Resursgrupp klar."
echo ""

# Steg 2: CosmosDB-konto (tar några minuter)
echo "Skapar CosmosDB-konto '$ACCOUNT_NAME'..."
echo "Det här tar 3–8 minuter. Vänta..."
az cosmosdb create \
  --name "$ACCOUNT_NAME" \
  --resource-group "$RESOURCE_GROUP" \
  --kind MongoDB \
  --server-version "4.2" \
  --capabilities EnableServerless \
  --locations regionName="$LOCATION" failoverPriority=0 isZoneRedundant=false \
  --output none

echo "CosmosDB-konto klart."
echo ""

# Steg 3: Databas
echo "Skapar databas '$DATABASE_NAME'..."
az cosmosdb mongodb database create \
  --account-name "$ACCOUNT_NAME" \
  --resource-group "$RESOURCE_GROUP" \
  --name "$DATABASE_NAME" \
  --output none

echo "Databas klar."
echo ""

# Steg 4: Samling
echo "Skapar samling '$COLLECTION_NAME' med shard key '$SHARD_KEY'..."
az cosmosdb mongodb collection create \
  --account-name "$ACCOUNT_NAME" \
  --resource-group "$RESOURCE_GROUP" \
  --database-name "$DATABASE_NAME" \
  --name "$COLLECTION_NAME" \
  --shard "$SHARD_KEY" \
  --output none

echo "Samling klar."
echo ""

# Steg 5: Connection string
echo "Hämtar connection string..."
CONNECTION_STRING=$(az cosmosdb keys list \
  --name "$ACCOUNT_NAME" \
  --resource-group "$RESOURCE_GROUP" \
  --type connection-strings \
  --query "connectionStrings[0].connectionString" \
  --output tsv)

echo ""
echo "=== Klar! ==="
echo "Konto:    $ACCOUNT_NAME"
echo "Databas:  $DATABASE_NAME"
echo "Samling:  $COLLECTION_NAME"
echo "Shard:    /$SHARD_KEY"
echo ""
echo "Connection string (spara den säkert — innehåller din nyckel):"
echo "$CONNECTION_STRING"
```

Gör scriptet körbart och kör det:

```bash
chmod +x provision-jobbloggen.sh
./provision-jobbloggen.sh
```

Om något steg misslyckas stoppar `set -euo pipefail` resten automatiskt. Du ser felmeddelandet och vet exakt var det gick fel.

**Det är detta som gör CLI bättre än portalen.** Portalen ger dig en bekväm vy — men varje klick är en manuell handling du gör en gång. Scriptet gör samma sak varje gång, på samma sätt, utan att du kan missa ett steg. Det kan versioneras i Git, delas med kollegor och köras i en CI/CD-pipeline.

## Kontrollpunkter

Markera varje punkt när du är klar:

- [ ] `az login` lyckades — terminalen visar dina prenumerationer
- [ ] Resursgruppen `jobbloggen-rg` finns i `northeurope`
- [ ] CosmosDB-kontot visas med `kind: MongoDB` och `EnableServerless`
- [ ] Databasen `jobbloggen_db` finns i kontot
- [ ] Samlingen `ansokningar` finns med shard key `/status`
- [ ] Connection string börjar med `mongodb://` och innehåller `.mongo.cosmos.azure.com`
- [ ] Scriptet kör utan felmeddelanden och skriver ut connection string

**Felsökning — vanliga problem:**

`"Account name already exists"` — Kontonamnet är globalt unikt. Lägg till ett nummer eller dina initialer.

`"The subscription is not registered to use namespace 'Microsoft.DocumentDB'"` — Kör `az provider register --namespace Microsoft.DocumentDB` och vänta ett par minuter.

`"AuthorizationFailed"` — Ditt konto saknar behörighet. Du behöver minst Contributor-rollen på prenumerationen.

Kommandot hänger i mer än 10 minuter — gå till portalen, öppna din resursgrupp och klicka på "Deployments" för att se status.

---

**15-minutersregeln:** När du är klar med övningen och inte längre behöver resurserna — ta bort dem. En CosmosDB-konto i serverless-läge kostar ingenting i viloläge, men resursgruppen är ändå bra att städa bort för att hålla ordning.

```bash
az group delete --name jobbloggen-rg --yes --no-wait
```

`--yes` hoppar över bekräftelsefrågans. `--no-wait` returnerar terminalen direkt — Azure fortsätter radera i bakgrunden.
