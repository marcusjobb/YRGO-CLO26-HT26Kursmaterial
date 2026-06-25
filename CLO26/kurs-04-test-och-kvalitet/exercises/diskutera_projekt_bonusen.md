# Bonusen — Diskutera mera! 💬

*Ett planeringsscenario i flera delar. Inga rätta svar — men konsekvenser av varje val.*

**Gruppstorlek:** 5 personer (roller tilldelas)
**Tid:** 60–90 minuter totalt

---

## Rollerna

Bestäm vem i gruppen som är vad:

| Roll | Ansvar |
|------|--------|
| **Scrum Master** | Håller i möten, skyddar teamet, eskalerar problem |
| **Kodare 1** | Backend-utveckling |
| **Kodare 2** | Backend-utveckling |
| **Frontend-utvecklare** | UI och integration |
| **Testare** | Kvalitetssäkring |

---

## Uppdraget

Ert team har fått ett spännande erbjudande.

KundAB behöver ett **kundhanteringssystem** klart till **fredag 7 maj 2027**.

Systemet ska hantera:
- Inloggning med roller (admin, användare, gäst)
- Kundregister med CRUD
- Orderhantering
- Rapportgenerering
- Exportfunktion (Excel/PDF)
- Integrering med befintligt faktureringssystem

Projektledaren berättar att teamet normalt levererar ett enklare kundhanteringssystem på **4 veckor**. Det här systemet är mer omfattande.

**Bonusen:** Klart innan deadline = **50 000 kr extra** till teamet att fördela.

---

## Teaminfo

Innan ni planerar — lite om era teammedlemmar:

- **Scrum Master** har erfarenhet av att facilitera men kodar inte aktivt
- **Kodare 1 och 2** är heltidskodare
- **Frontend-utvecklaren** kan viss backend men är starkast på UI
- **Testaren** är utbildad utvecklare och kan hoppa in och koda om det behövs — men om hen kodar, vem testar då?

---

## Del 1 — Planera (20 min)

Ni har 6 veckor: **måndag 22 mars till fredag 7 maj 2027**.

### Kalendern ser ut så här:

| Vecka | Datum | Röda dagar |
|-------|-------|-----------|
| 1 | 22–26 mars | **Långfredag 26/3** — röd dag |
| 2 | 29 mars–2 april | **Annandag påsk 29/3** — röd dag |
| 3 | 5–9 april | — |
| 4 | 12–16 april | — |
| 5 | 19–25 april | — |
| 6 | 28 april–7 maj | **1 maj på lördag** — ingen extra ledig dag. **Kristi Himmelsfärd 6/5** — röd dag, dagen INNAN deadline |

> OBS: **Kristi Himmelsfärd** är torsdagen den 6 maj — dagen innan ni ska leverera.
> **1 maj** infaller på en lördag 2027 — ingen extra ledig dag.

### Beräkna tillgängliga dagar

Ett team på 5 personer × 5 dagar = **25 dagsverk per vecka** normalt.

Räkna ut:
- Hur många effektiva dagsverk har ni totalt under de 5 veckorna?
- Hur fördelar ni arbetet på systemets delar?
- Vilka delar är beroende av varandra och måste göras i ordning?

---

## Del 2 — Komplikationen (10 min)

I mitten av sprint 2 meddelar **Kodare 2** att hen är sjuk i en vecka.
Hen har ett barn som har vattkoppor och kan inte komma in.

Ni har fortfarande samma deadline. Samma bonus.

*Hur påverkar det er plan? Vad prioriterar ni bort — om något?*

---

## Del 3 — Erbjudandet (10 min)

Projektledaren hör om situationen och lägger fram ett alternativ:

> *"Integrationen mot faktureringssystemet är komplex och tar uppskattningsvis en vecka.
> Vi kan låta ett AI-verktyg generera den delen. Det kostar:*
> - *Abonnemang: 500 kr/månad*
> - *Uppskattad token-kostnad för uppgiften: 800–1 200 kr*
> - *Promptning och anpassning: 1–2 dagars arbete*
>
> *Fördelen: ni sparar ca 3 dagsverk på kodning."*

Diskutera i gruppen:
1. Är det värt det ekonomiskt?
2. Vad händer om integrationen inte fungerar som förväntat?
3. Vem i teamet ska ansvara för att förstå och verifiera koden?
4. Vad **planerar ni in** som konsekvens av det här beslutet?

