---

title: Övning 2: Entity Framework Smärtpunkter
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/exercises/document_database/document_database_exercise_2.md"
description: "Efter den här övningen kommer du att kunna:"
tags: ["csharp", "database", "databaser", "document", "entity", "exercise", "framework", "git", "smärtpunkter", "ssh"]
week_fit: []
---

# Övning 2: Entity Framework Smärtpunkter

🟢


## 🎯 Mål med övningen

Efter den här övningen kommer du att kunna:

- Identifiera vanliga problem med Entity Framework i komplexa scenarion
- Förstå när migrations och strikta scheman blir begränsande
- Analysera N+1 query-problemet
- Utvärdera om ett problem löses bättre med NoSQL

## 🧩 Uppgiften

Ni har fått i uppgift att granska ett befintligt EF-baserat system och identifiera **smärtpunkter**.

Systemet är en **blogplattform** där:
- Användare kan skriva artiklar
- Artiklar kan ha kommentarer
- Kommentarer kan ha svar (nested comments)
- Artiklar kan ha taggar
- Artiklar kan ha olika typer av innehåll (text, bilder, kod, video embeds)

## 🔍 Del 1: Analysera EF-Modellen

Här är den nuvarande modellen:

```csharp
public class Article {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorId { get; set; }
    public User Author { get; set; }
    public List<Comment> Comments { get; set; }
    public List<ArticleTag> ArticleTags { get; set; }
}

public class Comment {
    public int Id { get; set; }
    public string Text { get; set; }
    public int ArticleId { get; set; }
    public Article Article { get; set; }
    public int? ParentCommentId { get; set; }
    public Comment ParentComment { get; set; }
    public List<Comment> Replies { get; set; }
}

public class Tag {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ArticleTag> ArticleTags { get; set; }
}

public class ArticleTag {
    public int ArticleId { get; set; }
    public Article Article { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}
```

**Frågor att diskutera:**

1. **Hur många tabeller behövs?**

2. **Vad händer när du vill visa en artikel med alla kommentarer och taggar?**
   - Hur många queries körs?
   - Hur skriver du EF-koden för att undvika N+1-problemet?

3. **Vad händer om vi vill lägga till "reactions" (👍, ❤️, 😂) till kommentarer?**
   - Behövs en ny tabell?
   - Behövs en migration?
   - Hur påverkar det existerande kod?

## 💥 Del 2: N+1 Problem

Här är kod som läser artiklar:

```csharp
var articles = await context.Articles.ToListAsync();

foreach (var article in articles) {
    Console.WriteLine($"Title: {article.Title}");
    Console.WriteLine($"Author: {article.Author.Name}"); // ⚠️
    Console.WriteLine($"Comments: {article.Comments.Count}"); // ⚠️
}
```

**Frågor:**

1. **Hur många queries körs om det finns 10 artiklar?**

2. **Varför kallas detta "N+1 problem"?**

3. **Hur fixar du det med `.Include()`?** Skriv den korrekta koden.

4. **Vad händer om du också vill ladda Replies för varje Comment?** Hur ser `.Include()` ut då?

## 🛠️ Del 3: Migration Helvete

Marketing kommer med ett nytt krav:

> "Vi vill att vissa artiklar ska kunna ha en **Hero Image**, en **Video Embed**, och **Code Snippets** med syntax highlighting."

**I EF måste du:**

1. Lägga till nya kolumner i `Articles`-tabellen
2. Skapa en migration
3. Köra migration på produktionsdatabasen
4. Uppdatera alla ställen där `Article` används

**Scenario:**

- Ni är 3 utvecklare som jobbar parallellt
- Utvecklare A skapar en migration för "Hero Image"
- Utvecklare B skapar en migration för "Video Embed"
- Utvecklare C skapar en migration för "Reading Time Estimate"

Alla mergear till main samma dag.

**Frågor:**

1. **Vad händer med migrations?**

2. **Kan alla tre migrations köras utan konflikt?**

3. **Hur löser ni detta i team?**

4. **Hur hade detta varit i MongoDB?**

## 🎨 Del 4: Flexibelt Innehåll

En artikel kan nu innehålla **olika typer** av innehåll:

- Textblock
- Bildblock (med caption)
- Kodblock (med språk och syntax highlighting)
- Video embeds (YouTube, Vimeo)
- Quotes (med författare)

**I SQL/EF:**

Hur modellerar ni detta?

Alternativ:
- En `Content`-tabell med `Type` kolumn (polymorphic)
- Separata tabeller för varje typ (`TextBlock`, `ImageBlock`, etc.)
- JSON-kolumn i `Articles`

Diskutera för- och nackdelar med varje approach.

## 🌟 Del 5: MongoDB Alternativ

Nu designar ni samma blogplattform i MongoDB.

Här är ett förslag:

```json
{
  "_id": "article_123",
  "title": "Why NoSQL Is Cool",
  "slug": "why-nosql-is-cool",
  "author": {
    "id": "user_42",
    "name": "Marcus Medina",
    "avatar": "https://..."
  },
  "content": [
    {
      "type": "text",
      "value": "NoSQL databases are flexible..."
    },
    {
      "type": "image",
      "url": "https://...",
      "caption": "MongoDB logo"
    },
    {
      "type": "code",
      "language": "csharp",
      "value": "var x = 42;"
    }
  ],
  "tags": ["nosql", "mongodb", "databases"],
  "comments": [
    {
      "id": "comment_1",
      "user": "Luke",
      "text": "Great article!",
      "reactions": { "👍": 5, "❤️": 2 },
      "replies": [
        {
          "id": "comment_2",
          "user": "Leia",
          "text": "I agree!"
        }
      ]
    }
  ],
  "createdAt": ISODate("2025-01-15T10:00:00Z"),
  "views": 1337
}
```

