# Övning 4 — Ta bort en låt 🟡

Lägga till är klart. Nu tar vi bort — men vi frågar användaren om de är säkra först.

---

## Uppgift

Lägg till ett menyval som låter användaren ta bort en låt. Användaren skriver in ett ID, ser vilken låt det gäller, och bekräftar borttagningen med `j` eller `n`.

### Krav

- Visa låtlistan så att användaren ser vilka ID:n som finns
- Fråga efter ID att ta bort
- Kontrollera att låten faktiskt finns — om ID inte finns, visa ett meddelande och avbryt
- Visa låtens titel: `"Ta bort 'Purple Rain'? (j/n): "`
- Om `j` → DELETE, skriv ut "Borttagen."
- Om `n` → avbryt utan att ta bort

---

## Förväntad output

```
=== Ta bort låt ===

ID  Titel                    Längd
--- ------------------------ -----
1   Bohemian Rhapsody        354
2   Hotel California         391
3   Stairway to Heaven       482
4   Purple Rain              520

Ange ID att ta bort: 4
Ta bort 'Purple Rain'? (j/n): j
Borttagen.
```

```
Ange ID att ta bort: 99
Ingen låt med ID 99 hittades.
```

---

## Tips

```csharp
// Hämta en specifik låt för att visa titeln innan DELETE
command.CommandText = "SELECT Titel FROM Låtar WHERE Id = @id";
command.Parameters.AddWithValue("@id", id);
var titel = command.ExecuteScalar() as string;

if (titel == null)
{
    Console.WriteLine($"Ingen låt med ID {id} hittades.");
    return;
}
```

```csharp
// Bekräftelse
Console.Write($"Ta bort '{titel}'? (j/n): ");
string svar = Console.ReadLine()?.ToLower() ?? "";
if (svar == "j")
{
    // DELETE här
}
```

---

## Utmanande frågor

- 💡 Vad händer om användaren skriver "J" med stor bokstav? Hanteras det?
- 💡 Varför visar vi titeln för bekräftelse istället för att radera direkt på ID?
- 💡 Hur gör du om du vill tillåta att skriva vilket ID som helst utan att det kraschar?
