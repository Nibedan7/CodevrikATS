# 1. Base image with ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# 2. Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything into the container
COPY . .

# Build and publish to /app/publish
RUN dotnet publish CdplATS.API.csproj -c Release -o /app/publish

# 3. Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CdplATS.Api.dll"]
