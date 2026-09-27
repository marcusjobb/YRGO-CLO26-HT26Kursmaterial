# Windows Forms — Scenarios

> Ingår inte i examinationen. Syftet är att utforska hur C# fungerar i ett riktigt fönster.
> Skapa ett nytt **Windows Forms App (.NET 10)**-projekt i Visual Studio för varje scenario.

---

## Scenario 1 🟢 — Humörknappen

Bygg en app med tre knappar: **Glad**, **Okej** och **Sur**.

### Krav

- Klick på **Glad** → bakgrunden blir grön och en Label visar `"😄 Härligt!"`
- Klick på **Okej** → bakgrunden blir gul och Labeln visar `"😐 Det går."`
- Klick på **Sur** → bakgrunden blir röd och Labeln visar `"😠 Ugh."`
- Appen ska ha en tydlig rubrik

### Tips

- Dra ut tre `Button` och en `Label` från toolbox
- Dubbelklicka på en knapp i designern för att skapa ett klick-event
- `this.BackColor = Color.LightGreen;` — ändrar formulärets bakgrundsfärg

### Förväntad känsla

En knapp, ett tydligt svar. Enkelt och roligt.

---

## Scenario 2 🟡 — Mini-kalkylator

Bygg en liten räknare med fyra operationer.

### Krav

- Två `TextBox` för inmatning av tal
- Fyra knappar: `+`, `−`, `×`, `÷`
- En `Label` som visar resultatet
- Division med noll ska visa ett meddelande: `"Går inte att dela med noll"`
- Inmatning som inte är ett tal ska hanteras snyggt (visa `"Ange ett giltigt tal"`)

### Tips

- `double.TryParse(textBox1.Text, out double a)` — säkert sätt att konvertera text till tal
- Håll beräkningslogiken i en separat metod, håll click-events korta

### Förväntad output

```
[  12  ] [ 4 ]  →  [ × ]  →  Resultat: 48
[  10  ] [ 0 ]  →  [ ÷ ]  →  Går inte att dela med noll
```
