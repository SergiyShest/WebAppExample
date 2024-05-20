
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 5293

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebService/WebService.csproj", "WebService/"]
COPY ["WebService.BLL/WebService.BLL.csproj", "WebService.BLL/"]
COPY ["WebService.Core/WebService.Core.csproj", "WebService.Core/"]
COPY ["WebService.DAL/WebService.DAL.csproj", "WebService.DAL/"]
RUN dotnet restore "./WebService/WebService.csproj"
COPY . .
WORKDIR "/src/WebService"
RUN dotnet build "./WebService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebService.dll"]