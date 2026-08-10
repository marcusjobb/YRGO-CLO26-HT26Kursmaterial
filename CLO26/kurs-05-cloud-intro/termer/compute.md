# Compute — Programmeringstermer

Compute = det som faktiskt kör kod. Azure erbjuder flera sätt att köra arbetsbelastningar, från full kontroll (VM) till ingen kontroll alls (Functions). Den här filen täcker de VM-specifika begreppen.

---

## Virtual Machine · Virtuell dator (VM)

En simulerad server som körs på Microsofts hårdvara — men du bestämmer operativsystem, installerad programvara och konfiguration. Det är IaaS: leverantören sköter det fysiska, du sköter allt ovanpå.

**Välj VM när du behöver:**
- Total kontroll över OS
- Köra programvara med specifika krav på miljön
- Lift-and-shift av ett befintligt system utan ombyggnad

> Du ansvarar fortfarande för patchar, uppdateringar och säkerhetskonfiguration. Azure sköter inte det åt dig.

## VM image · VM-avbildning

En förbyggd mall med OS och verktyg — en startpunkt du provisionar från. Istället för att installera Windows eller Ubuntu från scratch väljer du en image från Azure Marketplace och har en körbar server inom minuter.

Images kan vara Microsofts officiella (Windows Server, Ubuntu) eller egna anpassade avbildningar du byggt och sparat.

## VM size family · VM-storleksfamilj

Azure-VMs delas in i familjer baserat på vad de är optimerade för. Välj familj efter vad din arbetsbelastning gör mest.

| Familj | Optimerad för | Typisk användning |
|--------|--------------|-------------------|
| **B-serien** | Kostnad, bursting | Dev/test med tillfälliga CPU-toppar |
| **D-serien** | Allmänt | Webbservrar, appservrar |
| **E-serien** | Minne | In-memory-databaser, analysarbetsbelastningar |
| **F-serien** | CPU | Processorintensiva applikationslager |
| **M-serien** | Stort minne | Stora företagsdatabaser |
| **L-serien** | Lagring | Högt dataflöde, storage-tunga workloads |
| **N-serien** | GPU | AI-träning, grafikrendering |

## VM name · VM-namn (namnkodning)

VM-storlekens namn innehåller information om vad du betalar för. Exempel: `Standard_D2s_v5`

| Del | Betyder |
|-----|---------|
| `D` | Familj (D = generell användning) |
| `2` | Antal vCPU:er |
| `s` | Stöder Premium SSD |
| `v5` | Maskinvarugenerering |

## vCPU · Virtuell processor

En virtuell processorkärna — det som avgör hur mycket parallellt arbete VM:en kan göra. Fler vCPU:er = dyrare, men nödvändigt för CPU-intensiva arbetsbelastningar.

## VM Scale Set · VM-skalningsuppsättning

En grupp identiska, lastbalanserade VM:er som skalas automatiskt upp och ned baserat på efterfrågan eller schema. Du definierar konfigurationen en gång — skalningsuppsättningen ser till att alla instanser är identiska och justerar antalet utan att du behöver göra det manuellt.

Utan skalningsuppsättningar: du klickar fram VM:er manuellt och hoppas att du hinner.
Med skalningsuppsättningar: du sätter reglerna, Azure sköter resten.

## Availability Set · Tillgänglighetsuppsättning

En gruppering av VM:er som sprider dem över separata uppdaterings- och feldomäner — så att ett underhållsfönster eller ett hårdvarufel inte slår ut alla instanser samtidigt.

Tillgänglighetsuppsättningen kostar ingenting extra — du betalar bara för VM:erna.

> I regioner med tillgänglighetszoner är zonbaserad design att föredra — det ger bredare felisolering. Tillgänglighetsuppsättningar är för regioner utan zonstöd.

## Update domain · Uppdateringsdomän

En logisk grupp VM:er som Azure kan starta om samtidigt under planerat underhåll. Azure startar aldrig om alla uppdateringsdomäner på en gång — minst en grupp körs alltid.

## Fault domain · Feldomän

