# Pushen som kraschade production — Diskutera mera!

**Gruppstorlek:** 3–4 personer
**Tid:** 20 minuter

---

## Situationen

Jenny ar inne pa sin tredje vecka. Det ar fredag klockan 16:45. Kunden vant pa en fix.

Fredrik rusar forbi hennes skrivbord:
*"Jenny, pusha direkt till main. Vi har inte tid for pull request nu — kunden vant."*

Jenny tvekar. Hon vet att man inte brukar pusha direkt till main.

Fredrik: *"Jag tar ansvaret. Kor."*

Jenny pushar.

Klockan 17:03 kraschar production. En gammal konflikt i koden som ingen sag aktiverades av Jennys push. Hundratals kunder kan inte logga in.

Pa mandag kallar chefen till mote. Fragan pa bordet: **Vad hande och vem bar ansvaret?**

---

## Diskutera

**Innan ni borjar:** Alla laser forst situationen tyst for sig sjalva. Skriv ner din forsta tanke om ansvarsfragan pa en lapp. Efter diskussionen — har du andrat dig?

1. Vem bar ansvaret — Jenny eller Fredrik?
2. Spelar det roll att Fredrik sa "jag tar ansvaret"?
3. Borde Jenny ha vaggrat? Vad hade hant da?
4. Vad borde processen ha sett ut for att det har aldrig skulle handa?
5. Vad lar sig Jenny av det har — om kod, om arbetsplatser, om att saga nej?

---

## Inget ratt svar — men nagra saker att tanka pa

> Att "folja order" frigor dig inte fran ansvar for dina handlingar.
> Men en junior ska inte behova vara den sista forsvarslinjen mot daliga beslut.
>
> Bra system har skyddsmekanismer sa att en person inte kan krascha production.
> Det ar inte Jennys fel att systemet saknade sadana.

*Lararen leder avslutningsdiskussionen.*

---

<details>
<summary><strong>Lararversion — faciliteringsguide</strong></summary>

**PUBLICERAS INTE.** Denna version ar for lararen.

Det finns inget enkelt svar — det ar poangen.

**Bra diskussionspunkter:**
- Branch protection rules finns exakt for att forhindra det har
- "Jag tar ansvaret" ar latt att saga, svart att halla
- En junior i den positionen ar i en omojlig situation
- Systemdesign > individuellt ansvar — bygg system som forlater misstag

**Koppla till:** git workflow, pull requests, branch protection, deployment pipelines.

---

**Kopplingen till Milgram-experimentet**

Jennys situation ar en direkt parallell till Milgrams experiment (1963): en auktoritetsfigur ger en order, situationen ar stressad, och den underordnade lyssnar — trots att nagot kanns fel.

65 % av Milgrams deltagare fortsatte ge maximala "stotar" bara for att nagon i labbrock sade at dem att gora det. Jenny ar inte dum eller feg — hon befinner sig i exakt samma psykologiska situation.

**Fragan gruppen:** *"Vad hade behovts for att Jenny skulle ha vaggat saga nej?"*

Svaret leder till: psykologisk trygghet, tydliga processer, en kultur dar juniorer far ifragasatta.

*Koppla till fyrkanter-ovningen om gruppen gjort den.*
</details>
