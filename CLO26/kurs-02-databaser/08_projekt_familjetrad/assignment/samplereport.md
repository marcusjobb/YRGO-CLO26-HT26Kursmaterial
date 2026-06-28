---

title: {Ditt namn} MongoDB inlämning
author: Marcus Ackre Medina
type: assignment
topic: databaser
difficulty: 1
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/2_db/Assignments/Assignment_mongodb/samplereport.md"
description: "Steg för steg genomgång av varje del av uppgiften. Skriv det här inne och exekvera på MongoDBShell."
tags: ["databaser", "inlämning", "javascript", "mongodb", "namn}", "samplereport", "{ditt"]
week_fit: []
---

# {Ditt namn} MongoDB inlämning

🟢


Steg för steg genomgång av varje del av uppgiften. Skriv det här inne och exekvera på MongoDBShell.

## Steg 1: Skapa en databas

```javascript

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
use CatFoodRecipes
db.createCollection("recipes")
```

## Steg 2: Lägg till dokument

```javascript
db.recipes.insertOne({
    "name": "Tuna and Shrimp Cat Food",
})
```

Flera dokument

```javascript
db.recipes.insertMany([
    {
        "name": "Tuna and Shrimp Cat Food",
    },
    {
        "name": "Chicken and Rice Cat Food",
    }
])

osv
