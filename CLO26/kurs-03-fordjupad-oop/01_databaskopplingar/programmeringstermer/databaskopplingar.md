# 01 Databaskopplingar — Programmeringstermer

## ADO.NET
Microsofts äldre databastillgångsteknik. Använder SqlConnection, SqlCommand och SqlDataReader för att manuellt hantera databasanrop.

## Connection String
Sträng som innehåller anslutningsinformation till en databas: server, databasnamn, användare, lösenord.

## SqlConnection
Klass som hanterar anslutningen till en SQL Server-databas. Måste öppnas före och stängas efter användning.

## SqlCommand
Representerar en SQL-sats eller lagrad procedur som ska köras mot databasen.

## SqlDataReader
Strömmad läsare som läser data rad för rad från databasen. Framåtpekande, ingen återgång.

## Parameterized Query
SQL-fråga med parametrar (@param) istället för inlindade värden. Skyddar mot SQL injection.

## ORM
Object-Relational Mapping. Teknik som mappar databastabeller till C#-objekt automatiskt.

## Dapper
Lättviktig ORM/micro-ORM från Stack Overflow. Använder extension methods på IDbConnection.

## Connection Pooling
Återanvändning av databasanslutningar för att undvika overhead av att öppna/stänga.

## Dispose Pattern
C#-mönster för att frigöra resurser (t.ex. databasanslutningar) via IDisposable.

