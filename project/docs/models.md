# Domain Models Documentation

This document provides an overview of the domain models and their properties for the `server/src/Domain` module.

---

## Models Overview

### **Auth Models**
Models related to authentication and user roles.

#### **Role**
Represents a user role within the system.

- **Namespace:** `server.src.Domain.Models.Auth`
- **Base Class:** `IdentityRole`

##### Properties:
- **Description** (`string`): Description of the role.
- **IsActive** (`bool`): Indicates if the role is active.

##### Navigation Properties:
- **UserRoles** (`List<UserRole>`): Association with users having this role.

---

#### **User**
Represents a system user.

- **Namespace:** `server.src.Domain.Models.Auth`
- **Base Class:** `IdentityUser`

##### Properties:
- **FirstName** (`string`): User's first name.
- **LastName** (`string`): User's last name.
- **InitialPassword** (`string`): Initial password for the user.
- **Address** (`string`): User's address.
- **ZipCode** (`string`): ZIP code of the address.
- **City** (`string`): City of residence.
- **State** (`string`): State of residence.
- **Country** (`string`): Country of residence.
- **MobilePhoneNumber** (`string`): User's mobile phone number.
- **MobilePhoneNumberConfirmed** (`bool`): Whether the mobile phone number is confirmed.
- **Bio** (`string`): A short bio of the user.
- **DateOfBirth** (`DateTime`): User's date of birth.
- **IsActive** (`bool`): Indicates if the user is active.

##### Navigation Properties:
- **UserRoles** (`List<UserRole>`): Roles assigned to the user.

---

#### **UserRole**
Links a `User` to a `Role`.

- **Namespace:** `server.src.Domain.Models.Auth`
- **Base Class:** `IdentityUserRole<string>`

##### Navigation Properties:
- **User** (`User`): The associated user.
- **Role** (`Role`): The associated role.

---

### **Error Models**
Models for logging and managing errors.

#### **LogError**
Represents an error log entry.

- **Namespace:** `server.src.Domain.Models.Errors`

##### Properties:
- **Id** (`Guid`): Unique identifier for the error.
- **Error** (`string`): Error message.
- **StatusCode** (`int`): HTTP status code.
- **Instance** (`string`): Error instance information.
- **ExceptionType** (`string`): Type of the exception.
- **StackTrace** (`string`): Stack trace details.
- **Timestamp** (`DateTime`): Timestamp of the error.

---

### **Metrics Models**
Models for tracking application metrics.

#### **Telemetry**
Captures telemetry data for requests and responses.

- **Namespace:** `server.src.Domain.Models.Metrics`

##### Properties:
- **Id** (`Guid`): Unique identifier for telemetry entry.
- **Method** (`string`): HTTP method (e.g., GET, POST).
- **Path** (`string`): Request path.
- **StatusCode** (`int`): HTTP status code.
- **ResponseTime** (`long`): Time taken to generate a response.
- **MemoryUsed** (`long`): Memory used during the request.
- **CPUusage** (`double`): CPU usage percentage.
- **RequestBodySize** (`long`): Size of the request body.
- **RequestTimestamp** (`DateTime`): Timestamp of the request.
- **ResponseBodySize** (`long`): Size of the response body.
- **ResponseTimestamp** (`DateTime`): Timestamp of the response.
- **ClientIp** (`string`): Client's IP address.
- **UserAgent** (`string`): User-Agent string of the client.
- **ThreadId** (`string`): Thread ID for the request.

---

### **Weather Models**
Models for weather data and warnings.

#### **Forecast**
Represents a weather forecast.

- **Namespace:** `server.src.Domain.Models.Weather`

##### Properties:
- **Id** (`Guid`): Unique identifier for the forecast.
- **Date** (`DateTime`): Date of the forecast.
- **TemperatureC** (`int`): Forecasted temperature in Celsius.
- **Summary** (`string`): Weather summary.

##### Foreign Keys:
- **WarningId** (`Guid`): ID of the associated warning.

##### Navigation Properties:
- **Warning** (`Warning`): Associated weather warning.

---

#### **Warning**
Represents a weather warning.

- **Namespace:** `server.src.Domain.Models.Weather`

##### Properties:
- **Id** (`Guid`): Unique identifier for the warning.
- **Name** (`string`): Name of the warning.
- **Description** (`string`): Description of the warning.

##### Navigation Properties:
- **Forecasts** (`List<Forecast>`): List of forecasts associated with the warning.

---
