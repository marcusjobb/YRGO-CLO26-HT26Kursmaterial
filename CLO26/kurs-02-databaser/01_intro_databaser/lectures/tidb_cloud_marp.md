---
marp: true
theme: default
paginate: true
title: TiDB Cloud — gemensam databas i molnet
description: Vad TiDB Cloud är, varför vi kör det och connection string ni ska använda
style: |
  section {
    font-size: 26px;
  }
  section.lead {
    text-align: center;
    justify-content: center;
  }
  section.lead h1 {
    font-size: 64px;
  }
  table {
    font-size: 22px;
  }
  th {
    background: #2d3e50;
    color: #fff;
  }
  td strong {
    color: #d35400;
  }
  td:has(strong) {
    background: #fdebd0;
  }
  code {
    font-size: 0.9em;
  }
  blockquote {
    font-size: 22px;
    border-left: 6px solid #e67e22;
    color: #555;
  }
---

<!-- _class: lead -->
<!-- _paginate: false -->

# TiDB Cloud

En MySQL-databas i molnet — gratis, ingen Docker, hela gruppen ser samma data

---

## Problemet med lokal databas

Din Docker-container syns bara på **din dator**.

| | Lokal Docker | TiDB Cloud |
|---|---|---|
| Tillgänglighet | Bara din dator | **Hela gruppen** |
| Setup | docker-compose up | Webbformulär, 60 sek |
| Kostnad | Gratis | **Gratis** (Starter-plan) |
| Kreditkort | Nej | **Nej** |
| SQL-syntax | MySQL | **MySQL** (100% kompatibelt) |

> Allt SQL ni kan — fungerar exakt likadant mot TiDB Cloud.

---

## Varför Frankfurt?

Vi valde **EU (Frankfurt / eu-central-1)** som region — det är inte slumpmässigt.

**GDPR** kräver att personuppgifter om EU-medborgare behandlas inom EU.

- Frankfurt = AWS datacenter i Tyskland
- Data stannar i EU
- Tokyo eller Virginia hade fungerat tekniskt — **inte juridiskt**

> Som molnutvecklare är regionval alltid ett aktivt beslut.
> Fråga dig: *Var lagras datan, och vad kräver lagen?*

---

## Connection string — kopiera den här

```csharp
string connectionString =
    "Server=gateway01.eu-central-1.prod.aws.tidbcloud.com;" +
    "Port=4000;" +
    "Database=test;" +
    "User=<användarnamn från läraren>;" +
    "Password=<lösenord från läraren>;" +
    "SslMode=Required;";
```

Tre saker att lägga märke till:

- **Port 4000** — inte 3306 som lokal MySQL
- **SslMode=Required** — molndatabaser kräver krypterad anslutning
- **User/Password** — ni får dem av mig, skriv dem inte i koden

---

## Anslut och testa

```csharp
using MySql.Data.MySqlClient;

using var connection = new MySqlConnection(connectionString);
connection.Open();
Console.WriteLine("Ansluten!");
```

NuGet-paket:

```bash
dotnet add package MySql.Data
```

> Ser ni "Ansluten!" är ni klara. Resten är vanlig SQL.

---

## Lösenordet — viktigt

> **Lösenordet visas bara en gång** när clustret skapas.

Spara det direkt — annars måste ni återställa det under **Settings → Password → Reset**.

Det gäller er egna cluster när ni skapar ett för grupprojektet.

**Idag kör ni mot mitt cluster** — ni får connection string av mig.

---

<!-- _class: lead -->
<!-- _paginate: false -->

# Kör igång

Klistra in connection string, lägg till MySql.Data, tryck F5.

Om det står **Ansluten!** — ni är med i molnet. 🌩️
