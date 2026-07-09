# Inlämningsuppgift — Kontaktlistan

**Vecka:** 06  
**Deadline:** Söndag [vecka 6] 23:59  
**Förlängd deadline:** Fredag [vecka 7] 23:59  
**Inlämning:** Länk till din fork i Google Classroom

---

## Bakgrunden

Din telefon lagrar hundratals kontakter. Någonstans i ett datacenter körs kod som håller reda på dem.

Den koden ser ungefär ut som det du ska skriva nu.

Din uppgift är att bygga ett enkelt kontakthanteringsprogram — ett program där man kan lägga till kontakter, visa alla, och söka efter en specifik person. Inga databaser. Ingen molnlagring. Bara en lista och en klass.

---

## Vad gäller för den här inlämningen

*Som utbildare vill jag att du...*

- [ ] kan skriva en klass med properties och konstruktor
- [ ] kan använda `List<T>` för att lagra och hantera objekt
- [ ] kan bygga en menyloop med `while` och hantera användarval
- [ ] kan söka i en lista med en loop eller LINQ
- [ ] förstår skillnaden mellan en klass (mallen) och ett objekt (instansen)

*Bocka av dem själv innan du lämnar in.*

---

## Krav för Godkänt (G)

**Klassen `Contact`**
- [ ] Properties: `Name` (string), `Phone` (string), `Email` (string)
- [ ] Konstruktor som tar in och sätter alla tre värden

**Listan och programflödet**
- [ ] En `List<Contact>` används för att lagra kontakterna
- [ ] Minst **3 kontakter är förinlagda** i listan när programmet startar
- [ ] En `while`-loop håller igång ett meny-drivet gränssnitt tills användaren väljer att avsluta

**Menyval som måste finnas:**
- [ ] 1 — Lägg till kontakt (läs in namn, telefon och e-post från användaren)
- [ ] 2 — Visa alla kontakter (lista dem med nummer)
- [ ] 3 — Sök kontakt (sök på namn, skriv ut om kontakten hittas eller inte)
- [ ] 4 — Avsluta

- [ ] Ingen `var` används

---

## Krav för Väl Godkänt (VG)

*Alla G-krav måste vara uppfyllda. VG är ett tillägg, inte en ersättning.*

- [ ] Koden är uppdelad i metoder — t.ex. `AddContact`, `ShowAllContacts`, `SearchContact`
- [ ] Sökningen är skiftlägesokänslig (stor/liten bokstav spelar ingen roll)
- [ ] Inmatning valideras — tomma namn eller tomma fält accepteras inte
- [ ] Utskriften är välformaterad och lätt att läsa
- [ ] Det finns kommentarer som förklarar vad de viktigaste delarna gör

---

## Exempeloutput

```
=== KONTAKTLISTA ===
1. Lägg till kontakt
2. Visa alla kontakter
3. Sök kontakt
4. Avsluta
Val: 2

Kontakter (3 st):
1. Anna Andersson — 070-1234567 — anna@mail.se
2. Erik Berg — 073-9876543 — erik@mail.se
3. Maria Lind — 076-5554433 — maria@mail.se

=== KONTAKTLISTA ===
1. Lägg till kontakt
2. Visa alla kontakter
3. Sök kontakt
4. Avsluta
Val: 3

Ange namn att söka på: erik
Hittade: Erik Berg — 073-9876543 — erik@mail.se

=== KONTAKTLISTA ===
1. Lägg till kontakt
2. Visa alla kontakter
3. Sök kontakt
4. Avsluta
Val: 4

Hejdå!
```

---

## Tips

Det finns inga startfiler. Du bygger detta från scratch.

Börja med klassen `Contact` — se till att den fungerar som förväntat innan du bygger resten.  
Sedan: bygg menyloopen. Sedan: lägg till en funktion i taget.  
Om du fastnar i mer än 15 minuter: fråga klassen → AI → Marcus. I den ordningen.

---

## Bedömning

Rättning sker veckan efter deadline.  
Feedback per mail och i Google Classroom.  
Commits efter deadline beaktas inte — spara commit-hashen du lämnar in på.
