FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TechChallengeContatos.ConsumerService/TechChallengeContatos.ConsumerService.csproj", "TechChallengeContatos.ConsumerService/"]
COPY ["TechChallengeContatos.Infra/TechChallengeContatos.Infra.csproj", "TechChallengeContatos.Infra/"]
COPY ["TechChallengeContatos.Domain/TechChallengeContatos.Domain.csproj", "TechChallengeContatos.Domain/"]
RUN dotnet restore "TechChallengeContatos.ConsumerService/TechChallengeContatos.ConsumerService.csproj"
COPY . .
WORKDIR "/src/TechChallengeContatos.ConsumerService"
RUN dotnet build "TechChallengeContatos.ConsumerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TechChallengeContatos.ConsumerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallengeContatos.ConsumerService.dll"]
