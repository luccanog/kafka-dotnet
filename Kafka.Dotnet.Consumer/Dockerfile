#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Kafka.Dotnet.Consumer/Kafka.Dotnet.Consumer.csproj", "Kafka.Dotnet.Consumer/"]
RUN dotnet restore "Kafka.Dotnet.Consumer/Kafka.Dotnet.Consumer.csproj"
COPY . .
WORKDIR "/src/Kafka.Dotnet.Consumer"
RUN dotnet build "Kafka.Dotnet.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kafka.Dotnet.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kafka.Dotnet.Consumer.dll"]