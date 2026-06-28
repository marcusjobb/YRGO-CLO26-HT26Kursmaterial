---

title: 👨‍🏫 Lärarinstruktioner - Sport League Manager API
author: Marcus Ackre Medina
type: assignment
topic: projekt
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/4_test_and_quality_assurance/assignment/teacher_instructions.md"
description: "Kursdel:** Test och Kvalitetssäkring"
tags: ["csharp", "git", "installation", "instructions", "league", "lärarinstruktioner", "manager", "projekt", "sport", "teacher"]
week_fit: []
---

# 👨‍🏫 Lärarinstruktioner - Sport League Manager API

🔴


**Kursdel:** Test och Kvalitetssäkring
**Projektlängd:** 2 veckor
**Format:** Grupprojekt (2-3 personer)

---

## 🎯 Projektöversikt

Detta är ett TDD-fokuserat grupprojekt där studenterna bygger ett REST API för sportligahantering med fokus på:

- Test-Driven Development (TDD)
- SCRUM metodologi
- Git Flow
- API development
- Teamwork

---

## 📅 Tidslinje

### Vecka 1 (6-10 januari)

**Måndag 5/1 (Online 13-16) - Sprint Kickoff**

- Marcus: Genomgång av projektet (30 min)
- Teams: Sprint Planning 1 (2h 30min)
  - Välj domän (sport/budget/lager/etc.)
  - Skapa GitHub repo
  - Skriv User Stories (minst 15)
  - Planning Poker
  - Definiera Sprint Goal
  - Fördela första uppgifterna

**Onsdag 7/1 (Campus) - Development Sprint**

- Daily Standup (10 min)
- TDD implementation (hela dagen)
- Marcus: Runda runt och hjälp teams
- Code reviews mellan teammedlemmar

**Torsdag 8/1 (Campus) - Mid-Sprint**

- Daily Standup (10 min)
- Fortsatt development
- Marcus: Mid-sprint check-in med varje team (30 min per team)
  - Kolla kod kvalitet
  - Kolla test coverage
  - Ge feedback på process

---

### Vecka 2 (13-17 januari)

**Måndag 12/1 (Online 13-16) - Sprint Review & Planning**

- Sprint 1 Review (varje team 10 min)
- Sprint 1 Retrospective (teamvis)
- Sprint Planning 2
- Marcus: Sammanfattning och feedback

**Onsdag 14/1 (Campus) - Polish Sprint**

- Daily Standup
- Bugfixes och förbättringar
- UAT testing (teams testar varandras APIs)
- Dokumentation
- Marcus: Slutlig code review runda

**Torsdag 15/1 (Campus) - Finalization**

- Final standup
- Code review showcase (teams visar bästa testerna)
- Marcus: Genomgång av inlämningskrav
- Förbered presentationer

**Fredag 16/1**

- TENTA (projekt fortsätter parallellt)

**Söndag 18/1 23:59**

- DEADLINE: Projekt inlämning

---

### Vecka 3 (20-24 januari) - Demo Week

**Måndag 19/1 (Online 13-16)**

- Demo Day - 4 första teams
- Format: 15 min per team
  - 10 min presentation
  - 5 min frågor & feedback

**Onsdag 21/1 (Campus)**

- Demo Day - resterande teams
- Peer feedback
- Marcus: Awards ceremony
  - Best Tests
  - Best API Design
  - Best Teamwork
  - Best Documentation

---

## 🎓 Pedagogisk Approach

### Fokus Areas

**1. TDD (Highest Priority)**

- Studenterna ska VERKLIGEN skriva tester först
- Poängtera Red-Green-Refactor
- Visa exempel på bra test naming
- Diskutera vad som ska testas

**2. SCRUM Process**

- Påminn om Daily Standups
- Be teams uppdatera Kanban board
- Kolla att User Stories har Acceptance Criteria
- Retrospective är viktig för lärande

**3. Git Flow**

- Ingen direktpush till main!
- Pull Requests och Code Reviews
- Kolla commit history för arbetsfördelning
- Feature branches för varje User Story

**4. API Design**

- RESTful principles
- HTTP status codes
- Error handling
- Validation

---

## 🔍 Vad att Kolla Under Projektet

### Code Reviews med Teams (Mid-Sprint)

**Test Kvalitet:**

```csharp
// ✅ BRA
[Fact]
public void CalculatePoints_WithThreeWins_ReturnsNinePoints()

// ❌ DÅLIGT
[Fact]
public void Test1()
```

**Frågor att ställa:**

- Skrev ni testet först?
- Vad testar denna?
- Finns det edge cases?
- Är testet isolerat?

**Git Flow:**

- Visa `git log --graph --oneline --all`
- Finns feature branches?
- Är commits beskrivande?
- Görs code reviews?

**SCRUM Process:**

- Visa Kanban board
- Är den uppdaterad?
- Hur många stories är "Done"?
- Finns blockers?

---

## 📊 Bedömning

### G-Nivå Checklist

**Teknisk (30%)**

- [ ] API fungerar utan buggar
- [ ] CRUD för alla entiteter
- [ ] EF Core korrekt setup
- [ ] 80%+ code coverage
- [ ] CI/CD pipeline grön

**TDD (25%)**

- [ ] Tester före kod (fråga studenterna!)
- [ ] Alla tester gröna
- [ ] Unit + Integration tests
- [ ] Test naming convention följs

**SCRUM & Git (25%)**

- [ ] Kanban board uppdaterad
- [ ] 10+ User Stories med Acceptance Criteria
- [ ] Git Flow med 3+ branches
- [ ] Code reviews i PR:s
- [ ] Daily Standups dokumenterade (5+)
- [ ] Sprint Retrospective genomförd

