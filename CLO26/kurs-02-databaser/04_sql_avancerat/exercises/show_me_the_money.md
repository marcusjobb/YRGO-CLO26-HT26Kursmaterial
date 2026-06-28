---

title: Show me the money
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 2
language: sql
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/Show me the money.md"
description: "Eller 'Hur jag blev kompis med aggregatfunktioner."
tags: ["aggregat", "avg", "count", "databaser", "exercise", "group-by", "join", "money!", "show", "sql"]
week_fit: []
---

# Show me the money!

🟡



Eller "Hur jag blev kompis med aggregatfunktioner."

Nu har vi gjort tabeller om människor och bilar och äpplen och annat skoj, så nu gör vi nåt vettigare med databasen.

- Skapa en databas som du kallar MoneyTalks

- I den skapa en följande tabeller

Purchases

| Fält | Typ |
| --- | --- |
| ID | Int, indentifikation |
| Item | nvarchar(50) |
| Price | Float, default = 0 |
| DateOfPurchase | Date, default = GETDATE() |
| StoreId | int |


Purchases.Id är tabellens Primary Key

Purchases.StoreId är en Foreign key till Store.Id

Store

| Fält | Typ |
| --- | --- |
| ID | Int, indentifikation |
| Name | nvarchar(50) |
| City | nvarchar(50) |


Store.Id  är primary key för tabellen


Lite mer vi kan observera.

Att varje butik kan kopplas till många olika inköp, men varje inköp kan bara kopplas till en butik (vilket är logiskt iofs).

I databasvärlden tänker man såhär "En purchase har relationen 1-1 till en Store, men en Store har relationen 1 till många purchases"

Egentligen är detta ingen störren skillnad på hur det är när vi skapar klasser i C#

```


public class Purchases
{
public int Id { get; set; }
public string Item { get; set; }
public double price { get; set; } = 0;
public DateTime DateOfPurchase { get; set; } = DateTime.Now;
public long StoreId { get; set; }
}
public class Purchases
{
public int Id { get; set; }
public string Name { get; set; }
public string City { get; set; }
}
```


Men nu ska vi inte skapa klasser… än… vi ska kolla på databasen.

För att kunna testa så skapar vi två butiker, välj valfria namn och städer, i mitt exempel ser det ut så

```


INSERT INTO Stores ([Name], [City])
VALUES
('Hemskköp','Onsala'),
('Knas Ohlson','Kungsbacka'), 
('Hemskköp','Göteborg'),
('Elskjöp','Bergen');
```


Och nu lägger vi in ett några inköp

```


INSERT INTO Purchases ([Item], [Price], [DateOfPurchase],[StoreId])
VALUES
('Kattmat',19.25, '2021-01-03 12:34',2 ),
('Bröd',24.50, '2021-01-04 12:34',2 ),
('Brödrost',245.95, '2021-01-05 18:25',1 ),
('Monster',16.95, '2021-01-10 11:12',3 ),
('Yogurt',14.90, '2021-01-12 13:37',3 ),
('Mobiltelefon',8245.95, '2021-02-15 1:45',4 );
```


Där vi kan kallt konstatera att Hemskköp tar överpris på allt

Vill vi nu se vad vi har i vår databas kan vi kolla det med vanliga sökningar

```
Select * from Purchases
```

respektive

```
Select * from Stores
```

Men det säger inte mycket, så vi slår ihop tabellerna

```
Select * from Purchases, Stores
```


Och då får vi en fin lista där databasen snällt kopplat ihop alla inköp med alla butiker, detta innebär att enligt den har vi köpt 1st av varje vara i varje butik. Inte riktigt vad vi ville ha. Vi får alltså sätta ett filter så att den vet hur den ska koppla tabellerna.


```


Select * from Purchases, Stores
WHERE
Purchases.StoreId = Stores.Id
```


På detta sätt ställer vi frågan mot flera tabeller samtidigt. Innan har vi använt filtreringen i Where till att hantera egenskaper som ålder, men vi kan använda det för att jämföra mellan två tabeller, så vi gör det nu och kollar att Purchases.StoreId == Stores.Id

Nu kan vi se sambandet mellan tabellerna, men resultatet är inte vackert.

Vi försöker igen, istället för att ta med allt genom att skriva * så skriver vi in fälten vi vill ha

```

Select DateOfPurchase, Name, City, Item from Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
```


Nu kan vi se ett mönster i det. Grymt coolt ju!

Så nu ska vi se vad mer vi kan göra…

