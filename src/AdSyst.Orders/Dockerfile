FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY AdSyst.Common/AdSyst.Common.BusinessLayer/*.csproj AdSyst.Common/AdSyst.Common.BusinessLayer/
COPY AdSyst.Common/AdSyst.Common.Application/*.csproj AdSyst.Common/AdSyst.Common.Application/
COPY AdSyst.Common/AdSyst.Common.Presentation/*.csproj AdSyst.Common/AdSyst.Common.Presentation/
COPY AdSyst.Common/AdSyst.Common.Contracts/*.csproj AdSyst.Common/AdSyst.Common.Contracts/
COPY AdSyst.Common/AdSyst.Common.DAL.MongoDb/*.csproj AdSyst.Common/AdSyst.Common.DAL.MongoDb/
COPY AdSyst.Common/AdSyst.Common.Infrastructure/*.csproj AdSyst.Common/AdSyst.Common.Infrastructure/

COPY AdSyst.Orders/AdSyst.Orders.DAL.MongoDb/*.csproj AdSyst.Orders/AdSyst.Orders.DAL.MongoDb/
COPY AdSyst.Orders/AdSyst.Orders.BusinessLayer/*.csproj AdSyst.Orders/AdSyst.Orders.BusinessLayer/
COPY AdSyst.Orders/AdSyst.Orders.Application/*.csproj AdSyst.Orders/AdSyst.Orders.Application/
COPY AdSyst.Orders/AdSyst.Orders.Api/*.csproj AdSyst.Orders/AdSyst.Orders.Api/
COPY AdSyst.Orders/AdSyst.Orders.SyncDataServices/*.csproj AdSyst.Orders/AdSyst.Orders.SyncDataServices/

RUN dotnet restore AdSyst.Orders/AdSyst.Orders.Api/AdSyst.Orders.Api.csproj

COPY AdSyst.Common/ AdSyst.Common/
COPY AdSyst.Orders/ AdSyst.Orders/

RUN dotnet publish AdSyst.Orders/AdSyst.Orders.Api/AdSyst.Orders.Api.csproj --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
EXPOSE 80
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AdSyst.Orders.Api.dll"]