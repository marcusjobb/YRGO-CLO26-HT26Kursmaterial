---

title: Alternativ 3: Lagerhanteringssystem
author: Marcus Ackre Medina
type: assignment
topic: projekt
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/assignment/storage.md"
description: "Ett program som simulerar ett lagersystem genom att läsa produktdata och kundordrar från CSV-filer, bearbeta dem och spara det uppdaterade lagersaldot."
tags: ["alternativ", "csharp", "lagerhanteringssystem", "projekt", "storage", "visual-studio"]
week_fit: []
---

# Alternativ 3: Lagerhanteringssystem

🔴


## Koncept

Ett program som simulerar ett lagersystem genom att läsa produktdata och kundordrar från CSV-filer, bearbeta dem och spara det uppdaterade lagersaldot.

_Detta är en mer affärsorienterad uppgift som tränar filhantering, databearbetning och logisk problemlösning._

## Systemets funktion

### 1. Läsa lagerdata

- Programmet läser in produkter från `lager.csv`
- Varje produkt har namn, kategori, pris och antal i lager

### 2. Bearbeta kundordrar

- Programmet läser ordrar från `ordrar.csv`
- För varje order kontrolleras om varan finns i tillräcklig mängd
- Lagersaldot uppdateras vid lyckade ordrar

### 3. Spara uppdaterat lager

- Det nya lagersaldot sparas till `lager_uppdaterat.csv`
- En sammanfattning av bearbetningen visas

## Förslag på klassstruktur

```csharp
// Huvudklassen som hanterar all logik
public class InventoryManager
{
    private List<Product> products;
    private List<Order> orders;

    // Metoder: LoadProductsFromCsv(), LoadOrdersFromCsv(),
    //          ProcessOrders(), SaveUpdatedProductsToCsv()
}

// Representerar en vara i lagret
public class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }    // decimal för pengar
    public int Quantity { get; set; }

    // Metoder: CanFulfillOrder(int requestedQuantity), ReduceQuantity(int amount)
}

// Representerar en enskild kundorder
public class Order
{
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string ProductName { get; set; }
    public int QuantityOrdered { get; set; }
}
```

## Tekniska ledtrådar

### Filhantering

```csharp
// Tips för att läsa CSV-filer:
// File.ReadAllLines(filePath) ger dig en array med alla rader
// Första raden är ofta rubriker - hoppa över den
// Använd line.Split(',') för att dela upp varje rad i delar
```

### CSV-parsing

```csharp
// Exempel på hur en CSV-rad kan se ut:
// "Laptop Dell XPS,Electronics,15999.00,12"
// Efter Split(',') får du: ["Laptop Dell XPS", "Electronics", "15999.00", "12"]
//
// Kom ihåg att konvertera strängarna:
// int.Parse() för heltal
// decimal.Parse() för decimaltal
```

### Hitta produkter

```csharp
// Tips: Använd en foreach-loop för att hitta rätt produkt
// Jämför produktnamnet från ordern med namnen i din produktlista
// Alternativt: Lär dig om LINQ senare (products.FirstOrDefault(...))
```

## Exempel på CSV-filer

### lager.csv (input)

```
Name,Category,Price,Quantity
Laptop Dell XPS,Electronics,15999.00,12
Trådlös mus,Electronics,299.00,45
iPhone 15,Electronics,12999.00,8
Office-stol,Furniture,2499.00,6
Kaffemugg,Kitchen,79.00,50
```

### ordrar.csv (input)

```
CustomerId,CustomerName,ProductName,QuantityOrdered
CUST001,Anna Andersson,Laptop Dell XPS,2
CUST002,Björn Larsson,Trådlös mus,3
CUST003,Cecilia Nilsson,iPhone 15,1
CUST004,David Eriksson,Office-stol,15
CUST005,Emma Johansson,Kaffemugg,5
```

## Grundkrav för Godkänt

### Obligatoriska funktioner

1. **Läsa produkter:** Programmet läser `lager.csv` och skapar Product-objekt
2. **Läsa ordrar:** Programmet läser `ordrar.csv` och skapar Order-objekt
3. **Orderbearbetning:**
   - Kontrollera om produkten finns
   - Kontrollera om det finns tillräckligt i lager
   - Minska lagersaldot vid lyckad order
   - Skriv bekräftelse eller felmeddelande
