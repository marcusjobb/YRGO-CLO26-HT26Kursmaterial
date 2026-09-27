# Natten då produktionsdatabasen försvann — Halloween-mysteriet

*Samma sorts deduktion som "Mordet på Metropolitan Club" i kurs-01 — men den här gången löser
ni det med `INSERT`, `UPDATE` och `SELECT` istället för papper och penna.*

**Gruppstorlek:** 2–4 personer
**Tid:** 30–45 minuter
**Kräver:** en databas ni kan köra `CREATE TABLE` i (SQLite, SQL Server, MySQL — valfritt)

---

Klockan var 02:14 på natten — mitt under Pixelfabrikens årliga Halloween-fest — när larmet gick.
Produktionsdatabasen — den med alla spelares sparfiler — var borta. Raderad. Ingen backup hade
körts på tre dagar.

Fyra personer hade jour den natten, utklädda som alla andra på festen. Alla fyra hade giltig
åtkomst. En av dem smet iväg från festen, loggade in och körde kommandot som raderade allt.

Er uppgift: **bygg en databas av ledtrådarna och deducera fram vem det var.** Precis som förra
gången räcker det inte att gissa rätt — ni ska kunna visa, med en `SELECT`-fråga, exakt hur ni
kom fram till svaret.

---

## De misstänkta

- **Erik Dahl** — backend-utvecklare, känd för att aldrig gå hem i tid
- **Mira Sandberg** — DevOps-ingenjör, ansvarig för deployflödet
- **Tobias Lindqvist** — junior-utvecklare, nyanställd sedan tre månader
- **Nadia Petrén** — säkerhetsansvarig, den som *borde* ha förhindrat det här

---

## Ditt verktyg — skapa tabellerna

Börja med att skapa en tabell för de misstänkta. Den är tom på fakta till att börja med —
ni fyller i kolumnerna allteftersom ni löser ledtrådarna:

```sql
CREATE TABLE Personer (
    Id INT PRIMARY KEY,
    Namn VARCHAR(50) NOT NULL,
    Skrivbord INT NULL,
    KoppFarg VARCHAR(20) NULL,
    Kannetecken VARCHAR(100) NULL
);

INSERT INTO Personer (Id, Namn) VALUES
    (1, 'Erik Dahl'),
    (2, 'Mira Sandberg'),
    (3, 'Tobias Lindqvist'),
    (4, 'Nadia Petrén');
```

Skapa också en liten tabell för det som faktiskt är känt om **gärningspersonen** — inte vem det
är, bara vilket kännetecken som gäller för den personen. Den kommer ni använda i slutfrågan:

```sql
CREATE TABLE Fakta (
    Nyckel VARCHAR(50) PRIMARY KEY,
    Varde VARCHAR(50)
);
```

(Ni fyller i `Fakta` när ni läser ledtråd 14 nedan.)

---

## Ledtrådarna

Varje ledtråd är ett fakta. Vissa säger var någon satt. Andra säger vad någon hade för
Halloween-mugg eller utklädnad. En del binder ihop två fakta ni redan vet. Läs dem i valfri
ordning, men lös dem i den ordning som faktiskt går att lösa — precis som förra gången.

1. Erik Dahl satt vid skrivbord 21.
2. Personen vid skrivbord 25 hade en svart Halloween-mugg.
3. Antingen Tobias eller Erik var utklädd till zombie den kvällen larmet gick.
4. Nadia var alltid utklädd till häxa, med spetsig hatt och allt.
5. En av de misstänkta loggade in via mobil hotspot natten då databasen försvann.
6. Personen utklädd till zombie hade en brun Halloween-mugg.
7. Mira var utklädd till vampyr, med cape och allt.
8. Personen vid skrivbord 27 hade ett plastpumpahuvud på skrivbordet.
9. Mira satt vid skrivbord 23.
10. Personen med pumpahuvudet hade en röd Halloween-mugg.
11. Personen vid skrivbord 23 hade en grå Halloween-mugg.
12. Häxan satt vid skrivbord 25.
13. Tobias satt vid fönsterskrivbordet — den enda hörnplatsen i hela kontorslandskapet.
14. Personen som raderade produktionsdatabasen hade en **brun** Halloween-mugg.

