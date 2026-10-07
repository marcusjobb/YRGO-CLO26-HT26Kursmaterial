---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Windows Forms

### C# med riktiga fönster

_Kurs 01 · Vecka 40 · Nion Education_

---

## Hela kursen har vi skrivit detta

```csharp
Console.WriteLine("Hej!");
string svar = Console.ReadLine();
```

Det är bra för att lära sig C#.

Men ingen på ett riktigt jobb skriver konsolprogram.

---

## C# kan bygga mycket mer

| Vad | Teknik |
|-----|--------|
| Skrivbordsprogram | **Windows Forms / WPF** |
| Webbappar | **Blazor / ASP.NET** |
| Mobilappar | **.NET MAUI** |
| Spel | **Unity (C#)** |
| API:er | **ASP.NET Web API** |

Samma C# — helt olika program.

---

## Vad är Windows Forms?

- Microsofts ramverk för skrivbordsprogram sedan 2002
- Ingår i .NET — fungerar med exakt samma C# du kan
- Används i industri, vård, banker — fortfarande idag

---

## Projektstrukturen

```
MittProgram/
├── Form1.cs           ← Din kod — den du redigerar
├── Form1.Designer.cs  ← Genererad kod från designern — rör ALDRIG
└── Program.cs         ← Startar appen
```

`Form1.Designer.cs` skriver Visual Studio automatiskt när du drar ut kontroller i designern. Om du redigerar den manuellt kan appen sluta fungera.

---

## Designern

Öppna designern med **Shift+F7** eller dubbelklick på `Form1.cs`.

- Dra komponenter från **Toolbox** till formuläret
- Placera, ändra storlek, ändra färg — visuellt
- VS genererar koden åt dig

Nästan allt du gör i designern syns som en property i **Properties-panelen** (F4).

---

## Visuella objekt vs kodobjekt

**Visuella objekt** — det du ser i fönstret:
`Label`, `Button`, `TextBox`, `PictureBox`, `ListBox`

**Kodobjekt** — finns bara i koden, syns inte direkt:
`Timer`, `ImageList`, `ContextMenuStrip`, `ToolTip`

Båda sätts upp i designern, men kodobjekten har ingen synlig representation på formuläret.

---

## Kontrollernas hierarki

```
Form (fönstret)
└── Panel (grupperar kontroller)
    ├── Label
    ├── TextBox
    └── Button
```

En `Form` är en **container**. Du kan lägga kontroller direkt på Form, eller i `Panel`/`GroupBox` för att hålla ordning.

Varje kontroll är en klass — en `Button` är ett objekt av typen `Button`.

---

## Vanliga kontroller — visa och mata in text

| Kontroll | Vad | Viktig property |
|----------|-----|-----------------|
| `Label` | Visar text — kan inte redigeras | `Text` |
| `TextBox` | Textfält för inmatning | `Text`, `Multiline` |
| `RichTextBox` | Textfält med formatering | `Text`, `Rtf` |
| `MaskedTextBox` | Inmatning med format (t.ex. datum) | `Mask` |

```csharp
lblStatus.Text = "Sparat!";
string namn = txtNamn.Text;
```

---

## Vanliga kontroller — knappar och val

| Kontroll | Vad | Viktig property |
|----------|-----|-----------------|
| `Button` | Klickbar knapp | `Text`, `Enabled` |
| `CheckBox` | Kryssruta (ja/nej) | `Checked` |
| `RadioButton` | Välj ett av flera | `Checked` |
| `ComboBox` | Rullgardinsmeny | `Items`, `SelectedItem` |
| `ListBox` | Lista med val | `Items`, `SelectedItem` |

```csharp
if (chkGodkänn.Checked) { ... }
string valt = cboLand.SelectedItem.ToString();
```

---

## Vanliga kontroller — layout och bilder

| Kontroll | Vad |
|----------|-----|
| `Panel` | Osynlig container som grupperar kontroller |
| `GroupBox` | Container med synlig ram och titel |
| `PictureBox` | Visar en bild |
| `ProgressBar` | Visar en progress |
| `TabControl` | Flikar |

---

## Gemensamma properties (1/2)

Alla kontroller delar dessa:

| Property | Vad |
|----------|-----|
| `Name` | Kontrollens variabelnamn i koden |
| `Text` | Texten som visas |
| `Enabled` | `true`/`false` — aktiv eller nedtonad |
| `Visible` | `true`/`false` — synlig eller dold |

---

## Gemensamma properties (2/2)

| Property | Vad |
|----------|-----|
| `Size` | Bredd och höjd |
| `Location` | Position (X, Y) på formuläret |
| `BackColor` / `ForeColor` | Bakgrunds- och textfärg |
| `Font` | Teckensnitt och storlek |

---

## Dock — fyll ut utrymmet

`Dock` bestämmer hur en kontroll fyller sin container.

| Värde | Vad |
|-------|-----|
| `None` | Fast position (standard) |
| `Top` | Fyller kontainerns överkant |
| `Bottom` | Fyller underkanten |
| `Left` | Fyller vänsterkanten |
| `Right` | Fyller högerkanten |
| `Fill` | Fyller hela utrymmet |

---

## Dock — i praktiken

```csharp
panelMeny.Dock = DockStyle.Left;
dataGridView1.Dock = DockStyle.Fill;
```

Vanligt mönster: meny till vänster (`Left`) + innehåll fyller resten (`Fill`).

Sätts enkelt i Properties-panelen — välj värde i rullgardinsmenyn under **Dock**.

---

## Anchor — följ med när fönstret ändrar storlek

`Anchor` bestämmer vilka kanter en kontroll är "förankrad" vid.

- Förankrad `Top + Left` (standard) — stannar i övre vänstra hörnet
- Förankrad `Top + Right` — följer med höger kant
- Förankrad `Bottom + Left` — följer med underkanten
- Förankrad `Top + Bottom + Left + Right` — **sträcker sig** när fönstret ändrar storlek

Sätts i Properties-panelen under **Anchor** — klicka på pilarna.

---

## Events — kod som svar på händelser

I konsolen körs koden uppifrån och ned.
I WinForms körs kod som svar på **händelser**.

```csharp
// Körs när knappen klickas
private void btnSpara_Click(object sender, EventArgs e)
{
    lblStatus.Text = "Sparat!";
}
```

Du skapar ett event-handler genom att dubbelklicka på kontrollen i designern.

---

## Form-events — formulärets livscykel

| Event | När körs det? |
|-------|---------------|
| `Load` | När formuläret öppnas — bra för att ladda data |
| `Shown` | Efter att formuläret visas för första gången |
| `Resize` | Varje gång fönstret ändrar storlek |
| `FormClosing` | Precis innan formuläret stängs — bra för att spara |
| `FormClosed` | Efter att formuläret stängts |

```csharp
private void Form1_Load(object sender, EventArgs e)
{
    // Körs när appen startar — ladda lista, sätt standardvärden
    cboLand.Items.AddRange(new[] { "Sverige", "Norge", "Danmark" });
}
```

---

## Kontroll-events — vanligast

| Event | Kontroll | När |
|-------|----------|-----|
| `Click` | Button, Label, PictureBox | Klick |
| `TextChanged` | TextBox | Varje gång texten ändras |
| `CheckedChanged` | CheckBox, RadioButton | Kryssrutan ändras |
| `SelectedIndexChanged` | ComboBox, ListBox | Annat val görs |
| `KeyDown` / `KeyUp` | TextBox, Form | Tangent trycks/släpps |
| `MouseEnter` / `MouseLeave` | Alla | Musen rör sig in/ut |

---

## Kontroll-events — i praktiken

```csharp
private void txtSök_TextChanged(object sender, EventArgs e)
{
    // Filtrera listan direkt medan användaren skriver
    string sök = txtSök.Text.ToLower();
    lstResultat.Items.Clear();
    foreach (var namn in alleNamn)
        if (namn.ToLower().Contains(sök))
            lstResultat.Items.Add(namn);
}
```

---

## OOP i ett fönster — klassen

Din Djur-klass fungerar exakt likadant — bara ett nytt fönster.

```csharp
public class Djur
{
    public string Namn { get; set; }
    public string Ljud { get; set; }
    public string GörLjud() => $"{Namn} säger: {Ljud}!";
}
```

---

## OOP i ett fönster — formuläret

```csharp
private Dictionary<string, Djur> djur;

private void Form1_Load(object sender, EventArgs e)
{
    djur = new Dictionary<string, Djur>
    {
        { "Hund", new Djur { Namn = "Hund", Ljud = "Voff" } },
        { "Katt", new Djur { Namn = "Katt", Ljud = "Mjau" } }
    };
}

private void btnHund_Click(object sender, EventArgs e)
{
    lblLjud.Text = djur["Hund"].GörLjud();
    this.BackColor = Color.LightYellow;
}
```

---

<!-- _class: title -->

## Idag

1. Marcus bygger **Djurljudsmaskinen** live
2. Ni bygger egna appar

Ni kan redan C# som behövs. Det här är bara ett nytt fönster.
