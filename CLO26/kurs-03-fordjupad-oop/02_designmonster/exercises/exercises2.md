---

title: Fler övningar på variabler
author: Marcus Ackre Medina
type: exercise
topic: oop
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/Assignments/OOP/excersises/variables/exercises2.md"
description: "Bakgrundshistoria:** Han Solo vill beräkna den genomsnittliga hastigheten han behöver för att slutföra Kessel Run på rekordtid. Hjälp honom att skapa variabler för sträckan och tiden det tar och räkna"
tags: ["csharp", "exercise", "exercises2", "fler", "oop", "variabler", "visual-studio", "övningar"]
week_fit: []
---

# Fler övningar på variabler

🔴


## Han Solo's Kessel Run

**Bakgrundshistoria:** Han Solo vill beräkna den genomsnittliga hastigheten han behöver för att slutföra Kessel Run på rekordtid. Hjälp honom att skapa variabler för sträckan och tiden det tar och räkna ut hastigheten i ljusår per timme.

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
static void Main(string[] args) {
    // Skapa variabler för sträckan (i ljusår) och tiden (i timmar) här
    // Räkna ut hastigheten (sträcka / tid) här
    // Skriv ut hastigheten här
}
```

<details><summary><b>Tips 1</b></summary>
För att räkna ut genomsnittlig hastighet, använd formeln: hastighet = sträcka / tid.
</details></br>
<details><summary><b>Tips 2</b></summary>
Datatypen `double` är mest lämplig för detta eftersom vi vill ha decimalprecision när vi räknar ut hastigheten. Alternativt kan `float` användas, men `double` har högre precision.
</details></br>
<details><summary><b>Lösning</b></summary>

```csharp
static void Main(string[] args) {
    double kesselRunDistance = 12.5; // Sträcka i ljusår
    double kesselRunTime = 0.5; // Tid i timmar
    double averageSpeed = kesselRunDistance / kesselRunTime;
    Console.WriteLine($"För att slutföra Kessel Run på rekordtid måste Han Solo flyga i en hastighet av {averageSpeed} ljusår per timme.");
}
```

</details><br>

---

## Jedi Training Age

**Bakgrundshistoria:** En ung padawan vill räkna ut vid vilken ålder hen kommer att bli en fullfjädrad Jedi. Hjälp hen att skapa variabler för ålder och träningsår, och räkna ut åldern när träningsperioden är klar.

```csharp
static void Main(string[] args) {
    // Skapa variabler för ålder och träningsår här
    // Räkna ut ålder när träningsperioden är klar här
    // Skriv ut åldern här
}
```

<details><summary><b>Tips 1</b></summary>
För att räkna ut vid vilken ålder padawanen kommer att bli en fullfjädrad Jedi, använd formeln: slutålder = nuvarande ålder + träningsår.
</details></br>
<details><summary><b>Tips 2</b></summary>
Använd `int` datatypen eftersom ålder och träningsår är hela tal.
</details></br>
<details><summary><b>Lösning</b></summary>

```csharp
static void Main(string[] args) {
    int padawansAge = 18; // Ålder
    int trainingYears = 7; // Antal år av träning
    int jediAge = padawansAge + trainingYears;
    Console.WriteLine($"Padawan kommer att bli en fullfjädrad Jedi vid ålder {jediAge} år.");
}
```

</details><br>

---

## Millennium Falcon Cargo

**Bakgrundshistoria:** Millennium Falcon ska lasta ombord last för en handelsresa. Skapa variabler för lastens vikt och rymden i fartyget, och beräkna hur mycket utrymme som är kvar.

```csharp
static void Main(string[] args) {
    // Skapa variabler för lastens vikt (i ton) och rymd i fartyget (i ton) här
    // Räkna ut utrymmet som är kvar (rymd - lastens vikt) här
    // Skriv ut utrymmet här
}
```

<details><summary><b>Tips 1</b></summary>
För att räkna ut hur mycket utrymme som är kvar i Millennium Falcon efter att lasten har lagts till, använd formeln: kvarvarande utrymme = totalt utrymme - lastens vikt.
</details></br>
<details><summary><b>Tips 2</b></summary>
Använd `double` datatypen eftersom vikt och rymd kan vara decimaltal. Alternativt kan `float` användas, men `double` har högre precision.
</details></br>
<details><summary><b>Lösning</b></summary>

```csharp
static void Main(string[] args) {
    double cargoWeight = 10.5; // Lastens vikt i ton
    double shipCapacity = 20.0; // Fartygets rymd i ton
    double remainingSpace = shipCapacity - cargoWeight;
    Console.WriteLine($"Det finns {remainingSpace} ton utrymme kvar i Millennium Falcon.");
}
```

</details><br>

---

## Rebel Alliance Funding

**Bakgrundshistoria:** Rebel Alliance samlar in pengar för att bekämpa Imperiet. Skapa variabler för det aktuella finansieringsbeloppet och målet, och beräkna hur mycket som saknas för att nå målet.

```csharp
static void Main(string[] args) {
    // Skapa variabler för aktuellt finansieringsbelopp och målet (i miljoner credits) här
    // Beräkna bristen på finansiering (målet - aktuellt belopp) här
    // Skriv ut bristen här
}
```

<details><summary><b>Tips 1</b></summary>
För att räkna ut hur mycket pengar som saknas för att nå målet, använd formeln: brist = målbelopp - aktuellt finansieringsbelopp.
</details></br>
<details><summary><b>Tips 2</b></summary>
Använd `double` datatypen eftersom beloppet kan vara ett decimaltal. Alternativt kan `