---

## Uppgiften

Omvandla varje ledtråd till SQL. De flesta ledtrådar blir en `UPDATE`:

```sql
-- Ledtråd 1: Erik Dahl satt vid skrivbord 21
UPDATE Personer
SET Skrivbord = 21
WHERE Namn = 'Erik Dahl';
```

Vissa ledtrådar (som nr 2, 8, 10, 12) beskriver *skrivbordet*, inte namnet direkt — då måste
ni först ta reda på vem som satt där innan ni vet vem `UPDATE` ska träffa. Ibland behöver ni
lösa en annan ledtråd först. Det är hela poängen.

Ledtråd 3 och 6 hänger ihop på samma sätt som originalmysteriet: två personer är kandidater,
men bara en av dem kan stämma med resten av det ni redan vet.

Ledtråd 14 fyller ni in i `Fakta`-tabellen:

```sql
INSERT INTO Fakta (Nyckel, Varde) VALUES ('GarningspersonensKoppfarg', 'Brun');
```

När alla fyra rader i `Personer` är kompletta ska ni kunna hitta gärningspersonen med **en
enda fråga**, utan att skriva namnet i klartext. Testa att bygga en `SELECT` som använder
`Fakta`-tabellen i en subquery, till exempel i stil med:

```sql
SELECT Namn
FROM Personer
WHERE KoppFarg = (SELECT Varde FROM Fakta WHERE Nyckel = 'GarningspersonensKoppfarg');
```

Kör den. Stämmer svaret med vad ni kom fram till för hand?

**Extra utmaning:** skriv en `SELECT` som listar alla fyra personerna sorterade efter
skrivbordsnummer, med en extra kolumn som visar `'MISSTÄNKT'` eller `'FRIKÄND'` beroende på
Halloween-muggens färg — utan att hårdkoda namnet. (Tips: `CASE WHEN`.)

---

## Diskussionsfrågor

1. Vilken ledtråd var avgörande — den som gjorde att `WHERE`-satsen i slutfrågan gick att skriva?
2. Vilka `UPDATE`-satser var ni tvungna att köra i en specifik ordning? Vad hände om ni försökte
   köra dem i fel ordning?
3. Ledtråd 5 (mobil hotspot) — behövde ni den alls för att lösa fallet? Varför finns den med,
   tror ni?
4. Jämför med hur ni löste Metropolitan Club-mysteriet på papper i kurs-01. Vad var lättare med
   SQL? Vad var svårare?
5. Koppling till felsökning: när ni letar en bugg i riktig kod, är det ofta samma process —
   samla fakta (`SELECT`), utesluta alternativ (`WHERE`), och uppdatera er förståelse
   (`UPDATE` er egen mentala modell). Kan ni känna igen det mönstret här?

---

## Tips om ni kör fast

- Börja med de ledtrådar som direkt namnger en person (`Namn = '...'`) — de kan ni `UPDATE`:a
  direkt utan att behöva veta något annat först.
- Kör `SELECT * FROM Personer;` ofta under vägen för att se vad ni faktiskt vet och vad som
  fortfarande är `NULL`.
- Vissa ledtrådar beskriver "personen vid skrivbord X" — de kan ni inte lösa förrän ni vet
  *vem* som satt vid skrivbord X. Vänta med dem.
- Om två ledtrådar pekar på samma person men olika kandidater (som 3 och 6) — lös den ena
  ledtråden fullt ut för alla, sen kolla vem av kandidaterna som faktiskt stämmer.

> 💬 *Tänk på `Personer`-tabellen som er anteckningslapp. `UPDATE` är hur ni fyller i den —
> en kolumn i taget, ett fakta i taget.*

---
*Läraren leder avslutningsdiskussionen. Ingen komplett lösning finns i den här filen — det är
meningen att gruppen (eller läraren live) bygger fram den.*
