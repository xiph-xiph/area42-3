# Analysedocument – Snackbar module

## Inleiding

Binnen het project Area42 wordt gewerkt aan de ontwikkeling van een geïntegreerd prototype-informatiesysteem voor een fictief vakantiepark in de Achterhoek. Het doel van dit systeem is om bestaande handmatige processen binnen het park te digitaliseren en te structureren, zodat schaalbaarheid, efficiëntie en betrouwbaarheid worden verbeterd.

Het vakantiepark bestaat uit meerdere domeinen zoals horeca, parkonderhoud, security en businessanalyse, die gezamenlijk bijdragen aan één samenhangend systeem. Binnen dit geheel richt deze analyse zich op de snackbarmodule binnen de horecacomponent van het systeem. De snackbar vormt een zelfstandig onderdeel binnen de horeca-architectuur en ondersteunt zowel informatieve als transactionele functionaliteit, zoals het bekijken van informatie en het plaatsen van bestellingen.

De nadruk in dit deelproject ligt op de backend-architectuur en de bijbehorende datamodellering, waarbij rekening wordt gehouden met toekomstige uitbreidbaarheid en herbruikbaarheid binnen andere horecacomponenten.

---

## Onderzoeksvraag

De centrale onderzoeksvraag binnen dit onderdeel luidt:

**_Hoe kan de snackbar-component voor de Area42-webapp, waarin klanten bestellingen kunnen plaatsen voor specifieke tijdslots en medewerkers deze bestellingen efficiënt kunnen verwerken, rekening houdend met uitbreidbaarheid richting andere horecacomponenten, worden ontworpen?_**

---

## Stakeholders

Binnen dit onderdeel van het systeem zijn de volgende stakeholders geïdentificeerd:

### Klant

De klant is de eindgebruiker van het systeem binnen het vakantiepark. De klant kan:

- Het menu bekijken
- Informatie zoals openingstijden raadplegen
- Producten toevoegen aan een winkelwagen
- Een bestelling plaatsen voor een specifiek tijdslot

De klant heeft geen toegang tot interne orderverwerking of statusbeheer.

### Medewerker

De medewerker is verantwoordelijk voor de operationele verwerking van bestellingen binnen de snackbar. De medewerker kan:

- Bestellingen inzien
- Bestellingen filteren op status en tijdslot
- De status van bestellingen aanpassen (bijvoorbeeld: ontvangen, in bereiding, afgerond)

De medewerker fungeert hiermee als uitvoerende rol binnen het proces.

### Ontwikkelteam

Het ontwikkelteam is verantwoordelijk voor de technische realisatie van het systeem. Binnen deze context ligt de focus op:

- Backend-architectuur (ASP.NET Core)
- Datamodel en databaseontwerp (PostgreSQL + Entity Framework Core)
- API-ontwerp en integratie met frontend

---

## Taak- en probleemanalyse

### Functionele eisen

De snackbarmodule moet de volgende functionaliteit ondersteunen:

- Het weergeven van basisinformatie zoals openingstijden en algemene informatie
- Het tonen van een vast menu met producten en prijzen
- Het ondersteunen van een winkelwagen waar producten aan kunnen worden toegevoegd
- Het plaatsen van bestellingen voor een specifiek tijdslot als er een gevulde winkelwagen is
- Het beheren en verwerken van bestellingen door medewerkers
- Het wijzigen van de status van een bestelling binnen de medewerker-interface

Reserveringsfunctionaliteit maakt conceptueel deel uit van de horecastructuur, maar wordt primair door een andere module binnen het systeem ontwikkeld. De snackbar moet echter voorbereid zijn op herbruikbaarheid van deze logica.

---

### Niet-functionele eisen

Bij het ontwerp van het systeem zijn de volgende niet-functionele eisen van belang:

- **Modulariteit:** De snackbar moet onafhankelijk functioneren binnen de bredere horecastructuur en herbruikbare logica ondersteunen.
- **Schaalbaarheid:** Het systeem moet voorbereid zijn op uitbreiding naar meerdere horecavoorzieningen.
- **Onderhoudbaarheid:** Door gebruik van een gestructureerde architectuur moet het systeem eenvoudig uitbreidbaar blijven.
- **Veiligheid:** De winkelwagen- en orderlogica wordt volledig server-side beheerd om manipulatie via de client te voorkomen.

---

### Technische analyse

De gekozen technologie stack bestaat uit:

- **Backend:** ASP.NET Core
- **Database:** PostgreSQL
- **Frontend:** React

_Deze keuzes worden verder beschreven en onderbouwd in het Tech Stack document._

Het systeem maakt gebruik van een duidelijke scheiding tussen frontend en backend. De backend is verantwoordelijk voor alle logica rondom bestellingen, terwijl de frontend enkel dient als presentatielaag en inputlaag.

De winkelwagen wordt volledig server-side beheerd. Dit betekent dat alle toevoegingen en verwijderingen van producten via POST-requests worden verwerkt, zonder dat de client directe controle heeft over de interne status van de cart.

---

### Conceptueel Datamodel

De kernentiteiten binnen het systeem zijn:

- Product
- Order
- OrderItem
- User (Customer / Employee)

Het menu is voor dit dit project vastgesteld en niet direct aanpasbaar, maar deze zal wel opgeslagen worden in de database, waardoor het menu wel aangepast zou kunnen worden door handmatige SQL-queries, en (more importantly) kan deze functionaliteit later makkelijk geimplementeerd worden.

---

## Conclusie

De snackbarmodule binnen het Area42-project is ontworpen als een modulair en uitbreidbaar onderdeel binnen de horecastructuur van het vakantiepark. Door te focussen op een duidelijke scheiding tussen frontend en backend en het toepassen van een server-side gecontroleerd bestelsysteem, wordt de basis gelegd voor een veilige en schaalbare oplossing.

De gekozen architectuur in ASP.NET Core met PostgreSQL en Entity Framework Core biedt voldoende flexibiliteit om toekomstige uitbreidingen, zoals dynamische menu’s of uitgebreide reserveringssystemen, eenvoudig te integreren. Tegelijkertijd blijft het systeem bewust afgebakend om complexiteit in deze fase te beperken en de focus te leggen op een solide kernfunctionaliteit.

---
