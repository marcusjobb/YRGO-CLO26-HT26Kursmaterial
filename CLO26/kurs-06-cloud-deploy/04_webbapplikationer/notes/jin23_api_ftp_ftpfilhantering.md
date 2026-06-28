---

title: FTP filhantering
author: Marcus Ackre Medina
type: article
topic: api
difficulty: 1
language: java
status: adapted
marcus_voice: true
source: "Old_courses/books/JIN23/api/ftp/ftpfilhantering.md"
description: "När du ansluter till en FTP-server, har du oftast behov av att utföra vissa grundläggande operationer som att lista, skapa, läsa, radera och kopiera filer. Hur gör man det?"
tags: ["api", "filhantering", "ftpfilhantering", "git", "java"]
week_fit: []
---

# FTP filhantering

🟢


När du ansluter till en FTP-server, har du oftast behov av att utföra vissa grundläggande operationer som att lista, skapa, läsa, radera och kopiera filer. Hur gör man det?

Att interagera med en FTP-server innebär inte bara att hantera enskilda filer. Ibland måste du överföra flera filer samtidigt! För detta ändamål har FTP ett urval av kommandon specifikt avsedda för att hantera multipla filer. Funderar du över hur man hanterar flera filer med FTP?

Låt oss dyka ner i FTP:s värld och upptäcka detta tillsammans!

## När du läst detta ska du kunna

- Lista filer på en FTP-server.
- Skapa, läsa och radera filer.
- Kopiera filer från din lokala dator till servern och vice versa.
- Hantera multipla filer med `mput` och relaterade kommandon.

## Lista filer

När du är ansluten till en FTP-server, kan du använda kommandot `ls` för att lista filerna i den aktuella katalogen:

```bash
ls
```

## Skapa en ny fil

Med FTP kan du inte direkt skapa en tom fil. Istället kan du ladda upp en tom fil från din lokala dator. Skapa en tom fil lokalt och använd sedan `put` för att ladda upp den:

```bash
put local_empty_file.txt
```

## Läsa en fil

FTP är främst avsett för filöverföring, så att "läsa" en fil skulle innebära att du laddar ner den för lokal visning:

```bash
get file_to_read.txt
```

## Radera en fil

För att radera en fil på FTP-servern använder du `delete`-kommandot:

```bash
delete file_to_delete.txt
```

## Kopiera från lokal dator till servern

För att kopiera en fil från din lokala dator till FTP-servern använder du `put`:

```bash
put local_file.txt
```

## Kopiera från servern till lokal dator

Om du istället vill kopiera en fil från FTP-servern till din lokala dator, använd `get`:

```bash
get server_file.txt
```

## Hantera multipla filer

FTP innehåller specifika kommandon för att hantera flera filer åt gången. Dessa är särskilt användbara när du vill överföra stora mängder data:

### Skicka flera filer till servern:

För att ladda upp flera filer från din lokala dator till FTP-servern, använd `mput`:

```bash
mput file1.txt file2.txt file3.txt
```

Eller använd vildkort för att välja flera filer:

```bash
mput *.txt
```

### Hämta flera filer från servern:

Om du vill ladda ner flera filer från FTP-servern till din lokala dator, använd `mget`:

```bash
mget fileA.txt fileB.txt fileC.txt
```

Eller med vildkort:

```bash
mget *.txt
```

## Termer

| Term | Förklaring |
| --- | --- |
| `ls` | Lista filer och kataloger i den aktuella katalogen på FTP-servern. |
| `put` | Ladda upp en fil från den lokala datorn till FTP-servern. |
| `get` | Ladda ner en fil från FTP-servern till den lokala datorn. |
| `delete` | Radera en fil på FTP-servern. |
| `mput` | Ladda upp flera filer från den lokala datorn till FTP-servern. |
| `mget` | Ladda ner flera filer från FTP-servern till den lokala datorn. |

## Slutsats

Att kunna utföra dessa grundläggande operationer på en FTP-server ger dig kraften att hantera filer på avstånd. Även om det finns andra metoder och protokoll för filöverföring, förblir FTP ett pålitligt och ofta använt verktyg i nätverksvärlden.

## Obligatorisk dad joke:

Varför var datorn kall? Den hade för många fönster öppna!

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
