# Övning — Server-side validering: HyresBil

🟡 Mellannivå

---

## Bakgrunden

Du jobbar med **HyresBil** — ett bokningssystem för en biluthyrningskedja. Förra veckan byggde du formuläret och lade till JavaScript-validering i webbläsaren. Chefen är nöjd.

Men sedan händer det. En testare öppnar DevTools, stänger av JavaScript och skickar in ett tomt formulär. Bokning klar. Null-fel i databasen. Applikationen kraschar.

Lärdom: **lita aldrig på klienten.**

JavaScript-validering är bra UX — inte säkerhet. Allt som skickas från webbläsaren kan manipuleras. Det enda som verkligen skyddar din applikation är validering på servern, där du kontrollerar allt.

Den här övningen handlar om att göra det rätt.

---

## Vad gäller för den här övningen

- [ ] Du kan dekorera en modell med data annotations: `[Required]`, `[StringLength]`, `[Range]`, `[EmailAddress]`, `[RegularExpression]`
- [ ] Du kan använda `ModelState.IsValid` i en controller för att stoppa ogiltig data
- [ ] Du förstår varför client-side validering inte räcker
- [ ] Du kan skriva ett eget valideringsattribut (`CustomValidationAttribute`)
- [ ] Du kan visa felmeddelanden i vyn med `asp-validation-for` och `asp-validation-summary`

---

## Förutsättningar

Du behöver ett befintligt ASP.NET Core MVC-projekt med ett bokningsformulär. Om du inte har ett sedan tidigare — skapa ett nytt:

```bash
dotnet new mvc -n HyresBil
cd HyresBil
dotnet run
```

---

## Del 1 — Modellen med data annotations

Skapa filen `Models/Bokning.cs`. Den här klassen representerar ett bokningsformulär — och varje egenskap är dekorerad med regler som körs **på servern**.

```csharp
using System.ComponentModel.DataAnnotations;

namespace HyresBil.Models;

public class Bokning
{
    [Required(ErrorMessage = "Namn är obligatoriskt.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Namn måste vara mellan 2 och 50 tecken.")]
    public string? Namn { get; set; }

    [Required(ErrorMessage = "E-postadress är obligatorisk.")]
    [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
    [RegularExpression(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "E-postadressen saknar toppdomän (t.ex. .se eller .com).")]
    public string? Epost { get; set; }

    [Required(ErrorMessage = "Välj ett startdatum.")]
    [DataType(DataType.Date)]
    public DateTime? StartDatum { get; set; }

    [Required(ErrorMessage = "Antal dagar är obligatoriskt.")]
    [Range(1, 30, ErrorMessage = "Antal dagar måste vara mellan 1 och 30.")]
    public int AntalDagar { get; set; }

    [Required(ErrorMessage = "Biltyp är obligatorisk.")]
    [StringLength(30, ErrorMessage = "Biltyp får inte överstiga 30 tecken.")]
    public string? Biltyp { get; set; }
}
```

**Vad händer här?**

| Annotation | Vad den gör |
|---|---|
| `[Required]` | Fältet får inte vara tomt — körs på servern |
| `[StringLength]` | Sätter max- och minlängd på textsträngar |
| `[Range]` | Kontrollerar att ett tal ligger inom ett intervall |
| `[EmailAddress]` | Grundläggande e-postformatkoll |
| `[RegularExpression]` | Strängare kontroll med regex — t.ex. att toppdomänen faktiskt finns |

---

## Del 2 — Controllern med ModelState.IsValid

Skapa filen `Controllers/BokningController.cs`. Observera att `ModelState.IsValid` är vaktposten — om modellen inte uppfyller kraven stoppas allt och formuläret visas igen med felmeddelandena.

```csharp
using HyresBil.Models;
using Microsoft.AspNetCore.Mvc;

namespace HyresBil.Controllers;

public class BokningController : Controller
{
    [HttpGet]
    public IActionResult Skapa()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Skapa(Bokning bokning)
    {
        // Servern kontrollerar alla data annotations här
        if (!ModelState.IsValid)
        {
            // Skicka tillbaka formuläret med felmeddelandena — klienten luras inte
            return View(bokning);
        }

        // Här vet vi att datan är giltig
        TempData["Bekraftelse"] = $"Tack {bokning.Namn}! Din {bokning.Biltyp} är bokad från {bokning.StartDatum:d} i {bokning.AntalDagar} dagar.";

        return RedirectToAction(nameof(Bekraftelse));
    }

    [HttpGet]
    public IActionResult Bekraftelse()
    {
        return View();
    }
}
```

**Nyckelpoängen:** `ModelState.IsValid` körs *efter* att ASP.NET Core har försökt binda formulärdata till `Bokning`-objektet. Är något fel — datum saknas, email ogiltig, antalDagar utanför intervallet — är `IsValid` false och vi returnerar vyn med modellen ifylld så att studerande ser vad som gick fel.

---

## Del 3 — Kringgå client-side validering med DevTools

Innan du skriver ett enda test — gör det här manuellt. Det är viktigt att du ser det med egna ögon.

**Steg:**

1. Starta appen: `dotnet run`
2. Gå till `https://localhost:xxxx/Bokning/Skapa`
3. Öppna DevTools i webbläsaren (`F12`)
4. Gå till fliken **Console** och skriv:

```javascript
document.querySelectorAll('form').forEach(f => f.noValidate = true)
```

5. Eller gå till **Network** → inaktivera JavaScript helt via **Settings → Debugger → Disable JavaScript**
6. Skicka in ett tomt formulär

Utan server-side validering: formuläret går igenom. Med din controller ovan: formuläret stoppas och felmeddelanden visas.

