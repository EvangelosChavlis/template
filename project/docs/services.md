# API Services Documentation

This documentation provides detailed descriptions of the various services available in the system. Each service is designed to handle specific functionalities, such as managing user roles, handling weather forecasts, logging errors, and collecting telemetry data. This document serves as a comprehensive guide to understanding the services and their respective operations.

The services are structured in a way to facilitate scalability, maintainability, and ease of use, with clear API responses, robust error handling, and efficient database interactions. Below, you’ll find a list of all the services available in the system along with their functionalities.

## Table of Contents

1. [RoleService Documentation](#roleservice-documentation)
2. [UserService Documentation](#userservice-documentation)
3. [DataService Documentation](#dataservice-documentation)
4. [ErrorsService Documentation](#errorsservice-documentation)
5. [TelemetryService Documentation](#telemetryservice-documentation)
6. [ForecastsService Documentation](#forecastservice-documentation)
7. [WarningsService Documentation](#warningsservice-documentation)

Each section contains a detailed explanation of the service's purpose, its key operations, and how it interacts with the data layer. You can navigate to any of the services above for a more in-depth look.

# RoleService Documentation

The `RoleService` class is responsible for managing roles in the system. It provides various methods to perform CRUD operations, activate or deactivate roles, and initialize roles in bulk. The service leverages the `RoleManager` provided by ASP.NET Identity for role management.

---

### **GetRolesService**
Retrieves a list of all roles in the system.

- **Functionality:** 
  - Fetches all roles using the `RoleManager`.
  - Maps the roles to their corresponding DTOs (`ItemRoleDto`) for external use.
- **Use Case:** Displaying a list of roles in an administrative interface.

---

### **GetRoleByIdService**
Fetches the details of a specific role by its ID.

- **Functionality:** 
  - Looks up a role by ID.
  - Throws a `CustomException` if the role is not found.
  - Maps the role to its DTO representation.
- **Use Case:** Retrieving detailed information about a single role for editing or viewing.

---

### **CreateRoleService**
Creates a new role in the system.

- **Functionality:** 
  - Checks if a role with the specified name already exists.
  - Maps the provided DTO to a role entity.
  - Creates the role using `RoleManager`.
  - Throws a `CustomException` if the creation fails.
- **Use Case:** Adding new roles to the system, such as creating custom roles for specific user groups.

---

### **InitializeRolesService**
Initializes multiple roles in bulk.

- **Functionality:** 
  - Iterates over a list of role DTOs.
  - Calls `CreateRoleService` for each DTO.
  - Aggregates success messages for all roles.
- **Use Case:** Setting up initial roles during application deployment or resetting roles in bulk.

---

### **UpdateRoleService**
Updates an existing role.

- **Functionality:** 
  - Finds the role by its ID.
  - Throws a `CustomException` if the role is not found.
  - Updates the role's properties using the provided DTO.
  - Saves the changes using `RoleManager`.
  - Throws a `CustomException` if the update fails.
- **Use Case:** Modifying role properties, such as changing a role's description or status.

---

### **ActivateRoleService**
Activates a role in the system.

- **Functionality:** 
  - Finds the role by its ID.
  - Checks if the role is already active.
  - Sets the `IsActive` property to `true`.
  - Saves the changes using `RoleManager`.
  - Throws a `CustomException` if the operation fails.
- **Use Case:** Enabling roles that were previously deactivated.

---

### **DeactivateRoleService**
Deactivates a role in the system.

- **Functionality:** 
  - Finds the role by its ID.
  - Checks if the role is already inactive.
  - Sets the `IsActive` property to `false`.
  - Saves the changes using `RoleManager`.
  - Throws a `CustomException` if the operation fails.
- **Use Case:** Disabling roles that are no longer needed or are under review.

---

### **DeleteRoleService**
Deletes a role from the system.

- **Functionality:** 
  - Finds the role by its ID.
  - Throws a `CustomException` if the role is not found.
  - Deletes the role using `RoleManager`.
  - Throws a `CustomException` if the deletion fails.
- **Use Case:** Removing roles that are obsolete or no longer required.

---

# UserService Documentation

## Overview

The `UserService` class provides various services for user management, authentication, and authorization. It interacts with ASP.NET Core's `UserManager`, `SignInManager`, and `RoleManager` to perform operations such as user registration, login, password management, user activation, and more. It also supports JWT token generation and 2FA (Two-Factor Authentication).

---

## GetUsersService

This service retrieves a list of users from the database, with optional filtering, sorting, and pagination. It uses the `ICommonRepository` for fetching paged results and applies the necessary mappings to return a list of users in a `ListItemUserDto` format. The method also supports sorting based on default or user-provided criteria.

---

## GetUserByIdService

This service retrieves a user by their ID from the database. It includes the user’s roles and maps the user data to an `ItemUserDto` format. If the user is not found, a `CustomException` is thrown with a 404 status code.

---

## RegisterUserService

The `RegisterUserService` handles user registration by first checking if the provided email or username already exists. It creates a new user using the provided `UserDto`, then stores the user with the `UserManager`. If the registration fails, it returns an error message.

---

## InitializeUsersService

This service allows for batch user registration. It iterates over a list of `UserDto` objects, invoking the `RegisterUserService` method for each user. It returns a summary message for the registration process, including any successes or failures.

---

## LoginUserService

This service performs user authentication. It verifies the user's credentials by checking the provided username and password using `UserManager`. Upon successful login, a JWT token is generated and returned along with the `AuthenticatedUserDto` containing the user's details.

---

## LogoutUserService

The `LogoutUserService` logs the user out of the application by calling `SignOutAsync` on the `SignInManager`. It returns a success message upon successful logout.

---

## RefreshTokenService

This service allows for refreshing expired JWT tokens. It validates the provided token and generates a new token if the user is found and the token is valid. If the token is invalid, it throws a `CustomException`.

---

## ForgotPasswordService

The `ForgotPasswordService` generates a password reset token for a user based on their email address. This token can then be sent to the user via email (email service must be implemented separately). It returns a success message upon successfully generating the reset token.

---

## ResetPasswordService

This service allows users to reset their password using a valid reset token. It checks the validity of the token and, if valid, updates the user’s password. If the password reset fails, it returns a list of error messages.

---

## GeneratePasswordService

This service generates a new random password for a user. It uses the `UserManager` to generate a password reset token, resets the user’s password, and updates the user model with the new password.

---

## Enable2FAService

The `Enable2FAService` enables two-factor authentication (2FA) for a user. It generates a 2FA token and sends it to the user via email. This service requires email integration for token delivery.

---

## Verify2FAService

This service verifies a user’s 2FA token. If the token is valid, it enables 2FA for the user in the `UserManager`. It returns a success message upon successful verification.

---

## UpdateUserService

This service updates a user's information using a provided `UserDto`. It checks if the user exists, then applies the updates and saves them to the database using `UserManager`. If the update fails, it returns an error message.

---

## ActivateUserService

The `ActivateUserService` activates a user account. If the user is already active, it throws an exception. Otherwise, it updates the user’s `IsActive` status and returns a success message upon activation.

---

## DeactivateUserService

This service deactivates a user account by setting the `IsActive` flag to `false`. If the user is already deactivated, it throws an exception. The updated user state is saved to the database.

---

## LockUserService

The `LockUserService` locks a user’s account by enabling the lockout feature in the `UserManager`. If the user is already locked, it throws an exception. A success message is returned upon successful locking.

---

## UnlockUserService

This service unlocks a user’s account by disabling the lockout feature. If the user is not locked, it throws an exception. The update is saved to the database, and a success message is returned.

---

## ConfirmEmailUserService

This service confirms the email address of a user by setting the `EmailConfirmed` flag to `true`. If the email is already confirmed, it throws an exception. A success message is returned upon confirmation.

---

## ConfirmPhoneNumberUserService

The `ConfirmPhoneNumberUserService` confirms a user's phone number. It updates the `PhoneNumberConfirmed` flag in the user model and returns a success message if successful.

---

## ConfirmMobilePhoneNumberUserService

This service confirms the user’s mobile phone number. If successful, it updates the `MobilePhoneNumberConfirmed` flag and returns a success message.

---

## DeleteUserService

The `DeleteUserService` deletes a user from the database. It ensures the user exists, then calls `DeleteAsync` on the `UserManager`. If the deletion is successful, it returns a success message.

---

## AssignRoleToUserService

This service assigns a role to a user by adding the role to the user’s profile. If the role or user is not found, it throws an exception. Upon success, a message indicating the role assignment is returned.

---

## Private Methods

### GenerateJwtToken

This private method generates a JWT token for the user, including claims like the user’s email and ID. It signs the token using the secret key and returns the token as a string.

### GetPrincipalFromExpiredToken

This private method validates and extracts the claims from an expired JWT token. It is used for refreshing tokens and checking token validity.

### GeneratePassword

This private method generates a random password of a specified length, ensuring it includes lowercase letters, uppercase letters, digits, and special characters.

---

## Conclusion

The `UserService` class provides a wide range of user management features. Each service ensures that operations like registration, authentication, user profile updates, password management, and role assignments are executed securely, with clear exception handling and feedback for each operation.


# DataService Documentation

The `DataService` class is part of a backend application that provides functionality for seeding and clearing data. This service interacts with various other services to manage users, roles, warnings, and forecasts. The following services are used within `DataService`:

## **RoleService**

### Description:
The `RoleService` is responsible for managing user roles in the application. It provides functionality for initializing predefined roles such as "User", "Manager", and "Administrator". In the `SeedDataAsync` method, it is used to initialize the system's roles by passing a list of role data to the `InitializeRolesService` method. This ensures that the roles are available for user assignments.

### Responsibilities:
- Initialize and manage user roles.
- Ensure that the application has predefined roles available for user assignments.

---

## **UserService**

### Description:
The `UserService` handles user management tasks, including user creation, password generation, and user initialization. It is used in the `SeedDataAsync` method to create a list of users with randomly generated data using the `Bogus` library. The service's `InitializeUsersService` method is called to save the generated users into the database.

### Responsibilities:
- Generate passwords for new users.
- Initialize users in the application by creating user records.
- Provide utilities for managing user-related operations.

---

## **WarningsService**

### Description:
The `WarningsService` is used for managing and initializing weather warnings, which are associated with specific conditions (e.g., extreme, high, normal, low). In the `SeedDataAsync` method, `InitializeWarningsService` is called to seed warning types into the system. Each warning type has a name and description, and warnings are later linked to weather forecasts.

### Responsibilities:
- Manage weather warnings (e.g., extreme, high, low).
- Initialize and persist warning data.
- Associate warnings with weather forecasts.

---

## **ForecastsService**

### Description:
The `ForecastsService` is responsible for managing weather forecasts. It is used to generate random weather forecast data based on temperature ranges and associated warning levels. In the `SeedDataAsync` method, the service's `InitializeForecastsService` method is used to create and persist forecast data, including temperature, summary, and associated warnings.

### Responsibilities:
- Manage weather forecast data.
- Generate and persist weather forecasts.
- Associate warnings with weather forecasts based on temperature thresholds.

---

## **DataContext**

### Description:
`DataContext` is the Entity Framework database context for the application. It is used throughout the `DataService` to interact with the database and perform CRUD operations on entities such as `Users`, `Roles`, `Warnings`, and `Forecasts`. In methods like `SeedDataAsync` and `ClearDataAsync`, it is used to query, insert, and delete data from the database.

### Responsibilities:
- Interact with the database to manage entities.
- Provide the data storage mechanism for the application.

---

## Methods in `DataService`

### **SeedDataAsync**
This method is responsible for seeding data into the application. It performs the following steps:
- Initializes roles (User, Manager, Administrator).
- Generates and initializes users with random data (using the `Bogus` library).
- Initializes weather warnings (Extreme, High, Normal, Low).
- Generates and initializes weather forecasts based on temperature and associated warnings.

The method returns a `CommandResponse<string>` that indicates whether the data seeding was successful.

### **ClearDataAsync**
This method is responsible for clearing all data from the database. It removes entries from tables such as `Forecasts`, `Warnings`, `Users`, `Roles`, and others. After clearing the data, it saves the changes and returns a `CommandResponse<string>` indicating whether the data deletion was successful.

---

# ErrorsService Documentation

The `ErrorsService` class is responsible for managing and retrieving error logs from the system. It interacts with the database using the `ICommonRepository` and provides services for fetching paginated error data, filtering errors, and retrieving detailed information about specific errors by their unique identifier.

## Dependencies

- **DataContext**: Provides access to the database context for interacting with the `LogErrors` table in the database.
- **ICommonRepository**: This interface abstracts common repository operations like fetching paginated results and retrieving items by their ID.

## Services

###  ICommonRepository

The `ICommonRepository` is used for generic database operations across multiple entities. In the `ErrorsService` class, it is used to perform two key operations:
- Fetching paginated results of errors with optional filtering, sorting, and including related entities.
- Retrieving a single error by its unique identifier (ID).

The repository abstracts the underlying database access logic, allowing `ErrorsService` to focus on business logic and mappings.

#### Responsibilities:
- Retrieve paginated data with filters and sorting.
- Fetch single entities by their ID.
- Provide a way to abstract and reuse common database queries.

### ErrorsFiltrers

`ErrorsFiltrers` is a class used to handle filtering logic for errors in the system. In the `GetErrorsService` method, it checks if filtering parameters are provided in the `pageParams` and, if so, applies the `ErrorSearchFilter`. This filter is used to narrow down the error records based on search criteria, such as specific error names.

#### Responsibilities:
- Provide filtering functionality for error records.
- Allow dynamic search based on user input or query parameters.

### ErrorsIncludes

The `ErrorsIncludes` class is used to define the related entities that should be included when fetching error logs. In the `GetErrorsService` method, it calls the `GetErrorsIncludes()` function to include any necessary related data when querying the `LogErrors` table. This is useful when there are navigation properties that should be loaded along with the main error data.

#### Responsibilities:
- Specify related entities to be included in the query results.
- Support the eager loading of related data to reduce the number of queries.

### ErrorsMappings

In the `ErrorsService`, the `ErrorsMappings` class provides the mapping logic to convert raw error log data from the database (`LogError` entities) into Data Transfer Objects (DTOs) such as `ListItemErrorDto` and `ItemErrorDto`. This conversion is done using extension methods like `ListItemErrorDtoMapping()` and `ItemErrorDtoMapping()`, which ensure that the correct data is presented in a structured format for use by the frontend or API clients.

#### Responsibilities:
- Map database entities (like `LogError`) to DTOs (`ListItemErrorDto`, `ItemErrorDto`).
- Ensure that the data is structured appropriately for the consumer.

## Methods

### `GetErrorsService`

This method is responsible for retrieving a paginated list of error logs from the database. It accepts `UrlQuery` parameters for pagination, filtering, and sorting. Here's a breakdown of its functionality:

- **Filtering**: If the `UrlQuery` has a filter applied (e.g., searching for specific error names), it applies the `ErrorSearchFilter` from the `ErrorsFiltrers` class.
- **Sorting**: By default, the error logs are sorted by error name in ascending order. However, if the query parameters specify a different sorting order, it applies that.
- **Paging**: The results are paginated based on the provided page number and page size.
- **Mapping**: After retrieving the paginated error logs, the method maps each `LogError` entity to a `ListItemErrorDto` object.

It then returns a `ListResponse<List<ListItemErrorDto>>` object that contains the paginated data and metadata like total records, page size, and page number.

### `GetErrorByIdService`

This method is used to retrieve detailed information about a specific error log by its unique identifier (`Guid`). It follows these steps:

- **Search**: It searches for the error in the `LogErrors` table by its ID. If no matching error is found, it throws a `CustomException` with a `NotFound` HTTP status code.
- **Mapping**: If the error is found, it maps the `LogError` entity to an `ItemErrorDto`.
- **Response**: The method returns an `ItemResponse<ItemErrorDto>` object containing the mapped data.

The method allows clients to fetch detailed information about a particular error log, including all the relevant details.

## Conclusion

The `ErrorsService` class provides a set of services for retrieving and managing error log data. It uses the `ICommonRepository` to perform database operations like fetching paginated results and retrieving errors by ID. The service supports filtering, sorting, and paging of error data and ensures that the data is appropriately mapped into DTOs for client use. These methods are useful for applications that need to display or analyze error logs efficiently.


# TelemetryService Documentation

The `TelemetryService` class provides functionalities for retrieving telemetry data from the system. It handles filtering, sorting, paging, and mapping of telemetry records to appropriate Data Transfer Objects (DTOs). The service interacts with the database through the `ICommonRepository` and offers services to retrieve both paginated telemetry records and detailed information about a specific telemetry record.

## Dependencies

- **DataContext**: Provides access to the database context for interacting with the `TelemetryRecords` table in the database.
- **ICommonRepository**: Abstracts common repository operations like fetching paginated results and retrieving items by their ID.

## Services

### ICommonRepository

The `ICommonRepository` is used for common database operations that can be shared across multiple services. In the `TelemetryService` class, it is responsible for:
- Fetching paginated results of telemetry records with optional filtering, sorting, and including related data.
- Retrieving a single telemetry record by its unique identifier (ID).

The repository abstracts the underlying database access logic, allowing the service to focus on business logic, mapping, and handling specific requirements.

#### Responsibilities:
- Fetch paginated results with optional filtering, sorting, and includes.
- Retrieve single entities by their ID.
- Provide a reusable way to interact with the database.

### TelemetryFiltrers

`TelemetryFiltrers` is a class responsible for handling filtering logic related to telemetry records. In the `GetTelemetryService` method, it is used to apply a search filter if the `UrlQuery` contains filtering parameters. The filter checks the telemetry records based on search criteria such as the telemetry name or other relevant fields.

#### Responsibilities:
- Provide the filtering functionality for telemetry records.
- Apply dynamic search criteria based on user input or query parameters.

### TelemetryIncludes

`TelemetryIncludes` is a class used to define related entities to include when fetching telemetry data. In the `GetTelemetryService` method, it calls `GetTelemetryIncludes()` to include any necessary related data, which may be stored in related tables (e.g., related metrics, devices). This helps in reducing the number of database queries and optimizing performance.

#### Responsibilities:
- Specify related entities that should be included in the query results.
- Support eager loading of related data.

###  TelemetryMappings

The `TelemetryMappings` class provides the necessary mapping logic to convert raw telemetry data (from `Telemetry` entities) into Data Transfer Objects (DTOs) like `ListItemTelemetryDto` and `ItemTelemetryDto`. The conversion is done using extension methods such as `ListItemTelemetryDtoMapping()` and `ItemTelemetryDtoMapping()`, ensuring that the data is presented in the correct structure for the API response.

#### Responsibilities:
- Map database entities (`Telemetry`) to DTOs (`ListItemTelemetryDto`, `ItemTelemetryDto`).
- Structure data appropriately for API responses.

## Methods

### `GetTelemetryService`

This method is responsible for retrieving a paginated list of telemetry records from the database. The method accepts `UrlQuery` parameters to handle pagination, filtering, and sorting. The following actions are performed in this method:

- **Sorting**: If no sorting criteria are specified in the `UrlQuery`, the method defaults to sorting by telemetry name in ascending order. If a different sort order is provided, it applies that.
- **Filtering**: If the `UrlQuery` contains filtering criteria (e.g., searching by telemetry name), the method applies the appropriate filter using the `TelemetrySearchFilter` from `TelemetryFiltrers`.
- **Paging**: The telemetry records are retrieved in a paginated manner, based on the `UrlQuery` parameters for page number and page size.
- **Mapping**: The raw `Telemetry` entities are mapped to `ListItemTelemetryDto` objects using the `ListItemTelemetryDtoMapping()` method.

Finally, the method returns a `ListResponse<List<ListItemTelemetryDto>>` containing the paginated telemetry records and metadata such as the total record count, page size, and page number.

### `GetTelemetryByIdService`

This method retrieves detailed information about a specific telemetry record identified by its unique `Guid` ID. The following steps are performed:

- **Search**: The method queries the `TelemetryRecords` table using the provided ID. If the telemetry record is not found, a `CustomException` is thrown with an HTTP status code of `NotFound`.
- **Mapping**: If the telemetry record is found, it is mapped to an `ItemTelemetryDto` using the `ItemTelemetryDtoMapping()` method.
- **Response**: The method returns an `ItemResponse<ItemTelemetryDto>` containing the mapped telemetry data.

This service is useful for clients that need detailed information about a specific telemetry record.

## Conclusion

The `TelemetryService` class provides services for retrieving telemetry data, including:
- A paginated list of telemetry records with filtering, sorting, and paging capabilities.
- Detailed information about a specific telemetry record identified by its ID.

By utilizing the `ICommonRepository` for database interactions, `TelemetryFiltrers` for filtering, and `TelemetryMappings` for mapping entities to DTOs, the service offers efficient ways to manage telemetry data in the system. This ensures that the data is easily consumable for frontend applications and API clients.


# ForecastsService Documentation

The `ForecastsService` class provides the business logic for interacting with weather forecasts. It offers services for retrieving, creating, updating, and deleting forecast records. The service uses a repository pattern to interact with the database and perform operations on forecast and warning entities. The class also handles sorting, filtering, mapping, and pagination for better management of forecast data.

## Dependencies

- **DataContext**: The database context for interacting with the `Forecasts` and `Warnings` tables in the database.
- **ICommonRepository**: Provides common repository operations such as fetching records, creating, and updating entities.
- **ForecastsIncludes**: Specifies related entities to include in queries to optimize data retrieval.
- **ForecastMappings**: Contains mappings to transform raw data into DTOs used for API responses.

## Services

### **GetForecastsService**

This service retrieves a paginated list of forecast records from the database, allowing for filtering and sorting based on query parameters. The following steps are performed in this method:

- **Sorting**: If no sorting is specified in the request, it defaults to sorting by temperature.
- **Filtering**: If filtering parameters are provided, a filter expression is applied to the forecast records to search by specified criteria (e.g., temperature).
- **Paging**: Retrieves forecast data in a paginated manner, using `UrlQuery` parameters to determine the current page and page size.
- **Mapping**: Converts the fetched forecast data to a `ListItemForecastDto` for API consumption.

The service returns a `ListResponse<List<ListItemForecastDto>>` that includes the list of forecasts along with pagination metadata such as total records, page size, and current page.

### **GetForecastByIdService**

This service retrieves a specific forecast record by its unique ID. It performs the following actions:

- **Searching**: Searches for the forecast record with the given `id`. If the record is not found, a `CustomException` with a `NotFound` HTTP status is thrown.
- **Mapping**: Converts the forecast data to an `ItemForecastDto`.
- **Response**: Returns an `ItemResponse<ItemForecastDto>` containing the forecast data.

This service is used to fetch detailed information about a specific forecast record.

### **CreateForecastService**

This service creates a new forecast record in the system. The following steps are involved:

- **Searching Warning**: The method first checks if the provided `WarningId` exists in the database. If not, a `CustomException` with a `NotFound` HTTP status is thrown.
- **Mapping and Saving**: Maps the provided `ForecastDto` to a `Forecast` entity, associating it with the found warning, and adds it to the database.
- **Saving to Database**: The forecast is saved to the database, and a success message is returned indicating that the forecast was successfully created.

The service returns a `CommandResponse<string>` with a success message, indicating the forecast's creation status.

### **InitializeForecastsService**

This service creates multiple forecast records in the system. It iterates over a list of `ForecastDto` objects, performing the following actions for each:

- **Searching Warning**: Checks if the provided `WarningId` exists for each forecast in the database. If a warning is not found, a `CustomException` is thrown.
- **Mapping and Saving**: Maps each `ForecastDto` to a `Forecast` entity and saves it to the database.
- **Batch Saving**: After processing all forecasts, it saves them to the database and returns a message indicating the success or failure of the operation.

The service returns a `CommandResponse<string>` with a success message for the initialized forecasts.

### **UpdateForecastService**

This service updates an existing forecast record. It performs the following actions:

- **Searching Warning**: It first checks if the provided `WarningId` exists in the database. If not, a `CustomException` is thrown.
- **Searching Forecast**: It retrieves the forecast record by its ID. If the forecast is not found, a `CustomException` is thrown.
- **Mapping and Saving**: The provided `ForecastDto` is used to update the existing `Forecast` entity, and the changes are saved to the database.
- **Saving**: If the update is successful, it saves the changes to the database and returns a success message.

The service returns a `CommandResponse<string>` containing a success message for the updated forecast.

### **DeleteForecastService**

This service deletes an existing forecast record by its ID. It performs the following actions:

- **Searching Forecast**: Searches for the forecast record by its ID. If the forecast is not found, a `CustomException` is thrown.
- **Deleting**: The forecast record is removed from the database, and the changes are saved.
- **Saving**: After deletion, it saves the changes to the database and returns a success message indicating that the forecast was deleted.

The service returns a `CommandResponse<string>` containing a success message indicating the deletion status of the forecast.

# WarningsService Documentation

The `WarningsService` class is designed to manage weather-related warnings within the application. It provides a set of services that allow users to retrieve, create, update, and delete warnings, as well as perform various operations related to warning data. The service leverages repository patterns and mappings to interact with the underlying data and provide a clean and consistent API for users.

## Dependencies

- **DataContext**: Provides access to the application's database, particularly for querying and modifying `Warnings` data.
- **ICommonRepository**: A repository that provides common data access operations like fetching, saving, and updating entities.
- **WarningsIncludes**: Defines the related entities that should be included when querying warning records (e.g., associated forecasts).
- **WarningMappings**: Contains mappings for converting warning data into Data Transfer Objects (DTOs) for API responses.

## Services

###  **GetWarningsService**

This service retrieves a paginated list of weather warnings based on query parameters. It performs the following actions:

- **Sorting**: If no sorting is specified, the service defaults to sorting by the warning's name in ascending order.
- **Filtering**: If filter parameters are provided, the service applies filters to search warnings based on specific criteria.
- **Paging**: The service retrieves a page of warnings, taking into account the provided pagination parameters such as page number and page size.
- **Mapping**: After retrieving the warnings from the database, the service maps them into `ListItemWarningDto` objects suitable for API consumption.

The result is returned as a `ListResponse<List<ListItemWarningDto>>` containing the list of warnings and pagination information.

### **GetWarningsPickerService**

This service fetches all weather warnings from the database and returns them as a list of `PickerWarningDto` objects. It does not apply any sorting or filtering, and it is useful when you need to display a simple list of all available warnings in a dropdown or picker control.

The result is returned as an `ItemResponse<List<PickerWarningDto>>`, containing the list of warnings.

###  **GetWarningByIdService**

This service retrieves a specific weather warning by its unique identifier (`id`). It performs the following steps:

- **Searching**: The service searches for the warning with the specified ID. If no such warning is found, it throws a `CustomException` with a `NotFound` HTTP status.
- **Mapping**: Once the warning is found, it is mapped to an `ItemWarningDto` for a detailed API response.

The service returns an `ItemResponse<ItemWarningDto>` containing the details of the requested warning.

### **InitializeWarningsService**

This service allows for the creation of multiple warning records in a batch operation. It processes each `WarningDto` provided in the list:

- **Mapping and Saving**: Each `WarningDto` is mapped to a `Warning` model and added to the database.
- **Error Handling**: If any warning fails to save, a `CustomException` with a `BadRequest` HTTP status is thrown.

The service returns a `CommandResponse<string>` with a success message indicating that the warnings were successfully inserted into the database.

### **CreateWarningService**

This service creates a single new weather warning. The following steps are involved:

- **Mapping and Saving**: The `WarningDto` is mapped to a `Warning` model, which is then added to the database.
- **Saving**: The service attempts to save the new warning to the database, and if successful, a confirmation message is returned. If the save fails, a `CustomException` with a `BadRequest` HTTP status is thrown.

The service returns a `CommandResponse<string>` containing a success message indicating that the warning was successfully created.

### **UpdateWarningService**

This service updates an existing weather warning by its ID. The process is as follows:

- **Searching**: The service searches for the warning with the specified ID. If no warning is found, it throws a `CustomException` with a `NotFound` HTTP status.
- **Mapping and Saving**: The provided `WarningDto` is mapped to the existing `Warning` model, and the changes are saved to the database.
- **Saving**: If the update is successful, a confirmation message is returned. If the update fails, a `CustomException` with a `BadRequest` HTTP status is thrown.

The service returns a `CommandResponse<string>` containing a success message indicating that the warning was updated successfully.

### **DeleteWarningService**

This service deletes a specific weather warning by its ID. The following steps are performed:

- **Searching**: The service searches for the warning with the specified ID. If no such warning is found, it throws a `CustomException` with a `NotFound` HTTP status.
- **Deleting**: Once the warning is found, it is removed from the database, and the changes are saved.
- **Saving**: After deletion, if the operation is successful, a confirmation message is returned. If the deletion fails, a `CustomException` with a `BadRequest` HTTP status is thrown.

The service returns a `CommandResponse<string>` with a success message indicating that the warning was deleted successfully.

