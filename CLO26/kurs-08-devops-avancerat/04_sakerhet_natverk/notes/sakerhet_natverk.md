# Nätverkssäkerhet i Azure — Fördjupning

## Defense in Depth (Flerskiktat Försvar)

Säkerhet handlar inte om en enda mur, utan flera lager:

```
Användare → Internet → Brandvägg → NSG → App → Data
                    ↓         ↓      ↓      ↓
                 DDoS-skydd   LB   ASG   Kryptering
```

Varje lager skyddar mot olika typer av attacker. Om ett lager penetreras finns nästa kvar.

## Azure Virtual Network (VNet)

Ett VNet är ditt eget isolerade nätverk i Azure. Nyckelkoncept:

- **Address Space:** CIDR-block (t.ex. 10.0.0.0/16)
- **Subnets:** Underindelning (10.0.1.0/24 för frontend, 10.0.2.0/24 för backend)
- **VNet Peering:** Sammanlänka flera VNet (samma eller olika regioner)
- **DNS:** Intern DNS för resursnamn

## NSG vs ASG

**NSG (Network Security Group):**
- Brandväggsregler baserade på IP-adresser och portar
- Tillämpas på subnet eller NIC (network interface card)
- Default: ALL INBOUND DENY, ALL OUTBOUND ALLOW

**ASG (Application Security Group):**
- Logisk gruppering av VM baserat på applikationsroll
- Skapa NSG-regler som refererar ASG istället för IP
- Exempel: "Allow traffic from WebASG to DatabaseASG on port 1433"

## Azure Firewall

Hanterad, molnbaserad brandväggstjänst. Skillnad från NSG:
- Central hantering (en Azure Firewall för hela nätverket)
- Application FQDN-regler (tillåt trafik till *.windowsupdate.com)
- Threat intelligence (blockera kända skadliga IP-adresser)
- DNAT (översättning av publika portar till interna)

## VPN Gateway

Anslut lokalt nätverk till Azure via VPN:
- **Site-to-Site (S2S):** Anslut hela kontoret till Azure via VPN-enhet
- **Point-to-Site (P2S):** Enskilda datorer ansluter till Azure (för utvecklare/remote)
- **ExpressRoute:** Dedikerad privat fiberanslutning (dyrare, stabilare)

## Private Endpoint

Ger Azure-tjänster (Storage, SQL databas) en privat IP i ditt VNet. Trafiken går inte över internet — säkrare och bättre prestanda.

Utan Private Endpoint: `minkonto.blob.core.windows.net` → publikt internet
Med Private Endpoint: samma URL → privat IP i ditt VNet

## TLS/SSL

Transport Layer Security — kryptering av data i transit. Azure-hantering:

- **Azure Front Door** — TLS-terminering vid edge
- **Application Gateway** — TLS-terminering, vidarebefordrar till backend över HTTP
- **Let's Encrypt** — gratis SSL-certifikat (stöds i App Service via extension)
- **Key Vault** — central lagring av certifikat

## CIS Benchmark

Center for Internet Security publicerar benchmarks för Azure. Exempel:

- All offentlig lagring ska vara blockerad
- Diagnostikloggar ska vara aktiverade
- MFA ska krävas för alla användare
- Privata endpoints för alla PaaS-tjänster
- Just-in-time VM-åtkomst
