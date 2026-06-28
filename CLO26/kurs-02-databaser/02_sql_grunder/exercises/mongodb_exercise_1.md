---

title: Övning 1: MongoDB CRUD - Star Wars Karaktärer
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 3
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/exercises/mongodb/mongodb_exercise_1.md"
description: "Efter den här övningen kommer du att kunna:"
tags: ["crud", "databaser", "exercise", "git", "installation", "json", "karaktärer", "mongodb", "star", "visual-studio"]
week_fit: []
---

# Övning 1: MongoDB CRUD - Star Wars Karaktärer

🔴


## 🎯 Mål med övningen

Efter den här övningen kommer du att kunna:

- Ansluta till MongoDB via Compass eller Atlas
- Skapa collections och dokument
- Utföra CRUD-operationer (Create, Read, Update, Delete)
- Använda MongoDB query syntax
- Förstå skillnaden mellan embedded och referenced documents

## 🧩 Uppgiften

Ni ska bygga en **Star Wars karaktärsdatabas** i MongoDB.

Databasen ska innehålla karaktärer med olika egenskaper, vapen, och hemplaneter.

## 🚀 Kom igång: Setup

### Steg 1: Anslut till MongoDB

**Alternativ A: MongoDB Atlas (cloud)**

1. Öppna MongoDB Compass
2. Anslut med er Atlas connection string
3. Skapa en ny databas: `starwars_db`
4. Skapa en collection: `characters`

**Alternativ B: Docker (lokalt)**

```bash
docker run -d --name mongodb-local -p 27017:27017 mongo:latest
```

Anslut i Compass med: `mongodb://localhost:27017`

### Steg 2: Skapa Första Dokumentet

Klicka på `characters` collection → **Insert Document**.

Lägg till:

```json
{
  "name": "Luke Skywalker",
  "species": "Human",
  "homeworld": "Tatooine",
  "affiliation": "Jedi Order",
  "lightsaber": {
    "color": "green",
    "style": "single-blade"
  },
  "skills": ["Force push", "Lightsaber combat", "Piloting"],
  "age": 23
}
```

Klicka **Insert**.

Grattis! Första dokumentet sparat! 🎉

## ✅ Del 1: CREATE - Lägg Till Fler Karaktärer

Lägg till följande karaktärer (en i taget eller alla samtidigt med **Insert Many**):

**Darth Vader:**

```json
{
  "name": "Darth Vader",
  "species": "Human (Cyborg)",
  "homeworld": "Tatooine",
  "affiliation": "Sith Order",
  "lightsaber": {
    "color": "red",
    "style": "single-blade"
  },
  "skills": ["Force choke", "Lightsaber mastery", "Dark side powers"],
  "age": 45
}
```

**Yoda:**

```json
{
  "name": "Yoda",
  "species": "Unknown",
  "homeworld": "Unknown",
  "affiliation": "Jedi Order",
  "lightsaber": {
    "color": "green",
    "style": "shoto"
  },
  "skills": ["Force mastery", "Wisdom", "Acrobatic combat"],
  "age": 900
}
```

**Han Solo:**

```json
{
  "name": "Han Solo",
  "species": "Human",
  "homeworld": "Corellia",
  "affiliation": "Rebel Alliance",
  "weapon": {
    "type": "blaster",
    "model": "DL-44"
  },
  "skills": ["Piloting", "Marksmanship", "Smuggling"],
  "age": 32
}
```

**Notera:** Han Solo har **ingen lightsaber**, utan en `weapon` istället.

Detta är okej i MongoDB! Dokument kan ha olika struktur.

## 📖 Del 2: READ - Queries

Nu testar ni att **läsa** data med olika filter.

Använd **Filter**-rutan i Compass.

### Query 1: Hitta alla Jedi

```json
{ "affiliation": "Jedi Order" }
```

**Förväntat resultat:** Luke och Yoda

