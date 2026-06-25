# Inventering av old_courses för kurs-03 — Fördjupad OOP (v2)

Djupare genomgång med fokus på böcker, LINQ, async/await och SlarvigKod-exemplet.

---

## 1. Böckerna — kapitelstruktur och relevans

### 1.1 `books/csharp_cmyh/` — Campus Mölndal YH-boken (aktiv, uppdaterad 2025)

Den mest kompletta och aktuella boken. Skriven för YH-studerande, håller Marcus Medinas pedagogiska stil med emojis, metaforer och "dad jokes". Formaterad som Jekyll-site.

**Kapitel relevanta för kurs-03:**

| Kapitel (sökväg) | Innehåll | Relevansnivå |
|---|---|---|
| `C-Sharp/oop/inheritance.md` | Arv, base/derived class, virtual/override, sealed | Hög — Vecka 1 |
| `C-Sharp/oop/polymorphism/index.md` | Polymorfism, referenser, plugin-modell | Hög — Vecka 1 |
| `C-Sharp/oop/polymorphism/abstractclasses/` | Abstrakt klass, abstract methods, exempel | Hög — Vecka 1 |
| `C-Sharp/oop/polymorphism/interfaces/index.md` | Interface, implementation, kontrakt | Hög — Vecka 1 |
| `C-Sharp/oop/polymorphism/interfaces/plugins.md` | Interface som plugin-mekanism | Hög — Vecka 1 |
| `C-Sharp/entityframework/EntityFrameworkCore.md` | EF Core intro, setup, migrationer | Hög — Vecka 2 |
| `C-Sharp/entityframework/example.md` | Fullständigt EF-exempel | Hög — Vecka 2 |
| `C-Sharp/entityframework/migrationer.md` | Migrations-workflow | Hög — Vecka 2 |
| `C-Sharp/entityframework/context/dbcontext_lifecycle.md` | DbContext livscykel, DI-mönster | Medel — Vecka 2 |
| `C-Sharp/entityframework/context/konfiguration.md` | Connection strings, options | Medel — Vecka 2 |
| `C-Sharp/asynkron/index.md` | async/await, Task<T>, regler, misstag, when to use | Hög — Vecka 4 |
| `C-Sharp/asynkron/example.md` | Parallell filsökning med Task.WhenAll, SemaphoreSlim | Hög — Vecka 4 |
| `C-Sharp/api/rest_fundamentals.md` | REST-grunderna | Hög — Vecka 5 |
| `C-Sharp/api/endpoints.md` | Endpoint-design | Medel — Vecka 5 |
| `C-Sharp/quality_control/index.md` | Kodkvalitet intro | Medel — Vecka 3 |
| `C-Sharp/quality_control/code_reviews.md` | Code review-praktik | Medel — Vecka 3 |

**Viktiga luckor i csharp_cmyh:**
- Ingen LINQ-sektion hittad (sökt på alla filnamn)
- Ingen dedikerad SOLID-sektion (finns i 2024-material istället)
- Inga Design Patterns-kapitel (täcks i 2023-kursboken)

---

### 1.2 `books/devbook/` — Äldre CLO22-boken

Strukturellt identisk med CLO22-boken (same filstruktur). Innehåller exakt samma kapitel som `books/CLO22/`. Bedömning: dubblettbok, ingen ny information jämfört med csharp_cmyh. **Använd csharp_cmyh istället.**

Undantag: `Docs/csharp/asynkron/` och `Docs/csharp/oop/` fungerar som äldre referens om man vill jämföra tonläge.

---

### 1.3 `books/JIN23/` — Java-boken (JIN23)

Java-bok för integrationsutvecklare, inte C#. Strukturellt intressant men koden är Java.

**Kapitel med direkt pedagogisk överföring till kurs-03:**

| Kapitel | Innehåll | Hur det kan användas |
|---|---|---|
| `oop/arv.md` | Arv i Java | Strukturjämförelse Java vs C# |
| `oop/polymorfism/abstraktaklasser/` | Abstrakt klass Java | Se hur samma koncept ser ut i Java |
| `oop/polymorfism/interfaces/` | Interface Java | Jämförelse Java interface vs C# interface |
| `asynkron/index.md` | async Java-variant | Kontrast mot C# async/await |
| `aualityassurance/` | TDD, red-green-blue | Kan inspirera testövningar |