En grupp VM:er som delar samma potentiella felpunkt — samma rack, samma strömkälla, samma nätverksswitch. Om racket tappar ström är bara en feldomän påverkad, inte alla.

## Azure Virtual Desktop · Virtuellt Azure-skrivbord (AVD)

Ett hanterat fjärrskrivbord i molnet. Användarna loggar in från vilken enhet som helst och får ett fullständigt Windows-skrivbord — appar, filer och inställningar körs i Azure, inte på den lokala datorn.

Skillnaden mot en vanlig VM: du skapar inte en server per användare. AVD delar resurser smart — flera användare kan dela samma underliggande VM-kapacitet (multi-session). Centraliserad hantering, ett ställe att sätta åtkomstprinciper, ett ställe att patcha.

**Typiska användningsfall:**
- Supportteam som behöver identiska miljöer oavsett vem som jobbar vilket skift
- Tillfälliga konsulter som behöver åtkomst utan att få en företagsdator
- Hybridarbetare som jobbar från olika enheter och platser

> Data och appar stannar i Azure — ingenting lagras på den lokala enheten. Det minskar risken om en laptop tappas bort eller stjäls.

## Host pool · Värdpool

En samling VM:er som levererar skrivbord och appar via Azure Virtual Desktop. Värdpoolen är det som användarna faktiskt ansluter till — AVD distribuerar sessioner över poolens VM:er.

## Desktop virtualization · Skrivbordsvirtualisering

Att flytta skrivbordet från den fysiska datorn till en server (i molnet eller lokalt). Användaren ser ett vanligt Windows-skrivbord men det körs egentligen på en fjärrserver. RDP (Remote Desktop Protocol) är den klassiska tekniken bakom det.

## Windows Sandbox

En inbyggd lättvikts-VM i Windows 10/11 Pro för att köra okänd programvara isolerat. Startar på sekunder, försvinner spårlöst när du stänger det — ingen data, inga spår kvar på värddatorn.

Starkare isolering än Docker för det här syftet (en riktig VM-gräns, inte bara container-isolering). Aktiveras med:

```powershell
Enable-WindowsOptionalFeature -FeatureName "Containers-DisposableClientVM" -Online
```

> Docker kan också användas för isolerad testning av okänd kod, men container escape-sårbarheter gör Windows Sandbox till ett säkrare val för den uppgiften.

## Disaster recovery · Katastrofåterställning

Att ha backup-kapacitet redo i en annan region om primärregionen faller bort. Med Azure kan du hålla en lågkostnadsreserv igång i en annan region och starta om kritiska arbetsbelastningar där vid behov.

> Katastrofåterställning kräver aktiv planering — Azure failover:ar inte automatiskt om du inte konfigurerat det.

---

## Containrar

## Container · Behållare

En lättviktig virtualiseringsmiljö som paketerar en app och exakt vad den behöver för att köra — kod, beroenden, konfiguration. Inget eget OS. Containrar delar OS-kärnan med värddatorn.

| | VM | Container |
|---|---|---|
| OS | Eget, komplett | Delar värdens kärna |
| Starttid | Minuter | Sekunder |
| Storlek | Gigabyte | Megabyte |
| Isolering | Stark | Lättare men tillräcklig |
| Kontroll | Full | Begränsad till appen |

Välj container när du vill köra många instanser snabbt och billigt. Välj VM när du behöver full OS-kontroll.

## Docker

Den vanligaste containermotorn — verktyget som bygger, kör och hanterar containrar. En container byggs från en **Dockerfile** och paketeras som en **image**. Azure stöder Docker.

## Container image · Container-avbildning

En oföränderlig mall som innehåller allt en container behöver: app-kod, runtime, bibliotek, miljövariabler. Från en image skapar du godtyckligt många identiska containrar. Lagras i ett container registry (t.ex. Azure Container Registry).

## Azure Container Instances · ACI

Det snabbaste och enklaste sättet att köra en container i Azure. PaaS — du laddar upp din container, Azure kör den. Ingen VM att hantera, ingen orkestrering att konfigurera. Passar enstaka uppgifter och enkel testning.

## Azure Container Apps

