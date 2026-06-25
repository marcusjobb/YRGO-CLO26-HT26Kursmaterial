# Klassövning — Vi bygger ett repo tillsammans · Lärarversion

*PUBLICERAS INTE — Fil för studerande: `klassovning_01_klass_repo.md`*

---

## Förbered INNAN lektionen

### 1. Skapa repot på GitHub

Skapa ett nytt publikt repo under Nion-Education:
- Namn: `klass-clo26`
- Synlighet: **Public** (studerande behöver inte logga in för att klona)
- Initiera med en README

### 2. Skapa `classmates.md`

Lägg till filen med en startrad:

```markdown
# CLO26 — Klassen

| Namn | Var du kommer ifrån | En sak ingen vet om dig |
|------|---------------------|--------------------------|
| Marcus Medina | Göteborg | Råkade en gång committa trasig kod till Azure på jobbet |
```

Din egna rad är ett bra isbrytarexempel — och det visar att *alla* gör misstag (se KURSGUIDE.md).

### 3. Bjud in studerande som collaborators

GitHub → Settings → Collaborators → Add people

Eller: använd en GitHub Organization (Nion-Education) och ge klassen write-access till repot.

### 4. Skriv upp repoadressen på tavlan

```
https://github.com/Nion-Education/klass-clo26.git
```

---

## Under övningen

**Låt det hända.** Den som pushar second kommer att få `rejected` — det är poängen. Stå inte och förebygg det, låt de se felmeddelandet och hjälp dem sen.

**Vanliga scenarion:**
- Auto-merge: vanligast när folk ändrar olika rader → Git sköter det, de ser en merge commit
- Konflikt: när två ändrar samma rad → de behöver lösa det manuellt (se övningsfilen)
- Någon pushar utan att ha pulllat → rejected, förklara varför

**Avslutning:** Kör `git log --oneline` projektat på skärmen så hela klassen ser alla commits. Det är aha-ögonblicket.

---

## Timing

Passar bäst mot slutet av dag 2, efter att övning 1 och git grunder är klara.
Ca 25–35 minuter beroende på hur många konflikter som uppstår.

Konflikter = mer tid = mer lärande. Skynda inte igenom dem.
