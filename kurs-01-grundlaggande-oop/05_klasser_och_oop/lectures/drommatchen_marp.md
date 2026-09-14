---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Drömmatchen ⚽

### Inlämning 1 — Klasser, properties och konstruktorer

_Kurs 01 · Vecka 3–4 · Nion Education_

---

## Det här är inte vilken uppgift som helst

Inga kontrakt. Inga transferfönster.

Du är manager — och du sätter ihop din **absoluta drömuppställning**.

Pelé och Haaland i samma anfallspar?  
Maradona på tio? Din lokala idol på kanten?

**Det enda som saknas är koden.**

Och den skriver du nu. 🎉

---

## Vad ska vi bygga?

Två klasser som samarbetar:

```
Spelare                    Match
───────────────────        ─────────────────────────────
_namn                      _hemmalag
_nummer                    _bortalag
_position                  _datum
                           
Namn  { get; private set } Hemmalag  { get; private set }
Nummer                     Bortalag
Position                   Datum

Spelare(namn, nr, pos)     Match(hemma, borta, datum)
                           Presentera()
                           AnnounceraMålskytt(Spelare s)
```

---

## Så här kommer du igång

**Forka startrepot — det innehåller scaffoldade klasser och mallar.**

```
github.com/marcusjobb/clo26-drommatchen
```

1. Öppna länken — klicka **Fork** uppe till höger
2. Välj ditt eget konto som destination
3. Klona ditt forkade repo till din dator
4. Öppna i Rider eller VS Code — kör igång! 🚀

> Repot blir publikt efter genomgången på lektionen.

---

## Privata fält + property — varför?

Utan skydd kan vem som helst ändra spelarens namn:

```csharp
spelare1._namn = "Fejknamn";   // ← ingen kontroll
```

Med privat fält och property bestämmer **du** reglerna:

```csharp
private string _namn;          // bara klassen når hit

public string Namn             // utomstående kan bara läsa
{
    get { return _namn; }
    private set { _namn = value; }
}

spelare1.Namn = "Fejknamn";    // ← kompilatorfel — skyddat!
```

---

## Kortformen — samma sak, färre rader

Den långa formen och kortformen gör exakt samma sak.

```csharp
// Lång form — tydlig och explicit
public string Namn
{
    get { return _namn; }
    private set { _namn = value; }
}

// Kortform — C# fyller i resten åt dig
public string Namn { get; private set; }
//                   ───  ────────────
//                    │        └─ bara klassen får skriva
//                    └─ vem som helst får läsa
```

I startrepot ser du kommentarer där `get` och `private set` ska in — **det är din uppgift att fylla i dem.**

---

## Konstruktorn — klassen får liv

Konstruktorn kör en gång, precis när objektet skapas. Den sätter startvärden.

```csharp
public Spelare(string namn, int nummer, string position)
{
    _namn     = namn;
    _nummer   = nummer;
    _position = position;
}
```

```csharp
// Skapar ett objekt och anropar konstruktorn direkt
Spelare spelare1 = new Spelare("Zlatan Ibrahimović", 10, "Forward");
Spelare spelare2 = new Spelare("Thierry Henry",       7, "Forward");
```

> 💬 _"Utan konstruktorn — inget objekt med riktiga värden."_

---

## Klasser som samarbetar

`Match` tar emot ett `Spelare`-objekt som parameter — det är det som gör det häftigt.

```csharp
public void AnnounceraMålskytt(Spelare spelare)
{
    Console.WriteLine($"MÅÅÅL! #{spelare.Nummer} {spelare.Namn} ({spelare.Position})");
}
```

```csharp
Match match = new Match("Drömlagen FC", "Världselvan", "2026-09-14");

match.Presentera();
match.AnnounceraMålskytt(spelare1);
match.AnnounceraMålskytt(spelare2);
```

```plaintext
Drömlagen FC vs Världselvan — 2026-09-14

MÅÅÅL! #10 Zlatan Ibrahimović (Forward)
MÅÅÅL! #7 Thierry Henry (Forward)
```

---

## VG — matchen avgörs

Lägg till ett fält `_mål` på `Spelare` och en metod på `Match`:

```csharp
public bool ÄrMatchhjälte(Spelare spelare)
{
    return spelare.Mål >= 1;
}
```

```csharp
Console.WriteLine("ÄrMatchhjälte — Zlatan: " + match.ÄrMatchhjälte(spelare1));
Console.WriteLine("ÄrMatchhjälte — Henry: "  + match.ÄrMatchhjälte(spelare2));
```

```plaintext
ÄrMatchhjälte — Zlatan: True
ÄrMatchhjälte — Henry: False
```

> 💬 _"Varför returnerar metoden bool istället för att skriva ut direkt? Det är en VG-fråga i rapporten."_

---

## Kraven i korthet

| | G | VG |
|---|---|---|
| Klasser | `Spelare`, `Match` | + `_mål`, `Mål` på `Spelare` |
| Metoder | `Presentera()`, `AnnounceraMålskytt()` | + `ÄrMatchhjälte()` |
| Main | 2 spelare, 1 match, anropa metoderna | + anropa `ÄrMatchhjälte()` |
| Git | `.gitignore`, minst 5 commits | Rapport med VG-svar |
| Dokument | `RAPPORT.md`, `REFLEKTION.md` | VG-avsnittet ifyllt |

**Deadline:** Söndag 21 sep 2026, 23:59  
**Förlängd:** Fredag 25 sep 2026, 23:59

---

## `.gitignore` — ett hårt krav ⚠️

Saknas `.gitignore` som filtrerar bort `bin/`, `obj/` och `.vs/` →  **IG**.

Startrepot har redan en — men kolla att den finns och är rätt.

```gitignore
bin/
obj/
.vs/
```

Commits efter deadline beaktas inte — spara commit-hashen du lämnar in på.

---

<!-- _class: title -->

# Nu kör vi! ⚽🔥

### Forka repot — sätt ihop din drömuppställning

`github.com/marcusjobb/clo26-drommatchen`

_Lycka till. Arenans ljus är på._
