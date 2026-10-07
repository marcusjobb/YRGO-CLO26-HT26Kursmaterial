# Windows Forms — Läsartikel

> Ingår inte i examinationen. Läs detta om du vill förstå mer om vad vi gjorde idag.

---

## Vad är Windows Forms?

Windows Forms (WinForms) är Microsofts ramverk för att bygga skrivbordsprogram med grafiskt gränssnitt. Det har funnits sedan 2002 och ingår i .NET — samma plattform du redan använder.

Du har hela kursen skrivit program som körs i terminalen. WinForms låter dig istället bygga program med knappar, textrutor, menyer och fönster — det som brukar kallas GUI (Graphical User Interface).

---

## Projektstrukturen

```
MittProgram/
├── Form1.cs           ← Din kod — den du redigerar
├── Form1.Designer.cs  ← Genererad kod från designern — rör ALDRIG
└── Program.cs         ← Startar appen
```

`Form1.Designer.cs` skrivs automatiskt av Visual Studio när du drar ut kontroller i designern. Om du redigerar den manuellt kan appen sluta fungera eller bete sig konstigt.

---

## Designern

Öppna designern med **Shift+F7** eller dubbelklick på `Form1.cs` och välj "View Designer".

Härifrån drar du ut komponenter från **Toolbox** och placerar dem visuellt på formuläret. Allt du ändrar i designern syns som en egenskap i **Properties-panelen** (F4) — och tvärtom.

---

## Visuella objekt vs kodobjekt

**Visuella objekt** syns i fönstret när appen körs: `Label`, `Button`, `TextBox`, `PictureBox`, `ListBox`.

**Kodobjekt** finns bara i koden och syns inte direkt i fönstret: `Timer`, `ImageList`, `ContextMenuStrip`, `ToolTip`. Båda sätts upp i designern, men kodobjekten visas i en separat yta längst ner i designern.

---

## Kontrollernas hierarki

```
Form (fönstret)
└── Panel (grupperar kontroller)
    ├── Label
    ├── TextBox
    └── Button
```

En `Form` är en container. Du kan lägga kontroller direkt på Form, eller i `Panel` och `GroupBox` för att hålla ordning. Varje kontroll är ett objekt — en `Button` är en instans av klassen `Button`.

---

## Vanliga kontroller — visa och mata in text

| Kontroll | Vad | Viktig property |
|----------|-----|-----------------|
| `Label` | Visar text, kan inte redigeras av användaren | `Text` |
| `TextBox` | Textfält för inmatning | `Text`, `Multiline` |
| `RichTextBox` | Textfält med formatering (fet, kursiv) | `Text`, `Rtf` |
| `MaskedTextBox` | Inmatning med format, t.ex. datum | `Mask` |

```csharp
lblStatus.Text = "Sparat!";
string namn = txtNamn.Text;
```

---

## Vanliga kontroller — knappar och val

| Kontroll | Vad | Viktig property |
|----------|-----|-----------------|
| `Button` | Klickbar knapp | `Text`, `Enabled` |
| `CheckBox` | Kryssruta — ja eller nej | `Checked` |
| `RadioButton` | Välj ett av flera alternativ | `Checked` |
| `ComboBox` | Rullgardinsmeny | `Items`, `SelectedItem` |
| `ListBox` | Lista med valbara alternativ | `Items`, `SelectedItem` |

```csharp
if (chkGodkänn.Checked)
    lblStatus.Text = "Godkänt!";

string valtLand = cboLand.SelectedItem?.ToString() ?? "";
```

---

## Vanliga kontroller — layout och bilder

| Kontroll | Vad |
|----------|-----|
| `Panel` | Osynlig container som grupperar kontroller |
| `GroupBox` | Container med synlig ram och titel |
| `PictureBox` | Visar en bild |
| `ProgressBar` | Visar en progress (0–100) |
| `TabControl` | Flikar med olika innehåll |

---

## Gemensamma properties

Alla kontroller delar dessa egenskaper:

