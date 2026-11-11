# Designmönster — hur vi tänker

Kurs 3 · Fördjupad OOP i C# · CLO26 · YRGO

---

## Grundprincipen

> "Oj, jag ska ha en databas — vilka klasser behöver jag?"

| Klass | Vad den gör |
|-------|-------------|
| `Model` | Representerar en rad i databasen. En POCO-klass — bara egenskaper, inget annat. T.ex. en `Product`-klass med `Id`, `Namn` och `Pris`. |
| `DbContext` | Pratar med databasen. *Context* betyder "sammanhang" — den vet vilka tabeller som finns och hur man lägger till, hämtar och tar bort rader. EF Core:s `DbContext` är ett exempel på detta mönster. |
| `Service` | Sköter affärslogiken. *Affärslogik* = reglerna för hur appen fungerar. "En order kan inte läggas om produkten inte finns i lager" — det är affärslogik. Den bor i en Service-klass, inte i UI:t och inte i databasen. |

Allt ovan är vanliga C#-klasser. Det enda som skiljer dem åt är **vad de gör** — och vi namnger dem efter det.

Det är precis vad designmönster är: beprövade lösningar med namn som alla i teamet förstår. Istället för att förklara hela strukturen varje gång säger du "vi använder en Factory här" — och alla vet direkt hur det är tänkt.

---

## Creational — hur objekt skapas

### Singleton ⭐ du känner det

Se till att det bara finns *ett enda* exemplar av en klass i hela systemet — och att alla delar på det.

*Patrik öppnar en ny databasanslutning för varje order som läggs. En fredag kväll med hög trafik: 500 öppna anslutningar. Servern kraschar. Pizzabeställningarna slutar fungera.*

Med Singleton finns alltid bara en anslutning — alla delar på den. I ASP.NET Core: `services.AddSingleton<T>()`.

**Klasser:**
- `Databasanslutning` — den klass det bara ska finnas ett exemplar av

---

### Factory Method ★ föreläsning

Samla all kod som skapar objekt på ett enda ställe — en "fabrik" — istället för att sprida `new` över hela koden.

*Patrik på Kalles Pizzeria skriver `new SwishTjänst()` på 16 ställen. Han lägger till Klarna, missar tre. Kraschar på fredag. Med Factory Method ändrar han på ett enda ställe.*

**Klasser:**
- `BetalningsFabrik` — det enda stället med `new` — bestämmer vilket objekt som skapas
- `IBetalningsTjänst` — kontraktet (interface) som alla betalningsklasser måste följa
- `SwishTjänst`, `KortTjänst` — konkreta klasser som fabriken skapar

---

### Abstract Factory

Som Factory Method, men skapar hela *familjer* av relaterade klasser på en gång — utan att binda sig till en specifik variant.

*Emma bygger en app med ljust tema. Chefen vill ha ett mörkt tema till mobilversionen. Emma börjar kopiera knappar, formulär och ikoner — det är tio klasser, och ingen av dem matchar varandra riktigt.*

Med Abstract Factory byter hon en rad — hela temat byter sig på en gång.

**Klasser:**
- `ITemaFabrik` — kontraktet som beskriver vad fabriken kan skapa
- `LjustTemaFabrik`, `MörktTemaFabrik` — konkreta fabriker, en per variant
- `Knapp`, `Textfält` — de klasser som fabriken skapar (finns i ljus och mörk variant)

---

### Builder ★ föreläsning

Bygg upp ett komplext objekt steg för steg med en läsbar kedja av metoder — istället för en konstruktor med tio parametrar ingen förstår.

*Vad är `new Order("Sofie", null, true, false, 42)`? Ingen vet.*

`new OrderBuilder().FörKund("Sofie").MedExpress().Bygg()` — det förstår alla. `StringBuilder` i C# är Builder.

**Klasser:**
- `OrderBuilder` — sätter ihop Order steg för steg, varje metod returnerar `this` för att möjliggöra kedjan
- `Order` — det färdiga objektet som byggaren sätter ihop

---

### Prototype

Skapa nya objekt genom att kopiera ett befintligt — istället för att bygga upp det från noll varje gång.

*Carlos bygger en e-handel. Varje ny produkt skapas med hundra standardinställningar: moms, fraktklass, valuta, returregler... Han fyller i dem manuellt en i taget. Det tar tre minuter per produkt, och de flesta är identiska.*

Med Prototype klonar han en mall-produkt och ändrar bara det som skiljer sig. I C#: `ICloneable` med `Clone()`.

**Klasser:**
- `ProduktMall` — klassen som kan kopiera sig själv, implementerar `Clone()`

