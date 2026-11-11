---
marp: true
theme: default
paginate: true
title: Normalisering — från Excelhög till riktig databas
description: 1NF, 3NF och transitiva beroenden förklarade med ett hundägarregister
style: |
  section {
    font-size: 26px;
  }
  section.lead {
    text-align: center;
    justify-content: center;
  }
  section.lead h1 {
    font-size: 56px;
  }
  table {
    font-size: 18px;
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

# Normalisering

Från Excelhög till riktig databas

---

## Varför normaliserar vi?

En databas är inte ett Excel-ark.

Excel: fantastiskt för att räkna på ett dataset **en gång**.
Databas: ska hantera att tusentals rader läggs till, ändras och raderas — varje dag — utan att skapa inkonsekvens.

**Normalisering eliminerar tre klassiska problem:**

- **Uppdateringsanomali** — ändra ett faktum på ett ställe, men glömmer ett annat
- **Insättningsanomali** — kan inte lägga in data utan att lägga in orelaterad data
- **Raderingsanomali** — raderar en rad och tar oavsiktligt med sig annan information

---

## Excelfilen vi ärvde

Du har fått ansvar för att modernisera ett hundägarregister. Föregångaren var noggrann — allt finns sparat:

| PersonId | Förnamn | Efternamn | Postnummer | Ort | Land | HundNamn | HundRas | RasUrsprung | PappaNamn |
|---|---|---|---|---|---|---|---|---|---|
| 1 | Anna | Karlsson | 41117 | Göteborg | Sverige | **Fido** | Labrador | England | — |
| 1 | Anna | Karlsson | 41117 | Göteborg | Sverige | **Bella** | Border Collie | Skottland | Fido |
| 2 | Björn | Svensson | 41104 | Göteborg | Sverige | Rex | Schäfer | Tyskland | — |
| 3 | Cecilia | Lindqvist | 11357 | Stockholm | Sverige | Kula | Golden Retriever | Skottland | — |

Anna finns på rad 1 **och** rad 2. Hon har bara två hundar — men systemet betalar priset.

---

## Problemen syns direkt

**Uppdateringsanomali**
Anna byter adress. Vi måste hitta och uppdatera **alla** rader med Anna.
Missar vi en → Anna har nu två adresser. Ingen vet vilken som stämmer.

**Insättningsanomali**
Vi vill lägga in att Border Collies ursprung är Skottland.
Det går inte utan att det finns en hund att koppla det till.

**Raderingsanomali**
Cecilia slutar vara hundägare — vi tar bort hennes rad.
Nu försvinner all information om att Kula existerade. Och att Golden Retrievers ursprung är Skottland.

**Trasiga referenser**
`PappaNamn = "Fido"` är bara text. Molly nämns som mamma men finns inte ens i tabellen.
Systemet kan inte verifiera något. En stavning och länken är bruten.

---

## 1NF — separera entiteterna

> **Regel:** Varje kolumn ska innehålla ett enda, odelbart värde. Inga upprepande grupper.

Anna upprepas för att hon har flera hundar. Lösningen: **en tabell per entitet.**

**Person** — varje person en gång

| PersonId | Förnamn | Efternamn | Postnummer | Ort | Land |
|---|---|---|---|---|---|
| 1 | Anna | Karlsson | 41117 | Göteborg | Sverige |
| 2 | Björn | Svensson | 41104 | Göteborg | Sverige |
| 3 | Cecilia | Lindqvist | 11357 | Stockholm | Sverige |

Anna finns nu på exakt en rad. Adressändring = en uppdatering. Klart.

---

## 1NF — Hund och kopplingstabell

**Hund** — varje hund en gång, med riktiga referenser istället för text

| HundId | Namn | Ras | RasUrsprung | PappaId | MammaId |
|---|---|---|---|---|---|
| 1 | Fido | Labrador | England | NULL | NULL |
| 2 | Bella | Border Collie | Skottland | 1 | 5 |
| 5 | Molly | Border Collie | Skottland | NULL | NULL |

Molly existerar nu som en riktig rad. `PappaId = 1` pekar på Fido — inte en textsträng som kan vara felstavad.

**HundPerson** — kopplingen (en person kan ha många hundar)

| PersonId | HundId |
|---|---|
| 1 | 1 |
| 1 | 2 |

Anna äger Fido och Bella. En ny hund = en ny rad här. Person-tabellen rörs inte.

---

## Vi är i 1NF — men titta noga

Person-tabellen ser bättre ut, men det finns fortfarande ett problem gömt i den:

| PersonId | Förnamn | Postnummer | **Ort** | **Land** |
|---|---|---|---|---|
| 1 | Anna | 41117 | **Göteborg** | **Sverige** |
| 2 | Björn | 41104 | **Göteborg** | **Sverige** |
| 3 | Cecilia | 11357 | **Stockholm** | **Sverige** |

Vem bestämmer att Ort är "Göteborg"? Inte Anna — **postnumret** gör det.
Vem bestämmer att Land är "Sverige"? Inte personen — **Västra Götaland** gör det.

Det kallas **transitivt beroende**: A → B → C, men vi lagrar A, B, C i samma tabell som om C tillhör A direkt.

---

## 3NF — bryt kedjan

> **Regel:** Alla kolumner ska bero direkt på primärnyckeln — inte på andra kolumner.

`Postnummer → Ort` är ett faktum om postnumret, inte om personen.
`Ort → Land` är ett faktum om orten, inte om postnumret.

Vi bryter ut steg för steg:

**Land**

| LandId | Namn |
|---|---|
| 1 | Sverige |

**Ort**

| OrtId | Namn | LandId |
|---|---|---|
| 1 | Göteborg | 1 |
| 2 | Stockholm | 1 |

---

## 3NF — slutresultat Person

**Postnummer**

| Postnummer | OrtId |
|---|---|
| 41117 | 1 |
| 41104 | 1 |
| 11357 | 2 |

**Person** — nu utan transitiva beroenden

| PersonId | Förnamn | Efternamn | Gata | Husnr | Postnummer |
|---|---|---|---|---|---|
| 1 | Anna | Karlsson | Kungsgatan | 5 | 41117 |
| 2 | Björn | Svensson | Drottninggatan | 12 | 41104 |
| 3 | Cecilia | Lindqvist | Storgatan | 3 | 11357 |

`Person` vet sitt postnummer. Postnumret vet sin ort. Orten vet sitt land.
Varje faktum lagras **en gång**, på **rätt ställe**.

---

## Varför det är värt besväret

**Utan normalisering:**
- Anna byter adress → uppdatera N rader
- Göteborg byter postnummersystem → uppdatera alla personrader i hela databasen
- Radera Cecilia → Kula försvinner

**Med normalisering:**
- Anna byter adress → uppdatera 1 rad i Person
- Postnummer byter ort → uppdatera 1 rad i Postnummer
- Radera Cecilia → Kula finns kvar i Hund-tabellen

> Data som ändras uppdateras på ett ställe.
> Data som raderas tar inte med sig annan data.
> Data som läggs till behöver inte bära orelaterad information.

---

## Tumregeln

**1NF:** En cell = ett värde. Inga upprepande grupper. Separera entiteter i egna tabeller.

**3NF:** Alla kolumner beror direkt på primärnyckeln — inte på varandra.

Fråga dig för varje kolumn:

> *"Beror det här på raden — eller på något annat i raden?"*

Om det beror på något annat → bryt ut det till en egen tabell.

---

<!-- _class: lead -->
<!-- _paginate: false -->

# Nu kör vi

Ni har ett rörigt schema att normalisera.
Rita tabellerna på papper **innan** ni skriver SQL.

Penna och whiteboard slår IDE:t varje gång. 🐕
