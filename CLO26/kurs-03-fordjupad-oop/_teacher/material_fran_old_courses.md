# Inventering av gammalt kursmaterial — CLO26 kurs-03

> Genererad: 2026-06-15  
> Syfte: Kartlägga återanvändbart material för Fördjupad OOP (5 veckor)  
> Inventerat av: Claude (automatisk genomgång av /home/marcus/git/Old_courses/)

---

## Arv, polymorfism, abstrakt klass, interface (Vecka 1)

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/inheritance_abstracts/inheritance_abstracts_marp.md` | Marp-slides (1372 rader) | C# | Modern — full täckning: abstract, virtual, sealed, interface vs abstract, protected, multiple inheritance-problemet, repository-exempel | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/inheritance_abstracts/inheritance_exercise_1.md` | Övning | C# | Bra — Superhero-hierarki med abstract class, abstract method, virtual method. Tips i `<details>`-fällor + inbyggd lösning | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/inheritance_abstracts/inheritance_exercise_2.md` | Övning | C# | Bra — parallell övning med annat tema | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/inheritance_abstracts/inheritance_virtual_exercise_1.md` t.o.m. `_exercise_8.md` | Övningsserier (8 filer) | C# | Bred — täcker virtual/override i många varianter | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/inheritance_abstracts/inheritance_exercise_3.md` | Övning | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/abstraction_training.md` | Träningsfrågor (flerval) | C# | Bra — interaktiv med `<details>` för svar | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/inheritance_training.md` | Träningsfrågor (flerval) | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/interfaces_training.md` | Träningsfrågor (flerval) | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/polymorphism_training.md` | Träningsfrågor (flerval) | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2022/Polymorfism/ReadmeGenerator/ReadmeGenerator/` | Livekodsexempel | C# | Gammal stil — interface `IDocumentGenerator`, HTML- och Markdown-implementationer. Tydligt och enkelt polymorfism-exempel | Referens/inspiration |
| `/home/marcus/git/Old_courses/2022/Repetition/RepetitionInterfacesCLO22/` | Kodbas | C# | Gammal stil — `Ogre`, `Troll` implementerar ett interface, Game-klass. Väldigt enkelt, bra som första exempel | Behöver moderniseras |
| `/home/marcus/git/Old_courses/2023/java/OOP/` | Föreläsningar + övningar | Java | Gammal — Java-syntax, används som referens för pedagogik | Referens/inspiration |

**Kommentar vecka 1:** Det finns ett komplett, modernt C#-block från CLO25 (2025). Slidesen är 1372 rader och täcker allt från enkel arvskedja till repository-pattern via interface. Övningsbanken är rik — minst 11 övningsfiler på enbart detta ämne.

---

## Entity Framework Core, ORM, databasintegration (Vecka 2)

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/entity_framework/entity_framework_advanced_marp.md` | Marp-slides (1108 rader) | C# | Modern — EF Core med LocalDB, SSMS-integration, CRUD, relationer, migrations, connection strings, EF i ASP.NET Razor Pages | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/entity_framework/ef_exercise_1.md` | Övning | C# | Modern — BlogSystem med 1-to-Many och Many-to-Many, Repository Pattern, Sqlite och MySQL | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/2_databases/lectures/ef/03_entity_framework_magin_marp.md` | Marp-slides | C# | Modern — "EF-magin" intro, Code First, migrations, DbContext | Referens/inspiration |
| `/home/marcus/git/Old_courses/2025/2_databases/exercises/ef/03_00_hello_ef.md` t.o.m. `03_12_moviemix.md` | Övningsserier (12 filer) | C# | Mycket bra — progressiv serie från hello_ef till komplexa relationer. Teman: heroes, diary, pizza, hogwarts, plantpal, zombie, efmatch, moviemix | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/2_databases/exercises/repetition/3_repeat_cs_ef/SupportTicketApp/` | Kodbas | C# | Modern — SupportTicketApp med DbContext, Models (Customer, SupportAgent, Ticket, TicketMessage). Bra repetitionsexempel | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/2_databases/lectures/ef_core/part2_mysql_docker/` | Föreläsning + övningar | C# | Modern — MySQL i Docker, säkerhet, backup/restore | Referens/inspiration |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/ef_aspnet/` | Föreläsning (13 filer) | C# | Modern — EF + ASP.NET MVC, Razor Pages, MongoDB-alternativ, tag helpers. Komplett lärarmaterial med teacher_guide | Referens/inspiration |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/entity_framework_mongodb_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2022/Tentarepetition/EntityFrameworkRepetition/EntityFrameworkRepetition/` | Kodbas | C# | Gammal stil — `MinContext : DbContext` med Many-to-Many Person↔Dog, LocalDB. Kommentarer på svenska. Gammal C#-syntax | Referens/inspiration |
| `/home/marcus/git/Old_courses/2024/csharp/3_oop_adv/lectures/06_advanced_db_frameworks/` | Föreläsning | C# | Medel — databas-frameworks, gammal struktur men C#-relevant | Referens/inspiration |

