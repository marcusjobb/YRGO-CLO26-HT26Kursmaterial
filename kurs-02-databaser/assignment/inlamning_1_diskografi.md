# Inlämning 1 — Din favoritartists diskografi

**Individuell uppgift**

---

## Bakgrunden

Du är ett obotligt musiknörd som bestämt dig för att katalogisera en artists hela diskografi i en riktig databas. Inga mer Excel-ark. Inga mer anteckningsböcker. Bara ren, normaliserad SQL.

Välj en artist du faktiskt gillar. Det spelar ingen roll vem — men du ska kunna rabbla albumtitlar och låtar utan att googla. Det märks om du bryr dig.


---

## Vad du ska bygga

En SQL-databas med artistens diskografi. Den ska innehålla:

- Minst **3 album** med minst **3 låtar** vardera
- Minst **3 tabeller** med korrekta relationer
- **Primary keys** och **foreign keys** på alla kopplingar
- Normaliserad till **minst 3NF**
- Ett **ER-diagram** (relationsschema) som visar din design

---

## Föreslagna tabeller

```sql
artists (id, ...)
albums  (id, ...)
songs   (id, ...)
```

Du får utöka med fler tabeller om du vill — till exempel `tours` eller `collaborations`.

---

## Vad du ska göra

1. Forka mallen och döp forken rätt
2. Skapa databasen och tabellerna (CREATE TABLE)
3. Populera databasen med data (INSERT)
4. Skriv queries 1–5 (G-krav)
5. Skriv queries 6–8 om du siktar på VG
6. Fyll i reflektionen
7. Zippa och lämna in

---

## Queries

Skriv alla queries i `SQL-inlamning.md`.

### G — queries 1–5

1. Lista alla album, sorterade efter utgivningsår
2. Lista alla låtar på ett specifikt album (du väljer vilket)
3. Visa hur många låtar varje album innehåller
4. Hitta den längsta låten i hela databasen
5. Lista alla låtar i alfabetisk ordning

### VG — queries 6–8

6. Beräkna snittlängden på låtarna per album
7. Vilket album har flest låtar?
8. Lista alla låtar längre än 4 minuter (240 sekunder) — visa även albumtiteln

---

## Krav

### G — Godkänt

- [ ] Minst 3 tabeller med primary keys och foreign keys
- [ ] Minst 3 album med minst 3 låtar vardera
- [ ] Normaliserad till 3NF
- [ ] ER-diagram finns med i inlämningen
- [ ] Queries 1–5 skrivna och fungerar
- [ ] Minst en ny rad tillagd med INSERT
- [ ] Minst en rad borttagen med DELETE
- [ ] Minst en rad uppdaterad med UPDATE

### VG — Väl godkänt

Allt i G, plus:

- [ ] Queries 6–8 skrivna och fungerar
- [ ] Du kan muntligt förklara varför du valde din tabellstruktur
- [ ] Du kan förklara vad som händer i en JOIN
- [ ] Databasen har minst ett many-to-many-förhållande (t.ex. låt ↔ genre, artist ↔ tour)

---

## Inlämning

1. Forka mallen:  
   **[github.com/marcusjobb/clo26-diskografi](https://github.com/marcusjobb/clo26-diskografi)**

2. Döp din fork till: `förnamn-clo26-diskografi`  
   Exempel: `betty-clo26-diskografi`, `erik-clo26-diskografi`

3. Fyll i `SQL-inlamning.md` med din SQL-kod

4. Fyll i `reflektion.md`

5. Zippa hela mappen och lämna in på Google Classroom

Ingen rapport behövs.

**Deadline:** Söndag 18 oktober 23:59

---

*Lycka till. Och om du väljer Taylor Swift — All Too Well (10 minute version) är 621 sekunder.*
