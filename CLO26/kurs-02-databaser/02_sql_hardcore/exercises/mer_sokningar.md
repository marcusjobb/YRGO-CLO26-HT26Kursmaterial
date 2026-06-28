---

title: Mer sökningar
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 2
language: sql
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/Mer sökningar.md"
description: "Gå in på Mockaroo.com och skapa en tabell enligt detta"
tags: ["databaser", "delete", "distinct", "exercise", "mockaroo", "select", "sql", "ssms", "sökningar", "update"]
week_fit: []
---

# Mer sökningar

🟡



Gå in på Mockaroo.com och skapa en tabell enligt detta


Glöm inte att välja SQL

Kalla tabellen för WeirdPeople och glöm inte att klicka in Include Create Table

Ladda ner filen genom att klicka på

Om du känner dig äventyrlig klicka på den flera gånger så du får fler filer.


Skapa en Databas och kalla den Population, ifall du inte skapat en innan.

Ändra nu i din databas och lägg till fältet age som är en int


Såhär


Och spara din tabell.

Klicka nu på

Kör följande kod för att ge dem ålder

```
UPDATE WeirdPeople SET age=ABS(CHECKSUM(NEWID()) % 100);
```


Öppna nu filen du laddade ner i notepad, kopiera koden och kör den i din SSMS.

Om du laddat ner flera filer, kör en i taget, men efter den första, ta inte med Create Table delen för den kommer att gnälla om det – då tabellen redan finns.


Nu ska vi leka CSI, använd SQL för att svara på följande frågor genom att skapa SQL frågor. Diskutera gärna i grupp.

Vi rensar lite i databasen nu. Radera alla personer som är yngre än 15 år.

Hur många personer tycker om skräckfilmer?

Hur många personer över 18 år men yngre än 25 tränar Karate?

Hur många personer över 25 år tränar Karate?

Hur många män i åldern 25 till 32 tycker om Ford bilar?

Hur många som identifierar sig som kvinnor tycker om skräckfilmer?

Hur många personer i åldrarna 35 till 42 har efternamn som börjar på S?

- Och hur många av dem identifierar sig som män?

- Och av dessa män, hur många är över 50 år?

Hur många under 18 år tycker om Actionfilmer och BMW?

Hur många personer tycker om Subarus

- Är det mest äldre eller yngre personer?

Hur många personer har gmail konton?


Hur många poster finns i din databas

- Hur många unika förnamn finns det?

- Hur många unika efternamn finns det?

- Hur många unika för och efternamn tillsammans finns det?

Tips: för att få resultat som inte upprepar sig använd SELECT DISTINCT
 SELECT DISTINCT first_name FROM WeirdPeople


```
Fler frågor kommer sen
```

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
