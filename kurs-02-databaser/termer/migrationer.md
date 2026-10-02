# Migrationer — Programmeringstermer

## Migration
En C#-fil som beskriver ändringar i databasschemat. Varje migration har en `Up()`-metod (vad som ska hända) och en `Down()`-metod (hur man ångrar det).

```bash
Add-Migration AddEmailToCustomer
```

## Up()
Metod i en migration som definierar vad som ska ändras: skapa tabeller, lägga till kolumner, skapa index, etc.

## Down()
Metod i en migration som ångrar `Up()`: tar bort tabeller, kolumner, index. Måste spegla `Up()` exakt.

## InitialCreate
Namnet på den första migrationen. Skapar alla tabeller baserat på dina entities.

## Update-Database
Kommando som applicerar väntande migrationer på databasen. Kan specificera ett migration-namn för att rulla fram eller tillbaka.

```bash
Update-Database            # Applicera alla
Update-Database InitialCreate  # Rulla tillbaka till start
```

## Remove-Migration
Tar bort den senaste migrationen (förutsatt att den inte är applicerad på databasen).

## Script-Migration
Genererar ett SQL-skript av migrationerna istället för att köra dem direkt. Användbart för att granska SQL eller skicka till DBA.

```bash
Script-Migration -o migrate.sql
```

## Pending Migration
En migration som skapats men ännu inte applicerats på databasen. Syns med `Get-Migration` i PMC eller `dotnet ef migrations list` i CLI.

## Snapshots (Model Snapshot)
En fil (`AppDbContextModelSnapshot.cs`) som innehåller en ögonblicksbild av hela databasmodellen. EF Core använder den för att jämföra med entities och upptäcka ändringar.

## DbContext Model Snapshot
Sparas som `{DbContextName}ModelSnapshot.cs`. Denna fil ska ALDRIG tas bort — utan den vet EF Core inte vad som ändrats.

## Idempotent Migration
Migration som kan köras flera gånger utan att orsaka fel. EF Core håller reda på vilka migrationer som körts i tabellen `__EFMigrationsHistory`.

## Automatic Migration (föråldrat)
EF 6 hade stöd för automatisk migration vid runtime. EF Core stödjer detta inte — du måste skapa migrationer explicit. Detta är en förbättring (mer kontroll).

## Bundle (EF Core 6+)
Skapa en körbar fil som applicerar migrationer — användbart för CI/CD och produktion.

```bash
dotnet ef migrations bundle
./efbundle
```

## Migrations in Team
Regler för smidiga migrationer i team:
- Ändra aldrig publicerade migrationer
- Skapa en ny migration istället
- Vid merge-konflikt: ta bort din migration, pulla, skapa på nytt
- Applicera alltid migrationer efter git pull
