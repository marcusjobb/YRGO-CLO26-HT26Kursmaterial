# Inför tentan: läs och träna

Tentan är **onsdag 2 december, 09:00–12:00**, i exam.net. Inga hjälpmedel.

Du behöver inte kunna allt utantill. Tentan handlar om att **känna igen** och **förstå**:

- Du väljer rätt svar bland några alternativ.
- Du drar streck mellan begrepp och beskrivningar.
- Du tittar på ett klassdiagram och avgör vad som stämmer.

Du skriver ingen kod, och du skriver inga egna regex. Allt från vecka 4 (hell week) ingår **inte**.

Frågorna på tentan är nya, men de handlar om samma saker som här nedan. Gör gärna frågorna under "Testa dig själv" först. Då ser du direkt vad du redan kan och vad du behöver läsa mer om.

## Så här gör du

1. Läs anteckningen (📖).
2. Gör en övning (🟢) och titta på klassdiagrammet i den.
3. Gör frågorna under "Testa dig själv" (❓). Svara innan du tittar på svaret.

Hinner du bara en sak per område: gör frågorna. Det är där du lär dig mest.

## 1. Abstract och interface

Vad du ska kunna: skillnaden mellan en abstrakt klass ("är en sorts") och ett interface ("kan göra"), och när man väljer vilket.

- 📖 [Abstract och interface](../../01_trygg_start_oop/notes/abstract_och_interface.md)
- 📖 [Termer: OOP-repetition](../../01_trygg_start_oop/terms/oop_repetition.md)
- 🟢 [Övning 3: abstract och interface](../../01_trygg_start_oop/exercises/ovning_3_abstract_interface.md)
- 🟢 [Övning 4: garaget](../../01_trygg_start_oop/exercises/ovning_4_garaget.md)
- ❓ [Tentafrågor: klasser, arv, abstract och interface](../../01_trygg_start_oop/pre-exam/00_fragor_oop.md)

## 2. Databaser: ORM, NoSQL, SQL och MongoDB, EF Core

Vad du ska kunna:

- Vad en ORM är och vad EF Core gör åt dig.
- Vad NoSQL betyder.
- Skillnaden mellan SQL och MongoDB: tabeller mot dokument, och hur en lista sparas.
- Några EF-begrepp: vad `DbContext` är, vad en migration gör och hur en relation (en kund har många ordrar) syns i klasserna.

- 📖 [EF Core: spara objekt i en databas](../../02_ef_mongodb_designmonster/notes/ef_core_hello.md)
- 📖 [EF Core: relationer](../../02_ef_mongodb_designmonster/notes/ef_core_relationer.md)
- 📖 [MongoDB i lätt version](../../02_ef_mongodb_designmonster/notes/mongodb_light.md)
- 🟢 [Övning: hjältarna i databasen](../../02_ef_mongodb_designmonster/exercises/ovning_ef_core_hjaltarna.md)
- 🟢 [Övning: hjältarnas uppdrag](../../02_ef_mongodb_designmonster/exercises/ovning_ef_core_uppdrag.md)
- 🟢 [Övning: Mira i en låda (MongoDB)](../../01_trygg_start_oop/exercises/ovning_mongodb_light.md)
- ❓ [Tentafrågor: ORM, NoSQL och entiteter](../../02_ef_mongodb_designmonster/pre-exam/01_fragor_orm_nosql.md)
- ❓ [Tentafrågor: databaser, fil, minne och server](../../01_trygg_start_oop/pre-exam/02_fragor_databaser.md)

## 3. Designmönster: Strategy, Factory, Observer och Builder

Vad du ska kunna: förklara varje mönster med egna ord, känna igen det i en kodsnutt och i en vardagssituation. Det är de **fyra** mönstren som gäller. Resten av överblicken behöver du inte läsa för tentan.

