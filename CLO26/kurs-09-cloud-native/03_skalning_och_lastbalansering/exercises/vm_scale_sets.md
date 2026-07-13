# Övning — Azure VM Scale Sets och autoskalning

🟡

---

## Vad du ska göra

HyresBil har exploderat i popularitet. En fredag eftermiddag strömmar bokningarna in — och den enda VM:en som kör appen börjar svettas. CPU:n är på 95 %, svarstiderna skjuter i höjden, och kunderna börjar ringa in klagomål.

Du ska lösa det en gång för alla. Sätt upp en VM Scale Set (VMSS) som börjar med en enda instans och automatiskt skalar upp till fem när belastningen ökar — och skalar ner igen när det lugnar sig. Du simulerar sedan trafik för att se skalningen hända i realtid.

**Tid:** 45–60 minuter

---

## Förutsättningar

- Azure CLI inloggad (`az login`)
- En aktiv subscription
- Du känner till vad en VM är och vad CPU-belastning innebär
- SSH-nyckelpar finns på din maskin (kontrollera med `ls ~/.ssh/id_rsa.pub`)

---

## Del 1 — Skapa miljön

Börja med att sätta upp resurser. Allt hamnar i en gemensam resource group.

```bash
# Variabler — ändra bara om du vill
RG="rg-hyrsbil-vmss"
LOCATION="swedencentral"
VMSS_NAME="vmss-hyrsbil"

# Skapa resource group
az group create \
  --name $RG \
  --location $LOCATION
```

---

## Del 2 — Skapa VM Scale Set

Nu skapar du VMSS:en. Observera flaggorna noga — de styr storleken på poolen vid start och vilken image som används.

```bash
az vmss create \
  --resource-group $RG \
  --name $VMSS_NAME \
  --image Ubuntu2204 \
  --vm-sku Standard_B1s \
  --instance-count 1 \
  --admin-username azureuser \
  --generate-ssh-keys \
  --upgrade-policy-mode automatic \
  --public-ip-per-vm
```

`--instance-count 1` sätter startläget till en enda instans. Det är inte minimum — det är bara startvärdet. Minimum och maximum definieras i autoscale-regeln som du skapar i nästa steg.

`--public-ip-per-vm` ger varje instans en egen publik IP — det gör det enklare att SSH:a in på enskilda maskiner under övningen.

### Varför följer en Load Balancer automatiskt med?

När du kör `az vmss create` utan att ange något om load balancing skapar Azure ändå en Azure Load Balancer åt dig. Det beror på att en VMSS per definition är en grupp av identiska instanser som delar på trafiken — och utan en load balancer har du ingen mekanism som fördelar anrop mellan dem.

Load balancern gör tre saker:
1. Tar emot inkommande trafik på en gemensam IP-adress
2. Fördelar anropen jämnt mellan instanserna (round-robin som standard)
3. Kör hälsokontroller mot varje instans — om en instans inte svarar tas den ur rotation automatiskt

Du behöver inte konfigurera detta manuellt. Det är ett medvetet designbeslut från Azure: VMSS och load balancer är ett paket, för de är meningslösa utan varandra.

---

## Del 3 — Konfigurera autoskalning

Autoscale är en separat resurs i Azure Monitor. Du skapar den och kopplar den till VMSS:en.

```bash
# Hämta VMSS-resurs-ID
VMSS_ID=$(az vmss show \
  --resource-group $RG \
  --name $VMSS_NAME \
  --query id \
  --output tsv)

# Skapa autoscale-profilen med min 1, max 5 instanser
az monitor autoscale create \
  --resource-group $RG \
  --resource $VMSS_ID \
  --resource-type Microsoft.Compute/virtualMachineScaleSets \
  --name autoscale-hyrsbil \
  --min-count 1 \
  --max-count 5 \
  --count 1
```

`--count 1` är default-instansantalet när inga regler triggas — det vill säga normalläget. `--min-count 1` sätter golvet (VMSS körs aldrig med noll instanser). `--max-count 5` är taket.

### Regel: skala upp vid hög CPU

```bash
az monitor autoscale rule create \
  --resource-group $RG \
  --autoscale-name autoscale-hyrsbil \
  --condition "Percentage CPU > 70 avg 5m" \
  --scale out 1
```

`avg 5m` means att Azure tittar på snittet under 5 minuter innan regeln triggas — det skyddar mot tillfälliga toppar som ordnar sig själva. `--scale out 1` lägger till en instans per trigger.

### Regel: skala ner vid låg CPU

```bash
az monitor autoscale rule create \
  --resource-group $RG \
  --autoscale-name autoscale-hyrsbil \
  --condition "Percentage CPU < 30 avg 10m" \
  --scale in 1
```

Nedskalningstiden är längre (10 minuter) för att undvika "flapping" — ett mönster där VMSS:en skalar ner för tidigt och sedan tvingas skala upp igen direkt. Bättre att vänta lite för länge innan du skalar ner.

Kontrollera att båda reglerna är registrerade:

```bash
az monitor autoscale rule list \
  --resource-group $RG \
  --autoscale-name autoscale-hyrsbil \
  --output table
```

---

## Del 4 — Simulera last

