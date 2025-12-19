# syntax=docker/dockerfile:1.20
ARG DOTNET_VERSION=9.0

FROM mcr.microsoft.com/dotnet/aspnet:$DOTNET_VERSION-noble-chiseled AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:$DOTNET_VERSION-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["dotnet/sample-api/aspnetcoreapp/aspnetcoreapp.csproj", "dotnet/sample-api/aspnetcoreapp/"]

RUN dotnet restore "dotnet/sample-api/aspnetcoreapp/aspnetcoreapp.csproj"
COPY dotnet/ dotnet/
WORKDIR "/src/dotnet/sample-api/aspnetcoreapp"
RUN dotnet build "aspnetcoreapp.csproj" -c $BUILD_CONFIGURATION -o /app/build
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "aspnetcoreapp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aspnetcoreapp.dll"]