---

## Structural — hur klasser kopplas ihop

### Adapter

En mellanhand som översätter ett interface till ett annat — som en kontakt-adapter när du är utomlands.

*Anna ska integrera ett betalningssystem från 2008. Det pratar XML och SOAP. Hennes nya kod pratar med moderna interfaces. De förstår inte varandra — och Anna får inte ändra i det gamla systemet.*

Adaptern är tolken. Anna pratar med Adaptern, Adaptern pratar med det gamla systemet. Ingen av dem behöver ändras.

**Klasser:**
- `IBetalningsTjänst` — ditt interface, hur din kod vill prata
- `GammaltBetalningssystem` — den externa klassen med ett annat interface
- `BetalningsAdapter` — klassen som översätter, implementerar ditt interface och pratar med det externa systemet

---

### Bridge

Dela upp en klass i två delar: *vad den gör* och *hur den gör det* — så de kan ändras oberoende av varandra.

*Patrik ska skicka notiser via e-post och SMS. Han skriver `EpostNotifiering` och `SMSNotifiering`. Sen vill chefen ha "urgenta" och "normala" varianter. Nu fyra klasser. Sen kommer WhatsApp-kravet. Snart är det tolv klasser och alla är nästan identiska.*

Med Bridge: en klass för typen (urgent/normal) och en för kanalen (e-post/SMS). Koppla ihop dem fritt — aldrig fler klasser.

**Klasser:**
- `NotifieringsTjänst` — abstraktionen, den klass som koden faktiskt använder
- `INotifieringsKanal` — interfacet för hur meddelandet skickas
- `EpostKanal`, `SMSKanal` — konkreta implementationer av kanalen

---

### Composite

Behandla ett enskilt objekt och en grupp av objekt på exakt samma sätt.

*Emma bygger ett filhanteringssystem. Hon skriver en metod för att beräkna storleken på en fil — enkelt. Sen ska metoden fungera för mappar också. En mapp kan innehålla filer och andra mappar. Hur djupt som helst.*

Med Composite behandlar hon en fil och en mapp exakt likadant. Mappen räknar bara ihop sina barn — och varje barn gör samma sak.

**Klasser:**
- `IFilSystemKomponent` — kontraktet som båda typer måste följa
- `Fil` — ett lövobjekt, har inga barn
- `Mapp` — kan innehålla både filer och mappar

---

### Decorator

Lägg till nytt beteende till ett objekt utan att ändra det — som att lägga på ett extra lager utanpå.

*Sofies `OrderService` fungerar perfekt och har tio tester. Nu vill chefen att alla anrop ska loggas. Sofie vill inte röra originalkoden — risken att något går sönder är för stor.*

En Decorator omsluter `OrderService` och lägger till loggningen utanpå. Originalet märker ingenting. `BufferedStream` i C# är exakt detta — den omsluter en vanlig `Stream`.

**Klasser:**
- `IOrderTjänst` — kontraktet som båda följer
- `OrderTjänst` — originalobjektet med den faktiska logiken
- `LoggningDecorator` — omsluter OrderTjänst och lägger till loggning utan att ändra originalet

---

### Facade

Dölj ett komplext system bakom ett enkelt interface. Den som anropar behöver inte veta vad som händer bakom.

*Carlos ska lägga en order i Sofies system. Han ser att han måste anropa `LagerService`, sedan `BetalningsService`, sedan `LeveransService` — i rätt ordning, med rätt parametrar. Det är inte hans jobb att veta allt det där.*

Med Facade anropar han bara `OrderFacade.LäggOrder(order)`. Som att ringa receptionen på ett hotell — de fixar resten.

**Klasser:**
- `OrderFacade` — det enkla interfacet utåt, en metod inte tio
- `LagerService` — kontrollerar och minskar lagersaldo
- `BetalningsService` — genomför betalningen
- `LeveransService` — skapar leveransen

---

### Flyweight

Dela data som är *samma* för många objekt istället för att kopiera den överallt — sparar minne när du har tusentals liknande objekt.

*Carlos bygger ett spel med en skog av en miljon träd. Varje träd-objekt lagrar sin egen textur, färg och form. Spelet startar — och kraschar omedelbart. Uppgiftshanteraren visar 4 GB minnesanvändning.*

Med Flyweight delar alla träd av samma typ på en gemensam klass med texturen. Varje träd-objekt lagrar bara sin position. Minnet sjunker till under 400 MB.

