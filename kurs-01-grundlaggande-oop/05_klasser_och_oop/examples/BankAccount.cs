// BankAccount.cs
// Föreläsningsexempel — Klasser och inkapsling
// Används under vecka 3 för att visa private/public, properties och metoder

class BankAccount
{
    // property med private set — läsbar utifrån, skrivbar bara inifrån klassen
    public double Saldo { get; private set; }
    public string Ägare { get; private set; }
    public bool ÄrAktivt { get; private set; }

    // konstruktor — körs när objektet skapas
    public BankAccount(string ägare, double startSaldo)
    {
        Ägare = ägare;
        Saldo = startSaldo;
        ÄrAktivt = true;
    }

    // metod för att sätta in pengar
    public void SättIn(double belopp)
    {
        if (belopp <= 0)
        {
            Console.WriteLine("Beloppet måste vara positivt.");
            return;
        }
        Saldo += belopp;
        Console.WriteLine($"{Ägare} satte in {belopp} kr. Nytt saldo: {Saldo} kr.");
    }

    // metod för att ta ut pengar — returnerar true om det gick, false om inte
    public bool TaUt(double belopp)
    {
        if (belopp <= 0 || belopp > Saldo)
        {
            Console.WriteLine("Uttag nekat — otillräckligt saldo.");
            return false;
        }
        Saldo -= belopp;
        Console.WriteLine($"{Ägare} tog ut {belopp} kr. Nytt saldo: {Saldo} kr.");
        return true;
    }

    // metod som beskriver kontot
    public void Presentera()
    {
        string status = ÄrAktivt ? "Aktivt" : "Inaktivt";
        Console.WriteLine($"Konto: {Ägare} | Saldo: {Saldo} kr | Status: {status}");
    }

    static void Main()
    {
        BankAccount konto1 = new BankAccount("Alex", 1000);
        BankAccount konto2 = new BankAccount("Sam", 500);

        konto1.Presentera();
        konto2.Presentera();

        Console.WriteLine();

        konto1.SättIn(500);
        konto1.TaUt(200);
        konto2.TaUt(600);   // ska misslyckas

        Console.WriteLine();

        konto1.Presentera();
        konto2.Presentera();
    }
}
