# Stage 1: Build the API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the entire solution
COPY ../CdplATS.Entity ../CdplATS.Entity
COPY . .

# Publish API
RUN dotnet publish CdplATS.API.csproj -c Release -o /app/publish

# Stage 2: Runtime for API
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CdplATS.API.dll"]
