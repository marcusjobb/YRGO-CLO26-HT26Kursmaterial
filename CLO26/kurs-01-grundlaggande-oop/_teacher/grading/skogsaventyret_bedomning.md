# Lärarguide — Bedömning Skogsäventyret

*Inte för studerande. Inte för publicering.*

---

## G-checklista

Gå igenom repot och kryssa av. Om en punkt saknas: IG på den punkten.  
Alla G-punkter måste vara uppfyllda för godkänt.

**Karaktär**
- [ ] `Character`-klass finns med: HP, maxHP, attack, level, XP, poäng
- [ ] Properties används (get/set eller auto-properties) — inte bara publika fält
- [ ] HP återställs till det *nya* maxHP vid level-up (inte gamla maxHP)

**Monster-arv**
- [ ] `Monster`-basklass med minst: namn, HP, attack, XP-belöning
- [ ] Minst 2 subklasser som ärver från Monster
- [ ] Subklasserna är *faktiskt olika* — inte bara kopierade med andra namn

**Level-system**
- [ ] XP ökar vid monsterdöd
- [ ] Level-up triggas vid XP-tröskel
- [ ] maxHP ökar vid level-up
- [ ] attack ökar vid level-up
- [ ] HP återställs till maxHP vid level-up

**Spelloop**
- [ ] Spelet körs tills HP når 0
- [ ] Monster väljs slumpmässigt (Random eller liknande)
- [ ] Output är läsbar — man förstår vad som händer

**Reflektion**
- [ ] REFLEKTION.md finns i repot
- [ ] Minst 3 meningar per fråga — inte bara "det var bra"
- [ ] Arv-frågan är besvarad med ett konkret exempel från sin kod

---

## VG-checklista

Alla G-krav uppfyllda. Sedan:

**Vapen-arv**
- [ ] `Weapon`-basklass med: namn, attack-bonus, pris
- [ ] Minst 2 subklasser som ärver från Weapon
- [ ] Subklasserna är genuint olika (inte bara olika namn och siffror — ex. Bow har annat beteende än Sword, eller det motiveras i reflektionen)

**Shop**
- [ ] `Shop`-klass finns
- [ ] Köp minskar poäng
- [ ] Aktivt vapen påverkar attack i striden
- [ ] Shopen visas vid rätt tillfälle (inte mitt i strid)

**VG-reflektion**
- [ ] Vapnarvet motiverat konkret — varför arv, inte bara en lista med vapen?
- [ ] Skillnaden Weapon vs Sword förklarad med egna ord

---

## Vanliga fallgropar att leta efter

**Arv i namn, inte i kod**  
Goblin : Monster men Goblin ändrar ingenting, lägger till ingenting — det är ingen riktig subklass i praktiken. Fråga dem i feedback: vad gör Goblin annorlunda?

**HP-buggen**  
HP återställs till *gamla* maxHP istället för *nya*. Vanligt fel. Är det ett logikfel eller ett missförstånd av systemet?

**Vapnet påverkar inte striden**  
Shop finns, vapen köps, men attack-bonus används aldrig i uträkningen. Kolla stridskoden.

**Reflektionen är tom**  
"Det var lärorikt" är inte ett svar. Kräv ett konkret exempel från deras kod.

---

## Betygsmotivering — mall

```
BETYG: G / VG / IG

STYRKOR:
- [konkret från deras kod]
- [konkret från deras reflektion]

ATT FÖRBÄTTRA:
- [konkret, med förslag på nästa steg]

MOTIVERING:
[2-3 meningar om varför detta betyg, med hänvisning till kriterierna]
```

---

## Om IG

IG = en eller flera G-punkter uppfylls inte.  
Skriv exakt vilken/vilka punkter som saknas.  
Omtentamen: samma projekt med 1 veckas tillägg.

---

## Notering om AI-genererad kod

Om koden ser AI-genererad ut men gruppen kan förklara varje del → godkänt.  
Om gruppen inte kan förklara sin kod → IG oavsett kodkvalitet.  
Använd feedbacksamtalet för att testa förståelsen. "Vad gör den här metoden?" räcker.
