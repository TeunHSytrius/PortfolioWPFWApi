
# AI-gebruik en werkwijze – PortfolioWPFWApi

**Naam:** Teun Hanssen  
**Opleiding:** HBO-ICT – Software Engineering  
**Semester:** 3  
**Vak:** WPFW  
**Opdracht:** 3 – ASP.NET Core Web API en EF Core  
**AI-tool:** ChatGPT (OpenAI)  
**AIAS-niveau:** 2 – AI Planning

---

## 1. Waarom dit document?

In dit document houd ik bij hoe ik ChatGPT gebruik tijdens mijn opdracht. Ik wil duidelijk maken waarvoor ik AI gebruik, wat ik zelf doe en welke keuzes ik maak.

Ik gebruik ChatGPT regelmatig om dingen beter te begrijpen, een planning te maken of om te kijken of ik iets goed aanpak.

Mijn bedoeling is niet dat ChatGPT mijn hele opdracht maakt. Ik wil juist zelf leren hoe ik een backend bouw en waarom bepaalde onderdelen nodig zijn.

Ook wil ik eerlijk zijn over mijn AI-gebruik. Als ik hulp heb gekregen, wil ik dat gewoon vermelden.

Dit document vul ik tijdens de opdracht verder aan.

## 2. Wat mag volgens de opdracht?

Voor WPFW Opdracht 3 geldt AIAS-niveau 2 (AI Planning).

Volgens de opdracht mag ik AI gebruiken voor:

- Planning maken.
- Brainstormen en ideeën bedenken.
- Eerste onderzoek.
- Uitleg krijgen over onderwerpen.
- Structuur en aanpak voorbereiden.

De technische uitwerking, keuzes, onderbouwing en het eindresultaat moeten van mijzelf zijn.

Ook moet ik per bewijsstuk aangeven:

1. Welke AI-tool ik heb gebruikt.
2. Waarvoor ik deze heb gebruikt.
3. Wat ik zelf heb gecontroleerd of aangepast.

AI-codeaanvulling, zoals GitHub Copilot, is niet toegestaan. Dit moet ik dus uitzetten in mijn ontwikkelomgeving.

Standaard gegenereerde projectcode vanuit een IDE is wel toegestaan, zolang ik begrijp wat die code doet.

## 3. Hoe heb ik ChatGPT tot nu toe gebruikt?

### 3.1 Planning maken

Ik merkte dat ik moeite had om overzicht te houden over mijn opdrachten. Ik moest namelijk voor zowel Databases als WPFW nog verschillende onderdelen maken.

Daarom heb ik ChatGPT gevraagd om een planning te maken.

Ik heb hiervoor de opdrachtomschrijvingen en rubrics gegeven. ChatGPT heeft deze gebruikt om de werkzaamheden over verschillende dagen te verdelen.

Daarna heb ik aangegeven dat ik voor Databases al een deel had gemaakt. De planning is daarop aangepast, zodat ik niet opnieuw hoefde te beginnen aan werk dat al gedaan was.

Ik wilde uiteindelijk niet alleen een planning in de chat, maar ook echte taken die ik kon bijhouden.

Daarom heb ik ChatGPT gevraagd om Trello te gebruiken.

ChatGPT heeft vervolgens op mijn bestaande Trello-bord taken aangemaakt voor beide opdrachten. Deze taken bevatten onder andere:

- Wat ik op een bepaalde dag moet doen.
- Hoeveel tijd ervoor gepland is.
- Welke onderdelen van de rubric erbij horen.
- Wanneer ik iets af wil hebben.
- Welke bewijsstukken ik nodig heb.

**Wat heb ik zelf gedaan?**

Ik heb aangegeven welke opdrachten ik moest maken, welk werk al klaar was en dat ik Trello wilde gebruiken.

ChatGPT heeft geholpen met het verdelen en aanmaken van de taken. De planning gebruik ik zelf om mijn werkzaamheden uit te voeren en mijn voortgang bij te houden.

De planning is een hulpmiddel. Als iets langer duurt of anders loopt, moet ik deze zelf aanpassen.

### 3.2 GitHub en Rider opzetten

Voor WPFW Opdracht 3 heb ik zelf een GitHub-repository aangemaakt:

