# Programmeringstermer — Aritmetik

Matematik i kod ser nästan ut som vanlig matte — men med ett par fallgropar som är värda att känna till redan från början. Den klassiska: `10 / 3` ger inte `3.333...` om du jobbar med `int`. Det ger `3`. Och resten försvinner tyst.

**Se även:** [datatyper.md](datatyper.md) — skillnaden mellan `int` och `double` avgör vad räknesätten ger för svar.

---

## Addition `+`

Lägger ihop två tal. Om båda är `int` blir resultatet `int`. Om någon är `double` blir resultatet `double`.

```csharp
int summa = 10 + 3;
double decimalSumma = 10.0 + 3;

Console.WriteLine(summa);
Console.WriteLine(decimalSumma);
```

### Output
```plaintext
13
13
```

`+` används också för att sätta ihop strängar — det kallas **konkatenering**. `"Hej" + " " + "Anna"` ger `"Hej Anna"`. Det är samma operator, men ett helt annat beteende beroende på vad du plussar ihop.

---

## Subtraktion `-`

Drar av ett tal från ett annat.

```csharp
int skillnad = 10 - 3;
Console.WriteLine(skillnad);
```

### Output
```plaintext
7
```

---

## Multiplikation `*`

Multiplicerar två tal.

```csharp
int produkt = 10 * 3;
Console.WriteLine(produkt);
```

### Output
```plaintext
30
```

---

## Division `/`

Delar ett tal med ett annat. Här gömmer sig den klassiska fällan.

```csharp
int heltalsDivision = 10 / 3;
double decimalDivision = 10.0 / 3;

Console.WriteLine(heltalsDivision);
Console.WriteLine(decimalDivision);
```

### Output
```plaintext
3
3.3333333333333335
```

Rad 1 ger `3` — inte `3.33`. Det är **heltalsdivision**: när du delar ett `int` med ett `int` kastar C# bort decimaldelen och ger dig bara hela delen. Ingen avrundning — det trunkeras (huggs av).

Rad 2 ger det förväntade decimaltalet — för att en av operanderna är `10.0` (`double`) behandlas hela uttrycket som `double`-division.

<details><summary>Hur undviker jag heltalsdivision när jag inte vill ha det?</summary>

Gör en av operanderna till `double`. Du kan antingen skriva ut det direkt (`10.0 / 3`) eller casta en befintlig variabel:

```csharp
int täljare = 10;
int nämnare = 3;
double resultat = (double)täljare / nämnare;
Console.WriteLine(resultat);
```

### Output
```plaintext
3.3333333333333335
```

`(double)täljare` castar `täljare` till `double` precis i det uttrycket — variabeln `täljare` är fortfarande en `int`, inget ändras permanent.

</details>

---

## Modulo `%`

Ger **resten** efter heltalsdivision — den bit som inte fick plats i hela delar.

```csharp
int rest = 10 % 3;
Console.WriteLine(rest);
```

### Output
```plaintext
1
```

`10 / 3` ger `3` (hela delar), och `3 * 3 = 9` — det är `1` kvar. Modulo ger dig den 1:an.

Klassiska användningar: kolla om ett tal är jämnt (`tal % 2 == 0`), dela upp saker i grupper, "linda runt" ett index i en lista.

```csharp
int tal = 8;
bool ärJämnt = tal % 2 == 0;
Console.WriteLine(ärJämnt);
```

### Output
```plaintext
True
```

---

## Inkrement `++` och dekrement `--`

Ökar eller minskar en variabel med exakt 1. Ett kortkommando som dyker upp hela tiden — framför allt i loopar.

```csharp
int räknare = 5;
räknare++;
Console.WriteLine(räknare);

räknare--;
Console.WriteLine(räknare);
```

### Output
```plaintext
6
5
```

`räknare++` är exakt samma sak som `räknare = räknare + 1` — bara kortare att skriva.

<details><summary>Vad är skillnaden mellan i++ och ++i?</summary>

Båda ökar variabeln med 1, men de skiljer sig i vilket värde de ger tillbaka om du läser dem i ett uttryck:

```csharp
int a = 5;
int b = a++;   // b får 5 (gamla värdet), sedan ökar a till 6
int c = 5;
int d = ++c;   // c ökar till 6 FÖRST, sedan får d 6

Console.WriteLine(b); // 5
Console.WriteLine(a); // 6
Console.WriteLine(d); // 6
Console.WriteLine(c); // 6
```

### Output
```plaintext
5
6
6
6
```

I loopar (det vanligaste stället) spelar det ingen roll — `i++` och `++i` är identiska när du bara ökar en räknare och inte samtidigt läser av värdet. Håll dig till `i++` tills du stöter på ett fall där det faktiskt spelar roll.

</details>

---

## Sammansatta tilldelningsoperatorer `+=` `-=` `*=` `/=`

Genvägarna när du vill ändra en variabel baserat på sitt eget värde. Istället för `poäng = poäng + 10` skriver du `poäng += 10`.

```csharp
int poäng = 100;

poäng += 50;   // samma som: poäng = poäng + 50
Console.WriteLine(poäng);

poäng -= 30;   // samma som: poäng = poäng - 30
Console.WriteLine(poäng);

poäng *= 2;    // samma som: poäng = poäng * 2
Console.WriteLine(poäng);

poäng /= 4;    // samma som: poäng = poäng / 4
Console.WriteLine(poäng);
```

### Output
```plaintext
150
120
240
60
```

De är exakt likvärdiga med den längre formen — det är bara ett kortare sätt att skriva. Du ser dem mest i loopar och när du ackumulerar ett värde stegvis.

---

## Operatorsordning

C# följer samma ordning som i matematiken: multiplikation och division beräknas innan addition och subtraktion. Parenteser kan ändra ordningen.

```csharp
int utan = 2 + 3 * 4;
int med = (2 + 3) * 4;

Console.WriteLine(utan);
Console.WriteLine(med);
```

### Output
```plaintext
14
20
```

`2 + 3 * 4` beräknas som `2 + 12 = 14` (multiplikationen sker först). `(2 + 3) * 4` beräknas som `5 * 4 = 20` (parenteserna tvingar additionen att gå först). Osäker? Lägg till parenteser — det gör koden tydligare och datorn gör rätt.
