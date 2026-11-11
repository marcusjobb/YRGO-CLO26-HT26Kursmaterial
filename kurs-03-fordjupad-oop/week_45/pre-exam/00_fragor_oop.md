# Tentafrågor — OOP-repetition

Öva inför tentan. Varje fråga har fyra alternativ: ett rätt, ett lite roligt och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — Klass

Vad är en klass?

A) Ett färdigt exemplar som finns i minnet
B) En lista med variabler, utan metoder
C) Ett rum där programmerare sitter och kodar 🏫
D) En ritning som beskriver hur objekt av en viss sort ser ut och vad de kan göra

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: D**

**A)** Det är ett objekt. Klassen är ritningen, objektet är huset.
**B)** En klass kan ha både fält och metoder.
**C)** Rimligt rum, fel betydelse.
**D)** Rätt. Klassen är ritningen. Från den kan du skapa många objekt.

</details>

---

## Fråga 2 — Objekt

Vad är ett objekt?

A) Något man kan peka på med fingret 👆
B) Ett konkret exemplar av en klass som finns i minnet, till exempel hjälten Mira
C) En metod som returnerar något
D) Själva ritningen som alla hjältar bygger på

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Nästan, men inte i C#.
**B)** Rätt. Objektet skapas från klassen. Många objekt kan komma från samma klass.
**C)** Metoder gör saker. Objekt är sakerna.
**D)** Det är klassen.

</details>

---

## Fråga 3 — Property

I koden `public string Name { get; set; }`, vad är `Name`?

A) En metod
B) En kryddblandning 🧂
C) En property, som läser och sätter ett värde via get och set
D) Ett fält

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Metoder har parenteser och gör saker.
**B)** Gott, men inte i C#.
**C)** Rätt. En property ser ut som ett fält utifrån men styr åtkomsten med get och set.
**D)** Ett fält är en variabel direkt i klassen, utan get och set.

</details>

---

## Fråga 4 — Fält

I klassen står `private int _health;`. Vad är `_health`?

A) Ett fält, en variabel som hör till objektet
B) En konstruktor
C) En property
D) Hälsan som hjälten tappar när hen är hungrig 🍔

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Fält lagrar objektets data. Ofta private så att utsidan går via en property.
**B)** En konstruktor har samma namn som klassen.
**C)** En property har get och/eller set med klammerparenteser.
**D)** Spelmässigt, men inte vad kompilatorn tycker.

</details>

---

## Fråga 5 — Metod

Vad är en metod?

A) Ett sätt att skapa nya klasser
B) Ett recept som alltid ger samma kaffe ☕
C) Kod med ett namn som utför något och som hör till en klass
D) En variabel som lagrar text

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Klasser skapar du med nyckelordet class.
**B)** Ett bra recept. Men det är inte en metod.
**C)** Rätt. Till exempel `Attack()` eller `Heal()`. Metoder är det objekten kan göra.
**D)** Det är en variabel eller ett fält.

</details>

---

## Fråga 6 — Konstruktor

Vad gör en konstruktor?

A) Bygger ett hus av klossar 🧱
B) Körs när ett objekt skapas och sätter upp objektets startvärden
C) Tar bort objektet när det är klart
D) Skriver ut objektet i konsolen

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Bra för kidsen, fel för C#.
**B)** Rätt. Konstruktorn har samma namn som klassen och körs automatiskt vid `new`.
**C)** Det sköts av garbage collectorn i C#.
**D)** Utskrift gör du med till exempel Console.WriteLine.

</details>

---

## Fråga 7 — new

Vad gör `new Hero("Mira")`?

A) Ger Mira ett nytt liv 🎮
B) Skapar en ny klass som heter Mira
C) Skapar ett nytt objekt av klassen Hero och anropar konstruktorn
D) Tar bort hjälten Mira

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Det är spelmekanik, inte C#.
**B)** Klasser deklareras med `class`, inte med `new`.
**C)** Rätt. `new` skapar objektet i minnet och kör konstruktorn.
**D)** Ingenting tas bort.

</details>

---

## Fråga 8 — Arv

Vad betyder `class Wizard : Hero`?

A) Wizard och Hero blir exakt samma klass
B) Wizard ärver från Hero och får Heros fält och metoder
C) Wizard flyttar in hos Hero 🏠
D) Hero ärver från Wizard

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** De är två olika klasser, men Wizard bygger på Hero.
**B)** Rätt. Kolonet betyder arv. Wizard kan dessutom lägga till egna saker.
**C)** Mysigt, men inte vad kolonet gör.
**D)** Det är tvärtom: den som står efter kolonet är basklassen.

</details>

---

