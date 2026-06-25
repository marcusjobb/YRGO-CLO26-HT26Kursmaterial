# Fyrkanter v2 — Loop-motivation (vecka 4–5)

Uppföljare till `01_verktyg_och_git/exercises/gruppovning_01_fyrkanter.md`.

---

## Idén

Visa SAMMA typ av uppgift som de löste i vecka 1 — men med en svårare bild. De försöker räkna för hand och inser att det inte går. Det är den naturliga ingången till loopar.

Ingen moralkaka. Inget "nu ska ni lära er loopar för att det är viktigt". Bara: "hur många fyrkanter finns det i den här?"

---

## Bilden

Finns sparad som: `/home/marcus/Downloads/grid.jpg`

Struktur:
- 4 stora hörnrutor (vardera = 2×2 små rutor)
- Mitten: 6×6 rutnät av små rutor
- Totalt synliga: 40 (36 små + 4 stora)
- Totalt matematiskt (alla storlekar): 95+ (Gemini räknade 95, spanning-rutor tillkommer)

---

## Flödet i klassrummet

1. Visa bilden. Låt dem räkna precis som i vecka 1.
2. Samla svar på tavlan — stor spridning garanterad.
3. "Vad är rätt svar?" Ingen vet säkert.
4. "Hur skulle vi kunna ta reda på det?" — de kommer förr eller senare fram till att en dator kunde göra det.
5. Det är ingången till loopar och algoritmer.

---

## Koppling till kod (efter loopgenomgång)

```csharp
// Räkna kvadrater i ett n×n rutnät
int n = 6;
int total = 0;

for (int storlek = 1; storlek <= n; storlek++)
{
    int positioner = (n - storlek + 1) * (n - storlek + 1);
    total += positioner;
    Console.WriteLine($"{storlek}×{storlek}: {positioner} st");
}

Console.WriteLine($"Totalt: {total}");
```

Ger 91 för det centrala 6×6-rutnätet. Plus hörnrutorna och spanning-rutor för det fulla svaret.
