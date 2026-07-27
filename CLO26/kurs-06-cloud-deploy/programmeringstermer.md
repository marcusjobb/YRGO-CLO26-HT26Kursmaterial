# Programmeringstermer — Cloud Deploy (Kurs 06)

Azure-begrepp för molndeploy och infrastrukturkonfiguration.

---

## Nätverkstermer

**Azure Virtual Network (VNet)**
Ett privat nätverk i molnet för dina resurser. Grundläggande för all molninfrastruktur — här definierar du nätverk, subnät och säkerhetsprinciper.
Exempel: ett företags produktion ligger i ett VNet med strikt separering mellan frontend, backend och databas.

**Undernät (Subnet)**
En logisk uppdelning av ditt VNet:s IP-adressutrymme. Du placerar olika typer av resurser i olika undernät för organisering och säkerhet.
Exempel: Webbnivå i ett undernät (10.0.1.0/24), applikationsnivå i ett annat (10.0.2.0/24).

**Nätverkssäkerhetsgrupp (NSG)**
En brandvägg för dina resurser. Du definierar inbound och outbound-regler som styr all trafik.
Exempel: NSG-regel som säger "endast webbtjänsterna får ansluta till databasen på port 3306".

**Privat slutpunkt (Private Endpoint)**
En resurs som är endast åtkomlig inifrån sitt VNet. Den har ingen offentlig IP-adress.
Exempel: en datalagringstjänst (Azure Storage) som bara kan nås från aplikationer inom VNet:et.

**Routningstabell (Route Table)**
Regler för hur trafiken ska dirigeras mellan undernät och ut från VNet:et. Du kan styra trafikflödet helt.
Exempel: "all trafik till Internet ska gå genom en proxy-server, inte direkt".

**Användardefinerad väg (User-Defined Routes, UDR)**
Anpassade routningsregler som du skriver själv. Du kan styra hur paket ska dirigeras istället för att använda Azures standardvägval.
Exempel: "all trafik till 192.168.0.0/16 ska gå genom vår virtuella nätverksinstallation (firewall)".

**Tjänstslutpunkter (Service Endpoints)**
En säker väg för dina VNet-resurser att ansluta till Azure-tjänster (som SQL, Storage) utan att trafiken behöver gå över Internet.
Exempel: en webbserver i VNet:et kan ansluta säkert och privat till Azure SQL Database.

**Virtuell nätverksinstallation (Virtual Network Appliance)**
En specialiserad VM som gör nätverksjobb — ofta en brandvägg, router eller proxy. För mer avancerad trafikstyrning än NSG:er tillåter.
Exempel: ett företag kör en Palo Alto Networks-brandvägg som en VNA för trafikanalys.

---

## VPN och säkra anslutningar

**VPN Gateway**
En gateway som skapar krypterade tunnlar mellan ditt lokala nätverk och Azure. Säker anslutning för hybrid-miljöer.
Exempel: ett företags lokala datacenter ansluts till sitt Azure VNet via VPN Gateway.

**Plats-till-plats-VPN (Site-to-Site)**
En krypterad anslutning mellan två nätverksenheter — en lokalt, en i Azure. Ungefär som en säker fiber mellan två ställen.
Exempel: ett företags HQ i Stockholm och dess Azure-infrastruktur i molnet kopplas via S2S VPN.

**Punkt-till-plats-VPN (Point-to-Site)**
En säker VPN för enskilda datorer som behöver åtkomst till Azure-nätverket. Ofta för fjärranställda eller operatörer.
Exempel: en DevOps-ingenjör hemma ansluter via P2S VPN för att deploya till Azure.

**Aktiv/aktiv-gateway**
Två VPN-gateways som är aktiva samtidigt. Ger högre genomströmning och redundans.
Exempel: en kritis applikation behöver mycket bandbredd — aktiv/aktiv garanterar det.

**Zonredundant gateway**
En gateway som sprids över flera tillgänglighetszoner. Om en zon kraschar fungerar anslutningen ändå.
Exempel: en gateway som är resistent mot datacenterfel.

---

## Azure ExpressRoute (för avancerad deploy)

**Azure ExpressRoute**
En dedikerad privat fiber-anslutning från ditt datacenter direkt till Azure. Mycket snabbare och säkrare än VPN.
Exempel: ett stort finansbolag hyr en dedikerad 10Gbps-fiber från sitt datacenter till Azures infrastruktur.

**ExpressRoute-krets**
En ExpressRoute-anslutning via din ISP. Kretsen har en viss bandbredd och pris baserat på data som överförs.
Exempel: en 1Gbps ExpressRoute-krets från Telia till Azure.

---

## DNS

**Azure DNS**
Azures DNS-hosting. Du placerar dina domäner här för enkel integration med molninfrastrukturen.
Exempel: din domän example.com ligger på Azure DNS tillsammans med dina molnresurser.

