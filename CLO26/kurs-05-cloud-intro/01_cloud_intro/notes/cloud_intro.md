# Cloud-intro — läsmaterial

**Modul:** 01 — Moln, IaaS, PaaS och SaaS
**Kurs:** Cloud-intro, CLO26

---

## Varför moln?

Tänk dig att du är IT-ansvarig på ett medelstort företag. Säljteamet ber dig sätta upp en server åt dem inför ett projekt som startar om fyra veckor.

Det gamla sättet: du kontaktar en hårdvaruleverantör, speckar servern, väntar på leverans, rackar den, installerar OS, konfigurerar nätverk. Bästa fall: sex veckor och femtiotusen kronor.

Det nya sättet: du öppnar en terminal och skriver ett kommando. Fem minuter senare finns servern. Du betalar per timme den körs.

Det är molnet. Inte magin — bara en affärsmodell som förändrat hur mjukvara byggs och körs.

---

## Tjänstemodellerna

Molntjänster brukar delas in i tre nivåer beroende på hur mycket du ansvarar för själv.

### IaaS — Infrastructure as a Service

Du hyr infrastrukturen. Det innebär att du får tillgång till en virtuell server med en viss mängd CPU, minne och lagring. Operativsystemet installerar du själv. Du ansvarar för uppdateringar, säkerhet, programvara och allt annat som körs på servern.

Leverantören ansvarar för den fysiska hårdvaran, hypervisorn (mjukvaran som möjliggör virtualisering), nätverket och datacenteranläggningen.

**Exempel:** Azure VM, AWS EC2, Google Compute Engine

**När passar IaaS?**

- Du behöver full kontroll över operativsystemet
- Du kör äldre programvara som kräver en specifik miljö
- Du installerar en databas med anpassade inställningar
- Du har säkerhetskrav som kräver att du härdar OS:et själv

**När passar IaaS inte?**

- Ditt team saknar systemadministrationskompetens
- Du vill deployas snabbt utan att konfigurera infrastruktur
- Du kör en standardwebblösning

### PaaS — Platform as a Service

Du deployer din kod. Plattformen sköter allt annat — operativsystem, runtime, säkerhetspatchar, lastbalansering, skalning.

Du tänker inte på servrar. Du tänker på din applikation.

**Exempel:** Azure App Service, Google App Engine, Heroku

**När passar PaaS?**

- Du är en utvecklare som vill fokusera på kod, inte infrastruktur
- Teamet är litet och saknar dedikerad sysadmin
- Du har en standardapplikation: webb-API, webbsida, mikrotjänst
- Du vill ha auto-scaling utan att konfigurera det manuellt

**När passar PaaS inte?**

- Applikationen kräver programvara eller OS-konfiguration som plattformen inte stödjer
- Du har extrema prestandakrav och behöver tweaka lågnivåinställningar

### SaaS — Software as a Service

Du använder programvaran direkt via webbläsaren. Inga servrar, inga deployments, inga konfigurationer. Du loggar in och jobbar.

Leverantören ansvarar för absolut allt — infrastruktur, applikation, uppdateringar, backup. Du ansvarar för din data och dina användares behörigheter.

**Exempel:** Gmail, Microsoft 365, Figma, Slack, Salesforce, GitHub

**SaaS är det du redan använder varje dag.** Det är molnet i sin mest tillgängliga form.

---

## Jämförelse

| | IaaS | PaaS | SaaS |
|-|------|------|------|
| Kontroll | Hög | Medel | Låg |
| Ansvar | Hög | Medel | Låg |
| Flexibilitet | Stor | Medel | Begränsad |
| Tid till produktion | Längre | Snabb | Direkt |
| Målgrupp | Systemadministratörer | Utvecklare | Slutanvändare |

Det handlar om avvägning. Mer kontroll = mer ansvar. Mindre ansvar = mindre flexibilitet.

---

## De tre stora

### Microsoft Azure

Marknadsledande i Europa och Norden. Tätt integrerat med Microsofts ekosystem: Active Directory, Office 365, Windows Server. Populärt hos medelstora och stora företag med befintlig Microsoft-miljö.

För den här kursen använder vi Azure.

### Amazon Web Services (AWS)

Den ursprungliga molntjänsten — lanserades 2006. Bredast utbud av tjänster, störst global marknadsandel. Populärt hos startups och teknikbolag. Starka inom serverless och container-ekosystem.

### Google Cloud Platform (GCP)

