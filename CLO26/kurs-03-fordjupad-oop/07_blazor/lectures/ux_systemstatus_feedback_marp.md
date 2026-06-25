---
marp: true
theme: gaia
_class: lead
paginate: true
backgroundColor: #f5f5f5
color: #333

# Varför dör appen?
## Systemstatus, feedback och mänsklig kognition

---

## Tidsgränser på en blick

| Gräns | Tid | Upplevelse |
|-------|-----|------------|
| Omedelbart | < 100 ms | Systemet "tänker" med användaren |
| Flow (Doherty) | < 400 ms | Sömlöst, produktivt samarbete |
| Tolerabelt | < 1 s | Märks men flödet bryts inte |
| Kritiskt | > 10 s | Användaren ger upp mentalt |

---

### Regel 1: Systemets synlighet (Nielsen #1)
> "Användaren ska alltid veta vad som händer — i rimlig tid."

Varje knapptryckning startar en **kognitiv timer** i användarens hjärna. Är skärmen tyst i > 1 s börjar frågorna:

- Kraschade programmet?
- Klickade jag fel?
- Gick anropet fram?

**Lösningen:** Ge alltid omedelbar visuell bekräftelse. En knapp som trycks in, en spinner som startar, text som ändras. Lämna aldrig användaren i tystnad.

---

### Regel 1 — Kodexempel

```csharp
// ❌ Dåligt — tyst knapptryckning
private async void Spara_Click(object sender, EventArgs e)
{
    await SparaDataAsync(); // Användaren ser ingenting
}

// ✅ Bra — omedelbar bekräftelse
private async void Spara_Click(object sender, EventArgs e)
{
    SparaKnapp.Text = "Sparar…";
    SparaKnapp.Enabled = false;

    await SparaDataAsync();

    SparaKnapp.Text = "✓ Sparat!";
    SparaKnapp.Enabled = true;
}
```

---

### Regel 2: 0,1-sekundsgränsen (Omedelbar respons)
> "Under 100 ms upplevs systemet som en direkt förlängning av tanken."

Det finns en neurologisk gräns: under **100 ms** registrerar hjärnan ingen fördröjning alls. Systemet och användaren tänker i takt.

- Knappar ska ge visuell respons (tryck-animation, färgskifte) på < 100 ms
- Hover-effekter, menynavigering — alltid under denna tröskel
- Lokal logik (filtrera en lista, växla en toggle) ska aldrig vara långsammare

> **Tumregel:** Allt som bara rör UI-tillstånd — ingen nätverksanrop, ingen databas — ska ligga under 100 ms. Det är gratis ur ett UX-perspektiv.

---

### Regel 3: Doherty-tröskeln (0,4-sekundersregeln)
> "Produktiviteten ökar dramatiskt när systemet svarar på under 400 ms."

Under 400 ms uppstår ett **flow-tillstånd** — människa och dator jobbar i symbios. Över 400 ms bryts synkroniseringen och fokus börjar läcka.

**Vårt jobb som utvecklare:**
- Cacha ofta efterfrågad data i minnet
- Optimera databasfrågor (index, eager loading)
- Returnera svar tidigt, berika asynkront

---

### Regel 3 — Kodexempel

```csharp
// Minnescache för att hålla Doherty-gränsen
private readonly Dictionary<int, Produkt> _cache = new();

public async Task<Produkt> HämtaProduktAsync(int id)
{
    if (_cache.TryGetValue(id, out var cachadProdukt))
        return cachadProdukt; // ~0 ms — under 100 ms-gränsen!

    var produkt = await _db.Produkter.FindAsync(id);
    _cache[id] = produkt;
    return produkt;
}
```

Enkelt minnescache halverar ofta upplevd svarstid i CRUD-applikationer.

---

### Regel 4: 1-sekundsgränsen (Gränsen för flow)
> "Upp till 1 sekund behåller användaren tråden — men en indikator krävs."

Mellan 400 ms och 1 s märks fördröjningen, men hjärnan klamrar sig kvar vid uppgiften. Utan synlig feedback smyger sig tvivlet in.

**Designkrav:**
- Visa en laddningsindikator direkt — **spinner** eller **skeleton UI**
- Inaktivera knappar som inte ska klickas igen
- Föredra skeleton UI framför en ensam spinner — det ger hjärnan en layout att hålla fast vid

> **Skeleton UI** = grå platshållare i sidans form, fylls i när data anländer. Används av LinkedIn, Facebook, YouTube.

---

### Regel 5: 10-sekundsgränsen (Det totala raset)
> "Efter 10 sekunder har du förlorat användarens uppmärksamhet helt."

En roterande spinner i 10+ sekunder skapar **ångest** — användaren vet inte om det är 1 sekund eller 1 timme kvar. De byter flik, plockar upp telefonen, tror att systemet hängt sig.

**Krav vid tunga operationer:**
- Aldrig bara en spinner — det skapar ångest
- Använd en **progress bar** med text
- Visa vad som händer: *"Bearbetar fil 4 av 17…"*
- Ge en uppskattad återstående tid om möjligt

---

### Regel 5 — Kodexempel

