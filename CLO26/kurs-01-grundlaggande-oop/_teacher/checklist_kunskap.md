# Kunskapschecklista — Kurs 1

Allt de ska kunna när kursen är klar. Används för att planera innehåll och säkerställa täckning.

✅ = finns material | 🔨 = behöver byggas | ⏭️ = tas i senare kurs

---

## 🛠️ Verktyg och miljö

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 1 | Installera och konfigurera Git | 01 | ✅ |
| 2 | Använda Git Bash som terminal (ls, cd, pwd, mkdir) | 01 | ✅ |
| 3 | Installera och använda Visual Studio / Rider | 01 | ✅ |
| 4 | Installera .NET SDK och verifiera med dotnet --version | 01 | ✅ |
| 5 | Installera VS Code med relevanta extensions | 01 | ✅ |
| 6 | Skapa och använda ett GitHub-konto | 01 | ✅ |
| 7 | Generera SSH-nyckel och koppla till GitHub | 01 | ✅ |
| 8 | Installera ett NuGet-paket (`dotnet add package Namn`) | 01 | 🔨 |
| 9 | Uppdatera ett paket (`dotnet add package Namn --version X`) | 01 | 🔨 |
| 10 | Ta bort ett paket (`dotnet remove package Namn`) | 01 | 🔨 |
| 11 | Söka paket på nuget.org och läsa dokumentation | 01 | 🔨 |
| 12 | Förstå vad ett paket är — återanvändbar kod någon annan skrivit | 01 | 🔨 |
| 13 | Förstå att cloud-SDK:er (Azure, AWS) ÄR NuGet-paket | 01 | 🔨 |
| 14 | Förstå `.csproj` PackageReference och versionsnummer | 01 | 🔨 |

> 🟣 **Överkurs (modul 05):** Skapa och publicera ett NuGet-paket
>
> *Kräver att klasser och interfaces sitter — man gör ett litet bibliotek av sin egen kod.*
>
> | | Kan göra | |
> |---|----------|---|
> | Ö | Skapa ett Class Library-projekt (`dotnet new classlib`) | 🔨 |
> | Ö | Konfigurera `.csproj` med metadata (författare, version, beskrivning) | 🔨 |
> | Ö | Bygga ett NuGet-paket (`dotnet pack`) | 🔨 |
> | Ö | Publicera till nuget.org (`dotnet nuget push`) | 🔨 |
> | Ö | Installera sitt eget paket i ett annat projekt | 🔨 |
>
> *Hook: "Du har använt andras paket hela kursen. Nu gör du ett eget."*
> *Kopplar till: SRP, inkapsling, API-design — vad exponerar du, vad gömmer du?*

---

## 🌿 Git och versionshantering

### Grundkommandon

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 10 | git init, clone, add, commit, push, pull | 01 | ✅ |
| 11 | Skriva beskrivande commit-meddelanden | 01 | ✅ |
| 12 | Skapa och använda .gitignore | 01 | ✅ |
| 13 | Förstå git log och läsa historik | 01 | 🔨 |
| 14 | Fork och klona annans repo | 01 | 🔨 |
| 15 | Lösa en enkel merge-konflikt | 01 | ✅ (demo) |

### Branches och Git Flow

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 16 | Förstå varför branches finns (isolera arbete) | 01 | 🔨 |
| 17 | Skapa och byta branch (git branch, git checkout -b) | 01 | 🔨 |
| 18 | Öppna en pull request och beskriva vad den gör | 01 | 🔨 |
| 19 | Förstå main/dev/feature/fix-flödet | kurs 3 | ⏭️ |
| 20 | Merga feature-branch via PR | kurs 3 | ⏭️ |
| 21 | Förstå full Git Flow med release-branches | kurs 3 | ⏭️ |

---

