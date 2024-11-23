#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/EncontroDeLutadores.API/EncontroDeLutadores.API.csproj", "src/EncontroDeLutadores.API/"]
COPY ["src/EncontroDeLutadores.Aplicacao/EncontroDeLutadores.Aplicacao.csproj", "src/EncontroDeLutadores.Aplicacao/"]
COPY ["src/EncontroDeLutadores.Dominio/EncontroDeLutadores.Dominio.csproj", "src/EncontroDeLutadores.Dominio/"]
COPY ["src/EncontroDeLutadores.Infra.RabbitMQ/EncontroDeLutadores.Infra.RabbitMQ.csproj", "src/EncontroDeLutadores.Infra.RabbitMQ/"]
COPY ["src/EncontroDeLutadores.Servico/EncontroDeLutadores.Servico.csproj", "src/EncontroDeLutadores.Servico/"]
COPY ["src/EncontroDeLutadores.Infra/EncontroDeLutadores.Infra.csproj", "src/EncontroDeLutadores.Infra/"]
RUN dotnet restore "./src/EncontroDeLutadores.API/EncontroDeLutadores.API.csproj"
COPY . .
WORKDIR "/src/src/EncontroDeLutadores.API"
RUN dotnet build "./EncontroDeLutadores.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EncontroDeLutadores.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EncontroDeLutadores.API.dll"]