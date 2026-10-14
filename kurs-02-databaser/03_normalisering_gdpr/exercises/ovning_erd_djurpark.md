# Övning — ER-diagram: Djurparken

🟢

Det här är samma scenario som vi ritade på tavlan under lektionen. Nu gör du det själv — antingen på papper eller i [dbdiagram.io](https://dbdiagram.io).

## Entiteter och attribut

Rita fyra entiteter med följande attribut:

**Djur**
- `id` (PRIMARY KEY)
- `namn`
- `fodelsedag`

**Art**
- `id` (PRIMARY KEY)
- `artnamn`
- `latinskt_namn`

**Skötare**
- `id` (PRIMARY KEY)
- `namn`
- `anstallningsdatum`

**Hägn**
- `id` (PRIMARY KEY)
- `namn`
- `storlek_m2`

## Relationer

Rita dessa relationer och sätt ut kardinaliteten:

- Ett **Djur** tillhör en **Art** — *N:1* (många djur kan tillhöra samma art)
- Ett **Djur** bor i ett **Hägn** — *N:1* (många djur kan bo i samma hägn)
- En **Skötare** ansvarar för flera **Hägn** — *1:N* (en skötare, många hägn)

## Uppgift

Rita ER-diagrammet med alla entiteter, attribut och relationer.

> **Tips:** Kom ihåg att markera primärnycklar (understruket eller med nyckel-ikon) och sätt alltid ut kardinaliteten vid varje relation. I dbdiagram.io skriver du `ref: > tabell.kolumn` för att rita en relation.

## Förväntad struktur

Ditt diagram ska ha:
- 4 entiteter: `Djur`, `Art`, `Skötare`, `Hägn`
- 3 relationer med tydlig kardinalitet
- Primärnyckel markerad i varje entitet
- Utländska nycklar där de behövs (`art_id` i Djur, `hagn_id` i Djur, `skotare_id` i Hägn eller mellantabell)
