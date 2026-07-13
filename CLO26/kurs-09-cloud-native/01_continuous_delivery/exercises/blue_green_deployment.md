# Övning — Blue-Green Deployment

🔴 Utmaning

---

## Bakgrunden

HyresBil-appen rullar. Riktiga kunder. Riktiga bokningar. Och nu har produktchefen sagt det som alla fruktar: "Vi kan inte ha driftstopp vid deploy."

Tidigare har ni kört `az webapp restart` och hoppats på det bästa. En minut nere, kunder som ser felmeddelanden, en pinsam Slack-notis. Det är inte längre acceptabelt.

Lösningen kallas **blue-green deployment**. Du kör alltid **två identiska miljöer** — blue och green. En av dem är live. Den andra är reserved. När du deployer: du deployer till den som *inte* är live, testar den, och switchar sedan trafiken. Noll sekunder driftstopp. Rollback på under en minut om något går fel.

Det här är Continuous Delivery i praktiken — inte bara pipelinen, utan hela filosofin: varje deploy ska vara ofarlig.

---

## Vad gäller för den här övningen

- [ ] Som utbildare vill jag att du kan skapa och konfigurera två identiska Azure-VM-miljöer
- [ ] Som utbildare vill jag att du kan sätta upp ett Azure Load Balancer med backend pool och health probes
- [ ] Som utbildare vill jag att du kan deploya till den inaktiva miljön utan att röra den live-miljön
- [ ] Som utbildare vill jag att du kan switcha trafik och genomföra rollback med motivering
- [ ] Som utbildare vill jag att du kan förklara varför det här är CD och inte bara ett deployskript

*Dessa punkter är vad vi tittar på — inte om varje kommando ser identiskt ut som i exemplet.*

---

## Del 1 — Skapa de två miljöerna

Börja med att skapa din resource group och sedan de två VM:erna. Båda ska köra exakt samma applikation — det är poängen.

```bash
# Resource group
az group create \
  --name hyresbil-rg \
  --location swedencentral

# Blue-miljön
az vm create \
  --resource-group hyresbil-rg \
  --name hyresbil-blue \
  --image Ubuntu2204 \
  --size Standard_B1s \
  --admin-username azureuser \
  --generate-ssh-keys \
  --public-ip-sku Standard \
  --no-wait

# Green-miljön (identisk konfiguration)
az vm create \
  --resource-group hyresbil-rg \
  --name hyresbil-green \
  --image Ubuntu2204 \
  --size Standard_B1s \
  --admin-username azureuser \
  --generate-ssh-keys \
  --public-ip-sku Standard
```

Vänta tills båda är igång. Verifiera:

```bash
az vm list \
  --resource-group hyresbil-rg \
  --output table
```

Förväntad output (ungefär):

```
Name             ResourceGroup    Location       PowerState
---------------  ---------------  -------------  ------------
hyresbil-blue    hyresbil-rg      swedencentral  VM running
hyresbil-green   hyresbil-rg      swedencentral  VM running
```

Installera sedan appen på båda. SSH in i varje VM och kör:

```bash
sudo apt update && sudo apt install -y python3

# Skapa en minimal webbserver som svarar med vilken miljö det är
# (Byt ut "blue" mot "green" på green-servern)
cat << 'EOF' > /home/azureuser/app.py
from http.server import HTTPServer, BaseHTTPRequestHandler

class Handler(BaseHTTPRequestHandler):
    def do_GET(self):
        self.send_response(200)
        self.end_headers()
        self.wfile.write(b"HyresBil - BLUE environment - version 1.0")

HTTPServer(("0.0.0.0", 80), Handler).serve_forever()
EOF

sudo python3 /home/azureuser/app.py &
```

> **Notera:** I verkligheten deployer din pipeline den faktiska applikationen. Den här miniservern simulerar det så du kan se vilket svar du får.

---

## Del 2 — Load Balancer med backend pool

Nu sätter du upp Load Balancern. Den sitter framför dina VM:er och tar emot all trafik utifrån. Just nu pekar den på blue.

```bash
# Skapa en publik IP för Load Balancern
az network public-ip create \
  --resource-group hyresbil-rg \
  --name hyresbil-lb-ip \
  --sku Standard \
  --allocation-method Static

# Skapa Load Balancern
az network lb create \
  --resource-group hyresbil-rg \
  --name hyresbil-lb \
  --sku Standard \
  --public-ip-address hyresbil-lb-ip \
  --frontend-ip-name hyresbil-frontend \
  --backend-pool-name hyresbil-backend
```

Hämta de privata IP-adresserna för dina VM:er:

```bash
az vm list-ip-addresses \
  --resource-group hyresbil-rg \
  --output table
```

Notera IP:erna. Du behöver dem för nästa steg.

Lägg till **hyresbil-blue** i backend pool:

```bash
az network lb address-pool address add \
  --resource-group hyresbil-rg \
  --lb-name hyresbil-lb \
  --pool-name hyresbil-backend \
  --name blue-address \
  --ip-address <BLUE_PRIVATE_IP> \
  --vnet <DITT_VNET_NAMN>
```

---

## Del 3 — Health probes

En health probe är Load Balancerns sätt att fråga: "Är du fortfarande vid liv?" Den pingar din app med jämna mellanrum. Svarar den inte — tas den ut ur rotation automatiskt. Det är det som gör att ett fel inte syns för kunderna.

```bash
# Skapa health probe på port 80
az network lb probe create \
  --resource-group hyresbil-rg \
  --lb-name hyresbil-lb \
  --name hyresbil-probe \
  --protocol Http \
  --port 80 \
  --path / \
  --interval 5 \
  --threshold 2
```

