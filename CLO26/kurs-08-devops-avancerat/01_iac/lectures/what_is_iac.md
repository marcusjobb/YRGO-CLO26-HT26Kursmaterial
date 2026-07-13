# Vad är Infrastructure as Code?

Du har konfigurerat allt i portalen. Det tog dig tre timmar — rätt region, rätt storlek på VM:en, rätt nätverksinställningar, rätt brandväggsregler. Allt sitter. Miljön funkar.

Sedan ringer kollegan. Hon ska sätta upp exakt samma miljö för testteamet. Kan du skicka en guide?

Och där börjar problemet.

Du skriver ihop en lista med skärmdumpar och steg-för-steg-instruktioner. Kollegan följer dem, men hittar inte rätt knapp eftersom Azure uppdaterat portalen sedan du tog bilderna. Och ett av stegen är otydligt — det stod "välj storlek" men du menade Standard_B2s, inte Standard_B2ms. Liten skillnad. Stor kostnad.

Det här kallas **click-ops** — och det är ett av de vanligaste problemen i moderna driftorganisationer. Infrastruktur som konfigureras manuellt är svår att reproducera, enkel att göra fel på, och omöjlig att versionera.

## Lösningen: infrastruktur som kod

Infrastructure as Code (IaC) innebär att du beskriver din infrastruktur i textfiler istället för att klicka runt i ett webbgränssnitt. Du definierar vad du vill ha — ett virtuellt nätverk, tre virtuella maskiner, en lastbalanserare — och ett verktyg skapar det åt dig.

Filen är källkoden. Miljön är resultatet.

Det gör att du kan göra saker som är omöjliga med click-ops:

- Köra samma konfiguration i tio miljöer utan att missa ett steg
- Se exakt vad som förändrades i infrastrukturen sedan förra veckan, via git
- Återskapa en kraschad miljö på minuter istället för timmar

## Deklarativt eller imperativt

Det finns två sätt att tänka på IaC.

**Deklarativt**: du beskriver vad du vill ha. "Jag vill ha ett virtuellt nätverk med tre undernät och en gateway." Verktyget tar reda på hur det ska skapas och i vilken ordning.

**Imperativt**: du beskriver hur det ska göras. "Skapa ett virtuellt nätverk. Lägg till undernät. Koppla in gateway." Du styr ordningen och stegen manuellt.

Terraform och Bicep är deklarativa. Du skriver ett tillstånd — verktyget räknar ut skillnaden mot det nuvarande tillståndet och gör precis det som krävs för att nå dit. Ansible är mer imperativt och används oftast för konfiguration av servrar snarare än skapande av infrastruktur.

## Vilket verktyg passar när?

**Terraform** är det mest utbredda verktyget och fungerar mot i princip alla molnleverantörer. Bra val när du vill ha portabilitet eller redan har en blandad miljö.

**Bicep** är Microsofts eget deklarativa språk för Azure och är enklare att läsa än de äldre ARM-mallarna som det ersätter. Bra val när du jobbar renodlat i Azure.

**ARM templates** är föregångaren till Bicep — JSON-format, verbose, fortfarande vanligt i äldre projekt men sällan rätt val i ny kod.

**Ansible** används för att konfigurera servrar som redan finns: installera paket, justera inställningar, distribuera applikationer. Kompletterar ofta Terraform snarare än konkurrerar med det.

**Pulumi** låter dig skriva infrastruktur i vanliga programmeringsspråk som Python eller TypeScript. Bra om ditt team är starka i kod och vill undvika ett eget konfigurationsspråk.

## GitOps — infrastruktur i versionshantering

Om infrastrukturkoden lever i ett git-repo gäller samma arbetsflöde som för applikationskod: pull requests, code review, automatisk deploy vid merge. Det kallas GitOps.

Det innebär att en ny brandväggsregel behandlas precis som en ny feature. Någon skriver koden, någon annan granskar den, och när den är godkänd rullas den ut automatiskt. Inget som händer i produktion existerar utanför versionshistoriken.

## Idempotens — kör det igen, och igen

En central egenskap i bra IaC är idempotens: du kan köra samma kod tio gånger och få exakt samma resultat. Finns resursen redan skapas den inte igen — den lämnas som den är. Skiljer den sig från det önskade tillståndet justeras den.

Det gör det säkert att automatisera. Ingen rädsla för att "vad händer om vi kör det här fast miljön redan finns?" Svaret är: ingenting händer — om allt redan är rätt.
