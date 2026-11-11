---
marp: true
theme: default
class: invert
paginate: true
---

# Vilken databas väljer du? — SQLite, MySQL och PostgreSQL

**Kurs:** Databashantering och -design
**Modul:** 01 — Introduktion till databaser

---

## Vad ska vi lära oss idag?

- **Varför valet av databasmotor spelar roll** — inte "vilken är bäst" utan "vilken passar situationen"
- **SQLite** — repetition, när den räcker och när den inte gör det
- **MySQL** — serverbaserad, produktion, redan i kursen
- **PostgreSQL** — den vi inte kör men måste kunna resonera om
- **Jämförelsetabell** — snabb överblick
- **Beslutsövning** — du väljer, du motiverar

---

## Varför bry sig? Det är "bara" en connection string...

I C# byter du databas genom att byta provider:

```csharp
optionsBuilder.UseSqlite("Data Source=shop.db");
optionsBuilder.UseMySql("Server=localhost;...", ...);
optionsBuilder.UseNpgsql("Host=localhost;...");
```

Tekniskt enkelt. **Men valet påverkar:**
- Hur du deployar (fil vs server vs molntjänst)
- Hur många samtidiga användare appen klarar
- Vad du kan fråga efter (JSON, avancerade datatyper)
- Vad det kostar i drift

---

## SQLite — snabb repetition

Du har redan använt den i kursen.

- **Filbaserad** — hela databasen är en fil, ingen server
- **Serverless** — inget att installera, inget att starta
- **Perfekt för:** utveckling, prototyper, mindre appar, mobilappar, enkla skolprojekt

```csharp
optionsBuilder.UseSqlite("Data Source=shop.db");
```

Det är därför vi börjar där i den här kursen — noll friktion, direkt igång.

---

## SQLite — var den tar slut

- **En skrivare i taget** — flera samtidiga skrivningar krockar
- **Ingen nätverksåtkomst** — filen måste finnas lokalt, du kan inte koppla flera servrar mot den
- **Ingen inbyggd användarhantering** — vem som helst med filåtkomst kommer åt allt

**Tumregel:** så fort flera användare ska skriva samtidigt över nätverk — dags att byta.

---

## MySQL — servern vi installerar den här veckan

Se installationsguiden: `2_mysql_instalation.md`.

- **Serverbaserad** — separat process, du ansluter över nätverket
- **Klarar många samtidiga användare** utan att krocka
- **Brett stöd** — de flesta hostingleverantörer, molntjänster och verktyg pratar MySQL
- **Docker-vänlig** — en rad kod och du har en server igång

```bash
docker run --name mysql-dev -e MYSQL_ROOT_PASSWORD=hemligt \
    -p 3306:3306 -d mysql:8
```

**Perfekt för:** produktionsappar, team som delar databas, "vanliga" webbappar.

---

## PostgreSQL — den vi inte kör men bör känna till

Serverbaserad precis som MySQL, men med några skillnader som spelar roll:

- **Striktare typsystem** — mindre "tyst konvertering", färre överraskningar
- **Följer SQL-standarden hårdare** — mer förutsägbart mellan olika verktyg
- **Avancerade datatyper inbyggt** — riktigt bra JSON-stöd, arrays, geodata
- **Vanlig i molnmiljöer** — t.ex. Azure Database for PostgreSQL

**Perfekt för:** projekt som växer, komplexa datamodeller, eller när molnplattformen redan pekar dig mot Postgres.

---

## Jämförelsetabell

| Motor | Serverkrav | Bäst för | Exempel i denna kurs |
|-------|-----------|----------|----------------------|
| **SQLite** | Ingen — bara en fil | Utveckling, prototyper, mindre appar | Genomgående i kursen |
| **MySQL** | Ja — egen serverprocess | Produktion, team, "vanliga" webbappar | Installeras denna vecka |
| **PostgreSQL** | Ja — egen serverprocess | Växande projekt, komplex data, molnmiljöer | Nämns, används ej praktiskt |

Ingen av dem är "fel" — frågan är alltid: **vad kräver just det här projektet?**

---

## Så resonerar du i praktiken

Ställ dig tre frågor innan du väljer:

1. **Hur många skriver samtidigt?** En person → SQLite räcker. Flera → server krävs.
2. **Var körs appen?** Din egen dator → SQLite funkar fint. Molnet/produktion → MySQL eller Postgres.
3. **Hur komplex är datan?** Enkla tabeller → vilken som helst. JSON, arrays, strikt typning → Postgres vinner ofta.

---

## Prova själv

Tre scenarion. Välj databas för vart och ett — och **motivera varför**, inte bara vilken:

1. Ett **skolprojekt** du och en kompis kodar på under en helg.
2. En **produktionsapp** med 10 000 användare som skriver samtidigt, dygnet runt.
3. Ett **API i Azure** som lagrar strukturerad data med en del JSON-fält, och som ska skala upp om ett år.

Diskutera i par i 5 minuter — sen tar vi två grupper i helklass.

---

## Sammanfattning

- ✅ Valet av databasmotor handlar om situationen, inte om "bäst"
- ✅ SQLite = ingen server, perfekt för utveckling — men en skrivare i taget
- ✅ MySQL = serverbaserad, redan vår produktionsdatabas i modul 06
- ✅ PostgreSQL = striktare typsystem, stark i molnmiljöer, bra när projektet växer
- ➡️ Nästa gång: vi kopplar C# mot databasen med riktiga queries
