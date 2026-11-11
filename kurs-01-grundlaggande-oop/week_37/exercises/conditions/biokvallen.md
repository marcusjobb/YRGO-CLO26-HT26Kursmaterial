# Övning — Biokvällen

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🔴

Du ska bygga ett program som kombinerar tre separata switch-satser och sätter ihop resultaten till ett meddelande.

Ingen inläsning från tangentbordet — byt värdena direkt i koden och kör om.

> Sitter du fast i mer än 15 minuter? Be om hjälp — det är inte ett tecken på att du inte kan, det är precis vad 15-minutersregeln är till för.

---

## Uppgiften

Deklarera tre variabler — en för varje val:

```csharp
int filmsmakVal = 1;
int skadespelerskaVal = 1;
int skadespelareVal = 1;
```

**Steg 1 — Skriv ut menyer** (bara `Console.WriteLine`, inga switch än):

```
Välj filmsmak:
1 - Musical
2 - Action
3 - Skräck
4 - Komedi
5 - Drama
6 - Romantik

Välj skådespelerska:
1 - Meryl Streep
2 - Zendaya
3 - Viola Davis

Välj skådespelare:
1 - Keanu Reeves
2 - Denzel Washington
3 - Ryan Reynolds
```

**Steg 2 — Tre switch-satser:**

Deklarera tre strängvariabler **utanför** dina switch-satser:

```csharp
string filmsmak = "";
string skadespelerska = "";
string skadespelare = "";
```

Skriv sedan tre separata switch-satser som sätter rätt värde i varje variabel baserat på valen ovan. Om ett val är ogiltigt — sätt variabeln till `"okänd"`.

**Steg 3 — Skriv ut resultatet:**

```
Missa inte den spännande [filmsmak]-filmen med [skadespelerska] och [skadespelare] på bio ikväll!
```

---

## Förväntad output för val 2, 3, 1
```plaintext
Välj filmsmak:
1 - Musical
...

Välj skådespelerska:
...

Välj skådespelare:
...

Missa inte den spännande Action-filmen med Viola Davis och Keanu Reeves på bio ikväll!
```

---

<details><summary>Tips: scope — varför strängarna måste vara utanför</summary>

Variabler som deklareras inuti en `switch` (innanför `{ }`) försvinner när switch-blocket stängs. Om du deklarerar `filmsmak` inuti switch kan du inte använda den i `Console.WriteLine` efteråt.

Lösningen: deklarera strängarna **före** den första switch-satsen.

</details>

<details><summary>Tips: tre switch-satser — inte en</summary>

Varje val behöver sin egen switch. Du kan inte blanda alla tre valen i en och samma switch.

```csharp
switch (filmsmakVal) { ... }
switch (skadespelerskaVal) { ... }
switch (skadespelareVal) { ... }

Console.WriteLine($"Missa inte den spännande {filmsmak}-filmen med {skadespelerska} och {skadespelare} på bio ikväll!");
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
int filmsmakVal = 2;
int skadespelerskaVal = 3;
int skadespelareVal = 1;

Console.WriteLine("Välj filmsmak:");
Console.WriteLine("1 - Musical");
Console.WriteLine("2 - Action");
Console.WriteLine("3 - Skräck");
Console.WriteLine("4 - Komedi");
Console.WriteLine("5 - Drama");
Console.WriteLine("6 - Romantik");
Console.WriteLine();
Console.WriteLine("Välj skådespelerska:");
Console.WriteLine("1 - Meryl Streep");
Console.WriteLine("2 - Zendaya");
Console.WriteLine("3 - Viola Davis");
Console.WriteLine();
Console.WriteLine("Välj skådespelare:");
Console.WriteLine("1 - Keanu Reeves");
Console.WriteLine("2 - Denzel Washington");
Console.WriteLine("3 - Ryan Reynolds");
Console.WriteLine();

string filmsmak = "";
string skadespelerska = "";
string skadespelare = "";

switch (filmsmakVal)
{
    case 1: filmsmak = "Musical";   break;
    case 2: filmsmak = "Action";    break;
    case 3: filmsmak = "Skräck";    break;
    case 4: filmsmak = "Komedi";    break;
    case 5: filmsmak = "Drama";     break;
    case 6: filmsmak = "Romantik";  break;
    default: filmsmak = "okänd";    break;
}

switch (skadespelerskaVal)
{
    case 1: skadespelerska = "Meryl Streep";  break;
    case 2: skadespelerska = "Zendaya";       break;
    case 3: skadespelerska = "Viola Davis";   break;
    default: skadespelerska = "okänd";        break;
}

switch (skadespelareVal)
{
    case 1: skadespelare = "Keanu Reeves";       break;
    case 2: skadespelare = "Denzel Washington";  break;
    case 3: skadespelare = "Ryan Reynolds";      break;
    default: skadespelare = "okänd";             break;
}

Console.WriteLine($"Missa inte den spännande {filmsmak}-filmen med {skadespelerska} och {skadespelare} på bio ikväll!");
```

</details>
