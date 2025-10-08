FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bank.Management.Api/Bank.Management.Api.csproj", "Bank.Management.Api/"]
COPY ["Bank.Management.Application/Bank.Management.Application.csproj", "Bank.Management.Application/"]
COPY ["Bank.Management.Infrastructure/Bank.Management.Infrastructure.csproj", "Bank.Management.Infrastructure/"]
COPY ["Bank.Management.Domain/Bank.Management.Domain.csproj", "Bank.Management.Domain/"]
RUN dotnet restore "Bank.Management.Api/Bank.Management.Api.csproj"
COPY . .
WORKDIR "/src/Bank.Management.Api"
RUN dotnet build "./Bank.Management.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Bank.Management.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bank.Management.Api.dll"]
