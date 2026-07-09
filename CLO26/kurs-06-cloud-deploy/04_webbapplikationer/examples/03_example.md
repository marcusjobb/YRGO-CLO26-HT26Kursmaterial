# Exempel

🟢


Testerna nedan är skrivna i Postman och testar ett användar-API med endpoints för att skapa, hämta, uppdatera och ta bort användare. Testerna är skrivna i JavaScript och använder Postman's inbyggda testbibliotek för att verifiera att API:et fungerar korrekt.

```javascript
// Exempel 1: Komplett CRUD-testsuite för ett användar-API

// 1. CREATE (POST) - Skapa en ny användare
// Pre-request Script
const randomUsername = "testuser_" + Date.now();
const randomEmail = "test" + Date.now() + "@example.com";

pm.variables.set("username", randomUsername);
pm.variables.set("email", randomEmail);

// Förfrågan (Request)
// POST {{baseUrl}}/users
// Content-Type: application/json
/*
{
    "username": "{{username}}",
    "email": "{{email}}",
    "name": "Test Användare",
    "role": "user"
}
*/

// Tests
pm.test("Status code is 201", function () {
  pm.response.to.have.status(201);
});

pm.test("User created successfully", function () {
  const jsonData = pm.response.json();

  pm.expect(jsonData.username).to.equal(pm.variables.get("username"));
  pm.expect(jsonData.email).to.equal(pm.variables.get("email"));
  pm.expect(jsonData.id).to.exist;

  // Spara användar-ID för senare tester
  pm.environment.set("userId", jsonData.id);
});

pm.test("Response time is acceptable", function () {
  pm.expect(pm.response.responseTime).to.be.below(1000);
});

// 2. READ (GET) - Hämta användaren
// Förfrågan (Request)
// GET {{baseUrl}}/users/{{userId}}

// Tests
pm.test("Status code is 200", function () {
  pm.response.to.have.status(200);
});

pm.test("User data is correct", function () {
  const jsonData = pm.response.json();

  pm.expect(jsonData.id.toString()).to.equal(pm.environment.get("userId"));
  pm.expect(jsonData.username).to.equal(pm.variables.get("username"));
  pm.expect(jsonData.email).to.equal(pm.variables.get("email"));
  pm.expect(jsonData.name).to.equal("Test Användare");
  pm.expect(jsonData.role).to.equal("user");
});

// 3. UPDATE (PUT) - Uppdatera användaren
// Pre-request Script
const updatedEmail = "updated" + Date.now() + "@example.com";
pm.variables.set("updatedEmail", updatedEmail);

// Förfrågan (Request)
// PUT {{baseUrl}}/users/{{userId}}
// Content-Type: application/json
/*
{
    "email": "{{updatedEmail}}",
    "name": "Uppdaterad Användare"
}
*/

// Tests
pm.test("Status code is 200", function () {
  pm.response.to.have.status(200);
});

pm.test("User updated successfully", function () {
  const jsonData = pm.response.json();

  pm.expect(jsonData.id.toString()).to.equal(pm.environment.get("userId"));
  pm.expect(jsonData.username).to.equal(pm.variables.get("username")); // Fortfarande samma
  pm.expect(jsonData.email).to.equal(pm.variables.get("updatedEmail")); // Uppdaterad
  pm.expect(jsonData.name).to.equal("Uppdaterad Användare"); // Uppdaterad
});

// 4. DELETE - Ta bort användaren
// Förfrågan (Request)
// DELETE {{baseUrl}}/users/{{userId}}

// Tests
pm.test("Status code is 204", function () {
  pm.response.to.have.status(204);
});

// 5. Verify DELETE - Verifiera att användaren är borttagen
// Förfrågan (Request)
// GET {{baseUrl}}/users/{{userId}}

// Tests
pm.test("User should not exist (404)", function () {
  pm.response.to.have.status(404);
});

// Exempel 2: Autentisering och testning av skyddade resurser

// 1. Login och hämta token
// Förfrågan (Request)
// POST {{baseUrl}}/auth/login
// Content-Type: application/json
/*
{
    "username": "{{adminUsername}}",
    "password": "{{adminPassword}}"
}
*/

// Tests
pm.test("Status code is 200", function () {
  pm.response.to.have.status(200);
});

pm.test("Login successful and token received", function () {
  const jsonData = pm.response.json();

  pm.expect(jsonData.token).to.exist;
  pm.expect(jsonData.token).to.be.a("string");
  pm.expect(jsonData.token.length).to.be.greaterThan(10);

  // Spara token för senare användning
  pm.environment.set("authToken", jsonData.token);
});

// 2. Testa åtkomst till skyddad resurs med token
// Förfrågan (Request)
// GET {{baseUrl}}/admin/users
// Headers:
// Authorization: Bearer {{authToken}}

// Tests
pm.test("Access to protected resource successful", function () {
  pm.response.to.have.status(200);
});

pm.test("Protected data is returned", function () {
  const jsonData = pm.response.json();

  pm.expect(jsonData).to.be.an("array");
  pm.expect(jsonData.length).to.be.greaterThan(0);
});

// 3. Testa åtkomst till skyddad resurs utan token
// Förfrågan (Request)
// GET {{baseUrl}}/admin/users
// (Ingen Authorization header)

// Tests
pm.test("Access without token should be denied", function () {
  pm.response.to.have.status(401);
});

pm.test("Error message is appropriate", function () {
  const jsonData = pm.response.json();

  pm.expect(jsonData.error).to.exist;
  pm.expect(jsonData.error).to.include("authentication");
});

// 4. Testa åtkomst med ogiltig token
// Förfrågan (Request)
// GET {{baseUrl}}/admin/users
// Headers:
// Authorization: Bearer invalidtoken123

// Tests
pm.test("Access with invalid token should be denied", function () {
  pm.response.to.have.status(401);
});

// 5. Testa åtkomst till resurs som kräver specifik roll
// Förfrågan (Request) - Logga in som vanlig användare
// POST {{baseUrl}}/auth/login
// Content-Type: application/json
/*
{
    "username": "{{regularUsername}}",
    "password": "{{regularPassword}}"
}
*/

// Tests
pm.test("Login as regular user successful", function () {
  pm.response.to.have.status(200);

  const jsonData = pm.response.json();
  pm.environment.set("regularUserToken", jsonData.token);
});

// Förfrågan (Request) - Försök komma åt admin-resurs som vanlig användare
// GET {{baseUrl}}/admin/settings
// Headers:
// Authorization: Bearer {{regularUserToken}}

// Tests
pm.test("Regular user should not access admin resources", function () {
  pm.response.to.have.status(403); // Forbidden
});
```

