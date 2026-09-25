# Testa dig själv — Variabler

Inga poäng, ingen press. Välj det alternativ du tror stämmer — öppna sen förklaringen och se om du hade rätt.

---

### Fråga 1: Vad är en variabel?

- A) En databas
- B) En funktion som räknar ut ett värde
- C) En länk till en plats i minnet där ett värde sparas
- D) Inget som används i C# numera

<details><summary>Svar</summary>

**Rätt svar: C**

**A) En databas** — Fel. En databas lagrar data permanent på disk. En variabel finns bara medan programmet körs, i RAM-minnet.

**B) En funktion som räknar ut ett värde** — Fel. En funktion *gör* något. En variabel *håller* något — det är en viktig skillnad.

**C) En länk till en plats i minnet där ett värde sparas** — Rätt. När du skriver `int ålder = 25` skapas en plats i minnet. Variabelnamnet är adressen dit.

**D) Inget som används i C# numera** — Fel. Variabler är den allra grundläggandaste byggstenen i all programmering, oavsett språk.

</details>

---

### Fråga 2: Hur skapar du en variabel som håller texten "Kalle"?

- A) `text namn = "Kalle";`
- B) `string namn = "Kalle";`
- C) `int namn = "Kalle";`
- D) `namn = "Kalle";`

<details><summary>Svar</summary>

**Rätt svar: B**

**A) `text namn = "Kalle";`** — Fel. `text` är ingen datatyp i C#. Det heter `string`.

**B) `string namn = "Kalle";`** — Rätt. `string` är datatypen för text i C#. Anföringstecken runt värdet talar om att det är text.

**C) `int namn = "Kalle";`** — Fel. `int` är för heltal. Kompilatorn klagar direkt — du försöker stoppa text i en heltalsbox.

**D) `namn = "Kalle";`** — Fel. Utan datatyp vet inte C# vad `namn` är för något. Deklarationen saknas.

</details>

---

### Fråga 3: Vilken variabel hanterar decimaltal?

- A) `int pris = 9.99;`
- B) `string pris = "9.99";`
- C) `bool pris = 9.99;`
- D) `double pris = 9.99;`

<details><summary>Svar</summary>

**Rätt svar: D**

**A) `int pris = 9.99;`** — Fel. `int` hanterar bara heltal. Kompilatorn accepterar inte 9.99 — decimalen kastar bort.

**B) `string pris = "9.99";`** — Fel. Det är textsträngen "9.99", inte talet 9.99. Du kan inte räkna med den.

**C) `bool pris = 9.99;`** — Fel. `bool` är bara `true` eller `false`. Inget mellanting.

**D) `double pris = 9.99;`** — Rätt. `double` är datatypen för decimaltal i C#.

</details>

---

### Fråga 4: Vad är skillnaden mellan att *deklarera* och *tilldela* en variabel?

- A) Det är exakt samma sak — folk säger bara olika saker
- B) Deklarera skapar variabeln, tilldela ger den ett värde
- C) Tilldela skapar variabeln, deklarera ger den ett värde
- D) Man måste alltid göra båda på samma rad annars exploderar det

<details><summary>Svar</summary>

**Rätt svar: B**

**A) Det är exakt samma sak** — Fel. De *kan* ske på samma rad (`int x = 5;`) men de är två separata operationer.

**B) Deklarera skapar variabeln, tilldela ger den ett värde** — Rätt. `int x;` deklarerar. `x = 5;` tilldelar. `int x = 5;` gör båda på en gång.

**C) Tilldela skapar variabeln, deklarera ger den ett värde** — Fel. Tvärtom. Du kan inte tilldela en variabel som inte existerar ännu.

**D) Man måste alltid göra båda på samma rad annars exploderar det** — Fel. Du kan deklarera och tilldela på separata rader utan problem.

</details>

---

### Fråga 5: Vad skrivs ut?

```csharp
int x = 5;
x = 10;
Console.WriteLine(x);
```

- A) `5`
- B) `510`
- C) `x`
- D) `10`

<details><summary>Svar</summary>

**Rätt svar: D**

**A) `5`** — Fel. Värdet 5 skrevs över på rad 2. Variabeln `x` innehåller nu 10.

**B) `510`** — Fel. C# konkatenerar inte heltal automatiskt. Variabeln har ett värde i taget.

**C) `x`** — Fel (och lite roligt). `Console.WriteLine(x)` skriver ut *värdet* i variabeln, inte bokstaven x.

**D) `10`** — Rätt. Rad 2 ersätter värdet. Sista tilldelningen vinner.

</details>

---

### Fråga 6: Vad händer om du försöker lägga text i en int-variabel?

```csharp
int poäng = "hundra";
```

- A) C# konverterar texten till ett tal automatiskt
- B) Programmet kraschar när du kör det
- C) Kompilatorn klagar — koden kompilerar inte ens
- D) Ingenting — C# är okej med det

<details><summary>Svar</summary>

**Rätt svar: C**

**A) C# konverterar texten automatiskt** — Fel. C# är ett *statiskt typat* språk. Det gissar inte vad du menar.

**B) Programmet kraschar när du kör det** — Fel, men nära. Felet uppstår *innan* du kör — kompilatorn stoppar dig redan när du bygger.

**C) Kompilatorn klagar — koden kompilerar inte** — Rätt. Det är en stor fördel med C#: fel som detta hittas direkt, inte ute hos användaren.

**D) Ingenting — C# är okej med det** — Fel. C# är väldigt noga med datatyper. Det är meningen.

</details>
