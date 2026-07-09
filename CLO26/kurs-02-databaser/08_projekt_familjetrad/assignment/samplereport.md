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
