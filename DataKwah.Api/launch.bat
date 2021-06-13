cd ./ClientApp/
npm i
npm run build
cd ..
dotnet build ./DataKwah.Api.csproj
dotnet run --project ./DataKwah.Api.csproj --launch-profile DataKwah.Production
