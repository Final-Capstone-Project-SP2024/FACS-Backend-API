#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FireDetection.Backend.API/FireDetection.Backend.API.csproj", "FireDetection.Backend.API/"]
COPY ["FireDetection.Backend.Infrastructure/FireDetection.Backend.Infrastructure.csproj", "FireDetection.Backend.Infrastructure/"]
COPY ["FireDetection.Backend.Domain/FireDetection.Backend.Domain.csproj", "FireDetection.Backend.Domain/"]
RUN dotnet restore "./FireDetection.Backend.API/./FireDetection.Backend.API.csproj"
COPY . .
WORKDIR "/src/FireDetection.Backend.API"
RUN dotnet build "./FireDetection.Backend.API.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FireDetection.Backend.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FireDetection.Backend.API.dll"]