4. **Spara resultat:** Det uppdaterade lagret sparas till `lager_uppdaterat.csv`
5. **Sammanfattning:** Visa statistik (antal lyckade/misslyckade ordrar)

### Exempel på programkörning

```
=== LAGERHANTERINGSSYSTEM ===
Läser produkter från lager.csv...
5 produkter inlästa.

Bearbetar ordrar från ordrar.csv...

✓ Order från Anna Andersson skickad: 2x Laptop Dell XPS
✓ Order från Björn Larsson skickad: 3x Trådlös mus
✓ Order från Cecilia Nilsson skickad: 1x iPhone 15
✗ Order från David Eriksson kunde inte skickas: Otillräckligt lager för Office-stol (begärt: 15, finns: 6)
✓ Order från Emma Johansson skickad: 5x Kaffemugg

Orderbearbetning slutförd!
- 4 ordrar skickade
- 1 order kunde inte skickas

Sparar uppdaterat lager till lager_uppdaterat.csv...
Klart!
```

### Felhantering som krävs

- Hantera om CSV-filer saknas
- Hantera produkter som inte finns i lagret
- Hantera ogiltiga datatyper (t.ex. om någon skriver text istället för siffror)
- Kontrollera att alla nödvändiga kolumner finns

## Förslag på programflöde

```csharp
static void Main(string[] args)
{
    InventoryManager manager = new InventoryManager();

    try
    {
        // 1. Läs in data
        manager.LoadProductsFromCsv("lager.csv");
        manager.LoadOrdersFromCsv("ordrar.csv");

        // 2. Bearbeta ordrar
        manager.ProcessOrders();

        // 3. Spara resultat
        manager.SaveUpdatedProductsToCsv("lager_uppdaterat.csv");

        Console.WriteLine("Lagerhantering slutförd!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Fel uppstod: {ex.Message}");
    }
}
```

## Krav för Väl Godkänt

Välj **minst en** av följande utbyggnader:

### 1. Textbaserat menysystem

Skapa en interaktiv meny där användaren kan:

- Visa alla produkter
- Lägga till ny produkt
- Fylla på lager för befintlig produkt
- Söka produkter
- Visa lagerstatistik

```
=== LAGERMENY ===
1. Visa alla produkter
2. Lägg till produkt
3. Fyll på lager
4. Bearbeta ordrar
5. Avsluta
Välj alternativ:
```

### 2. Restordersystem

När en order inte kan uppfyllas:

- Spara ordern i `restordrar.csv`
- När lager fylls på, kontrollera automatiskt om restordrar kan uppfyllas
- Prioritera restordrar framför nya ordrar

### 3. Avancerad rapportering

- Skapa `orderrapport.csv` med detaljerad information
- Visa totalt ordervärde per kund
- Lista mest populära produkter
- Beräkna total intäkt från lyckade ordrar

## Bedömningsfokus

### Teknisk implementation (40%)

- Fungerar CSV-läsning och skrivning korrekt?
- Korrekt hantering av datatyper (decimal för pris, int för antal)
- Lämplig användning av klasser och listor

### Affärslogik (30%)

- Korrekt lagerhantering (rätt mängder dras av)
- Hantering av edge cases (produkt finns ej, för lite i lager)
- Tydlig feedback till användaren

### Kodkvalitet (30%)

- Välstrukturerad kod med tydligt ansvar per klass
- Bra felhantering med try-catch
- Kommentarer och läsbar kod

## Tips för framgång

1. **Testa med små filer först:** Börja med bara några produkter och ordrar
2. **Kontrollera dina CSV-filer:** Öppna dem i Excel/LibreOffice för att se strukturen
3. **Debugga steg för steg:** Skriv ut vad som händer i varje steg
4. **Hantera fel tidigt:** Lägg till try-catch runt fil-operationer
5. **Validera data:** Kontrollera att siffror är siffror och inte text

## Vanliga fallgropar att undvika

- Glöm inte hoppa över första raden (rubrikraden) när du läser CSV
- Kom ihåg att `decimal.Parse()` kan krascha om strängen inte är ett tal
- Se till att produktnamn stämmer exakt överens mellan lager och ordrar
- Spara den uppdaterade CSV:n med samma format som originalet

---

_Lycka till med ditt lagerhanteringssystem! Detta projekt ger dig värdefull erfarenhet av filhantering och affärslogik som används i verkliga system._