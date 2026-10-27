
# PortfolioWPFWApi

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-ASP.NET_Core-239120?logo=csharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/Database-SQL_Server-CC2927)
![EF Core](https://img.shields.io/badge/ORM-Entity_Framework_Core-purple)
![License](https://img.shields.io/badge/License-Educational-blue)

**Talen / Languages:** [Nederlands](#nederlands) | [English](#english)

---

# Nederlands

## Over het project

PortfolioWPFWApi is een backend voor mijn persoonlijke portfoliosite. Dit project maak ik voor het vak WPFW binnen de opleiding HBO-ICT Software Engineering aan De Haagse Hogeschool.

Het doel is om een REST API te ontwikkelen waarmee ik projecten en blogposts kan beheren. Hiervoor gebruik ik ASP.NET Core, Entity Framework Core en Microsoft SQL Server.

Tijdens deze opdracht richt ik me vooral op het bouwen van een duidelijke backend, het scheiden van verantwoordelijkheden en het werken met een database.

## Technologieën

| Technologie | Gebruik |
|---|---|
| C# | Programmeertaal |
| .NET 10 | Framework |
| ASP.NET Core Web API | REST API |
| Entity Framework Core | Databasecommunicatie |
| Microsoft SQL Server | Database |
| Swagger UI | API testen en documenteren |
| JetBrains Rider | Ontwikkelomgeving |
| Git & GitHub | Versiebeheer |

## Functionaliteiten

De API is bedoeld voor het beheren van twee onderdelen van mijn portfolio:

**Projecten**
- Alle projecten ophalen.
- Eén project ophalen op basis van ID.
- Een nieuw project toevoegen.
- Een bestaand project wijzigen.
- Een project verwijderen.

**Blogposts**
- Alle blogposts ophalen.
- Eén blogpost ophalen op basis van ID.
- Een nieuwe blogpost toevoegen.
- Een bestaande blogpost wijzigen.
- Een blogpost verwijderen.

De API gebruikt DTO's om gegevens uit te wisselen en Entity Framework Core om gegevens op te slaan in SQL Server.

## Projectstructuur

Ik gebruik een structuur met verschillende lagen. Hierdoor blijft de code overzichtelijk en heeft ieder onderdeel een eigen verantwoordelijkheid.

```text
PortfolioWPFWApi/
│
├── Controllers/     # Verwerken HTTP-verzoeken
├── Services/        # Applicatielogica
├── Data/            # Database en DbContext
├── Models/          # Database-entiteiten
├── DTOs/            # Gegevens voor API-verzoeken
├── Migrations/      # Databasewijzigingen
│
├── Program.cs       # Configuratie van de API
└── appsettings.json # Algemene instellingen
```

Een controller ontvangt het HTTP-verzoek en roept de juiste service aan. De service verwerkt de logica en gebruikt Entity Framework Core voor de database.

Zo voorkom ik dat alle code in één bestand of controller terechtkomt.

## Installatie en gebruik

### Benodigdheden

Voor het uitvoeren van dit project heb je nodig:

- .NET 10 SDK
- Microsoft SQL Server
- Een IDE zoals JetBrains Rider of Visual Studio
- Git

### Repository clonen

```bash
git clone https://github.com/TeunHSytrius/PortfolioWPFWApi.git
cd PortfolioWPFWApi
```

### Dependencies installeren

```bash
dotnet restore
```

### Database configureren

De connectionstring wordt lokaal ingesteld. Gevoelige gegevens, zoals wachtwoorden, worden niet opgeslagen in de GitHub-repository.

Voor lokale ontwikkeling gebruik ik hiervoor .NET User Secrets.

Ga naar de projectmap:

```bash
cd PortfolioWPFWApi
```

Stel de connectionstring in, bijvoorbeeld voor SQL Server Express met Windows-authenticatie:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost\SQLEXPRESS;Database=PortfolioWPFW;Trusted_Connection=True;TrustServerCertificate=True;"
```

De database wordt via Entity Framework Core gekoppeld. De gebruikte methode voor databaseaanmaak en eventuele migraties wordt in de projectconfiguratie vastgelegd.

### Applicatie starten

Voer vanuit de projectmap uit:

```bash
dotnet run
```

De API kan vervolgens getest worden via Swagger UI op het lokale adres van de applicatie, met het pad:

```text
/swagger
```

Dit geldt wanneer Swagger UI is geconfigureerd en ingeschakeld voor de ontwikkelomgeving.

## Mijn werkwijze

Ik werk tijdens de ontwikkeling in kleine stappen. Hierdoor kan ik makkelijker controleren of alles werkt en problemen sneller terugvinden.

Mijn aanpak:

1. **Voorbereiden:** eerst bekijken wat ik moet maken en welke eisen daarbij horen.
2. **Uitwerken:** één onderdeel tegelijk ontwikkelen, zodat het overzichtelijk blijft.
3. **Testen:** controleren of de functionaliteit doet wat ik verwacht.
4. **Verbeteren:** fouten oplossen en de code waar nodig overzichtelijker maken.
5. **Committen:** afgeronde wijzigingen vastleggen in Git.
6. **Documenteren:** belangrijke keuzes, bronnen en uitgevoerde tests bijhouden.

Ik probeer niet alles tegelijk te bouwen. Eerst zorg ik dat de basis goed werkt, daarna breid ik deze verder uit.

### Git-werkwijze

Ik gebruik GitHub om mijn wijzigingen bij te houden. Ik maak commits wanneer ik een onderdeel heb afgerond of een duidelijke wijziging heb gemaakt.
Voor mijn commits gebruik ik zoveel mogelijk vaste namen:

| Type | Betekenis |
|---|---|
| `feat:` | Nieuwe functionaliteit |
| `fix:` | Fout opgelost |
| `refactor:` | Code verbeterd zonder nieuwe functionaliteit |
| `docs:` | Documentatie aangepast |
| `chore:` | Onderhoud of configuratie |
| `test:` | Tests toegevoegd of gewijzigd |

Ik controleer mijn wijzigingen voordat ik commit en probeer commits klein en duidelijk te houden.

## AI-verantwoording

Tijdens dit project gebruik ik AI als hulpmiddel bij het leren en plannen.
Ik heb ChatGPT gebruikt voor het opzetten van een planning in Trello, het structureren van mijn werkwijze en het voorbereiden van documentatie, waaronder deze README.
AI kan mij helpen met uitleg en ideeën wanneer ik iets niet begrijp. De technische keuzes, implementatie en controle van mijn werk voer ik zelf uit.
Ik houd mij hierbij aan AIAS-niveau 2 van de opdracht. AI-codeaanvulling gebruik ik niet. Eventueel AI-gebruik wordt per bewijsstuk verder verantwoord in Boulder, inclusief het doel en mijn eigen controle.

## Bronnen

- [Microsoft Learn – ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [Microsoft Learn – Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Microsoft Learn – C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- Lesmateriaal WPFW – De Haagse Hogeschool

## Auteur

**Teun Hanssen**  
HBO-ICT – Software Engineering  
De Haagse Hogeschool

GitHub: [TeunHSytrius](https://github.com/TeunHSytrius)

---

# English

## About the project

PortfolioWPFWApi is a backend for my personal portfolio website. I am developing this project as part of the WPFW course in the HBO-ICT Software Engineering programme at The Hague University of Applied Sciences.

The goal is to build a REST API for managing projects and blog posts using ASP.NET Core, Entity Framework Core and Microsoft SQL Server.

The main focus is on building a structured backend, separating responsibilities and working with a relational database.

## Technologies

| Technology | Purpose |
|---|---|
| C# | Programming language |
| .NET 10 | Framework |
| ASP.NET Core Web API | REST API |
| Entity Framework Core | Database access |
| Microsoft SQL Server | Database |
| Swagger UI | API documentation and testing |
| JetBrains Rider | Development environment |
| Git & GitHub | Version control |

## Features

The API is designed to manage two parts of my portfolio.

**Projects**
- Retrieve all projects.
- Retrieve a project by ID.
- Create a project.
- Update an existing project.
- Delete a project.

**Blog posts**
- Retrieve all blog posts.
- Retrieve a blog post by ID.
- Create a blog post.
- Update an existing blog post.
- Delete a blog post.

DTOs are used to exchange data through the API. Entity Framework Core handles database operations.

## Architecture

I use a layered structure to keep the code organised and responsibilities separated.

```text
PortfolioWPFWApi/
│
├── Controllers/     # Handle HTTP requests
├── Services/        # Application logic
├── Data/            # Database and DbContext
├── Models/          # Database entities
├── DTOs/            # API data transfer objects
├── Migrations/      # Database migrations
│
├── Program.cs       # Application configuration
└── appsettings.json # General settings
```

Controllers receive HTTP requests and call the appropriate services. Services handle application logic and use Entity Framework Core for database access.

This makes the application easier to understand and maintain.

## Installation

### Requirements

- .NET 10 SDK
- Microsoft SQL Server
- JetBrains Rider or Visual Studio
- Git

### Clone the repository

```bash
git clone https://github.com/TeunHSytrius/PortfolioWPFWApi.git
cd PortfolioWPFWApi
```

### Restore dependencies

```bash
dotnet restore
```

### Database configuration

Database credentials and other sensitive settings are kept outside the GitHub repository.

For local development, .NET User Secrets are used to store the connection string.

Navigate to the project directory:

```bash
cd PortfolioWPFWApi
```

Example connection string for SQL Server Express with Windows authentication:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost\SQLEXPRESS;Database=PortfolioWPFW;Trusted_Connection=True;TrustServerCertificate=True;"
```

Database creation and migrations are handled through the chosen Entity Framework Core configuration.

### Run the application

From the project directory:

```bash
dotnet run
```

When Swagger UI is configured and enabled for development, the API can be tested at:

```text
/swagger
```

## Development workflow

I work in small steps to keep the development process clear and manageable.

My approach consists of:

1. **Prepare:** understand the requirements before starting.
2. **Develop:** implement one feature at a time.
3. **Test:** verify that the functionality works as expected.
4. **Improve:** fix errors and improve code structure.
5. **Commit:** save completed changes using Git.
6. **Document:** keep track of important decisions, sources and tests.

I focus on getting the basic functionality working before adding extra features.

### Git workflow

I use GitHub to track changes throughout development.

I follow simple commit naming conventions:

| Type | Purpose |
|---|---|
| `feat:` | New feature |
| `fix:` | Bug fix |
| `refactor:` | Code improvement |
| `docs:` | Documentation |
| `chore:` | Maintenance or configuration |
| `test:` | Tests |


I review my changes before committing and try to keep commits small and meaningful.

## AI usage

During this project, I use AI as a supporting tool for learning and planning.

I have used ChatGPT to help structure my Trello planning, organise my workflow and prepare documentation, including this README.

AI may help me understand concepts and explore ideas. The technical decisions, implementation and verification of my work remain my own responsibility.

I follow the assignment's AIAS Level 2 guidelines and do not use AI code completion. Any additional AI usage is documented per evidence item in Boulder, including its purpose and my own verification.

## References

- [Microsoft Learn – ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [Microsoft Learn – Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Microsoft Learn – C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- WPFW course materials – The Hague University of Applied Sciences

## Author

**Teun Hanssen**  
HBO-ICT – Software Engineering  
The Hague University of Applied Sciences

GitHub: [TeunHSytrius](https://github.com/TeunHSytrius)
