#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Pokedex.Api/Pokedex.Api.csproj", "Pokedex.Api/"]
COPY ["Pokedex.Repository/Pokedex.Repository.csproj", "Pokedex.Repository/"]
COPY ["Pokedex.Domain/Pokedex.Domain.csproj", "Pokedex.Domain/"]
COPY ["Pokedex.Service/Pokedex.Service.csproj", "Pokedex.Service/"]
RUN dotnet restore "Pokedex.Api/Pokedex.Api.csproj"
COPY . .
WORKDIR "/src/Pokedex.Api"
RUN dotnet build "Pokedex.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokedex.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokedex.Api.dll"]