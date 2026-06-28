---

title: Mystiska Metoder För Mångsidiga Mästerkodare
author: Marcus Ackre Medina
type: exercise
topic: methods
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Enkla metoder/Mystiska Metoder För Mångsidiga Mästerkodare.docx"
description: "Mystiska Metoder För Mångsidiga"
tags: ["csharp", "exercise", "för", "methods", "metoder", "mystiska", "mästerkodare.docx", "mångsidiga", "test"]
week_fit: []
---
Mystiska Metoder För Mångsidiga
Mästerkodare
1 – Jultomtens dagräknare
Jultomten behöver hålla koll på antal dagar från idag till jul. Skriv en metod som tar emot en
DateTime och räknar ut antal dagar till jul från det datumet.
public void DaysTillXmas(DateTime today)
{
// Skriv din kod här

}

2 – txeTsegnälkaB

😊

Skapa en metod som tar emot en sträng och skriver den baklänges. Testa med ”odlas”,”paris”,
”släp”, ”sallad”,”atlas”, ”tacocat”
public void BaklängesText(string text)
{
// Skriv din kod här

}

3 – Siffror till text
Skapa en metod som tar emot ett tal, sedan ska den skriva ut varje tal i textform från ”ett” till ”nio”. I
detta exempel gör det inget att vi inte tar hänsyn till tio, hundra eller tusental.
public void TalTillText(int tal)
{
// Skriv din kod här
}

4 – Minsta talet först (Swap)
Skapa en metod som tar emot två tal, om första talet är större än andra talet så ska de byta plats.
Sedan ska den skriva ut talen i storleksordning.
public void MinstaTaletFörst(int a, int b)
{
// Skriv din kod här
}

5 – Medelvärdet av en array
Skapa en metod som tar emot en array av ints, och räknar ut medelvärdet av dem
public void Medelvärde(int[] allaTal)
{
// Skriv din kod här
}

6 – Överlagring
Nu ska vi räkna nummerologiskt. Skapa en metod som tar emot en sträng och summerar värden på
dina bokstäver (a=1, b=2 osv)
public void AdderaSymboler(string text)
{
// Skriv din kod här
}

Skapa en metod som tar emot en siffra och summerar sifforna i den. 142 = 7, 303 = 6, 87=15
public void AdderaSymboler (int tal)
{
// Skriv din kod här
}

Metoderna kan heta likadant då de har olika inparametrar. Det kallas överlagring.

7 – Rad av tecken
Skriv en metod som tar emot en char och en int, sedan skriver den ut char symbolen lika många
gånger som int värdet anger.
public void RadAvTecken(char symbol, int antal)
{
// Skriv din kod här
}

8 – Bromssträcka
För att beräkna bromssträclka följer vi formeln eller i klartext

Friktionstalet är .8 på torr asfalt. Om du kör i 50 är bromssträckan 12.5m.
Gör en metod som tar emot hastighet och räknar ut bromssträckan.
public void Bromssträcka(double hastighet)
{
// Skriv din kod här
}

9 – Procent av…
Nu ska du räkna hur mycket x procent av ett tal är. Exempelvis, hur mycket är 3% av 352? 10.56.
Skapa en metod som tar emot procenttal och värde att räkna från.
public void ProcentAv(double proc, double value)
{
// Skriv din kod här
}

10 – Lägg till procent
Nu ska vi lägga till x procent till ett värde. Skapa en metod för det.

public void LäggTillProcent(double proc, double value)
{
// Skriv din kod här
}

11 – Hur många procent är x av ett värde?
Nu ska du räkna ut hur många procent ett tal är av ett annat tal.
public void HurMångaProcentÄr(double num, double value)
{
// Skriv din kod här
}

12 – Lucky 8ball
Du ska skapa en metod som fungerar som en lucky 8ball. En sådan boll svarar på frågor med 20 olika
svar som väljs slumpmässigt av en T20, alltså en tärning med 20 sidor.
It is certain.

Outlook good.

Concentrate and ask again.

It is decidedly so.

Yes.

Don't count on it.

Without a doubt.

Signs point to yes.

My reply is no.

Yes definitely.

Reply hazy, try again.

My sources say no.

You may rely on it.

Ask again later.

Outlook not so good.

As I see it, yes.

Better not tell you now.

Very doubtful.

Most likely.

Cannot predict now.

Nu ska du skapa en metod som slumpar fram ett värde mellan 1 och 20 och visar något av de svaren.
public void EightBall()
{
Random random = new Random(); // Instansiera slumpgeneratorn
int answer = random.Next(1, 21); // Välj ett tal mellan 1 och 20
// Skriv resten av koden här
}

