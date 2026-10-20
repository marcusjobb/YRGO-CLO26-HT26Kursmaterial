# Labbserie — Databaser i C# (kurs 02)

Sju labbar som bygger på varandra. Varje labb introducerar ett nytt abstraktionssteg, men bara när studerande har sett problemet det löser. Labb 7 är en valfri fördjupning.

| Labb | Titel | Nyckelbegrepp | Nivå |
|------|-------|--------------|------|
| 1 | Raw SQL | SQLite, appsettings.json, .gitignore, secrets | 🟢 |
| 2 | SQLiteHandler | Klass, encapsulation, overloading | 🟢 |
| 3 | Hero-klassen | Objekt som parameter, reflection, CreateTable | 🟡 |
| 4 | TiDBHandler + Interface | Interface, konfigurationsstyrt databasval, läckande abstraktion | 🟡 |
| 5 | Generics | `IDbHandler<T>`, generisk reflection, relation hero/pet | 🔴 |
| 6 | Dependency Injection | DI-container, services, Singleton/Transient | 🔴 |
| 7 | Fler servrar (valfri) | Fler handlers, dialekter, Strategy/Adapter | 🔴 |

## Tidsplan (ANTAGANDE: Marcus verifierar mot schemat)

| Labb | Utlagd | Inlämning |
|------|--------|-----------|
| 1 | ons 2026-10-14 | tors 2026-10-15 |
| 2 | tors 2026-10-15 | tis 2026-10-20 |
| 3 | tis 2026-10-20 | tors 2026-10-22 |
| 4 | tors 2026-10-22 | tis 2026-10-27 |
| 5 | tis 2026-10-27 | tors 2026-10-29 |
| 6 | tors 2026-10-29 | tis 2026-11-03 |
| 7 (valfri) | datum ej satt | datum ej satt |

## Pedagogisk röd tråd

Varje labb slutar med ett problem som nästa löser:

- **Labb 1:** Det fungerar, men `Program.cs` är ett kaos av SQL
- **Labb 2:** Klassen döljer SQL, men vi skickar fortfarande in lösa strängar
- **Labb 3:** Objekt som parametrar, men nu vill vi stödja en annan databas
- **Labb 4:** Interface löser bytet, men `Pet` kräver att vi kopierar allt
- **Labb 5:** Generics löser kopieringen, men vi laddar husdjur för hand och skapar beroenden manuellt
- **Labb 6:** DI och services löser det sista. Så här fungerar ASP.NET Core
- **Labb 7:** Bevis: en helt ny databas utan att röra resten av koden

## Setup (labb 1)

```
dotnet add package Microsoft.Data.Sqlite
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package MySqlConnector                             # labb 4
dotnet add package Microsoft.Extensions.DependencyInjection   # labb 6
dotnet add package Npgsql                                     # labb 7 (PostgreSQL)
```

Kopiera `appsettings.example.json` till `appsettings.json` och fyll i dina värden. Filen finns aldrig i git.
