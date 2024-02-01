FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Workshopper.DatabaseUpdater/Workshopper.DatabaseUpdater.csproj", "Workshopper.DatabaseUpdater/"]
COPY ["Workshopper.Infrastructure/Workshopper.Infrastructure.csproj", "Workshopper.Infrastructure/"]
COPY ["Workshopper.Application/Workshopper.Application.csproj", "Workshopper.Application/"]
COPY ["Workshopper.Domain/Workshopper.Domain.csproj", "Workshopper.Domain/"]
RUN dotnet restore "Workshopper.DatabaseUpdater/Workshopper.DatabaseUpdater.csproj"
COPY . .
WORKDIR "/src/Workshopper.DatabaseUpdater"
RUN dotnet build "Workshopper.DatabaseUpdater.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Workshopper.DatabaseUpdater.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workshopper.DatabaseUpdater.dll"]