---

## Del 4 — Fredagen innan deadline (10 min)

Det är torsdag 29 april, klockan 16:00.

Systemet verkar fungera. Ni har testat de viktigaste flödena.
Testaren säger: *"Jag skulle behöva ytterligare två dagar för ordentlig testning."*

Deadline är imorgon fredag. Bonusen är 50 000 kr.

**Ni måste besluta:**

**A)** Leverera imorgon som planerat. Systemet verkar fungera.

**B)** Kontakta kunden, berätta läget, be om förlängt deadline. Riskerar bonusen.

**C)** Leverera imorgon men berätta för kunden att ytterligare testning rekommenderas.

*Vad väljer ni? Varför? Vad kan gå fel med varje alternativ?*

---

## Del 5 — Tre veckor senare (10 min)

Välj ett av scenarierna baserat på ert beslut i del 4:

**Om ni valde A:**
> Kunden hör av sig. Det är problem med exportfunktionen — den kraschar vid stora datamängder.
> Det tar tre dagar att hitta och fixa buggen.
> Kunden är irriterad men nöjd med att det löste sig snabbt.
> *Fick ni bonusen? Var det värt det?*

**Om ni valde B:**
> Kunden accepterade förlängningen motvilligt.
> Ni levererade en vecka senare — utan bonus.
> Systemet fungerar utan kända buggar.
> *Vad kostar en veckas extra arbete jämfört med bonusen? Vad kostar er trovärdighet?*

**Om ni valde C:**
> Kunden tog emot systemet. Läste varningen. Ignorerade den.
> Tre veckor senare: exportkrash. Kunden är arg — *"Ni visste om det!"*
> *Hjälpte varningen er juridiskt? Etiskt?*

---

## Slutdiskussion

1. Vilket beslut var rätt — A, B eller C?
2. Vad borde ha planerats annorlunda **från dag ett**?
3. Vad lärde ni er om estimering, röda dagar och beroenden?
4. AI-alternativet — var det rätt val? Vad saknades i beslutsprocessen?
5. Scrum Master — vad var din roll när komplikationerna dök upp?

> 💬 *Det finns inget scenario där man slipper undan konsekvenserna — bara olika typer av konsekvenser.*

---

<details>
<summary>Lärarens anteckningar</summary>

**Kalkyl att ha redo om gruppen fastnar:**

Normal hastighet: 4 veckor × 25 dagsverk = 100 dagsverk för ett enklare system.
Det här systemet: uppskattningsvis 1.8× = ~180 dagsverk.

Tillgängliga dagsverk (spring 2027, 6 veckor, 5 pers):
- Vecka 1: 4 dagar × 5 = 20 (Långfredag borta)
- Vecka 2: 4 dagar × 5 = 20 (Annandag påsk borta)
- Vecka 3: 5 × 5 = 25
- Vecka 4: 5 × 5 = 25
- Vecka 5: 5 × 5 = 25
- Vecka 6: 4 dagar × 5 = 20 (Kristi Himmelsfärd 6/5 borta — dagen INNAN leverans)
- **Totalt: 135 dagsverk** — mot uppskattade 180.

Och fällan: Kristi Himmelsfärd är torsdagen den 6 maj.
Fredag 7 maj är deadline. Sista effektiva testdagen är **onsdag 5 maj**.
De som inte planerar för det inser det alldeles för sent.

Det håller inte oavsett om de vibear eller inte — testning tar tid, och den tiden måste in i planen.

Om de använder testaren som kodare sparar de ca 5 dagsverk på kodning, men tappar
i princip all dedikerad testtid. Det är ett kort man kan spela — men det är ett dyrt kort.

**När AI-förslaget dyker upp — ställ bara dessa två frågor:**
1. *"Hur har ni verifierat att programmet fungerar?"*
2. *"Hur många veckor har ni planerat för testning?"*

Om svaret på fråga 2 är noll — de gör en Birger.
Låt dem inse det själva. Säg ingenting mer.

Om de inte inser det: låt dem köra på. Del 2-4 tar hand om resten.

**AI-alternativet:** 1–2 dagars promptning + token-kostnad sparar 3 dagsverk netto.
Hjälper lite — men testaren behöver fortfarande tid att förstå och testa koden.
Om ingen testar integrationen är det en tickande bomb.

**Budskapet:** Planeringen är problemet — inte verktygen.

</details>