### Query 2: Hitta alla från Tatooine

```json
{ "homeworld": "Tatooine" }
```

**Förväntat resultat:** Luke och Vader

### Query 3: Hitta alla med röd lightsaber

```json
{ "lightsaber.color": "red" }
```

**Förväntat resultat:** Darth Vader

Notera `.` notation för att komma åt nested fields!

### Query 4: Hitta alla med "Force push" som skill

```json
{ "skills": "Force push" }
```

**Förväntat resultat:** Luke

MongoDB söker automatiskt i arrays!

### Query 5: Hitta alla äldre än 100 år

```json
{ "age": { "$gt": 100 } }
```

**Förväntat resultat:** Yoda

`$gt` = **greater than**. MongoDB operators börjar med `$`.

## 🔄 Del 3: UPDATE - Uppdatera Dokument

### Update 1: Luke får en ny lightsaber

Luke bytte från grön till blå lightsaber i vissa filmer.

I Compass:
1. Hitta Luke
2. Klicka på dokumentet
3. Ändra `lightsaber.color` från `"green"` till `"blue"`
4. Klicka **Update**

Eller via query (om ni kör i Mongo Shell):

```javascript
db.characters.updateOne(
  { "name": "Luke Skywalker" },
  { "$set": { "lightsaber.color": "blue" } }
)
```

### Update 2: Lägg till en ny skill till Yoda

Yoda lär sig "Teaching" som skill.

```javascript
db.characters.updateOne(
  { "name": "Yoda" },
  { "$push": { "skills": "Teaching" } }
)
```

`$push` lägger till ett element i en array.

### Update 3: Han Solo blir äldre

Hans ålder ökar med 5 år:

```javascript
db.characters.updateOne(
  { "name": "Han Solo" },
  { "$inc": { "age": 5 } }
)
```

`$inc` = **increment** (öka värdet).

## 🗑️ Del 4: DELETE - Ta Bort Dokument

### Delete 1: Ta bort en karaktär

Lägg till Jar Jar Binks (om ni vågar):

```json
{
  "name": "Jar Jar Binks",
  "species": "Gungan",
  "homeworld": "Naboo",
  "affiliation": "Gungan Grand Army",
  "skills": ["Clumsiness", "Luck"]
}
```

Nu ta bort honom:

```javascript
db.characters.deleteOne({ "name": "Jar Jar Binks" })
```

Alla är glada nu. 😌

### Delete 2: Ta bort alla Sith

```javascript
db.characters.deleteMany({ "affiliation": "Sith Order" })
```

**OBS:** Detta tar bort ALLA Sith!

Använd `deleteMany` med försiktighet.

## 🕵️‍♂️ Hur testar ni att det funkar?

- **CREATE**: Kolla att nya dokument dyker upp i Compass
- **READ**: Verifiera att rätt dokument matchar era queries
- **UPDATE**: Kolla att fält faktiskt ändrats
- **DELETE**: Bekräfta att dokument försvunnit

## 🤔 Diskussion i paret

Snacka ihop er!

1. **Vad hände med Han Solo?**
   - Han har `weapon` istället för `lightsaber`. Fungerade det ändå? Varför?

2. **Vad händer om ni kör denna query?**

   ```json
   { "lightsaber": { "$exists": true } }
   ```

   Vilka karaktärer matchar?

3. **Embedded vs Referenced:**
   - Just nu är `lightsaber` **embedded** (del av dokumentet).
   - Vad hade hänt om ni hade en separat `lightsabers`-collection istället?
   - Vilka för- och nackdelar finns?

## 🔥 BONUS: Advanced Queries

### Bonus 1: Hitta alla Jedi ELLER Sith

```json
{ "affiliation": { "$in": ["Jedi Order", "Sith Order"] } }
```

### Bonus 2: Hitta alla med minst 3 skills

```json
{ "skills": { "$exists": true, "$not": { "$size": 0 } } }
```

