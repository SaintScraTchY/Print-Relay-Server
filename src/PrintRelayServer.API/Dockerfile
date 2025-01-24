FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PrintRelayServer.API/PrintRelayServer.API.csproj", "PrintRelayServer.API/"]
RUN dotnet restore "PrintRelayServer.API/PrintRelayServer.API.csproj"
COPY . .
WORKDIR "/src/PrintRelayServer.API"
RUN dotnet build "PrintRelayServer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PrintRelayServer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrintRelayServer.API.dll"]