Nu ska du tvinga CPU:n uppåt så att autoscale-regeln triggas. Du SSH:ar in på instansen och kör `stress-ng` — ett verktyg som medvetet maximerar processorn.

### Hitta instansens IP-adress

```bash
az vmss list-instance-public-ips \
  --resource-group $RG \
  --name $VMSS_NAME \
  --output table
```

Notera IP-adressen till instans 0.

### SSH och installera stress-ng

```bash
ssh azureuser@<IP-ADRESS>

# Inne på VM:en:
sudo apt-get update -y && sudo apt-get install -y stress-ng
```

### Starta lasten

```bash
# Kör stress-ng med fyra CPU-workers i 10 minuter
stress-ng --cpu 4 --timeout 600s &
```

`&` kör processen i bakgrunden så att terminalen är fri. Du kan nu öppna en andra terminal och övervaka skalningen.

### Alternativ: Apache Benchmark mot load balancern

Om du hellre vill simulera HTTP-trafik mot appen (mer realistiskt) kan du använda `ab` (Apache Benchmark) från din lokala maskin. Hämta load balancerns publika IP:

```bash
az network public-ip list \
  --resource-group $RG \
  --query "[].{Name:name, IP:ipAddress}" \
  --output table
```

Kör sedan:

```bash
# Skicka 50 000 anrop med 200 i parallell — ersätt IP:n
ab -n 50000 -c 200 http://<LOAD-BALANCER-IP>/
```

---

## Del 5 — Övervaka skalningen i realtid

Öppna en ny terminal (låt stress-ng fortsätta köra i den andra) och kör följande med jämna mellanrum. Du bör se instansantalet öka.

```bash
# Lista alla instanser och deras status
az vmss list-instances \
  --resource-group $RG \
  --name $VMSS_NAME \
  --output table
```

Outputen visar varje instans med dess ID, provisioneringstatus och power state. När autoscale triggar ser du nya instanser dyka upp med status `Creating` och sedan `Succeeded`.

Kontrollera även aktuellt instansantal:

```bash
az vmss show \
  --resource-group $RG \
  --name $VMSS_NAME \
  --query sku.capacity \
  --output tsv
```

Vänta 5–10 minuter. Siffran ska stiga från 1 mot 5 om CPU:n håller sig över 70 %.

---

## Del 6 — Stoppa lasten och se nedskalning

Gå tillbaka till VM-terminalen och avbryt stress-ng:

```bash
# Hitta och avsluta processen
pkill stress-ng
```

Vänta nu 10–15 minuter och kör `az vmss list-instances` igen. Instanserna ska börja försvinna tills bara en återstår.

Det är cooldown-perioden i autoscale-regeln som håller tillbaka nedskalningen — Azure väntar på att snittet ska ligga under 30 % i 10 minuter i rad innan det skalar ner en instans.

---

## Varför stateless appar passar VMSS — och stateful appar inte gör det

En VMSS fungerar så att vilken instans som helst kan ta vilket anrop som helst. Det förutsätter att appen inte minns något lokalt mellan anropen.

Tänk på HyresBil: om en bokningsession sparas i minnet på instans 2, och nästa anrop från samma användare hamnar på instans 4 — vad händer då? Sessionen är borta. Användaren loggas ut, bokningen bryts, kunden ringer in till supporten.

**Stateless app** (passar VMSS):
- Sessioner sparas i Redis eller en databas
- Varje instans kan ta över från en annan utan att tappa data
- Om en instans kraschar förlorar ingen användare sin session

**Stateful app** (passar inte VMSS):
- Sessioner, fil-uploads eller cache ligger i VM:ens eget minne eller lokala disk
- Anropet måste alltid nå samma instans ("sticky sessions")
- Sticky sessions motverkar load balancing och gör skalning ineffektiv

Regeln: om din app behöver komma ihåg något mellan anrop — se till att det minnet bor utanför VM:en (i en databas, i Redis, i Blob Storage) innan du sätter upp en VMSS.

---

## Städa upp

Ta bort hela resource group när du är klar. Det tar bort VMSS, load balancern, public IP:erna och autoscale-resursen.

```bash
az group delete \
  --name $RG \
  --yes \
  --no-wait
```

`--no-wait` gör att kommandot returnerar direkt — borttagningen sker i bakgrunden. Kontrollera att resource group är borta efter några minuter:

```bash
az group exists --name $RG
```

---

## Kontrollpunkter

Innan du avslutar, kontrollera att du kan svara på det här:

- [ ] Köra `az vmss list-instances` och förklara vad varje kolumn betyder.
- [ ] Förklara varför Azure skapar en load balancer automatiskt vid `az vmss create`.
- [ ] Säga med egna ord varför cooldown-perioden finns — vad händer om den är för kort?
- [ ] Förklara skillnaden på `--instance-count`, `--min-count` och `--max-count`.
- [ ] Motivera varför HyresBil behöver vara stateless för att VMSS ska fungera korrekt.

---

**15-minutersregeln:** Om du kört fast i mer än 15 minuter på samma punkt — ta en paus, läs felmeddelandet högt för dig själv, och fråga sedan en kurskamrat eller läraren. Fastna inte i tystnad.
