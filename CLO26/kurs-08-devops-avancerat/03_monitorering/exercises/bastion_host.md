# Övning — Azure Bastion: nå din VM utan att öppna internet

🟡 Mellannivå

---

## Bakgrunden

Du har satt upp en VM som heter **MinVM**. Den kör en intern tjänst och ska aldrig vara direkt nåbar från internet — inga publika IP-adresser, inga öppna portar.

Men du behöver fortfarande kunna logga in och felsöka. Vad gör du?

Det naiva svaret: "Jag öppnar port 22 och SSH:ar in." Det är också det svar som får säkerhetsteamet att rycka ihop.

---

## Threat model: vad händer om port 22 är öppen mot internet?

Prova det mentala experimentet: du sätter en publik IP på din VM och öppnar port 22 mot `0.0.0.0/0`. Inom minuter börjar automatiserade botar från hela världen knacka på dörren. De provar svaga lösenord, kända exploits och vanliga SSH-nycklar som läckt i tidigare dataintrång. Det är inte paranoia — det är verklig trafik som du kan se i Azure Monitor om du lämnar porten öppen.

SSH mot internet kräver att du har:
- Perfekt nyckelhantering, utan undantag
- Uppdaterad SSH-daemon, alltid
- Brandväggsregler som aldrig glömts att stängas
- Noll misstag från alla i teamet, för alltid

Azure Bastion tar bort den ekvationen. Ingen publik IP på VM:en. Ingen öppen port 22 mot internet. Åtkomsten går via Azures kontrollplan — autentiserad med ditt Azure AD-konto, loggad, och stängd när du stänger webbläsarfliken.

---

## Vad gäller för den här övningen

- [ ] Som utbildare vill jag att du förstår varför en VM utan publik IP är säkrare
- [ ] Som utbildare vill jag att du kan skapa ett VNet med rätt subnätkonfiguration för Bastion
- [ ] Som utbildare vill jag att du kan skapa en VM som saknar publik IP-adress
- [ ] Som utbildare vill jag att du kan provisionera en Azure Bastion-resurs och koppla den till rätt subnät
- [ ] Som utbildare vill jag att du kan öppna en SSH-session via Bastion i Azure-portalen

---

## Uppgift

### Steg 1 — Skapa resursgrupp

Börja med att samla allt i en resursgrupp. Byt ut `<ditt-suffix>` mot dina initialer eller liknande — resurser som Bastion kräver unika namn inom din prenumeration.

```bash
az group create \
  --name rg-bastion-<ditt-suffix> \
  --location swedencentral
```

---

### Steg 2 — Skapa VNet med två subnät

Azure Bastion kräver ett subnät som heter exakt `AzureBastionSubnet`. Det är inte valfritt — heter det något annat fungerar inte Bastion. Subnätet måste också ha ett tillräckligt stort adressutrymme (minst `/26`).

```bash
az network vnet create \
  --resource-group rg-bastion-<ditt-suffix> \
  --name vnet-bastion-demo \
  --address-prefix 10.0.0.0/16 \
  --subnet-name default \
  --subnet-prefix 10.0.1.0/24
```

Skapa sedan Bastion-subnätet separat:

```bash
az network vnet subnet create \
  --resource-group rg-bastion-<ditt-suffix> \
  --vnet-name vnet-bastion-demo \
  --name AzureBastionSubnet \
  --address-prefix 10.0.2.0/27
```

Kontrollera att båda subnäten syns:

```bash
az network vnet subnet list \
  --resource-group rg-bastion-<ditt-suffix> \
  --vnet-name vnet-bastion-demo \
  --output table
```

### Förväntad output (steg 2)

```
Name                 AddressPrefix    ProvisioningState
-------------------  ---------------  -------------------
default              10.0.1.0/24      Succeeded
AzureBastionSubnet   10.0.2.0/27      Succeeded
```

---

### Steg 3 — Skapa VM utan publik IP

Notera `--public-ip-address ""` — det är det som skiljer den här VM:en från en exponerad server. Ingen publik IP skapas.