## Fråga 9 — Basklass och barnklass

I `class Dog : Animal`, vilken är basklassen?

A) Ingen, hundar behöver ingen bas 🐕
B) Dog
C) Animal
D) Båda är basklasser

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Alla hundar behöver en bas, om än bara en kudde.
**B)** Dog är barnklassen, den som ärver.
**C)** Rätt. Basklassen är den som ärvs från. Dog är barnklassen.
**D)** En klass är antingen bas eller barn i just den relationen.

</details>

---

## Fråga 10 — virtual och override

Vad gör `virtual` och `override`?

A) `virtual` betyder att metoden bara finns i VR 🥽
B) `virtual` gör metoden privat och `override` döljer den
C) `override` tar bort metoden från basklassen
D) `virtual` låter barnklasser ersätta metoden, och `override` är barnklassens ersättning

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: D**

**A)** Verkligheten är trevligare än så.
**B)** De ändrar inte åtkomsten, de styr om metoden kan ersättas.
**C)** Basklassens metod finns kvar. Barnklassen använder sin egen.
**D)** Rätt. Basklassen markerar med virtual, barnklassen skriver sin egen version med override.

</details>

---

## Fråga 11 — Polymorfism

Vad är polymorfism?

A) Samma anrop ger olika beteende beroende på vilket objekt som svarar
B) Att en metod har många parametrar
C) Att en klass ärver från flera klasser
D) Att en trollkarl kan förvandla sig till en groda 🐸

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Du anropar `Attack()` på en lista med hjältar och varje typ gör sin egen attack.
**B)** Det är inte poängen. Polymorfism handlar om typerna.
**C)** C# tillåter bara en basklass. Interfaces kan däremot vara flera.
**D)** Magiskt, men bara i spelet.

</details>

---

## Fråga 12 — abstract class

Vad gäller för en `abstract class`?

A) Den är abstrakt som konst, ingen förstår den 🎨
B) Den får inte ha några metoder
C) Den kan bara användas en gång i programmet
D) Du kan inte skapa objekt av den direkt, den är en ritning för barnklasser

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: D**

**A)** Vi förstår den, men vi kan inte `new`:a den.
**B)** Den får ha både vanliga och abstrakta metoder.
**C)** Den kan ärvas av hur många klasser som helst.
**D)** Rätt. Abstrakta klasser används som gemensam bas. Du skapar objekt av barnklasserna.

</details>

---

## Fråga 13 — interface

Vad är ett `interface`?

A) Ett löfte om vad en klass kan göra, vilka metoder den har, utan att säga hur
B) En klass som du kan skapa objekt av
C) En fil som lagrar data
D) En app med snygg design som ingen använder 📱

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Klassen som implementerar interfacet måste själv skriva hur.
**B)** Du kan inte göra `new` på ett interface.
**C)** Interface lagrar inget, det beskriver bara kontraktet.
**D)** Det vore ett UI, inte ett interface i C#.

</details>

---

## Fråga 14 — Arv eller interface

När passar arv, och när passar interface?

A) Arv för "kan göra", interface för "är en"
B) Alltid arv, interface är bara för skojs skull 🎪
C) Arv för fält och interface för metoder
D) Arv för "är en" (Dog är ett Animal), interface för "kan göra" (kan skälla)

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: D**

**A)** Det är tvärtom.
**B)** Interface är ett viktigt verktyg, särskilt i designmönster.
**C)** Båda kan ha metoder, men det är relationen som avgör.
**D)** Rätt. Tumregel: "är en" ger arv, "kan göra" ger interface.

</details>

---

## Fråga 15 — OfType

Vad gör `animals.OfType<Dog>()`?

A) Skapar en ny hund i listan
B) Plockar ut bara de objekt i listan som är av typen Dog
C) Frågar hundarna vad de heter 🐕
D) Gör om alla djur till hundar

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Det skapar ingenting, det filtrerar.
**B)** Rätt. OfType filtrerar på typ och returnerar bara de som matchar.
**C)** Det vore trevligt, men nej.
**D)** Typerna ändras inte, objekten är som de var.

</details>

---

## Fråga 16 — I..Able

Varför heter interface ofta `IMeowAble` eller `IBarkAble`?

A) I:et visar att det är ett interface, och -Able säger vad klassen kan göra
B) För att katter inte kan skälla 🐈
C) Kompilatorn kräver att namnet börjar med I
D) I står för instans och Able för abstract

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Det är en namnkonvention i C#: `I` först, sedan förmågan.
**B)** Sant, men inte därför de heter så.
**C)** Det är en konvention, inte ett krav.
**D)** Det är inte vad bokstäverna står för.

</details>
