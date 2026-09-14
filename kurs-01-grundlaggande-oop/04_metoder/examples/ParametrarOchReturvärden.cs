// Vad gör det här programmet:
//   Visar hur parametrar och returvärden fungerar i praktiken.
//   Två metoder — en som adderar heltal och en som dividerar med skydd mot nolldivision.
//
// Koncept som visas:
//   - Parametrar är lokala kopior (originalet påverkas inte)
//   - return skickar tillbaka värdet — inte variabeln
//   - Guard clause: kontrollera ogiltigt tillstånd tidigt och returnera

internal class Program
{
    private static void Main(string[] args)
    {
        int x = 10;
        int y = 11;

        // x och y skickas som argument — metoden får kopior av värdena.
        // Vad metoden kallar sina parametrar internt spelar ingen roll här.
        int sum = Add(x, y);
        Console.WriteLine(sum);   // 21

        // Vi testar division med 0 — metoden hanterar det utan krasch.
        double result = Divide(x, 0);
        Console.WriteLine(result);   // 0
    }

    // Tar emot två heltal och returnerar deras summa.
    // 'a' och 'b' är lokala variabler — de existerar bara inuti metoden.
    static int Add(int a, int b)
    {
        int mjau = a + b;   // variabelnamnet spelar ingen roll för den som anropar
        return mjau;        // värdet returneras, inte variabeln
    }

    // Dividerar x med y och returnerar resultatet.
    // Guard clause: om y är 0 returnerar vi 0 direkt — vi undviker att ens försöka dividera.
    static double Divide(double x, double y)
    {
        if (y == 0)
            return 0;

        return x / y;
    }
}
