// Betyg.cs — villkorskedjor med if / else if / else
//
// Livekod baserad på läxuppgiften.
// Visar varför ordningen på if-satser spelar roll.

Console.WriteLine("Betyg!");

int poäng = 72;

// ─────────────────────────────────────────────────────────────
// Varför spelar ordningen roll?
// ─────────────────────────────────────────────────────────────
//
// Fel ordning — kontrollerar >= 50 INNAN >= 70:
//
//   if (poäng >= 50)          ← 72 >= 50 är sant → "C", vi slutar här
//       Console.WriteLine("C");
//   else if (poäng >= 70)     ← körs aldrig för poäng = 72
//       Console.WriteLine("B");
//
// Studeranden med 72 poäng får C trots att B är rätt.
// Rätt princip: börja alltid med det strängaste (högsta) villkoret.

// ─────────────────────────────────────────────────────────────
// else if vs else
// ─────────────────────────────────────────────────────────────
//
//   else if (poäng >= 70)   ← kräver ett villkor i parentesen
//   else                    ← tar INGET villkor — "allt annat"
//
// else är alltid den absolut sista grenen.
// Det kan bara finnas EN else per if-kedja.
// Den körs när inget av de föregående villkoren stämde.

if (poäng >= 90)
    Console.WriteLine("Du fick A — Utmärkt!");
else if (poäng >= 70)
    Console.WriteLine("Du fick B — Bra jobbat!");
else if (poäng >= 50)
    Console.WriteLine("Du fick C — Godkänt.");
else
    Console.WriteLine("Du fick F — Försök igen.");