**Frågor:**

1. **Hur många queries behövs för att visa denna artikel?**

2. **Hur lägger du till en ny content-type (t.ex. "poll")?**
   - Behövs en migration?
   - Behövs schema-ändringar?

3. **Vad händer om författaren ändrar sitt namn från "Marcus" till "Marcus Medina"?**
   - I SQL?
   - I MongoDB?

4. **Hur söker du efter alla artiklar taggade med "nosql"?**

## 🤝 Diskussion i paret

Snacka igenom:

1. **Vilka av EF-problemen försvinner med MongoDB?**

2. **Vilka NYA problem introduceras med MongoDB?**

3. **Om ni var tech lead, vilken databas skulle ni välja för denna blogg?** Varför?

## 🔥 BONUS: Kodbyte och Analys

Byt kod/design med ett annat par.

Granska deras MongoDB-design och leta efter:

1. **Duplicerad data** - Finns samma info på flera ställen?
2. **Djupt nested data** - Är dokument svåra att navigera?
3. **Saknade index** - Vilka fält borde ha index?
4. **Säkerhetsrisker** - Kan känslig data exponeras?

Ge feedback till varandra!

## 💭 Reflektionsfrågor

1. **Har ni själva stött på liknande EF-problem i era projekt?** Ge exempel.

2. **Vad är det VÄRSTA som kan hända om man migrerar från SQL till MongoDB mitt i ett projekt?**

3. **Finns det en medelväg?** (Hint: Hybrid-arkitekturer)

<details>
<summary>💡 Klicka här för lösningsförslag och diskussionspoäng</summary>

## Del 1: EF-Modellen

**Antal tabeller:** Minst 5
- `Articles`
- `Users`
- `Comments`
- `Tags`
- `ArticleTags` (join-tabell)

**Query för att visa artikel med allt:**

```csharp
var article = await context.Articles
    .Include(a => a.Author)
    .Include(a => a.Comments)
        .ThenInclude(c => c.Replies)
    .Include(a => a.ArticleTags)
        .ThenInclude(at => at.Tag)
    .FirstOrDefaultAsync(a => a.Id == articleId);
```

Det blir **en** query, men den är **enorm** och kan bli långsam.

## Del 2: N+1 Problem

**Utan Include:**
- 1 query för articles
- 10 queries för authors (en per artikel)
- 10 queries för comments (en per artikel)
- **Total: 21 queries**

**Med Include:**

```csharp
var articles = await context.Articles
    .Include(a => a.Author)
    .Include(a => a.Comments)
        .ThenInclude(c => c.Replies)
    .ToListAsync();
```

Nu blir det **1 query** (men med stora JOINs).

## Del 3: Migration Konflikt

När tre utvecklare skapar migrations parallellt:

```
Migration1: AddHeroImageToArticles
Migration2: AddVideoEmbedToArticles
Migration3: AddReadingTimeToArticles
```

**Problem:**
- Migration2 förväntar sig att databasen är i state efter Migration1
- Men Migration3 vet inget om Migration1 eller Migration2
- När alla mergeas till main blir ordningen fel

**Lösning:**
- Merge migrations i rätt ordning
- Eller: Squasha migrations innan merge
- Eller: Använd tools som FluentMigrator

**I MongoDB:**
Inget migration-problem. Lägg bara till fält när du vill.

## Del 4: Flexibelt Innehåll

**Alternativ 1: Polymorphic Table**

```csharp
public class ContentBlock {
    public int Id { get; set; }
    public string Type { get; set; } // "text", "image", "code"
    public string Data { get; set; } // JSON-serialiserad data
}
```

Problem: Ingen type safety. `Data` är bara en string.

**Alternativ 2: Separate Tables**

```csharp
public class TextBlock { ... }
public class ImageBlock { ... }
public class CodeBlock { ... }
```

Problem: Svårt att hämta i rätt ordning.

**Alternativ 3: JSON Column**

```csharp
public class Article {
    public string ContentJson { get; set; } // JSON array
}
```

Problem: Ingen EF-validering. Förlorar relationsdatabas-fördelar.

**MongoDB Approach:**

Inget problem! Content är bara en array av objekt:

```json
"content": [
  { "type": "text", "value": "..." },
  { "type": "image", "url": "...", "caption": "..." }
]
```

Lägg till nya typer när du vill.

## Del 5: MongoDB Tradeoffs

**Fördelar:**
- 1 query för hela artikeln
- Ingen migration för nya content-types
- Snabbt att läsa

**Nackdelar:**
- Om författare byter namn måste DU uppdatera alla artiklar
- Ingen foreign key constraint (kan referera användare som inte finns)
- Risk för dokument över 16 MB (MongoDB max size)

**Lösning för namnändring:**

Istället för att duplicera användardata, spara bara ID:

```json
"author": {
  "id": "user_42"
}
```

Och hämta användarnamn från en separat `users`-collection när du visar artikeln.

Detta kallas **reference pattern** i MongoDB.

## Hybrid-Arkitektur för Blogg

**SQL (affärskritiskt):**
- Users
- Subscriptions
- Payments

**MongoDB (flexibelt):**
- Articles (med comments embedded)
- Analytics
- Session logs

API-lagret kombinerar data från båda.

## Slutsats

Det finns **ingen perfekt lösning**.

Välj baserat på:
- **Affärskritisk data?** → SQL
- **Flexibel struktur?** → MongoDB
- **Komplexa relationer?** → SQL
- **Hög läslast?** → MongoDB
- **Strikta transaktioner?** → SQL

Ofta: **Använd båda**.

</details>

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
