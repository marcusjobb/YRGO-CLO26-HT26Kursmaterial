---

title: Hur man skapar Snippets
author: Marcus Ackre Medina
type: lecture
topic: databaser
difficulty: 2
language: csharp
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/Hur man skapar Snippets.md"
description: "I det här experimentet ska vi skapa ett par snippets"
tags: ["csharp", "databaser", "entity-framework", "productivity", "snippets", "visual-studio"]
week_fit: []
---

# Snippets

🟡


I det här experimentet ska vi skapa ett par snippets

En snippet är en shortcut i Visual Studio som generar kod till oss.

För detta öppnar vi kör funktionen i Windows (Windows knapp + R) eller den vanliga filutforskaren vi väljer att öppna följande mapp

%USERPROFILE%\Documents\Visual Studio 2019\Code Snippets\Visual C#\My Code Snippets


# EfPoco

Skapa en textfil och bry dig inte om namnet, öppna den och skriv in följande

```


<?xml version="1.0" encoding="utf-8"?>
<CodeSnippets xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
<CodeSnippet Format="1.0.0">
<Header>
<Title>Entity Framework Model</Title>
<Author>Ditt namn</Author>
<Description>Inserts properties for an Entity Framework Model.</Description>
<Shortcut>EfPoco</Shortcut>
</Header>
<Snippet>
<Code Language="CSharp">
<![CDATA[        [Key]
public int Id { get; set; }
public string Name { get; set; }]]>
</Code>
<Imports>
<Import>
<Namespace>System.ComponentModel.DataAnnotations</Namespace>
</Import>
</Imports>
</Snippet>
</CodeSnippet>
</CodeSnippets>
```


Det enda du behöver ändra är dessa rader

```


<Title>Entity Framework Model</Title>
<Author>Ditt namn</Author>
<Description>Inserts properties for an Entity Framework Model.</Description>
<Shortcut>EfPoco</Shortcut>
```


Titel : Vad den ska heta, den visas ibland
Author : Det är du
Description: En snyggare beskrivning på dess funktion

Och slutligen
<![CDATA[        [Key]

```

public int Id { get; set; }
public string Name { get; set; }]]>
```


Innanför <![CDATA[ ]]> kan du skriva vilken kod du vill, dock är det vissa tecken som är specialhanterade och måste skrivas på ett speciellt sätt. $ är något som får CDATA att krascha så skriv alltid $$ för att den inte ska flippa ur.

Nu kan du spara och stänga texteditorn

Döp om filen till EFModel.snippet


# ConnectionString

Då connectionstring är tråkig att skriva kan vi skapa en snippet för det med

Skapa en textfil

Öppna den

Fyll i följande data

```


<?xml version="1.0" encoding="utf-8"?>
<CodeSnippets xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
<CodeSnippet Format="1.0.0">
<Header>
<Title>SQL Server Connection String</Title>
<Author>Me!</Author>
<Description>Inserts SQL Server connection string.</Description>
<Shortcut>ConStr</Shortcut>
</Header>
<Snippet>
<Code Language="CSharp">
<![CDATA[optionsBuilder.UseSqlServer($$@"Server=.\SQLEXPRESS;Database={DatabaseName}; Trusted_Connection = true;");]]>
</Code>
</Snippet>
</CodeSnippet>
</CodeSnippets>
```

Ändra Author och Shortcut om du vill. Anpassa Connectionstringen innanför CDATA till vad du vill.

Sen kan du spara och döpa om den till SQL Server ConnectionString.snippet


# DBSet

Nu gör vi en snippet för att skapa DBSets

Skapa en textfil

Öppna den

Fyll i följande data

```


<?xml version="1.0" encoding="utf-8"?>
<CodeSnippets xmlns="http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet">
<CodeSnippet Format="1.0.0">
<Header>
<Title>DBSet generator</Title>
<Author>Me!</Author>
<Description>Creates a DBSet.</Description>
<Shortcut>DBSet</Shortcut>
</Header>
<Snippet>
<Code Language="CSharp">
<![CDATA[public DbSet<$Model$> $Model$s { get; set; }]]>
</Code>
<Declarations>
<Literal>
<ID>Model</ID>
<ToolTip>Choose the model to use.</ToolTip>
<Default>MyModel</Default>
</Literal>
</Declarations>
</Snippet>
</CodeSnippet>
</CodeSnippets>
```

Här skapar vi en variabel som vi kallar MyModel, den kan användas i vår kod som $MyModel$ och då kommer VS att fråga oss om vilken model vi vill använda.

Spara den och döp om den till DBSet.snippet

Starta om Visual Studio och njut av dina skapelser!

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
