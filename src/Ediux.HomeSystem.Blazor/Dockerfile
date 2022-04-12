#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
ARG BUILDMODE=Release
COPY ["NuGet.Config", "."]
COPY ["src/Ediux.HomeSystem.Blazor/Ediux.HomeSystem.Blazor.csproj", "src/Ediux.HomeSystem.Blazor/"]
COPY ["src/Ediux.HomeSystem.HttpApi.Client/Ediux.HomeSystem.HttpApi.Client.csproj", "src/Ediux.HomeSystem.HttpApi.Client/"]
COPY ["src/Ediux.HomeSystem.Application.Contracts/Ediux.HomeSystem.Application.Contracts.csproj", "src/Ediux.HomeSystem.Application.Contracts/"]
COPY ["src/Ediux.HomeSystem.Domain.Shared/Ediux.HomeSystem.Domain.Shared.csproj", "src/Ediux.HomeSystem.Domain.Shared/"]
RUN dotnet restore "src/Ediux.HomeSystem.Blazor/Ediux.HomeSystem.Blazor.csproj"
COPY . .
WORKDIR "/src/src/Ediux.HomeSystem.Blazor"
RUN dotnet build "Ediux.HomeSystem.Blazor.csproj" -c ${BUILDMODE} -o /app/build

FROM build AS publish
RUN dotnet publish "Ediux.HomeSystem.Blazor.csproj" -c ${BUILDMODE} -o /app/publish

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
ENV RemoteServices_BaseUrl=https://www.ediuxnet.idv.tw:44318
ENTRYPOINT ["dotnet", "Ediux.HomeSystem.Blazor.dll"]
