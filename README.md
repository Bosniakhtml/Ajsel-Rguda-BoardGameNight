# Avans Spelavonden-app

**Auteur:** Ajsel Rguda  
**Platform:** ASP.NET Core 8 / C# / Entity Framework Core / SQL Server

## Projecten

- `BoardGameNight.Domain`: domeinmodellen, enumeraties en domeinexceptions.
- `BoardGameNight.DomainServices`: services en repository-interfaces.
- `BoardGameNight.Infrastructure`: EF Core-contexten, migraties, repositories en seeddata.
- `BoardGameNight.UI`: MVC/Razor-webapplicatie.
- `BoardGameNight.WebApi`: REST API en GraphQL-endpoint.
- `BoardGameNight.UI.Tests`: unit tests voor domeinservices.

## Lokaal starten

1. Installeer de .NET 8 SDK en SQL Server LocalDB (via Visual Studio Installer).
2. Kopieer `.env.example` naar `.env`.
3. Pas beide connection strings in `.env` aan als jouw lokale SQL Server andere instellingen nodig heeft.
4. Open `BoardGameNight.sln` in Visual Studio.
5. Voer vanuit de solutionmap in een terminal de migraties uit (zie hieronder).
6. Start `BoardGameNight.UI` of `BoardGameNight.WebApi` als startup project.

`.env` bevat lokale configuratie en mag nooit naar Git worden gepusht. De connection strings moeten verwijzen naar twee aparte databases: één voor bordspellengegevens en één voor ASP.NET Identity.

## Database migraties

Installeer zo nodig de EF CLI:

```powershell
dotnet tool install --global dotnet-ef --version 8.0.10
```

Zorg dat de twee environment variables `BoardgameConnectionString` en `AuthDbConnectionString` in de terminal zijn ingesteld en voer uit:

```powershell
dotnet ef database update --project BoardGameNight.Infrastructure --startup-project BoardGameNight.UI --context BoardgameDbContext
dotnet ef database update --project BoardGameNight.Infrastructure --startup-project BoardGameNight.UI --context AuthDbContext
```

De design-time factories vereisen deze variabelen expliciet; er staat geen computergebonden databaseverbinding meer in de code.

## GitHub Actions

De workflow `.github/workflows/ci.yml` restore't, bouwt en test de solution op pushes en pull requests naar `main` of `master`. Bekijk de Actions-tab op GitHub na je eerste push.

## Azure / deployment

Maak in Azure twee App Services (één voor de MVC UI en één voor de API) en twee Azure SQL-databases. Configureer in beide App Services onder **Environment variables / Configuration**:

- `BoardgameConnectionString`
- `AuthDbConnectionString`

Gebruik connection strings met de juiste Azure SQL server/database en credentials; plaats ze niet in `appsettings.json`, de repository of dit README-bestand. Geef de Azure SQL-server toegang vanaf de App Services en maak de migraties aan voordat de applicaties worden gestart.

De Azure deployment workflow moet pas worden toegevoegd/geactiveerd nadat de Azure resources en GitHub repository secrets zijn aangemaakt. CI build/test staat al klaar, maar deployment is nog niet geconfigureerd.

## Bekende aandachtspunten voor validatie

- Er is nog geen build/test-run uitgevoerd in deze omgeving omdat de .NET SDK hier niet beschikbaar is. Laat GitHub Actions en/of `dotnet test` de compileerbaarheid en tests bevestigen.
- Controleer alle user stories en business rules tegen de opdracht. Een aantal REST CRUD-endpoints retourneert momenteel `501 Not Implemented`.
- Controleer seeddata: de voorbeeldaccounts en gedeelde wachtwoorden zijn uitsluitend demonstratiedata en mogen niet als veilige productieaccounts worden gebruikt.
- De bestaande testprojectnaam `BoardGameNight.UI.Tests` is historisch; de tests richten zich grotendeels op services.
