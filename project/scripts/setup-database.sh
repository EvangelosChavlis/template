#!/bin/bash

# Script to add EF migrations and update the database

# Exit on error
set -e

cd ..

# Prompting the user for the migration name
echo "Enter the migration name:"
read -r migration_name

# Validating the migration name
if [ -z "$migration_name" ]; then
  echo "Error: Migration name cannot be empty."
  exit 1
fi

# Define migration names correctly
migration_data="${migration_name}_data"
migration_archive="${migration_name}_archive"

# Adding the migration for DataContext (saved in Migrations/Data)
echo "Adding migration '$migration_data' for DataContext..."
dotnet ef migrations add "$migration_data" --context DataContext -s server/src/Api/ -p server/src/Persistence/ --output-dir Migrations/Data

# Adding the migration for ArchiveContext (saved in Migrations/Archive)
echo "Adding migration '$migration_archive' for ArchiveContext..."
dotnet ef migrations add "$migration_archive" --context ArchiveContext -s server/src/Api/ -p server/src/Persistence/ --output-dir Migrations/Archive

# Updating the database for DataContext
echo "Updating the database for DataContext..."
dotnet ef database update --context DataContext -s server/src/Api/ -p server/src/Persistence/

# Updating the database for ArchiveContext
echo "Updating the database for ArchiveContext..."
dotnet ef database update --context ArchiveContext -s server/src/Api/ -p server/src/Persistence/

# Uncomment these lines if you need to drop the databases before running migrations
# echo "Dropping the database for DataContext..."
# dotnet ef database drop --context DataContext -s server/src/Api/ -p server/src/Persistence/ -f

# echo "Dropping the database for ArchiveContext..."
# dotnet ef database drop --context ArchiveContext -s server/src/Api/ -p server/src/Persistence/ -f

echo "Database setup completed successfully."
