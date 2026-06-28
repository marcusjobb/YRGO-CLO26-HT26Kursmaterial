---

title: Sqlite C# Övningar
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Databas/SQL/SQLite C# Övningar.docx"
description: "Alla övningar här bygger på SQLite databasen video_games.db som jag har lagt upp på Teams"
tags: ["csharp", "databaser", "exercise", "sql", "sqlite", "övningar.docx"]
week_fit: []
---
SQLite C# övningar
Alla övningar här bygger på SQLite databasen video_games.db som jag har lagt upp på Teams
(Siffror i exemplerna är inte exakta, jag bara visar hur en layout kan se ut!)

1 SELECT
Skriv ett program som skriver ut 10 rader(records) i tabellen ’video_games’ åt gången.
https://www.tutorialspoint.com/sqlite/sqlite_select_query.htm

Super Mario 64 | Nintendo | 1994
....

|

....

Bonus uppgifter

1. Gör en toppranking med de 10 mest lönsamma spelen per genre
Action
| Platformer | Sport
| ....
Call of Duty 4| Super Mario 64 | NBA 2006
| ....
....
2. Visa mängden spel och genomsnittligt vinst per genre (matris)
| Platformer | Sport
| ....
#games
| 23
| 10
| ....
avg sales | 1.51
| 2.5
| ....
....
3. Visa en procentandel spel, per genre, per konsoll (genre x konsoll matris)

Wii
Xbox
....

| Platformer | Sport
| 16%
| 10%
| 2%
| 23%

| ....
| ....
| ....

2 Metadata
Använd https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/metadata (GetSchemaTable)
för att även hitta namn på kolumnerna och vilken typ en kolumn har. Skriv ut denna information över
de 10 tidigare raderna.

Title
| Publisher | Release Year | ....
string
| string | int
| ....
Super Mario 64 | Nintendo | 1994
| ....
Call of Duty 4 | Microsoft | 2006
| ....
....

3 INSERT
Skapa en metod i C# som låter dig lägga in en ny rad (ett nytt record). Skapa en UI så användaren kan
lägga till rader genom programmet.
https://www.tutorialspoint.com/sqlite/sqlite_insert_query.htm

4 DELETE
Skapa en metod som låter dig ta bort ett spel från tabellen genom att matcha på ett namn, årtal och
plattform. Skapa en UI så användaren kan skriva in parametrarna, se vad som kommer att raderas
och sen få välja om raderingen ska genomföras eller ej. (Så först måste en SELECT ske och sen en
DELETE)
https://www.tutorialspoint.com/sqlite/sqlite_delete_query.htm

5 UPDATE
Samma sak som #4 men denna gång kan användaren välja att ändra värdet inne i någon av
kolumnerna på en rad som matchar namn, årtal och plattform.
https://www.tutorialspoint.com/sqlite/sqlite_update_query.htm
