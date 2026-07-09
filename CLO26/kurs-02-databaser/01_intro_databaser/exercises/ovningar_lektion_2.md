Övningar – Lektion 2


Relational schema är en översikt över en databas. Databasen är organiserad i en relational schema med rektanglar för varje tabell där titeln på tabellen står överst, alla kolumner är placerade inuti, primary och foreign keys är markerade samt samband mellan dessa markerade med one-to-many pilar och symboler. Se exempel nedanför.


- Vi har i dagens föreläsning visat exempel på en Sales databas med olika kopplade tabeller: sales, items, customers och companies, se tabellerna nedanför. Skapa en relational schema för denna databasen.


- Vi har en databas med tre tabeller:

- Anställda med kolumnerna: person_namn, gata, stad.

- Arbete med kolumnerna: person_namn, företag_namn, lön

- Företag med kolumnerna: företag_namn, stad

Vilka är de bästa primary och foreign keys i denna databasen?


- Kolla på tabellen nedanför. Kan vi använda name som en primary key för denna tabellen? Om inte, varför?


MySQL

Installera MySQL workbench och server. Installationsguide finns på Teams. SQL databasen employees.sql finns i Teams under L1. Ladda hem den och öppna den i MySQL. Kör filen och skapa en query fil där ni testar SELECT * FROM employees;. Gör er bekanta med interfacet och testa olika funktioner på samma sätt som i föreläsningen.


- Undersök emplyees databasen ni har laddat hem i MySQL. Vi har lärt oss hur man öppnar en databas i MySQL, kör den (ctrl+enter) och att man kan se databasen under fliken som heter schemas genom att klicka uppdatera knappen bredvid. Då kan man undersöka databasen med vilka tabeller och kolumner som finns. Gör en relational schema  modell för employees databasen och kom med förslag på vad ni tycker borde vara primary och foreign keys.


Extra uppgift om relational schemas av svårare karaktär

- Ett TV företag vill utveckla en databas för att lagra data om TV serierna företaget producerar. Databasen skall bland annat innehålla information om skådespelare och regissörer i TV serierna. Skådespelare och regissörer är anställda i företaget. En TV-serie är indelad i avsnitt. Varje avsnitt kan sändas vid flera tillfällen. En skådespelare kan delta i många serier. Varje avsnitt i en serie regisseras av en av regissörerna, men olika avsnitt kan regisseras av olika direktörer. Exempel på frågor (queries) man skulle vilja ställa till databasen är:

- • Vilka skådespelare spelar i serien Big Sister?

- • I vilken serie deltar skådespelaren Bertil Bom?

- • Vilka skådespelare deltar i mer än en serie?

- • Hur många gånger har det första avsnittet i serien Wild Lies sänts? På vilka tider?

- • Vilken regissör har regisserat flest avsnitt?

- Skapa en relational schema modell för databasen. Hitta vad som borde användas som primary och foreign keys.

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
