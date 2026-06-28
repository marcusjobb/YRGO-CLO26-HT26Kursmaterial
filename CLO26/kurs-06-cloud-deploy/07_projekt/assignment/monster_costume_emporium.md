---

title: Monster Costume Emporium – Från ADO.NET till Entity Framework 🎃
author: Marcus Ackre Medina
type: assignment
topic: projekt
difficulty: 1
language: bash
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/assignment/part_2/monster_costume_emporium.md"
description: "Systrarna Hexwell i Shadow Hollow behöver din hjälp igen! Den här gången får du ta över en färdig konsolapplikation byggd med **ADO.NET** och **SQLite**. Din uppgift är att förstå hur allt hänger ihop"
tags: ["ado.net", "bash", "costume", "emporium", "entity", "framework", "från", "git", "monster", "projekt"]
week_fit: []
---

# Monster Costume Emporium – Från ADO.NET till Entity Framework 🎃

🟢


Systrarna Hexwell i Shadow Hollow behöver din hjälp igen! Den här gången får du ta över en färdig konsolapplikation byggd med **ADO.NET** och **SQLite**. Din uppgift är att förstå hur allt hänger ihop, för att sedan modernisera lösningen till **Entity Framework Core** och bygga vidare med lite smart funktionalitet.

---

## 🧵 Bakgrund

Butiken *Monster Costume Emporium* säljer handsydda monsterkostymer till Grimvilles mest illustra kunder. För att hålla koll på lager och försäljningar byggdes en snabb ADO.NET-lösning. Den fungerar – men är svår att underhålla och saknar vettig statistik. Ditt mål är att lyfta koden till en renare EF Core-arkitektur och ge systrarna bättre beslutsunderlag.

Skapa en ny git repo på klassens github organisation med namnet **[assignment-ef-{ditt gafenamn}](https://github.com/organizations/Campus-Molndal-CLO25/repositories/new)** och lägg in koden där. Dela länken med din lärare och ladda upp din zip med kod till google classroom.


---

## 📦 Startmaterial (delas ut till studenterna)

I mappen `MonsterCostumeAdo/` ligger en enkel .NET 9-konsolapp:

- **Databas:** SQLite-fil `monster_costume.db`.
- **Tabeller:** `Costumes` och `CostumeSales` (se struktur nedan).
- **Seeding:** Körs automatiskt vid start om databasen är tom.
- **Meny:**
  1. Visa kostymer i lager
  2. Visa gjorda försäljningar
  3. Registrera ny försäljning (kontrollerar lagersaldo och minskar stock)
  0. Avsluta
- **Teknik:** Direkt SQL via `Microsoft.Data.Sqlite` (ADO.NET) i `Program.cs`.

> Koden är avsiktligt kompakt och ganska monolitisk för att studenterna tydligt ska se skillnaden när de bygger om den.

---

## 🗄️ Databasstruktur

### `Costumes`

| Kolumn | Typ   | Beskrivning                                   |
| ------ | ----- | --------------------------------------------- |
| Id     | int   | Primärnyckel (auto-inkrement)                 |
| Name   | text  | Kostymens namn (t.ex. "Night Stalker Cloak") |
| Monster| text  | Vilket monster den passar                      |
| Stock  | int   | Antal i lager                                 |
| Price  | real  | Pris i guldmynt                               |

### `CostumeSales`

| Kolumn       | Typ  | Beskrivning                                                    |
| ------------ | ---- | -------------------------------------------------------------- |
| Id           | int  | Primärnyckel (auto-inkrement)                                  |
| CostumeId    | int  | Främmande nyckel → `Costumes.Id`                               |
| CustomerName | text | Kundens namn                                                   |
| Quantity     | int  | Antal köpta kostymer                                          |
| SaleDate     | text | Datum/tid ISO 8601                                            |
| Notes        | text | Valfri kommentar                                              |

Relationen är **en kostym → många försäljningar**.

---

## ✅ Grundkrav (G) – Gör om till Entity Framework Core

För att få G ska studenterna ta den befintliga appen och stegvis konvertera den till EF Core utan att tappa funktionalitet.

1. **Skapa EF Core-modeller**
   - `Costume` och `CostumeSale` med samma fält som tabellerna.
   - Navigeringsproperty `Costume.CostumeSales`.

2. **DbContext**
   - Skapa `MonsterCostumeContext : DbContext` med `DbSet<Costume>` och `DbSet<CostumeSale>`.
   - Konfigurera SQLite-anslutning mot samma fil (`monster_costume.db`).
   - Flytta `EnsureCreated()`-logik från ADO-versionen till `Main` (eller en `Bootstrapper`).

3. **Seeding med EF Core**
   - Ersätt ADO-seedingen med EF-kod som lägger in samma tre kostymer och tre försäljningar **endast om tabellerna är tomma**.

4. **Dataåtkomst**
   - Byt ut alla ADO.NET-anrop mot EF Core (LINQ + `SaveChanges`).
   - Behåll menystrukturen och programmets utskrifter.
   - Säkerställ att försäljningsregistreringen fortfarande minskar lagersaldo och sparar posten.

5. **Kodstruktur**
   - Dela gärna upp programmet i klasser/metoder (t.ex. `CostumeService`, `SaleService`).
   - Rensa bort oanvänd ADO.NET-kod och paket.

> När allt fungerar som tidigare, fast med EF Core under huven, är G-kraven uppfyllda.

---

## 🌟 Utmaning (VG) – Statistik och filtrering

Bygg vidare på EF-versionen och lägg till extra värde i butiken.

1. **Intäktsöversikt**
   - Ny menyoption: visa totala intäkter per kostym (`Quantity * Price`).
   - Sortera fallande på intäkt.

2. **Filtrera försäljningar**
   - Ny menyoption: filtrera försäljningar på datumintervall (från/till).
   - Använd `DateTime.Parse` och LINQ-filter.

3. **Lagerlarm**
   - Vid start eller via meny: lista kostymer där lagret är under ett valt gränsvärde (t.ex. < 5).
   - Låt användaren mata in gränsen.

4. **Presentation**
   - Förbättra utskrifterna (t.ex. tabellform, färger, summeringar i slutet).

> VG-nivån handlar om att utnyttja EF Core mer avancerat och ge en liten menynavigerad "rapportdel" till Hexwell-systrarna.

---

## 🧪 Förslag på testflöde

1. Kör originalappen (ADO.NET) → se att database seedas och menyvalen fungerar.
2. Konvertera stegvis till EF Core och verifiera efter varje ändring.
3. Säkerställ att seedingen endast sker första gången databasen är tom.
4. (VG) Testa statistikmenyerna med olika datumintervall och lagernivåer.
5. Kontrollera databasen i valfri SQLite-klient om du vill granska resultatet.

```bash
cd MonsterCostumeAdo
# Originalversion (ADO.NET)
dotnet run

# Efter omvandling till EF Core
dotnet run
```

---

## 💡 Tips

- Börja med att kapsla in ADO.NET-koden i separata metoder → blir lättare att ersätta med EF.
- `Include` hjälper dig att plocka med kostymen när du hämtar försäljningar (`context.CostumeSales.Include(x => x.Costume)`).
- För datumfiltrering kan du använda `DateTime.TryParse` för att undvika krascher.
- Glöm inte `Console.ResetColor()` efter färgsatt text.
- Länka gärna ut modellklasser och EF-kod i separata filer för tydlighet.

---

_Lycka till med moderniseringen! När du är klar har du både bemästrat rå SQL och visat att du kan bygga en robust EF Core-lösning. Systrarna Hexwell kommer bli överlyckliga._ 🦇
