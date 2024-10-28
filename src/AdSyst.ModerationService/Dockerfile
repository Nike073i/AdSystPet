FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY AdSyst.Common/AdSyst.Common.BusinessLayer/*.csproj AdSyst.Common/AdSyst.Common.BusinessLayer/
COPY AdSyst.Common/AdSyst.Common.Application/*.csproj AdSyst.Common/AdSyst.Common.Application/
COPY AdSyst.Common/AdSyst.Common.Presentation/*.csproj AdSyst.Common/AdSyst.Common.Presentation/
COPY AdSyst.Common/AdSyst.Common.Contracts/*.csproj AdSyst.Common/AdSyst.Common.Contracts/
COPY AdSyst.Common/AdSyst.Common.DAL.MongoDb/*.csproj AdSyst.Common/AdSyst.Common.DAL.MongoDb/
COPY AdSyst.Common/AdSyst.Common.Infrastructure/*.csproj AdSyst.Common/AdSyst.Common.Infrastructure/

COPY AdSyst.ModerationService/AdSyst.Moderation.DAL.MongoDb/*.csproj AdSyst.ModerationService/AdSyst.Moderation.DAL.MongoDb/
COPY AdSyst.ModerationService/AdSyst.Moderation.BusinessLayer/*.csproj AdSyst.ModerationService/AdSyst.Moderation.BusinessLayer/
COPY AdSyst.ModerationService/AdSyst.Moderation.Application/*.csproj AdSyst.ModerationService/AdSyst.Moderation.Application/
COPY AdSyst.ModerationService/AdSyst.Moderation.Api/*.csproj AdSyst.ModerationService/AdSyst.Moderation.Api/

RUN dotnet restore AdSyst.ModerationService/AdSyst.Moderation.Api/AdSyst.Moderation.Api.csproj

COPY AdSyst.Common/ AdSyst.Common/
COPY AdSyst.ModerationService/ AdSyst.ModerationService/

RUN dotnet publish AdSyst.ModerationService/AdSyst.Moderation.Api/AdSyst.Moderation.Api.csproj --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
EXPOSE 80
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AdSyst.Moderation.Api.dll"]