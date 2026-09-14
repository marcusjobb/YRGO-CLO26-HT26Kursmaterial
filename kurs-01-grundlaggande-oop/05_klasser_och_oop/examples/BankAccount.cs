// Vad gör det här programmet:
//   Demonstrerar inkapsling med ett bankkonto — privata fält, properties
//   med get/set-logik, och metoder som validerar indata innan de ändrar state.
//
// Koncept som visas:
//   - Privat backing field + publik property (full get/set-syntax)
//   - Varför vi inte ger direkt tillgång till fältet utifrån
//   - 'value' — den magiska variabeln inuti set
//   - Auto-property { get; set; } vs full get { } set { }
//   - Varför metoderna kontrollerar negativt värde

Console.WriteLine("Hello, World!");

BankAccount acc = new BankAccount();
acc.Insättning(4500);
Console.WriteLine($"Mitt bankkonto har {acc.Saldo} spänn!");

Console.WriteLine("Jag handlar elprylar för 2500");
acc.Uttag(2500);
Console.WriteLine($"Jag har nu {acc.Saldo} kvar.");

acc.Uttag(-510);                                  // negativt uttag — ska ignoreras
Console.WriteLine("Jag handlar koncertbiljetter för 3500");
acc.Uttag(3500);                                  // mer än saldot — ska inte gå igenom
Console.WriteLine($"Jag har nu {acc.Saldo} kvar.");


class BankAccount
{
    // ── FÄLT (field) ──────────────────────────────────────────────────────────
    //
    // 'saldo' är ett privat backing field — ett vanligt fält som lagrar det
    // faktiska värdet i minnet. Det är märkt 'private', vilket betyder att
    // ingen utanför klassen kan läsa eller skriva det direkt.
    //
    // Utan 'private' skulle vem som helst kunna göra:
    //   acc.saldo = -99999;    ← ingen kontroll, inga regler
    //
    // Vi vill inte det. Så vi låter propertyn vara grinden.
    private double saldo = 1000;


    // ── PROPERTY (full get/set-syntax) ────────────────────────────────────────
    //
    // En property ser ut som ett fält utifrån, men är egentligen två metoder:
    // en get-metod (läsare) och en set-metod (skrivare).
    //
    // Full syntax används när du behöver LOGIK i get eller set.
    // Jämför med auto-property längst ned i den här filen.
    //
    //   Utifrån klassen:
    //     double x = acc.Saldo;      → anropar get
    //     acc.Saldo = 500;           → anropar set  (fungerar INTE — private set)
    //
    public double Saldo
    {
        get
        {
            return saldo;           // get: returnera värdet av det privata fältet
        }
        private set
        {
            // 'value' är en inbyggd variabel som bara finns inuti set.
            // Den innehåller det värde som försöker tilldelas.
            //   Saldo = 3000  →  value är 3000
            //   Saldo -= 500  →  value är (nuvarande Saldo - 500)
            //
            // Vi kollar att value > 0 för att förhindra att saldot
            // sätts till ett negativt tal eller noll.
            // Om villkoret inte uppfylls händer ingenting — tyst avvisning.
            if (value > 0)
                saldo = value;
        }
    }

    // ── METODER ───────────────────────────────────────────────────────────────
    //
    // Metoderna är den enda vägen utifrån för att ÄNDRA saldot.
    // Det är de som äger valideringslogiken — propertyn skyddar mot ogiltiga
    // värden, men metoderna kommunicerar varför något avvisas.

    public void Insättning(double belopp)
    {
        // Vi kontrollerar 'belopp', inte saldot.
        // Negativt insättningsbelopp är ingen giltig transaktion.
        if (belopp < 0)
            Console.WriteLine("GTFO!");
        else if (belopp > 20000)
            Console.WriteLine("Du är en skummis!");  // flagga ovanligt stor insättning
        else
            Saldo += belopp;    // anropar private set — set-logiken avgör om det går igenom
    }

    public void Uttag(double belopp)
    {
        // Negativt uttag (t.ex. -510) är inte ett giltigt belopp — avvisa det.
        // Om vi inte kollade detta skulle ett negativt uttag fungera som
        // en insättning, vilket är ett säkerhetshål.
        if (belopp < 0)
            Console.WriteLine("GTFO!");
        else
            Saldo -= belopp;    // om resultatet blir ≤ 0 avvisar private set tyst
    }
}


// ── JÄMFÖRELSE: auto-property vs full get/set ─────────────────────────────────
//
// Auto-property — används när du INTE behöver logik i get eller set.
// C# genererar backing field åt dig automatiskt, du ser det aldrig.
//
//   public double Saldo { get; private set; }
//
// Motsvarar exakt det här — men utan möjlighet att lägga in if-satsen:
//
//   private double saldo;
//   public double Saldo
//   {
//       get { return saldo; }
//       private set { saldo = value; }   // ingen validering
//   }
//
// Tumregel:
//   Behöver du validering eller beräkning i get/set? → full syntax
//   Behöver du bara styra vem som får läsa/skriva?   → auto-property räcker
