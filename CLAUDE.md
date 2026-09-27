# CLAUDE.md — YRGO CLO26 Studerande-repo

## ⛔ DETTA ÄR ETT STUDERANDE-REPO

Claude får **aldrig** commita eller pusha direkt till det här repot.

Det spelar ingen roll vad uppgiften är. Det spelar ingen roll om det verkar snabbt och enkelt.
**Direkta commits hit är alltid fel.**

## Det enda godkända sättet att publicera

Kör från lärarrepots `CLO26/`-mapp:

```bash
bash _scripts/publish_week.sh <kursmapp> <modulmapp>
```

Exempel:
```bash
bash _scripts/publish_week.sh kurs-01-grundlaggande-oop 02_syntax_och_variabler
```

## Varför

Lärarmaterial (tentafrågor, facit, bedömningsunderlag, GDPR-känslig data) har tre gånger
råkat publiceras till studerande via direkta commits. Det orsakar allvarliga konsekvenser
för den akademiska integriteten.

## Om du öppnat det här repot av misstag

Stäng sessionen. Öppna lärarrepot istället:
`/home/nionit/git/Kursmaterial/YRGO/yrgo-kursmaterial/`
