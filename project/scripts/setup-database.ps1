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

# Define the folder path for migrations
$MigrationsFolderPath = "server/src/Persistence/Migrations"

# Delete the Migrations folder if it exists
if (Test-Path $MigrationsFolderPath) {
    Write-Host "Deleting the Migrations folder..."
    Remove-Item -Path $MigrationsFolderPath -Recurse -Force
} else {
    Write-Host "Migrations folder does not exist."
}

dotnet ef database drop --context DataContext -s server/src/Api/ -p server/src/Persistence/ -f
dotnet ef database drop --context ArchiveContext -s server/src/Api/ -p server/src/Persistence/ -f

# Define migration names correctly
$MigrationData = "${MigrationName}_data"
$MigrationArchive = "${MigrationName}_archive"

# Adding the migration for DataContext (saved in Migrations/Data)
Write-Host "Adding migration $MigrationData for DataContext..."
dotnet ef migrations add "$MigrationData" --context DataContext -s server/src/Api/ -p server/src/Persistence/ --output-dir Migrations/Data

# Adding the migration for ArchiveContext (saved in Migrations/Archive)
Write-Host "Adding migration $MigrationArchive for ArchiveContext..."
dotnet ef migrations add "$MigrationArchive" --context ArchiveContext -s server/src/Api/ -p server/src/Persistence/ --output-dir Migrations/Archive

# Updating the database for DataContext
Write-Host "Updating the database for DataContext..."
dotnet ef database update --context DataContext -s server/src/Api/ -p server/src/Persistence/

# Updating the database for ArchiveContext
Write-Host "Updating the database for ArchiveContext..."
dotnet ef database update --context ArchiveContext -s server/src/Api/ -p server/src/Persistence/

Write-Host "Database setup completed successfully." -ForegroundColor Green
