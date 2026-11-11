# Programmeringstermer — Git-konflikter

## Merge-konflikt

En merge-konflikt uppstår när Git inte kan slå ihop två versioner av samma fil automatiskt. Det händer när du och någon annan (eller du på två ställen) har ändrat **samma rad** i samma fil, och Git inte vet vilken version som ska gälla.

Det ser skrämmande ut första gången. Det är inte farligt — Git har inte krashat, Git väntar på att du ska fatta ett beslut.

```bash
git pull

# Output vid konflikt:
CONFLICT (content): Merge conflict in Program.cs
Automatic merge failed; fix conflicts and then commit the result.
```

```mermaid
flowchart LR
    A[Tony ändrar rad 5] --> C[(GitHub)]
    B[Steve ändrar rad 5] --> C
    C -->|git pull| D[💥 Merge-konflikt\nGit vet inte vilket som gäller]
    style A fill:#f0f0f0,stroke:#888,color:#111
    style B fill:#f0f0f0,stroke:#888,color:#111
    style C fill:#1565c0,stroke:#0d47a1,color:#fff
    style D fill:#b71c1c,stroke:#7f0000,color:#fff
```

**Se även:** [Konfliktmarkering](#konfliktmarkering), [git merge --abort](#git-merge---abort).

---

## Konfliktmarkering

När en merge-konflikt uppstår lägger Git in markeringar direkt i filen för att visa de två versionerna:

```csharp
<<<<<<< HEAD
Console.WriteLine("Vi gör det tillsammans. Det är vad ett team gör.");
=======
Console.WriteLine("Stark Industries löser det här. Ensam.");
>>>>>>> origin/main
```

| Markering | Vad den betyder |
|-----------|-----------------|
| `<<<<<<< HEAD` | Din version börjar här |
| `=======` | Skiljelinjen mellan de två versionerna |
| `>>>>>>> origin/main` | Serverns version slutar här |

Du måste manuellt välja vad filen ska innehålla och ta bort alla tre markeringarna. De ska inte vara kvar i slutresultatet.

<details><summary>Tre sätt att lösa</summary>

**Behåll din version** — ta bort serverns del och markeringarna:
```csharp
Console.WriteLine("Vi gör det tillsammans. Det är vad ett team gör.");
```

**Behåll serverns version** — ta bort din del och markeringarna:
```csharp
Console.WriteLine("Stark Industries löser det här. Ensam.");
```

**Kombinera båda** — ta bort markeringarna, behåll båda raderna:
```csharp
Console.WriteLine("Vi gör det tillsammans. Det är vad ett team gör.");
Console.WriteLine("Stark Industries löser det här. Ensam.");
```

I VS Code och Rider syns konfliktmarkeringarna med knappar ovanför: **Accept Current Change**, **Accept Incoming Change**, **Accept Both Changes**. Använd knapparna — det är lättare än att redigera markeringarna för hand.

</details>

---

## Lösa en konflikt (ritualen)

När du har redigerat filen och tagit bort alla konfliktmarkeringar:

```bash
git status                               # verifiera — ska inte visa "both modified"
git add Program.cs                       # markera filen som löst
git commit -m "Löste konflikt i Program.cs"
git push
```

```mermaid
flowchart TD
    A[git pull → konflikt] --> B[Öppna filen\nredigera bort markeringarna]
    B --> C[git add filnamn.cs]
    C --> D[git commit -m 'Löste konflikt...']
    D --> E[git push]
    style A fill:#b71c1c,stroke:#7f0000,color:#fff
    style B fill:#f0f0f0,stroke:#888,color:#111
    style C fill:#f0f0f0,stroke:#888,color:#111
    style D fill:#f0f0f0,stroke:#888,color:#111
    style E fill:#1565c0,stroke:#0d47a1,color:#fff
```

`git status` visar `both modified` så länge konflikten finns kvar. Efter `git add` är den markerad som löst.

---

## git merge --abort

Ångrar ett pågående merge-försök och återställer koden till läget precis innan `git pull`. Användbart om konflikten är förvirrande och du vill börja om med klarare huvud.

```bash
git merge --abort
```

Fungerar bara om du **inte** redan kört `git add` och `git commit` på konflikten. Kör du den efter commit händer ingenting — commiten är redan gjord.

<details><summary>När använder man det?</summary>

- Du körde `git pull` och fick fem konflikter i filer du inte förstår
- Du vill prata med en kursare om hur filerna ska slås ihop innan du bestämmer dig
- Du råkade köra `git pull` på fel branch

Efter `git merge --abort` kan du köra `git status` och se att allt är tillbaka till normalt.

</details>

---

## "both modified"

Det Git skriver i `git status` när en fil har en olöst merge-konflikt:

```bash
git status

# Output:
Unmerged paths:
  (use "git add <file>..." to mark resolution)
        both modified:   Program.cs
```

`both modified` = du och någon annan har båda ändrat filen. Git väntar på att du löser konflikten och kör `git add`.

**Se även:** [Lösa en konflikt](#lösa-en-konflikt-ritualen).
