# Virtuella Servrar i Azure

## Vad är en Virtuell Maskin (VM)?

En VM är en programvaruemulering av en fysisk dator. En hypervisor (t.ex. Microsoft Hyper-V) kör på den fysiska hårdvaran och skapar isolerade miljöer — virtuella maskiner — som var och en har sitt eget OS, CPU, minne och lagring.

Azure använder en specialbyggd hypervisor som bygger på Hyper-V.

## IaaS — Infrastructure as a Service

Med en VM får du **full kontroll** över allt från OS och uppåt:

| Du ansvarar för | Azure ansvarar för |
|----------------|-------------------|
| OS (uppdateringar, säkerhet) | Fysisk hårdvara |
| Installerad programvara | Hypervisor |
| Databas, webserver, etc. | Nätverksinfrastruktur |
| Backup av data | Datacenter (el, kyla, fysisk säkerhet) |

## VM-storlekar i Azure

Azure erbjuder många serier. Vanligast för utveckling:

| Serie | Typ | Användning |
|-------|-----|------------|
| B-serie | Burstable | Utveckling, låg belastning |
| D-serie | Generell | De flesta produktionsappar |
| E-serie | Minnesoptimerad | Databaser, cache |
| F-serie | CPU-optimerad | Batch, gaming |

## Nätverk och NSG

Network Security Groups (NSG) är Azure's inbyggda brandvägg:

- **Default:** All inkommande trafik blockeras
- **Regler:** Explicit tillåt på specifika portar från specifika källor
- **Prioritet:** Lägre nummer = högre prioritet

## SSH vs RDP

| Metod | OS | Port | Autentisering |
|-------|-----|------|---------------|
| SSH | Linux | 22 | Nyckelpar (rekommenderas) eller lösenord |
| RDP | Windows | 3389 | Lösenord eller Azure AD |

## Kostnadsoptimering

- **Deallokera** (stoppa) VM:n när den inte används — betala bara för lagring
- **Auto-shutdown** — schemalägg avstängning (t.ex. 18:00 varje dag)
- **B-serien** — burstable, perfekt för utveckling och test
- **Reserved Instances** — upp till 72% rabatt för 1/3 år

## Viktigaste lärdomarna

- VM = IaaS — du äger OS och allt ovanpå
- SSH-nycklar är säkrare än lösenord för Linux
- NSG är din brandvägg — var restriktiv med öppna portar
- Stäng av utvecklings-VM på natten och helger
- Välj rätt VM-serie för din arbetsbelastning