`--interval 5` betyder att proben skickar ett anrop var 5:e sekund. `--threshold 2` betyder att om 2 svar på rad misslyckas — ut ur rotation.

Skapa sedan en load balancing rule som kopplar ihop frontend, backend och proben:

```bash
az network lb rule create \
  --resource-group hyresbil-rg \
  --lb-name hyresbil-lb \
  --name hyresbil-rule \
  --protocol Tcp \
  --frontend-port 80 \
  --backend-port 80 \
  --frontend-ip-name hyresbil-frontend \
  --backend-pool-name hyresbil-backend \
  --probe-name hyresbil-probe
```

Testa att allt funkar. Hämta Load Balancerns publika IP:

```bash
az network public-ip show \
  --resource-group hyresbil-rg \
  --name hyresbil-lb-ip \
  --query ipAddress \
  --output tsv
```

Öppna den i webbläsaren eller kör:

```bash
curl http://<LOAD_BALANCER_IP>
```

Förväntad output:

```
HyresBil - BLUE environment - version 1.0
```

Bra. Blue är live.

---

## Del 4 — Deploy till green medan blue är live

Det är dags för den nya versionen. Blue rullar fortfarande mot kunderna. Du deployer version 2.0 till green — utan att en enda kund märker det.

SSH in i hyresbil-green och uppdatera appen:

```bash
# Stoppa gamla instansen
pkill -f app.py

# Deploy "version 2.0"
cat << 'EOF' > /home/azureuser/app.py
from http.server import HTTPServer, BaseHTTPRequestHandler

class Handler(BaseHTTPRequestHandler):
    def do_GET(self):
        self.send_response(200)
        self.end_headers()
        self.wfile.write(b"HyresBil - GREEN environment - version 2.0")

HTTPServer(("0.0.0.0", 80), Handler).serve_forever()
EOF

sudo python3 /home/azureuser/app.py &
```

Verifiera att green svarar korrekt **direkt på dess IP** — inte via Load Balancern:

```bash
curl http://<GREEN_PRIVATE_IP>
```

Förväntad output:

```
HyresBil - GREEN environment - version 2.0
```

Kör dina röktest. Logga in manuellt om du vill. Låt en kollega testa. Den här fasen är din chans att vara säker innan du switchar.

Kör fortfarande `curl http://<LOAD_BALANCER_IP>` — du ska fortfarande se blue. Kunderna märker ingenting.

---

## Del 5 — Traffic switch: blue ut, green in

Allt ser bra ut på green. Nu är det dags.

Ta bort blue från backend pool:

```bash
az network lb address-pool address remove \
  --resource-group hyresbil-rg \
  --lb-name hyresbil-lb \
  --pool-name hyresbil-backend \
  --name blue-address
```

Lägg till green:

```bash
az network lb address-pool address add \
  --resource-group hyresbil-rg \
  --lb-name hyresbil-lb \
  --pool-name hyresbil-backend \
  --name green-address \
  --ip-address <GREEN_PRIVATE_IP> \
  --vnet <DITT_VNET_NAMN>
```

Verifiera switchen:

```bash
curl http://<LOAD_BALANCER_IP>
```

Förväntad output:

```
HyresBil - GREEN environment - version 2.0
```

Det är det. Ingen nedtid. Inga felmeddelanden. Ingen stressad Slack.

---

## Del 6 — Rollback

Green har ett fel. En kund rapporterar att bokningsflödet hänger sig. Du behöver gå tillbaka till blue — nu.

```bash
# Ta bort green
az network lb address-pool address remove \
  --resource-group hyresbil-rg \
  --lb-name hyresbil-lb \
  --pool-name hyresbil-backend \
  --name green-address

# Lägg tillbaka blue
az network lb address-pool address add \
  --resource-group hyresbil-rg \
  --lb-name hyresbil-lb \
  --pool-name hyresbil-backend \
  --name blue-address \
  --ip-address <BLUE_PRIVATE_IP> \
  --vnet <DITT_VNET_NAMN>
```

Verifiera:

```bash
curl http://<LOAD_BALANCER_IP>
```

Förväntad output:

```
HyresBil - BLUE environment - version 1.0
```

Under en minut. Det är vad rollback ska ta. Inte en lång utredning, inte en nödsituation. Två kommandon.

---

## Reflektionsfrågor (skriv ner svaren)

1. Varför är det här Continuous Delivery och inte bara ett snyggt deployskript? Vad är skillnaden i *tankesättet*?

2. Vad händer med pågående HTTP-anrop när du switchar trafik? Är det ett problem med den setup du byggt?

3. Vilka tre saker måste vara sanna för att blue-green ska fungera i ett skarpt system med databas?

4. Health proben pingar var 5:e sekund med threshold 2. Det innebär att en trasig server kan vara live i upp till 10 sekunder. Hur skulle du minska det fönstret, och vad är avvägningen?

---

## Tips

> Har du en miljö som svarar men inte den andra? Kontrollera NSG-regler (Network Security Group) — port 80 måste vara öppen inåt på båda VM:erna.

> Hittar du inte ditt vnet-namn? Kör `az network vnet list --resource-group hyresbil-rg --output table`.

> ⏱️ **15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, en AI eller läraren. Kämpa inte ensam längre än så.

---

*Facit och diskussionssvar finns hos läraren.*

*Av Marcus Ackre Medina — Nion Education — marcus.medina@nionit.com*
