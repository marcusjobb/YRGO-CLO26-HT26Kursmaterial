# Övning — Normalisering: Orderdata

🟢

Du får en kaotisk tabell med orderdata. Din uppgift är att normalisera den steg för steg — precis som vi gick igenom på lektionen.

## Utgångspunkt — den platta tabellen

```
order_id | kund_namn | kund_epost        | produkt_1   | produkt_2  | produkt_3 | butik_namn  | butik_stad
---------|-----------|-------------------|-------------|------------|-----------|-------------|----------
1001     | Kalle B   | kalle@mail.se     | Tangentbord | Mus        |           | Teknikdosen | Göteborg
1002     | Sara L    | sara@mail.se      | Skärm       |            |           | Teknikdosen | Göteborg
1003     | Kalle B   | kalle@mail.se     | Headset     | Webbkamera | USB-hubb  | Datagruvan  | Stockholm
```

---

## Steg 1 — Identifiera problemen

Titta på tabellen ovan. Lista **minst fyra problem** du ser med den här strukturen.

> **Tips:** Tänk på vad som händer om Kalle byter e-postadress. Eller om vi vill lägga till en fjärde produkt på en order.

---

## Steg 2 — 1NF: ett värde per cell

Första normalformen kräver att varje cell innehåller exakt ett värde — inga listor, inga tomma kolumner för "nästa produkt".

Hur strukturerar du om tabellen för att uppfylla 1NF? Visa hur den nya tabellen ser ut.

> **Tips:** Du kan behöva fler rader men färre kolumner.

---

## Steg 3 — 2NF: bryt ut upprepningarna

Andra normalformen kräver att alla kolumner beror på hela primärnyckeln — inte bara en del av den.

Kund-informationen upprepas för varje order Kalle gör. Butiksinformationen upprepas för varje order från samma butik.

Bryt ut `Kund` och `Butik` till egna tabeller. Visa hur de tre tabellerna ser ut med exempeldata.

---

## Steg 4 — 3NF: kontrollera transitiva beroenden

Tredje normalformen kräver att inga icke-nyckelfält beror på ett annat icke-nyckelfält.

Finns det något kvar i tabellerna från steg 3 som bryter mot 3NF? Förklara vad du hittar (eller motivera varför du inte hittar något).

> **Tips:** Tänk på om `butik_stad` beror på `order_id` eller på `butik_id`.

---

## Steg 5 — Skriv SQL

Skriv `CREATE TABLE` för slutresultatet. Du ska ha minst tre tabeller: `Kund`, `Butik` och `Order`. Om du behöver en mellantabell för produkter — lägg till den också.

```sql
-- Skriv din SQL här
```
