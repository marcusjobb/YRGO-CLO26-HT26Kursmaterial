# Git Flow + Code Reviews – granska innan merge

🟡

**Scenario:** Ni mergar till develop, men koden granskas inte. Buggar smyger sig in. Nu inför ni code reviews — ingen PR mergas förrän någon annan har godkänt.

## Steg för steg

### 1. Skapa repo

Ett repo med skyddade `main` och `develop`. Alla klonar.

### 2. Feature branch + PR

Alla skapar en feature branch, lägger till en fil (t.ex. en kodsnutt eller en fråga), committar, pushar, och gör en PR till `develop`.

### 3. Granska

Gå till PR:n på GitHub. Klicka på Files changed. Kommentera på specifika rader:

- "Den här metoden kan förenklas"
- "Saknas null-check här"
- "Bra lösning! 🚀"

Välj sedan:
- **Comment** – bara en kommentar
- **Approve** – godkänn
- **Request changes** – begär ändringar

### 4. Ändra + merga

Om någon begär ändringar → pusha en ny commit till samma branch. PR:n uppdateras automatiskt. När alla godkänt → merga.

---

<details>
<summary>💡 Tips 1 – Var konstruktiv</summary>

Skriv *vad* som är fel och *varför*, inte bara "det här är dåligt".

❌ Dåligt: "Fel"
✅ Bra: "Den här loopar i onödan — använd `.FirstOrDefault()` istället"

</details>

<details>
<summary>💡 Tips 2 – Använd GitHub-flödet</summary>

I PR:n, klicka på en rad för att kommentera direkt på koden. Då ser alla exakt vad du menar. Använd "Request changes" om något måste fixas, annars "Approve".

</details>

---

<details>
<summary>✅ Förslagslösning – hela flödet</summary>

```bash
# Alla gör samma sak:
git clone <repo-url>
cd <repo>
git checkout -b feature/min-kod
echo "function add(a, b) { return a + b; }" > minfil.js
git add .
git commit -m "Lägg till add-funktion"
git push origin feature/min-kod
# → PR till develop

# Granska på GitHub → kommentera → godkänn → merge
```

</details>

---

## Reflektion

- Varför ska man inte merga sin egen PR?
- Vad är skillnaden på "Comment" och "Request changes"?
- Hur påverkar code reviews kodkvaliteten på sikt?

---

_Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig._
