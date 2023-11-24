FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5043

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RetailMaster/RetailMaster.csproj", "RetailMaster/"]
RUN dotnet restore "RetailMaster/RetailMaster.csproj"
COPY . .
WORKDIR "/src/RetailMaster"
RUN dotnet build "RetailMaster.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RetailMaster.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RetailMaster.dll"]
