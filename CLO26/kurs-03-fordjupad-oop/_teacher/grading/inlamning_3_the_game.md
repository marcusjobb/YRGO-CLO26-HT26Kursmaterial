---

title: Inlämning 3 - The Game
author: Marcus Ackre Medina
type: exam
topic: oop
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "reference/exercises_to_spread_out/Inlämning 3 - The game.md"
description: "I den här labben ska ni skapa ett enkelt textbaserat spel, som går ut på att döda monster och få experience points, tills man når level 10 då spelet avslutas."
tags: ["arv", "csharp", "git", "inheritance", "inlämning", "klasser", "oop", "shop", "spel", "textbaserat"]
week_fit: []
---

# Inlämning 3

🔴



## The Game!

I den här labben ska ni skapa ett enkelt textbaserat spel, som går ut på att döda monster och få experience points, tills man når level 10 då spelet avslutas.


Syftet med labben är att få öva delvis på "vanlig" programmering, men också på hur man programmerar med olika klasser och hur de interagerar med varandra. Först presenteras hur spelet ska se ut, och sedan kommer tips på hur man kan tänkas implementera det.


Den här gången får du inte instruktioner på hur du ska göra, spelet är helt och hållet din att skapa enligt din fantasi. Dock ska vissa villkor uppfyllas för att det ska bli G och VG.


## Go adventuring

När man ger sig ut på äventyr finns det en liten chans (~10%) att inget händer:


Men oftast så möter man ett monster! Striderna går till som så att spelaren och monstret slår på varandra varannan gång, där användaren får klicka på enter emellan varje omgång:


och den som dör först förlorar. Om spelaren vinner, får denne så mycket experience points som monstret ger, och sedan visas huvudmenyn igen:


Om monstret vinner, dör spelaren och spelet avslutas:


Om spelaren når level 10, vinner denne spelet:


# Krav

Nedan följer information om vilka klasser ni ska skapa, samt hur ni kan implementera dem. Ni får designa systemet själva om ni vill, men det finns ett par saker ni måste ha med även då:


Player.

- En klass för att representera spelaren

- name,

- level,

- exp,

- hp

- Monster. En klass för att representera ett monster

- name

- SpecificMonster. Specifika monster som ärver/implementerar Monster

- exp monstret ger när det dör

- hp


# VG-uppgift: Shop-system

## Shop-system

Gör så att monster droppar slumpmässigt mängd guld när de besegras, som spelaren plockar upp. Detta guld kan spelaren sedan spendera i en affär (via huvudmenyn). I affären ska det säljas styrkeamuletter och försvarsamuletter - styrkeamuletter ökar hur mycket skada man ger, och försvarsamuletter minskar hur mycket skada man tar. Ni ska göra detta genom att skapa Shop.cs, samt genom att förändra flera av klasserna ni redan har skapat.


Hur man tjänar guld Monster droppar guld när de besegras, som spelaren får. Både spelaren och monstret måste nu hålla koll på pengar. Förslag på monster-metoder: getGold() Förslag på playermetoder: getGold(), giveGold(), takeGold().

Anropa dessa på lämpligt vis när en strid har vunnits.


# Kodning

## Styrka/uthållighet

Hur mycket spelaren skadar, och hur mycket spelaren tar i skada, ska nu bero på hur stark/uthållig denne är. Ni kommer behöva skapa variabler (t.ex. strength och toughness), som ska påverka skadan som spelaren ger och tar emot vid attack. Dessa variabler ska också kunna förändras från andra klasser (de ska förändras av Shop, när ni köper amuletter) Exempel på hur strength och toughness kan spela roll: låt attack() generera ett tal mellan strength och strength*2. Subtrahera toughness från varje attack vi får på oss, så om jag har toughness 2 och får 8 skada på mig, så tar jag bara 6 skada.


När man väljer menyalternativ 2, ska all denna information skrivas ut:


## Shop

I huvudmenyn, ska man kunna välja att gå in i en affär. Där kan man köpa amuletter, eller avsluta.


Om vi har tillräckligt med pengar, kan vi köpa amuletter som förändrar vår strength/toughness:


När vi sedan strider, märker vi av effekterna. Här har vi köpt massa uthållighet amuletter:


# Inlämning


Inlämning sker i en zipfil som heter Labb3G.zip eller Labb3VG.zip beroende på vilken nivå du implementerat. Filen laddas upp till Classroom.


Det är inte min regel utan kommer ifrån Skolverket och har med rättssäkerhet att göra. Om det skulle bli några oklarheter om ett satt betyg eller något så måste jag kunna visa vad jag har baserat mitt betyg på. Problemet med att lämna in en länk till ett repo är att jag inte kan vara helt säker på att de filerna som ligger där i kommer vara kvar i efterhand.


T.ex. kan någon som är missnöjd med ett betyg ta bort hela repot och plötsligt blir det väldigt oklart vad jag har baserat betyget på.


Har ni däremot skickat in en zippad fil så ligger det i systemet som Campus Mölndal administrerar så det finns en viss kontroll över filerna från skolans sida.


Sedan kanske det är lite överflödigt att kräva båda. Git-repot däremot är väldigt bra skydd för er om ni skulle hamna i en situation där misstänkt fusk uppstår, en snabb titt på ett välcommittat git-repo brukar räcka för att se att en student har skrivit koden själv.


Sedan skulle ni missa det kraven och bara skickat in ett repo så kommer jag rätta efter repot och be er skicka upp en zip i efterhand, utan att jag låter det påverka betyget. Tekniska fel uppstår med digitala inlämningar och jag tycker inte det ska påverka er.


Bildkällor:

- Photo by cottonbro from Pexels

- Photo by cottonbro from Pexels

- Photo by cottonbro from Pexels
