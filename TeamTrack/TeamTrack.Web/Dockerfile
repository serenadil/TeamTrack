# Fase base, usata in produzione
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase di build, usata per compilare il progetto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia dei progetti e restore delle dipendenze
COPY ["TeamTrack.Dominio/TeamTrack.Dominio.csproj", "Dominio/"]
COPY ["TeamTrack.Infrastrutture/TeamTrack.Infrastrutture.csproj", "Infrastrutture/"]
COPY ["TeamTrack.Servizi/TeamTrack.Servizi.csproj", "Servizi/"]
COPY ["TeamTrack.API/TeamTrack.API.csproj", "API/"]
COPY ["TeamTrack.Web/TeamTrack.Web.csproj", "TeamTrack.Web/"]
RUN dotnet restore "./TeamTrack.Web/TeamTrack.Web.csproj"
COPY . ./

# Costruzione dell'applicazione
WORKDIR "/src/TeamTrack.Web"
RUN dotnet build "./TeamTrack.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase di pubblicazione
FROM build AS publish
RUN dotnet publish "./TeamTrack.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase per installare gli strumenti globali (dotnet-ef)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS tool-install
WORKDIR /app
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Fase finale di produzione
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=tool-install /root/.dotnet/tools /root/.dotnet/tools
ENTRYPOINT ["dotnet", "TeamTrack.Web.dll"]
