#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Projector.Elastic/Projector.Elastic/SimpleViewProjector.Elastic.csproj", "src/Projector.Elastic/Projector.Elastic/"]
COPY ["src/EventBus.Kafka.Abstraction/EventBus.Kafka.Abstraction.csproj", "src/EventBus.Kafka.Abstraction/"]
COPY ["src/Messages/Messages.csproj", "src/Messages/"]
COPY ["src/Settings/Settings.csproj", "src/Settings/"]
COPY ["src/Queries.Core/Queries.Core.csproj", "src/Queries.Core/"]
COPY ["src/DataAccess.Mongo/DataAccess.Mongo.csproj", "src/DataAccess.Mongo/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/DataAccess.Elastic/DataAccess.Elastic.csproj", "src/DataAccess.Elastic/"]
RUN dotnet restore "src/Projector.Elastic/Projector.Elastic/SimpleViewProjector.Elastic.csproj"
COPY . .
WORKDIR "/src/src/Projector.Elastic/Projector.Elastic"
RUN dotnet build "SimpleViewProjector.Elastic.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleViewProjector.Elastic.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleViewProjector.Elastic.dll"]