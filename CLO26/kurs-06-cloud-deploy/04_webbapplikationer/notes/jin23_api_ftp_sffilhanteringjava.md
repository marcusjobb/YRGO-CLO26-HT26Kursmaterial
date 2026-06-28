---

title: SFTP-Filhantering i Java
author: Marcus Ackre Medina
type: article
topic: api
difficulty: 1
language: java
status: adapted
marcus_voice: true
source: "Old_courses/books/JIN23/api/ftp/sffilhanteringjava.md"
description: "SFTP står för Secure File Transfer Protocol och är ett säkert sätt att överföra filer över ett nätverk. För att kunna arbeta med SFTP i Java kan vi använda biblioteket JSch. Lägg till det i ditt Maven"
tags: ["api", "git", "java", "sffilhanteringjava", "sftp-filhantering"]
week_fit: []
---

# SFTP-Filhantering i Java

🟢


SFTP står för Secure File Transfer Protocol och är ett säkert sätt att överföra filer över ett nätverk. För att kunna arbeta med SFTP i Java kan vi använda biblioteket JSch. Lägg till det i ditt Maven-projekt genom att lägga till följande i `pom.xml`:

```xml
<dependencies>
    <!-- Lägg till JSch beroendet här -->
    <dependency>
        <groupId>com.jcraft</groupId>
        <artifactId>jsch</artifactId>
        <version>0.1.55</version>
    </dependency>
</dependencies>
```

## Anslut till servern

För att ansluta till en SFTP-server i Java använder du JSch-biblioteket. Skapa en ny session och ange serverns adress:

```java
JSch jsch = new JSch();
Session session = jsch.getSession("username", "sftp.example.com", 22);
session.setPassword("password");
session.connect();
ChannelSftp sftpChannel = (ChannelSftp) session.openChannel("sftp");
sftpChannel.connect();
```

## Lista filer

För att lista filer på servern:

```java
Vector<ChannelSftp.LsEntry> list = sftpChannel.ls(".");
for (ChannelSftp.LsEntry entry : list) {
    System.out.println(entry.getFilename());
}
```

## Skapa, läsa, radera och andra operationer

Med `ChannelSftp`, kan du utföra alla dina filoperationer, som att ladda upp, ladda ner, radera, och så vidare:

```java
// Skapa eller skriv till fil
sftpChannel.put(new FileInputStream("local_file.txt"), "remote_file.txt");

// Läsa en fil
sftpChannel.get("remote_file.txt", "local_file.txt");

// Radera en fil
sftpChannel.rm("remote_file.txt");

// Radera en katalog
sftpChannel.rmdir("remote_dir");

// Skapa en katalog
sftpChannel.mkdir("new_dir");

// Ändra katalog
sftpChannel.cd("new_dir");
```

## Stäng anslutningen

När du är klar med SFTP-anslutningen, stäng kanalen och sessionen:

```java
sftpChannel.disconnect();
session.disconnect();
```

## Slutsats

I detta avsnitt har vi utforskat hur man hanterar filer med SFTP i Java för att säkert överföra filer över nätverk. Du kan läsa mer om JSch på [JSch's officiella webbplats](http://www.jcraft.com/jsch/).

## Obligatorisk dad joke:

Varför var SFTP:n stolt? Eftersom det alltid höll saker låsta och säkra! 😄

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
