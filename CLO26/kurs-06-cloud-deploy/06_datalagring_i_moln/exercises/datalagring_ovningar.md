# Datalagring i Molnet — Övningar

## Övning 1: Välj rätt lagringstjänst

För varje scenario, välj rätt Azure-tjänst och motivera:

| Scenario | Tjänst | Varför? |
|----------|--------|---------|
| Ladda upp användarprofiler (bilder) | | |
| Cacha databasfrågor för prestanda | | |
| Hantera en kö av email som ska skickas | | |
| Strukturerad relationsdata med ACID | | |
| Enkel key-value-lagring för 10M poster | | |
| Global NoSQL-databas med 99.999% SLA | | |
| Loggfiler som används sällan | | |

---

## Övning 2: Skapa en Storage Account (Azure CLI)

Skriv Azure CLI-kommandon för att:

1. Skapa en resource group
2. Skapa en storage account (LRS, Hot tier)
3. Skapa en blob container med namnet "bilder"
4. Ladda upp en bildfil till containern
5. Lista alla blobs i containern

```bash
# Skriv dina kommandon här:
```

---

## Övning 3: SAS Token

Skapa en SAS-token i C# för att ge tillfällig läsåtkomst:

```csharp
// Generera en SAS-token som:
// - Gäller i 1 timme
// - Ger read-behörighet till en specifik blob
// - Bara från en specifik IP

// Din kod här:
```

**Fråga:** Varfär är SAS-tokens säkrare än att göra en container publik?

---

## Övning 4: Azure SQL Database

Du ska designa en databas för en webbshop:

1. Skapa en Azure SQL Database (CLI eller portal)
2. Designa tabeller för: Kund, Produkt, Order, Orderrad
3. Skapa connection string för din app
4. Anslut från C# med EF Core eller ADO.NET

```sql
-- Skapa dina tabeller här:
```

**Viktigt:** Tänk på säkerhet — använd Managed Identity istället för lösenord i connection string.

---

## Övning 5: Välj mellan datalagring

Du bygger en app där användare kan:
- Ladda upp bilder på sina produkter (max 10 MB/st)
- Spara produktdata med kategorier och priser
- Cacha topplistan (visas 10 000 gånger/min)
- Skicka notiser vid nya ordrar
- Söka efter produkter på titel

**För varje behov:**
1. Välj Azure-tjänst
2. Motivera varför (och varför inte alternativen)
3. Uppskatta kostnad (gratis/fri/medium/dyr)

---

## Övning 6: Reflektion

1. När skulle du välja Blob Storage över Azure SQL Database?
2. Vad är skillnaden mellan Hot, Cool, Cold och Archive tiers?
3. Varför använda Redis Cache istället för att bara läsa från databasen?
4. Vad är Cosmos DB bra för? Och mindre bra för?
