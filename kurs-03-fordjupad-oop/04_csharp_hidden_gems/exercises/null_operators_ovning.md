# Null-operatorerna: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

Skriv om den här metoden med `?.` och `??`:

```csharp
string GetCity(Customer? customer)
{
    if (customer != null && customer.Address != null)
        return customer.Address.City;
    return "okänd";
}
```

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
string GetCity(Customer? customer) => customer?.Address?.City ?? "okänd";
```

</details>
