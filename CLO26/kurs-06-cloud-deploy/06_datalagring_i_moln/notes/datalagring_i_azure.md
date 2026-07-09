# Datalagring i Azure

## Översikt

Azure erbjuder många datalagringstjänster. Valet beror på datatyp, skalbarhetsbehov och budget.

## Azure Storage Account

En storage account innehåller fyra tjänster:

| Tjänst | Typ | Användning |
|--------|-----|------------|
| **Blob Storage** | Ostrukturerad data | Bilder, videor, backups, loggar |
| **File Storage** | SMB-share | Nätverksdisk, lift-and-shift |
| **Queue Storage** | Meddelandekö | Asynkron kommunikation |
| **Table Storage** | NoSQL key-value | Enkel lagring för stora datamängder |

### Blob Storage Tiers

| Tier | Kostnad/GB | Åtkomstkostnad | Bäst för |
|------|-----------|---------------|----------|
| Hot | Högst | Lägst | Aktiv data |
| Cool | Mellan | Mellan | Data som används ibland |
| Cold | Låg | Hög | Arkivdata |
| Archive | Lägst | Högst | Långtidsarkivering |

## Azure SQL Database

Hanterad SQL Server — samma T-SQL som lokalt.

**Fördelar mot egen SQL Server:**
- Inbyggd backup (point-in-time restore)
- Automatic tuning
- Hot patching
- Lätt att skala upp/ner

## Cosmos DB

Global NoSQL-databas med SLA på 99.999% för läsning.

**API-val:**
- **SQL API** — vanligast för .NET-utvecklare
- **MongoDB API** — om du kommer från MongoDB
- **Cassandra API** — för storskalig kolumnlagring
- **Gremlin API** — grafdatabaser

## Redis Cache

Azure Redis är en hanterad Redis-server för caching.

**Användningsområden:**
- Cacha databasfrågor (minska belastning)
- Session state (istället för in-memory)
- SignalR backplane (realtidskommunikation)
- Rate limiting

## Välja rätt datalagring

1. **Relationsdata, ACID-krav** → Azure SQL Database
2. **Ostrukturerade filer** → Blob Storage
3. **Cache** → Redis
4. **Global distribution, hög skalbarhet** → Cosmos DB
5. **Meddelandekö** → Queue Storage
6. **Serverless databas** → Azure SQL Serverless

## Viktigaste lärdomarna

- Azure Storage är basen för det mesta — blob, file, queue, table
- Välj storage tier baserat på hur ofta data nås
- Azure SQL Database är hanterad SQL — samma syntax, mindre drift
- Cosmos DB är för global skalning, inte för enkla CRUD-appar
- Redis gör allt snabbare — cach:a ofta, men ogiltigförklara rätt
