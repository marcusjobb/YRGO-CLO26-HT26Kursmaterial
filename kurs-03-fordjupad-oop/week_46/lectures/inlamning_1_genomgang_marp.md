---
marp: true
theme: nion-dark
paginate: true
---

# Inlämning 1: Hotel Grandioso

**Kurs:** Fördjupad OOP i C#
**Deadline:** söndag 22 november

Marcus Ackre Medina · YRGO · CLO26

---

## Berättelsen

Hotel Grandioso bokar rum i ett Excel-ark. Det kraschar när Margareta i receptionen trycker på fel tangent.

Ni bygger ett bokningssystem med EF Core. Och det ska vara omöjligt att dubbelboka ett rum, även för Margareta.

---

## Ni har redan övat på allt

| Inlämningen kräver | Det övade vi på |
|---|---|
| Entiteter som klasser, Code First | *Hjältarna i databasen* |
| Migrationer | `migrations add Init` och `AddQuests` |
| Boka, visa, avboka | `Add`, `ToList`, `Remove` |
| Relationer (rum ↔ bokning) | *Mira tar uppdrag* |
| Antal nätter × pris | `End.DayNumber - Start.DayNumber` |
| Ingen dubbelbokning (VG) | En-raden: `a.Start < b.End && b.Start < a.End` |

---

## Rita först, koda sen

```text
 RoomType 1 ──── * Room 1 ──── * Booking * ──── 1 Guest
                                                  1
                                                  │
 Treatment 1 ──── * SpaBooking * ─────────────────┘
```

Läs så här: en rumstyp har många rum. Ett rum har många bokningar. En gäst har många bokningar.

Fem minuter med papper och penna sparar tre timmar.

---

## Så börjar du (steg för steg)

1. Skapa ett GitHub-repo och ett konsolprojekt i det.
2. Skriv **en** klass: `RoomType`. Lägg till `Room` med `RoomTypeId`.
3. `dotnet ef migrations add Init` och kör. Fungerar det? Fortsätt.
4. Lägg in hotellets rum med kod (en gång).
5. Visa alla rum. Sedan `Guest` och `Booking` (ny migration).
6. Boka, visa, avboka. Nu har du grunden för G.
7. Sist: dubbelbokningsspärren, lediga rum och spa (VG).

En klass i taget. Kör efter varje steg.

---

## G och VG

**G:** boka rum, avboka, totalpris, visa bokningar, visa rum, Code First med migrationer,
ett UML-klassdiagram och ett GDPR-avsnitt i README.

**VG:** allt i G, och
- ingen dubbelbokning
- spabokning
- lediga rum för ett datumintervall
- du kan **förklara** relationerna och LINQ-frågan

---

## Det som brukar gå snett

- Glömt migrationen: felet `PendingModelChangesWarning`. Kör `dotnet ef migrations add <Namn>`.
- Tom lista: glömt `Include(...)`.
- Fel antal nätter: räkna `CheckOut.DayNumber - CheckIn.DayNumber`. Utcheckningsdagen räknas inte.
- Allt i `Program.cs`: okej att börja så. Dela upp i klasser när det fungerar.

---

## Lämna in

GitHub-repo med:

1. Projekt som går att köra
2. `README.md` som säger hur man kör det, med UML-diagrammet och GDPR-avsnittet
3. Migrationerna

**Deadline:** söndag 22 november. Frågor tar vi på fredagens handledning.
