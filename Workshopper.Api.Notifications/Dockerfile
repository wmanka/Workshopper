FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Workshopper.Api.Notifications/Workshopper.Api.Notifications.csproj", "Workshopper.Api.Notifications/"]
COPY ["Workshopper.Api.Common/Workshopper.Api.Common.csproj", "Workshopper.Api.Common/"]
COPY ["Workshopper.Application/Workshopper.Application.csproj", "Workshopper.Application/"]
COPY ["Workshopper.Domain/Workshopper.Domain.csproj", "Workshopper.Domain/"]
COPY ["Workshopper.Infrastructure/Workshopper.Infrastructure.csproj", "Workshopper.Infrastructure/"]
RUN dotnet restore "Workshopper.Api.Notifications/Workshopper.Api.Notifications.csproj"
COPY . .
WORKDIR "/src/Workshopper.Api.Notifications"
RUN dotnet build "Workshopper.Api.Notifications.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Workshopper.Api.Notifications.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workshopper.Api.Notifications.dll"]
