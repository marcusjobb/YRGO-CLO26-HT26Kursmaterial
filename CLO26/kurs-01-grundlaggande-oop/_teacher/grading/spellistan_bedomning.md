# Lärarguide — Bedömning Spellistan

*Inte för studerande.*

---

## G-checklista

Alla punkter måste vara uppfyllda för G.

**Klassen `MusicArtist`**
- [ ] Minst 5 privata fält (name, genre, debutYear, country, isActive)
- [ ] Properties med `private set` (eller motsv.) — inte publika fält
- [ ] Konstruktor finns och används vid `new MusicArtist(...)`
- [ ] `Describe()` finns och skriver ut något läsbart
- [ ] `Perform()` finns och skriver ut ett meddelande

**Main()**
- [ ] Minst 3 objekt skapas
- [ ] `Describe()` anropas
- [ ] `Perform()` anropas på minst ett objekt

**Reflektion**
- [ ] REFLEKTION.md finns i repot
- [ ] Frågan om private/public besvarad konkret (inte bara "för säkerhets skull")
- [ ] Klass vs objekt förklarat med egna ord

---

## VG-checklista

Alla G-punkter uppfyllda. Sedan:

- [ ] `Song`-klass finns med minst 4 fält
- [ ] `DurationFormatted` returnerar korrekt `"m:ss"`-format (kolla edge case: 3:07 inte 3:7)
- [ ] `MusicArtist` har `List<Song>` som privat fält
- [ ] `AddSong()` lägger faktiskt till i listan
- [ ] `ShowDiscography()` loopar och visar alla låtar
- [ ] Minst 3 låtar skapas och läggs till i Main()
- [ ] VG-reflektion besvarad med konkret motivering

---

## Fallgropar att leta efter

**Publika fält istället för properties**  
`public string name;` är inte en property. Det är ett publikt fält. Saknar kontroll — det är poängen. Är det G? Nej. Ge feedback: "Det fungerar, men properties ger kontroll vi pratar om i kursen."

**`DurationFormatted` formaterar fel**  
`3:07` är rätt. `3:7` är fel. Kontrollera: `seconds % 60 < 10` → ska ha en nolla framför. Vanligt miss.

**Listan är publik eller statisk**  
`public List<Song> songs = new List<Song>();` — publikt fält, ingen inkapsling. Samma problem som ovan.

**Reflektion på en rad**  
"Det är säkrare" är inte ett svar. Fråga: vad händer konkret om `name` är publik? Kan de visa det?

---

## Betygsmotivering — mall

```
BETYG: G / VG / IG

STYRKOR:
- [nämn något konkret och specifikt från deras kod]

ATT FÖRBÄTTRA:
- [konkret punkt med förslag]

MOTIVERING:
[2-3 meningar kopplade till kriterierna]
```

---

## Om IG

Skriv exakt vilken/vilka G-punkter som inte uppfylldes.  
Omtentamen: samma inlämning med en veckas tillägg, deadline fre 25 sep.
