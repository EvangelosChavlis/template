# PowerShell script to add EF migrations and update the database

# Exit on error
$ErrorActionPreference = "Stop"

# Move to the parent directory
Set-Location ..

# Prompting the user for the migration name
$MigrationName = Read-Host "Enter the migration name"

# Validating the migration name
if ([string]::IsNullOrWhiteSpace($MigrationName)) {
    Write-Host "Error: Migration name cannot be empty." -ForegroundColor Red
    exit 1
}

# Adding the migration
Write-Host "Adding the migration '$MigrationName'..."
dotnet ef migrations add $MigrationName -s server/src/Api/ -p server/src/Persistence/

# Updating the database
Write-Host "Updating the database..."
dotnet ef database update -s server/src/Api/ -p server/src/Persistence/

Write-Host "Database setup completed successfully." -ForegroundColor Green
