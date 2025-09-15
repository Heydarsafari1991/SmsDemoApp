# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SmsDemoApp.Worker/SmsDemoApp.Worker.csproj", "SmsDemoApp.Worker/"]
COPY ["src/Infrastructure/SmsDemoApp.Infrastructure.CrossCutting/SmsDemoApp.Infrastructure.CrossCutting.csproj", "src/Infrastructure/SmsDemoApp.Infrastructure.CrossCutting/"]
COPY ["src/Core/SmsDemoApp.Application/SmsDemoApp.Application.csproj", "src/Core/SmsDemoApp.Application/"]
COPY ["src/Core/SmsDemoApp.Domain/SmsDemoApp.Domain.csproj", "src/Core/SmsDemoApp.Domain/"]
COPY ["src/Infrastructure/SmsDemoApp.Infrastructure.Identity/SmsDemoApp.Infrastructure.Identity.csproj", "src/Infrastructure/SmsDemoApp.Infrastructure.Identity/"]
COPY ["src/Infrastructure/SmsDemoApp.Infrastructure.Persistence/SmsDemoApp.Infrastructure.Persistence.csproj", "src/Infrastructure/SmsDemoApp.Infrastructure.Persistence/"]
COPY ["src/Ui/SmsDemoApp.WebFramework/SmsDemoApp.WebFramework.csproj", "src/Ui/SmsDemoApp.WebFramework/"]
RUN dotnet restore "./SmsDemoApp.Worker/SmsDemoApp.Worker.csproj"
COPY . .
WORKDIR "/src/SmsDemoApp.Worker"
RUN dotnet build "./SmsDemoApp.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SmsDemoApp.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmsDemoApp.Worker.dll"]