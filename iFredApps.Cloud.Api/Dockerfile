# Usar imagem base do .NET para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos do projeto e dependências
COPY iFredApps.Cloud.Api/*.csproj iFredApps.Cloud.Api/
COPY iFredApps.Cloud.Core/*.csproj iFredApps.Cloud.Core/
COPY iFredApps.Cloud.Data/*.csproj iFredApps.Cloud.Data/
COPY iFredApps.Lib/*.csproj iFredApps.Lib/

# Restaurar dependências
RUN dotnet restore

# Copiar todo o código-fonte do projeto
COPY iFredApps.Cloud.Api/ iFredApps.Cloud.Api/
COPY iFredApps.Cloud.Core/ iFredApps.Cloud.Core/
COPY iFredApps.Cloud.Data/ iFredApps.Cloud.Data/
COPY iFredApps.Lib/ iFredApps.Lib/

# Publicar o aplicativo
WORKDIR /app/iFredApps.Cloud.Api
RUN dotnet publish -c Release -o out

# Criar a imagem final para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/iFredApps.Cloud.Api/out .
ENTRYPOINT ["dotnet", "iFredApps.Cloud.Api.dll"]
