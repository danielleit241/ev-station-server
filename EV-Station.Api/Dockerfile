FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EV-Station.Api/EV-Station.Api.csproj", "EV-Station.Api/"]
COPY ["EV-Station.Application/EV-Station.Application.csproj", "EV-Station.Application/"]
COPY ["EV-Station.Domain/EV-Station.Domain.csproj", "EV-Station.Domain/"]
COPY ["EV-Station.Infrastructure/EV-Station.Infrastructure.csproj", "EV-Station.Infrastructure/"]
RUN dotnet restore "./EV-Station.Api/EV-Station.Api.csproj"
COPY . .
WORKDIR "/src/EV-Station.Api"
RUN dotnet build "./EV-Station.Api.csproj" -c Release -o /app/build
RUN dotnet publish "./EV-Station.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
RUN groupadd -r appuser && useradd -r -g appuser appuser && chown -R appuser:appuser /app
USER appuser

ENTRYPOINT ["dotnet", "EV-Station.Api.dll"]
