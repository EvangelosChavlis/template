#!/bin/bash

# Exit on error
set -e

cd ..

echo "Running Domain.Unit.Tests"
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Auth.RolesDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Auth.UsersDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Common.CommandResponseTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Common.ItemResponseTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Common.ListResponseTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Common.PaginatedListTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Metrics.ErrorsDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Metrics.TelemetryDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Weather.ForecastsDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Dto.Weather.WarningsDtoTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Extensions.DateTimeExtensionsTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Auth.RoleTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Auth.UserRoleTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Auth.UserTests
dotnet test server/test/Domain.Unit.Tests --filter server.tests.Domain.Unit.Tests.Models.Common.EnvelopeTests
dotnet test server/test/Domain.Unit.Tests --filter server.tests.Domain.Unit.Tests.Models.Common.IncludeThenIncludeTests
dotnet test server/test/Domain.Unit.Tests --filter server.tests.Domain.Unit.Tests.Models.Common.UrlQueryTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Errors.CustomExceptionTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Errors.ErrorDetailsTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Errors.LogErrorTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Metrics.TelemetryTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Weather.ForecastTests
dotnet test server/test/Domain.Unit.Tests --filter server.test.Domain.Unit.Tests.Models.Weather.WarningTests


echo "Running Persistence.Unit.Tests"
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Auth.RoleConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Auth.UserConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Auth.UserRoleConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Metrics.LogErrorConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Metrics.TelemetryConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Weather.ForecastConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Configurations.Weather.WarningConfigurationTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Unit.Tests.Contexts.DataContextTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Unit.Tests.Extensions.DbContextExtensionsTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Unit.Tests.Repositories.CommonRepositoryTests
dotnet test server/test/Persistence.Unit.Tests --filter server.test.Persistence.Unit.Tests.DependencyInjectionTests


echo "Running Application.Unit.Tests"
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Filters.Auth.UserFiltersTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Filters.Metrics.ErrorsFiltersTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Filters.Metrics.TelemetryFiltersTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Filters.Weather.ForecastFiltersTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Filters.Weather.WarningFiltersTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Includes.Auth.UserIncludesTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Includes.Metrics.ErrorsIncludesTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Includes.Metrics.TelemetryIncludesTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Includes.Weather.ForecastsIncludesTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Includes.Weather.WarningsIncludesTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Auth.RolesMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Auth.UsersMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Metrics.ErrorsMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Metrics.TelemetryMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Weather.ForecastsMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.tests.Application.Mappings.Weather.WarningsMappingsTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Auth.RoleServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Auth.UserServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Metrics.ErrorsServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Metrics.TelemetryServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Weather.ForecastsServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Services.Weather.WarningsServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.Enable2FAValidatorTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.ForgotPasswordValidatorTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.ResetPasswordValidatorTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.RoleValidatorsTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.UserLoginValidatorTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.UserValidatorsTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Auth.Verify2FAValidatorTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Weather.ForecastsServiceTests
dotnet test server/test/Application.Unit.Tests --filter server.test.Application.Unit.Tests.Validators.Weather.WarningsServiceTests


echo "Running Api.Integration.Tests"
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Weather.ForecastsControllerTests
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Weather.WarningsControllerTests
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Metrics.ErrorsControllerTests;
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Metrics.TelemetryControllerTests;
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Auth.RolesControllerTests;
dotnet test server/test/Api.Integration.Tests --filter server.test.Api.Integration.Tests.Controllers.Auth.UsersControllerTests;

