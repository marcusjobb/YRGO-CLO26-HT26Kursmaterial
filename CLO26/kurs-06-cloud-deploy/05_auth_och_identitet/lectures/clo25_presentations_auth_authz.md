---

title: AUTHENTICATION & AUTHORIZATION
author: Marcus Ackre Medina
type: lecture
topic: auth
difficulty: 3
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/presentations/auth-authzhtml.md"
description: "Identity, Access, and ASP.NET Core"
tags: ["auth", "authentication", "authorization", "authzhtml", "azure", "cloud", "dotnet", "git", "security"]
week_fit: []
---

ACD • Week 3

# AUTHENTICATION & AUTHORIZATION

🔴


Identity, Access, and ASP.NET Core

Advanced Cloud Development • 2026

## Authentication

The process of verifying the identity of a user, system, or application trying to access a resource

It confirms that users are who they claim to be

Asks the question: "Who are you?"

## Authorization

The process of determining the permissions an authenticated user has over secured resources

Defines the level of access and which actions are allowed

Asks the question: "What are you allowed to do?"

## Authentication vs Authorization

Authorization happens after authentication — you must know *who* someone is before you can decide *what* they may do

Authentication: "Who are you?" — proves identity

Authorization: "What are you allowed to do?" — grants or denies access

## Authentication — Key Concepts

Principal: an entity that can be authenticated — a human user, a computer, or a service

Credentials: the "id card" presented — passwords, certificates, tokens, or biometrics

Identity: what the principal receives once authenticated — a digital badge carrying claims

Claim: a piece of information about the principal — name, role, email, preferences

## The Full Picture

Principal
user / service


Credentials



Authentication
verifies identity


issues



Identity

Claims
name • email
role • ...


Authorization



Resource
protected data

presents
grants / denies

## Authentication — Methods

Username / Password: the classic pair — simple, familiar, but weakest on its own

Token-Based (JWT): the server issues a signed token after login; the client attaches it to subsequent requests

Multi-Factor (MFA): two or more independent checks — something you know, something you have, something you are

## Authentication — Types

Local: handled directly by the application — you own the user store

Remote: delegated to an external identity provider — OAuth / OIDC with Google, Facebook, Microsoft

Managed: a cloud service runs the user directory for you — Azure AD B2C, AWS Cognito

## Takeaway — Authentication

A Principal presents Credentials; Authentication issues an Identity carrying Claims

Methods range from passwords to tokens to MFA — layered, not exclusive

You can run auth locally, delegate it, or let the cloud manage it

Next: now that we know *who* the user is — what may they do?

## Authorization — Key Concepts

Roles: categories of users defined by what they are allowed to do — Admin, Candidate, Guest

Permissions: specific rights assigned to roles or users — read, write, delete, approve

Roles *group* permissions so access can be reasoned about at a human scale

## Authorization — Methods

Role-Based: users belong to roles; permissions attach to roles — `Administrator`, `Guest`

Claim-Based: access decided from claims on the identity — department, age, subscription tier

Policy-Based: a named set of requirements — all must be satisfied for access

## OAuth 2.0

Open Authorization — a delegated access protocol, *not* an authentication protocol

Lets third-party apps access user data without seeing credentials

An access token authorizes calls to the Resource Server on the user's behalf

Uses bearer tokens — possess the token, gain the access

## OpenID Connect (OIDC)

An authentication layer built on top of OAuth 2.0

OAuth tells you *what* a token may do — OIDC also tells you who the user is

ID Token: a JWT issued after authentication, carrying claims about the user

JWT = `Header` . `Payload` . `Signature` — compact, signed, self-describing

## OIDC Authorization Code Flow

1

Login attempt

2

Redirect to IdP

3

User consents

4

Auth code

5

Exchange code

6

Tokens returned

7

Decode ID token

8

Call UserInfo

1 — Login attempt: the user tries to sign in to the application

2 — Redirect: the app sends the user to the OIDC provider for authentication

3 — Consent: after signing in, the user approves the permissions the app requests

4 — Auth code: the provider redirects back to the app's redirect URI with a short-lived code

5 — Exchange: the app calls the provider server-side, trading the code for tokens

6 — Tokens: the provider validates the code and returns an `id_token` and `access_token`

7 — Decode: the app decodes the ID token to read user claims

8 — UserInfo: the access token calls UserInfo or other APIs on behalf of the user

## ASP.NET Core — Middleware

Authentication and authorization are both middleware components in the request pipeline

Authentication middleware: inspects the incoming request and builds a `ClaimsPrincipal` from it

Authorization middleware: checks that principal against the requirements of the endpoint

Order matters: `UseAuthentication()` must come *before* `UseAuthorization()`

## ASP.NET Core — Cookie vs JWT

Cookie Authentication: on login the server issues a session cookie; the browser returns it on every request

Great for server-rendered web apps — MVC, Razor Pages — the browser handles it transparently

JWT Authentication (OIDC): the client sends a bearer token in the `Authorization` header

Great for APIs and SPAs — stateless, cross-domain, decoupled from cookies

## ASP.NET Core — [Authorize]

`[Authorize]` on a controller or action requires an authenticated user to access it

`[AllowAnonymous]` overrides `[Authorize]` for specific endpoints — useful for login and public pages

```
[Authorize]
public class AdminController : Controller
{
    [AllowAnonymous]
    public IActionResult Login() => View();
}
```

## ASP.NET Core — Roles & Claims

Restrict by role directly in the attribute — comma-separate to allow several

```
[Authorize(Roles = "Admin")]
public IActionResult Dashboard() => View();

[Authorize(Roles = "Admin,Candidate")]
public IActionResult Profile() => View();
```

Claims are part of the user's identity — check them in code via `User.HasClaim(...)` or with a named policy

## Demo

1. Create a new ASP.NET Core application

2. Wire up authentication and authorization middleware

3. Add a login page backed by ASP.NET Core Identity

4. Decorate a controller with `[Authorize]` and try accessing it

5. Log in, visit the protected page, then log out

?

Questions