**Det är skillnaden.** JavaScript-valideringen försvinner med en knapptryckning. Server-side validering kan aldrig stängas av från klienten.

---

## Del 4 — Custom validation attribute: StartDatum måste vara i framtiden

Data annotations täcker det vanligaste — men ibland behöver du affärslogik som inte finns inbyggt. Då skriver du ett eget attribut.

Skapa filen `Validation/FutureDateAttribute.cs`:

```csharp
using System.ComponentModel.DataAnnotations;

namespace HyresBil.Validation;

// Ärver från ValidationAttribute — det är grunden för alla valideringsattribut
public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // Kontrollera att vi faktiskt fått ett datum
        if (value is DateTime datum)
        {
            // Startdatum måste vara imorgon eller senare
            if (datum.Date <= DateTime.Today)
            {
                return new ValidationResult("Startdatumet måste vara i framtiden.");
            }
        }

        // null hanteras av [Required] — vi behöver inte upprepa det här
        return ValidationResult.Success;
    }
}
```

Lägg sedan till attributet på `StartDatum` i din modell:

```csharp
using HyresBil.Validation;

// I Bokning.cs — uppdatera StartDatum:
[Required(ErrorMessage = "Välj ett startdatum.")]
[DataType(DataType.Date)]
[FutureDate]
public DateTime? StartDatum { get; set; }
```

Nu kontrollerar servern att ingen bokar en bil för igår — oavsett vad klienten skickar.

---

## Del 5 — Vyn med asp-validation-for och asp-validation-summary

Skapa filen `Views/Bokning/Skapa.cshtml`. Tag helpers som `asp-validation-for` och `asp-validation-summary` plockar upp felmeddelanden från `ModelState` och visar dem direkt i formuläret.

```html
@model HyresBil.Models.Bokning

@{
    ViewData["Title"] = "Boka bil";
}

<h2>Boka bil</h2>

@* Sammanfattning av alla fel — visas om något är ogiltigt *@
<div asp-validation-summary="ModelOnly" class="text-danger"></div>

<form asp-action="Skapa" method="post">

    <div class="mb-3">
        <label asp-for="Namn" class="form-label"></label>
        <input asp-for="Namn" class="form-control" />
        @* Felmeddelandena för just det här fältet *@
        <span asp-validation-for="Namn" class="text-danger"></span>
    </div>

    <div class="mb-3">
        <label asp-for="Epost" class="form-label"></label>
        <input asp-for="Epost" class="form-control" type="email" />
        <span asp-validation-for="Epost" class="text-danger"></span>
    </div>

    <div class="mb-3">
        <label asp-for="StartDatum" class="form-label"></label>
        <input asp-for="StartDatum" class="form-control" type="date" />
        <span asp-validation-for="StartDatum" class="text-danger"></span>
    </div>

    <div class="mb-3">
        <label asp-for="AntalDagar" class="form-label"></label>
        <input asp-for="AntalDagar" class="form-control" type="number" min="1" max="30" />
        <span asp-validation-for="AntalDagar" class="text-danger"></span>
    </div>

    <div class="mb-3">
        <label asp-for="Biltyp" class="form-label"></label>
        <input asp-for="Biltyp" class="form-control" />
        <span asp-validation-for="Biltyp" class="text-danger"></span>
    </div>

    <button type="submit" class="btn btn-primary">Boka nu</button>

</form>

@section Scripts {
    @* Aktiverar client-side validering som komplement — men servern är alltid sista ordet *@
    <partial name="_ValidationScriptsPartial" />
}
```

Skapa också bekräftelsevyn `Views/Bokning/Bekraftelse.cshtml`:

```html
@{
    ViewData["Title"] = "Bokningsbekräftelse";
}

<h2>Bokning bekräftad!</h2>

@if (TempData["Bekraftelse"] != null)
{
    <div class="alert alert-success">
        @TempData["Bekraftelse"]
    </div>
}

<a asp-action="Skapa" class="btn btn-outline-primary">Gör en ny bokning</a>
```

---

## Testa din validering

Starta appen och testa systematiskt. Varje rad nedan ska ge ett felmeddelande från servern:

| Vad du skickar | Förväntat felmeddelande |
|---|---|
| Tomt formulär | Alla fält markeras som obligatoriska |
| Namn: "A" (ett tecken) | "Namn måste vara mellan 2 och 50 tecken" |
| E-post: `test@test` (utan toppdomän) | "E-postadressen saknar toppdomän" |
| Antal dagar: `0` | "Antal dagar måste vara mellan 1 och 30" |
| Antal dagar: `31` | "Antal dagar måste vara mellan 1 och 30" |
| StartDatum: gårdagens datum | "Startdatumet måste vara i framtiden" |

Gå sedan in i DevTools och kringgå JavaScript-valideringen (se Del 3). Servern ska fortfarande stoppa ogiltig data.

---

## Tips

> Glöm inte `using`-direktiven i modellen. Både `System.ComponentModel.DataAnnotations` och din egna namespace för `FutureDateAttribute` måste importeras.

> `asp-validation-summary="ModelOnly"` visar fel som inte är kopplade till ett specifikt fält — t.ex. generiska felmeddelanden du lägger till med `ModelState.AddModelError("", "...")`. `"All"` visar allt, inklusive fältspecifika fel en gång till.

> Custom validation-attribut är kraftfulla för affärslogik: kontrollera att ett slutdatum är efter startdatumet, att ett postnummer matchar ett mönster, att en rabattkod faktiskt finns i databasen.

> ⏱️ **15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, en AI eller läraren. Kämpa inte ensam längre än så.

---

*Facit finns hos läraren.*
