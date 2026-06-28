---
marp: true
theme: default
class: invert
paginate: true
---

# Git-konflikter
## När två versioner krockar

---

## Hur uppstår en konflikt?

Tony Stark och Steve Rogers jobbar på samma fil.

```
Tony:   Console.WriteLine("Stark Industries löser det här. Ensam.");
Steve:  Console.WriteLine("Vi gör det tillsammans. Det är vad ett team gör.");
```

Tony pushar först. Steve pushar — och Git sätter stopp.

> Git kan inte välja. Det är **ditt** jobb att bestämma.

---

## Vad händer steg för steg

```
Steve kör: git pull

Auto-merging Program.cs
CONFLICT (content): Merge conflict in Program.cs
Automatic merge failed; fix conflicts and then commit the result.
```

Git har inte krashat. Git väntar på ett beslut.

---

## Vad du ser i filen

```csharp
<<<<<<< HEAD
Console.WriteLine("Vi gör det tillsammans. Det är vad ett team gör.");
=======
Console.WriteLine("Stark Industries löser det här. Ensam.");
>>>>>>> origin/main
```

| Markering | Betydelse |
|-----------|-----------|
| `<<<<<<< HEAD` | Din version börjar här |
| `=======` | Skiljelinjen |
| `>>>>>>> origin/main` | Serverns version slutar här |

---

## Tre val

**Behåll din version (Steve):**
```csharp
Console.WriteLine("Vi gör det tillsammans. Det är vad ett team gör.");
```

**Behåll serverns version (Tony):**
```csharp
Console.WriteLine("Stark Industries löser det här. Ensam.");
```

**Kombinera — Civil War undvikt:**
```csharp
Console.WriteLine("Vi gör det tillsammans. Det är vad ett team gör.");
Console.WriteLine("Stark Industries löser det här. Ensam.");
```

Ta bort alla `<<<<<<<`, `=======`, `>>>>>>>`. De ska inte vara kvar.

---

## I VS Code / Rider — klicka istället

Editorn visar konflikten med knappar:

- **Accept Current Change** — behåll din version
- **Accept Incoming Change** — behåll serverns version
- **Accept Both Changes** — behåll båda

Använd knapparna. Manuell redigering av markörerna funkar, men det är lätt att missa en.

---

## Ritualen efter lösningen

```bash
git status                          # verifiera att konflikten är löst
git add Program.cs                  # markera som löst
git commit -m "Löste konflikt i Program.cs"
git push
```

`git status` visar `both modified` så länge konflikten finns kvar.
När du lagt till filen med `git add` är den markerad som löst.

---

## Ångra och börja om

Gick det åt skogen? Ingen panik.

```bash
git merge --abort
```

Koden återgår till läget precis innan `git pull`. Prova igen med klarare huvud.

---

## Undvik konflikter — tre vanor

1. **Pull innan du börjar jobba** — inte bara innan du pushar
2. **Committa och pusha ofta** — ju längre du väntar desto mer hinner divergera
3. **Kommunicera** — "jag jobbar på den här filen nu"

Konflikter händer alla. De löser sig alltid.

---

## Sammanfattning

- Konflikt = Git kan inte välja automatiskt mellan två versioner
- Markörerna `<<<<<<<`, `=======`, `>>>>>>>` visar de två versionerna
- Du bestämmer vad slutresultatet ska bli
- Efter lösning: `git add` → `git commit` → `git push`
- Fastnar du: `git merge --abort` och börja om

**Läs mer:** `notes/git_konflikter.md`
