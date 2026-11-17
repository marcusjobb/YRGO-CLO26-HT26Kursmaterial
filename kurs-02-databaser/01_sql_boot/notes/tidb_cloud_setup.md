# TiDB Cloud — gemensam databas i molnet

🟢

Den här guiden visar hur du skapar ett gratis konto på TiDB Cloud och kopplar upp dig från C#. TiDB är MySQL-kompatibelt — all SQL och alla NuGet-paket du redan kan fungerar exakt likadant.

---

## Varför TiDB Cloud?

När hela gruppen ska jobba mot samma databas räcker inte en lokal Docker-container — den syns bara på din dator. TiDB Cloud ger er en gemensam MySQL-databas i molnet, gratis, utan att ni behöver ett kreditkort.

| | **Lokal Docker** | **TiDB Cloud** |
|---|---|---|
| **Tillgänglighet** | Bara din dator | Hela gruppen |
| **Setup** | docker-compose up | Webbformulär, 60 sek |
| **Kostnad** | Gratis | Gratis (Starter-plan) |
| **Kreditkort** | Nej | Nej |
| **SQL-syntax** | MySQL | MySQL (100% kompatibelt) |

---

## Varför vi valde EU (Frankfurt)?

Under skapandet av clustret väljer du **Region: Frankfurt (eu-central-1)**.

Det är inte slumpmässigt — det handlar om **GDPR**.

> GDPR (General Data Protection Regulation) är EU:s dataskyddslag. Den kräver att personuppgifter om EU-medborgare behandlas inom EU, eller under avtal som garanterar likvärdigt skydd.

Om ni lagrar testdata med riktiga namn, e-postadresser eller annan personinformation i databasen gäller GDPR. Då ska datan ligga i EU.

**Frankfurt (eu-central-1)** = AWS datacenter i Tyskland = data stannar i EU.

Tokyo eller Virginia hade fungerat tekniskt — men inte juridiskt för personuppgifter.

> Som molnutvecklare är regionval alltid ett aktivt beslut. Vana att tänka: *Var lagras datan, och vad kräver lagen?*

---

## Hur vi jobbar med TiDB Cloud

Vi kör i två steg:

**Steg 1 — Idag (labb):** Du ansluter mot lärarens cluster. Du får en färdig connection string och fokuserar på att skriva kod — inte på att sätta upp infrastruktur.

**Steg 2 — Grupprojekt:** Din grupp skapar ett eget gratis konto och ett eget cluster. Ni äger er databas och bestämmer strukturen själva.

> Starter-planen tillåter upp till **5 gratis instances per organisation** — mer än nog för ett grupprojekt.

---

## 1. Skapa konto

1. Gå till **tidbcloud.com**
2. Klicka **Sign Up**
3. Välj **Continue with Google** eller **GitHub** — snabbast
4. Inget kreditkort behövs

---

## 2. Skapa ett cluster

1. Klicka **Create Cluster** (eller **Create Resource** om du omdirigeras)
2. Välj plan: **Starter** — *From $0/month* ✅
3. Fyll i:
   - **Instance Name:** något beskrivande, t.ex. `grupp3-databas`
   - **Cloud Provider:** AWS
   - **Region:** **Frankfurt (eu-central-1)** ← viktigt, se ovan
4. **Monthly Spending Limit:** lämna på `$0.00`
5. Klicka **Create**

Clustret är klart på ~60 sekunder. Status blir **Active**.

---

## 3. Hämta connection string

1. Öppna ditt cluster
2. Klicka **Connect** uppe till höger
3. Välj **Connect With: General**
4. Kopiera connection string — den ser ut ungefär så här:

```
Host:     gateway01.eu-central-1.prod.aws.tidbcloud.com
Port:     4000
User:     <får du av läraren>
Password: <får du av läraren>
Database: test
```

> Lösenordet visas bara en gång när clustret skapas — spara det direkt. Behöver du ett nytt: **Settings → Password → Reset**.

> **Spara lösenordet direkt** — det visas bara en gång. Behöver du ett nytt kan du återställa det under Settings.

---

## 4. Ansluta från C#

### NuGet-paket

```bash
dotnet add package MySql.Data
# eller
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### Connection string i C#

```csharp
string connectionString =
    "Server=gateway01.eu-central-1.prod.aws.tidbcloud.com;" +
    "Port=4000;" +
    "Database=test;" +
    "User=<användarnamn från läraren>;" +
    "Password=<lösenord från läraren>;" +
    "SslMode=Required;";
```

> **Port 4000** — inte 3306 som lokal MySQL. TiDB Cloud använder alltid 4000 för publika endpoints.

> `SslMode=Required` är obligatoriskt mot TiDB Cloud — molndatabaser kräver krypterad anslutning. Lokala Docker-databaser behöver det inte.

### ADO.NET

```csharp
using MySql.Data.MySqlClient;

using var connection = new MySqlConnection(connectionString);
connection.Open();
Console.WriteLine("Ansluten!");
```

### EF Core DbContext

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
}
```

Sedan:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 5. Skapa en databas för gruppen

Inne i TiDB Cloud finns en **SQL Editor** där ni kan köra kommandon direkt i webbläsaren — ungefär som phpMyAdmin fast enklare.

Skapa en databas för projektet:

```sql
CREATE DATABASE gruppprojekt;
USE gruppprojekt;
```

Alla i gruppen ansluter med samma connection string — ni delar på samma databas i realtid.

---

## Sammanfattning

```
TiDB Cloud Starter = gratis MySQL i molnet, inget kreditkort
Region Frankfurt   = data i EU = GDPR-kompatibelt
SslMode=Required   = alltid vid molndatabas
Lösenordet         = visas bara en gång, spara det
SQL Editor         = webbläsarbaserat alternativ till phpMyAdmin
```

---

Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
