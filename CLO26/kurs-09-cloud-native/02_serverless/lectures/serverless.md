# Serverless — det finns servrar, men de är inte ditt problem

Tänk dig att du beställer pizza. Du behöver inte äga ugnen, köpa ugnen, starta ugnen eller stänga av den när du är klar. Du ringer. Pizzan dyker upp. Ugnen är någon annans bekymmer.

Det är serverless.

Servrar finns förstås fortfarande. Någonstans i ett datacenter snurrar hårdvara och håller igång din kod. Men du ser den inte, sköter den inte och betalar inte för den när du inte använder den. Det är den stora grejen — och det förändrar ganska mycket i hur vi designar system.

---

## FaaS och BaaS — två smaker av samma idé

Serverless brukar delas in i två läger.

**FaaS — Function as a Service** handlar om att du skickar upp en enda funktion till molnet. Den körs när något triggar den — ett HTTP-anrop, en tidsinställning, ett meddelande i en kö. Sedan är den borta igen. Azure Functions är ett typexempel, och det är vad vi fokuserar på den här veckan.

**BaaS — Backend as a Service** är lite bredare. Här konsumerar du hela tjänster utan att bygga dem: autentisering, databaser, fillagring. Firebase är ett klassiskt exempel. Skillnaden mot FaaS är att du inte skriver logiken — den finns redan, du integrerar mot den.

---

## Azure Functions: triggers och bindings

I Azure Functions är en *trigger* det som väcker din funktion till liv. Några du kommer att träffa:

- **HTTP-trigger** — någon anropar en URL och din funktion svarar. Klassisk REST-endpoint, serverless-stil.
- **Timer-trigger** — kör varje natt klockan 02:00, oavsett om du sover eller inte. Cron-uttryck styr schemat.
- **Queue-trigger** — ett meddelande dyker upp i en Azure Storage Queue och din funktion plockar upp det.
- **Blob-trigger** — en fil laddas upp till Azure Storage och din funktion reagerar direkt.

Utöver triggers finns *bindings* — ett sätt att koppla funktionen mot andra resurser utan boilerplate. Du tar emot ett meddelande från en kö och skriver ett svar direkt till en blob, och Azure sköter kopplingarna. Funktionen håller fokus på logiken.

---

## Cold start — vad är det och när spelar det roll?

Din funktion sover när ingen använder den. Första gången någon triggar den måste den vakna: kod laddas, miljön initialiseras, allting startar upp. Det kallas *cold start* och kan ta allt från en halv sekund till ett par sekunder beroende på språk och ramverk.

För en bakgrundsprocess som körs varje natt är det totalt ointressant. Ingen märker av ett par sekunders uppstartstid klockan tre på natten. Men för en API-endpoint där en användare sitter och väntar — då kan det kännas som en evighet. Cold start är inte ett problem i sig, det är ett problem i fel kontext.

---

## Vad kostar det?

Azure Functions har tre prismodeller, och skillnaden är stor.

**Konsumtionsplan** är den klassiska serverless-modellen. Du betalar per anrop och per exekveringstid. Noll trafik = noll kostnad. Perfekt för saker som körs sällan eller oregelbundet. Cold starts förekommer här.

**Premium-plan** håller instanser varma hela tiden — inga cold starts, snabb respons, men du betalar dygnet runt oavsett trafik. Väljer du premium betalar du för snabbheten.

**Dedikerad plan** är i praktiken en vanlig App Service. Du hyr en server. Tekniskt sett kan du köra funktioner där, men det är inte riktigt serverless i andan.

---

## Serverless passar inte alltid

Det är lätt att bli förälskad. Betala bara för det du använder! Automatisk skalning! Inga servrar att sköta! Men det finns verkliga begränsningar du måste känna till.

Funktioner är *stateless* — de minns ingenting mellan anrop. Behöver du dela tillstånd måste det lagras externt i en databas eller cache. Dessutom är exekveringstiden begränsad: standardgränsen är 5 minuter på konsumtionsplanen, max 10. Långkörande processer passar inte in.

Sen finns vendor lock-in. När du väl byggt allt i Azure Functions är det inte trivialt att flytta till AWS Lambda eller Google Cloud Functions. API:er, triggers och konfigurationsformat skiljer sig. Du är inte fångad, men det kostar att byta.

Summan av kardemumman: serverless är ett utmärkt verktyg för händelsedrivna, kortlivade och stateless uppgifter. Långkörande processer, stateful workflows och saker som kräver delat minne — det är inte serverlessens hemmaplan.

**Du har mer koll på serverless nu än du tror. Kör hårt!**

---

*Av Marcus Ackre Medina — Nion Education — marcus.medina@nionit.com*
