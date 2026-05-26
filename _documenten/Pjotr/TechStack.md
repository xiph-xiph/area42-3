# Tech stack document

## Inleiding

Dit document geeft een overzicht van de gekozen technologieën voor het project. De bestaande inhoud hieronder beschrijft de keuzes voor backend, database, ORM, frontend, state management en API-communicatie.

---

## Backend Framework: ASP.NET Core (C#)

ASP.NET Core is gekozen als backend framework omdat de projectvereisten voorschrijven dat er in C# ontwikkeld moet worden. Binnen het .NET-ecosysteem is ASP.NET Core de standaardoplossing voor het bouwen van web-API's.

Daarnaast sluit het framework goed aan op een gestructureerde architectuur. Het ondersteunt het gebruik van controller-based patterns, waardoor de backend logisch kan worden opgedeeld in afzonderlijke verantwoordelijkheden per domein of functionaliteit. Dit draagt bij aan onderhoudbaarheid en overzicht binnen het project.

ASP.NET Core is daarnaast een veelgebruikte technologie binnen software development teams. Het opdoen van ervaring met dit framework wordt daarom gezien als waardevol voor professionele ontwikkeling en inzetbaarheid op de arbeidsmarkt.

---

## Database: PostgreSQL

Voor de database zal gebruik worden gemaakt van PostgreSQL als relationele database. PostgreSQL is gekozen vanwege de combinatie van betrouwbaarheid, flexibiliteit en brede functionaliteit binnen moderne softwareontwikkeling.

Een belangrijk voordeel van PostgreSQL is de ondersteuning voor JSON-kolommen, waardoor het mogelijk is om semi-gestructureerde data op te slaan binnen een relationeel datamodel. Dit biedt flexibiliteit in situaties waarin datavelden kunnen variëren zonder over te stappen naar een NoSQL-model.

Daarnaast ondersteunt PostgreSQL geavanceerde functionaliteiten zoals full-text search. Hierdoor kan krachtige zoekfunctionaliteit direct binnen de database worden geïmplementeerd, zonder afhankelijk te zijn van externe zoekoplossingen zoals Elasticsearch.

Hoewel deze geavanceerde functionaliteiten voor dit project waarschijnlijk niet volledig nodig zullen zijn vanwege de beperkte complexiteit, biedt PostgreSQL hierdoor wel ruimte voor uitbreiding en toekomstige flexibiliteit. Het project zou technisch gezien ook met andere relationele databases uitgevoerd kunnen worden zonder significante verschillen in implementatie in de applicatielaag.

Toch is bewust gekozen voor PostgreSQL vanwege de brede toepasbaarheid, de sterke ondersteuning binnen de industrie en de wens om ervaring op te doen met een veelzijdige en veelgebruikte database-oplossing binnen software development.

---

## ORM (Entity Framework Core)

Voor de interactie tussen de ASP.NET Core backend en de PostgreSQL database zal gebruik worden gemaakt van Entity Framework Core als ORM.

Entity Framework Core is gekozen omdat het de interactie met de database abstraheert via C#-objecten, waardoor er minder handmatige SQL-query's geschreven hoeven te worden. Dit verhoogt de ontwikkelsnelheid en verkleint de kans op fouten in database-interacties.

Daarnaast biedt Entity Framework Core ondersteuning voor migrations, waardoor de database-structuur op een gecontroleerde en versiebeheerbare manier kan worden aangepast gedurende de ontwikkeling van het project.

Door de nauwe integratie met ASP.NET Core sluit Entity Framework Core goed aan op de gekozen backend stack en draagt het bij aan een consistente en gestructureerde architectuur.

---

## Frontend Framework: React (TypeScript)

React is gekozen als frontend framework omdat het momenteel het meest gebruikte frontend framework is binnen de industrie. Hierdoor sluit de keuze aan bij veelvoorkomende praktijken binnen software development teams en levert het relevante ervaring op voor toekomstige software development functies.

Daarnaast sluit React goed aan op de learning outcomes van Media, waarin expliciet wordt verwacht dat er met JavaScript gewerkt wordt. Omdat React is opgebouwd rond JavaScript, past dit beter binnen de gestelde eisen dan alternatieven zoals Blazor.

Een aanvullende overweging is dat Blazor minder geschikt wordt geacht voor dit type project en minder gangbaar is als technologie in frontendgerichte portfolio's binnen web development contexten. Hierdoor is gekozen voor een technologie die breder toepasbaar is binnen moderne webontwikkeling.Daarnaast is React gekozen vanwege de bestaande ervaring binnen het team.

Alternatieven zoals Angular en Vue zijn overwogen, maar niet gekozen vanwege een combinatie van factoren: minder aanwezige ervaring binnen het team en de behoefte aan een efficiënte samenwerking binnen de beperkte projecttijd.

TypeScript is gekozen in plaats van puur JavaScript. De belangrijkste reden hiervoor is type safety: TypeScript maakt het mogelijk om fouten vroegtijdig te detecteren tijdens compile-time, wat de betrouwbaarheid en onderhoudbaarheid van de code verhoogt.

TypeScript wordt ook breed toegepast in moderne frontend development teams; vrijwel alle grootschalige JavaScript ontwikkeling gebruikt tegenwoordig TypeScript. Hierdoor is ervaring met TypeScript erg relevant voor professionele softwareontwikkeling.

Hoewel TypeScript een extra leercurve heeft, wordt deze in de praktijk beperkt doordat het gebruik van TypeScript syntax optioneel is. Hierdoor kan in een vroege fase ook JavaScript-syntax worden gebruikt, terwijl later stapsgewijs strengere typing kan worden toegepast.

---

## State management: geen externe library

Binnen het frontend zal geen gebruik worden gemaakt van een externe state management library, zoals Redux of Zustand. De belangrijkste reden hiervoor is de beperkte complexiteit van het project. De app zal geen complexe globale state hebben die een state management oplossing noodzakelijk maakt.

In plaats daarvan zal gebruik worden gemaakt van de ingebouwde state management mogelijkheden van React, zoals `useState` en `useContext`. Deze bieden voldoende functionaliteit om state op een overzichtelijke manier te beheren zonder extra lagen toe te voegen.

Daarnaast zorgt het vermijden van een externe state management library voor een lagere technische overhead. Dit vereenvoudigt de codebase, verlaagt de leercurve voor teamleden en beperkt de afhankelijkheden binnen het project.

---

## API communicatie: native Fetch API

Voor communicatie tussen de front- en backend zal gebruik worden gemaakt van JavaScript's Fetch API. Hiervoor is gekozen omdat Fetch standaard beschikbaar is in moderne browsers en geen externe dependencies vereist.

Door gebruik te maken van Fetch wordt de complexiteit van de frontend beperkt en blijft de afhankelijkhedenlijst minimaal. Voor de schaal en structuur van dit project biedt Fetch voldoende functionaliteit voor het uitvoeren van HTTP-requests richting de ASP.NET Core backend.

Daarnaast sluit deze aanpak goed aan bij de wens om de frontend zo licht mogelijk te houden, zonder libraries zoals Axios, aangezien de benodigde functionaliteit binnen de native API beschikbaar is.

---