## Newman - Kommandoradsverktyg för Postman

Newman är ett kommandoradsverktyg som låter dig köra Postman-samlingar och miljöer från terminalen. Detta gör det enkelt att automatisera testning och integration av API:er i CI/CD-pipelines eller andra automatiserade processer.

För att köra en Postman-samling med

collection.json

```json
{
  "info": {
    "_postman_id": "f1b9f7d7-4b7b-4b3b-8b3b-7b3b7b3b7b3b",
    "name": "User API Tests",
    "schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
  },
  "item": [
    {
      "name": "Create User",
      "request": {
        "method": "POST",
        "header": [],
        "body": {
          "mode": "raw",
          "raw": "{\"username\": \"testuser\", \"email\": \"batman@wayne.com", \"name\": \"Bruce Wayne\", \"role\": \"admin\"}"
        },
        "url": {
          "raw": "https://api.example.com/users",
          "protocol": "https",
          "host": [
            "api",
            "example",
            "com"
          ],
          "path": [
            "users"
          ]
        },
        "description": "Create a new user"
        },
        "response": []
        }
        }
        ]
}
```

environment.json

````json
{
  "name": "Local Environment",
  "values": [
    {
      "key": "baseUrl",
      "value": "http://localhost:3000",
      "enabled": true
    }
  ],
  "_postman_variable_scope": "environment",
  "_postman_exported_at": "2021-09-01T12:00:00.000Z",
  "_postman_exported_using": "Postman/9.0.5"
}

```bash
newman run collection.json -e environment.json
````

I exemplet ovan ersätter `collection.json` med sökvägen till din Postman-samling och `environment.json` med sökvägen till din Postman-miljö. Detta kommer att köra samlingen med den angivna miljön och visa resultatet i terminalen.

Newman kan också generera rapporter i olika format, inklusive HTML, JSON och JUnit XML, för att underlätta rapportering och analys av testresultat.
