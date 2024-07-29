# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TechChallengeContatos.Web/TechChallengeContatos.Web.csproj", "TechChallengeContatos.Web/"]
COPY ["TechChallengeContatos.Infra/TechChallengeContatos.Infra.csproj", "TechChallengeContatos.Infra/"]
COPY ["TechChallengeContatos.Domain/TechChallengeContatos.Domain.csproj", "TechChallengeContatos.Domain/"]
COPY ["TechChallengeContatos.Service/TechChallengeContatos.Service.csproj", "TechChallengeContatos.Service/"]
RUN dotnet restore "TechChallengeContatos.Web/TechChallengeContatos.Web.csproj"
COPY . .
WORKDIR "/src/TechChallengeContatos.Web"
RUN dotnet build "TechChallengeContatos.Web.csproj" -c Release -o /app/build
RUN dotnet publish "TechChallengeContatos.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TechChallengeContatos.Web.dll"]
