# Property pattern på en record

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Givet `record Order(string Status, int ItemCount)`, skriv `string Message(Order order)` med ett switch-uttryck:
- Status "Klar" och minst en vara: "Skickas"
- Status "Klar" och inga varor: "Tom order"
- Allt annat: "Väntar"

Fyll i det som står `/* din kod */`.

```csharp
string Message(Order order) => order switch
{
    /* din kod:
       status "Klar" och fler än 0 varor ger "Skickas"
       status "Klar" i övrigt ger "Tom order"
       allt annat ger "Väntar" */
};

Console.WriteLine(Message(new Order("Klar", 3)));
Console.WriteLine(Message(new Order("Klar", 0)));
Console.WriteLine(Message(new Order("Ny", 2)));

record Order(string Status, int ItemCount);
```

Exempeloutput:

```text
Skickas
Tom order
Väntar
```

<details markdown="1">
<summary>Tips 1: titta på egenskaperna</summary>

Ett property pattern kollar värdet på egenskaper inuti objektet. Du skriver det med klammerparenteser, till exempel `{ Status: "Klar" }`.

</details>

<details markdown="1">
<summary>Tips 2: flera egenskaper samtidigt</summary>

Du kan kolla flera egenskaper i samma mönster. Separera dem med komma. För antalet varor kan du använda ett relationsmönster som `> 0`.

</details>

<details markdown="1">
<summary>Tips 3: ordningen</summary>

Den mest specifika raden ska stå först. Börja med:

```csharp
{ Status: "Klar", ItemCount: > 0 } => "Skickas",
```

Sedan en rad som bara kollar `Status`, och sist `_`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
string Message(Order order) => order switch
{
    { Status: "Klar", ItemCount: > 0 } => "Skickas",
    { Status: "Klar" } => "Tom order",
    _ => "Väntar"
};

Console.WriteLine(Message(new Order("Klar", 3)));
Console.WriteLine(Message(new Order("Klar", 0)));
Console.WriteLine(Message(new Order("Ny", 2)));

record Order(string Status, int ItemCount);
```

`record Order` står sist. I ett program med top-level statements måste typer deklareras efter koden som körs.

</details>
