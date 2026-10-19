# Övning 1 — Konsolmenyn 🟢

Innan vi pratar med någon databas behöver vi en meny som fungerar. Den här övningen handlar bara om C# och konsolen — ingen SQLite ännu.

---

## Uppgift

Skapa ett konsolprogram som visar en meny med fyra alternativ. När användaren skriver ett nummer händer något (du skriver bara ut vad som *skulle* hända — inte faktisk logik). Programmet fortsätter visa menyn tills användaren väljer "Avsluta".

Menyn ska se ut ungefär så här:

```
=== Min musikapp ===
1. Visa alla låtar
2. Lägg till låt
3. Ta bort låt
4. Avsluta

Välj: _
```

### Krav

- Meny skrivs ut i en loop
- Ogiltigt val (t.ex. bokstäver eller siffra som inte finns) → vänligt felmeddelande, meny visas igen
- Val 1–3 skriver ut ett meddelande: `"[Här kommer låtlistan]"`, `"[Här lägger vi till]"` etc.
- Val 4 avslutar programmet

---

## Förväntad output

```
=== Min musikapp ===
1. Visa alla låtar
2. Lägg till låt
3. Ta bort låt
4. Avsluta

Välj: 1
[Här kommer låtlistan]

=== Min musikapp ===
1. Visa alla låtar
2. Lägg till låt
3. Ta bort låt
4. Avsluta

Välj: hej
Ogiltigt val. Försök igen.

=== Min musikapp ===
...

Välj: 4
Hejdå!
```

---

## Utmanande frågor

- 💡 Vad händer om du skriver ett nummer som inte finns i menyn, t.ex. `99`? Hanteras det av din kod?
- 💡 Varför använder man `int.TryParse` istället för `int.Parse` när man läser från konsolen?
- 💡 Hur skulle du lägga till ett femte menyalternativ utan att ändra loop-logiken?