**Design Patterns i JIN23:** Inga dedikerade kapitel hittade. Java-DesignPatterns-materialet finns separat under `2023/java/DesignPatterns/`, inte i boken.

---

## 2. Djupdykning: Gamla kursmaterial

### 2.1 `2025/3_advanced_oop/` — Mest fullständig version

Den mest relevanta källan. Marp-slides + övningar + live-material + lösningar.

**Alla Marp-slides (sökväg: `lectures/`):**

| Fil | Ämne | Vecka CLO26 |
|---|---|---|
| `inheritance_abstracts/inheritance_abstracts_marp.md` | Arv, abstrakt klass, virtual/override | Vecka 1 |
| `solid/solid_marp.md` | Alla 5 SOLID-principer | Vecka 3 |
| `refactoring/refactoring_marp.md` | Refactoring-principer | Vecka 3 |
| `refactoring/refactoring_basics_marp.md` | Basics av refactoring | Vecka 3 |
| `creational_patterns/creational_patterns_marp.md` | Factory, Singleton, Builder, Prototype | Vecka 3 |
| `behavioral_patterns/behavioral_patterns_marp.md` | Observer, Strategy, Command, Chain of Resp | Vecka 3 |
| `structural_patterns/structural_patterns_marp.md` | Adapter, Facade, Decorator, Composite | Vecka 3 |
| `design_patterns_overview_marp.md` | Översikt alla patterns-kategorier | Vecka 3 |
| `entity_framework/entity_framework_advanced_marp.md` | EF Core avancerat | Vecka 2 |
| `ef_aspnet/ef_aspnet_marp.md` | EF + ASP.NET kombinerat | Vecka 5 |
| `aspnet/aspnet_marp.md` | ASP.NET intro | Vecka 5 |
| `mvc/mvc_marp.md` | MVC-mönstret | Vecka 5 |
| `mvc/mvc_input_marp.md` | MVC med formulärinput | Vecka 5 |
| `api_clients/api_clients_marp.md` | API-klienter | Vecka 5 |
| `api_clients/api_clients_basics_marp.md` | API basics | Vecka 5 |
| `uml/uml_marp.md` | UML-diagram | Vecka 1–2 |
| `extensions/extension_methods_marp.md` | Extension methods | Vecka 4 (LINQ-förberedelse) |
| `caching/caching_marp.md` | Caching | Vecka 4–5 |
| `vibe_coding/vibe_coding_intro_marp.md` | AI-assisterat kodande — fallgropar | Valfri |

**Alla övningar (sökväg: `exercises/`):**

| Fil | Ämne | Antal övningar |
|---|---|---|
| `inheritance_abstracts/inheritance_exercise_1–3.md` | Arv, grundläggande | 3 |
| `inheritance_abstracts/inheritance_virtual_exercise_1–8.md` | virtual/override, polymorfism | 8 |
| `solid/solid_exercise_1–3.md` | SOLID, en princip per övning | 3 |
| `creational_patterns/creational_exercise_1–2.md` | Factory, Singleton | 2 |
| `behavioral_patterns/behavioral_exercise_1.md` | Observer, Strategy | 1 |
| `structural_patterns/structural_exercise_1.md` | Adapter, Facade | 1 |
| `entity_framework/ef_exercise_1.md` | EF Core CRUD | 1 |
| `refactoring/refactoring_exercise_1.md` | Refactoring av befintlig kod | 1 |
| `refactoring/refactoring_live_example/` | **SlarvigKod** — se sektion 4 | komplett projekt |
| `api_clients/api_clients_exercise_1.md` | API-klienter | 1 |
| `aspnet/aspnet_exercise_1.md` | ASP.NET intro | 1 |
| `uml/uml_exercise_1.md` | UML-ritning | 1 |

---

### 2.2 `2023/csharp/DesignPatterns/` — Dedikerad patterns-kurs

Tre föreläsningar med slides + kod-filer per pattern. C#-kod.

