#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Projector.Elastic/Projector.Elastic.csproj", "src/Projector.Elastic/"]
COPY ["src/DataAccess.Elastic/DataAccess.Elastic.csproj", "src/DataAccess.Elastic/"]
COPY ["src/Settings/Settings.csproj", "src/Settings/"]
COPY ["src/EventBus.Kafka/EventBus.Kafka.csproj", "src/EventBus.Kafka/"]
COPY ["src/EventBus.Kafka.Abstraction/EventBus.Kafka.Abstraction.csproj", "src/EventBus.Kafka.Abstraction/"]
COPY ["src/Queries.Core/Queries.Core.csproj", "src/Queries.Core/"]
RUN dotnet restore "src/Projector.Elastic/Projector.Elastic.csproj"
COPY . .
WORKDIR "/src/src/Projector.Elastic"
RUN dotnet build "Projector.Elastic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Projector.Elastic.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projector.Elastic.dll"]