# Övning 1 — Bilen 🚗

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟢 Grundnivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Du jobbar på ett biluthyrningsföretag. De behöver ett system för att hålla koll på sina bilar — märke, modell, hur många mil de körts och om de är tillgängliga för uthyrning just nu.

Systemet är enkelt. Men det måste vara rätt byggt.

---

## Vad gäller för den här övningen

- [ ] Kan skriva en klass med privata fält och properties
- [ ] Förstår vad en konstruktor gör
- [ ] Kan anropa metoder på ett objekt

---

## Uppgift

Skapa en klass `Car` med följande:

**Privata fält:**
- `make` (string) — märke, t.ex. "Volvo"
- `model` (string) — modell, t.ex. "V70"
- `mileage` (int) — antal körda mil
- `isAvailable` (bool) — tillgänglig för uthyrning?

**Properties** (publik get, privat set):
- `Make`, `Model`, `Mileage`, `IsAvailable`

**Konstruktor** som tar in: make, model, startmileage  
*(Alla nya bilar startar som tillgängliga — `isAvailable = true`)*

**Metoder:**
- `Rent()` — om bilen är tillgänglig: sätt `isAvailable = false`, skriv ut `"[märke] [modell] är nu uthyrd."` Annars: `"Bilen är inte tillgänglig."`
- `Return(int milDriven)` — lägg till `milDriven` till `mileage`, sätt `isAvailable = true`, skriv ut `"Bilen är tillbaka. Total körsträcka: [mileage] mil."`
- `Describe()` — skriv ut bilens info (se exempeloutput)

**I `Main()`:**
- Skapa minst 2 bilar
- Hyr ut en bil med `Rent()`
- Försök hyra ut samma bil igen
- Lämna tillbaka den med `Return(15)`
- Anropa `Describe()` på båda bilarna

---

## Exempeloutput

```
Volvo V70 är nu uthyrd.
Bilen är inte tillgänglig.
Bilen är tillbaka. Total körsträcka: 12315 mil.

--- Bilinfo ---
Märke:       Volvo
Modell:      V70
Körsträcka:  12315 mil
Status:      Tillgänglig

--- Bilinfo ---
Märke:       Toyota
Modell:      Corolla
Körsträcka:  4200 mil
Status:      Tillgänglig
```

---

## Klar snabbt? Utmaning 🔴

Lägg till ett privat fält `rentalCount` (int) som räknar hur många gånger bilen hyrts ut.  
Visa det i `Describe()`.

Klassen håller koll på det själv — ingen ska kunna sätta det utifrån.
