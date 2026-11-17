---
marp: true
theme: default
paginate: true
title: Hemligheter i git
description: Varför appsettings.json aldrig ska committas, och vad som händer när någon gör det ändå
style: |
  section {
    font-size: 26px;
  }
  section.lead {
    text-align: center;
    justify-content: center;
  }
  section.lead h1 {
    font-size: 64px;
  }
  section.lead p {
    font-size: 28px;
  }
  table {
    font-size: 22px;
  }
  th {
    background: #2d3e50;
    color: #fff;
  }
  code {
    font-size: 0.9em;
  }
  blockquote {
    font-size: 22px;
    border-left: 6px solid #e67e22;
    color: #555;
  }
---

<!-- _class: lead -->
<!-- _paginate: false -->

# Hemligheter i git

Din connectionstring innehåller ett lösenord.
Vad händer om den hamnar på GitHub?

---

## Var är vi nu?

```
Kurs 02 — Databaser
├── ✅ SQL-grunder
├── ✅ SQLite
├── ✅ Transaktioner
└── 👉 MySQL i Docker + TiDB Cloud    ← idag
        └── 👉 Hur vi skyddar lösenorden
```

Idag får du ett lösenord till en riktig molndatabas. Då behöver vi veta var det **inte** får hamna.

---

## Konfiguration hör inte hemma i koden

```csharp
var connectionString = "Server=localhost;Database=heroes;Password=hemligt";
```

Det här är ett designfel, även om lösenordet är ofarligt:

- Ny server eller ny miljö betyder att du måste ändra och kompilera om koden
- Koden och konfigurationen blandas ihop
- Lösenordet följer med koden dit koden går

> Vi läser connectionstring från `appsettings.json` **från första raden, varje gång**. Det ska aldrig bli en vana att skriva den i koden.

---

## Lösningen: konfiguration utanför koden

```csharp
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = config.GetConnectionString("Default");
```

`Program.cs` vet inte var databasen finns. Den frågar bara konfigurationen.

Men nu har vi flyttat problemet: lösenordet ligger i en **fil**. Vad händer med filen när du kör `git push`?

---

## Git glömmer aldrig

- Varje commit sparas för alltid i historiken
- Tar du bort filen i nästa commit, finns den kvar i den förra
- Alla som klonar repot får hela historiken

```bash
git log --all -- appsettings.json
git show <commit>:appsettings.json
```

> Att ta bort filen räcker inte. Ett pushat lösenord är ett **läckt** lösenord.

---

## Hur många gör det här misstaget?

<!-- Livedemo: sök på GitHub (kräver inloggning) -->

Sök på GitHub efter filer som heter `appsettings.json` och innehåller `Password=`:

```
path:appsettings.json "Password="
```

Titta på **antalet träffar**. Öppna inte någons uppgifter och använd dem aldrig.

Många träffar är platshållare som `Password=changeme`. Men tillräckligt många är riktiga.

---

## Etik: det här gör vi inte

- Vi öppnar inte andras connectionstrings
- Vi testar inte lösenord vi hittar
- Vi klistrar inte in riktiga hemligheter i chatten, slides eller rapporter

Att hitta en läcka är en sak. Att använda den är olagligt, även om repot är publikt.

---

## Vi gör ett eget försök

Vi skapar ett **publikt engångsrepo** och pushar falska hemligheter.

1. En fil med ett vanligt `Password=` i en connectionstring
2. En fil med en falsk nyckel i ett format som GitHub känner igen

Vad tror du händer i varje fall? Skriv ner din gissning innan vi kör.

---

## Livedemo

<!-- Demo: skapa publikt repo, pusha båda filerna, visa resultatet, radera repot -->

1. Skapa repot på GitHub (publikt, ett engångsrepo)
2. Pusha `appsettings.json` med `Password=Hemligt-Demo-123`
3. Pusha en fil med en falsk leverantörsnyckel
4. Jämför vad GitHub gör i de två fallen
5. Radera repot

> Lösenordet i demon är påhittat och används ingenstans.

---

## Vad såg vi?

GitHub har skydd som fångar **kända nyckelformat** (secret scanning och push protection).

Men ett vanligt lösenord i en connectionstring har inget fast format. Det slinker lätt igenom.

| | Skyddas av GitHub? |
|---|---|
| Känd API-nyckel | Ofta ja |
| `Password=` i en connectionstring | Oftast nej |

> Skyddsnätet är inte tillräckligt. Därför `.gitignore` från **första commit**.

---

## Så här gör vi i praktiken

1. Lägg `appsettings.json` i `.gitignore` **innan** första commit
2. Checka in `appsettings.example.json` med platshållare

```json
{
  "ConnectionStrings": {
    "Default": "Server=DIN_HOST;Port=4000;Database=heroes;User=DIN_ANVÄNDARE;Password=DITT_LÖSENORD"
  }
}
```

3. Skriv i README: *"Kopiera `appsettings.example.json` till `appsettings.json` och fyll i dina värden."*

---

## Och om det händer ändå?

Du har pushat ett lösenord. Vad gör du?

1. **Byt lösenordet direkt.** Det viktigaste steget
2. Ta bort filen ur repot och lägg den i `.gitignore`
3. Räkna med att det gamla lösenordet redan är läckt

> Att ta bort filen i nästa commit räcker inte. Lösenordet finns kvar i historiken. Byt det.

---

## Nästa steg (utanför kursen)

`appsettings.json` är inte rätt plats för riktiga hemligheter, inte ens på din egen dator.

| Miljö | Vad man använder |
|---|---|
| Utveckling | `dotnet user-secrets` |
| Produktion | Miljövariabler eller Azure Key Vault |

Det kommer vi tillbaka till i molnkurserna.

---

## Kontrollfrågor

1. Varför ska connectionstring inte ligga i koden, även för en lokal SQLite-fil?
2. Varför räcker det inte att ta bort `appsettings.json` i nästa commit?
3. Vad är skillnaden mellan `appsettings.json` och `appsettings.example.json`, och vilken checkar vi in?
4. Du har pushat ett lösenord av misstag. Vad gör du först?

---

<!-- _class: lead -->
<!-- _paginate: false -->

# Sammanfattning

Läs konfiguration från `appsettings.json`.
Lägg filen i `.gitignore` före första commit.
Checka in en `.example`-fil.
Läckt lösenord? **Byt det.**
