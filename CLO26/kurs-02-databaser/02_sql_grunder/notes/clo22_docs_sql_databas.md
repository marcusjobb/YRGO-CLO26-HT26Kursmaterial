---

title: Databas
author: Marcus Ackre Medina
type: article
topic: sql
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/CLO22/Docs/sql/Databas.md"
description: "<details open markdown='block'>"
tags: ["csharp", "databas", "git", "sql"]
week_fit: []
---

Databaser

<details open markdown="block">
 <summary>
 Innehållsförteckning
 </summary>
 {: .text-delta }

1. TOC
{:toc}

</details>

### Skapa en databas

För att skapa en ny databas kan du använda följande SQL-kod:

```sql
CREATE DATABASE databasnamn;
```

### Skapa en databas om den inte finns

Om du vill skapa en databas bara om den inte redan finns kan du använda följande SQL-kod:

```sql
CREATE DATABASE IF NOT EXISTS databasnamn;
```

### Ta bort en databas

Det är viktigt att komma ihåg att ta säkerhetskopior innan du tar bort en databas, eftersom all data kommer att gå förlorad. Här är kodexemplet för att ta bort en databas:

```sql
DROP DATABASE databasnamn;
```

### Ändra namn på databasen

Att ändra namnet på en databas kan vara riskabelt eftersom det kan skapa problem om du har kopplat dig till databasen med ett program. Om du ändå vill ändra namnet på databasen kan du använda följande SQL-kod:

```sql
ALTER DATABASE databasnamn RENAME TO nytt_databasnamn;
```

Det är viktigt att använda försiktighet när du arbetar med databaser för att undvika oavsiktlig dataförlust eller andra problem. Kom ihåg att alltid göra säkerhetskopior och vara medveten om konsekvenserna av dina handlingar.

### Slutsats

Databaser är en viktig del av moderna applikationer och tillåter oss att lagra och hantera data på ett strukturerat sätt. Genom att använda SQL, det vanligaste databasspråket, kan vi skapa och hantera databaser med hjälp av enkla kommandon. Genom att förstå hur man skapar, ändrar och tar bort databaser kan vi bygga kraftfulla och stabila applikationer.

### Termer

- **Databas**: En strukturerad samling av data som kan hanteras och manipuleras med hjälp av datorbaserade system.
- **SQL**: Structured Query Language, ett programmeringsspråk som används för att kommunicera med och hantera relationella databaser.

### TL;DR-sammanfattning

Databaser är en viktig del av moderna applikationer och tillåter oss att lagra och hantera data på ett strukturerat sätt. Genom att använda SQL kan vi skapa och hantera databaser med hjälp av enkla kommandon. Det är viktigt att vara försiktig när man arbetar med databaser för att undvika oavsiktlig dataförlust eller andra problem.

### Obligatorisk Dad-joke

Varför var databasen så arg?
För att den hade alltid mycket att *tabellera* över!

### Källor

- [Länk till källa 1](https://www.example.com)
- [Länk till källa 2](https://www.example.com)

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
