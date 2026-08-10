# Azure-arkitektur — Programmeringstermer

Hierarkin i Azure går från ytterst till innerst: **Konto → Prenumeration → Resursgrupp → Resurs**. Allt du gör i Azure lever någonstans i den strukturen.

---

## Azure account · Azure-konto

Kontot som allt tillhör — det du loggar in med. Knutet till en identitet (e-post + Microsoft-konto) och till faktureringen. Ett företag kan ha ett enda konto med många prenumerationer under sig.

> Tänk på kontot som hyresvärden. Prenumerationerna är lägenheterna.

## Subscription · Prenumeration

En faktureringsenhet och åtkomstgräns inom ett Azure-konto. Alla resurser du skapar hör till en prenumeration — och det är prenumerationen som faktureras.

En prenumeration har två typer av gränser:

| Typ | Vad det innebär |
|-----|-----------------|
| **Faktureringsgräns** | Varje prenumeration genererar en egen faktura — lätt att spåra kostnader per team eller miljö |
| **Åtkomstkontrollgräns** | Principer och åtkomstregler sätts per prenumeration — dev och prod kan ha helt olika regler |

**Typiskt upplägg:**
- `sub-dev` — utvecklarna bygger och bryter
- `sub-test` — QA och integrationstester
- `sub-prod` — det som kunder faktiskt använder

