---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Blazor

### C# på webben

_Kurs 01 · Vecka 40 · Nion Education_

---

## Windows Forms = skrivbordet

Programmet körs på din dator.
Bara du kan använda det.

## Blazor = webben

Programmet körs i en webbläsare.
Alla kan nå det — med en länk.

---

## Problemet med webbutveckling (tidigare)

```
Frontend → JavaScript
Backend  → C# / Java / Python
```

Två språk. Två kodbaser. Dubbelt att hålla koll på.

---

## Blazors lösning

```
Frontend → C#
Backend  → C#
```

Du skriver **bara C#** — hela vägen.

---

## Projektstrukturen

```
MinBlazorApp/
├── Components/
│   ├── Pages/         ← En .razor-fil per sida
│   ├── Layout/        ← Meny, sidhuvud, sidfot
│   └── App.razor      ← Rot-komponenten
├── wwwroot/           ← CSS, bilder, statiska filer
└── Program.cs         ← Startar webbservern
```

Du arbetar nästan uteslutande i `Components/Pages/`.

---

## En .razor-komponent — uppbyggnad

```razor
@page "/minSida"
@rendermode InteractiveServer

<!-- HTML-delen — vad som visas -->
<h1>Rubrik</h1>
<p>@meddelande</p>

@code {
    // C#-delen — logiken
    private string meddelande = "Hej!";
}
```

HTML överst. C# i `@code`. Det är hela strukturen.

---

## @page och routing

```razor
@page "/produkter"
```

Det här är URL:en till sidan. Navigera dit i webbläsaren med `/produkter`.

```razor
@page "/produkter/{id:int}"
```

Du kan ta emot parametrar från URL:en:

```razor
@code {
    [Parameter]
    public int Id { get; set; }
}
```

---

## @rendermode — varför det behövs

```razor
@rendermode InteractiveServer
```

Utan det här: sidan renderas en gång på servern, sedan händer ingenting.
Med det här: Blazor håller en live-anslutning och uppdaterar sidan dynamiskt.

**Alltid med när du vill att knappar och inmatning ska fungera.**

---

## Vanliga HTML-element (1/2) — text och inmatning

| Element | Vad |
|---------|-----|
| `<h1>` – `<h3>` | Rubriker |
| `<p>` | Stycke |
| `<input>` | Textfält, kryssruta, radioknapp |
| `<textarea>` | Stort textfält, flera rader |

Samma element som på alla webbsidor — men du styr dem med C#.

---

## Vanliga HTML-element (2/2) — interaktion och layout

| Element | Vad |
|---------|-----|
| `<button>` | Klickbar knapp |
| `<select>` | Rullgardinsmeny |
| `<ul>` + `<li>` | Punktlista |
| `<table>` | Tabell med rader och kolumner |
| `<div>` | Osynlig container för layout |

---

## @bind — tvåvägsbindning

`@bind` kopplar ett HTML-element till en C#-variabel.

```razor
<input @bind="namn" />
<p>Hej @namn!</p>

@code {
    private string namn = "";
}
```

- Skriver du i fältet → variabeln uppdateras
- Variabeln ändras i koden → fältet uppdateras

Uppdateras vid `onchange` (när du klickar ur fältet). Vill du ha live-uppdatering:

```razor
<input @bind:event="oninput" @bind="sökord" />
```

---

## @onclick — klick kör en metod

```razor
<button @onclick="SägHej">Klicka</button>
<p>@meddelande</p>

@code {
    private string meddelande = "";

    private void SägHej()
    {
        meddelande = "Hej från Blazor!";
    }
}
```

---

## Vanliga events

| Event | Element | När |
|-------|---------|-----|
| `@onclick` | button, div, span | Klick |
| `@oninput` | input, textarea | Varje tangenttryckning |
| `@onchange` | input, select | Fältet lämnas |
| `@onsubmit` | form | Formulär skickas |
| `@onkeydown` | input | Tangent trycks |
| `@onmouseover` | Valfritt | Musen går över elementet |

---

## @oninput — live-uppdatering

```razor
<input @oninput="Filtrera" placeholder="Sök..." />
<p>Du söker: @sökord</p>

@code {
    private string sökord = "";

    private void Filtrera(ChangeEventArgs e)
    {
        sökord = e.Value?.ToString() ?? "";
    }
}
```

`@oninput` triggar vid **varje tangenttryckning** — till skillnad från `@onchange` som väntar tills du klickar ur fältet.

---

## @if — villkorlig rendering

```razor
@if (inloggad)
{
    <p>Välkommen!</p>
}
else
{
    <p>Logga in för att fortsätta.</p>
}

@code {
    private bool inloggad = false;
}
```

---

## @foreach — visa en lista

```razor
<ul>
    @foreach (var sak in saker)
    {
        <li>@sak</li>
    }
</ul>

@code {
    private List<string> saker = new() { "Pizza", "Tacos", "Sushi" };
}
```

Samma `List<T>` du kan — visas nu i webbläsaren.

---

## select med @bind

```razor
<select @bind="valtLand">
    <option value="">Välj land</option>
    <option value="SE">Sverige</option>
    <option value="NO">Norge</option>
    <option value="DK">Danmark</option>
</select>
<p>Du valde: @valtLand</p>

@code {
    private string valtLand = "";
}
```

---

## Komponentens livscykel

```razor
@code {
    protected override void OnInitialized()
    {
        // Körs när komponenten skapas — ladda data här
        saker = HämtaFrånDatabas();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            // Körs efter att HTML:en renderades för första gången
        }
    }
}
```

Samma tanke som `Form_Load` i WinForms.

---

## Parametrar — skicka data till en komponent

```razor
<!-- Föräldrakomponent -->
<MittKort Titel="Hej" Färg="blue" />

<!-- MittKort.razor -->
<div style="color: @Färg">
    <h2>@Titel</h2>
</div>

@code {
    [Parameter] public string Titel { get; set; } = "";
    [Parameter] public string Färg { get; set; } = "black";
}
```

---

## Styling

CSS skrivs i `wwwroot/app.css` — gäller hela appen.

Varje komponent kan ha sin **egen** CSS-fil: `MittKort.razor.css` — gäller bara den komponenten.

```css
/* MittKort.razor.css */
div {
    border: 1px solid #ccc;
    padding: 1rem;
    border-radius: 8px;
}
```

---

<!-- _class: title -->

## Idag

1. Marcus bygger en **reaktiv lista** live i Blazor
2. Ni bygger egna webbappar

Googla friskt — det gör alla Blazor-utvecklare.