Googles molnplattform. Starka inom AI, maskininlärning och dataanalys (BigQuery är branschstandard). Ofta ett alternativ för bolag som redan använder Google Workspace.

**Namnen skiljer sig — koncepten är desamma.** En Azure VM och en AWS EC2-instans löser samma problem på i princip samma sätt.

---

## Regioner och availability zones

### Region

En region är ett geografiskt område med ett eller flera datacenter. När du skapar en resurs väljer du i vilken region den ska finnas.

Exempel på Azure-regioner: Sweden Central (Stockholm), North Europe (Dublin), West Europe (Amsterdam).

**Varför spelar regionen roll?**

- **Latens.** Ju närmre dina användare är regionen, desto snabbare respons.
- **GDPR och lagkrav.** EU-data måste stanna inom EU. Väljer du en region utanför EU bryter du mot GDPR om du hanterar persondata.
- **Pris.** Priset varierar mellan regioner. USA-regioner är ofta billigare än Europa.
- **Tillgänglighet.** Inte alla tjänster finns i alla regioner.

### Availability Zones

Inom varje region finns availability zones — separata, fysiskt isolerade datacenter med egen strömförsörjning och eget nätverk. Om ett datacenter drabbas av ett strömavbrott eller brand påverkas inte de andra zonerna.

```
Region: Sweden Central
┌──────────────────────────────────────────────┐
│  Zone 1          Zone 2          Zone 3       │
│  Datacenter A    Datacenter B    Datacenter C  │
└──────────────────────────────────────────────┘
```

Kör du i en enda zone och den zonen faller — din app är nere. Kör du i tre zones kan du överleva ett helt datacenteravbrott utan att användarna märker något.

**För kursen:** du kör i en zon. I produktion på ett riktigt system vill du alltid ha redundans.

---

## Prissättning

### Pay-as-you-go

Det vanligaste sättet att börja. Du betalar per timme (eller sekund) för det du använder. Ingen bindningstid. Om du stänger av servern slutar du betala.

Passar: testmiljöer, developmentmiljöer, projekt med okänd belastning.

### Reserverade instanser

Du förbinder dig att använda en viss resurs i ett eller tre år. I utbyte får du upp till 72 procent rabatt jämfört med pay-as-you-go.

Passar: stabil produktionsmiljö där du vet att servern ska köra 24/7 under lång tid.

### Spot-instanser (AWS) / Spot VMs (Azure)

Du köper outnyttjad kapacitet till kraftig rabatt. Nackdelen: leverantören kan ta tillbaka kapaciteten med kort varsel (30 sekunder på AWS).

Passar: batch-jobb, rendering, analys — uppgifter som tål avbrott.

---

## Varför moln istället för lokal server?

Det finns situationer där en lokal server fortfarande är rätt val — exempelvis om du har strikta säkerhetskrav, hanterar känslig data i en reglerad bransch, eller redan har investerat tungt i lokal hårdvara.

Men för de flesta moderna applikationer är molnet standardvalet. Anledningarna:

- **Hastighet.** Servern är redo på minuter, inte veckor.
- **Elasticitet.** Du skalar upp inför en kampanj och ner efteråt. Med en fysisk server betalar du för maxkapaciteten hela tiden.
- **Ingen kapitalkostnad.** Ingen server att köpa, ingen rack att betala för. Driftskostnad i stället för investeringskostnad.
- **Global räckvidd.** Du kan driftsätta i 60+ regioner världen över på samma sätt.
- **Inbyggd redundans.** Availability zones och geografisk replikering är tillgängligt för alla.

---

## Sammanfattning

- **IaaS** ger dig full kontroll och full kontroll kräver fullt ansvar. Rätt för systemadministratörer och situationer som kräver specifik konfiguration.
- **PaaS** låter dig fokusera på koden. Plattformen hanterar infrastrukturen.
- **SaaS** är programvara du bara använder. Du äger ingen del av stacken.
- **Azure, AWS och GCP** är de tre dominerande leverantörerna. Koncepten är desamma — namnen skiljer sig.
- **Region** bestämmer var din data fysiskt finns. Välj rätt för latens, GDPR och pris.
- **Availability zones** ger hög tillgänglighet inom en region.
- **Pay-as-you-go** är flexibelt. Reserverade instanser är billigare på lång sikt.

Nästa modul: du provisionerar en riktig server med ett terminalkommando och konfigurerar den från grunden.
