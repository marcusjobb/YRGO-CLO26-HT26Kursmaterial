# Övning 2 — Ditt första repository

🟢 Grundnivå

> 📖 Läs [`../notes/git_grunder.md`](../notes/git_grunder.md) innan du börjar — där förklaras varje steg ordentligt.

## Mål

Efter den här övningen kan du:
- Skapa ett lokalt Git-repository
- Göra din första commit
- Koppla repot till GitHub och pusha

## Uppgiften

Du ska skapa ett litet C#-program som hälsar på dig, committa det och pusha till GitHub.
Tänk på det som din första officiella commit som utvecklare.

## Var hamnar dina projekt?

Beroende på vilken IDE du kör skapas projekt på olika ställen. Bra att veta innan du skapar massa projekt och inte hittar dem.

> **Tips:** Ge alltid projekt beskrivande namn — annars har du snart en hel drös med `projekt1`, `projekt2` och ingen aning om vad de gör.


> ⚠️ **VARNING** ⚠️
> Lägg inte dina projekt i `C:\Users\` eller `C:\Users\användarnamn\` direkt.
> Om du initierar git där kommer ALLA dina filer (bilder, dokument, allt) att spåras
> och plötsligt har du 10 miljoner ändringar i git. Det skrämmer vettet av de flesta.


<details>
<summary>📁 Visual Studio Community</summary>

Visual Studio skapar alltid projekt i: `Documents\Source\Repos\`

Du väljer projektnamnet när du skapar — det blir en mapp inuti `Repos\`.

</details>

<details>
<summary>📁 JetBrains Rider</summary>

Jetbrains rider skapar projekt i: `IdeaProjects\` (direkt i din hemkatalog)

Exempel: `C:\Users\dittnamn\IdeaProjects\HejVarlden\`

</details>

<details>
<summary>📁 VS Code</summary>

Skapar ingenting automatiskt — du bestämmer själv var projektet hamnar.
Skapa en mapp (`git` eller `projekt`) i din home-mapp så du samlar alla projekt på ett ställe.

Jag rekommenderar att du lägger det i Documents mappen `Documents\Source\Mina Skapelser\` eller något annat som är lätt att komma ihåg

</details>

---

## Kom igång

### 1. Skapa projektet

<details>
<summary>Visual Studio Community</summary>

File → New → Project → Console App (.NET) → välj namn `HejVarlden` → Create

</details>

<details>
<summary>JetBrains Rider</summary>

New Solution → Console Application → välj namn `HejVarlden` → Create

</details>

<details>
<summary>VS Code / Git Bash</summary>

Öppna Git Bash och kör:

```bash
mkdir mitt-forsta-repo
cd mitt-forsta-repo
dotnet new console -n HejVarlden
cd HejVarlden
code .
```

</details>

---

### 2. Redigera Program.cs

Öppna `Program.cs` och ersätt innehållet med:

```csharp
Console.WriteLine("Hej världen!");
Console.WriteLine($"Mitt namn: [Ditt namn här]");
Console.WriteLine($"Datum: {DateTime.Now:yyyy-MM-dd}");
Console.WriteLine("Mitt första Git-hanterade C#-projekt!");
```

Kör och kontrollera att det fungerar:

<details>
<summary>Visual Studio / Rider</summary>

Tryck **F5** eller klicka **Run**.

_OBS! Om du kör med en laptop finns det risk att du fått med den ofattbart ondskefulla knappen FN, då måste du troligen klicka på FN+F5 för att programmet ska starta_

</details>

<details>
<summary>VS Code / Git Bash</summary>

```bash
dotnet run
```

</details>

---

### 3. Skapa .gitignore

**Viktigt — gör detta INNAN första commit!**

Skapa filen `.gitignore` i projektmappen med detta innehåll:

```
bin/
obj/
.vs/
*.user
.idea/
.vscode/
.env
```

<details>
<summary>Visual Studio</summary>

Högerklicka projektet → Add → New Item → sök "gitignore"

</details>

<details>
<summary>JetBrains Rider</summary>

Högerklicka projektet → New → File → `.gitignore`

</details>

<details>
<summary>VS Code / Git Bash</summary>

```bash
touch .gitignore
code .gitignore
```

</details>

---

### 4. Initiera Git och gör första commit

Öppna **Git Bash**, navigera till projektmappen och kör:

```bash
git init
git status
git add .
git commit -m "Initial commit: Hello World console app"
git log --oneline
```

> Oavsett vilken IDE du använder — Git-kommandona körs alltid i **Git Bash**.

_Jag vet att Editorn kan göra jobbet åt dig, även VS Code men du måste lära dig att göra det med konsolen, också._


---

### 5. Skapa repo på GitHub och pusha

Gå till **github.com** och skapa ett nytt repository:
- Namn: `mitt-forsta-repo`
- Synlighet: Public
- **Lägg inte till README** (vi har redan filer)

Kör sedan i Git Bash:

```bash
git remote add origin git@github.com:ditt-användarnamn/mitt-forsta-repo.git
git push -u origin main
```

<details>
<summary>Djupdykning — vad gör dessa kommandon?</summary>

`git remote add origin ...` — talar om för Git var på internet ditt repo bor. "origin" är bara ett smeknamn för adressen.

`git push -u origin main` — skickar din kod till GitHub. `-u` sätter origin som default så nästa gång räcker det med `git push`.

</details>

---

### 6. Gör en ändring och committa igen

Lägg till en rad i Program.cs:

```csharp
Console.WriteLine("Version 2 — nu kan jag Git!");
```

Spara och committa:

```bash
git add .
git commit -m "Add version 2 message"
git push
```

---

## Förväntat resultat

```
Hej världen!
Mitt namn: [Ditt namn]
Datum: 2026-09-01
Mitt första Git-hanterade C#-projekt!
```

På GitHub ska du se ditt repo med 2 commits, och **inte** se mapparna `bin/` eller `obj/`.

---

## Tänk på det här

- `bin/` och `obj/` ska **inte** synas i ditt GitHub-repo — det sköter `.gitignore`
- Varje commit ska ha ett beskrivande meddelande
- Kör `git status` ofta — det kostar ingenting och ger dig alltid en tydlig bild

> 💬 *"Det här är hur jag brukar göra det. Har du en annan approach som funkar? Kör på den."*

<details>
<summary>🔥 Bonus — Kodbyte</summary>

Byt kod med en klasskamrat. Din uppgift: läs deras kod och svara på:
- Förstår du vad den gör utan att köra den?
- Har de beskrivande variabelnamn?
- Finns `.gitignore`? Är `bin/` exkluderad?

Ge feedback — målet är att hjälpa varandra, inte kritisera.

</details>
