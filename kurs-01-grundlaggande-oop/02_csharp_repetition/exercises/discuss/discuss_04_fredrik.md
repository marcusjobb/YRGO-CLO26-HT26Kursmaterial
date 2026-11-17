# Fullstack-experten Fredrik — Diskutera mera!

**Gruppstorlek:** 3–4 personer
**Tid:** 15–20 minuter
**Ingen kod kravs**

---

## Historien

Fredrik ar senior fullstack-utvecklare med tio ars erfarenhet. Han ar kand for att "alltid ha
ratt" och att sallan forklara sina beslut.

Pa ett mote presenterar han losningen pa ett bug som kostat teamet tre dagar:

*"Jag hittade problemet. Rad 247 i UserService. Jag fixade det i morse."*

Jenny (nu inne pa sin andra vecka) rackter upp handen: *"Vad var felet egentligen?"*

Fredrik: *"En typisk nyborjarmiss. Det ar fixat nu."*

Kalle tittar i koden och ser att andringen pa rad 247 ar:

```
// Gammal kod (borttagen):
// if (user.age >= 18)

// Ny kod:
if (user.age > 17)
```

Systemet fungerar nu. Buggen ar borta.

---

## Del 1 — Individuellt (5 min)

**Sa har du tanker:** Du far 10 pastacenden. For varje ska du avgora om pastacendet stammer.
Precis som i Jenny-ovningen: vad vet du sakert? Vad antar du?

| # | Pastacende | Svar |
|---|-----------|------|
| 1 | Fredrik loste buggen. | |
| 2 | Andringen pa rad 247 var korrekt. | |
| 3 | Det var en nyborjarmiss. | |
| 4 | Jenny borde inte ha fragat. | |
| 5 | Kalle forstar nu vad felet var. | |
| 6 | Fredrik kommunicerade losningen professionellt. | |
| 7 | `>= 18` och `> 17` ger alltid samma resultat. | |
| 8 | Teamet larde sig nagot av den har buggen. | |
| 9 | Fredrik borde ha forkart felet for teamet. | |
| 10 | Buggen berodde pa ett logikfel. | |

---

## Del 2 — Diskutera i gruppen (10 min)

**Tank efter:** Pastacende 7 ar knepigt. For dig som programmerare — ar `>= 18` samma sak som `> 17`? Nar skulle de kunna ge olika resultat? (Ledtrad: tank pa datatyper som inte ar heltal.)

- Vad **vet** ni om vad felet faktiskt var?
- Vad tycker ni om hur Fredrik kommunicerade losningen?
- Vad borde ha hant istallet?
- Vad kan Jenny lara sig av det har motet — om kod OCH om arbetsplatser?

---

## Kopplingen till programmering

> Kod som "fungerar" ar inte samma sak som kod som ar ratt — eller forstadd.
> Och en losning som inte forklaras loser inte problemet for teamet, bara for stunden.

> Det har ar ocksa en paminnelse: **det finns inga dumma fragor.** Jenny fragade ratt.

*Lararen leder avslutningsdiskussionen.*

---

<details>
<summary><strong>Lararversion — faciliteringsguide</strong></summary>

**PUBLICERAS INTE.** Denna version ar for lararen.

**Det knepiga:** `>= 18` och `> 17` ger **samma resultat for heltal** men ar konceptuellt olika. Det ena uttrycker avsikten tydligare. Bra diskussion om lasbarhet och intentionell kod.

**Fredriks monster:** Han loste buggen tekniskt men kommunicerade inte. Det ar ett klassiskt senior-monster som forstor teamkultur — ingen lar sig nagot, och nasta gang uppstar samma bugg.

**Poanger att lyfta:**
- Clean code handlar om att koden ska kommunicera avsikt, inte bara fungera
- Commit-meddelanden ska forklara *varfor*, inte bara *vad*
- "Jag fixade det" ar inte samma sak som "vi forstar vad som gick fel"
- Psykologisk trygghet — kan Jenny stalla fragor till Fredrik utan att kanna sig dum?

**Bra foljdfraga:** "Vad hade hant om Fredrik skrivit ett commit-meddelande som forkart felet?"

**Kopplar till:** clean code, commit-meddelanden, code reviews, arbetsplatskommunikation. Forbereder for kurs 7 (konsultmassighet).
</details>
