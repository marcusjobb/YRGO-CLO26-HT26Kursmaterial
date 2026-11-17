# Bygg din egen AI-chatt i C#

Den här guiden tar dig genom ett litet men riktigt AI-chattprogram, bit för bit. Du kör det i terminalen, och du kan se exakt vad som skickas till modellen och vad som kommer tillbaka.

Vi har tre mål: att förstå **context**, **tokens** och **message history**. Allt annat är bara verktyg för att se dem.

> 🧭 **Tips:** Läs en runda i taget. Kör koden, titta på filerna i `data/`, och först sedan läser du nästa runda.

---

## Innehåll

0. [Förberedelser](#0-förberedelser)
1. [Runda 1: Modellen har ingen aning (utan historik)](#runda-1-modellen-har-ingen-aning-utan-historik)
2. [Runda 2: Historiken gör att modellen minns](#runda-2-historiken-gör-att-modellen-minns)
3. [Runda 3: Context och tokens](#runda-3-context-och-tokens)
4. [Runda 4: Komprimering](#runda-4-komprimering)
5. [Runda 5: Verktyg som läser och skriver filer](#runda-5-verktyg-som-läser-och-skriver-filer)
6. [Runda 6: Koden, bit för bit](#runda-6-koden-bit-för-bit)
7. [Bonus: personligheter](#bonus-personligheter)
8. [Ninja-utmaningar](#ninja-utmaningar)

---

## 0. Förberedelser

Du behöver:

- **.NET 10 SDK.** Kontrollera med `dotnet --version`. Det ska stå `10.`.
- **En API-nyckel** till en OpenAI-kompatibel tjänst. Använd din egen. Dela den inte med andra och lägg den aldrig i koden eller i git.

Sätt nyckeln i din terminal. Det gäller bara den här sessionen.

**PowerShell (Windows):**
```powershell
$env:CHAT_API_KEY = "din-nyckel"
```

**Bash (Linux/macOS):**
```bash
export CHAT_API_KEY="din-nyckel"
```

Det finns fler inställningar, men de har standardvärden. Du kan lägga till dem när du vill:

| Variabel | Standard | Vad den gör |
|---|---|---|
| `CHAT_API_KEY` | (krävs) | Din nyckel |
| `CHAT_BASE_URL` | `https://api.openai.com/v1` | Adressen till tjänsten |
| `CHAT_MODEL` | `gpt-4o-mini` | Modellen som svarar |
| `CHAT_HISTORY` | `1` | `0` betyder att historiken inte skickas med |
| `CHAT_WINDOW` | `20` | Hur många turer som skickas med |
| `CHAT_COMPACT_AT` | `50` | Efter så här många turer komprimeras samtalet |
| `CHAT_PERSONA` | (tom) | Välj en personlighet: `Margareta`, `Becky`, `Bosse` eller `Glenn` |

Starta programmet från projektmappen:

```bash
dotnet run --project AiChat
```

Du ska se en rad som börjar med `Modell:`. Skriv `exit` för att avsluta.

---

## Runda 1: Modellen har ingen aning (utan historik)

### Det här ska du göra

Starta om programmet med historiken avstängd. I PowerShell:

```powershell
$env:CHAT_HISTORY = "0"
dotnet run --project AiChat
```

I bash: `CHAT_HISTORY=0 dotnet run --project AiChat`.

1. Skriv ett påstående om dig själv, till exempel: **"Jag heter Anna och jag har en katt som heter Mimmi."**
2. Vänta på svaret.
3. Fråga: **"Vad heter min katt?"**

### Vad hände?

Modellen säger antagligen att den inte vet. Det är inget fel. Den har fått **bara din senaste fråga**, och inget av det du sagt tidigare.

Här är poängen: en språkmodell minns ingenting själv. Varje anrop är ett nytt, fristående anrop. Det som gör att den "minns" är det **vi skickar med**.

### Titta på bevisen

Öppna `data/kommunikation.json` i VS Code. Hitta det sista `request`-objektet. Där ser du listan `messages`. Den innehåller bara ett enda `user`-meddelande: din fråga om katten.

> 🔍 **Fundera på:** Vad skulle du behöva lägga till i listan för att modellen skulle svara rätt?

---

## Runda 2: Historiken gör att modellen minns

### Det här ska du göra

Stäng av programmet, sätt `CHAT_HISTORY` till `1` (eller ta bort variabeln), och kör om samma test:

1. Säg **"Jag heter Anna och jag har en katt som heter Mimmi."**
2. Fråga **"Vad heter min katt?"**

Nu svarar modellen rätt.

### Vad hände?

Den här gången skickades **historiken** med. Programmet läser `data/historik.json`, bygger en lista med alla tidigare turer, och skickar med den i varje anrop.

Öppna `data/historik.json`. Där ser du varje tur i ordning:

```json
[
  { "type": "turn", "speaker": "user", "content": "Jag heter Anna ..." },
  { "type": "turn", "speaker": "assistant", "content": "Hej Anna! ..." }
]
```

Öppna sedan `data/kommunikation.json` och titta på den senaste `request`. Nu finns både din första fråga och svaret i listan, innan den nya frågan.

### Begreppet: message history

**Message history** är listan av alla meddelanden som skickas till modellen i ett anrop. Den växer för varje tur. Ju längre listan är, desto mer har modellen att titta på, och desto mer kostar varje anrop.

> 🧠 **Tänk på en kokbok:** Modellen är en kock som bara läser recepten du lägger på bordet. Om du tar bort ett recept, kan kocken inte följa det.

---

## Runda 3: Context och tokens

### Context

**Context** är allt modellen ser i ett anrop: systemmeddelandet, historiken och det nya meddelandet. Det är det enda modellen använder för att svara. Det finns ingen annan källa.

Skriv `/kontext` i chatten. Du ser exakt vad som skickas nu, med roll och början av varje meddelande.

### Tokens

Modellen räknar inte i ord eller tecken, utan i **tokens**. En token är ungefär fyra tecken på engelska, och lite mer på svenska. Ordet "katt" är en eller två tokens. "Grattis" kan vara två.

Tokens är det du betalar för. Det finns två slag:

- **Tokens in (prompt):** allt som skickas, alltså hela contexten.
- **Tokens ut (completion):** det modellen skriver tillbaka.

Efter varje svar visas en rad som:

```
(tokens in: 191, ut: 10 · kontext: 4 meddelanden)
```

### Experiment

1. Skriv en kort fråga, till exempel "Hej". Notera `tokens in`.
2. Skriv fem längre meddelanden, till exempel en lång berättelse om din helg.
3. Skriv en kort fråga igen. Jämför `tokens in`.

Ser du? **Varje nytt svar blir dyrare**, även om din fråga är lika kort. Det beror på att hela historiken skickas med varje gång.

> 💸 **Det är därför långa chattar kostar.** Det är inte bara ditt senaste meddelande som räknas.

### Vad är tokens ut då?

Det är svaret. Be modellen skriva en lång text, till exempel "Berätta en lång saga om en katt", och jämför `tokens ut` med en kort fråga. Längre svar kostar mer.

---

## Runda 4: Komprimering

Det finns en lösning när historiken blir för lång: **komprimering**. Modellen sammanfattar samtalet, och sammanfattningen ersätter allt som var före.

### Det här ska du göra

1. Chatta fram ungefär tio turer om något du tycker om.
2. Skriv `/kontext` och notera antalet meddelanden.
3. Skriv `/compact`.
4. Skriv `/kontext` igen.

Antalet meddelanden har gått ner. I contexten finns nu en systemrad som börjar med "Sammanfattning av tidigare samtal".

### Vad hände i filerna?

Öppna `data/historik.json` och scrolla till slutet. Du hittar en händelse med `"type": "compact"`. Den innehåller sammanfattningen.

Notera också att **gamla turer finns kvar** i filen. Historiken raderas aldrig. Det är bara det modellen får se som ändras. Det kallas en **append-only logg**: vi lägger till, men vi skriver aldrig över.

> 🧹 **Fråga dig själv:** Vad förlorar vi när vi komprimerar? Vad kan vi inte längre fråga om?

---

## Runda 5: Verktyg som läser och skriver filer

Hittills har modellen bara pratat. Nu ger vi den två verktyg: `read_file` och `write_file`. Den kan be om att få använda dem, och programmet kör det.

### Det här ska du göra

Skriv i chatten:

> Skriv en kort dikt om en katt till filen dikt.txt.

Du ser en rad som `[verktyg] write_file(...)`. Sen kommer modellens svar. Kolla sedan mappen `arbetsyta/`. Där ligger `dikt.txt`.

Be sedan modellen:

> Läs filen dikt.txt och berätta vad den handlar om.

### Hur går det till?

Modellen svarar inte alltid med text. Ibland svarar den med ett **verktygsanrop**: "Kör write_file med dessa argument." Programmet kör verktyget, och skickar resultatet tillbaka som ett `tool`-meddelande. Sen frågar programmet modellen igen, och nu har den resultatet att gå på.

Det här är en **verktygsloop**. Den finns i `ChatService.RunToolRoundsAsync`. Vi begränsar den till fyra varv, så att en modell som fastnar inte kör för evigt.

Titta i `data/kommunikation.json`. Där ser du verktygsanropet i svaret, och `tool`-meddelandet i nästa förfrågan.

### Säkerhet: arbetsytan

Verktygen får bara läsa och skriva i `arbetsyta/`. Om modellen försöker skriva till `../hemlig.txt` stoppas det av `Workspace.Resolve`. Testa gärna själv:

> Skriv "hej" till filen ../utanfor.txt.

Du får ett felmeddelande, och ingen fil skapas utanför arbetsytan.

---

## Runda 6: Koden, bit för bit

Nu tittar vi på hur koden är uppdelad. Varje fil har **ett** ansvar, och varje mapp samlar en sorts klasser.

```
AiChat/
├── Program.cs            Startpunkten: kopplar ihop alla delar
├── Config/               Inställningar från miljövariabler
├── Models/               Datatyper: meddelanden, svar, tokens
├── Llm/                  Allt som pratar med API:t
├── Storage/              Sparar historik och kommunikation i JSON
├── Services/             Logiken: chatten och komprimeringen
├── Tools/                Verktygen och arbetsytan
└── Ui/                   Konsolloopen
```

### Models: datatyper utan logik

`ChatMessage`, `ToolCall`, `LlmReply` och `TokenUsage` innehåller bara data. De gör inget. Det är lätt att testa och lätt att läsa.

```csharp
public sealed record ChatMessage(string Role, string Content, ...);
```

En **record** är en klass som bara har data. Den är oföränderlig, och C# skriver likhetskontroll och text-representation åt dig.

### Llm: allt som pratar med API:t

`LlmClient` är den enda klassen som vet hur HTTP-anropet ser ut. Den bygger JSON, skickar den, och tolkar svaret.

Den är bakom ett **interface**, `ILlmClient`. Det betyder att `ChatService` inte bryr sig om vilken modell som svarar. I testerna byter vi ut den mot en låtsasmodell, `FakeLlm`.

> 🧩 **Varför interface?** Det gör att vi kan testa chattlogiken utan nätverk och utan pengar. Det kallas **dependency inversion**: den högre nivån beror på en beskrivning, inte på den konkreta klassen.

### Storage: JSON som kan läsas av människor

`JsonFile` läser och skriver listor. `HistoryStore` sparar turer och komprimeringar. `JsonCommunicationLog` sparar varje förfrågan och svar.

Alla filer skrivs via en temporär fil, och sen byter vi namn på den. Då blir en halvskriven fil omöjlig, även om datorn kraschar mitt i skrivningen.

### Services: logiken

`ChatService` gör tre saker: bygger meddelandena, kör verktygsloopen och sparar resultatet. `Compactor` gör en enda sak: sammanfattar.

Lägg märke till att `ChatService` inte skriver ut något. Det returnerar ett `ChatResult`. Det är konsolen som bestämmer hur det visas. Det är **separation of concerns**: logik och utskrift är skilda.

### Tools: verktygen

`ITool` är en beskrivning av ett verktyg. `ReadFileTool` och `WriteFileTool` implementerar den. `ToolRegistry` samlar dem, och kör det modellen ber om.

Lägg till ett nytt verktyg? Skapa en ny fil, implementera `ITool`, och lägg till den i `Program.cs`. Ingen annan kod behöver ändras. Det kallas **öppen för utökning, stängd för ändring**.

### Ui: konsolen

`ChatLoop` läser text, kör kommandon som `/kontext`, och skriver ut svaren. Den vet inget om HTTP eller JSON.

### Program.cs: kopplingen

`Program.cs` är den enda plats där allt sätts ihop. Den skapar objekten och skickar in dem i varandra. Det kallas **composition root**.

```csharp
var llm = new LlmClient(settings.ApiKey, settings.BaseUrl, communicationLog);
var chat = new ChatService(llm, history, tools, settings);
```

### Testerna

Kör testerna från projektmappen:

```bash
dotnet test AiChat.Tests
```

Titta på `ChatServiceTests`. Där ser du två saker som vi har pratat om: att utan historik skickas bara det nya meddelandet, och att med historik skickas alla tidigare turer.

---

## Bonus: personligheter

Vill du att assistenten ska låta som någon? Sätt `CHAT_PERSONA`:

```powershell
$env:CHAT_PERSONA = "Becky"
dotnet run --project AiChat
```

| Namn | Så låter det |
|---|---|
| `Margareta` | Trött, torr och ärlig livserfaren kvinna |
| `Becky` | Swiftie som gärna gör Taylor Swift-kopplingar |
| `Bosse` | Stockholmare med torr humor och lugnt tempo |
| `Glenn` | Göteborgare, varm och lite skojig |

Personligheten ligger först i systemprompten, och påverkar tonen. Den ändrar inte fakta. Om du skriver ett namn som inte finns får du ett felmeddelande med listan över de som finns.

**Läs koden:** `Config/Personas.cs` är listan, och `Config/Persona.cs` är datatypen. Lägg till en ny personlighet genom att lägga till en rad i listan. Ingen annan kod behöver ändras. Det är samma tanke som med verktygen: ny funktion, utan att ändra det gamla.

---

## Ninja-utmaningar

Välj en eller flera. De är skrivna så att du kan börja litet och bygga vidare.

### 🥷 Nybörjarninja: räkna tokens totalt

Lägg till ett kommando `/tokens` som visar summan av tokens för hela sessionen. Tips: ta vara på `TokenUsage` som `ChatResult` redan bär, och spara en löpande summa i `ChatService`.

### 🥷 Mellannivå: `list_files`-verktyget

Skapa `ListFilesTool` som listar filerna i arbetsytan. Implementera `ITool`, registrera verktyget i `Program.cs`, och testa att modellen kan använda det.

### 🥷 Mellannivå: skriv-skydd

`write_file` skriver över utan att fråga. Gör så att verktyget vägrar skriva över en fil som redan finns, och ge modellen ett tydligt felmeddelande. Skriv ett test som visar att det fungerar.

### 🥷 Avancerat: egen komprimeringsstrategi

I stället för att komprimera allt, behåll de tre senaste turerna och sammanfatta resten. Lägg det i `Compactor` och skriv tester för gränsfallen: vad händer om det bara finns en tur?

### 🥷 Avancerat: visa kostnaden

Om du vet priset per miljon tokens för din modell (leverantörens prislista), räkna ut kostnaden för varje svar och visa den i raden under svaret. Var uppmärksam på att in- och utprisen ofta skiljer sig.

---

## Sammanfattning

- **Modellen minns ingenting själv.** Det vi skickar med är allt den vet.
- **Context** är allt som skickas i ett anrop.
- **Tokens** är vad du betalar för, både in och ut.
- **Message history** växer för varje tur, och därför blir långa samtal dyrare.
- **Komprimering** minskar contexten, men förlorar detaljer.
- **Verktyg** gör att modellen kan göra saker, men vi bestämmer vad den får göra.

Du klarar det här. Kom ihåg att titta i `data/`. Där ser du allt som händer, och det är den bästa lärare du har. 🎉
