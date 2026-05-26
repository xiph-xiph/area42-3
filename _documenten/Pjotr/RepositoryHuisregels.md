# GitHub Repository Huisregels – Area42-3

## Doel van deze afspraken

Deze afspraken zijn opgesteld om binnen het ontwikkelteam consistent en overzichtelijk samen te werken in één gedeelde codebase. Omdat meerdere teamleden parallel werken aan verschillende onderdelen van het systeem, zijn duidelijke regels nodig om conflicten te voorkomen en de kwaliteit van de repository te bewaken.

---

## Branching strategie

Binnen dit project hanteren we een eenvoudige Git-structuur met één centrale main branch:

- **`main`**
  Bevat altijd stabiele en werkende code die klaar is voor oplevering.

Daarnaast werken we met ondersteunende branches:

- **`feature/*`**
  Voor nieuwe functionaliteit of uitbreidingen.
  Voorbeeld: `feature/snackbar-order-system`

- **`fix/*`**
  Voor bugfixes en correcties.
  Voorbeeld: `fix/cart-calculation-error`

### Regels:

- Er wordt nooit direct op `main` aan code gewerkt.
- Alle wijzigingen verlopen via een pull request.
- Feature- en fix-branches worden verwijderd na succesvolle merge.
- De enige uitzondering is voor het werk aan documenten, in de `_documenten` map, omdat deze compleet los staan van de code. Commits met betrekking enkel tot documenten mogen gepusht worden naar `main` zonder pull request.

---

## 3. Commit werkwijze

We hanteren geen strikt commit conventiesysteem, maar streven naar duidelijke en overzichtelijke commits.

### Richtlijnen:

- Werk in logische stappen in plaats van grote, langdurige commits.
- Splits werk zoveel mogelijk op in kleine, functionele wijzigingen.
- Commits moeten begrijpelijk zijn voor andere teamleden zonder extra uitleg.

---

## 4. Pull Requests

Alle wijzigingen worden geïntegreerd via pull requests.

### Vereisten:

- Elke PR heeft een duidelijke titel.
- De beschrijving mag kort en bondig zijn.
- Elke PR is gekoppeld aan een feature- of fix-branch.
- Elke PR wordt gereviewd door minimaal één teamlid voordat deze wordt gemerged.

---

## 5. Repository structuur

De repository is opgedeeld in drie hoofdsecties:

- **`/frontend`**
  Bevat de React-applicatie.

- **`/backend`**
  Bevat de ASP.NET Core backend applicatie.

- **`/_documents`**
  Bevat projectdocumentatie en individuele werkdocumenten van teamleden.
  Deze map wordt gebruikt om alle relevante documenten binnen versiebeheer op te slaan.

---
