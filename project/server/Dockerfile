# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy everything to the build context
COPY . .

# Restore dependencies
RUN dotnet restore src/Api

# Build the application
RUN dotnet build src/Api -c Release -o /build

# Publish the application
RUN dotnet publish src/Api -c Release -o /publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /publish .

# Expose the app port
EXPOSE 80

# Set the entry point for the container
CMD ["dotnet", "Api.dll"]