float`användas, men`double` har högre precision.

</details></br>
<details><summary><b>Lösning</b></summary>

```csharp
static void Main(string[] args) {
    double currentFunding = 50.0; // Aktuellt finansieringsbelopp i miljoner credits
    double fundingGoal = 100.0; // Målbelopp i miljoner credits
    double fundingShortage = fundingGoal - currentFunding;
    Console.WriteLine($"Rebel Alliance saknar {fundingShortage} miljoner credits för att nå sitt mål.");
}
```

</details><br>

---

## Darth Vader's Breath Duration

**Bakgrundshistoria:** Darth Vader andas med hjälp av sin mask. Skapa variabler för varaktigheten av varje andetag och antalet [andetag han tar per minut](https://www.youtube.com/watch?v=SXWTmDbcOD0). Beräkna hur länge han andas varje timme.

```csharp
static void Main(string[] args) {
    // Skapa variabler för varaktigheten av varje andetag och antalet andetag per minut här
    // Beräkna den totala varaktigheten av andning per timme här
    // Skriv ut varaktigheten här
}
```

<details><summary><b>Tips 1</b></summary>
För att räkna ut den totala varaktigheten av andning per timme, använd formeln: total varaktighet = varaktighet per andetag * andetag per minut * 60.
</details></br>
<details><summary><b>Lösning</b></summary>

```csharp
static void Main(string[] args) {
    double breathDuration = 4.0; // Andningens varaktighet i sekunder
    int breathsPerMinute = 15; // Andetag per minut
    double totalBreathingDuration = breathDuration * breathsPerMinute * 60;
    Console.WriteLine($"Darth Vader andas totalt {totalBreathingDuration} sekunder varje timme.");
}
```

</details><br>

---

## Lightsaber Duel Outcome\*\*

(Avancerad)

**Bakgrundshistoria:** Två Jedi kämpar i en duell med sina ljussablar. Varje Jedi har en kraftnivå. Den med högre kraftnivå vinner. Bestäm vinnaren!

```csharp
static void Main(string[] args) {
    // Skapa variabler för de två Jedi's kraftnivåer här
    // Jämför kraftnivåerna och bestäm vinnaren här
    // Skriv ut vinnaren här
}
```

<details><summary><b>Tips 1</b></summary>
Du kan använda `if`, `else if` och `else` för att jämföra kraftnivåerna och avgöra vinnaren.
</details></br>
<details><summary><b>Lösning</b></summary>

```csharp
static void Main(string[] args) {
    int jediOnePower = 75; // Kraftnivå för första Jedi
    int jediTwoPower = 85; // Kraftnivå för andra Jedi

    if (jediOnePower > jediTwoPower) {
        Console.WriteLine("Första Jedi vinner duellen!");
    } else if (jediTwoPower > jediOnePower) {
        Console.WriteLine("Andra Jedi vinner duellen!");
    } else {
        Console.WriteLine("Det är oavgjort!");
    }
}
```

</details><br>

---

## Ewok Population Growth

**Bakgrundshistoria:** Ewokerna på Endor upplever en befolkningsökning. Skapa variabler för nuvarande befolkning och årlig tillväxtprocent. Beräkna befolkningen om 5 år.

```csharp
static void Main(string[] args) {
    // Skapa variabler för nuvarande befolkning och årlig tillväxtprocent här
    // Beräkna befolkningen om 5 år här
    // Skriv ut den förväntade befolkningen här
}
```

<details><summary><b>Tips 1</b></summary>
För att räkna ut befolkningen om 5 år med en konstant tillväxtprocent, använd formeln: framtida befolkning = nuvarande befolkning * (1 + tillväxtprocent) ^ 5.
</details></br>
<details><summary><b>Lösning</b></summary>

```csharp
static void Main(string[] args) {
    double currentPopulation = 5000; // Nuvarande Ewok-befolkning
    double growthRate = 0.05; // 5% årlig tillväxt
    double futurePopulation = currentPopulation * Math.Pow((1 + growthRate), 5);
    Console.WriteLine($"Befolkningen om 5 år kommer att vara ungefär {Math.Round(futurePopulation)} Ewoks.");
}
```

</details><br>

---

## Galactic Credits Exchange

**Bakgrundshistoria:** En handlare i Mos Eisley vill konvertera sina galaktiska krediter till en annan valuta. Hjälp honom med konverteringen.

```csharp
static void Main(string[] args) {
    // Skapa variabler för mängden galaktiska krediter och växelkursen här
    // Beräkna mängden av den nya valutan här
    // Skriv ut mängden av den nya valutan här
}
```

<details><summary><b>Tips 1</b></summary>
För att konvertera galaktiska krediter till en annan valuta, använd formeln: ny valuta = galaktiska krediter * växelkurs.
</details></br>
<details><summary><b>Lösning</b></summary>

```csharp
static void Main(string[] args) {
    double galacticCredits = 1000; // Mängden galaktiska krediter
    double exchangeRate = 0.75; // Växelkursen till den nya valutan
    double newCurrency = galacticCredits * exchangeRate;
    Console.WriteLine($"För {galacticCredits} galaktiska krediter får handlaren {newCurrency} av den nya valutan.");
}
```

</details><br>

---

## Chewbacca's Roar Duration

**Bakgrundshistoria:** Chewbacca är känd för sitt karakteristiska vrål. Mät hur länge hans vrål varar i sekunder och konvertera det till minuter.

```csharp
static void Main(string[] args) {
    // Skapa en variabel för vrålets varaktighet i sekunder här
    // Konvertera varaktigheten till minuter här
    // Skriv ut varaktigheten i minuter här
}
```

<details><summary><b>Tips 1</b></summary>
För att konvertera sekunder till minuter, använd formeln: minuter = sekunder / 60.
</details></br>
<details><summary><b>Lösning</b></summary>

```csharp
static void Main(string[] args) {
    int roarDurationInSeconds = 45; // Vrålets varaktighet i sekunder
    double roarDurationInMinutes = (double)roarDurationInSeconds / 60;
    Console.WriteLine($"Chewbacca's vrål varar i {roarDurationInMinutes} minuter.");
}
```

</details><br>

---

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
