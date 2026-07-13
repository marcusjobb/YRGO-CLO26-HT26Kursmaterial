# Övning — Nginx med Bicep och cloud-init

🟡

---

## Vad du ska göra

Du ska deploya en Ubuntu VM i Azure med Bicep och installera Nginx på den — på två olika sätt. I slutet har du en webbserver som visar en HTML-sida med ditt eget namn.

Målet är inte att förstå varje rad Bicep utantill. Målet är att du ska förstå *varför* du väljer cloud-init framför en Custom Script Extension, och tvärtom.

**Scenario:** Du heter "MinServer". Du deployar din första webbserver i molnet. Ingen komplex app — bara Nginx som svarar med en enkel HTML-sida.

**Tid:** 45–60 minuter

---

## Förutsättningar

- Azure CLI inloggad (`az login`)
- Bicep installerat (`az bicep install`)
- En resource group att deploya till (skapa en om du inte har: `az group create --name rg-minserver --location swedencentral`)
- Grundläggande koll på vad en VM är (från tidigare övningar)

---

## Del 1 — Cloud-init vid skapande

Cloud-init är ett verktyg som körs **en gång** när VM:en startar för allra första gången. Du skickar med ett bash-script vid deployningen — VM:en kör det automatiskt under uppstarten. Du behöver aldrig logga in manuellt för att installera Nginx.

### Fil 1: `install_nginx.sh`

Skapa en fil som heter `install_nginx.sh` i samma mapp som din Bicep-fil:

```bash
#!/bin/bash
set -e

apt-get update -y
apt-get install -y nginx

# Skapa en enkel startsida med ditt namn
cat > /var/www/html/index.html <<'EOF'
<!DOCTYPE html>
<html lang="sv">
<head>
  <meta charset="UTF-8">
  <title>MinServer</title>
</head>
<body>
  <h1>Hej från MinServer!</h1>
  <p>Den här sidan körs på min första Azure VM.</p>
  <p>Deployad med Bicep och cloud-init.</p>
</body>
</html>
EOF

systemctl enable nginx
systemctl start nginx
```

`set -e` gör att scriptet avbryts direkt om ett kommando misslyckas — bra vana i automationsscript.

---

### Fil 2: `main.bicep`

Skapa `main.bicep` i samma mapp:

```bicep
@description('Prefix som används för alla resurser')
param prefix string = 'minserver'

@description('Azure-region för resurserna')
param location string = resourceGroup().location

@description('Användarnamn till VM:en')
param adminUsername string = 'azureuser'

@description('SSH public key för inloggning')
@secure()
param adminPublicKey string

// Variabler för namngivning
var vmName = '${prefix}-vm'
var nicName = '${prefix}-nic'
var vnetName = '${prefix}-vnet'
var subnetName = 'default'
var nsgName = '${prefix}-nsg'
var publicIpName = '${prefix}-pip'

// Network Security Group — tillåt HTTP och SSH
resource nsg 'Microsoft.Network/networkSecurityGroups@2023-09-01' = {
  name: nsgName
  location: location
  properties: {
    securityRules: [
      {
        name: 'allow-ssh'
        properties: {
          priority: 100
          protocol: 'Tcp'
          access: 'Allow'
          direction: 'Inbound'
          sourceAddressPrefix: '*'
          sourcePortRange: '*'
          destinationAddressPrefix: '*'
          destinationPortRange: '22'
        }
      }
      {
        name: 'allow-http'
        properties: {
          priority: 110
          protocol: 'Tcp'
          access: 'Allow'
          direction: 'Inbound'
          sourceAddressPrefix: '*'
          sourcePortRange: '*'
          destinationAddressPrefix: '*'
          destinationPortRange: '80'
        }
      }
    ]
  }
}

// Virtuellt nätverk
resource vnet 'Microsoft.Network/virtualNetworks@2023-09-01' = {
  name: vnetName
  location: location
  properties: {
    addressSpace: {
      addressPrefixes: ['10.0.0.0/16']
    }
    subnets: [
      {
        name: subnetName
        properties: {
          addressPrefix: '10.0.1.0/24'
          networkSecurityGroup: {
            id: nsg.id
          }
        }
      }
    ]
  }
}

// Publik IP-adress
resource publicIp 'Microsoft.Network/publicIPAddresses@2023-09-01' = {
  name: publicIpName
  location: location
  sku: {
    name: 'Standard'
  }
  properties: {
    publicIPAllocationMethod: 'Static'
  }
}

// Nätverksgränssnitt
resource nic 'Microsoft.Network/networkInterfaces@2023-09-01' = {
  name: nicName
  location: location
  properties: {
    ipConfigurations: [
      {
        name: 'ipconfig1'
        properties: {
          subnet: {
            id: vnet.properties.subnets[0].id
          }
          publicIPAddress: {
            id: publicIp.id
          }
        }
      }
    ]
  }
}

// Virtuell maskin med cloud-init via customData
resource vm 'Microsoft.Compute/virtualMachines@2023-09-01' = {
  name: vmName
  location: location
  properties: {
    hardwareProfile: {
      vmSize: 'Standard_B1s'
    }
    storageProfile: {
      imageReference: {
        publisher: 'Canonical'
        offer: '0001-com-ubuntu-server-jammy'
        sku: '22_04-lts-gen2'
        version: 'latest'
      }
      osDisk: {
        createOption: 'FromImage'
        managedDisk: {
          storageAccountType: 'Standard_LRS'
        }
      }
    }
    osProfile: {
      computerName: vmName
      adminUsername: adminUsername
      // cloud-init: ladda in bash-scriptet och koda det i base64
      customData: base64(loadTextContent('install_nginx.sh'))
      linuxConfiguration: {
        disablePasswordAuthentication: true
        ssh: {
          publicKeys: [
            {
              path: '/home/${adminUsername}/.ssh/authorized_keys'
              keyData: adminPublicKey
            }
          ]
        }
      }
    }
    networkProfile: {
      networkInterfaces: [
        {
          id: nic.id
        }
      ]
    }
  }
}

// Output: IP-adressen du kan öppna i webbläsaren
output publicIpAddress string = publicIp.properties.ipAddress
output vmName string = vm.name
```