**Föreläsning 1 — Creational Patterns:**
- `01_intro.md`, `02_simple_factory.md`, `03_factory_method.md`, `04_abstract_factory.md`
- `05_builder.md`, `06_prototype.md`, `07_singleton.md`
- Kod: `code/02_simple_factory_01–03.cs`, `07_singleton_01–02.cs` m.fl.

**Föreläsning 2 — Structural Patterns:**
- Adapter, Bridge, Composite, Decorator, Facade, Flyweight, Proxy
- Varje pattern: slides + 2–4 kod-filer

**Föreläsning 3 — Behavioral Patterns:**
- Chain of Responsibility, Command, Iterator, Mediator, **Observer**, State, **Strategy**, Template, Visitor
- Observer: `code/05_observer01–04.cs` — direkt användbart för kurs-03
- Strategy: `code/07_strategy01–03.cs` — direkt användbart för kurs-03

**Kvalitetsbedömning:** Bra slides men lite torra (ingen Marcus-röst). Koden är gedigen. Rekommenderas som referens snarare än direkt återanvändning av slides.

---

### 2.3 `2023/java/DesignPatterns/` — Java-version av samma kurs

Identisk struktur som C#-versionen ovan, men Java-kod. Bonus: föreläsning 1 har en `articles/`-undermapp med långformat-läsning per pattern (Java). Kan användas för jämförelsereferens vid undervisning om patterns är språkoberoende.

---

### 2.4 `2024/csharp/3_oop_adv/` — Föregående CLO-kurs

Strukturerat med numrerade föreläsningsblock + Marp + quizz + exempel.

**Relevanta block:**

| Block | Ämne | Filer |
|---|---|---|
| `02_design_patterns/` | Design Patterns + SOLID i databaskontext | 5 lektioner + komplett C#-projekt |
| `04_clean_code/` | Clean Code 5-lektion-serie, SOLID, refactoring, clean architecture | 5+1 lektioner |
| `06_advanced_db_frameworks/` | EF Core, Repository Pattern, async/await | 5 lektioner |
| `06_advanced_db_frameworks/06_repetition/` | SOLID per princip (01a–01e), async/await, EF | Se nedan |

**Repetitionsmappen `06_repetition/` är ett fynd:**
- `01a_srp.md` till `01e_summary.md` — en fil per SOLID-princip, kort och tydlig
- `04_async_och_tasks.md` — dedikerat async/await-material (2024-version)
- `05_ef.md`, `05_ef_sqlite.md`, `05_ef_funktioner.md` — EF Core med SQLite

---

### 2.5 `2022/Kodkvalite/` — Äldsta Clean Code-materialet

12 filer. Inte Marp-slides utan läsartiklar. Enkel, tydlig stil.

| Fil | Innehåll |
|---|---|
| `CleanCode/CleanCode-Namngivning.md` | Namngivning variabler/metoder/klasser |
| `CleanCode/CleanCode-Metoder.md` | Metodlängd, SRP, extract method |
| `CleanCode/CleanCode-Klasser.md` | Klassansvar |
| `CleanCode/CleanCode-Kommentarer.md` | Kommentarer, self-documenting code |
| `CleanCode/CleanCode-Variabler.md` | Variabelnamn |
| `CleanCode/CleanCode-Struktur.md` | Kodstruktur |
| `Refaktorering.md` | Refactoring-tekniker (30+ konkreta grepp) |
| `CodeReview.md` | Code review-process |
| `Testning.md` | Testning intro |
| `CleanCode-Felhantering.md` | Felhantering Clean Code-stil |

**Kvalitetsbedömning:** Äldre men pålitlig. Inte Marcus-röst från 2025, men konkret och direkt. `Refaktorering.md` har de bästa konkreta kodomvandlingsexemplen (if → ternary, switch → expression, expression-bodied members etc.). Bra komplement till SlarvigKod-övningen.

---

## 3. Specifika luckor: LINQ och async/await

### 3.1 LINQ — lägesrapport

**Finns:** Spridd information på tre ställen, inget samlat i en bra lektion.