[PortfolioWPFWApi](https://github.com/TeunHSytrius/PortfolioWPFWApi)

Ik gebruik JetBrains Rider als ontwikkelomgeving.

Ik heb hierin een ASP.NET Core Web API-project aangemaakt met .NET 10. Vervolgens heb ik het project gekoppeld aan GitHub en mijn eerste commit gemaakt.

Ik heb screenshots aan ChatGPT laten zien om mijn voortgang te laten zien.

ChatGPT heeft toen gekeken naar mijn setup en aangegeven welke onderdelen ik nog moest controleren.

Hierbij werd onder andere gekeken naar:

- De projectstructuur.
- De .NET-versie.
- Het gebruik van controllers.
- De GitHub-repository.
- De Git-commits.

**Wat heb ik zelf gedaan?**

Ik heb zelf de repository aangemaakt, het project opgezet in Rider en mijn eerste commit gepusht.

ChatGPT heeft dit niet voor mij gedaan, maar heeft gekeken naar wat ik had gemaakt en uitleg gegeven over mogelijke verbeteringen.

### 3.3 Gitignore maken

Tijdens het opzetten van mijn repository merkte ik dat er ook bestanden van Rider en gegenereerde bestanden op GitHub stonden.

Ik wist niet precies welke bestanden wel en niet in Git moesten staan.

Daarom heb ik ChatGPT gevraagd om een `.gitignore` te maken die past bij mijn project.

ChatGPT heeft een voorstel gemaakt voor .NET 10, ASP.NET Core, Rider en SQL Server.

Ik heb daarnaast gevraagd om uitleg bij de terminalcommando's, omdat ik wilde begrijpen wat ze doen.

Een voorbeeld hiervan is:

`git rm -r --cached .idea`

ChatGPT heeft uitgelegd dat hiermee de map uit Git wordt gehaald, maar lokaal blijft bestaan.

Later heb ik ChatGPT ook rechtstreeks mijn repository laten bekijken. Hierbij werd onder andere gezien dat mijn `.gitignore` aanwezig was, maar dat de `obj`-map nog in Git stond.

**Wat heb ik zelf gedaan?**

Ik heb ChatGPT gevraagd om de configuratie en uitleg. Ik heb de `.gitignore` aan mijn repository toegevoegd.

De controle van de repository is door ChatGPT uitgevoerd. Ik blijf zelf verantwoordelijk voor welke bestanden ik uiteindelijk commit.

### 3.4 Uitleg over wachtwoorden en configuratie

Ik heb ChatGPT gevraagd hoe ik later wachtwoorden en andere gevoelige gegevens kan opslaan.

Mijn eerste idee was om hiervoor een `.env`-bestand of een JSON-bestand te gebruiken.

ChatGPT heeft uitgelegd dat er een verschil is tussen databasewachtwoorden en wachtwoorden van gebruikers die inloggen.

Ook heeft ChatGPT uitgelegd wat .NET User Secrets zijn.

Hierdoor heb ik geleerd dat ik lokale configuratie en geheime gegevens niet zomaar in mijn GitHub-repository moet zetten.

Het loginsysteem is een mogelijke uitbreiding voor later. Voor deze opdracht wil ik eerst de verplichte API-functionaliteit maken.

### 3.5 README en documentatie

Ik heb ChatGPT gevraagd om een README voor mijn repository.

Hierbij wilde ik dat de README zowel Nederlands als Engels bevatte, met badges en een duidelijke uitleg over het project.

De eerste voorstellen waren niet helemaal wat ik wilde. Er werd bijvoorbeeld te veel geschreven over de huidige status van het project, terwijl ik vooral een algemene projectuitleg wilde.

Daarom heb ik gevraagd om de tekst aan te passen.

Ook wilde ik een duidelijkere AI-verantwoording. Eerst kwam deze in de README terecht, maar ik vond het overzichtelijker om hiervoor een apart Markdown-bestand te gebruiken.

ChatGPT heeft geholpen bij het maken van de conceptteksten en structuur van deze documentatie.

**Wat heb ik zelf gedaan?**

Ik heb aangegeven wat ik in de documenten wilde hebben en welke onderdelen ik anders wilde zien.

De teksten zijn met hulp van ChatGPT voorbereid. Ik moet zelf controleren of alles klopt en de uiteindelijke tekst aanpassen aan mijn eigen werk en ervaringen.

---

## 4. Welke keuzes heb ik gemaakt?

Hier houd ik mijn keuzes bij, zodat ik later kan uitleggen waarom ik iets op een bepaalde manier heb gedaan.

### 4.1 JetBrains Rider

Ik gebruik JetBrains Rider voor het ontwikkelen van mijn API.

Ik werk hiermee aan mijn C#-project en gebruik de ingebouwde terminal voor Git en .NET-commando's.

### 4.2 .NET 10

Ik gebruik .NET 10 omdat dit in de opdracht als technische eis staat.

Ik heb dit ingesteld bij het aanmaken van mijn project. ChatGPT heeft later in mijn repository gecontroleerd dat het project op `net10.0` staat.

### 4.3 Controllers

Ik wil mijn API opbouwen met controllers en een duidelijke scheiding tussen verschillende onderdelen.

ChatGPT heeft uitgelegd dat ik bijvoorbeeld controllers, services, models, DTO's en een datalaag kan gebruiken.

Dit lijkt mij een overzichtelijke manier om mijn project op te bouwen.

Ik moet tijdens het ontwikkelen nog goed leren welke verantwoordelijkheid ieder onderdeel heeft.

### 4.4 Entity Framework Core en SQL Server

Entity Framework Core en SQL Server zijn verplicht voor deze opdracht.

Ik wil deze gebruiken om projecten en blogposts op te slaan.

Het voordeel is dat ik met C#-objecten kan werken en de database via Entity Framework Core kan benaderen.

De precieze uitwerking en de keuze voor bijvoorbeeld migrations of EnsureCreated moet ik nog maken en uitleggen.

### 4.5 Git en Trello

Ik gebruik GitHub voor mijn versiebeheer en Trello voor het plannen van werkzaamheden.

Mijn bedoeling is om na afgeronde onderdelen een commit te maken.

Ik wil hiermee kunnen terugzien wat ik heb veranderd en hoe het project is opgebouwd.

---

## 5. Hoe wil ik AI verder gebruiken?

Tijdens de rest van de opdracht wil ik ChatGPT vooral blijven gebruiken als een soort hulpmiddel bij het leren.

Bijvoorbeeld wanneer ik:

- Niet begrijp wat een bepaald begrip betekent.
- Verschillen tussen twee technische mogelijkheden wil begrijpen.
- Wil weten wat een foutmelding betekent.
- Mijn eigen aanpak wil bespreken.
- Wil controleren of ik alle onderdelen uit de rubric heb meegenomen.
- Vragen wil voorbereiden voor een feedbackgesprek.

Ik wil hierbij eerst zelf proberen een oplossing te bedenken.

Als iets niet lukt, kan ik ChatGPT om uitleg vragen. Daarna wil ik zelf bepalen hoe ik verderga.

Ik moet wel rekening houden met AIAS-niveau 2. Dit betekent dat ik geen door AI uitgewerkte applicatiecode of onderbouwing als mijn eigen werk mag inleveren.

Ook zet ik AI-codeaanvulling uit tijdens het programmeren.

---

## 6. AI-gebruik per bewijsstuk

De opdracht vraagt om een verantwoording per bewijsstuk.

Daarom houd ik naast de algemene uitleg ook een overzicht bij.

| Onderdeel | Tool | Waarvoor gebruikt? | Eigen controle / aanpassing |
|---|---|---|---|
| Planning | ChatGPT | Planning gemaakt op basis van de opdracht en rubric. | Aangegeven wat al klaar was en de planning laten aanpassen. |
| Trello | ChatGPT | Tickets en checklists aangemaakt. | Het bestaande bord gekozen en de planning gebruiken voor mijn eigen werk. |
| Projectsetup | ChatGPT | Screenshots en projectstructuur besproken. | Project en repository zelf aangemaakt in Rider/GitHub. |
| Gitignore | ChatGPT | Voorstel voor .gitignore en uitleg over Git-commando's. | Bestand toegevoegd en repository opnieuw laten controleren. |
| Configuratie | ChatGPT | Uitleg over User Secrets, .env en wachtwoorden. | Nog toepassen waar nodig tijdens de ontwikkeling. |
| README | ChatGPT | Concept voor projectdocumentatie. | Inhoud en gewenste indeling aangegeven; definitieve controle en aanpassing nodig. |
| AI-verantwoording | ChatGPT | Concept voor dit document gemaakt op basis van eerdere gesprekken. | Feiten controleren, aanpassen in eigen woorden en tijdens de opdracht aanvullen. |

**Let op:** Dit overzicht gaat over mijn voorbereiding en het AI-gebruik dat tot nu toe besproken is.

Wanneer ik verder werk aan de controllers, services, DTO's en database, moet ik dit overzicht aanvullen met het daadwerkelijke gebruik.

Ik wil hierbij ook vastleggen welke onderdelen ik zonder AI heb gemaakt.

---

## 7. Eindverantwoordelijkheid

Ik gebruik ChatGPT om dingen beter te begrijpen en mijn werkzaamheden te organiseren.

Dit betekent niet dat alle voorstellen van ChatGPT automatisch correct zijn. Ik moet de antwoorden zelf controleren en vergelijken met mijn lesmateriaal, de rubric en mijn eigen project.

De code en technische uitwerking moeten van mijzelf komen.

Ik wil tijdens het feedbackgesprek kunnen uitleggen wat mijn code doet, waarom ik bepaalde keuzes heb gemaakt en wat ik tijdens de opdracht heb geleerd.

Dit document blijft daarom een verslag dat ik tijdens de ontwikkeling verder aanvul.

---

## 8. Gebruikte bronnen

- WPFW Opdracht 3 en rubric – De Haagse Hogeschool.
- Lesmateriaal ASP.NET Core Web API en Entity Framework Core.
- [Microsoft Learn – ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [Microsoft Learn – Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- ChatGPT (OpenAI) – gebruikt voor planning, uitleg en voorbereiding.

**Laatst bijgewerkt:** 27 oktober 2026  
**Status:** Wordt aangevuld tijdens de opdracht.
