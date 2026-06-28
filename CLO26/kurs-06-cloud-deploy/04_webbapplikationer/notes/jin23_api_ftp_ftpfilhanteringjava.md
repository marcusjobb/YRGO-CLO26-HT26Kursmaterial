---

title: FTP-Filhantering i Java
author: Marcus Ackre Medina
type: article
topic: api
difficulty: 1
language: java
status: adapted
marcus_voice: true
source: "Old_courses/books/JIN23/api/ftp/ftpfilhanteringjava.md"
description: "För att kunna arbeta med FTP behöver du lägga till ett externt bibliotek till ditt projekt. I det här exemplet använder vi Apache Commons Net, som är ett vanligt bibliotek för att arbeta med FTP i Jav"
tags: ["api", "ftp-filhantering", "ftpfilhanteringjava", "git", "java", "oop"]
week_fit: []
---

# FTP-Filhantering i Java

🟢


För att kunna arbeta med FTP behöver du lägga till ett externt bibliotek till ditt projekt. I det här exemplet använder vi Apache Commons Net, som är ett vanligt bibliotek för att arbeta med FTP i Java. Du kan lägga till biblioteket i Maven-projektet genom att lägga till följande i `pom.xml`:

```xml
    <dependencies>
        <!-- Lägg till Apache Commons Net beroendet här -->
        <dependency>
            <groupId>commons-net</groupId>
            <artifactId>commons-net</artifactId>
            <version>3.8.0</version>
        </dependency>
    </dependencies>
```

## Anslut till servern

För att ansluta till en FTP-server i Java använder du klassen `FTPClient` från paketet `org.apache.commons.net.ftp`. För att skapa en ny instans av klassen behöver du ange en `String` med serverns adress:

```java
FTPClient client = new FTPClient();
client.connect("ftp.example.com");
```

## Logga in

När du har anslutit till servern behöver du logga in med ett användarnamn och lösenord. Detta görs med metoden `login`:

```java
client.login("username", "password");
```

## Lista filer

För att lista filer på servern använder du metoden `listFiles`:

```java
FTPFile[] files = client.listFiles();
```

## Skapa en ny fil

För att skapa en ny fil på servern använder du metoden `storeFile`:

```java
client.storeFile("new_file.txt", new ByteArrayInputStream("".getBytes()));
```

## Läsa en fil

För att läsa en fil från servern använder du metoden `retrieveFile`:

```java
client.retrieveFile("file_to_read.txt", new FileOutputStream("local_file.txt"));
```

## Radera en fil

För att radera en fil på servern använder du metoden `deleteFile`:

```java
client.deleteFile("file_to_delete.txt");
```

## Kopiera från lokal dator till servern

För att kopiera en fil från din lokala dator till servern använder du metoden `storeFile`:

```java
client.storeFile("local_file.txt", new FileInputStream("local_file.txt"));
```

## Kopiera från servern till lokal dator

För att kopiera en fil från servern till din lokala dator använder du metoden `retrieveFile`:

```java
client.retrieveFile("server_file.txt", new FileOutputStream("server_file.txt"));
```

## Hantera multipla filer

För att hantera flera filer åt gången använder du metoden `mput`:

```java
client.mput(new String[] { "file1.txt", "file2.txt", "file3.txt" });
```

## Stäng anslutningen

När du är klar med FTP-anslutningen behöver du stänga den med metoden `disconnect`:

```java
client.disconnect();
```

## Exempel

Här är ett komplett exempel på hur du kan använda FTP i Java för att lista filer på en server:

```java
import java.io.IOException;
import java.net.SocketException;
import java.util.Arrays;

import org.apache.commons.net.ftp.FTPClient;
import org.apache.commons.net.ftp.FTPFile;

public class Main {

    public static void main(String[] args) throws SocketException, IOException {
        FTPClient client = new FTPClient();
        client.connect("ftp.example.com");
        client.login("username", "password");
        FTPFile[] files = client.listFiles();
        Arrays.stream(files).forEach(file -> System.out.println(file.getName()));
        client.disconnect();
    }

}
```

## Slutsats

I det här inlägget har vi tittat på hur du kan hantera filer med FTP i Java. Du kan läsa mer om Apache Commons Net på [Apache Commons Net](https://commons.apache.org/proper/commons-net/).

## Obligatorisk dad-joke

Varför gick FTP:n till doktorn? Den kände sig inte synkroniserad!

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