| Källa | Sökväg | Bedömning |
|---|---|---|
| 2024 quick rep | `practice_exercises/quick_repetition/17_linq.md` | **Bra utgångspunkt.** LexCorp-tema, Where/OrderBy/Select, lambda-förklaring, dad joke, quiz. Men märkt "OBS kommer inte med på tentan" — tonläget signalerar att det är extra. |
| 2024 fluent LINQ | `practice_exercises/quick_repetition/18_fluent_linq.md` | Komplement till 17. Jämför query syntax vs method syntax. LexCorp/Lex Luthor-tema. |
| 2022 live-kod | `2022/Linq/LiveKod CLO22 Linq 2022-11-15/Demo/FluentLinq.cs` | Markus's egna live-kod: `from person in people where person.Age > 30 orderby person.Name select person` + kommenterad method-syntax. Kompakt, äkta. |
| 2023 live-kod | `2023/csharp/Assignments/Livekod/2023/04-21 - Linqdemo/readme.md` (tom) | Tre mappar med readme-filer men inget innehåll |
| 2024 EntityFramework | `practice_exercises/EntityFramework/Linq och dess magiska värld.pdf` | PDF-fil, troligtvis Codic-material |
| Codic-material | `Material från Codic/C#/Övningar/Linq/Linq och dess magiska värld.pdf` | Ej läst (PDF), men titeln talar för sig |

**Slutsats LINQ:** Ingen färdig, fristående Marp-lektion om LINQ existerar i old_courses. `17_linq.md` + `18_fluent_linq.md` + FluentLinq.cs är de bästa komponenterna att bygga ifrån. Kurs-03 behöver en ny LINQ-lektion.

---

### 3.2 async/await — lägesrapport

**Finns:** Mycket bra material i csharp_cmyh-boken och som quick repetition-fil.

| Källa | Sökväg | Bedömning |
|---|---|---|
| csharp_cmyh boken | `C-Sharp/asynkron/index.md` | **Bästa källan.** Pasta-metaforen, sync vs async visual, alla regler (async void, smittning, deadlock), HttpClient-exempel, Task.WhenAll, vanliga misstag. Pedagogiskt gedigen. |
| csharp_cmyh boken | `C-Sharp/asynkron/example.md` | Parallell filsökning — konkret och imponerande exempel med SemaphoreSlim och IProgress<T>. Lite avancerat för intro men perfekt som fördjupning. |
| 2024 quick rep | `practice_exercises/quick_repetition/19_asynch.md` | Star Labs/The Flash-tema, HttpClient, grundläggande. Kortare variant. |
| 2024 repetition | `06_advanced_db_frameworks/06_repetition/04_async_och_tasks.md` | Repetitionsmaterialet — konkret EF+async |

**Slutsats async/await:** Materialet finns och håller hög klass. `asynkron/index.md` från csharp_cmyh-boken är det enda som behövs som lästext. En Marp-lektion saknas men kan byggas direkt från den. En fristående övning saknas specifikt för async — behöver skapas.

---

## 4. SlarvigKod — utförlig beskrivning

**Sökvägar:**
- Övnings-version: `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/refactoring/refactoring_live_example/`
- Live-coding-version: `/home/marcus/git/Old_courses/2025/3_advanced_oop/live/refactoring_playground/`
- Lösning: `exercises/refactoring/refactoring_live_example_solution/`

### Vad är det?

SlarvigKod är ett komplett pedagogiskt projekt designat för att träna refactoring och Clean Code. Det är ett fungerande C# konsolprogram — ett butikssystem med CRUD-operationer — som är **avsiktligt skrivet dåligt**. Programmet fungerar korrekt men bryter mot nästan alla Clean Code-principer.

### Vad programmet gör

Ett enkelt lagersystem med meny:
1. Visa produkter
2. Lägg till produkt
3. Sälj produkt
4. Lägg till lager
5. Sök produkt
6. Rabattkalkylator
7. Statistik
8. Avsluta

Seed-data innehåller Laptop, Mus, Tangentbord, Skärm, Hörlurar.

### Inbyggda code smells (dokumenterade i CODE_SMELLS.md)

10 väldefinierade code smells — varje smell har namn, förklaring, kod-exempel och lösningsförslag:

1. **Long Method** — allt i Main(), 150+ rader
2. **Mysterious Name** — variabler heter `n`, `p`, `q`, `c`, `s`, `f`, `a`, `b`, `t`, `m`, `tv`, `tp`, `hp`, `lp`, `hn`, `ln`
3. **Primitive Obsession** — tre parallella listor (`List<string>`, `List<double>`, `List<int>`) istället för en `Product`-klass
4. **Magic Numbers** — `0.25` (moms), `5` (låg lagernivå), `999999` (initial lägsta pris), rabattsatser `0.9`, `0.85` osv.
5. **Duplicated Code** — samma `Parse()`-mönster upprepas utan felhantering
6. **Data Clumps** — `n[i]`, `p[i]`, `q[i]` förekommer alltid tillsammans men är aldrig en klass
7. **Long Parameter List** — metoder tar in tre listor som parametrar
8. **Feature Envy** — beräkningsmetoder är hungriga på data som borde ägas av en `Product`-klass
9. **Inappropriate Intimacy** — direktaccess till listorna istället för encapsulation
10. **Shotgun Surgery** — ändra momssatsen kräver ändringar på fem ställen

### Pedagogisk struktur (LARAR_GUIDE.md)

Fem faser med tidsplan:
- **Fas 1 — Introduktion (15 min):** Visa koden, låt studerande diskutera "vad är fel?"
- **Fas 2 — Live-demo (30 min):** Läraren visar live: skapa Product-klass, konvertera listor
- **Fas 3 — Självständigt arbete (2–3 h):** Studerande följer REFACTORING_CHECKLIST.md
- **Fas 4 — Code Review (30 min):** Par-programmering, jämför lösningar
- **Fas 5 — Visa lösningen (30 min):** Gå igenom solution/-mappen, förklara design decisions

**Total tid: 4–6 timmar.** Passar en hel kursdag.

### Refactoring Checklist (REFACTORING_CHECKLIST.md)

10 fas-specifika checklister som leder studeranden steg för steg:

1. Skapa Product-klass
2. Extrahera metoder ur Main
3. Byt namn på alla variabler (full mappning: `c` → `userChoice`, `tv` → `totalValue` etc.)
4. Extrahera konstanter (VAT_RATE, LOW_STOCK_THRESHOLD)
5. Skapa InventoryManager-klass
6. Lägg till felhantering (try-catch runt alla Parse)
7. UI-separation (MenuUI-klass)
8. Statistik-klass (bonus)
9. Rabattkalkylator-klass (bonus)
10. Filhantering (bonus)

### Färdig lösning (refactoring_live_example_solution/)

Lösningen är splittad i 7 klasser:
- `Program.cs` — 4 rader entry point
- `Product.cs` — datamodell
- `InventoryManager.cs` — affärslogik
- `MenuUI.cs` — UI-separation
- `DiscountCalculator.cs` — rabattlogik
- `InventoryStatistics.cs` — statistikmodell
- `Constants.cs` — alla konstanter samlade

**Metriken:** 150+ rader i Main → 4 rader i Main. Det är den siffran att skriva på tavlan.

### G/VG-kriterier (direkt från LARAR_GUIDE.md)

**G:** Product-klass skapad, minst 5 extraherade metoder, beskrivande variabelnamn, magic numbers borta, programmet fungerar.

**VG:** Separation of concerns (minst 4 klasser), felhantering, DRY konsekvent, SOLID-principer synliga, kan förklara alla design decisions.

### Varför det är ett pedagogiskt guldkorn

- Det är ett **fungerande program** — studerande vet vad de ska behålla
- Alla smells är **namngivna och dokumenterade** — ingen gissningslek
- Lösningen visar **konkret förbättring** med mätbara metrics (150 → 4 rader)
- Det finns en **koppling framåt**: LARAR_GUIDE.md föreslår Repository Pattern, Strategy Pattern, Factory Pattern och unit tests som nästa steg — perfekt brygga till Design Patterns-veckan
- Live-playground-versionen i `live/refactoring_playground/` har exakt samma filer — kan användas för teacher live-coding och studerande övning separat

---

## 5. Bokkapitel → Marp-lektion → Vecka (mappning)

