FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Workshopper.Api.Auth/Workshopper.Api.Auth.csproj", "Workshopper.Api.Auth/"]
COPY ["Workshopper.Api.Auth.Contracts/Workshopper.Api.Auth.Contracts.csproj", "Workshopper.Api.Auth.Contracts/"]
COPY ["Workshopper.Application/Workshopper.Application.csproj", "Workshopper.Application/"]
COPY ["Workshopper.Domain/Workshopper.Domain.csproj", "Workshopper.Domain/"]
COPY ["Workshopper.Infrastructure/Workshopper.Infrastructure.csproj", "Workshopper.Infrastructure/"]
RUN dotnet restore "Workshopper.Api.Auth/Workshopper.Api.Auth.csproj"
COPY . .
WORKDIR "/src/Workshopper.Api.Auth"
RUN dotnet build "Workshopper.Api.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Workshopper.Api.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workshopper.Api.Auth.dll"]