Facit:
1 – Jultomtens dagräknare
public void DaysTillXmas(DateTime today)
{
DateTime Xmas = new DateTime(DateTime.Now.Year, 12, 25);
TimeSpan diff = (Xmas - today);
Console.WriteLine(diff.Days + " dagar.");
}

2 – txeTsegnälkaB
public void BaklängesText(string text)
{
for (int i = text.Length - 1; i >= 0; i--)
{
Console.Write(text[i]);
}
}

3 – Siffror till text
public void TalTillText(int tal)
{
string nummer = tal.ToString();
for (int i = 0; i < nummer.Length; i++)
{
switch(nummer[i])
{
case '0': Console.Write("Noll"); break;
case '1': Console.Write("Ett"); break;
case '2': Console.Write("Två"); break;
case '3': Console.Write("Tre"); break;
case '4': Console.Write("Fyra"); break;
case '5': Console.Write("Fem"); break;
case '6': Console.Write("Sex"); break;
case '7': Console.Write("Sju"); break;
case '8': Console.Write("Åtta"); break;
case '9': Console.Write("Nio"); break;
default: Console.Write("Wtf?"); break;
}
}
Console.WriteLine();
}

4 – Minsta talet först
public void MinstaTaletFörst(int a, int b)
{

if (a > b)
{
int c = a;
a = b;
b = c;
}
Console.WriteLine(a + " " + b);
}

5 – Medelvärdet av en array
public void Medelvärde(int[] allaTal)
{
int sum = 0;
for (int i = 0; i < allaTal.Length; i++)
{
sum += allaTal[i];
}
double medel = sum / allaTal.Length;
Console.WriteLine("Medelvärdet är " + medel);
}

6 – Överlagring
public void AdderaSymboler(string text)
{
int diff = 'a' - 1;
int sum = 0;
for (int i = 0; i < text.Length; i++)
{
sum += text[i] - diff;
}
Console.WriteLine(sum);
}
public void AdderaSymboler(int tal)
{
string nummer = tal.ToString();
int sum = 0;
for (int i = 0; i < nummer.Length; i++)
{
sum += int.Parse(nummer[i].ToString());
}
Console.WriteLine(sum);
}

7 – Rad av tecken
Skriv en metod som tar emot en char och en int, sedan skriver den ut char symbolen lika många
gånger som int värdet anger.
public void RadAvTecken(char symbol, int antal)
{
Console.WriteLine(new String(symbol, antal)); // LOL

}

8 – Bromssträcka
public void Bromssträcka(double hastighet)
{
double friction = .8;
double s = Math.Pow(hastighet, 2) / (250 * friction);
Console.WriteLine(s);
}

9 – Procent av…
public void ProcentAv(double proc, double value)
{
Console.WriteLine(value * (proc / 100));
}

10 – Lägg till procent
public void LäggTillProcent(double proc, double value)
{
Console.WriteLine(value * ((proc / 100) + 1));
}

11 – Hur många procent är x av ett värde?
public void HurMångaProcentÄr(double num, double value)
{
Console.WriteLine(((num/value)*100) + "%");
}

12 – Lucky 8ball
public void EightBall()
{
Random random = new Random();
int answer = random.Next(1, 21);
switch (answer)
{
case 01: Console.WriteLine("It is certain."); break;
case 02: Console.WriteLine("It is decidedly so."); break;
case 03: Console.WriteLine("Without a doubt."); break;
case 04: Console.WriteLine("Yes definitely."); break;
case 05: Console.WriteLine("You may rely on it."); break;
case 06: Console.WriteLine("As I see it, yes."); break;
case 07: Console.WriteLine("Most likely."); break;
case 08: Console.WriteLine("Outlook good."); break;
case 09: Console.WriteLine("Yes."); break;
case 10: Console.WriteLine("Signs point to yes."); break;
case 11: Console.WriteLine("Reply hazy, try again."); break;
case 12: Console.WriteLine("Ask again later."); break;
case 13: Console.WriteLine("Better not tell you now."); break;
case 14: Console.WriteLine("Cannot predict now."); break;
case 15: Console.WriteLine("Concentrate and ask again."); break;
case 16: Console.WriteLine("Don't count on it."); break;
case 17: Console.WriteLine("My reply is no."); break;
case 18: Console.WriteLine("My sources say no."); break;
case 19: Console.WriteLine("Outlook not so good."); break;
case 20: Console.WriteLine("Very doubtful."); break;
default:
Console.WriteLine("Never play leapfrog with a unicorn"); break;
break;

}
}
