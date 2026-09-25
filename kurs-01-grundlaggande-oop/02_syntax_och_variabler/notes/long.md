Vad är en Long?

En long är en datatyp som används för att lagra heltal. En long kan lagra större heltal än en int. En long kan lagra heltal mellan -9 223 372 036 854 775 808 och 9 223 372 036 854 775 807. En long är 8 byte stor.

Så här deklarerar du en long:

```csharp
long myLong = 1234567890123456789;
```

Du kan också använda long.TryParse() för att försöka konvertera en sträng till en long. Om konverteringen lyckas så returnerar metoden true, annars returnerar den false.

```csharp
long myLong = long.tryParse("1234567890123456789");
``` 

Du kan använda long.MaxValue för att få det största värdet som en long kan lagra. Du kan använda long.MinValue för att få det minsta värdet som en long kan lagra.

```csharp
long myLong = long.MaxValue; // myLong är nu 9 223 372 036 854 775 807
long myLong = long.MinValue; // myLong är nu -9 223 372 036 854 775 808
```

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
