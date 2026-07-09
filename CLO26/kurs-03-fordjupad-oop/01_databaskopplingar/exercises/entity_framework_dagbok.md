# Entity Framework Dagbok

🔴



Då vi börjat med Entity Framework är det lika bra att ge sig på det hands on.

Vi börjar med en enkel liten modell och döper den till Anteckningar

| Publika fält | Förklaring |
| --- | --- |
| ID | Index för posten som skapas |
| Datum | DateTime |
| Titel | String med titeln för din anteckning |
| Anteckning | String med din text som du vill spara |


Det är allt.

Skapa nu din Databasclass som du kan kalla för Dagbok och glöm inte att ärva från DbContent. 
Kör sedan i Package Consolen för att skapa din första Migration. Kör sedan en Database-Update och ditt program är redo att köras.

I main ska du nu visa en meny

- Spara anteckning

- Visa lista

- Visa anteckning

- Sök anteckning

- Avsluta

Spara anteckning

Ta emot inputs från användaren tills användaren skriver en tomrad, sedan sparar du allt i databasen. (inklusive dagens datum)


Visa lista

Visa en lista på alla anteckningar som sparats, begränsa dock anteckningsfältets output till 40 tecken och ersätt radbyte (\r\n) med mellanslag.


Sök anteckning på datum

Fråga efter datum och sök efter det specifika datumet (även om man inte angett tid), visa innehållet i anteckningen. Om flera hittas, så lista allihopa. Om bara en hittas, visa hela anteckningen.


Sök anteckning på titel

Fråga efter titel och sök efter den specifika titeln, visa innehållet i anteckningen. Om flera hittas, så lista allihopa. Om bara en hittas, visa hela anteckningen.


Sök anteckning på ord

Fråga efter specifik ord som finns i anteckning (kan vara flera ord i rad också) och sök efter det specifika ordföljden, visa innehållet i anteckningen. Om flera hittas, så lista allihopa.


Lite rolig program ändå. Du kan använda det som dagbok, den kommer dock inte att vara krypterad… men bättre än inget…

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
