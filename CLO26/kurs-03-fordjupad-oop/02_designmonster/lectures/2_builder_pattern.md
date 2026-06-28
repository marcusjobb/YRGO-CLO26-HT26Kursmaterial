---

title: 2. Builder-mönstret
author: Marcus Ackre Medina
type: lecture
topic: oop
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/1_oop/lectures/08_design_patterns/2_builder_pattern.md"
description: "- **Introduktion:** Builder-mönstret är ett skapandemönster som används för att konstruera komplexa objekt steg för"
tags: ["builder", "builder-mönstret", "csharp", "oop", "pattern", "visual-studio"]
week_fit: []
---

# 2. Builder-mönstret

🟢


## Föreläsningsmaterial

### Förstå Builder-mönstret

#### 1. Vad är Builder-mönstret?

- **Introduktion:** Builder-mönstret är ett skapandemönster som används för att konstruera komplexa objekt steg för
  steg. Det separerar konstruktionen av ett objekt från dess representation, vilket gör det möjligt att skapa olika
  representationer av ett objekt med samma byggprocess.
- **Varför använda Builder-mönstret?:**
    - **Hantera komplexa objekt:** När ett objekt har många obligatoriska och valfria attribut blir konstruktorn svår
      att använda. Builder-mönstret gör det möjligt att bygga objektet på ett mer läsbart och strukturerat sätt.
    - **Flexibilitet:** Det ger flexibilitet att skapa olika variationer av ett objekt utan att skapa flera
      överbelastade konstruktörer.
    - **Förbättrad läsbarhet:** Genom att använda metoder som tydligt beskriver varje steg i objektets konstruktion
      förbättras kodens läsbarhet.

#### 2. Struktur och användning

- **Struktur av Builder-mönstret:**
    - **Builder-klass:** Innehåller metoder för att sätta värden för olika attribut och en `build()`-metod för att skapa
      objektet.
    - **Produktklass (Produkt):** Klassen som vi bygger med hjälp av Builder-mönstret.

- **Exempel:** Skapa en `Car`-klass med Builder-mönstret för att hantera olika attribut som modell, färg, motor, etc.

**Kodexempel i C#:**

```csharp
public class Program()
{
    public static void Main()
    {
        Car car = new Carbuilder("Sittoträng").SetColor("Spygrön").Build();
    }    
} 
public class Car
{
    public string Model { get; }
    public string Color { get; set; }
    public string Engine { get; set; }
    public int Seats { get; set; }

    private Car(CarBuilder builder)
    {
        this.Model = builder.Model;
        this.Color = builder.Color;
        this.Engine = builder.Engine;
        this.Seats = builder.Seats;
    }

    public class CarBuilder
    {
        private string model="Rishög";
        private string color="Vit med rostfläckar";
        private string engine="Gammal moppemotor"; // 80tals Trabant
        private int seats=4;

        public CarBuilder(string model)
        {
            this.model = model;
        }

        public CarBuilder SetColor(string color)
        {
            this.color = color;
            return this;
        }

        public CarBuilder SetEngine(string engine)
        {
            this.engine = engine;
            return this;
        }

        public CarBuilder SetSeats(int seats)
        {
            this.seats = seats;
            return this;
        }

        public Car Build()
        {
            return new Car(){Model=model, Engine=engine, Color=color, Seats=seats};
        }
    }

    public override string ToString()
    {
        return $"Car [model={model}, color={color}, engine={engine}, seats={seats}]";
    }
}
```

#### 3. Användning av Builder-mönstret

- **Byggprocessen:** Skapa en ny bil genom att använda `CarBuilder` och anropa metoder för att sätta olika attribut.
- **Flexibilitet:** Observera hur metoderna kan kedjas för att skapa objektet steg för steg, vilket ger en mycket läsbar
  kod.

