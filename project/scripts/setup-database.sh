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

# Adding the migration
echo "Adding the migration '$migration_name'..."
dotnet ef migrations add "$migration_name" -s server/src/Api/ -p server/src/Persistence/

# Updating the database
echo "Updating the database..."
dotnet ef database update -s server/src/Api/ -p server/src/Persistence/

echo "Database setup completed successfully."
