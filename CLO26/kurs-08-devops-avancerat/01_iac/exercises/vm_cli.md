# Övning — Provisionera MinVM med az CLI

🟡

Du klickade ihop MinVM i portalen förra övningen. Nu gör du exakt samma sak — men utan att röra musen. Allt via terminalen, allt repeterbart, allt automatiserbart.

Det är så proffs jobbar.

**15-minutersregeln:** Fastnar du längre än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

---

## Förutsättningar

- Azure CLI installerat och inloggat (`az login`)
- Terminal öppen (Git Bash på Windows, vanlig terminal på Mac/Linux)

Verifiera att CLI:t fungerar:

```bash
az --version
```

---

## DEL 1 — Resource Group

### Varför resource groups?

En resource group är en logisk behållare i Azure. Allt du skapar — VM, nätverkskort, IP-adress, diskar — hamnar i samma grupp. När du tar bort gruppen försvinner allt på en gång. Ingen glömda resurser, inga oväntade kostnader.

Tänk på det som en projektmapp: allt som hör ihop ligger på samma ställe.

### Skapa en resource group

```bash
az group create \
  --name MinVM-RG \
  --location northeurope
```

**Flaggor:**

| Flagga | Vad den gör |
|--------|-------------|
| `--name` | Namn på gruppen — välj något beskrivande |
| `--location` | Vilken Azure-region resurserna skapas i |

### Förväntad output

```json
{
  "id": "/subscriptions/<subscription-id>/resourceGroups/MinVM-RG",
  "location": "northeurope",
  "name": "MinVM-RG",
  "properties": {
    "provisioningState": "Succeeded"
  }
}
```

`"provisioningState": "Succeeded"` — det är kvittot. Gruppen finns.

### Lista dina resource groups

```bash
az group list --output table
```

`--output table` skriver ut resultatet som en läsbar tabell istället för råa JSON. Bra för att snabbt se vad du har.

Du ska se `MinVM-RG` i listan.

---

## DEL 2 — VM via CLI

### Sätt variabler

Börja med att sätta variablerna i terminalsessionen. Det gör kommandona kortare och enklare att ändra:

```bash
RG="MinVM-RG"
VM="MinVM"
USER="azureuser"
```

### Skapa VM:n

```bash
az vm create \
  --resource-group $RG \
  --name $VM \
  --image Ubuntu2204 \
  --size Standard_B1s \
  --admin-username $USER \
  --generate-ssh-keys
```

**Flaggor:**

| Flagga | Vad den gör |
|--------|-------------|
| `--resource-group` | Vilken grupp VM:n ska hamna i |
| `--name` | Namn på VM:n |
| `--image` | Operativsystem — Ubuntu 22.04 LTS |
| `--size` | VM-storlek — B1s är minsta möjliga, räcker för övningar |
| `--admin-username` | Användarnamn du loggar in med via SSH |
| `--generate-ssh-keys` | Skapar SSH-nyckelpar automatiskt om det inte finns något i `~/.ssh/` |

Det tar 1–2 minuter. Vänta tills terminalen svarar.

### Förväntad output

```json
{
  "fqdns": "",
  "id": "/subscriptions/.../resourceGroups/MinVM-RG/providers/Microsoft.Compute/virtualMachines/MinVM",
  "location": "northeurope",
  "macAddress": "...",
  "powerState": "VM running",
  "privateIpAddress": "10.0.0.4",
  "publicIpAddress": "20.100.XX.XX",
  "resourceGroup": "MinVM-RG",
  "zones": ""
}
```

Spara `publicIpAddress` — du behöver den strax.

### Öppna portarna

Azure blockerar all inkommande trafik som standard. Du måste explicit tillåta SSH och HTTP:

```bash
az vm open-port --resource-group $RG --name $VM --port 22
az vm open-port --resource-group $RG --name $VM --port 80
```

**Port 22** — SSH, så du kan logga in.  
**Port 80** — HTTP, så webbtrafik når VM:n.

Varje `az vm open-port` skapar en regel i Network Security Group (NSG) — Azures brandvägg för VM:n.

### Hämta IP-adressen

```bash
az vm list-ip-addresses \
  --resource-group $RG \
  --name $VM \
  --output table
```

Du ser både privat och publik IP. Den publika är den du SSH:ar till.

Eller lägg den direkt i en variabel:

```bash
IP=$(az vm list-ip-addresses \
  --resource-group $RG \
  --name $VM \
  --query "[0].virtualMachine.network.publicIpAddresses[0].ipAddress" \
  --output tsv)

echo "Din VM:s IP: $IP"
```

### SSH-uppkoppling

```bash
ssh $USER@$IP
```

Du är inne på VM:n. Kör ett snabbtest:

```bash
uname -a
```

Du ska se något i stil med `Linux MinVM ... x86_64 GNU/Linux`. Det bekräftar att du är inne och att det är rätt maskin.

Logga ut:

```bash
exit
```

---

## Komplett bash-script

Spara det här som `provision_minvm.sh` och kör det från din terminal. Det gör allt från noll till SSH-test.

```bash
#!/bin/bash
set -e  # Avbryt om något kommando misslyckas

# ─── Variabler ───────────────────────────────────────────────
RG="MinVM-RG"
VM="MinVM"
USER="azureuser"
LOCATION="northeurope"

echo "==> Skapar resource group: $RG"
az group create \
  --name $RG \
  --location $LOCATION \
  --output none

echo "==> Skapar VM: $VM (tar 1–2 min)"
az vm create \
  --resource-group $RG \
  --name $VM \
  --image Ubuntu2204 \
  --size Standard_B1s \
  --admin-username $USER \
  --generate-ssh-keys \
  --output none

echo "==> Öppnar port 22 (SSH)"
az vm open-port --resource-group $RG --name $VM --port 22 --output none

echo "==> Öppnar port 80 (HTTP)"
az vm open-port --resource-group $RG --name $VM --port 80 --output none

echo "==> Hämtar publik IP"
IP=$(az vm list-ip-addresses \
  --resource-group $RG \
  --name $VM \
  --query "[0].virtualMachine.network.publicIpAddresses[0].ipAddress" \
  --output tsv)

echo "==> VM är redo. IP: $IP"
echo ""
echo "Testa SSH-uppkopplingen:"
ssh -o StrictHostKeyChecking=no $USER@$IP "uname -a && echo 'SSH fungerar!'"
```

Kör scriptet:

```bash
chmod +x provision_minvm.sh
./provision_minvm.sh
```

### Förväntad output från scriptet

```
==> Skapar resource group: MinVM-RG
==> Skapar VM: MinVM (tar 1–2 min)
==> Öppnar port 22 (SSH)
==> Öppnar port 80 (HTTP)
==> Hämtar publik IP
==> VM är redo. IP: 20.100.XX.XX

Testa SSH-uppkopplingen:
Linux MinVM 6.X.X-azure #1 SMP ... x86_64 GNU/Linux
SSH fungerar!
```

---

## Städa upp

Azure kostar pengar varje sekund resurser körs. Ta bort allt när du är klar:

```bash
az group delete --name $RG --yes --no-wait
```

`--yes` hoppar över bekräftelsefrågan. `--no-wait` returnerar direkt utan att vänta tills gruppen är borta. Borttagningen sker i bakgrunden.

---

## Utmanande frågor

1. Vad är skillnaden på `--output table`, `--output json` och `--output tsv`? När är respektive format användbart?
2. `--generate-ssh-keys` skapar nycklar i `~/.ssh/id_rsa`. Vad händer om du kör scriptet igen när nycklar redan finns?
3. Varför är det bättre att spara IP-adressen i en variabel med `$()` istället för att kopiera den manuellt från terminalen?

---

## Lösningsförslag

### Fråga 1

`--output table` är lättläst för människor men svår att parsa i scripts. `--output json` är bra för felsökning och när du vill se hela svaret. `--output tsv` (tab-separated) är perfekt när du ska plocka ut ett värde till en variabel med `$()` — inget extra JSON-skräp, bara värdet.

### Fråga 2

Om `~/.ssh/id_rsa` redan finns skapar Azure CLI inga nya nycklar — det använder de befintliga. Du kan köra `--generate-ssh-keys` hur många gånger som helst utan att befintliga nycklar skrivs över.

### Fråga 3

Manuell kopiering är fel vid ett av tio tillfällen, och det kan vara svårt att märka. En variabel är deterministisk — samma IP varje gång, automatiskt, utan att du behöver tänka. Det är hela poängen med scripting: ta bort det mänskliga felet.