**Klasser:**
- `TeckenFormat` — Flyweight, den delade klassen med typsnitt, storlek och stil
- `TeckenFormatFabrik` — hanterar och återanvänder Flyweight-objekt, ger tillbaka ett befintligt om det redan finns
- `Tecken` — Context, håller det unika per tecken (vilket bokstav, var på sidan)

---

### Proxy

En klass som ser ut som originalet utåt, men kontrollerar åtkomsten till det — kan lägga till cachning, säkerhetskontroll eller lazy loading.

*Emmas app hämtar produktdata från ett externt API. Varje sidladdning tar 2 sekunder. Chefen klagar. Emma vet att produkterna ändras högst en gång per timme — men appen hämtar dem hundra gånger per minut.*

En Proxy-klass svarar med cachad data när den finns. Annars frågar den API:et. Resten av koden märker ingen skillnad.

**Klasser:**
- `IDataTjänst` — kontraktet som båda klasser följer
- `DataTjänst` — originalobjektet som gör det riktiga jobbet
- `CachadDataTjänst` — proxy, kontrollerar cacheminnet innan den vidarebefordrar till originalet

---

### Repository ★ föreläsning

En klass som hanterar all kommunikation med databasen. Resten av koden frågar Repository — aldrig databasen direkt.

*Sofie byter databas från SQL Server till PostgreSQL. Med Repository ändrar hon på ett enda ställe. Utan det: veckor av jobb.*

**Klasser:**
- `IOrderRepository` — kontraktet, vad du kan göra med orders
- `SqlOrderRepository` — konkret implementation som pratar med SQL Server
- `Order` — modellklassen, representerar en rad i orders-tabellen

---

## Behavioral — hur klasser kommunicerar

### Chain of Responsibility

En kedja av klasser. Varje klass försöker hantera förfrågan — kan den det, hanterar den den. Annars skickas den vidare till nästa.

*Sofie bygger ett API. Varje request ska loggas, kontrolleras mot inloggning och sedan mot behörighet. Hon stoppar in allt i en metod — 200 rader, omöjlig att testa, omöjlig att ändra i.*

Med Chain of Responsibility: tre separata klasser, en kedja. Varje klass gör sitt och anropar `next()`. ASP.NET Core:s middleware-pipeline är exakt det här.

**Klasser:**
- `IHandler` — kontraktet för varje länk i kedjan
- `LoggningHandler` — loggar förfrågan och skickar vidare
- `AutentiseringsHandler` — kontrollerar inloggning, svarar med fel eller skickar vidare
- `BehörighetsHandler` — kontrollerar rättigheter och hanterar eller vidarebefordrar

---

### Command

Paketera en åtgärd som ett eget objekt — då kan den sparas, köas, ångras eller köras vid ett senare tillfälle.

*Anna bygger en texteditor. Chefen vill ha Ctrl+Z. Men "klistra in", "ta bort" och "formattera" är funktioner utspridda i tio klasser. Hur sparar hon dem i en lista och kör dem baklänges?*

Varje åtgärd blir ett eget Command-objekt med `Utför()` och `Ångra()`. Historiklistan håller koll. Ctrl+Z plockar det senaste och kör `Ångra()`. `ICommand` i WPF bygger på detta.

**Klasser:**
- `IKommando` — kontraktet, alla kommandon har `Utför()` och `Ångra()`
- `SkapaOrderKommando` — ett konkret kommando, vet hur man skapar en order och hur man ångrar det
- `KommandoHistorik` — håller koll på alla utförda kommandon (Ctrl+Z-listan)

---

### Interpreter

En klass som läser och förstår ett enkelt "minispråk" — t.ex. ett sökfilter, en regelformel eller ett konfigurationsuttryck.

*Patrik vill att butikspersonalen ska kunna skriva rabattregler själva i ett textfält: "om Pris > 100 OCH Kategori = 'pizza' ge 10% rabatt". Men hur tolkar koden det? Det är inte en vanlig if-sats — det är text som ska utvärderas.*

En Interpreter läser regeln och bygger upp ett träd av uttrycksobjekt som utvärderas mot varje produkt. SQL-parsers och regex-motorer fungerar på samma sätt.

**Klasser:**
- `IUttryck` — kontraktet, varje del av minispråket implementerar detta
- `OchUttryck`, `EllerUttryck` — logiska operatorer som kombinerar andra uttryck
- `Jämförelse` — ett lövobjekt, t.ex. "Pris > 100"

---

### Iterator ⭐ du känner det

Gå igenom en samling objekt ett steg i taget — utan att behöva veta hur samlingen är uppbyggd.

