# Fase base, usata in produzione  
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase di build, usata per compilare il progetto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia solo i file .csproj per eseguire il restore
COPY ./TeamTrack.Dominio/TeamTrack.Dominio.csproj ./TeamTrack.Dominio/
COPY ./TeamTrack.Infrastrutture/TeamTrack.Infrastrutture.csproj ./TeamTrack.Infrastrutture/
COPY ./TeamTrack.Servizi/TeamTrack.Servizi.csproj ./TeamTrack.Servizi/
COPY ./TeamTrack.API/TeamTrack.API.csproj ./TeamTrack.API/
COPY ./TeamTrack.Web/TeamTrack.Web.csproj ./TeamTrack.Web/

# Esegui il restore 
RUN dotnet restore "./TeamTrack.Web/TeamTrack.Web.csproj"


# Copia tutti i file
COPY . ./

# Costruzione dell'applicazione
WORKDIR "/src/TeamTrack.Web"
RUN dotnet build "./TeamTrack.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase di pubblicazione
FROM build AS publish
RUN dotnet publish "./TeamTrack.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase finale di produzione
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Installa il .NET SDK direttamente dai repository ufficiali di Microsoft
RUN apt-get update \
    && apt-get install -y wget apt-transport-https software-properties-common \
    && wget https://packages.microsoft.com/config/ubuntu/22.04/prod.list \
    && mv prod.list /etc/apt/sources.list.d/microsoft-prod.list \
    && wget -q https://packages.microsoft.com/keys/microsoft.asc -O- | apt-key add - \
    && apt-get update \
    && apt-get install -y dotnet-sdk-8.0

# Copia l'applicazione pubblicata dalla fase di pubblicazione
COPY --from=publish /app/publish .

# Installa il tool dotnet-ef nella fase finale
RUN dotnet tool install --global dotnet-ef

# Imposta la variabile PATH correttamente
ENV PATH="$PATH:/root/.dotnet/tools"

# Esegui l'applicazione
ENTRYPOINT ["dotnet", "TeamTrack.Web.dll"]
