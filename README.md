# PortfolioWPFWApi

**Een ASP.NET Core REST API voor projecten en blogposts op mijn portfoliowebsite.**

Ontwikkeld voor Webprogrammeren Frameworks (WPFW) · HBO-ICT Software Engineering · De Haagse Hogeschool.

![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?style=flat-square)
![EF Core](https://img.shields.io/badge/EF%20Core-10.0.14-176BA0?style=flat-square)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=flat-square)
![Identity](https://img.shields.io/badge/ASP.NET%20Identity-Basis%20aanwezig-59636E?style=flat-square)
![Swagger](https://img.shields.io/badge/Swagger-API%20documentatie-85EA2D?style=flat-square&logo=swagger&logoColor=black)

---

## Over het project

De API beheert **projecten** en **blogposts** via REST-endpoints. De backend is gebouwd met controllers, afzonderlijke services, DTO's en Entity Framework Core. Gegevens worden opgeslagen in Microsoft SQL Server.

Er is ook een **ASP.NET Core Identity-basis** aanwezig om de API verder te beveiligen met gebruikersaccounts en rollen. De registratie- en loginfunctionaliteit is nog niet geïmplementeerd.

### Huidige status

| Onderdeel                                 | Status                                               |
| ----------------------------------------- | ---------------------------------------------------- |
| CRUD voor projecten en blogposts          | Geïmplementeerd; eerder handmatig getest via Swagger |
| EF Core + SQL Server                      | Geconfigureerd; migrations lokaal toegepast          |
| Identity-datamodel en autorisatiepolicies | Geïmplementeerd                                      |
| Registratie, login en roltoekenning       | Nog niet geïmplementeerd                             |
| Blogreacties en moderatie                 | Gepland                                              |
| Geautomatiseerde xUnit-tests              | Nog niet aanwezig                                    |
| React-frontend                            | Nog niet gebouwd                                     |

## API-endpoints

| Methode  | Endpoint                      | Functie                | Toegang  |
| -------- | ----------------------------- | ---------------------- | -------- |
| `GET`    | `/api/projects`               | Alle projecten ophalen | Openbaar |
| `GET`    | `/api/projects/{projectId}`   | Project ophalen        | Openbaar |
| `POST`   | `/api/projects`               | Project aanmaken       | Admin    |
| `PUT`    | `/api/projects/{projectId}`   | Project wijzigen       | Admin    |
| `DELETE` | `/api/projects/{projectId}`   | Project verwijderen    | Admin    |
| `GET`    | `/api/blogposts`              | Alle blogposts ophalen | Openbaar |
| `GET`    | `/api/blogposts/{blogpostId}` | Blogpost ophalen       | Openbaar |
| `POST`   | `/api/blogposts`              | Blogpost aanmaken      | Admin    |
| `PUT`    | `/api/blogposts/{blogpostId}` | Blogpost wijzigen      | Admin    |
| `DELETE` | `/api/blogposts/{blogpostId}` | Blogpost verwijderen   | Admin    |

De API gebruikt onder andere de volgende HTTP-statuscodes:

| Statuscode         | Betekenis                                |
| ------------------ | ---------------------------------------- |
| `200 OK`           | Gegevens succesvol opgehaald             |
| `201 Created`      | Nieuw record aangemaakt                  |
| `204 No Content`   | Record succesvol gewijzigd of verwijderd |
| `401 Unauthorized` | Gebruiker is niet aangemeld              |
| `403 Forbidden`    | Gebruiker heeft onvoldoende rechten      |
| `404 Not Found`    | Het opgevraagde record bestaat niet      |

> **Let op:** de schrijfroutes zijn al afgeschermd met een Admin-policy. Zolang registratie en login ontbreken, kan nog niemand als Admin inloggen om deze routes te gebruiken.

## Architectuur

De backend gebruikt een **gelaagde architectuur** waarin HTTP-afhandeling, applicatielogica en databasetoegang van elkaar gescheiden zijn.

```text
Client / Swagger
       |
       v
   Controllers          HTTP-afhandeling en statuscodes
       |
       v
     Services           CRUD-logica en DTO-mapping
       |
       v
ApplicationDbContext    Entity Framework Core
       |
       v
    SQL Server          Opslag van projecten, blogposts en Identity
```

### Projectstructuur

```text
PortfolioWPFWApi/
├── backend/
│   ├── PortfolioWPFWApi.sln
│   └── PortfolioWPFWApi/
│       ├── Controllers/
│       │   ├── ProjectsController.cs
│       │   └── BlogpostsController.cs
│       ├── Services/
│       │   ├── ProjectService.cs
│       │   └── BlogpostService.cs
│       ├── DTOs/
│       │   ├── CreateProjectDto.cs
│       │   ├── UpdateProjectDto.cs
│       │   ├── ProjectDto.cs
│       │   ├── CreateBlogpostDto.cs
│       │   ├── UpdateBlogpostDto.cs
│       │   └── BlogpostDto.cs
│       ├── Models/
│       │   ├── Project.cs
│       │   ├── Blogpost.cs
│       │   └── ApplicationUser.cs
│       ├── Data/
│       │   └── ApplicationDbContext.cs
│       ├── Security/
│       │   ├── AppRole.cs
│       │   └── AuthorizationPolicies.cs
│       ├── Migrations/
│       │   ├── 20261104234333_InitialCreate.cs
│       │   ├── 20261104234333_InitialCreate.Designer.cs
│       │   ├── 20261105170748_AddIdentity.cs
│       │   ├── 20261105170748_AddIdentity.Designer.cs
│       │   └── ApplicationDbContextModelSnapshot.cs
│       ├── Properties/
│       │   └── launchSettings.json
│       ├── Program.cs
│       ├── appsettings.json
│       └── PortfolioWPFWApi.csproj
├── frontend/
├── Docs/
│   └── AI_GEBRUIK.md
├── .gitignore
└── README.md
```

### Verantwoordelijkheden

| Onderdeel   | Verantwoordelijkheid                                |
| ----------- | --------------------------------------------------- |
| Controllers | HTTP-verzoeken ontvangen en responses teruggeven    |
| Services    | CRUD-functionaliteit en omzetting naar DTO's        |
| Models      | Database-entiteiten en het Identity-gebruikersmodel |
| DTOs        | Gegevensuitwisseling tussen client en API           |
| Data        | EF Core, DbContext en databasetoegang               |
| Security    | Centrale rolnamen en autorisatiepolicies            |
| Program.cs  | Dependency injection, configuratie en middleware    |

### Ontwerpkeuze

Ik heb gekozen voor controllers met afzonderlijke services om de verantwoordelijkheden duidelijk te scheiden.

Controllers verwerken HTTP-verzoeken en bepalen welke statuscodes worden teruggegeven. Services verzorgen de CRUD-logica en databasebewerkingen. Entity Framework Core communiceert via de `ApplicationDbContext` met SQL Server.

De API ontvangt en verstuurt DTO's in plaats van rechtstreeks database-entiteiten beschikbaar te stellen.

Deze structuur houdt de code overzichtelijk, onderhoudbaar en geschikt om later uit te breiden.

### Performance

De services gebruiken:

- Asynchrone EF Core-methodes.
- `AsNoTracking()` voor read-only queries.
- Centrale DTO-mapping.
- Dependency injection voor services en databasecontext.

Bij grotere datasets kunnen pagination en gerichte databaseprojecties worden toegevoegd. De huidige lijst-endpoints halen alle records op.

---

## Installatie en configuratie

### Benodigdheden

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Git](https://git-scm.com/)
- [JetBrains Rider](https://www.jetbrains.com/rider/) of Visual Studio
- EF Core CLI (`dotnet ef`)

SQL Server Management Studio (SSMS) is optioneel, maar kan gebruikt worden om de database te inspecteren.

### 1. Repository clonen

```powershell
git clone https://github.com/TeunHSytrius/PortfolioWPFWApi.git
cd PortfolioWPFWApi/backend/PortfolioWPFWApi
```

### 2. Dependencies installeren

```powershell
dotnet restore
```

Controleer eventueel of het project compileert:

```powershell
dotnet build
```

### 3. Databaseverbinding configureren

De API gebruikt Microsoft SQL Server via Entity Framework Core.

De connectionstring wordt opgehaald via de configuratiesleutel:

`ConnectionStrings:DefaultConnection`

Voor lokale ontwikkeling gebruiken we **.NET User Secrets**. Hiermee hoeven persoonlijke databasegegevens niet in de repository te worden opgeslagen.

Stel de connectionstring in via de terminal:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<JOUW_LOKALE_CONNECTIONSTRING>"
```

Vervang `<JOUW_LOKALE_CONNECTIONSTRING>` door een verbinding die past bij je eigen SQL Server-installatie.

De connectionstring bevat doorgaans:

- De naam van de SQL Server-instance.
- De naam van de database, bijvoorbeeld `PortfolioWPFW`.
- De gekozen authenticatiemethode.
- De instellingen voor een versleutelde verbinding.

> **Beveiliging:** sla geen databasewachtwoorden, API-sleutels of andere vertrouwelijke gegevens op in Git. .NET User Secrets bewaart deze waarden buiten het project, maar versleutelt ze lokaal niet. Gebruik voor productie een geschikte secretopslag en een correct geconfigureerde TLS-verbinding.

### 4. EF Core CLI installeren

Installeer de EF Core-tool wanneer `dotnet ef` nog niet beschikbaar is:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.14
```

Wanneer de tool al geïnstalleerd is, hoef je dit commando niet opnieuw uit te voeren.

### 5. Database aanmaken of bijwerken

De repository bevat twee EF Core-migrations:

| Migration                      | Beschrijving                                     |
| ------------------------------ | ------------------------------------------------ |
| `20261104234333_InitialCreate` | Maakt de tabellen `Projects` en `Blogposts` aan  |
| `20261105170748_AddIdentity`   | Voegt de tabellen voor ASP.NET Core Identity toe |

Pas de migrations toe met:

```powershell
dotnet ef database update
```

EF Core maakt de database aan als die nog niet bestaat en de databasegebruiker daarvoor voldoende rechten heeft.

Bij een bestaande database worden alleen nog ontbrekende migrations toegepast.

Controleer de migrations met:

```powershell
dotnet ef migrations list
```

De migrations zijn tijdens de ontwikkeling lokaal toegepast en opgenomen in GitHub.

### Databasestructuur

Na het uitvoeren van beide migrations bevat de database onder andere:

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

De tabel `__EFMigrationsHistory` houdt bij welke migrations al zijn uitgevoerd.

### Toekomstige migrations

Wanneer het datamodel verandert, kan een nieuwe migration worden aangemaakt:

```powershell
dotnet ef migrations add NaamVanMigration
```

Controleer de gegenereerde wijzigingen voordat je ze toepast:

```powershell
dotnet ef database update
```

---

## API starten

### HTTPS-certificaat configureren

Voor lokale HTTPS-ontwikkeling kan het ontwikkelcertificaat worden vertrouwd met:

```powershell
dotnet dev-certs https --trust
```

### Applicatie starten

Start de API via het bestaande HTTPS-profiel:

```powershell
dotnet run --launch-profile https
```

De lokale adressen zijn ingesteld in `Properties/launchSettings.json`.

| Protocol | Adres                    |
| -------- | ------------------------ |
| HTTPS    | `https://localhost:7235` |
| HTTP     | `http://localhost:5167`  |

### Swagger UI

Swagger UI is tijdens ontwikkeling beschikbaar op:

**https://localhost:7235/swagger/index.html**

Met Swagger kun je de beschikbare endpoints bekijken en HTTP-verzoeken uitvoeren.

Swagger wordt uitsluitend ingeschakeld in de Development-omgeving.

---

## Authenticatie en autorisatie

De backend gebruikt **ASP.NET Core Identity met Entity Framework Core** als basis voor gebruikersbeheer en beveiliging.

Identity is geïntegreerd in de bestaande `ApplicationDbContext`.

### Gebruikersrollen

Voor mijn portfoliowebsite zijn de volgende rollen ontworpen:

| Rol         | Verantwoordelijkheid                              |
| ----------- | ------------------------------------------------- |
| `Bezoeker`  | Projecten en blogposts bekijken                   |
| `Moderator` | Later blogreacties modereren                      |
| `Admin`     | Projecten en blogposts beheren en later modereren |

Publieke informatie is ook zonder account toegankelijk.

De rolnamen zijn gedefinieerd in `Security/AppRole.cs`.

### Autorisatiepolicies

De autorisatiepolicies zijn centraal vastgelegd in `Security/AuthorizationPolicies.cs`.

| Policy                | Toegestane rollen | Gebruik                                                  |
| --------------------- | ----------------- | -------------------------------------------------------- |
| `CanManageContent`    | Admin             | Projecten en blogposts aanmaken, wijzigen en verwijderen |
| `CanModerateComments` | Moderator, Admin  | Toekomstige moderatie van blogreacties                   |

De controllers gebruiken `[Authorize]` op schrijfroutes en `[AllowAnonymous]` op publieke GET-routes.

### Cookie-authenticatie

ASP.NET Core Identity is geconfigureerd voor cookie-authenticatie met:

- HTTP-only cookies.
- Secure cookies via HTTPS.
- Een ingestelde sessieduur.
- Unieke e-mailadressen.
- Wachtwoordvereisten.
- Tijdelijke accountblokkering na meerdere mislukte inlogpogingen.

### Huidige implementatiestatus

De Identity-infrastructuur is aanwezig, maar de volgende functionaliteit moet nog worden ontwikkeld:

- Registreren met `UserManager`.
- Inloggen en uitloggen.
- Identity-rollen aanmaken met `RoleManager`.
- Een standaardrol aan nieuwe gebruikers toekennen.
- Lokaal accounts met verhoogde rechten beschikbaar maken.
- CSRF-bescherming voor cookie-gebaseerde schrijfacties.
- Geautomatiseerde authenticatie- en autorisatietests.

Er zijn momenteel geen standaardaccounts of wachtwoorden opgenomen in de repository.

De aanwezige rolconstanten zorgen niet automatisch voor rolrecords in de database.

Na het wijzigen van de rol van een gebruiker moet de authenticatiecookie worden vernieuwd, bijvoorbeeld door opnieuw in te loggen.

### Afstemming op WPFW-opdracht 4

De officiële opdracht 4 vraagt om de rollen `Gast`, `Editor` en `Admin`, waarbij Editor en Admin schrijfrechten krijgen.

Het huidige ontwerp voor mijn website gebruikt `Bezoeker`, `Moderator` en `Admin`, waarbij alleen Admin inhoud mag beheren.

Deze verschillen moeten bij de verdere uitwerking worden opgelost om aan de verplichte criteria van opdracht 4 te voldoen.

---

## Testen

### Handmatig testen met Swagger

De CRUD-endpoints zijn tijdens de ontwikkeling handmatig getest via Swagger UI.

Voor zowel projecten als blogposts kan de volgende testvolgorde worden gevolgd:

1. Maak een record aan met `POST`.
2. Controleer met beide `GET`-routes of het record bestaat.
3. Wijzig het record met `PUT`.
4. Controleer met `GET` of de wijzigingen zijn opgeslagen.
5. Herstart de applicatie en controleer of de gegevens bewaard blijven.
6. Verwijder het record met `DELETE`.
7. Controleer of `GET` op hetzelfde ID `404 Not Found` oplevert.

De volledige bewijsreeks, inclusief persistentie na een herstart, moet nog worden vastgelegd.

De schrijfroutes vereisen inmiddels een Admin-account. Omdat de loginfunctionaliteit nog ontbreekt, kunnen deze routes momenteel niet opnieuw als Admin via Swagger worden getest.

### Geautomatiseerde tests

Voor de verdere ontwikkeling wordt een afzonderlijk xUnit-testproject toegevoegd aan de solution.

De geplande tests controleren onder andere:

- CRUD-functionaliteit voor projecten en blogposts.
- Correcte HTTP-statuscodes.
- Verzoeken met niet-bestaande ID's.
- Authenticatie en autorisatie.
- Toegestane en geweigerde schrijfacties.
- Randgevallen en foutafhandeling.

Na het toevoegen van het testproject kunnen de tests vanuit de hoofdmap van de repository worden uitgevoerd met:

```powershell
dotnet test backend/PortfolioWPFWApi.sln
```

**Huidige status:** er is nog geen xUnit-testproject aanwezig. Het commando levert daarom nog geen bewijs van geautomatiseerde CRUD- of autorisatietests.

Testcoverage wordt bij de verdere uitwerking gemeten en onderbouwd.

---

## Verdere ontwikkeling

De volgende uitbreidingen zijn gepland:

- Registratie-, login- en logoutfunctionaliteit.
- Identity-rollen aanmaken en toekennen.
- Afstemming op de autorisatievereisten van opdracht 4.
- Inputvalidatie voor API-verzoeken.
- CSRF-bescherming bij cookie-authenticatie.
- Blogreacties met moderatiemogelijkheden.
- Een afzonderlijk xUnit-testproject.
- Geautomatiseerde tests en testcoverage.
- Koppeling met de frontend.
- Pagination en gerichte DTO-projecties wanneer nodig.

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
- [Role-based authorization](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-10.0)
- [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-10.0)
- [xUnit en dotnet test](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-csharp-with-xunit)

---

**PortfolioWPFWApi**
HBO-ICT Software Engineering · De Haagse Hogeschool
Webprogrammeren Frameworks (WPFW)
