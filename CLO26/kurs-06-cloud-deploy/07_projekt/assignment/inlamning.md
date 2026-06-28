---

title: 📦 Inlämningsinstruktioner - TDD Grupprojekt
author: Marcus Ackre Medina
type: assignment
topic: projekt
difficulty: 1
language: bash
status: adapted
marcus_voice: true
source: "Old_courses/2025/4_test_and_quality_assurance/assignment/INLAMNING.md"
description: "DEADLINE:** Söndag 19 januari 2026, kl. 23:59"
tags: ["bash", "git", "grupprojekt", "inlamning", "inlämningsinstruktioner", "installation", "projekt", "visual-studio"]
week_fit: []
---

# 📦 Inlämningsinstruktioner - TDD Grupprojekt

🟢


**DEADLINE:** Söndag 19 januari 2026, kl. 23:59

> **🎉 Ni har byggt något GRYMMT!** Nu är det dags att visa upp det! Följ instruktionerna nedan så att allt går smidigt. **Ni kommer klara detta perfekt!** 💪

**VIKTIGT:** Läs denna guide NOGA för att undvika försenad inlämning! ⚠️ *(Ingen panik, vi går igenom allt steg för steg!)*

---

## 🎯 Vad Ska Lämnas In?

### 1️⃣ GitHub Repository (EN person lämnar in)

**VEM:** En person i gruppen (utse en "Inlämningsansvarig")

**VAD:**
1. Zippad fil med hela projektet (Download ZIP från GitHub)
2. Länk till ert GitHub repo

**VAR:** Google Classroom → "TDD Grupprojekt - Repository"

**INNEHÅLL I REPOT:**

```
my-project-api/
├── .github/
│   └── workflows/
│       └── dotnet.yml              ✅ CI/CD måste vara grön!
├── src/
│   ├── MyProject.Api/              ✅ Alla controllers
│   ├── MyProject.Core/             ✅ Entities, Services
│   └── MyProject.Tests/            ✅ xUnit Tests (80%+ coverage)
├── docs/
│   ├── user-stories/               ✅ Minst 10 User Stories
│   ├── sprint-planning/            ✅ Sprint 1 & 2 planning
│   ├── daily-standups/             ✅ Minst 5 standups
│   └── retrospectives/             ✅ Sprint retros
├── .gitignore
├── README.md                       ✅ Komplett med build badge
└── MyProject.sln
```

**CHECKLISTA INNAN INLÄMNING:**

- [ ] CI/CD är GRÖN (alla tester passerar)
- [ ] README har build badge OCH alla teammedlemmars namn
- [ ] Swagger fungerar (om API-projekt)
- [ ] Repot är PUBLIKT eller läsrättigheter för Marcus (GitHub: `marcusmedinaswe`)
- [ ] Zippad fil nedladdad från GitHub (Code → Download ZIP)

---

### 2️⃣ Individuell Reflektion + Repo-länk (ALLA lämnar in)

**VEM:** ALLA teammedlemmar individuellt

**VAD:**
1. Din personliga reflection-mall.md (ifylld)
2. Länk till GitHub repo

**VAR:** Google Classroom → "TDD Grupprojekt - Individuell Reflektion"

**FORMAT:** Markdown (.md fil) eller PDF

**VIKTIGT:**

- ✅ Reflektionen lämnas ENDAST in på Classroom (INTE i repot!)
- ✅ Varför inte i repot? Så du kan vara ärlig om problem utan att teamet läser!
- ✅ Besvara alla G-frågor (minst 80 ord per svar)
- ✅ Om du siktar på VG: besvara alla VG-frågor (minst 120 ord per svar)
- ✅ Inkludera kodexempel
- ✅ Inkludera länk till GitHub repo i din inlämning
- ❌ Kopiera INTE dina teammedlemmars svar
- ❌ Lämna INTE in som grupp (detta är individuellt!)
- ❌ Lägg INTE reflektionen i repot (Marcus samlar dem senare för bedömning)

**Mall:** [reflection-mall.md](reflection-mall.md)

---

## 👥 Rollfördelning vid Inlämning

### Inlämningsansvarig (1 person)

**Ansvar:**

