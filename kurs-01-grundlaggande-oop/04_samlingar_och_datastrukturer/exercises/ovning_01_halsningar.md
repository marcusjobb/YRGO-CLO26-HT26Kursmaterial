# Övning — Hälsningar

🟢

Du ska skriva en metod som tar emot ett namn och skriver ut en hälsning. Sedan anropar du den flera gånger med olika namn.

---

## Steg 1: Skriv metoden

Skapa en metod som heter `PrintGreeting` och tar emot ett namn som `string`. Metoden ska skriva ut:

```
Hej, [namn]!
```

Anropa metoden tre gånger med namnen `"Anna"`, `"Björn"` och `"Camilla"`.

### Förväntad output
```plaintext
Hej, Anna!
Hej, Björn!
Hej, Camilla!
```

<details><summary>Tips: hur ser en void-metod med string-parameter ut?</summary>

```csharp
static void PrintGreeting(string name)
{
    Console.WriteLine("Hej, " + name + "!");
}
```

Anropa den med:

```csharp
PrintGreeting("Anna");
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
PrintGreeting("Anna");
PrintGreeting("Björn");
PrintGreeting("Camilla");

static void PrintGreeting(string name)
{
    Console.WriteLine("Hej, " + name + "!");
}
```

</details>

---

## Steg 2: Lägg till en avdelare

Uppdatera `PrintGreeting` så att den också skriver ut en rad med streck efter hälsningen:

```
--------------------------------
```

### Förväntad output
```plaintext
Hej, Anna!
--------------------------------
Hej, Björn!
--------------------------------
Hej, Camilla!
--------------------------------
```

<details><summary>Tips</summary>

Du ändrar bara inuti metoden — anropen i toppen behöver inte röras. Det är det fina med metoder: ett ställe att ändra, alla anrop uppdateras automatiskt.

</details>

<details><summary>Lösningsförslag</summary>

```csharp
PrintGreeting("Anna");
PrintGreeting("Björn");
PrintGreeting("Camilla");

static void PrintGreeting(string name)
{
    Console.WriteLine("Hej, " + name + "!");
    Console.WriteLine("--------------------------------");
}
```

</details>

---

## Steg 3: Lägg till en hälsningsfras

Uppdatera metoden igen — lägg till en rad till som skriver `Välkommen!` efter namnet men innan avdelaren.

### Förväntad output
```plaintext
Hej, Anna!
Välkommen!
--------------------------------
Hej, Björn!
Välkommen!
--------------------------------
Hej, Camilla!
Välkommen!
--------------------------------
```

<details><summary>Lösningsförslag</summary>

```csharp
PrintGreeting("Anna");
PrintGreeting("Björn");
PrintGreeting("Camilla");

static void PrintGreeting(string name)
{
    Console.WriteLine("Hej, " + name + "!");
    Console.WriteLine("Välkommen!");
    Console.WriteLine("--------------------------------");
}
```

</details>