**Kommentar vecka 2:** Utmärkt täckning. Mest värdefull är övningsserien från 2025/2_databases/exercises/ef/ med 12 progressiva övningar. `ef_exercise_1.md` (BlogSystem) är ett bra C#-klart exempel. EF-slides från advanced_oop 2025 är moderna och täcker LocalDB+SSMS vilket är CLO26-relevant.

---

## Design Patterns: Factory, Singleton, Observer, Strategy (Vecka 3)

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/creational_patterns/creational_patterns_marp.md` | Marp-slides (1541 rader) | C# | Modern — Factory, Factory Method, Builder, Singleton, Abstract Factory, Prototype. Anti-patterns, testbarhet, Real-World-exempel | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/behavioral_patterns/behavioral_patterns_marp.md` | Marp-slides (1323 rader) | C# | Modern — Strategy, Observer, Command, Template Method, State. Swish/PayPal som Strategy-exempel | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/creational_patterns/creational_exercise_1.md` | Övning | C# | Bra — Factory Pattern för databasanslutning (Dev/Test/Prod) med IDatabaseConnection-interface. Tony Stark/JARVIS-tema | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/creational_patterns/creational_exercise_2.md` | Övning | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/behavioral_patterns/behavioral_exercise_1.md` | Övning | C# | Bra — Strategy Pattern med IPaymentStrategy, CreditCard/Swish/PayPal. Inkluderar `ShoppingCart` som Context-klass | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/design_patterns_overview_marp.md` | Marp-slides | C# | Modern — översikt av alla patterns, varför/när | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/factory_pattern_training.md` | Träningsfrågor (flerval) | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/singleton_pattern_training.md` | Träningsfrågor (flerval) | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/strategy_pattern_training.md` | Träningsfrågor (flerval) | C# | Bra — inkluderar Open/Closed-kopplingen | Kan användas direkt |
| `/home/marcus/git/Old_courses/2022/Tentarepetition/FactoryPatternPizza/FactoryPatternPizza/Program.cs` | Kodbas | C# | Gammal stil — `PizzaFactory.CreatePizza(string type)` med switch-sats. Enkel, pedagogisk, men allt i en fil | Referens/inspiration |
| `/home/marcus/git/Old_courses/2022/Tentarepetition/SingletonRepetition/SingletonRepetition/Program.cs` | Kodbas | C# | Gammal stil — `Settings`-klass med private static `_instance` och `GetInstance()`. Klassiskt lazy singleton, kommentarer på svenska | Referens/inspiration |
| `/home/marcus/git/Old_courses/2023/java/DesignPatterns/lecture1/` | Föreläsning | Java | Medel — Factory, Factory Method, Abstract Factory, Builder, Prototype, Singleton. Marp-slides med bilder. Java-syntax men konceptuellt solid | Referens/inspiration |
| `/home/marcus/git/Old_courses/2023/java/DesignPatterns/lecture2/` | Föreläsning | Java | Medel — Adapter, Bridge, Composite, Decorator, Facade, Flyweight, Proxy. Med kod och bilder | Referens/inspiration |
| `/home/marcus/git/Old_courses/2023/java/DesignPatterns/lecture3/` | Föreläsning | Java | Medel — Observer, Strategy, Command, Iterator, Mediator, State, Template, Visitor. Med kod och UML-bilder | Referens/inspiration |
| `/home/marcus/git/Old_courses/2024/csharp/3_oop_adv/lectures/02_design_patterns/` | Föreläsning (20 filer) | C# | Medel — Design Patterns för databashantering, UML, SOLID i databas-context. Äldre struktur | Referens/inspiration |

