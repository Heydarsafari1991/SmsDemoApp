FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Ui/Adly.Api/Adly.Api.csproj", "src/Ui/Adly.Api/"]
COPY ["src/Infrastructure/SmsDemoApp.Infrastructure.Persistence/SmsDemoApp.Infrastructure.Persistence.csproj", "src/Infrastructure/SmsDemoApp.Infrastructure.Persistence/"]
COPY ["src/Core/SmsDemoApp.Application/SmsDemoApp.Application.csproj", "src/Core/SmsDemoApp.Application/"]
COPY ["src/Core/SmsDemoApp.Domain/SmsDemoApp.Domain.csproj", "src/Core/SmsDemoApp.Domain/"]
COPY ["src/Infrastructure/SmsDemoApp.Infrastructure.Identity/SmsDemoApp.Infrastructure.Identity.csproj", "src/Infrastructure/SmsDemoApp.Infrastructure.Identity/"]
COPY ["src/Infrastructure/SmsDemoApp.Infrastructure.CrossCutting/SmsDemoApp.Infrastructure.CrossCutting.csproj", "src/Infrastructure/SmsDemoApp.Infrastructure.CrossCutting/"]
COPY ["src/Ui/SmsDemoApp.WebFramework/SmsDemoApp.WebFramework.csproj", "src/Ui/SmsDemoApp.WebFramework/"]
RUN dotnet restore "src/Ui/Adly.Api/Adly.Api.csproj"
COPY . .
WORKDIR "/src/src/Ui/Adly.Api"
RUN dotnet build "Adly.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Adly.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adly.Api.dll"]
