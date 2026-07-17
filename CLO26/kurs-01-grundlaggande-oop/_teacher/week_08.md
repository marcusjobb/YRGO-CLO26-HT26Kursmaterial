# Vecka 8 — Skogsäventyret, del 2 (avslutning)

## Datum

- **Söndag 27 sep 2026, 23:59** — inlämningsdeadline
- **Onsdag 30 sep 2026** — förmiddag tenta, eftermiddag redovisning
- **Fredag 2 okt 2026, 23:59** — förlängd deadline (för de som inte hann)

## Dagplan

### Tisdag — Sista handledningsdagen

- Sista möjligheten till handledning innan inlämning
- Påminn om: commit-hash sparas efter inlämning, inga ändringar accepteras efter rättning påbörjats
- REFLEKTION.md måste finnas i repot — ett krav, inte valfritt
- Grupper utan VG-features: fokusera på att G-kraven är kompletta och fungerande

### Onsdag — Tenta + redovisning

**Förmiddag: Tenta**
Se `exam/` för tentainnehåll.

**Eftermiddag: Redovisningar**
- Varje grupp kör sin kod live — Marcus ställer frågor
- Frågor riktas till alla i gruppen, inte bara den som kodade mest
- Bedömningsfokus: förstår de vad de byggt? Kan de motivera designvalen?
- Anteckna commit-hash + datum direkt när gruppen presenterar

### Fredag — Rättning

- Hämta repos via commit-hash (från presentationsdagen)
- Rättningslogg: `_teacher/grading/week_08.md`
- Feedback skrivs av Claude → Marcus granskar → skickas per mail

---

## Rättningsguide

### G-checklista

- [ ] Game loop som körs tills spelaren dör
- [ ] `Player` med TakeDamage, Heal, GainXP, LevelUp
- [ ] `Monster`-basklass med minst 3 subklasser med egna stats
- [ ] Strid: försvara (halv skada), anfall, spring (slump)
- [ ] Poängtavla vid game over
- [ ] `REFLEKTION.md` besvarar alla G-frågor

### VG-checklista

- [ ] `Weapon`-basklass med minst 2 subklasser
- [ ] Monster tappar guld, shop säljer vapen
- [ ] Arenan: 7 monster, sorterade, kan inte avbrytas
- [ ] `REFLEKTION.md` besvarar VG-frågorna (datastrukturval motiverat)
