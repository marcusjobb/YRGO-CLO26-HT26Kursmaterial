# Git-konflikter

En konflikt uppstår när Git inte kan avgöra vilken version av en fil som ska gälla.
Det händer när du och någon annan (eller du själv på två ställen) har ändrat
samma rad i samma fil.

Det ser skrämmande ut första gången. Det är det inte.

---

## Hur en konflikt ser ut

När du kör `git pull` och det finns en konflikt ser det ut så här:

```
Auto-merging Program.cs
CONFLICT (content): Merge conflict in Program.cs
Automatic merge failed; fix conflicts and then commit the result.
```

Öppnar du filen ser du märkningar som Git lagt in:

```csharp
<<<<<<< HEAD
Console.WriteLine("Min version av raden");
=======
Console.WriteLine("Serverns version av raden");
>>>>>>> origin/main
```

- Allt mellan `<<<<<<< HEAD` och `=======` är **din** version
- Allt mellan `=======` och `>>>>>>> origin/main` är **serverns** version

---

## Lösa en konflikt

### Steg 1 — Se vilka filer som har konflikter

```bash
git status
```

Filer med konflikter visas som `both modified`.

### Steg 2 — Öppna filen och bestäm dig

Du måste manuellt välja vad slutresultatet ska bli. Tre alternativ:

**Behåll din version:**
```csharp
Console.WriteLine("Min version av raden");
```

**Behåll serverns version:**
```csharp
Console.WriteLine("Serverns version av raden");
```

**Kombinera båda:**
```csharp
Console.WriteLine("Min version av raden");
Console.WriteLine("Serverns version av raden");
```

Ta bort alla `<<<<<<<`, `=======` och `>>>>>>>` — de ska inte vara kvar.

### Steg 3 — Markera som löst och committa

```bash
git add Program.cs
git commit -m "Löste konflikt i Program.cs"
git push
```

---

## Editor gör det enklare

VS Code/ VS / Rider visar konflikter med knappar direkt i editorn:

- **Accept Current Change** — behåll din version
- **Accept Incoming Change** — behåll serverns version
- **Accept Both Changes** — behåll båda
- **Compare Changes** — visa skillnaderna sida vid sida

Använd dessa istället för att redigera markeringarna manuellt.

---

## Undvika konflikter

Konflikter uppstår när flera ändrar samma fil. Några vanor som minskar risken:

- **Pull alltid innan du börjar jobba**
- **Committa och pusha ofta** — ju längre du väntar desto mer hinner divergera
- **Kommunicera med ditt team** — "jag jobbar på den här filen nu"
- **Jobba i branches** — var sin gren, merge när klart (mer om det senare)

---

## Ångra en pull som gick fel

Om du vill börja om och kasta bort din senaste merge-attempt:

```bash
git merge --abort
```

Koden återgår till läget innan du körde `git pull`.

---

## Kom ihåg

En konflikt är inte ett fel — det är Git som ber dig fatta ett beslut.
Det händer alla. Det löser sig alltid.
