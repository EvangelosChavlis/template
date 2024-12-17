#!/bin/bash

# Parse command-line arguments
OBJ=false
CLEAN=false

while [[ "$#" -gt 0 ]]; do
    case $1 in
        --obj) OBJ=true ;;
        --clean) CLEAN=true ;;
    esac
    shift
done

# Navigate to the parent directory
cd ..

# Get the current directory path
DIR=$(pwd)

# Define the paths to clean
paths=(
    "server/src/Domain"
    "server/src/Application"
    "server/src/Persistence"
    "server/src/Api"
)

# Remove bin and obj directories and restore if required
for path in "${paths[@]}"; do
    bin_path="$path/bin"
    rm -rf "$bin_path"

    if [ "$OBJ" = true ]; then
        obj_path="$path/obj"
        rm -rf "$obj_path"
        dotnet restore "$path"
    fi
done

# If clean is not enabled, run the projects
if [ "$CLEAN" = false ]; then
    # Open new terminal windows to run the APIs and npm commands
    osascript <<EOF
        tell application "Terminal"
            do script "dotnet watch run --project $DIR/server/src/Api"
            do script "cd $DIR/client && npm run dev"
        end tell
EOF
fi

