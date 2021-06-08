# .NET Core EntityFramework Migrations

## Update the Entity Framework tools to latest version
`dotnet tool update --global dotnet-ef`

## Go to project migration directory
`cd .\DataKwah.Persistence.Migrations\`

## Add a migration
`dotnet ef migrations add <migration_name> -s ..\DataKwah.Api -p .\DataKwah.Persistence.Migrations.csproj`

## Apply migration to database
`dotnet ef database update -s ..\DataKwah.Api -p .\DataKwah.Persistence.Migrations.csproj`

## Migrations list
- `dotnet ef migrations add InitialSetup -s ..\DataKwah.Api -p .\DataKwah.Persistence.Migrations.csproj`
- `dotnet ef migrations add AddProductStateReason -s ..\DataKwah.Api -p .\DataKwah.Persistence.Migrations.csproj`
- `dotnet ef migrations add AddReviewDateAndRating -s ..\DataKwah.Api -p .\DataKwah.Persistence.Migrations.csproj`