**Se även:** [Resource group](#resource-group--resursgrupp), [Management group](#management-group--hanteringsgrupp)

## Resource group · Resursgrupp

En logisk behållare för resurser som hör ihop. En webbapp, dess databas och dess lagring kan samlas i samma resursgrupp — och när projektet är slut tar du bort hela gruppen på en gång.

Resursgruppen är din städenhet.

**Tre regler du måste kunna:**
1. En resurs tillhör exakt **en** resursgrupp åt gången (går att flytta, men inte ha i två)
2. Resursgrupper **kan inte kapslas** i varandra
3. Resursgrupper **kan inte döpas om** — välj namn noggrant från början

Åtgärder på gruppen gäller alla resurser i den: tar du bort gruppen tar du bort allt. Beviljar du åtkomst gäller det alla resurser i gruppen.

> Välj namngivningskonvention direkt. `rg-prod-swecentral` är bättre än `MinGrupp1`.

**Se även:** [Resource](#resource--resurs), [Naming convention](#naming-convention--namngivningskonvention)

## Resource · Resurs

En enskild Azure-tjänst som du skapat — en VM, en databas, ett lagringskonto, en App Service. Allt du provisionar i Azure är en resurs.

Varje resurs har:
- Ett namn (måste vara unikt inom sin scope)
- En typ (t.ex. `Microsoft.Web/sites` för App Service)
- En region den körs i
- En resursgrupp den tillhör

## Management group · Hanteringsgrupp

En nivå ovanför prenumerationer — används i större organisationer för att sätta policies på flera prenumerationer samtidigt. Principer och åtkomst som sätts på en hanteringsgrupp **ärvs nedåt** genom hela hierarkin automatiskt.

**Hierarkin — arv går alltid nedåt:**
```
Tenant root group  ← en enda rotgrupp per organisation
  └── Hanteringsgrupp
        └── Prenumeration
              └── Resursgrupp
                    └── Resurs
```

Gränser att känna till:
- Max **6 nivåer** djupt (exkl. rot och prenumerationsnivå)
- Max **10 000** hanteringsgrupper per katalog
- Varje grupp/prenumeration har exakt **en** förälder

**Exempel:** Sätt en policy på hanteringsgruppen "Produktion" som säger att VM:er bara får skapas i `Sweden Central`. Den policyn gäller automatiskt för alla prenumerationer, resursgrupper och resurser under — ingen kan åsidosätta det.

Nybörjare behöver sällan skapa hanteringsgrupper. Men förstå att nivån finns och varför.

## Policy inheritance · Principarv

Inställningar, åtkomstregler och policies som sätts på en högre nivå i hierarkin ärvs automatiskt av allt nedanför. En RBAC-tilldelning på hanteringsgruppen gäller alla prenumerationer, resursgrupper och resurser under — utan att du behöver konfigurera det individuellt.

> Arv går bara **nedåt** — en resurs kan inte påverka sin resursgrupp uppåt.

## Naming convention · Namngivningskonvention

Regler för hur du döper resurser i Azure. Viktigt för att hålla ordning när antalet resurser växer. Resursgrupper kan inte döpas om — ett dåligt namn sitter fast.

Microsofts rekommenderade format: `[typ]-[projekt]-[miljö]-[region]-[nummer]`

**Exempel:**
- `rg-webbshop-prod-swec-001` — resursgrupp
- `vm-api-dev-swec-001` — virtuell maskin
- `st-bilder-prod-swec-001` — storage account

## Free tier · Kostnadsfri nivå

Azure erbjuder ett gratis konto med:
- 12 månaders fri tillgång till populära tjänster
- 30 dagars kredit vid registrering
- 65+ tjänster som alltid är gratis

Studerande kan registrera sig med ett studentkonto (utan kreditkort) och få $100 i kredit plus gratis utvecklarverktyg i 12 månader.

> Resurser kostar pengar även om du glömmer dem. Ta alltid bort det du inte längre behöver.

---

## Fysisk infrastruktur

Den logiska hierarkin (konto → prenumeration → resursgrupp → resurs) bestämmer *vem som äger vad*. Den fysiska hierarkin bestämmer *var det faktiskt körs*.

## Datacenter

En fysisk anläggning fylld med servrar i rack — dedikerad ström, kylning och nätverksinfrastruktur. Du interagerar aldrig direkt med enskilda datacenter i Azure. Det du väljer är regioner och zoner — Azure sköter resten.

## Region · Region

Ett geografiskt område med minst ett datacenter, ofta flera. Datacentren i en region hänger ihop via ett nätverk med låg latens.

När du skapar en resurs väljer du region. Valet påverkar:
- Var datan fysiskt lagras (relevant för GDPR)
- Hur nära slutanvändarna tjänsten körs
- Vilka tjänster och VM-storlekar som är tillgängliga

**Exempel:** `Sweden Central`, `West Europe`, `East US`

> Vissa tjänster — som Entra ID och Azure DNS — är globala och kräver inget regionval.

## Latency · Latens

Fördröjningen det tar för data att färdas från en punkt till en annan. Mäts i millisekunder. En region nära dina användare = lägre latens = snabbare upplevelse.

## Availability zone · Tillgänglighetszon

Fysiskt separata datacenter inom en och samma region — egna strömkällor, kylning och nätverk. Om ett datacenter (zon) faller bort fortsätter de andra att köra.

En tillgänglighetszonaktiverad region har alltid **minst tre** zoner. De hänger ihop via snabb fiberoptik.

Azure-tjänster delas in i tre kategorier utifrån zonstöd:

| Kategori | Vad det innebär | Exempel |
|----------|-----------------|---------|
| **Zonindelad** | Du väljer exakt vilken zon resursen körs i | VM, hanterade diskar, IP-adresser |
| **Zonredundant** | Azure replikerar automatiskt mellan zoner | Zone-redundant lagring, SQL Database |
| **Icke-regional** | Alltid tillgänglig, påverkas inte av zon- eller regionavbrott | Entra ID, Azure DNS, Traffic Manager |

> Inte alla regioner stöder tillgänglighetszoner — kontrollera innan du designar för hög tillgänglighet.

## Region pair · Regionpar

Två regioner i samma geografi (USA, Europa, Asien) kopplade ihop — minst 800 km från varandra. Om en katastrof slår ut hela regionen finns den parade regionen kvar.

Fördelar:
- Azure uppdaterar aldrig båda regionerna i ett par samtidigt
- Vid ett stort Azure-haveri prioriteras återställning av en region i varje par
- Data stannar inom samma geografiska område (viktigt för GDPR och compliance)

**Exempel på regionpar:** `Sweden Central` ↔ `Sweden South`, `West Europe` ↔ `North Europe`

<details><summary>Enriktade par — specialfall</summary>

De flesta regionpar är dubbelriktade (båda fungerar som backup för varandra). Undantag finns: Brasilien södra är parat med Södra centrala USA — men i bara en riktning. Brasilien södra backar upp Södra centrala USA, men inte tvärtom.

</details>

## Sovereign region · Nationell region

En isolerad instans av Azure — åtskild från det publika molnet av juridiska eller säkerhetsmässiga skäl. Används av myndigheter och reglerade branscher i länder med krav på datasuveränitet.

**Exempel:**
- `US Gov Virginia` / `US DoD Central` — för amerikanska myndigheter, drivs av säkerhetskontrollerad personal
- `China East` / `China North` — drivs av 21Vianet, inte av Microsoft direkt

Som utvecklare i Sverige behöver du sällan röra nationella regioner — men det är bra att veta att de finns och varför.

## Geography · Geografi (Azure)

Den övergripande gruppering ovanför regioner — ett kontinent eller ett land. `Europe`, `United States`, `Asia Pacific`. Regionpar väljs alltid inom samma geografi för att uppfylla krav på dataplacering.

## Failover · Redundansväxling

Att automatiskt byta till en backup-resurs när primärresursen slutar fungera. Kan ske på zon-nivå (inom en region) eller region-nivå (till regionparet).

> Inte alla Azure-tjänster failover automatiskt. För vissa måste du konfigurera replikering och återställning manuellt.
