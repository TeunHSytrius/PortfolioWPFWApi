# PortfolioWPFWApi

**Een ASP.NET Core REST API voor projecten en blogposts op mijn portfoliowebsite.**

Ontwikkeld voor Webprogrammeren Frameworks (WPFW) · HBO-ICT Software Engineering · De Haagse Hogeschool.

![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?style=flat-square)
![EF Core](https://img.shields.io/badge/EF%20Core-10.0.14-176BA0?style=flat-square)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=flat-square)
![Identity](https://img.shields.io/badge/ASP.NET%20Identity-Authenticatie%20en%20autorisatie-59636E?style=flat-square)
![Swagger](https://img.shields.io/badge/Swagger-API%20documentatie-85EA2D?style=flat-square&logo=swagger&logoColor=black)

---

## Over het project

Dit project is de backend van mijn portfoliowebsite. De API is gebouwd met ASP.NET Core Web API en beheert projecten en blogposts via REST-endpoints.

Voor de database gebruik ik Microsoft SQL Server in combinatie met Entity Framework Core. De API is opgebouwd uit controllers, services, DTO's en modellen.

Voor opdracht 4 van WPFW heb ik ASP.NET Core Identity toegevoegd. Hiermee kunnen gebruikers een account aanmaken, inloggen en uitloggen. Ook heb ik autorisatie toegevoegd, zodat niet iedere gebruiker gegevens kan aanpassen.

De backend bevat daarnaast een afzonderlijk xUnit-testproject om de API automatisch te testen.

### Huidige status

| Onderdeel                           | Status                   |
| ----------------------------------- | ------------------------ |
| CRUD voor projecten en blogposts    | Geïmplementeerd          |
| Entity Framework Core en SQL Server | Geïmplementeerd          |
| ASP.NET Core Identity               | Geïmplementeerd          |
| Registratie, login en logout        | Geïmplementeerd          |
| Cookie-authenticatie                | Geïmplementeerd          |
| Rollen en autorisatie               | Geïmplementeerd          |
| Invoervalidatie                     | Geïmplementeerd          |
| xUnit-tests                         | 38 testgevallen aanwezig |
| Blogreacties en moderatie           | Gepland                  |
| React-frontend                      | Nog niet gebouwd         |

---

## API-endpoints

De API heeft endpoints voor projecten, blogposts en gebruikersaccounts.

### Projects

| Methode  | Endpoint                    | Functie                | Toegang  |
| -------- | --------------------------- | ---------------------- | -------- |
| `GET`    | `/api/projects`             | Alle projecten ophalen | Openbaar |
| `GET`    | `/api/projects/{projectId}` | Project ophalen        | Openbaar |
| `POST`   | `/api/projects`             | Project aanmaken       | Admin    |
| `PUT`    | `/api/projects/{projectId}` | Project wijzigen       | Admin    |
| `DELETE` | `/api/projects/{projectId}` | Project verwijderen    | Admin    |

### Blogposts

| Methode  | Endpoint                      | Functie                | Toegang  |
| -------- | ----------------------------- | ---------------------- | -------- |
| `GET`    | `/api/blogposts`              | Alle blogposts ophalen | Openbaar |
| `GET`    | `/api/blogposts/{blogpostId}` | Blogpost ophalen       | Openbaar |
| `POST`   | `/api/blogposts`              | Blogpost aanmaken      | Admin    |
| `PUT`    | `/api/blogposts/{blogpostId}` | Blogpost wijzigen      | Admin    |
| `DELETE` | `/api/blogposts/{blogpostId}` | Blogpost verwijderen   | Admin    |

### Authenticatie

| Methode | Endpoint             | Functie                     | Toegang  |
| ------- | -------------------- | --------------------------- | -------- |
| `POST`  | `/api/auth/register` | Account aanmaken            | Openbaar |
| `POST`  | `/api/auth/login`    | Inloggen                    | Openbaar |
| `GET`   | `/api/auth/me`       | Ingelogde gebruiker ophalen | Ingelogd |
| `POST`  | `/api/auth/logout`   | Uitloggen                   | Ingelogd |

### HTTP-statuscodes

De API gebruikt onder andere de volgende statuscodes:

| Statuscode         | Betekenis                                              |
| ------------------ | ------------------------------------------------------ |
| `200 OK`           | Verzoek succesvol uitgevoerd                           |
| `201 Created`      | Nieuw account of record aangemaakt                     |
| `204 No Content`   | Record gewijzigd of verwijderd, of gebruiker uitgelogd |
| `400 Bad Request`  | Ongeldige invoer                                       |
| `401 Unauthorized` | Gebruiker is niet ingelogd                             |
| `403 Forbidden`    | Gebruiker heeft onvoldoende rechten                    |
| `404 Not Found`    | Het opgevraagde record bestaat niet                    |

---

## Architectuur

Ik heb gekozen voor een gelaagde architectuur met controllers en services. Op deze manier zijn de verantwoordelijkheden binnen de API gescheiden.

De controllers verwerken HTTP-verzoeken en geven de juiste responses terug. De services bevatten de CRUD-logica en gebruiken Entity Framework Core om gegevens op te halen of aan te passen.

Voor de communicatie tussen de API en de client gebruik ik DTO's. Hierdoor worden de database-entiteiten niet rechtstreeks teruggegeven.

```text
Client / Swagger
       |
       v
   Controllers          HTTP-verzoeken en responses
       |
       v
     Services           CRUD-logica en DTO-mapping
       |
       v
ApplicationDbContext    Entity Framework Core
       |
       v
    SQL Server          Database
```

### Projectstructuur

```text
PortfolioWPFWApi/
├── backend/
│   ├── PortfolioWPFWApi.sln
│   ├── PortfolioWPFWApi/
│   │   ├── Controllers/
│   │   │   ├── AuthController.cs
│   │   │   ├── ProjectsController.cs
│   │   │   └── BlogpostsController.cs
│   │   ├── Services/
│   │   │   ├── ProjectService.cs
│   │   │   └── BlogpostService.cs
│   │   ├── DTOs/
│   │   ├── Models/
│   │   ├── Data/
│   │   │   └── ApplicationDbContext.cs
│   │   ├── Security/
│   │   │   ├── AppRole.cs
│   │   │   └── AuthorizationPolicies.cs
│   │   ├── Migrations/
│   │   ├── Properties/
│   │   │   └── launchSettings.json
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── PortfolioWPFWApi.csproj
│   └── Tests/
│       ├── ApiWebApplicationFactory.cs
│       ├── AuthControllerTests.cs
│       ├── ProjectsControllerTests.cs
│       ├── BlogpostControllerTests.cs
│       └── Tests.csproj
├── frontend/
├── Docs/
│   └── AI_GEBRUIK.md
├── .gitignore
└── README.md
```

### Verantwoordelijkheden

| Onderdeel   | Verantwoordelijkheid                 |
| ----------- | ------------------------------------ |
| Controllers | HTTP-verzoeken verwerken             |
| Services    | CRUD-functionaliteit uitvoeren       |
| Models      | Database-entiteiten                  |
| DTOs        | Gegevens versturen en ontvangen      |
| Data        | Databasecontext en EF Core           |
| Security    | Rollen en autorisatiepolicies        |
| Program.cs  | Configuratie en dependency injection |
| Tests       | Automatisch testen van de API        |

### Performance

Voor de databasebewerkingen gebruik ik asynchrone EF Core-methodes.

Bij GET-verzoeken gebruiken de services `AsNoTracking()`, omdat de opgehaalde gegevens niet worden aangepast.

Ook gebruik ik dependency injection voor de services en de databasecontext.

Op dit moment halen de lijst-endpoints alle records op. Pagination kan later worden toegevoegd wanneer dat nodig is.

---

## Installatie en configuratie

### Benodigdheden

Om het project lokaal te draaien heb je het volgende nodig:

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- Microsoft SQL Server
- Git
- JetBrains Rider of Visual Studio
- Entity Framework Core CLI

SQL Server Management Studio (SSMS) kan worden gebruikt om de database te bekijken en gebruikersrollen aan te passen.

### 1. Repository clonen

Clone de repository:

```powershell
git clone https://github.com/TeunHSytrius/PortfolioWPFWApi.git
```

Ga daarna naar het API-project:

```powershell
cd PortfolioWPFWApi/backend/PortfolioWPFWApi
```

### 2. Dependencies installeren

Herstel de NuGet-packages:

```powershell
dotnet restore
```

Controleer eventueel of het project compileert:

```powershell
dotnet build
```

### 3. Databaseverbinding instellen

De API gebruikt SQL Server via Entity Framework Core.

De connectionstring wordt opgehaald met:

`ConnectionStrings:DefaultConnection`

Ik gebruik .NET User Secrets om de lokale connectionstring buiten de repository te bewaren.

Stel de connectionstring in met:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<JOUW_CONNECTIONSTRING>"
```

Vervang de placeholder door de connectionstring van je eigen SQL Server-database.

In de connectionstring staan onder andere de servernaam, databasenaam en authenticatie-instellingen.

**Let op:** sla geen wachtwoorden, connectionstrings met gevoelige gegevens of API-sleutels op in GitHub. User Secrets voorkomt dat deze gegevens in de repository staan, maar versleutelt ze lokaal niet.

### 4. Entity Framework Core CLI installeren

Als `dotnet ef` nog niet is geïnstalleerd:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.14
```

Wanneer de tool al aanwezig is, kun je deze stap overslaan.

### 5. Database aanmaken

De repository bevat twee migrations:

| Migration       | Functie                                       |
| --------------- | --------------------------------------------- |
| `InitialCreate` | Maakt de tabellen voor projecten en blogposts |
| `AddIdentity`   | Voegt de Identity-tabellen toe                |

Voer de migrations uit met:

```powershell
dotnet ef database update
```

Hiermee wordt de database aangemaakt of bijgewerkt.

Je kunt de beschikbare migrations controleren met:

```powershell
dotnet ef migrations list
```

### Databasestructuur

De database bevat onder andere de volgende tabellen:

```text
PortfolioWPFW
├── dbo.Projects
├── dbo.Blogposts
├── dbo.AspNetUsers
├── dbo.AspNetRoles
├── dbo.AspNetUserRoles
├── dbo.AspNetUserClaims
├── dbo.AspNetRoleClaims
├── dbo.AspNetUserLogins
├── dbo.AspNetUserTokens
└── dbo.__EFMigrationsHistory
```

De Identity-tabellen worden gebruikt voor gebruikersaccounts, rollen en andere gegevens die Identity nodig heeft.

### Nieuwe migrations

Wanneer het datamodel verandert, kan een nieuwe migration worden aangemaakt:

```powershell
dotnet ef migrations add NaamVanMigration
```

Daarna kan de database worden bijgewerkt:

```powershell
dotnet ef database update
```

Controleer de migration voordat je deze toepast.

---

## API starten

### HTTPS-certificaat

Voor lokaal gebruik van HTTPS kun je het ontwikkelcertificaat vertrouwen:

```powershell
dotnet dev-certs https --trust
```

### Applicatie starten

Start de API vanuit de map `backend/PortfolioWPFWApi`:

```powershell
dotnet run --launch-profile https
```

De adressen staan ingesteld in `Properties/launchSettings.json`.

| Protocol | Adres                    |
| -------- | ------------------------ |
| HTTPS    | `https://localhost:7235` |
| HTTP     | `http://localhost:5167`  |

### Swagger

Tijdens development is Swagger beschikbaar op:

**https://localhost:7235/swagger/index.html**

Via Swagger kun je de endpoints bekijken en HTTP-verzoeken uitvoeren.

Swagger is alleen ingeschakeld in de Development-omgeving.

---

## Authenticatie en autorisatie

Voor gebruikersbeheer gebruik ik ASP.NET Core Identity in combinatie met Entity Framework Core.

De registratie- en loginroutes heb ik zelf toegevoegd in `AuthController`.

### Gebruikersrollen

Ik gebruik de volgende rollen voor mijn portfoliowebsite:

| Rol         | Rechten                                                  |
| ----------- | -------------------------------------------------------- |
| `Bezoeker`  | Projecten en blogposts bekijken                          |
| `Moderator` | Bedoeld voor het later beheren van blogreacties          |
| `Admin`     | Projecten en blogposts aanmaken, wijzigen en verwijderen |

De rollen staan centraal in `Security/AppRole.cs`.

Bij het opstarten controleert de API met `RoleManager` of deze rollen al in de database staan. Rollen die nog ontbreken, worden automatisch aangemaakt.

De rol `Moderator` is voorbereid voor toekomstige functionaliteit. Er is nog geen moderatie-endpoint geïmplementeerd.

### Autorisatiepolicies

De autorisatiepolicies staan in `Security/AuthorizationPolicies.cs`.

| Policy                | Rollen           | Gebruik                        |
| --------------------- | ---------------- | ------------------------------ |
| `CanManageContent`    | Admin            | Projecten en blogposts beheren |
| `CanModerateComments` | Moderator, Admin | Later blogreacties modereren   |

De GET-routes zijn openbaar met `[AllowAnonymous]`.

Voor POST, PUT en DELETE gebruiken de controllers:

```csharp
[Authorize(Policy = AuthorizationPolicies.CanManageContent)]
```

Hierdoor worden de rechten door de API gecontroleerd. De frontend bepaalt dus niet zelf of een gebruiker toegang heeft.

### Registreren

Een nieuw account kan worden aangemaakt via:

`POST /api/auth/register`

Voorbeeld van een request:

```json
{
  "email": "test@example.com",
  "password": "<EIGEN_STERK_WACHTWOORD>"
}
```

Bij registratie gebruikt de API `UserManager` om de gebruiker aan te maken.

Iedere nieuwe gebruiker krijgt automatisch de rol `Bezoeker`. Via de registratieroute kunnen geen hogere rollen worden gekozen.

Identity controleert of het e-mailadres geldig en uniek is en of het wachtwoord voldoet aan de ingestelde eisen.

De minimale wachtwoordlengte is 12 tekens. Daarnaast gelden de overige wachtwoordregels van Identity.

Bij een geslaagde registratie geeft de API `201 Created` terug.

Na registratie moet de gebruiker nog apart inloggen.

### Inloggen

Inloggen gebeurt via:

`POST /api/auth/login`

Hierbij worden het e-mailadres en wachtwoord meegestuurd.

De API gebruikt `SignInManager` om de inloggegevens te controleren.

Na een geslaagde login wordt een authenticatiecookie teruggegeven. De browser kan deze cookie bij volgende verzoeken meesturen.

De API gebruikt geen JWT-token voor deze login.

Bij verkeerde inloggegevens geeft de API `401 Unauthorized` terug.

### Cookie-authenticatie

De authenticatiecookie is als volgt ingesteld:

- HTTP-only.
- Alleen via HTTPS.
- `SameSite=Lax`.
- Een geldigheidsduur van 8 uur.
- Sliding expiration.

De cookie wordt gebruikt om te herkennen welke gebruiker een verzoek uitvoert.

Bij beveiligde endpoints controleert de API daarna of de gebruiker voldoende rechten heeft.

### Ingelogde gebruiker ophalen

Met:

`GET /api/auth/me`

kan de ingelogde gebruiker zijn accountgegevens ophalen.

De response bevat het gebruikers-ID, e-mailadres en de gebruikersnaam.

Zonder geldige aanmelding geeft deze route `401 Unauthorized` terug.

### Uitloggen

Uitloggen gebeurt via:

`POST /api/auth/logout`

De API gebruikt `SignInManager.SignOutAsync()` om de gebruiker af te melden.

Bij een geslaagde logout wordt `204 No Content` teruggegeven.

Na het uitloggen kan de gebruiker de beveiligde routes niet meer gebruiken zonder opnieuw in te loggen.

---

## Rollen lokaal toekennen

Nieuwe gebruikers krijgen standaard de rol `Bezoeker`.

Om de schrijfrechten van de API te testen, kan ik een geregistreerd account handmatig de rol `Admin` geven via SQL Server Management Studio.

Hiervoor is geen aparte beheerroute gemaakt.

### Werkwijze

1. Start de API zodat de rollen beschikbaar zijn.
2. Registreer een testaccount via `/api/auth/register`.
3. Open SQL Server Management Studio.
4. Selecteer de lokale database die door `DefaultConnection` wordt gebruikt.
5. Controleer of je niet de testdatabase `PortfolioWPFW_Test` gebruikt.
6. Voer het SQL-script hieronder uit.
7. Log uit en daarna opnieuw in met het testaccount.
8. Controleer via Swagger of de gebruiker schrijfrechten heeft.

### SQL-script

Vervang het e-mailadres door dat van het geregistreerde account.

```sql
DECLARE @Email NVARCHAR(256) = N'test@example.com';
DECLARE @UserId NVARCHAR(450);
DECLARE @RoleId NVARCHAR(450);

IF DB_NAME() = N'PortfolioWPFW_Test'
    THROW 50000, 'Gebruik niet de testdatabase.', 1;

SELECT @UserId = Id
FROM dbo.AspNetUsers
WHERE NormalizedEmail = UPPER(@Email);

SELECT @RoleId = Id
FROM dbo.AspNetRoles
WHERE NormalizedName = N'ADMIN';

IF @UserId IS NULL OR @RoleId IS NULL
    THROW 50000, 'Gebruiker of rol niet gevonden.', 1;

IF NOT EXISTS
(
    SELECT 1
    FROM dbo.AspNetUserRoles
    WHERE UserId = @UserId AND RoleId = @RoleId
)
BEGIN
    INSERT INTO dbo.AspNetUserRoles (UserId, RoleId)
    VALUES (@UserId, @RoleId);
END;
```

Het script zoekt de gebruiker en de Admin-rol op en koppelt deze aan elkaar.

Als het account de Admin-rol al heeft, wordt deze niet opnieuw toegevoegd.

De bestaande Bezoeker-rol hoeft niet te worden verwijderd. Identity ondersteunt meerdere rollen per gebruiker.

Voor het toekennen van de Moderator-rol kan dezelfde procedure worden gebruikt met `MODERATOR` als rolnaam.

**Belangrijk:** na het aanpassen van een rol moet de gebruiker opnieuw inloggen. De bestaande authenticatiecookie kan namelijk nog de oude rolgegevens bevatten.

### Toegangsrechten controleren

| Gebruiker     | GET        | POST, PUT en DELETE |
| ------------- | ---------- | ------------------- |
| Niet ingelogd | Toegestaan | 401 Unauthorized    |
| Bezoeker      | Toegestaan | 403 Forbidden       |
| Moderator     | Toegestaan | 403 Forbidden       |
| Admin         | Toegestaan | Toegestaan          |

De backend controleert deze rechten met de ingestelde autorisatiepolicy.

---

## Testen met Swagger

Swagger kan worden gebruikt om de authenticatie en autorisatie handmatig te controleren.

Een eenvoudige testvolgorde is:

1. Open Swagger via HTTPS.
2. Registreer een account met `POST /api/auth/register`.
3. Log in via `POST /api/auth/login`.
4. Controleer de aanmelding met `GET /api/auth/me`.
5. Probeer als Bezoeker een project aan te maken.
6. Controleer of de API `403 Forbidden` teruggeeft.
7. Ken het account lokaal de Admin-rol toe.
8. Log uit en opnieuw in.
9. Probeer opnieuw een project aan te maken.
10. Controleer of de API `201 Created` teruggeeft.

Swagger kent zelf geen rollen toe. De browser stuurt de authenticatiecookie mee en de API bepaalt of het verzoek is toegestaan.

Wanneer de cookie niet wordt meegestuurd, kan de login ook met een HTTP-client zoals Postman worden getest.

---

## Invoervalidatie

Voor de invoer gebruikt de API aparte DTO's.

Bij projecten is `Title` verplicht. Bij blogposts zijn `Title` en `Content` verplicht.

Hiervoor gebruik ik de `[Required]`-annotatie op de Create- en Update-DTO's.

De controllers gebruiken `[ApiController]`, waardoor ASP.NET Core ongeldige modelgegevens automatisch kan afwijzen met `400 Bad Request`.

De tests controleren ook ongeldige JSON-invoer, zoals een verkeerd datatype voor een titel of een ongeldige datum bij een blogpost.

---

## Geautomatiseerde tests

Voor opdracht 4 heb ik een afzonderlijk xUnit-testproject toegevoegd aan dezelfde solution.

De tests staan in:

`backend/Tests`

Ik gebruik `WebApplicationFactory` om de API via HTTP te testen. Hiermee worden niet alleen controller-methodes getest, maar ook routing, modelbinding, authenticatie en autorisatie.

### Testbestanden

| Bestand                      | Testgevallen | Onderdeel                     |
| ---------------------------- | -----------: | ----------------------------- |
| `ProjectsControllerTests.cs` |           14 | Projects CRUD en autorisatie  |
| `BlogpostControllerTests.cs` |           14 | Blogposts CRUD en autorisatie |
| `AuthControllerTests.cs`     |           10 | Registratie, login en logout  |
| **Totaal**                   |       **38** |                               |

Deze 38 testgevallen staan op de `master`-branch van de repository.

### Wat wordt getest?

De CRUD-tests controleren onder andere:

- Beide GET-routes.
- POST, PUT en DELETE.
- Verzoeken met een onbekend ID.
- Verzoeken zonder aanmelding.
- Verzoeken met onvoldoende rechten.
- Toegestane schrijfacties als Admin.
- Ongeldige invoer bij POST en PUT.
- Het opslaan van gegevens na POST.
- Het aanpassen van gegevens na PUT.
- Het verwijderen van gegevens na DELETE.

Bij POST wordt het aangemaakte record opnieuw opgehaald met GET om te controleren of het is opgeslagen.

De PUT- en DELETE-tests controleren ook het resultaat van de bewerking.

De authenticatietests controleren:

- Registratie met geldige gegevens.
- Registratie met ongeldige gegevens.
- Registratie met een bestaand e-mailadres.
- Inloggen met juiste en verkeerde gegevens.
- Inloggen met een onbekende gebruiker.
- `/me` met en zonder aanmelding.
- Uitloggen en de toegang daarna.

### Arrange, Act en Assert

Ik gebruik het AAA-principe om de tests overzichtelijk te houden.

- **Arrange:** testgegevens en benodigde onderdelen voorbereiden.
- **Act:** de actie uitvoeren, bijvoorbeeld een HTTP-request.
- **Assert:** controleren of de response en het resultaat kloppen.

Deze onderdelen zijn in de tests herkenbaar met `// Arrange`, `// Act` en `// Assert`.

### Aparte testdatabase

De tests gebruiken een afzonderlijke SQL Server-database:

`PortfolioWPFW_Test`

Hierdoor worden de normale gegevens van de API niet gebruikt voor de tests.

De testconnectionstring wordt opgeslagen in .NET User Secrets onder:

`ConnectionStrings:TestConnection`

Stel deze in vanuit de map `backend/PortfolioWPFWApi`:

```powershell
dotnet user-secrets set "ConnectionStrings:TestConnection" "<JOUW_TEST_CONNECTIONSTRING>"
```

De testdatabase moet vooraf bestaan en de benodigde migrations bevatten.

De `ApiWebApplicationFactory` gebruikt deze connectionstring in plaats van de normale databaseverbinding.

Er wordt gecontroleerd of de databasenaam daadwerkelijk `PortfolioWPFW_Test` is. Wanneer dit niet klopt, wordt de testconfiguratie gestopt.

De tests maken eigen testgegevens aan en verwijderen deze na afloop.

### Tests uitvoeren

Voer vanuit de hoofdmap van de repository uit:

```powershell
dotnet test backend/PortfolioWPFWApi.sln
```

De tests kunnen ook vanuit JetBrains Rider worden uitgevoerd.

### Code coverage

Om de code coverage te meten gebruik ik `coverlet.collector`.

De coverage kan worden verzameld met:

```powershell
dotnet test backend/PortfolioWPFWApi.sln --collect:"XPlat Code Coverage"
```

Het rapport wordt opgeslagen onder `TestResults` als `coverage.cobertura.xml`.

#### Eerdere coverage-meting

Bij een eerdere run met 34 tests zijn de volgende resultaten gemeten:

| Onderdeel                               | Coverage |
| --------------------------------------- | -------: |
| Totale line coverage                    |   29,43% |
| Totale branch coverage                  |   80,43% |
| Line coverage zonder EF Core-migrations |   95,91% |
| ProjectsController                      |     100% |
| BlogpostsController                     |     100% |
| ProjectService                          |     100% |
| BlogpostService                         |     100% |

De totale line coverage is lager doordat de gegenereerde EF Core-migrations en de modelsnapshot worden meegenomen in het rapport.

Deze bestanden worden tijdens de HTTP-tests niet uitgevoerd, omdat de tests gebruikmaken van een bestaande testdatabase waarop de migrations al moeten zijn toegepast.

Zonder de gegenereerde migratiebestanden komt de herberekende line coverage uit op 95,91%.

Dit percentage is berekend op basis van het eerdere coveragebestand en niet met een aparte gefilterde Coverlet-run.

De coverage richt zich vooral op de controllers, services en de werking van de API.

De genoemde resultaten horen nog bij de eerdere run met 34 tests. De actuele coverage van alle 38 tests moet opnieuw worden gemeten.

---

## Verdere ontwikkeling

De backend is bedoeld om later gekoppeld te worden aan mijn portfoliowebsite.

De volgende onderdelen kunnen nog worden toegevoegd:

- Blogreacties met moderatiemogelijkheden.
- Een beveiligde beheerroute voor het toekennen van rollen.
- Bescherming tegen CSRF-aanvallen bij cookie-gebaseerde schrijfacties.
- De koppeling met de React-frontend.
- Aanvullende CORS- en cookieconfiguratie wanneer de frontend vanaf een andere origin draait.
- Pagination voor grotere hoeveelheden gegevens.

De huidige opdracht richt zich op de backend. De React-frontend en deployment worden nog niet uitgewerkt.

---

## Documentatie en bronnen

### Projectdocumentatie

- [AI-gebruik en werkwijze](Docs/AI_GEBRUIK.md)
- [GitHub-repository](https://github.com/TeunHSytrius/PortfolioWPFWApi)
- [Portfoliowebsite](https://github.com/TeunHSytrius/TeunHSytrius.github.io)

### Technische documentatie

- [ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-10.0)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [EF Core Migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-10.0)
- [Identity API authorization](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-10.0)
- [Role-based authorization](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-10.0)
- [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-10.0)
- [xUnit en dotnet test](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-csharp-with-xunit)

---

**PortfolioWPFWApi**
HBO-ICT Software Engineering · De Haagse Hogeschool
Webprogrammeren Frameworks (WPFW)
