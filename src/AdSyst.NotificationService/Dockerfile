FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY AdSyst.Common/AdSyst.Common.BusinessLayer/*.csproj AdSyst.Common/AdSyst.Common.BusinessLayer/
COPY AdSyst.Common/AdSyst.Common.Application/*.csproj AdSyst.Common/AdSyst.Common.Application/
COPY AdSyst.Common/AdSyst.Common.Presentation/*.csproj AdSyst.Common/AdSyst.Common.Presentation/
COPY AdSyst.Common/AdSyst.Common.Contracts/*.csproj AdSyst.Common/AdSyst.Common.Contracts/
COPY AdSyst.Common/AdSyst.Common.DAL.MongoDb/*.csproj AdSyst.Common/AdSyst.Common.DAL.MongoDb/
COPY AdSyst.Common/AdSyst.Common.Infrastructure/*.csproj AdSyst.Common/AdSyst.Common.Infrastructure/

COPY AdSyst.NotificationService/AdSyst.Notifications.DAL.MongoDb/*.csproj AdSyst.NotificationService/AdSyst.Notifications.DAL.MongoDb/
COPY AdSyst.NotificationService/AdSyst.Notifications.BusinessLayer/*.csproj AdSyst.NotificationService/AdSyst.Notifications.BusinessLayer/
COPY AdSyst.NotificationService/AdSyst.Notifications.Application/*.csproj AdSyst.NotificationService/AdSyst.Notifications.Application/
COPY AdSyst.NotificationService/AdSyst.Notifications.Api/*.csproj AdSyst.NotificationService/AdSyst.Notifications.Api/

RUN dotnet restore AdSyst.NotificationService/AdSyst.Notifications.Api/AdSyst.Notifications.Api.csproj

COPY AdSyst.Common/ AdSyst.Common/
COPY AdSyst.NotificationService/ AdSyst.NotificationService/

RUN dotnet publish AdSyst.NotificationService/AdSyst.Notifications.Api/AdSyst.Notifications.Api.csproj --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
EXPOSE 80
EXPOSE 36600
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AdSyst.Notifications.Api.dll"]