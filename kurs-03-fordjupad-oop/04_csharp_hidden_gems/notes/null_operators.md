# Null-operatorerna

## Anders kiosk: klockan 07:58

Anders har byggt en app för sina stamkunder. Klockan 07:58 kraschar den. Orsaken: en kund har aldrig lämnat sitt telefonnummer.

Hur ska appen hantera en kund utan telefonnummer? Fundera lite på det innan du läser vidare.

## Ett tomt skåp

`null` betyder att det inte finns något i skåpet. Frågar du efter innehållet kraschar det (`NullReferenceException`). Frågar du försiktigt med `?.` får du `null` tillbaka i stället.

## Tre operatorer

```csharp
string? name = null;

int length = name?.Length ?? 0;   // 0: inget objekt, då 0
name ??= "Gäst";                  // sätter "Gäst" om name är null
```

- `?.` går bara vidare om objektet inte är `null`.
- `??` ger ett reservvärde om värdet är `null`.
- `??=` sätter ett värde om variabeln är `null`.

## Samma sak, längre väg

```csharp
int length = 0;
if (name != null)
    length = name.Length;
```

Samma beteende, med fler rader och fler chanser att glömma något.

## Tips: låt var ta frågetecknet

```csharp
string? name = GetName();   // du skriver ? själv
int? count = GetCount();

var name = GetName();       // var tar typen från högersidan, inklusive ?
var count = GetCount();
```

Med `var` slipper du skriva `string?` och `int?` själv.

Det går bara när det finns något att utgå från. `var x = null;` ger fel (`CS0815`), för då vet kompilatorn inte vilken typ det är.

## Fem null-kontroller

Så här kan en metod se ut när Anders skriver den med vanliga `if`-satser:

```csharp
string ContactLine(Customer? c)
{
    string name = "Okänd", city = "okänd stad", phone = "saknas";
    if (c != null)
    {
        if (c.Name != null) name = c.Name;
        if (c.Phone != null) phone = c.Phone;
        if (c.Address != null)
            if (c.Address.City != null) city = c.Address.City;
    }
    return $"{name}, {city}, {phone}";
}
```

Fem gånger `!= null`. Det här kan alla skriva. Men tänk om det går kortare?

## Samma metod, tre rader

```csharp
string ContactLine(Customer? c)
{
    string name = c?.Name ?? "Okänd";
    string city = c?.Address?.City ?? "okänd stad";
    string phone = c?.Phone ?? "saknas";
    return $"{name}, {city}, {phone}";
}
```

Exakt samma utskrift i alla fall: ingen kund, tom kund, bara namn, adress utan stad och allt ifyllt. Tre rader i stället för en trappa av `if`.

Lägg märke till kedjan `c?.Address?.City`. Om någon länk i kedjan är `null` får du reservvärdet. Det är särskilt användbart när data kommer från en databas eller ett API, där delar kan saknas.

## Summan av kardemumman

- `?.` hoppar över `null` i stället för att krascha.
- `??` ger ett reservvärde.
- `??=` fyller i om det är tomt.

## Proffsens tips: kolla ingredienserna först

Minns ni kladdkakan från metodlektionerna i kurs 1? Den är tillbaka.

Ska du baka en kladdkaka tittar du i skafferiet **innan** du sätter på ugnen och smälter chokladen. Finns smöret? Finns äggen? Saknas något avbryter du direkt, inte när degen redan är halvvägs.

Samma sak med data som kommer in i en metod:

**Kolla inputen först. Använd den sen.**

### Så ser det ut i kod

```csharp
decimal CalculateTotal(Order? order)
{
    // 1. Kolla ingredienserna
    ArgumentNullException.ThrowIfNull(order);
    if (order.Items.Count == 0)
        return 0;

    // 2. Nu kan vi baka
    return order.Items.Sum(i => i.Price);
}
```

Felet kommer direkt och tydligt, i stället för långt därifrån och svårt att spåra.

## Prova själv

Gör `null_operators_ovning.md`, `null_operators_ovning_2.md` och `null_operators_ovning_3.md`.

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