*Sofie har en `List<Order>`. Carlos hämtar orders från en databas. Emma läser dem från en fil. De vill alla loopa igenom orderna — men datakällan är helt olika. Ska de skriva tre olika loopar?*

Nej. `IEnumerable<T>` + `foreach` fungerar för alla tre — utan att du behöver veta vad som ligger under. Hela LINQ bygger på Iterator.

**Klasser:**
- `IEnumerable<T>` — samlingen, det du loopar över
- `IEnumerator<T>` — iteratorn, håller koll på var du är i samlingen

---

### Mediator

Istället för att klasser pratar direkt med varandra pratar de med en Mediator. Mediatorn sköter kommunikationen.

*Patriks system: när en order läggs pratar `OrderService` direkt med `LagerService`, som pratar direkt med `EpostService`, som pratar direkt med SMS-tjänsten. En ändring i ett ställe kräver ändringar på fyra andra ställen. Alla känner alla.*

Med Mediator pratar alla med Mediatorn. Ingen vet om de andra. Som ett chattrum — du skickar till chatten, chatten vidarebefordrar. MediatR i ASP.NET Core är ett exempel.

**Klasser:**
- `IMediator` — kontraktet för mediatorn
- `OrderMediator` — sköter kommunikationen, vet vem som ska ha vad
- `LagerService`, `EpostService` — kollegor, pratar via mediatorn inte direkt med varandra

---

### Memento

Spara ett ögonblicksfoto av ett objekts tillstånd så att du kan återgå till det senare.

*Anna är halvvägs igenom att redigera en stor rapport i Sofies texteditor. Hon trycker fel och allt är borta. Sofie hade inte lagt in Ctrl+Z — Anna är rasande och rapporten är förlorad.*

Med Memento sparas ett Snapshot vid varje steg. Ctrl+Z plockar det senaste och återställer. Samma princip används för spelsparning.

**Klasser:**
- `Texteditor` — objektet vars tillstånd ska sparas, kan skapa och återställa ett Snapshot
- `Snapshot` — en fryst kopia av tillståndet vid ett visst tillfälle
- `Historik` — håller koll på alla Snapshots, detta är Ctrl+Z-listan

---

### Observer ★ föreläsning

En klass meddelar "det hände något!" utan att veta vem som lyssnar. De som bryr sig prenumererar och reagerar.

*Sofies `OrderService` ska minska lagret, skicka ett bekräftelsemail och notifiera plocklistan — allt när en order läggs. Hon stoppar in allt i `LäggOrder()`. Sen kommer kravet på SMS-avisering. Sen push-notis. Metoden växer och OrderService vet om allt.*

Med Observer meddelar `OrderService` bara "en order lades" — och vem som lyssnar är inte dess problem. `event` och `delegate` i C# *är* Observer. `knapp.Click += handler` är samma sak.

**Klasser:**
- `OrderService` — publicerar event när en order läggs, vet inte om sina prenumeranter
- `event Action<Order> OrderLagd` — signalen som skickas ut
- `LagerService`, `EpostService` — prenumeranter som reagerar på eventet utan att OrderService känner till dem

---

### State

En klass beter sig olika beroende på vilket tillstånd den befinner sig i — utan ett hav av `if/else`.

*Carlos bygger orderhantering. Han skriver en stor `Hantera()`-metod: om status är "skapad" gör X, annars om "betald" gör Y, annars om "skickad"... — 300 rader, alla blandade. Sen ska han lägga till tillståndet "retur". Han vågar knappt öppna filen.*

Med State: en klass per tillstånd. `order.Betala()` gör rätt sak automatiskt — utan ett enda if.

**Klasser:**
- `Order` — klassen vars beteende ändras, håller en referens till sitt nuvarande tillstånd
- `IOrderTillstånd` — kontraktet för varje tillstånds beteende
- `SkapadOrder`, `BetaldOrder`, `SkickadOrder` — en klass per tillstånd med rätt beteende

---

### Strategy ★ föreläsning

Byt ut en algoritm runtime utan att ändra koden runt om. Algoritmen bor i en egen klass.

*Patrik ska bygga sortering för Kalles Pizzeria-meny. Varje ny sorteringstyp ger fler grenar i samma metod. Nytt krav varje vecka. Metoden som bara växer.*

`meny.OrderBy(r => r.Pris)` — lambdan du skickar in *är* strategin. Du väljer sorteringslogik utan att ändra `OrderBy`. Nytt krav? Ny klass. Rör ingenting annat.