- 📖 [Strategy](../../02_ef_mongodb_designmonster/notes/strategy.md)
- 📖 [Factory](../../02_ef_mongodb_designmonster/notes/factory.md)
- 📖 [Observer](../../02_ef_mongodb_designmonster/notes/observer.md)
- 📖 [Builder](../../02_ef_mongodb_designmonster/notes/builder.md)
- 🟢 [Övning: Strategy](../../02_ef_mongodb_designmonster/exercises/ovning_strategy.md)
- 🟢 [Övning: Factory](../../02_ef_mongodb_designmonster/exercises/ovning_factory.md)
- 🟢 [Övning: Observer](../../02_ef_mongodb_designmonster/exercises/ovning_observer.md)
- 🟢 [Övning: Builder](../../02_ef_mongodb_designmonster/exercises/ovning_builder.md)
- ❓ [Quiz: mönstren](../../02_ef_mongodb_designmonster/lectures/quiz_patterns_marp.md)
- ❓ [Tentafrågor: designmönster och UML](../../02_ef_mongodb_designmonster/pre-exam/01_fragor_designmonster.md)

Vill du träna mer: [Factory: verkstadsjobb](../../03_refactoring_clean_code_kanban/exercises/factory_verkstadsjobb.md) och [Builder: bokning](../../03_refactoring_clean_code_kanban/exercises/builder_bokning.md).

## 4. UML: klassdiagram

Vad du ska kunna: läsa ett klassdiagram. Vad betyder `+` och `-`, `1` och `*`, pilen för arv och den streckade pilen för interface?

- 📖 [UML: en ritning av koden](../../02_ef_mongodb_designmonster/notes/uml_klassdiagram.md)
- 🟢 Titta på klassdiagrammet i varje övning ovan och läs det innan du läser koden.

## 5. Kanban

Vad du ska kunna: vad kolumnerna Backlog, Working, Test och Done betyder, och vad en WIP-gräns är.

- 📖 [Kanban](../../03_refactoring_clean_code_kanban/notes/kanban.md)
- 📖 [Termer: Kanban och regex](../../03_refactoring_clean_code_kanban/terms/kanban_och_regex.md)

## 6. Kodkvalitet: code smells, SOLID, DRY, KISS och refaktorering

Vad du ska kunna: förklara DRY, KISS och SRP, känna igen ett code smell (lång metod, magiskt tal, duplicerad kod), och veta att refaktorering betyder att koden blir bättre utan att den gör något annat.

- 📖 [Code smells](../../03_refactoring_clean_code_kanban/notes/code_smells.md)
- 📖 [SOLID, DRY och KISS](../../03_refactoring_clean_code_kanban/notes/solid_dry_kiss.md)
- 📖 [Refaktorering: Extract Method och Rename](../../03_refactoring_clean_code_kanban/notes/refaktorering.md)
- 📖 [Termer: kodkvalitet](../../03_refactoring_clean_code_kanban/terms/kodkvalitet.md)
- 🟢 [Övning: Extract Method och Rename](../../03_refactoring_clean_code_kanban/exercises/refaktorering_extract_method.md)
- 🟢 [Övning: refaktorering, enbokstavsnamn](../../03_refactoring_clean_code_kanban/exercises/refaktorering_enbokstav.md)
- ❓ [Tentafrågor: refaktorering, kodkvalitet och Kanban](../../03_refactoring_clean_code_kanban/pre-exam/01_refaktorering_och_kanban.md)

## 7. Regex: känna igen ett mönster

Vad du ska kunna: läsa ett enkelt regex och säga vad det används till. Vad betyder `^`, `$`, `\d`, `{3}` och `?`, och vad händer om `^` och `$` saknas?

- 📖 [Regex](../../03_refactoring_clean_code_kanban/notes/regex.md)
- 🟢 [Regexövningar](../../03_refactoring_clean_code_kanban/exercises/regex_ovningar.md)

## Måndag 30 november

Då repeterar vi databaser och designmönster tillsammans. Kom med frågor om det som känns kluddigt. Det är precis det vi är till för.
