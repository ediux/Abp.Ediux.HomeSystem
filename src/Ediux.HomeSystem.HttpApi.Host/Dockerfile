#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Ediux.HomeSystem.HttpApi.Host/Ediux.HomeSystem.HttpApi.Host.csproj", "src/Ediux.HomeSystem.HttpApi.Host/"]
COPY ["src/Ediux.HomeSystem.EntityFrameworkCore/Ediux.HomeSystem.EntityFrameworkCore.csproj", "src/Ediux.HomeSystem.EntityFrameworkCore/"]
COPY ["src/Ediux.HomeSystem.Domain/Ediux.HomeSystem.Domain.csproj", "src/Ediux.HomeSystem.Domain/"]
COPY ["src/Ediux.HomeSystem.Domain.Shared/Ediux.HomeSystem.Domain.Shared.csproj", "src/Ediux.HomeSystem.Domain.Shared/"]
COPY ["src/Ediux.HomeSystem.HttpApi/Ediux.HomeSystem.HttpApi.csproj", "src/Ediux.HomeSystem.HttpApi/"]
COPY ["src/Ediux.HomeSystem.Application.Contracts/Ediux.HomeSystem.Application.Contracts.csproj", "src/Ediux.HomeSystem.Application.Contracts/"]
COPY ["src/Ediux.HomeSystem.Application/Ediux.HomeSystem.Application.csproj", "src/Ediux.HomeSystem.Application/"]
RUN dotnet restore "src/Ediux.HomeSystem.HttpApi.Host/Ediux.HomeSystem.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Ediux.HomeSystem.HttpApi.Host"
RUN dotnet build "Ediux.HomeSystem.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ediux.HomeSystem.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ediux.HomeSystem.HttpApi.Host.dll"]