**Kommentar vecka 3:** Rikt material. De två Marp-filerna från 2025 (creational + behavioral) täcker alla fyra patterns (Factory, Singleton, Observer, Strategy) med moderna C#-exempel. Övningarna är välstrukturerade. Java-materialet från 2023 är bra för pedagogisk inspiration men behöver konverteras.

---

## SOLID-principerna, Clean Code, Refactoring (Vecka 4)

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/solid/solid_marp.md` | Marp-slides (1043 rader) | C# | Modern — alla fem SOLID-principer med dåliga/bra kodexempel, Mermaid-diagram, praktisk kodjämförelse | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/refactoring/refactoring_marp.md` | Marp-slides | C# | Modern | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/refactoring/refactoring_basics_marp.md` | Marp-slides | C# | Modern | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/solid/solid_exercise_1.md` | Övning | C# | Bra — SRP med `UserManager` God Object som ska brytas ut till User/UserRepository/EmailService/UserValidator/ActivityLogger | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/solid/solid_exercise_2.md` | Övning | C# | Bra — OCP eller annan SOLID-princip | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/solid/solid_exercise_3.md` | Övning | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/refactoring/refactoring_exercise_1.md` | Övning | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/refactoring/refactoring_live_example/Program.cs` | Livekodsexempel (dålig kod) | C# | Modern — SlarvigKod-projektet: butikssystem med `List<string> products`, `List<double> prices`, `List<int> quantities` som separata listor, ingen OOP. Perfekt refaktoreringsexempel | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/refactoring/refactoring_live_example_solution/` | Livekodsexempel (refaktorerat) | C# | Modern — Product-klass, DiscountCalculator, InventoryManager, InventoryStatistics, MenuUI, Constants. Alla SRP | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/refactoring/refactoring_live_example/CODE_SMELLS.md` | Lärarmaterial | C# | Bra — dokumenterar code smells i SlarvigKod | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/refactoring/refactoring_live_example/LARAR_GUIDE.md` | Lärarmaterial | C# | Bra — lärarguide för live-session | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/refactoring/refactoring_live_example/REFACTORING_CHECKLIST.md` | Studentmaterial | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/solid_principles_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/refactoring_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2022/Kodkvalite/CleanCode/CleanCode-Klasser.md` | Läsartikel | C# | Gammal stil — SRP med Filemanager-exempel. Text är fortfarande pedagogiskt klar | Referens/inspiration |
| `/home/marcus/git/Old_courses/2022/Kodkvalite/CleanCode/CleanCode-Namngivning.md` | Läsartikel | C# | Gammal stil — tydlig, välskriven | Referens/inspiration |
| `/home/marcus/git/Old_courses/2022/Kodkvalite/CleanCode/CleanCode-Metoder.md` | Läsartikel | C# | Gammal stil — max 10 rader-regeln, exempel | Referens/inspiration |
| `/home/marcus/git/Old_courses/2022/Kodkvalite/CleanCode/CleanCode-Struktur.md` | Läsartikel | C# | Gammal stil | Referens/inspiration |
| `/home/marcus/git/Old_cores/2022/Kodkvalite/Refaktorering.md` | Läsartikel | C# | Gammal stil — switch-expressions, expression-bodied members, if-forenklingar. Gammal men fortfarande relevant | Behöver moderniseras |
| `/home/marcus/git/Old_courses/2024/csharp/3_oop_adv/lectures/04_clean_code/` | Föreläsning (20 filer) | C# | Medel — 5 lektioner: grundprinciper, refactoring-patterns, SOLID, clean architecture, code review. Marp + texter + quiz + exempel | Referens/inspiration |
| `/home/marcus/git/Old_courses/2024/csharp/3_oop_adv/lectures/04_clean_code/3_solid_principles.md` | Läsartikel | C# | Medel — Mermaid-diagram, tydlig text, exempel med `UserManager`. Något överdrivet kommenterad (ChatGPT-stil) | Behöver moderniseras |
| `/home/marcus/git/Old_courses/2023/java/CleanCode/article1.md` t.o.m. `article10.md` | Läsartiklar (10 filer) | Java | Medel — Java-kodstandarder: packagenamn, imports, namngivning, metoder. God pedagogisk form, Java-syntax | Referens/inspiration |

**Kommentar vecka 4:** Refactoring-liveexemplet (SlarvigKod + RefactoredShop) är ett fantastiskt pedagogiskt verktyg — finns både dålig och bra version med lärarguide. SOLID-slides är moderna och kompletta. 2022 Kodkvalite-artiklarna är lite gamla men välskrivna på svenska — bra som kompletterande läsmaterial.

