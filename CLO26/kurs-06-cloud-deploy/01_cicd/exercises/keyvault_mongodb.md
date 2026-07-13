# Övning — Azure Key Vault med MongoDB

🟡 Mellannivå

---

## Bakgrunden

HyresBil-appen är redo att deployas till Azure. Men det finns ett problem: MongoDB-connection stringen innehåller användarnamn och lösenord — och den får absolut inte ligga i en `.env`-fil i repot.

Varför inte? Git-historiken är permanent. Även om du raderar filen senare finns hemligheten kvar i alla tidigare commits. Vem som helst som klonar repot ser den. Är det ett öppet repo — eller om repot läcker — är din databas exponerad. Det är inte en hypotetisk risk. Det händer hela tiden, och det leder till dataintrång, GDPR-brott och en väldigt jobbig fredageftermiddag.

Lösningen är Azure Key Vault: ett hanterat valv där hemligheter lagras krypterat och accesskontrollerat. VM:en hämtar connection stringen vid uppstart — via sin identitet, utan lösenord, utan att strängen någonsin finns i kod eller fil.

---

## Vad gäller för den här övningen

- [ ] Som utbildare vill jag att du kan skapa ett Azure Key Vault och lägga in en secret via CLI
- [ ] Som utbildare vill jag att du förstår varför Managed Identity är bättre än API-nycklar eller lösenord
- [ ] Som utbildare vill jag att du kan ge en VM-identitet läsrättighet till Key Vault via RBAC
- [ ] Som utbildare vill jag att du kan hämta en secret från Key Vault på en körande VM
- [ ] Som utbildare vill jag att du förstår hur .NET-appen tar emot värdet som en miljövariabel via startup-script

Dessa punkter är vad vi tittar på — inte om dina resurser heter exakt samma sak som i exemplet.

---

## Uppgift

### Steg 1 — Skapa Key Vault

Börja med att skapa resursgruppen och Key Vault. Välj ett unikt suffix för valvnamnet — Key Vault-namn måste vara globalt unika i hela Azure.

```bash
# Skapa resursgrupp om den inte redan finns
az group create \
  --name hyresbil-rg \
  --location northeurope

# Skapa Key Vault med RBAC-auktorisering
az keyvault create \
  --name hyresbil-kv-<dina-initialer> \
  --resource-group hyresbil-rg \
  --location northeurope \
  --enable-rbac-authorization true
```

`--enable-rbac-authorization true` är ett krav. Utan det används access policies istället för RBAC, och rolltilldelningarna i nästa steg har ingen effekt.

---

### Steg 2 — Ge dig själv administratörsrättigheter

För att kunna lägga in secrets behöver du rollen **Key Vault Administrator** på valvet.

```bash
# Hämta ditt eget användar-ID
USER_ID=$(az ad signed-in-user show --query id --output tsv)

# Hämta Key Vault:ets resurs-ID
VAULT_ID=$(az keyvault show \
  --name hyresbil-kv-<dina-initialer> \
  --resource-group hyresbil-rg \
  --query id \
  --output tsv)

# Tilldela rollen
az role assignment create \
  --assignee "$USER_ID" \
  --role "Key Vault Administrator" \
  --scope "$VAULT_ID"
```

RBAC-tilldelningar propagerar inte omedelbart. Vänta ungefär 2 minuter innan du fortsätter.

---

### Steg 3 — Lägg in connection stringen som en secret

```bash
az keyvault secret set \
  --vault-name hyresbil-kv-<dina-initialer> \
  --name "MongoConnectionString" \
  --value "mongodb+srv://hyresbil:dittlosenord@cluster.mongodb.net/hyresbil"
```

Verifiera att den finns:

```bash
az keyvault secret show \
  --vault-name hyresbil-kv-<dina-initialer> \
  --name "MongoConnectionString" \
  --query value \
  --output tsv
```

Du ska se connection stringen i terminalen. Den finns nu i ett krypterat valv — inte i din kod, inte i din `.env`, inte i Git.

---

### Steg 4 — Aktivera Managed Identity på VM:en

Istället för att ge VM:en ett lösenord eller en API-nyckel för att komma åt Key Vault, ger du den en **Managed Identity** — en identitet som Azure hanterar automatiskt. Inga credentials att lagra, inga hemligheter att rotera manuellt, ingenting att råka committa.

```bash
az vm identity assign \
  --resource-group hyresbil-rg \
  --name HyresBilVM
```

Hämta VM:ens identitets-ID:

```bash
VM_PRINCIPAL_ID=$(az vm identity show \
  --resource-group hyresbil-rg \
  --name HyresBilVM \
  --query principalId \
  --output tsv)

echo "VM Principal ID: $VM_PRINCIPAL_ID"
```

---

