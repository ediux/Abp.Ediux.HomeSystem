#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
ARG BUILDMODE=Release
COPY ["NuGet.Config", "."]
COPY ["src/Ediux.HomeSystem.Web/Ediux.HomeSystem.Web.csproj", "src/Ediux.HomeSystem.Web/"]
COPY ["src/Ediux.HomeSystem.EntityFrameworkCore/Ediux.HomeSystem.EntityFrameworkCore.csproj", "src/Ediux.HomeSystem.EntityFrameworkCore/"]
COPY ["src/Ediux.HomeSystem.Domain/Ediux.HomeSystem.Domain.csproj", "src/Ediux.HomeSystem.Domain/"]
COPY ["src/Ediux.HomeSystem.Domain.Shared/Ediux.HomeSystem.Domain.Shared.csproj", "src/Ediux.HomeSystem.Domain.Shared/"]
COPY ["src/Ediux.HomeSystem.HttpApi/Ediux.HomeSystem.HttpApi.csproj", "src/Ediux.HomeSystem.HttpApi/"]
COPY ["src/Ediux.HomeSystem.Application.Contracts/Ediux.HomeSystem.Application.Contracts.csproj", "src/Ediux.HomeSystem.Application.Contracts/"]
COPY ["src/Ediux.HomeSystem.Application/Ediux.HomeSystem.Application.csproj", "src/Ediux.HomeSystem.Application/"]
RUN dotnet restore "src/Ediux.HomeSystem.Web/Ediux.HomeSystem.Web.csproj"
COPY . .
WORKDIR "/src/src/Ediux.HomeSystem.Web"
RUN dotnet build "Ediux.HomeSystem.Web.csproj" -c ${BUILDMODE} -o /app/build


FROM build AS publish
RUN dotnet publish "Ediux.HomeSystem.Web.csproj" -c ${BUILDMODE} -o /app/publish

FROM base AS final
WORKDIR "/"
RUN mkdir /https
WORKDIR /https
RUN ln -s /app/aspnetapp.pfx aspnetapp.pfx
WORKDIR /app
COPY --from=publish /app/publish .
ENV SQLServerHost=sqlserver
ENV DBUser=sa
ENV DBPassword=Ediux@2019
ENV DBName=HomeSystem
ENV DBConnTimeout=20
ENV PFXPassword=Ediux@2019
ENV PFXPath=/https/aspnetapp.pfx
ENV DEVINRELEASE=N
ENV ASPNETCORE_URLS=https://+
ENV ASPNETCORE_HTTPS_PORT=44311
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=$PFXPath
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=$PFXPassword
ENTRYPOINT ["dotnet", "Ediux.HomeSystem.Web.dll"]
