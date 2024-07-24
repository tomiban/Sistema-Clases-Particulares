# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Instalar wkhtmltopdf y libgdiplus
USER root
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    libgdiplus \
    wkhtmltopdf \
    && apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TeddyMVC.csproj", "."]
RUN dotnet restore "./TeddyMVC.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "./TeddyMVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TeddyMVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeddyMVC.dll"]
