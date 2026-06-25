# _scripts/

## VSCode-tasks (.vscode/tasks.json)

Tasks (Ctrl+Shift+P → "Tasks: Run Task") för den öppna `.md`-filen:

- **Marp: Rendera mermaid-diagram** — körs `render_mermaid.py` på filens mapp
- **Marp: Exportera till PDF** / **till HTML** — exporterar bredvid originalet
- **Marp: Presentera** — startar `marp -s` från repo-roten på port `53280`
  (C64 border-color-registret 🕹️, om den inte redan körs) och öppnar den
  aktuella slidesen i Chrome via `localhost:53280`. Tryck **`f`** i
  webbläsaren för fullskärm/presenterläge, **`Esc`** för att gå ur. (Chrome
  är en flatpak och saknar filsystemsåtkomst till `/home/nionit`, därför
  `localhost` och inte `file://`.) Servern täcker hela repot, så att byta
  till en annan `.md`-fil och köra "Presentera" igen funkar utan omstart.
  Porten valdes bort från `8080` eftersom den ofta redan är upptagen av
  andra dev-servrar.

  Tasken pollar `localhost:53280` (upp till 5 sekunder, var 0.5s) innan
  webbläsaren öppnas, så att en kallstart av `marp -s` hinner bli klar
  innan sidan laddas — annars hinner Chrome (som ofta redan körs och
  återanvänds) visa "anslutningen nekades" innan servern svarar.

  Servern startas via `setsid -f`, som kör den i en helt egen session.
  Utan det dödar VSCode bakgrundsprocessen så fort tasken (resten av
  kommandoraden) är klar — `marp -s` hann skriva ut "Start server
  listened" men dog innan webbläsaren kunde ansluta.
- **Marp: Stoppa server** — stänger ner `marp -s` om något krånglar.

## render_mermaid.py

**Vad det gör:**
Går igenom en mapp rekursivt och letar efter ```` ```mermaid ```` -block i
markdown-filer. Varje block renderas till en PNG via mermaid-cli och
markdown-blocket ersätts med en bildreferens. Källkoden sparas som en
`.mmd`-syskonfil i en `diagrams/`-mapp bredvid markdown-filen, så
diagrammet går att redigera och rendera om senare.

Körs scriptet igen utan ändringar händer inget (idempotent) — det letar
bara efter nya ```` ```mermaid ```` -block. Om en `.mmd`-fil i `diagrams/`
har redigerats och är nyare än sin `.png`, renderas den om automatiskt.

**Varför PNG och inte live-mermaid i Marp/VSCode:**
Mermaid-diagram som renderas live i Marp-previewen klipps/komprimeras
opålitligt beroende på diagramtyp och VSCode-cache. Förrenderade PNG:er
är förutsägbara och funkar identiskt vid föreläsning.

**Varför källkoden INTE läggs i en `<!-- -->`-kommentar i markdown-filen:**
Mermaids pilsyntax (`-->`, `-->>`) avslutar HTML-kommentarer i förtid, och
Marps mermaid-preprocessor plockar upp ```` ```mermaid ```` -block även
inuti kommentarer. Båda gör att diagrammet läcker ut som synlig text och
kan slå sönder sidindelningen. `.mmd`-syskonfilen är den säkra
motsvarigheten — fullt redigerbar, och scriptet hittar den automatiskt.

**Hur man kör det:**

```bash
python3 res/_scripts/render_mermaid.py <mapp>
```

- `<mapp>` — mapp att söka i (rekursivt). Default: nuvarande mapp.
- `--config <fil>` — mermaid-tema (JSON). Default: `res/mermaid-dark.json`.
- `--scale <n>` — upplösningsskala för PNG. Default: `2`.
- `--theme <fil>` — Marp-tema (CSS) för export. Default: `res/nion_dark.css`.
- `--html` — exportera varje Marp-slide (`marp: true` i front matter) till `.html` bredvid originalet.
- `--pdf` — exportera varje Marp-slide till `.pdf` bredvid originalet.

`--html`/`--pdf` körs bara på filer med `marp: true` i front matter —
vanliga markdown-filer (README, anteckningar m.m.) påverkas inte. De
exporterade filerna (`*_marp.html` / `*_marp.pdf`) är byggartefakter och
ligger i `.gitignore`.

**Exempel — konvertera alla lektioner i ett kursmodul:**

```bash
python3 res/_scripts/render_mermaid.py kurs-01-grundlaggande-oop/05_klasser_och_oop
```

**Arbetsflöde för att redigera ett befintligt diagram:**

1. Öppna `diagrams/<filnamn>_<N>.mmd` och ändra mermaid-koden.
2. Kör scriptet igen på samma mapp — bilden renderas om automatiskt.

**Beroenden:**

- Python 3
- Node.js / npx (kör `@mermaid-js/mermaid-cli` via `npx -y`, ingen global
  installation krävs)

**CSS-koppling:**
`res/nion_dark.css` har en regel `section img[src*="diagrams/"]` som
begränsar diagrambilder till `max-height: 420px` så stora diagram (t.ex.
sekvensdiagram) inte sväller över slidens kant. Behöver ett enskilt
diagram vara större/mindre än så, använd Marps resize-syntax i markdown:
`![h:300](diagrams/namn.png)`.

**Köra om bara renderingen (utan att ändra markdown):**
Om man bara vill uppdatera PNG:er från redan utdragna `.mmd`-filer (t.ex.
efter att ha redigerat ett diagram), kör scriptet på samma mapp igen —
steget som letar efter nya ```` ```mermaid ```` -block hittar inget nytt
och hoppar över filen, medan `.mmd`-filer nyare än sin `.png` renderas om.
