FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY AdSyst.Common/AdSyst.Common.BusinessLayer/*.csproj AdSyst.Common/AdSyst.Common.BusinessLayer/
COPY AdSyst.Common/AdSyst.Common.Application/*.csproj AdSyst.Common/AdSyst.Common.Application/
COPY AdSyst.Common/AdSyst.Common.Presentation/*.csproj AdSyst.Common/AdSyst.Common.Presentation/
COPY AdSyst.Common/AdSyst.Common.Contracts/*.csproj AdSyst.Common/AdSyst.Common.Contracts/
COPY AdSyst.Common/AdSyst.Common.Infrastructure/*.csproj AdSyst.Common/AdSyst.Common.Infrastructure/
COPY AdSyst.AuthService/AdSyst.AuthService.Domain/*.csproj AdSyst.AuthService/AdSyst.AuthService.Domain/
COPY AdSyst.AuthService/AdSyst.AuthService.Application/*.csproj AdSyst.AuthService/AdSyst.AuthService.Application/
COPY AdSyst.AuthService/AdSyst.AuthService.Infrastructure/*.csproj AdSyst.AuthService/AdSyst.AuthService.Infrastructure/
COPY AdSyst.AuthService/AdSyst.AuthService.Api/*.csproj AdSyst.AuthService/AdSyst.AuthService.Api/

RUN dotnet restore AdSyst.AuthService/AdSyst.AuthService.Api/AdSyst.AuthService.Api.csproj
COPY AdSyst.Common/ AdSyst.Common/
COPY AdSyst.AuthService/ AdSyst.AuthService/
RUN dotnet publish AdSyst.AuthService/AdSyst.AuthService.Api/AdSyst.AuthService.Api.csproj --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
EXPOSE 80
EXPOSE 36600
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AdSyst.AuthService.Api.dll"]