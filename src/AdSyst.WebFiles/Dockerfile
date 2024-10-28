FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY AdSyst.Common/AdSyst.Common.BusinessLayer/*.csproj AdSyst.Common/AdSyst.Common.BusinessLayer/
COPY AdSyst.Common/AdSyst.Common.Application/*.csproj AdSyst.Common/AdSyst.Common.Application/
COPY AdSyst.Common/AdSyst.Common.Presentation/*.csproj AdSyst.Common/AdSyst.Common.Presentation/
COPY AdSyst.Common/AdSyst.Common.DAL.MongoDb/*.csproj AdSyst.Common/AdSyst.Common.DAL.MongoDb/
COPY AdSyst.Common/AdSyst.Common.Contracts/*.csproj AdSyst.Common/AdSyst.Common.Contracts/
COPY AdSyst.Common/AdSyst.Common.Infrastructure/*.csproj AdSyst.Common/AdSyst.Common.Infrastructure/

COPY AdSyst.WebFiles/AdSyst.WebFiles.DAL.MongoDb/*.csproj AdSyst.WebFiles/AdSyst.WebFiles.DAL.MongoDb/
COPY AdSyst.WebFiles/AdSyst.WebFiles.BusinessLayer/*.csproj AdSyst.WebFiles/AdSyst.WebFiles.BusinessLayer/
COPY AdSyst.WebFiles/AdSyst.WebFiles.Application/*.csproj AdSyst.WebFiles/AdSyst.WebFiles.Application/
COPY AdSyst.WebFiles/AdSyst.WebFiles.Api/*.csproj AdSyst.WebFiles/AdSyst.WebFiles.Api/

RUN dotnet restore AdSyst.WebFiles/AdSyst.WebFiles.Api/AdSyst.WebFiles.Api.csproj

COPY AdSyst.Common/ AdSyst.Common/
COPY AdSyst.WebFiles/ AdSyst.WebFiles/

RUN dotnet publish AdSyst.WebFiles/AdSyst.WebFiles.Api/AdSyst.WebFiles.Api.csproj --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
EXPOSE 80
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AdSyst.WebFiles.Api.dll"]