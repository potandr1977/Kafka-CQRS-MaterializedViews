#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Commands.Api/Commands.Api/Commands.Api.csproj", "src/Commands.Api/Commands.Api/"]
COPY ["src/EventBus.Kafka.Abstraction/EventBus.Kafka.Abstraction.csproj", "src/EventBus.Kafka.Abstraction/"]
COPY ["src/Messages/Messages.csproj", "src/Messages/"]
COPY ["src/Settings/Settings.csproj", "src/Settings/"]
COPY ["src/DataAccess.Mongo/DataAccess.Mongo.csproj", "src/DataAccess.Mongo/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Commands.Application/Commands.Application.csproj", "src/Commands.Application/"]
COPY ["src/Business/Business.csproj", "src/Business/"]
RUN dotnet restore "src/Commands.Api/Commands.Api/Commands.Api.csproj"
COPY . .
WORKDIR "/src/src/Commands.Api/Commands.Api"
RUN dotnet build "Commands.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Commands.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Commands.Api.dll"]