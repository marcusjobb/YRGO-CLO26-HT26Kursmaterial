# Träningsuppgifter: Test & Kvalitet — Vecka 1

> **Tema:** Clean code, kodkvalitet och verktyg  
> **Modul:** 01 — Clean code, 06 — Kodkvalitet

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är Clean Code?

a. Kod som är tvättad och ren från kommentarer<br>b. Riktlinjer för att skapa läsbar, underhållbar och förståelig kod — inte regler, utan rekommendationer<br>c. Kod som bara använder ett fåtal bibliotek<br>d. Kod som körs snabbast möjligt

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Riktlinjer för att skapa läsbar, underhållbar och förståelig kod — inte regler, utan rekommendationer

  **Förklaringar:**

  - ❌ **a) Utan kommentarer** - FEL: Bra kommentarer är en del av Clean Code
  - ✅ **b) Läsbar och underhållbar kod** - **RÄTT**: Clean Code är riktlinjer från programmerare till programmerare. Inte lagar — förslag. I projekt bestämmer man gemensam standard innan man börjar
  - ❌ **c) Få bibliotek** - FEL: Clean Code handlar om kodstil och struktur, inte antal bibliotek
  - ❌ **d) Snabbast** - FEL: Clean Code prioriterar läsbarhet, inte prestanda (även om de ofta sammanfaller)
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är C#-konventionen för metodnamn?

a. camelCase — `calculateTotal`<br>b. PascalCase — `CalculateTotal`<br>c. snake_case — `calculate_total`<br>d. Inga regler

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** PascalCase — `CalculateTotal`

  **Förklaringar:**

  - ❌ **a) camelCase** - FEL: Det används för metodvariabler och parametrar
  - ✅ **b) PascalCase** - **RÄTT**: Metoder, klasser, properties, const-värden och enums använder PascalCase i C#. `public void CalculateTotal()`, `class Person`, `public string Name { get; set; }`
  - ❌ **c) snake_case** - FEL: Används inte i C# (förekommer i Python, Ruby)
  - ❌ **d) Inga regler** - FEL: C# har etablerade namnkonventioner som alla följer
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är rätt C#-konvention för interface-namn?

a. `PersonInterface`<br>b. `IPerson`<br>c. `Person_I`<br>d. `_Person`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `IPerson`

  **Förklaringar:**

  - ❌ **a) PersonInterface** - FEL: Onödigt långt. I C# använder vi I-prefix
  - ✅ **b) IPerson** - **RÄTT**: `I` prefix är standard i C#: `IRepository`, `IUserService`, `IDisposable`. Det gör interfaces omedelbart igenkännbara
  - ❌ **c) Person_I** - FEL: Suffix är inte C#-konvention. Prefix I är standard
  - ❌ **d) _Person** - FEL: Understreck prefix används för privata fält, inte interfaces
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är en bra tumregel för kodlängd per rad?

a. Max 200 tecken<br>b. Om raden är längre än halva skärmen är den för lång<br>c. Max 5 ord<br>d. Det finns ingen begränsning

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Om raden är längre än halva skärmen är den för lång

  **Förklaringar:**

  - ❌ **a) 200 tecken** - FEL: De flesta kodstandarder rekommenderar 80-120 tecken
  - ✅ **b) Halva skärmen** - **RÄTT**: En bra praktisk regel. Om du måste scrolla horisontellt för att läsa koden är den för lång. Bryt upp i flera rader
  - ❌ **c) 5 ord** - FEL: Alldeles för kort — det skulle göra koden onödigt svårläst
  - ❌ **d) Ingen begränsning** - FEL: Långa rader är svåra att läsa och förstå
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är problemet med hårdkodade värden som `if (age > 65)`?

a. Ingenting — det fungerar utmärkt<br>b. Talet 65 är ett "magiskt tal" — det är otydligt varför just 65 och svårt att ändra<br>c. 65 är för högt<br>d. Det går inte att kompilera

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Talet 65 är ett "magiskt tal" — det är otydligt varför just 65 och svårt att ändra

  **Förklaringar:**

  - ❌ **a) Fungerar utmärkt** - FEL: Det fungerar, men är dålig kodkvalitet
  - ✅ **b) Magiskt tal** - **RÄTT**: Varför 65? Är det pensionsålder? Rabatt för senior? Använd en konstant: `const int RetirementAge = 65`. Då är koden självdokumenterande och lätt att ändra på ett ställe
  - ❌ **c) För högt** - FEL: Det numeriska värdet är inte problemet
  - ❌ **d) Kompilerar inte** - FEL: Det kompilerar, men är dålig stil
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

I vilken ordning ska medlemmar organiseras i en C#-klass enligt Clean Code?

a. Publika metoder först, privata sist<br>b. Konstruktor, variabler, properties, publika metoder, privata metoder, statiska metoder<br>c. Slumpmässig ordning<br>d. Alfabetisk ordning

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Konstruktor, variabler, properties, publika metoder, privata metoder, statiska metoder

  **Förklaringar:**

  - ❌ **a) Publika först** - FEL: Konstruktorn och fält kommer före metoder
  - ✅ **b) Standardordning** - **RÄTT**: En konsekvent ordning gör klassen förutsägbar att läsa. Konstruktor först (hur skapas objektet?), sen fält/properties, sen metoder (publika → privata → statiska)
  - ❌ **c) Slumpmässig** - FEL: Gör klassen svår att navigera
  - ❌ **d) Alfabetisk** - FEL: Logisk ordning (konstruktor först) är viktigare än alfabetisk
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är en POCO (Plain Old CLR Object)?

a. En klass utan arv från något ramverk — enkel databärare med properties<br>b. En klass som ärvt från DbContext<br>c. En klass med endast statiska metoder<br>d. En databas-tabell

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En klass utan arv från något ramverk — enkel databärare med properties

  **Förklaringar:**

  - ✅ **a) Enkel databärare** - **RÄTT**: `public class Person { public string Name { get; set; } }` — en POCO är en ren klass som inte ärver från något ramverk. Används ofta för DTOs och entiteter
  - ❌ **b) Ärvt DbContext** - FEL: DbContext är från Entity Framework, inte en POCO
  - ❌ **c) Statiska metoder** - FEL: POCOs har oftast bara properties, inga metoder (eller väldigt få)
  - ❌ **d) Databastabell** - FEL: En POCO är en C#-klass som kan *representera* en tabell, men är inte tabellen själv
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är "Rubber Duck Debugging"?

a. Att debugga med en gummianka — förklara koden högt för ankan för att hitta felet själv<br>b. Att använda en debugger med anka-tema<br>c. Ett testverktyg för C#<br>d. Att be en kollega att titta på koden

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att debugga med en gummianka — förklara koden högt för ankan för att hitta felet själv

  **Förklaringar:**

  - ✅ **a) Förklara för ankan** - **RÄTT**: Sätt en gummianka på skrivbordet. När du fastnar, förklara problemet högt för ankan, rad för rad. Oftast hittar du felet själv mitt i förklaringen — utan att fråga någon annan
  - ❌ **b) Anka-tema debugger** - FEL: Det är en teknik, inte ett verktyg
  - ❌ **c) Testverktyg** - FEL: Det är en debugging-teknik, inte ett testverktyg
  - ❌ **d) Fråga kollega** - FEL: Poängen är att du ska lösa det själv genom att formulera problemet
</details>

<div style="text-align: container; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
