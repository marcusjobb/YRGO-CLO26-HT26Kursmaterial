# Träningsuppgifter: Cloud Deploy — Modul 3

> **Modul:** 05 — Auth och identitet (OAuth2, JWT, roller)

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är skillnaden mellan authentication och authorization?

a. Samma sak — båda handlar om säkerhet<br>b. Authentication = "Vem är du?", Authorization = "Vad får du göra?"<br>c. Authentication = "Vad får du göra?", Authorization = "Vem är du?"<br>d. Authentication är för användare, authorization är för system

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Authentication = "Vem är du?", Authorization = "Vad får du göra?"

  **Förklaringar:**

  - ❌ **a) Samma sak** - FEL: Två helt olika koncept — du måste vara autentiserad innan du kan auktoriseras
  - ✅ **b) Vem vs Vad** - **RÄTT**: Authentication: logga in med användarnamn+lösenord (verifiera identitet). Authorization: kolla om användaren har behörighet att visa admin-sidan eller ta bort en användare
  - ❌ **c) Omvänt** - FEL: Omvänt — authN först, authZ sen
  - ❌ **d) Användare vs system** - FEL: Båda gäller för både användare och system
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är OAuth2?

a. Ett protokoll för att logga in med lösenord<br>b. Ett auktoriseringsramverk som låter en app få begränsad åtkomst till en användares resurser utan att dela lösenordet<br>c. En krypteringsalgoritm<br>d. En databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett auktoriseringsramverk som låter en app få begränsad åtkomst till en användares resurser utan att dela lösenordet

  **Förklaringar:**

  - ❌ **a) Lösenordsinloggning** - FEL: OAuth2 är för auktorisering, inte för lösenordsinloggning (det är OpenID Connect)
  - ✅ **b) Delegerad åtkomst** - **RÄTT**: "Logga in med Google" — du ger en app rätt att se din Google-kalender. Appen får ALDRIG ditt Google-lösenord. Den får en access token som bara gäller för kalendern
  - ❌ **c) Kryptering** - FEL: OAuth2 använder HTTPS, men är inte krypteringsalgoritm
  - ❌ **d) Databas** - FEL: OAuth2 är ett protokoll, inte en databas
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är en JWT (JSON Web Token)?

a. Ett sätt att lagra JSON i en databas<br>b. En kompakt, URL-säker token som innehåller JSON-data (claims) — används för att skicka verifierad information mellan parter<br>c. Ett JavaScript-ramverk<br>d. En databas-fråga

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En kompakt, URL-säker token som innehåller JSON-data (claims)

  **Förklaringar:**

  - ❌ **a) JSON i databas** - FEL: JWT är en token som skickas i HTTP-huvuden, inte lagrad i databas
  - ✅ **b) Kompakt, verifierbar token** - **RÄTT**: En JWT ser ut som: `xxxx.yyyy.zzzz` (header.payload.signature). Innehåller claims: `{ "sub": "user123", "role": "admin", "exp": 1700000000 }`. Signaturen bevisar att den inte är manipulerad
  - ❌ **c) JavaScript-ramverk** - FEL: Ingenting med JS att göra. JWT är språkoberoende
  - ❌ **d) Databas-fråga** - FEL: JWT är en token för autentisering/auktorisering
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är en "claim" i JWT-sammanhang?

a. Ett påstående om en användare eller entitet — t.ex. användar-ID, roll, epost<br>b. En reklamation<br>c. Ett felmeddelande<br>d. Ett datum

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett påstående om en användare eller entitet

  **Förklaringar:**

  - ✅ **a) Information om användaren** - **RÄTT**: Claims är data i JWT:ns payload. Standard claims: `sub` (subject = användar-ID), `role`, `email`, `exp` (expiration). Du kan lägga till egna claims: `{ "department": "IT" }`
  - ❌ **b) Reklamation** - FEL: Teknisk term, inget med försäkring eller reklamation
  - ❌ **c) Felmeddelande** - FEL: Claims är data, inte fel
  - ❌ **d) Datum** - FEL: Datum kan vara en claim, men alla claims är inte datum
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är en "role" (roll) inom auktorisering?

a. Ett användarnamn<br>b. En grupp av behörigheter — t.ex. "Admin", "User", "Moderator"<br>c. En databas-tabell<br>d. Ett lösenord

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En grupp av behörigheter

  **Förklaringar:**

  - ❌ **a) Användarnamn** - FEL: Användarnamn identifierar, roller auktoriserar
  - ✅ **b) Grupp av behörigheter** - **RÄTT**: Istället för att ge varje användare individuella rättigheter skapar du roller. "Admin" får göra allt. "User" får läsa och skriva egen data. "Viewer" får bara läsa
  - ❌ **c) Databas-tabell** - FEL: Roller är ett koncept, inte nödvändigtvis en tabell
  - ❌ **d) Lösenord** - FEL: Roller och lösenord är helt olika saker
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad innebär `[Authorize(Roles = "Admin")]` i ASP.NET Core?

a. Alla kan anropa metoden<br>b. Bara användare med rollen "Admin" får anropa metoden — annars returneras 403 Forbidden<br>c. Användaren måste vara inloggad, rollen spelar ingen roll<br>d. Metoden kräver ingen autentisering

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Bara användare med rollen "Admin" får anropa metoden — annars returneras 403 Forbidden

  **Förklaringar:**

  - ❌ **a) Alla kan** - FEL: Authorize-attributet BEGRÄNSAR åtkomst
  - ✅ **b) Bara Admin-roll** - **RÄTT**: `[Authorize(Roles = "Admin")]` på en controller eller metod — om användaren inte har Admin-rollen får de 403 Forbidden. Rollen kollas från JWT:ns claims
  - ❌ **c) Bara inloggad** - FEL: `[Authorize]` utan parameter kräver bara inloggning. Med Roles krävs SPECIFIK roll
  - ❌ **d) Ingen autentisering** - FEL: `[Authorize]` kräver autentisering
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
