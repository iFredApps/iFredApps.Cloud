# Usar imagem base do .NET para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar todos os arquivos do projeto
COPY . .

# Restaurar depend�ncias
WORKDIR /app/iFredApps.Cloud/iFredApps.Cloud.Api
RUN dotnet restore

# Publicar o aplicativo
RUN dotnet publish -c Release -o /app/out

# Criar a imagem final para execu��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "iFredApps.Cloud.Api.dll"]
