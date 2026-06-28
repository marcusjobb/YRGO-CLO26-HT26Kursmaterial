---

title: Övningar If
author: Marcus Ackre Medina
type: lecture
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/If-satser/Övningar If.docx"
description: "Felaktiga värden är värden mindre noll"
tags: ["conditions", "csharp", "if.docx", "övningar"]
week_fit: []
---
Övning 1: Omvandla till kod
Felaktiga värden är värden mindre noll

Övning 2: Ålder och fordon
Skriv ett program som frågar efter ålder och berättar om man får köra
•

EU Moped (15 år)

•

Personbil (18 år)

•

Tung lastbil (21 år)

•

Buss (24 år)

Övning 3:
För att få ett slumpvalt tal måste man först skapa en instans av slumpgeneratorn.
Random rnd=new Random();
För att få ett slumptal ur den skriver man
int guess = rnd.Next(0,100);
Skumpgeneratorn kommer då att välja ett tal mellan 0 och 99.
Det ska stå öka men rättstavningen dummade till det, så det står Oka
Tänk på att du ska ställa en vanlig fråga och omvandla talet du fått inmatat från sträng till en int

Övning 4 : Hundår igen…
I förra försöket med hund och katt-år fick vi samma resultat på båda sorterna. Nu ska vi korrigera
misstaget.
Hundar:
Hundens ålder
0,5
1 hundår
2 hundår
3 hundår
4 hundår
5 hundår
6 hundår
7 hundår
8 hundår
9 hundår
10 hundår
11 hundår
12 hundår
13 hundår
14 hundår
15 hundår
16 hundår

Antalet människoår
19,9 människoår
31 människoår
42,1 människoår
48,6 människoår
53,2 människoår
56,8 människoår
59,7 människoår
62,1 människoår
64,3 människoår
66,2 människoår
67,8 människoår
69,4 människoår
70,8 människoår
72 människoår
73,2 människoår
74,3 människoår
75,4 människoår

Källa:
https://nyheter24.se/nyheter/vetenskap/936284-forskare-har-ar-det-nya-sattet-att-rakna-hundar
Katter
Kattens ålder
1 år
2 år
3 år
4 år
5 år
6 år
7 år
8 år
9 år
10 år
11 år
12 år
13 år
14 år
15 år
16 år
17 år
18 år
19 år
20 år
21 år

Människans ålder
14 år
24 år
28 år
32 år
36 år
40 år
44 år
48 år
52 år
56 år
60 år
64 år
68 år
72 år
76 år
80 år
84 år
88 år
92 år
96 år
100 år

Källa: https://sverigesradio.se/artikel/4750990
Det finns två sätt att lösa detta på, det ena är att använda matematiska formler och den andra att
helt enkelt följa tabellen.
Hundar följer formeln: Människoår = 16 * ln(hundår) + 31
I c# är anger man ln(x) genom Math.log(x)
Katter följer formeln: de två första åren i en katts liv snarare kan räknas som de första 24 åren i en
människas liv. Därefter är varje människoår ungefär lika med fyra kattår.
För katter måste vi alltså ha if-satser. Det behöver vi egentligen inte med hundar.
Skriv ett program som frågar efter din ålder och räknar ut dina katt respektive hundår.

Övning 5a: En boll
Skapa ett program som skriver en liten boll på skärmen.
Tips, du kommer att behöva variabler för att hålla koll på kolumn och rad, och för att hålla koll på
lodrät och vågrät hastighet.
Bollens vågräta position = 1 (kolumn)
Bollens lodräta position = 1 (rad)
Bollens vågräta hastighet = 1
Bollens lodräta hastighet = 1
Max vågrät = 100
Min vågrät = 1
Max lodrät = 20
Min lodrät = 1
Då är det fyra variabler till…
Bollen ska ha valfri färg
Skapa en evig loop enligt följande:
While (true)
Om bollens kolumn är större än maxvärdet ska vågrät hastighet bli negativ
Eller Om bollens kolumn är mindre än minvärdet ska vågrät hastighet bli positiv
Om bollens rad är större än maxvärdet ska lodrätt hastighet bli negativ
Eller Om bollens rad är mindre än minvärdet ska lodrätt hastighet bli positiv
Addera lodrät hastighet till bollens lodräta position
Addera vågrät hastighet till bollens vågräta position
Använd CursorPosition för att placera pekaren på skärmen med de nya koordinaterna
Skriv en boll -symbol

Övning 5b : Hitta gränserna
Lek med Max och Min värdena för lodrät och vågrät gränsfall och se hur långt du kan pressa
gränserna innan din consol flippar ur eller bollen försvinner ur skärmen.

Övning 5c : Snyggare bollhantering
I början av din While(true) sats lägg till följande:
Använd CursorPosition för att placera pekaren på skärmen
Skriv ett mellanslag

I slutet av din while, innan sista måsvingen, lägg till en paus
Threading.Thread.Sleep(100); // Pausa programmet lite
