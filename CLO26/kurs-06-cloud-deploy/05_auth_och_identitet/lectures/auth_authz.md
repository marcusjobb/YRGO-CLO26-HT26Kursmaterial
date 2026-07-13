# Autentisering och auktorisering

Innan en webbapplikation släpper in en användare behöver den ställa två helt olika frågor — och det är lätt att blanda ihop dem.

Den första frågan är: **vem är du?** Det kallas autentisering. Den andra frågan är: **vad får du göra?** Det kallas auktorisering.

Tänk på ett kontorshus. När du sveper ditt passerkort vid entrén bekräftar du vem du är — det är autentisering. Innanför dörren avgör systemet om du har rätt att gå in i serverrummet, skriva ut på tredje våningen, eller bara vara i öppna kontorslandskapet — det är auktorisering. De hänger ihop, men de är inte samma sak. Auktorisering förutsätter alltid att autentisering redan har skett.

## Hur identiteten förvaltas

Den enklaste formen av autentisering är användarnamn och lösenord. När du loggar in skapas en **session** på servern — en liten minneslapp som säger "den här webbläsaren är inloggad som Marcus". Webbläsaren får en session-cookie som den skickar med vid varje anrop, och servern slår upp minneslappen. Enkelt, men det kräver att servern håller koll på alla aktiva sessioner.

Ett modernare alternativ är **JWT — JSON Web Token**. Istället för att servern minns dig ger den dig ett signerat bevis direkt: ett litet paket med information om vem du är, vilka roller du har, och hur länge beviset gäller. Du bär med dig beviset i varje anrop i en HTTP-header, och servern behöver bara verifiera signaturen — ingen databasslagning krävs. Det gör JWT-autentisering naturligt stateless och väl lämpat för API:er och distribuerade system.

## OAuth 2.0 och OpenID Connect

Många system delegerar inloggningen till en extern part. Det är vad du gör när du trycker på "Logga in med Google" — du berättar för din app att Google kan bekräfta vem du är. Det protokollet heter **OAuth 2.0**, och flödet ser ungefär ut så här: appen skickar dig till Google, du loggar in och godkänner, Google skickar tillbaka en kod till appen, appen byter koden mot en access token.

OAuth 2.0 handlar egentligen om delegerad **åtkomst** — att ge en app rätt att agera på dina vägnar. För att också veta **vem** du är byggde man på ett lager till: **OpenID Connect (OIDC)**. Det är OIDC som ger appen ett ID-token, och i det tokenet finns dina claims — namn, e-post, roller och annat som beskriver dig.

## Azure AD och Entra ID

I en yrkesmiljö loggar studerande, kollegor och kunder sällan in med ett specifikt applösenord. Istället finns en central identitetsplattform. Microsofts heter **Azure Active Directory**, numera omdöpt till **Entra ID**.

Entra ID är en molnbaserad katalogtjänst. Din organisation är en **tenant** — ett eget utrymme med sina användare, grupper och policies. Varje app du bygger registreras i Entra ID som en **App Registration** och får ett unikt Client ID. Därifrån kan appen be om inloggning via OIDC, ta emot tokens, och veta precis vem som loggat in — utan att du behöver bygga något eget lösenordssystem.

Det är rätt tillvägagångssätt för de flesta enterprise-applikationer: låt Entra ID sköta identiteten, fokusera på det din app faktiskt ska göra.

## Claims och roller

Ett **claim** är ett påstående om en användare: `name = "Marcus"`, `email = "marcus@nionit.com"`, `role = "Admin"`. Tänk på det som information som är inlåst i nyckelringen — servern utfärdade den vid inloggning och har signerat den.

**Roller** är ett sätt att gruppera behörigheter. Istället för att lista exakt vad varje enskild användare får göra säger du att alla i rollen `Admin` får göra det här, och alla i rollen `Candidate` får göra det där. Roller är claims — de bärs med i tokenet precis som namn och e-post.

## ASP.NET Core Identity och `[Authorize]`

ASP.NET Core har inbyggt stöd för allt detta. **ASP.NET Core Identity** är ett färdigt bibliotek för lokal användarhantering — det sköter lösenordshashing, sessioner, och rollhantering åt dig.

I koden skyddar du en kontroller eller en action med attributet `[Authorize]`. Utan det är endpointen öppen för alla. Med det måste användaren vara inloggad — och om du anger `[Authorize(Roles = "Admin")]` måste tokenet innehålla rätt roll.

```csharp
[Authorize]
public class DashboardController : Controller
{
    [AllowAnonymous]
    public IActionResult Login() => View();

    [Authorize(Roles = "Admin")]
    public IActionResult AdminPanel() => View();
}
```

`[AllowAnonymous]` åsidosätter `[Authorize]` för en specifik action — praktiskt för inloggningssidan, som uppenbarligen måste vara tillgänglig utan att man redan är inloggad.

Det är grundplattan. Autentisering bekräftar identiteten, auktorisering avgör vad den identiteten får göra — och ramverket hanterar mekaniken åt dig, så länge du vet varför det fungerar som det gör.
