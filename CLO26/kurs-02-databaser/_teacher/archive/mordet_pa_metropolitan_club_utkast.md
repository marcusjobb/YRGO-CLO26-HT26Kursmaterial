# Mordet på Metropolitan Club

🟡



Vi ska köra en SQL övning genom att lösa ett mord. Bara för att visa att databaser är mer än bara personregister. Det kan användas till mer coola saker.

Jag har hämtat informationen från min en av mina favoritböcker, "Elementary Basic – learning to program computers in Basic with Sherlock Holmes".

ISBN – 91-14601-6

I den boken beskrivs fall som Sherlock Holmes får lösa med en uråldrig maskin som använder sig något Basic-aktigt språk.

Nog om det… Vi börjar med fallbeskrivningen.


H

ela London följde intresserat – och societeten var bestört över – mordet på en välrenommerad konsthandlare. Mordet skedde på Metropolitan Club, under synnerligen egendomliga och oförklarliga omständigheter.

Allmänheten delgavs endast vissa detaljer om brottet, medan en betydande mängd fakta hemlighölls eftersom det rörde framstående medlemmar av societeten. Nu ska jag inte gå in på dessa fakta då Sherlock Holmes bett om min fullständiga diskretion.

"Du minns säkert vårt besök på Metropolitan Club i förra veckan", började Holmes, "och det ryktbara föremålet för vår undersökning."

"Scotland Yards förundersökning avslöjade fyra misstänkta", sade han och rekapitulerade fallet. "Fyra personer var inbokade i angränsande rum ovanför själva klubben. En av dem var Sir Raymond Jasper, en framstående jurist. Den andra var en revisor vid namn Robert Holman. De båda övriga, övriga Reginald Woodley och en mr James Pope, var besökare från Northumberland – utan samröre med varandra, vilket bör tilläggas. Polisen sammanställde en lista med fakta rörande fallet och de fyra herrarna, men sin vana trogen lyckades den inte upptäcka det egendomliga i ett antal detaljer och förbisåg därmed deras betydelse. Jag tycks aldrig få dem att inse betydelsen av att studera människors skjortärmar eller de ledtrådar som en skosula lämnar."


"Här har vi en skiss över några av klubbens uthyrningsrum och en lista över de ledtrådar vi samlade."


- Sir Raymond Jasper bodde på rum 10

- Mannen i rum 14 hade svart hår

- Antingen Överste Woodley eller Sir Raymond bar pincené

- Mr Pope bar alltid ett fickur i guld

- En av de misstänkta sågs åka i en fyrhjulig hästdroska

- Mannen med pincené har brunt hår

- Mr Holman bar en klackring i rubin

- Mannen på rum 16 hade trasiga manschetter

- Mr Holman bodde på rum 12

- Mannen med de trasiga manschetterna hade rött hår

- Mannen på rum 12 hade grått hår

- Mannen med fickuret bodde i rum 14

- Övereste Woosley bodde i ett hörnrum

- Mördaren hade brunt hår

"Nå min vän", sa jag efter att ha studerat listan, "det är möjligt att de här ledtrådarna är bra, men en hastig blick på dem säger mig inte vem som var mördaren."

"Det är därför polisen förbisåg dem. Ledtrådarna är värdelösa såvida vi inte kan bestämma ett speciellt förhållande mellan dem emellan och se hur de passar in i större mönster. För att göra det måste vi fundera ut en algoritm som vi kan följa."

Vad Holmes menade var att vi skulle titta igenom ledtrådarna, en efter en och samla informationen som går att samla. Om ledtråden inte går att använda det, lägg det åt sidan och läs nästa ledtråd. När du gått igenom listan, börja igen med ledtrådarna du lagt åt sidan, tills du hittat mördaren.

Vi ska dock inte programmera detta i Basic, som i boken, utan i SQL.

Vi börjar med att skapa en tabell

```

CREATE TABLE [dbo].[MetropolitanClub](
[Hårfärg] [nvarchar](50) NULL,
[Transport] [nvarchar](50) NULL,
[Kännetecken] [nvarchar](50) NULL,
[Rumsnr] [nvarchar](50) NULL,
[Namn] [nvarchar](50) NULL
) ON [PRIMARY]
```


Nu ska vi ta en ledtråd i taget och använda antingen INSERT INTO eller UPDATE. Vi tittar på de två första ledtrådarna.  Om du kör detta vi SSMS behöver du inte tänka på parametrar. Om du skriver kod i C# måste du ha parametrar.

- Sir Raymond Jasper bodde på rum 10

```
INSERT INTO MetropolitanClub (Namn, RumsNr) VALUES ('Raymond Jasper','10');
```

- Mannen i rum 14 hade svart hår

```
INSERT INTO MetropolitanClub (Kännetecken, RumsNr) VALUES ('Svart hår','14');
```


Fortsätt nu med övriga ledtrådar och samla ihop din lista av INSERTS och UPDATES i den ordning du skriver dem. Så att du kan jämföra med andra senare. Som du ser blir inte alla Inserts likadana, den första lagrade namn och den andra kännetecken. Dock är Rumsnummer är bra sätt att länka ihop ledtrådarna.

Glöm inte att köra SELECT * from MetropolitanClub då och då för att se resultatet

(Den första raden som raderar allt är för att nollställa tabellen innan alla inserts körs)


Lösningen kommer i ett senare dokument…

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
