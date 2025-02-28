#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StoreService.Api.Gateway/StoreService.Api.Gateway.csproj", "StoreService.Api.Gateway/"]
RUN dotnet restore "StoreService.Api.Gateway/StoreService.Api.Gateway.csproj"
COPY . .
WORKDIR "/src/StoreService.Api.Gateway"
RUN dotnet build "StoreService.Api.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StoreService.Api.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoreService.Api.Gateway.dll"]