**Klasser:**
- `ISorteringsStrategi` — kontraktet, alla sorteringsklasser måste ha metoden `Sortera()`
- `SorteraPåPris`, `SorteraPåPopularitet` — konkreta algoritmer, en klass per sorteringssätt
- `Meny` — klassen som använder strategin, tar emot vilken som helst

---

### Template Method

Definiera stegen i en algoritm i en basklass. Låt subklasserna bestämma hur *varje steg* ska utföras.

*Emma bygger en rapportgenerator — en för PDF och en för Excel. Hon kopierar koden och anpassar. Sen behöver hon ändra hur data hämtas. Nu måste hon ändra på två ställen. Nästa vecka vill chefen ha en tredje variant: CSV.*

Med Template Method: basklassen håller ordningen (hämta → formattera → exportera). PDF, Excel och CSV fyller bara i hur varje steg fungerar. Ändringen i datahämtningen görs en gång.

**Klasser:**
- `Rapportgenerator` — abstrakt basklass, definierar stegen i rätt ordning
- `HämtaData()`, `Formattera()`, `Exportera()` — abstrakta metoder som subklasserna måste implementera
- `PdfRapport`, `ExcelRapport` — konkreta klasser som fyller i hur varje steg fungerar

---

### Visitor

Utför operationer på objekt utan att ändra deras klasser. Besökaren ("visitorn") tar med sig logiken.

*Carlos har ett orderträd med `Order`, `Produkt` och `Rabatt`-klasser. Han vill beräkna totalkostnaden. Sen exportera till XML. Sen generera en skattetabell. Om han lägger all den logiken i varje klass — de är snart omöjliga att förstå.*

Med Visitor: en `KostnadsBeräknare` besöker varje objekt och räknar. En `XMLExportör` besöker och exporterar. Klasserna ändras aldrig.

**Klasser:**
- `IVisitor` — kontraktet, en metod per typ av objekt som kan besökas
- `KostnadsBeräknare`, `XMLExportör` — konkreta visitors med specifik logik
- `IElement` — kontraktet för objekt som kan ta emot en visitor, har metoden `AcceptVisitor()`

---

## Arkitekturmönster

### 3-lagersarkitektur

Dela upp koden i tre lager med tydliga ansvar. Varje lager pratar bara med lagret under sig.

```
UI / Presentation
        ↕
Affärslogik / Service
        ↕
Datakoppling / Repository / Databas
```

**Klasser:**
- `MainPage` / `OrderController` — UI-lagret, visar data och tar emot knapptryck
- `OrderService` — affärslagret, reglerna för hur appen fungerar
- `IOrderRepository`, `SqlOrderRepository` — datalagret, all kommunikation med databasen

---

### Clean Architecture

Kärnlogiken i mitten, omringad av lager. Beroenden pekar alltid *inåt* — det yttersta lagret (UI, databas) vet om det inre, men inte tvärtom. Byt ut databasen utan att röra affärslogiken.

```
[ UI / Web / API ]
  → [ Application — use cases, service-klasser ]
    → [ Domain — entiteter och affärsregler ]
    ← [ Infrastructure — db, externa tjänster ]
```

---

### MVC — Model · View · Controller

Dela upp koden i tre typer av klasser: vad som *visas* (View), hur data *ser ut* (Model) och vad som *händer* (Controller).

**Klasser:**
- `Product` — Model, POCO-klass som motsvarar en rad i databasen
- `Products.cshtml` / `ProductPage.razor` — View, hanterar det som visas för användaren: webbsidor, formulär, tabeller
- `ProductController` — Controller, förbereder det som vyn ska visa: hämtar data, kör Service-metoder, skickar tillbaka ett svar

---

### MVVM — Model · View · ViewModel

Används i Blazor och WPF. ViewModel är en mellanklass som håller tillståndet och logiken för det som visas — View binder direkt till ViewModel utan att behöva en Controller.

**Klasser:**
- `Product` — Model, data som hämtas från databasen via Repository eller Service
- `ProductPage.razor` / `.xaml` — View, det som användaren ser, binder mot ViewModels egenskaper
- `ProductViewModel` — ViewModel, håller tillståndet, hanterar knapptryck, pratar med Services. "Glue" mellan Model och View

---

### CQRS

Command Query Responsibility Segregation — separera läsoperationer från skrivoperationer. Läsning och skrivning kan skalas och optimeras oberoende av varandra.

```
Read:  QueryHandler  → ReadModel  (optimerat för visning)
Write: CommandHandler → WriteModel + Events
```

---

*★ föreläsning = fullständiga kodexempel finns i föreläsningen*  
*⭐ du känner det = du har redan använt det utan att veta om det*
