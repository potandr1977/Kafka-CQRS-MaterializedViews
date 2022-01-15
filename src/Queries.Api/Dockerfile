#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Queries.Api/Queries.Api.csproj", "src/Queries.Api/"]
COPY ["src/DataAccess.Elastic/DataAccess.Elastic.csproj", "src/DataAccess.Elastic/"]
COPY ["src/Queries.Core/Queries.Core.csproj", "src/Queries.Core/"]
COPY ["src/Settings/Settings.csproj", "src/Settings/"]
COPY ["src/EventBus.Kafka/EventBus.Kafka.csproj", "src/EventBus.Kafka/"]
COPY ["src/EventBus.Kafka.Abstraction/EventBus.Kafka.Abstraction.csproj", "src/EventBus.Kafka.Abstraction/"]
COPY ["src/Queries.Application/Queries.Application.csproj", "src/Queries.Application/"]
RUN dotnet restore "src/Queries.Api/Queries.Api.csproj"
COPY . .
WORKDIR "/src/src/Queries.Api"
RUN dotnet build "Queries.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Queries.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Queries.Api.dll"]