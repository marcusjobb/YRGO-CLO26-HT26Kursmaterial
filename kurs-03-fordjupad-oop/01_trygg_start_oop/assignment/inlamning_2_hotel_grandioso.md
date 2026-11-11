# Inlämning 2 — Hotel Grandioso

**Individuell uppgift**

---

## Bakgrunden

Hotel Grandioso behöver ett nytt bokningssystem. Det gamla systemet är ett Excel-ark som ingen förstår och som kraschar varje gång Margareta på receptionen råkar trycka på fel tangent.

Du ska bygga ett nytt system med EF Core. Systemet ska hantera rumsbokning, prissättning och spabehandlingar — och det ska vara omöjligt att dubbelbokaett rum, även för Margareta.

---

## Hotellets struktur

```
Våning 3 (Toppvåning)
├── Rum 301–303  Standard (120 m²) — 1 200 kr/natt
├── Rum 304      Lyxsvit (180 m²) — 2 800 kr/natt
└── Rum 305      Bröllopsvit (220 m²) — 3 500 kr/natt

Våning 2
└── Rum 201–205  Standard (100 m²) — 1 000 kr/natt

Våning 1
└── Rum 101–105  Standard (80 m²) — 800 kr/natt

Källare — Spa Grandioso
├── Entré (per timme)    150 kr
├── Massage (60 min)     950 kr
├── Pedikyr (stortån)    350 kr
└── Pedikyr (hel fot)    650 kr
```

---

## Entiteter (minst dessa)

```csharp
Room          // RoomNumber, Floor, SizeM2, PricePerNight, RoomType
RoomType      // Name (Standard/Luxury/Bridal), Description
Booking       // GuestName, CheckIn, CheckOut, RoomId → Room
SpaBooking    // GuestName, Date, TreatmentId → Treatment
Treatment     // Name, DurationMinutes, Price
```

Du får lägga till fler entiteter om du ser ett behov.

---

## Funktioner som ska finnas

### G — Godkänt

- [ ] Boka ett rum (gästnamn, incheckning, utcheckning)
- [ ] Avboka en bokning
- [ ] Räkna ut totalpris för en bokning (antal nätter × pris per natt)
- [ ] Visa alla aktuella bokningar
- [ ] Visa alla rum (med typ och pris)
- [ ] EF Core Code First med migrationer

### VG — Väl godkänt

Allt i G, plus:

- [ ] Systemet förhindrar dubbelbokning — samma rum kan inte bokas två gånger för överlappande datum
- [ ] Boka en spabehandling (gästnamn, datum, behandling)
- [ ] Visa tillgängliga rum för ett givet datumintervall
- [ ] Du kan muntligt förklara dina entitetsrelationer och varför du valde dem
- [ ] Du kan förklara hur LINQ-frågan för tillgänglighetskontroll fungerar

---

## Förväntad körning (exempel)

```
=== Hotel Grandioso ===

1. Boka rum
2. Avboka
3. Visa bokningar
4. Visa lediga rum
5. Boka spa
6. Avsluta

Val: 1
Gästnamn: Anna Svensson
Incheckning (YYYY-MM-DD): 2026-08-15
Utcheckning (YYYY-MM-DD): 2026-08-18
Välj rum:
  101 - Standard  80m²  800 kr/natt
  201 - Standard 100m² 1000 kr/natt
  305 - Bröllopsvit 220m² 3500 kr/natt
  ...
Rumsnummer: 305
Bokning bekräftad! Totalt: 10 500 kr (3 nätter × 3 500 kr)
```

---

## Tekniska krav

- EF Core Code First
- SQLite eller LocalDB (välj ett)
- Migrationer i projektet
- LINQ för queries — inga råa SQL-strängar

---

## Inlämning

GitHub-repo med:

1. Komplett C#-projekt som går att köra
2. `README.md` med instruktioner för hur man kör projektet
3. Migrationer inkluderade

**Deadline:** Fredag v3 (exakt datum annonseras på Classroom)

---

*Tips: Börja med entiteterna och relationsdiagrammet innan du skriver en rad kod. Fem minuter med papper och penna sparar tre timmar av omskrivning.*
