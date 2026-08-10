# Auth och Identitet — Programmeringstermer

> 🖼️ **Bild:** Meme — "Authentication vs Authorization" med bild av en dörrvakt (autentisering = vem är du?) och en nyckelknippa (auktorisering = vilka dörrar får du öppna?)

---

## Autentisering · Autentisering

Autentisering är processen att verifiera att du är den du påstår dig vara. Lösenord, BankID, fingeravtryck, engångskoder — alla är former av autentisering.

Tänk på det som när du visar legitimation vid en nattklubb. Dörrvakten verifierar att du är den på ID-kortet — det är autentisering.

```csharp
// ASP.NET Core — kräv autentisering på en controller
[Authorize]
public class MinProfilController : Controller
{
    // Bara inloggade användare når hit
    public IActionResult Index() => View();
}
```

Autentisering svarar på: *Vem är du?* — Auktorisering (nästa term) svarar på: *Vad får du göra?*

---

## Auktorisering · Auktorisering

Auktorisering avgör vad en autentiserad användare har rätt att göra. Du vet vem de är — nu avgör du vad de får.

Du är på nattklubb (autentiserad). Dörrvakten kontrollerar om du har VIP-armband (auktorisering). Du kan vara inne på krogen utan att ha tillgång till VIP-loungen.

```csharp
// ASP.NET Core — kräv specifik roll
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    // Bara användare med rollen "Admin" når hit
    public IActionResult AnvändarLista() => View();
}
```

Vanligt misstag: man implementerar autentisering men glömmer auktorisering. Alla inloggade kan se allt — inklusive andra användares data. Det är ett allvarligt säkerhetshål.

---

## OAuth 2.0 · OAuth 2.0

OAuth 2.0 är en industristandard för att ge appar begränsad åtkomst till en resurs utan att dela lösenordet. "Logga in med Google" är OAuth 2.0 i praktiken.

Tänk på det som ett parkeringskvitto till hotellparkering. Du ger valet ditt parkeringskvitto (access token) — inte bilnyckeln. De kan parkera bilen men inte öppna din handskfack.

```mermaid
sequenceDiagram
    participant A as Din App
    participant B as Användare
    participant C as Entra ID
    participant D as Resurs (API)

    B->>A: Logga in
    A->>C: Begär access token\n(med client_id + scopes)
    C->>B: Visa inloggningssida
    B->>C: Användarnamn + lösenord
    C->>A: Access token
    A->>D: Anropa API med token
    D->>A: Data
```

Scopes avgör vad token ger åtkomst till: `User.Read` = läs profil, `Mail.Send` = skicka e-post. Be alltid om minsta möjliga scopes.

> 🖼️ **Bild:** Skärmdump av "Begär appbehörigheter"-dialogen i Microsoft-inloggning med synliga scopes

---

## OpenID Connect · OpenID Connect

OpenID Connect (OIDC) är ett autentiseringslager ovanpå OAuth 2.0. OAuth 2.0 hanterar auktorisering; OIDC lägger till vem användaren faktiskt är.

OAuth 2.0 är som att ge bort ett bibliotekskort (du får låna böcker). OpenID Connect är som att lekarbibliotekariet också kollar körkort och bekräftar att det är just du.

Med OIDC får du ett extra token: **ID-token** (ett JWT med användarens namn, e-post, bild etc.) utöver access-token. Entra ID, Google och Facebook stöder OIDC.

I .NET: använd `AddMicrosoftIdentityWebApp()` — det hanterar hela OIDC-flödet åt dig.

```csharp
// Program.cs
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
```

---

## JWT · JSON Web Token

Ett JWT (JSON Web Token) är en kompakt, URL-säker token som innehåller information om användaren (claims). Den är signerad — mottagaren kan verifiera att den inte manipulerats.

Tänk på det som ett igenkänningskort med chipet på baksidan. Du kan läsa kortet (se claims), och chipet (signaturen) bevisar att det är äkta.

```
# JWT är tre base64-delar separerade av punkter
eyJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJ1c2VyMTIzIiwicm9sZSI6IkFkbWluIn0.SIGNATUR
     ^-- Header          ^-- Payload (claims)                              ^-- Signatur
```

Payload-delen kan vem som helst läsa (base64-dekoda). Skriv aldrig hemlig information i JWT-claims. Signaturen skyddar mot manipulation, inte mot läsning.