**Privat DNS-domän**
En DNS-domän som endast fungerar inifrån ditt VNet. Externa användare kan inte nå dessa namn.
Exempel: en domän prod.internal som bara dina interna Azure-resurser kan nå.

**Aliaspost**
En DNS-post som automatiskt pekar till en Azure-resurs. Om resursen byter IP uppdateras posten automatiskt.
Exempel: API-alias api.example.com pekar alltid till rätt Application Gateway oavsett dess IP.

**Offentlig IP-adress (Public IP)**
En IP-adress som är synlig på Internet och kan nås från vilken plats som helst.
Exempel: en webbserver får en offentlig IP så att användare från hela världen kan nå den.

**Offentlig lastbalanserare (Public Load Balancer)**
En Azure-tjänst som fördelar inkommande trafik mellan flera servrar. Gör att du kan hantera mycket trafik.
Exempel: en populär webbplats använder en lastbalanserare för att fördela förfrågningar mellan 10 webbbservrar.

**App Service-miljö (App Service Environment)**
En dedikerad miljö för att köra Azure App Service-appar med större isolation och kontroll.
Exempel: ett företag kör sina känsliga affärsappar i en App Service Environment för extra säkerhet.

**Azure Kubernetes Service (AKS)**
En hanterad Kubernetes-tjänst för att köra och hantera behållarbaserade applikationer i skala.
Exempel: ett SaaS-företag använder AKS för att köra hundratals mikrotjänster i Docker-behållare.

**VM-skalningsuppsättningar (Virtual Machine Scale Sets)**
En grupp identiska virtuella datorer som automatiskt skalas upp eller ner baserat på efterfrågan.
Exempel: en e-commerce-sajt behöver 5 servrar på normal dag och 100 servrar på Black Friday — skalningsuppsättningar hanterar det automatiskt.

**Azure SQL-databaser**
En hanterad relationsdatabas i molnet. Du behöver inte hantera servern — Azure gör det.
Exempel: en app lagrar kunddata i Azure SQL Database istället för att sätta upp egen databasserver.

**Lagringskonton (Storage Accounts)**
En molntjänst för att lagra filer, blobbar, köer och tabeller. Otroligt skalbar och hållbar.
Exempel: en mobil-app sparar användarfoton i Azure Blob Storage för att de ska vara åtkomliga överallt.

**Microsofts stamnätverk (Microsoft Backbone Network)**
Microsofts globala privata nätverksinfrastruktur som förbinder alla Azure-regioner och datacenter.
Exempel: när två VNet:s i olika länder peer-kopplas, använder de Microsofts stamnätverk för att kommunicera privat.

**Krypterad anslutning**
En säker kommunikationskanal där data är krypterad så att endast avsändare och mottagare kan läsa den.
Exempel: VPN-anslutningar använder kryptering för att skydda data från avlyssning.

**Inkommande och utgående säkerhetsregler (Inbound/Outbound Rules)**
Regler som definierar vilken trafik som får in och ut ur en nätverksresurs.
Exempel: "Tillåt inkommande HTTP från Internet" och "Blockera all utgående trafik utom till Azure-tjänster".

**WAN-optimering (Wide Area Network Optimization)**
Teknik för att förbättra hastighet och effektivitet i långväga nätverkskommunikation.
Exempel: en virtuell nätverksinstallation optimerar data som skickas mellan Stockholm och Tokyo.

**Protokoll (Network Protocol)**
En standard för hur datorer kommunicerar — definierar format, ordning och regler för utbyte av information.
Exempel: HTTP för webbar, SMTP för e-post, DNS för namnuppslagning.

**Virtuell nätverkspeering (Virtual Network Peering)**
En direkt anslutning mellan två VNet:s så de kan kommunicera privat utan att gå via Internet.
Exempel: ett företag har ett VNet i Sverige och ett i USA — peering låter dem kommunicera säkert via Microsofts stamnätverk.

**Nätverkstrafik (Network Traffic)**
Data som flödar mellan resurser i ett nätverk. Kan filtreras och dirigeras enligt regler.
Exempel: HTTP-trafik från en klient till en webbserver är ett exempel på nätverkstrafik.

**Border Gateway Protocol (BGP)**
Ett dynamiskt routningsprotokoll som utbyter väginformation mellan nätverksenheter automatiskt.
Exempel: när du lägger till ett nytt undernät i ditt VNet sprids denna information automatiskt via BGP.

**Isolering och segmentering**
Principen att separera resurser i olika nätverk eller undernät för att begränsa kommunikation och förbättra säkerhet.
Exempel: produktion och testmiljö ligger i separata VNet:s för att förhindra konfigurationsmisstag.

**Namnmatchning (DNS)**
Processen att översätta ett namn (t.ex. "database.internal") till en IP-adress som datorn kan förstå.
Exempel: en webbserver frågar "vad är IP-adressen till database.internal?" och får svaret 10.0.2.5.

