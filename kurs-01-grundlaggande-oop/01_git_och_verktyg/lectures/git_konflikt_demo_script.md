# Lärarmanus — Live-demo: Git konflikter

**Tid:** ~20 minuter
**Placering:** Dag 3, eftermiddag — efter att de gjort övning 2

Kör detta live med projektor. Prata högt om vad du gör och varför.
Låt det ta tid — lugna pauser är bra.

---

## Förberedelse (innan lektionen)

Ha ett GitHub-repo redo med en enkel fil. Använd gärna ett du skapade live
under lektionen så de känner igen det.

---

## Del 1 — Skapa och pusha

```bash
# Skapa en fil
echo "Hej från min dator" > hej.txt

# Committa och pusha
git add hej.txt
git commit -m "Add hej.txt"
git push
```

> *"Nu finns filen både lokalt och på GitHub. Allt är synkat."*

Visa filen på GitHub i webbläsaren.

---

## Del 2 — Ändra på servern, visa lokalt

Redigera `hej.txt` direkt på GitHub (klicka pennan, ändra texten, committa via webben).

> *"Jag ändrade filen på servern. Vad tror ni hände lokalt?"*

```bash
cat hej.txt
```

> *"Ingenting. Min lokala fil vet inte om ändringen."*

```bash
git pull
cat hej.txt
```

> *"Nu fick jag ändringen. Det är det pull gör — hämtar vad servern vet."*

---

## Del 3 — Ändra lokalt, visa på servern

```bash
# Ändra lokalt
echo "Nu ändrar jag lokalt" >> hej.txt
git add hej.txt
git commit -m "Add local change"
```

Visa GitHub i webbläsaren — filen har inte ändrats där.

> *"Jag har en commit lokalt som servern inte vet om. Vad händer om jag pushar nu?"*

```bash
git push
```

> *"Fungerar! Ingen konflikt — servern hade inga nya ändringar sedan jag pullade."*

---

## Del 4 — Skapa en riktig konflikt

Nu gör vi det ordentligt.

**Ändra lokalt:**
```bash
echo "Rad från min dator" >> hej.txt
git add hej.txt
git commit -m "Local change before conflict"
```

**Utan att pusha** — gå till GitHub och redigera `hej.txt` direkt på servern.
Lägg till en annan rad på samma ställe. Committa via webben.

**Tillbaka i terminalen:**
```bash
git push
```

```
! [rejected] main -> main (fetch first)
error: failed to push some refs
```

> *"Nu sa det nej. Servern har en commit jag inte har. Jag måste pull:a först."*

```bash
git pull
```

```
CONFLICT (content): Merge conflict in hej.txt
Automatic merge failed; fix conflicts and then commit the result.
```

---

## Del 5 — Förklara `<<<<<<<`

```bash
cat hej.txt
```

```
<<<<<<< HEAD
Rad från min dator
=======
Rad från GitHub-webben
>>>>>>> origin/main
```

> *"Git vet inte vilken rad som ska gälla. Det är ditt beslut, inte Gits.
> Allt mellan `<<<<< HEAD` och `=====` är din version.
> Allt mellan `=====` och `>>>>> origin/main` är serverns version."*


> [Marcus, berätta här att du faktiskt fått in gruppinlämningar med `<<<<<<<` kvar i koden —
> den studerande hade committat konflikten som om det var riktig kod.
> Kollegorna var inte nådiga. Det händer en gång — sedan aldrig igen.]

Öppna filen i VS Code och visa de inbyggda knapparna:
- Accept Current Change
- Accept Incoming Change
- Accept Both Changes

Välj en, spara, och:

```bash
git add hej.txt
git commit -m "Resolve conflict in hej.txt"
git push
```

> *"Klart. Konflikten är löst. Det händer alla — det löser sig alltid."*

---

## Avslutning

> *"Vad lärde vi oss? Pull innan push. Alltid.
> Och om det ändå blir en konflikt — det är inte farligt.
> Git ber dig bara fatta ett beslut."*

Peka på `notes/git_konflikter.md` för de som vill läsa mer.
