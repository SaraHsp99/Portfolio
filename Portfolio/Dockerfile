FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Portfolio/Portfolio.Web.csproj", "Portfolio/"]
COPY ["Portfolio.EntitiyFramework/Portfolio.Infrastructure.csproj", "Portfolio.EntitiyFramework/"]
COPY ["Portfolio.Core/Portfolio.Core.csproj", "Portfolio.Core/"]
COPY ["Portfolio.IOC/Portfolio.IOC.csproj", "Portfolio.IOC/"]
COPY ["Portfolio.Provider/Portfolio.Provider.csproj", "Portfolio.Provider/"]
RUN dotnet restore "Portfolio/Portfolio.Web.csproj"
COPY . .
WORKDIR "/src/Portfolio"
RUN dotnet build "Portfolio.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Portfolio.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portfolio.Web.dll"]
