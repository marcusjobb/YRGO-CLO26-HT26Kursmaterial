# Övning 1: SQL vs Dokumentdatabas - Produktkatalog

🟢


## 🎯 Mål med övningen

Efter den här övningen kommer du att kunna:

- Förstå skillnaden mellan SQL och dokumentdatabas-modellering
- Identifiera när en dokumentdatabas är bättre än SQL
- Designa en flexibel datastruktur för varierande produkttyper
- Resonera om tradeoffs mellan normalisering och denormalisering

## 🧩 Uppgiften

Ni jobbar på en webshop som säljer **allt möjligt**: elektronik, kläder, böcker, möbler.

Problemet?

Varje produktkategori har **helt olika** attribut:

- **Laptop**: RAM, processor, skärmstorlek, grafikkort
- **T-shirt**: Storlek, färg, material, passform
- **Bok**: Författare, sidantal, ISBN, förlag
- **Soffa**: Material, antal sittplatser, färg, mått

Er uppgift är att **designa datastrukturen** för denna produktkatalog - både i SQL och som dokumentdatabas.

## 🚀 Del 1: SQL-Design

Hur skulle ni modellera detta i SQL?

Tänk igenom:

1. Vilka tabeller behöver ni?
2. Hur hanterar ni att olika produkter har olika attribut?
3. Hur många JOIN-operationer krävs för att visa EN produkt med alla detaljer?

Skissa upp tabellstrukturen (ni behöver inte skriva SQL-kod, bara beskriv):

```
Tabell: Products
- Id
- Name
- Price
- CategoryId

Tabell: Categories
- Id
- Name

Tabell: ??? (hur hanterar ni attributen?)
```

## 🤔 Fundera På

**Scenario 1:** Marketing vill lägga till ett nytt attribut "Eco-Friendly Badge" för vissa produkter.

- Hur påverkar det er SQL-design?
- Behöver ni en migration?
- Hur många tabeller påverkas?

**Scenario 2:** Ni får 1000 requests/sekund för att visa produkter.

- Hur många queries krävs per produktvisning?
- Hur snabbt blir det?

## 💡 Del 2: Dokumentdatabas-Design

Nu designar ni samma produktkatalog som MongoDB-dokument.

Här är ett förslag för en laptop:

```json
{
  "_id": "prod_12345",
  "name": "MacBook Pro 14",
  "category": "laptop",
  "price": 25000,
  "inStock": true,
  "specs": {
    "ram": "16GB",
    "processor": "M2 Pro",
    "screen": "14 inch",
    "graphics": "Integrated"
  },
  "tags": ["apple", "professional", "portable"]
}
```

**Er uppgift:**

Skapa liknande dokument för:
1. En T-shirt
2. En bok
3. En soffa

Tänk på:
- Vilka fält är gemensamma för **alla** produkter?
- Vilka fält är specifika per kategori?
- Hur hanterar ni variationer (t-shirt med olika storlekar)?

## 🕵️‍♂️ Del 3: Jämför Lösningarna

Fyll i denna tabell baserat på era designs:

| **Aspekt** | **SQL** | **MongoDB** |
|------------|---------|-------------|
| Antal tabeller/collections | ? | ? |
| Queries för att visa 1 produkt | ? | ? |
| Lätt att lägga till nytt attribut? | ? | ? |
| Risk för data-inkonsistens | ? | ? |
| Prestanda vid hög läslast | ? | ? |

## 🤝 Diskussion i paret

Snacka ihop er!

1. **Vilken lösning känns "renare"?** SQL med strikta regler eller MongoDB med flexibilitet?

2. **Vad händer om ni behöver söka efter "alla produkter med RAM > 8GB"?**
   - Hur gör ni det i SQL?
   - Hur gör ni det i MongoDB?

3. **Vad händer om priset på MacBook Pro ändras och samma produkt är refererad i 50 kundvagnar?**
   - Hur hanterar SQL detta?
   - Hur hanterar MongoDB detta?

## 🔥 BONUS: Hybrid-Lösning

Vissa företag använder **båda**.

Fundera på:
- Vad skulle ni lagra i SQL?
- Vad skulle ni lagra i MongoDB?
- Hur skulle de kommunicera?

Exempel:
- **SQL**: Beställningar, användare, betalningar (affärskritiskt!)
- **MongoDB**: Produktkatalog, recensioner, loggning (flexibelt!)

Skissa upp en arkitektur för detta.

## 💭 Reflektionsfrågor

Innan ni går vidare, svara på:

1. **Vilken databas skulle ni välja för denna webshop?** Motivera.

2. **Finns det någon situation där SQL skulle vara BÄTTRE för produktkatalogen?**

3. **Vad är den största RISKEN med att använda MongoDB här?**

<details>
<summary>💡 Klicka här för diskussionspoäng och insikter</summary>

## SQL-Design: EAV-Pattern

Ett klassiskt approach är **Entity-Attribute-Value**:

```
Tabell: Products
- Id, Name, Price, CategoryId

Tabell: ProductAttributes
- Id, ProductId, AttributeName, AttributeValue
```

**Problem:**
- För att visa en produkt: JOIN med ProductAttributes
- Varje attribut är en rad → många rader per produkt
- Queries blir komplexa och långsamma
- Svårt att validera datatyper (allt är strings)

## MongoDB-Design: Flexibla Dokument

T-shirt exempel:

```json
{
  "_id": "prod_67890",
  "name": "Star Wars T-Shirt",
  "category": "clothing",
  "price": 299,
  "inStock": true,
  "specs": {
    "size": "L",
    "color": "Black",
    "material": "100% Cotton",
    "fit": "Regular"
  },
  "tags": ["starwars", "nerd", "comfortable"]
}
```

Bok exempel:

```json
{
  "_id": "prod_11111",
  "name": "Clean Code",
  "category": "book",
  "price": 450,
  "inStock": true,
  "specs": {
    "author": "Robert C. Martin",
    "pages": 464,
    "isbn": "978-0132350884",
    "publisher": "Prentice Hall"
  },
  "tags": ["programming", "best-practices"]
}
```

**Fördelar:**
- En query: `db.products.findOne({ _id: "prod_12345" })`
- Lätt att lägga till nya fält (ingen migration)
- Snabba läsningar (ingen JOIN)

**Nackdelar:**
- Ingen schema-validering (du kan spara fel data)
- Svårare att garantera konsistens
- Duplicerad data om produkter refereras på flera ställen

## Tradeoffs

**När SQL är bättre:**
- Strikta affärsregler (pris får inte vara negativt)
- Många relationer mellan entiteter
- Transaktioner är kritiska
- Data ändras ofta och måste vara konsistent

**När MongoDB är bättre:**
- Datastrukturen varierar mycket
- Läshastighet är viktigare än skrivhastighet
- Flexibilitet viktigare än strikta regler
- Horisontell skalning krävs

## Hybrid-Arkitektur

```
┌─────────────────┐         ┌──────────────────┐
│   SQL Database  │         │ MongoDB Database │
├─────────────────┤         ├──────────────────┤
│ Users           │         │ Products         │
│ Orders          │◄───────►│ Reviews          │
│ Payments        │         │ Sessions         │
└─────────────────┘         └──────────────────┘
```

API-lager hanterar kommunikation mellan dem.

Order innehåller referens till produkt-ID, men produktdetaljer hämtas från MongoDB.

</details>

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
