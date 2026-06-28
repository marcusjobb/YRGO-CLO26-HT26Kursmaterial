---

title: 1_exercises
author: Marcus Ackre Medina
type: example
topic: api
difficulty: 1
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/java/5_api/13_create_jar_files_with_the_mave_and_gradle/1_exercises.md"
description: "Här kommer övningar för **Skapa JAR-filer med IntelliJ IDEA** på olika svårighetsnivåer, enligt dina riktlinjer."
tags: ["api", "java", "visual-studio"]
week_fit: []
---

Här kommer övningar för **Skapa JAR-filer med IntelliJ IDEA** på olika svårighetsnivåer, enligt dina riktlinjer.

---

## 🟢 **Lätt nivå – Fullständiga instruktioner och tips**

### **Övning 1: Skapa en enkel JAR-fil i IntelliJ**

#### **Mål**:

Skapa ett nytt Java-projekt i IntelliJ, skriv en enkel klass och generera en körbar JAR-fil.

#### **Instruktioner**:

1. Öppna **IntelliJ IDEA**.
2. Skapa ett nytt Java-projekt med Maven som bygghanterare.
3. Skapa en **Main Class** i paketet `se.campusmolndal.jardemo` med följande kod:

   ```java
   package se.campusmolndal.jardemo;

   /**
    * Enkel demoapplikation för att skapa en JAR-fil.
    *
    * @author Marcus Medina, Campus Mölndal
    * @created 2025-03-10
    */
   public class App {
       public static void main(String[] args) {
           System.out.println("Hello, JAR world!");
       }
   }
   ```

4. **Konfigurera en JAR-export**:

   - Öppna **File → Project Structure → Artifacts**.
   - Klicka på **"+"** → **"JAR"** → **"From modules with dependencies..."**.
   - Välj `App` som **Main Class**.
   - Spara inställningarna.

5. **Bygg JAR-filen**:

   - Gå till **Build → Build Artifacts → Build**.

6. **Kör JAR-filen** i terminalen:
   ```bash
   java -jar out/artifacts/JarDemo_jar/JarDemo.jar
   ```

#### ✅ **Mål uppnått om**:

- Du ser texten `"Hello, JAR world!"` i terminalen.

<details><summary>💡 Lösning</summary>

- Om IntelliJ inte hittar `Main Class`, kontrollera att paketnamnet är rätt.
- Kontrollera att du använder **Java 21**.
- Se till att du bygger om JAR-filen innan du testar den igen.

</details>

---

## 🟠 **Medelnivå – Mindre vägledning**

### **Övning 2: Ta emot och visa argument från kommandoraden**

#### **Mål**:

Utöka din JAR-fil så att den kan ta emot argument och skriva ut dem.

#### **Instruktioner**:

1. Använd koden från föregående övning.
2. Modifiera `main`-metoden så att programmet kan ta emot och skriva ut argument som skickas via terminalen.
3. Bygg en ny JAR-fil och kör den med argument, t.ex.:
   ```bash

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
   java -jar JarDemo.jar argument1 argument2
   ```
4. Programmet ska skriva ut:
   ```
   Argument som skickades in:
   1. argument1
   2. argument2
   ```

#### ✅ **Mål uppnått om**:

- Programmet korrekt visar alla argument som skickas via terminalen.

<details><summary>💡 Lösning</summary>

```java
public class App {
    public static void main(String[] args) {
        System.out.println("Välkommen till IntelliJ JAR-demo!");

        if (args.length > 0) {
            System.out.println("Argument som skickades in:");
            for (int i = 0; i < args.length; i++) {
                System.out.println((i + 1) + ". " + args[i]);
            }
        } else {
            System.out.println("Inga argument skickades.");
        }
    }
}
```

</details>

---

## 🔴 **Utmaning – Minimal vägledning**

### **Övning 3: Skapa en Fat JAR med externa beroenden**

#### **Mål**:

- Lägg till ett externt bibliotek (`commons-lang3`).
- Modifiera koden så att programmet använder en metod från biblioteket.
- Skapa en **Fat JAR** där alla beroenden inkluderas.

#### **Instruktioner**:

- Lägg till `commons-lang3` som beroende i ditt projekt.
- Använd `StringUtils.reverse("text")` för att vända en sträng.
- Bygg en Fat JAR som inkluderar alla beroenden.
- Kör JAR-filen och bekräfta att den fungerar utan fel.

#### ✅ **Mål uppnått om**:

- Programmet vänder en sträng korrekt.
- Fat JAR-filen kan köras utan att sakna beroenden.

<details><summary>💡 Lösning</summary>

1. Lägg till beroendet i `pom.xml`:

   ```xml
   <dependencies>
       <dependency>
           <groupId>org.apache.commons</groupId>
           <artifactId>commons-lang3</artifactId>
           <version>3.13.0</version>
       </dependency>
   </dependencies>
   ```

2. Uppdatera koden:

   ```java
   import org.apache.commons.lang3.StringUtils;

   public class App {
       public static void main(String[] args) {
           System.out.println("Välkommen till IntelliJ JAR-demo!");

           if (args.length > 0) {
               for (String arg : args) {
                   System.out.println("Original: " + arg);
                   System.out.println("Reversed: " + StringUtils.reverse(arg));
               }
           } else {
               System.out.println("Inga argument skickades.");
           }
       }
   }
   ```

3. Skapa en **Fat JAR** genom att lägga till `maven-shade-plugin` i `pom.xml`:

   ```xml
   <build>
       <plugins>
           <plugin>
               <groupId>org.apache.maven.plugins</groupId>
               <artifactId>maven-shade-plugin</artifactId>
               <version>3.5.0</version>
               <executions>
                   <execution>
                       <phase>package</phase>
                       <goals>
                           <goal>shade</goal>
                       </goals>
                   </execution>
               </executions>
           </plugin>
       </plugins>
   </build>
   ```

4. Bygg och testa Fat JAR-filen:

   ```bash
   mvn clean package
   java -jar target/JarDemo-1.0-SNAPSHOT.jar Hello
   ```

</details>

---

## 💡 **Verklig användning – Open-ended uppgift**

### **Övning 4: Skapa ett interaktivt program och generera en JAR**

#### **Mål**:

- Skapa ett program som tar emot **användarinput** och gör något mer avancerat.
- Generera en **körbar JAR** och dokumentera hur en slutanvändare kan köra programmet.

#### **Förslag på funktionalitet**:

- En enkel **räknare** där användaren kan lägga till och subtrahera siffror.
- En **filhanterare** som listar filer i en katalog.
- Ett **CLI-spel** som genererar slumpmässiga tal och låter spelaren gissa.

#### **Exempelkrav**:

1. Programmet ska kunna köras med:
   ```bash
   java -jar MyApp.jar
   ```
2. Användaren ska kunna interagera via terminalen.
3. Programmet ska ge **feedback** beroende på användarens input.
4. Programmet måste ha **felhantering**.

💡 **Extra utmaning**: Skapa en README-fil som beskriver hur man bygger och kör din applikation!

---

## **Reflektionsfrågor**

1. Vad är skillnaden mellan en vanlig JAR-fil och en Fat JAR?
2. Varför behöver vi specificera en **Main Class** i en JAR-fil?
3. Hur skulle du kunna förbättra arbetsflödet för att bygga och köra JAR-filer i större projekt?

---

**Säg till om du vill ha fler utmaningar! 😃**
