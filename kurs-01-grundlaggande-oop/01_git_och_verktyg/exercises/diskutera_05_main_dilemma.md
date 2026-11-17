# Pushen som kraschade production — Diskutera mera! 💬

**Gruppstorlek:** 3–4 personer
**Tid:** 20 minuter

---

## Situationen

Jenny är inne på sin tredje vecka. Det är fredag klockan 16:45. Kunden väntar på en fix.

Fredrik rusar förbi hennes skrivbord:

> "Jenny, pusha direkt till main. Vi har inte tid för pull request nu — kunden väntar."

Jenny tvekar. Hon vet att man inte brukar pusha direkt till main.

Fredrik: "Jag tar ansvaret. Kör."

Jenny pushar.

Klockan 17:03 kraschar production. En gammal konflikt i koden som ingen såg aktiverades av Jennys push. Hundratals kunder kan inte logga in.

På måndag kallar chefen till möte. Frågan på bordet: Vad hände och vem bär ansvaret?

---

## Diskutera

- Vem bär ansvaret — Jenny eller Fredrik?
- Spelar det roll att Fredrik sa "jag tar ansvaret"?
- Borde Jenny ha vägrat? Vad hade hänt då?
- Vad borde processen ha sett ut för att det här aldrig skulle hända?
- Vad lär sig Jenny av det här — om kod, om arbetsplatser, om att säga nej?

---

## Inget rätt svar — men några saker att tänka på

- Att "följa order" frigör dig inte från ansvar för dina handlingar.
- Men en junior ska inte behöva vara den sista försvarslinjen mot dåliga beslut.
- Bra system har skyddsmekanismer så att en person inte kan krascha production ensam.
- Det är inte Jennys fel att systemet saknade sådana.

---

*Läraren leder avslutningsdiskussionen.*
