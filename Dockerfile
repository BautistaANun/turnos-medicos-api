# Etapa 1: compilar y publicar con el SDK completo
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Primero solo los .csproj: si no cambian, Docker reutiliza la capa del restore
COPY src/TurnosMedicos.Domain/TurnosMedicos.Domain.csproj src/TurnosMedicos.Domain/
COPY src/TurnosMedicos.Application/TurnosMedicos.Application.csproj src/TurnosMedicos.Application/
COPY src/TurnosMedicos.Infrastructure/TurnosMedicos.Infrastructure.csproj src/TurnosMedicos.Infrastructure/
COPY src/TurnosMedicos.Api/TurnosMedicos.Api.csproj src/TurnosMedicos.Api/
RUN dotnet restore src/TurnosMedicos.Api/TurnosMedicos.Api.csproj

COPY src/ src/
RUN dotnet publish src/TurnosMedicos.Api/TurnosMedicos.Api.csproj -c Release -o /app/publish --no-restore

# Etapa 2: imagen final, solo con el runtime de ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "TurnosMedicos.Api.dll"]