```csharp
public async Task ImporteraFilerAsync(
    List<string> filer,
    IProgress<(int Klar, int Totalt, string AktivFil)> progress)
{
    for (int i = 0; i < filer.Count; i++)
    {
        await BearbetaFilAsync(filer[i]);

        progress.Report((i + 1, filer.Count, filer[i]));
        // UI visar: "Bearbetar fil 4 av 17 — rapport_mars.csv"
    }
}
```

`IProgress<T>` är .NETs inbyggda mekanism för att rapportera asynkron progress till UI-tråden utan race conditions.

---

### Regel 6: Illusionen av framsteg (Kognitiv UX)
> "Väntetid handlar inte om sekunder — det handlar om psykologi."

Två system med exakt samma svarstid upplevs olika om ett av dem visar rörlig framstegsinformation. Hjärnan tolererar väntan om den ser linjära framsteg.

| Mönster | Används när |
|---------|-------------|
| **Spinner** | Kort, odefinierad tid (< 1 s) |
| **Skeleton UI** | Layouten är känd i förväg |
| **Progress bar** | Mätbar progress (%) |
| **Steg-indikator** | Flerstegsprocess (1 av 4) |

> Designa förloppsindikatorn tidigt — det är inte "piff", det är kritisk infrastruktur.

---

### Regel 7: Optimistisk UI (Modernt UX-mönster)
> "Visa resultatet innan servern bekräftat — korrigera tyst vid fel."

Sociala medier gör detta hela tiden: en like-knapp fylls i omedelbart, servern uppdateras i bakgrunden. Appen känns **blixtsnabb** utan att faktiskt vara det.

**När det passar:**
- Reversibla handlingar (gilla, bokmärke, toggle)
- Pålitliga API:er med låg felprocent

**När det inte passar:**
- Betalningar, finansiella transaktioner
- Handlingar som är svåra att återställa

---

### Regel 7 — Kodexempel

```csharp
public async Task GillaInläggAsync(int inläggId)
{
    // 1. Omedelbar visuell bekräftelse — väntar inte på servern
    _gilladAv.Add(_aktuelltAnvändareId);
    StateHasChanged();

    try
    {
        await _api.GillaAsync(inläggId);
    }
    catch
    {
        // 2. Tyst återställning om servern svarar fel
        _gilladAv.Remove(_aktuelltAnvändareId);
        VisaFelmeddelande("Kunde inte spara — försök igen.");
    }
}
```

---

### Regel 8: Felhantering (Nielsen #9)
> "Felmeddelanden ska vara på vanlig svenska, peka ut problemet och föreslå en lösning."

**Tre krav på ett bra felmeddelande:**
1. Förklara *vad* som gick fel (utan teknisk jargong)
2. Förklara *varför* om möjligt
3. Berätta *vad användaren kan göra* åt det

| ❌ Dåligt | ✅ Bra |
|-----------|--------|
| `Error: 503` | "Servern är tillfälligt otillgänglig" |
| `NullReferenceException` | "Fältet Namn får inte vara tomt" |
| "Ett fel uppstod" | "Kontrollera din anslutning och försök igen" |

---

### Regel 8 — Kodexempel

```csharp
// ❌ Dåligt — teknisk och hjälplös
catch (Exception ex)
{
    VisaFel($"Error: {ex.Message}");
}

// ✅ Bra — mänsklig och handlingsbar
catch (HttpRequestException) when (ÄrOffline())
{
    VisaFel(
        rubrik: "Ingen internetanslutning",
        text: "Kontrollera din anslutning och försök igen.",
        knapp: "Försök igen"
    );
}
catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
{
    VisaFel(
        rubrik: "Åtkomst nekad",
        text: "Du saknar behörighet för denna åtgärd.",
        knapp: "Logga in på nytt"
    );
}
```

---

### Regel 9: Mikrointeraktioner (Tyst bekräftelse)
> "Varje handling förtjänar ett svar — även om svaret är litet."

Mikrointeraktioner är de små animationer och tillståndsförändringar som bekräftar att systemet registrerat input. De är osynliga när de fungerar — irriterande när de saknas.

| Handling | Mikrointeraktion |
|----------|-----------------|
| Klicka knapp | Tryck-animation (scale 95 %) |
| Skicka formulär | Knapp → spinner → bockmark |
| Radera rad | Rad tonar ut och krymper |
| Autospara | "Sparat" dyker upp, tonar bort |
| Fel i fält | Fält skakar + röd kantlinje |

> En "shake"-animation på ett felaktigt lösenordsfält kommunicerar *"fel"* utan ett enda ord.

---

## Sammanfattning

| # | Regel | Gräns | Åtgärd |
|---|-------|-------|--------|
| 1 | Systemets synlighet | Alltid | Bekräfta varje handling |
| 2 | Omedelbar respons | < 100 ms | Lokal UI-reaktion |
| 3 | Doherty-tröskeln | < 400 ms | Cache + optimera queries |
| 4 | Flow-gränsen | < 1 s | Spinner / Skeleton UI |
| 5 | Uppmärksamhetsgräns | < 10 s | Progress bar + statustext |
| 6 | Illusion av framsteg | — | Välj rätt laddningsmönster |
| 7 | Optimistisk UI | — | Uppdatera innan svar |
| 8 | Felhantering | — | Mänskliga felmeddelanden |
| 9 | Mikrointeraktioner | — | Bekräfta varje handling |
