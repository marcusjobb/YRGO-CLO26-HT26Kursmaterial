---

title: Luffarschack Tdd
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/TDD/Luffarschack TDD.docx"
description: "Dagens utmaning blir ett luffarschackspel. Lätt och enkelt, eller?"
tags: ["csharp", "exercise", "luffarschack", "oop", "tdd.docx", "test", "testing"]
week_fit: []
---
Luffarschack
Dagens utmaning blir ett luffarschackspel. Lätt och enkelt, eller?
Projektet ska delas i klasser, exempelvis (men ej ett krav)




Input (tar in värden från spelare)
Output (skriver ut spelplan och meddelanden)
SimulatedPlayer (datorns spelhjärna)

För att kunna samarbeta trots att man inte delar koden behövs interfaces som talar om vilka metoder
och vilka parametrar som ska skickas mellan klasserna.
I denna övning ska vi dela upp ansvaret mellan programmerarna




Projektledare
Testare
Programmerare

Spelregler
Tre-i-rad eller tripp trapp trull är en variant av luffarschack som spelas på en spelplan med endast 3*3
rutor, och syftet är sålunda att få tre lika i rad.
Det är ett mycket lättspelat spel och är ett av världens populäraste brädspel som kan spelas på ett
papper med en spelplan med nio rutor i en kvadrat.
Spelet går ut på att ställa ut eller flytta sina tre markeringar (X eller O) så att man får tre i rad, antingen
lodrätt, vågrätt, eller diagonalt.
Källa: https://sv.wikipedia.org/wiki/Tre_i_rad

Rollerna
Projektledaren

Projektledaren ska samordna mellan testare och programmerare. Projektledaren svarar på frågor om
projektet som båda parter har och ser till att de får den hjälp och information de behöver (exempelvis
googlar fram lösningar till frågor).
Projektledaren samlar ihop koden och kör den när både testare och programmerare skickat upp sin kod,
och rapporterar resultatet sedan.

Testare

Testarens roll är att observera spelreglerna och skapa test scenarios.
Exempelvis



Spelaren försöker placera sig på en redan tagen position?
Spelaren placerar sin pjäs utanför spelplanen

Därefter med hjälp av interfacet skapa en klass som får agera som mockup tills programmeraren är klar
med sin klass.
Med hjälp av Interface och Test scenarios ska generera test cases och tester som ska sedan köras mot
den färdiga klassen.
Test Scenarios och test cases ska dokumenteras i en textfil som skickas med test classen

Programmerare

Programmeraren ska i samråd med testaren och projektledaren skapa ett interface som ska användas.
När koden är klar ska den testas med testarens givna tester.
Under programmerandet kommer Projektledaren och andra i gruppen att hjälpa programmeraren med
sin kod, antingen genom att skapa metoder eller guida om programmeraren fastnar.
