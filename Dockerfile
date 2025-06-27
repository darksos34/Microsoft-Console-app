FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build
WORKDIR /App
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/aspnet:9.0@sha256:b4bea3a52a0a77317fa93c5bbdb076623f81e3e2f201078d89914da71318b5d8 AS base
WORKDIR /App
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ConsoleApp1.csproj", "./"]
RUN dotnet restore "ConsoleApp1.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "./ConsoleApp1.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ConsoleApp1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleApp1.dll"]
