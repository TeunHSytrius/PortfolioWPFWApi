
# PortfolioWPFWApi

![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-ASP.NET_Core-239120?style=flat-square&logo=csharp&logoColor=white)
![EF Core](https://img.shields.io/badge/ORM-Entity_Framework_Core-512BD4?style=flat-square)
![SQL Server](https://img.shields.io/badge/Database-SQL_Server-CC2927?style=flat-square)
![Rider](https://img.shields.io/badge/IDE-JetBrains_Rider-000000?style=flat-square&logo=rider&logoColor=white)
![Education](https://img.shields.io/badge/Project-HBO--ICT-blue?style=flat-square)

**Nederlands** | [English](#english)

---

# Nederlands

## Over het project

PortfolioWPFWApi is de backend van mijn persoonlijke portfoliosite.

Ik maak dit project voor WPFW Opdracht 3 van de opleiding HBO-ICT Software Engineering aan De Haagse Hogeschool.

Het doel is om een REST API te bouwen waarmee ik mijn projecten en blogposts kan beheren. De gegevens worden opgeslagen in Microsoft SQL Server en de communicatie met de database verloopt via Entity Framework Core.

Tijdens deze opdracht leer ik hoe ik een backend opbouw, hoe verschillende onderdelen samenwerken en hoe ik verantwoordelijkheden in mijn code kan scheiden.

De repository bevat de backend. Mijn portfoliosite zelf staat in een aparte repository:

[Mijn portfoliosite](https://github.com/TeunHSytrius/TeunHSytrius.github.io)

## Technologieën

Voor dit project gebruik ik de volgende technologieën:

| Technologie | Gebruik |
|---|---|
| C# | Programmeertaal |
| .NET 10 | Framework |
| ASP.NET Core Web API | Backend en REST API |
| Entity Framework Core | Communicatie met de database |
| Microsoft SQL Server | Opslaan van gegevens |
| Swagger UI | Testen van API-endpoints |
| JetBrains Rider | Ontwikkelomgeving |
| Git en GitHub | Versiebeheer |
| Trello | Planning en werkzaamheden |

## Functionaliteiten

De API wordt ontwikkeld voor twee onderdelen van mijn portfolio.

### Projecten

Met de API wil ik de volgende acties ondersteunen:

| Methode | Endpoint | Functie |
|---|---|---|
| GET | `/api/projects` | Alle projecten ophalen |
| GET | `/api/projects/{id}` | Eén project ophalen |
| POST | `/api/projects` | Nieuw project toevoegen |
| PUT | `/api/projects/{id}` | Project aanpassen |
| DELETE | `/api/projects/{id}` | Project verwijderen |

Een project bevat onder andere een titel, beschrijving, categorie, GitHub-link en datum.

### Blogposts

| Methode | Endpoint | Functie |
|---|---|---|
| GET | `/api/blogposts` | Alle blogposts ophalen |
| GET | `/api/blogposts/{id}` | Eén blogpost ophalen |
| POST | `/api/blogposts` | Nieuwe blogpost toevoegen |
| PUT | `/api/blogposts/{id}` | Blogpost aanpassen |
| DELETE | `/api/blogposts/{id}` | Blogpost verwijderen |

Een blogpost bevat een titel, inhoud en publicatiedatum.

De routes hierboven zijn de beoogde API-routes. De daadwerkelijke werking wordt tijdens de ontwikkeling in Swagger getest.

## Projectstructuur

Ik gebruik een structuur met verschillende lagen. Ik heb hiervoor gekozen omdat ik de verantwoordelijkheden in mijn applicatie duidelijk wil scheiden.

De structuur die ik wil gebruiken:

```text
PortfolioWPFWApi/
│
├── .gitignore
├── README.md
├── PortfolioWPFWApi.sln
│
├── docs/
│   └── AI_GEBRUIK.md
│
└── PortfolioWPFWApi/
    │
    ├── Controllers/
    │   ├── ProjectsController.cs
    │   └── BlogpostsController.cs
    │
    ├── Services/
    │   ├── ProjectService.cs
    │   └── BlogpostService.cs
    │
    ├── Data/
    │   └── ApplicationDbContext.cs
    │
    ├── Models/
    │   ├── Project.cs
    │   └── Blogpost.cs
    │
    ├── DTOs/
    │   ├── Projects/
    │   │   ├── CreateProjectDto.cs
    │   │   ├── UpdateProjectDto.cs
    │   │   └── ProjectDto.cs
    │   │
    │   └── Blogposts/
    │       ├── CreateBlogpostDto.cs
    │       ├── UpdateBlogpostDto.cs
    │       └── BlogpostDto.cs
    │
    ├── Migrations/
    ├── Properties/
    │
    ├── Program.cs
    ├── appsettings.json
    └── PortfolioWPFWApi.csproj
```

Dit is mijn geplande structuur. Niet alle bestanden hoeven vanaf het begin aanwezig te zijn.

### Waarom deze structuur?

Ik wil voorkomen dat alle functionaliteit in de controllers terechtkomt.

De controllers verwerken HTTP-verzoeken en bepalen welke HTTP-resultaten teruggegeven worden.

De services bevatten de applicatielogica. Via de DbContext wordt de database benaderd.

Daarnaast gebruik ik DTO's. Hiermee bepaal ik welke gegevens de API ontvangt en terugstuurt, zonder rechtstreeks database-entiteiten te gebruiken.

Ik vind deze indeling overzichtelijk en denk dat het hierdoor makkelijker wordt om onderdelen aan te passen.

## Installatie

### Benodigdheden

Voor het draaien van deze API heb je nodig:

- .NET 10 SDK
- Microsoft SQL Server of SQL Server Express
- JetBrains Rider of Visual Studio
- Git

### 1. Repository clonen

```bash
git clone https://github.com/TeunHSytrius/PortfolioWPFWApi.git
cd PortfolioWPFWApi
```

### 2. Dependencies herstellen

```bash
dotnet restore
```

Dit installeert de NuGet-packages die voor het project nodig zijn.

### 3. Database configureren

De applicatie gebruikt Microsoft SQL Server.

Voor lokale gevoelige configuratie gebruik ik .NET User Secrets, zodat wachtwoorden of andere geheime gegevens niet in GitHub terechtkomen.

Ga naar de projectmap:

```bash
cd PortfolioWPFWApi
```

Als User Secrets nog niet zijn ingesteld:

```bash
dotnet user-secrets init
```

Een voorbeeld van een connectionstring voor SQL Server Express met Windows-authenticatie:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" 'Server=.\SQLEXPRESS;Database=PortfolioWPFW;Trusted_Connection=True;TrustServerCertificate=True;'
```

De exacte servernaam hangt af van je eigen SQL Server-installatie.

De database wordt gekoppeld via Entity Framework Core en een eigen DbContext.

**Database aanmaken:** de definitieve instructies voor EF Core-migraties of `EnsureCreated` worden toegevoegd zodra dit onderdeel is ingericht.

### 4. Applicatie starten

Vanuit de projectmap:

```bash
dotnet run
```

Na het starten toont de terminal het lokale adres waarop de API draait.

### 5. API testen

Voor het testen van de endpoints gebruik ik Swagger UI via het NuGet-pakket `Swashbuckle.AspNetCore`.

Wanneer Swagger UI geconfigureerd en ingeschakeld is, is deze bereikbaar via:

```text
https://localhost:<poort>/swagger
```

Hiermee kan ik de endpoints uitvoeren en controleren of de juiste gegevens en HTTP-statuscodes worden teruggegeven.

## Mijn werkwijze

Ik probeer tijdens de ontwikkeling in kleine stappen te werken. Hierdoor kan ik beter bijhouden wat ik aan het doen ben en raak ik minder snel het overzicht kwijt.

Ik gebruik hiervoor Trello en GitHub.

### Planning met Trello

Ik heb mijn WPFW-opdracht opgedeeld in kleinere taken. Hiervoor heb ik ChatGPT gebruikt om een planning te maken op basis van de opdrachtomschrijving en rubric.

De tickets bevatten werkzaamheden, geplande datums en onderdelen die ik moet controleren.

Ik gebruik mijn bestaande Trello-bord en werk met:

- **ToDo:** taken waar ik nog aan moet beginnen.
- **Doing:** taken waar ik mee bezig ben.
- **Done:** taken die ik heb afgerond.

De planning kan veranderen wanneer een onderdeel langer duurt dan verwacht.

### Git en commits

Ik gebruik Git om mijn wijzigingen bij te houden.

Mijn bedoeling is om commits te maken wanneer ik een onderdeel heb afgerond of een duidelijke wijziging heb aangebracht.

Ik gebruik hierbij de volgende commitbenamingen:

| Type | Betekenis |
|---|---|
| `feat:` | Nieuwe functionaliteit |
| `fix:` | Een fout opgelost |
| `refactor:` | Code verbeterd |
| `docs:` | Documentatie |
| `chore:` | Configuratie of onderhoud |
| `test:` | Tests toegevoegd of aangepast |

Een voorbeeld:

```bash
git status
git add .
git commit -m "feat: add projects controller"
git push origin master
```

Ik controleer welke bestanden ik commit en probeer mijn wijzigingen duidelijk te omschrijven.

## Veiligheid en configuratie

Bij het ontwikkelen wil ik voorkomen dat gevoelige gegevens in mijn repository terechtkomen.

Daarom gebruik ik een `.gitignore` voor lokale instellingen, tijdelijke bestanden en andere bestanden die niet op GitHub hoeven te staan.

Voor databasewachtwoorden en andere geheimen gebruik ik lokale configuratie zoals .NET User Secrets.

Ook houd ik rekening met het verschil tussen configuratiegeheimen en wachtwoorden van gebruikers. Als ik later authenticatie toevoeg, moeten gebruikerswachtwoorden veilig worden gehasht.

## AI-gebruik en verantwoording

Voor deze opdracht geldt **AIAS-niveau 2 (AI Planning)**.

Ik gebruik ChatGPT als hulpmiddel bij het plannen, voorbereiden en begrijpen van bepaalde onderwerpen.

Tot nu toe heb ik ChatGPT onder andere gebruikt voor:

- Het maken en aanpassen van mijn planning.
- Het aanmaken van tickets in Trello.
- Uitleg over de projectstructuur.
- Hulp bij mijn `.gitignore` en Git-commando's.
- Het bekijken en controleren van mijn GitHub-repository.
- Uitleg over configuratie en het opslaan van geheime gegevens.
- Het voorbereiden van documentatie, waaronder deze README.

ChatGPT heeft ook geholpen bij het opstellen van de eerste teksten voor deze documentatie.

De technische uitwerking, keuzes, onderbouwing en het eindresultaat moeten van mijzelf zijn. AI-codeaanvulling gebruik ik niet, omdat dit volgens de opdracht niet is toegestaan.

Ik houd mijn AI-gebruik apart bij, zodat duidelijk is welke hulp ik heb gekregen en wat ik zelf heb gedaan of gecontroleerd.

**[Lees mijn uitgebreide AI-verantwoording](docs/AI_GEBRUIK.md)**

Mijn procesverantwoording, feedback en werkelijk bestede uren worden daarnaast in Boulder bijgehouden.

## Bronnen

- [Microsoft Learn – ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [Microsoft Learn – Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Microsoft Learn – C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [Microsoft Learn – .NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets)
- Lesmateriaal WPFW – De Haagse Hogeschool

## Auteur

**Teun Hanssen**

HBO-ICT – Software Engineering  
De Haagse Hogeschool

[GitHub-profiel](https://github.com/TeunHSytrius)

---

# English

## About the project

PortfolioWPFWApi is the backend for my personal portfolio website.

I am developing this project for WPFW Assignment 3 as part of the HBO-ICT Software Engineering programme at The Hague University of Applied Sciences.

The goal is to build a REST API for managing my projects and blog posts.

The application uses ASP.NET Core Web API, Entity Framework Core and Microsoft SQL Server.

During this assignment, I focus on learning how to structure a backend, separate responsibilities and work with a relational database.

The frontend of my portfolio is maintained in a separate repository:

[Portfolio website](https://github.com/TeunHSytrius/TeunHSytrius.github.io)

## Technologies

| Technology | Purpose |
|---|---|
| C# | Programming language |
| .NET 10 | Framework |
| ASP.NET Core Web API | Backend and REST API |
| Entity Framework Core | Database communication |
| Microsoft SQL Server | Data storage |
| Swagger UI | API testing |
| JetBrains Rider | Development environment |
| Git and GitHub | Version control |
| Trello | Planning and task management |

## Features

The API is designed to manage two parts of my portfolio.

### Projects

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/projects` | Retrieve all projects |
| GET | `/api/projects/{id}` | Retrieve a project by ID |
| POST | `/api/projects` | Create a project |
| PUT | `/api/projects/{id}` | Update a project |
| DELETE | `/api/projects/{id}` | Delete a project |

### Blog posts

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/blogposts` | Retrieve all blog posts |
| GET | `/api/blogposts/{id}` | Retrieve a blog post by ID |
| POST | `/api/blogposts` | Create a blog post |
| PUT | `/api/blogposts/{id}` | Update a blog post |
| DELETE | `/api/blogposts/{id}` | Delete a blog post |

These are the planned API endpoints. Their implementation and behaviour are verified during development.

## Project structure

I use a layered structure to separate responsibilities within the application.

| Folder | Responsibility |
|---|---|
| `Controllers/` | Handle HTTP requests and responses |
| `Services/` | Contain application logic |
| `Data/` | Database configuration and DbContext |
| `Models/` | Database entities |
| `DTOs/` | API input and output models |
| `Migrations/` | EF Core database migrations, if used |
| `docs/` | Additional documentation |

I chose this structure because I want to keep my controllers focused on HTTP handling instead of putting all the logic in one place.

Services handle application logic, while Entity Framework Core is used for database access.

DTOs are used to control which data is received and returned by the API.

The folder structure is implemented step by step during development.

## Installation

### Requirements

- .NET 10 SDK
- Microsoft SQL Server or SQL Server Express
- JetBrains Rider or Visual Studio
- Git

### 1. Clone the repository

```bash
git clone https://github.com/TeunHSytrius/PortfolioWPFWApi.git
cd PortfolioWPFWApi
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Configure the database

The project uses Microsoft SQL Server.

For local development, sensitive configuration is stored using .NET User Secrets.

Navigate to the project directory:

```bash
cd PortfolioWPFWApi
```

If User Secrets have not been configured:

```bash
dotnet user-secrets init
```

Example for SQL Server Express using Windows authentication:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" 'Server=.\SQLEXPRESS;Database=PortfolioWPFW;Trusted_Connection=True;TrustServerCertificate=True;'
```

The server name may need to be changed depending on your local SQL Server installation.

Database creation instructions using EF Core migrations or `EnsureCreated` will be added once the database setup is implemented.

### 4. Run the application

```bash
dotnet run
```

### 5. Test the API

Once Swagger UI is configured and enabled, the API can be tested at:

```text
https://localhost:<port>/swagger
```

Swagger is used to test the CRUD endpoints and their HTTP responses.

## Development workflow

I work in small steps so I can keep track of my progress and understand what I am building.

I use Trello to organise my tasks and GitHub for version control.

My workflow consists of:

1. Reading the requirements before starting.
2. Planning the work in Trello.
3. Developing one part at a time.
4. Testing the functionality.
5. Fixing problems and improving the code.
6. Committing completed changes.
7. Documenting important decisions and feedback.

### Git workflow

I use descriptive commit messages.

| Type | Purpose |
|---|---|
| `feat:` | New functionality |
| `fix:` | Bug fix |
| `refactor:` | Code improvements |
| `docs:` | Documentation |
| `chore:` | Maintenance or configuration |
| `test:` | Tests |

Example:

```bash
git status
git add .
git commit -m "feat: add projects controller"
git push origin master
```

I review my changes before committing them.

## Security and configuration

I use a `.gitignore` to exclude unnecessary local files and sensitive configuration from version control.

Database credentials and other secrets should not be committed to GitHub.

For local development, I use .NET User Secrets. User authentication may be added as a separate feature later.

## AI usage and documentation

This assignment follows **AIAS Level 2 (AI Planning)**.

I use ChatGPT as a supporting tool for planning, preparation and understanding technical concepts.

So far, I have used ChatGPT to help with:

- Creating and adjusting my project planning.
- Creating Trello tasks and checklists.
- Understanding possible project structures.
- Preparing my `.gitignore` and understanding Git commands.
- Reviewing my GitHub repository.
- Learning about local configuration and secrets.
- Preparing documentation, including this README.

ChatGPT also helped prepare the initial drafts of the documentation.

The technical implementation, decisions, justification and final result remain my own responsibility. AI code completion is not used because it is not allowed for this assignment.

I keep a separate document explaining how I have used AI and what I have done or verified myself.

**[Read my AI usage documentation (Dutch)](docs/AI_GEBRUIK.md)**

Additional process documentation, feedback and actual study hours are recorded in Boulder.

## References

- [Microsoft Learn – ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [Microsoft Learn – Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Microsoft Learn – C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [Microsoft Learn – .NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets)
- WPFW course materials – The Hague University of Applied Sciences

## Author

**Teun Hanssen**

HBO-ICT – Software Engineering  
The Hague University of Applied Sciences

[GitHub profile](https://github.com/TeunHSytrius)
