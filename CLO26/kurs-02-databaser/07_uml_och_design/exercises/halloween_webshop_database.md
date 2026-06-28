---

title: Gruppuppgift: Halloween Webshop Database Design
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 3
language: python
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/exercises/groupassignment/halloween-webshop-database.md"
description: "Tidsdag + kort presentation på onsdag"
tags: ["database", "databaser", "design", "exercise", "git", "gruppuppgift:", "halloween", "ssh", "verktyg", "visual-studio"]
week_fit: []
---

# Gruppuppgift: Halloween Webshop Database Design

🔴


## Tidsram

Tidsdag + kort presentation på onsdag

## Bakgrund

Ni har fått i uppdrag att designa databasen för en ny Halloween-webshop som ska
sälja kostymer, dekorationer och godis. Detta är er första normaliserings-uppgift
så vi håller det enkelt men realistiskt.

Uppgiften handlar om att planera en databas och rita upp den med UML. Ni kan använda **[dbdiagram.io](https://dbdiagram.io)** men ni kan också använda papper och penna och fotografera det.

**VIKTIGT** Ni ska bara planera databasen, inte implementera den i SQL. Inte skapa en färdig webshop. Bara planera databasen och diskutera era val.

Gruppuppdelningen finns här: [groups.md](./groups.md)

## Uppgiften

Designa en komplett databasstruktur i **[dbdiagram.io](https://dbdiagram.io)** och
förbered en kort presentation (5-10 min) av era val.

## Vad webshopen behöver hantera

### Kunder

- Kundinformation för registrerade användare
- Leveransadresser (en kund kan ha flera)
- Kontaktuppgifter

### Produkter

- Halloween-kostymer (olika storlekar)
- Dekorationer
- Godis och snacks
- Produktbilder och beskrivningar
- Lagerantal

### Beställningar

- Kundorder med flera produkter
- Orderstatus (pending, shipped, delivered, cancelled)
- Orderdatum och leveransdatum
- Totalpris

### Kategorier

- Produkter ska kunna grupperas i kategorier
- En produkt kan höra till flera kategorier (t.ex. "Barn" och "Kostymer")

## Hints och tips

Att läsa:

- [Relationsdatabaser](https://campusmolndaleducation.github.io/csharp_cmyh/Docs/C-Sharp/databases/relational_databases/)
- [Databasplanering](https://campusmolndaleducation.github.io/csharp_cmyh/Docs/C-Sharp/databases/database_planning/)
- [UML och ER-diagram](https://campusmolndaleducation.github.io/csharp_cmyh/Docs/C-Sharp/databases/uml_database_design/)
- [Normalisering](https://campusmolndaleducation.github.io/csharp_cmyh/Docs/C-Sharp/databases/normalization/)
- [SQL-Zoo Normalisation](https://sqlzoo.net/wiki/Data_normalization)
- [Tutorial points UML](https://www.tutorialspoint.com/uml/index.htm)

### Ritverktyg

- [dbdiagram.io](https://dbdiagram.io/home) - Gratis och enkel att använda
- [yuml.me](https://yuml.me/) - Enkel textbaserad syntax
- [draw.io](https://app.diagrams.net/) - Mer avancerade diagram

### Normalisering

Tänk på:

- Ingen upprepning av data (eller så lite som möjligt)
- Varje tabell ska ha ett tydligt syfte
- Använd PRIMARY KEY och FOREIGN KEY korrekt

### Förslag på tabeller att överväga

- `Customers` - Kundinformation
- `Addresses` - Leveransadresser
- `Products` - Produktinformation
- `Categories` - Produktkategorier
- `Orders` - Beställningar
- `OrderItems` - Produkter i en beställning
- `ProductCategories` - Koppling mellan produkter och kategorier

### Datatyper att tänka på

- Email: VARCHAR(255)
- Telefonnummer: VARCHAR(20)
- Pris: DECIMAL(10, 2)
- Datum: DATE eller DATETIME
- Status: ENUM eller VARCHAR
- Beskrivningar: TEXT

### Relationer att fundera över

- En kund kan ha flera adresser
- En order tillhör en kund
- En order innehåller en till många produkter
- En produkt kan finnas i många orders
- En produkt kan tillhöra flera kategorier

## Leveranser

### 1. ER-Diagram i dbdiagram.io

Skapa ert diagram med:

- Alla tabeller med kolumner och datatyper
- PRIMARY KEY markerade
- FOREIGN KEY relationer ritade
- Kardinalitet angivna (1:1, 1:N, N:M)

### 2. Presentation (5-10 min)

Förklara:

- Er övergripande struktur
- Varför ni valde dessa tabeller
- Hur ni hanterade N:M relationer
- Minst ett normaliseringsval ni gjorde

### 3. Dokumentation

En kort README.md som förklarar:

- Vilka tabeller ni har
- Viktiga designval
- Eventuella antaganden ni gjorde

## Vad vi letar efter i presentationen

- Fungerar grundstruktur för kunder, produkter och orders
- PRIMARY och FOREIGN KEYs korrekt använda
- Förståelse för hur ni undvek dataduplicering
- Förklaring av era designval
- Hur ni hanterade N:M relationer

## Resurser

- **dbdiagram.io** - Gratis ER-diagram verktyg
- **Normalisering** - Se kursmaterialet om 1NF, 2NF, 3NF
- **Datatyper** - MySQL/SQL Server datatype reference

## Frågor att diskutera i gruppen

1. Ska vi spara orderhistorik även när produkter tas bort från sortimentet?
2. Hur hanterar vi produkter med varianter (t.ex. olika storlekar)?
3. Behöver vi spara priset både på produkten OCH i orderraden? Varför?
4. Hur många adresser kan en kund ha?
5. Kan en produkt vara slut i lager men ändå kunna beställas?

## Tips för grupparbetet

1. **Börja enkelt** - Skissa på papper först
2. **Normalisera steg för steg** - Identifiera 1NF, sedan 2NF, sedan 3NF
3. **Diskutera** - Olika perspektiv ger bättre design
4. **Testa** - Går det att lägga en order? Uppdatera lager? Ta bort en produkt?
5. **Dokumentera** - Anteckna era val medan ni fattar dem

Lycka till! 🎃👻🦇

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
