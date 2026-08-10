# Programmeringstermer — Strängmetoder

`string` har ett helt eget universum av metoder för att städa, leta i och bygga om text. Vi återanvänder samma exempelsträng genom hela filen — en lite slarvigt skriven `"    Bruce Wayne   "` — så du ser exakt vad varje metod faktiskt gör med samma data.

**Se även:** [string](../../01_verktyg_och_git/programmeringstermer/datatyper.md#string), [Variabler i minnet](../../01_verktyg_och_git/programmeringstermer/datatyper.md#variabler-i-minnet) — för hur `namn[0]` hänger ihop med det här.

---

## Trim()

Tar bort blanksteg (och andra "whitespace"-tecken som tab och radbrytning) i **början och slutet** av en sträng. Inget annat.

```csharp
string batman = "    Bruce Wayne   ";
Console.WriteLine(batman + "!");
Console.WriteLine(batman.Trim() + "!");
```

### Output
```plaintext
    Bruce Wayne   !
Bruce Wayne!
```

<details><summary>Varför försvann inte mellanslaget mitt i namnet?</summary>

`Trim()` bryr sig bara om kanterna — allt **före** den första bokstaven och allt **efter** den sista. Mellanslaget mellan "Bruce" och "Wayne" sitter mitt i strängen, inte i kanten, så det rör `Trim()` aldrig. Om du behöver städa mellanslag mitt i en sträng är det ett helt annat verktyg (`Replace`, se nedan) — inte `Trim()`.

</details>

**Se även:** [TrimStart() / TrimEnd()](#trimstart--trimend) — samma idé, men bara en sida i taget.

---

## TrimStart() / TrimEnd()

`Trim()` städar båda sidor på en gång. Ibland vill du bara ha en sida.

```csharp
string batman = "    Bruce Wayne   ";
Console.WriteLine("[" + batman.TrimStart() + "]");
Console.WriteLine("[" + batman.TrimEnd() + "]");
```

### Output
```plaintext
[Bruce Wayne   ]
[    Bruce Wayne]
```

Hakparenteserna är bara där för att du ska *se* var mellanslagen faktiskt försvann respektive blev kvar — de är inte en del av metoden.

---

## ToUpper() / ToLower()

Gör om hela strängen till versaler eller gemener. Påverkar bara bokstäver — siffror och tecken lämnas orörda.

```csharp
string batman = "Bruce Wayne";
Console.WriteLine(batman.ToUpper());
Console.WriteLine(batman.ToLower());
```

### Output
```plaintext
BRUCE WAYNE
bruce wayne
```

<details><summary>Vanlig fallgrop: jämföra strängar oavsett storlek på bokstäver</summary>

`"Batman" == "batman"` är `false` — C# bryr sig om versaler/gemener vid jämförelse som standard. Ett vanligt trick är att göra båda sidor till samma skick innan du jämför:

```csharp
string svar = "BATMAN";
bool ratt = svar.ToLower() == "batman".ToLower(); // true
```

Detta beror på att `'a'` har teckenkoden ([ASCII](https://www.ascii-code.com/)/Unicode-värdet) 97, medan `'A'` har värdet 65 — de är bokstavligen olika tal under ytan, precis som i [char-avsnittet i datatyper.md](../../01_verktyg_och_git/programmeringstermer/datatyper.md#char). C# jämför de underliggande talen, inte "samma bokstav oavsett skick", så `==` ser `'a'` och `'A'` som helt olika tecken.

</details>

---

## Contains()

Kollar om en sträng finns **någonstans** inuti en annan sträng. Ger tillbaka `true` eller `false` — inte var den hittades.

```csharp
string batman = "Bruce Wayne is Batman";
Console.WriteLine(batman.Contains("Batman"));
Console.WriteLine(batman.Contains("Robin"));
```

### Output
```plaintext
True
False
```

`True` betyder att "Batman" faktiskt finns någonstans i texten — det gör den (sist i strängen). `False` på rad två betyder att "Robin" inte förekommer alls. `Contains()` svarar bara ja eller nej, den bryr sig inte om *var* i strängen den hittades — för det behöver du `IndexOf()`.

**Se även:** [IndexOf()](#indexof) — om du vill veta *var* den hittades, inte bara *om*.

---

## IndexOf()

Letar upp **var** en delsträng börjar, räknat i index (kom ihåg: position 0 är första tecknet, se [Variabler i minnet](../../01_verktyg_och_git/programmeringstermer/datatyper.md#variabler-i-minnet)). Hittas den inte alls — `-1`, inte ett fel.

```csharp
string batman = "Bruce Wayne is Batman";
Console.WriteLine(batman.IndexOf("Wayne"));
Console.WriteLine(batman.IndexOf("Robin"));
```

### Output
```plaintext
6
-1
```

`-1` är inte ett skräpvärde — det är meningen. Kolla alltid om resultatet är `-1` innan du litar på det som ett riktigt index.

`6` är lite lurigt — är det den **sjätte bokstaven** eller den **sjätte positionen**? Svaret är: ingen av dem, riktigt. Räkna med fingrarna från `B` (index 0): `B`(0) `r`(1) `u`(2) `c`(3) `e`(4) `[mellanslag]`(5) `W`(6). `W` i "Wayne" är faktiskt den **sjunde** bokstaven i strängen om du räknar i ordningen 1, 2, 3... men dess **index** är 6, för index räknar **steg från start** (se [Variabler i minnet](../../01_verktyg_och_git/programmeringstermer/datatyper.md#variabler-i-minnet)) — inte vilken bokstav i ordningen det är. `IndexOf()` ger dig alltid antal steg, inte en plats i en numrerad lista.

---

## Replace()

Byter ut **alla** förekomster av en delsträng mot en annan. Inte bara den första — alla.

```csharp
string batman = "Bruce Wayne is Bruce Wayne when nobody's watching";
Console.WriteLine(batman.Replace("Bruce Wayne", "Batman"));
```

### Output
```plaintext
Batman is Batman when nobody's watching
```

Lägg märke till att **båda** förekomsterna av "Bruce Wayne" byttes ut, inte bara den första. `Replace()` letar igenom hela strängen och byter ut varenda träff den hittar — om du bara vill byta ut en specifik förekomst behöver du kombinera `Substring()`/`IndexOf()` istället.

<details><summary>Klassisk fallgrop: "bruce" hittar inte "Bruce"</summary>

```csharp
string batman = "Bruce Wayne";
Console.WriteLine(batman.Replace("bruce", "Robin"));
```

### Output
```plaintext
Bruce Wayne
```

Ingenting hände — `"bruce"` (litet b) är inte samma sträng som `"Bruce"` (stort B), precis som [`ToUpper()`/`ToLower()`](#toupper--tolower) visade. `Replace()` jämför tecken för tecken, exakt, och stort/litet B är olika tal under ytan. Den hittar inget att byta ut, så den ger tillbaka strängen oförändrad — helt utan att klaga eller krascha, vilket gör buggen lätt att missa.

**Fixen** — säg explicit åt `Replace()` att strunta i skillnaden mellan stort och litet:

```csharp
string batman = "Bruce Wayne";
Console.WriteLine(batman.Replace("bruce", "Robin", StringComparison.OrdinalIgnoreCase));
```

### Output
```plaintext
Robin Wayne
```

</details>

---

## Substring()

Plockar ut en bit av en sträng, baserat på startindex (och valfritt — hur många tecken du vill ha).

```csharp
string batman = "Bruce Wayne";
Console.WriteLine(batman.Substring(6));     // från index 6 till slutet
Console.WriteLine(batman.Substring(0, 5));  // från index 0, 5 tecken
```

### Output
```plaintext
Wayne
Bruce
```

<details><summary>Varför `Substring(0, 5)` och inte `Substring(0, 6)` för "Bruce"?</summary>

"Bruce" har fem bokstäver (B-r-u-c-e), så du vill ha fem tecken — den andra parametern är **antal tecken**, inte ett slutindex. Räkna bokstäverna i ordet du vill ha, inte var nästa ord börjar.

</details>

---

## Split()

Klyver en sträng till en array av flera strängar, baserat på ett avgränsartecken.

```csharp
string maskerade = "Batman,Robin,Catwoman";
string[] namn = maskerade.Split(',');
Console.WriteLine(namn[0]); // visar första namnet
Console.WriteLine(namn[1]);
Console.WriteLine(namn.Length); // talar om antalet namn i listan
```

### Output
```plaintext
Batman
Robin
3
```

`Split(',')` klyver strängen vid varje komma och ger dig en `string[]` — en array, inte en sträng längre. Det är därför `namn.Length` ger `3` (antal *element* i arrayen) och inte längden på en text. Glöm inte att den ursprungliga `,`-avgränsaren försvinner i resultatet — den finns inte kvar i något av de tre namnen.

**Se även:** [string.Join()](#stringjoin) — den omvända operationen.

---

## StartsWith() / EndsWith()

Kollar om en sträng börjar eller slutar med en specifik delsträng. Som `Contains()`, men låst till en av kanterna.

```csharp
string batman = "Bruce Wayne";
Console.WriteLine(batman.StartsWith("Bruce"));
Console.WriteLine(batman.EndsWith("Wayne"));
Console.WriteLine(batman.StartsWith("Wayne"));
```

### Output
```plaintext
True
True
False
```

Raderna 1–2 är `True` eftersom "Bruce Wayne" faktiskt börjar med "Bruce" och slutar med "Wayne". Rad 3 är `False` — strängen börjar inte med "Wayne", den *slutar* med det. `StartsWith()` och `EndsWith()` bryr sig om exakt position, inte bara om delsträngen finns någonstans (det är jobbet för `Contains()`).

---

## IsNullOrEmpty() / IsNullOrWhiteSpace()

Två statiska metoder — du anropar dem på `string`-typen själv, inte på en variabel. Skillnaden mellan dem är vad de räknar som "tomt".

```csharp
string tom = "";
string bara_mellanslag = "   ";

Console.WriteLine(string.IsNullOrEmpty(tom));
Console.WriteLine(string.IsNullOrEmpty(bara_mellanslag));
Console.WriteLine(string.IsNullOrWhiteSpace(bara_mellanslag));
```

### Output
```plaintext
True
False
True
```

<details><summary>Varför skiljer det sig på rad 2?</summary>

`"   "` är inte tom — den innehåller faktiskt tre mellanslagstecken, så `IsNullOrEmpty` säger `False`. `IsNullOrWhiteSpace` är den strängare kollen — den räknar en sträng som bara innehåller blanksteg som "tom" i praktiken, inte bara en sträng med noll tecken. Använd `IsNullOrWhiteSpace` när du validerar formulärfält — en användare som bara tryckt mellanslag har inte fyllt i något, även om strängen tekniskt inte är `""`.

</details>

---

## PadLeft() / PadRight()

Fyller ut en sträng till en viss längd med ett tecken (mellanslag som standard) — på vänster respektive höger sida.

```csharp
string batman = "42";
Console.WriteLine(batman.PadLeft(5, '0'));
Console.WriteLine(batman.PadRight(5, '!'));
```

### Output
```plaintext
00042
42!!!
```

Klassiskt användningsområde: nollfyllda nummer (`00042`) eller att rättjustera en kolumn i en utskrift.

---

## Insert() / Remove()

`Insert()` klistrar in text på en specifik position. `Remove()` tar bort ett antal tecken från en position.

```csharp
string batman = "Bruce Wayne";
Console.WriteLine(batman.Insert(5, " 'The Dark Knight'"));
Console.WriteLine(batman.Remove(5));
```

### Output
```plaintext
Bruce 'The Dark Knight' Wayne
Bruce
```

`Remove(5)` utan andra parametrar tar bort **allt** från index 5 och framåt — `Remove(start, antal)` finns också om du bara vill ta bort en bit i mitten.

---

## LastIndexOf()

Som `IndexOf()`, men letar **bakifrån** — ger dig den sista förekomsten istället för den första.

```csharp
string batman = "Batman fights crime, Batman wins";
Console.WriteLine(batman.IndexOf("Batman"));
Console.WriteLine(batman.LastIndexOf("Batman"));
```

### Output
```plaintext
0
21
```

Samma sökord, samma sträng, men två olika svar — `IndexOf()` hittar "Batman" först vid index 0 (där strängen börjar), medan `LastIndexOf()` fortsätter förbi den och hittar den **sista** förekomsten vid index 21. Använd `LastIndexOf()` när det kan finnas flera träffar och du specifikt vill ha den som ligger längst bak, t.ex. sista snedstrecket i en filsökväg.

---

## string.Join()

Motsatsen till `Split()` — limmar ihop en array av strängar till en enda, med ett valfritt mellantecken.

```csharp
string[] namn = { "Batman", "Robin", "Catwoman" };
Console.WriteLine(string.Join(", ", namn));
```

### Output
```plaintext
Batman, Robin, Catwoman
```

`string.Join()` tar en array (tre separata strängar) och gör den till **en** sträng, med `", "` klistrat in mellan varje element — men inte före det första eller efter det sista. Det är den vanligaste vägen att gå från en lista av saker till en snygg, läsbar textrad, t.ex. för att skriva ut en lista med namn på en rad istället för att loopa och `Console.WriteLine` varje namn separat.

**Se även:** [Split()](#split) — om du har en hel sträng och vill ha en array, gör tvärtom.

---

## Raw string literals — `"""..."""`

Tre citattecken istället för ett. Allt mellan dem tas **exakt** som det står — citattecken, backslash, radbrytningar, allt — utan att du behöver escapa något.

```csharp
string vanlig = "Batman säger: \"Jag är natten.\"";
string raw = """Batman säger: "Jag är natten." """;

Console.WriteLine(vanlig);
Console.WriteLine(raw);
```

### Output
```plaintext
Batman säger: "Jag är natten."
Batman säger: "Jag är natten."
```

<details><summary>När är det här faktiskt användbart?</summary>

Den vanliga varianten kräver `\"` för varje citattecken — blir snabbt svårläst med JSON, regex-mönster eller filsökvägar fulla av backslash (`C:\Users\...`). Med raw string literals skriver du det exakt som det ser ut, ingen escape behövs:

```csharp
string json = """
{
    "namn": "Batman",
    "stad": "Gotham"
}
""";
```

*(Tillgängligt sedan C# 11 — fortfarande underutnyttjat i mycket kod, värt att känna till oavsett vilken C#-version man jobbar i.)*

</details>

---

## String interpolation — `$"{}"`

Inte en metod, men hör hemma här: sättet du normalt blandar text och variabler på.

```csharp
string namn = "Batman";
int aldersskillnad = 0;
Console.WriteLine($"{namn} har funnits sedan 1939, {aldersskillnad} år skiljer honom från sig själv.");
```

### Output
```plaintext
Batman har funnits sedan 1939, 0 år skiljer honom från sig själv.
```

Lägg märke till att `{namn}` och `{aldersskillnad}` försvinner helt i outputen — de ersätts av variablernas *värden*, inte deras namn. `$`-tecknet framför citattecknet är vad som gör att C# letar efter `{}` och fyller i innehållet, istället för att skriva ut måsvingarna bokstavligt.

**Se även:** Hela `$"{}"`-konceptet (och varför `\n` ger en ny rad) går vi igenom rad för rad i [`fredrik_akare.md`](../exercises/fredrik_akare.md). Se även [string.Format()](#stringformat) — föregångaren till samma idé, fortfarande vanlig i äldre kod.

---

## string.Format()

Föregångaren till string interpolation. Samma idé — blanda text och variabler — men platshållarna är numrerade (`{0}`, `{1}`...) istället för att innehålla variabelnamnet direkt.

```csharp
string namn = "Batman";
int aldersskillnad = 0;
Console.WriteLine(string.Format("{0} har funnits sedan 1939, {1} år skiljer honom från sig själv.", namn, aldersskillnad));
```

### Output
```plaintext
Batman har funnits sedan 1939, 0 år skiljer honom från sig själv.
```

Exakt samma resultat som `$"{}"`-versionen ovan — `{0}` syftar på det första argumentet efter formatsträngen, `{1}` på det andra, och så vidare.

<details><summary>Varför skulle jag använda Format() när interpolation finns?</summary>

I ny kod gör du det sällan — `$"{}"` är kortare och lättare att läsa, eftersom variabelnamnet syns direkt i platshållaren istället för att du måste räkna `{0}`, `{1}`, `{2}`... och hålla koll på vilket argument som hör till vilket nummer. Men `string.Format()` dyker fortfarande upp i äldre kodbaser, och samma `{0}`-numrering används av flera andra metoder (bland annat loggningsbibliotek) — så det är värt att kunna känna igen även om du själv väljer interpolation.

</details>

**Se även:** [String interpolation](#string-interpolation--) — den modernare versionen av samma idé.

---

## Nästa steg: StringBuilder

Alla metoder ovan har en dold kostnad — `string` i C# är **immutable** (oföränderlig). Varje gång du kör `Replace`, `Trim` eller `+` på en sträng skapas en helt **ny** sträng i minnet, originalet rörs aldrig. För en enstaka strängoperation märks det inte. Bygger du en sträng bit för bit i en loop, hundratals eller tusentals gånger, börjar det kosta — varje steg skapar en ny kopia av allt som redan byggts.

`StringBuilder` löser det genom att jobba med en muterbar buffert istället. Det är en egen kategorifil, `stringbuilder.md`, när den finns.
