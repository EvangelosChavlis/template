# Unit and Integration Tests Documentation

This document provides an overview of the tests being executed by the script. The script runs a series of unit and integration tests across different layers of the application, including **Domain**, **Persistence**, **Application**, and **API** layers.

## Test Categories

The tests are grouped into the following categories:

1. **Domain Unit Tests**: Tests related to domain models, business logic, and domain extensions.
2. **Persistence Unit Tests**: Tests related to database context, repositories, and database configurations.
3. **Application Unit Tests**: Tests related to application services, mappings, and validation logic.
4. **API Integration Tests**: Tests related to the API controllers and their integration with the application logic.

---

## 1. Domain Unit Tests

### Weather Forecast Tests
- **`server.tests.Domain.Models.Weather.ForecastTests`**: Tests related to the weather forecast model and its associated logic.

### Common Tests
- **`server.tests.Domain.Models.Common.EnvelopeTests`**: Tests for the `Envelope` class, which is likely used to wrap responses.
- **`server.tests.Domain.Models.Common.IncludeThenIncludeTests`**: Tests for scenarios involving nested includes in database queries.
- **`server.tests.Domain.Models.Common.UrlQueryTests`**: Tests for query string parsing or URL-based logic.

### Domain Extensions
- **`server.tests.Domain.Extensions.DateTimeExtensionsTests`**: Tests for extension methods related to date and time manipulation.

### Custom Exceptions
- **`server.test.Domain.Unit.Tests.Exceptions.CustomExceptionTests`**: Tests for custom exceptions and their handling in the application.

### Weather Warnings
- **`server.test.Domain.Unit.Tests.Models.Weather.WarningTests`**: Tests related to weather warnings and their functionality.

### Metrics Telemetry
- **`server.test.Domain.Unit.Tests.Models.Metrics.TelemetryTests`**: Tests related to telemetry data and its processing.

### Error Details and Logging
- **`server.test.Domain.Unit.Tests.Models.Errors.ErrorDetailsTests`**: Tests for the `ErrorDetails` class used in the application for error handling.
- **`server.test.Domain.Unit.Tests.Models.Errors.LogErrorTests`**: Tests for logging errors in the system.

### Authentication and Roles
- **`server.test.Domain.Unit.Tests.Models.Auth.RoleTests`**: Tests related to roles in the authentication system.
- **`server.test.Domain.Unit.Tests.Models.Auth.UserRoleTests`**: Tests related to user roles in the authentication system.
- **`server.test.Domain.Unit.Tests.Models.Auth.UserTests`**: Tests related to user models and user-related logic.

### Dto Tests
- **`server.test.Domain.Unit.Tests.Dto.Common.CommandResponseTests`**: Tests for command response objects in the application.
- **`server.test.Domain.Unit.Tests.Dto.Common.ItemResponseTests`**: Tests for item response objects.
- **`server.test.Domain.Unit.Tests.Dto.Common.ListResponseTests`**: Tests for list response objects.
- **`server.test.Domain.Unit.Tests.Dto.Common.PaginatedListTests`**: Tests for paginated list response objects.
- **`server.test.Domain.Unit.Tests.Dto.Auth.RolesDtoTests`**: Tests for data transfer objects related to roles.
- **`server.test.Domain.Unit.Tests.Dto.Auth.UsersDtoTests`**: Tests for data transfer objects related to users.

---

## 2. Persistence Unit Tests

### Dependency Injection and Repositories
- **`server.test.Persistence.Unit.Tests.DependencyInjectionTests`**: Tests for the dependency injection setup in the persistence layer.
- **`server.test.Persistence.Unit.Tests.Repositories.CommonRepositoryTests`**: Tests for common repository functionality.
- **`server.test.Persistence.Unit.Tests.Extensions.DbContextExtensionsTests`**: Tests for database context extension methods.
- **`server.test.Persistence.Unit.Tests.Contexts.DataContextTests`**: Tests for the `DataContext` class, which likely interacts with the database.

### Configuration Tests
- **`server.test.Persistence.Configurations.Weather.ForecastConfigurationTests`**: Tests for the configuration related to the weather forecast.
- **`server.test.Persistence.Configurations.Weather.WarningConfigurationTests`**: Tests for the configuration related to weather warnings.
- **`server.test.Persistence.Configurations.Metrics.LogErrorConfigurationTests`**: Tests for the configuration related to error logging in metrics.
- **`server.test.Persistence.Configurations.Metrics.TelemetryConfigurationTests`**: Tests for the configuration related to telemetry data.
- **`server.test.Persistence.Configurations.Auth.RoleConfigurationTests`**: Tests for the configuration related to authentication roles.
- **`server.test.Persistence.Configurations.Auth.UserConfigurationTests`**: Tests for the configuration related to users in authentication.
- **`server.test.Persistence.Configurations.Auth.UserRoleConfigurationTests`**: Tests for the configuration related to user roles.

