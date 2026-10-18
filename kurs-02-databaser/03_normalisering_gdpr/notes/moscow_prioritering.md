# MoSCoW-prioritering

Du har en lista med saker att bygga och inte tillräckligt med tid. Någon måste bestämma vad som faktiskt måste finnas och vad som kan vänta. MoSCoW är en metod för att göra det beslutet synligt och delat i hela teamet.

---

## De fyra kategorierna

| Bokstav | Kategori | Vad det innebär |
|---------|----------|-----------------|
| **M** | Must have | Utan det här fungerar inte systemet. Leveransen misslyckas om det saknas. |
| **S** | Should have | Viktigt, men inte livsavgörande. Kan utelämnas om tid saknas utan att allt rasar. |
| **C** | Could have | Trevligt att ha. Tas med om det finns tid och kapacitet. |
| **W** | Won't have (this time) | Medvetet ute ur den här leveransen. Inte ett nej för alltid — bara ett nej nu. |

De två vokalerna i mitten (oo och o) är "stummade" för att akronymen ska uttalas: **MoSCoW**.

---

## Varför det fungerar

Utan en prioriteringsmetod händer det här: alla krav känns lika viktiga, ingenting stryks, och teamet springer runt och halvfärdigställer allt i stället för att leverera något klart.

MoSCoW tvingar fram ett beslut. Det är inte ett tekniskt verktyg — det är ett kommunikationsverktyg. Product owner, studerande och kund måste komma överens om vad som är vad, och det samtalet är mer värdefullt än listan.

> **Kom ihåg:** Skippar du MoSCoW hamnar du i *Coulda Woulda Shoulda* — ett liv av ångest och halvfärdiga databaser.

---

## Exempel: djursjukhusets databas

Gruppen som designar djursjukhusets databas har den här listan med önskade funktioner:

| Funktion | Kategori | Motivering |
|----------|----------|------------|
| Registrera djur med ägare | **Must** | Kärnan i systemet |
| Söka på djurets namn | **Must** | Veterinären måste hitta patienten snabbt |
| Journalpost per besök | **Should** | Viktig men systemet fungerar utan dag ett |
| Vaccinationshistorik | **Could** | Bra att ha, inte kritiskt i MVP |
| Automatiska påminnelsemail | **Won't** | För komplex — sparas till version 2 |
| Mörkt läge i gränssnittet | **Won't** | Inget med databasdesign att göra |

Resultatet: designgruppen kan fokusera på relationen `Owners → Animals → Visits` och skippa allt annat tills grunden fungerar.

---

## Koppling till databasdesign

MoSCoW avgör vilka **tabeller** och **relationer** som behöver finnas i version 1.

- **Must** → måste modelleras och normaliseras nu
- **Should** → planera för det i ER-diagrammet men bygg det i nästa sprint
- **Could / Won't** → nämns i reflection-dokumentet men påverkar inte nuvarande schema

Det är bättre att ha en välnormaliserad liten databas än ett halvfärdigt stort schema.

---

## Att tänka på

- Allt kan inte vara Must. Om allt är Must har ni inte prioriterat — ni har kopierat listan.
- Won't have är inte "nej för alltid". Det är ett ärat, dokumenterat icke-beslut.
- Prioriteringen ska göras i teamet, inte av en person ensam.
