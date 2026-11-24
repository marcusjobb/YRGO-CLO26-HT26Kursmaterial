# Null-operatorerna: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Nivå:** 🟢

**Tid:** ca 20 minuter.

## Uppgifter

Anders kund har en adress med en stad. Båda får saknas:

```csharp
class Address
{
    public string? City { get; set; }
}

class Customer
{
    public string? Name { get; set; }
    public Address? Address { get; set; }
}
```

Skriv om den här metoden med `?.` och `??`:

```csharp
string GetCity(Customer? customer)
{
    if (customer != null && customer.Address != null && customer.Address.City != null)
        return customer.Address.City;
    return "okänd";
}
```

Fyll i det som står `/* din kod */`.

```csharp
string GetCity(Customer? customer) =>
    /* din kod:
       gå från customer till Address till City
       stanna utan fel om något på vägen saknas
       saknas staden, använd "okänd" */;

Console.WriteLine(GetCity(new Customer(new Address("Göteborg"))));
Console.WriteLine(GetCity(new Customer(null)));
Console.WriteLine(GetCity(null));

record Address(string City);
record Customer(Address? Address);
```

Exempeloutput:

```text
Göteborg
okänd
okänd
```

<details markdown="1">
<summary>Tips 1: vad gör ?.</summary>

`a?.B` betyder: om `a` är `null`, bli `null`. Annars, hämta `B`. Du slipper alltså en `if` för varje steg.

</details>

<details markdown="1">
<summary>Tips 2: en kedja</summary>

Du kan sätta ihop flera `?.` efter varandra. Om något steg är `null` blir hela kedjan `null`, och resten hoppas över.

</details>

<details markdown="1">
<summary>Tips 3: reservvärdet</summary>

`??` ger värdet till höger om det till vänster är `null`. Kedjan börjar så här:

```csharp
customer?.Address?. ...
```

Avsluta med `??` och reservtexten.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
string GetCity(Customer? customer) => customer?.Address?.City ?? "okänd";

Console.WriteLine(GetCity(new Customer(new Address("Göteborg"))));
Console.WriteLine(GetCity(new Customer(null)));
Console.WriteLine(GetCity(null));

record Address(string City);
record Customer(Address? Address);
```

</details>
