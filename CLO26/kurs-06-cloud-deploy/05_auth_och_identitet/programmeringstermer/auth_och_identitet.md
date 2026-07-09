# 05 Auth Och Identitet — Programmeringstermer

## Autentisering
Verifiering av vem användaren är: lösenord, biometri, SSO, OAuth.

## Auktorisering
Bestämmer vad en autentiserad användare har tillgång till. Ofta roller eller claims.

## OAuth 2.0
Industristandard för auktorisering. Ger appar begränsad åtkomst utan att dela lösenord.

## OpenID Connect
Autentiseringslager ovanpå OAuth 2.0. Används av Microsoft Entra ID, Google, Facebook.

## JWT
JSON Web Token. Självständig token som innehåller användarinformation (claims). Används i OAuth 2.0.

## Bearer Token
Åtkomsttoken som skickas i HTTP-huvudet: `Authorization: Bearer <token>`.

## B2C
Business-to-Consumer. Azure AD B2C för kundautentisering med sociala identiteter.

## MFA
Multi-Factor Authentication. Två eller fler verifieringsmetoder: lösenord + kod + biometri.

## Entra ID (Azure AD)
Microsofts molnbaserade identitets- och åtkomsthantering. Används av Office 365, Azure, appar.

## Client Secret
Hemlig nyckel för en app-registrering i Entra ID. Används vid OAuth 2.0-flöden.

