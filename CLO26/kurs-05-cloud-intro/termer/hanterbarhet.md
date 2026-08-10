# Hanterbarhet och hållbarhet — Programmeringstermer

## Manageability · Hanterbarhet

Hur enkelt det är att hantera och styra sina molnresurser. Delas in i två delar: vad molnet gör åt dig (hantering AV molnet) och hur du själv kan styra molnet (hantering I molnet).

---

## Management of the cloud · Hantering av molnet

Det molnet sköter automatiskt — utan att du behöver peka och klicka. Exempel: startar om en kraschad server, skalar upp vid hög belastning, skickar larm om något beter sig konstigt.

**Se även:** [Auto-scaling](#auto-scaling--automatisk-skalning), [Health monitoring](#health-monitoring--hälsoövervakning)

## Management in the cloud · Hantering i molnet

De olika sätt du kan styra och konfigurera molnet på: webbportalen (peka och klicka), CLI (terminalen), API (programmatiskt) eller PowerShell (skript). Välj det som passar situationen — ett team kör ofta alla fyra beroende på uppgift.

**Se även:** [CLI](#cli--kommandoradsgränssnitt)

## Scale up · Vertikal skalning

Att göra en enskild resurs kraftfullare — mer CPU, mer RAM, snabbare disk. Du byter ut VM:en mot en större modell. Enkelt att förstå, men det finns ett tak: du kan inte skala en enskild maskin hur mycket som helst.

**Exempel:** Din app-server är seg. Du uppgraderar från Standard_B2s till Standard_D4s. Samma server, mer muskler.

## Scale out · Horisontell skalning

Att lägga till fler instanser av samma resurs istället för att göra en instans större. Tre servrar istället för en kraftigare server. Trafiken fördelas mellan dem med en load balancer.

Föredraget i molnet — det finns i princip inget tak, och du kan skala tillbaka automatiskt när trycket lättar.

**Exempel:** Din webbtjänst får plötsligt tio gånger mer trafik. Istället för en gigantisk server kör du tio normala — och stänger av nio när det lugnar ner sig.

## Auto-scaling · Automatisk skalning

Molnet lägger automatiskt till fler resurser (skalar ut) när belastningen ökar — och tar bort dem igen när det lugnar ner sig. Som en kock som ringer in extrapersonal under lunchen och skickar hem dem kl 14.

<details><summary>Varför funkar det så?</summary>

Automatisk skalning bygger på regler du sätter upp: "om CPU > 80% i 5 minuter, starta en ny instans". Molnet övervakar kontinuerligt och agerar när gränsen nås.

</details>

## Infrastructure template · Infrastruktursmall

En förkonfigurerad mall som beskriver exakt vilka resurser som ska skapas och hur de ska konfigureras. Distribuerar du från en mall får alla miljöer (dev, test, prod) exakt samma setup — inga manuella misstag.

> I Azure kallas detta ARM-mallar (Azure Resource Manager) eller Bicep-filer.

## Health monitoring · Hälsoövervakning

Molnet kollar löpande om dina resurser mår bra. Krånglar något — en server svarar inte, en tjänst crashar — kan molnet automatiskt byta ut den mot en ny utan att du ens märker det.

## Alert · Avisering

Automatiskt larm som triggas när ett mätvärde passerar en gräns du satt upp. Exempel: "meddela mig om CPU är över 90% i mer än 10 minuter" eller "skicka ett mail om felfrekvensen stiger". Du bestämmer vad som är anmärkningsvärt — molnet håller koll.

**Se även:** [Health monitoring](#health-monitoring--hälsoövervakning)

## CLI · Kommandoradsgränssnitt

Command-Line Interface. Textbaserat verktyg för att styra molnet via terminalen. Snabbare än portalen för repetitiva uppgifter, och enkelt att automatisera i skript. Azure CLI heter `az`.

---

## Sustainability · Hållbarhet

Att använda molnresurser på ett sätt som minimerar onödig energiförbrukning. Handlar i praktiken om att inte slösa — stäng av det du inte använder, välj rätt storlek, övervaka vad som faktiskt nyttjas.

## Resource utilization · Resursutnyttjande

Hur stor andel av de betalda resurserna som faktiskt används. En server som är igång men idle (gör ingenting) har lågt resursutnyttjande — du betalar för kapacitet du inte tar tillvara.

## Over-provisioning · Överprovisionering

Att beställa mer kapacitet än du behöver — "för säkerhets skull". Vanligt misstag. Resulterar i höga kostnader och onödig energiförbrukning. Molnets flexibilitet gör att du kan börja litet och skala upp vid behov istället.

**Se även:** [Auto-scaling](#auto-scaling--automatisk-skalning), [Right-sizing](#right-sizing--rätt-storlek)

## Right-sizing · Rätt storlek

Att välja en resurs (VM, databas, tjänst) vars kapacitet faktiskt matchar behovet — varken för stor eller för liten. En överdimensionerad VM kostar lika mycket som en lagom stor, men levererar inget extra värde.

## Governance · Styrning

Policies och regler som styr hur molnresurser får användas inom en organisation. Exempel: "alla resurser måste ha en kostnadstagg", "inga offentliga IP-adresser utan godkännande". Styrning hindrar resursspridning (eng. *sprawl*) och håller kostnader och säkerhet under kontroll.
