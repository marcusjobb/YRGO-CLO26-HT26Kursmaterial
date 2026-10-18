# GDPR i praktiken — riktiga scenarier

Den här artikeln fördjupar scenarierna från föreläsningen. Frågorna är verkliga — lösningarna kräver att man förstår *varför* lagen ser ut som den gör.

---

## Fotoalbum med kompisar online

### Publikt album

Du skapar ett fotoalbum på webben och sätter det till "publikt — alla kan se det."

Hushållsundantaget i Art. 2.2c gäller **inte** för offentlig publicering. Dina kompisar i bilderna har inte samtyckt till att synas på ett öppet internet. Bilder är personuppgifter (du kan identifiera personen). Det krävs laglig grund — i de flesta fall **samtycke** från de fotograferade.

**Vad gäller i praktiken:** Fråga de som syns i bilden. Har de sagt OK? Då är du trygg. Har de inte det — håll albumet privat.

### Privat album — bara kompisarna

Du skapar ett privat album och skickar en länk till de som är med på bilderna.

Det liknar hushållsundantaget mer, men det finns en viktig faktor: **plattformen**. Bilderna lagras på ett företags servrar. Företaget behandlar personuppgifter (bildfiler innehåller metadata: plats, tid, kanske ansiktsigenkänning). Det krävs att plattformen har ett ordentligt GDPR-avtal.

**Vad gäller i praktiken:** Privat album med de fotograferade = rimligt. Välj en europeisk eller GDPR-certifierad tjänst. Undvik plattformar som tränar AI-modeller på uppladdade bilder.

---

## Kund vill bli anonymiserad en vecka efter köpet

### Är det OK att anonymisera?

Art. 17 (rätten att bli glömd) ger kunden rätt att begära radering. Men rättigheten har undantag:

> Art. 17.3b: "Behandlingen är nödvändig för att uppfylla en rättslig förpliktelse."

Bokföringslagen (Sverige) kräver att verifikationer sparas i **7 år**. En faktura eller orderrad är en verifikation. Det går inte att radera den, oavsett vad kunden begär.

### Vad kan man ta bort?

| Data | Ta bort? |
|------|---------|
| Namn och kontaktuppgifter | ✅ Ja |
| Orderhistorik (utan personkoppling) | ✅ Ja, anonymisera |
| Faktura-ID och belopp | ❌ Nej — bokföringslagen |
| Leveransadress | ✅ Ja (om ej del av verifikation) |
| Marknadsföringsprofil | ✅ Ja |

**Bra databasdesign hjälper:** Med normalisering tar du bort kunden ur `Kunder`-tabellen (med CASCADE) och ersätter nödvändiga kopplingar med "Anonymiserad kund" i bokföringsraden. Du raderar inte ordern — du kopplar loss personen från den.

---

## Anonymiserad kund vill ha garantireparation

Kunden begärde anonymisering och är nu borta ur systemet. Tre månader senare återkommer de med en trasig produkt och vill ha garanti.

### Problemet

Garantin är kopplad till ett köp. Köpet finns i systemet (bokföringen), men kopplingen till personen är borttagen.

### Lösningen

Kunden **äger sitt kvitto** — det är deras bevis, inte systemets. Med ett kvitto kan du:

1. Verifiera att köpet skedde (datum, produkt, belopp finns i bokföringen)
2. Genomföra reparationen utan att skapa ett nytt kundkonto
3. Dokumentera ärendet med ett tillfälligt referensnummer

**Viktigt:** Anonymisering tar bort kundens konto — inte deras rättigheter som konsument. Garantirätten (konsumentköplagen) lever kvar.

**Vad detta lär oss om systemdesign:** Informera kunden *innan* anonymisering att framtida ärenden kräver kvitto. Bygg en tydlig process för "anonym kund med ärendehistorik."

---

## Anonymiserad kund vill lämna tillbaka varan

Kunden anonymiserades dagen efter köpet. Tre dagar senare ångrar de sig och vill returnera.

### Vad händer?

Exakt samma situation som garanti — fast med kortare tidsspann. Kunden har kvitto. Bokföringen har ordern. Kopplingen dem emellan är borttagen.

Returen är möjlig, men den hanteras **manuellt**:

1. Kunden visar kvitto
2. Du matchar mot bokföringsposten (order-ID finns på kvittot)
3. Du genomför återbetalning och loggar det som ett nytt bokföringstillfälle
4. Inget nytt konto skapas — kunden är fortfarande anonym

**Lärdomen:** Berätta vad anonymisering innebär *innan* kunden gör det. En modal dialog i appen: "Om du anonymiserar dig nu kan framtida ärenden bara hanteras med kvitto. Vill du fortsätta?"

---

## Kund vill ha utdrag över all data ni har om dem (Art. 15)

### Vad är Art. 15?

Rätten till tillgång — en registrerad person kan begära ett **registerutdrag**: en lista över all data ni behandlar om dem.

### Vad måste ni inkludera?

- Alla personuppgifter: namn, adress, e-post, telefon
- Orderhistorik
- Loggar (inloggningar, klick, beteendedata)
- Cookies och spårning om de kan kopplas till personen
- Marknadsföringsprofil
- Vem data delats med (tredje parter)
- Hur länge ni planerar att lagra datan

### Tidsram

**30 dagar** från att begäran kom in. Kan förlängas till 90 dagar vid komplexa ärenden — men kunden ska informeras inom 30 dagar om förlängningen.

### I praktiken

Det krävs att databasen är **sökbar per person**. Med en normaliserad databas och ett kundID är detta ett SELECT-kommando. Med en denormaliserad kaos-databas — en mardröm.

```sql
-- Med normaliserad databas: enkelt registerutdrag
SELECT k.Namn, k.Email, k.Telefon,
       o.OrderId, o.Datum, o.Belopp,
       p.Namn AS Produkt
FROM   Kunder k
JOIN   Ordrar o ON o.KundId = k.KundId
JOIN   Produkter p ON p.ProduktId = o.ProduktId
WHERE  k.KundId = 42;
```

**Det är ytterligare ett skäl att normalisera:** Det gör juridisk efterlevnad möjlig.

---

## En okänd ringer och frågar vad du sparat om kund X

### Svaret

**Nej.** Art. 5.1f — integritet och konfidentialitet. Personuppgifter får inte lämnas ut till obehöriga.

Kunden X har inte gett samtycke till att deras uppgifter lämnas ut till den som ringer.

### Vad gör du?

1. Verifiera inte att personen finns i systemet
2. Hänvisa till er dataskyddspolicy
3. Logga samtalet (internt)

**Undantag:** Polisen med ett domstolsbeslut, eller kunden själv som ringer och identifierar sig. I båda fallen — följ er interna process, inte magkänslan.
