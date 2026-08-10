# Tjänstmodeller — Programmeringstermer

## Shared responsibility model · Modell för delat ansvar

Principen om att ansvar delas mellan dig och molnleverantören — men fördelningen beror på vilken tjänstmodell du valt. I IaaS är det mesta ditt ansvar. I SaaS är det mesta leverantörens. PaaS landar någonstans mitt emellan.

| Lager | IaaS | PaaS | SaaS |
|---|---|---|---|
| Fysisk hårdvara | Leverantör | Leverantör | Leverantör |
| Operativsystem | **Du** | Leverantör | Leverantör |
| Applikation | **Du** | **Du** | Leverantör |
| Data | **Du** | **Du** | **Du** |

> Data är alltid ditt ansvar, oavsett modell.

## Infrastructure as a Service · Infrastruktur som en tjänst (IaaS)

Du hyr hårdvara i molnet — servrar, nätverk, lagring. Leverantören sköter det fysiska (el, kylning, inbrott). Du sköter allt ovanpå: operativsystem, patchar, nätverkskonfiguration, databaser.

Maximal kontroll, men också maximalt egenansvar. Passar när du behöver styra precis hur miljön är konfigurerad.

**Se även:** [Shared responsibility model](#shared-responsibility-model--modell-för-delat-ansvar), [Lift-and-shift](#lift-and-shift-migration--lift-and-shift-migrering)

## Lift-and-shift migration · Lift-and-shift-migrering

Att flytta en befintlig lokal server till molnet i princip som den är — utan att bygga om arkitekturen. Du "lyfter" den ur ditt eget datacenter och "shiftar" över den till IaaS i molnet.

Snabbaste vägen till molnet, men du tar med dig både styrkor och svagheter från det gamla systemet.

<details><summary>När är lift-and-shift rätt val?</summary>

Bra när du behöver flytta snabbt, eller när applikationen är svår att modernisera (legacy-system). Inte optimalt om du vill dra full nytta av molnets skalbarhet och managerade tjänster — för det krävs ofta en större ombyggnad.

</details>

## Workload · Arbetsbelastning

Samlingsbegrepp för det som körs på en server eller i molnet — en applikation, en databas, en batch-process. "Migrera en arbetsbelastning" = flytta det som körs till en ny miljö.

## On-premises · Lokalt (on-prem)

Infrastruktur som du äger och driver själv, fysiskt placerad i din egna lokal eller ditt eget datacenter. Motsatsen till molnet. Ofta förkortat "on-prem".

> Vanlig fråga vid molnmigration: vad ska ligga kvar on-prem, och vad ska upp i molnet?

## Operating system patch · OS-patch / Operativsystemsuppdatering

En uppdatering som åtgärdar säkerhetshål eller buggar i operativsystemet. I IaaS är det **ditt ansvar** att hålla OS patchat. I PaaS och SaaS sköter leverantören det. En opatchad server är en av de vanligaste inkörsportarna för attacker.

---

## Platform as a Service · Plattform som en tjänst (PaaS)

Du hyr en hel utvecklingsmiljö — operativsystem, databas, runtime och utvecklingsverktyg ingår och sköts av leverantören. Du fokuserar på koden och datan. Inget OS att patcha, inga licenser att jaga.

Mellannivån mellan IaaS (maxkontroll, maxjobb) och SaaS (ingen kontroll, inget jobb). App Service i Azure är ett klassiskt PaaS-exempel — du pushar din .NET-app, Azure kör den.

**Se även:** [Shared responsibility model](#shared-responsibility-model--modell-för-delat-ansvar), [Middleware](#middleware--mellanprogram), [Managed service](#managed-service--hanterad-tjänst)

## Middleware · Mellanprogram

Programvara som sitter i lagret mellan operativsystemet och din applikation. Hanterar saker som autentisering, meddelandeköer och databasanslutningar — grejer som alla appar behöver men ingen vill bygga om från grunden.

I PaaS ingår middleware som en del av paketet. I IaaS är det ditt ansvar att installera och konfigurera den.

## Development framework · Utvecklingsramverk

En färdig plattform att bygga applikationer på — .NET, Node.js, Django. Ramverket ger dig struktur, standardlösningar för vanliga problem och en massa kod du slipper skriva.

I PaaS-kontext: du väljer ramverk, leverantören ser till att det finns tillgängligt och uppdaterat i miljön.

## High availability · Hög tillgänglighet

Systemet är designat för att vara uppe nästan hela tiden — ofta mätt i "nines": 99,9 % = ungefär 8 timmar nersatt per år, 99,99 % = ungefär 52 minuter. Molnplattformar bygger in hög tillgänglighet automatiskt i PaaS-tjänster.

> Om du kör IaaS och vill ha hög tillgänglighet får du konfigurera det själv — load balancers, redundanta instanser, failover.

## Multi-tenancy · Flera klientorganisationer

Många kunders applikationer delar samma underliggande infrastruktur — men isolerade från varandra. Som lägenheter i ett hyreshus: du delar fasaden och trapphuset, men din lägenhet är din.

PaaS och SaaS är byggda på multi-tenancy. Det är en av anledningarna till att de är kostnadseffektiva.

## Managed service · Hanterad tjänst

En tjänst där leverantören tar ansvar för drift, uppdateringar, skalning och tillgänglighet. Du använder tjänsten, inte maskinen bakom den.

Azure SQL Database är en hanterad tjänst — det är en SQL Server, men du slipper patcha, säkerhetskopiera och skala den manuellt.

**Motsats:** Att köra SQL Server själv på en IaaS-VM — då hanterar du allt.

## Business intelligence · Affärsintelligens (BI)

Att analysera data för att hitta mönster och insikter som hjälper till att fatta bättre beslut. I PaaS-kontext: leverantören tillhandahåller analysverktyg som en tjänst — du kopplar in dina data, verktyget kör analysen.

Azure Synapse Analytics och Power BI är exempel på BI-tjänster i PaaS-familjen.

## Test- och utvecklingsmiljö · Development/test environment

En tillfällig miljö för att testa kod utan att röra produktionssystemet. I molnet kan du starta en sådan på minuter och stänga ner den när du är klar — du betalar bara för den tid du faktiskt använder.

Vanligt IaaS-scenario: du replikerar produktionsmiljöns konfiguration i en test-VM, kör testerna, river ner den.

---

## Software as a Service · Programvara som en tjänst (SaaS)

Du använder ett färdigt program via webbläsaren — leverantören sköter allt: infrastruktur, plattform, applikation, uppdateringar. Du hanterar bara dina egna data och vem som har tillgång.

Minst flexibel av de tre modellerna, men enklast att komma igång med. Kräver ingen teknisk expertis för att använda. Microsoft 365, Gmail och Slack är SaaS. Du installerar ingenting, du loggar bara in.

> Du äger alltid dina data — även i SaaS. Vad som händer med dem om du avslutar abonnemanget är en annan fråga. Läs avtalet.

**Se även:** [Shared responsibility model](#shared-responsibility-model--modell-för-delat-ansvar), [Data governance](#data-governance--datastyrning)

## Data governance · Datastyrning

Regler och processer för hur organisationens data får hanteras — vem som äger den, vem som får läsa eller ändra den, hur länge den sparas och hur den skyddas.

I SaaS är datastyrning extra viktig: du lägger din data i någon annans system. Du behöver veta var den lagras, vem hos leverantören som kan nå den och vad som händer med den när kontraktet går ut.

## Identity and access management · Identitets- och åtkomsthantering (IAM)

Att styra vem som är vem (identitet) och vad de får göra (åtkomst). I SaaS är detta i princip allt du hanterar — leverantören tar hand om resten.

I Azure heter det Entra ID (tidigare Azure Active Directory). IAM avgör om Kalle får läsa en fil, om Pelle får deploya till produktion, och om en app får prata med en annan app.

**Se även:** [RBAC](#) i `säkerhet.md` (om den filen finns)

## Operational overhead · Driftsbörda

Mängden arbete det krävs för att hålla ett system igång — patchar, uppdateringar, skalning, felsökning, säkerhetskopiering. Driftsbördan minskar ju högre upp i tjänstmodellen du går.

| Modell | Driftsbörda |
|--------|-------------|
| IaaS | Hög — du hanterar OS och uppåt |
| PaaS | Medel — du hanterar kod och data |
| SaaS | Låg — du hanterar konto och data |