| Property | Vad |
|----------|-----|
| `Name` | Kontrollens variabelnamn i koden |
| `Text` | Texten som visas |
| `Enabled` | `true`/`false` — aktiv eller nedtonad |
| `Visible` | `true`/`false` — synlig eller dold |
| `Size` | Bredd och höjd i pixlar |
| `Location` | Position (X, Y) på formuläret |
| `BackColor` / `ForeColor` | Bakgrunds- och textfärg |
| `Font` | Teckensnitt och storlek |

Sätt dem i Properties-panelen i designern, eller i kod:

```csharp
btnSpara.Enabled = false;
lblStatus.ForeColor = Color.DarkRed;
```

---

## Dock — fylla ut utrymmet

`Dock` bestämmer hur en kontroll fyller sin container. Istället för att ange en fast position låter du kontrollen "docka" mot en kant.

| Värde | Vad |
|-------|-----|
| `None` | Fast position (standard) |
| `Top` | Fyller kontainerns överkant |
| `Bottom` | Fyller underkanten |
| `Left` | Fyller vänsterkanten |
| `Right` | Fyller högerkanten |
| `Fill` | Fyller hela utrymmet |

```csharp
panelMeny.Dock = DockStyle.Left;
dataGridView1.Dock = DockStyle.Fill;
```

Vanligt mönster: en meny till vänster (`Left`) och ett innehållsområde som fyller resten (`Fill`). Sätts enkelt i Properties-panelen under **Dock**.

---

## Anchor — sträck när fönstret ändrar storlek

`Anchor` bestämmer vilka kanter en kontroll är "förankrad" vid. När fönstret ändrar storlek rör sig kontrollen eller sträcks ut beroende på förankringen.

- **Top + Left** (standard) — stannar i övre vänstra hörnet, ingen sträckning
- **Top + Right** — följer med höger kant när fönstret breddas
- **Bottom + Left** — följer med underkanten när fönstret höjs
- **Top + Bottom + Left + Right** — sträcker sig åt alla håll

Sätts i Properties-panelen under **Anchor** — klicka på pilarna i det lilla diagrammet.

---

## Events — kod som körs vid händelser

I ett konsolprogram körs koden uppifrån och ned. I WinForms körs kod som svar på **händelser** — events.

```csharp
private void btnSpara_Click(object sender, EventArgs e)
{
    lblStatus.Text = "Sparat!";
}
```

Du skapar ett event-handler genom att dubbelklicka på en kontroll i designern. VS skapar metoden och kopplar den automatiskt.

---

## Form-events — formulärets livscykel

| Event | När körs det? |
|-------|---------------|
| `Load` | När formuläret öppnas — perfekt för att ladda data |
| `Shown` | Efter att formuläret visats för första gången |
| `Resize` | Varje gång fönstret ändrar storlek |
| `FormClosing` | Precis innan formuläret stängs — bra för att spara |
| `FormClosed` | Efter att formuläret stängts |

```csharp
private void Form1_Load(object sender, EventArgs e)
{
    // Körs när appen startar
    cboLand.Items.AddRange(new[] { "Sverige", "Norge", "Danmark" });
    cboLand.SelectedIndex = 0;
}

private void Form1_FormClosing(object sender, FormClosingEventArgs e)
{
    // Fråga användaren om de vill spara innan de stänger
    var svar = MessageBox.Show("Spara innan du stänger?", "Spara",
        MessageBoxButtons.YesNo);
    if (svar == DialogResult.Yes)
        Spara();
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

```csharp
private void txtSök_TextChanged(object sender, EventArgs e)
{
    string sök = txtSök.Text.ToLower();
    lstResultat.Items.Clear();
    foreach (var namn in alleNamn)
        if (namn.ToLower().Contains(sök))
            lstResultat.Items.Add(namn);
}
```

---

## OOP fungerar direkt

Alla dina klasser, Dictionary och List fungerar exakt likadant i WinForms som i konsolprogram.

```csharp
public class Djur
{
    public string Namn { get; set; }
    public string Ljud { get; set; }
    public string GörLjud() => $"{Namn} säger: {Ljud}!";
}
```

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

## Var används WinForms?

Mer än man tror. Interna system i banker, sjukvård och industri körs ofta på WinForms. Det är stabil, välbeprövad teknik och snabb att bygga med — du kan stöta på det som C#-utvecklare.
