# Skalning och lastbalansering i Azure

Tänk dig att du driver en pizzarestaurang. En fredag kväll börjar kön slingra sig ut på gatan. Du har två alternativ: byta ut din kassör mot en som jobbar tre gånger snabbare, eller öppna tre kassor till. Det är skillnaden mellan **vertikal** och **horisontell** skalning — och det är en av de viktigaste designbesluten du tar när du bygger molnapplikationer.

## Vertikal vs horisontell skalning

**Vertikal skalning** (scale up) innebär att du ger din befintliga server mer resurser — fler CPU-kärnor, mer RAM, snabbare disk. Det är enkelt att förstå och kräver inga ändringar i koden. Problemet är att det finns ett tak: en server kan bara bli så stor, och under uppgraderingen är tjänsten oftast nere. Det är din supersport-kassör — otroligt effektiv, men det finns bara en av hen.

**Horisontell skalning** (scale out) innebär att du lägger till fler instanser av samma server som kör parallellt. Trafiken delas upp mellan dem. Det är dyrare att designa för, men belöningen är enorm: du kan i teorin skala i det oändliga, och om en instans kraschar tar de andra över. Det är dina tre kassor — var och en gör samma sak, men ihop hanterar de tio gånger fler kunder.

I moderna molnarkitekturer väljer man nästan alltid horisontell skalning som strategi. Vertikal skalning används som ett kortsiktigt nödläge.

## Azure Load Balancer vs Application Gateway

När du har flera instanser behöver du något som fördelar trafiken mellan dem. Azure erbjuder två huvudalternativ, och vilket du väljer beror på vad du behöver se.

**Azure Load Balancer** opererar på **lager 4** (transport-lagret) i OSI-modellen. Det innebär att den ser TCP/UDP-paket — IP-adresser och portar — men ingenting om vad som faktiskt skickas inuti. Den är extremt snabb och billig, och passar perfekt för infrastruktur-trafik: databaskopplingar, interna mikrotjänster, allt där du behöver rå prestanda utan att inspektera innehållet.

**Application Gateway** opererar på **lager 7** (applikationslagret). Den förstår HTTP och HTTPS. Det innebär att den kan fatta beslut baserat på URL:er, HTTP-headers, cookies och domännamn. Du kan dirigera `/api/*` till ett kluster av API-servrar och `/static/*` till ett separat kluster med statiska resurser. Du kan terminera TLS, aktivera Web Application Firewall, och göra cookie-baserad session affinity. Priset är högre latens och kostnad jämfört med Load Balancer.

Tumregel: om du behöver intelligenta routingbeslut baserade på innehållet — välj Application Gateway. Om du bara behöver fördela last snabbt och billigt — välj Load Balancer.

## Health probes — när en instans inte svarar

Varken Load Balancer eller Application Gateway är blinda. Båda konfigureras med **health probes**: regelbundna kontroller som frågar varje instans "mår du bra?". Det kan vara ett HTTP GET mot `/health` som förväntas returnera 200 OK, eller en enkel TCP-kontroll mot en port.

Om en instans inte svarar inom konfigurerad tid, eller svarar med fel, markeras den som ohälsosam och tas ur rotationen. Trafik slutar skickas dit tills den svarar korrekt igen. Det är hela poängen: du vill aldrig att en användare träffar en trasig instans. Health probes är det automatiska skyddslagret som ser till att det inte händer.

## Autoscale: reaktiv och schemalagd

Azure kan skala antalet instanser automatiskt baserat på två principer.

**Reaktiv autoscale** (metric-based) reagerar på vad som händer just nu. Du sätter upp regler som "om CPU-användning överstiger 70% i fem minuter — lägg till två instanser" och "om CPU-användning understiger 30% i tio minuter — ta bort en instans". Det fungerar bra för oförutsägbar last, men det finns en inbyggd fördröjning: en ny instans tar tid att starta och värma upp. Du reagerar alltid lite för sent.

**Schemalagd autoscale** är smartare när lasten är förutsägbar. Du vet att varje måndag klockan 08:00 loggar hundratals studerande in i er lärplattform. Istället för att vänta på att CPU:n ska klättra, ber du Azure skala upp till tio instanser klockan 07:45. Du är redo innan trycket kommer. Det är din pizzarestaurang som öppnar extrakassorna en halvtimme innan fredagsrushen, inte mitt i den.

I praktiken kombinerar du ofta båda: schemalagd skalning för känd periodisk last, reaktiv som säkerhetsnät för det oväntade.

## Stateless-kravet och sessionsproblemet

Här är fällan som många går i när de börjar skala horisontellt. En användare loggar in och din server skapar en session — ett objekt i serverns minne som håller reda på att just den här personen är inloggad. Nästa request från samma användare hamnar hos en annan instans. Den instansen vet ingenting om sessionen. Användaren kastas ut och tvingas logga in igen.

Det är inte ett bugg i Azure — det är ett fundamentalt problem med att lagra tillstånd i serverminnet när du har flera servrar.

Lösningen är att göra dina tjänster **stateless**: varje request ska bära med sig all information som behövs, oberoende av vilken instans som hanterar den. Det finns två vanliga tillvägagångssätt.

**Azure Cache for Redis** är en delad, distribuerad cache utanför dina instanser. Sessionen lagras inte i serverminnet utan i Redis, och alla instanser har tillgång till samma data. Det fungerar precis som tidigare — men nu kan vilken instans som helst hitta sessionen.

**JWT (JSON Web Tokens)** löser problemet på ett helt annat sätt: sessionen existerar inte på servern alls. All information kodas in i en kryptografiskt signerad token som klienten bär med sig i varje request. Servern verifierar signaturen, läser ut informationen, och behöver aldrig slå upp något. Varje request är helt självständig.

Stateless-design är inte bara en teknisk detalj — det är en arkitekturprincip som avgör om din applikation faktiskt kan skalas när det verkligen behövs.
