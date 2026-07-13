---
marp: true
theme: nion-dark
paginate: true
---

# Cloud-intro
**Kurs:** Cloud-intro
**Modul:** 01 — Moln, IaaS, PaaS och SaaS
Marcus Ackre Medina · YRGO · CLO26

---

## Vad ska vi lära oss idag?

- Varför moln — och vad är det egentligen?
- Tre tjänstemodeller: **IaaS**, **PaaS**, **SaaS**
- De tre stora leverantörerna: **Azure**, **AWS**, **GCP**
- Regioner och availability zones
- Prissättning — pay-as-you-go vs reserverade instanser
- Vad du som framtida systemutvecklare faktiskt behöver kunna

---

## Varför moln?

Tänk dig att du är IT-ansvarig på ett medelstort företag.

Du behöver en server. Alternativen är:

| Alternativ | Tid | Kostnad | Flexibilitet |
|------------|-----|---------|--------------|
| Köp fysisk server | 4–8 veckor | 50 000–200 000 kr | ❌ |
| Hyr datacenterplats (co-lo) | 2–4 veckor | Högt fast pris | ❌ |
| Molnserver (IaaS) | **5 minuter** | Betala per timme | ✅ |

Företaget behöver servern i tre månader. Vad väljer du?

---

## Molnet — andras datorer (men smarta)

> "Molnet är bara andras datorer."

Sant. Men de är:
- Geografiskt spridda över hela världen
- Redundanta — om en brinner ner tar nästa över
- Elastiska — du skalar upp på en minut, ner på en minut
- Betalda per sekund — inte per år

Du hyr kapacitet. Du äger ingen hårdvara.

---

## Tjänstemodellerna — tre nivåer

```
┌─────────────────────────────────────┐
│           SaaS — du använder        │
│         (Gmail, Teams, Figma)       │
├─────────────────────────────────────┤
│          PaaS — du deployer         │
│       (Azure App Service)           │
├─────────────────────────────────────┤
│         IaaS — du konfigurerar      │
│         (Azure VM, AWS EC2)         │
└─────────────────────────────────────┘
             ▲ Mer kontroll
             ▼ Mer ansvar
```

**Ju lägre i stacken, desto mer gör du själv.**

---

## IaaS — Infrastructure as a Service

Du hyr infrastrukturen. OS och allt ovanpå är ditt ansvar.

| Du ansvarar för | Leverantören ansvarar för |
|----------------|--------------------------|
| Operativsystem | Fysisk hårdvara |
| Säkerhetsuppdateringar | Hypervisor |
| Installerad programvara | Nätverk + datacenter |
| Skalning | Strömförsörjning, kyla |

**Exempel:** Azure VM, AWS EC2, Google Compute Engine

---

## IaaS — när passar det?

✅ Du behöver full kontroll över OS  
✅ Du kör legacy-programvara som kräver specifik konfiguration  
✅ Du ska installera en databas med speciella inställningar  
✅ Du har specifika säkerhetskrav som kräver härdning av OS  

❌ Du vill slippa tänka på OS-uppdateringar  
❌ Du behöver snabb deployment utan infrastrukturarbete  
❌ Ditt team är litet och saknar sysadmin-kompetens  

---

## PaaS — Platform as a Service

Du deployer din applikation. Plattformen sköter resten.

| Du ansvarar för | Leverantören ansvarar för |
|----------------|--------------------------|
| Din applikationskod | OS + uppdateringar |
| Konfiguration av appen | Runtime och middleware |
| Skalningsinställningar | Lastbalansering |
| | Patching, säkerhet |

**Exempel:** Azure App Service, Google App Engine, Heroku

---

## PaaS — när passar det?

✅ Du vill fokusera på koden, inte infrastrukturen  
✅ Du behöver auto-scaling utan att konfigurera det manuellt  
✅ Teamet är litet — ingen dedikerad systemadministratör  
✅ Du har en standardapplikation (webb-API, webbsida)  

❌ Du behöver installera specialprogram som PaaS inte stödjer  
❌ Du behöver full kontroll över OS-konfigurationen  
❌ Applikationen har extrema prestandakrav  

---

## SaaS — Software as a Service

Du använder programvaran direkt. Inga servrar, inga konfigurationer.

| Du ansvarar för | Leverantören ansvarar för |
|----------------|--------------------------|
| Din data | Allt annat |
| Användarbehörigheter | Infrastruktur |
| | Applikation |
| | Driftsättning |
| | Uppdateringar |

**Exempel:** Gmail, Microsoft 365, Figma, Slack, Salesforce

---

## IaaS / PaaS / SaaS — sammanfattning

| | IaaS | PaaS | SaaS |
|-|------|------|------|
| **Kontroll** | Hög | Medel | Låg |
| **Ansvar** | Hög | Medel | Låg |
| **Flexibilitet** | ✅✅✅ | ✅✅ | ✅ |
| **Tid till produktion** | Längre | Snabb | Direkt |
| **Exempel** | Azure VM | App Service | Gmail |
| **Passar för** | Systemadm. | Utvecklare | Slutanvändare |

