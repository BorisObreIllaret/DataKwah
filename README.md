# DataKwah

A .NET Core 5 & Angular project using [CQRS pattern](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation) and [Mediator pattern](https://en.wikipedia.org/wiki/Mediator_pattern).

## Instructions
- **Clone**: clone this reposotory using `git clone https://github.com/BorisObreIllaret/DataKwah.git`.
- **Setup**: Open a terminal and go to DataKwah directory using `cd DataKwah`.
- **Build frontend**:
  - Go to front directory: `cd ./DataKwah.Api/ClientApp/`.
  - Restore packages: `npm i`.
  - Build angular project: `npm run build`.
- **Build backend**:
  - Go to back directory: `cd ..`, you should be in directory `DataKwah/DataKwah.Api`.
  - Build .NET Core project: `dotnet build ./DataKwah.Api.csproj`.
- **Run**: `dotnet run --project ./DataKwah.Api.csproj --launch-profile DataKwah.Production`.
- Open a web browser and to to url: `https://localhost:5001/`. 

## Project structure

- **API**: Projects for managing Web API.
  - **DataKwah.Api**: Entrypoint of solution. Contains API settings and database file.
  - **DataKwah.Api/ClientApp**: Angular project.
  - **DataKwah.Api.Controllers**: API controllers.
  - **DataKwah.Api.Middleware**: Custom API middlewares, like exception filter.
- **Application**: Application projects, containing buisiness logic
  - **DataKwah.Application.Commands**: Commands for managing application state.
  - **DataKwah.Application.Queries**: Queries for retrieving application data.
  - **DataKwah.Application.Services**: Application services, like indexation service.
- **Core**: Shared projects
  - **DataKwah.Core.Extensions**: Extensions methods
  - **DataKwah.Core.Filter**: Filter classes
- **Domain**: Entities declaration and configuration
  - **DataKwah.Domain.Entities**: Entities (models)
  - **DataKwah.Domain.Configuration**: Entities configuration (used by Entity Framework)
- **Persistence**: Persistence management, closely related to Entity Framework 
  - **DataKwah.Persistence**: Database Contexts
  - **DataKwah.Persistence.Migrations**: Database migrations
  - **DataKwah.Persistence.Repositories**: Repositories for querying and updating database
  
## Key features
- Usage of asynchrone syntaxe and cancellation tokens
- Thin controllers thanks to mediator
- Split between read requests and write requests thanks to command and query separation

## Improvements needed
- Backend: need to split interfaces and concrete implementations
- Backend: add unit tests
- Front: implement server side validation