**Kodexempel i C#:**

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        Car car = new Car.CarBuilder("Sedan")
                .SetColor("Red")
                .SetEngine("V6")
                .SetSeats(4)
                .Build();

        Console.WriteLine(car);
    }
}
```

---

## Övningsuppgifter

### Uppgift 1: Implementera ett Builder-mönster

1. **Uppgift:** Implementera ett Builder-mönster för en `Computer`-klass. Klassen ska ha attribut som `CPU`, `RAM`,
   `Storage`, och `GraphicsCard`.
2. **Mål:** Förstå hur man implementerar Builder-mönstret för att hantera objekt med många attribut.

<details>
  <summary>Lösningsförslag</summary>

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
public class Computer
{
    private string CPU;
    private string RAM;
    private string Storage;
    private string GraphicsCard;

    private Computer(ComputerBuilder builder)
    {
        this.CPU = builder.CPU;
        this.RAM = builder.RAM;
        this.Storage = builder.Storage;
        this.GraphicsCard = builder.GraphicsCard;
    }

    public class ComputerBuilder
    {
        public string CPU { get; }
        public string RAM { get; }
        public string Storage { get; private set; }
        public string GraphicsCard { get; private set; }

        public ComputerBuilder(string CPU, string RAM)
        {
            this.CPU = CPU;
            this.RAM = RAM;
        }

        public ComputerBuilder SetStorage(string storage)
        {
            this.Storage = storage;
            return this;
        }

        public ComputerBuilder SetGraphicsCard(string graphicsCard)
        {
            this.GraphicsCard = graphicsCard;
            return this;
        }

        public Computer Build()
        {
            return new Computer(this);
        }
    }

    public override string ToString()
    {
        return $"Computer [CPU={CPU}, RAM={RAM}, Storage={Storage}, GraphicsCard={GraphicsCard}]";
    }
}
```

</details>

### Uppgift 2: Använd Builder-mönstret

1. **Uppgift:** Använd `ComputerBuilder` för att skapa olika typer av datorer (t.ex. en spel-dator, en arbetsstation).
2. **Mål:** Förstå hur Builder-mönstret ger flexibilitet och enkelhet vid skapandet av objekt med olika konfigurationer.

<details>
  <summary>Lösningsförslag</summary>

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        Computer gamingPC = new Computer.ComputerBuilder("Intel i9", "32GB")
                .SetStorage("1TB SSD")
                .SetGraphicsCard("NVIDIA RTX 3080")
                .Build();

        Computer officePC = new Computer.ComputerBuilder("Intel i5", "16GB")
                .SetStorage("512GB SSD")
                .Build();

        Console.WriteLine(gamingPC);
        Console.WriteLine(officePC);
    }
}
```

</details>

### Uppgift 3: Utvidga Builder-mönstret

1. **Uppgift:** Utvidga Builder-mönstret för att hantera fler attribut och valmöjligheter i `Computer`-klassen, såsom
   `OperatingSystem`, `PowerSupply`, och `CoolingSystem`.
2. **Mål:** Förstå hur man kan utöka Builder-mönstret för att skapa ännu mer komplexa objekt.

<details>
  <summary>Lösningsförslag</summary>

```csharp
public class Computer
{
    private string CPU;
    private string RAM;
    private string Storage;
    private string GraphicsCard;
    private string OperatingSystem;
    private string PowerSupply;
    private string CoolingSystem;

    private Computer(ComputerBuilder builder)
    {
        this.CPU = builder.CPU;
        this.RAM = builder.RAM;
        this.Storage = builder.Storage;
        this.GraphicsCard = builder.GraphicsCard;
        this.OperatingSystem = builder.OperatingSystem;
        this.PowerSupply = builder.PowerSupply;
        this.CoolingSystem = builder.CoolingSystem;
    }

    public class ComputerBuilder
    {
        public string CPU { get; }
        public string RAM { get; }
        public string Storage { get; private set; }
        public string GraphicsCard { get; private set; }
        public string OperatingSystem { get; private set; }
        public string PowerSupply { get; private set; }
        public string CoolingSystem { get; private set; }

        public ComputerBuilder(string CPU, string RAM)
        {
            this.CPU = CPU;
            this.RAM = RAM;
        }

        public ComputerBuilder SetStorage(string storage)
        {
            this.Storage = storage;
            return this;
        }

        public ComputerBuilder SetGraphicsCard(string graphicsCard)
        {
            this.GraphicsCard = graphicsCard;
            return this;
        }

        public ComputerBuilder SetOperatingSystem(string operatingSystem)
        {
            this.OperatingSystem = operatingSystem;
            return this;
        }

        public ComputerBuilder SetPowerSupply(string powerSupply)
        {
            this.PowerSupply = powerSupply;
            return this;
        }

        public ComputerBuilder SetCoolingSystem(string coolingSystem)
        {
            this.CoolingSystem = coolingSystem;
            return this;
        }

        public Computer Build()
        {
            return new Computer(this);
        }
    }

    public override string ToString()
    {
        return $"Computer [CPU={CPU}, RAM={RAM}, Storage={Storage}, GraphicsCard={GraphicsCard}, " +
                $"OperatingSystem={OperatingSystem}, PowerSupply={PowerSupply}, CoolingSystem={CoolingSystem}]";
    }
}
```

</details>

---

Dessa övningsuppgifter är utformade för att täcka mer än 30-40 minuter.

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
