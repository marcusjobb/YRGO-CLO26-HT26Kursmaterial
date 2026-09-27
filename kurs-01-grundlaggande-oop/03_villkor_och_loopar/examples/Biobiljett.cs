// Biobiljett.cs — if, else if och switch
//
// Livekod som visar tre sätt att välja biljettpris baserat på ålder.
// Alla tre versioner löser samma problem — skillnaden är hur C# hanterar dem.

Console.WriteLine("Biobiljett!");

int barnPris = 99;
int vuxenPris = 169;
int pensionärsPris = 119;
int pris = 0;

Console.Write("Hur gammal är du? ");
int.TryParse(Console.ReadLine(), out int ålder);
// TryParse är säkrare än int.Parse — den kraschar inte om användaren skriver "abc"

// ─────────────────────────────────────────────────────────────
// Version 1 — tre separata if-satser
// ─────────────────────────────────────────────────────────────
// Alla tre if-satser körs alltid, oavsett vad de föregående returnerade.
// Fungerar här, men är onödigt — om villkoren hade överlappat
// kunde pris ha skrivits över av en senare if-sats.

// if (ålder < 18)
//     pris = barnPris;
// if (ålder is >= 18 and < 67)
//     pris = vuxenPris;
// if (ålder >= 67)
//     pris = pensionärsPris;

// ─────────────────────────────────────────────────────────────
// Version 2 — if / else if / else if
// ─────────────────────────────────────────────────────────────
// C# slutar jämföra så fort ett villkor stämmer.
// Tydligare avsikt och mer effektivt.

// if (ålder < 18)
//     pris = barnPris;
// else if (ålder is >= 18 and < 67)
//     pris = vuxenPris;
// else if (ålder >= 67)
//     pris = pensionärsPris;

// ─────────────────────────────────────────────────────────────
// Version 3 — switch med range-mönster (C# 9+)
// ─────────────────────────────────────────────────────────────
// Ren och lättläst struktur när det finns flera tydligt avgränsade fall.
// break avslutar varje case — utan break faller koden genom till nästa.

switch (ålder)
{
    case < 18:
        pris = barnPris;
        break;
    case >= 18 and < 67:
        pris = vuxenPris;
        break;
    case >= 67:
        pris = pensionärsPris;
        break;
}

Console.WriteLine($"Biopriset är {pris} kr");
