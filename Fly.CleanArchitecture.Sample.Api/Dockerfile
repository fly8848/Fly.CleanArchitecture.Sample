FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Fly.CleanArchitecture.Sample.Api/Fly.CleanArchitecture.Sample.Api.csproj", "Fly.CleanArchitecture.Sample.Api/"]
RUN dotnet restore "Fly.CleanArchitecture.Sample.Api/Fly.CleanArchitecture.Sample.Api.csproj"
COPY . .
WORKDIR "/src/Fly.CleanArchitecture.Sample.Api"
RUN dotnet build "./Fly.CleanArchitecture.Sample.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fly.CleanArchitecture.Sample.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fly.CleanArchitecture.Sample.Api.dll"]