---

## API/REST, ASP.NET intro, LINQ, async/await (Vecka 4–5)

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/api_clients/api_clients_marp.md` | Marp-slides | C# | Modern — HTTP-klienter, REST-anrop, JSON-deserialisering | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/api_clients/api_clients_basics_marp.md` | Marp-slides | C# | Modern | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/api_clients/api_clients_advanced_marp.md` | Marp-slides | C# | Modern | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/api_clients/MultiAIDemo/` | Kodbas | C# | Modern — `IAIProvider`-interface, `AIProviderBuilder`, 8 konkreta providers (Ollama, Groq, Gemini, etc.). Visar Factory + Interface i verklig API-kontext. Top-level statements, async/await genomgående | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/api_clients/WolframConsoleApp/` | Kodbas | C# | Modern — `WolframService` med HttpClient, async `GetResultAsync()`. Enkel och tydlig | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/api_clients/api_clients_exercise_1.md` | Övning | C# | Bra — REST API-klient-övning | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/api_clients/wolfram_alpha_exercise.md` | Övning | C# | Bra — konkret API-övning med lösning | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/api_clients/ai_chatbot_exercise.md` | Övning | C# | Bra — chatbot mot lokal AI-API, async/await | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exercises/aspnet/aspnet_exercise_1.md` | Övning | C# | Modern — ASP.NET Core Middleware, DI, `ILogger`, async `InvokeAsync`, request timing | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/aspnet/aspnet_marp.md` | Marp-slides | C# | Modern — ASP.NET intro | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/aspnet/01-asp-taggar-grunder.md` t.o.m. `05-jamforelse.md` | Läsmaterial (5 filer) | C# | Modern — Tag Helpers, Razor-syntax, Blazor-jämförelse | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/extensions/extension_methods_marp.md` | Marp-slides | C# | Modern — Extension Methods, Fluent API, Records (C# 9), Extension Members (C# 14). Täcker LINQ-liknande mönster | Kan användas direkt |
| `/home/marcus/git/Old_courses/2022/Linq/LiveKod CLO22 Linq 2022-11-15/` | Kodbas | C# | Gammal stil — `FluentLinq.cs` med SQL-liknande LINQ-syntax + method syntax sida vid sida. `ExtensionsDemo.cs` och `ListExtensions.cs`. Kommentarer på svenska. Ursprungligen av Marcus Medina/Codic | Behöver moderniseras |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/lectures/api_clients/wolfram_alpha_lecture.md` | Läsartikel | C# | Modern — steg-för-steg guide för WolframAlpha-integration | Kan användas direkt |
| `/home/marcus/git/Old_courses/2024/java/5_api/` | Kurs (Java) | Java | Medel — Spring Boot API, REST-principer, controllers. Bra för konceptuell förståelse men Java | Referens/inspiration |
| `/home/marcus/git/Old_courses/2023/java/apiwebservices/` | Kurs (Java) | Java | Medel — REST/API 10 föreläsningar + övningar | Referens/inspiration |
| `/home/marcus/git/Old_courses/2023/csharp/Assignments/DAKVS/excersises/api/wolframalpha.md` | Övning | C# | Gammal — WolframAlpha-övning från 2023. Relevant tema men äldre format | Referens/inspiration |

**Kommentar vecka 4-5 (API/async):** LINQ saknar ett dedikerat modernt föreläsningsmaterial för C#. Det närmaste är `extension_methods_marp.md` (som visar LINQ-liknande mönster) och `FluentLinq.cs` från 2022 (gammal men pedagogiskt enkel). Async/await är representerat i API-klientmaterialet men saknar en fristående föreläsning. **Detta är det tydligaste innehållsgapet i kursen.**

---