| Bokkapitel (exakt sökväg) | Marp-lektion (2025) | Vecka |
|---|---|---|
| `csharp_cmyh/C-Sharp/oop/inheritance.md` | `inheritance_abstracts_marp.md` | Vecka 1 |
| `csharp_cmyh/C-Sharp/oop/polymorphism/abstractclasses/` | `inheritance_abstracts_marp.md` | Vecka 1 |
| `csharp_cmyh/C-Sharp/oop/polymorphism/interfaces/` | `inheritance_abstracts_marp.md` | Vecka 1 |
| `csharp_cmyh/C-Sharp/entityframework/EntityFrameworkCore.md` | `entity_framework_advanced_marp.md` | Vecka 2 |
| `csharp_cmyh/C-Sharp/entityframework/example.md` | `entity_framework_advanced_marp.md` | Vecka 2 |
| `csharp_cmyh/C-Sharp/entityframework/migrationer.md` | `entity_framework_advanced_marp.md` | Vecka 2 |
| `2022/Kodkvalite/CleanCode/` (6 filer) | `refactoring_marp.md` / `refactoring_basics_marp.md` | Vecka 3 |
| `2022/Kodkvalite/Refaktorering.md` | `refactoring_marp.md` | Vecka 3 |
| `2024/.../06_repetition/01a–01e_*.md` (SOLID per princip) | `solid_marp.md` | Vecka 3 |
| `2023/csharp/DesignPatterns/lecture1/` (Factory, Singleton) | `creational_patterns_marp.md` | Vecka 3 |
| `2023/csharp/DesignPatterns/lecture3/05_observer.md + 07_strategy.md` | `behavioral_patterns_marp.md` | Vecka 3 |
| `csharp_cmyh/C-Sharp/asynkron/index.md` | Ingen Marp finns — **behöver skapas** | Vecka 4 |
| `2024/quick_repetition/17_linq.md + 18_fluent_linq.md` | Ingen Marp finns — **behöver skapas** | Vecka 4 |
| `csharp_cmyh/C-Sharp/api/rest_fundamentals.md` | `api_clients_marp.md` / `aspnet_marp.md` | Vecka 5 |
| `csharp_cmyh/C-Sharp/asp.net/aspnetcore/` | `aspnet_marp.md` | Vecka 5 |

---

## 6. Vad som saknas och behöver skapas

| Ämne | Status | Vad behövs |
|---|---|---|
| LINQ + lambda-uttryck | Fragmenterat. `17_linq.md` + `18_fluent_linq.md` finns. | En Marp-lektion (utgå från 17+18 + FluentLinq.cs) + övning |
| async/await | Texter finns (`asynkron/index.md` är utmärkt). | En Marp-lektion + fristående övning (ej EF-kopplad) |
| SOLID (fristående lektion) | `solid_marp.md` finns. Per-princip-texterna `01a–01e.md` finns. | Stämmer — kan användas |
| SlarvigKod-övning | Komplett och redo. | Adapteras till CLO26-terminologi |
| Design Patterns (Factory, Singleton, Observer, Strategy) | Slides finns. C#-kod finns. | Kan återanvändas |
| EF Core intro | Text + slides finns. | Kan återanvändas |
| ASP.NET intro | Slides + guides finns. | Kan återanvändas |

---

## 7. Rekommendationer

1. **SlarvigKod** kan användas nästan direkt för vecka 3 (Clean Code/refactoring). Justera terminologi till "studerande" och ta bort emojis i kodkommentarer om det matchar Marcus-rösten.

2. **LINQ-lektion** behöver skapas. Bygg på `17_linq.md` + `18_fluent_linq.md` + Marcus's egna FluentLinq.cs (2022). Vinkla det som "SQL-liknande i C#" — studerande har sett SQL.

3. **async/await-lektion** behöver skapas som Marp. Boken `asynkron/index.md` är redan redo som lästext. Pasta-metaforen håller.

4. **Design Patterns:** `2023/csharp/DesignPatterns/lecture1/` och `lecture3/` är direktanvändbara för Factory (slide `02_simple_factory.md`), Singleton (`07_singleton.md`), Observer (`05_observer.md`) och Strategy (`07_strategy.md`).

5. **SOLID-per-princip-texterna** i `2024/.../06_repetition/01a–01e_*.md` är korta och pedagogiska — läs dem, de är bättre än de långa lecture-filerna.

---

*Skapad: 2026-06-15 av Claude (Sonnet 4.6)*
