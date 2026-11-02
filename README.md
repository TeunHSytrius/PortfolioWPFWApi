# PortfolioWPFWApi

PortfolioWPFWApi is een REST API voor mijn persoonlijke portfoliowebsite. Het project is ontwikkeld voor **Webprogrammeren Frameworks (WPFW)** binnen de opleiding HBO-ICT Software Engineering aan De Haagse Hogeschool.

De API is bedoeld om projecten en blogposts te beheren via HTTP-verzoeken. Hiervoor gebruik ik ASP.NET Core Web API met een gelaagde architectuur.

## Technologieën

| Technologie            | Toepassing                 |
| ---------------------- | -------------------------- |
| C#                     | Programmeertaal            |
| .NET 10 / ASP.NET Core | Web API-framework          |
| Entity Framework Core  | Databasetoegang            |
| Microsoft SQL Server   | Database                   |
| Swagger UI             | API-documentatie en testen |
| JetBrains Rider        | Ontwikkelomgeving          |
| Git en GitHub          | Versiebeheer               |

## Functionaliteiten

De API is ontworpen voor het beheren van:

- **Projecten:** titel, beschrijving, categorie, GitHub-link en datum.
- **Blogposts:** titel, inhoud en publicatiedatum.

Beide onderdelen gebruiken CRUD-functionaliteit via REST-endpoints:

| Methode | Functie                      |
| ------- | ---------------------------- |
| GET     | Gegevens ophalen             |
| POST    | Nieuwe gegevens toevoegen    |
| PUT     | Bestaande gegevens aanpassen |
| DELETE  | Gegevens verwijderen         |

De API-routes zijn `/api/projects` en `/api/blogposts`.

## Projectstructuur

```text
PortfolioWPFWApi/
├── backend/
│   ├── PortfolioWPFWApi.sln
│   └── PortfolioWPFWApi/
│       ├── Controllers/
│       ├── Services/
│       ├── Models/
│       ├── DTOs/
│       ├── Data/
│       ├── Program.cs
│       └── PortfolioWPFWApi.csproj
├── frontend/
├── Docs/
└── README.md
```

## Architectuur

De backend gebruikt een **gelaagde architectuur**, waarbij ieder onderdeel een eigen verantwoordelijkheid heeft:

- **Controllers:** verwerken HTTP-verzoeken en geven HTTP-antwoorden terug.
- **Services:** bevatten de applicatielogica.
- **Models:** beschrijven de database-entiteiten.
- **DTOs:** bepalen welke gegevens de API ontvangt en terugstuurt.
- **Data:** verzorgt de databasetoegang via Entity Framework Core.

Ik heb voor deze architectuur gekozen om verantwoordelijkheden gescheiden te houden. Hierdoor blijft de code overzichtelijker, onderhoudbaar en eenvoudiger te testen.

De map `frontend/` is bedoeld voor de React-frontend van opdracht 5.

## Installatie en starten

### Benodigdheden

- .NET 10 SDK
- Microsoft SQL Server voor databasefunctionaliteit
- JetBrains Rider of Visual Studio

### Repository clonen

```bash
git clone https://github.com/TeunHSytrius/PortfolioWPFWApi.git
cd PortfolioWPFWApi/backend/PortfolioWPFWApi
```

### API starten

```bash
dotnet restore
dotnet run --launch-profile http
```

Swagger UI is in de Development-omgeving bereikbaar via:

http://localhost:5167/swagger

Voor databasefunctionaliteit moet een lokale SQL Server-verbinding worden geconfigureerd.

## Links

- [Mijn portfoliowebsite](https://github.com/TeunHSytrius/TeunHSytrius.github.io)
- [Trello – Projectplanning](https://trello.com/b/kMkjs15F/school-todo)
