---
marp: true
theme: default
class: invert
paginate: true
---

# Yrkesroll och Kommunikation

**Kurs:** Yrkesroll
**Modul:** 01 — Kommunikation och Professionalitet

---

## Vad ska vi lära oss idag?

- **Utvecklarens yrkesroll** — förväntningar och ansvar
- **Kommunikation** — tekniska koncept för icke-tekniska
- **Code Review** — kultur och teknik
- **Självledarskap** — ta ansvar för din utveckling
- **Teamwork** — samarbeta effektivt

---

## Utvecklarens Roll

En utvecklare gör mer än bara skriva kod:

| Område | Exempel |
|--------|---------|
| **Tekniskt** | Skriva kod, felsöka, arkitektur, testa |
| **Kommunikation** | Förklara tekniska beslut, skriva dokumentation |
| **Samarbete** | Code review, parprogrammering, möten |
| **Planering** | Estimera, bryta ner tasks, prioritera |
| **Lärande** | Hålla sig uppdaterad, lära nytt på jobbet |
| **Leverans** | CI/CD, deployment, drift |

---

## Kommunikation med Intressenter

**Intressenter** = alla som påverkas av projektet.

| Intressent | Bryr sig om | Undvik |
|------------|-------------|--------|
| Kund/PO | Funktion, deadline, kostnad | Tekniska detaljer |
| Chef | Resurser, risker, framsteg | Kodnivå-diskussioner |
| Team | Implementation, arkitektur | Byråkrati |
| Användare | Att det fungerar | Hur det fungerar internt |

**Gyllene regeln:** Anpassa språket efter mottagaren.

---

## Förklara Teknik för Icke-Tekniska

**Dåligt:** "Vi måste byta från Entity Framework till Dapper för att vi har N+1-problem med lazy loading och Change Tracker orsakar prestandaproblem i vår mikroservices-arkitektur."

**Bra:** "Appen är långsam när den hämtar kunddata. Med en annan databasteknik kan vi göra den snabbare. Det tar ungefär 3 dagar att byta."

---

## Code Review — Kultur

Code review är INTE:
- ❌ En chans att visa dig smartare
- ❌ Personlig kritik
- ❌ En formalitet som måste godkännas

Code review ÄR:
- ✅ Kollektivt ägande av koden
- ✅ Kunskapsdelning
- ✅ En chans att hitta buggar och förbättringar
- ✅ En lärande-möjlighet

---

## Code Review — Praktiska Tips

**Som granskare:**
- Fokusera på logik och design, inte kodstil
- Fråga: "Varför gjorde du så här?" istället för "Det är fel"
- Om du inte förstår koden — be om förtydligande
- Godkänn om det är bra nog, inte perfekt

**Som skribent:**
- Gör PR:er små (max 200-300 rader)
- Skriv en bra PR-beskrivning
- Kommentera din egen kod om den är otydlig
- Ta inte kritik personligt

---

## Självledarskap

**Du äger din egen utveckling.**

- **Be om hjälp när du fastnat** (men försök först i 20 min)
- **Säg när du inte kan** — bättre tidigt än sent
- **Lär dig av misstag** — gör en post-mortem för dig själv
- **Dokumentera** vad du lärt dig
- **Sätt upp mål** — vad vill du kunna om 6 månader?

---

## Time Management

| Teknik | Beskrivning |
|--------|-------------|
| **Pomodoro** | 25 min fokus → 5 min paus → upprepa |
| **Time-boxing** | Sätt en timer på en uppgift — sluta när den ringer |
| **Eisenhower-matris** | Prioritera: Viktigt+Bråttom göra först |
| **"Eat the Frog"** | Gör den jobbigaste uppgiften först på morgonen |
| **Deep Work** | Blocka kalendern för fokuserat arbete |

---

## Sammanfattning

- ✅ Utvecklarens roll = kod + kommunikation + samarbete
- ✅ Anpassa språket efter mottagaren
- ✅ Code review = lärande, inte kritik
- ✅ Självledarskap = äg din utveckling
- ✅ Time management = planera och fokusera

---
