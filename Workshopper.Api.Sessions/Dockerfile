FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Workshopper.Api.Sessions/Workshopper.Api.Sessions.csproj", "Workshopper.Api.Sessions/"]
COPY ["Workshopper.Domain/Workshopper.Domain.csproj", "Workshopper.Domain/"]
COPY ["Workshopper.Api.Common/Workshopper.Api.Common.csproj", "Workshopper.Api.Common/"]
COPY ["Workshopper.Application/Workshopper.Application.csproj", "Workshopper.Application/"]
COPY ["Workshopper.Infrastructure/Workshopper.Infrastructure.csproj", "Workshopper.Infrastructure/"]
COPY ["Workshopper.Api.Sessions.Contracts/Workshopper.Api.Sessions.Contracts.csproj", "Workshopper.Api.Sessions.Contracts/"]
RUN dotnet restore "Workshopper.Api.Sessions/Workshopper.Api.Sessions.csproj"
COPY . .
WORKDIR "/src/Workshopper.Api.Sessions"
RUN dotnet build "Workshopper.Api.Sessions.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Workshopper.Api.Sessions.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workshopper.Api.Sessions.dll"]