1. Kontrollera att ALL dokumentation finns i repot
2. Kontrollera att CI/CD är grön
3. Kontrollera att README har alla teammedlemmars namn
4. Ladda ner ZIP från GitHub (Code → Download ZIP)
5. Lämna in ZIP + GitHub repo-länk på Google Classroom

**Tips:** Välj denna person på Sprint Planning 1!

### Alla Teammedlemmar

**Ansvar:**

1. Skriva din egen reflection-mall.md
2. Lämna in DIN reflektion + repo-länk INDIVIDUELLT på Google Classroom
3. **VIKTIGT:** Lägg INTE reflektionen i repot (endast på Classroom!)

---

## 🚨 Vanliga Misstag (UNDVIK DESSA!)

**Dessa misstag händer VARJE år! 😅 Men inte för ER, för ni läser ju detta! Smart! 🧠**

### ❌ Misstag #1: "Jag glömde lämna in min reflektion"

**Resultat:** Komplettering krävs, försenad bedömning

**Lösning:** Lägg in påminnelse 18/1 att lämna in!

---

### ❌ Misstag #2: "Repot är privat och Marcus har ingen access"

**Resultat:** Kan inte rättas, komplettering

**Lösning:**

```bash
# Gör repot publikt ELLER lägg till Marcus
Settings → Collaborators → Add people → marcusmedinaswe
```

---

### ❌ Misstag #3: "CI/CD är röd vid inlämning"

**Resultat:** Automatiskt komplettering

**Lösning:** Fixa tester INNAN deadline!

```bash
dotnet test                    # Kör lokalt först
git push                       # Trigga CI/CD
# Vänta tills GitHub Actions är grön ✅
```

---

### ❌ Misstag #4: "Endast EN person lämnade in reflektion för hela gruppen"

**Resultat:** Alla utom den personen får IG

**Lösning:** ALLA lämnar in reflektion INDIVIDUELLT!

---

### ❌ Misstag #5: "Vi glömde zippa projektet"

**Resultat:** Komplettering krävs

**Lösning:**

```
GitHub → Code (grön knapp) → Download ZIP
Ladda upp ZIP-filen på Google Classroom
```

---

## ✅ Inlämningsprocess Steg-för-Steg

### Dag 1-2 Innan Deadline (17-18 januari)

**1. Kod-Freeze (Sista buggfixar)**

```bash
# Mergea sista features till develop
git checkout develop
git merge feature/last-feature

# Mergea develop till main
git checkout main
git merge develop
git push
```

**2. Verifiera CI/CD**

- Gå till GitHub Actions
- Kontrollera att senaste push är GRÖN ✅
- Om RÖD: fixa omedelbart!

**3. README Final Check**

- [ ] Projektbeskrivning finns
- [ ] Setup-instruktioner finns
- [ ] Build badge visar grön status
- [ ] ALLA teammedlemmars namn listade (obligatoriskt!)
- [ ] API endpoints dokumenterade (om API)

**4. Dokumentation Final Check**

```
docs/
├── user-stories/          ✅ Minst 10 stories
├── sprint-planning/       ✅ Båda sprintarna
├── daily-standups/        ✅ Minst 5 standups allt som allt
└── retrospectives/        ✅ Minst 1 retro
```

**5. Skriv Individuell Reflektion**

- Använd [reflection-mall.md](reflection-mall.md)
- Besvara alla frågor ärligt och utförligt
- Inkludera kodexempel
- Spara som `ditt-namn-reflection.md`
- **OBS:** Lägg INTE i repot! Lämnas endast in på Classroom!

---

### Deadline-Dagen (19 januari)

**Före kl. 20:00:**

**Inlämningsansvarig:**

1. Ladda ner projektet som ZIP från GitHub:
   - GitHub → Code (grön knapp) → Download ZIP
2. Gå till Google Classroom → "TDD Grupprojekt - Repository"
3. Ladda upp ZIP-filen
4. Klistra in GitHub repo URL i kommentarsfältet: `https://github.com/username/project-name`
5. Lägg till teammedlemmar i kommentarsfältet:

   ```
   Teammedlemmar:
   - Alice Andersson
   - Bob Bengtsson
   - Charlie Carlsson

   Inlämningsansvarig: Alice Andersson
   ```

6. Submit

**ALLA teammedlemmar (individuellt):**

