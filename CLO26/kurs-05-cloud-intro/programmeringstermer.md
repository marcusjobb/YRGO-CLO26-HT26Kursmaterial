# Programmeringstermer — Cloud Intro (Kurs 05)

Viktiga Azure-begrepp för introduktion till molninfrastruktur.

---

## Nätverkstermer

**Azure Virtual Network (VNet)**
Ett privat nätverk i molnet. Du skapar ett nätverk med egna IP-adresser, segmenterar det i undernät, och låter dina molnresurser kommunicera säkert.
Exempel: en startups hela molninfrastruktur ligger i ett VNet — webbservrar i ett undernät, databas i ett annat.

**Undernät (Subnet)**
En del av ditt VNet:s IP-adressutrymme. Du delar upp VNet:et för att organisera resurser — frontend här, backend där.
Exempel: VNet 10.0.0.0/16 har två undernät: 10.0.1.0/24 för webbtjänster, 10.0.2.0/24 för databaser.

**Nätverkssäkerhetsgrupp (Network Security Group, NSG)**
Azures brandvägg. Du skriver regler för vilken trafik som får in och ut ur dina resurser.
Exempel: NSG-regel som tillåter HTTP och HTTPS men blockerar SSH från att komma från Internet.

**Offentlig IP-adress (Public IP)**
En IP-adress som är synlig på Internet. Du tilldelar den till en resurs som ska vara tillgänglig utifrån.
Exempel: din webbserver får 51.104.45.23 och blir åtkomlig via denna IP.

---

## Isolering och segmentering

**Isolering av nätverk**
En VNet är helt åtskild från andra VNet:s. Resurser i ditt VNet kan inte komma åt resurser i andras VNet utan explicit tillåtelse.
Exempel: ett företags VNet kan inte se ett annat företags VNet — komplett separation.

**Segmentering med undernät**
Att dela upp ett VNet i mindre undernät gör det möjligt att organisera resurser och använda olika säkerhetsregler för olika delar.
Exempel: en applikation har frontend i ett undernät och databas i ett annat — NSG:er kan då skydda databasen från all extern trafik.

**Namnmatchning (DNS)**
Azure har en inbyggd DNS-tjänst så att resurser i ditt VNet kan hitta varandra via namn istället för IP-adresser.
Exempel: en webbserver kan ansluta till databasen genom att skriva "database.internal" istället för en IP-adress.

## Introduktion till molnanslutningar

**Punkt-till-plats-VPN (Point-to-Site)**
En säker anslutning från en enskild dator till ditt molnätverk. Ofta för fjärranställda eller utvecklare som behöver åtkomst.
Exempel: en utvecklare hemma kör en VPN-klient och får åtkomst till sitt företags Azure-nätverk.

**Virtuell nätverkspeering (Virtual Network Peering)**
Två Azure-nätverk som ansluter direkt till varandra. Trafiken mellan dem är privat och går inte över Internet.
Exempel: ett företag har två VNet:s — ett i Sverige och ett i UK. Peering låter dem kommunicera direkt.

**Tjänstslutpunkter (Service Endpoints)**
En säker väg för dina VNet-resurser att ansluta till Azure-tjänster (som databaser eller lagring) utan att gå över Internet.
Exempel: en webbserver i VNet:et ansluter säkert till Azure SQL Database via en tjänstslutpunkt.

---

## DNS

**Azure DNS**
Azures DNS-tjänst för din domän. Du hostar dina DNS-poster i Azure tillsammans med resten av infrastrukturen.
Exempel: din domän example.com ligger på Azure DNS.

**App Service-miljö (App Service Environment)**
En dedikerad miljö för att köra Azure App Service-appar med hög prestanda och säkerhet.
Exempel: ett företag kör sina kritiska webbappar i en App Service-miljö för att få större kontroll och isolation.

**Azure Kubernetes Service (AKS)**
En hanterad Kubernetes-tjänst för att köra behållarbaserade applikationer i skala.
Exempel: ett startup använder AKS för att köra mikrotjänster i Docker-behållare.

**VM-skalningsuppsättningar (Virtual Machine Scale Sets)**
En grupp identiska virtuella datorer som skalas automatiskt baserat på efterfrågan.
Exempel: en webbapp behöver 2 datorer vid låg last och 10 vid högsta last — skalningsuppsättningar hanterar det automatiskt.

**Azure SQL-databaser**
Molnbaserad relationsdatabas som är helt hanterad av Azure.
Exempel: en app lagrar användardata i en Azure SQL Database istället för att köra egen databasserver.

**Lagringskonton (Storage Accounts)**
En Azure-tjänst för att lagra data — filer, blobbar, köer, tabeller.
Exempel: en app sparar användarfoton i Azure Blob Storage som en del av ett lagringskonto.

**Microsofts stamnätverk (Microsoft Backbone Network)**
Microsofts globala, privata nätverksinfrastruktur som förbinder Azure-datacenter världen över.
Exempel: när två peer-kopplade Azure-nätverk kommunicerar, använder de Microsofts privata stamnätverk, inte Internet.

**Krypterad anslutning**
En säker kommunikationsväg där data är krypterad så att endast avsändare och mottagare kan läsa det.
Exempel: en VPN-anslutning från ditt hem till Azure använder kryptering så att ingen kan läsa vad du skickar.

**Inkommande och utgående säkerhetsregler**
Regler som definierar vilken trafik som får in och ut ur en resurs.
Exempel: en NSG-regel kan säga "tillåt inkommande HTTP från Internet" men "blockera all utgående trafik till Internet".

**WAN-optimering (Wide Area Network)**
Teknik för att förbättra prestanda och effektivitet i långväga nätverkskommunikation.
Exempel: en virtuell nätverksinstallation optimerar trafik mellan ett kontor i Sverige och ett datacenter i USA.

**Protokoll (Network Protocol)**
En standard för hur datorer kommunicerar — bestämmer format, ordning och vilka åtgärder som krävs.
Exempel: HTTP är ett protokoll för webbkommunikation, TCP/IP är grundprotokollet för Internet.

