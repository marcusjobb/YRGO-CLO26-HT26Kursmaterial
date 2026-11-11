# Övning 3 — Lägg till en låt 🟢

Databasen visar data. Nu tar vi emot input från användaren och sparar det.

---

## Uppgift

Bygg vidare på övning 2. Lägg till ett menyval som låter användaren skriva in en låttitel och en längd (i sekunder), och spara det i databasen.

### Krav

- Fråga efter titel och längd
- Längden ska vara ett heltal — om användaren skriver fel typ, visa ett felmeddelande och fråga igen
- Spara med parameteriserad query (`@titel`, `@langd`)
- Skriv ut "Låt sparad!" när det gick bra
- Gå tillbaka till huvudmenyn efteråt

---

## Förväntad output

```
=== Lägg till låt ===
Titel: Purple Rain
Längd i sekunder: 520
Låt sparad!

=== Min musikapp ===
1. Visa alla låtar
2. Lägg till låt
...

Välj: 1

=== Låtlista ===
ID  Titel                    Längd
--- ------------------------ -----
1   Bohemian Rhapsody        354
2   Hotel California         391
3   Stairway to Heaven       482
4   Purple Rain              520
```

---

## Tips

```csharp
// Parameteriserad INSERT — aldrig strängkonkatenering
command.CommandText = "INSERT INTO Låtar (Titel, Längd) VALUES (@titel, @langd)";
command.Parameters.AddWithValue("@titel", titel);
command.Parameters.AddWithValue("@langd", langd);
command.ExecuteNonQuery();
```

```csharp
// Säker inläsning av heltal
Console.Write("Längd i sekunder: ");
if (!int.TryParse(Console.ReadLine(), out int langd))
{
    Console.WriteLine("Det där var inte ett heltal. Försök igen.");
    // hantera på ett lämpligt sätt
}
```

---

## Utmanande frågor

- 💡 Varför är parameteriserade queries viktigare när användaren skriver in data?
- 💡 Vad händer om användaren skriver en tom titel? Borde du kontrollera det?
- 💡 Hur skulle du kontrollera att en låt med exakt samma titel inte redan finns?
