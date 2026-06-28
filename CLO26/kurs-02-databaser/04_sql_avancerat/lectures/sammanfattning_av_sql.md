---

title: Sammanfattning av SQL
author: Marcus Ackre Medina
type: lecture
topic: databaser
difficulty: 1
language: sql
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/Sammanfattning av SQL.md"
description: "En databas är ett program som lagrar 'data', alltså information i en fil. Ungefär på samma sätt som Excel men med lite smartare funktioner för sökning och indexering."
tags: ["crud", "databaser", "foreign-key", "join", "sammanfattning", "sql", "sql-lektionen", "transactions"]
week_fit: []
---

# Sammanfattning av SQL-lektionen

🟢



En databas är ett program som lagrar "data", alltså information i en fil. Ungefär på samma sätt som Excel men med lite smartare funktioner för sökning och indexering.


# SQL – Heroes

# Skapa databas

CREATE DATABASE DCHeroes;
USE DCHeroes;

Man använder USE kommandot för att tala om för scriptet att i fortsättningen ska den köra med den valda databasen.

# CRUDL

Crud kallas ibland för CRUDL för att man vill kunna se listan på alla rader i tabellen och inte bara en i taget…

| Create | INSERT INTO (fält) VALUES (värden),(värden); |
| --- | --- |
| Read | SELECT fält FROM tabell WHERE villkor; |
| Update | UPDATE Tabell SET fält=värde, fält2=värde2; |
| Delete | DELETE FROM tabell WHERE fält = värde; |
| List | SELECT fält FROM tabell |


# Skapa tabeller

Primary Key = huvudnyckel för tabellen, auto_increment betyder att räknaren kommer att ökas med ett varje gång en ny rad läggs till i databasen.

CREATE TABLE Heroes (
    heroId INTEGER PRIMARY KEY auto_increment,
    name VARCHAR(50) NOT NULL,
    lastName VARCHAR(50) NOT NULL,
    age INTEGER,
    email VARCHAR(50),
    phone VARCHAR(50)
);

CREATE TABLE Pets(
    petId INTEGER PRIMARY KEY auto_increment,
    pet VARCHAR(50)
);

## Kopplingstabell

En kopplingstabell är en tabell som kopplar ihop två eller flera andra tabeller

CREATE TABLE Owner(
    ownerId INTEGER PRIMARY KEY auto_increment,
    heroId INTEGER,
    petId INTEGER,
    FOREIGN KEY (heroId) REFERENCES Heroes(heroId),
    FOREIGN KEY (petId) REFERENCES Pets(petId)
);


## FOREIGN KEY

Med raden FOREIGN KEY (heroId) REFERENCES Heroes(heroId) talar vi om för databasen att tabellen ska ha hänvisning till tabellen Heroes och kolumnen heroId. Detta gör att vi inte kan radera hunden från pets listan utan att först ha raderat kopplingen till Clark Kent. En sådan regel kallas Restriction. Restriction används för att hindra databasanvändarna från att göra dumma saker. Denna restriction gör också att vi inte kan radera tabellen pets eller Heroes så länge det finns kopplingar i Owner tabellen.

# inmatning

Mata in hjältarna

INSERT INTO Heroes (name, lastName, age, email, phone)
VALUES
       ('Bruce','Wayne',42, 'bruce@waynecorp.com','5555-4567-1212'),
       ('Celina','Kyle',36, 'celina@meow.org','5555-4242-1331'),
       ('Victor','Stone',35, 'cyborg@rus.com','5555-8888-8888');

Mata in djur

INSERT INTO Pets (pet)
VALUES
       ('cat'),
       ('dog'),
       ('bat');

Koppla djuren till sina respektive hjältar

INSERT INTO Owner (heroId, petId)
VALUES (1,2), (2,3),(3,1);

# select - Visa listan på hjältar

SELECT * from Heroes;

## Visa listan på djur

SELECT * FROM Pets;

## Visa listan på ägare

SELECT * FROM Owner;


## Koppla ihop informationen från alla tre tabeller

För att kunna se hur tabellerna fungerar tillsammans får vi koppla ihop dem på detta sätt. I SELECT raden talar vi om vilka tabeller som är inblandade. I WHERE raderna förklarar vi för databasen hur den ska koppla ihop informationen.