PaaS med inbyggd lastbalansering och autoskalning. Liknar ACI men passar bättre för appar som behöver växa och krympa med trafiken. Du slipper hantera underliggande infrastruktur.

## Azure Kubernetes Service · AKS

Hanterad Kubernetes i Azure. Kubernetes är en orkestreringstjänst — tänk en dirigent för containrar. Den ser till att rätt antal kör, startar om de som kraschar, fördelar trafiken och rullar ut uppdateringar utan nedetid.

AKS = Kubernetes fast Microsoft sköter dirigentpodiet åt dig.

Kraftfullt men komplext. Rätt val när du kör många containrar som behöver kommunicera och koordineras.

## Microservice architecture · Mikrotjänstarkitektur

Att dela upp en applikation i många små, oberoende tjänster — varje tjänst gör en sak och körs i sin egen container. Frontend, backend och databaslager som separata containrar.

Fördelen: du kan skala, uppdatera och driftsätta varje del separat utan att röra resten av systemet.

**Motsats:** Monolit — en stor app som gör allt. Enklare att börja med, svårare att skala och underhålla.

## Container orchestration · Containerorkestrering

Att automatiskt hålla koll på en massa containrar som körs samtidigt — starta dem, stänga ner dem, starta om de som kraschar, skala upp vid trafiktoppar, fördela trafiken jämnt. Kubernetes är den dominerande lösningen.

*Not: Microsoft använder "vagnparkshantering" i sin svenska översättning. Vagnpark = en samling fordon. Det låter konstigt på containrar. Det är det också — ignorera ordet, förstå konceptet.*

---

## Serverless

## Serverless · Serverlöst

Ingen infrastruktur att hantera. Du skriver koden — molnet sköter allt under: servrar, skalning, patchar, tillgänglighet. Du betalar bara för den tid koden faktiskt körs, inte för idle-tid.

"Serverlöst" betyder inte att det inte finns servrar. Det betyder att du inte behöver bry dig om dem.

## Azure Functions

Händelsestyrd, serverlös körningsmiljö. Du skriver en funktion — en bit kod som gör en sak. Azure kör den när en trigger aktiverar den och frigör resursen direkt när den är klar.

**Välj Functions när:**
- Koden svarar på en händelse (HTTP-anrop, timer, meddelande)
- Arbetet är snabbt — sekunder, inte minuter
- Efterfrågan är oregelbunden och svår att förutspå

## Trigger · Utlösare

Det som aktiverar en Azure Function. Utan trigger händer ingenting.

Vanliga triggers:
- **HTTP** — en REST-förfrågan träffar en endpoint
- **Timer** — "kör varje natt kl 02:00"
- **Queue** — ett meddelande dyker upp i en meddelandekö
- **Blob** — en fil laddas upp till storage

## Stateless · Tillståndslöst

Standardläget för Azure Functions. Varje körning startar med ett blankt minne — funktionen vet ingenting om tidigare anrop. Enkelt, snabbt och skalas obegränsat.

Som en miniräknare som nollställs efter varje beräkning.

## Stateful · Tillståndskänsligt (Durable Functions)

Funktionen minns var den var. Används för workflows med flera steg där en körning väntar på en annan: "steg 1 klart → inväntar godkännande → steg 2 startar."

Hanteras via **Durable Functions** — ett tillägg till Azure Functions som spårar tillstånd automatiskt.

## Durable Functions

En utökning av Azure Functions som ger tillståndskänslig orchestrering. Passar långvariga workflows, fläktprocesser (fan-out/fan-in) och mänskliga godkännandeflöden.

## Event-driven · Händelsestyrt

Arkitekturmönster där kod körs *som svar på* en händelse, inte kontinuerligt. Ingenting körs tills något händer — en användare klickar, en fil sparas, ett meddelande anländer. Functions är händelsestyrt per design.

## Pay-per-execution · Betala per körning

Prissättningsmodellen för serverless. Du debiteras bara för den CPU-tid som förbrukas när koden faktiskt körs — inte för server-idle. Kostar ingenting kl 03:00 om ingen anropar din funktion.
