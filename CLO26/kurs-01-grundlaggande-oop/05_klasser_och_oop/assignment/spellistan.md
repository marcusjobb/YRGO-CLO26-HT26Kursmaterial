# Inlämningsuppgift 1 — Spellistan 🎵

**Vecka:** 3 (v38)  
**Deadline:** Söndag 21 sep 2026, 23:59  
**Förlängd deadline:** Fredag 25 sep 2026, 23:59  
**Inlämning:** Länk till din fork i Google Classroom

---

## Bakgrunden

Taylor Swift har 274 låtar i sin diskografi.

Det är för många att hålla i huvudet. Det är därför vi har kod.

Din uppgift är att bygga ett artistregister — ett enkelt program där du kan lagra och visa info om musikartister. Du väljer själv vilka artister du lägger in. Det kan vara Taylor Swift. Det kan vara Metallica. Det kan vara din favoritartist som ingen annan i klassen känner till.

Vi dömer inte. Vi tittar på koden.

---

## Vad gäller för den här inlämningen

*Som utbildare vill jag att du...*

- [ ] kan skriva en klass med privata fält och properties
- [ ] förstår vad en konstruktor gör och varför man använder den
- [ ] kan skilja på vad som är `private` och vad som är `public` — och förklara varför
- [ ] kan skapa objekt och anropa metoder på dem
- [ ] kan reflektera kring dina designval

*Bocka av dem själv innan du lämnar in.*

---

## Krav för Godkänt (G)

**Klassen `MusicArtist`**
- [ ] Privata fält för: `name`, `genre`, `debutYear`, `country`, `isActive`
- [ ] Alla fält exponeras via publika properties (get — privat set)
- [ ] Konstruktor som tar in och sätter alla värden
- [ ] Metod `Describe()` som skriver ut artistens info i terminalen — snyggt och läsbart
- [ ] Metod `Perform()` som skriver ut ett meddelande, t.ex. `"Taylor Swift spelar live på Gothia Cup Arena!"`

**I `Main()`**
- [ ] Minst **3 artister** skapas med `new MusicArtist(...)`
- [ ] `Describe()` anropas på varje artist
- [ ] `Perform()` anropas på minst en artist

**Reflektion** — lämnas in som `REFLEKTION.md` i ditt repo
- [ ] Varför är fälten privata? Vad händer om de är publika?
- [ ] Vad är skillnaden mellan klassen `MusicArtist` och ett objekt av den klassen?

---

## Krav för Väl Godkänt (VG)

*Alla G-krav måste vara uppfyllda. VG är ett tillägg, inte en ersättning.*

**Klassen `Song`**
- [ ] Privata fält för: `title`, `album`, `releaseYear`, `durationSeconds`
- [ ] Property `DurationFormatted` som returnerar låtlängden som `"3:45"` (minuter:sekunder)
- [ ] Konstruktor
- [ ] Metod `Display()` som skriver ut låtens info

**Utökat `MusicArtist`**
- [ ] En `List<Song>` inuti klassen (privat)
- [ ] Metod `AddSong(Song song)` som lägger till en låt
- [ ] Metod `ShowDiscography()` som listar alla artistens låtar

**I `Main()`**
- [ ] Minst **3 låtar** skapas och läggs till på en artist
- [ ] `ShowDiscography()` anropas och ger läsbar output

**VG-reflektion** *(i samma `REFLEKTION.md`)*
- [ ] Varför har `MusicArtist` en lista av `Song` och inte tvärtom?
- [ ] Vad är `DurationFormatted` för typ av property? Hade du kunnat lösa det utan den?

---

## Tips

Det finns inga startfiler. Du bygger detta från scratch.

Börja med att rita klassen på papper — namn, fält, metoder — innan du öppnar VS Code.  
Om du fastnar i mer än 15 minuter: fråga klassen → AI → Marcus. I den ordningen.

Koda vilt. 🎵

---

## Exempeloutput (G)

```
--- Artistinfo ---
Namn:      Taylor Swift
Genre:     Pop / Country
Debut:     2006
Land:      USA
Aktiv:     Ja

--- Artistinfo ---
Namn:      Robyn
Genre:     Synthpop / Electropop
Debut:     1995
Land:      Sverige
Aktiv:     Ja

Taylor Swift spelar live på Tele2 Arena!
```

## Exempeloutput (VG tillägg)

```
--- Diskografi: Taylor Swift ---
1. Anti-Hero          | Midnights   | 2022 | 3:20
2. Shake It Off       | 1989        | 2014 | 3:39
3. All Too Well (10m) | Red (TV)    | 2021 | 10:13
```

---

## Bedömning

Rättning sker veckan efter deadline.  
Feedback per mail och i Google Classroom.  
Commits efter deadline beaktas inte — spara commit-hashen du lämnar in på.
