# Klassövning — Vi bygger ett repo tillsammans

🟢 Grundnivå · Hela klassen · Ca 30 minuter

---

Ni är nu ett utvecklingsteam.

Läraren har skapat ett gemensamt repo. Er uppgift är att alla bidra till det — på riktigt, samtidigt.

---

## Steg 1 — Klona klassens repo

Läraren skriver upp repoadressen på tavlan.

```bash
git clone https://github.com/Nion-Education/klass-clo26.git
cd klass-clo26
```

---

## Steg 2 — Lägg till dig själv

Öppna filen `classmates.md` och lägg till en rad om dig själv i tabellen:

```
| Förnamn Efternamn | Var du kommer ifrån | En sak ingen vet om dig |
```

Spara filen.

---

## Steg 3 — Committa

```bash
git add classmates.md
git commit -m "Add [ditt namn]"
```

---

## Steg 4 — Pusha

```bash
git push
```

**En person lyckas direkt.** Alla andra får ett felmeddelande som ser ut ungefär så här:

```
! [rejected] main -> main (fetch first)
error: failed to push some refs
hint: Updates were rejected because the remote contains work that you do not have locally.
```

Det är inte ett fel du gjort. Det betyder att någon annan pushade före dig och att din lokala kopia inte längre är uppdaterad.

---

## Steg 5 — Hämta andras ändringar

```bash
git pull
```

Två saker kan hända:

**A) Auto-merge** — Git löser det själv. Du ser ett meddelande om en merge commit. Kör sedan:

```bash
git push
```

**B) Konflikt** — Du och någon annan har ändrat samma rad. Git kan inte välja automatiskt. Filen ser ut så här:

```
<<<<<<< HEAD
| Anna Svensson | Göteborg | Kan jonglera |
=======
| Erik Lindgren | Stockholm | Har aldrig sett Titanic |
>>>>>>> origin/main
```

Behåll **båda** raderna, ta bort konfliktmarkeringarna (`<<<<<<<`, `=======`, `>>>>>>>`), spara och kör:

```bash
git add classmates.md
git commit -m "Merge — lägg till [ditt namn]"
git push
```

---

## Steg 6 — Se resultatet

När alla är klara:

```bash
git pull
git log --oneline
```

Ni ser alla commits — vem som pushade när, i vilken ordning. Öppna `classmates.md` och se hela klassen samlad i en fil.

---

## Det som just hände

Ni jobbade precis som ett riktigt utvecklingsteam. Samma sak händer varje dag på företag världen över — flera personer ändrar samma kodbas, Git håller ordning.

Det är därför `git pull` alltid är första steget innan du börjar jobba på något nytt.

---

*Läraren leder genomgången efter att alla pushat.*