## Examinationsmaterial

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/PROV_ADVANCED_OOP.md` | Tentafrågor (flerval, 30 frågor) | C# | Bra — 2 timmar, Del 1 OOP Fundamentals (14 frågor: Abstraction, Inheritance, Polymorphism, Interface), Del 2 (16 frågor: SOLID, Design Patterns, EF, API). Modern C#-kod i frågorna | Behöver moderniseras |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/abstraction_training.md` | Träningsfrågor | C# | Bra — interaktiv self-test med `<details>` | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/inheritance_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/interfaces_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/polymorphism_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/factory_pattern_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/singleton_pattern_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/strategy_pattern_training.md` | Träningsfrågor | C# | Bra — kopplar Strategy till OCP | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/solid_principles_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/refactoring_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/entity_framework_mongodb_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/exam/api_client_csharp_training.md` | Träningsfrågor | C# | Bra | Kan användas direkt |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/assignment/README.md` | Gruppuppgift | C# | Modern — CLO25-grupprojekt: Console/ASP.NET app med EF, caching, API-integration, 3+ design patterns, UML, Git Flow, Kanban. Teammedlemmar namngivna (bör anonymiseras) | Behöver moderniseras |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/assignment/teacher_matrix.md` | Bedömningsmatris | C# | Modern — G/VG-kriteriematris: teknisk implementation, design patterns, kodkvalitet, dokumentation, arbetsprocess | Behöver moderniseras |
| `/home/marcus/git/Old_courses/2025/3_advanced_oop/assignment/reflection-mall.md` | Mall | C# | Bra | Kan användas direkt |

**Kommentar examinationsmaterial:** `PROV_ADVANCED_OOP.md` är skriven för CLO25 och märkt som sådan — bör anpassas till CLO26. Bedömningsmatrisen och uppgiftsbeskrivningen är strukturmässigt utmärkta men innehåller CLO25-specifika uppgifter och riktiga studerandenamn som måste tas bort.

---

## Sammanfattning

### Finns det gott om

- **Arv, polymorfism, abstract, interface** — komplett block med slides (1372 rader), 11+ övningar, 4 träningsquiz
- **Entity Framework Core** — 12 progressiva övningar, moderna slides med LocalDB/SSMS
- **Design Patterns** — 3 kompletta Marp-filer (creational 1541 rader + behavioral 1323 rader + overview), övningar, quiz. Factory och Strategy är särskilt välrepresenterade
- **SOLID + Refactoring** — utmärkt livekodsexempel (SlarvigKod/RefactoredShop) med lärarguide, 3 SOLID-övningar, moderna slides
- **API-klienter och ASP.NET** — MultiAIDemo-kodbas visar async/await i verklig kontext, övningar finns
- **Träningsquiz för tentarepetition** — 12 filer med flervalsfrågor och inbyggda svar

### Saknas helt

- **LINQ — dedikerat modernt föreläsningsmaterial i C#**: Det enda som finns är `FluentLinq.cs` från 2022 (gammal syntax) och extensions-slides som tangerar LINQ-mönster. Behöver byggas från grunden
- **async/await — fristående föreläsning**: async/await förekommer i API-övningar men det finns ingen Marp-slide eller dedikerad text som undervisar konceptet isolerat (Task, await, async void vs async Task, deadlocks)
- **Observer Pattern i C# med events**: behavioral_patterns_marp.md täcker Observer, men det finns inget dedikerat C#-exempel som visar `event`/`EventHandler`/delegates kopplade till Observer Pattern

### Rekommenderad prioritering

1. **Direkt återanvändning** — Ta `inheritance_abstracts_marp.md`, `solid_marp.md`, `creational_patterns_marp.md`, `behavioral_patterns_marp.md`, `entity_framework_advanced_marp.md` och anpassa CLO25-märkningar till CLO26. Dessa är klara att använda
2. **Refactoring-liveexemplet** — `SlarvigKod`-projektet + `RefactoredShop`-lösningen är ett pedagogiskt ädelsten. Kopieras direkt med minimal anpassning
3. **Övningsbanken** — Välj ut ca 2 övningar per vecka från det befintliga materialet. Det finns mer än nog — välj scenarion som passar CLO26-gruppen
4. **Bygg LINQ-föreläsning** — Skapa ny Marp-fil baserad på `FluentLinq.cs` (2022) som referens + modernisera till C# 10+ syntax: method syntax, query syntax, vanliga operatorer (Where, Select, OrderBy, GroupBy, FirstOrDefault, Any, Sum)
5. **Bygg async/await-föreläsning** — Skapa ny fristående Marp-fil: Task, async/await, ConfigureAwait, gemensamma fällor. Kan använda `WolframConsoleApp/WolframService.cs` (2025) som liveexempel
6. **Anpassa examinationsmaterialet** — `PROV_ADVANCED_OOP.md` och `teacher_matrix.md` är utmärkta strukturmässigt men behöver CLO26-rubrik och anonymiserade studentnamn
