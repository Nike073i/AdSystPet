FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY AdSyst.Common/AdSyst.Common.Contracts/*.csproj AdSyst.Common/AdSyst.Common.Contracts/
COPY AdSyst.EmailWorker/*.csproj AdSyst.EmailWorker/

RUN dotnet restore AdSyst.EmailWorker/AdSyst.EmailWorker.csproj
COPY AdSyst.Common/AdSyst.Common.Contracts/ AdSyst.Common/AdSyst.Common.Contracts/
COPY AdSyst.EmailWorker/ AdSyst.EmailWorker/
RUN dotnet publish AdSyst.EmailWorker/AdSyst.EmailWorker.csproj --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
EXPOSE 80
EXPOSE 36600
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AdSyst.EmailWorker.dll"]