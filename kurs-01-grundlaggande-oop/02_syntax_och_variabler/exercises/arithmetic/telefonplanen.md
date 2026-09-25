# Övning — Telefonplanen

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

Du letar efter ett nytt mobilabonnemang. Tre operatörer erbjuder var sitt paket — men vilket är billigast i längden?

---

## Flödesschema

![Diagram](diagrams/telefonplanen_1.png)

<!-- mermaid: diagrams/telefonplanen_1.mmd -->

## Kodning

---

## Steg 1: Deklarera abonnemangen

```csharp
string op1 = "Telia";
int pris1 = 299;
int gb1 = 30;

string op2 = "Halebop";
int pris2 = 199;
int gb2 = 20;

string op3 = "Comviq";
int pris3 = 149;
int gb3 = 10;
```

Skriv ut alla tre med namn, pris och GB.

### Förväntad output
```plaintext
Telia:   299 kr/mån  30 GB
Halebop: 199 kr/mån  20 GB
Comviq:  149 kr/mån  10 GB
```

<details><summary>Hur formaterar jag utskriften snyggt?</summary>

```csharp
Console.WriteLine($"{op1,-8} {pris1} kr/mån  {gb1} GB");
```

`{op1,-8}` vänsterjusterar texten i ett 8 tecken brett fält — vilket gör att kolumnerna hamnar snyggt.

</details>

---

## Steg 2: Beräkna årkostnad

En plan med ett mobilkort brukar löpa i 24 månader. Beräkna totalkostnaden för varje abonnemang under hela bindningstiden.

### Förväntad output
```plaintext
Telia 24 mån:   ### kr
Halebop 24 mån: ### kr
Comviq 24 mån:  ### kr
```

<details><summary>Hur räknar jag ut 24 månader?</summary>

```csharp
int total1 = pris1 * 24;
```

</details>

---

## Steg 3: Pris per GB

Räkna ut vad varje GB faktiskt kostar — månadsvis.

### Förväntad output
```plaintext
Telia:   ### kr/GB
Halebop: ### kr/GB
Comviq:  ### kr/GB
```

<details><summary>Division i C# — observera</summary>

```csharp
int prisPerGb1 = pris1 / gb1;
```

Du delar två `int` vilket ger heltalsdivision. 299 / 30 = 9 (inte 9,96).  
Vill du ha decimaler behöver du `double` — men det är en annan lektion.

</details>

---

## Steg 4: Sammanfattning

Skriv ut en sammanfattande rad: "Billigast per månad: [operatör]"

(Du väljer själv vilket det är — ingen if-sats krävs. Räkna, titta, skriv ut.)

<details><summary>Lösningsförslag</summary>

```csharp
string op1 = "Telia";    int pris1 = 299; int gb1 = 30;
string op2 = "Halebop";  int pris2 = 199; int gb2 = 20;
string op3 = "Comviq";   int pris3 = 149; int gb3 = 10;

int total1 = pris1 * 24;
int total2 = pris2 * 24;
int total3 = pris3 * 24;

int prisPerGb1 = pris1 / gb1;
int prisPerGb2 = pris2 / gb2;
int prisPerGb3 = pris3 / gb3;

Console.WriteLine($"{op1,-8} {pris1} kr/mån  {gb1} GB");
Console.WriteLine($"{op2,-8} {pris2} kr/mån  {gb2} GB");
Console.WriteLine($"{op3,-8} {pris3} kr/mån  {gb3} GB");
Console.WriteLine();
Console.WriteLine($"{op1} 24 mån:   {total1} kr");
Console.WriteLine($"{op2} 24 mån: {total2} kr");
Console.WriteLine($"{op3} 24 mån:  {total3} kr");
Console.WriteLine();
Console.WriteLine($"{op1}:   {prisPerGb1} kr/GB");
Console.WriteLine($"{op2}: {prisPerGb2} kr/GB");
Console.WriteLine($"{op3}:  {prisPerGb3} kr/GB");
Console.WriteLine();
Console.WriteLine("Billigast per månad: Comviq");
```

</details>

## Bonusuppgift

Du streamar video och förbrukar i snitt 3 GB per dag. Räkna ut hur många dagar varje abonnemang räcker — och hur mycket extradatan kostar om du kör slut.

(Tips: Comviq tar 5 kr/GB extra. Halebop 3 kr/GB. Telia 0 kr — de bara stryper hastigheten.)
