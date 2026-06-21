# Uitleg API-test-setup Auth-backend endpoint

Deze testset controleert de `AuthController` via echte HTTP-calls op de backend, maar zonder een losse handmatig gestarte webserver. De tests zijn gemaakt als integratietests: de route, modelbinding, validatie en de businesslaag worden samen aangesproken.

## Doel van de setup

De bedoeling van deze tests is om te verifiëren dat de auth-endpoints correct reageren op:

- geldige registratie;
- ontbrekende velden;
- dubbele e-mailadressen;
- ongeldige e-mailformaten;
- zwakke wachtwoorden;
- geldige login;
- verkeerde wachtwoorden;
- niet-bestaande accounts.

## Wat wordt getest en waarom

De tests kijken niet alleen of de endpoint bestaat, maar of de volledige auth-flow klopt.

- Bij registratie wordt getest of een gebruiker echt kan worden aangemaakt, omdat hier validatie, hashing en opslag samenkomen.
- Bij dubbele of ongeldige invoer wordt getest of de backend foutmeldingen geeft, omdat slechte data niet door mag lekken naar de database.
- Bij login wordt getest of correcte inloggegevens een token opleveren, omdat de frontend alleen verder kan werken als auth succesvol is.
- Bij verkeerde wachtwoorden of onbekende accounts wordt getest of de backend toegang weigert, omdat beveiliging niet mag afhangen van alleen de UI.

Daarmee bewijzen de tests dat de controller, service, repository en token-generatie samen het verwachte gedrag leveren.

## Hoe de testomgeving werkt

De testprojecten verwijzen rechtstreeks naar het backend-project via een project reference. Daardoor start de testhost de echte applicatiecode, inclusief controllers, services en repository-laag.

De kern van de testopzet staat in `AuthTests.cs`:

- `WebApplicationFactory<Program>` start een testserver voor de backend;
- `CreateClient()` levert een `HttpClient` waarmee de tests requests sturen naar de API;
- `PostAsJsonAsync(...)` verstuurt JSON-payloads naar de endpoints;
- de responses worden gecontroleerd op statuscode en, waar nodig, op de inhoud van de response body.

## Belangrijke configuratie

In de testconstructor wordt de JWT-secret expliciet gezet:

```csharp
Environment.SetEnvironmentVariable("JwtSecretKey", "test-secret-key-has-to-be-32-characters-long-or-it-will-not-work");
```

Dat is nodig omdat `Program.cs` de JWT-validatie configureert met de waarde van `JwtSecretKey`. Zonder die variabele kan de backend niet goed opstarten.

De backend gebruikt verder de normale dependency injection uit `Program.cs`:

- `AuthService` voor de auth-logica;
- `UserRepository` voor database-toegang;
- `Argon2PasswordHasher` voor wachtwoordhashing;
- `JWTTokenGenerator` voor tokens.

## Testdata en isolatie

Elke test genereert unieke gegevens met `Guid.NewGuid()` of helpermethodes uit `Helpers.cs`. Zo krijgt elke testcase een eigen e-mailadres en blijft de testdata zo veel mogelijk gescheiden.

De helpermethodes maken willekeurige testwaarden:

- naam;
- telefoonnummer;
- wachtwoord.

Dat voorkomt dat tests elkaar beïnvloeden als ze meerdere keren worden uitgevoerd.

## Geteste endpoints

### Register

De registertest stuurt een POST-request naar:

```text
/api/auth/register
```

Bij een geldige payload verwacht de test:

- statuscode `200 OK`;
- body met `success: true`;
- melding `Registratie geslaagd.`

De negatieve tests controleren onder andere:

- ontbrekende velden -> `400 BadRequest`;
- duplicaat e-mailadres -> `400 BadRequest`;
- ongeldig e-mailformaat -> `400 BadRequest`;
- zwak wachtwoord -> `400 BadRequest`.

### Login

De logintest stuurt een POST-request naar:

```text
/api/auth/login
```

Bij geldige inloggegevens verwacht de test:

- statuscode `200 OK`;
- een `TokenDto` met `Success = true`;
- een niet-lege token;
- een ingevulde geldigheidsdatum.

De negatieve tests controleren:

- ontbrekende velden -> `400 BadRequest`;
- fout wachtwoord -> `401 Unauthorized`;
- niet-bestaand e-mailadres -> `401 Unauthorized`.

## Hoe de backend zelf reageert

De controller in `AuthController.cs` doet zelf weinig logica. Die stuurt het request door naar `AuthService` en vertaalt het resultaat naar de juiste HTTP-response:

- `Register` geeft `200 OK` terug bij succes en `400 BadRequest` bij fouten;
- `Login` geeft `200 OK` terug bij succes en `401 Unauthorized` bij foutieve inloggegevens.

Daardoor testen we niet alleen de controller, maar ook de volledige keten erachter.

## Samenvatting

Deze API-test-setup controleert de auth-endpoints als echte integratietests. De testserver wordt automatisch opgezet, de requests lopen via JSON over HTTP, en de uitkomsten worden gevalideerd op statuscode en response-inhoud. Zo wordt snel zichtbaar of registratie en login nog correct werken na wijzigingen in backend, validatie of database-logica.
