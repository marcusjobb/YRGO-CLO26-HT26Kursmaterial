# Övning — CosmosDB via portalen

🟡

## Vad du ska göra

Provisionera ett Azure Cosmos DB-konto med MongoDB API och lagra jobbansökningar i en databas du sätter upp själv.

## Förutsättningar

- Åtkomst till Azure Portal med ett aktivt konto
- En resursgrupp du kan använda (skapa en ny om du inte har en)
- Grundläggande förståelse för vad en databas och en collection är

## Steg

### 1. Skapa ett Cosmos DB-konto

Gå till Azure Portal och sök efter **Azure Cosmos DB** i sökfältet högst upp.

Klicka på **Skapa** och välj sedan **Azure Cosmos DB for MongoDB**. Det är MongoDB API vi använder — det innebär att du kommunicerar med databasen med MongoDB-syntax, men lagringen sköts av Azure bakom kulisserna.

Fyll i fälten:

- **Resursgrupp:** välj din befintliga eller skapa en ny
- **Kontonamn:** `jobbloggen-[ditt-namn]` (måste vara globalt unikt i Azure)
- **Plats:** West Europe
- **Kapacitetsläge:** välj **Serverless**

Serverless innebär att du bara betalar för de anrop du faktiskt gör — ingen kostnad när ingenting händer. Det är rätt val för en app med ojämn trafik, till exempel en jobblogg som du uppdaterar ett par gånger i veckan.

Lämna övriga inställningar på standard och klicka **Granska + skapa**, sedan **Skapa**.

Det tar ungefär 2–4 minuter. Azure skapar kontot i bakgrunden.

---

### 2. Skapa databasen och collectionen

Gå till ditt nya Cosmos DB-konto när distributionen är klar.

Klicka på **Data Explorer** i vänstermenyn.

Klicka på **New Collection**. Ett formulär visas till höger.

Fyll i:

- **Database name:** `jobbloggen_db` — välj **Create new**
- **Collection name:** `ansokningar`
- **Shard key:** `/status`

Varför `/status` som shard key? Cosmos DB distribuerar data baserat på shard key. Du har ett begränsat antal statusvärden (`skickad`, `intervju`, `avslag`) men varje status kan ha massor av ansökningar. Det gör `/status` till ett rimligt val för den här övningens storlek. I ett riktigt system med miljoner dokument skulle du behöva tänka noggrannare på det.

Klicka **OK**.

---

### 3. Infoga testdokument

Du är fortfarande i Data Explorer. Expandera `jobbloggen_db` → `ansokningar` i vänster träd och klicka på **Documents**.

Klicka på **New Document**.

Klistra in detta dokument och klicka **Save**:

```json
{
  "jobbTitle": "Backend-utvecklare",
  "företag": "Knowit",
  "datum": "2026-03-10",
  "status": "intervju",
  "anteckningar": "Teknisk intervju vecka 12. Ta upp erfarenheter av REST API."
}
```

Skapa ett nytt dokument och klistra in:

```json
{
  "jobbTitle": "Cloud Engineer",
  "företag": "Advania",
  "datum": "2026-03-14",
  "status": "skickad",
  "anteckningar": "Sökte via LinkedIn. Kräver Azure-certifiering."
}
```

Skapa ett tredje dokument:

```json
{
  "jobbTitle": "DevOps-konsult",
  "företag": "CGI",
  "datum": "2026-02-28",
  "status": "avslag",
  "anteckningar": "Fick avslag utan motivering. Sökte igen inte."
}
```

Varje dokument du sparar får ett automatgenererat `_id`-fält av MongoDB. Det är databasens sätt att ge varje dokument en unik identifierare.

---

### 4. Hämta connection string

Du behöver connection string för att ansluta en applikation till databasen senare.

Gå till **Connection strings** i vänstermenyn (under Settings).

Kopiera **Primary Connection String**. Den ser ut ungefär så här:

```
mongodb://jobbloggen-[ditt-namn]:NYCKEL==@jobbloggen-[ditt-namn].mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb...
```

Spara den på ett säkert ställe — behandla den som ett lösenord. Den ger full skrivåtkomst till hela ditt konto.

---

### 5. Utforska Data Explorer med filter

Gå tillbaka till **Data Explorer** → `ansokningar` → **Documents**.

I filterfältet längst upp skriver du:

```json
{"status": "intervju"}
```

Klicka **Apply Filter**. Du ska nu bara se ansökan till Knowit.

Prova sedan:

```json
{"status": "avslag"}
```

Prova ett filter på jobbTitle:

```json
{"jobbTitle": "Cloud Engineer"}
```

Data Explorer kör MongoDB-querysyntax direkt i portalen — praktiskt för att snabbt kolla vad som finns i databasen utan att skriva en rad kod.

## Kontrollpunkter

Innan du är klar — kontrollera att du har gjort allt på listan:

- [ ] Cosmos DB-kontot är skapat med **MongoDB API** och **Serverless**
- [ ] Databasen `jobbloggen_db` och collectionen `ansokningar` finns i Data Explorer
- [ ] Tre testdokument med rätt fält är insatta och synliga
- [ ] Du har kopierat och sparat connection string
- [ ] Du har kört minst ett filter i Data Explorer och fått korrekt filtrerat resultat

---

Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
