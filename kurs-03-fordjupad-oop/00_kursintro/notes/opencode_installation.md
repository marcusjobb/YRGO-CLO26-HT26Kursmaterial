# Installera OpenCode och koppla gratis modeller

**OpenCode** är ett AI-kodverktyg som körs i terminalen. Det är öppen källkod, och du kan koppla det till olika AI-leverantörer. Den här guiden visar hur du installerar det och hur du kopplar ett gratis OpenRouter-konto.

**Efter det här kan du:** installera OpenCode på din dator, koppla ett OpenRouter-konto och välja en gratismodell.

## Innan du börjar

- Du behöver en terminal. På Windows kan du använda PowerShell, men installationsskriptet kräver Git Bash eller WSL.
- Du behöver ett konto på OpenRouter. Det är gratis.
- Lägg inte in personuppgifter eller känsliga elevdata i prompten. Det gäller alla AI-tjänster, även gratisvarianter.

## 1. Installera OpenCode

Dokumentationen på [opencode.ai/docs](https://opencode.ai/docs/) är den officiella källan. Kontrollera versionen där innan du installerar.

**macOS och Linux, installationsskript:**

```bash
curl -fsSL https://opencode.ai/v2/install | bash
```

**macOS och Linux, pakethanterare:** välj en av dessa, om du redan har den installerad.

| Pakethanterare | Plattform | Kommando |
|---|---|---|
| Homebrew | macOS, Linux | `brew install anomalyco/tap/opencode-v2` |
| mise | macOS, Linux | `mise use -g opencode` (kräver mise 2026.10.9 eller senare) |
| AUR (paru eller yay) | Arch Linux | `paru -S opencode-beta` eller `yay -S opencode-beta` |

**Node.js krävs för npm, pnpm, yarn och bun-varianterna.** Installera Node.js (en LTS-version från [nodejs.org](https://nodejs.org/)) först och kontrollera med `node --version`. Sedan:

```bash
npm install -g @opencode/cli
```

pnpm och yarn fungerar på samma sätt, men kommandona skiljer sig från npm. Bun är en egen körmiljö och behöver inte Node.js, men kontrollera dess installationsguide.

**Windows:** det finns ingen officiell installation via winget, Chocolatey eller Scoop. Så här gör du:

1. Ladda ner OpenCode från dokumentationen, välj x64 eller ARM64 beroende på din dator.
2. Packa upp zip-filen.
3. Lägg `opencode.exe` i en mapp som finns med i PATH.
4. Öppna en ny terminal och kör `opencode --version`.

Steg 3 och 4 är inte beskrivna i den officiella dokumentationen, så kontrollera dem själv. Om kommandot inte hittas är det oftast PATH som saknas. Ett alternativ är att köra installationen i WSL och följa macOS/Linux-stegen. Windows-varianten med npm kräver också Node.js.

**Äldre version 1:** `npm install -g opencode-ai` installerar version 1. Kontrollera vilken version som gäller för kursen innan du väljer.

## 2. Starta OpenCode i din projektmapp

```bash
cd sökvägen/till/ditt/projekt
opencode
```

## 3. Koppla OpenRouter

1. Gå till [openrouter.ai/settings/keys](https://openrouter.ai/settings/keys) och skapa en API-nyckel med **Create API Key**. Nyckeln börjar med `sk-or-v1-`. Spara den, den visas bara en gång.
2. I OpenCode skriver du `/connect` och söker efter **OpenRouter**.
3. Klistra in nyckeln när du blir ombedd.
4. Skriv `/models` och välj en modell.

Tips: om inga modeller dyker upp, starta om OpenCode helt.

## 4. Gratis modeller

Gratismodeller på OpenRouter har suffixet `:free` i sitt namn. Listan ändras ofta, så kontrollera den själv på [openrouter.ai/collections/free-models](https://openrouter.ai/collections/free-models). Den listan visade 16 modeller i november 2026, rankade efter hur mycket de används. Några exempel:

| Modell (slug) | Leverantör |
|---|---|
| `google/gemma-4-26b-a4b-it:free` | Google |
| `nvidia/nemotron-3-ultra-550b-a55b:free` | NVIDIA |
| `cohere/north-mini-code:free` | Cohere |

I OpenCode skriver du `openrouter/` framför slugen, till exempel `openrouter/google/gemma-4-26b-a4b-it:free`.

Om en modell inte längre finns i listan, välj en annan. Det är vanligt att gratismodeller byts ut.

## 5. Gränser för gratiskonton

- **20 förfrågningar per minut** och **50 per dag** per modell.
- Misslyckade förfrågningar räknas också mot dagsgränsen.
- Med ett gratiskonto gäller gränserna ovan. Efter ett engångsköp på **10 USD i krediter** höjs dagsgränsen till 1 000 förfrågningar per dag.

## Kontroll innan du börjar arbeta

- Skriv en enkel fråga, till exempel "Vad gör filen Program.cs?", och kontrollera att svaret stämmer med koden.
- Om svaret är tomt eller ett felmeddelande visas, kontrollera nyckeln och att rätt modell är vald.
