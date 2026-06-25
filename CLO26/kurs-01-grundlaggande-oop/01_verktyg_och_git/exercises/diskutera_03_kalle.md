# Kodaren Kalle — Diskutera mera! 💬

**Gruppstorlek:** 3–4 personer
**Tid:** 15–20 minuter
**Ingen kod krävs**

---

## Historien

Kalle jobbar sent en fredagskväll. Han är ensam på kontoret. Systemet ska vara redo på måndag.

Klockan 22:17 får han ett Slack-meddelande från sin kollega Fatima:
*"Kalle, pushar du eller ska jag? Jag är klar med min del."*

Kalle svarar: *"Jag är klar om 10 min."*

Klockan 22:31 loggar Fatima in i systemet och ser att koden redan är pushad — men inte av henne.

Commit-meddelandet lyder: *"Final cleanup and deploy prep"*
Commit-tid: **22:24**
Commit-användare: **k.lindqvist**

Kalle heter Karl Lindqvist. Hans GitHub-användarnamn är **k.lindqvist**.

Men Kalle stirrar på skärmen:
*"Jag har inte committat något."*

---

## Del 1 — Individuellt (5 min)

Markera varje påstående **utan att diskutera med gruppen**.

Använd:
- **S** = Sant (du är säker, historien bekräftar det)
- **F** = Falskt (du är säker, historien motbevisar det)
- **?** = Okänt / går inte att avgöra

| # | Påstående | Svar |
|---|-----------|------|
| 1 | Kalle pushade koden klockan 22:24. | |
| 2 | Fatima pushade koden. | |
| 3 | Git-historiken har fel. | |
| 4 | Kalle är den enda med användarnamnet k.lindqvist. | |
| 5 | Koden som pushades är Kalles kod. | |
| 6 | Commit-meddelandet skrevs av Kalle. | |
| 7 | Fatima och Kalle jobbade på samma fil. | |
| 8 | Kalle vet exakt vad han gjort den senaste timmen. | |

---

## Del 2 — Diskutera i gruppen (5–10 min)

- Vad kan ha hänt?
- Litar ni på git-historiken? Varför / varför inte?
- Vad borde Kalle göra *innan* han drar några slutsatser?

---

## Del 3 — Avslöjandet

*Läraren läser upp det här när grupperna är klara med sin diskussion.*

> Kalle kör `git log` en gång till och scrollar upp.
>
> Där, tre rader ovanför, ser han en commit från **22:09** — också av k.lindqvist.
>
> Han kör `history` i terminalen. Rad 847: `git commit -m "Final cleanup and deploy prep"`
>
> Klockan 22:24. Det stämmer.
>
> Kalle hade committat. Han var så inne i sitt flöde — och så trött — att han inte mindes det.

---

## Kopplingen till programmering

Git ljuger sällan. Innan du misstänker systemet — eller någon annan — kontrollera vad **du** faktiskt gjort.

> `git log`, `git status` och terminalhistoriken är dina bästa vänner när något känns konstigt.

Det är därför vi skriver **beskrivande commit-meddelanden**. Framtida du — trött, stressad, måndag morgon — kommer att tacka dig.

---

*Läraren leder avslutningsdiskussionen.*
