# UX, Systemstatus och Feedback — Övningar

## Övning 1: Analysera en app

Välj en app du använder dagligen (t.ex. Instagram, BankID, en webbshop).

**Svara på dessa frågor:**
1. Hur lång tid tar det innan du får feedback när du trycker på en knapp?
2. Använder appen skeleton UI, spinner eller progress bar?
3. Vad händer om internet försvinner — hur hanteras felet?
4. Använder appen optimistisk UI? (uppdateras innan servern svarat)
5. Hur ser felmeddelanden ut? Tekniska eller mänskliga?

---

## Övning 2: Bygg en Blazor-komponent

Skapa en `LaddaKnapp`-komponent i Blazor som:

```razor
<LaddaKnapp OnClick="SparaData" Laddningstext="Sparar...">
    Spara
</LaddaKnapp>
```

**Komponenten ska:**
1. Visas som en vanlig knapp från början
2. Vid klick: bli disabled + visa spinner + visa `Laddningstext`
3. När `OnClick` är klar: visa en grön bock i 2 sekunder, återgå sedan
4. Om `OnClick` kastar ett fel: visa rött felmeddelande under knappen

**Tips:** Använd `EventCallback`, `Disabled`, och `StateHasChanged()`.

---

## Övning 3: Skapa skeleton UI

Bygg en `ProductCardSkeleton`-komponent som visar grå platshållare medan data laddas:

```razor
@if (produkt is null)
{
    <ProductCardSkeleton />
}
else
{
    <ProductCard Produkt="produkt" />
}
```

**Designa skeleton:**
- Rektangel för bild (högst upp)
- 2-3 linjer för titel och pris
- Rund cirkel för avatar/profilbild
- Använd CSS-animation (pulsering eller shimmer)

---

## Övning 4: Felhantering — före och efter

Nedan är en metod med dålig felhantering. Förbättra den:

```csharp
// FÖRE — Dålig felhantering
private async Task HamtaOrderData()
{
    try
    {
        order = await api.GetOrderAsync(orderId);
    }
    catch (Exception ex)
    {
        felMeddelande = ex.Message;
    }
}
```

**Skriv om den så att den:**
- Hanterar olika feltyper separat (network, auth, server error)
- Visar ett mänskligt, handlingsbart felmeddelande
- Ger användaren möjlighet att försöka igen
- Loggar felet för utvecklaren

---

## Övning 5: Respons-tidsanalys

Mät och dokumentera responsitder för olika operationer i en app:

| Operation | Uppmätt tid | Gräns (< 100 ms / < 400 ms / < 1 s / > 1 s) | Åtgärd |
|-----------|-------------|------------------------------------------------|--------|
| Klicka toggle-knapp | | < 100 ms | Ingen |
| Spara formulär | | | |
| Hämta lista | | | |
| Sökning | | | |

1. Identifiera minst en operation som överskrider rekommenderad gräns
2. Föreslå en förbättring (cache, optimistisk UI, skeleton)

---

## Övning 6: Reflektion

1. Varför spelar 100 ms-gränsen roll? Vad händer neurologiskt?
2. När är optimistisk UI olämpligt?
3. Varför är "Ett fel uppstod" ett dåligt felmeddelande?
4. Vad är skillnaden mellan en spinner och en progress bar — när använder du vad?
