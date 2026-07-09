---
marp: true
theme: default
class: invert
paginate: true
---

# Virtuella Servrar i Azure

**Kurs:** Cloud Intro
**Modul:** 02 — Virtuell Server

---

## Vad ska vi lära oss idag?

- **Virtuella maskiner** — vad är en VM och varför använda den?
- **Skapa en VM i Azure** — portal, CLI, IaC
- **SSH** — anslut till Linux-VM
- **RDP** — anslut till Windows-VM
- **Nätverk** — NSG, publika/privata IP
- **Kostnader** — optimera din VM-faktura

---

## Vad är en Virtuell Maskin?

En VM emulerar en fysisk dator i programvara.

```
┌──────────────────────────────────┐
│         Hypervisor               │
│  (Azure: hypervisor från MS)     │
├────────────┬─────────────────────┤
│   VM 1     │     VM 2            │
│  Ubuntu    │    Windows          │
│  2 vCPU    │    4 vCPU           │
│  8 GB RAM  │   16 GB RAM         │
│  30 GB SSD │  128 GB SSD         │
└────────────┴─────────────────────┘
```

**IaaS** — du ansvarar för OS och applikationer.

---

## När välja VM vs PaaS?

| Scenario | Välj VM | Välj PaaS (App Service) |
|----------|---------|------------------------|
| Full kontroll över OS | ✅ | ❌ |
| Anpassad programvara | ✅ | ❌ |
| Legacy-applikation | ✅ | ❌ |
| Egen databas | ✅ | ❌ |
| Snabb deployment | ❌ | ✅ |
| Auto scaling | ❌ | ✅ |
| OS-uppdateringar | Du ansvarar | Hanterat |
| Kostnad | Högre (24/7) | Lägre (pay-per-use) |

---

## Skapa en VM — Azure Portal

1. Azure Portal → Virtual Machines → Create
2. **Resource Group:** skapa ny eller välj befintlig
3. **VM Name:** `min-vm`
4. **Region:** West Europe
5. **Image:** Ubuntu 24.04 LTS eller Windows Server 2022
6. **Size:** Standard_B2s (2 vCPU, 4 GB RAM)
7. **Authentication:** SSH public key (Linux) eller Password
8. **Inbound ports:** SSH(22) eller RDP(3389)
9. **Review + Create**

---

## SSH-nycklar — Säker autentisering

```bash
# Generera SSH-nyckelpar (om du inte har ett)
ssh-keygen -t rsa -b 4096 -f ~/.ssh/azure_key

# Anslut till din VM
ssh -i ~/.ssh/azure_key azureuser@51.144.12.34

# Första gången: acceptera fingerprint
# Du är nu inloggad på din Ubuntu-server!
```

- **PEM-fil:** din privata nyckel (.pem). Förvara säkert!
- **Public key:** läggs på VM:n vid skapande
- **Lösenord:** mindre säkert, men enklare för Windows (RDP)

---

## Network Security Group (NSG)

NSG-styr trafik till/från din VM:

```bash
# Tillåt trafik från din IP på port 80 (HTTP)
az network nsg rule create \
    --resource-group myRG \
    --nsg-name myVM-NSG \
    --name AllowHTTP \
    --priority 100 \
    --source-address-prefixes $(curl -s ifconfig.me)/32 \
    --destination-port-ranges 80 \
    --access Allow
```

**Regel:** Allow på specifik port från specifik källa.
**Default:** All inkommande trafik blockeras.

---

## Installera programvara

```bash
# Uppdatera paket
sudo apt update && sudo apt upgrade -y

# Installera Nginx (webbserver)
sudo apt install nginx -y

# Kontrollera att den körs
systemctl status nginx

# Öppna i webbläsaren: http://din-ip
```

---

## Auto-shutdown — Spara pengar!

En VM som körs 24/7 kostar mycket. Stäng av den när du inte använder den:

```bash
# Aktivera auto-shutdown via Azure CLI
az vm auto-shutdown \
    --resource-group myRG \
    --name myVM \
    --time 18:00 \
    --email "min@epost.se"
```

**Tips:**
- Utvecklings-VM: stäng av 18-08 och helger
- Deallokera (stoppa) istället för att bara stänga av — betala då bara för lagring
- Använd B-serien (burstable) för låg belastning

---

## Sammanfattning

- ✅ VM = full kontroll, eget OS och programvara
- ✅ IaaS = du ansvarar för allt ovan hypervisorn
- ✅ SSH för Linux, RDP för Windows
- ✅ NSG-styr nätverkstrafik
- ✅ Auto-shutdown sparar pengar
- ➡️ Nästa: Containrar med Docker — nästa steg i virtualisering

---
