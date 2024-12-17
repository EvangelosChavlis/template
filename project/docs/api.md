# API Endpoints Documentation

This section outlines the available API endpoints, providing a summary and detailed description of each operation.

## Data

### GET /api/data/seed
**Summary**: Seed database with initial data  
**Description**: This endpoint populates the database with the initial set of data required for the application. It is typically used during the first setup or re-initialization of the system.

### GET /api/data/clear
**Summary**: Clear all data from the database  
**Description**: This endpoint deletes all data from the database, effectively resetting it to an empty state. This can be useful for testing or when you need to remove all existing records.

---

## Roles

### GET /api/auth/roles
**Summary**: Retrieve all roles  
**Description**: This endpoint fetches all available roles in the system. Roles are used to control access permissions across various resources in the application.

### POST /api/auth/roles
**Summary**: Create a new role  
**Description**: This endpoint allows for the creation of a new role with specific permissions and access levels. The role is added to the system and can later be assigned to users.

### GET /api/auth/roles/{id}
**Summary**: Retrieve a role by ID  
**Description**: This endpoint retrieves a specific role by its unique ID. The details of the role, including permissions, are returned.

### PUT /api/auth/roles/{id}
**Summary**: Update a role  
**Description**: This endpoint allows for the modification of an existing role's details, such as permissions and access control.

### DELETE /api/auth/roles/{id}
**Summary**: Delete a role  
**Description**: This endpoint deletes an existing role from the system. After deletion, users assigned to this role may lose access to certain resources.

### POST /api/auth/roles/initialize
**Summary**: Initialize roles  
**Description**: This endpoint initializes predefined roles in the system. It is used to ensure that the system has all necessary roles set up.

### GET /api/auth/roles/activate/{id}
**Summary**: Activate a role  
**Description**: This endpoint activates a role, enabling it for use within the system. Only active roles can be assigned to users.

### GET /api/auth/roles/deactivate/{id}
**Summary**: Deactivate a role  
**Description**: This endpoint deactivates a role, preventing it from being assigned to new users. Deactivating a role does not affect users who already have it.

---

## Users

### GET /api/auth/users
**Summary**: Retrieve all users  
**Description**: This endpoint retrieves a list of all users in the system, including details such as their roles and status.

### POST /api/auth/users/create
**Summary**: Create a new user  
**Description**: This endpoint allows for the creation of a new user, including the assignment of roles and other necessary details.

### POST /api/auth/users/initialize
**Summary**: Initialize users  
**Description**: This endpoint initializes a set of users in the system, typically used when setting up new environments or when populating the system with default users.

### POST /api/auth/users/logout
**Summary**: User logout  
**Description**: This endpoint logs the current user out of the system, invalidating their authentication token.

### POST /api/auth/users/refresh
**Summary**: Refresh user token  
**Description**: This endpoint refreshes the user's authentication token, extending their session without requiring re-login.

### POST /api/auth/users/forgot-password
**Summary**: Forgot password  
**Description**: This endpoint initiates the password reset process by sending a reset link to the user's registered email address.

### POST /api/auth/users/reset-password
**Summary**: Reset password  
**Description**: This endpoint allows users to reset their password by providing a new password, typically after completing a password recovery process.

### GET /api/auth/users/generate-password/{id}
**Summary**: Generate a new password for a user  
**Description**: This endpoint generates a new password for a specified user, typically used in administrative scenarios.

### POST /api/auth/users/enable-2fa
**Summary**: Enable two-factor authentication for a user  
**Description**: This endpoint enables two-factor authentication (2FA) for a user, enhancing the security of their account.

### POST /api/auth/users/verify-2fa
**Summary**: Verify two-factor authentication for a user  
**Description**: This endpoint verifies the two-factor authentication code provided by the user, completing the authentication process.

### GET /api/auth/users/{id}
**Summary**: Get user by ID  
**Description**: This endpoint retrieves the details of a specific user by their unique ID.

### PUT /api/auth/users/{id}
**Summary**: Update user details  
**Description**: This endpoint updates the details of an existing user, such as email, role, or personal information.

### DELETE /api/auth/users/{id}
**Summary**: Delete user  
**Description**: This endpoint deletes a user from the system, removing all associated data and revoking their access.

### GET /api/auth/users/activate/{id}
**Summary**: Activate a user  
**Description**: This endpoint activates a user account, enabling the user to log in and access the system.

### GET /api/auth/users/deactivate/{id}
**Summary**: Deactivate a user  
**Description**: This endpoint deactivates a user, preventing them from logging in or accessing any system resources.

### GET /api/auth/users/lock/{id}
**Summary**: Lock a user  
**Description**: This endpoint locks a user account, preventing them from accessing the system until unlocked.

