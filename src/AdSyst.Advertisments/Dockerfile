FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY AdSyst.Common/AdSyst.Common.BusinessLayer/*.csproj AdSyst.Common/AdSyst.Common.BusinessLayer/
COPY AdSyst.Common/AdSyst.Common.Application/*.csproj AdSyst.Common/AdSyst.Common.Application/
COPY AdSyst.Common/AdSyst.Common.Presentation/*.csproj AdSyst.Common/AdSyst.Common.Presentation/
COPY AdSyst.Common/AdSyst.Common.Contracts/*.csproj AdSyst.Common/AdSyst.Common.Contracts/
COPY AdSyst.Common/AdSyst.Common.Infrastructure/*.csproj AdSyst.Common/AdSyst.Common.Infrastructure/

COPY AdSyst.Advertisments/AdSyst.Advertisments.Api/*.csproj AdSyst.Advertisments/AdSyst.Advertisments.Api/
COPY AdSyst.Advertisments/AdSyst.Advertisments.Application/*.csproj AdSyst.Advertisments/AdSyst.Advertisments.Application/
COPY AdSyst.Advertisments/AdSyst.Advertisments.Domain/*.csproj AdSyst.Advertisments/AdSyst.Advertisments.Domain/
COPY AdSyst.Advertisments/AdSyst.Advertisments.Infrastructure/*.csproj AdSyst.Advertisments/AdSyst.Advertisments.Infrastructure/

RUN dotnet restore AdSyst.Advertisments/AdSyst.Advertisments.Api/AdSyst.Advertisments.Api.csproj
COPY AdSyst.Common/ AdSyst.Common/
COPY AdSyst.Advertisments/ AdSyst.Advertisments/
RUN dotnet publish AdSyst.Advertisments/AdSyst.Advertisments.Api/AdSyst.Advertisments.Api.csproj --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
EXPOSE 80
EXPOSE 36600
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AdSyst.Advertisments.Api.dll"]