SELECT name, lastName, pet from Heroes, Pets, Owner
WHERE
      Owner.heroId = Heroes.heroId AND
      Owner.petId = Pets.petId;

# Transactions – skydd mot tabbar

En transaction skyddar databasen från felaktiga inmatningar eller raderingar.

START TRANSACTION;
DELETE FROM Heroes WHERE heroId>0;
SELECT * from Heroes;

## Rollback återställer allt

ROLLBACK;

## Commit sparar ändringar

COMMIT;

# UPDATE – ändra data i tabellen

UPDATE Heroes 
SET 
      email = 'MrCyborg@cyborg.rus' 
WHERE heroId = 4;

# ORDER BY - Sortering

Man kan sortera resultatet av sin sökning direkt i frågan.

## Sortera på namn

SELECT name, lastname from Heroes ORDER BY name;

## Sortera på efternamn

SELECT name, lastname from Heroes ORDER BY lastname;

## Sortera på ålder

SELECT name, lastname from Heroes ORDER BY age;

## DESC - Sortera på ålder i omvänd ordning (äldst först)

SELECT name, lastname from Heroes ORDER BY age DESC;

# COUNT - Ta reda på antal hjältar

SELECT COUNT (heroId) FROM Heroes;

# DISTINCT - Ta reda på antal åldersgrupper

Distinct ser till att vi aldrig får dubbletter i sökningarna.

SELECT COUNT (DISTINCT age) FROM Heroes;

# Ta reda på hur många som heter Bruce

(Det kan vara Bruce Banner och Bruce Wayne)

SELECT COUNT (heroId) FROM Heroes Where Name='Bruce';

# Ta reda på antal unika namn i listan

Nu räknar vi alla Bruce som en, oavsett hur många det är

SELECT COUNT(DISTINCT name) FROM Heroes;

# Fler hjältar att leka med

INSERT INTO person (name, lastname, age, email, phone) 
VALUES
('Diana', 'Prince', 28, 'diana@amazon.com', '555-555-5552'),
('Peter', 'Parker', 28, 'peter@dailybugle.com', '555-155-5155'),
('Bruce', 'Banner', 28, 'bruce@culvertUni.org', '555-545-5755'),
('Selina', 'Kyle', 25, 'selina@meow.org', '555-575-5559'),
('Wilson', 30, 'dead@pool.org', '555-585-1555'),
('Wick', 30, 'babayaga@contineltal.org', '555-755-5254'),
('Arthur', 'Curry', 42, 'arthur@atlante.an', '555-545-5255'),
('barry', 'Allen', 21, 'barry.allen@centralcity.pd', '555-558-8888'), ('James', 'Gordon', 45, 'james.gorgon@gothamcity.pd', '555-565-5655'),
('Alfred', 'Pennyworth', 52, 'alfred.pennyworth@waymansion.com', '555-535-3555'),
('Amanda', 'Waller', 43, 'boss@argus.org', '555-455-5554');

## DELETE FROM - Radera

Efter att ha matat in alla så ser vi att några av dem inte tillhör DC utan snarare Marvel. Så vi får ta bort dem ur listan. Man kan antingen ta bort dem genom att söka på deras namn, men det är inte alltid bra att göra så, då det kan råka finnas flera personer som heter så. Om vi ska ta bort Hulk så skulle vi kunna radera Bruce

DELETE FROM Heroes WHERE name='Bruce';

Men det skulle även ta Bruce Wayne, alltså Batman och det får inte ske. Vi gör snarare så att vi letar upp ID på den hjälten vi vill radera.

SELECT heroId, name, lastName FROM Heroes;

Och sedan väljer vi det Id vi vill ta bort, exempelvis Id 5.

DELETE FROM Heroes WHERE heroId=5;

Då vi raderar baserat på indexet så är vi 100% säkra på att rätt rad raderas.

# Bra länkar

- https://www.mysqltutorial.org/

- https://www.w3schools.com/mysql/mysql_sql.asp

- https://sqlzoo.net/wiki/SQL_Tutorial

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
