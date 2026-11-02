# Live Coding 3: SQL vs MongoDB Design Showdown

🟢


**Tid:** 15-20 minuter
**Verktyg:** Whiteboard/Skärm + Compass
**Mål:** Visa designskillnader konkret

---

## 🎯 Manus för Marcus

### Scenario Introduction (2 min)

**SAG:**
> "Ni ska bygga ett blogg-system. Användare skriver artiklar. Artiklar har kommentarer. Hur designar vi detta?"

**RITA PÅ WHITEBOARD:**

```
Krav:
- Användare (namn, email)
- Artiklar (titel, innehåll, författare)
- Kommentarer (text, författare, artikel)
```

**SAG:**
> "Vi gör detta PÅ TVÅ SÄTT. Först SQL. Sen MongoDB. Jämför."

---

### SQL Design (5 min)

**SAG:**
> "SQL-tänk: Normalisera. Tre tabeller."

**RITA:**

```
┌─────────────┐
│   Users     │
├─────────────┤
│ Id (PK)     │
│ Name        │
│ Email       │
└─────────────┘
       ▲
       │
┌──────┴──────┐
│  Articles   │
├─────────────┤
│ Id (PK)     │
│ Title       │
│ Content     │
│ AuthorId FK │
└─────────────┘
       ▲
       │
┌──────┴──────┐
│  Comments   │
├─────────────┤
│ Id (PK)     │
│ Text        │
│ ArticleId FK│
│ AuthorId FK │
└─────────────┘
```

**SAG:**
> "Tre tabeller. Foreign keys. Normaliserat. Ingen duplicerad data."

---

**SAG:**
> "Visa en artikel med alla kommentarer - hur?"

**SKRIV SQL:**

```sql
SELECT
    a.Title,
    a.Content,
    u.Name AS Author,
    c.Text AS Comment,
    cu.Name AS Commenter
FROM Articles a
JOIN Users u ON a.AuthorId = u.Id
LEFT JOIN Comments c ON a.Id = c.ArticleId
LEFT JOIN Users cu ON c.AuthorId = cu.Id
WHERE a.Id = 1;
```

**SAG:**
> "Tre joins. För EN artikel. Funkar, men komplext."

**PAUS:**
> "Vad händer om vi lägger till taggar? Ny tabell. Ny join."

---

### MongoDB Design (5 min)

**SAG:**
> "MongoDB-tänk: Denormalisera. Embedded documents."

**ÖPPNA COMPASS:**

**SAG:**
> "Skapa collection: blog_posts"

**GÖR:**
Insert document:

```json
{
  "title": "Why NoSQL Is Cool",
  "content": "NoSQL databases offer flexibility...",
  "author": {
    "userId": "user_123",
    "name": "Marcus Medina",
    "email": "marcus@example.com"
  },
  "comments": [
    {
      "id": "comment_1",
      "text": "Great article!",
      "author": {
        "userId": "user_456",
        "name": "Luke Skywalker"
      },
      "createdAt": "2025-01-15T10:00:00Z"
    },
    {
      "id": "comment_2",
      "text": "Very helpful!",
      "author": {
        "userId": "user_789",
        "name": "Leia Organa"
      },
      "createdAt": "2025-01-15T11:30:00Z"
    }
  ],
  "tags": ["nosql", "databases", "mongodb"],
  "createdAt": "2025-01-15T09:00:00Z",
  "views": 1337
}
```

**SAG:**
> "Ett dokument. Allt här. Användare embedded. Kommentarer embedded."

---

**SAG:**
> "Visa samma artikel med alla kommentarer - hur?"

**GÖR I COMPASS:**

```json
{ "_id": ObjectId("...") }
```

**SAG:**
> "En query. Noll joins. Allt data i ett dokument."

---

### Jämförelse: Pros & Cons (4 min)

**RITA TABELL PÅ WHITEBOARD:**

```
┌──────────────────┬─────────────────┬──────────────────┐
│                  │      SQL        │     MongoDB      │
├──────────────────┼─────────────────┼──────────────────┤
│ Queries          │ 3 JOINs         │ 1 query          │
│ Läshastighet     │ Långsammare     │ Snabbare         │
│ Duplicerad data  │ Ingen           │ Massor           │
│ Schema changes   │ Migration       │ Bara lägg till   │
│ Datakonsistens   │ Garanterad (FK) │ Manual handling  │
└──────────────────┴─────────────────┴──────────────────┘
```

**SAG:**
> "SQL: Ingen duplicering, men jobbiga joins."

**SAG:**
> "MongoDB: Snabba reads, men vad händer om Marcus byter namn?"