`loadTextContent('install_nginx.sh')` läser in bash-filen och `base64()` kodar den — cloud-init på Azure kräver base64-kodad text i `customData`.

---

### Deploya Del 1

Kör detta i terminalen (ersätt SSH-nyckeln med din faktiska publika nyckel):

```bash
az deployment group create \
  --resource-group rg-minserver \
  --template-file main.bicep \
  --parameters adminPublicKey="$(cat ~/.ssh/id_rsa.pub)"
```

När deployningen är klar ser du IP-adressen i output. Vänta ca 2 minuter — cloud-init körs i bakgrunden efter att VM:en startat. Öppna sedan `http://<ip-adress>` i webbläsaren.

Du bör se din HTML-sida.

---

## Del 2 — Custom Script Extension efteråt

VM:en är deployad och Nginx kör. Nu vill du uppdatera HTML-sidan utan att skapa en ny VM. Det är här Custom Script Extension (CSE) kommer in.

CSE är en Azure VM-extension som kör ett script på en **befintlig** VM. Du kan lägga till den i Bicep och köra om deployningen — Azure lägger till extensionen utan att röra resten av VM:en.

### Fil 3: `update_page.sh`

Skapa `update_page.sh` i samma mapp:

```bash
#!/bin/bash
set -e

cat > /var/www/html/index.html <<'EOF'
<!DOCTYPE html>
<html lang="sv">
<head>
  <meta charset="UTF-8">
  <title>MinServer — uppdaterad</title>
</head>
<body>
  <h1>Uppdaterad av Custom Script Extension</h1>
  <p>Den här sidan byttes ut via CSE — utan att VM:en skapades om.</p>
  <p>Deployad: $(date '+%Y-%m-%d %H:%M')</p>
</body>
</html>
EOF

systemctl restart nginx
```

---

### Lägg till CSE i `main.bicep`

Lägg till följande resurs **efter** VM-resursen i `main.bicep`. Den är beroende av `vm`, därav `dependsOn`:

```bicep
// Custom Script Extension — kör update_page.sh på befintlig VM
resource vmExtension 'Microsoft.Compute/virtualMachines/extensions@2023-09-01' = {
  name: 'customScript'
  parent: vm
  location: location
  properties: {
    publisher: 'Microsoft.Azure.Extensions'
    type: 'CustomScript'
    typeHandlerVersion: '2.1'
    autoUpgradeMinorVersion: true
    protectedSettings: {
      // Scriptet skickas direkt som base64-kodad sträng
      script: base64(loadTextContent('update_page.sh'))
    }
  }
}
```

`protectedSettings` krypteras av Azure innan de skickas till VM:en — använd det för känsliga värden eller script du inte vill ha i klartext i deployment-loggar.

---

### Kör om deployningen

```bash
az deployment group create \
  --resource-group rg-minserver \
  --template-file main.bicep \
  --parameters adminPublicKey="$(cat ~/.ssh/id_rsa.pub)"
```

Azure ser att VM:en redan finns och lämnar den ifred — men lägger till extensionen och kör `update_page.sh`. Ladda om `http://<ip-adress>` i webbläsaren. Du bör se den uppdaterade sidan.

---

## Jämförelse: när använder du vad?

| | Cloud-init | Custom Script Extension |
|---|---|---|
| **Körs** | En gång vid VM-start | När du deployar extensionen |
| **Passar för** | Initial setup — Nginx, paket, användare | Uppdateringar på befintlig VM |
| **Kräver** | Ny VM (eller att VM:en inte startats ännu) | Befintlig, körande VM |
| **Felsökning** | `/var/log/cloud-init-output.log` | `/var/log/azure/custom-script/handler.log` |
| **Kan köras igen** | Nej — körs bara en gång | Ja — varje gång du deployar om |

**Tumregel:** cloud-init för allt som ska finnas från start. CSE för allt du vill förändra eller lägga till efteråt.

---

## Kontrollpunkter

Innan du avslutar, kontrollera att du kan svara på det här:

- [ ] Öppna `http://<ip-adress>` i webbläsaren och se HTML-sidan från cloud-init.
- [ ] Köra om deployningen med CSE och se att sidan uppdateras utan att VM:en tas bort.
- [ ] Förklara varför `customData` kräver base64-kodning.
- [ ] Hitta felloggarna för cloud-init respektive CSE på VM:en via SSH.
- [ ] Säga med egna ord: när väljer du cloud-init, när väljer du CSE?

---

**15-minutersregeln:** Om du kört fast i mer än 15 minuter på samma punkt — ta en paus, läs felmeddelandet högt för dig själv, och fråga sedan en kurskamrat eller läraren. Fastna inte i tystnad.
