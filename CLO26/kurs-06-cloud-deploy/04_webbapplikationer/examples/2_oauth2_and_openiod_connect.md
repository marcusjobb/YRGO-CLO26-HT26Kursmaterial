---

title: 2_oauth2_and_openiod_connect
author: Marcus Ackre Medina
type: example
topic: api
difficulty: 1
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/java/5_api/08_more_authentication_and_authorization/2_oauth2_and_openiod_connect.md"
description: "- **OAuth2** är som en vakt som bestämmer vad en app får göra"
tags: ["and", "api", "connect", "javascript", "oauth2", "openiod", "visual-studio"]
week_fit: []
---

#### Introduktion till OAuth2 och OpenID Connect - För nybörjare som kan JWT

#### Vad är skillnaden mellan OAuth2 och OpenID Connect?

- **OAuth2** är som en vakt som bestämmer vad en app får göra
- **OpenID Connect (OIDC)** är ett ID-system som bygger på OAuth2 och använder JWT för att bevisa vem du är

#### Tänk på det så här:

1. **OAuth2** är som ett hotellkort:

   - Ger dig tillgång till vissa rum
   - Fungerar bara under en viss tid
   - Säger inget om vem du är

2. **OpenID Connect** är som ett ID-kort:
   - Bevisar vem du är
   - Innehåller din personliga information
   - Bygger på OAuth2-systemet

#### Praktiskt exempel

Tänk dig att du vill logga in på en app med ditt Google-konto:

```javascript
// Konfigurera OAuth2/OIDC
const config = {
  clientId: "din-app-id",
  authEndpoint: "https://accounts.google.com/o/oauth2/v2/auth",
  scope: "openid profile email", // OpenID Connect scope
  redirectUri: "https://din-app.com/callback",
};

// Starta inloggningsprocessen
function loggaInMedGoogle() {
  const url = `${config.authEndpoint}?
        client_id=${config.clientId}&
        response_type=code&
        scope=${config.scope}&
        redirect_uri=${config.redirectUri}`;

  window.location.href = url;
}
```

#### Hur fungerar det i praktiken?

1. **Användaren klickar "Logga in med Google"**

   - Din app skickar användaren till Google
   - Som att gå till receptionen för att få ett hotellkort

2. **Google frågar användaren**

   - "Vill du låta denna app se din profil?"
   - "Vill du låta denna app läsa din e-post?"
   - Som att skriva under ett papper i receptionen

3. **Om användaren säger ja**
   - Google skickar tillbaka en speciell kod
   - Din app använder koden för att hämta:
     - En access token (hotellkortet)
     - En ID token (ID-kortet) med användarinfo

#### Exempel på hur man tar emot användaren

```javascript
// När Google skickar tillbaka användaren
async function hanteraCallback(kod) {
  // Byt ut koden mot tokens
  const tokens = await hämtaTokens(kod);

  // tokens.access_token = OAuth2 token (vad du får göra)
  // tokens.id_token = OpenID Connect token (vem du är)

  // Spara tokens säkert (INTE i localStorage!)
  sparaTokensSäkert(tokens);
}
```

#### Viktiga begrepp att känna till:

1. **Scope**

   - Bestämmer vad appen får tillgång till
   - Till exempel: `openid profile email`
   - Som att välja vilka rum hotellkortet ska ge tillgång till

2. **ID Token**

   - En JWT som innehåller användarinfo
   - Signerad av tjänsten (t.ex. Google)
   - Innehåller info som namn, e-post, profilbild

3. **Access Token**
   - Används för att komma åt API:er
   - Som ett tillfälligt passerkort
   - Har en begränsad livstid

#### Säkerhetstips

1. Använd alltid HTTPS
2. Spara aldrig tokens i localStorage
3. Validera alltid ID tokens
4. Använd säkra cookies för tokens

Nu när du kan JWT sedan tidigare, tänk på att OpenID Connect använder JWT för sina ID tokens - det är samma teknik du redan känner till, bara i ett större sammanhang!