---

## 3. Application Unit Tests

### Mappings
- **`server.tests.Application.Mappings.Weather.Collections.ForecastsMappingsTests`**: Tests related to mapping of weather forecast models.
- **`server.tests.Application.Mappings.Weather.Collections.WarningsMappingsTests`**: Tests for mapping of weather warnings models.
- **`server.tests.Application.Mappings.Metrics.ErrorsMappingsTests`**: Tests for mapping of error models in metrics.
- **`server.tests.Application.Mappings.Metrics.TelemetryMappingsTests`**: Tests for mapping of telemetry data.
- **`server.tests.Application.Mappings.Auth.RolesMappingsTests`**: Tests for mapping roles-related data.
- **`server.tests.Application.Mappings.Auth.UsersMappingsTests`**: Tests for mapping users-related data.

### Validators
- **`server.test.Application.Unit.Tests.Validators.Auth.Enable2FAValidatorTests`**: Tests for 2FA enabling validation.
- **`server.Application.Unit.Tests.Validators.Auth.UserLoginValidatorTests`**: Tests for user login validation.
- **`server.test.Application.Unit.Tests.Validators.Auth.ForgotPasswordValidatorTests`**: Tests for forgotten password validation.
- **`server.test.Application.Unit.Tests.Validators.Auth.ResetPasswordValidatorTests`**: Tests for reset password validation.
- **`server.test.Application.Unit.Tests.Validators.Auth.RoleValidatorsTests`**: Tests for role validation in authentication.
- **`server.test.Application.Unit.Tests.Validators.Auth.UserValidatorsTests`**: Tests for user validation in authentication.
- **`server.test.Application.Unit.Tests.Validators.Auth.Verify2FAValidatorTests`**: Tests for 2FA verification validation.

### Services
- **`server.test.Application.Unit.Tests.Services.Metrics.ErrorsServiceTests`**: Tests for error handling services in the metrics domain.
- **`server.test.Application.Unit.Tests.Services.Metrics.TelemetryServiceTests`**: Tests for telemetry data services.

### Includes and Filters
- **`server.test.Application.Unit.Tests.Includes.Weather.Collections.ForecastsIncludesTests`**: Tests for inclusion of related weather forecast data.
- **`server.test.Application.Unit.Tests.Includes.Weather.Collections.WarningsIncludesTests`**: Tests for inclusion of related weather warning data.
- **`server.test.Application.Unit.Tests.Includes.Metrics.TelemetryIncludesTests`**: Tests for inclusion of related telemetry data.
- **`server.test.Application.Unit.Tests.Includes.Errors.ErrorsIncludesTests`**: Tests for inclusion of related error data.
- **`server.test.Application.Unit.Tests.Includes.Auth.UserIncludesTests`**: Tests for inclusion of user-related data in authentication.
- **`server.test.Application.Unit.Tests.Filters.Weather.ForecastFiltersTests`**: Tests for filtering weather forecast data.
- **`server.test.Application.Unit.Tests.Filters.Weather.WarningFiltersTests`**: Tests for filtering weather warning data.
- **`server.test.Application.Unit.Tests.Filters.Metrics.ErrorsFiltersTests`**: Tests for filtering error data in metrics.
- **`server.test.Application.Unit.Tests.Filters.Metrics.TelemetryFiltersTests`**: Tests for filtering telemetry data.
- **`server.test.Application.Unit.Tests.Filters.Auth.UserFiltersTests`**: Tests for filtering user data in authentication.

---

## 4. API Integration Tests

### Weather and Metrics Controllers
- **`server.test.Api.Integration.Tests.Controllers.Weather.Collections.ForecastsControllerTests`**: Integration tests for the weather forecast controller.
- **`server.test.Api.Integration.Tests.Controllers.Weather.Collections.WarningsControllerTests`**: Integration tests for the weather warnings controller.
- **`server.test.Api.Integration.Tests.Controllers.Metrics.ErrorsControllerTests`**: Integration tests for the metrics error handling controller.
- **`server.test.Api.Integration.Tests.Controllers.Metrics.TelemetryControllerTests`**: Integration tests for the telemetry data controller.

### Authentication Controllers
- **`server.test.Api.Integration.Tests.Controllers.Auth.RolesControllerTests`**: Integration tests for the roles controller in authentication.
- **`server.test.Api.Integration.Tests.Controllers.Auth.UsersControllerTests`**: Integration tests for the users controller in authentication.
