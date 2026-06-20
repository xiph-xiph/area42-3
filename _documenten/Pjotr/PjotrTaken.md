## Backend verantwoordelijkheden

Ik ben verantwoordelijk voor de authentication endpoints en het takeaway bestelsysteem binnen de backend applicatie.

## Authenticatie (Registratie en inloggen)

Binnen de authentication zijn registratie- en inlogfunctionaliteiten geïmplementeerd. Bij registratie worden wachtwoorden gehasht met Argon2 voor veilige opslag. Voor het inloggen wordt JWT-authenticatie gebruikt, waarbij tokens worden gegenereerd en gebruikt voor toegang tot beveiligde endpoints.

## Takeaway bestelsysteem

Voor het takeaway bestelsysteem heb ik de backend-logica ontwikkeld voor het beheren van winkelwagens en bestellingen. Klanten kunnen items toevoegen, wijzigen en verwijderen uit hun winkelwagen. Tijdens de checkout wordt de winkelwagen omgezet naar een definitieve bestelling.

## Menu endpoint

Daarnaast heb ik het menu endpoint ontwikkeld. Menu items worden opgeslagen in de database en order items refereren hiernaar om aan te geven welke producten zijn besteld.

## Autorisatie

Ik heb ook gewerkt aan de autorisatie binnen het systeem. Hierbij bepalen rollen zoals customer en employee welke data toegankelijk is. Klanten hebben alleen toegang tot hun eigen bestellingen, terwijl medewerkers alle bestellingen binnen het systeem kunnen inzien.

## Frontend integratie en ondersteuning

Tot slot heb ik bijgedragen aan de integratie tussen de React frontend en de ASP.NET Core backend API. Deze koppeling wordt gerealiseerd via HTTP-requests met Axios. Daarnaast heb ik ondersteuning geboden aan Nick met de basisprincipes van React, zodat hij zelfstandig de frontendstructuur en pagina’s kon ontwikkelen.