---

### Problem: Uppdatera Användarnamn (3 min)

**SAG:**
> "Marcus blir 'Marcus Medina PhD'. Hur uppdaterar vi?"

**SQL:**

```sql
UPDATE Users SET Name = 'Marcus Medina PhD' WHERE Id = 'user_123';
```

**SAG:**
> "EN rad. Alla artiklar och kommentarer ser nya namnet automatiskt via JOIN."

---

**MONGODB:**

**SAG:**
> "MongoDB: Hitta ALLA dokument där Marcus nämns."

**GÖR I COMPASS:**

```json
{ "author.userId": "user_123" }
```

**SAG:**
> "Men det täcker bara articles. Vad med kommentarer?"

**SKRIV:**

```javascript
db.blog_posts.updateMany(
  { "author.userId": "user_123" },
  { $set: { "author.name": "Marcus Medina PhD" } }
)

db.blog_posts.updateMany(
  { "comments.author.userId": "user_123" },
  { $set: { "comments.$[elem].author.name": "Marcus Medina PhD" } },
  { arrayFilters: [{ "elem.author.userId": "user_123" }] }
)
```

**SAG:**
> "Komplicerat. Måste uppdatera på flera ställen."

---

### Lösning: Hybrid Approach (3 min)

**SAG:**
> "Bästa lösningen? Använd båda."

**RITA:**

```
MongoDB (blog_posts):
{
  "title": "...",
  "content": "...",
  "authorId": "user_123",  ← Referens, inte embedded
  "comments": [...]
}

SQL (users):
Id | Name              | Email
---+-------------------+------------------
123| Marcus Medina PhD | marcus@...
```

**SAG:**
> "Artiklar i MongoDB. Användare i SQL. Bäst av båda."

**SAG:**
> "När du visar artikel: Hämta user från SQL med authorId."

---

**SKRIV PSEUDOKOD:**

```csharp
// 1. Hämta artikel från MongoDB
var article = await mongoCollection.Find(a => a.Id == articleId).FirstAsync();

// 2. Hämta user från SQL
var author = await dbContext.Users.FindAsync(article.AuthorId);

// 3. Kombinera
var viewModel = new {
    Title = article.Title,
    Content = article.Content,
    AuthorName = author.Name,
    Comments = article.Comments
};
```

**SAG:**
> "Två queries. Men uppdateringar blir enkla."

---

### Avslutning (2 min)

**SAG:**
> "Sammanfattning:"

**SKRIV:**

```
📌 SQL:
✅ Konsistens garanterad
✅ Inga dupliceringar
❌ Komplexa joins
❌ Migrations krångligt

📌 MongoDB:
✅ Snabba reads
✅ Flexibelt schema
❌ Duplicerad data
❌ Manual konsistenshantering

🎯 HYBRID:
✅ Bäst av båda
✅ Rätt verktyg för rätt data
```

**SAG:**
> "Ingen är bäst överallt. Välj baserat på use case."

---

## 📋 Cheat Sheet för Marcus

### Om de frågar:

**"Måste man välja ett?"**
→ "Nej! Spotify använder båda. SQL för användare/payments. MongoDB för spellistor/metadata."

**"Varför inte alltid MongoDB om det är snabbare?"**
→ "Snabbare reads, men uppdateringar blir komplexa. Transaktioner svårare. Data kan bli inkonsistent."

**"Varför inte alltid SQL om det är säkrare?"**
→ "Migrations blir jobbiga. Scheman ändras långsamt. Joins blir dyra vid stora datasets."

**"Kan man göra joins i MongoDB?"**
→ "Ja, $lookup i aggregation pipeline. Men långsamt. Undvik om möjligt."

**"Vad använder ni på Campus Mölndal?"**
→ "Vi kör SQL i produktion. Men många startups kör MongoDB. Lär er båda."

---

## ⏱️ Timing Breakdown

- Scenario: 2 min
- SQL Design: 5 min
- MongoDB Design: 5 min
- Comparison: 4 min
- Update Problem: 3 min
- Hybrid: 3 min
- Wrap-up: 2 min

**Total: 24 minuter** (kan kortas till 18 om ni skippar hybrid-delen)

---

## 🎬 Pro Tips

- **Rita mycket** - visuellt > text
- **Jämför side-by-side** - SQL till vänster, MongoDB till höger
- **Fråga dem:** "Vad skulle ni välja?" innan du visar lösning
- **Använd färger** på whiteboard (blå = SQL, grön = MongoDB)
- **Ha exempel öppet** i Compass redan innan

Lycka till! 🚀

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
