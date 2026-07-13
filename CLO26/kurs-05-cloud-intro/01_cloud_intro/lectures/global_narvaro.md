# Global närvaro — molnet finns överallt

## Du behöver inte ens åka dit

En av de coolaste sakerna med molnet är att du kan starta en server i Sydney på tio sekunder — utan att lämna soffan. Och den servern är *fysiskt* i Sydney, i ett av AWS eller Azures datacenter. Inte på din laptop någonstans i Sverige.

Det här är global närvaro. Och det förändrar allt.

## Regioner

Molnleverantörerna bygger datacenter i kluster som kallas *regioner*. Varje region är ett geografiskt område med en eller flera datacenter. Azure har över 60 regioner världen över. AWS har över 30.

När du väljer region väljer du var din data fysiskt finns. Det spelar roll för:
- **Latens**: vill du att dina användare i Europa ska få snabb respons? Välj en europeisk region.
- **Lagkrav**: GDPR kräver att vissa data stannar inom EU. Då kan du inte köra i USA.
- **Kostnad**: olika regioner har olika priser.

## Tillgänglighetszoner

Inom varje region finns *availability zones* — separata datacenter med egen ström, eget nätverk, egna grejer. Om en zon brinner ner (bokstavligen eller bildligt) fortsätter de andra.

Så du kan bygga system som är *fault tolerant* — om en server går ner i zon A fortsätter den i zon B. Användarna märker inget.

## Edge locations

Utöver regionerna finns *edge locations* — små noder som cachar innehåll nära användarna. Det är därför Netflix funkar även om deras servrar står i Virginia. Din film cachas på en edge node nära dig.

## Varför bry sig?

För att det är lätt att glömma. Man startar en server, den finns någonstans, man tänker inte mer på det. Men global närvaro är en superkraft om du använder den rätt.

Vill du nå användare över hela världen? Välj rätt regioner. Vill du vara säker på att din app överlever ett strömavbrott? Använd availability zones.

Och kom ihåg: molnet är inte *ingenstans*. Det är *överallt*.
