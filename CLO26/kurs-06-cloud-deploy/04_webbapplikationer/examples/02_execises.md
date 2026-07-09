# Övningar: API-testning: Strategier och bästa praxis

🟢


## Övning 1: Utforska ett API med Postman

**Beskrivning:**
Använd Postman för att utforska och testa ett öppet REST API (t.ex. JSONPlaceholder, OpenWeatherMap eller liknande). Lär dig att göra grundläggande CRUD-operationer och analysera svar.

**Ledtrådar:**

- Skapa en ny Postman-collection med mappar för olika typer av anrop
- Gör minst följande anrop:
  - GET för att hämta en lista med resurser
  - GET för att hämta en specifik resurs
  - POST för att skapa en ny resurs
  - PUT för att uppdatera en resurs
  - DELETE för att ta bort en resurs
- Använd Postman-miljövariabler för att lagra bas-URL och eventuella API-nycklar
- Använd "Tests"-fliken för att lägga till enkla tester som verifierar statuskod och svarformat

**Exempel på enkelt test i Postman:**

```javascript
pm.test("Status code is 200", function () {
  pm.response.to.have.status(200);
});

pm.test("Response contains expected user", function () {
  var jsonData = pm.response.json();
  pm.expect(jsonData.name).to.eql("John Doe");
});
```

## Övning 2: Skapa automatiserade testsviter i Postman

**Beskrivning:**
Skapa en testsvit i Postman för ett användar-API som inkluderar beroenden mellan anrop och verifierar att API-beteendet är korrekt genom alla steg i ett flöde.

**Ledtrådar:**

- Skapa en collection som testar ett helt användarflöde (skapa, hämta, uppdatera, ta bort)
- Använd miljövariabler för att spara data mellan anrop (t.ex. spara ID från ett POST-anrop för användning i senare GET-anrop)
- Lägg till Pre-request Scripts för att förbereda data eller miljö
- Lägg till tester som verifierar att svaren innehåller rätt data och statuskodar
- Använd "Collection Runner" för att köra hela testsviten i sekvens

**Exempel på hur man sparar data mellan anrop:**

```javascript
// I "Tests" för ett POST-anrop
var jsonData = pm.response.json();
pm.environment.set("userId", jsonData.id);

// I ett efterföljande GET-anrop: använd {{userId}} i URL:en
```

## Övning 3: Skapa enkla enhetstester för en API-controller

**Beskrivning:**
Skapa grundläggande enhetstester för en enkel UserController i Spring Boot. Du behöver testa att controllern korrekt hanterar CRUD-operationer.

**Ledtrådar:**

- Använd JUnit 5 för att skriva testerna
- Använd Mockito för att mocka beroenden (t.ex. UserService)
- Testa minst dessa scenarier:
  - Att hämta en användare med giltigt ID returnerar rätt användare
  - Att hämta en användare med ogiltigt ID kastar rätt exception
  - Att skapa en användare med giltiga data fungerar korrekt

**Exempel på UserController som kan testas:**

```java
@RestController
@RequestMapping("/api/users")
public class UserController {
    private final UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }

    @GetMapping("/{id}")
    public ResponseEntity<User> getUserById(@PathVariable Long id) {
        return ResponseEntity.ok(userService.findById(id));
    }

    @PostMapping
    public ResponseEntity<User> createUser(@RequestBody @Valid User user) {
        return ResponseEntity.status(HttpStatus.CREATED)
                            .body(userService.createUser(user));
    }
}
```

## Övning 2: Utöka enhetstesterna med felhantering

**Beskrivning:**
Utöka enhetstesterna från övning 1 för att testa API-controllerns felhanteringsmekanism, inklusive validering av indata och hantering av exceptions.

**Ledtrådar:**

- Testa valideringsfel (t.ex. när e-post har ogiltigt format)
- Testa hantering av exceptions som kastas från service-lagret
- Verifiera att rätt HTTP-statuskoder och felmeddelanden returneras
- Använd @ExceptionHandler eller ControllerAdvice i controllern för att hantera fel

**Tips:**
Skapa en testhelper-klass för att generera testdata med olika egenskaper (giltig användare, användare med ogiltigt e-postformat, etc.)

## Övning 5: Skriv enkla API-tester med RestAssured

**Beskrivning:**
Använd RestAssured för att skriva enkla integrationstester mot ett färdigt användar-API. API:et har endpoints för att hämta, skapa, uppdatera och ta bort användare.

**Ledtrådar:**

- Konfigurera RestAssured med basURL och relevant port
- Skriv tester för att verifiera att GET /api/users/{id} returnerar rätt användare
- Skriv tester för att verifiera att POST /api/users skapar en ny användare
- Kontrollera att rätt statuskoder returneras (200 OK, 201 Created, etc.)

**Exempel på grundläggande RestAssured-test:**

```java
@Test
public void testGetUserById() {
    given()
        .pathParam("id", 1)
    .when()
        .get("/api/users/{id}")
    .then()
        .statusCode(200)
        .contentType(ContentType.JSON)
        .body("name", equalTo("Anna Andersson"))
        .body("email", equalTo("anna@example.com"));
}
```

## Övning 6: Utöka RestAssured-testerna med negativa testfall

**Beskrivning:**
Utöka RestAssured-testerna från övning 3 med fler negativa testfall för att verifiera att API:et hanterar felaktiga anrop korrekt.

**Ledtrådar:**

- Testa att hämta en användare som inte finns (bör ge 404 Not Found)
- Testa att skapa en användare med ogiltig data (bör ge 400 Bad Request)
- Verifiera att felmeddelanden innehåller förväntad information
- Testa hantering av ogiltiga indata-format (t.ex. skicka felaktigt formaterad JSON)

**Tips:**
Organisera testerna i testklasser baserat på endpoint eller funktionalitet för att göra dem mer överskådliga.

## Övning 7: Integrera testerna med GitHub Actions

**Beskrivning:**
Lägg till en enkel GitHub Actions-konfiguration för att köra dina tester automatiskt när kod pushas till klassens GitHub-repository.

**Ledtrådar:**

- Skapa en `.github/workflows`-mapp i ditt repository
- Lägg till en YAML-fil för konfiguration av GitHub Actions
- Konfigurera bygget att köra Maven- eller Gradle-kommandon för att exekvera testerna
- Ställ in notifieringar för när tester misslyckas

**Exempel på en enkel GitHub Actions-konfigurationsfil:**

```yaml
name: API Test Workflow

on: [push, pull_request]

jobs:
  test:
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v3

      - name: Set up JDK 17
        uses: actions/setup-java@v3
        with:
          java-version: "17"
          distribution: "temurin"

      - name: Build and run tests with Maven
        run: mvn test
```

## Övning 8: Analysera testresultat och kodtäckning

**Beskrivning:**
Konfigurera projektet för att generera kodtäckningsrapporter och analysera testresultaten för att identifiera områden som behöver mer testning.

**Ledtrådar:**

- Lägg till JaCoCo-plugin i Maven/Gradle-konfigurationen för att generera kodtäckningsrapporter
- Analysera kodtäckningsrapporten för att hitta områden med låg täckning
- Skriv ytterligare tester för att förbättra täckningen
- Diskutera i gruppen: vilka områden är viktigast att ha hög kodtäckning för?

**Tips:**
För att visualisera kodtäckningen i GitHub Actions, kan du konfigurera GitHub Actions att ladda upp kodtäckningsrapporter som artefakter så att de kan granskas efter varje körning.