---

## De tre stora leverantörerna

**Microsoft Azure**
Stark inom företagsmarknaden. Tätt integrerat med Office 365, Active Directory, Windows. Marknadsledande i Europa och Norden.

**Amazon Web Services (AWS)**
Pionjären — bredast utbud, störst marknadsandel globalt. Stark inom startup-ekosystemet.

**Google Cloud Platform (GCP)**
Starka på AI/ML, data och analytics. Bra prisnivå. Används ofta som alternativ eller komplement.

---

## Azure — viktigaste tjänsterna

| Kategori | Tjänst | Vad det är |
|----------|--------|------------|
| IaaS | Azure VM | Virtuell server |
| PaaS | App Service | Driftsätt webbapp direkt |
| PaaS | Azure SQL | Hanterad databas |
| Lagring | Blob Storage | Filer, bilder, backuper |
| Nätverk | Virtual Network | Privat nätverk i molnet |
| Identity | Azure AD | Användare och behörigheter |

---

## AWS och GCP — snabbjämförelse

| Azure | AWS | GCP | Kategori |
|-------|-----|-----|----------|
| Azure VM | EC2 | Compute Engine | IaaS server |
| App Service | Elastic Beanstalk | App Engine | PaaS |
| Azure SQL | RDS | Cloud SQL | Databas |
| Blob Storage | S3 | Cloud Storage | Fillagring |
| Azure AD | IAM | Cloud IAM | Identity |

Namnen skiljer sig — koncepten är desamma.

---

## Regioner — var finns din data?

En **region** är ett geografiskt område med ett eller flera datacenter.

- **Azure:** 60+ regioner — Sweden Central (Stockholm), North Europe (Dublin)...
- **AWS:** 30+ regioner — eu-north-1 (Stockholm), eu-west-1 (Irland)...
- **GCP:** 40+ regioner — europe-north1 (Finland), europe-west4 (Nederländerna)...

**Varför spelar det roll?**
- **Latens** — närmre användare = snabbare respons
- **GDPR** — EU-data måste stanna inom EU
- **Pris** — West Europe ≠ East US i pris

---

## Availability Zones — hög tillgänglighet

Inom varje region finns **availability zones** (AZ) — separata datacenter.

```
Region: Sweden Central
┌─────────────────────────────────────────────┐
│  AZ 1            AZ 2            AZ 3        │
│  Datacenter A    Datacenter B    Datacenter C │
│  Egen ström      Egen ström      Egen ström   │
│  Eget nätverk    Eget nätverk    Eget nätverk │
└─────────────────────────────────────────────┘
```

Om AZ 1 faller — AZ 2 och 3 fortsätter.  
Kör du i en AZ? En incident kan stänga ner din app.  
Kör du i tre AZ:er? Du klarar ett helt datacenteravbrott.

---

## Prissättning — hur betalar man?

**Pay-as-you-go (PAYG)**
- Betalar per timme (ofta per sekund)
- Ingen bindningstid
- Perfekt för: test, dev, okänd belastning

**Reserverade instanser**
- Binder dig i 1 eller 3 år
- Upp till **72% billigare** än PAYG
- Perfekt för: stabil produktion, förutsägbar belastning

**Spot-instanser (AWS) / Spot VMs (Azure)**
- Outnyttjad kapacitet till kraftig rabatt
- Kan stängas av med 30 sekunders varning
- Perfekt för: batch-jobb, rendering, analys

---

## Varför moln istället för lokal server?

| Lokal server | Moln |
|-------------|------|
| Hög investeringskostnad | Ingen kapitalkostnad |
| 4–8 veckors leveranstid | Klar på 5 minuter |
| Fast kapacitet | Elastisk kapacitet |
| Du ansvarar för hårdvara | Leverantören ansvarar |
| Ingen global räckvidd | 60+ regioner direkt |
| Katastrofskydd är dyrt | Inbyggt med AZ |

*En startup som behöver servrar idag har inte 8 veckor att vänta.*

---

## Sammanfattning

- ✅ **IaaS** — full kontroll, du sköter OS. Exempel: Azure VM
- ✅ **PaaS** — du deployer koden, plattformen sköter resten. Exempel: App Service
- ✅ **SaaS** — du använder programvaran. Exempel: Gmail
- ✅ **Azure, AWS, GCP** — olika namn, liknande koncept
- ✅ **Region** = geografiskt datacenterområde. Välj rätt för latens och GDPR
- ✅ **AZ** = isolerade datacenter inom en region — för hög tillgänglighet
- ✅ **PAYG** för flexibilitet, reserverat för rabatt

➡️ Nästa: provisionera en riktig server via terminalen
