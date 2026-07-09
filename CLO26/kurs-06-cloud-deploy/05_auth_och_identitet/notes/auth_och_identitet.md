# Autentisering och Identitet — Fördjupning

## Autentisering vs Auktorisering

- **Autentisering** = VEM är du? (login, identifiering)
- **Auktorisering** = Vad får DU göra? (behörighetskontroll)

Exempel: När du loggar in på Office 365 autentiserar du dig (e-post + lösenord). Om du sen försöker öppna en annans kalender kontrollerar systemet om du är auktoriserad.

## OAuth 2.0-flöden

### Authorization Code Flow (standard för web-appar)

1. Användaren klickar "Logga in med Google"
2. Appen skickar användaren till Google med en redirect-URI
3. Google visar inloggningssida, användaren godkänner
4. Google redirectar tillbaka med en authorization code
5. Appen byter koden mot en access token (och refresh token)
6. Appen använder token för att hämta data från Google API

### Client Credentials Flow (server-till-server)

1. Appen autentiserar sig med client ID + client secret
2. Får en access token direkt (ingen användare inblandad)
3. Anropar API med token

## JWT (JSON Web Token)

En JWT består av tre delar, separerade med punkt:

```
header.payload.signature
```

- **Header:** Typ av token och algoritm (t.ex. HS256)
- **Payload:** Claims (användarinformation, roller, expiration)
- **Signature:** Verifierar att token inte manipulerats

JWT är **självständig** — servern behöver inte slå upp token i en databas. All information finns i token.

## Azure AD / Entra ID

Microsofts identitetsplattform för molnet. Nyckelkoncept:

- **Tenant:** En organisation i Entra ID (contoso.onmicrosoft.com)
- **App Registration:** Registrera din app för att få ett Client ID
- **Managed Identity:** Automatisk identitet för Azure-resurser — inget lösenord krävs
- **Conditional Access:** Policy-baserad åtkomstkontroll (kräver MFA från okänd plats)

## App Registration — Steg i Azure Portal

1. Azure Portal → Entra ID → App Registrations → New Registration
2. Namnge din app, välj kontotyper som stöds
3. Ange Redirect URI (t.ex. https://localhost:5001/signin-oidc)
4. Notera Application (Client) ID
5. Skapa en Client Secret (för server-side appar)
6. Konfigurera API Permissions (vilka scopes din app behöver)

## Säkra ett API med JWT

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://login.microsoftonline.com/{tenant-id}";
        options.Audience = "{client-id}";
    });
```