1. Gå till Google Classroom → "TDD Grupprojekt - Individuell Reflektion"
2. Ladda upp din `namn-reflection.md` eller PDF
3. I textfältet/kommentarsfältet: Klistra in länk till GitHub repo
4. Submit

**Före kl. 23:59:**

- Dubbelkolla att DU har lämnat in din egen reflektion!
- Skicka ett meddelande i Discord #project när ni är klara 🎉

---

## 📊 Vad Händer Efter Inlämning?

### Vecka 3 (19-21 januari) - Demo Days

**Måndag 19/1:**

- Teams presenterar sina projekt (10 min demo + 5 min Q&A)

**Onsdag 21/1:**

- Fortsatta presentationer
- Awards Ceremony 🏆
  - Best Tests
  - Best API Design
  - Best SCRUM Process
  - Best Code Quality

### Rättning

**Timeline:**

- Preliminär feedback: Inom 1 vecka
- Final bedömning: Inom 2 veckor

**Bedömning baseras på:**

- Teknisk implementation (API, Databas, Tester)
- TDD-process (commits visar test-först)
- SCRUM-dokumentation
- Git Flow
- Kodkvalitet
- Individuell reflektion

**Bedömningsmatris:** [teacher_matrix.md](teacher_matrix.md)

---

## 🆘 Hjälp, Något Gick Fel!

### Problem: "Jag missade deadline med 5 minuter!"

**Lösning:** Kontakta Marcus OMEDELBART via Discord/email med förklaring.

### Problem: "Min reflektion syns inte på Google Classroom efter submit"

**Lösning:** Kolla Submissions-sidan. Om filen saknas, lämna in igen och kontakta Marcus.

### Problem: "CI/CD blev röd 10 minuter innan deadline!"

**Lösning:**

1. Lämna in ändå
2. Kommentera i Google Classroom inlämningen: "CI/CD failing due to [orsak], fixing in komplettering"
3. Fixa och pusha fix direkt efter deadline
4. Kontakta Marcus

### Problem: "Jag är sjuk på deadline-dagen"

**Lösning:** Kontakta Marcus INNAN deadline med läkarintyg/sjukanmälan.

---

## 📞 Kontakt

**Frågor om inlämning:**

- Discord eller Email: marcus.medina-ramirez@molndal.se

**Tekniska problem (GitHub, CI/CD):**

- Discord: #help
- Fråga teamet först!

---

## ✅ Final Checklist (Skriv ut och bocka av!)

### Hela Gruppen

- [ ] GitHub repo är publikt ELLER Marcus har access
- [ ] CI/CD är grön ✅
- [ ] README är komplett med build badge OCH alla teammedlemmars namn
- [ ] Alla 4 entiteter implementerade
- [ ] 80%+ test coverage
- [ ] Swagger fungerar (API-projekt)
- [ ] 10+ User Stories i `docs/user-stories/`
- [ ] Sprint plannings i `docs/sprint-planning/`
- [ ] Minst 5 standups i `docs/daily-standups/`
- [ ] Sprint retrospectives i `docs/retrospectives/`
- [ ] Inlämningsansvarig vald
- [ ] ZIP-fil nedladdad från GitHub
- [ ] ZIP + repo-länk inlämnad på Google Classroom

### Individuellt (ALLA)

- [ ] Jag skrev min egen reflektion
- [ ] Jag besvarade alla G-frågor (minst 80 ord)
- [ ] Jag besvarade alla VG-frågor om jag siktar VG (minst 120 ord)
- [ ] Jag inkluderade kodexempel
- [ ] Jag inkluderade länk till GitHub repo i min inlämning
- [ ] Jag lämnade in min reflektion INDIVIDUELLT på Google Classroom
- [ ] Jag verifierade att inlämningen gick igenom
- [ ] Jag lade INTE reflektionen i repot (endast Classroom!)

---

**Lycka till! 🚀**

> **💪 Ni har byggt något OTROLIGT coolt! Ni ska vara så stolta över er själva!** Efter detta projekt kan ni säga: "Jag har byggt ett professionellt API med TDD, SCRUM och CI/CD" - det är portfolio-material som imponerar! Ni är grymma! 🌟

_"The only way to miss the deadline is to not submit at all"_

**Vi ses på Demo Day - där ni får flexa vad ni byggt! 🎤✨**

---

_© Campus Mölndal 2026 - Test och Kvalitetssäkring CLO25_
