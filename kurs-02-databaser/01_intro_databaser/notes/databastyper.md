# Vilken databas väljer du? — SQLite, MySQL och PostgreSQL

## Varför valet spelar roll

I C# är det tekniskt enkelt att byta databas — du ändrar connection string och provider:

```csharp
optionsBuilder.UseSqlite("Data Source=shop.db");
optionsBuilder.UseMySql("Server=localhost;...", ...);
optionsBuilder.UseNpgsql("Host=localhost;...");
```

Men valet av databasmotor påverkar mycket mer än en rad kod:

- Hur du deployar (fil vs server vs molntjänst)
- Hur många användare som kan skriva samtidigt
- Vad du kan lagra och söka på
- Vad det kostar i drift

Det handlar aldrig om vilken databas som är *bäst* — det handlar om vilken som passar *situationen*.

---

## SQLite — ingen server, bara en fil

Du har redan använt SQLite i kursen. Hela databasen är en enda fil på disk. Ingen separat serverprocess behöver köras — applikationen pratar direkt med filen.

```csharp
optionsBuilder.UseSqlite("Data Source=shop.db");
```

**Passar för:** utveckling, prototyper, mindre appar, mobilappar, enkla skolprojekt.

Anledningen till att vi börjar med SQLite är enkel: noll friktion. Du skapar en fil och kör. Inget att installera, inget att starta, inget som kan gå fel i miljön.

### Begränsningarna

SQLite klarar inte allt:

- **En skrivare i taget.** Flera parallella skrivningar krockar — inte ett problem i ett litet projekt, men kritiskt i en produktionsapp.
- **Ingen nätverksåtkomst.** Filen måste finnas lokalt. Du kan inte koppla flera servrar mot samma SQLite-fil.
- **Ingen inbyggd användarhantering.** Vem som helst med tillgång till filen kan läsa allt.

**Tumregel:** så fort flera användare behöver skriva samtidigt över ett nätverk — dags att byta.

---

## MySQL — servern vi kör i produktion

MySQL är en serverbaserad databas. En separat process kör konstant och lyssnar på anslutningar. Applikationer kopplar upp sig via nätverket och ställer frågor.

```bash
docker run --name mysql-dev -e MYSQL_ROOT_PASSWORD=hemligt \
    -p 3306:3306 -d mysql:8
```

**Passar för:** produktionsappar, team som delar databas, "vanliga" webbappar.

Fördelarna mot SQLite:
- Klarar tusentals parallella skrivningar utan att krocka
- Brett stöd — de flesta hostingleverantörer och molntjänster pratar MySQL
- Användarhantering och behörighetskontroll inbyggt

MySQL installeras i kurs-02. Det är den databasmotor vi kör mot produktion under resten av kursen.

---

## PostgreSQL — den vi resonerar om men inte kör

PostgreSQL är serverbaserad precis som MySQL, men med några skillnader som kan göra stor skillnad beroende på projekt:

- **Striktare typsystem.** Mindre tyst konvertering — färre överraskningar i produktion.
- **Följer SQL-standarden hårdare.** Mer förutsägbart beteende mellan verktyg och versioner.
- **Avancerade datatyper inbyggt.** Riktigt bra JSON-stöd, arrays, geodata — utan tilläggsplugins.
- **Vanlig i molnmiljöer.** Azure Database for PostgreSQL, Google Cloud SQL, Supabase — alla bygger på Postgres.

**Passar för:** projekt som ska växa, komplexa datamodeller, eller när molnplattformen pekar mot Postgres.

Vi kör inte Postgres praktiskt i kursen, men du ska kunna resonera om när du *borde* välja den.

---

## Jämförelsetabell

| Motor | Serverkrav | Bäst för | Status i denna kurs |
|-------|-----------|----------|----------------------|
| **SQLite** | Nej — bara en fil | Utveckling, prototyper, enkla appar | Används genomgående |
| **MySQL** | Ja — serverprocess | Produktion, team, webbappar | Installeras kurs-02 |
| **PostgreSQL** | Ja — serverprocess | Växande projekt, komplex data, molnmiljöer | Nämns, används ej |

Ingen av dem är fel. Frågan är alltid: vad kräver just det här projektet?

---

## Tre frågor som styr valet

Ställ dig dessa innan du väljer databasmotor i ett nytt projekt:

1. **Hur många skriver samtidigt?**
   En person → SQLite räcker. Flera parallellt → server krävs.

2. **Var körs appen?**
   Din dator lokalt → SQLite funkar. Molnet eller produktion → MySQL eller Postgres.

3. **Hur komplex är datan?**
   Enkla tabeller → vilken som helst. JSON-fält, arrays, strikt typning → Postgres vinner ofta.

---

## Öva: tre scenarion

Ta ställning till vart och ett — välj databas och motivera varför:

1. **Ett skolprojekt** du och en kompis kodar på under en helg.
2. **En produktionsapp** med 10 000 användare som skriver samtidigt, dygnet runt.
3. **Ett API i Azure** som lagrar strukturerad data med en del JSON-fält, och som ska skala upp om ett år.

Det räcker inte att svara vilket — du måste kunna motivera valet utifrån de tre frågorna ovan.
