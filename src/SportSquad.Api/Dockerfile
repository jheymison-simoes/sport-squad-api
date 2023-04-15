FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SportSquad.Api/SportSquad.Api.csproj", "SportSquad.Api/"]
COPY ["src/SportSquad.Business/SportSquad.Business.csproj", "SportSquad.Business/"]
COPY ["src/SportSquad.Domain/SportSquad.Domain.csproj", "SportSquad.Domain/"]
COPY ["src/SportSquad.Data/SportSquad.Data.csproj", "SportSquad.Data/"]
RUN dotnet restore "SportSquad.Api/SportSquad.Api.csproj"
COPY . .
WORKDIR "src/SportSquad.Api"
RUN dotnet build "SportSquad.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SportSquad.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportSquad.Api.dll"]
