FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

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



# # Use the official image as a parent image
# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# # Set the working directory inside the container
# WORKDIR /app

# # Copy the csproj file and restore any dependencies (via NUGET)
# COPY *.csproj ./
# RUN dotnet restore

# # Copy the project files into the container
# COPY . ./

# # Publish the application to the /app/out directory
# RUN dotnet publish -c Release -o out

# # Generate the runtime image
# FROM mcr.microsoft.com/dotnet/aspnet:6.0
# WORKDIR /app
# COPY --from=build-env /app/out .
# ENTRYPOINT ["dotnet", "RetailMaster.dll"]
