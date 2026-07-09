# **Övningar: Publicera API:er på lokala servrar**  

🟢

I dessa övningar kommer du att **bygga och testa ett API lokalt** samt konfigurera det för att fungera på ditt nätverk.  

✅ **Du kommer att lära dig att:**  
1. **Starta och köra ett API på en lokal server**  
2. **Ändra API-porten och konfigurera servern**  
3. **Testa API:et via terminal och Postman**  
4. **Lägga till CORS-inställningar**  
5. **Göra API:et tillgängligt på ditt nätverk**  

---

## **💡 Övning 1 – Skapa och starta en lokal API-server**  
I denna övning ska du bygga en **enkel API-server med Spring Boot**.  

### ✅ **Uppgift:**  
1. Skapa ett nytt **Spring Boot-projekt**.  
2. Skapa en REST-controller som returnerar en **lista av produkter**.  
3. Kör servern och testa API:et i webbläsaren eller Postman.  

🔍 **Ledtrådar:**  
- Använd `@RestController` och `@RequestMapping` för att skapa en API-endpoint.  
- Din metod bör returnera en lista av strängar (`List<String>`).  
- Standardporten i Spring Boot är `8080`, hur kan du ändra den?  
- Använd `mvn spring-boot:run` för att starta servern.  

📍 **Testa i webbläsaren/Postman:**  
```
http://localhost:8080/products
```

<details>
<summary>💡 Lösning</summary>

**1. Skapa ett nytt Spring Boot-projekt:**  
Använd Spring Initializr:  
- **Project:** Maven  
- **Language:** Java  
- **Spring Boot:** 3.x  
- **Dependencies:** Spring Web  

**2. Skapa en REST-controller:**  

**File: `projects/local-api/src/main/java/se/campusmolndal/localapi/controllers/ProductController.java`**
```java
package se.campusmolndal.localapi.controllers;

import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
@RequestMapping("/products")
public class ProductController {

    @GetMapping
    public List<String> getProducts() {
        return List.of("Laptop", "Smartphone", "Tablet");
    }
}
```

**3. Starta servern:**  
```bash
mvn spring-boot:run
```

</details>

---

## **💡 Övning 2 – Ändra API-port och konfigurera servern**  
Nu ska du **ändra standardporten från `8080` till `9090`** och lägga till en **serverkonfiguration**.  

### ✅ **Uppgift:**  
1. Ändra porten i `application.properties`.  
2. Lägg till en inställning för att sätta ett **servernamn**.  
3. Starta om servern och verifiera att API:et nu körs på den nya porten.  

🔍 **Ledtrådar:**  
- Hur kan du ändra `server.port` i Spring Boot?  
- Var kan du lägga till ett **servernamn** i inställningarna?  
- Vad måste du göra efter att ha ändrat inställningarna för att de ska börja gälla?  

📍 **Testa API:et på den nya porten:**  
```
http://localhost:9090/products
```

<details>
<summary>💡 Lösning</summary>

**1. Ändra porten i `application.properties`**  

**File: `projects/local-api/src/main/resources/application.properties`**
```
server.port=9090
server.servlet.context-path=/api
```

**2. Starta om servern:**  
```bash
mvn spring-boot:run
```

</details>

---

## **💡 Övning 3 – Testa API:et via terminalen (cURL)**  
Nu ska du **testa API:et utan Postman eller webbläsare**, genom att använda **cURL**.  

### ✅ **Uppgift:**  
1. Anropa API:et med `cURL` från terminalen.  
2. Vad svarar servern?  
3. Om det inte fungerar, undersök varför!  

🔍 **Ledtrådar:**  
- Vad gör `curl http://localhost:9090/api/products`?  
- Vilket HTTP-kommando används?  
- Vad händer om servern **inte är igång**? Hur kan du starta om den?  

<details>
<summary>💡 Lösning</summary>

**1. Testa API:et med cURL:**  
```bash
curl http://localhost:9090/api/products
```

**Förväntat svar:**
```json
["Laptop", "Smartphone", "Tablet"]
```

**Om det inte fungerar:**  
- Kontrollera att servern körs (`mvn spring-boot:run`).  
- Kontrollera att du använder rätt port (`9090`).  
- Testa att starta om servern.  

</details>

---

## **💡 Övning 4 – Hantera CORS-problem**  
Om du försöker anropa API:et från en **frontend-app** (t.ex. en React-app) kan du få ett **CORS-fel**.  

### ✅ **Uppgift:**  
1. Skapa en **CORS-konfigurationsklass**.  
2. Tillåt **alla domäner** att anropa API:et.  
3. Starta om servern och testa API:et från en frontend-app.  

🔍 **Ledtrådar:**  
- Vilken Spring Boot-annotering används för att **tillåta CORS** på en specifik endpoint?  
- Hur kan du tillåta **alla domäner** att anropa API:et?  
- Vad händer om du **inte tillåter CORS**?  

<details>
<summary>💡 Lösning</summary>

**1. Lägg till CORS-annotering på controllern:**  

**File: `projects/local-api/src/main/java/se/campusmolndal/localapi/controllers/ProductController.java`**
```java
@RestController
@RequestMapping("/products")
@CrossOrigin(origins = "*") // Allows all domains
public class ProductController {
}
```

**2. Alternativ lösning – Skapa en CORS-konfigurationsklass:**  

**File: `projects/local-api/src/main/java/se/campusmolndal/localapi/config/CorsConfig.java`**
```java
package se.campusmolndal.localapi.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.CorsRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

@Configuration
public class CorsConfig {
    @Bean
    public WebMvcConfigurer corsConfigurer() {
        return new WebMvcConfigurer() {
            @Override
            public void addCorsMappings(CorsRegistry registry) {
                registry.addMapping("/**").allowedOrigins("*");
            }
        };
    }
}
```

</details>

---

## **💡 Övning 5 – Gör API:et tillgängligt på ditt nätverk**  
Nu ska du **låta andra enheter i samma nätverk anropa ditt API**.  

### ✅ **Uppgift:**  
1. Ta reda på din **lokala IP-adress**.  
2. Ändra API-konfigurationen så att servern lyssnar på hela nätverket.  
3. Starta om API:et och testa från en annan enhet i nätverket, t.ex. mobil eller surfplatta.  

🔍 **Ledtrådar:**  
- Hur hittar du din IP-adress på **Windows** (`ipconfig`) och **Mac/Linux** (`ifconfig`)?  
- Vilken `server.address` måste du använda för att servern ska bli tillgänglig för andra enheter?  
- Vad händer om en annan enhet i nätverket försöker anropa `http://192.168.X.X:9090/api/products`?  

<details>
<summary>💡 Lösning</summary>

**1. Hitta din lokala IP-adress:**  
- **Windows:** `ipconfig | findstr IPv4`  
- **Mac/Linux:** `ifconfig | grep inet`  

**2. Uppdatera `application.properties`:**  

**File: `projects/local-api/src/main/resources/application.properties`**
```
server.address=0.0.0.0
server.port=9090
```

**3. Starta om servern och testa från en annan enhet:**  
- Från en mobil i samma nätverk:  
```
http://192.168.X.X:9090/api/products
```

</details>