### GET /api/auth/users/unlock/{id}
**Summary**: Unlock a user  
**Description**: This endpoint unlocks a user account, restoring their access to the system.

### GET /api/auth/users/confirm/email/{id}
**Summary**: Confirm a user's email  
**Description**: This endpoint confirms a user's email address, ensuring that they have access to the provided email account.

### GET /api/auth/users/confirm/phoneNumber/{id}
**Summary**: Confirm a user's phone number  
**Description**: This endpoint confirms a user's phone number, typically used for multi-factor authentication.

### GET /api/auth/users/confirm/mobilePhoneNumber/{id}
**Summary**: Confirm a user's mobile phone number  
**Description**: This endpoint confirms a user's mobile phone number, usually for sending SMS-based notifications or for 2FA purposes.

### GET /api/auth/users/assign/{userId}/{roleId}
**Summary**: Assign a role to a user  
**Description**: This endpoint assigns a specified role to a user, providing them with the permissions associated with that role.

### POST /api/auth/users/register
**Summary**: Register a new user  
**Description**: This endpoint registers a new user in the system, creating an account with necessary details and sending a confirmation link or email.

### POST /api/auth/users/login
**Summary**: User login  
**Description**: This endpoint allows users to log in to the system using their credentials and receive an authentication token.

---

## Forecasts

### GET /api/weather/forecasts
**Summary**: Get a list of weather forecasts  
**Description**: This endpoint retrieves all available weather forecasts, providing details such as the temperature, location, and forecast type.

### POST /api/weather/forecasts
**Summary**: Create a new weather forecast  
**Description**: This endpoint allows the creation of a new weather forecast, providing the necessary data like temperature, date, and location.

### GET /api/weather/forecasts/{id}
**Summary**: Get a specific weather forecast by ID  
**Description**: This endpoint retrieves a specific weather forecast by its unique ID, including all details associated with the forecast.

### PUT /api/weather/forecasts/{id}
**Summary**: Update an existing weather forecast  
**Description**: This endpoint updates an existing weather forecast's details, such as the forecast's temperature, date, and location.

### DELETE /api/weather/forecasts/{id}
**Summary**: Delete a weather forecast by ID  
**Description**: This endpoint deletes a specific weather forecast based on its unique ID.

### POST /api/weather/forecasts/initialize
**Summary**: Initialize multiple weather forecasts  
**Description**: This endpoint initializes a set of weather forecasts, typically used to populate the system with a predefined set of forecast data.

---

## Warnings

### GET /api/weather/warnings
**Summary**: Get a list of weather warnings  
**Description**: This endpoint retrieves all active weather warnings in the system, providing details such as the warning type, severity, and location.

### POST /api/weather/warnings
**Summary**: Create a new weather warning  
**Description**: This endpoint creates a new weather warning, allowing the system to notify users of hazardous weather conditions.

### GET /api/weather/warnings/{id}
**Summary**: Get a specific weather warning by ID  
**Description**: This endpoint retrieves a specific weather warning by its unique ID, providing all relevant details about the warning.

### PUT /api/weather/warnings/{id}
**Summary**: Update an existing weather warning  
**Description**: This endpoint updates the details of an existing weather warning, such as the severity level or affected areas.

### DELETE /api/weather/warnings/{id}
**Summary**: Delete a weather warning by ID  
**Description**: This endpoint deletes a specific weather warning based on its unique ID.

### GET /api/weather/warnings/picker
**Summary**: Get a list of weather warnings for the picker  
**Description**: This endpoint retrieves a list of weather warnings designed for quick selection by the user interface, typically used in UI pickers or dropdowns.

### POST /api/weather/warnings/initialize
**Summary**: Initialize multiple weather warnings  
**Description**: This endpoint initializes a set of weather warnings in the system, useful for initial setup or populating the system with predefined warnings.

---

## Errors

### GET /api/metrics/errors
**Summary**: Get a list of errors  
**Description**: This endpoint retrieves all recorded errors in the system, providing details such as error type, timestamp, and severity.

### GET /api/metrics/errors/{id}
**Summary**: Get a specific error by ID  
**Description**: This endpoint retrieves a specific error by its unique ID, providing detailed information about the error.

---

## Telemetry

### GET /api/metrics/telemetry
**Summary**: Get a list of telemetry data  
**Description**: This endpoint retrieves all available telemetry data, which includes system performance metrics, usage statistics, and other relevant data points.

### GET /api/metrics/telemetry/{id}
**Summary**: Get specific telemetry data by ID  
**Description**: This endpoint retrieves specific telemetry data based on its unique ID, providing detailed information about the system's performance at a given time.