### Steg 5 — Ge VM-identiteten läsrättighet till Key Vault

VM:en behöver bara läsa secrets — inte skriva eller administrera. Tilldela rollen **Key Vault Secrets User** — det är minsta möjliga behörighet för att uppgiften ska fungera.

```bash
az role assignment create \
  --assignee "$VM_PRINCIPAL_ID" \
  --role "Key Vault Secrets User" \
  --scope "$VAULT_ID"
```

Vänta igen 2 minuter. Samma sak som innan — RBAC propagerar inte direkt.

---

### Steg 6 — Hämta secreten från VM:en

SSH in på VM:en och kontrollera att den kan hämta secreten via sin Managed Identity:

```bash
ssh azureuser@<VM-IP>
```

Inne på VM:en:

```bash
az keyvault secret show \
  --vault-name hyresbil-kv-<dina-initialer> \
  --name "MongoConnectionString" \
  --query value \
  --output tsv
```

Du ska se connection stringen utan att ha loggat in med `az login`. VM:en autentiserar automatiskt via sin identitet mot Azure Instance Metadata Service. Det är det Managed Identity gör i praktiken.

---

### Steg 7 — Startup-script som injekterar miljövariabeln

Skapa filen `/opt/hyresbil/start.sh` på VM:en. Skriptet hämtar secreten vid varje uppstart och skickar in den som en miljövariabel till appen:

```bash
#!/bin/bash

# Hämta connection string från Key Vault
MONGO_CS=$(az keyvault secret show \
  --vault-name hyresbil-kv-<dina-initialer> \
  --name "MongoConnectionString" \
  --query value \
  --output tsv)

# Starta appen med connection stringen som miljövariabel
export MONGO_CONNECTION_STRING="$MONGO_CS"
exec dotnet /opt/hyresbil/HyresBil.dll
```

Gör skriptet körbart:

```bash
chmod +x /opt/hyresbil/start.sh
```

Uppdatera systemd-tjänsten (`/etc/systemd/system/hyresbil.service`) så att den kör startup-scriptet i stället för att anropa `dotnet` direkt:

```ini
[Unit]
Description=HyresBil ASP.NET App

[Service]
WorkingDirectory=/opt/hyresbil
ExecStart=/opt/hyresbil/start.sh
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=hyresbil
User=www-data

[Install]
WantedBy=multi-user.target
```

Ladda om och starta om tjänsten:

```bash
sudo systemctl daemon-reload
sudo systemctl restart hyresbil
```

---

### Steg 8 — .NET-appen läser miljövariabeln

I din `Program.cs` läser appen connection stringen precis som vilken miljövariabel som helst. Den behöver inte veta att värdet kom från Key Vault — det är en infrastrukturdetalj som startup-scriptet hanterar.

```csharp
var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING")
    ?? throw new InvalidOperationException(
        "MONGO_CONNECTION_STRING saknas. Kontrollera att startup-scriptet körs korrekt.");

builder.Services.AddSingleton<IMongoClient>(
    new MongoClient(connectionString));
```

Kastar du ett undantag vid saknad variabel fångar du felet tidigt — appen kraschar vid uppstart med ett tydligt felmeddelande i stället för att gå igång och sedan misslyckas med varje databasanrop.

---

## Exempeloutput

När du kör `az keyvault secret show` på VM:en ska du se connection stringen:

```
mongodb+srv://hyresbil:dittlosenord@cluster.mongodb.net/hyresbil
```

När appen startar via systemd ska `journalctl` visa att den startade utan fel:

```bash
sudo journalctl -u hyresbil -n 20
```

```
Jul 13 09:14:01 HyresBilVM hyresbil[1234]: info: Microsoft.Hosting.Lifetime[14]
Jul 13 09:14:01 HyresBilVM hyresbil[1234]:       Now listening on: http://[::]:5000
Jul 13 09:14:02 HyresBilVM hyresbil[1234]:       Application started. Press Ctrl+C to shut down.
Jul 13 09:14:02 HyresBilVM hyresbil[1234]:       Hosting environment: Production
```

## Tips

> Key Vault-namn måste vara globalt unika i Azure. Om `hyresbil-kv` är taget — lägg till dina initialer eller ett nummer: `hyresbil-kv-am1`.

> Om du får `403 Forbidden` direkt efter `az role assignment create` — vänta 2 minuter och försök igen. RBAC är inte synkront.

> Azure CLI på VM:en autentiserar automatiskt via Managed Identity. Du ska inte köra `az login` inne på VM:en.

> Startup-skriptet körs varje gång tjänsten startar. Roterar du connection stringen i Key Vault räcker det att köra `sudo systemctl restart hyresbil` — ingen redeploy behövs.

> ⏱️ **15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, en AI eller mig. I den ordningen.

---

*Facit finns hos läraren.*