```bash
az vm create \
  --resource-group rg-bastion-<ditt-suffix> \
  --name MinVM \
  --image Ubuntu2204 \
  --vnet-name vnet-bastion-demo \
  --subnet default \
  --public-ip-address "" \
  --admin-username azureuser \
  --generate-ssh-keys \
  --size Standard_B1s
```

Verifiera att ingen publik IP tilldelats:

```bash
az vm show \
  --resource-group rg-bastion-<ditt-suffix> \
  --name MinVM \
  --show-details \
  --query "publicIps" \
  --output tsv
```

### Förväntad output (steg 3)

```
(tom rad — ingen publik IP finns)
```

---

### Steg 4 — Skapa publik IP för Bastion och provisionera Bastion-resursen

Bastion-resursen självt behöver en publik IP — det är dit du ansluter från din webbläsare. Men den IP:n är kopplad till Bastion, inte till VM:en.

```bash
az network public-ip create \
  --resource-group rg-bastion-<ditt-suffix> \
  --name pip-bastion \
  --sku Standard \
  --allocation-method Static
```

Skapa sedan Bastion:

```bash
az network bastion create \
  --resource-group rg-bastion-<ditt-suffix> \
  --name bastion-demo \
  --vnet-name vnet-bastion-demo \
  --public-ip-address pip-bastion \
  --location swedencentral
```

> Bastion tar 5–10 minuter att provisionera. Det är normalt.

Kontrollera status:

```bash
az network bastion show \
  --resource-group rg-bastion-<ditt-suffix> \
  --name bastion-demo \
  --query "provisioningState" \
  --output tsv
```

### Förväntad output (steg 4)

```
Succeeded
```

---

### Steg 5 — Koppla upp via Bastion i portalen

1. Gå till [portal.azure.com](https://portal.azure.com)
2. Sök upp **MinVM** under Virtual machines
3. Klicka **Connect** i toppmenyn → välj **Bastion**
4. Fyll i användarnamn (`azureuser`) och välj SSH Private Key
5. Klicka **Connect** — en SSH-session öppnas direkt i webbläsaren

Du ska nu se en terminal i webbläsarfönstret och vara inloggad på VM:en — utan att port 22 är öppen mot internet och utan en publik IP på servern.

Kör ett enkelt kommando för att bekräfta att du är inne:

```bash
hostname && ip a | grep "inet "
```

### Förväntad output (steg 5)

```
MinVM
    inet 127.0.0.1/8 scope host lo
    inet 10.0.1.X/24 brd 10.0.1.255 scope global eth0
```

IP-adressen ska börja på `10.0.1.` — det är det privata subnätet. Ingen publik adress syns.

---

### Steg 6 — Reflektera

Svara skriftligt (några meningar räcker) på frågorna nedan. Du kan skriva i chatten med läraren eller i en kommentar i portalen:

1. Vad är skillnaden i attackyta mellan en VM med publik IP och en utan?
2. Var sker autentiseringen när du använder Bastion — på VM:en eller i Azure?
3. Om en kollega frågar "varför inte bara öppna port 22, det är ju lättare?" — vad svarar du?

---

### Steg 7 — Städa upp

```bash
az group delete \
  --name rg-bastion-<ditt-suffix> \
  --yes \
  --no-wait
```

Bastion-resursen kostar pengar per timme — låt den inte stå och ticka.

---

## Tips

> Subnätnamnet `AzureBastionSubnet` är inte en rekommendation utan ett krav. Azure returnerar ett fel om du döper det till något annat, t.ex. `bastion-subnet`.

> Bastion Basic-SKU:n räcker för den här övningen. Om du ser valet Bastion Standard — välj Basic, det är billigare och ger allt du behöver här.

> Om `az network bastion create` misslyckas med ett felmeddelande om subnätstorlek: kontrollera att `AzureBastionSubnet` har prefix `/27` eller större (dvs. `/27` eller `/26`, inte `/28`).

> ⏱️ **15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, en AI eller läraren. Kämpa inte ensam längre än så.

---

*Facit finns hos läraren.*
