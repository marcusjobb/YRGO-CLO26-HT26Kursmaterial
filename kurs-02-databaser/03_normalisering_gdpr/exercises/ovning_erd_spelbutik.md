# Övning — ER-diagram: Spelbutiken

🟡

Samma uppgift som djurparken, men nu är scenariot en onlinespelbutik. Strukturen är densamma — entiteter, attribut, relationer — men du behöver också identifiera vilka relationer som kräver mellantabeller.

## Entiteter och attribut

Rita fem entiteter med följande attribut:

**Spel**
- `id` (PRIMARY KEY)
- `titel`
- `pris`
- `utgivningsdatum`

**Utvecklare**
- `id` (PRIMARY KEY)
- `namn`
- `land`

**Genre**
- `id` (PRIMARY KEY)
- `namn`

**Kund**
- `id` (PRIMARY KEY)
- `namn`
- `epost`

**Order**
- `id` (PRIMARY KEY)
- `orderdatum`
- `kund_id` (FOREIGN KEY)

## Relationer

Rita dessa relationer och sätt ut kardinaliteten:

- Ett **Spel** har en **Utvecklare** — *N:1* (ett spel har en utvecklare, en utvecklare kan ha gjort många spel)
- Ett **Spel** tillhör en eller flera **Genrer** — *N:M* (ett spel kan ha flera genrer, en genre kan gälla många spel)
- En **Kund** kan ha många **Orders** — *1:N* (en kund, många orders)
- En **Order** kan innehålla många **Spel** — *N:M* (en order, många spel — ett spel kan finnas i många orders)

## Uppgift

1. Rita ER-diagrammet med alla entiteter, attribut och relationer.
2. Identifiera vilka relationer som kräver en mellantabell och **förklara varför** med egna ord.

> **Tips:** En N:M-relation kan inte representeras direkt med en utländsk nyckel — du behöver en mellantabell med två utländska nycklar. Djurpark-exemplet hade inga N:M-relationer, men den här butiken har två stycken.

## Extra utmaning 🔴

Skriv SQL-koden (`CREATE TABLE`) för de två mellantabellerna du identifierade. Tänk på:
- Mellantabellen ska ha en sammansatt primärnyckel (PRIMARY KEY med två kolumner)
- Båda kolumnerna ska vara FOREIGN KEY mot respektive tabell

## Förväntad struktur

Ditt diagram ska ha:
- 5 entiteter: `Spel`, `Utvecklare`, `Genre`, `Kund`, `Order`
- 4 relationer med tydlig kardinalitet
- 2 mellantabeller för N:M-relationerna: `SpelGenre` och `OrderSpel`
- Primär- och utländska nycklar markerade överallt