Kolla dina tokens på [jwt.io](https://jwt.io) — oumbärlig vid felsökning.

---

## Bearer Token · Bärartoken

En bearer token är en access token som skickas i HTTP-headern för varje anrop till ett skyddat API. "Bearer" = den som bär den har åtkomst.

Det är som en festivalarmband. Vem som helst med armbandet på handleden (bearer) kan gå in på festivalen — ingen ytterligare ID-kontroll. Tappa den inte.

```http
GET /api/minprofil HTTP/1.1
Host: api.mittföretag.se
Authorization: Bearer eyJhbGciOiJSUzI1NiJ9...
```

```csharp
// Hämta token och anropa API
var token = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { "User.Read" });
_httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
```

Tokens har en livstid (ofta 1 timme). Använd refresh tokens för att hämta nya tokens utan ny inloggning.

---

## B2C · Azure AD B2C

Azure AD B2C (Business-to-Consumer) är Microsofts identitetstjänst för externa kunder — inte anställda. Kunder kan logga in med sin Google-, Facebook- eller Apple-identitet.

Tänk på det som dörrkortet till ett shoppingcenter. Du kan komma in med Swish-appen, Klarna-kontot eller din e-post — centret bryr sig inte om vilket, bara att du kan identifiera dig.

Klart för: kundportaler, e-handel, offentliga appar med många användare. Inte för: anställdainloggning — använd Entra ID (Azure AD) för det.

```json
// appsettings.json — B2C-konfiguration
"AzureAdB2C": {
    "Instance": "https://mittföretag.b2clogin.com/",
    "ClientId": "din-app-client-id",
    "Domain": "mittföretag.onmicrosoft.com",
    "SignUpSignInPolicyId": "B2C_1_signupsignin1"
}
```

---

## MFA · Multifaktorautentisering

MFA (Multi-Factor Authentication) kräver att användaren verifierar sin identitet på två eller fler sätt: något de vet (lösenord) + något de har (telefon) + något de är (fingeravtryck).

Tänk på det som ett kassaskåp med kod OCH nyckel. Ingen av dem räcker ensam — du behöver båda.

Entra ID: MFA kan krävas med Conditional Access-policyer — t.ex. "kräv MFA om användaren loggar in från ett okänt land". I Entra ID kan du tvinga MFA för alla admins med ett klick.

Vanligt misstag: man aktiverar MFA men låter "legacy authentication" (SMTP, IMAP, äldre Office) vara öppen. Det kringgår MFA. Blockera legacy auth i Conditional Access.

> 🖼️ **Bild:** Skärmdump av Microsofts Authenticator-app med en 6-siffrig kod som tickar ner

---

## Entra ID · Microsoft Entra ID

Microsoft Entra ID (tidigare Azure AD) är Microsofts molnbaserade identitets- och åtkomsthantering. Det är SSO (Single Sign-On) för alla Microsofts tjänster och tusentals tredjeparts-appar.

Tänk på det som ett passystem för hela kontorskomplexet. Samma kort öppnar ytterdörren, datorn, printers och kaffemaskinen — och du behöver bara ett kort.

Varje Azure-tenant har ett Entra ID. Användare, grupper, appar och tjänster registreras här. Ditt Azure-konto är redan i Entra ID.

```bash
# Logga in med Azure CLI — Entra ID i bakgrunden
az login
# Visa inloggad användare
az account show --query user.name
```

Entra ID hanterar Managed Identity — det är hur Azure-tjänster autentiserar mot varandra utan lösenord.

---

## Client Secret · Klienthemlighet

En client secret är ett lösenord för en appregistrering i Entra ID. Appen använder det för att autentisera mot Entra ID i server-till-server-flöden.

Det är appens lösenord — inte användarens. Precis som ditt lösenord: håll det hemligt, rotera det regelbundet, och lägg det aldrig i källkod.

```bash
# Skapa en client secret i Azure CLI
az ad app credential reset \
  --id din-app-id \
  --append \
  --display-name "prod-2026"
```

```json
// appsettings.json — ALDRIG i källkod
"AzureAd": {
    "ClientId": "...",
    "ClientSecret": "env:AZURE_CLIENT_SECRET"  // Hämtas från miljövariabel
}
```

Bättre alternativ: Managed Identity. Ingen hemlighet = ingenting att hantera eller läcka. Använd client secrets bara när Managed Identity inte är möjlig.
