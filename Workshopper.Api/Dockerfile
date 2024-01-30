FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Workshopper.Api/Workshopper.Api.csproj", "Workshopper.Api/"]
COPY ["Workshopper.Infrastructure/Workshopper.Infrastructure.csproj", "Workshopper.Infrastructure/"]
COPY ["Workshopper.Application/Workshopper.Application.csproj", "Workshopper.Application/"]
COPY ["Workshopper.Domain/Workshopper.Domain.csproj", "Workshopper.Domain/"]
COPY ["Workshopper.Api.Contracts/Workshopper.Api.Contracts.csproj", "Workshopper.Api.Contracts/"]
COPY ["Workshopper.Api.Common/Workshopper.Api.Common.csproj", "Workshopper.Api.Common/"]
RUN dotnet restore "Workshopper.Api/Workshopper.Api.csproj"
COPY . .
WORKDIR "/src/Workshopper.Api"
RUN dotnet build "Workshopper.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Workshopper.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workshopper.Api.dll"]
