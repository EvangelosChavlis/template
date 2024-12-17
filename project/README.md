# Weather App Documentation

This is a weather application built with **.NET 8** for the server project and **React**  with **TypeScript** for the client project. The app provides functionality to view, create, update, and manage weather forecasts and warnings.

## Table of Contents
1. [Project Structure](#project-structure)
2. [Client project](#client-project)
3. [Server project](#server-project)
4. [Development Setup](#development-setup)
5. [API Documentation](#api-documentation)

## Project Structure

```plaintext
weather-app/
│
├── client/                      # React client project
│   ├── models/                  # TypeScript models
│   └── src/
│       ├── modules/             # Pages and Components (View, Create, Update, Table)
│       ├── api.ts               # API calls using React-Query
│
├── docs/                        # Documentation files
│   ├── api.md                   # API documentation
│   ├── model.md                 # Models documentation
│   ├── services.md              # Services documentation
│   ├── tests.md                 # Testing documentation
│
├── scripts/                     # Utility scripts
│   ├── run-dev                  # Run the application locally
│   ├── run-tests                # Run the tests
│   ├── setup-database           # Setup the database
│
├── server/                      # .NET 8 server project
│   ├── src/
│   │   ├── Domain/
│   │   │   ├── Models           # Domain models
│   │   │   ├── Dto              # Data transfer objects
│   │   │   └── Extensions       # Extensions
│   │   ├── Application/
│   │   │   ├── Services         # Business logic services
│   │   │   ├── Interfaces       # Service interfaces
│   │   │   ├── Mappings         # Object mapping logic
│   │   │   ├── Includes         # Include logic for queries
│   │   │   └── Filters          # Query filters
│   │   ├── Persistence/
│   │   │   ├── Configurations   # DB configurations
│   │   │   ├── Contexts         # Database contexts
│   │   └── Api/
│   │       └── Controllers      # API controllers
│   ├── test/
│   │   ├── Domain.Unit.Tests    # Unit tests for Domain layer
│   │   ├── Application.Unit.Tests # Unit tests for Application layer
│   │   ├── Persistence.Unit.Tests # Unit tests for Persistence layer
│   │   └── Api.Integration.Tests # Integration tests for API layer
│   ├── server.sln              # .NET solution file
```

## Client project

The client project of the weather app is built with **React** and **TypeScript**. It uses popular libraries like **React-Query** for API calls and **React-Bootstrap** for UI components.

- **React-Query**: Used for data fetching and caching to provide a smooth and efficient user experience by minimizing redundant network requests.
- **React-Bootstrap**: Provides responsive, Bootstrap-styled components for building a modern, mobile-friendly UI.
- **Toastify**: A library for showing notifications, making it easy to display success or error messages.

### File Structure

- **`models/`**: Contains TypeScript models for various entities such as:
  - `Forecast`
  - `Warning`

- **`src/modules/`**: Contains pages and components organized by functionality:
  - **`View/`**: Displays data in different formats.
  - **`Create/`**: Allows users to create new entries, such as adding a new forecast or warning.
  - **`Update/`**: Lets users update existing data.
  - **`Table/`**: Displays data in a tabular format for better readability.
  - **`api.ts`**: Handles all API calls using React-Query, centralizing and simplifying data fetching logic for various parts of the app.

---

## Server project

The server project of the weather app is developed with **.NET 8** and follows a layered architecture for organizing code.

### Domain Layer
The **Domain Models** represent the core data structures of the application, defining entities such as `Forecast`, `Warning`, and others. These models are the foundation of the app’s logic.

### Application Layer
The **Application Layer** contains the business logic and services that interact with the domain models. It includes:
- **Services**: Handles core operations such as creating, reading, updating, and deleting data.
- **Interfaces**: Defines the contract for services to be implemented by concrete classes.
- **Mappings**: Contains logic for mapping between DTOs and domain models.

### Persistence Layer
The **Persistence Layer** manages the interaction with the database, including:
- **Contexts**: Defines the DbContext for accessing and manipulating data in the database.
- **Configurations**: Contains database configuration settings.

## Development Setup

### Prerequisites

Before starting the development setup, ensure you have the following installed:

- **.NET 8 SDK**: This is required to build and run the server project of the app. You can download it from the official .NET website: [Download .NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
  
- **Node.js**: This is needed to run the React application (client project). You can download it from the official Node.js website: [Download Node.js](https://nodejs.org/).
  
- **npm** or **yarn**: These are package managers used to install and manage dependencies for the React app. You can install npm with Node.js or choose to install yarn:  
  - **npm**: It comes bundled with Node.js.
  - **yarn**: You can install it via npm with the command `npm install -g yarn` or follow the installation instructions on the [Yarn website](https://yarnpkg.com/).

# API Documentation

The API is designed to handle all operations related to weather forecasts and warnings. It provides endpoints to manage weather data, including viewing, creating, updating, and deleting forecasts and warnings.

- **[API Endpoints](docs/api.md)**: Learn about the endpoints of application.
- **[Testing](docs/tests.md)**: Learn about the testing strategies and how to run tests for and backend.
- **[Models](docs/model.md)**: Detailed overview of the data models used in the application.
- **[Services](docs/services.md)**: Explanation of the services that handle business logic.


This `README.md` template provides clear sections for both client project and server project setup, API documentation, development instructions, and testing guidelines. It also offers links to more detailed documentation for API endpoints, models, and services.