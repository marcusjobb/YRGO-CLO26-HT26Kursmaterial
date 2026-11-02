# Databasserver — Programmeringstermer

## Databasserver
En separat process (eller separat maskin) som tar emot anslutningar från klienter, hanterar data och svarar på frågor. Klienterna kommunicerar via ett nätverksprotokoll — oftast TCP/IP.

```
[Klient 1: Webb-API]  ──┐
[Klient 2: Admin-app] ──┤──→ [Databasserver] ──→ [Data på disk]
[Klient 3: Rapport]   ──┘
```

## Vanliga databasservrar

| Server | Typ | Vanligt i |
|--------|-----|-----------|
| SQL Server | Relationell | Microsoft/Azure-stack |
| PostgreSQL | Relationell | Open source, cloud |
| MySQL / MariaDB | Relationell | Webb, Linux |
| MongoDB | Dokumentdatabas | NoSQL, JavaScript |
| Redis | Nyckel-värde | Cache, sessioner |

## Klient–server-modellen

Databasservern är en egen process med eget minne och egna resurser. Klienten (din applikation) ansluter via en **connection string** och skickar SQL-frågor. Servern utför arbetet och returnerar resultatet.

```
Applikation                    SQL Server
───────────                    ──────────
"SELECT * FROM Products"  →    Utför query
                          ←    Returnerar rader
```

## Connection string

Innehåller all information som behövs för att ansluta:

```
Server=localhost;Database=AppDb;User Id=sa;Password=YourPass;TrustServerCertificate=true
│               │                │          │
Server/IP       Databasnamn      Användare  Lösenord
```

I `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=AppDb;User Id=sa;Password=YourPass;TrustServerCertificate=true"
  }
}
```

I C# via EF Core:
```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
```

## SQL Server med Docker

Istället för att installera SQL Server lokalt — kör det i en container:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd" \
  -p 1433:1433 --name sqlserver \
  mcr.microsoft.com/mssql/server:2022-latest
```

Anslut sedan med connection string `Server=localhost,1433;...`

## Fördelar med databasserver

| Fördel | Förklaring |
|--------|-----------|
| Många samtidiga användare | Hanterar parallella anslutningar och skrivoperationer |
| Åtkomstkontroll | Egna användarkonton med rättigheter per tabell |
| Transaktioner | ACID-garantier även under hög last |
| Skalbarhet | Kan köra på dedikerad maskin, kluster eller i molnet |

## Port-nummer att känna till

| Databas | Standardport |
|---------|-------------|
| SQL Server | 1433 |
| PostgreSQL | 5432 |
| MySQL | 3306 |
| MongoDB | 27017 |
| Redis | 6379 |

## Databasserver vs fildatabas vs minnesdatabas

```
Minnesdatabas   Fildatabas      Databasserver
─────────────   ──────────      ─────────────
RAM             Fil på disk     Separat process
Snabbast        Snabb           Nätverksfördröjning
Ingen persist.  Persistent      Persistent
1 process       1 process       Obegränsat många
Tester/cache    Appar/mobil     Produktion/webb-API
```
