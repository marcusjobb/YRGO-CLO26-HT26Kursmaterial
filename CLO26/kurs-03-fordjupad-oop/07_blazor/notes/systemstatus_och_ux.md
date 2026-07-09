# Systemstatus, Feedback och UX i Blazor

## Vad är UX och varför spelar det roll?

UX (User Experience) handlar om hur en användare upplever din applikation. Bra UX gör att användaren känner sig trygg, produktiv och i kontroll. Dålig UX skapar frustration, misstag och i värsta fall — att användaren slutar använda din app.

I Blazor handlar UX mycket om hur du hanterar tillståndsändringar, laddning och fel.

## Response Time Guidelines (Jakob Nielsen)

### < 100 ms — Omedelbar respons
Systemet upplevs som en direkt förlängning av tanken. Inga indikatorer behövs.

**Blazor:**
```csharp
@code {
    private bool isDarkMode;
    
    private void ToggleDarkMode()
    {
        isDarkMode = !isDarkMode;  // < 1 ms — omedelbar UI-ändring
    }
}
```

### 100–400 ms — Doherty Threshold
Upplevs som flytande. Synkroniseringen mellan människa och dator är intakt.

### 400 ms – 1 s — Flow-gränsen
Märks men bryter inte fokus. Använd spinner/skeleton UI.

**Blazor-skeleton:**
```csharp
@if (produkter is null)
{
    <div class="skeleton">
        <div class="skeleton-line" style="width: 80%"></div>
        <div class="skeleton-line" style="width: 60%"></div>
    </div>
}
else
{
    <ProductList Products="produkter" />
}
```

### > 10 s — Uppmärksamhetsgränsen
Användaren ger upp. Använd progress bar med text.

## Optimistisk UI i Blazor

Uppdatera UI:t omedelbart, vänta inte på servern:

```csharp
private async Task GillaInlägg(int id)
{
    inlägg.Gillat = true;              // Omedelbart
    inlägg.GillaCount++;
    StateHasChanged();

    try
    {
        await Api.GillaAsync(id);       // Bakgrundsanrop
    }
    catch
    {
        inlägg.Gillat = false;          // Återställ vid fel
        inlägg.GillaCount--;
        felMeddelande = "Kunde inte spara ditt gilla.";
    }
}
```

## Felhantering som användaren förstår

| Dåligt | Bra |
|--------|-----|
| "Error: 500" | "Servern svarar inte just nu. Försök igen om en stund." |
| "NullReferenceException" | "Fältet 'Namn' måste fyllas i." |
| "Ett fel uppstod" | "Kontrollera din internetanslutning." |

## Mikrointeraktioner

Små animationer som bekräftar handlingar:

- Knapptryck → skala till 95% direkt
- Sparat → grön bock som tonar bort
- Felaktigt fält → skaka + röd kant

## Viktigaste lärdomarna

- Ge alltid feedback på användarens handlingar — inom 100 ms om möjligt
- Använd rätt laddningsindikator för rätt situation
- Optimistisk UI = snabb känsla, men hantera fel återställning
- Felmeddelanden ska vara mänskliga, specifika och handlingsbara
- Mikrointeraktioner är kritisk infrastruktur, inte "piff"
