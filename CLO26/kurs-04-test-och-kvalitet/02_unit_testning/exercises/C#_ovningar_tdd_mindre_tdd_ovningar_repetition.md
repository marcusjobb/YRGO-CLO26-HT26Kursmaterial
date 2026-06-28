---

title: Mindre Tdd Övningar (repetition)
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/TDD/Mindre TDD övningar (repetition).docx"
description: "Skapa en klass kallad StringHelper"
tags: ["(repetition).docx", "csharp", "exercise", "mindre", "tdd", "test", "testing", "övningar"]
week_fit: []
---
Mindre TDD övningar
Skapa en klass kallad StringHelper
Den ska ha följande publika metoder, skapa inte metoderna än dock… bara tomma metoder som
returnerar string.empty eller new List<string>
Skriv en tom metod i taget, testa det (enligt nästa sida) och sedan skapa koden till din metod.
När testet fungerar, refacturera och ta nästa metod.
Metod
GetWord
StringToList
RemoveWord
RemoveWordAt
InsertWordAfter
SwapWords

Parameter
string text
int x
char separator=' '
string text
char separator=' '
string text,
string remove
string text,
int pos
string text,
string after
string add
string text,
string word1,
string word2

Returnerar
Splittrar strängen med separator som skickats in (om
det skickats in) och returnerar det ordet i det index
som skickades in.
Splittar en sträng och returnerar en List<string>
Tar bort valt ord från en text, returnerar texten
Tar bort valt ord från valt index, returnerar texten
Lägger till ord efter ett givet ord, exempelvis
InsertWordAfter (”The cat in the hat”,”in”,”side”)
Returnerar texten
Byt plays på orden, returnera texten

Tester att testa
Metod
GetWord

StringToList
RemoveWord
RemoveWordAt

InsertWordAfter

SwapWords

Parameter
1. Skicka in en tom text och värde 1 på x
2. Skicka en ”Hello World” och -1 på x
3. Skicka en ”Hello World” och 100 på x
4. Skicka in en tom sträng och värde 0
1. Skicka in en tom text
2. Skicka in en kort text och sätt separator till något som inte finns i
strängen
1. Skicka in “hello world”, och ordet ”world” som remove
2. Skicka in en tom text och , och ordet ”world” som remove
3. Skicka in ”Hello World” och mellanslag som remove
1. Skicka in “A cat in a hat” och index 2
2. Skicka in “A cat in a hat” och index -1
3. Skicka in “A cat in a hat” och index 25
4. Skicka in en tom sträng och index 0
1. Skicka in “A cat in a hat”, “ a “, “a huge”
2. Skicka in “Trust the force” och “force”, “cat”
3. Skicka in “Trust the force” och tom sträng, “meow”
1.
2.
3.
4.

Skicka in “It's not wise to upset a Wookiee.”, 1, 3
Skicka in “It's not wise to upset a Wookiee.” ,-1, 4
Skicka in “It's not wise to upset a Wookiee.”, 1,124
Skicka in “It's not wise to upset a Wookiee.”,1,1

Kom gärna på fler tester att testa…