Vi kan ta reda på hur många gånger vi har handlat i en butik, så kan vi använda funktionen Count(). Man talar om för den vilket fält som den ska räkna på och sen gör den det.

```

Select Count(Stores.Id) from Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
```


Det låter bra i teorin men i praktiken får vi bara en summa på antal rader som har ett värde. 
Bummer!

Vi försöker igen, vi talar om för den att den ska visa namnet också.

```

Select Name, City, Count(Stores.Id) from Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
```


Då får vi ett error… vilken buzzkill!


Det är nämligen så att när man använder funktioner för att göra beräkningar så behandlar dessa funktioner många rader samtidigt, och när vi säger att vi vill se Namn på butiken så vet den inte vilken rad vi menar. I normala fall skulle detta innebära alla butiker i alla rader, men funktionen klumpar ihop flera rader. Det vi får göra är att tala om för den att gruppera raderna som är likadana, alltså alla rader med samma namn. Då får vi Alla namn, men bara en unik per rad och vi får summan av antalet. Jisses det blev en lång förklaring. Let's roll!

```


Select Name, Count(Name) from Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
Group by name
```


Nämen se där… det blev genast mycket bättre. Nu vet vi hur många gånger vi handlar var. När vi nu kollar på resultatet ser vi också att antalet har ett knasigt kolumnnamn, vi kan egentligen döpa om den till vad som helst med hjälp av kommandot AS.


```


Select Name, Count(Stores.Id) as VisitsToTheFreakingStore from Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
Group by name
```

Ok Kanske lite väl att ta i, vi gör det lite snyggare.

```


Select Name, Count(Name) as Visits from Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
Group by name
```


Sådärja! Nu vet vi hur många gånger vi besökt en butik men vi vet inte hur mycket pengar vi slösat där.

Vi har tillgång till funktionen Sum() som summerar alla värden i ett fält, enligt de ramar vi ger den.


Om vi kör


```

Select Sum(Price) as WastedMoney from Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
```

får vi reda på hur mycket pengar vi använt upp allt som allt. Så vi får gruppera informationen igen för att få mer specifik information.


```


Select Name, City, Count(Stores.Id) as Visits, Sum(Price) as WastedMoney, 
       Max(Price) as MostExpensive
From Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
Group by Name, City
```


Nu så! Vi lägger till lite sortering så kommer det här att bli grymt!


```


Select Name, City, Count(Stores.Id) as Visits, Sum(Price) as WastedMoney, 
       Max(Price) as MostExpensive
From Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
Group by Name, City
Order By WastedMoney Desc
```


Nu kan vi alltså se var vi slösat mest pengar. Då vill vi självklart veta vad som är billigast, motsatsen till Max är Min.

```


Select Name, City, Count(Stores.Id) as Visits, Sum(Price) as WastedMoney,
Max(Price) as MostExpensive, Min(Price) as LeastExpensive
From Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
Group by Name, City
Order By WastedMoney Desc
```

Vad är medel av det vi köper? AVG – i det här fallet inte ett antivirus, utan en förkortning på Average.

Så medelvärdet av alla pengar vi slösat bort är följande

```


Select AVG(Price) as AveragePrice
From Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
```


Och så lägger vi det i vår stora SQL sats.

```


Select Name, City, Count(Stores.Id) as Visits, Sum(Price) as WastedMoney,
Max(Price) as MostExpensive, Min(Price) as LeastExpensive,
AVG(Price) as AveragePrice
From Purchases, Stores
WHERE Purchases.StoreId = Stores.Id
Group by Name, City
Order By WastedMoney Desc
```

Hur coolt som helst. Vi kan alltså få fram statistik på våra inköp genom de olika aggregatfunktionerna.


Så vad kan vi nu göra med detta.

Vi skulle kunna skapa en vy med vår stora SQL sats.

Högerklicka på vyer och välj "New view".


Tryck bara close, vi behöver inte välja just nu

I fönstret som dyker upp, i mitten finns en liten söt ruta där det står SELECT FROM, klistra in SQL koden där och tryck CTRL + R.


Nu kommer fönstret att uppdatera sig med våra tabeller och alla fälten vi valt.

Spara den som HowIWasteMoney eller någon trevligare vy namn. Den kommer att varna att Order By kan bråka men spara ändå. Nu kan du anropa den som om det vore en tabell.

```
Select * from HowIWasteMoney;
```

Om du känner för att göra det mer användbart, skapa vyer av de olika sökningar vi gjort. Fördelen med vyerna är att du kan anropa dem som tabeller. Hur som helst, nu har du lärt dig hur man använder aggregatfunktionerna.

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
