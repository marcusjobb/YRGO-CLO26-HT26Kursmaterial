# Self-test — Dictionary

Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

---

### Fråga 1

Hur deklarerar man ett Dictionary med `string`-nycklar och `int`-värden?

a. `Dictionary poäng = new Dictionary();`<br>b. `Dictionary<string, int> poäng = new Dictionary<string, int>();`<br>c. `Dictionary<int, string> poäng = new Dictionary<int, string>();`<br>d. `var poäng = Dictionary["string", "int"];`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `Dictionary<string, int> poäng = new Dictionary<string, int>();`

  **Förklaringar:**

  - ❌ **a) Utan typer** - FEL: Dictionary kräver att man anger nyckeltyp och värdetyp med `<K, V>`
  - ✅ **b) string-nyckel, int-värde** - **RÄTT**: `Dictionary<string, int>` — första typen är nyckel, andra är värde. `poäng["Alex"] = 42;` fungerar sedan
  - ❌ **c) int-nyckel, string-värde** - FEL: Det är omvänt — med den deklarationen är nyckeln ett `int` och värdet en `string`
  - ❌ **d) Hakparentes-syntax** - FEL: Det är inte giltig C#-syntax
</details>

---

### Fråga 2

Hur lägger man till ett nyckelvärdespar i ett Dictionary?

a. Bara `dict["namn"] = "värde";` fungerar<br>b. Bara `dict.Add("namn", "värde");` fungerar<br>c. Båda fungerar, men de beter sig lite olika<br>d. `dict.Insert("namn", "värde");`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Båda fungerar, men de beter sig lite olika

  **Förklaringar:**

  - ❌ **a) Bara indexerare** - FEL: Indexeraren fungerar, men `Add` fungerar också
  - ❌ **b) Bara Add** - FEL: Indexeraren fungerar också
  - ✅ **c) Båda, men olika** - **RÄTT**: `dict.Add("Alex", 42)` kastar ett undantag om nyckeln redan finns. `dict["Alex"] = 42` skriver *över* ett befintligt värde tyst. Välj efter behov
  - ❌ **d) Insert** - FEL: `Insert` finns inte på Dictionary
</details>

---

### Fråga 3

Vad händer om du skriver `Console.WriteLine(poäng["Okänd"]);` och nyckeln "Okänd" inte finns?

a. Du får `null`<br>b. Du får `0`<br>c. Programmet kastar ett `KeyNotFoundException`<br>d. Dictionary lägger automatiskt till "Okänd" med värdet 0

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Programmet kastar ett `KeyNotFoundException`

  **Förklaringar:**

  - ❌ **a) null** - FEL: C# kastar ett undantag — det returnerar inte null (till skillnad från vissa andra språk)
  - ❌ **b) 0** - FEL: Dictionary gissar inte ett standardvärde och returnerar det tyst
  - ✅ **c) KeyNotFoundException** - **RÄTT**: Dictionary kraschar om nyckeln inte finns. Använd alltid `ContainsKey()` eller `TryGetValue()` innan du slår upp
  - ❌ **d) Lägger till automatiskt** - FEL: Dictionary lägger inte till nycklar automatiskt vid läsning. Det gör däremot `dict["nyNyckel"] = värde;` vid skrivning
</details>

---

### Fråga 4

Vilket är det säkraste sättet att hämta ett värde från ett Dictionary?

a. Slå upp direkt: `dict[nyckel]`<br>b. Kontrollera med `ContainsKey()` innan uppslagning<br>c. Använd `TryGetValue()` som returnerar false om nyckeln saknas<br>d. Både b och c är säkra alternativ

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Både b och c är säkra alternativ

  **Förklaringar:**

  - ❌ **a) Direkt uppslagning** - FEL: Om nyckeln saknas kraschar programmet
  - ❌ **b) Bara ContainsKey** - DELVIS RÄTT: Det fungerar, men det är två uppslag (ett för ContainsKey, ett för värdet)
  - ❌ **c) Bara TryGetValue** - DELVIS RÄTT: `TryGetValue` gör det i ett steg: `if (dict.TryGetValue("Alex", out int p)) { ... }`
  - ✅ **d) Båda är säkra** - **RÄTT**: `ContainsKey + []` är tydligt och läsbart. `TryGetValue` är mer kompakt och effektivt. Välj det som passar bäst
</details>

---

### Fråga 5

Hur loopar man igenom ett Dictionary och får tillgång till både nyckel och värde?

a. `foreach (string key in dict)`<br>b. `foreach (KeyValuePair<string, int> par in dict)`<br>c. `for (int i = 0; i < dict.Count; i++)`<br>d. `foreach (var v in dict.Values)`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `foreach (KeyValuePair<string, int> par in dict)`

  **Förklaringar:**

  - ❌ **a) Bara nyckeln** - FEL: `foreach (string key in dict)` loopar över nycklarna — du når inte värdena direkt
  - ✅ **b) KeyValuePair** - **RÄTT**: Varje iteration ger dig `par.Key` och `par.Value`. Det är standardsättet. Du kan förkorta till `foreach (var par in dict)` och få samma resultat
  - ❌ **c) for med index** - FEL: Dictionary har inget index — du kan inte nå element med `dict[0]`
  - ❌ **d) Bara värdena** - FEL: `dict.Values` ger alla värden, men du har inte nyckeln tillgänglig
</details>

---

### Fråga 6

Kan ett Dictionary ha två identiska nycklar?

a. Ja, men bara om värdena är olika<br>b. Ja, Dictionary hanterar duplikat automatiskt<br>c. Nej — alla nycklar måste vara unika<br>d. Det beror på vilken typ nyckeln har

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Nej — alla nycklar måste vara unika

  **Förklaringar:**

  - ❌ **a) Olika värden tillåter duplikat** - FEL: Nyckeln måste alltid vara unik, oavsett vad värdet är
  - ❌ **b) Hanterar automatiskt** - FEL: `dict.Add("Alex", 10); dict.Add("Alex", 20);` kastar ett `ArgumentException` — det hanteras inte tyst
  - ✅ **c) Nycklar måste vara unika** - **RÄTT**: Det är hela poängen med ett Dictionary — en nyckel pekar på exakt ett värde. Vill du ha flera värden per nyckel, använd `Dictionary<string, List<int>>`
  - ❌ **d) Beror på typ** - FEL: Kravet på unika nycklar gäller för alla nyckeltyper
</details>
