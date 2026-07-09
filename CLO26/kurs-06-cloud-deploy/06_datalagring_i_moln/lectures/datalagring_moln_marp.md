---
marp: true
theme: default
class: invert
paginate: true
---

# Datalagring i Molnet

**Kurs:** Cloud Deploy
**Modul:** 06 — Datalagring i Molnet

---

## Vad ska vi lära oss idag?

- **Azure Storage** — blob, file, queue, table
- **Azure SQL Database** — hanterad relationsdatabas
- **Cosmos DB** — global NoSQL-databas
- **Azure Redis** — cache-lager
- **Välja rätt datalagring** — när använda vad?

---

## Azure Storage — Fyra tjänster i en

```
        Azure Storage Account
┌──────────┬──────────┬──────────┬──────────┐
│  Blobs   │  Files   │  Queues  │  Tables  │
│ (bilder, │ (SMB-    │ (medde-  │ (NoSQL,  │
│  videor) │  share)  │  lande-  │  key-val)│
│          │          │  köer)   │          │
└──────────┴──────────┴──────────┴──────────┘
```

- **Blob:** Ostrukturerad data (bilder, videor, backups)
- **File:** Nätverksshare (montera som en disk)
- **Queue:** Meddelandekö (asynkron kommunikation)
- **Table:** NoSQL key-value (enkelt, billigt)

---

## Blob Storage — Tiers

| Tier | Kostnad (GB) | Åtkomstkostnad | Hämtningstid | Användning |
|------|-------------|---------------|-------------|------------|
| Hot | Högst | Lägst | Direkt | Aktuella bilder, media |
| Cool | Mellan | Mellan | Direkt | Data som används ibland |
| Cold | Låg | Hög | Inom timmar | Arkiv, loggar |
| Archive | Lägst | Högst | Inom dagar | Backup, compliance |

```bash
# Ladda upp fil
az storage blob upload \
    --account-name mittkonto \
    --container-name bilder \
    --name logo.png \
    --file ./logo.png \
    --tier Hot
```

---

## SAS Tokens — Tillfällig åtkomst

Ge en klient tillfällig åtkomst till en specifik blob:

```csharp
BlobSasBuilder sasBuilder = new()
{
    BlobContainerName = "bilder",
    BlobName = "logo.png",
    Resource = "b",  // b = blob, c = container
    ExpiresOn = DateTimeOffset.UtcNow.AddHours(1),
    StartsOn = DateTimeOffset.UtcNow
};
sasBuilder.SetPermissions(BlobSasPermissions.Read);

string sasToken = sasBuilder.ToSasQueryParameters(
    storageSharedKeyCredential).ToString();
string url = $"https://mittkonto.blob.core.windows.net/bilder/logo.png?{sasToken}";
```

---

## Azure SQL Database

Hanterad SQL Server i molnet. Samma SQL-syntax som lokalt.

```bash
# Skapa
az sql server create --name min-server --resource-group myRG
az sql db create --server min-server --name webshop --resource-group myRG

# Connection string i app
Server=min-server.database.windows.net;Database=webshop;
User Id=admin;Password=...;
```

**Fördelar:** Inbyggd backup, HA, skalning, hot patchning.
**Nackdel:** Dyrare än SQL Server i egen VM.

---

## Cosmos DB — Global NoSQL

```bash
az cosmosdb create --name min-cosmos --resource-group myRG
az cosmosdb sql database create \
    --account-name min-cosmos --name webshop
```

**Nyckelfunktioner:**
- Global distribution (replikera data över hela världen)
- Multi-model: SQL API, MongoDB API, Cassandra API, Gremlin (graph), Table
- SLA på 99.999% läsning
- Autoskala RU (Request Units)

---

## Azure Redis — Cache

```bash
az redis create --name min-cache --resource-group myRG --sku Basic --vm-size c0
```

```csharp
// Användning i ASP.NET Core
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "min-cache.redis.cache.windows.net:6380,password=...";
});
```

**Användning:**
- Cache:a databasfrågor
- Session state (istället för in-memory)
- SignalR backplane
- Rate limiting

---

## Välja rätt datalagring

| Behov | Välj |
|-------|------|
| Relationsdata, ACID | Azure SQL Database |
| Bilder, videor, filer | Blob Storage |
| Cache, session | Redis |
| Global NoSQL, hög skalbarhet | Cosmos DB |
| Meddelandekö | Queue Storage |
| Serverless databas | Azure SQL Serverless |
| Sökning | Azure Cognitive Search |
| Data lake / analytics | Azure Data Lake, Synapse |

---

## Sammanfattning

- ✅ Azure Storage = blobs, files, queues, tables
- ✅ Azure SQL Database = hanterad relationsdatabas
- ✅ Cosmos DB = global NoSQL med multi-model
- ✅ Redis = snabb cache för prestanda
- ✅ Välj lagring baserat på datatyp och behov

---