**Dokumentation (20%)**

- [ ] README komplett (setup, endpoints, team)
- [ ] API endpoints dokumenterade
- [ ] Build badge fungerar
- [ ] Reflektion G-frågor besvarade (80+ ord)

---

### VG-Nivå Checklist

**Kräver G + följande:**

**Avancerad Testning (20%)**

- [ ] 90%+ code coverage
- [ ] Mocking med NSubstitute
- [ ] Parametriserade tester ([Theory])
- [ ] Edge cases testade

**Avancerad API (20%)**

- [ ] Pagination implementerad
- [ ] Filtering & Sorting
- [ ] DTOs med AutoMapper
- [ ] Swagger/OpenAPI docs

**Business Logic (20%)**

- [ ] Komplexa beräkningar (tabellställning, etc.)
- [ ] Aggregerad statistik
- [ ] SOLID-principer tillämpade

**SCRUM Advanced (20%)**

- [ ] Velocity tracking (burndown chart)
- [ ] Definition of Done per story
- [ ] Retrospective action items
- [ ] Sprint Goal achievement mätning

**VG-Reflektion (20%)**

- [ ] VG-frågor besvarade (120+ ord)
- [ ] Djup teknisk analys
- [ ] Kodexempel inkluderade
- [ ] Diskuterar design trade-offs

---

## 🚨 Vanliga Problem & Lösningar

### Problem: "Vi skriver alla tester i slutet"

**Lösning:**

- Påminn om TDD cykeln
- Kolla commits - tester ska komma före implementation
- Kräv att nästa feature görs med TDD
- Visa exempel live

### Problem: "Ingen gör code reviews"

**Lösning:**

- Kräv minst 1 approval per PR
- Setup branch protection på main
- Visa exempel på bra code review kommentarer
- Gör en code review tillsammans med teamet

### Problem: "Kanban board är inte uppdaterat"

**Lösning:**

- Påminn vid Daily Standup
- Integrera board-uppdatering i standup
- Visa hur board hjälper teamet
- Kolla board vid mid-sprint check-in

### Problem: "En teammedlem jobbar inte"

**Lösning:**

- Kolla git commits - tydlig data
- Prata enskilt med personen
- Diskutera i retrospective
- Eventuellt individuell bedömning

### Problem: "Mycket merge conflicts"

**Lösning:**

- Visa hur man löser konflikter
- Rekommendera mindre, mer frekventa commits
- Pair programming för svåra delar
- Git workshop om behövs

---

## 💡 Tips för Handledning

### Vid Daily Standups (om du är med)

**Frågor att ställa:**

- Vad var största utmaningen igår?
- Vad är målet idag?
- Finns det blockers jag kan hjälpa med?
- Hur ligger ni till mot Sprint Goal?

### Vid Mid-Sprint Check-in

**Agenda (30 min per team):**

1. Demo vad ni byggt (10 min)
2. Visa tester (5 min)
3. Git & branching strategi (5 min)
4. Diskutera utmaningar (5 min)
5. Feedback & tips (5 min)

### Vid Sprint Review

**Format:**

- Varje team: 10 min presentation
- Fokusera på demo av fungerande features
- Frågor från andra teams
- Din feedback

### Vid Retrospective

**Facilitera diskussion:**

- Vad gick bra?
- Vad gick mindre bra?
- Vad ska vi förbättra?
- Action items (konkreta!)

---

## 📁 Inlämning

**Vad ska lämnas in?**

1. **GitHub Repository URL**

   - Dela organisationens repo
   - Säkerställ att jag har access

2. **Google Classroom**

   - Zip av hela projektet (backup)
   - Länk till GitHub repo
   - Länk till Kanban board

3. **Individuella Reflektioner**
   - Ska finnas i repot: `docs/reflections/[namn].md`
   - Varje teammedlem lämnar egen

**Deadline: Söndag 18/1 23:59**

---

## 🏆 Awards Ceremony (21/1)

**Kategorier:**

**🧪 Best Tests Award**

- Bäst test coverage
- Mest kreativa test cases
- Tydligast test naming

**🎨 Best API Design Award**

- RESTful design
- Swagger docs
- Error handling

**🤝 Best Teamwork Award**

- Git commits fördelning
- Code review kvalitet
- SCRUM process

**📚 Best Documentation Award**

- README
- API docs
- Code comments

**🚀 Best Overall Project**

- Helhetsbedömning
- Fungerar perfekt
- Clean code

---

## ✅ Marcus Checklista

**Vecka 1:**

- [ ] Genomgång av projekt (Måndag)
- [ ] Runda runt teams (Onsdag)
- [ ] Mid-sprint check-ins (Torsdag)

**Vecka 2:**

- [ ] Sprint Reviews (Måndag)
- [ ] UAT facilitation (Onsdag)
- [ ] Final code reviews (Torsdag)

**Vecka 3:**

- [ ] Demo Day facilitation (Måndag/Onsdag)
- [ ] Bedömning klart
- [ ] Awards ceremony

---

## 📞 Support

**Studenterna kan få hjälp via:**

- Frågor i klassrummet
- Discord #help channel
- Vid handledningstillfällen

**Uppmuntra:**

- Googla fel (del av lärandet!)
- Fråga teamet först
- Stack Overflow är OK
- AI-verktyg OK för förklaringar (ej kodgenerering)

---

_© Campus Mölndal 2026 - Lärarinstruktioner_
_Marcus Medina_
