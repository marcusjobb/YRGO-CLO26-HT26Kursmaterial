# Fildatabas — Programmeringstermer

## Fildatabas
En databas som lagrar all data i en eller flera filer direkt på disk. Ingen separat serverprocess krävs — applikationen läser och skriver filen direkt.

Vanligaste exemplet: **SQLite**.

## SQLite
Världens mest använda databas. Hela databasen bor i en enda `.db`-fil. Används bland annat i Android, iOS, webbläsare och Electron-appar.

```
myapp.db       ← allt: tabeller, index, data — i en fil
```

## Fördelar med fildatabas

| Fördel | Förklaring |
|--------|-----------|
| Noll konfiguration | Ingen server att installera eller starta |
| Portabel | Kopiera filen = kopiera hela databasen |
| Bra för dev/test | Snabb att sätta upp, enkel att återställa |
| Offline-first | Fungerar utan nätverksanslutning |

## Nackdelar

- Sämre prestanda vid många samtidiga skrivoperationer
- Inte lämplig för applikationer med många parallella användare
- Ingen inbyggd autentisering eller nätverksåtkomst

## SQLite med EF Core i .NET

```csharp
// Installera: dotnet add package Microsoft.EntityFrameworkCore.Sqlite

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=myapp.db"));
```

Filen `myapp.db` skapas automatiskt i projektmappen vid första migrering.

## Connection string-format

```
Data Source=myapp.db                        // relativ sökväg
Data Source=C:/data/myapp.db               // absolut sökväg
Data Source=:memory:                        // minnesdatabas (se minnesdatabas.md)
```

## Jämförelse: fildatabas vs databasserver

```
Fildatabas (SQLite)          Databasserver (SQL Server)
────────────────────         ──────────────────────────
En fil på disk               Separat process/server
Ingen installation           Kräver installation/Docker
1 skrivare i taget           Många parallella skrivare
Perfekt för: appar,          Perfekt för: webb-API,
prototyper, tester           produktion, flera klienter
```

## När ska du välja SQLite?

- Mobilappar och desktopappar med en användare
- Prototyper och proof-of-concept
- Enhetstester (eller använd minnesdatabas)
- Inbäddade system och IoT
