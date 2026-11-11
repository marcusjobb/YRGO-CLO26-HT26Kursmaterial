---
marp: true
theme: nion-dark
paginate: true
---

# Måndag 9 november

**Vecka 2 av 5** · Fördjupad OOP i C#

Mira flyttar in i en databas.

---

## Agenda för dagen

**Förmiddag**
1. Repetition av förra veckan (15 min)
2. EF Core: spara objekt i en databas
3. EF Core: relationer, och hur databasen stoppar en dubbelbokning

**Eftermiddag**
4. MongoDB: samma hjältar som dokument (spara, hitta, ändra, ta bort)
5. Inlämning 1: Hotel Grandioso, vi går igenom den tillsammans
6. Backspegel: vad gjorde vi i dag? (15 min)

Läxa C (tisdag): Tildas bibliotek.

---

## Repetition: förra veckan på 15 minuter

Fem frågor. Svara för dig själv först, i huvudet eller på papper.

Sedan visar jag svaret.

Det är inget prov. Att plocka fram det du redan kan är det som får det att fastna.

---

## Fråga 1: klass och objekt

```csharp
Hero mira = new Hero("Mira");
Hero bruno = new Hero("Bruno");
```

Hur många **klasser** och hur många **objekt** ser du?

---

## Svar 1

**En klass:** `Hero`. Ritningen.

**Två objekt:** `mira` och `bruno`. Två hus byggda från samma ritning.

`new` är byggarbetaren som bygger huset.

---

## Fråga 2: arv

```csharp
class Character { public virtual string Attack() => "Slår"; }
class Wizard : Character { public override string Attack() => "Kastar eld"; }

Character c = new Wizard();
Console.WriteLine(c.Attack());
```

Vad skrivs ut?

---

## Svar 2

`Kastar eld`

Variabeln är en `Character`, men objektet är en `Wizard`.

`virtual` säger "får bytas ut". `override` byter ut. Objektet bestämmer.

---

## Fråga 3: abstract eller interface?

Du vill säga: *"Alla som kan läka ska ha metoden `Heal()`."*

Det kan vara en trollkarl, en dryck eller en sjuksköterska. De har inget annat gemensamt.

`abstract class` eller `interface`?

---

## Svar 3

**Interface:** `IHealer`.

| `abstract class` | `interface` |
|---|---|
| "Är en": Wizard **är en** Character | "Kan": Wizard **kan** läka |
| Delar kod med sina barn | Ett löfte om vad man kan |
| Bara en förälder | Hur många som helst |

---

## Fråga 4: JSON

```csharp
class Hero { public string Name; public int Health; }

string json = JsonSerializer.Serialize(new Hero { Name = "Mira", Health = 100 });
Console.WriteLine(json);
```

Vad skrivs ut, och varför?

---

## Svar 4

```
{}
```

Tomt. `Name` och `Health` är **fält**, och `JsonSerializer` sparar bara **properties**.

```csharp
class Hero { public string Name { get; set; } public int Health { get; set; } }
```

Nu blir det `{"Name":"Mira","Health":100}`.

---

## Fråga 5: katterna

I onsdags sparade vi katter i en lista och skrev listan till en JSON-fil.

Vad betyder **CRUD**? Säg de fyra orden.

---

## Svar 5

| Bokstav | Betyder | Med listan |
|---|---|---|
| **C** | Create, skapa | `cats.Add(...)` |
| **R** | Read, läsa | `foreach` eller `Find` |
| **U** | Update, ändra | ändra en property |
| **D** | Delete, ta bort | `cats.Remove(...)` |

I dag gör vi exakt samma fyra saker, fast i en riktig databas.

---

<!-- Här tar dagens föreläsningar vid: ef_core_hello, ef_core_relationer, mongodb_light, inlamning_1_genomgang. Backspegeln nedan visas sist på dagen. -->

# Backspegel

Vad gjorde vi i dag?

---

## Din tur först (3 min)

Skriv ner, för dig själv:

1. **En sak** du kan nu som du inte kunde i morse.
2. **En sak** som fortfarande känns oklar.

Den oklara saken tar du med till fredagens handledning.

---

## Snabba frågor

1. Vad gör `SaveChanges()`?
2. Vad gör `Include(...)`?
3. Var tittar du för att se att Mira verkligen hamnade i databasen?
4. Vad är skillnaden mellan en rad i SQLite och ett dokument i MongoDB?

---

## Det här gjorde vi i dag

- ✅ Repetition: klass, arv, abstract och interface, JSON och CRUD
- ✅ EF Core: `DbContext`, migration och `SaveChanges()`
- ✅ Relationer: en hjälte, många uppdrag, och `Include`
- ✅ DB Browser: vi tittade in i databasen
- ✅ MongoDB: samma CRUD, men som dokument
- ✅ Inlämning 1: Hotel Grandioso

---

## Härnäst

- **Tisdag:** Läxa C, Tildas bibliotek. Samma steg som i dag, i din egen takt.
- **Onsdag:** UML och designmönster, snälla verktyg.
- **Fredag:** handledning. Ta med din oklara sak.
- **Inlämning 1:** deadline söndag 22 november.

Bra jobbat i dag. 💙
