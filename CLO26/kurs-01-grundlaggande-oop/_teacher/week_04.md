# Vecka 4 — Metoder

**Tema:** Metoder, parametrar, returvärden, DRY
**Mål:** Den studerande kan skriva metoder med parametrar och returvärden och motivera varför man bryter ut kod i metoder

## Lärarens anteckningar

Metoder är brobygget mellan "procedurell kod i Main" och "objektorienterad design i klasser".
Den studerande har sett metoder i vecka 2-repetitionen — nu fördjupar vi och formaliserar.

Betona DRY tidigt och låt dem känna smärtan av upprepning innan du visar lösningen.
Börja gärna med ett exempel där samma beräkning görs tre gånger i Main — låt dem föreslå hur man fixar det.

### Dag 1 — Void-metoder och metoder med returvärde

- Föreläsning: `metoder_marp.md`
- Live-kodning: bygg `Add`, `Subtract` och `Divide` (med nollkoll) tillsammans
- Övning 1: `calculator_methods.md`

### Dag 2 — Metoder som anropar varandra

- Repetition: vad returnerar en metod? Vad är skillnaden mot void?
- Live-kodning: `GetGrade` + `CalculateAverage` — visa hur metoder kedjas
- Övning 2: `grade_calculator.md`
- Snabbare grupper: `string_methods.md`

### Dag 3 — Handledning

- Online handledning via Discord/Meet
- Ingen inlämning denna vecka — fokus på förståelse inför nästa modul

## Publicera

```
publish:
  - 04_metoder/README.md
  - 04_metoder/lectures/metoder_marp.md
  - 04_metoder/notes/metoder.md
  - 04_metoder/programmeringstermer/metoder.md
  - 04_metoder/examples/MethodExamples.cs
  - 04_metoder/exercises/calculator_methods.md
  - 04_metoder/exercises/grade_calculator.md
  - 04_metoder/exercises/string_methods.md
  - 04_metoder/exercises/basic_algorithms.md
  - 04_metoder/tentafragor/fragor.md
```

## Inlämning denna vecka

Ingen inlämning vecka 4.