## 📝 Markdown

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 22 | Rubriker (# ## ###) | 07 | 🔨 |
| 23 | Fetstil, kursiv, kod inline | 07 | 🔨 |
| 24 | Listor (numrerade och punktade) | 07 | 🔨 |
| 25 | Kodblock med språkmarkering | 07 | 🔨 |
| 26 | Tabeller | 07 | 🔨 |
| 27 | Länkar och bilder | 07 | 🔨 |
| 28 | Skriva en README.md för ett projekt | 07 | 🔨 |

---

## 📊 Flödesscheman och UML

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 29 | Rita ett flödesschema för en algoritm (Mermaid flowchart) | 05 | 🔨 |
| 30 | Planera ett program med flödesschema INNAN man kodar | 05 | 🔨 |
| 31 | Rita ett klassdiagram med en klass (Mermaid classDiagram) | 05 | 🔨 |
| 32 | Rita arv i klassdiagram (--|>, --\|>) | 05 | 🔨 |
| 33 | Förstå association, aggregation och komposition | 05 | 🔨 |
| 34 | Läsa och förstå ett UML-diagram någon annan ritat | 05 | 🔨 |
| 35 | Rita sekvensdiagram för ett anropsflöde | kurs 3 | ⏭️ |
| 36 | Fullständig UML med alla relationstyper | kurs 3 | ⏭️ |

---

## 🔤 C# Grunder — syntax och datatyper

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 37 | Console.WriteLine och Console.ReadLine | 02 | 🔨 |
| 38 | Deklarera och tilldela variabler: int, string, bool, double, char | 02 | 🔨 |
| 39 | Använda explicita typer (int x = 10, inte var) | 02 | 🔨 |
| 40 | String interpolation ($"Hej {namn}") | 02 | 🔨 |
| 41 | Konvertera typer: int.Parse, int.TryParse, Convert.ToInt32 | 02 | 🔨 |
| 42 | Förstå skillnaden trunkering vs avrundning | 02 | 🔨 |
| 43 | Aritmetiska operatorer (+, -, *, /, %) | 02 | 🔨 |
| 44 | Jämförelseoperatorer (==, !=, <, >, <=, >=) | 02 | 🔨 |
| 45 | Logiska operatorer (&&, \|\|, !) | 02 | 🔨 |
| 46 | Indexera en sträng och förstå char (str[0]) | 02 | 🔨 |

---

## 🔑 Åtkomstnivåer

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 47 | `public` — åtkomst överallt | 05 | 🔨 |
| 48 | `private` — bara inom klassen | 05 | 🔨 |
| 49 | `protected` — klassen och ärvda klasser | 05 | 🔨 |
| 50 | `internal` — inom samma assembly | 05 | 🔨 |
| 51 | `protected internal` — assembly ELLER ärvd klass | kurs 3 | ⏭️ |
| 52 | `private protected` — assembly OCH ärvd klass | kurs 3 | ⏭️ |
| 53 | `file` (C# 11) — bara inom filen | kurs 3 | ⏭️ |

---

## 🔀 Flow control

### Villkor

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 54 | if / else if / else | 03 | 🔨 |
| 55 | Nästlade if-satser | 03 | 🔨 |
| 56 | Ternär operator (condition ? a : b) | 03 | 🔨 |
| 57 | Null-coalescing (??) och null-conditional (?.) | 03 | 🔨 |
| 58 | Relational patterns i if — `if (a is > 3 and < 10)` (C# 9) | 03 | 🔨 |
| 59 | Logical patterns — `and`, `or`, `not` i villkor | 03 | 🔨 |
| 60 | `is not null` och `is null` — modernare null-check | 03 | 🔨 |
| 61 | switch-sats (klassisk) | 03 | 🔨 |
| 62 | switch-uttryck (C# 8, expression form) | 03 | 🔨 |
| 63 | Pattern matching i switch (when, type patterns, enum patterns) | 03 | 🔨 |

### Loopar

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 64 | for-loop | 03 | 🔨 |
| 65 | while-loop | 03 | 🔨 |
| 66 | do-while-loop (körs alltid minst en gång) | 03 | 🔨 |
| 67 | foreach-loop | 03 | 🔨 |
| 68 | Nästlade loopar | 03 | 🔨 |
| 69 | break och continue | 03 | 🔨 |
| 70 | Identifiera och undvika oändlig loop | 03 | 🔨 |
| 71 | Förstå skillnaden for vs while vs do-while — när man väljer vad | 03 | 🔨 |
| 72 | For-loop-fällan: `i--` i loopkroppen när man menar `i++` → evig loop | 03 | 🔨 |
| 73 | For-loop-fällan: `for (i = 10; i > 0; i++)` — fel riktning | 03 | 🔨 |
| 74 | `foreach` hör hemma EFTER arrays/listor — introduceras i modul 06 | 06 | 🔨 |
| 75 | `while (true)` och `for (;;)` som medvetna eviga loopar (servrar, spel) | 03 | 🔨 |

---

## 🔧 Metoder

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
### Returtyper och parametrar

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 76 | Void-metod — gör något, returnerar ingenting | 04 | 🔨 |
| 77 | Metod med parametrar — tar emot int, string, bool | 04 | 🔨 |
| 78 | Metod med returvärde — returnerar int, string, bool | 04 | 🔨 |
| 79 | Metod som tar emot och returnerar ett objekt (egen klass) | 04 | 🔨 |
| 80 | Metod som tar emot en `List<T>` | 04 | 🔨 |
| 81 | Metod som returnerar en `List<T>` | 04 | 🔨 |
| 82 | Metod som returnerar en tuple `(int, string)` | 04 | 🔨 |
| 83 | Förstå skillnaden returtyp vs `out`-parameter | 04 | 🔨 |
| 84 | Anropa metoder från Main | 04 | 🔨 |
| 85 | Förstå scope (lokala variabler) | 04 | 🔨 |
| 86 | Dela upp ett program i flera metoder (SRP) | 04 | 🔨 |
| 87 | `out`-parameter — förstå `int.TryParse(s, out int n)` | 04 | 🔨 |
| 88 | `ref`-parameter — skicka in och ändra ett värde | 04 | 🔨 |
| 89 | Skillnaden `out` vs `ref` vs vanlig parameter | 04 | 🔨 |
| 90 | `params` — metod som tar valfritt antal argument | 04 | 🔨 |
| 91 | `params` med samlingstyper (C# 14) | 04 | 🔨 |
| 92 | Rekursion — metod som anropar sig själv | 04 | 🔨 |
| 93 | Basfall — utan det: oändlig rekursion / StackOverflow | 04 | 🔨 |
| 94 | Tuple som returvärde `(int, string)` | 04 | 🔨 |
| 95 | Namngivna tuples `(int Width, int Height)` | 04 | 🔨 |
| 96 | Dekonstruera tuple `var (w, h) = GetSize()` | 04 | 🔨 |

### Expression-bodied methods — en rad

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 97 | Gammal stil: `void Say() { Console.WriteLine("Hej"); }` | 04 | 🔨 |
| 98 | Modern stil: `void Say() => Console.WriteLine("Hej");` (C# 6) | 04 | 🔨 |
| 99 | Med returvärde: `string GetName() => "Marcus";` | 04 | 🔨 |
| 100 | Förstå att `=>` i metoder och properties är samma koncept | 04 | 🔨 |

> 💡 `params` är varför `Console.WriteLine("Hej {0} och {1}", namn1, namn2)` fungerar.
> C# 14 utökar params till att fungera med alla samlingstyper — inte bara arrayer.

> 🟣 **Överkurs (vecka 2):** Anonyma metoder
>
> | | Kan göra | |
> |---|----------|---|
> | Ö | Förstå vad en anonym metod är — en funktion utan namn | 🔨 |
> | Ö | Gammal stil: `delegate(int x) { return x * 2; }` | 🔨 |
> | Ö | Modern stil: `x => x * 2` (lambda) | 🔨 |
> | Ö | Spara en lambda i `Action<T>` eller `Func<T, TResult>` | 🔨 |
> | Ö | Förstå att LINQ-metoderna tar anonyma metoder som argument | 🔨 |
>
> *Hook: "Vad om du kunde skicka ett recept till en metod istället för ett värde?"*

> 🟣 **Överkurs (vecka 2):** Method overloads
>
> | | Kan göra | |
> |---|----------|---|
> | Ö | Skriva metoder med samma namn men olika parametrar | 🔨 |
> | Ö | Förstå hur kompilatorn väljer rätt overload | 🔨 |
> | Ö | Se varför `Console.WriteLine` kan ta int, string, bool, object... | 🔨 |
>
> *Hook: "Hur vet C# vilken WriteLine du menar? Den gör det inte — den väljer."*
> *Finns med för att overloads dyker upp överallt i inbyggda typer — bra att känna igen mönstret.*

---

## 🏗️ Klasser och OOP

### Klasstyper

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 101 | Vanlig klass — instansieras med `new` | 05 | 🔨 |
| 102 | `static class` — kan inte instansieras, bara statiska medlemmar | 05 | 🔨 |
| 103 | `abstract class` — kan inte instansieras, definierar kontrakt | 05 | 🔨 |
| 104 | `partial class` — en klass uppdelad i flera filer | 05 | 🔨 |
| 105 | `sealed class` — förhindrar arv | 05 | 🔨 |
| 106 | Private constructor — hindrar extern instansiering | 05 | 🔨 |

> 🟣 **Överkurs (vecka 3):** Design patterns — experimentera och förstå grunden
>
> *Designmönster är lösningar på återkommande problem. Du känner igen dem
> när du ser dem — och det är poängen. Ingen gör dem perfekt från början.*
>
> | | Kan göra | |
> |---|----------|---|
> | Ö | **Singleton** — en klass som bara kan ha en instans (private constructor + static instance) | 🔨 |
> | Ö | **Factory** — en metod som skapar objekt utan att exponera konstruktorn | 🔨 |
> | Ö | **Builder** — bygga ett komplext objekt steg för steg (fluent API) | 🔨 |
> | Ö | **Facade** — ett enkelt gränssnitt framför ett komplext system (t.ex. databasbyte utan att ändra affärslogik) | 🔨 |
> | Ö | Bygga en egen länkad lista från scratch — förstå vad `LinkedList<T>` gör under huven | 🔨 |
> | Ö | Se vilka patterns som redan finns inbyggda i C# (`IEnumerable` = Iterator, events = Observer) | 🔨 |
>
> *Kurs 3 fördjupar SOLID, DI och fullständiga pattern-implementationer.*

### Minnesmodell — Stack och Heap

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 107 | Förstå skillnaden värdetyp (stack) och referenstyp (heap) | 05 | 🔨 |
| 108 | Förklara vad som händer i minnet när man kopierar en `int` vs en `Cat` | 05 | 🔨 |
| 109 | Förstå varför `null` finns — och vad det betyder | 05 | 🔨 |
| 110 | `struct` som värdetyp vs `class` som referenstyp | 05 | 🔨 |

### Konstanter, readonly och statiska klasser

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 111 | `const` — kompileringskonstant, kopplar till "magiska tal" | 05 | 🔨 |
| 112 | `readonly` — sätts en gång vid körning (i konstruktor) | 05 | 🔨 |
| 113 | `static class` — kan inte instansieras (Math, Console ÄR sådana) | 05 | 🔨 |
| 114 | Förstå när man väljer statisk klass (utility/helpers) | 05 | 🔨 |

### Grundläggande klasser

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 115 | Skapa en klass med properties | 05 | 🔨 |
| 116 | Skapa instanser av en klass (new) | 05 | 🔨 |
| 117 | Förstå skillnaden instans vs statisk | 05 | 🔨 |
| 118 | Förstå inkapsling — varför private properties | 05 | 🔨 |
| 119 | `record` — oföränderlig data med inbyggd värde-jämförelse (C# 9) | 05 | 🔨 |
| 120 | `record` vs `class` — när väljer man vad | 05 | 🔨 |
| 121 | `with`-uttryck — `var kopia = katt with { Name = "Felix" }` | 05 | 🔨 |
| 122 | Objektinitierare — `new Cat { Name = "Whiskers", Age = 3 }` | 05 | 🔨 |
| 123 | `init`-only setter — property som bara sätts vid skapande | 05 | 🔨 |
| 124 | `#nullable enable` och `string?` vs `string` | 05 | 🔨 |
| 125 | Förstå kompilatorns null-varningar — null-säkerhet | 05 | 🔨 |
| 126 | Boxing och unboxing — int packad i object och tillbaka | 05 | 🔨 |

### Konstruktorer — när har man vad till vad

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 127 | Parameterlös konstruktor — `new Cat()` | 05 | 🔨 |
| 128 | Parameteriserad konstruktor — `new Cat("Whiskers", 3)` | 05 | 🔨 |
| 129 | Overloadade konstruktorer — flera `Cat(...)` för olika scenarion | 05 | 🔨 |
| 130 | Konstruktorkedja med `this(...)` — undvik kodupprepning | 05 | 🔨 |
| 131 | Optional parameters i konstruktor — `Cat(string name, int age = 0)` | 05 | 🔨 |
| 132 | Primary constructor (C# 12) — `class Cat(string Name, int Age)` | 05 | 🔨 |
| 133 | Statisk konstruktor — körs en gång när klassen laddas | 05 | 🔨 |
| 134 | Förstå NÄR man väljer vilken konstruktor-stil | 05 | 🔨 |

### Properties i detalj

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 135 | Auto-property ({ get; set; }) | 05 | 🔨 |
| 136 | Read-only property ({ get; }) | 05 | 🔨 |
| 137 | { get; private set; } — skrivbar internt, läsbar externt | 05 | 🔨 |
| 138 | Expression-bodied property (=> beräknat värde) | 05 | 🔨 |
| 139 | Property med backing field och validering i set | 05 | 🔨 |

### Arv och polymorfism

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 140 | Skriva en klass som ärver från en annan | 05 | 🔨 |
| 141 | Använda override och virtual | 05 | 🔨 |
| 142 | Förstå polymorfism med ett konkret exempel | 05 | 🔨 |
| 143 | abstract class — kan inte instansieras, definierar kontrakt | 05 | 🔨 |
| 144 | interface — kontrakt utan implementation | 05 | 🔨 |
| 145 | Förstå skillnaden abstract class vs interface | 05 | 🔨 |

### Indexers och extension methods

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 146 | Skriva en indexer i en klass (this[int index]) | 05 | 🔨 |
| 147 | Förstå vad en extension method är och varför den finns | 05 | 🔨 |
| 148 | Skriva en enkel extension method på en befintlig typ | 05 | 🔨 |
| 149 | Extension properties (C# 14 — extension members) | 05 | 🔨 |
| 150 | Se kopplingen: extension methods → fluent API → LINQ | 05 | 🔨 |

---

## 📦 Datastrukturer

### Arrays

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 151 | Deklarera och iterera en array | 06 | 🔨 |
| 152 | Indexera en array (arr[0], arr[i]) | 06 | 🔨 |
| 153 | Array.Sort, Array.Reverse, Array.IndexOf | 06 | 🔨 |
| 154 | Förstå skillnaden fast längd (array) vs dynamisk (List) | 06 | 🔨 |

### Generics — förstå \<T\> innan List\<T\>

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 155 | Förstå vad `<T>` betyder — platshållare för en typ | 06 | 🔨 |
| 156 | Förklara varför `List<int>` och `List<string>` är samma klass — DRY | 06 | 🔨 |
| 157 | Skriva en enkel generisk metod (`T Max<T>(T a, T b)`) | 06 | 🔨 |
| 158 | Skriva en enkel generisk klass | kurs 3 | ⏭️ |

### List\<T\>

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 159 | Add, Remove, Clear, Count, Contains | 06 | 🔨 |
| 160 | Indexera en lista (list[i]) | 06 | 🔨 |
| 161 | Sort, Reverse, Insert, RemoveAt | 06 | 🔨 |
| 162 | IndexOf, Find, FindAll | 06 | 🔨 |

### Queue\<T\> och Stack\<T\>

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 163 | Queue: Enqueue, Dequeue, Peek — FIFO | 06 | 🔨 |
| 164 | Stack: Push, Pop, Peek — LIFO | 06 | 🔨 |
| 165 | Förklara när Queue vs Stack passar | 06 | 🔨 |

### Dictionary\<K,V\>

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 166 | Add, ContainsKey, TryGetValue, Remove | 06 | 🔨 |
| 167 | Iterera med foreach (KeyValuePair) | 06 | 🔨 |

### Fler samlingstyper

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 168 | HashSet\<T\> — unika värden, snabb sökning | 06 | 🔨 |
| 169 | LinkedList\<T\> — billig insättning i mitten | 06 | 🔨 |
| 170 | SortedList\<K,V\> — sorterad nyckel/värde | 06 | 🔨 |
| 171 | SortedDictionary\<K,V\> — sorterat dict, snabbare insert | 06 | 🔨 |
| 172 | ObservableCollection\<T\> — notifierar vid ändringar | kurs 3 | ⏭️ |
| 173 | Förstå när man väljer varje samlingstyp | 06 | 🔨 |

### Enum

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 174 | Definiera och använda enum | 06 | 🔨 |
| 175 | Enum med explicita värden (`enum Status { Active = 1, Inactive = 2 }`) | 06 | 🔨 |
| 176 | Enum i switch och pattern matching (`case Status.Active:`) | 06 | 🔨 |
| 177 | Enum.Parse och Enum.TryParse — konvertera string till enum | 06 | 🔨 |
| 178 | Enum.GetValues — iterera alla värden | 06 | 🔨 |
| 179 | `[Flags]` enum — kombinera värden med bitvis OR | 06 | 🔨 |
| 180 | Casta int till enum och tillbaka | 06 | 🔨 |

### LINQ — de fem grundläggande

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 181 | `.Where()` — filtrera en samling | 06 | 🔨 |
| 182 | `.Select()` — omvandla varje element | 06 | 🔨 |
| 183 | `.ToList()` — konvertera LINQ-resultat till lista | 06 | 🔨 |
| 184 | `.FirstOrDefault()` — hämta första träffen eller null | 06 | 🔨 |
| 185 | `.Any()` — finns det något som matchar? | 06 | 🔨 |
| 186 | Förstå att LINQ är extension methods på IEnumerable | 06 | 🔨 |
| 187 | LINQ med lambda: `list.Where(x => x.Age > 18)` | 06 | 🔨 |

### Övrigt

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 188 | Välja rätt datastruktur för ett givet problem | 06 | 🔨 |
| 189 | Motivera sitt val av datastruktur | 06 | 🔨 |
| 190 | yield return för lat iteration | kurs 3 | ⏭️ |

> 🟣 **Överkurs (vecka 4):** Array-algoritmer
>
> | | Kan göra | |
> |---|----------|---|
> | Ö | Dela en array i två delar vid ett index | 🔨 |
> | Ö | Slå samman två arrayer till en | 🔨 |
> | Ö | Rotera en array (flytta sista elementet till första) | 🔨 |
> | Ö | Ta bort dubletter utan HashSet (manuell algoritm) | 🔨 |
> | Ö | Förstå att `Array.Copy` och `Span<T>` gör detta mer effektivt | 🔨 |

---

## 🧮 Algoritmer

> Enkla algoritmer på lektionen — svårare i överkurs.
> Fokus: förstå *hur* man tänker, inte bara att det finns en inbyggd metod.
>
> 🎬 **AlgoRythmics** — visa videon INNAN du förklarar algoritmen.
> Studerande har en visuell/kinestetisk bild av vad algoritmen gör *innan* koden.
> Kanal: https://www.youtube.com/@AlgoRythmics
> Spellista sortering: https://www.youtube.com/watch?v=R8bM6pxlrLY&list=PLcX11VWS1PdDhBYsMe5pR3FnOdYXHYkTE

### Förberedelse — innan sökning

> Studerande måste ha *känt på* skillnaden mellan strategier innan de ser algoritmen.

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 191 | Övning: "Gissa ett tal" — datorn tänker på ett tal, du gissar | 03 | 🔨 |
| 192 | Övning: spela med olika strategier (slumpmässigt, uppifrån, mitten) | 03 | 🔨 |
| 193 | Reflektera: vilken strategi krävde färst gissningar — och varför? | 03 | 🔨 |
| 194 | Övning: "Tänk på ett tal — datorn gissar" (datorn kör binary search) | 06 | 🔨 |
| 195 | Se att datorn alltid hittar svaret på max 7 gissningar (1–100) | 06 | 🔨 |

> *Övning 191–193 hör hemma i modul 03 (loopar + if). Övning 194–195 direkt innan binärsökningsteorin i 06.*
> *Studerande som suttit och "halverat" manuellt förstår O(log n) intuitivt — inte som formel.*

### Lektion — sökning

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 196 | Linjärsökning — hitta ett element i en lista manuellt | 06 | 🔨 |
| 197 | Binärsökning — förstå varför sorterad lista gör sökning snabbare | 06 | 🔨 |
| 198 | Förstå att inbyggda `BinarySearch` och `Contains` finns | 06 | 🔨 |

> 🎬 Linjärsökning: https://www.youtube.com/watch?v=C46QfTjVCNU (Flamenco)
> 🎬 Binärsökning: https://www.youtube.com/watch?v=iP897Z5Nerk (Flamenco)

### Lektion — enkla sorteringsalgoritmer

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 194 | Bubble sort — jämför grannar, byt plats, upprepa | 06 | 🔨 |
| 195 | Insert sort — ta ett element, sätt in på rätt plats | 06 | 🔨 |
| 196 | Select sort — hitta minsta, lägg först, upprepa | 06 | 🔨 |
| 197 | Förstå att inbyggd `Sort()` är snabbare — men varför? | 06 | 🔨 |

> 🎬 Bubble sort: https://www.youtube.com/watch?v=lyZQPjUT5B4 (Csángó-dans, 2.2M visningar)
> 🎬 Insert sort: https://www.youtube.com/watch?v=ROalU379l3U (Rumänsk folkdans, 1M visningar)
> 🎬 Select sort: https://www.youtube.com/watch?v=Ns4TPTC8whw (Zigenarfolkdans)

> 🟣 **Överkurs — Sorteringsalgoritmer (vecka 4)**
>
> | | Kan göra | |
> |---|----------|---|
> | Ö | **Merge sort** — dela och härska, O(n log n) | 🔨 |
> | Ö | **Quick sort** — pivotera och sortera, O(n log n) snitt | 🔨 |
> | Ö | **Heap sort** — prioritetskö som sorteringsverktyg | 🔨 |
> | Ö | **Shell sort** — förbättrad insert sort med gap-sekvens | 🔨 |
> | Ö | Förstå tidskomplexitet: O(n), O(n log n), O(n²) | 🔨 |
>
> 🎬 Merge sort: https://www.youtube.com/watch?v=XaqR3G_NVoo (Transylvansk-saxisk dans)
> 🎬 Quick sort: https://www.youtube.com/watch?v=ywWBy6J5gz8 (Küküllőmenti, 2.3M visningar)
> 🎬 Heap sort: https://www.youtube.com/watch?v=Xw2D9aJRBY4 (Mezőségi folkdans)
> 🎬 Shell sort: https://www.youtube.com/watch?v=CmPA7zE8mx0 (Székely-dans)

> 🟣 **Överkurs — Grafalgoritmer**
>
> | | Kan göra | |
> |---|----------|---|
> | Ö | **Dijkstras algoritm** — kortaste vägen i en viktad graf | 🔨 |
> | Ö | **Levenshtein-avstånd** — redigera ett ord till ett annat (fuzzy search) | 🔨 |
>
> *Dijkstra = Waze och Google Maps under huven.*
> *Levenshtein = Git diff, IDE-autocompletion, stavningskorrigering.*

> 🟣 **Överkurs (kurs 3+):** Plugin-arkitektur
>
> | | Kan göra | |
> |---|----------|---|
> | Ö | Förstå vad ett plugin är — ett tillägg som laddas dynamiskt | 🔨 |
> | Ö | Skriva ett interface som plugins implementerar | 🔨 |
> | Ö | Ladda en assembly dynamiskt med reflection | 🔨 |
> | Ö | Bygga ett enkelt plugin-system med `IPlugin` | 🔨 |
>
> *Kräver: interfaces, reflection, DI — tas långt fram.*

---

## ⚡ Asynkron programmering

> Allt i molnet *väntar* — nätverk, databas, lagring. async är hur man väntar utan att frysa programmet.
> Introducera konceptet + enkel `await` i kurs 1. Full parallellism i kurs 3.

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 195 | Förstå varför async finns — I/O-väntan ska inte blockera tråden | 06 | 🔨 |
| 196 | `await` på en `Task` / `Task<T>` | 06 | 🔨 |
| 197 | Skriva en `async Task`- och `async Task<T>`-metod | 06 | 🔨 |
| 198 | `async Main` — en konsol-app som gör ett async-anrop | 06 | 🔨 |
| 199 | Förstå att en `Task` är "ett löfte om ett framtida värde" | 06 | 🔨 |
| 200 | `Task.WhenAll`, parallellism, cancellation | kurs 3 | ⏭️ |

---

## 🌐 JSON, File I/O och HttpClient

### JSON — `System.Text.Json`

> Cloud-API:er pratar JSON. Egna klasser ÄR JSON-objekt sett från ett annat håll.

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 201 | `JsonSerializer.Serialize(obj)` — klass → JSON-sträng | 06 | 🔨 |
| 202 | `JsonSerializer.Deserialize<T>(json)` — JSON → klass | 06 | 🔨 |
| 203 | Förstå kopplingen property ↔ JSON-fält | 06 | 🔨 |
| 204 | Serialisera och deserialisera en `List<T>` | 06 | 🔨 |

### Fil-I/O

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 205 | `File.ReadAllText`, `WriteAllText`, `ReadAllLines` | 06 | 🔨 |
| 206 | Sökvägar — relativa vs absoluta, `Path.Combine` | 06 | 🔨 |
| 207 | `StreamReader` / `StreamWriter` med `using` | 06 | 🔨 |
| 208 | Förstå varför filer måste stängas (IDisposable, resurshantering) | 06 | 🔨 |

### HttpClient — första API-kontakten

> async + JSON + HttpClient = den första riktiga "moln-känslan". Capstone/bonus kurs 1.

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 209 | Göra ett GET-anrop mot ett publikt API | 06 | 🔨 |
| 210 | Läsa JSON-svaret och deserialisera till egen klass | 06 | 🔨 |
| 211 | Förstå statuskoder på grundnivå (200, 404, 500) | 06 | 🔨 |

---

## 🧰 Inbyggda typer och metoder

### string

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 212 | Length, ToUpper, ToLower, Trim | 02 | 🔨 |
| 213 | Contains, StartsWith, EndsWith | 02 | 🔨 |
| 214 | Substring, Replace | 02 | 🔨 |
| 215 | Split och string.Join | 02 | 🔨 |
| 216 | IndexOf, LastIndexOf | 02 | 🔨 |
| 217 | string.IsNullOrEmpty, IsNullOrWhiteSpace | 02 | 🔨 |
| 218 | Förstå att string är immutable | 02 | 🔨 |
| 219 | `StringBuilder` — bygga strängar effektivt i loopar | 02 | 🔨 |
| 220 | Verbatim-sträng `@"C:\Users\sökväg"` | 02 | 🔨 |
| 221 | Raw string literals `"""..."""` (C# 11) — perfekt för JSON/HTML | 02 | 🔨 |
| 222 | `char.IsDigit`, `IsLetter`, `IsWhiteSpace`, `ToUpper` | 02 | 🔨 |

### Math

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 223 | Math.Abs, Math.Max, Math.Min | 02 | 🔨 |
| 224 | Math.Round, Math.Floor, Math.Ceiling | 02 | 🔨 |
| 225 | Math.Pow, Math.Sqrt | 02 | 🔨 |
| 226 | Math.PI, Math.E | 02 | 🔨 |

### Övriga inbyggda

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 227 | Console.Clear, Console.ForegroundColor | 02 | 🔨 |
| 228 | DateTime.Now, .AddDays, .ToString("format") | 02 | 🔨 |
| 229 | Random.Next, .NextDouble | 02 | 🔨 |
| 230 | int.MaxValue, int.MinValue | 02 | 🔨 |
| 231 | Environment.Exit, Environment.NewLine | 02 | 🔨 |

> 🟣 **Överkurs (vecka 2):** String format och formatters
>
> | | Kan göra | |
> |---|----------|---|
> | Ö | Format-specifiers: `{0:F2}`, `{0:C}`, `{0:D}`, `{0:X}` (hex!), `{0:P}` | 🔨 |
> | Ö | `string.Format(...)` vs `$"..."` — när väljer man vad | 🔨 |
> | Ö | `ToString("format")` på numeriska typer och DateTime | 🔨 |
> | Ö | `IFormattable` — skriva en klass vars ToString tar ett format | 🔨 |
> | Ö | Composite formatting och alignment (`{0,-10}` vänsterjustera) | 🔨 |
>
> *Hook: "Varför skriver `{0:X}` ut ett tal i hex? För att formatterare är program-inom-program."*
> *Kopplar till: hex-överkursen, `DateTime.ToString("yyyy-MM-dd")`, valutaformatering.*

---

## 🌐 Specialklasser och specialinterfaces

### object — roten till allt

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 232 | Förstå att allt i C# ärver från object | 05 | 🔨 |
| 233 | Använda GetType() och typeof() | 05 | 🔨 |
| 234 | Förstå att string är en referenstyp men beter sig som värdetyp (==) | 05 | 🔨 |

> 🟣 **Överkurs (vecka 3):** Override av inbyggda object-metoder
>
> | | Kan göra | |
> |---|----------|---|
> | Ö | Override ToString() för läsbar utskrift av egna klasser | 🔨 |
> | Ö | Använda ToString() som debuggverktyg — se objektet, inte typen | 🔨 |
> | Ö | Förstå Equals() och GetHashCode() — och varför de hänger ihop | 🔨 |
> | Ö | Implementera IEquatable\<T\> för korrekt == | 🔨 |
>
> *Hook: "Varför skriver Console.WriteLine(minKatt) ut 'MyApp.Cat' och inte 'Whiskers, 3 år'?"*
> *Praktiskt: override ToString() är ett av de snabbaste debug-verktygen som finns — se 47 objekt i en lista direkt.*

### Nullable och felhantering

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 235 | Nullable\<T\> / T? — vad null betyder | 05 | 🔨 |
| 236 | Null-conditional operator (?.) och null-coalescing (??) | 05 | 🔨 |
| 237 | try / catch / finally — grundläggande felhantering | 05 | ✅ (planerad) |
| 238 | catch med specifik Exception-typ | 05 | 🔨 |
| 239 | throw new Exception("meddelande") | 05 | 🔨 |
| 240 | Förstå stacktrace och läsa den | 05 | 🔨 |
| 241 | Skriva egna Exception-klasser | kurs 3 | ⏭️ |

### Specialinterfaces

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 242 | IEnumerable\<T\> — möjliggör foreach, förstå vad det betyder | 06 | 🔨 |
| 243 | IComparable\<T\> — implementera för att möjliggöra Sort() | 06 | 🔨 |
| 244 | IDisposable — using-satsen och resurshantering | 06 | 🔨 |
| 245 | IEquatable\<T\> — anpassad == och Equals | 06 | 🔨 |
| 246 | ICollection\<T\>, IList\<T\> — hierarkin av samlingsinterfaces | kurs 3 | ⏭️ |
| 247 | ICloneable, IFormattable, IConvertible | kurs 3 | ⏭️ |

---

## 🎯 Överkurs — Klasser som egna datatyper

> **Format:** Ingen föreläsning. Lektionen är inbakad i materialet.
> Markerat "Ej krav — ej tenta" i alla filer.
> Publiceras varje vecka för de som vill ha mer.

### Nivå 1 — Klasser som datatyper (viktomvandling + geometri)

*Studerande bygger en `Weight`-klass och en geometriklass (`Circle` eller `Rectangle`)
som fungerar som riktiga datatyper med konverteringsmetoder.*

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| Ö1 | Skapa en `Weight`-klass med värde i kg och konverteringsmetoder (lbs, g, oz) | 434 | 🔨 |
| Ö2 | Skapa en `Circle`-klass med area och omkrets som properties | 435 | 🔨 |
| Ö3 | Förstå varför en klass kan vara bättre än ett löst tal (typsäkerhet) | 436 | 🔨 |
| Ö4 | Jämföra två Weight-objekt med metoder (IsHeavierThan, Equals) | 437 | 🔨 |

### Nivå 2 — Operator overloading (extra överkurs inom överkursen)

*Lägger till +, -, ==, < på Weight och Circle. Kräver att nivå 1 sitter.*

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| Ö5 | Overloada `+` och `-` på `Weight` | 438 | 🔨 |
| Ö6 | Overloada `==` och `!=` (kräver Equals + GetHashCode) | 439 | 🔨 |
| Ö7 | Overloada `<` och `>` för viktjämförelse | 440 | 🔨 |
| Ö8 | Implicit konvertering från double till Weight | 441 | 🔨 |
| Ö9 | Förstå varför string `==` fungerar (är operator-overload) | 442 | 🔨 |

*Nivå 2 kopplar direkt till IComparable och IEquatable — studerande som når hit förstår varför interfaces finns.*

---

## ✨ Clean Code

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 248 | Namnge variabler, metoder och klasser beskrivande | löpande | 🔨 |
| 249 | Skriva korta metoder med ett ansvar (SRP) | löpande | 🔨 |
| 250 | Undvika upprepning (DRY) | löpande | 🔨 |
| 251 | Skriva kommentarer som förklarar *varför*, inte *vad* | löpande | 🔨 |
| 252 | Undvika magiska tal (namnge konstanter) | löpande | 🔨 |
| 253 | Formatera kod konsekvent (indragning, mellanrum) | löpande | 🔨 |
| 254 | Förklara andras kod — inte bara sin egen | löpande | 🔨 |

---

## 🐛 Felsökning

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 255 | Läsa ett felmeddelande och hitta var felet är | löpande | 🔨 |
| 256 | Använda breakpoints i IDE:n | löpande | 🔨 |
| 257 | Felsöka med Console.WriteLine | löpande | 🔨 |
| 258 | Googla / fråga AI med ett välformulerat felmeddelande | löpande | 🔨 |
| 259 | Förstå skillnaden compile error vs runtime error | löpande | 🔨 |

---

## 💬 Mjuka kompetenser

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| 260 | Beskriva vad ett kodblock gör i vanlig svenska | löpande | 🔨 |
| 261 | Ge och ta emot feedback på kod | löpande | 🔨 |
| 262 | Presentera ett nyckelord för klassen (5 min) | v2+ | 🔨 |
| 263 | Skriva en reflektion om vad man lärt sig | löpande | 🔨 |
| 264 | Identifiera vad man vet vs vad man antar | löpande | ✅ |

---

## Sammanfattning

| Område | Antal | Klara | ⏭️ Kurs 3+ |
|--------|-------|-------|------------|
| Verktyg | 7 | 460 | 0 |
| Git | 11 | 461 | 3 |
| Markdown | 7 | 462 | 0 |
| Flödesscheman / UML | 463 | 0 | 2 |
| C# grunder | 464 | 0 | 0 |
| Åtkomstnivåer | 7 | 465 | 3 |
| Flow control (villkor) | 466 | 0 | 0 |
| Flow control (loopar) | 467 | 0 | 0 |
| Metoder | 6 | 468 | 0 |
| Klasser / OOP | 469 | 0 | 0 |
| Klasser som datatyper (bonus) | 470 | 0 | 0 |
| Datastrukturer | 23 | 471 | 2 |
| Inbyggda typer | 472 | 0 | 0 |
| Specialklasser / interfaces | 473 | 1 | 4 |
| Clean Code | 474 | 0 | 0 |
| Felsökning | 5 | 475 | 0 |
| Mjuka kompetenser | 476 | 1 | 0 |
| **Totalt (kärnmaterial)** | **172** | **12** | **14** |
| **Bonus (operatoröverlagring)** | **6** | **0** | **0** |

*12 av 172 kärnpunkter har material. 14 skjuts till kurs 3+. 146 att bygga i kurs 1.*

---

## 🎃 Överkurs — Binär och hexadecimal matematik

> **Timing:** Placera nära Halloween (slutet av oktober) — för skämtet.
> `OCT 31 = DEC 25` → Oktaltalet 31 = 3×8+1 = 25 i decimal.
> Programmerare blandar ihop Halloween och jul. Förklara varför.

| # | Kan göra | Var | Status |
|---|----------|-----|--------|
| H1 | Förstå binärt talsystem (bas 2) | överkurs | 🔨 |
| H2 | Konvertera mellan binärt och decimalt | överkurs | 🔨 |
| H3 | Förstå hexadecimalt talsystem (bas 16, 0–F) | överkurs | 🔨 |
| H4 | Konvertera mellan hex och decimalt | överkurs | 🔨 |
| H5 | Förstå oktalt talsystem (bas 8) — för OCT 31 = DEC 25 | överkurs | 🔨 |
| H6 | Läsa en färgkod (#FF5733) och förstå vad siffrorna betyder | överkurs | 🔨 |
| H7 | Förstå chmod 755 = rwxr-xr-x i binärt | överkurs | 🔨 |
| H8 | Bitvisa operatorer (&, \|, ^, ~, <<, >>) | överkurs | 🔨 |
| H9 | Använda Convert.ToString(n, 2) och Convert.ToInt32(s, 16) i C# | överkurs | 🔨 |

*Skämtet: "Why do programmers confuse Halloween and Christmas? Because OCT 31 = DEC 25."*
*Förklaringen ÄR lektionen. Den som förstår skämtet förstår talsystem.*

---

## 🌐 Kurs-03 — Bonusmaterial: Webb (ej kursplan)

> Konsolen blir tråkig. Från kurs-03 introduceras webb som bonusmaterial —
> **inte del av kursen, inte på tentan** — men för att koden ska kännas levande.

| | Kan göra | Status |
|---|----------|--------|
| B | Förstå vad ASP.NET Core MVC är (routes, controllers, views) | 🔨 |
| B | Skapa en enkel MVC-app med en sida som visar data | 🔨 |
| B | Grundläggande HTML (tags, struktur, formulär) | 🔨 |
| B | Grundläggande CSS (selektorer, färger, layout) | 🔨 |
| B | Förstå vad Blazor är och hur det skiljer sig från MVC | 🔨 |
| B | Bygga en enkel Blazor-komponent som visar en lista | 🔨 |

*Poängen: de OOP-kunskaper studerande har från kurs-01/02/03 är direkt användbara i MVC.
Samma klasser, samma logik — fast i ett webbfönster istället för en konsol.
Det gör att kurs-04 molnkurser inte kommer som en chock.*

---

## Öppna frågor att besluta

- [ ] Git Flow (main/dev/feature/fix): bekräftat till kurs 3
- [ ] Indexers: hör hemma i kurs 1 (modul 05) eller kurs 3?
- [ ] try/catch: kurs 1 eller kurs 2?
- [ ] Extension properties (C# 14): är de redo för kurs 1-studerande?
