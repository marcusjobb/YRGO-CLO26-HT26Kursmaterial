# 03 Normalisering Gdpr — Programmeringstermer

## Normalisering
Process att strukturera data för att minimera redundans och beroenden.

## 1NF
Första normalformen: atomära värden, ingen upprepning av kolumner. Varje cell innehåller ett värde.

## 2NF
Andra normalformen: uppfyller 1NF + alla icke-nyckel-attribut beror på hela primärnyckeln.

## 3NF
Tredje normalformen: uppfyller 2NF + inga transitiva beroenden (A→B→C).

## Redundans
Onödig duplicering av data. Ökar lagringsbehov och risk för inkonsekvens.

## Transitivt Beroende
När ett icke-nyckel-attribut beror på ett annat icke-nyckel-attribut. Måste brytas upp i 3NF.

## Funktionellt Beroende
När ett attribut unikt bestämmer värdet på ett annat: CustomerID → Name.

## Denormalisering
Medvetet bryta normalisering för prestanda. Färre JOIN på bekostnad av redundans.

## GDPR
Dataskyddsförordning. Krav på hantering av personuppgifter: samtycke, radering, portabilitet.

## Pseudonymisering
Ersätt identifierande data med pseudonymer. Möjliggör analys utan att exponera personer.

