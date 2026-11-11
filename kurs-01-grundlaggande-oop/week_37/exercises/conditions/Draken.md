# Övning — Draken

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp händelsekedjan på papper — vad leder till vad? Rita klart, lägg ner pennan, öppna sedan VS Code.

🔴

En hjälte hittar ett svärd. Det är inte ett bra tecken för draken — eller byn.

Du ska modellera en orsakskedja med booleska variabler. Varje händelse beror på den föregående. En variabel kan brytas loss från kedjan och sättas separat.

> Sitter du fast i mer än 15 minuter? Be om hjälp — det är inte ett tecken på att du inte kan, det är precis vad 15-minutersregeln är till för.

---

## Uppgiften

### Steg 1 — Deklarera orsakskedjan

```csharp
// Startpunkt — denna kan du ändra
bool hjaltenHittarSvardet = true;

// Konsekvenskedjan — tilldelas från föregående bool
bool drakenVaknar             = hjaltenHittarSvardet;
bool bynBrinner               = drakenVaknar;
bool kungenSanderBrev         = bynBrinner;
bool hjaltenAccepterarUppdrag = kungenSanderBrev;

// Kan sättas oberoende — hjälten kanske förlorar även om allt annat stämmer
bool hjaltenBesegrarDraken = hjaltenAccepterarUppdrag;
```

### Steg 2 — Skriv ut berättelsen med if/else

Skriv en `if`-sats för varje händelse. Om händelsen är `true` — skriv ut vad som hände.

**Den sista händelsen måste ha en `else`:**

```
true  → "Svärdet träffar. Draken faller. Byn jublar."
false → "Draken är för stark. Hjälten reträtt. Sagan fortsätter..."
```

---

## Förväntad output (alla true)

```plaintext
Hjälten hittar ett gammalt svärd i skogen. Det glöder svagt.
Draken vaknar. Marken skakar.
Byn brinner. Skrik hörs på avstånd.
Kungen skickar ett brev: "Vi behöver dig."
Hjälten accepterar uppdraget. Det finns inget val.
Svärdet träffar. Draken faller. Byn jublar.
```

## Förväntad output (hjaltenHittarSvardet = false)

```plaintext
Draken sover. Byn är trygg. Hjälten går hem.
```

## Förväntad output (hjaltenBesegrarDraken = false, resten true)

```plaintext
Hjälten hittar ett gammalt svärd i skogen. Det glöder svagt.
Draken vaknar. Marken skakar.
Byn brinner. Skrik hörs på avstånd.
Kungen skickar ett brev: "Vi behöver dig."
Hjälten accepterar uppdraget. Det finns inget val.
Draken är för stark. Hjälten reträtt. Sagan fortsätter...
```

---

<details><summary>Tips: oberoende bool</summary>

`hjaltenBesegrarDraken` är tilldelad från kedjan som standard — men du kan bryta loss den:

```csharp
bool hjaltenBesegrarDraken = false; // hjälten misslyckas trots att allt annat stämmer
```

Det är ett viktigt mönster: de flesta booleans i ett riktigt program är oberoende. Kedjan här är ett pedagogiskt exempel för att visa hur tilldelning fungerar.

</details>

<details><summary>Tips: else när kedjan är false från start</summary>

Om `hjaltenHittarSvardet` är `false` triggas ingen if-sats — men det ser konstigt ut med helt tom output. Lägg till en `else` på första if:en för att hantera det fallet.

```csharp
if (hjaltenHittarSvardet)
    Console.WriteLine("Hjälten hittar ett gammalt svärd i skogen. Det glöder svagt.");
else
    Console.WriteLine("Draken sover. Byn är trygg. Hjälten går hem.");
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
bool hjaltenHittarSvardet = true;

bool drakenVaknar             = hjaltenHittarSvardet;
bool bynBrinner               = drakenVaknar;
bool kungenSanderBrev         = bynBrinner;
bool hjaltenAccepterarUppdrag = kungenSanderBrev;
bool hjaltenBesegrarDraken    = hjaltenAccepterarUppdrag;

if (hjaltenHittarSvardet)
    Console.WriteLine("Hjälten hittar ett gammalt svärd i skogen. Det glöder svagt.");
else
    Console.WriteLine("Draken sover. Byn är trygg. Hjälten går hem.");

if (drakenVaknar)
    Console.WriteLine("Draken vaknar. Marken skakar.");

if (bynBrinner)
    Console.WriteLine("Byn brinner. Skrik hörs på avstånd.");

if (kungenSanderBrev)
    Console.WriteLine("Kungen skickar ett brev: \"Vi behöver dig.\"");

if (hjaltenAccepterarUppdrag)
    Console.WriteLine("Hjälten accepterar uppdraget. Det finns inget val.");

if (hjaltenBesegrarDraken)
    Console.WriteLine("Svärdet träffar. Draken faller. Byn jublar.");
else if (hjaltenAccepterarUppdrag)
    Console.WriteLine("Draken är för stark. Hjälten reträtt. Sagan fortsätter...");
```

</details>
