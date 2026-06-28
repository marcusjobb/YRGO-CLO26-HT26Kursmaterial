---

title: Bankkonton Och Tdd
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/BDD/Bankkonton och TDD.docx"
description: "I dagens TDD övning ska vi simulera bank."
tags: ["bankkonton", "csharp", "exercise", "tdd.docx", "test", "testing"]
week_fit: []
---
Bankkonton och TDD
I dagens TDD övning ska vi simulera bank.
Det finns olika sorts bankkonto
1. Sparkonto som tillåter max 5 uttag om året
(fler kan göras men då kostar det 1% av uttaget)
2. Lönekonto som tillåter uttag utan hinder
3. Kreditkonto som tillåter kredit över en viss gräns
4. Investeringskonto som tillåter ett uttag om året
Så nu ska vi skapa TDD för bankkonton
Vilka regler gäller för bankkonton rent allmänt?





Man får inte ta ut mer pengar än vad som finns i kontot
o Om kredit finns får man inte ta ut mer än krediten
Man får inte sätta ut minusvärden ( alltså sätta in -100:- )
Bankens egna avgifter tas ut även om kontot är på minus
Cash Insättningar på Max 15000 åt gången (annars kan man börja misstänka att det är
pengatvätt), inga fler insättningar tillåts den dagen.

Konton
Vi behöver en klass för bankkonton, men för att slippa göra en massa specialregler, skapa en klass med
bankkonto och låt sedan kreditkonto och sparkontot ärva, och implementera de regler som gäller för
dem.















MoneyLaundryWarning (property bool)
Cash (property double)
HasCard (property bool)
HasSwish(property bool)
MaxSwishPerWeek (property double)
CurrentSwishAmount (property double)
AccountNr (property string)
Deposit (double amount, string note)
Widthdraw (double amount, string note)
MoveMoneyTo(AccountNr, string note)
Autogiro (double amount string company)
Cardused(double amount, string purchase)
SwishRecieve(double amount, string phoneNumber)
SwishSend(double amount, string phoneNumber)

Kreditkort eller kreditkonto


CreditLimit (property double)

Sparkonto



MaxWithdraws (property int)
WithdrawsThisYear (property int)

Regler
Nu ska ni skapa ett projekt för bankkonton och ett tillhörande projekt för TDD
Ni ska komma på olika sätt man kan luras med banken, och skapa tester som bevisar att det inte är
möjligt att luras.
Kan man sätta in minusvärden i kontot?
Kan man ta ut mer pengar än vad kontot har?
Kan man flytta pengar mellan konton om täckning inte finns?
Kan man stoppa in pengar på kontot om kontot har varningsflagga?
Triggas varningsflaggan om man gör en insättning på 15000?
Triggas varningsflaggan om man gör en insättning på 14999? Och sedan gör en insättning till på 1:Triggas varningsflaggan om man gör en insättning på 14999? Och sedan flyttar över 20:- från ett annat
konto?
Kan man flytta pengar till och från om kontot har varningsflagga? (bör man kunna det?)
Butikskortläsare är inte alltid snabba på sina bankärenden, detta kan innebära att köp godkänns trots att
kontot saknar täckning. Exempelvis om ett köp görs 14:30 på måndag och godkänns, men själva
transaktioner kommer inte in förrän onsdag midnatt. Vilket innebär att man kan ha råkat tömma kontot
efter köpet och kontot kommer att vara på noll, men då köpet har godkänts kommer pengarna att dras
ändå. Vissa modernare kortläsare maskiner kopplar sig dock till kontot direkt. Men om datumet skiljer
på mer än 12 timmar bör uttaget godkännas, då kunden fått sin vara och försäljaren kommer att hamna i
kläm om de inte får betalt (egentligen ska man dubbelchecka historiken men vi bryr oss inte om det)
Swish har en gräns på 3000:- per vecka som standard (att ta emot är OK dock).
Vad händer om du swishar för 3000 (gränsen) och höjer gränsen till 5000, därefter swishar du 2000 till?
Vad händer om du swishar för 3000 (gränsen) och sänker gränsen till 2000?
Finns det fler regler att ta upp och kontrollera?
Diskutera och testa

Ni får välja ut en representant som ska visa presentera era tester nästa lektion!

Exempel på testflöde
Swish Test 1
1.
2.
3.
4.
5.

Skapa ett bankkonto
Koppla Swish till den
Sätt in 4000:Swisha 2000:Det bör fungera utan problem

Swish Test 2
1.
2.
3.
4.
5.
6.
7.

Skapa ett bankkonto
Koppla Swish till den
Sätt in 4000:Swisha 2000:Swisha 1000:Swisha 10:Vid sista försöker ska Swish neka då maxgränsen är nådd

Swish Test 3
1.
2.
3.
4.
5.
6.
7.
8.
9.

Skapa ett bankkonto
Koppla Swish till den
Sätt in 4000:Swisha 2000:Swisha 1000:Ändra Swish maxgräns till 4000
Swisha 1000:Swisha 10:Vid sista försöker ska Swish neka då maxgränsen är nådd
