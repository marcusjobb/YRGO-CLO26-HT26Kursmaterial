# ERD — Programmeringstermer

## Entity Relationship Diagram (ERD)
Diagramtyp som visar entiteter och relationer mellan dem. Används främst inom databasdesign för att modellera datastrukturer.

## Entitet
Något vi vill lagra data om — en "tabell" i databasen. Exempel: Customer, Product, Order.

## Attribut
En egenskap hos en entitet — en "kolumn" i tabellen. Exempel: Name, Price, Email.

## Primary Key (PK)
Unik identifierare för varje rad i en tabell. Kan vara ett attribut eller en kombination av flera.

## Foreign Key (FK)
Attribut i en tabell som pekar på Primary Key i en annan tabell. Skapar relationen mellan tabellerna.

## Kardinalitet
Anger antalet instanser i en relation:
- **1:1 (En-till-en)** — En post i Tabell A matchar exakt en post i Tabell B
- **1:N (En-till-många)** — En post i Tabell A matchar många i Tabell B (vanligast)
- **N:M (Många-till-många)** — Många poster i Tabell A matchar många i Tabell B (kräver kopplingstabell)

## Kopplingstabell (Junction Table)
En extra tabell som används för att lösa N:M-relationer. Innehåller FK:ar till båda originaltabellerna.

## Chen Notation
ERD-notation som använder romber för relationer, rektanglar för entiteter och ovaler för attribut.

## Crow's Foot Notation
ERD-notation som använder "kråkfötter" för att visa multiplicitet. Vanligast i moderna verktyg.

## Svag entitet (Weak Entity)
Entitet som inte existerar utan en annan entitet. Exempel: OrderItem kan inte finnas utan Order.

## Normalisering
Process för att strukturera data för att minimera redundans och beroenden. 1NF → 2NF → 3NF.

## Reverse Engineering
Process att generera ett ERD från en befintlig databas. MySQL Workbench stödjer detta.