(Tyvärr stöder MongoDB inte direkt `$size: {$gte: 3}`, men ni kan använda aggregation pipeline för det.)

### Bonus 3: Text Search

Först, skapa ett text index:

```javascript
db.characters.createIndex({ "name": "text", "skills": "text" })
```

Nu kan ni söka:

```javascript
db.characters.find({ "$text": { "$search": "Force" } })
```

Detta hittar alla dokument där "Force" nämns i name eller skills.

## 💭 Reflektionsfrågor

1. **Hur kändes MongoDB query syntax jämfört med SQL?**

2. **Vad är enklare?**
   - Läsa data i MongoDB eller SQL?
   - Uppdatera nested data (som lightsaber.color)?

3. **Vad saknar ni från SQL?**
   - Joins?
   - Schema-validering?

<details>
<summary>💡 Klicka här för facit och extra tips</summary>

## Fullständiga Queries (Mongo Shell Syntax)

### CREATE

```javascript
db.characters.insertOne({
  "name": "Leia Organa",
  "species": "Human",
  "homeworld": "Alderaan",
  "affiliation": "Rebel Alliance",
  "skills": ["Leadership", "Diplomacy", "Blaster combat"],
  "age": 23
})
```

### READ

**Hitta alla människor:**

```javascript
db.characters.find({ "species": "Human" })
```

**Hitta den äldsta karaktären:**

```javascript
db.characters.find().sort({ "age": -1 }).limit(1)
```

**Hitta alla med lightsaber OCH från Tatooine:**

```javascript
db.characters.find({
  "lightsaber": { "$exists": true },
  "homeworld": "Tatooine"
})
```

### UPDATE

**Byt affiliation för alla från Tatooine till "Tatooine Natives":**

```javascript
db.characters.updateMany(
  { "homeworld": "Tatooine" },
  { "$set": { "affiliation": "Tatooine Natives" } }
)
```

**Lägg till ett helt nytt fält (t.ex. "rank"):**

```javascript
db.characters.updateOne(
  { "name": "Yoda" },
  { "$set": { "rank": "Grand Master" } }
)
```

Inga migrations behövs! Bara gamla dokument som inte har `rank`.

### DELETE

**Ta bort alla utan lightsaber:**

```javascript
db.characters.deleteMany({ "lightsaber": { "$exists": false } })
```

## Embedded vs Referenced

**Embedded (nuvarande approach):**

```json
{
  "name": "Luke",
  "lightsaber": { "color": "blue" }
}
```

**Fördelar:**
- En query hämtar allt
- Snabbt

**Nackdelar:**
- Om samma lightsaber används av flera (t.ex. Anakin → Luke), dupliceras data

**Referenced approach:**

```json
{
  "name": "Luke",
  "lightsaberId": "saber_123"
}
```

Separat collection:

```json
{
  "_id": "saber_123",
  "color": "blue",
  "previousOwners": ["Anakin", "Luke"]
}
```

**Fördelar:**
- Ingen duplicering

**Nackdelar:**
- Kräver två queries (eller `$lookup`)

**Regel:**
- **Embed** om data är 1-to-1 eller 1-to-few
- **Reference** om data är 1-to-many eller many-to-many

## Indexering

Skapa index för snabbare queries:

```javascript
db.characters.createIndex({ "name": 1 })
db.characters.createIndex({ "affiliation": 1 })
```

Nu blir queries på `name` och `affiliation` mycket snabbare!

## Aggregation Pipeline

Om ni vill räkna antal karaktärer per affiliation:

```javascript
db.characters.aggregate([
  { "$group": { "_id": "$affiliation", "count": { "$sum": 1 } } }
])
```

Output:

```json
{ "_id": "Jedi Order", "count": 2 }
{ "_id": "Sith Order", "count": 1 }
{ "_id": "Rebel Alliance", "count": 1 }
```

</details>

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
