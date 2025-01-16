# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 8084

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar e restaurar as depend�ncias em uma �nica etapa para efici�ncia
COPY iFredApps.Lib/iFredApps.Lib.csproj iFredApps.Cloud/iFredApps.Lib/
COPY iFredApps.Cloud/iFredApps.Cloud.Api/iFredApps.Cloud.Api.csproj iFredApps.Cloud/iFredApps.Cloud.Api/
COPY iFredApps.Cloud/iFredApps.Cloud.Core/iFredApps.Cloud.Core.csproj iFredApps.Cloud/iFredApps.Cloud.Core/
COPY iFredApps.Cloud/iFredApps.Cloud.Data/iFredApps.Cloud.Data.csproj iFredApps.Cloud/iFredApps.Cloud.Data/
RUN dotnet restore iFredApps.Cloud/iFredApps.Cloud.Api/iFredApps.Cloud.Api.csproj

# Copiar o restante do c�digo-fonte
COPY . .

# Construir o projeto
WORKDIR /src/iFredApps.Cloud/iFredApps.Cloud.Api
RUN dotnet build -c Release -o /app/build

# Etapa de publica��o
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Etapa final (runtime)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "iFredApps.Cloud.Api.dll"]
