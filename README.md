# Clean Architecture Template

This is a template for setting up a Clean Architecture project with .NET, React, and TypeScript. It aims to provide a well-structured, modular, and scalable architecture for modern web applications.

## Features

- **Clean Architecture**: A layered approach to software architecture that promotes separation of concerns and maintainability.
- **.NET 8.0 server project**: A robust and flexible backend using the latest version of .NET.
- **React & TypeScript client project**: A modern, type-safe front-end using React and TypeScript.

## Installation

To install the template globally on your machine, use the following command:

`dotnet new install Clean.Architecture.Template.NetReactTS::1.0.0`

## Project Creation
To create a new project using this template:

### Generate a New Project
Run the following command to generate a project:


`dotnet new cleanArch --name Your-Project-Name`

Replace Your-Project-Name with the desired name of your project.

This will generate a new project with a complete folder structure, pre-configured to follow Clean Architecture principles.
   

## Project Structure

The template includes the following structure:

- **server project**: 
  - `src/Api`: The API layer, including Controllers, Services, and API logic.
  - `src/Application`: Application services and business logic, handling use cases and orchestration.
  - `src/Domain`: Core domain models, entities, and domain logic.
  - `src/Persistence`: Data access layer, typically using EF Core or other ORM solutions.
  - `test/Api.Integration.Tests`: Integration tests for the API layer, ensuring the API endpoints function correctly.
  - `test/Application.Unit.Tests`: Unit tests for application services and business logic.
  - `test/Domain.Unit.Tests`: Unit tests for the domain models and logic.
  - `test/Persistence.Unit.Tests`: Unit tests for the persistence layer, focusing on data access and interactions with databases.
- **client project**: 
  - `client/src`: React and TypeScript-based client
- **Documentation**: 
  - `docs`: Project documentation, guides, and notes
- **Scripts**: 
  - `scripts`: Utility scripts for common tasks

## Getting Started

1. Clone the repository or use the template to create your own project.
   
   ```bash
   git clone https://github.com/EvangelosChavlis/template.git

2. Import packages for client project.

   ````bash 
   cd project/client
   npm install 
   cd ..
3. Run project
   ```bash
   cd scripts
   ./run-dev.sh (or Powershell file)
3. Happy coding!

## Author
Evangelos Chavlis  

[GitHub Profile](https://github.com/EvangelosChavlis)

[LinkedIn Profile](https://www.linkedin.com/in/vagelis-chavlis/)