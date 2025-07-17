# Stage 1: Build the API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything from the root where .sln and all projects exist
COPY . .

# Publish the API project specifically
RUN dotnet publish CodeVrikATS.API/CodeVrikATS.API.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CodeVrikATS.API.dll"]
