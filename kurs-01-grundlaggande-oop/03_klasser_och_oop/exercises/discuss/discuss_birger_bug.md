# Birger Bugg — Diskutera mera! 💬

*En berättelse i tre akter om Viktor Viber, en bugg som fick ett eget namn, och valet som ingen vill behöva göra.*

**Gruppstorlek:** 3–4 personer
**Tid:** 30–40 minuter

---

## Akt 1 — Telefonsamtalet

Det är en tisdagskväll. Viktor Viber sitter och experimenterar med Claude Code när telefonen ringer.

Det är Maria från KundAB. Hon är lite stressad.

*"Viktor, hej! Förlåt att jag ringer direkt men det är lite bråttom — det är nåt konstigt med inloggningen för admin-kontot. Kan du kolla?"*

Viktor är lite inne i sitt flow men säger ja. Han kör programmet, loggar in som admin.
Buggen dyker upp direkt. Tydlig. Reproducerbar.

*"Jag kollar på det nu"*, säger han och lägger på.

Han öppnar sin AI-assistent och klistrar in felmeddelandet.
AI:n spottar ut en lösning på trettio sekunder. Viktor läser igenom den snabbt. Ser rimlig ut.
Han kör programmet. Buggen syns inte längre.

*Perfekt*, tänker Viktor. *Maria kommer att bli glad på morgonen.*

Han pushar till main. Stänger laptopen. Går och lägger sig.

---

## Diskutera — efter akt 1

*Pausa här. Diskutera i gruppen:*

1. Hur många fel har Viktor gjort hittills?
2. Vad borde ha hänt istället för att Maria ringde Viktor direkt?
3. Varför är det ett problem att "buggen syns inte längre" inte är samma sak som "buggen är fixad"?

---

## Akt 2 — Birger Bugg vaknar

Tre veckor går. Ingen hör något. Viktor har glömt bort det hela.

Sedan ringer telefonen igen — men den här gången ringer Maria till **projektledaren**.

*"Det är något väldigt konstigt med systemet. Alla verkar vara administratörer. Inklusive våra kunder. De kan se varandras... ja, allt."*

Projektledaren hämtar Kalle. Kalle hämtar Fredrik. De börjar gräva.

Det tar inte lång tid att hitta vad AI:n gjorde: djupt inne i autentiseringskoden sitter en liten rad som Viktor aldrig förstod:

```csharp
user.SetAdmin = true;
```

AI:n hade inte fixat buggen. AI:n hade kringgått den — genom att sätta **alla användare** som administratörer. Buggen försvann för att alla fick tillgång till allt.

Och det värsta: varje användare som loggat in de senaste tre veckorna har `IsAdmin = true` sparat i databasen. Koden kan ändras på en minut. Men datan — datan sitter kvar.

*Hej, Birger Bugg. Välkommen till systemet.*

---

## Diskutera — efter akt 2

1. Varför hjälper det inte att bara ändra tillbaka koden?
2. Hur lång tid tror ni det tar att fixa det här egentligen?
3. Vad har de drabbade kunderna rätt att kräva?
4. Viktor "testade att buggen var borta" — varför var det ett otillräckligt test?

---

## Akt 3 — Loggarna ljuger aldrig

Maria ringer tillbaka med ett meddelande från sin chef:

*"Vi betalar inte för det här. Det är ett problem ni orsakat."*

Projektledaren behöver veta vem som pushade ändringen. En snabb titt i loggarna:

```
2026-10-14 23:47 — commit: "fix admin login bug" — v.viber
```

Viktor kallas in till mötet.

Han förklarar att han "bara testade AI:ns lösning" och att "den verkade fungera".

Det är tyst en lång stund i rummet.

---

## Diskutera — efter akt 3

1. Vad borde Viktor säga på mötet?
2. Är det ett bra försvar att "AI:n föreslog det"?
3. Vad händer med Viktor — varning, avsked, eller något annat?
4. Vad borde **företaget** ha haft på plats för att det här aldrig skulle kunna hända?
5. Om ni var Viktor — hade ni direkt berättat vad som hänt, eller hoppats att ingen märker?

---

## Den jobbiga frågan

> Marcus råkade en gång pusha känsliga uppgifter till ett publikt repo.
> Hans val: berätta direkt för alla berörda, ta samtalen med rektor och jurister.
>
> *"Det var inte kul att ta smällen — men hellre det än att hålla tyst och åka fast senare."*
>
> Viktor valde att inte berätta. Loggarna berättade istället.
> **Vad väljer ni?**

---
*Läraren leder avslutningsdiskussionen.*

