---
title: Git Flow + C#-klasser – bygg tillsammans
author: Marcus Ackre Medina
type: exercise
topic: git
difficulty: 2
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/Assignments/OOP/excersises/gitflow/gitflow4.md"
description: "Kombinera Git Flow med C# — varje gruppmedlem skapar en klass via feature branch, code review och merge."
tags: ["bash", "git", "gitflow", "csharp", "oop", "grupparbete"]
week_fit: []
---

# Git Flow + C#-klasser – bygg tillsammans

🟡

**Scenario:** Ni bygger ett klassbibliotek tillsammans. Varje person skapar en klass via Git Flow. Ingen får röra någon annans klass förrän den är mergad till develop. Era brancher ska ha namn som matchar det ni ska göra, alltså inte **kalle** och **pelles branch** utan **class Cat** eller **new class Dog**. På samma sätt ska era commits ha texter som **Created class ....** eller **added method to Cat**. 

## Steg för steg

### 1. Skapa projektet

En person skapar ett tomt konsolprojekt, pushar till GitHub:

```bash
dotnet new console -n TeamProject
cd TeamProject
git init
git add .
git commit -m "Initial commit: tomt konsolprojekt"
# Skapa repo på GitHub (utan README), kör:
git remote add origin <repo-url>
git push -u origin main
git checkout -b develop
git push origin develop
```

Skydda `main` och `develop` med branch-regler (kräv PR).

### 2. Alla klonar + skapar feature branch

```bash
git clone <repo-url>
cd TeamProject
git checkout -b feature/djur-klass
```

### 3. Skapa en klass

Skapa `Models/Djur.cs`:

```csharp
namespace TeamProject.Models;

public class Djur
{
    public string Namn { get; set; } = "";
    public int Alder { get; set; }

    public Djur(string namn, int alder)
    {
        Namn = namn;
        Alder = alder;
    }

    public void Presentera()
    {
        Console.WriteLine($"{Namn} är {Alder} år gammal.");
    }
}
```

### 4. Commit + push + PR

```bash
git add .
git commit -m "Lägg till Djur-klass"
git push origin feature/djur-klass
# → PR feature/djur-klass → develop, be om review
```

### 5. Review + merge

Gruppkamrat granskar PR:n och kollar:
- Namngivning (PascalCase?)
- Är properties rimliga?
- Saknas något?

Godkänn → merge till develop.

### 6. Uppdatera + ny feature

```bash
git checkout develop
git pull origin develop
git checkout -b feature/byggnad-klass
# skapa Models/Byggnad.cs, repeat...
```

---

<details>
<summary>💡 Tips 1 – Models-mappen</summary>

Skapa en `Models/`-mapp i projektet innan ni börjar:

```bash
mkdir Models
git add .
git commit -m "Skapa Models-mapp"
git push origin develop
```

Då slipper alla skapa den själva och får merge-konflikter i onödan.

</details>

<details>
<summary>💡 Tips 2 – Olika klasser, samma mapp</summary>

Alla skapar olika filer i `Models/`. Då blir det inga merge-konflikter — ni jobbar på olika ställen i koden. Först när någon vill ändra i en annans klass behöver ni samordna er.

</details>

---

<details>
<summary>✅ Förslagslösning – exempel-klasser</summary>

```csharp
// Models/Djur.cs
namespace TeamProject.Models;

public class Djur
{
    public string Namn { get; set; }
    public int Alder { get; set; }

    public Djur(string namn, int alder)
    {
        Namn = namn;
        Alder = alder;
    }

    public void Presentera()
    {
        Console.WriteLine($"{Namn} är {Alder} år.");
    }
}
```

```csharp
// Models/Byggnad.cs
namespace TeamProject.Models;

public class Byggnad
{
    public string Adress { get; set; }
    public int Vaningar { get; set; }

    public Byggnad(string adress, int vaningar)
    {
        Adress = adress;
        Vaningar = vaningar;
    }

    public void Info()
    {
        Console.WriteLine($"{Adress}, {Vaningar} våningar.");
    }
}
```

```csharp
// Program.cs
using TeamProject.Models;

var djur = new Djur("Katt", 3);
var hus = new Byggnad("Storgatan 1", 2);

djur.Presentera();
hus.Info();
```

</details>

---

## Reflektion

- Varför fungerar Git Flow bra när alla skapar olika klasser?
- Vad händer när ni måste ändra i samma klass?
- Hur skulle ni hantera en situation där någon vill ändra Djur-klassen efter att den mergats?

---

_Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig._
